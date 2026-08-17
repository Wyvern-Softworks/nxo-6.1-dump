using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using ExitGames.Client.Photon;
using GorillaLocomotion;
using GorillaNetworking;
using HarmonyLib;
using NXO.Menu;
using NXO.Utilities;
using Photon.Pun;
using Photon.Realtime;
using Photon.Voice.PUN;
using Photon.Voice.Unity;
using UnityEngine;

namespace NXO.Mods.Categories;

public static class Overpowered
{
	private enum StatusEffects
	{
		Slow,
		Vibrate
	}

	private static int GetElevatorLocation()
	{
		object location = ReflectionCompat.GetField<object>(GRElevatorManager._instance, "currentLocation");
		return location != null ? Convert.ToInt32(location) : 0;
	}

	private static string GetRigOwnerUserId(VRRig rig)
	{
		return GetPhotonView(rig)?.Owner?.UserId;
	}

	[CompilerGenerated]
	private sealed class CapturedVariables710
	{
		public NetPlayer player;

		internal void StumpKickGun_Lambda0()
		{
			PhotonNetworkController instance = PhotonNetworkController.Instance;
			int num = Random.Range(0, 99);
			int num2 = num;
			string text = num2.ToString().PadLeft(2, '0');
			num = Random.Range(0, 99999999);
			num2 = num;
			((PhotonNetworkController)instance).shuffler = text + num2.ToString().PadLeft(8, '0');
			PhotonNetworkController instance2 = PhotonNetworkController.Instance;
			num = Random.Range(0, 99999999);
			num2 = num;
			((PhotonNetworkController)instance2).keyStr = num2.ToString().PadLeft(8, '0');
			SendFriendJoinEvent(((GorillaComputer)GorillaComputer.instance).friendJoinCollider, RigManager.GetPhotonPlayer(player));
			Safety.ResetNetworkLimits();
		}
	}

	[CompilerGenerated]
	private sealed class StumpKickDelay_StateMachine68 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int State;

		private object Current;

		public Action action;

		public Action action2;

		public float extraDelay;

		public bool changeQueue;

		private bool joinedRoomPatchEnabledCaptured1;

		private string queueArchiveCaptured2;

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

		private bool MoveNext()
		{
			int num = State;
			num = (((uint)num <= 4u) ? num : 5) + 21;
			int num2 = num;
			if (num2 != 22)
			{
				State = -1;
				((PhotonNetworkController)PhotonNetworkController.Instance).FriendIDList.Clear();
				Current = (object)new WaitForSeconds(extraDelay);
				State = 1;
				return true;
			}
			State = -1;
			joinedRoomPatchEnabledCaptured1 = MenuPatches.JoinedRoomPatch.JoinedRoomPatch_State_01;
			queueArchiveCaptured2 = ((GorillaComputer)GorillaComputer.instance).currentQueue;
			if (changeQueue)
			{
				((GorillaComputer)GorillaComputer.instance).currentQueue = Room.GenerateRoomCode();
				Action obj = action;
				if (obj != null)
				{
					obj();
					Current = (object)new WaitForSeconds(0.3f);
					State = 2;
				}
			}
			else
			{
				Action obj2 = action;
				if (obj2 != null)
				{
					obj2();
					Current = (object)new WaitForSeconds(0.3f);
					State = 2;
				}
			}
			Current = (object)new WaitForSeconds(0.3f);
			State = 2;
			return true;
		}

		bool IEnumerator.MoveNext()
		{
			return this.MoveNext();
		}

		[DebuggerHidden]
		public StumpKickDelay_StateMachine68(int State)
		{
			this.State = State;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			queueArchiveCaptured2 = null;
			State = -2;
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	private static float StumpKickDelay_StateMachine68_Value_01 = 0f;

	private static readonly Dictionary<VRRig, int> StumpKickDelay_StateMachine68_Lookup_01 = new Dictionary<VRRig, int>();

	public static float StumpKickDelay_StateMachine68_Value_03;

	private static float StumpKickDelay_StateMachine68_Value_05;

	private static float StumpKickDelay_StateMachine68_Value_07;

	public static Coroutine StumpKickDelay_StateMachine68_Routine_01;

	private static DeployableObject StumpKickDelay_StateMachine68_Reference_01;

	private static GameObject StumpKickDelay_StateMachine68_Object_01;

	private static float StumpKickDelay_StateMachine68_Value_04 = 0f;

	private static float StumpKickDelay_StateMachine68_Value_02 = 0f;

	public static int StumpKickDelay_StateMachine68_Index_01 = 0;

	private static float StumpKickDelay_StateMachine68_Value_09 = 0f;

	public static float StumpKickDelay_StateMachine68_Value_08;

	public static bool StumpKickDelay_StateMachine68_State_01 = false;

	public static string StumpKickDelay_StateMachine68_Text_01;

	private static float StumpKickDelay_StateMachine68_Value_06;

	private static bool StumpKickDelay_StateMachine68_State_02;

	public static float StumpKickDelay_StateMachine68_Value_10;

	private static VRRig StumpKickDelay_StateMachine68_Reference_02;

	private static Vector3 StumpKickDelay_StateMachine68_Position_01;

	public static readonly List<LineRenderer> StumpKickDelay_StateMachine68_Items_01 = new List<LineRenderer>();

	public static void GrabMetroCrashGun()
	{
		RunGrabGun(GetMetroCrashPosition);
	}

	public static void GrabBringAll()
	{
		RunGrabAll(GetBringPosition);
	}

	public static bool CanCall(this CallLimiter limiter, float? time = null)
	{
		if (limiter == null)
		{
			return false;
		}
		float[] callTimeHistory = ReflectionCompat.GetField<float[]>(limiter, "callTimeHistory");
		int callHistoryLength = Math.Min(ReflectionCompat.GetField(limiter, "callHistoryLength", 0), callTimeHistory?.Length ?? 0);
		if (callTimeHistory == null || callHistoryLength <= 0)
		{
			return true;
		}
		int oldTimeIndex = Mathf.Clamp(ReflectionCompat.GetField(limiter, "oldTimeIndex", 0), 0, callHistoryLength - 1);
		float nextAllowedTime = callTimeHistory[oldTimeIndex];
		return nextAllowedTime == float.MinValue || nextAllowedTime <= (time ?? Time.time);
	}

	public static void SlowGun()
	{
		if (Variables.IsMasterClient() && GunLib.TrySelectRig() && Time.time > StumpKickDelay_StateMachine68_Value_05)
		{
			RaiseEventOptions val = new RaiseEventOptions();
			val.TargetActors = new int[1] { GunLib.GunLib_Reference_06.Creator.ActorNumber };
			SendStatusEffect((StatusEffects)0, val);
			StumpKickDelay_StateMachine68_Value_05 = Time.time + 1f;
		}
	}

	public static void MatSpamGun()
	{
		if (Variables.IsMasterClient() && GunLib.TrySelectRig() && Time.time > StumpKickDelay_StateMachine68_Value_03)
		{
			StumpKickDelay_StateMachine68_Value_03 = Time.time + 0.1f;
			CycleRigTagState(GunLib.GunLib_Reference_06);
		}
	}

	public static void TouchToMatSpam()
	{
		if (!Variables.IsMasterClient())
		{
			return;
		}
		Transform[] hands = (Transform[])(object)new Transform[2]
		{
			Variables.Variables_Reference_09.leftHandTransform,
			Variables.Variables_Reference_09.rightHandTransform
		};
		using IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			while (true)
			{
				VRRig current = enumerator.Current;
				if ((Object)(object)current == (Object)null || current.isOfflineVRRig || current.isLocal)
				{
					break;
				}
				if (RigManager.AreHandsNearTargets(hands, (Transform[])(object)new Transform[2]
				{
					current.headMesh.transform,
					current.bodyTransform
				}))
				{
					if (Time.time > StumpKickDelay_StateMachine68_Value_03)
					{
						StumpKickDelay_StateMachine68_Value_03 = Time.time + 0.1f;
						CycleRigTagState(current);
					}
					return;
				}
				if (!enumerator.MoveNext())
				{
					return;
				}
			}
		}
	}

	public static void SpazAllTargets()
	{
		if (!Variables.IsMasterClient())
		{
			return;
		}
		HitTargetNetworkState[] array = Variables.FindObjectsCached<HitTargetNetworkState>(false);
		int num = 0;
		if (num < array.Length)
		{
			do
			{
				HitTargetNetworkState val = array[num];
				ReflectionCompat.SetField(val, "hitCooldownTime", 0);
				val.TargetHit(Vector3.zero, Vector3.zero);
				num++;
			}
			while (num < array.Length);
		}
	}

	private static void SendStatusEffect(StatusEffects statusEffect, RaiseEventOptions options)
	{
		if (Variables.IsMasterClient())
		{
			object[] array = new object[1] { (int)statusEffect };
			PhotonNetwork.RaiseEvent((byte)3, (object)new object[3]
			{
				NetworkSystem.Instance.ServerTimestamp,
				(byte)2,
				array
			}, options, SendOptions.SendUnreliable);
			Safety.ResetNetworkLimits();
		}
	}

	public static void SlowAura()
	{
		if (!Variables.IsMasterClient() || !PhotonNetwork.InRoom || Time.time < StumpKickDelay_StateMachine68_Value_05)
		{
			return;
		}
		Vector3 position = ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position;
		using IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			while (true)
			{
				VRRig current = enumerator.Current;
				if ((Object)(object)current == (Object)null || current.isOfflineVRRig || current.isLocal)
				{
					break;
				}
				if (Vector3.Distance(position, ((Component)current).transform.position) < 4f)
				{
					StumpKickDelay_StateMachine68_Value_05 = Time.time + 1f;
					RaiseEventOptions val = new RaiseEventOptions();
					val.TargetActors = new int[1] { current.Creator.ActorNumber };
					SendStatusEffect((StatusEffects)0, val);
					return;
				}
				if (!enumerator.MoveNext())
				{
					return;
				}
			}
		}
	}

	public static void GrabFlingAll()
	{
		RunGrabAll(GetRandomFlingPosition);
	}

	public static void ResetStumpKickToSpecificRoom()
	{
		StumpKickDelay_StateMachine68_Text_01 = null;
		StumpKickDelay_StateMachine68_State_02 = false;
		ButtonHandler.Button button;
		if (SearchAndKeyboard.KeyCollider_State_02)
		{
			SearchAndKeyboard.SubmitKeyboardInput(cancelled: true);
			button = ModButtons.buttons.FirstOrDefault((ButtonHandler.Button b) => b != null && b.buttonText?.StartsWith("Kick To Specific Room") == true);
			if (button != null)
			{
				goto Branch_00af;
			}
		}
		else
		{
			button = ModButtons.buttons.FirstOrDefault((ButtonHandler.Button b) => b != null && b.buttonText?.StartsWith("Kick To Specific Room") == true);
			if (button != null)
			{
				goto Branch_00af;
			}
		}
		NotificationLib.ShowNotification(NotificationLib.NotificationType.Deleted, "Kick Room Cleared");
		return;
		Branch_00af:
		button.SetText("Kick To Specific Room");
		NotificationLib.ShowNotification(NotificationLib.NotificationType.Deleted, "Kick Room Cleared");
	}

	public static void DestroyAll()
	{
		using IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.GetEnumerator();
		if (!enumerator.MoveNext())
		{
			return;
		}
		while (true)
		{
			VRRig current = enumerator.Current;
			if ((Object)(object)current == (Object)null || current.isOfflineVRRig || current.isLocal)
			{
				PhotonNetwork.OpRemoveCompleteCacheOfPlayer(current.Creator.ActorNumber);
				if (!enumerator.MoveNext())
				{
					break;
				}
			}
			else if (!enumerator.MoveNext())
			{
				break;
			}
		}
	}

	private static void RunGrabGun(Func<VRRig, Vector3> posFor)
	{
		UpdateGrabLines();
		if (GunLib.TrySelectRig())
		{
			GrabRigAtPosition(GunLib.GunLib_Reference_06, posFor(GunLib.GunLib_Reference_06));
			if (!AreGrabInputsReleased())
			{
				return;
			}
		}
		else
		{
			((Behaviour)VRRig.LocalRig).enabled = true;
			if (!AreGrabInputsReleased())
			{
				return;
			}
		}
		MenuPatches.GrabPatch.GrabPatch_State_08 = false;
		VRRig.LocalRig.BreakHandLinks();
		((Behaviour)VRRig.LocalRig).enabled = true;
	}

	public static void StumpKickToSpecificRoom()
	{
		if (SearchAndKeyboard.KeyCollider_State_02 || StumpKickDelay_StateMachine68_State_02)
		{
			return;
		}
		SearchAndKeyboard.OpenTextInput(StumpKickDelay_StateMachine68_Text_01 ?? "", "Enter room code...");
		SearchAndKeyboard.KeyCollider_Text_01 = delegate(string code)
		{
			StumpKickDelay_StateMachine68_State_02 = true;
			ButtonHandler.Button button = ModButtons.buttons.FirstOrDefault((ButtonHandler.Button b) => b != null && b.buttonText?.StartsWith("Kick To Specific Room") == true);
			if (string.IsNullOrEmpty(code))
			{
				StumpKickDelay_StateMachine68_Text_01 = null;
				if (button != null)
				{
					button.SetText("Kick To Specific Room");
					NotificationLib.ShowNotification(NotificationLib.NotificationType.Deleted, "Kick Room Cleared");
				}
				else
				{
					NotificationLib.ShowNotification(NotificationLib.NotificationType.Deleted, "Kick Room Cleared");
				}
			}
			else
			{
				StumpKickDelay_StateMachine68_Text_01 = code;
				if (button != null)
				{
					button.SetText("Kick To Specific Room : " + code);
					NotificationLib.ShowNotification(NotificationLib.NotificationType.Saved, "Kick Room Set To `" + code + "`");
				}
				else
				{
					NotificationLib.ShowNotification(NotificationLib.NotificationType.Saved, "Kick Room Set To `" + code + "`");
				}
			}
		};
		SearchAndKeyboard.KeyCollider_Callback_01 = delegate
		{
			ButtonHandler.Button button = ModButtons.buttons.FirstOrDefault((ButtonHandler.Button b) => b != null && b.buttonText?.StartsWith("Kick To Specific Room") == true);
			if (button != null)
			{
				button.Enabled = false;
				StumpKickDelay_StateMachine68_Text_01 = null;
				StumpKickDelay_StateMachine68_State_02 = false;
				if (button == null)
				{
					return;
				}
			}
			else
			{
				StumpKickDelay_StateMachine68_Text_01 = null;
				StumpKickDelay_StateMachine68_State_02 = false;
				if (button == null)
				{
					return;
				}
			}
			button.SetText("Kick To Specific Room");
		};
	}

	public static void GetTouchedToMatSpam()
	{
		if (!Variables.IsMasterClient())
		{
			return;
		}
		Transform[] targets = (Transform[])(object)new Transform[2]
		{
			Variables.Variables_Reference_09.offlineVRRig.headMesh.transform,
			Variables.Variables_Reference_09.offlineVRRig.bodyTransform
		};
		using IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			while (true)
			{
				VRRig current = enumerator.Current;
				if ((Object)(object)current == (Object)null || current.isOfflineVRRig || current.isMyPlayer)
				{
					break;
				}
				if (RigManager.AreHandsNearTargets((Transform[])(object)new Transform[2] { current.leftHandTransform, current.rightHandTransform }, targets))
				{
					if (Time.time > StumpKickDelay_StateMachine68_Value_03)
					{
						StumpKickDelay_StateMachine68_Value_03 = Time.time + 0.1f;
						CycleRigTagState(current);
					}
					return;
				}
				if (!enumerator.MoveNext())
				{
					return;
				}
			}
		}
	}

	public static void TagPlayer(NetPlayer plr)
	{
		if (!PhotonNetwork.InRoom || (Object)(object)GorillaGameManager.instance == (Object)null || plr == null)
		{
			return;
		}
		int num = (int)GorillaGameManager.instance.GameType() - 1;
		num = (((uint)num <= 10u) ? num : 11) + 221;
		int num2 = num;
		if (num2 == 222)
		{
			return;
		}
		GorillaGameManager instance = GorillaGameManager.instance;
		GorillaTagManager val = (GorillaTagManager)(object)((instance is GorillaTagManager) ? instance : null);
		if (val != null)
		{
			if (val.isCurrentlyTag)
			{
				ReflectionCompat.Invoke(val, "ChangeCurrentIt", plr, true);
			}
			else if (val.currentInfected != null && !val.currentInfected.Contains(plr))
			{
				val.AddInfectedPlayer(plr, true);
			}
		}
	}

	public static void BarrelFlingAll()
	{
		if ((Time.time < StumpKickDelay_StateMachine68_Value_04 && StumpKickDelay_StateMachine68_Value_04 != 0f) || VRRigCache.ActiveRigs.Where((VRRig r) => (Object)(object)r != (Object)null && !r.isOfflineVRRig && !r.isLocal && r.Creator != null).ToList().Count == 0)
		{
			return;
		}
		List<VRRig> list = VRRigCache.ActiveRigs
			.Where((VRRig r) => (Object)(object)r != (Object)null && !r.isOfflineVRRig && !r.isLocal && r.Creator != null)
			.ToList();
		StumpKickDelay_StateMachine68_Index_01 %= list.Count;
		int num = 0;
		if (num < 6)
		{
			do
			{
				VRRig target = list[StumpKickDelay_StateMachine68_Index_01];
				RaiseEventOptions val = new RaiseEventOptions();
				val.TargetActors = new int[1] { list[StumpKickDelay_StateMachine68_Index_01].Creator.ActorNumber };
				BarrelFlingPlayer(target, val);
				num++;
			}
			while (num < 6);
		}
		StumpKickDelay_StateMachine68_Index_01 = (StumpKickDelay_StateMachine68_Index_01 + 1) % list.Count;
	}

	public static void DestroyPlayerGun()
	{
		if (GunLib.TrySelectRig())
		{
			PhotonNetwork.OpRemoveCompleteCacheOfPlayer(GunLib.GunLib_Reference_06.Creator.ActorNumber);
		}
	}

	public static void UpdateGrabLines()
	{
		if ((Object)(object)GorillaTagger.Instance == (Object)null || (Object)(object)GorillaTagger.Instance.offlineVRRig == (Object)null)
		{
			using (List<LineRenderer>.Enumerator enumerator = StumpKickDelay_StateMachine68_Items_01.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					while (true)
					{
						LineRenderer current = enumerator.Current;
						if (!((Object)(object)current != (Object)null))
						{
							break;
						}
						Object.Destroy((Object)(object)((Component)current).gameObject);
						if (!enumerator.MoveNext())
						{
							goto EndBranch_00b5;
						}
					}
					continue;
					EndBranch_00b5:
					break;
				}
			}
			StumpKickDelay_StateMachine68_Items_01.Clear();
			return;
		}
		Vector3 position = ((Component)GorillaTagger.Instance.offlineVRRig).transform.position;
		int num = 0;
		using (IEnumerator<VRRig> enumerator2 = VRRigCache.ActiveRigs.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				while (true)
				{
					VRRig current2 = enumerator2.Current;
					if ((Object)(object)current2 == (Object)null || current2.isMyPlayer || current2.isOfflineVRRig)
					{
						break;
					}
					if (!current2.leftHandLink.CanBeGrabbed())
					{
						if (!current2.rightHandLink.CanBeGrabbed())
						{
							break;
						}
					}
					if (StumpKickDelay_StateMachine68_Items_01.Count <= num)
					{
						do
						{
							LineRenderer val = new GameObject("GrabLine").AddComponent<LineRenderer>();
							((Renderer)val).material = new Material(Variables.Variables_Reference_02);
							float startWidth = (val.endWidth = 0.015f);
							val.startWidth = startWidth;
							val.useWorldSpace = true;
							val.positionCount = 2;
							StumpKickDelay_StateMachine68_Items_01.Add(val);
						}
						while (StumpKickDelay_StateMachine68_Items_01.Count <= num);
					}
					LineRenderer val2 = StumpKickDelay_StateMachine68_Items_01[num];
					((Renderer)val2).enabled = true;
					Color startColor = (val2.endColor = Color.blue);
					val2.startColor = startColor;
					val2.SetPosition(0, position);
					val2.SetPosition(1, ((Component)current2).transform.position);
					num++;
					if (!enumerator2.MoveNext())
					{
						goto EndBranch_02f2;
					}
				}
				continue;
				EndBranch_02f2:
				break;
			}
		}
		int num3 = num;
		if (num3 >= StumpKickDelay_StateMachine68_Items_01.Count)
		{
			return;
		}
		while (true)
		{
			if ((Object)(object)StumpKickDelay_StateMachine68_Items_01[num3] != (Object)null)
			{
				((Renderer)StumpKickDelay_StateMachine68_Items_01[num3]).enabled = false;
				num3++;
				if (num3 >= StumpKickDelay_StateMachine68_Items_01.Count)
				{
					break;
				}
			}
			else
			{
				num3++;
				if (num3 >= StumpKickDelay_StateMachine68_Items_01.Count)
				{
					break;
				}
			}
		}
	}

	private static Vector3 GetMetroCrashPosition(VRRig r)
	{
		Vector3 val = new Vector3(137.0344f, 66.6208f, -42.8385f) - ((Component)r).transform.position;
		Vector3 normalized = ((Vector3)val).normalized;
		return ((Component)r).transform.position + Vector3.up * 200f + normalized * 400f;
	}

	public static void BarrelFlingGun()
	{
		if (GunLib.TrySelectRig())
		{
			VRRig target = GunLib.GunLib_Reference_06;
			RaiseEventOptions val = new RaiseEventOptions();
			val.TargetActors = new int[1] { RigManager.GetPlayer(GunLib.GunLib_Reference_06).ActorNumber };
			BarrelFlingPlayer(target, val);
		}
	}

	public static void DeafenGun()
	{
		if (!GunLib.TrySelectRig())
		{
			return;
		}
		int num = 0;
		if (num < 2)
		{
			do
			{
				SendDeafenEvent(new int[1] { GunLib.GunLib_Reference_06.Creator.ActorNumber });
				num++;
			}
			while (num < 2);
		}
	}

	public static void GrabBringGun()
	{
		RunGrabGun(GetBringPosition);
	}

	public static void ClearGrabLines()
	{
		using (List<LineRenderer>.Enumerator enumerator = StumpKickDelay_StateMachine68_Items_01.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				while (true)
				{
					LineRenderer current = enumerator.Current;
					if (!((Object)(object)current != (Object)null))
					{
						break;
					}
					Object.Destroy((Object)(object)((Component)current).gameObject);
					if (!enumerator.MoveNext())
					{
						goto EndBranch_0063;
					}
				}
				continue;
				EndBranch_0063:
				break;
			}
		}
		StumpKickDelay_StateMachine68_Items_01.Clear();
	}

	public static void GetTouchedToElevatorKick()
	{
		Transform[] targets = (Transform[])(object)new Transform[2]
		{
			Variables.Variables_Reference_09.offlineVRRig.headMesh.transform,
			Variables.Variables_Reference_09.offlineVRRig.bodyTransform
		};
		using IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			while (true)
			{
				VRRig current = enumerator.Current;
				if ((Object)(object)current == (Object)null || current.isOfflineVRRig || current.isMyPlayer)
				{
					break;
				}
				if (RigManager.AreHandsNearTargets((Transform[])(object)new Transform[2] { current.leftHandTransform, current.rightHandTransform }, targets))
				{
					((NetworkView)GRElevatorManager._instance).SendRPC("RemoteElevatorButtonPress", (RpcTarget)2, new object[1] { new int[2]
					{
						3,
						GetElevatorLocation()
					} });
					Safety.ResetNetworkLimits();
					return;
				}
				if (!enumerator.MoveNext())
				{
					return;
				}
			}
		}
	}

	public static PhotonView GetPhotonView(this VRRig rig)
	{
		NetworkView networkView = ReflectionCompat.GetField<NetworkView>(rig, "netView");
		return networkView != null ? networkView.GetView : null;
	}

	public static void TouchToDeafen()
	{
		Transform[] hands = (Transform[])(object)new Transform[2]
		{
			Variables.Variables_Reference_09.leftHandTransform,
			Variables.Variables_Reference_09.rightHandTransform
		};
		using IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			while (true)
			{
				VRRig current = enumerator.Current;
				if ((Object)(object)current == (Object)null || current.isOfflineVRRig || (Object)(object)current == (Object)(object)Variables.Variables_Reference_09.offlineVRRig)
				{
					break;
				}
				Transform[] targets = (Transform[])(object)new Transform[2]
				{
					current.headMesh.transform,
					current.bodyTransform
				};
				if (RigManager.AreHandsNearTargets(hands, targets))
				{
					SendDeafenEvent(new int[1] { current.Creator.ActorNumber });
					return;
				}
				if (!enumerator.MoveNext())
				{
					return;
				}
			}
		}
	}

	public static void GetTouchedToBarrelFling()
	{
		Transform[] targets = (Transform[])(object)new Transform[2]
		{
			Variables.Variables_Reference_09.offlineVRRig.headMesh.transform,
			Variables.Variables_Reference_09.offlineVRRig.bodyTransform
		};
		using IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			while (true)
			{
				VRRig current = enumerator.Current;
				if ((Object)(object)current == (Object)null || current.isOfflineVRRig || current.isMyPlayer)
				{
					break;
				}
				if (RigManager.AreHandsNearTargets((Transform[])(object)new Transform[2] { current.leftHandTransform, current.rightHandTransform }, targets))
				{
					RaiseEventOptions val = new RaiseEventOptions();
					val.TargetActors = new int[1] { RigManager.GetPlayer(current).ActorNumber };
					BarrelFlingPlayer(current, val);
					return;
				}
				if (!enumerator.MoveNext())
				{
					return;
				}
			}
		}
	}

	public static void GetTouchedToLag()
	{
		Transform[] targets = (Transform[])(object)new Transform[2]
		{
			Variables.Variables_Reference_09.offlineVRRig.headMesh.transform,
			Variables.Variables_Reference_09.offlineVRRig.bodyTransform
		};
		using IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			while (true)
			{
				VRRig current = enumerator.Current;
				if ((Object)(object)current == (Object)null || current.isOfflineVRRig || current.isMyPlayer)
				{
					break;
				}
				if (RigManager.AreHandsNearTargets((Transform[])(object)new Transform[2] { current.leftHandTransform, current.rightHandTransform }, targets))
				{
					SendLagEvents(new int[1] { current.Creator.ActorNumber });
					return;
				}
				if (!enumerator.MoveNext())
				{
					return;
				}
			}
		}
	}

	[IteratorStateMachine(typeof(StumpKickDelay_StateMachine68))]
	public static IEnumerator StumpKickDelay(Action action, Action action2, float extraDelay = 0f, bool changeQueue = false)
	{
		return new StumpKickDelay_StateMachine68(0)
		{
			action = action,
			action2 = action2,
			extraDelay = extraDelay,
			changeQueue = changeQueue
		};
	}

	public static void GrabCrashGun()
	{
		RunGrabGun(GetCrashPosition);
	}

	private static void SetGrabSerializationTarget(VRRig target, Vector3 pos)
	{
		StumpKickDelay_StateMachine68_Reference_02 = target;
		StumpKickDelay_StateMachine68_Position_01 = pos;
		MenuPatches.SerializationPatch.SerializationPatch_State_01 = SerializeGrabbedRig;
	}

	public static void LagAura()
	{
		if (!PhotonNetwork.InRoom)
		{
			return;
		}
		List<int> list = new List<int>();
		Vector3 position = ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position;
		using (IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				while (true)
				{
					VRRig current = enumerator.Current;
					if ((Object)(object)current == (Object)null || current.isOfflineVRRig || current.isLocal)
					{
						break;
					}
					if (Vector3.Distance(position, ((Component)current).transform.position) < 4f)
					{
						list.Add(current.Creator.ActorNumber);
						if (!enumerator.MoveNext())
						{
							goto EndBranch_0131;
						}
					}
					else if (!enumerator.MoveNext())
					{
						goto EndBranch_0131;
					}
				}
				continue;
				EndBranch_0131:
				break;
			}
		}
		if (list.Count > 0)
		{
			SendLagEvents(list.ToArray());
		}
	}

	public static void VibrateAll()
	{
		if (Variables.IsMasterClient() && InputHandler.IsRightTriggerPressed() && Time.time > StumpKickDelay_StateMachine68_Value_07)
		{
			SendStatusEffect((StatusEffects)1, new RaiseEventOptions
			{
				Receivers = (ReceiverGroup)0
			});
			StumpKickDelay_StateMachine68_Value_07 = Time.time + 0.5f;
		}
	}

	public static void StumpKickGun()
	{
		if (!GunLib.GunGrips)
		{
			GunLib.SetGunVisualsVisible(isVisible: false);
			GunLib.GunLib_Reference_06 = null;
			return;
		}
		GunLib.UpdateGunRaycast();
		Collider collider = ((RaycastHit)GunLib.GunLib_Reference_07).collider;
		GunLib.GunLib_Reference_02 = ((collider != null) ? ((Component)collider).GetComponentInParent<VRRig>() : null);
		if (GunLib.GunTriggers && Time.time > StumpKickDelay_StateMachine68_Value_06)
		{
			if ((Object)(object)GunLib.GunLib_Reference_06 == (Object)null)
			{
				GunLib.GunLib_Reference_06 = GunLib.GunLib_Reference_02;
				return;
			}
			CapturedVariables710 LocalScope3 = new CapturedVariables710();
			LocalScope3.player = RigManager.GetPlayer(GunLib.GunLib_Reference_06);
			StumpKickDelay_StateMachine68_Value_06 = Time.time + 0.5f;
			if (!((GorillaComputer)GorillaComputer.instance).friendJoinCollider.playerIDsCurrentlyTouching.Contains(LocalScope3.player.UserId))
			{
				NotificationLib.ShowNotification(NotificationLib.NotificationType.Error, "Player Must Be In Stump");
				return;
			}
			if (!NetworkSystem.Instance.SessionIsPrivate)
			{
				NotificationLib.ShowNotification(NotificationLib.NotificationType.Error, "You Must Be In A Private Room");
				return;
			}
			((MonoBehaviour)CoroutineHelper.Instance).StartCoroutine(StumpKickDelay(delegate
			{
				PhotonNetworkController instance = PhotonNetworkController.Instance;
				int num = Random.Range(0, 99);
				int num2 = num;
				string text = num2.ToString().PadLeft(2, '0');
				num = Random.Range(0, 99999999);
				num2 = num;
				((PhotonNetworkController)instance).shuffler = text + num2.ToString().PadLeft(8, '0');
				PhotonNetworkController instance2 = PhotonNetworkController.Instance;
				num = Random.Range(0, 99999999);
				num2 = num;
				((PhotonNetworkController)instance2).keyStr = num2.ToString().PadLeft(8, '0');
				SendFriendJoinEvent(((GorillaComputer)GorillaComputer.instance).friendJoinCollider, RigManager.GetPhotonPlayer(LocalScope3.player));
				Safety.ResetNetworkLimits();
			}, delegate
			{
				Room.CreatePublic(StumpKickDelay_StateMachine68_Text_01 ?? Room.GenerateRoomCode(), isPublic: false, 0, (JoinType)1);
			}));
		}
		else if (!GunLib.GunTriggers)
		{
			GunLib.GunLib_Reference_06 = null;
		}
	}

	public static void SetTagLagEnabled(bool enable)
	{
		if (Variables.IsMasterClient())
		{
			((GorillaTagManager)GorillaGameManager.instance).tagCoolDown = (enable ? float.MaxValue : 5f);
		}
	}

	public static void GrabBreakMovementGun()
	{
		RunGrabGun(GetSynchronizedPosition);
	}

	public static void UntagPlayer(NetPlayer plr)
	{
		if (!PhotonNetwork.InRoom || (Object)(object)GorillaGameManager.instance == (Object)null || plr == null)
		{
			return;
		}
		int num = (int)GorillaGameManager.instance.GameType() - 1;
		num = (((uint)num <= 10u) ? num : 11) + 270;
		int num2 = num;
		if (num2 == 271)
		{
			return;
		}
		GorillaGameManager instance = GorillaGameManager.instance;
		GorillaTagManager val = (GorillaTagManager)(object)((instance is GorillaTagManager) ? instance : null);
		if (val == null)
		{
			return;
		}
		if (val.isCurrentlyTag)
		{
			if (val.currentIt == plr)
			{
				val.currentIt = null;
			}
		}
		else if (val.currentInfected != null && val.currentInfected.Contains(plr))
		{
			val.currentInfected.Remove(plr);
		}
	}

	public static void SetCurrentTagger(NetPlayer plr)
	{
		if (!PhotonNetwork.InRoom || (Object)(object)GorillaGameManager.instance == (Object)null || plr == null)
		{
			return;
		}
		int num = (int)GorillaGameManager.instance.GameType() - 1;
		num = (((uint)num <= 10u) ? num : 11) + 301;
		int num2 = num;
		if (num2 != 302)
		{
			GorillaGameManager instance = GorillaGameManager.instance;
			GorillaTagManager val = (GorillaTagManager)(object)((instance is GorillaTagManager) ? instance : null);
			if (val != null)
			{
				ReflectionCompat.Invoke(val, "ChangeCurrentIt", plr, true);
			}
		}
	}

	public static void GetTouchedToDeafen()
	{
		Transform[] targets = (Transform[])(object)new Transform[2]
		{
			Variables.Variables_Reference_09.offlineVRRig.headMesh.transform,
			Variables.Variables_Reference_09.offlineVRRig.bodyTransform
		};
		using IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			while (true)
			{
				VRRig current = enumerator.Current;
				if ((Object)(object)current == (Object)null || current.isOfflineVRRig || current.isMyPlayer)
				{
					break;
				}
				if (RigManager.AreHandsNearTargets((Transform[])(object)new Transform[2] { current.leftHandTransform, current.rightHandTransform }, targets))
				{
					SendDeafenEvent(new int[1] { current.Creator.ActorNumber });
					return;
				}
				if (!enumerator.MoveNext())
				{
					return;
				}
			}
		}
	}

	public static void LockRoom(bool status)
	{
		if (PhotonNetwork.InRoom)
		{
			Dictionary<byte, object> dictionary = new Dictionary<byte, object>();
			PhotonHashtable val = new PhotonHashtable();
			val.Add((byte)253, (object)status);
			val.Add((byte)254, (object)status);
			val.Add(byte.MaxValue, (object)(status ? PhotonNetwork.CurrentRoom.MaxPlayers : 0));
			dictionary.Add(251, (object)val);
			dictionary.Add(250, true);
			dictionary.Add(231, null);
			Dictionary<byte, object> dictionary2 = dictionary;
			((PhotonPeer)PhotonNetwork.CurrentRoom.LoadBalancingClient.LoadBalancingPeer).SendOperation((byte)252, dictionary2, SendOptions.SendReliable);
			GorillaScoreboardTotalUpdater.instance.UpdateActiveScoreboards();
		}
	}

	public static void GrabRigAtPosition(VRRig target, Vector3 pos)
	{
		if ((Object)(object)target == (Object)null || target.isLocal)
		{
			return;
		}
		if (!target.leftHandLink.CanBeGrabbed() && !target.rightHandLink.CanBeGrabbed())
		{
			MenuPatches.GrabPatch.GrabPatch_State_08 = false;
			VRRig.LocalRig.BreakHandLinks();
			((Behaviour)VRRig.LocalRig).enabled = true;
			return;
		}
		MenuPatches.GrabPatch.GrabPatch_State_08 = true;
		((Behaviour)VRRig.LocalRig).enabled = false;
		((Component)VRRig.LocalRig).transform.position = pos;
		SetGrabSerializationTarget(target, pos);
		TakeMyHand_HandLink val = target.rightHandLink;
		TakeMyHand_HandLink val2 = VRRig.LocalRig.rightHandLink;
		float rejectGrabsUntilTimestamp = ReflectionCompat.GetField(val, "rejectGrabsUntilTimestamp", 0f);
		if (val.grabbedPlayer != NetworkSystem.Instance.LocalPlayer)
		{
			goto Branch_018b;
		}
		return;
		Branch_018b:
		if (StumpKickDelay_StateMachine68_Value_10 <= 0f)
		{
			if (!(rejectGrabsUntilTimestamp > Time.time))
			{
				StumpKickDelay_StateMachine68_Value_10 = Time.time + 0.2f;
				if (Time.time <= StumpKickDelay_StateMachine68_Value_10)
				{
					return;
				}
			}
			else
			{
				StumpKickDelay_StateMachine68_Value_10 = rejectGrabsUntilTimestamp;
				if (Time.time <= StumpKickDelay_StateMachine68_Value_10)
				{
					return;
				}
			}
		}
		else if (Time.time <= StumpKickDelay_StateMachine68_Value_10)
		{
			return;
		}
		((Component)VRRig.LocalRig).transform.position = target.syncPos;
		val2.TentacleTryCreateLink(val);
		rejectGrabsUntilTimestamp = ReflectionCompat.GetField(val, "rejectGrabsUntilTimestamp", 0f);
		if (!(rejectGrabsUntilTimestamp > Time.time))
		{
			StumpKickDelay_StateMachine68_Value_10 = Time.time + 0.2f;
		}
		else
		{
			StumpKickDelay_StateMachine68_Value_10 = rejectGrabsUntilTimestamp;
		}
		return;
	}

	private static Vector3 GetBringPosition(VRRig r)
	{
		return ((Component)VRRig.LocalRig).transform.position - ((Component)VRRig.LocalRig).transform.forward * 10f;
	}

	public static bool AreGrabInputsReleased()
	{
		if (!GunLib.GunGrips && !GunLib.GunTriggers && !InputHandler.IsRightGripPressed() && !InputHandler.IsLeftGripPressed() && !InputHandler.IsRightTriggerPressed())
		{
			return !InputHandler.IsLeftTriggerPressed();
		}
		return false;
	}

	public static void SpamEvent()
	{
		if (Variables.IsMasterClient() && Time.time - StumpKickDelay_StateMachine68_Value_09 >= 0.1f)
		{
			StumpKickDelay_StateMachine68_Value_09 = Time.time;
			if (GreyZoneManager.Instance.GreyZoneActive)
			{
				((GreyZoneManager)GreyZoneManager.Instance).DeactivateGreyZoneAuthority();
			}
			else
			{
				((GreyZoneManager)GreyZoneManager.Instance).ActivateGreyZoneAuthority();
			}
		}
	}

	public static void ResetGrabFlingGun()
	{
		StumpKickDelay_StateMachine68_Reference_02 = null;
		if ((Delegate?)MenuPatches.SerializationPatch.SerializationPatch_State_01 == (Delegate?)new Func<bool>(SerializeGrabbedRig))
		{
			MenuPatches.SerializationPatch.SerializationPatch_State_01 = null;
			MenuPatches.GrabPatch.GrabPatch_State_08 = false;
			if ((Object)(object)VRRig.LocalRig != (Object)null)
			{
				goto Branch_0080;
			}
		}
		else
		{
			MenuPatches.GrabPatch.GrabPatch_State_08 = false;
			if ((Object)(object)VRRig.LocalRig != (Object)null)
			{
				goto Branch_0080;
			}
		}
		ClearGrabLines();
		return;
		Branch_0080:
		VRRig.LocalRig.BreakHandLinks();
		((Behaviour)VRRig.LocalRig).enabled = true;
		ClearGrabLines();
	}

	private static Vector3 GetSynchronizedPosition(VRRig r)
	{
		return r.syncPos;
	}

	public static void SendBarrelFlingEvents(VRRig p)
	{
		if (Time.time < StumpKickDelay_StateMachine68_Value_02)
		{
			return;
		}
		StumpKickDelay_StateMachine68_Value_02 = Time.time + 1f;
		int num = 0;
		if (num < 525)
		{
			do
			{
				LoadBalancingClient networkingClient = PhotonNetwork.NetworkingClient;
				object[] obj = new object[1] { float.NaN };
				RaiseEventOptions val = new RaiseEventOptions();
				val.CachingOption = (EventCaching)2;
				val.TargetActors = new int[1] { p.Creator.ActorNumber };
				SendOptions val2 = default(SendOptions);
				val2.Reliability = false;
				val2.DeliveryMode = (DeliveryMode)0;
				networkingClient.OpRaiseEvent((byte)186, (object)obj, val, val2);
				num++;
			}
			while (num < 525);
		}
	}

	public static void EarrapeGun()
	{
		if (!GunLib.TrySelectRig())
		{
			return;
		}
		IEnumerator<VRRig> enumerator;
		if (!StumpKickDelay_StateMachine68_State_01)
		{
			Sound.SetSquareWaveMicrophoneEnabled(enable: true);
			StumpKickDelay_StateMachine68_State_01 = true;
			enumerator = VRRigCache.ActiveRigs.GetEnumerator();
		}
		else
		{
			enumerator = VRRigCache.ActiveRigs.GetEnumerator();
		}
		try
		{
			while (enumerator.MoveNext())
			{
				while (true)
				{
					VRRig current = enumerator.Current;
					if ((Object)(object)current == (Object)null || current.isOfflineVRRig || current.isLocal || current.Creator.ActorNumber == GunLib.GunLib_Reference_06.Creator.ActorNumber)
					{
						break;
					}
					SendDeafenEvent(new int[1] { current.Creator.ActorNumber });
					if (!enumerator.MoveNext())
					{
						goto EndBranch_0147;
					}
				}
				continue;
				EndBranch_0147:
				break;
			}
		}
		finally
		{
			enumerator?.Dispose();
		}
		MenuPatches.SerializationPatch.SerializationPatch_State_01 = delegate
		{
			PhotonView val;
			if (PhotonNetwork.InRoom)
			{
				if (!((Object)(object)Variables.Variables_Reference_09 != (Object)null) || !((Object)(object)Variables.Variables_Reference_09.myVRRig != (Object)null))
				{
					val = null;
					if (!((Object)(object)val == (Object)null))
					{
						goto Branch_00a4;
					}
				}
				else
				{
					val = ((Component)Variables.Variables_Reference_09.myVRRig).GetComponent<PhotonView>();
					if (!((Object)(object)val == (Object)null))
					{
						goto Branch_00a4;
					}
				}
				goto Branch_00d7;
			}
			bool result = true;
			goto Branch_02ab;
			Branch_00a4:
			if ((Object)(object)VRRig.LocalRig == (Object)null)
			{
				goto Branch_00d7;
			}
			if ((Object)(object)GunLib.GunLib_Reference_06 == (Object)null || GunLib.GunLib_Reference_06.Creator == null)
			{
				MenuPatches.SerializationPatch.SerializationPatch_State_01 = null;
				result = true;
			}
			else
			{
				PhotonSerializer.SerializeAllViews(exclude: true, (PhotonView[])(object)new PhotonView[1] { val });
				PhotonSerializer.RigPose rigPose = PhotonSerializer.RigPose.Capture(VRRig.LocalRig);
				try
				{
					((Component)VRRig.LocalRig).transform.SetPositionAndRotation(((Component)GunLib.GunLib_Reference_06).transform.position, ((Component)GunLib.GunLib_Reference_06).transform.rotation);
					VRRig.LocalRig.head.rigTarget.SetPositionAndRotation(GunLib.GunLib_Reference_06.head.rigTarget.position, GunLib.GunLib_Reference_06.head.rigTarget.rotation);
					VRRig.LocalRig.leftHand.rigTarget.SetPositionAndRotation(GunLib.GunLib_Reference_06.leftHand.rigTarget.position, GunLib.GunLib_Reference_06.leftHand.rigTarget.rotation);
					VRRig.LocalRig.rightHand.rigTarget.SetPositionAndRotation(GunLib.GunLib_Reference_06.rightHand.rigTarget.position, GunLib.GunLib_Reference_06.rightHand.rigTarget.rotation);
					PhotonView pv = val;
					RaiseEventOptions val2 = new RaiseEventOptions();
					val2.TargetActors = new int[1] { GunLib.GunLib_Reference_06.Creator.ActorNumber };
					PhotonSerializer.SerializePhotonView(pv, val2);
					Safety.ResetNetworkLimits();
				}
				catch (Exception)
				{
					rigPose.Restore(VRRig.LocalRig);
					bool result2 = default(bool);
					return result2;
				}
				result = false;
			}
			Branch_02ab:
			return result;
			Branch_00d7:
			result = true;
			goto Branch_02ab;
		};
	}

	public static void GrabMetroCrashAll()
	{
		RunGrabAll(GetMetroCrashPosition);
	}

	public static void TouchToElevatorKick()
	{
		Transform[] hands = (Transform[])(object)new Transform[2]
		{
			Variables.Variables_Reference_09.leftHandTransform,
			Variables.Variables_Reference_09.rightHandTransform
		};
		using IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			while (true)
			{
				VRRig current = enumerator.Current;
				if ((Object)(object)current == (Object)null || current.isOfflineVRRig || (Object)(object)current == (Object)(object)Variables.Variables_Reference_09.offlineVRRig)
				{
					break;
				}
				Transform[] targets = (Transform[])(object)new Transform[2]
				{
					current.headMesh.transform,
					current.bodyTransform
				};
				if (RigManager.AreHandsNearTargets(hands, targets))
				{
					((NetworkView)GRElevatorManager._instance).SendRPC("RemoteElevatorButtonPress", (RpcTarget)2, new object[1] { new int[2]
					{
						3,
						GetElevatorLocation()
					} });
					Safety.ResetNetworkLimits();
					return;
				}
				if (!enumerator.MoveNext())
				{
					return;
				}
			}
		}
	}

	public static void LagGun()
	{
		if (GunLib.TrySelectRig())
		{
			SendLagEvents(new int[1] { GunLib.GunLib_Reference_06.Creator.ActorNumber });
		}
	}

	public static void SlowOnTouch()
	{
		if (!Variables.IsMasterClient() || !PhotonNetwork.InRoom || Time.time < StumpKickDelay_StateMachine68_Value_05)
		{
			return;
		}
		Transform[] hands = (Transform[])(object)new Transform[2]
		{
			Variables.Variables_Reference_09.leftHandTransform,
			Variables.Variables_Reference_09.rightHandTransform
		};
		using IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			while (true)
			{
				VRRig current = enumerator.Current;
				if ((Object)(object)current == (Object)null || current.isOfflineVRRig || current.isLocal)
				{
					break;
				}
				if (RigManager.AreHandsNearTargets(hands, (Transform[])(object)new Transform[2]
				{
					current.headMesh.transform,
					current.bodyTransform
				}))
				{
					StumpKickDelay_StateMachine68_Value_05 = Time.time + 1f;
					RaiseEventOptions val = new RaiseEventOptions();
					val.TargetActors = new int[1] { current.Creator.ActorNumber };
					SendStatusEffect((StatusEffects)0, val);
					return;
				}
				if (!enumerator.MoveNext())
				{
					return;
				}
			}
		}
	}

	public static void VibrateGun()
	{
		if (Variables.IsMasterClient() && GunLib.TrySelectRig() && Time.time > StumpKickDelay_StateMachine68_Value_07)
		{
			StumpKickDelay_StateMachine68_Value_07 = Time.time + 0.5f;
			RaiseEventOptions val = new RaiseEventOptions();
			val.TargetActors = new int[1] { GunLib.GunLib_Reference_06.Creator.ActorNumber };
			SendStatusEffect((StatusEffects)1, val);
		}
	}

	public static void CycleRigTagState(VRRig rig)
	{
		if (!NetworkSystem.Instance.IsMasterClient)
		{
			NotificationLib.ShowNotification(NotificationLib.NotificationType.Error, "You're Not Master Client");
			return;
		}
		NetPlayer plr = RigManager.GetPlayer(rig);
		StumpKickDelay_StateMachine68_Lookup_01.TryGetValue(rig, out var value);
		int num = value + 1;
		value = num;
		num = value % 4;
		value = num;
		if ((int)GorillaGameManager.instance.GameType() == 0)
		{
			if (value < 4)
			{
				value = 4;
				StumpKickDelay_StateMachine68_Lookup_01[rig] = value;
				int num2 = value;
				int num3 = num2;
				int num4 = num3;
				num4 = (((uint)num4 <= 3u) ? num4 : 4) + 361;
				int num5 = num4;
				if (num5 != 362)
				{
					goto Branch_016f;
				}
			}
			else
			{
				StumpKickDelay_StateMachine68_Lookup_01[rig] = value;
				int num2 = value;
				int num3 = num2;
				int num6 = num3;
				num6 = (((uint)num6 <= 3u) ? num6 : 4) + 361;
				int num5 = num6;
				if (num5 != 362)
				{
					goto Branch_016f;
				}
			}
		}
		else
		{
			StumpKickDelay_StateMachine68_Lookup_01[rig] = value;
			int num2 = value;
			int num3 = num2;
			int num7 = num3;
			num7 = (((uint)num7 <= 3u) ? num7 : 4) + 361;
			int num5 = num7;
			if (num5 != 362)
			{
				goto Branch_016f;
			}
		}
		UntagPlayer(plr);
		return;
		Branch_016f:
		TagPlayer(plr);
	}

	private static void RunGrabAll(Func<VRRig, Vector3> posFor)
	{
		UpdateGrabLines();
		foreach (VRRig activeRig in VRRigCache.ActiveRigs)
		{
			if ((Object)(object)activeRig == (Object)null || activeRig.isMyPlayer || activeRig.isOfflineVRRig || (!activeRig.leftHandLink.CanBeGrabbed() && !activeRig.rightHandLink.CanBeGrabbed()))
			{
				continue;
			}
			GrabRigAtPosition(activeRig, posFor(activeRig));
			return;
		}
		MenuPatches.GrabPatch.GrabPatch_State_08 = false;
		VRRig.LocalRig.BreakHandLinks();
		((Behaviour)VRRig.LocalRig).enabled = true;
	}

	public static void SlowAll()
	{
		if (Variables.IsMasterClient() && InputHandler.IsRightTriggerPressed() && Time.time > StumpKickDelay_StateMachine68_Value_05)
		{
			SendStatusEffect((StatusEffects)0, new RaiseEventOptions
			{
				Receivers = (ReceiverGroup)0
			});
			StumpKickDelay_StateMachine68_Value_05 = Time.time + 1f;
		}
	}

	public static void GrabBreakMovementAll()
	{
		RunGrabAll(GetSynchronizedPosition);
	}

	public static void GrabCrashAll()
	{
		RunGrabAll(GetCrashPosition);
	}

	public static void DeafenAll()
	{
		int num = 0;
		if (num < 2)
		{
			do
			{
				SendDeafenEvent((object)(ReceiverGroup)0);
				num++;
			}
			while (num < 2);
		}
	}

	public static void MatSpamAll()
	{
		if (!Variables.IsMasterClient() || !(Time.time > StumpKickDelay_StateMachine68_Value_03))
		{
			return;
		}
		StumpKickDelay_StateMachine68_Value_03 = Time.time + 0.1f;
		using IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.GetEnumerator();
		if (enumerator.MoveNext())
		{
			do
			{
				CycleRigTagState(enumerator.Current);
			}
			while (enumerator.MoveNext());
		}
	}

	public static void TouchToLag()
	{
		Transform[] hands = (Transform[])(object)new Transform[2]
		{
			Variables.Variables_Reference_09.leftHandTransform,
			Variables.Variables_Reference_09.rightHandTransform
		};
		using IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			while (true)
			{
				VRRig current = enumerator.Current;
				if ((Object)(object)current == (Object)null || current.isOfflineVRRig || (Object)(object)current == (Object)(object)Variables.Variables_Reference_09.offlineVRRig)
				{
					break;
				}
				Transform[] targets = (Transform[])(object)new Transform[2]
				{
					current.headMesh.transform,
					current.bodyTransform
				};
				if (RigManager.AreHandsNearTargets(hands, targets))
				{
					SendLagEvents(new int[1] { current.Creator.ActorNumber });
					return;
				}
				if (!enumerator.MoveNext())
				{
					return;
				}
			}
		}
	}

	public static void VibrateOnTouch()
	{
		if (!Variables.IsMasterClient() || !PhotonNetwork.InRoom || Time.time < StumpKickDelay_StateMachine68_Value_07)
		{
			return;
		}
		Transform[] hands = (Transform[])(object)new Transform[2]
		{
			Variables.Variables_Reference_09.leftHandTransform,
			Variables.Variables_Reference_09.rightHandTransform
		};
		using IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			while (true)
			{
				VRRig current = enumerator.Current;
				if ((Object)(object)current == (Object)null || current.isOfflineVRRig || current.isLocal)
				{
					break;
				}
				if (RigManager.AreHandsNearTargets(hands, (Transform[])(object)new Transform[2]
				{
					current.headMesh.transform,
					current.bodyTransform
				}))
				{
					StumpKickDelay_StateMachine68_Value_07 = Time.time + 0.5f;
					RaiseEventOptions val = new RaiseEventOptions();
					val.TargetActors = new int[1] { current.Creator.ActorNumber };
					SendStatusEffect((StatusEffects)1, val);
					return;
				}
				if (!enumerator.MoveNext())
				{
					return;
				}
			}
		}
	}

	public static void BarrelFlingPlayer(VRRig target, RaiseEventOptions options, Vector3 PunchVelocity = default(Vector3))
	{
		if (Time.time < StumpKickDelay_StateMachine68_Value_04 || (Object)(object)target == (Object)null || (Object)(object)((Component)target).transform == (Object)null || target.head == null || (Object)(object)target.head.rigTarget == (Object)null)
		{
			return;
		}
		int num = 0;
		if (num >= 2)
		{
			return;
		}
		do
		{
			if ((Object)(object)StumpKickDelay_StateMachine68_Reference_01 == (Object)null)
			{
				GameObject obj = GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body_pivot/shoulder.L/upper_arm.L/forearm.L/TransferrableItemLeftArm/DropZoneAnchor/HoldableThrowableBarrelLeprechaun_Anchor(Clone)/LMAPE.") ?? GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body_pivot/shoulder.R/upper_arm.R/forearm.R/TransferrableItemRightArm/DropZoneAnchor/HoldableThrowableBarrelLeprechaun_Anchor(Clone)/LMAPE.");
				object hP8AAYL;
				if (obj == null)
				{
					hP8AAYL = null;
				}
				else
				{
					TransferrableItemSlotTransformOverride component = obj.GetComponent<TransferrableItemSlotTransformOverride>();
					if (component == null)
					{
						hP8AAYL = null;
					}
					else
					{
						TransferrableObject followingTransferrableObject = component.followingTransferrableObject;
						hP8AAYL = ((followingTransferrableObject != null) ? ((Component)followingTransferrableObject).GetComponent<DeployableObject>() : null);
					}
				}
				StumpKickDelay_StateMachine68_Reference_01 = (DeployableObject)hP8AAYL;
				StumpKickDelay_StateMachine68_Object_01 = GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body_pivot/shoulder.L/upper_arm.L/forearm.L/TransferrableItemLeftArm/DropZoneAnchor/HoldableThrowableBarrelLeprechaun_Anchor(Clone)/LMAPE.") ?? GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body_pivot/shoulder.R/upper_arm.R/forearm.R/TransferrableItemRightArm/DropZoneAnchor/HoldableThrowableBarrelLeprechaun_Anchor(Clone)/LMAPE.");
				if ((Object)(object)StumpKickDelay_StateMachine68_Reference_01 == (Object)null)
				{
					break;
				}
			}
			else if ((Object)(object)StumpKickDelay_StateMachine68_Reference_01 == (Object)null)
			{
				break;
			}
			((MonoBehaviour)CoroutineHelper.Instance).StartCoroutine(RestoreStumpKickStateDelayed());
			((Behaviour)GorillaTagger.Instance.offlineVRRig).enabled = false;
			try
			{
				((Component)GorillaTagger.Instance.offlineVRRig).transform.position = ((Component)target).transform.position - new Vector3(0f, 0.2f, 0f);
				Collider[] componentsInChildren = StumpKickDelay_StateMachine68_Object_01.GetComponentsInChildren<Collider>(true);
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].enabled = false;
				}
				((TransferrableObject)StumpKickDelay_StateMachine68_Reference_01).currentState = (PositionState)1;
				((VRMap)VRRig.LocalRig.rightMiddle).calcT = 1f;
				((VRMap)VRRig.LocalRig.rightMiddle).LerpFinger(1f, false);
				((HoldableObject)StumpKickDelay_StateMachine68_Reference_01).OnGrab(((TransferrableObject)StumpKickDelay_StateMachine68_Reference_01).gripInteractor, ((EquipmentInteractor)EquipmentInteractor.instance).rightHand);
				Vector3 val = target.head.rigTarget.position + Vector3.down * -0.04f;
				Vector3 val2;
				Quaternion val3;
				int num2;
				int num3;
				if (!(PunchVelocity != default(Vector3)))
				{
					val2 = (Vector3.up + new Vector3(0f, 50f, 0f)) * 100f;
					val3 = Quaternion.Euler((float)Random.Range(0, 260), (float)Random.Range(0, 360), (float)Random.Range(0, 160));
					num2 = 0;
					num3 = 588;
				}
				else
				{
					val2 = PunchVelocity;
					val3 = Quaternion.Euler((float)Random.Range(0, 260), (float)Random.Range(0, 360), (float)Random.Range(0, 160));
					num2 = 0;
					num3 = 588;
				}
				if (num2 < 3)
				{
					do
					{
						SendBarrelFlingEvents(target);
						num2++;
					}
					while (num2 < 3);
				}
				ReflectionCompat.Invoke(StumpKickDelay_StateMachine68_Reference_01, "DeployLocal", val, val3, val2, false);
				StumpKickDelay_StateMachine68_Object_01.transform.position = val;
				StumpKickDelay_StateMachine68_Object_01.transform.rotation = val3;
				PhotonSignal<long, int, long> deploySignal = ReflectionCompat.GetField<PhotonSignal<long, int, long>>(StumpKickDelay_StateMachine68_Reference_01, "_deploySignal");
				if (deploySignal == null)
				{
					num3 = 590;
				}
				else
				{
					deploySignal.Raise((ReceiverGroup)0, BitPackUtils.PackWorldPosForNetwork(val), BitPackUtils.PackQuaternionForNetwork(val3), BitPackUtils.PackWorldPosForNetwork(val2 * 50f));
				}
				if (num3 == 590)
				{
				}
			}
			catch (Exception)
			{
				((Behaviour)GorillaTagger.Instance.offlineVRRig).enabled = true;
				break;
			}
			VRRig.LocalRig.RemoteRigUpdate();
			ReflectionCompat.InvokeStatic(typeof(PhotonNetwork), "ResetPhotonViewsOnSerialize");
			ReflectionCompat.InvokeStatic(typeof(PhotonNetwork), "RunViewUpdate");
			((TransferrableObject)StumpKickDelay_StateMachine68_Reference_01).currentState = (PositionState)1;
			((TransferrableObject)StumpKickDelay_StateMachine68_Reference_01).storedZone = (DropPositions)1;
			StumpKickDelay_StateMachine68_Value_04 = Time.time + 0.7f;
			num++;
		}
		while (num < 2);
	}

	public static void SendLagEvents(int[] targetActors)
	{
		if (!PhotonNetwork.InRoom || Time.time < StumpKickDelay_StateMachine68_Value_01)
		{
			return;
		}
		StumpKickDelay_StateMachine68_Value_01 = Time.time + Settings.CapturedVariables3760_Value_19;
		RaiseEventOptions val = new RaiseEventOptions
		{
			TargetActors = targetActors,
			CachingOption = (EventCaching)2
		};
		SendOptions val2 = default(SendOptions);
		val2.Reliability = false;
		val2.DeliveryMode = (DeliveryMode)0;
		SendOptions val3 = val2;
		int num = 0;
		if (num < Settings.CapturedVariables3760_Index_26)
		{
			do
			{
				PhotonNetwork.NetworkingClient.LoadBalancingPeer.OpRaiseEvent((byte)186, (object)new object[1] { float.NaN }, val, val3);
				num++;
			}
			while (num < Settings.CapturedVariables3760_Index_26);
		}
		Safety.ResetNetworkLimits();
	}

	public static void TaggedSound(int id)
	{
		if (Variables.IsMasterClient() && InputHandler.IsRightTriggerPressed() && Time.time > StumpKickDelay_StateMachine68_Value_08)
		{
			object[] array = new object[3] { id, 99999f, false };
			object[] array2 = new object[3]
			{
				PhotonNetwork.ServerTimestamp,
				(byte)3,
				array
			};
			try
			{
				PhotonNetwork.RaiseEvent((byte)3, (object)array2, new RaiseEventOptions
				{
					Receivers = (ReceiverGroup)1
				}, SendOptions.SendUnreliable);
			}
			catch (Exception)
			{
			}
			Safety.ResetNetworkLimits();
			StumpKickDelay_StateMachine68_Value_08 = Time.time + 0.1f;
		}
	}

	public static void SendDeafenEvent(object player)
	{
		RaiseEventOptions val = new RaiseEventOptions();
		if (player is ReceiverGroup receivers)
		{
			val.Receivers = receivers;
		}
		else
		{
			if (!(player is int[] targetActors))
			{
				return;
			}
			val.TargetActors = targetActors;
		}
		SendOptions val2 = default(SendOptions);
		val2.Reliability = true;
		val2.Channel = 0;
		SendOptions val3 = val2;
		Dictionary<byte, object> dictionary = new Dictionary<byte, object>
		{
			{ 1, 255 },
			{ 2, 48000 },
			{ 3, 2 },
			{ 4, 20000 },
			{ 5, 30000 },
			{ 10, null },
			{
				11,
				(byte)0
			},
			{
				12,
				(byte)11
			}
		};
		object[] array = new object[3]
		{
			(byte)0,
			(byte)1,
			new object[1] { dictionary }
		};
		((LoadBalancingClient)((VoiceConnection)PhotonVoiceNetwork.Instance).Client).OpRaiseEvent((byte)202, (object)array, val, val3);
	}

	public static IEnumerator RestoreStumpKickStateDelayed()
	{
		yield return (object)new WaitForSeconds(0.3f);
		MenuPatches.GrabPatch_State_09 = false;
		((Behaviour)VRRig.LocalRig).enabled = true;
		((Component)StumpKickDelay_StateMachine68_Reference_01).gameObject.SetActive(true);
		((TransferrableObject)StumpKickDelay_StateMachine68_Reference_01).storedZone = (DropPositions)1;
		((TransferrableObject)StumpKickDelay_StateMachine68_Reference_01).currentState = (PositionState)1;
	}

	public static void ElevatorKickGun()
	{
		if (GunLib.TrySelectRig())
		{
			((NetworkView)GRElevatorManager._instance).SendRPC("RemoteElevatorButtonPress", (RpcTarget)2, new object[1] { new int[2]
			{
				3,
				GetElevatorLocation()
			} });
			Safety.ResetNetworkLimits();
		}
	}

	public static void SendFriendJoinEvent(GorillaFriendCollider friendCollider, PhotonPlayer player)
	{
		((PhotonNetworkController)PhotonNetworkController.Instance).FriendIDList.Add(player.UserId);
		object[] array = new object[2]
		{
			((PhotonNetworkController)PhotonNetworkController.Instance).shuffler,
			((PhotonNetworkController)PhotonNetworkController.Instance).keyStr
		};
		NetEventOptions val = new NetEventOptions();
		val.TargetActors = new int[1] { player.ActorNumber };
		NetEventOptions val2 = val;
		if (friendCollider.playerIDsCurrentlyTouching.Contains(PhotonNetwork.LocalPlayer.UserId) && friendCollider.playerIDsCurrentlyTouching.Contains(player.UserId) && player != PhotonNetwork.LocalPlayer)
		{
			AccessTools.Method("RoomSystem:SendEvent")?.Invoke(null, new object[4] { (byte)4, array, val2, false });
		}
		else if (!friendCollider.playerIDsCurrentlyTouching.Contains(PhotonNetwork.LocalPlayer.UserId))
		{
			NotificationLib.ShowNotification(NotificationLib.NotificationType.Error, "You Must Be In Stump");
		}
	}

	public static void TriggerElevatorKick()
	{
		((NetworkView)GRElevatorManager._instance).SendRPC("RemoteElevatorButtonPress", (RpcTarget)2, new object[1] { new int[2]
		{
			3,
			GetElevatorLocation()
		} });
		Safety.ResetNetworkLimits();
	}

	public static void StumpKickAll()
	{
		if (PhotonNetwork.InRoom)
		{
			if (!NetworkSystem.Instance.SessionIsPrivate)
			{
				NotificationLib.ShowNotification(NotificationLib.NotificationType.Error, "You Must Be In A Private Room");
				return;
			}
			((MonoBehaviour)CoroutineHelper.Instance).StartCoroutine(StumpKickDelay(delegate
			{
				PhotonNetworkController instance = PhotonNetworkController.Instance;
				int num = Random.Range(0, 99);
				int num2 = num;
				string text = num2.ToString().PadLeft(2, '0');
				num = Random.Range(0, 99999999);
				num2 = num;
				((PhotonNetworkController)instance).shuffler = text + num2.ToString().PadLeft(8, '0');
				PhotonNetworkController instance2 = PhotonNetworkController.Instance;
				num = Random.Range(0, 99999999);
				num2 = num;
				((PhotonNetworkController)instance2).keyStr = num2.ToString().PadLeft(8, '0');
				using (IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.Where((VRRig rig) => !rig.IsLocalOrGhostRig() && ((GorillaComputer)GorillaComputer.instance).friendJoinCollider.playerIDsCurrentlyTouching.Contains(GetRigOwnerUserId(rig))).GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						do
						{
							VRRig current = enumerator.Current;
							SendFriendJoinEvent(((GorillaComputer)GorillaComputer.instance).friendJoinCollider, RigManager.GetPhotonPlayer(RigManager.GetPlayer(current)));
						}
						while (enumerator.MoveNext());
					}
				}
				Safety.ResetNetworkLimits();
			}, delegate
			{
				Room.CreatePublic(StumpKickDelay_StateMachine68_Text_01 ?? Room.GenerateRoomCode(), isPublic: false, 0, (JoinType)1);
			}));
		}
		else
		{
			NotificationLib.ShowNotification(NotificationLib.NotificationType.Error, "Not In A Room");
		}
	}

	public static void ClearCurrentTagger(NetPlayer plr)
	{
		if (!PhotonNetwork.InRoom || (Object)(object)GorillaGameManager.instance == (Object)null || plr == null)
		{
			return;
		}
		int num = (int)GorillaGameManager.instance.GameType() - 1;
		num = (((uint)num <= 10u) ? num : 11) + 336;
		int num2 = num;
		if (num2 != 337)
		{
			GorillaGameManager instance = GorillaGameManager.instance;
			GorillaTagManager val = (GorillaTagManager)(object)((instance is GorillaTagManager) ? instance : null);
			if (val != null && val.currentIt == plr)
			{
				ReflectionCompat.Invoke(val, "ChangeCurrentIt", (NetPlayer)null, true);
			}
		}
	}

	public static void VibrateAura()
	{
		if (!Variables.IsMasterClient() || !PhotonNetwork.InRoom || Time.time < StumpKickDelay_StateMachine68_Value_07)
		{
			return;
		}
		Vector3 position = ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position;
		using IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			while (true)
			{
				VRRig current = enumerator.Current;
				if ((Object)(object)current == (Object)null || current.isOfflineVRRig || current.isLocal)
				{
					break;
				}
				if (Vector3.Distance(position, ((Component)current).transform.position) < 4f)
				{
					StumpKickDelay_StateMachine68_Value_07 = Time.time + 0.5f;
					RaiseEventOptions val = new RaiseEventOptions();
					val.TargetActors = new int[1] { current.Creator.ActorNumber };
					SendStatusEffect((StatusEffects)1, val);
					return;
				}
				if (!enumerator.MoveNext())
				{
					return;
				}
			}
		}
	}

	public static void SerializeView(this PhotonView view, RaiseEventOptions options = null)
	{
		PhotonSerializer.SerializePhotonView(view, options);
	}

	public static void GrabFlingGun()
	{
		RunGrabGun(GetRandomFlingPosition);
	}

	private static Vector3 GetCrashPosition(VRRig r)
	{
		return Vector3.down * 2.1474836E+09f;
	}

	private static bool SerializeGrabbedRig()
	{
		if (!PhotonNetwork.InRoom)
		{
			return true;
		}
		if (!MenuPatches.GrabPatch.GrabPatch_State_08 || (Object)(object)StumpKickDelay_StateMachine68_Reference_02 == (Object)null || StumpKickDelay_StateMachine68_Reference_02.Creator == null || (Object)(object)Variables.Variables_Reference_09.myVRRig == (Object)null)
		{
			StumpKickDelay_StateMachine68_Reference_02 = null;
			MenuPatches.SerializationPatch.SerializationPatch_State_01 = null;
			return true;
		}
		PhotonSerializer.SerializeAllViews(exclude: true, (PhotonView[])(object)new PhotonView[1] { Variables.Variables_Reference_09.myVRRig.GetView });
		Vector3 position = ((Component)VRRig.LocalRig).transform.position;
		bool enabled = ((Behaviour)VRRig.LocalRig).enabled;
		((Behaviour)VRRig.LocalRig).enabled = false;
		((Component)VRRig.LocalRig).transform.position = StumpKickDelay_StateMachine68_Position_01;
		PhotonView getView = Variables.Variables_Reference_09.myVRRig.GetView;
		RaiseEventOptions val = new RaiseEventOptions();
		val.TargetActors = new int[1] { StumpKickDelay_StateMachine68_Reference_02.Creator.ActorNumber };
		PhotonSerializer.SerializePhotonView(getView, val);
		((Component)VRRig.LocalRig).transform.position = position;
		((Behaviour)VRRig.LocalRig).enabled = enabled;
		Safety.ResetNetworkLimits();
		return false;
	}

	public static void TouchToBarrelFling()
	{
		Transform[] hands = (Transform[])(object)new Transform[2]
		{
			Variables.Variables_Reference_09.leftHandTransform,
			Variables.Variables_Reference_09.rightHandTransform
		};
		using IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			while (true)
			{
				VRRig current = enumerator.Current;
				if ((Object)(object)current == (Object)null || current.isOfflineVRRig || current.isLocal)
				{
					break;
				}
				Transform[] targets = (Transform[])(object)new Transform[2]
				{
					current.headMesh.transform,
					current.bodyTransform
				};
				if (RigManager.AreHandsNearTargets(hands, targets))
				{
					RaiseEventOptions val = new RaiseEventOptions();
					val.TargetActors = new int[1] { RigManager.GetPlayer(current).ActorNumber };
					BarrelFlingPlayer(current, val);
					return;
				}
				if (!enumerator.MoveNext())
				{
					return;
				}
			}
		}
	}

	public static void BarrelFlingNearbyPlayers()
	{
		using IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.Where((VRRig x) => !x.isLocal).GetEnumerator();
		if (!enumerator.MoveNext())
		{
			return;
		}
		while (true)
		{
			VRRig current = enumerator.Current;
			if (!(Vector3.Distance(VRRig.LocalRig.leftHand.rigTarget.position, current.bodyTransform.position) > 0.5f) || Vector3.Distance(VRRig.LocalRig.rightHand.rigTarget.position, current.bodyTransform.position) <= 0.5f)
			{
				RaiseEventOptions val = new RaiseEventOptions();
				val.TargetActors = new int[1] { current.Creator.ActorNumber };
				BarrelFlingPlayer(current, val, GTPlayer.Instance.GetHandVelocityTracker(Vector3.Distance(VRRig.LocalRig.leftHand.rigTarget.position, current.bodyTransform.position) <= 0.5f).GetAverageVelocity(false, 0.15f, false) + (Vector3.up + new Vector3(0f, 50f, 0f)) * 100f);
				if (!enumerator.MoveNext())
				{
					break;
				}
			}
			else if (!enumerator.MoveNext())
			{
				break;
			}
		}
	}

	public static void LagAll()
	{
		if (!InputHandler.IsRightTriggerPressed())
		{
			return;
		}
		List<int> list = new List<int>();
		using (IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				while (true)
				{
					VRRig current = enumerator.Current;
					if ((Object)(object)current == (Object)null || current.isOfflineVRRig || current.isLocal)
					{
						break;
					}
					list.Add(current.Creator.ActorNumber);
					if (!enumerator.MoveNext())
					{
						goto EndBranch_00ce;
					}
				}
				continue;
				EndBranch_00ce:
				break;
			}
		}
		if (list.Count > 0)
		{
			SendLagEvents(list.ToArray());
		}
	}

	public static void EarrapeAll()
	{
		Sound.SetSquareWaveMicrophoneEnabled(enable: true);
		PhotonSerializer.OverrideSerializedPose(delegate(VRRig targetRig)
		{
			((Component)VRRig.LocalRig).transform.SetPositionAndRotation(((Component)targetRig).transform.position, ((Component)targetRig).transform.rotation);
			VRRig.LocalRig.head.rigTarget.SetPositionAndRotation(targetRig.head.rigTarget.position, targetRig.head.rigTarget.rotation);
			VRRig.LocalRig.leftHand.rigTarget.SetPositionAndRotation(targetRig.leftHand.rigTarget.position, targetRig.leftHand.rigTarget.rotation);
			VRRig.LocalRig.rightHand.rigTarget.SetPositionAndRotation(targetRig.rightHand.rigTarget.position, targetRig.rightHand.rigTarget.rotation);
		});
	}

	private static Vector3 GetRandomFlingPosition(VRRig r)
	{
		float num = Random.Range(50000f, 100000f);
		return new Vector3(num, num, num);
	}
}
