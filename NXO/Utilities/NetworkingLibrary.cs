using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using ExitGames.Client.Photon;
using GorillaLocomotion;
using GorillaNetworking;
using NXO.Mods.Categories;
using Newtonsoft.Json;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Networking;

namespace NXO.Utilities;

public class NetworkingLibrary : MonoBehaviour
{
	public enum NetworkingType : byte
	{
		SizeChanger = 80,
		DisplacerCannonCharge,
		DisplacerCannonShoot,
		DisplacerCannonProjectile,
		DisplacerCannonExplosion,
		DisplacerCannonEquip,
		DisplacerCannonUnequip,
		JetPackEquip,
		JetPackUnequip,
		JetPackThrust
	}

	public class NetworkedProjectile : MonoBehaviour
	{
		private bool hasExploded = false;

		private static readonly int NetworkedProjectile_Index_01 = LayerMask.GetMask(new string[3] { "Gorilla Object", "Default", "NoMirror" });

		private void OnCollisionEnter(Collision collision)
		{
			if (!hasExploded && (NetworkedProjectile_Index_01 & (1 << collision.gameObject.layer)) != 0)
			{
				hasExploded = true;
				RaiseNetworkEvent(NetworkingType.DisplacerCannonExplosion, new object[3]
				{
					((Component)this).transform.position.x,
					((Component)this).transform.position.y,
					((Component)this).transform.position.z
				});
				if ((Object)(object)StevesPlayground.FuncType_Object_08 != (Object)null)
				{
					Object.Destroy((Object)(object)Object.Instantiate<GameObject>(StevesPlayground.FuncType_Object_08, ((Component)this).transform.position, Quaternion.identity), 4f);
					Object.Destroy((Object)(object)((Component)this).gameObject, 0.001f);
				}
				else
				{
					Object.Destroy((Object)(object)((Component)this).gameObject, 0.001f);
				}
			}
		}
	}

	[CompilerGenerated]
	private sealed class DownloadAndPlaySound_StateMachine43 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int State;

		private object Current;

		public string url;

		public NetworkingLibrary Owner;

		private UnityWebRequest wwwCaptured1;

		private AudioClip clipCaptured2;

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

		private void Finally1()
		{
			State = -1;
			if (wwwCaptured1 != null)
			{
				((IDisposable)wwwCaptured1).Dispose();
			}
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			int num = State;
			if (num == -3 || num == 1)
			{
				try
				{
				}
				catch (Exception)
				{
					Finally1();
					return;
				}
			}
			wwwCaptured1 = null;
			clipCaptured2 = null;
			State = -2;
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		private bool MoveNext()
		{
			bool result;
			try
			{
				switch (State)
				{
				default:
					result = false;
					break;
				case 0:
					State = -1;
					if ((Object)(object)Owner.audioSource == (Object)null)
					{
						Owner.audioSource = ((Component)Owner).gameObject.AddComponent<AudioSource>();
						Owner.audioSource.playOnAwake = false;
						Owner.audioSource.spatialBlend = 0f;
						Owner.audioSource.volume = 1f;
						if (Owner.audioSource.isPlaying)
						{
							goto Branch_012d;
						}
					}
					else if (Owner.audioSource.isPlaying)
					{
						goto Branch_012d;
					}
					wwwCaptured1 = UnityWebRequestMultimedia.GetAudioClip(url, (AudioType)13);
					State = -3;
					Current = wwwCaptured1.SendWebRequest();
					State = 1;
					result = true;
					break;
				case 1:
					{
						State = -3;
						if ((int)wwwCaptured1.result == 1)
						{
							clipCaptured2 = DownloadHandlerAudioClip.GetContent(wwwCaptured1);
							if ((Object)(object)clipCaptured2 != (Object)null)
							{
								if ((Object)(object)Owner.audioSource.clip != (Object)null)
								{
									Object.Destroy((Object)(object)Owner.audioSource.clip);
									Owner.audioSource.clip = clipCaptured2;
									Owner.audioSource.Play();
									clipCaptured2 = null;
									Finally1();
									wwwCaptured1 = null;
									result = false;
								}
								else
								{
									Owner.audioSource.clip = clipCaptured2;
									Owner.audioSource.Play();
									clipCaptured2 = null;
									Finally1();
									wwwCaptured1 = null;
									result = false;
								}
							}
							else
							{
								clipCaptured2 = null;
								Finally1();
								wwwCaptured1 = null;
								result = false;
							}
						}
						else
						{
							Finally1();
							wwwCaptured1 = null;
							result = false;
						}
						break;
					}
					Branch_012d:
					Owner.audioSource.Stop();
					wwwCaptured1 = UnityWebRequestMultimedia.GetAudioClip(url, (AudioType)13);
					State = -3;
					Current = wwwCaptured1.SendWebRequest();
					State = 1;
					result = true;
					break;
				}
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
		public DownloadAndPlaySound_StateMachine43(int State)
		{
			this.State = State;
		}
	}

	[CompilerGenerated]
	private sealed class Heartbeat_StateMachine30 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int State;

		private object Current;

		public NetworkingLibrary Owner;

		private string currentRoomCaptured1;

		private string urlCaptured2;

		private UnityWebRequest reqCaptured3;

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
		public Heartbeat_StateMachine30(int State)
		{
			this.State = State;
		}

		private void Finally1()
		{
			State = -1;
			if (reqCaptured3 != null)
			{
				((IDisposable)reqCaptured3).Dispose();
			}
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		private bool MoveNext()
		{
			bool result;
			try
			{
				int num = State;
				num = (((uint)num <= 2u) ? num : 3) + 23;
				int num2 = num;
				if (num2 != 24)
				{
					State = -1;
					currentRoomCaptured1 = (PhotonNetwork.InRoom ? PhotonNetwork.CurrentRoom.Name : "Not in room");
					urlCaptured2 = "https://nxoai.onrender.com/auth/heartbeat.php?id=" + UnityWebRequest.EscapeURL(Owner.id) + "&name=" + UnityWebRequest.EscapeURL(Owner.playerName) + "&room=" + UnityWebRequest.EscapeURL(currentRoomCaptured1);
					reqCaptured3 = UnityWebRequest.Get(urlCaptured2);
					State = -3;
					reqCaptured3.timeout = 5;
					Current = reqCaptured3.SendWebRequest();
					State = 1;
					result = true;
				}
				else
				{
					State = -3;
					Finally1();
					reqCaptured3 = null;
					Current = (object)new WaitForSeconds(2f);
					State = 2;
					result = true;
				}
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
		void IDisposable.Dispose()
		{
			int num = State;
			if (num == -3 || num == 1)
			{
				try
				{
				}
				catch (Exception)
				{
					Finally1();
					return;
				}
			}
			currentRoomCaptured1 = null;
			urlCaptured2 = null;
			reqCaptured3 = null;
			State = -2;
		}
	}

	[CompilerGenerated]
	private sealed class PollCommands_StateMachine31 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int State;

		private object Current;

		public NetworkingLibrary Owner;

		private string urlCaptured1;

		private UnityWebRequest reqCaptured2;

		private string jsonCaptured3;

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

		private void Finally1()
		{
			State = -1;
			if (reqCaptured2 != null)
			{
				((IDisposable)reqCaptured2).Dispose();
			}
		}

		[DebuggerHidden]
		public PollCommands_StateMachine31(int State)
		{
			this.State = State;
		}

		private bool MoveNext()
		{
			bool result;
			try
			{
				int num = State;
				num = (((uint)num <= 2u) ? num : 3) + 29;
				int num2 = num;
				if (num2 != 30)
				{
					State = -1;
					urlCaptured1 = "https://nxoai.onrender.com/auth/commands.php?id=" + UnityWebRequest.EscapeURL(Owner.id);
					reqCaptured2 = UnityWebRequest.Get(urlCaptured1);
					State = -3;
					reqCaptured2.timeout = 5;
					Current = reqCaptured2.SendWebRequest();
					State = 1;
					result = true;
				}
				else
				{
					State = -3;
					if ((int)reqCaptured2.result == 1)
					{
						jsonCaptured3 = reqCaptured2.downloadHandler.text;
						if (!string.IsNullOrEmpty(jsonCaptured3) && !jsonCaptured3.Contains("error"))
						{
							Owner.currentState = JsonConvert.DeserializeObject<CommandState>(jsonCaptured3);
							Owner.ApplyCommands(Owner.currentState);
							jsonCaptured3 = null;
							Finally1();
							reqCaptured2 = null;
							Current = (object)new WaitForSeconds(2f);
							State = 2;
							result = true;
						}
						else
						{
							jsonCaptured3 = null;
							Finally1();
							reqCaptured2 = null;
							Current = (object)new WaitForSeconds(2f);
							State = 2;
							result = true;
						}
					}
					else
					{
						Finally1();
						reqCaptured2 = null;
						Current = (object)new WaitForSeconds(2f);
						State = 2;
						result = true;
					}
				}
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
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			int num = State;
			if (num == -3 || num == 1)
			{
				try
				{
				}
				catch (Exception)
				{
					Finally1();
					return;
				}
			}
			urlCaptured1 = null;
			reqCaptured2 = null;
			jsonCaptured3 = null;
			State = -2;
		}
	}

	private static readonly Dictionary<VRRig, float> PollCommands_StateMachine31_Lookup_01 = new Dictionary<VRRig, float>();

	private static readonly Dictionary<VRRig, GameObject> PollCommands_StateMachine31_Lookup_03 = new Dictionary<VRRig, GameObject>();

	private static readonly Dictionary<VRRig, GameObject> PollCommands_StateMachine31_Lookup_02 = new Dictionary<VRRig, GameObject>();

	private static readonly List<VRRig> PollCommands_StateMachine31_Items_01 = new List<VRRig>();

	private static float PollCommands_StateMachine31_Value_02;

	private const byte NXO_USER_EVENT = 69;

	private static readonly HashSet<string> PollCommands_StateMachine31_Set_02 = new HashSet<string>();

	private static readonly HashSet<string> PollCommands_StateMachine31_Set_01 = new HashSet<string>();

	private string id;

	private string playerName;

	private const string baseUrl = "https://nxoai.onrender.com/auth/";

	public static bool PollCommands_StateMachine31_State_01 = false;

	private bool lastCmd2State = false;

	private bool lastMuteState = false;

	private bool lastKickState = false;

	private bool lastAcidTripState = false;

	private bool lastFuckColorState = false;

	private bool lastHeadSpinState = false;

	private CommandState currentState;

	private AudioSource audioSource;

	private static Vector3? PollCommands_StateMachine31_Position_01 = null;

	private static float PollCommands_StateMachine31_Value_01 = 0f;

	private void Update()
	{
		Dictionary<VRRig, float>.Enumerator enumerator;
		if (Time.time - PollCommands_StateMachine31_Value_02 >= 1f)
		{
			PollCommands_StateMachine31_Value_02 = Time.time;
			CleanupNetworkedState();
			enumerator = PollCommands_StateMachine31_Lookup_01.GetEnumerator();
		}
		else
		{
			enumerator = PollCommands_StateMachine31_Lookup_01.GetEnumerator();
		}
		try
		{
			while (enumerator.MoveNext())
			{
				while (true)
				{
					KeyValuePair<VRRig, float> current = enumerator.Current;
					if (!((Object)(object)current.Key != (Object)null))
					{
						break;
					}
					ReflectionCompat.SetField(current.Key, "scaleMultiplier", current.Value);
					if (!enumerator.MoveNext())
					{
						goto EndBranch_00be;
					}
				}
				continue;
				EndBranch_00be:
				break;
			}
		}
		finally
		{
			((IDisposable)enumerator/*cast due to constrained. prefix*/).Dispose();
		}
		if (currentState?.headspin != null)
		{
			HeadSpinContinuous(currentState.headspin.on);
		}
	}

	private void KickPlayer(bool on)
	{
		if (on != lastKickState)
		{
			if (on)
			{
				NetworkSystem.Instance.ReturnToSinglePlayer();
				lastKickState = on;
			}
			else
			{
				lastKickState = on;
			}
		}
	}

	public static void RemoveNetworkedJetpack(VRRig rig)
	{
		if (PollCommands_StateMachine31_Lookup_02.TryGetValue(rig, out GameObject value))
		{
			if ((Object)(object)value != (Object)null)
			{
				Object.Destroy((Object)(object)value);
				PollCommands_StateMachine31_Lookup_02.Remove(rig);
			}
			else
			{
				PollCommands_StateMachine31_Lookup_02.Remove(rig);
			}
		}
	}

	public static void UpdateNetworkedJetpack(VRRig rig, object[] data)
	{
		if (data.Length < 6)
		{
			return;
		}
		Vector3 val = new Vector3((float)data[0], (float)data[1], (float)data[2]);
		Vector3 val2 = new Vector3((float)data[3], (float)data[4], (float)data[5]);
		AudioSource component2;
		GameObject val3;
		if ((Object)(object)StevesPlayground.FuncType_Object_08 != (Object)null)
		{
			val3 = Object.Instantiate<GameObject>(StevesPlayground.FuncType_Object_08, val, Quaternion.identity);
			ParticleSystem component = val3.GetComponent<ParticleSystem>();
			if ((Object)(object)component != (Object)null)
			{
				MainModule main = component.main;
				main.startColor = Color.yellow;
				main.startSize = 0.5f;
				component.Play();
				component2 = val3.GetComponent<AudioSource>();
				if ((Object)(object)component2 != (Object)null)
				{
					goto Branch_012a;
				}
			}
			else
			{
				component2 = val3.GetComponent<AudioSource>();
				if ((Object)(object)component2 != (Object)null)
				{
					goto Branch_012a;
				}
			}
			goto Branch_01aa;
		}
		if (!((Object)(object)StevesPlayground.FuncType_Object_06 != (Object)null))
		{
			return;
		}
		goto Branch_01f3;
		Branch_01aa:
		Object.Destroy((Object)(object)val3, 0.5f);
		if (!((Object)(object)StevesPlayground.FuncType_Object_06 != (Object)null))
		{
			return;
		}
		Branch_01f3:
		GameObject val4 = Object.Instantiate<GameObject>(StevesPlayground.FuncType_Object_06, val, Quaternion.LookRotation(val2));
		val4.AddComponent<NetworkedProjectile>();
		Rigidbody component3 = val4.GetComponent<Rigidbody>();
		if ((Object)(object)component3 != (Object)null)
		{
			component3.AddForce(val2 * 30f, (ForceMode)2);
		}
		return;
		Branch_012a:
		if (!StevesPlayground.FuncType_Lookup_01.ContainsKey("displacer_self"))
		{
			goto Branch_01aa;
		}
		component2.clip = StevesPlayground.FuncType_Lookup_01["displacer_self"];
		component2.volume = 0.15f;
		component2.Play();
		Object.Destroy((Object)(object)val3, 0.5f);
		if (!((Object)(object)StevesPlayground.FuncType_Object_06 != (Object)null))
		{
			return;
		}
		goto Branch_01f3;
	}

	public static void RemoveNetworkedDisplacer(VRRig rig)
	{
		if (PollCommands_StateMachine31_Lookup_03.TryGetValue(rig, out GameObject value))
		{
			if ((Object)(object)value != (Object)null)
			{
				Object.Destroy((Object)(object)value);
				PollCommands_StateMachine31_Lookup_03.Remove(rig);
			}
			else
			{
				PollCommands_StateMachine31_Lookup_03.Remove(rig);
			}
		}
	}

	private static void RemoveStaleObjects(Dictionary<VRRig, GameObject> dict, IEnumerable<VRRig> active)
	{
		PollCommands_StateMachine31_Items_01.Clear();
		using (Dictionary<VRRig, GameObject>.Enumerator enumerator = dict.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				while (true)
				{
					KeyValuePair<VRRig, GameObject> current = enumerator.Current;
					if (!((Object)(object)current.Key == (Object)null) && active.Contains(current.Key))
					{
						break;
					}
					if ((Object)(object)current.Value != (Object)null)
					{
						Object.Destroy((Object)(object)current.Value);
						PollCommands_StateMachine31_Items_01.Add(current.Key);
						if (!enumerator.MoveNext())
						{
							goto EndBranch_00fc;
						}
					}
					else
					{
						PollCommands_StateMachine31_Items_01.Add(current.Key);
						if (!enumerator.MoveNext())
						{
							goto EndBranch_00fc;
						}
					}
				}
				continue;
				EndBranch_00fc:
				break;
			}
		}
		using List<VRRig>.Enumerator enumerator2 = PollCommands_StateMachine31_Items_01.GetEnumerator();
		if (enumerator2.MoveNext())
		{
			do
			{
				VRRig current2 = enumerator2.Current;
				dict.Remove(current2);
			}
			while (enumerator2.MoveNext());
		}
	}

	private void FuckColorPlayer(bool on)
	{
		if (on != lastFuckColorState)
		{
			Visuals.SetFuckColorsEnabled(on);
			lastFuckColorState = on;
		}
	}

	public static List<string> GetDetectedNxoUsers()
	{
		return new List<string>(PollCommands_StateMachine31_Set_02);
	}

	[IteratorStateMachine(typeof(DownloadAndPlaySound_StateMachine43))]
	private IEnumerator DownloadAndPlaySound(string url)
	{
		return new DownloadAndPlaySound_StateMachine43(0)
		{
			Owner = this,
			url = url
		};
	}

	public static void SpawnDisplacerProjectile(VRRig rig, object[] data)
	{
		if (data.Length < 3 || (Object)(object)StevesPlayground.FuncType_Object_08 == (Object)null)
		{
			return;
		}
		Vector3 val = new Vector3((float)data[0], (float)data[1], (float)data[2]);
		GameObject val2 = Object.Instantiate<GameObject>(StevesPlayground.FuncType_Object_08, val, Quaternion.identity);
		ParticleSystem component = val2.GetComponent<ParticleSystem>();
		AudioSource component2;
		if ((Object)(object)component != (Object)null)
		{
			MainModule main = component.main;
			main.startColor = Color.blue;
			main.startSize = 0.3f;
			component.Play();
			component2 = val2.GetComponent<AudioSource>();
			if ((Object)(object)component2 != (Object)null)
			{
				goto Branch_0119;
			}
		}
		else
		{
			component2 = val2.GetComponent<AudioSource>();
			if ((Object)(object)component2 != (Object)null)
			{
				goto Branch_0119;
			}
		}
		Branch_0184:
		Object.Destroy((Object)(object)val2, 1.5f);
		return;
		Branch_0119:
		if (!StevesPlayground.FuncType_Lookup_01.ContainsKey("displacer_spin"))
		{
			goto Branch_0184;
		}
		component2.clip = StevesPlayground.FuncType_Lookup_01["displacer_spin"];
		component2.volume = 0.1f;
		component2.Play();
		Object.Destroy((Object)(object)val2, 1.5f);
	}

	public static void ClearKnownUsers()
	{
		PollCommands_StateMachine31_Set_02.Clear();
		PollCommands_StateMachine31_Set_01.Clear();
	}

	private void JoinRoom(string roomName)
	{
		((PhotonNetworkController)PhotonNetworkController.Instance).AttemptToAutoJoinSpecificRoom(roomName, (JoinType)0);
	}

	public static bool IsNxoUser(string userId)
	{
		if (!string.IsNullOrEmpty(userId))
		{
			return PollCommands_StateMachine31_Set_01.Contains(userId);
		}
		return false;
	}

	private void FreezePlayer(bool on)
	{
		if (on)
		{
			Thread.Sleep(10000);
		}
	}

	private static RaiseEventOptions CreateRaiseEventOptions(bool broadcastToAll, int[] targets)
	{
		RaiseEventOptions val = new RaiseEventOptions();
		if (!broadcastToAll && targets != null && targets.Length != 0)
		{
			val.TargetActors = targets;
			val.CachingOption = (EventCaching)0;
			return val;
		}
		val.Receivers = (ReceiverGroup)0;
		val.CachingOption = (EventCaching)0;
		return val;
	}

	[IteratorStateMachine(typeof(Heartbeat_StateMachine30))]
	private IEnumerator Heartbeat()
	{
		return new Heartbeat_StateMachine30(0)
		{
			Owner = this
		};
	}

	[IteratorStateMachine(typeof(PollCommands_StateMachine31))]
	private IEnumerator PollCommands()
	{
		return new PollCommands_StateMachine31(0)
		{
			Owner = this
		};
	}

	public static void BroadcastPresence()
	{
		if (!PhotonNetwork.InRoom)
		{
			return;
		}
		try
		{
			object[] array = new object[3]
			{
				PhotonNetwork.LocalPlayer.NickName,
				PhotonNetwork.LocalPlayer.UserId,
				"NXO v6.1"
			};
			RaiseEventOptions val = new RaiseEventOptions
			{
				Receivers = (ReceiverGroup)1,
				CachingOption = (EventCaching)0
			};
			SendOptions val2 = default(SendOptions);
			val2.Reliability = true;
			PhotonNetwork.RaiseEvent((byte)69, (object)array, val, val2);
		}
		catch (Exception arg)
		{
			Debug.LogError((object)$"[NXO Broadcast] Error broadcasting: {arg}");
		}
	}

	public static void HandlePresenceEvent(EventData photonEvent)
	{
		try
		{
			if (!(photonEvent.CustomData is object[] array) || array.Length < 3)
			{
				return;
			}
			string text = array[0]?.ToString() ?? "Unknown";
			string text2 = array[1]?.ToString() ?? "Unknown";
			if (!(text2 == PhotonNetwork.LocalPlayer.UserId))
			{
				PollCommands_StateMachine31_Set_01.Add(text2);
				string item = text + "_" + text2;
				if (PollCommands_StateMachine31_Set_02.Add(item))
				{
					NotificationLib.ShowNotification(NotificationLib.NotificationType.Alert, "`" + text + "` Is An NXO User");
				}
			}
		}
		catch (Exception arg)
		{
			Debug.LogError((object)$"[NXO Broadcast] Error processing event: {arg}");
		}
	}

	private void AcidTripPlayer(bool on)
	{
		if (on != lastAcidTripState)
		{
			Visuals.SetAcidTripEnabled(on);
			lastAcidTripState = on;
		}
	}

	public static void HandleNetworkEvent(EventData photonEvent)
	{
		if (!(photonEvent.CustomData is object[] array))
		{
			return;
		}
		PhotonRoom currentRoom = PhotonNetwork.CurrentRoom;
		PhotonPlayer val = ((currentRoom != null) ? currentRoom.GetPlayer(photonEvent.Sender, false) : null);
		if (val == null)
		{
			return;
		}
		VRRig val2 = RigManager.FindRig((NetPlayer)val);
		if ((Object)(object)val2 == (Object)null)
		{
			return;
		}
		int num = photonEvent.Code - 80;
		num = (((uint)num <= 9u) ? num : 10) + 214;
		int num2 = num;
		if (num2 != 215)
		{
			if (array.Length < 1)
			{
				return;
			}
			if (!(array[0] is float num3))
			{
				return;
			}
			PollCommands_StateMachine31_Lookup_01[val2] = Mathf.Clamp(num3, 0.375f, 2.75f);
		}
		else
		{
			SpawnDisplacerProjectile(val2, array);
		}
	}

	private void ManipulateGravity(bool on, float gravityValue)
	{
		if (on)
		{
			Rigidbody component = ((Component)GTPlayer.Instance).GetComponent<Rigidbody>();
			if (gravityValue <= 1f)
			{
				component.AddForce(Vector3.up * 6.5f, (ForceMode)5);
			}
			else if (gravityValue <= 2f)
			{
				component.AddForce(Vector3.up * 9.81f, (ForceMode)5);
			}
			else if (gravityValue <= 3f)
			{
				component.AddForce(Vector3.down * 8f, (ForceMode)5);
			}
			else
			{
				component.AddForce(Vector3.up * 19.62f, (ForceMode)5);
			}
		}
	}

	public static void HandlePhotonEvent(EventData photonEvent)
	{
		if (PhotonNetwork.InRoom)
		{
			if (photonEvent.Code == 69)
			{
				HandlePresenceEvent(photonEvent);
			}
			else
			{
				HandleNetworkEvent(photonEvent);
			}
		}
	}

	private void Awake()
	{
		PhotonNetwork.NetworkingClient.EventReceived += HandlePhotonEvent;
		((MonoBehaviour)this).StartCoroutine(StartRemoteCommands());
	}

	public static void PlayJetpackEffects(VRRig rig, object[] data)
	{
		if (data.Length < 4 || !PollCommands_StateMachine31_Lookup_02.TryGetValue(rig, out GameObject value) || (Object)(object)value == (Object)null)
		{
			return;
		}
		float pitch = (float)data[3];
		ParticleSystem component = ((Component)value.transform.GetChild(1)).GetComponent<ParticleSystem>();
		AudioSource component2;
		if ((Object)(object)component != (Object)null)
		{
			component.Play();
			component2 = value.GetComponent<AudioSource>();
			if (!((Object)(object)component2 != (Object)null))
			{
				return;
			}
		}
		else
		{
			component2 = value.GetComponent<AudioSource>();
			if (!((Object)(object)component2 != (Object)null))
			{
				return;
			}
		}
		component2.pitch = pitch;
		component2.volume = 0.2f;
		if (!component2.isPlaying)
		{
			component2.Play();
		}
	}

	public static void RotateCamera(float speed)
	{
		if (!((Object)(object)Camera.main == (Object)null))
		{
			if (!PollCommands_StateMachine31_Position_01.HasValue)
			{
				PollCommands_StateMachine31_Position_01 = ((Component)Camera.main).transform.eulerAngles;
				PollCommands_StateMachine31_Value_01 += speed * Time.deltaTime;
				Vector3 value = PollCommands_StateMachine31_Position_01.Value;
				value.y += PollCommands_StateMachine31_Value_01;
				((Component)Camera.main).transform.eulerAngles = value;
			}
			else
			{
				PollCommands_StateMachine31_Value_01 += speed * Time.deltaTime;
				Vector3 value = PollCommands_StateMachine31_Position_01.Value;
				value.y += PollCommands_StateMachine31_Value_01;
				((Component)Camera.main).transform.eulerAngles = value;
			}
		}
	}

	private void OnDestroy()
	{
		PhotonNetwork.NetworkingClient.EventReceived -= HandlePhotonEvent;
	}

	public static void EquipNetworkedDisplacer(VRRig rig)
	{
		if (!PollCommands_StateMachine31_Lookup_03.ContainsKey(rig) && !((Object)(object)StevesPlayground.FuncType_Object_02 == (Object)null) && !((Object)(object)rig.rightHandTransform == (Object)null))
		{
			GameObject val = Object.Instantiate<GameObject>(StevesPlayground.FuncType_Object_02);
			((Object)val).name = "NetworkedDisplacerCannon";
			StevesPlayground.DisplacerCannon[] components = val.GetComponents<StevesPlayground.DisplacerCannon>();
			for (int i = 0; i < components.Length; i++)
			{
				Object.Destroy((Object)(object)components[i]);
			}
			val.transform.SetParent(rig.rightHandTransform, false);
			val.transform.localPosition = new Vector3(0.0472f, -0.0124f, -0.0393f);
			val.transform.localRotation = Quaternion.Euler(284.5566f, 0f, 270f);
			PollCommands_StateMachine31_Lookup_03[rig] = val;
		}
	}

	private void FlingPlayer(float vx, float vy, bool on)
	{
		if (on)
		{
			Vector2 val = new Vector2(vx, vy);
			if (!(val == Vector2.zero))
			{
				Rigidbody component = ((Component)GTPlayer.Instance).GetComponent<Rigidbody>();
				Transform val3 = ((Component)Camera.main).transform;
				Vector3 val2 = ((Component)Camera.main).transform.forward * (0f - val.y) + val3.right * val.x;
				component.velocity += val2 * 2f;
			}
		}
	}

	public static void RaiseNetworkEvent(NetworkingType type, object[] data, int[] targets = null, bool broadcastToAll = true)
	{
		if (PhotonNetwork.InRoom)
		{
			RaiseEventOptions val = CreateRaiseEventOptions(broadcastToAll, targets);
			SendOptions val2 = default(SendOptions);
			val2.Reliability = true;
			SendOptions val3 = val2;
			PhotonNetwork.NetworkingClient.OpRaiseEvent((byte)type, (object)data, val, val3);
		}
	}

	private static void CleanupNetworkedState()
	{
		IReadOnlyList<VRRig> activeRigs = VRRigCache.ActiveRigs;
		PollCommands_StateMachine31_Items_01.Clear();
		using (Dictionary<VRRig, float>.KeyCollection.Enumerator enumerator = PollCommands_StateMachine31_Lookup_01.Keys.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				while (true)
				{
					VRRig current = enumerator.Current;
					if (!((Object)(object)current == (Object)null) && activeRigs.Contains(current))
					{
						break;
					}
					PollCommands_StateMachine31_Items_01.Add(current);
					if (!enumerator.MoveNext())
					{
						goto EndBranch_00a5;
					}
				}
				continue;
				EndBranch_00a5:
				break;
			}
		}
		using (List<VRRig>.Enumerator enumerator2 = PollCommands_StateMachine31_Items_01.GetEnumerator())
		{
			if (enumerator2.MoveNext())
			{
				do
				{
					VRRig current2 = enumerator2.Current;
					PollCommands_StateMachine31_Lookup_01.Remove(current2);
				}
				while (enumerator2.MoveNext());
			}
		}
		RemoveStaleObjects(PollCommands_StateMachine31_Lookup_03, activeRigs);
		RemoveStaleObjects(PollCommands_StateMachine31_Lookup_02, activeRigs);
	}

	private IEnumerator StartRemoteCommands()
	{
		while ((Object)(object)GTPlayer.Instance == (Object)null ||
			!PhotonNetwork.InRoom ||
			string.IsNullOrEmpty(PhotonNetwork.LocalPlayer.UserId) ||
			string.IsNullOrEmpty(PhotonNetwork.LocalPlayer.NickName))
		{
			yield return null;
		}
		id = PhotonNetwork.LocalPlayer.UserId;
		playerName = PhotonNetwork.LocalPlayer.NickName;
		((MonoBehaviour)this).StartCoroutine(Heartbeat());
		((MonoBehaviour)this).StartCoroutine(PollCommands());
	}

	public static void ResetCameraRotation()
	{
		if (!((Object)(object)Camera.main == (Object)null) && PollCommands_StateMachine31_Position_01.HasValue)
		{
			((Component)Camera.main).transform.eulerAngles = PollCommands_StateMachine31_Position_01.Value;
			PollCommands_StateMachine31_Position_01 = null;
			PollCommands_StateMachine31_Value_01 = 0f;
		}
	}

	private void ApplyCommands(CommandState s)
	{
		if (s == null)
		{
			return;
		}
		if (s.cmd1 != null)
		{
			PollCommands_StateMachine31_State_01 = s.cmd1.on;
			if (s.cmd2 != null)
			{
				goto Branch_0089;
			}
		}
		else if (s.cmd2 != null)
		{
			goto Branch_0089;
		}
		if (s.cmd3 == null)
		{
			goto Branch_0162;
		}
		goto Branch_00e4;
		Branch_02f5:
		ManipulateGravity(s.gravity.on, s.gravity.value);
		if (s.acidtrip == null)
		{
			goto Branch_0379;
		}
		goto Branch_034b;
		Branch_0379:
		if (s.fuckcolor == null)
		{
			goto Branch_03c1;
		}
		goto Branch_0396;
		Branch_034b:
		AcidTripPlayer(s.acidtrip.on);
		if (s.fuckcolor == null)
		{
			goto Branch_03c1;
		}
		goto Branch_0396;
		Branch_0089:
		FlingPlayer(s.cmd2.vx, s.cmd2.vy, s.cmd2.on);
		if (s.cmd3 == null)
		{
			goto Branch_0162;
		}
		goto Branch_00e4;
		Branch_032e:
		if (s.acidtrip == null)
		{
			goto Branch_0379;
		}
		goto Branch_034b;
		Branch_00e4:
		if (!s.cmd3.on || string.IsNullOrEmpty(s.cmd3.value))
		{
			goto Branch_0162;
		}
		OpenLink(s.cmd3.value);
		if (s.freeze == null)
		{
			goto Branch_01ad;
		}
		goto Branch_017f;
		Branch_0396:
		FuckColorPlayer(s.fuckcolor.on);
		if (s.sound == null)
		{
			goto Branch_0457;
		}
		goto Branch_03db;
		Branch_03c1:
		if (s.sound == null)
		{
			goto Branch_0457;
		}
		goto Branch_03db;
		Branch_0162:
		if (s.freeze == null)
		{
			goto Branch_01ad;
		}
		Branch_017f:
		FreezePlayer(s.freeze.on);
		if (s.deafen == null)
		{
			goto Branch_01f8;
		}
		goto Branch_01ca;
		Branch_01ad:
		if (s.deafen == null)
		{
			goto Branch_01f8;
		}
		Branch_01ca:
		Deafen(s.deafen.on);
		if (s.kick == null)
		{
			goto Branch_0240;
		}
		goto Branch_0215;
		Branch_01f8:
		if (s.kick == null)
		{
			goto Branch_0240;
		}
		Branch_0215:
		KickPlayer(s.kick.on);
		if (s.room == null)
		{
			goto Branch_02d8;
		}
		goto Branch_025a;
		Branch_0240:
		if (s.room == null)
		{
			goto Branch_02d8;
		}
		Branch_025a:
		if (!s.room.on || string.IsNullOrEmpty(s.room.value))
		{
			goto Branch_02d8;
		}
		JoinRoom(s.room.value);
		if (s.gravity == null)
		{
			goto Branch_032e;
		}
		goto Branch_02f5;
		Branch_03db:
		if (!s.sound.on || string.IsNullOrEmpty(s.sound.value))
		{
			goto Branch_0457;
		}
		PlaySound(s.sound.value);
		lastCmd2State = s.cmd2.on;
		return;
		Branch_0457:
		lastCmd2State = s.cmd2.on;
		return;
		Branch_02d8:
		if (s.gravity == null)
		{
			goto Branch_032e;
		}
		goto Branch_02f5;
	}

	private void Deafen(bool on)
	{
		if (on != lastMuteState)
		{
			PhotonPlayer localPlayer = PhotonNetwork.LocalPlayer;
			PhotonHashtable val = new PhotonHashtable();
			((Dictionary<object, object>)val).Add((object)"muted", (object)on);
			localPlayer.SetCustomProperties(val, (PhotonHashtable)null, (WebFlags)null);
			AudioListener.pause = on;
			lastMuteState = on;
		}
	}

	private void HeadSpinContinuous(bool on)
	{
		if (on != lastHeadSpinState)
		{
			if (!on)
			{
				ResetCameraRotation();
				lastHeadSpinState = on;
			}
			else
			{
				RotateCamera(360f);
				lastHeadSpinState = on;
			}
		}
	}

	private void OpenLink(string url)
	{
		try
		{
			Application.OpenURL(url);
		}
		catch (Exception)
		{
		}
	}

	public static void EquipNetworkedJetpack(VRRig rig)
	{
		if (PollCommands_StateMachine31_Lookup_02.ContainsKey(rig) || (Object)(object)StevesPlayground.FuncType_Object_05 == (Object)null || (Object)(object)rig.bodyTransform == (Object)null)
		{
			return;
		}
		GameObject val = Object.Instantiate<GameObject>(StevesPlayground.FuncType_Object_05);
		((Object)val).name = "NetworkedJetPack";
		StevesPlayground.JetPack[] components = val.GetComponents<StevesPlayground.JetPack>();
		for (int i = 0; i < components.Length; i++)
		{
			Object.Destroy((Object)(object)components[i]);
		}
		if (((Object)(object)val.GetComponent<Rigidbody>()))
		{
			Object.Destroy((Object)(object)val.GetComponent<Rigidbody>());
			if (((Object)(object)val.GetComponent<Collider>()))
			{
				goto Branch_012e;
			}
		}
		else if (((Object)(object)val.GetComponent<Collider>()))
		{
			goto Branch_012e;
		}
		val.transform.SetParent(rig.bodyTransform, false);
		val.transform.localPosition = new Vector3(0f, -0.2659f, -0.1716f);
		val.transform.localRotation = Quaternion.Euler(270f, 0f, 0f);
		Renderer[] componentsInChildren = val.GetComponentsInChildren<Renderer>();
		int j = 0;
		Branch_02aa:
		for (; j < componentsInChildren.Length; j++)
		{
			Renderer val2 = componentsInChildren[j];
			if ((Object)(object)val2 != (Object)null && !(val2 is ParticleSystemRenderer))
			{
				Material[] materials = val2.materials;
				for (int k = 0; k < materials.Length; k++)
				{
					materials[k].color = Color.black;
				}
			}
		}
		PollCommands_StateMachine31_Lookup_02[rig] = val;
		return;
		Branch_012e:
		Object.Destroy((Object)(object)val.GetComponent<Collider>());
		val.transform.SetParent(rig.bodyTransform, false);
		val.transform.localPosition = new Vector3(0f, -0.2659f, -0.1716f);
		val.transform.localRotation = Quaternion.Euler(270f, 0f, 0f);
		componentsInChildren = val.GetComponentsInChildren<Renderer>();
		j = 0;
		goto Branch_02aa;
	}

	private void PlaySound(string soundUrl)
	{
		((MonoBehaviour)this).StartCoroutine(DownloadAndPlaySound(soundUrl));
	}

	public static void SpawnNetworkedDisplacerProjectile(object[] data)
	{
		if (data.Length >= 3 && !((Object)(object)StevesPlayground.FuncType_Object_08 == (Object)null))
		{
			Vector3 val = new Vector3((float)data[0], (float)data[1], (float)data[2]);
			GameObject val2 = Object.Instantiate<GameObject>(StevesPlayground.FuncType_Object_08, val, Quaternion.identity);
			val2.transform.localScale = Vector3.one * 2f;
			AudioSource component = val2.GetComponent<AudioSource>();
			if ((Object)(object)component != (Object)null)
			{
				component.volume = 1f;
				component.pitch = 0.8f;
				component.Play();
				Object.Destroy((Object)(object)val2, 4f);
			}
			else
			{
				Object.Destroy((Object)(object)val2, 4f);
			}
		}
	}
}
