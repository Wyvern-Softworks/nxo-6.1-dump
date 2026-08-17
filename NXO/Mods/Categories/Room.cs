using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ExitGames.Client.Photon;
using GorillaGameModes;
using GorillaNetworking;
using GorillaTagScripts;
using NXO.Menu;
using NXO.Utilities;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace NXO.Mods.Categories;

public class Room : MonoBehaviourPunCallbacks
{
	[CompilerGenerated]
	private sealed class ForceCreateRoom_StateMachine11 : IAsyncStateMachine
	{
		public int State;

		public AsyncTaskMethodBuilder Builder;

		public string name;

		public RoomConfig options;

		private TaskAwaiter Awaiter1;

		private TaskAwaiter<NetJoinResult> Awaiter2;

		private void MoveNext()
		{
			int num = State;
			try
			{
				TaskAwaiter<NetJoinResult> awaiter;
				if (num == 0)
				{
					TaskAwaiter taskAwaiter = Awaiter1;
					Awaiter1 = default(TaskAwaiter);
					State = -1;
					taskAwaiter.GetResult();
					NetworkSystem instance = NetworkSystem.Instance;
					awaiter = TryCreateRoomCompat((NetworkSystemPUN)((instance is NetworkSystemPUN) ? instance : null), name, options).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						goto Branch_019b;
					}
					goto Branch_01f0;
				}
				ForceCreateRoom_StateMachine11 stateMachine;
				if (num != 1)
				{
					if (NetworkSystem.Instance.InRoom)
					{
						TaskAwaiter taskAwaiter = NetworkSystem.Instance.ReturnToSinglePlayer().GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							State = 0;
							Awaiter1 = taskAwaiter;
							stateMachine = this;
							Builder.AwaitUnsafeOnCompleted(ref taskAwaiter, ref stateMachine);
							return;
						}
						taskAwaiter.GetResult();
						NetworkSystem instance2 = NetworkSystem.Instance;
						awaiter = TryCreateRoomCompat((NetworkSystemPUN)((instance2 is NetworkSystemPUN) ? instance2 : null), name, options).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							goto Branch_019b;
						}
					}
					else
					{
						NetworkSystem instance3 = NetworkSystem.Instance;
						awaiter = TryCreateRoomCompat((NetworkSystemPUN)((instance3 is NetworkSystemPUN) ? instance3 : null), name, options).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							goto Branch_019b;
						}
					}
					goto Branch_01f0;
				}
				awaiter = Awaiter2;
				Awaiter2 = default(TaskAwaiter<NetJoinResult>);
				State = -1;
				awaiter.GetResult();
				goto EndBranch_0011;
				Branch_019b:
				State = 1;
				Awaiter2 = awaiter;
				stateMachine = this;
				Builder.AwaitUnsafeOnCompleted(ref awaiter, ref stateMachine);
				return;
				Branch_01f0:
				awaiter.GetResult();
				EndBranch_0011:;
			}
			catch (Exception exception)
			{
				State = -2;
				Builder.SetException(exception);
				return;
			}
			State = -2;
			Builder.SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			this.MoveNext();
		}

		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			this.SetStateMachine(stateMachine);
		}
	}

	public static string ForceCreateRoom_StateMachine11_Text_01;

	public static bool ForceCreateRoom_StateMachine11_State_01;

	private static Task<NetJoinResult> TryCreateRoomCompat(NetworkSystemPUN network, string roomName, RoomConfig options)
	{
		return ReflectionCompat.Invoke(network, "TryCreateRoom", roomName, options) as Task<NetJoinResult> ?? Task.FromResult(default(NetJoinResult));
	}

	private static byte GetRoomSizeForCreate(GTZone zone, GameModeType mode, bool privateRoom, bool subscribed)
	{
		object result = ReflectionCompat.InvokeStatic(ReflectionCompat.FindType("RoomSystem"), "GetRoomSizeForCreate", zone, mode, privateRoom, subscribed);
		return result is byte size ? size : (byte)10;
	}

	private static Task SendPartyFollowCommandsCompat()
	{
		return ReflectionCompat.Invoke(PhotonNetworkController.Instance, "SendPartyFollowCommands") as Task ?? Task.CompletedTask;
	}

	public Room()
	{
	}

	public static void DumpAllRpcs()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("=== ALL RPCS IN GORILLA TAG ===");
		stringBuilder.AppendLine($"Generated: {DateTime.Now}\n");
		int num = 0;
		Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
		foreach (Assembly assembly in assemblies)
		{
			try
			{
				Type[] types = assembly.GetTypes();
				int num2 = 0;
				while (num2 < types.Length)
				{
					Type type = types[num2];
					IEnumerable<MethodInfo> enumerable = from m in type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
						where ((MemberInfo)m).GetCustomAttribute<PunRPC>() != null
						select m;
					if (enumerable.Any())
					{
						stringBuilder.AppendLine("\n━━━ " + type.FullName + " ━━━");
						using (IEnumerator<MethodInfo> enumerator = enumerable.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								while (true)
								{
									MethodInfo methodInfo = enumerator.Current;
									ParameterInfo[] parameters = methodInfo.GetParameters();
									string text;
									if (parameters.Length == 0)
									{
										text = "no parameters";
										stringBuilder.AppendLine("  [RPC] " + methodInfo.Name + "(" + text + ")");
										num++;
										if (!enumerator.MoveNext())
										{
											break;
										}
										continue;
									}
									text = string.Join(", ", parameters.Select((ParameterInfo p) => p.ParameterType.Name + " " + p.Name));
									stringBuilder.AppendLine("  [RPC] " + methodInfo.Name + "(" + text + ")");
									num++;
									if (!enumerator.MoveNext())
									{
										break;
									}
								}
							}
						}
						num2++;
					}
					else
					{
						num2++;
					}
				}
			}
			catch (Exception)
			{
			}
		}
		stringBuilder.AppendLine($"\n\nTotal RPCs Found: {num}");
		string text2 = Path.Combine(Variables.Variables_Text_01, $"RPC_Dump_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt");
		File.WriteAllText(text2, stringBuilder.ToString());
		Process.Start(new ProcessStartInfo(text2)
		{
			UseShellExecute = true
		});
	}

	public static void Disconnect()
	{
		if (!PhotonNetwork.InRoom)
		{
			NotificationLib.ShowNotification(NotificationLib.NotificationType.Error, "Not In A Room");
		}
		else
		{
			NetworkSystem.Instance.ReturnToSinglePlayer();
		}
	}

	public static void CreatePublic(string roomName, bool isPublic, byte roomSize = 0, JoinType roomJoinType = (JoinType)0)
	{
		RoomConfig val4;
		if (roomSize > 10 && !roomName.StartsWith("@"))
		{
			string text = "@" + roomName;
			roomName = text;
			GorillaNetworkJoinTrigger val = ((PhotonNetworkController)PhotonNetworkController.Instance).currentJoinTrigger ?? ((GorillaComputer)GorillaComputer.instance).GetJoinTriggerForZone("forest");
			RoomConfig val2 = new RoomConfig
			{
				createIfMissing = true,
				isJoinable = true,
				isPublic = isPublic,
				MaxPlayers = ((roomSize == 0) ? GetRoomSizeForCreate(val.zone, Enum.Parse<GameModeType>(((GorillaComputer)GorillaComputer.instance).currentGameMode.Value, true), !isPublic, SubscriptionManager.IsLocalSubscribed()) : roomSize)
			};
			Hashtable val3 = new Hashtable();
			((Dictionary<object, object>)val3).Add((object)"platform", (object)ReflectionCompat.GetField(PhotonNetworkController.Instance, "platformTag", "OTHER"));
			((Dictionary<object, object>)val3).Add((object)"gameMode", (object)val.GetFullDesiredGameModeString());
			((Dictionary<object, object>)val3).Add((object)"language", (object)((object)LocalisationManager.CurrentLanguage).ToString());
			((Dictionary<object, object>)val3).Add((object)"fan_club", (object)(SubscriptionManager.IsLocalSubscribed() ? "true" : "false"));
			((Dictionary<object, object>)val3).Add((object)"queueName", (object)((GorillaComputer)GorillaComputer.instance).currentQueue);
			val2.CustomProps = val3;
			val4 = val2;
			ReflectionCompat.SetField(PhotonNetworkController.Instance, "currentJoinType", roomJoinType);
			if ((int)roomJoinType != 2)
			{
				goto Branch_02a2;
			}
		}
		else
		{
			GorillaNetworkJoinTrigger val = ((PhotonNetworkController)PhotonNetworkController.Instance).currentJoinTrigger ?? ((GorillaComputer)GorillaComputer.instance).GetJoinTriggerForZone("forest");
			RoomConfig val5 = new RoomConfig
			{
				createIfMissing = true,
				isJoinable = true,
				isPublic = isPublic,
				MaxPlayers = ((roomSize == 0) ? GetRoomSizeForCreate(val.zone, Enum.Parse<GameModeType>(((GorillaComputer)GorillaComputer.instance).currentGameMode.Value, true), !isPublic, SubscriptionManager.IsLocalSubscribed()) : roomSize)
			};
			Hashtable val6 = new Hashtable();
			((Dictionary<object, object>)val6).Add((object)"platform", (object)ReflectionCompat.GetField(PhotonNetworkController.Instance, "platformTag", "OTHER"));
			((Dictionary<object, object>)val6).Add((object)"gameMode", (object)val.GetFullDesiredGameModeString());
			((Dictionary<object, object>)val6).Add((object)"language", (object)((object)LocalisationManager.CurrentLanguage).ToString());
			((Dictionary<object, object>)val6).Add((object)"fan_club", (object)(SubscriptionManager.IsLocalSubscribed() ? "true" : "false"));
			((Dictionary<object, object>)val6).Add((object)"queueName", (object)((GorillaComputer)GorillaComputer.instance).currentQueue);
			val5.CustomProps = val6;
			val4 = val5;
			ReflectionCompat.SetField(PhotonNetworkController.Instance, "currentJoinType", roomJoinType);
			if ((int)roomJoinType != 2)
			{
				goto Branch_02a2;
			}
		}
		goto Branch_02ce;
		Branch_03ba:
		val4.SetFriendIDs(((PhotonNetworkController)PhotonNetworkController.Instance).FriendIDList);
		Branch_0350:
		if (ForceCreateRoom_StateMachine11_State_01)
		{
			NetworkSystem instance = NetworkSystem.Instance;
			ReflectionCompat.SetField((NetworkSystemPUN)((instance is NetworkSystemPUN) ? instance : null), "internalState", 16);
			ForceCreateRoomAsync(roomName, val4);
		}
		else
		{
			NetworkSystem.Instance.ConnectToRoom(roomName, val4, -1);
		}
		return;
		Branch_02ce:
		Task.Run((Func<Task>)SendPartyFollowCommandsCompat);
		JoinType val7 = roomJoinType;
		JoinType val8 = val7;
		int num = (int)val8 - 1;
		num = (((uint)num <= 3u) ? num : 4) + 82;
		int num2 = num;
		if (num2 != 83)
		{
			goto Branch_03ba;
		}
		goto Branch_03d6;
		Branch_02a2:
		if ((int)roomJoinType == 4)
		{
			goto Branch_02ce;
		}
		val7 = roomJoinType;
		val8 = val7;
		int num3 = (int)val8 - 1;
		num3 = (((uint)num3 <= 3u) ? num3 : 4) + 82;
		num2 = num3;
		if (num2 != 83)
		{
			goto Branch_03ba;
		}
		Branch_03d6:
		val4.SetFriendIDs(FriendshipGroupDetection.Instance.PartyMemberIDs.ToList());
		goto Branch_0350;
	}

	[AsyncStateMachine(typeof(ForceCreateRoom_StateMachine11))]
	[DebuggerStepThrough]
	public static Task ForceCreateRoomAsync(string name, RoomConfig options)
	{
		ForceCreateRoom_StateMachine11 stateMachine = new ForceCreateRoom_StateMachine11();
		stateMachine.Builder = AsyncTaskMethodBuilder.Create();
		stateMachine.name = name;
		stateMachine.options = options;
		stateMachine.State = -1;
		stateMachine.Builder.Start(ref stateMachine);
		return stateMachine.Builder.Task;
	}

	public static void JoinSpecificRoom()
	{
		SearchAndKeyboard.OpenTextInput("", "Enter room code...");
		SearchAndKeyboard.KeyCollider_Text_01 = delegate(string code)
		{
			if (string.IsNullOrEmpty(code))
			{
				NotificationLib.ShowNotification(NotificationLib.NotificationType.Error, "No Room Code Entered");
			}
			else
			{
				((PhotonNetworkController)PhotonNetworkController.Instance).AttemptToJoinSpecificRoomWithCallback(code, (JoinType)0, (Action<NetJoinResult>)delegate(NetJoinResult result)
				{
					if ((int)result == 2)
					{
						NotificationLib.ShowNotification(NotificationLib.NotificationType.Error, "Room Is Full");
					}
					else if ((int)result == 4)
					{
						NotificationLib.ShowNotification(NotificationLib.NotificationType.Error, "Unknown");
					}
				});
			}
		};
	}

	public static void Reconnect()
	{
		if (string.IsNullOrEmpty(ForceCreateRoom_StateMachine11_Text_01))
		{
			NotificationLib.ShowNotification(NotificationLib.NotificationType.Error, "No Room Code Saved");
			return;
		}
		if (PhotonNetwork.InRoom)
		{
			NetworkSystem.Instance.ReturnToSinglePlayer();
			CoroutineHelper.InvokeAfterDelay(1.5f, Reconnect);
			return;
		}
		((PhotonNetworkController)PhotonNetworkController.Instance).AttemptToJoinSpecificRoomWithCallback(ForceCreateRoom_StateMachine11_Text_01, (JoinType)0, (Action<NetJoinResult>)delegate(NetJoinResult result)
		{
			if ((int)result == 2)
			{
				NotificationLib.ShowNotification(NotificationLib.NotificationType.Error, "Room Is Full");
			}
			else if ((int)result == 4)
			{
				NotificationLib.ShowNotification(NotificationLib.NotificationType.Error, "Unknown");
			}
		});
	}

	public static void AntiGrabAppQuit(bool isActive)
	{
		GameObject val = GameObject.Find("Environment Objects/TriggerZones_Prefab/ZoneTransitions_Prefab/QuitBox");
		if ((Object)(object)val != (Object)null)
		{
			val.SetActive(isActive);
		}
	}

	public static void JoinRandomPublic()
	{
		if (PhotonNetwork.InRoom)
		{
			NetworkSystem.Instance.ReturnToSinglePlayer();
			CoroutineHelper.InvokeAfterDelay(1.5f, JoinRandomPublic);
			return;
		}
		GorillaNetworkJoinTrigger val = ((PhotonNetworkController)PhotonNetworkController.Instance).currentJoinTrigger ?? ((GorillaComputer)GorillaComputer.instance).GetJoinTriggerForZone("forest");
		if ((Object)(object)val == (Object)null)
		{
			NotificationLib.ShowNotification(NotificationLib.NotificationType.Error, "No Join Trigger Found");
		}
		else
		{
			((PhotonNetworkController)PhotonNetworkController.Instance).AttemptToJoinPublicRoom(val, (JoinType)0, (List<ValueTuple<string, string>>)null, false);
		}
	}

	public static void UnlockVIM()
	{
		SubscriptionManager manager = ReflectionCompat.GetStaticField<SubscriptionManager>(typeof(SubscriptionManager), "Instance");
		Dictionary<NetPlayer, SubscriptionDetails> dictionary = ReflectionCompat.GetField<Dictionary<NetPlayer, SubscriptionDetails>>(manager, "subData");
		if (dictionary == null)
		{
			return;
		}
		using List<KeyValuePair<NetPlayer, SubscriptionDetails>>.Enumerator enumerator = dictionary.ToList().GetEnumerator();
		if (!enumerator.MoveNext())
		{
			return;
		}
		while (true)
		{
			KeyValuePair<NetPlayer, SubscriptionDetails> current = enumerator.Current;
			if (current.Key.IsLocal)
			{
				SubscriptionDetails value = current.Value;
				value.active = true;
				value.tier = 1;
				value.daysAccrued = 128;
				dictionary[current.Key] = value;
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

	public static void DisableNetworkTriggers(bool isActive)
	{
		GameObject val = GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab");
		if ((Object)(object)val != (Object)null)
		{
			val.SetActive(isActive);
		}
	}

	public static string ResetCreatePublic()
	{
		string text;
		do
		{
			text = GenerateRoomCode();
		}
		while (((GorillaComputer)GorillaComputer.instance).CheckAutoBanListForName(text));
		return text;
	}

	public static string GenerateRoomCode(int length = 4)
	{
		char[] array = new char[length];
		int num = 0;
		if (num < length)
		{
			do
			{
				array[num] = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"[Random.Range(0, "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".Length)];
				num++;
			}
			while (num < length);
		}
		return new string(array);
	}

	public static void GrabOwnID()
	{
		StringBuilder stringBuilder = new StringBuilder("SELF ID GRABBED FROM NXO");
		stringBuilder.AppendLine("NAME: " + PhotonNetwork.LocalPlayer.NickName + " ID: " + PhotonNetwork.LocalPlayer.UserId);
		string text = Path.Combine(Variables.Variables_Text_01, "Self_ID_By_NXO.txt");
		File.WriteAllText(text, stringBuilder.ToString());
		Process.Start(new ProcessStartInfo(text)
		{
			UseShellExecute = true
		});
	}

	public static void GrabAllIDs()
	{
		if (PhotonNetwork.CurrentRoom == null || PhotonNetwork.PlayerList == null || PhotonNetwork.PlayerList.Length == 0)
		{
			Debug.LogError((object)"Failed to grab IDs: No room or players found.");
			return;
		}
		StringBuilder stringBuilder = new StringBuilder("IDS GRABBED FROM NXO \nIDS GRABBED FROM ROOM: " + PhotonNetwork.CurrentRoom.Name + "\n\n");
		PhotonPlayer[] playerList = PhotonNetwork.PlayerList;
		foreach (PhotonPlayer val in playerList)
		{
			stringBuilder.AppendLine("NAME: " + val.NickName + " ID: " + val.UserId);
		}
		string text = Path.Combine(Variables.Variables_Text_01, "Grabbed_IDs_By_NXO.txt");
		File.WriteAllText(text, stringBuilder.ToString());
		Process.Start(new ProcessStartInfo(text)
		{
			UseShellExecute = true
		});
	}
}
