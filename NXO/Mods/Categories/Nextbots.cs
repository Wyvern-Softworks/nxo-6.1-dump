using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using NXO.Menu;
using NXO.Utilities;
using Pathfinding;
using UnityEngine;
using UnityEngine.Networking;

namespace NXO.Mods.Categories;

public static class Nextbots
{
	public enum NextbotBehaviour
	{
		Aggressive,
		Ambusher,
		Stalker
	}

	public class NextbotEntity : MonoBehaviour
	{
		public Transform visual;

		public NextbotBehaviour behaviour = NextbotBehaviour.Aggressive;

		public AudioClip ambientClip;

		public AudioClip jumpscareClip;

		private Rigidbody _rb;

		private AudioSource _audio;

		private bool _caught;

		private bool _shaking;

		private GameObject _blackout;

		private Vector3 _lastPos;

		private float _stuckTime;

		private float _escapeTimer;

		private float _escapeSign = 1f;

		private bool _triggered;

		private Seeker _seeker;

		private Path _path;

		private int _waypoint;

		private bool _calculatingPath;

		private float _repathTimer;

		private bool _usePathfinding;

		private CapsuleCollider _col;

		private float _groundDist;

		private float _jumpTimer;

		private int _jumpStage;

		private const float RepathInterval = 0.3f;

		private const float WaypointRadius = 0.9f;

		private const float JumpTrigger = 5f;

		private const float JumpCooldown = 1f;

		private const float CatchRadius = 1.3f;

		private const float SeparationRadius = 3f;

		private const float SeparationWeight = 1.2f;

		private const float Accel = 32f;

		private const float AvoidRange = 4f;

		private const float AvoidWeight = 3f;

		private const float CatchUpRange = 9f;

		private const float CatchUpBoost = 1.4f;

		private const float StuckSpeed = 0.6f;

		private const float StuckTrigger = 0.3f;

		private const float EscapeTime = 1f;

		private const float AmbushRange = 5f;

		private const float StalkerBoost = 1.6f;

		private const float StalkerViewDot = 0.4f;

		private void Halt()
		{
			Vector3 linearVelocity = _rb.linearVelocity;
			Vector3 val = Vector3.MoveTowards(new Vector3(linearVelocity.x, 0f, linearVelocity.z), Vector3.zero, 32f * Time.fixedDeltaTime);
			_rb.linearVelocity = new Vector3(val.x, linearVelocity.y, val.z);
			_stuckTime = 0f;
			_lastPos = ((Component)this).transform.position;
		}

		private bool IsWatched(Vector3 pos)
		{
			Transform val;
			if (!((Object)(object)Variables.Variables_Reference_09 != (Object)null) || !((Object)(object)Variables.Variables_Reference_09.mainCamera != (Object)null))
			{
				if (!((Object)(object)Camera.main != (Object)null))
				{
					if (!((Object)(object)Variables.Variables_Reference_06 != (Object)null))
					{
						val = null;
						if ((Object)(object)val == (Object)null)
						{
							goto Branch_010b;
						}
					}
					else
					{
						val = ((Component)Variables.Variables_Reference_06.headCollider).transform;
						if ((Object)(object)val == (Object)null)
						{
							goto Branch_010b;
						}
					}
				}
				else
				{
					val = ((Component)Camera.main).transform;
					if ((Object)(object)val == (Object)null)
					{
						goto Branch_010b;
					}
				}
			}
			else
			{
				val = Variables.Variables_Reference_09.mainCamera.transform;
				if ((Object)(object)val == (Object)null)
				{
					goto Branch_010b;
				}
			}
			Vector3 val2 = pos - val.position;
			float magnitude = ((Vector3)val2).magnitude;
			if (magnitude < 0.01f)
			{
				return true;
			}
			val2 /= magnitude;
			return Vector3.Dot(val.forward, val2) >= 0.4f;
			Branch_010b:
			return false;
		}

		private Vector3 Whisker(Vector3 pos, Vector3 dir, Vector3 seek, float playerDist)
		{
			float num = Mathf.Min(4f, playerDist - 1.5f);
			RaycastHit val = default(RaycastHit);
			if (!(num <= 0.1f) && Physics.Raycast(pos, dir, out val, num, Variables.GetInteractionLayerMask()))
			{
				Vector3 normal = ((RaycastHit)val).normal;
				normal.y = 0f;
				if (((Vector3)normal).sqrMagnitude < 0.0001f)
				{
					return Vector3.zero;
				}
				((Vector3)normal).Normalize();
				Vector3 val2 = Vector3.Cross(Vector3.up, normal);
				if (Vector3.Dot(val2, seek) < 0f)
				{
					val2 = -val2;
					return (val2 + normal * 0.35f) * (1f - ((RaycastHit)val).distance / 4f);
				}
				return (val2 + normal * 0.35f) * (1f - ((RaycastHit)val).distance / 4f);
			}
			return Vector3.zero;
		}

		private void EndJumpscare()
		{
			if (SpawnRoutine_StateMachine22_State_01)
			{
				Process.GetCurrentProcess().Kill();
			}
			else
			{
				((MonoBehaviour)this).StartCoroutine(FadeOutAndDie());
			}
		}

		private void OnDestroy()
		{
			SpawnRoutine_StateMachine22_Items_01.Remove(((Component)this).gameObject);
			if ((Object)(object)_blackout != (Object)null)
			{
				Renderer component = _blackout.GetComponent<Renderer>();
				if ((Object)(object)component != (Object)null && (Object)(object)component.sharedMaterial != (Object)null)
				{
					Object.Destroy((Object)(object)component.sharedMaterial);
					Object.Destroy((Object)(object)_blackout);
					if ((Object)(object)visual != (Object)null)
					{
						goto Branch_0103;
					}
				}
				else
				{
					Object.Destroy((Object)(object)_blackout);
					if ((Object)(object)visual != (Object)null)
					{
						goto Branch_0103;
					}
				}
			}
			else if ((Object)(object)visual != (Object)null)
			{
				goto Branch_0103;
			}
			if (!((Object)(object)ambientClip != (Object)null))
			{
				goto Branch_017e;
			}
			goto Branch_0153;
			Branch_0103:
			Object.Destroy((Object)(object)((Component)visual).gameObject);
			if (!((Object)(object)ambientClip != (Object)null))
			{
				goto Branch_017e;
			}
			goto Branch_0153;
			Branch_017e:
			if (!((Object)(object)jumpscareClip != (Object)null))
			{
				return;
			}
			Branch_019e:
			if ((Object)(object)jumpscareClip != (Object)(object)ambientClip)
			{
				Object.Destroy((Object)(object)jumpscareClip);
			}
			return;
			Branch_0153:
			Object.Destroy((Object)(object)ambientClip);
			if (!((Object)(object)jumpscareClip != (Object)null))
			{
				return;
			}
			goto Branch_019e;
		}

		private IEnumerator ShakeRoutine()
		{
			if (!((Object)(object)visual == (Object)null) && _shaking)
			{
				do
				{
					visual.localPosition = new Vector3(Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f), 0.5f);
					visual.localRotation = Quaternion.Euler(0f, 0f, Random.Range(-12f, 12f));
					visual.localScale = Vector3.one * Random.Range(0.8f, 1f);
					yield return (object)new WaitForSeconds(Random.Range(0.03f, 0.11f));
				}
				while (_shaking);
			}
		}

		private void Awake()
		{
			_rb = ((Component)this).GetComponent<Rigidbody>();
			_audio = ((Component)this).GetComponent<AudioSource>();
			_seeker = ((Component)this).GetComponent<Seeker>();
			_usePathfinding = (Object)(object)_seeker != (Object)null;
			_col = ((Component)this).GetComponent<CapsuleCollider>();
			_groundDist = (((Object)(object)_col != (Object)null) ? (_col.height * 0.5f) : 1f) + 0.35f;
			_lastPos = ((Component)this).transform.position;
		}

		private void OnPathComplete(Path p)
		{
			_calculatingPath = false;
			if (p.error || p.vectorPath == null || p.vectorPath.Count == 0)
			{
				_path = null;
				return;
			}
			_path = p;
			_waypoint = 0;
		}

		private void Catch()
		{
			_caught = true;
			if ((Object)(object)_rb != (Object)null)
			{
				_rb.constraints = (RigidbodyConstraints)0;
				_rb.linearVelocity = Vector3.zero;
				_rb.isKinematic = true;
				if ((Object)(object)Variables.Variables_Reference_09 != (Object)null)
				{
					goto Branch_008d;
				}
			}
			else if ((Object)(object)Variables.Variables_Reference_09 != (Object)null)
			{
				goto Branch_008d;
			}
			goto Branch_00ac;
			Branch_0143:
			_blackout = GameObject.CreatePrimitive((PrimitiveType)0);
			Object.DestroyImmediate((Object)(object)_blackout.GetComponent<Collider>());
			Transform val;
			_blackout.transform.SetParent(val, false);
			_blackout.transform.localPosition = Vector3.zero;
			_blackout.transform.localScale = Vector3.one * 1.6f;
			Shader val2 = Shader.Find("Sprites/Default") ?? Shader.Find("Unlit/Color") ?? Shader.Find("Standard");
			_blackout.GetComponent<Renderer>().material = new Material(val2)
			{
				color = Color.black,
				renderQueue = 3000
			};
			AudioSource audio;
			if ((Object)(object)visual != (Object)null)
			{
				visual.SetParent(val, false);
				visual.localPosition = new Vector3(0f, 0f, 0.5f);
				visual.localRotation = Quaternion.identity;
				visual.localScale = Vector3.one * 0.9f;
				audio = _audio;
				if ((Object)(object)audio != (Object)null)
				{
					goto Branch_02b8;
				}
			}
			else
			{
				audio = _audio;
				if ((Object)(object)audio != (Object)null)
				{
					goto Branch_02b8;
				}
			}
			_shaking = true;
			((MonoBehaviour)this).StartCoroutine(ShakeRoutine());
			((MonoBehaviour)this).Invoke("EndJumpscare", 5f);
			return;
			Branch_02b8:
			audio.spatialBlend = 0f;
			audio.volume = 1f;
			audio.loop = true;
			if ((Object)(object)jumpscareClip != (Object)null)
			{
				audio.Stop();
				audio.clip = jumpscareClip;
				if ((Object)(object)audio.clip != (Object)null)
				{
					goto Branch_0347;
				}
			}
			else if ((Object)(object)audio.clip != (Object)null)
			{
				goto Branch_0347;
			}
			goto Branch_03a2;
			Branch_008d:
			if (!((Object)(object)Variables.Variables_Reference_09.mainCamera != (Object)null))
			{
				goto Branch_00ac;
			}
			val = Variables.Variables_Reference_09.mainCamera.transform;
			if (!((Object)(object)val == (Object)null))
			{
				goto Branch_0143;
			}
			return;
			Branch_03a2:
			_shaking = true;
			((MonoBehaviour)this).StartCoroutine(ShakeRoutine());
			((MonoBehaviour)this).Invoke("EndJumpscare", 5f);
			return;
			Branch_00ac:
			if (!((Object)(object)Variables.Variables_Reference_06 != (Object)null))
			{
				val = null;
				if ((Object)(object)val == (Object)null)
				{
					return;
				}
			}
			else
			{
				val = ((Component)Variables.Variables_Reference_06.headCollider).transform;
				if ((Object)(object)val == (Object)null)
				{
					return;
				}
			}
			goto Branch_0143;
			Branch_0347:
			if (audio.isPlaying)
			{
				goto Branch_03a2;
			}
			audio.Play();
			_shaking = true;
			((MonoBehaviour)this).StartCoroutine(ShakeRoutine());
			((MonoBehaviour)this).Invoke("EndJumpscare", 5f);
		}

		private bool IsGrounded(Vector3 pos)
		{
			return Physics.Raycast(pos, Vector3.down, _groundDist, Variables.GetInteractionLayerMask());
		}

		private void HaltInstant()
		{
			_rb.linearVelocity = new Vector3(0f, _rb.linearVelocity.y, 0f);
			_stuckTime = 0f;
			_lastPos = ((Component)this).transform.position;
		}

		private void FixedUpdate()
		{
			if (_caught || (Object)(object)Variables.Variables_Reference_06 == (Object)null || (Object)(object)_rb == (Object)null)
			{
				return;
			}
			Vector3 position = ((Component)this).transform.position;
			Vector3 val = ((Component)Variables.Variables_Reference_06.headCollider).transform.position - position;
			val.y = 0f;
			float magnitude = ((Vector3)val).magnitude;
			if (magnitude < 0.001f)
			{
				_rb.linearVelocity = new Vector3(0f, _rb.linearVelocity.y, 0f);
				return;
			}
			Vector3 val2 = val / magnitude;
			float num = 1f;
			switch (behaviour)
			{
			case NextbotBehaviour.Ambusher:
				if (!_triggered)
				{
					if (!(magnitude < 5f))
					{
						Halt();
						return;
					}
					_triggered = true;
				}
				break;
			case NextbotBehaviour.Stalker:
				if (IsWatched(position))
				{
					HaltInstant();
					return;
				}
				num = 1.6f;
				break;
			}
			bool flag = false;
			Vector3 val4;
			int num2;
			if (_usePathfinding)
			{
				_repathTimer -= Time.fixedDeltaTime;
				if (_repathTimer <= 0f && !_calculatingPath && (Object)(object)AstarPath.active != (Object)null)
				{
					_repathTimer = 0.3f;
					_calculatingPath = true;
					_seeker.StartPath(position, ((Component)Variables.Variables_Reference_06.headCollider).transform.position, new OnPathDelegate(OnPathComplete));
					if (HasLineOfSight(position))
					{
						goto Branch_02f3;
					}
				}
				else if (HasLineOfSight(position))
				{
					goto Branch_02f3;
				}
				if (_path == null || _path.vectorPath.Count <= 0)
				{
					goto Branch_0470;
				}
				if (_waypoint < _path.vectorPath.Count - 1)
				{
					while (HorizontalDistanceSquared(position, _path.vectorPath[_waypoint]) < 0.80999994f)
					{
						_waypoint++;
						if (_waypoint < _path.vectorPath.Count - 1)
						{
							continue;
						}
						break;
					}
				}
				Vector3 val3 = _path.vectorPath[_waypoint] - position;
				val3.y = 0f;
				if (((Vector3)val3).sqrMagnitude > 0.0001f)
				{
					val2 = ((Vector3)val3).normalized;
					flag = true;
					val4 = Vector3.zero;
					num2 = 0;
				}
				else
				{
					val4 = Vector3.zero;
					num2 = 0;
				}
			}
			else
			{
				val4 = Vector3.zero;
				num2 = 0;
			}
			goto Branch_05e2;
			Branch_0cee:
			_stuckTime = 0f;
			return;
			Branch_0b7d:
			_jumpStage = 0;
			Vector3 val5 = position - _lastPos;
			float num3 = ((Vector3)val5).magnitude / Time.fixedDeltaTime;
			_lastPos = position;
			if (_escapeTimer <= 0f)
			{
				goto Branch_0c04;
			}
			goto Branch_0cee;
			Branch_0470:
			val4 = Vector3.zero;
			num2 = 0;
			goto Branch_05e2;
			Branch_0c04:
			if (num3 >= 0.6f || !(magnitude > 1.8f))
			{
				goto Branch_0cee;
			}
			_stuckTime += Time.fixedDeltaTime;
			if (_stuckTime > 0.3f)
			{
				_escapeSign = PickEscapeSide(position, val2);
				_escapeTimer = 1f;
				if (_usePathfinding)
				{
					_repathTimer = 0f;
					_path = null;
					_stuckTime = 0f;
				}
				else
				{
					_stuckTime = 0f;
				}
			}
			return;
			Branch_0981:
			_jumpTimer -= Time.fixedDeltaTime;
			float num7;
			if (_jumpTimer <= 0f && IsGrounded(position))
			{
				_jumpTimer = 1f;
				_jumpStage = Mathf.Min(_jumpStage + 1, 3);
				if (_jumpStage != 1)
				{
					if (_jumpStage != 2)
					{
						float num4 = 1f;
						float num5 = Mathf.Max(0.1f, 0f - Physics.gravity.y);
						float num6 = Mathf.Sqrt(2f * num5 * (num7 + 0.5f)) * num4;
						Vector3 linearVelocity = _rb.linearVelocity;
						_rb.linearVelocity = new Vector3(linearVelocity.x, num6, linearVelocity.z);
					}
					else
					{
						float num4 = 0.75f;
						float num5 = Mathf.Max(0.1f, 0f - Physics.gravity.y);
						float num6 = Mathf.Sqrt(2f * num5 * (num7 + 0.5f)) * num4;
						Vector3 linearVelocity = _rb.linearVelocity;
						_rb.linearVelocity = new Vector3(linearVelocity.x, num6, linearVelocity.z);
					}
				}
				else
				{
					float num4 = 0.5f;
					float num5 = Mathf.Max(0.1f, 0f - Physics.gravity.y);
					float num6 = Mathf.Sqrt(2f * num5 * (num7 + 0.5f)) * num4;
					Vector3 linearVelocity = _rb.linearVelocity;
					_rb.linearVelocity = new Vector3(linearVelocity.x, num6, linearVelocity.z);
				}
			}
			val5 = position - _lastPos;
			num3 = ((Vector3)val5).magnitude / Time.fixedDeltaTime;
			_lastPos = position;
			if (_escapeTimer <= 0f)
			{
				goto Branch_0c04;
			}
			goto Branch_0cee;
			Branch_05e2:
			if (num2 < SpawnRoutine_StateMachine22_Items_01.Count)
			{
				while (true)
				{
					GameObject val6 = SpawnRoutine_StateMachine22_Items_01[num2];
					if (!((Object)(object)val6 == (Object)null) && !((Object)(object)val6 == (Object)(object)((Component)this).gameObject))
					{
						Vector3 val7 = position - val6.transform.position;
						val7.y = 0f;
						float magnitude2 = ((Vector3)val7).magnitude;
						if (!(magnitude2 <= 0.001f) && magnitude2 < 3f)
						{
							val4 += val7 / (magnitude2 * magnitude2);
							num2++;
							if (num2 >= SpawnRoutine_StateMachine22_Items_01.Count)
							{
								break;
							}
						}
						else
						{
							num2++;
							if (num2 >= SpawnRoutine_StateMachine22_Items_01.Count)
							{
								break;
							}
						}
					}
					else
					{
						num2++;
						if (num2 >= SpawnRoutine_StateMachine22_Items_01.Count)
						{
							break;
						}
					}
				}
			}
			Vector3 val9;
			if (_escapeTimer > 0f)
			{
				_escapeTimer -= Time.fixedDeltaTime;
				Vector3 val8 = Vector3.Cross(Vector3.up, val2) * _escapeSign;
				val9 = val2 * 0.4f + val8;
				val9.y = 0f;
				if (((Vector3)val9).sqrMagnitude > 0.0001f)
				{
					goto Branch_07c4;
				}
			}
			else if (!flag)
			{
				Vector3 val10 = Whisker(position, val2, val2, magnitude) + Whisker(position, Quaternion.Euler(0f, 35f, 0f) * val2, val2, magnitude) * 0.5f + Whisker(position, Quaternion.Euler(0f, -35f, 0f) * val2, val2, magnitude) * 0.5f;
				val9 = val2 + val4 * 1.2f + val10 * 3f;
				val9.y = 0f;
				if (((Vector3)val9).sqrMagnitude > 0.0001f)
				{
					goto Branch_07c4;
				}
			}
			else
			{
				Vector3 val10 = Vector3.zero;
				val9 = val2 + val4 * 1.2f + val10 * 3f;
				val9.y = 0f;
				if (((Vector3)val9).sqrMagnitude > 0.0001f)
				{
					goto Branch_07c4;
				}
			}
			float num8 = SpawnRoutine_StateMachine22_Value_01 * num * ((magnitude > 9f) ? 1.4f : 1f);
			Vector3 val11 = new Vector3(_rb.linearVelocity.x, 0f, _rb.linearVelocity.z);
			val11 = Vector3.MoveTowards(val11, val9 * num8, 32f * Time.fixedDeltaTime);
			_rb.linearVelocity = new Vector3(val11.x, _rb.linearVelocity.y, val11.z);
			num7 = ((Component)Variables.Variables_Reference_06.headCollider).transform.position.y - position.y;
			if (!(num7 > 5f))
			{
				goto Branch_0b7d;
			}
			goto Branch_0981;
			Branch_02f3:
			flag = true;
			goto Branch_0470;
			Branch_07c4:
			((Vector3)val9).Normalize();
			num8 = SpawnRoutine_StateMachine22_Value_01 * num * ((magnitude > 9f) ? 1.4f : 1f);
			val11 = new Vector3(_rb.linearVelocity.x, 0f, _rb.linearVelocity.z);
			val11 = Vector3.MoveTowards(val11, val9 * num8, 32f * Time.fixedDeltaTime);
			_rb.linearVelocity = new Vector3(val11.x, _rb.linearVelocity.y, val11.z);
			num7 = ((Component)Variables.Variables_Reference_06.headCollider).transform.position.y - position.y;
			if (!(num7 > 5f))
			{
				goto Branch_0b7d;
			}
			goto Branch_0981;
		}

		private static float HorizontalDistanceSquared(Vector3 a, Vector3 b)
		{
			float num = a.x - b.x;
			float num2 = a.z - b.z;
			return num * num + num2 * num2;
		}

		private void UpdateAmbientAudio()
		{
			if ((Object)(object)_audio == (Object)null || (Object)(object)_audio.clip == (Object)null)
			{
				return;
			}
			bool shouldPlay = behaviour == NextbotBehaviour.Aggressive ||
				(behaviour == NextbotBehaviour.Ambusher && _triggered);
			if (shouldPlay)
			{
				if (!_audio.isPlaying)
				{
					_audio.Play();
				}
			}
			else if (_audio.isPlaying)
			{
				_audio.Stop();
			}
		}

		private IEnumerator FadeOutAndDie()
		{
			_shaking = false;
			GifAnimator gif = ((Component)this).GetComponent<GifAnimator>();
			if ((Object)(object)gif != (Object)null)
			{
				((Behaviour)gif).enabled = false;
			}

			Renderer visualRenderer = ((Object)(object)visual != (Object)null) ? ((Component)visual).GetComponent<Renderer>() : null;
			Material visualMaterial = ((Object)(object)visualRenderer != (Object)null) ? visualRenderer.sharedMaterial : null;
			if ((Object)(object)visualMaterial != (Object)null)
			{
				visualMaterial.color = Color.red;
			}

			Renderer blackoutRenderer = ((Object)(object)_blackout != (Object)null) ? _blackout.GetComponent<Renderer>() : null;
			Material blackoutMaterial = ((Object)(object)blackoutRenderer != (Object)null) ? blackoutRenderer.sharedMaterial : null;
			yield return (object)new WaitForSeconds(0.5f);

			for (float elapsed = 0f; elapsed < 1.5f; elapsed += Time.deltaTime)
			{
				float alpha = Mathf.Clamp01(1f - elapsed / 1.5f);
				if ((Object)(object)visualMaterial != (Object)null)
				{
					Color color = visualMaterial.color;
					color.a = alpha;
					visualMaterial.color = color;
				}
				if ((Object)(object)blackoutMaterial != (Object)null)
				{
					Color color = blackoutMaterial.color;
					color.a = alpha;
					blackoutMaterial.color = color;
				}
				if ((Object)(object)_audio != (Object)null)
				{
					_audio.volume = alpha;
				}
				yield return null;
			}
			Object.Destroy((Object)(object)((Component)this).gameObject);
		}

		private bool HasLineOfSight(Vector3 pos)
		{
			Vector3 val = ((Component)Variables.Variables_Reference_06.headCollider).transform.position - pos;
			float magnitude = ((Vector3)val).magnitude;
			if (magnitude < 0.5f)
			{
				return true;
			}
			return !Physics.Raycast(pos, val / magnitude, magnitude - 0.5f, Variables.GetInteractionLayerMask());
		}

		private float PickEscapeSide(Vector3 pos, Vector3 seek)
		{
			int num = Variables.GetInteractionLayerMask();
			bool rightBlocked = Physics.Raycast(pos, Quaternion.Euler(0f, 55f, 0f) * seek, 4f, num);
			bool leftBlocked = Physics.Raycast(pos, Quaternion.Euler(0f, -55f, 0f) * seek, 4f, num);
			if (leftBlocked && !rightBlocked)
			{
				return 1f;
			}
			if (rightBlocked && !leftBlocked)
			{
				return -1f;
			}
			if (!(Random.value < 0.5f))
			{
				return -1f;
			}
			return 1f;
		}

		private void Update()
		{
			if (_caught || (Object)(object)Variables.Variables_Reference_06 == (Object)null)
			{
				return;
			}
			UpdateAmbientAudio();
			if (Vector3.Distance(((Component)this).transform.position, ((Component)Variables.Variables_Reference_06.headCollider).transform.position) < 1.3f)
			{
				Catch();
			}
			else
			{
				if (!((Object)(object)visual != (Object)null))
				{
					return;
				}
				Vector3 val = ((Component)Variables.Variables_Reference_06.headCollider).transform.position - visual.position;
				if (((Vector3)val).sqrMagnitude > 0.0001f)
				{
					Quaternion val2 = Quaternion.LookRotation(val) * Quaternion.Euler(0f, 180f, 0f);
					if (!((Object)(object)_rb != (Object)null))
					{
						float num = Mathf.Clamp(Vector3.Dot(Vector3.zero, visual.right), -2f, 2f) * 6f;
						float num2 = Mathf.Sin(Time.time * 5f) * 2.5f;
						visual.rotation = val2 * Quaternion.Euler(0f, 0f, num + num2);
					}
					else
					{
						float num = Mathf.Clamp(Vector3.Dot(_rb.linearVelocity, visual.right), -2f, 2f) * 6f;
						float num2 = Mathf.Sin(Time.time * 5f) * 2.5f;
						visual.rotation = val2 * Quaternion.Euler(0f, 0f, num + num2);
					}
				}
			}
		}
	}

	[CompilerGenerated]
	private sealed class SpawnRoutine_StateMachine22 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int State;

		private object Current;

		public string imageUrl;

		public string audioUrl;

		public float speed;

		public float size;

		public string name;

		public string jumpscareUrl;

		private List<GifDecoder.Frame> framesCaptured1;

		private AudioClip clipCaptured2;

		private AudioClip jumpClipCaptured3;

		private string labelCaptured4;

		private UnityWebRequest reqCaptured5;

		private byte[] dataCaptured6;

		private Texture2D tCaptured7;

		private UnityWebRequest reqCaptured8;

		private UnityWebRequest reqCaptured9;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return Current;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return Current;
			}
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		private void Finally3()
		{
			State = -1;
			if (reqCaptured9 != null)
			{
				((IDisposable)reqCaptured9).Dispose();
			}
		}

		private void Finally2()
		{
			State = -1;
			if (reqCaptured8 != null)
			{
				((IDisposable)reqCaptured8).Dispose();
			}
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			int num = State - -5;
			num = (((uint)num <= 8u) ? num : 9) + 16;
			int num2 = num;
			if (num2 == 17)
			{
				try
				{
				}
				catch (Exception)
				{
					Finally2();
					return;
				}
			}
			else
			{
				try
				{
				}
				catch (Exception)
				{
					Finally3();
					return;
				}
			}
			framesCaptured1 = null;
			clipCaptured2 = null;
			jumpClipCaptured3 = null;
			labelCaptured4 = null;
			reqCaptured5 = null;
			dataCaptured6 = null;
			tCaptured7 = null;
			reqCaptured8 = null;
			reqCaptured9 = null;
			State = -2;
		}

		private void Finally1()
		{
			State = -1;
			if (reqCaptured5 != null)
			{
				((IDisposable)reqCaptured5).Dispose();
			}
		}

		private bool MoveNext()
		{
			bool result;
			try
			{
				int num = State;
				num = (((uint)num <= 3u) ? num : 4) + 69;
				int num2 = num;
				if (num2 == 70)
				{
					State = -3;
					if ((int)reqCaptured5.result == 1 && !(reqCaptured5.GetResponseHeader("Content-Type") ?? "").Contains("text/html"))
					{
						dataCaptured6 = reqCaptured5.downloadHandler.data;
						if (GifDecoder.IsGif(dataCaptured6))
						{
							framesCaptured1 = GifDecoder.DecodeFrames(dataCaptured6);
							dataCaptured6 = null;
							if (framesCaptured1 != null)
							{
								goto Branch_0298;
							}
						}
						else
						{
							tCaptured7 = new Texture2D(2, 2);
							if (ImageConversion.LoadImage(tCaptured7, dataCaptured6))
							{
								framesCaptured1 = new List<GifDecoder.Frame>
								{
									new GifDecoder.Frame
									{
										texture = tCaptured7,
										delay = 0f
									}
								};
								tCaptured7 = null;
								dataCaptured6 = null;
								if (framesCaptured1 != null)
								{
									goto Branch_0298;
								}
							}
							else
							{
								Object.Destroy((Object)(object)tCaptured7);
								tCaptured7 = null;
								dataCaptured6 = null;
								if (framesCaptured1 != null)
								{
									goto Branch_0298;
								}
							}
						}
					}
					else if (framesCaptured1 != null)
					{
						goto Branch_0298;
					}
					goto Branch_02ce;
				}
				State = -1;
				framesCaptured1 = null;
				clipCaptured2 = null;
				jumpClipCaptured3 = null;
				labelCaptured4 = (string.IsNullOrEmpty(name) ? "" : (name + " - "));
				if (string.IsNullOrEmpty(imageUrl))
				{
					if (string.IsNullOrEmpty(audioUrl))
					{
						goto Branch_03c2;
					}
					goto Branch_0364;
				}
				reqCaptured5 = UnityWebRequest.Get(imageUrl);
				State = -3;
				Current = reqCaptured5.SendWebRequest();
				State = 1;
				result = true;
				goto EndBranch_0000;
				Branch_0364:
				reqCaptured8 = UnityWebRequestMultimedia.GetAudioClip(audioUrl, GetAudioTypeFromUrl(audioUrl));
				State = -4;
				((DownloadHandlerAudioClip)reqCaptured8.downloadHandler).streamAudio = false;
				Current = reqCaptured8.SendWebRequest();
				State = 2;
				result = true;
				goto EndBranch_0000;
				Branch_03c2:
				if (!string.IsNullOrEmpty(jumpscareUrl))
				{
					reqCaptured9 = UnityWebRequestMultimedia.GetAudioClip(jumpscareUrl, GetAudioTypeFromUrl(jumpscareUrl));
					State = -5;
					((DownloadHandlerAudioClip)reqCaptured9.downloadHandler).streamAudio = false;
					Current = reqCaptured9.SendWebRequest();
					State = 3;
					result = true;
				}
				else
				{
					CreateNextbotEntity(framesCaptured1, clipCaptured2, jumpClipCaptured3, speed, size);
					result = false;
				}
				goto EndBranch_0000;
				Branch_02ce:
				NotificationLib.ShowNotification(NotificationLib.NotificationType.Error, labelCaptured4 + "Image link not working, double check it");
				Finally1();
				reqCaptured5 = null;
				if (string.IsNullOrEmpty(audioUrl))
				{
					goto Branch_03c2;
				}
				goto Branch_0364;
				Branch_0298:
				if (framesCaptured1.Count == 0)
				{
					goto Branch_02ce;
				}
				Finally1();
				reqCaptured5 = null;
				if (string.IsNullOrEmpty(audioUrl))
				{
					goto Branch_03c2;
				}
				goto Branch_0364;
				EndBranch_0000:;
			}
			catch (Exception)
			{
				((IDisposable)this).Dispose();
				bool result2 = default(bool);
				return result2;
			}
			return result;
		}

		bool IEnumerator.MoveNext()
		{
			return this.MoveNext();
		}

		[DebuggerHidden]
		public SpawnRoutine_StateMachine22(int State)
		{
			this.State = State;
		}
	}

	public static readonly List<GameObject> SpawnRoutine_StateMachine22_Items_01 = new List<GameObject>();

	private static PhysicsMaterial SpawnRoutine_StateMachine22_Material_01;

	public static bool SpawnRoutine_StateMachine22_State_01 = false;

	public static ButtonHandler.Button SpawnRoutine_StateMachine22_Button_01;

	private static readonly (float speed, string desc)[] Recovered_Reference_25 = new(float, string)[5]
	{
		(2f, "Slow"),
		(3.5f, "Normal"),
		(5f, "Fast"),
		(7.5f, "Very Fast"),
		(11f, "Insane")
	};

	private static int SpawnRoutine_StateMachine22_Index_02 = 1;

	public static float SpawnRoutine_StateMachine22_Value_01 = 3.5f;

	public static string SpawnRoutine_StateMachine22_Text_01 = "Normal";

	public static ButtonHandler.Button SpawnRoutine_StateMachine22_Button_02;

	private static readonly NextbotBehaviour[] SpawnRoutine_StateMachine22_Values_01 = new NextbotBehaviour[3]
	{
		NextbotBehaviour.Aggressive,
		NextbotBehaviour.Ambusher,
		NextbotBehaviour.Stalker
	};

	private static int SpawnRoutine_StateMachine22_Index_01 = 0;

	public static NextbotBehaviour SpawnRoutine_StateMachine22_Reference_01 = NextbotBehaviour.Aggressive;

	private static PhysicsMaterial SlipMaterial
	{
		get
		{
			object obj = SpawnRoutine_StateMachine22_Material_01;
			if (obj == null)
			{
				PhysicsMaterial val = new PhysicsMaterial("NextbotSlip")
				{
					dynamicFriction = 0f,
					staticFriction = 0f,
					bounciness = 0f,
					frictionCombine = (PhysicsMaterialCombine)2,
					bounceCombine = (PhysicsMaterialCombine)2
				};
				SpawnRoutine_StateMachine22_Material_01 = val;
				obj = (object)val;
			}
			return (PhysicsMaterial)obj;
		}
	}

	private static string NormalizeAssetUrl(string url)
	{
		if (string.IsNullOrEmpty(url))
		{
			return url;
		}
		string text = url.Trim();
		url = text;
		if (url.Contains("github.com") && url.Contains("/blob/"))
		{
			string text2 = url.Replace("github.com", "raw.githubusercontent.com").Replace("/blob/", "/");
			url = text2;
			return url;
		}
		return url;
	}

	private static AudioType GetAudioTypeFromUrl(string url)
	{
		string text = url.ToLower();
		if (text.Contains(".wav"))
		{
			return (AudioType)20;
		}
		if (text.Contains(".ogg"))
		{
			return (AudioType)14;
		}
		return (AudioType)13;
	}

	public static void CycleNextbotBehaviour(bool forward)
	{
		if (!forward)
		{
			SpawnRoutine_StateMachine22_Index_01 = (SpawnRoutine_StateMachine22_Index_01 - 1 + SpawnRoutine_StateMachine22_Values_01.Length) % SpawnRoutine_StateMachine22_Values_01.Length;
			SpawnRoutine_StateMachine22_Reference_01 = SpawnRoutine_StateMachine22_Values_01[SpawnRoutine_StateMachine22_Index_01];
			SpawnRoutine_StateMachine22_Button_02?.SetText($"Behaviour : {SpawnRoutine_StateMachine22_Reference_01}");
		}
		else
		{
			SpawnRoutine_StateMachine22_Index_01 = (SpawnRoutine_StateMachine22_Index_01 + 1) % SpawnRoutine_StateMachine22_Values_01.Length;
			SpawnRoutine_StateMachine22_Reference_01 = SpawnRoutine_StateMachine22_Values_01[SpawnRoutine_StateMachine22_Index_01];
			SpawnRoutine_StateMachine22_Button_02?.SetText($"Behaviour : {SpawnRoutine_StateMachine22_Reference_01}");
		}
	}

	public static void SpawnNextbot(string imageUrl, string audioUrl, float speed = 3.5f, float size = 2f, string name = "", string jumpscareUrl = "")
	{
		if (!((Object)(object)CoroutineHelper.Instance == (Object)null))
		{
			((MonoBehaviour)CoroutineHelper.Instance).StartCoroutine(SpawnRoutine(NormalizeAssetUrl(imageUrl), NormalizeAssetUrl(audioUrl), speed, size, name, NormalizeAssetUrl(jumpscareUrl)));
		}
	}

	public static void SelectNextbotSpeed(string desc)
	{
		int num = 0;
		if (num >= Recovered_Reference_25.Length)
		{
			return;
		}
		do
		{
			if (Recovered_Reference_25[num].desc == desc)
			{
				SpawnRoutine_StateMachine22_Index_02 = num;
				(SpawnRoutine_StateMachine22_Value_01, SpawnRoutine_StateMachine22_Text_01) = Recovered_Reference_25[num];
				SpawnRoutine_StateMachine22_Button_01?.SetText("Nextbot Speed : " + SpawnRoutine_StateMachine22_Text_01);
				break;
			}
			num++;
		}
		while (num < Recovered_Reference_25.Length);
	}

	[IteratorStateMachine(typeof(SpawnRoutine_StateMachine22))]
	private static IEnumerator SpawnRoutine(string imageUrl, string audioUrl, float speed, float size, string name, string jumpscareUrl)
	{
		return new SpawnRoutine_StateMachine22(0)
		{
			imageUrl = imageUrl,
			audioUrl = audioUrl,
			speed = speed,
			size = size,
			name = name,
			jumpscareUrl = jumpscareUrl
		};
	}

	public static void ClearNextbots()
	{
		int num = 0;
		if (num < SpawnRoutine_StateMachine22_Items_01.Count)
		{
			while (true)
			{
				if ((Object)(object)SpawnRoutine_StateMachine22_Items_01[num] != (Object)null)
				{
					Object.Destroy((Object)(object)SpawnRoutine_StateMachine22_Items_01[num]);
					num++;
					if (num >= SpawnRoutine_StateMachine22_Items_01.Count)
					{
						break;
					}
				}
				else
				{
					num++;
					if (num >= SpawnRoutine_StateMachine22_Items_01.Count)
					{
						break;
					}
				}
			}
		}
		SpawnRoutine_StateMachine22_Items_01.Clear();
		NotificationLib.ShowNotification(NotificationLib.NotificationType.Disabled, "Cleared Nextbots");
	}

	public static void SelectNextbotBehaviour(string name)
	{
		int num = 0;
		if (num >= SpawnRoutine_StateMachine22_Values_01.Length)
		{
			return;
		}
		do
		{
			if (SpawnRoutine_StateMachine22_Values_01[num].ToString() == name)
			{
				SpawnRoutine_StateMachine22_Index_01 = num;
				SpawnRoutine_StateMachine22_Reference_01 = SpawnRoutine_StateMachine22_Values_01[num];
				SpawnRoutine_StateMachine22_Button_02?.SetText($"Behaviour : {SpawnRoutine_StateMachine22_Reference_01}");
				break;
			}
			num++;
		}
		while (num < SpawnRoutine_StateMachine22_Values_01.Length);
	}

	public static void CycleNextbotSpeed(bool forward)
	{
		if (!forward)
		{
			SpawnRoutine_StateMachine22_Index_02 = (SpawnRoutine_StateMachine22_Index_02 - 1 + Recovered_Reference_25.Length) % Recovered_Reference_25.Length;
			(SpawnRoutine_StateMachine22_Value_01, SpawnRoutine_StateMachine22_Text_01) = Recovered_Reference_25[SpawnRoutine_StateMachine22_Index_02];
			SpawnRoutine_StateMachine22_Button_01?.SetText("Nextbot Speed : " + SpawnRoutine_StateMachine22_Text_01);
		}
		else
		{
			SpawnRoutine_StateMachine22_Index_02 = (SpawnRoutine_StateMachine22_Index_02 + 1) % Recovered_Reference_25.Length;
			(SpawnRoutine_StateMachine22_Value_01, SpawnRoutine_StateMachine22_Text_01) = Recovered_Reference_25[SpawnRoutine_StateMachine22_Index_02];
			SpawnRoutine_StateMachine22_Button_01?.SetText("Nextbot Speed : " + SpawnRoutine_StateMachine22_Text_01);
		}
	}

	private static void CreateNextbotEntity(List<GifDecoder.Frame> frames, AudioClip clip, AudioClip jumpClip, float speed, float size)
	{
		if ((Object)(object)Variables.Variables_Reference_06 == (Object)null)
		{
			return;
		}
		GameObject val = new GameObject("NXO Nextbot");
		val.layer = 8;
		Transform transform = ((Component)Variables.Variables_Reference_06.headCollider).transform;
		Vector3 val2 = Quaternion.Euler(0f, Random.Range(-80f, 80f), 0f) * transform.forward;
		val2.y = 0f;
		GameObject val5;
		Material val7;
		bool flag = (Object)(object)AstarPath.active != (Object)null;
		if (((Vector3)val2).sqrMagnitude < 0.001f)
		{
			val2 = transform.forward;
			val.transform.position = transform.position + ((Vector3)val2).normalized * 12f + Vector3.up * 4f;
			Rigidbody val3 = val.AddComponent<Rigidbody>();
			val3.useGravity = true;
			val3.constraints = (RigidbodyConstraints)112;
			val3.collisionDetectionMode = (CollisionDetectionMode)2;
			val3.interpolation = (RigidbodyInterpolation)1;
			CapsuleCollider val4 = val.AddComponent<CapsuleCollider>();
			val4.radius = size * 0.35f;
			val4.height = size;
			((Collider)val4).sharedMaterial = SlipMaterial;
			val5 = GameObject.CreatePrimitive((PrimitiveType)5);
			Object.DestroyImmediate((Object)(object)val5.GetComponent<MeshCollider>());
			val5.transform.SetParent(val.transform, false);
			val5.transform.localScale = Vector3.one * size;
			Shader val6 = Shader.Find("Sprites/Default") ?? Shader.Find("Unlit/Transparent") ?? Shader.Find("Standard");
			val7 = new Material(val6);
			if (frames != null)
			{
				goto Branch_02ce;
			}
		}
		else
		{
			val.transform.position = transform.position + ((Vector3)val2).normalized * 12f + Vector3.up * 4f;
			Rigidbody val3 = val.AddComponent<Rigidbody>();
			val3.useGravity = true;
			val3.constraints = (RigidbodyConstraints)112;
			val3.collisionDetectionMode = (CollisionDetectionMode)2;
			val3.interpolation = (RigidbodyInterpolation)1;
			CapsuleCollider val4 = val.AddComponent<CapsuleCollider>();
			val4.radius = size * 0.35f;
			val4.height = size;
			((Collider)val4).sharedMaterial = SlipMaterial;
			val5 = GameObject.CreatePrimitive((PrimitiveType)5);
			Object.DestroyImmediate((Object)(object)val5.GetComponent<MeshCollider>());
			val5.transform.SetParent(val.transform, false);
			val5.transform.localScale = Vector3.one * size;
			Shader val6 = Shader.Find("Sprites/Default") ?? Shader.Find("Unlit/Transparent") ?? Shader.Find("Standard");
			val7 = new Material(val6);
			if (frames != null)
			{
				goto Branch_02ce;
			}
		}
		Branch_02e6:
		Texture2D val8 = null;
		if (!((Object)(object)val8 != (Object)null))
		{
			goto Branch_039e;
		}
		goto Branch_032f;
		Branch_039e:
		val7.renderQueue = 4000;
		val5.GetComponent<Renderer>().sharedMaterial = val7;
		GifAnimator gifAnimator = val.AddComponent<GifAnimator>();
		gifAnimator.frames = ((frames != null && frames.Count > 0) ? frames.ToArray() : null);
		gifAnimator.mat = val7;
		if (!((Object)(object)clip != (Object)null))
		{
			goto Branch_0404;
		}
		Branch_0433:
		AudioSource val9 = val.AddComponent<AudioSource>();
		val9.clip = clip;
		val9.loop = true;
		val9.spatialBlend = 1f;
		val9.minDistance = 2f;
		val9.maxDistance = 60f;
		val9.rolloffMode = (AudioRolloffMode)1;
		val9.volume = 0.1f;
		val9.playOnAwake = false;
		if (!((Object)(object)AstarPath.active != (Object)null))
		{
			goto Branch_053a;
		}
		goto Branch_04c9;
		Branch_0404:
		if ((Object)(object)jumpClip != (Object)null)
		{
			goto Branch_0433;
		}
		if (!((Object)(object)AstarPath.active != (Object)null))
		{
			goto Branch_053a;
		}
		Branch_04c9:
		val.AddComponent<Seeker>();
		val.AddComponent<FunnelModifier>();
		NextbotEntity nextbotEntity = val.AddComponent<NextbotEntity>();
		nextbotEntity.visual = val5.transform;
		nextbotEntity.behaviour = SpawnRoutine_StateMachine22_Reference_01;
		nextbotEntity.ambientClip = clip;
		nextbotEntity.jumpscareClip = jumpClip;
		SpawnRoutine_StateMachine22_Items_01.Add(val);
		flag = (Object)(object)AstarPath.active != (Object)null;
		NotificationLib.ShowNotification(NotificationLib.NotificationType.Enabled, flag ? "Nextbot Spawned (Pathfinding)" : "Nextbot Spawned");
		return;
		Branch_02ce:
		if (frames.Count <= 0)
		{
			goto Branch_02e6;
		}
		val8 = frames[0].texture;
		if (!((Object)(object)val8 != (Object)null))
		{
			goto Branch_039e;
		}
		Branch_032f:
		val7.mainTexture = (Texture)(object)val8;
		val7.renderQueue = 4000;
		val5.GetComponent<Renderer>().sharedMaterial = val7;
		gifAnimator = val.AddComponent<GifAnimator>();
		gifAnimator.frames = ((frames != null && frames.Count > 0) ? frames.ToArray() : null);
		gifAnimator.mat = val7;
		if (!((Object)(object)clip != (Object)null))
		{
			goto Branch_0404;
		}
		goto Branch_0433;
		Branch_053a:
		nextbotEntity = val.AddComponent<NextbotEntity>();
		nextbotEntity.visual = val5.transform;
		nextbotEntity.behaviour = SpawnRoutine_StateMachine22_Reference_01;
		nextbotEntity.ambientClip = clip;
		nextbotEntity.jumpscareClip = jumpClip;
		SpawnRoutine_StateMachine22_Items_01.Add(val);
		NotificationLib.ShowNotification(NotificationLib.NotificationType.Enabled, flag ? "Nextbot Spawned (Pathfinding)" : "Nextbot Spawned");
	}
}
