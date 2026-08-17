using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using ExitGames.Client.Photon;
using GorillaLocomotion;
using GorillaNetworking;
using HarmonyLib;
using NXO.Mods.Categories;
using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.Internal;
using PlayFab.SharedModels;
using UnityEngine;
using CosmeticItem = GorillaNetworking.CosmeticsController.CosmeticItem;
using Player = Photon.Realtime.Player;

namespace NXO.Utilities;

[HarmonyPatch]
public class MenuPatches
{
	[HarmonyPatch(typeof(VRRig), "PostTick")]
	public class RigAnimationPatch
	{
		public static Action RigAnimationPatch_Callback_01;

		[HarmonyPostfix]
		private static void Postfix(VRRig __instance)
		{
			if (__instance.isLocal)
			{
				RigAnimationPatch_Callback_01?.Invoke();
			}
		}
	}

	[HarmonyPatch(typeof(Slingshot), "GetLaunchVelocity")]
	public class SlingshotAimbot
	{
		[CompilerGenerated]
		private sealed class CapturedVariables10
		{
			public List<NetPlayer> excludedPlayers;

			public Transform head;

			internal bool Postfix_Lambda1(VRRig rig)
			{
				return !excludedPlayers.Contains(RigManager.GetPlayer(rig));
			}

			internal float Postfix_Lambda2(VRRig rig)
			{
				Vector3 val = ((Component)rig).transform.position - head.position;
				Vector3 normalized = ((Vector3)val).normalized;
				return Vector3.Angle(head.forward, normalized) + Vector3.Distance(head.position, ((Component)rig).transform.position) * 0.1f;
			}
		}

		public static bool CapturedVariables10_State_01;

		[HarmonyPostfix]
		public static void Postfix(Slingshot __instance, ref Vector3 __result)
		{
			CapturedVariables10 LocalScope18 = new CapturedVariables10();
			if (!CapturedVariables10_State_01)
			{
				return;
			}
			if (!((TransferrableObject)__instance).InLeftHand())
			{
				if (InputHandler.IsRightTriggerPressed())
				{
					return;
				}
			}
			else if (InputHandler.IsLeftTriggerPressed())
			{
				return;
			}
			LocalScope18.excludedPlayers = new List<NetPlayer>();
			VRRig val2;
			if (PhotonNetwork.InRoom && (Object)(object)GorillaGameManager.instance != (Object)null)
			{
				int num = (int)GorillaGameManager.instance.GameType() - 1;
				num = (((uint)num <= 10u) ? num : 11) + 29;
				int num2 = num;
				if (num2 != 30)
				{
					GorillaTagManager val = (GorillaTagManager)GorillaGameManager.instance;
					if (val.isCurrentlyTag)
					{
						LocalScope18.excludedPlayers.Add(val.currentIt);
					}
					else
					{
						LocalScope18.excludedPlayers.AddRange(val.currentInfected);
					}
					LocalScope18.head = ((Component)Variables.Variables_Reference_09.headCollider).transform;
					val2 = (from rig in VRRigCache.ActiveRigs
						where !rig.isLocal && (Object)(object)rig != (Object)null
						where !LocalScope18.excludedPlayers.Contains(RigManager.GetPlayer(rig))
						select rig).OrderBy(delegate(VRRig rig)
					{
						Vector3 val6 = ((Component)rig).transform.position - LocalScope18.head.position;
						Vector3 normalized = ((Vector3)val6).normalized;
						return Vector3.Angle(LocalScope18.head.forward, normalized) + Vector3.Distance(LocalScope18.head.position, ((Component)rig).transform.position) * 0.1f;
					}).FirstOrDefault();
					if ((Object)(object)val2 == (Object)null)
					{
						return;
					}
				}
				else
				{
					LocalScope18.head = ((Component)Variables.Variables_Reference_09.headCollider).transform;
					val2 = (from rig in VRRigCache.ActiveRigs
						where !rig.isLocal && (Object)(object)rig != (Object)null
						where !LocalScope18.excludedPlayers.Contains(RigManager.GetPlayer(rig))
						select rig).OrderBy(delegate(VRRig rig)
					{
						Vector3 val6 = ((Component)rig).transform.position - LocalScope18.head.position;
						Vector3 normalized = ((Vector3)val6).normalized;
						return Vector3.Angle(LocalScope18.head.forward, normalized) + Vector3.Distance(LocalScope18.head.position, ((Component)rig).transform.position) * 0.1f;
					}).FirstOrDefault();
					if ((Object)(object)val2 == (Object)null)
					{
						return;
					}
				}
			}
			else
			{
				LocalScope18.head = ((Component)Variables.Variables_Reference_09.headCollider).transform;
				val2 = (from rig in VRRigCache.ActiveRigs
					where !rig.isLocal && (Object)(object)rig != (Object)null
					where !LocalScope18.excludedPlayers.Contains(RigManager.GetPlayer(rig))
					select rig).OrderBy(delegate(VRRig rig)
				{
					Vector3 val6 = ((Component)rig).transform.position - LocalScope18.head.position;
					Vector3 normalized = ((Vector3)val6).normalized;
					return Vector3.Angle(LocalScope18.head.forward, normalized) + Vector3.Distance(LocalScope18.head.position, ((Component)rig).transform.position) * 0.1f;
				}).FirstOrDefault();
				if ((Object)(object)val2 == (Object)null)
				{
					return;
				}
			}
			Vector3 position = val2.headMesh.transform.position;
			Vector3 val3 = val2.LatestVelocity();
			val3.y *= 0.33f;
			Vector3 position2 = ((Component)__instance.center).transform.position;
			float num3 = Vector3.Distance(position2, position) / 20f;
			Vector3 val4 = position + val3 * num3 - position2;
			Vector3 val5 = new Vector3(val4.x, 0f, val4.z);
			float magnitude = ((Vector3)val5).magnitude;
			float y = val4.y;
			float num4 = 0f - Physics.gravity.y;
			float num5 = Mathf.Sqrt(num4 * (y + Mathf.Sqrt(magnitude * magnitude + y * y))) * 2.5f;
			float num6 = num5 * num5;
			float num7 = num6 * num6 - num4 * (num4 * magnitude * magnitude + 2f * y * num6);
			if (num7 <= 0f)
			{
				__result = ((Vector3)val4).normalized * num5;
				return;
			}
			float num8 = Mathf.Atan((num6 - Mathf.Sqrt(num7)) / (num4 * magnitude));
			__result = ((Vector3)val5).normalized * Mathf.Cos(num8) * num5 + Vector3.up * Mathf.Sin(num8) * num5;
		}
	}

	[HarmonyPatch(typeof(PhotonNetworkController), "OnJoinedRoom")]
	public class JoinedRoomPatch
	{
		public static bool JoinedRoomPatch_State_01;

		[HarmonyPrefix]
		private static void Prefix()
		{
			Projectile.ClearProjectileCache();
			if (JoinedRoomPatch_State_01)
			{
				ReflectionCompat.SetField(PhotonNetworkController.Instance, "currentJoinType", (JoinType)6);
			}
		}
	}

	[HarmonyPatch(typeof(PhotonNetworkController), "AttemptToJoinRankedPublicRoom")]
	public class RankedPatch
	{
		public static bool RankedPatch_State_01;

		public static string RankedPatch_Text_02;

		public static string RankedPatch_Text_01;

		[HarmonyPrefix]
		public static bool Prefix(GorillaNetworkJoinTrigger triggeredTrigger, JoinType roomJoinType = (JoinType)0)
		{
			if (RankedPatch_State_01)
			{
				ReflectionCompat.Invoke(PhotonNetworkController.Instance, "AttemptToJoinRankedPublicRoomAsync", triggeredTrigger, RankedPatch_Text_01 ?? ((object)RankedProgressionManager.Instance.GetRankedMatchmakingTier()/*cast due to constrained. prefix*/).ToString(), RankedPatch_Text_02 ?? "PC", roomJoinType);
				return false;
			}
			return true;
		}
	}

	[HarmonyPatch]
	public class FailedToSpawn
	{
		public static bool FailedToSpawn_State_01;

		private static MethodBase TargetMethod()
		{
			return AccessTools.Method("GorillaWrappedSerializer:FailedToSpawn");
		}

		[HarmonyPrefix]
		public static bool Prefix(Component __instance)
		{
			if (!FailedToSpawn_State_01)
			{
				return true;
			}
			((Component)__instance).gameObject.SetActive(false);
			return false;
		}
	}

	[HarmonyPatch(typeof(NewMapsDisplay), "UpdateSlideshow")]
	public class NewMapsDisplayPatch
	{
		[HarmonyPrefix]
		private static bool Prefix()
		{
			return false;
		}
	}

	[HarmonyPatch(typeof(PhotonNetwork), "RunViewUpdate")]
	public class SerializationPatch
	{
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action SerializationPatch_Callback_01;

		public static Func<bool> SerializationPatch_State_01;

		public static event Action OnSerialization
		{
			[CompilerGenerated]
			add
			{
				Action oUHPQZPU = SerializationPatch_Callback_01;
				Action action = oUHPQZPU;
				Action action2 = action;
				Action value2 = (Action)Delegate.Combine(action2, value);
				oUHPQZPU = Interlocked.CompareExchange(ref SerializationPatch_Callback_01, value2, action2);
				action = oUHPQZPU;
				if ((object)action != action2)
				{
					do
					{
						action2 = action;
						value2 = (Action)Delegate.Combine(action2, value);
						action = Interlocked.CompareExchange(ref SerializationPatch_Callback_01, value2, action2);
					}
					while ((object)action != action2);
				}
			}
			[CompilerGenerated]
			remove
			{
				Action oUHPQZPU = SerializationPatch_Callback_01;
				Action action = oUHPQZPU;
				Action action2 = action;
				Action value2 = (Action)Delegate.Remove(action2, value);
				oUHPQZPU = Interlocked.CompareExchange(ref SerializationPatch_Callback_01, value2, action2);
				action = oUHPQZPU;
				if ((object)action != action2)
				{
					do
					{
						action2 = action;
						value2 = (Action)Delegate.Remove(action2, value);
						action = Interlocked.CompareExchange(ref SerializationPatch_Callback_01, value2, action2);
					}
					while ((object)action != action2);
				}
			}
		}

		[HarmonyPrefix]
		private static bool Prefix()
		{
			if (!PhotonNetwork.InRoom)
			{
				return true;
			}
			int num = 9;
			try
			{
				Action oUHPQZPU = SerializationPatch_Callback_01;
				if (oUHPQZPU == null)
				{
					num = 10;
				}
				else
				{
					oUHPQZPU();
				}
				if (num == 10)
				{
				}
			}
			catch (Exception ex)
			{
				Debug.Log((object)("SerializationPatch tick error: " + ex.Message));
			}
			if (SerializationPatch_State_01 == null)
			{
				return true;
			}
			try
			{
				return SerializationPatch_State_01();
			}
			catch (Exception ex)
			{
				Debug.Log((object)("SerializationPatch override error: " + ex.Message));
				SerializationPatch_State_01 = null;
				if ((Object)(object)VRRig.LocalRig != (Object)null)
				{
					((Behaviour)VRRig.LocalRig).enabled = true;
					return true;
				}
				return true;
			}
		}
	}

	[HarmonyPatch]
	public class PlayFabRateLimitPatch
	{
		[HarmonyPatch(typeof(LoadBalancingClient), "OnDisconnectMessageReceived")]
		public class BlockRateLimitDisconnect
		{
			[HarmonyPrefix]
			private static bool Prefix(DisconnectMessage obj)
			{
				if (obj.Code == -35)
				{
					return false;
				}
				return true;
			}
		}

		[HarmonyPatch(typeof(MonoBehaviourPunCallbacks), "OnDisconnected")]
		public class BlockRateLimitDisconnect1
		{
			[HarmonyPrefix]
			private static bool Prefix(DisconnectCause cause)
			{
				if ((int)cause == 12 || (int)cause == 6 || (int)cause == 7)
				{
					return false;
				}
				return true;
			}
		}

		[HarmonyPatch(typeof(PlayFabHttp), "SendErrorEvent")]
		[HarmonyPrefix]
		private static bool FilterRateLimitError(PlayFabRequestCommon request, PlayFabError error)
		{
			if ((int)error.Error == 1214)
			{
				return false;
			}
			return true;
		}
	}

	[HarmonyPatch]
	internal static class PhotonSafeLimiter
	{
		public static int PhotonSafeLimiter_Index_01 = 499;

		private static readonly Queue<float> PhotonSafeLimiter_Value_01 = new Queue<float>();

		private static readonly object PhotonSafeLimiter_Reference_01 = new object();

		public static void ResetPhotonRateLimiter()
		{
			lock (PhotonSafeLimiter_Reference_01)
			{
				PhotonSafeLimiter_Value_01.Clear();
			}
		}

		public static bool TryAcquirePhotonSendSlot()
		{
			lock (PhotonSafeLimiter_Reference_01)
			{
				float realtimeSinceStartup = Time.realtimeSinceStartup;
				if (PhotonSafeLimiter_Value_01.Count > 0)
				{
					while (realtimeSinceStartup - PhotonSafeLimiter_Value_01.Peek() >= 1f)
					{
						PhotonSafeLimiter_Value_01.Dequeue();
						if (PhotonSafeLimiter_Value_01.Count > 0)
						{
							continue;
						}
						break;
					}
				}
				if (PhotonSafeLimiter_Value_01.Count >= PhotonSafeLimiter_Index_01)
				{
					return false;
				}
				PhotonSafeLimiter_Value_01.Enqueue(realtimeSinceStartup);
				return true;
			}
		}
	}

	[HarmonyPatch(typeof(PhotonNetwork), "RaiseEvent", new Type[]
	{
		typeof(byte),
		typeof(object),
		typeof(RaiseEventOptions),
		typeof(SendOptions)
	})]
	internal class Patch_RaiseEvent
	{
		[HarmonyPrefix]
		private static bool Prefix(ref bool __result)
		{
			if (PhotonSafeLimiter.TryAcquirePhotonSendSlot())
			{
				return true;
			}
			__result = false;
			return false;
		}
	}

	[HarmonyPatch(typeof(PhotonNetwork), "RaiseEventInternal", new Type[]
	{
		typeof(byte),
		typeof(object),
		typeof(RaiseEventOptions),
		typeof(SendOptions)
	})]
	internal class Patch_RaiseEventInternal
	{
		[HarmonyPrefix]
		private static bool Prefix(ref bool __result)
		{
			if (PhotonSafeLimiter.TryAcquirePhotonSendSlot())
			{
				return true;
			}
			__result = false;
			return false;
		}
	}

	[HarmonyPatch(typeof(PhotonNetwork), "SendInstantiate")]
	internal class Patch_SendInstantiate
	{
		[HarmonyPrefix]
		private static bool Prefix(ref bool __result)
		{
			if (PhotonSafeLimiter.TryAcquirePhotonSendSlot())
			{
				return true;
			}
			__result = false;
			return false;
		}
	}

	[HarmonyPatch]
	internal class Patch_PhotonNetwork_RPC
	{
		[HarmonyPrefix]
		private static bool Prefix()
		{
			return PhotonSafeLimiter.TryAcquirePhotonSendSlot();
		}

		[HarmonyTargetMethods]
		private static IEnumerable<MethodBase> TargetPhotonNetworkRpcMethods()
		{
			return from x in typeof(PhotonNetwork).GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
				where x.Name == "RPC"
				select x;
		}
	}

	[HarmonyPatch]
	internal class Patch_PhotonView_RPC
	{
		[HarmonyTargetMethods]
		private static IEnumerable<MethodBase> TargetPhotonViewRpcMethods()
		{
			return from x in typeof(PhotonView).GetMethods(BindingFlags.Instance | BindingFlags.Public)
				where x.Name == "RPC" || x.Name == "RpcSecure"
				select x;
		}

		[HarmonyPrefix]
		private static bool Prefix()
		{
			return PhotonSafeLimiter.TryAcquirePhotonSendSlot();
		}
	}

	[HarmonyPatch(typeof(PhotonNetwork), "OnEvent")]
	internal class InterceptPendingReports3
	{
		[CompilerGenerated]
		private sealed class CapturedVariables00
		{
			public EventData photonEvent;

			internal bool Prefix_Lambda2(Player x)
			{
				return x.ActorNumber == photonEvent.Sender;
			}

			internal bool Prefix_Lambda0(Player x)
			{
				return x.ActorNumber == photonEvent.Sender;
			}
		}

		[HarmonyPrefix]
		private static bool Prefix(EventData photonEvent)
		{
			CapturedVariables00 LocalScope5 = new CapturedVariables00();
			LocalScope5.photonEvent = photonEvent;
			if (LocalScope5.photonEvent.Code != 50)
			{
				return true;
			}
			if (LocalScope5.photonEvent.CustomData is object[] array)
			{
				if ((string)array[0] == PhotonNetwork.LocalPlayer.UserId)
				{
					array[0] = (from x in PhotonNetwork.PlayerListOthers
						where x.ActorNumber == LocalScope5.photonEvent.Sender
						select x.UserId).FirstOrDefault();
					NotificationLib.ShowNotification(NotificationLib.NotificationType.Alert, (from x in PhotonNetwork.PlayerListOthers
						where x.ActorNumber == LocalScope5.photonEvent.Sender
						select x.NickName).FirstOrDefault() + " tried to report you");
					return false;
				}
				return true;
			}
			return true;
		}
	}

	[HarmonyPatch]
	public class GroupPatch
	{
		public static bool GroupPatch_State_01;

		private static MethodBase TargetMethod()
		{
			return AccessTools.Method("RoomSystem:SearchForNearby");
		}

		[HarmonyPrefix]
		public static bool Prefix()
		{
			return !GroupPatch_State_01;
		}
	}

	[HarmonyPatch(typeof(GTPlayer), "TakeMyHand_ProcessMovement")]
	public class GrabPatch
	{
		public static bool GrabPatch_State_08;

		[HarmonyPrefix]
		public static bool Prefix(GTPlayer __instance)
		{
			return !GrabPatch_State_08;
		}
	}

	public static bool GrabPatch_State_06;

	public static bool GrabPatch_State_07 = true;

	public static bool GrabPatch_State_03;

	public static float GrabPatch_Value_01 = 99999f;

	public static float GrabPatch_Value_02 = 99999f;

	public static int GrabPatch_Index_01 = 1;

	public static bool GrabPatch_State_05;

	public static bool GrabPatch_State_09;

	public static bool GrabPatch_State_11 = false;

	public static bool GrabPatch_State_12;

	public static bool GrabPatch_State_13;

	public static bool GrabPatch_State_01;

	public static bool GrabPatch_State_10;

	public static bool GrabPatch_State_14;

	public static bool GrabPatch_State_02;

	public static bool GrabPatch_State_04;

	[HarmonyPatch(typeof(MonkeAgent), "IncrementRPCCallLocal")]
	[HarmonyPrefix]
	private static bool IncrementRPCCallLocalPrefix(PhotonMessageInfoWrapped infoWrapped, string rpcFunction)
	{
		return false;
	}

	[HarmonyPatch(typeof(GorillaSpeakerLoudness), "UpdateLoudness")]
	[HarmonyPrefix]
	private static bool MicPrefix(GorillaSpeakerLoudness __instance)
	{
		if (GrabPatch_State_01)
		{
			return ((Object)((Component)__instance).gameObject).name != "Local Gorilla Player";
		}
		return true;
	}

	[HarmonyPatch(typeof(PlayFabHttp), "InitializeScreenTimeTracker")]
	[HarmonyPrefix]
	private static bool InitializeScreenTimeTrackerPrefix()
	{
		return false;
	}

	[HarmonyPatch(typeof(ForceVolume), "OnTriggerStay")]
	[HarmonyPrefix]
	public static bool ForceVolumeStayPrefix()
	{
		return !GrabPatch_State_05;
	}

	[HarmonyPatch(typeof(MonkeAgent), "DispatchReport")]
	[HarmonyPrefix]
	private static bool DispatchReportPrefix()
	{
		return false;
	}

	[HarmonyPatch(typeof(MonkeAgent), "QuitDelay", MethodType.Enumerator)]
	[HarmonyPrefix]
	private static bool QuitDelayPrefix()
	{
		return false;
	}

	[HarmonyPatch(typeof(MonkeAgent), "LogErrorCount")]
	[HarmonyPrefix]
	private static bool LogErrorCountPrefix(string logString, string stackTrace, LogType type)
	{
		return false;
	}

	[HarmonyPatch(typeof(MonkeAgent), "CheckReports")]
	[HarmonyPrefix]
	private static bool CheckReportsPrefix()
	{
		return false;
	}

	[HarmonyPatch(typeof(Player), "SetCustomProperties")]
	[HarmonyPrefix]
	public static bool SetCustomPropertiesPrefix(Player __instance, ref Hashtable propertiesToSet)
	{
		if (__instance.IsLocal && GrabPatch_State_02 && ((IEnumerable<KeyValuePair<object, object>>)propertiesToSet).Any((KeyValuePair<object, object> prop) => prop.Key.ToString() != "didTutorial"))
		{
			return false;
		}
		return true;
	}

	[HarmonyPatch(typeof(GTPlayer), "AntiTeleportTechnology", MethodType.Normal)]
	[HarmonyPrefix]
	private static bool AntiTeleportTechnologyPrefix()
	{
		return false;
	}

	[HarmonyPatch(typeof(MonkeAgent), "GetRPCCallTracker")]
	[HarmonyPrefix]
	private static bool GetRPCCallTrackerPrefix()
	{
		return false;
	}

	[HarmonyPatch(typeof(PlayFabDeviceUtil), "GetAdvertIdFromUnity")]
	[HarmonyPrefix]
	private static bool GetAdvertIdPrefix()
	{
		return false;
	}

	[HarmonyPatch(typeof(GorillaGameManager), "ForceStopGame_DisconnectAndDestroy")]
	[HarmonyPrefix]
	private static bool ForceStopGamePrefix()
	{
		return false;
	}

	[HarmonyPatch(typeof(GTPlayer), "ApplyKnockback")]
	[HarmonyPrefix]
	public static bool ApplyKnockbackPrefix(Vector3 direction, float speed)
	{
		return !GrabPatch_State_13;
	}

	[HarmonyPatch(typeof(VRRig), "SetHandEffectData")]
	[HarmonyPrefix]
	private static bool SetHandEffectDataPrefix(VRRig __instance, object effectContext, int audioClipIndex, bool isDownTap, bool isLeftHand, StiltID stiltID, float handTapVolume, float handTapSpeed, Vector3 dirFromHitToHand)
	{
		if (!GrabPatch_State_06 || !__instance.isLocal)
		{
			return true;
		}
		if (GrabPatch_State_03)
		{
			object surfaceData = ReflectionCompat.Invoke(VRRig.LocalRig, "GetHandSurfaceData", audioClipIndex);
			MaterialData handSurfaceData = surfaceData is MaterialData materialData ? materialData : default(MaterialData);
			AccessTools.Field(effectContext.GetType(), "soundFX")?.SetValue(effectContext, handSurfaceData.audio);
			AccessTools.Field(effectContext.GetType(), "speed")?.SetValue(effectContext, GrabPatch_Value_02);
			AccessTools.Field(effectContext.GetType(), "soundVolume")?.SetValue(effectContext, GrabPatch_Value_01);
			if (PhotonNetwork.InRoom && GrabPatch_Index_01 > 1)
			{
				int num = 0;
				if (num < GrabPatch_Index_01)
				{
					do
					{
						Variables.Variables_Reference_09.myVRRig.SendRPC("RPC_PlayHandTap", (RpcTarget)0, new object[3] { audioClipIndex, isLeftHand, handTapSpeed });
						num++;
					}
					while (num < GrabPatch_Index_01);
				}
				Safety.ResetNetworkLimits();
				return false;
			}
			return false;
		}
		if (!GrabPatch_State_07)
		{
			AccessTools.Field(effectContext.GetType(), "speed")?.SetValue(effectContext, 0f);
			AccessTools.Field(effectContext.GetType(), "soundVolume")?.SetValue(effectContext, 0f);
			Variables.Variables_Reference_09.handTapVolume = 0f;
			Variables.Variables_Reference_09.handTapSpeed = 0f;
			ReflectionCompat.SetField(Variables.Variables_Reference_09, "audioClipIndex", -1);
			return false;
		}
		return true;
	}

	[HarmonyPatch(typeof(MonkeAgent), "SendReport")]
	[HarmonyPrefix]
	private static bool SendReportPrefix(string susReason, string susId, string susNick)
	{
		if (GrabPatch_State_11 && susId == PhotonNetwork.LocalPlayer.UserId)
		{
			NotificationLib.ShowNotification(NotificationLib.NotificationType.Alert, "Anti-Cheat Reported For " + susReason);
			return false;
		}
		return false;
	}

	[HarmonyPatch(typeof(ForceVolume), "OnTriggerEnter")]
	[HarmonyPrefix]
	public static bool ForceVolumeEnterPrefix()
	{
		return !GrabPatch_State_05;
	}

	[HarmonyPatch(typeof(Player), "set_CustomProperties")]
	[HarmonyPrefix]
	public static bool SetCustomPropertiesSetterPrefix(Player __instance, ref Hashtable value)
	{
		if (__instance.IsLocal && GrabPatch_State_02 && ((IEnumerable<KeyValuePair<object, object>>)value).Any((KeyValuePair<object, object> prop) => prop.Key.ToString() != "didTutorial"))
		{
			return false;
		}
		return true;
	}

	[HarmonyPatch(typeof(PlayFabClientAPI), "ReportDeviceInfo")]
	[HarmonyPrefix]
	private static bool ReportDeviceInfoPrefix()
	{
		return false;
	}

	[HarmonyPatch(typeof(LegalAgreements), "Update")]
	[HarmonyPrefix]
	private static bool LegalAgreementsUpdatePrefix(LegalAgreements __instance)
	{
		if (!GrabPatch_State_04)
		{
			return true;
		}
		((ControllerInputPoller)ControllerInputPoller.instance).leftControllerPrimary2DAxis.y = -1f;
		ReflectionCompat.SetField(__instance, "scrollSpeed", 10f);
		ReflectionCompat.SetField(__instance, "_maxScrollSpeed", 10f);
		return false;
	}

	[HarmonyPatch(typeof(VRRig), "OnDisable")]
	[HarmonyPrefix]
	private static bool OnDisablePrefix(VRRig __instance)
	{
		return !__instance.isLocal;
	}

	[HarmonyPatch(typeof(GrowingSnowballThrowable), "OnEnable")]
	[HarmonyPostfix]
	public static void SnowballPostfix(GrowingSnowballThrowable __instance)
	{
		if (GrabPatch_State_14)
		{
			__instance.IncreaseSize(5);
		}
		else if (GrabPatch_State_10)
		{
			__instance.IncreaseSize(Settings.CapturedVariables3760_Index_29);
		}
	}

	[HarmonyPatch(typeof(MonkeAgent), "IncrementRPCCall", new Type[]
	{
		typeof(PhotonMessageInfo),
		typeof(string)
	})]
	[HarmonyPrefix]
	private static bool IncrementRPCCallPrefix(PhotonMessageInfo info, string callingMethod = "")
	{
		return false;
	}

	[HarmonyPatch(typeof(MonkeAgent), "ShouldDisconnectFromRoom")]
	[HarmonyPrefix]
	private static bool ShouldDisconnectFromRoomPrefix()
	{
		return false;
	}

	[HarmonyPatch(typeof(VRRig), "Awake")]
	[HarmonyPrefix]
	private static bool AwakePrefix(VRRig __instance)
	{
		return ((Object)((Component)__instance).gameObject).name != "Local Gorilla Player(Clone)";
	}

	[HarmonyPatch(typeof(CosmeticsController), "PressWardrobeItemButton", new Type[]
	{
		typeof(CosmeticItem),
		typeof(bool),
		typeof(bool)
	})]
	[HarmonyPrefix]
	public static void WardrobeItemPrefix(CosmeticsController __instance, CosmeticItem cosmeticItem)
	{
		try
		{
			if (Fun.ServerSideEquipRoutine_StateMachine101_State_06 && !((Object)(object)__instance == (Object)null) && !cosmeticItem.isNullItem && !string.IsNullOrEmpty(cosmeticItem.itemName) && Fun.ServerSideEquipRoutine_StateMachine101_Text_02.Contains(cosmeticItem.itemName) && !__instance.IsCosmeticEquipped(cosmeticItem))
			{
				Fun.StartServerSideEquip();
			}
		}
		catch (Exception)
		{
		}
	}

	[HarmonyPatch(typeof(ForceVolume), "OnTriggerExit")]
	[HarmonyPrefix]
	public static bool ForceVolumeExitPrefix()
	{
		return !GrabPatch_State_05;
	}

	[HarmonyPatch(typeof(PlayFabClientAPI), "AttributeInstall")]
	[HarmonyPrefix]
	private static bool AttributeInstallPrefix()
	{
		return false;
	}

	[HarmonyPatch(typeof(VRRig), "IsPositionInRange")]
	[HarmonyPostfix]
	public static void Postfix(VRRig __instance, ref bool __result, Vector3 position, float range)
	{
		NetPlayer val = RigManager.GetPlayer(__instance) ?? null;
		if (!GrabPatch_State_09 || !__instance.isLocal)
		{
			if (val != NetworkSystem.Instance.LocalPlayer)
			{
				return;
			}
		}
		__result = true;
	}

	[HarmonyPatch(typeof(KIDManager), "HasPermissionToUseFeature")]
	[HarmonyPostfix]
	public static void GrantFeaturePermission(ref bool __result)
	{
		if (GrabPatch_State_12)
		{
			__result = true;
		}
	}

	[HarmonyPatch(typeof(GorillaNetworkPublicTestJoin2), "GracePeriod")]
	[HarmonyPrefix]
	private static bool GracePeriod2Prefix()
	{
		return false;
	}

	[HarmonyPatch(typeof(GTPlayer), "GetSlidePercentage")]
	[HarmonyPostfix]
	private static void GetSlidePercentagePostfix(ref float __result)
	{
		if (Movement.Movement_State_10)
		{
			__result = 0f;
		}
		else if (Movement.Movement_State_01)
		{
			__result = 1f;
		}
	}

	[HarmonyPatch(typeof(KIDManager), "UseKID")]
	[HarmonyPrefix]
	private static bool UseKidPrefix(ref Task<bool> __result)
	{
		if (!GrabPatch_State_04)
		{
			return true;
		}
		__result = Task.FromResult(result: false);
		return false;
	}

	[HarmonyPatch(typeof(PlayFabClientAPI), "ReportPlayer", MethodType.Normal)]
	[HarmonyPrefix]
	private static bool ReportPlayerPrefix(ReportPlayerClientRequest request, Action<ReportPlayerClientResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
	{
		return false;
	}

	[HarmonyPatch(typeof(PlayFabClientInstanceAPI), "ReportDeviceInfo")]
	[HarmonyPrefix]
	private static bool ReportDeviceInfoInstancePrefix()
	{
		return false;
	}

	[HarmonyPatch(typeof(AgeSlider), "PostUpdate")]
	[HarmonyPrefix]
	private static bool AgeSliderPostUpdatePrefix(AgeSlider __instance)
	{
		if (!GrabPatch_State_04)
		{
			return true;
		}
		ReflectionCompat.SetField(__instance, "_currentAge", 21);
		ReflectionCompat.SetField(__instance, "holdTime", 0.1f);
		return false;
	}

	[HarmonyPatch(typeof(GorillaNetworkPublicTestsJoin), "GracePeriod")]
	[HarmonyPrefix]
	private static bool GracePeriod1Prefix()
	{
		return false;
	}

	[HarmonyPatch(typeof(GorillaTelemetry), "IsConnected")]
	[HarmonyPrefix]
	private static bool IsConnectedPrefix()
	{
		return false;
	}

	[HarmonyPatch(typeof(PlayFabDeviceUtil), "SendDeviceInfoToPlayFab")]
	[HarmonyPrefix]
	private static bool SendDeviceInfoPrefix()
	{
		return false;
	}

	[HarmonyPatch(typeof(VRRig), "IncrementRPC", new Type[]
	{
		typeof(PhotonMessageInfoWrapped),
		typeof(string)
	})]
	[HarmonyPrefix]
	private static bool IncrementRPCPrefix(PhotonMessageInfoWrapped info, string sourceCall)
	{
		return false;
	}

	[HarmonyPatch(typeof(VRRig), "PostTick")]
	[HarmonyPrefix]
	private static bool PostTickPrefix(VRRig __instance)
	{
		if (__instance.isLocal)
		{
			return ((Behaviour)__instance).enabled;
		}
		return true;
	}

	[HarmonyPatch(typeof(GameObject), "CreatePrimitive", MethodType.Normal)]
	[HarmonyPostfix]
	private static void CreatePrimitivePostfix(GameObject __result)
	{
		Renderer component = __result.GetComponent<Renderer>();
		if ((Object)(object)component != (Object)null)
		{
			Material material = component.material;
			material.shader = Variables.Variables_Reference_10;
			material.color = Color.blue;
		}
	}

	[HarmonyPatch(typeof(PrivateUIRoom), "StartOverlay")]
	[HarmonyPrefix]
	private static bool PrivateRoomOverlayPrefix()
	{
		return !GrabPatch_State_04;
	}

	[HarmonyPatch(typeof(ModIOTermsOfUse_v1), "PostUpdate")]
	[HarmonyPrefix]
	private static bool ModIoTermsPostUpdatePrefix(ModIOTermsOfUse_v1 __instance)
	{
		if (!GrabPatch_State_04)
		{
			return true;
		}
		__instance.TurnPage(999);
		((ControllerInputPoller)ControllerInputPoller.instance).leftControllerPrimary2DAxis.y = -1f;
		ReflectionCompat.SetField(__instance, "holdTime", 0.1f);
		return false;
	}

	[HarmonyPatch(typeof(PlayFabClientInstanceAPI), "ReportPlayer", MethodType.Normal)]
	[HarmonyPrefix]
	private static bool ReportPlayerInstancePrefix(ReportPlayerClientRequest request, Action<ReportPlayerClientResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
	{
		return false;
	}
}
