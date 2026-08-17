using System;
using System.Collections;
using System.Collections.Generic;

using GorillaLocomotion;
using NXO.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace NXO.Mods.Categories;

public static class StevesPlayground
{
	public class DisplacerCannon : MonoBehaviour
	{
		public class Projectile : MonoBehaviour
		{
			private bool hasExploded = false;

			public void OnCollisionEnter(Collision collision)
			{
				if (!hasExploded && (LayerMask.GetMask(new string[3] { "Gorilla Object", "Default", "NoMirror" }) & (1 << collision.gameObject.layer)) != 0)
				{
					hasExploded = true;
					NetworkingLibrary.RaiseNetworkEvent(NetworkingLibrary.NetworkingType.DisplacerCannonExplosion, new object[3]
					{
						((Component)this).transform.position.x,
						((Component)this).transform.position.y,
						((Component)this).transform.position.z
					});
					if ((Object)(object)FuncType_Object_08 != (Object)null)
					{
						Object.Destroy((Object)(object)Object.Instantiate<GameObject>(FuncType_Object_08, ((Component)this).transform.position, Quaternion.identity), 4f);
						Object.Destroy((Object)(object)((Component)this).gameObject, 0.001f);
					}
					else
					{
						Object.Destroy((Object)(object)((Component)this).gameObject, 0.001f);
					}
				}
			}
		}

		public static bool Projectile_State_01;

		public AudioSource audioSource;

		public AudioClip shootSound;

		public AudioClip chargeSound;

		public Animator animator;

		public ParticleSystem shoot;

		public ParticleSystem charge;

		public bool isCharging = false;

		public void Update()
		{
			if (!Projectile_State_01)
			{
				Object.Destroy((Object)(object)((Component)this).gameObject);
			}
			else
			{
				if (isCharging)
				{
					return;
				}
				if (!InputHandler.IsRightTriggerPressed())
				{
					if (!((ButtonControl)Keyboard.current.enterKey).isPressed)
					{
						return;
					}
				}
				((MonoBehaviour)this).StartCoroutine(Charge());
			}
		}

		public IEnumerator Charge()
		{
			if (isCharging)
			{
				yield break;
			}

			isCharging = true;
			Transform chargeBone = FindChildRecursive("Charge", ((Component)this).transform);
			NetworkingLibrary.RaiseNetworkEvent(NetworkingLibrary.NetworkingType.DisplacerCannonCharge, new object[3]
			{
				chargeBone.position.x,
				chargeBone.position.y,
				chargeBone.position.z
			});
			charge.Play();
			if ((Object)(object)animator != (Object)null)
			{
				animator.speed = 0f;
			}
			if ((Object)(object)audioSource != (Object)null && (Object)(object)chargeSound != (Object)null)
			{
				audioSource.volume = 0.1f;
				audioSource.PlayOneShot(chargeSound);
			}

			const float duration = 1f;
			for (float elapsed = 0f; elapsed < duration; elapsed += Time.deltaTime)
			{
				if ((Object)(object)animator != (Object)null)
				{
					animator.speed = Mathf.Lerp(0f, 1f, elapsed / duration);
				}
				yield return null;
			}

			if ((Object)(object)animator != (Object)null)
			{
				animator.speed = 1f;
			}
			charge.Stop();
			Shoot();
			yield return (object)new WaitForSeconds(0.2f);
			isCharging = false;
		}

		public void Shoot()
		{
			Transform val = FindChildRecursive("Launch", ((Component)this).transform);
			NetworkingLibrary.RaiseNetworkEvent(NetworkingLibrary.NetworkingType.DisplacerCannonShoot, new object[6]
			{
				val.position.x,
				val.position.y,
				val.position.z,
				(-val.up).x,
				(-val.up).y,
				(-val.up).z
			});
			if ((Object)(object)audioSource != (Object)null && (Object)(object)shootSound != (Object)null)
			{
				audioSource.volume = 0.15f;
				audioSource.PlayOneShot(shootSound);
				if ((Object)(object)shoot != (Object)null)
				{
					goto Branch_014c;
				}
			}
			else if ((Object)(object)shoot != (Object)null)
			{
				goto Branch_014c;
			}
			if (!((Object)(object)FuncType_Object_06 != (Object)null))
			{
				return;
			}
			goto Branch_0195;
			Branch_014c:
			shoot.Play();
			if (!((Object)(object)FuncType_Object_06 != (Object)null))
			{
				return;
			}
			Branch_0195:
			GameObject val2 = Object.Instantiate<GameObject>(FuncType_Object_06, val.position, val.rotation);
			val2.AddComponent<Projectile>();
			Rigidbody component = val2.GetComponent<Rigidbody>();
			if ((Object)(object)component != (Object)null)
			{
				component.AddForce(-val.up * 30f, (ForceMode)2);
			}
		}
	}

	public class JetPack : MonoBehaviour
	{
		public static bool JetPack_State_01;

		public ParticleSystem thrusterEffect;

		public AudioSource source;

		public void Update()
		{
			if (!JetPack_State_01)
			{
				FuncType_Object_03.SetActive(false);
				return;
			}
			FuncType_Object_03.SetActive(true);
			Rigidbody rigidbody;
			Vector2 val;
			if ((InputHandler.IsLeftTriggerPressed() && InputHandler.IsRightTriggerPressed()) || ((ButtonControl)Keyboard.current.spaceKey).isPressed)
			{
				rigidbody = Variables.Variables_Reference_09.rigidbody;
				if (!((Object)(object)rigidbody != (Object)null))
				{
					return;
				}
				source.pitch = 0.8f + Variables.Variables_Reference_09.rigidbody.velocity.y / 20f;
				rigidbody.AddForce(Vector3.up * 1500f, (ForceMode)0);
				thrusterEffect.Play();
				Vector3 position = ((Component)thrusterEffect).transform.position;
				NetworkingLibrary.RaiseNetworkEvent(NetworkingLibrary.NetworkingType.JetPackThrust, new object[4] { position.x, position.y, position.z, source.pitch });
				val = InputHandler.GetJoystickAxis(left: false);
				if (((Vector2)val).magnitude <= 0.1f && Keyboard.current != null)
				{
					if (((ButtonControl)Keyboard.current.upArrowKey).isPressed)
					{
						val.y++;
						if (((ButtonControl)Keyboard.current.downArrowKey).isPressed)
						{
							goto Branch_0232;
						}
					}
					else if (((ButtonControl)Keyboard.current.downArrowKey).isPressed)
					{
						goto Branch_0232;
					}
					if (!((ButtonControl)Keyboard.current.leftArrowKey).isPressed)
					{
						goto Branch_02bb;
					}
					goto Branch_0288;
				}
				if (GTPlayer.Instance.IsGroundedButt)
				{
					return;
				}
				goto Branch_0351;
			}
			thrusterEffect.Stop();
			source.pitch = 0f;
			return;
			Branch_030f:
			if (GTPlayer.Instance.IsGroundedButt)
			{
				return;
			}
			goto Branch_0351;
			Branch_02de:
			val.x++;
			if (GTPlayer.Instance.IsGroundedButt)
			{
				return;
			}
			goto Branch_0351;
			Branch_0232:
			val.y--;
			if (!((ButtonControl)Keyboard.current.leftArrowKey).isPressed)
			{
				goto Branch_02bb;
			}
			goto Branch_0288;
			Branch_0351:
			if (((Vector2)val).magnitude > 0.1f && (Object)(object)rigidbody != (Object)null)
			{
				Vector3 forward = Variables.Variables_Reference_09.offlineVRRig.bodyTransform.forward;
				Vector3 right = Variables.Variables_Reference_09.offlineVRRig.bodyTransform.right;
				Vector3 val2 = forward * val.y + right * val.x;
				Vector3 normalized = ((Vector3)val2).normalized;
				Debug.Log((object)$"JetPack Move Direction: {normalized}");
				rigidbody.AddForce(normalized * 1500f, (ForceMode)0);
			}
			return;
			Branch_02bb:
			if (!((ButtonControl)Keyboard.current.rightArrowKey).isPressed)
			{
				goto Branch_030f;
			}
			goto Branch_02de;
			Branch_0288:
			val.x--;
			if (!((ButtonControl)Keyboard.current.rightArrowKey).isPressed)
			{
				goto Branch_030f;
			}
			goto Branch_02de;
		}
	}

	public class WebSling : MonoBehaviour
	{
		public static bool WebSling_State_01;

		public LineRenderer webLine;

		public SpringJoint? joint;

		public bool isLeftHand;

		private Vector3 hitPoint;

		private bool isExtending;

		private bool isRecoiling;

		private float currentLength;

		private float targetLength;

		private float recoilTime;

		public void DrawCurvedWeb(Vector3 start, Vector3 end)
		{
			int positionCount = webLine.positionCount;
			int num = 0;
			if (num < positionCount)
			{
				do
				{
					float num2 = (float)num / (float)(positionCount - 1);
					float num3 = Mathf.Sin(num2 * MathF.PI) * 0.3f * Vector3.Distance(start, end);
					Vector3 val = Vector3.down * num3;
					Vector3 val2 = Vector3.Lerp(start, end, num2) + val;
					webLine.SetPosition(num, val2);
					num++;
				}
				while (num < positionCount);
			}
		}

		public void AnimateWebExtend(Transform hand)
		{
			currentLength = Mathf.MoveTowards(currentLength, targetLength, Time.deltaTime * 70f);
			Vector3 val = hitPoint - hand.position;
			Vector3 normalized = ((Vector3)val).normalized;
			Vector3 end = hand.position + normalized * currentLength;
			DrawCurvedWeb(hand.position, end);
			if (Mathf.Abs(currentLength - targetLength) < 0.05f)
			{
				isExtending = false;
				isRecoiling = true;
				recoilTime = 0f;
			}
		}

		public void AnimateRecoil(Transform hand)
		{
			recoilTime += Time.deltaTime * 10f;
			float num = Mathf.Sin(recoilTime) * 0.2f * (1f - Mathf.Clamp01(recoilTime / 2f));
			Vector3 val = hitPoint - hand.position;
			Vector3 normalized = ((Vector3)val).normalized;
			Vector3 end = hitPoint + normalized * num;
			DrawCurvedWeb(hand.position, end);
			if (recoilTime > MathF.PI)
			{
				isRecoiling = false;
				DrawCurvedWeb(hand.position, hitPoint);
			}
		}

		public void UpdateWebLine()
		{
			if (!((Object)(object)joint == (Object)null) && !((Object)(object)webLine == (Object)null) && !((Object)(object)Variables.Variables_Reference_09 == (Object)null) && !((Object)(object)Variables.Variables_Reference_09.offlineVRRig == (Object)null))
			{
				if (!isLeftHand)
				{
					Transform rightHandTransform = Variables.Variables_Reference_09.offlineVRRig.rightHandTransform;
					DrawCurvedWeb(rightHandTransform.position, ((Joint)joint).connectedAnchor);
				}
				else
				{
					Transform rightHandTransform = Variables.Variables_Reference_09.offlineVRRig.leftHandTransform;
					DrawCurvedWeb(rightHandTransform.position, ((Joint)joint).connectedAnchor);
				}
			}
		}

		public void Update()
		{
			if (!WebSling_State_01)
			{
				Object.Destroy((Object)(object)((Component)this).gameObject);
				return;
			}
			if ((Object)(object)webLine == (Object)null)
			{
				Debug.LogError((object)"WebSling: webLine is null! Make sure the prefab has a LineRenderer.");
				return;
			}
			bool flag;
			if (!isLeftHand)
			{
				flag = InputHandler.IsRightTriggerPressed();
				if (!isLeftHand)
				{
					goto Branch_00b0;
				}
			}
			else
			{
				flag = InputHandler.IsLeftTriggerPressed();
				if (!isLeftHand)
				{
					goto Branch_00b0;
				}
			}
			Transform val = Variables.Variables_Reference_09.offlineVRRig.leftHandTransform;
			if (!flag)
			{
				goto Branch_0491;
			}
			goto Branch_00fa;
			Branch_00b0:
			val = Variables.Variables_Reference_09.offlineVRRig.rightHandTransform;
			if (!flag)
			{
				goto Branch_0491;
			}
			goto Branch_00fa;
			Branch_05ac:
			AnimateRecoil(val);
			return;
			Branch_0571:
			AnimateWebExtend(val);
			if (!isRecoiling)
			{
				return;
			}
			goto Branch_05ac;
			Branch_00fa:
			if ((Object)(object)joint == (Object)null)
			{
				RaycastHit val2 = default(RaycastHit);
				if (Physics.Raycast(val.position, val.forward, out val2, 50f))
				{
					hitPoint = ((RaycastHit)val2).point;
					joint = ((Component)Variables.Variables_Reference_09).gameObject.AddComponent<SpringJoint>();
					((Joint)joint).autoConfigureConnectedAnchor = false;
					((Joint)joint).connectedAnchor = hitPoint;
					float num = Vector3.Distance(Variables.Variables_Reference_09.rigidbody.position, hitPoint);
					joint.maxDistance = num * 0.8f;
					joint.minDistance = num * 0.25f;
					joint.spring = 5000f;
					joint.damper = 4000f;
					((Joint)joint).massScale = 5f;
					webLine.positionCount = 20;
					currentLength = 0f;
					targetLength = num;
					isExtending = true;
					isRecoiling = false;
					recoilTime = 0f;
				}
				else
				{
					hitPoint = val.position + val.forward * 16f;
					joint = ((Component)Variables.Variables_Reference_09).gameObject.AddComponent<SpringJoint>();
					((Joint)joint).autoConfigureConnectedAnchor = false;
					((Joint)joint).connectedAnchor = hitPoint;
					float num = Vector3.Distance(Variables.Variables_Reference_09.rigidbody.position, hitPoint);
					joint.maxDistance = num * 0.8f;
					joint.minDistance = num * 0.25f;
					joint.spring = 5000f;
					joint.damper = 4000f;
					((Joint)joint).massScale = 5f;
					webLine.positionCount = 20;
					currentLength = 0f;
					targetLength = num;
					isExtending = true;
					isRecoiling = false;
					recoilTime = 0f;
				}
			}
			else
			{
				UpdateWebLine();
				try
				{
					Vector3 localControllerVelocity;
					if (!isLeftHand)
					{
						localControllerVelocity = OVRInput.GetLocalControllerVelocity((Controller)2);
						if (((Vector3)localControllerVelocity).magnitude >= 2.5f)
						{
							goto Branch_03c3;
						}
					}
					else
					{
						localControllerVelocity = OVRInput.GetLocalControllerVelocity((Controller)1);
						if (((Vector3)localControllerVelocity).magnitude >= 2.5f)
						{
							goto Branch_03c3;
						}
					}
					goto EndBranch_0354;
					Branch_03c3:
					if ((Object)(object)joint == (Object)null)
					{
						Debug.LogWarning((object)"WebSling Joint is null during pull attempt.");
						return;
					}
					Vector3 val3 = ((Joint)joint).connectedAnchor - ((Component)Variables.Variables_Reference_09).transform.position;
					Vector3 val4 = ((Vector3)val3).normalized * 4.5f;
					Variables.Variables_Reference_09.rigidbody.AddForce(val4, (ForceMode)2);
					Debug.Log((object)"WebSling Pulling Player Towards Web Point!");
					EndBranch_0354:;
				}
				catch (Exception ex)
				{
					Debug.LogError((object)("WebSling Update Error: " + ex.Message));
				}
			}
			if (!isExtending)
			{
				goto Branch_0592;
			}
			goto Branch_0571;
			Branch_0491:
			if ((Object)(object)joint != (Object)null)
			{
				Object.Destroy((Object)(object)joint);
				joint = null;
				isExtending = false;
				isRecoiling = false;
				currentLength = 0f;
				targetLength = 0f;
				webLine.positionCount = 0;
				if (isExtending)
				{
					goto Branch_0571;
				}
			}
			else
			{
				isExtending = false;
				isRecoiling = false;
				currentLength = 0f;
				targetLength = 0f;
				webLine.positionCount = 0;
				if (isExtending)
				{
					goto Branch_0571;
				}
			}
			Branch_0592:
			if (!isRecoiling)
			{
				return;
			}
			goto Branch_05ac;
		}
	}

	public enum FuncType
	{
		DisplacerCannon,
		Jetpack
	}

	public static GameObject FuncType_Object_02;

	public static GameObject FuncType_Object_06;

	public static GameObject FuncType_Object_08;

	public static GameObject FuncType_Object_05;

	public static GameObject FuncType_Object_03;

	public static GameObject FuncType_Object_04;

	public static GameObject FuncType_Object_01;

	public static GameObject FuncType_Object_07;

	public static Dictionary<string, AudioClip> FuncType_Lookup_01 = new Dictionary<string, AudioClip>();

	public static AssetBundle FuncType_Reference_01;

	private static List<Transform> FuncType_Items_01 = new List<Transform>(32);

	public static void UpdateNetworkedEquipment(FuncType type, bool add)
	{
		if (add)
		{
			EquipItem(type);
		}
		else
		{
			UnequipItem(type);
		}
	}

	public static void EquipItem(FuncType type)
	{
		FuncType funcType2;
		if ((Object)(object)FuncType_Reference_01 == (Object)null)
		{
			FuncType_Reference_01 = AssetHandler.LoadAssetBundle("NXO.Resources.nxostevestuff");
			FuncType_Object_02 = FuncType_Reference_01.LoadAsset<GameObject>("Displacer_P");
			FuncType_Object_06 = FuncType_Reference_01.LoadAsset<GameObject>("DisplacerShot_P");
			FuncType_Object_08 = FuncType_Reference_01.LoadAsset<GameObject>("DisplacerCumshot_P");
			FuncType_Object_05 = FuncType_Reference_01.LoadAsset<GameObject>("JetPack_P");
			FuncType_Object_03 = Object.Instantiate<GameObject>(FuncType_Object_05);
			((Object)FuncType_Object_03).name = "JetPack";
			Object.Destroy((Object)(object)FuncType_Object_03.GetComponent<Rigidbody>());
			Object.Destroy((Object)(object)FuncType_Object_03.GetComponent<Collider>());
			FuncType_Object_03.transform.SetParent(((Component)Variables.Variables_Reference_09.offlineVRRig.bodyRenderer).transform, true);
			FuncType_Object_03.transform.localPosition = new Vector3(0f, -0.2659f, -0.1716f);
			FuncType_Object_03.transform.localRotation = Quaternion.Euler(270f, 0f, 0f);
			FuncType_Object_03.SetActive(false);
			FuncType_Object_03.AddComponent<JetPack>().thrusterEffect = ((Component)FuncType_Object_03.transform.GetChild(1)).gameObject.GetComponent<ParticleSystem>();
			FuncType_Object_03.GetComponent<JetPack>().source = FuncType_Object_03.GetComponent<AudioSource>();
			FuncType_Lookup_01 = new Dictionary<string, AudioClip>();
			AudioClip[] array = FuncType_Reference_01.LoadAllAssets<AudioClip>();
			int num = 0;
			while (num < array.Length)
			{
				AudioClip val = array[num];
				if (((Object)val).name.StartsWith("displacer_"))
				{
					FuncType_Lookup_01[((Object)val).name] = val;
					Debug.Log((object)("Loaded Displacer Cannon Audio Clip: " + ((Object)val).name));
					num++;
				}
				else
				{
					num++;
				}
			}
			FuncType_Object_04 = FuncType_Reference_01.LoadAsset<GameObject>("Websling_P");
			FuncType funcType = type;
			funcType2 = funcType;
			if (funcType2 != FuncType.DisplacerCannon)
			{
				goto Branch_0263;
			}
		}
		else
		{
			FuncType funcType = type;
			funcType2 = funcType;
			if (funcType2 != FuncType.DisplacerCannon)
			{
				goto Branch_0263;
			}
		}
		if (!DisplacerCannon.Projectile_State_01)
		{
			DisplacerCannon.Projectile_State_01 = true;
			Debug.Log((object)"Displacer Cannon Activated!");
			NetworkingLibrary.RaiseNetworkEvent(NetworkingLibrary.NetworkingType.DisplacerCannonEquip, new object[0]);
			DisplacerCannon displacerCannon = Object.Instantiate<GameObject>(FuncType_Object_02).AddComponent<DisplacerCannon>();
			displacerCannon.shootSound = FuncType_Lookup_01["displacer_self"];
			displacerCannon.chargeSound = FuncType_Lookup_01["displacer_spin"];
			GameObject val2 = ((Component)displacerCannon).gameObject;
			displacerCannon.audioSource = val2.AddComponent<AudioSource>();
			displacerCannon.animator = val2.GetComponentInChildren<Animator>();
			displacerCannon.shoot = ((Component)FindChildRecursive("Launch", val2.transform)).GetComponent<ParticleSystem>();
			displacerCannon.charge = ((Component)FindChildRecursive("Charge", val2.transform)).GetComponent<ParticleSystem>();
			((Component)displacerCannon).transform.SetParent(Variables.Variables_Reference_09.offlineVRRig.rightHandTransform, true);
			((Component)displacerCannon).transform.localPosition = new Vector3(0.0472f, -0.0124f, -0.0393f);
			((Component)displacerCannon).transform.localRotation = Quaternion.Euler(284.5566f, 0f, 270f);
		}
		return;
		Branch_0263:
		if (funcType2 != FuncType.Jetpack || JetPack.JetPack_State_01)
		{
			return;
		}
		JetPack.JetPack_State_01 = true;
		NetworkingLibrary.RaiseNetworkEvent(NetworkingLibrary.NetworkingType.JetPackEquip, new object[0]);
		FuncType_Object_03.SetActive(true);
		Renderer[] componentsInChildren = FuncType_Object_03.GetComponentsInChildren<Renderer>();
		int num2 = 0;
		while (num2 < componentsInChildren.Length)
		{
			Renderer val3 = componentsInChildren[num2];
			if ((Object)(object)val3 != (Object)null && !(val3 is ParticleSystemRenderer))
			{
				Material[] materials = val3.materials;
				for (int i = 0; i < materials.Length; i++)
				{
					materials[i].color = Color.black;
				}
				num2++;
			}
			else
			{
				num2++;
			}
		}
		Debug.Log((object)"JetPack Activated!");
	}

	public static Transform FindChildRecursive(string name, Transform targ)
	{
		FuncType_Items_01.Clear();
		FuncType_Items_01.Add(targ);
		if (FuncType_Items_01.Count > 0)
		{
			do
			{
				Transform val = FuncType_Items_01[FuncType_Items_01.Count - 1];
				FuncType_Items_01.RemoveAt(FuncType_Items_01.Count - 1);
				if (((Object)val).name == name)
				{
					return val;
				}
				int num = 0;
				int childCount = val.childCount;
				if (num < childCount)
				{
					do
					{
						FuncType_Items_01.Add(val.GetChild(num));
						num++;
					}
					while (num < childCount);
				}
			}
			while (FuncType_Items_01.Count > 0);
		}
		return null;
	}

	public static void UnequipItem(FuncType type)
	{
		switch (type)
		{
		case FuncType.DisplacerCannon:
			if (DisplacerCannon.Projectile_State_01)
			{
				DisplacerCannon.Projectile_State_01 = false;
				NetworkingLibrary.RaiseNetworkEvent(NetworkingLibrary.NetworkingType.DisplacerCannonUnequip, new object[0]);
				Debug.Log((object)"Displacer Cannon Deactivated!");
			}
			break;
		case FuncType.Jetpack:
			if (JetPack.JetPack_State_01)
			{
				JetPack.JetPack_State_01 = false;
				NetworkingLibrary.RaiseNetworkEvent(NetworkingLibrary.NetworkingType.JetPackUnequip, new object[0]);
				Debug.Log((object)"JetPack Deactivated!");
			}
			break;
		}
	}
}
