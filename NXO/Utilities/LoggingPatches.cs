using System;
using System.Collections.Generic;
using System.Text;
using ExitGames.Client.Photon;
using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace NXO.Utilities;

[HarmonyPatch]
public static class LoggingPatches
{
	[HarmonyPatch(typeof(PhotonNetwork), "RaiseEvent")]
	[HarmonyPrefix]
	public static void LogOutgoingEvent(byte eventCode, object eventContent, RaiseEventOptions raiseEventOptions, SendOptions sendOptions)
	{
		if (!Logger.CapturedVariables40_State_01 || Logger.CapturedVariables40_Set_01.Contains(eventCode))
		{
			return;
		}
		try
		{
			Player localPlayer = PhotonNetwork.LocalPlayer;
			string arg = ((localPlayer != null) ? localPlayer.NickName : null) ?? "Local";
			Player localPlayer2 = PhotonNetwork.LocalPlayer;
			int num = ((localPlayer2 != null) ? localPlayer2.ActorNumber : (-1));
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("═══════════════════════════════════════════════════════════════");
			stringBuilder.AppendLine("[EVENT] PhotonNetwork OUT");
			stringBuilder.AppendLine($"  ├─ Code: {eventCode}");
			stringBuilder.AppendLine($"  ├─ Sender: {arg} (Actor #{num})");
			if (eventContent != null)
			{
				stringBuilder.AppendLine("  ├─ Data: " + Logger.FormatData(eventContent));
				if (raiseEventOptions == null)
				{
					goto Branch_0131;
				}
			}
			else if (raiseEventOptions == null)
			{
				goto Branch_0131;
			}
			if (raiseEventOptions.TargetActors == null)
			{
				goto Branch_01c8;
			}
			goto Branch_0166;
			Branch_0131:
			goto Branch_01c8;
			Branch_0166:
			stringBuilder.AppendLine("  ├─ Targets: [" + string.Join(", ", raiseEventOptions.TargetActors) + "]");
			stringBuilder.AppendLine($"  └─ Time: {DateTime.Now:HH:mm:ss.fff}");
			stringBuilder.AppendLine("═══════════════════════════════════════════════════════════════");
			Debug.Log((object)stringBuilder.ToString());
			return;
			Branch_01c8:
			stringBuilder.AppendLine($"  └─ Time: {DateTime.Now:HH:mm:ss.fff}");
			stringBuilder.AppendLine("═══════════════════════════════════════════════════════════════");
			Debug.Log((object)stringBuilder.ToString());
		}
		catch (Exception)
		{
		}
	}

	[HarmonyPatch(typeof(PhotonPeer), "SendOperation", new Type[]
	{
		typeof(byte),
		typeof(Dictionary<byte, object>),
		typeof(SendOptions)
	})]
	[HarmonyPrefix]
	public static void LogOutgoingOperation(byte operationCode, Dictionary<byte, object> operationParameters, SendOptions sendOptions)
	{
		if (!Logger.CapturedVariables40_State_01)
		{
			return;
		}
		try
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("═══════════════════════════════════════════════════════════════");
			stringBuilder.AppendLine("[OPERATION] SendOperation");
			stringBuilder.AppendLine($"  ├─ OpCode: {operationCode}");
			if (operationParameters != null)
			{
				using (Dictionary<byte, object>.Enumerator enumerator = operationParameters.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						do
						{
							KeyValuePair<byte, object> current = enumerator.Current;
							stringBuilder.AppendLine($"  ├─ [{current.Key}] = {Logger.FormatData(current.Value)}");
						}
						while (enumerator.MoveNext());
					}
				}
				stringBuilder.AppendLine($"  └─ Time: {DateTime.Now:HH:mm:ss.fff}");
				stringBuilder.AppendLine("═══════════════════════════════════════════════════════════════");
				Debug.Log((object)stringBuilder.ToString());
			}
			else
			{
				stringBuilder.AppendLine($"  └─ Time: {DateTime.Now:HH:mm:ss.fff}");
				stringBuilder.AppendLine("═══════════════════════════════════════════════════════════════");
				Debug.Log((object)stringBuilder.ToString());
			}
		}
		catch (Exception)
		{
		}
	}

	[HarmonyPatch(typeof(VRRig), "IncrementRPC", new Type[]
	{
		typeof(PhotonMessageInfoWrapped),
		typeof(string)
	})]
	[HarmonyPrefix]
	public static void LogWrappedRigRpc(VRRig __instance, PhotonMessageInfoWrapped info, string sourceCall)
	{
		if (!Logger.CapturedVariables40_State_01 || Logger.CapturedVariables40_Set_02.Contains(sourceCall))
		{
			return;
		}
		try
		{
			string arg = "Unknown";
			int num = -1;
			NetworkView netView = ReflectionCompat.GetField<NetworkView>(__instance, "netView");
			PhotonView val = ((netView != null) ? netView.GetView : null);
			if ((Object)(object)val != (Object)null && val.Owner != null)
			{
				arg = val.Owner.NickName ?? "Unknown";
				num = val.Owner.ActorNumber;
			}
			else if (__instance != null && __instance.Creator != null)
			{
				goto Branch_012f;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("═══════════════════════════════════════════════════════════════");
			stringBuilder.AppendLine("[RPC] VRRig");
			stringBuilder.AppendLine("  ├─ Method: " + sourceCall);
			stringBuilder.AppendLine($"  ├─ Sender: {arg} (Actor #{num})");
			stringBuilder.AppendLine($"  └─ Time: {DateTime.Now:HH:mm:ss.fff}");
			stringBuilder.AppendLine("═══════════════════════════════════════════════════════════════");
			Debug.Log((object)stringBuilder.ToString());
			return;
			Branch_012f:
			arg = __instance.Creator.NickName ?? "Unknown";
			num = __instance.Creator.ActorNumber;
			stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("═══════════════════════════════════════════════════════════════");
			stringBuilder.AppendLine("[RPC] VRRig");
			stringBuilder.AppendLine("  ├─ Method: " + sourceCall);
			stringBuilder.AppendLine($"  ├─ Sender: {arg} (Actor #{num})");
			stringBuilder.AppendLine($"  └─ Time: {DateTime.Now:HH:mm:ss.fff}");
			stringBuilder.AppendLine("═══════════════════════════════════════════════════════════════");
			Debug.Log((object)stringBuilder.ToString());
		}
		catch (Exception)
		{
		}
	}

	[HarmonyPatch(typeof(PhotonView), "TransferOwnership", new Type[] { typeof(Player) })]
	[HarmonyPrefix]
	public static void LogOwnershipTransfer(PhotonView __instance, Player newOwner)
	{
		if (!Logger.CapturedVariables40_State_01)
		{
			return;
		}
		try
		{
			Player owner = __instance.Owner;
			string text = ((owner != null) ? owner.NickName : null) ?? "None";
			if (newOwner == null)
			{
				string text2 = null ?? "None";
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("═══════════════════════════════════════════════════════════════");
				stringBuilder.AppendLine("[OWNERSHIP] Transfer");
				stringBuilder.AppendLine($"  ├─ ViewID: {__instance.ViewID}");
				stringBuilder.AppendLine("  ├─ From: " + text);
				stringBuilder.AppendLine("  ├─ To: " + text2);
				stringBuilder.AppendLine($"  └─ Time: {DateTime.Now:HH:mm:ss.fff}");
				stringBuilder.AppendLine("═══════════════════════════════════════════════════════════════");
				Debug.Log((object)stringBuilder.ToString());
			}
			else
			{
				string text2 = newOwner.NickName ?? "None";
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("═══════════════════════════════════════════════════════════════");
				stringBuilder.AppendLine("[OWNERSHIP] Transfer");
				stringBuilder.AppendLine($"  ├─ ViewID: {__instance.ViewID}");
				stringBuilder.AppendLine("  ├─ From: " + text);
				stringBuilder.AppendLine("  ├─ To: " + text2);
				stringBuilder.AppendLine($"  └─ Time: {DateTime.Now:HH:mm:ss.fff}");
				stringBuilder.AppendLine("═══════════════════════════════════════════════════════════════");
				Debug.Log((object)stringBuilder.ToString());
			}
		}
		catch (Exception)
		{
		}
	}

	[HarmonyPatch(typeof(PhotonView), "RequestOwnership")]
	[HarmonyPrefix]
	public static void LogOwnershipRequest(PhotonView __instance)
	{
		if (!Logger.CapturedVariables40_State_01)
		{
			return;
		}
		try
		{
			Player owner = __instance.Owner;
			string text = ((owner != null) ? owner.NickName : null) ?? "None";
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("═══════════════════════════════════════════════════════════════");
			stringBuilder.AppendLine("[OWNERSHIP] Request");
			stringBuilder.AppendLine($"  ├─ ViewID: {__instance.ViewID}");
			stringBuilder.AppendLine("  ├─ CurrentOwner: " + text);
			stringBuilder.AppendLine($"  └─ Time: {DateTime.Now:HH:mm:ss.fff}");
			stringBuilder.AppendLine("═══════════════════════════════════════════════════════════════");
			Debug.Log((object)stringBuilder.ToString());
		}
		catch (Exception)
		{
		}
	}

	[HarmonyPatch(typeof(MonkeAgent), "IncrementRPCCallLocal", new Type[]
	{
		typeof(PhotonMessageInfoWrapped),
		typeof(string)
	})]
	[HarmonyPrefix]
	public static void LogLocalRpc(MonkeAgent __instance, PhotonMessageInfoWrapped infoWrapped, string rpcFunction)
	{
		if (!Logger.CapturedVariables40_State_01 || Logger.CapturedVariables40_Set_02.Contains(rpcFunction))
		{
			return;
		}
		try
		{
			string arg = "Unknown";
			int num = -1;
			PhotonView component = ((Component)__instance).GetComponent<PhotonView>();
			if ((Object)(object)component != (Object)null && component.Owner != null)
			{
				arg = component.Owner.NickName ?? "Unknown";
				num = component.Owner.ActorNumber;
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("═══════════════════════════════════════════════════════════════");
				stringBuilder.AppendLine("[RPC] MonkeAgent.Local");
				stringBuilder.AppendLine("  ├─ Method: " + rpcFunction);
				stringBuilder.AppendLine($"  ├─ Sender: {arg} (Actor #{num})");
				stringBuilder.AppendLine($"  └─ Time: {DateTime.Now:HH:mm:ss.fff}");
				stringBuilder.AppendLine("═══════════════════════════════════════════════════════════════");
				Debug.Log((object)stringBuilder.ToString());
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("═══════════════════════════════════════════════════════════════");
				stringBuilder.AppendLine("[RPC] MonkeAgent.Local");
				stringBuilder.AppendLine("  ├─ Method: " + rpcFunction);
				stringBuilder.AppendLine($"  ├─ Sender: {arg} (Actor #{num})");
				stringBuilder.AppendLine($"  └─ Time: {DateTime.Now:HH:mm:ss.fff}");
				stringBuilder.AppendLine("═══════════════════════════════════════════════════════════════");
				Debug.Log((object)stringBuilder.ToString());
			}
		}
		catch (Exception)
		{
		}
	}

	[HarmonyPatch(typeof(VRRig), "IncrementRPC", new Type[]
	{
		typeof(PhotonMessageInfo),
		typeof(string)
	})]
	[HarmonyPrefix]
	public static void LogRigRpc(VRRig __instance, PhotonMessageInfo info, string sourceCall)
	{
		if (!Logger.CapturedVariables40_State_01 || Logger.CapturedVariables40_Set_02.Contains(sourceCall))
		{
			return;
		}
		try
		{
			Player sender = info.Sender;
			string arg = ((sender != null) ? sender.NickName : null) ?? "Unknown";
			Player sender2 = info.Sender;
			int num = ((sender2 != null) ? sender2.ActorNumber : (-1));
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("═══════════════════════════════════════════════════════════════");
			stringBuilder.AppendLine("[RPC] VRRig");
			stringBuilder.AppendLine("  ├─ Method: " + sourceCall);
			stringBuilder.AppendLine($"  ├─ Sender: {arg} (Actor #{num})");
			stringBuilder.AppendLine($"  └─ Time: {DateTime.Now:HH:mm:ss.fff}");
			stringBuilder.AppendLine("═══════════════════════════════════════════════════════════════");
			Debug.Log((object)stringBuilder.ToString());
		}
		catch (Exception)
		{
		}
	}

	[HarmonyPatch(typeof(MonkeAgent), "IncrementRPCCall", new Type[]
	{
		typeof(PhotonMessageInfo),
		typeof(string)
	})]
	[HarmonyPrefix]
	public static void LogAgentRpc(PhotonMessageInfo info, string callingMethod)
	{
		if (!Logger.CapturedVariables40_State_01 || Logger.CapturedVariables40_Set_02.Contains(callingMethod))
		{
			return;
		}
		try
		{
			Player sender = info.Sender;
			string arg = ((sender != null) ? sender.NickName : null) ?? "Unknown";
			Player sender2 = info.Sender;
			int num = ((sender2 != null) ? sender2.ActorNumber : (-1));
			Player sender3 = info.Sender;
			bool flag = sender3 != null && sender3.IsMasterClient;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("═══════════════════════════════════════════════════════════════");
			stringBuilder.AppendLine("[RPC] MonkeAgent");
			stringBuilder.AppendLine("  ├─ Method: " + callingMethod);
			stringBuilder.AppendLine(string.Format("  ├─ Sender: {0} (Actor #{1}) {2}", arg, num, flag ? "[MASTER]" : ""));
			stringBuilder.AppendLine($"  └─ Time: {DateTime.Now:HH:mm:ss.fff}");
			stringBuilder.AppendLine("═══════════════════════════════════════════════════════════════");
			Debug.Log((object)stringBuilder.ToString());
		}
		catch (Exception)
		{
		}
	}

	[HarmonyPatch(typeof(LoadBalancingClient), "OnEvent")]
	[HarmonyPrefix]
	public static void LogIncomingEvent(EventData photonEvent)
	{
		if (!Logger.CapturedVariables40_State_01 || Logger.CapturedVariables40_Set_01.Contains(photonEvent.Code))
		{
			return;
		}
		try
		{
			int sender = photonEvent.Sender;
			Room currentRoom = PhotonNetwork.CurrentRoom;
			Player val = ((currentRoom != null) ? currentRoom.GetPlayer(sender, false) : null);
			string arg;
			if (val == null)
			{
				arg = null ?? $"Actor{sender}";
				if (val == null)
				{
					goto Branch_00e6;
				}
			}
			else
			{
				arg = val.NickName ?? $"Actor{sender}";
				if (val == null)
				{
					goto Branch_00e6;
				}
			}
			bool isMasterClient = val.IsMasterClient;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("═══════════════════════════════════════════════════════════════");
			stringBuilder.AppendLine("[EVENT] Photon IN");
			stringBuilder.AppendLine($"  ├─ Code: {photonEvent.Code}");
			stringBuilder.AppendLine(string.Format("  ├─ Sender: {0} (Actor #{1}) {2}", arg, sender, isMasterClient ? "[MASTER]" : ""));
			if (photonEvent.CustomData == null)
			{
				goto Branch_023b;
			}
			goto Branch_0201;
			Branch_023b:
			if (photonEvent.Parameters == null)
			{
				goto Branch_0334;
			}
			Branch_0258:
			var enumerator = photonEvent.Parameters.GetEnumerator();
			try
			{
				if (enumerator.MoveNext())
				{
					do
					{
						KeyValuePair<byte, object> current = enumerator.Current;
						stringBuilder.AppendLine($"  ├─ [{current.Key}] = {Logger.FormatData(current.Value)}");
					}
					while (enumerator.MoveNext());
				}
			}
			finally
			{
				((IDisposable)enumerator/*cast due to constrained. prefix*/).Dispose();
			}
			stringBuilder.AppendLine($"  └─ Time: {DateTime.Now:HH:mm:ss.fff}");
			stringBuilder.AppendLine("═══════════════════════════════════════════════════════════════");
			Debug.Log((object)stringBuilder.ToString());
			return;
			Branch_00e6:
			isMasterClient = false;
			stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("═══════════════════════════════════════════════════════════════");
			stringBuilder.AppendLine("[EVENT] Photon IN");
			stringBuilder.AppendLine($"  ├─ Code: {photonEvent.Code}");
			stringBuilder.AppendLine(string.Format("  ├─ Sender: {0} (Actor #{1}) {2}", arg, sender, isMasterClient ? "[MASTER]" : ""));
			if (photonEvent.CustomData == null)
			{
				goto Branch_023b;
			}
			goto Branch_0201;
			Branch_0334:
			stringBuilder.AppendLine($"  └─ Time: {DateTime.Now:HH:mm:ss.fff}");
			stringBuilder.AppendLine("═══════════════════════════════════════════════════════════════");
			Debug.Log((object)stringBuilder.ToString());
			return;
			Branch_0201:
			stringBuilder.AppendLine("  ├─ CustomData: " + Logger.FormatData(photonEvent.CustomData));
			if (photonEvent.Parameters == null)
			{
				goto Branch_0334;
			}
			goto Branch_0258;
		}
		catch (Exception)
		{
		}
	}

	[HarmonyPatch(typeof(LoadBalancingClient), "OpRaiseEvent")]
	[HarmonyPrefix]
	public static void LogRaisedEvent(byte eventCode, object customEventContent, RaiseEventOptions raiseEventOptions, SendOptions sendOptions)
	{
		if (!Logger.CapturedVariables40_State_01 || Logger.CapturedVariables40_Set_01.Contains(eventCode))
		{
			return;
		}
		try
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("═══════════════════════════════════════════════════════════════");
			stringBuilder.AppendLine("[OP] OpRaiseEvent");
			stringBuilder.AppendLine($"  ├─ Code: {eventCode}");
			stringBuilder.AppendLine("  ├─ Data: " + Logger.FormatData(customEventContent));
			if (raiseEventOptions?.TargetActors != null)
			{
				goto Branch_00ea;
			}
			stringBuilder.AppendLine($"  └─ Time: {DateTime.Now:HH:mm:ss.fff}");
			stringBuilder.AppendLine("═══════════════════════════════════════════════════════════════");
			Debug.Log((object)stringBuilder.ToString());
			return;
			Branch_00ea:
			stringBuilder.AppendLine("  ├─ Targets: [" + string.Join(", ", raiseEventOptions.TargetActors) + "]");
			stringBuilder.AppendLine($"  └─ Time: {DateTime.Now:HH:mm:ss.fff}");
			stringBuilder.AppendLine("═══════════════════════════════════════════════════════════════");
			Debug.Log((object)stringBuilder.ToString());
		}
		catch (Exception)
		{
		}
	}
}
