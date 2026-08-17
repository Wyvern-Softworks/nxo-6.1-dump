using System;
using System.Collections.Generic;
using System.Linq;

using ExitGames.Client.Photon;
using GorillaNetworking;
using NXO.Menu;
using NXO.Utilities;
using Photon.Pun;
using Photon.Realtime;
using Photon.Voice.Unity;
using UnityEngine;

namespace NXO.Mods.Categories;

public class Safety : MonoBehaviourPunCallbacks
{
	private static bool Safety_State_01 = false;

	private static bool Safety_State_04 = false;

	private static float Safety_Value_04 = 0f;

	private static float Safety_Value_03 = 0.5f;

	private static GameObject Safety_Object_01 = null;

	private static readonly List<ButtonHandler.Button> Safety_Items_01 = new List<ButtonHandler.Button>();

	private static bool Safety_State_06;

	private static bool Safety_State_02;

	private const string PanicButtonText = "Panic (X)";

	private static string[] Safety_Text_01 = new string[10] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "zero" };

	private static float Safety_Value_01;

	private static float Safety_Value_02 = -1f;

	private static bool Safety_State_03;

	public static readonly string[] Safety_Text_03 = new string[11]
	{
		"EPIC", "REAL", "NOT", "SILLY", "LITTLE", "BIG", "MAYBE", "SUB2", "OG", "FR",
		"NOT"
	};

	public static readonly string[] Safety_Text_04 = new string[12]
	{
		"GT", "VR", "LOL", "FAN", "XD", "LOL", "MONKE", "YT", "NOT", "FR",
		"LMAO", "GTAG"
	};

	public static readonly string[] Safety_Text_02 = new string[66]
	{
		"0", "PBBV", "J3VU", "BEES", "NEMO", "LEMMING", "BILLY", "TIMMY", "MINIGAMES", "JMANCURLY",
		"VMT", "ELLIOT", "DAISY09", "MONK", "MONKE", "MONKI", "MONKEY", "MONKIY", "GORILL", "GOORILA",
		"GORILLA", "TTT", "TTTPIG", "PPPTIG", "K9", "BANANA", "PEANUTBUTTER", "GHOSTMONKE", "STATUE", "NOVA",
		"LUNAR", "MOON", "SUN", "RANDOM", "UNKNOWN", "GLITCH", "BUG", "ERROR", "CODE", "HACKER",
		"MODDER", "INVIS", "INVISIBLE", "TAGGER", "Recovered_Reference_18", "BLUE", "RED", "GREEN", "PURPLE", "YELLOW",
		"BLACK", "WHITE", "BROWN", "CYAN", "GRAY", "GREY", "BANNED", "LEMON", "PLUSHIE", "CHEETO",
		"TIKTOK", "YOUTUBE", "TWITCH", "DISCORD", "MODDER", "HACKER"
	};

	private static bool Safety_State_05;

	private static readonly List<VRRig> Safety_Items_03 = new List<VRRig>();

	private static readonly List<VRRig> Safety_Items_02 = new List<VRRig>();

	public static int Safety_Index_02 = 4000;

	public static int Safety_Index_01 = 7;

	public static void ChangeIdentityOnDisconnect(Action identityType)
	{
		if (!PhotonNetwork.InRoom && Safety_State_05 && identityType != null)
		{
			identityType();
			Safety_State_05 = PhotonNetwork.InRoom;
		}
		else
		{
			Safety_State_05 = PhotonNetwork.InRoom;
		}
	}

	public static void NameSpoof()
	{
		List<VRRig> list = new List<VRRig>();
		using (List<VRRig>.Enumerator enumerator = Safety_Items_03.GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				while (true)
				{
					VRRig current = enumerator.Current;
					if (!VRRigCache.ActiveRigs.Contains(current))
					{
						list.Add(current);
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
		}
		using (List<VRRig>.Enumerator enumerator2 = list.GetEnumerator())
		{
			if (enumerator2.MoveNext())
			{
				do
				{
					VRRig current2 = enumerator2.Current;
					Safety_Items_03.Remove(current2);
				}
				while (enumerator2.MoveNext());
			}
		}
		list.Clear();
		string nickName = PhotonNetwork.NickName;
		using (IEnumerator<VRRig> enumerator3 = VRRigCache.ActiveRigs.GetEnumerator())
		{
			while (enumerator3.MoveNext())
			{
				while (!enumerator3.Current.isLocal)
				{
					VRRig val = enumerator3.Current;
					string text;
					string text2;
					string text3;
					if (!Safety_Items_03.Contains(val))
					{
						if (Random.Range(0, 3) != 0)
						{
							text = "";
							if (Random.Range(0, 3) != 0)
							{
								goto Branch_0216;
							}
						}
						else
						{
							text = Safety_Text_03[Random.Range(0, Safety_Text_03.Length)];
							if (Random.Range(0, 3) != 0)
							{
								goto Branch_0216;
							}
						}
						text2 = Safety_Text_04[Random.Range(0, Safety_Text_04.Length)];
						text3 = text + Safety_Text_02[Random.Range(0, Safety_Text_02.Length)] + text2;
						if (text3.Length <= 12)
						{
							goto Branch_029a;
						}
						goto Branch_032a;
					}
					if (!enumerator3.MoveNext())
					{
						goto EndBranch_03d8;
					}
					continue;
					Branch_029a:
					RigManager.SetPlayerName(text3, noColor: true);
					GorillaTagger.Instance.myVRRig.SendRPC("RPC_InitializeNoobMaterial", RigManager.GetPlayer(val), new object[3]
					{
						Random.Range(0f, 1f),
						Random.Range(0f, 1f),
						Random.Range(0f, 1f)
					});
					Safety_Items_03.Add(val);
					if (!enumerator3.MoveNext())
					{
						goto EndBranch_03d8;
					}
					continue;
					Branch_032a:
					RigManager.SetPlayerName(text3.Substring(0, 12), noColor: true);
					GorillaTagger.Instance.myVRRig.SendRPC("RPC_InitializeNoobMaterial", RigManager.GetPlayer(val), new object[3]
					{
						Random.Range(0f, 1f),
						Random.Range(0f, 1f),
						Random.Range(0f, 1f)
					});
					Safety_Items_03.Add(val);
					if (!enumerator3.MoveNext())
					{
						goto EndBranch_03d8;
					}
					continue;
					Branch_0216:
					text2 = "";
					text3 = text + Safety_Text_02[Random.Range(0, Safety_Text_02.Length)] + text2;
					if (text3.Length <= 12)
					{
						goto Branch_029a;
					}
					goto Branch_032a;
				}
				continue;
				EndBranch_03d8:
				break;
			}
		}
		if (PhotonNetwork.NickName != nickName)
		{
			PhotonNetwork.NickName = nickName;
		}
	}

	public static void ResetVisualizeAntiReport()
	{
		if ((Object)(object)Safety_Object_01 != (Object)null)
		{
			Object.Destroy((Object)(object)Safety_Object_01);
			Safety_Object_01 = null;
		}
	}

	public static void Panic()
	{
		bool pressed = InputHandler.IsLeftPrimaryPressed();
		if (pressed && !Safety_State_02)
		{
			if (Safety_State_06)
			{
				RestorePanicFeatures();
				Safety_State_02 = true;
			}
			else
			{
				EnablePanicMode();
				Safety_State_02 = true;
			}
		}
		else if (!pressed)
		{
			Safety_State_02 = false;
		}
	}

	public static void BypassAutomod()
	{
		GorillaTagger.moderationMutedTime = -1f;
		Recorder primaryRecorder;
		if (((GorillaComputer)GorillaComputer.instance).autoMuteType != "OFF")
		{
			((GorillaComputer)GorillaComputer.instance).autoMuteType = "OFF";
			PlayerPrefs.SetInt("autoMute", 0);
			PlayerPrefs.Save();
			primaryRecorder = NetworkSystem.Instance.VoiceConnection.PrimaryRecorder;
			if ((Object)(object)primaryRecorder == (Object)null)
			{
				return;
			}
		}
		else
		{
			primaryRecorder = NetworkSystem.Instance.VoiceConnection.PrimaryRecorder;
			if ((Object)(object)primaryRecorder == (Object)null)
			{
				return;
			}
		}
		if ((int)primaryRecorder.SourceType == 1)
		{
			return;
		}
		float num = 0f;
		GorillaSpeakerLoudness component = ((Component)VRRig.LocalRig).GetComponent<GorillaSpeakerLoudness>();
		if ((Object)(object)component != (Object)null)
		{
			num = component.Loudness;
			if (num == 0f)
			{
				goto Branch_0147;
			}
		}
		else if (num == 0f)
		{
			goto Branch_0147;
		}
		Safety_Value_02 = -1f;
		Safety_State_03 = false;
		Safety_Value_01 = num;
		return;
		Branch_0238:
		Safety_Value_01 = num;
		return;
		Branch_01b0:
		if (!Safety_State_03 && Time.time - Safety_Value_02 >= 0.25f)
		{
			primaryRecorder.RestartRecording(true);
			Safety_State_03 = true;
		}
		goto Branch_0238;
		Branch_0147:
		if (Safety_Value_01 != 0f)
		{
			Safety_Value_02 = Time.time;
			Safety_State_03 = false;
			if (!(Safety_Value_02 <= 0f))
			{
				goto Branch_01b0;
			}
		}
		else if (!(Safety_Value_02 <= 0f))
		{
			goto Branch_01b0;
		}
		goto Branch_0238;
	}

	private static void EnablePanicMode()
	{
		NotificationLib.ShowNotification(NotificationLib.NotificationType.Alert, "Panic Enabled, Features Disabled.");
		Safety_Items_01.Clear();
		ButtonHandler.Button[] array = ModButtons.buttons;
		int num = 0;
		while (num < array.Length)
		{
			ButtonHandler.Button button;
			while (true)
			{
				button = array[num];
				if (button == null || !button.Enabled || button.buttonText == "Panic (X)")
				{
					break;
				}
				Safety_Items_01.Add(button);
				button.Enabled = false;
				Action onDisable = button.onDisable;
				if (onDisable != null)
				{
					onDisable();
					NXOUI.TrackModDisabled(button.buttonText);
					num++;
					int num2 = ((num < array.Length) ? 1 : 0) * -9 + 18;
					if (num2 != 15)
					{
						if (num2 != 9)
						{
							goto EndBranch_0127;
						}
						continue;
					}
				}
				goto Branch_0100;
			}
			num++;
			continue;
			Branch_0100:
			NXOUI.TrackModDisabled(button.buttonText);
			num++;
			continue;
			EndBranch_0127:
			break;
		}
		Safety_State_06 = true;
		Main.RebuildMenu();
	}

	public static void ResetChangeIdentityOnDisconnect()
	{
		string text = "gorilla";
		int num = 0;
		if (num < 4)
		{
			do
			{
				text += Random.Range(0, 9);
				num++;
			}
			while (num < 4);
		}
		RigManager.SetPlayerName(text);
		byte b = (byte)Random.Range(0, 255);
		byte b2 = (byte)Random.Range(0, 255);
		byte b3 = (byte)Random.Range(0, 255);
		RigManager.SetPlayerColor((Color32)(new Color32(b, b2, b3, byte.MaxValue)));
	}

	public static void SetRankedBadgeSpoofEnabled(bool enable)
	{
		MenuPatches.RankedPatch.RankedPatch_State_01 = enable;
		float currentElo = ReflectionCompat.GetField(VRRig.LocalRig, "currentRankedELO", 0f);
		int questSubTier = ReflectionCompat.GetField(VRRig.LocalRig, "currentRankedSubTierQuest", 0);
		int pcSubTier = ReflectionCompat.GetField(VRRig.LocalRig, "currentRankedSubTierPC", 0);
		if (MenuPatches.RankedPatch.RankedPatch_State_01 && (!Mathf.Approximately(currentElo, (float)Safety_Index_02) || questSubTier != Safety_Index_01 || pcSubTier != Safety_Index_01))
		{
			VRRig.LocalRig.SetRankedInfo((float)Safety_Index_02, Safety_Index_01, Safety_Index_01, true);
		}
	}

	public static void BypassModCheckers()
	{
		PhotonPlayer localPlayer = PhotonNetwork.LocalPlayer;
		if (localPlayer == null || localPlayer.CustomProperties == null || ((Dictionary<object, object>)(object)localPlayer.CustomProperties).Count == 0)
		{
			return;
		}
		Hashtable val = new Hashtable();
		using (IEnumerator<string> enumerator = (from keyObj in ((Dictionary<object, object>)(object)localPlayer.CustomProperties).Keys.ToList()
			select keyObj?.ToString() into key
			where key != null
			where !key.Equals("didTutorial")
			select key).GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				do
				{
					string current = enumerator.Current;
					val[(object)current] = null;
				}
				while (enumerator.MoveNext());
			}
		}
		if (((Dictionary<object, object>)(object)val).Count > 0)
		{
			localPlayer.SetCustomProperties(val, (Hashtable)null, (WebFlags)null);
		}
	}

	public static void VisualizeAntiReport()
	{
		if (!PhotonNetwork.InRoom)
		{
			if ((Object)(object)Safety_Object_01 != (Object)null)
			{
				Object.Destroy((Object)(object)Safety_Object_01);
				Safety_Object_01 = null;
			}
			return;
		}
		foreach (GorillaPlayerScoreboardLine allScoreboardLine in GorillaScoreboardTotalUpdater.allScoreboardLines)
		{
			if (allScoreboardLine.linePlayer != NetworkSystem.Instance.LocalPlayer || (Object)(object)allScoreboardLine.reportButton == (Object)null)
			{
				continue;
			}
			Transform transform = ((Component)allScoreboardLine.reportButton).gameObject.transform;
			if ((Object)(object)Safety_Object_01 == (Object)null)
			{
				Safety_Object_01 = GameObject.CreatePrimitive((PrimitiveType)0);
				Object.Destroy((Object)(object)Safety_Object_01.GetComponent<Collider>());
				Renderer val = Safety_Object_01.GetComponent<Renderer>();
				val.material.shader = Shader.Find("GUI/Text Shader");
				val.material.color = new Color(1f, 0f, 0f, 0.3f);
				Safety_Object_01.transform.position = transform.position;
				Safety_Object_01.transform.localScale = Vector3.one * Settings.CapturedVariables3760_Value_18;
			}
			else
			{
				Safety_Object_01.transform.position = transform.position;
				Safety_Object_01.transform.localScale = Vector3.one * Settings.CapturedVariables3760_Value_18;
			}
			break;
		}
	}

	public Safety()
	{
	}

	public static void ColorSpoof()
	{
		List<VRRig> list = new List<VRRig>();
		using (List<VRRig>.Enumerator enumerator = Safety_Items_02.GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				while (true)
				{
					VRRig current = enumerator.Current;
					if (!VRRigCache.ActiveRigs.Contains(current))
					{
						list.Add(current);
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
		}
		using (List<VRRig>.Enumerator enumerator2 = list.GetEnumerator())
		{
			if (enumerator2.MoveNext())
			{
				do
				{
					VRRig current2 = enumerator2.Current;
					Safety_Items_02.Remove(current2);
				}
				while (enumerator2.MoveNext());
			}
		}
		list.Clear();
		using IEnumerator<VRRig> enumerator3 = (from rig in VRRigCache.ActiveRigs
			where !rig.isLocal
			where !Safety_Items_02.Contains(rig)
			select rig).GetEnumerator();
		if (enumerator3.MoveNext())
		{
			do
			{
				VRRig current3 = enumerator3.Current;
				GorillaTagger.Instance.myVRRig.SendRPC("RPC_InitializeNoobMaterial", RigManager.GetPlayer(current3), new object[3]
				{
					Random.Range(0f, 1f),
					Random.Range(0f, 1f),
					Random.Range(0f, 1f)
				});
				Safety_Items_02.Add(current3);
			}
			while (enumerator3.MoveNext());
		}
	}

	private static void RestorePanicFeatures()
	{
		using (List<ButtonHandler.Button>.Enumerator enumerator = Safety_Items_01.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				while (true)
				{
					ButtonHandler.Button current = enumerator.Current;
					if (current == null)
					{
						break;
					}
					current.Enabled = true;
					Action onEnable = current.onEnable;
					if (onEnable != null)
					{
						onEnable();
						NXOUI.TrackModEnabled(current.buttonText);
						if (!enumerator.MoveNext())
						{
							goto EndBranch_00ae;
						}
					}
					else
					{
						NXOUI.TrackModEnabled(current.buttonText);
						if (!enumerator.MoveNext())
						{
							goto EndBranch_00ae;
						}
					}
				}
				continue;
				EndBranch_00ae:
				break;
			}
		}
		Safety_Items_01.Clear();
		Safety_State_06 = false;
		NotificationLib.ShowNotification(NotificationLib.NotificationType.Alert, "Panic Disabled, Features Restored.");
		Main.RebuildMenu();
	}

	public static void AntiReport(bool autoQueue, bool reconnect)
	{
		if (!PhotonNetwork.InRoom)
		{
			return;
		}
		using List<GorillaPlayerScoreboardLine>.Enumerator enumerator = GorillaScoreboardTotalUpdater.allScoreboardLines.GetEnumerator();
		while (enumerator.MoveNext())
		{
			while (true)
			{
				GorillaPlayerScoreboardLine current = enumerator.Current;
				if (current.linePlayer != NetworkSystem.Instance.LocalPlayer || (Object)(object)current.reportButton == (Object)null)
				{
					break;
				}
				Transform transform = ((Component)current.reportButton).gameObject.transform;
				float num = Settings.CapturedVariables3760_Value_18;
				using (IEnumerator<VRRig> enumerator2 = VRRigCache.ActiveRigs.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						while (true)
						{
							VRRig current2 = enumerator2.Current;
							if ((Object)(object)current2 == (Object)null || (Object)(object)current2 == (Object)(object)Variables.Variables_Reference_09.offlineVRRig)
							{
								break;
							}
							float num2 = Vector3.Distance(current2.rightHandTransform.position, transform.position);
							float num3 = Vector3.Distance(current2.leftHandTransform.position, transform.position);
							if (num2 < num || num3 < num)
							{
								NetworkSystem.Instance.ReturnToSinglePlayer();
								NotificationLib.ShowNotification(NotificationLib.NotificationType.Alert, "`" + current2.Creator.NickName + "` Attempted to Report You!");
								if (autoQueue)
								{
									CoroutineHelper.InvokeAfterDelay(2f, Room.JoinRandomPublic);
								}
								else if (reconnect)
								{
									CoroutineHelper.InvokeAfterDelay(2f, Room.Reconnect);
								}
								return;
							}
							if (!enumerator2.MoveNext())
							{
								goto EndBranch_026f;
							}
						}
						continue;
						EndBranch_026f:
						break;
					}
				}
				if (!enumerator.MoveNext())
				{
					return;
				}
			}
		}
	}

	public static void ResetPanic()
	{
		if (Safety_State_06)
		{
			RestorePanicFeatures();
		}
	}

	public static void SetRankedPlatformSpoofEnabled(bool enabled, string target = null)
	{
		MenuPatches.RankedPatch.RankedPatch_State_01 = enabled;
		MenuPatches.RankedPatch.RankedPatch_Text_02 = target;
	}

	public static void FakeLag()
	{
		bool flag = InputHandler.IsRightPrimaryPressed();
		if (!Safety_State_04 & flag)
		{
			Safety_State_01 = !Safety_State_01;
			Safety_State_04 = flag;
			if (Safety_State_01)
			{
				goto Branch_006f;
			}
		}
		else
		{
			Safety_State_04 = flag;
			if (Safety_State_01)
			{
				goto Branch_006f;
			}
		}
		Branch_00e8:
		if (!Safety_State_01 && (Object)(object)Variables.Variables_Reference_09.offlineVRRig != (Object)null)
		{
			((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = true;
		}
		return;
		Branch_006f:
		if (!(Time.time > Safety_Value_04))
		{
			goto Branch_00e8;
		}
		Safety_Value_04 = Time.time + Safety_Value_03;
		((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = !((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled;
		Safety_Value_03 = Random.Range(0.1f, 0.4f);
	}

	public static void ResetNetworkLimits()
	{
		if (!PhotonNetwork.InRoom)
		{
			return;
		}
		try
		{
			((MonkeAgent)MonkeAgent.instance).rpcErrorMax = int.MaxValue;
			((MonkeAgent)MonkeAgent.instance).rpcCallLimit = int.MaxValue;
			((MonkeAgent)MonkeAgent.instance).logErrorMax = int.MaxValue;
			ReflectionCompat.GetField<System.Collections.IDictionary>(MonkeAgent.instance, "userRPCCalls")?.Clear();
			PhotonNetwork.MaxResendsBeforeDisconnect = int.MaxValue;
			PhotonNetwork.QuickResends = int.MaxValue;
			PhotonNetwork.SendAllOutgoingCommands();
		}
		catch (Exception)
		{
		}
	}

	public static void AntiModerator()
	{
		if (!PhotonNetwork.InRoom)
		{
			return;
		}
		using IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			while (true)
			{
				VRRig current = enumerator.Current;
				if (current.isOfflineVRRig)
				{
					break;
				}
				if (ReflectionCompat.Invoke(current, "HasCosmetic", "LBAAK") is bool hasCosmetic && hasCosmetic)
				{
					NetworkSystem.Instance.ReturnToSinglePlayer();
					NotificationLib.ShowNotification(NotificationLib.NotificationType.Alert, "`" + current.Creator.NickName + "` Is A Moderator");
					CoroutineHelper.InvokeAfterDelay(1.5f, Room.JoinRandomPublic);
					return;
				}
				if (!enumerator.MoveNext())
				{
					return;
				}
			}
		}
	}

	public static void ChangeIdentity()
	{
		float value = Random.value;
		float value2 = Random.value;
		float value3 = Random.value;
		((Behaviour)GorillaTagger.Instance.offlineVRRig).enabled = false;
		PlayerPrefs.SetFloat("redValue", value);
		PlayerPrefs.SetFloat("greenValue", value3);
		PlayerPrefs.SetFloat("blueValue", value2);
		PlayerPrefs.Save();
		((Component)GorillaTagger.Instance.offlineVRRig).transform.position = ((Component)GorillaComputer.instance).gameObject.transform.position;
		ReflectionCompat.Invoke(GorillaComputer.instance, "ProcessColorState", (GorillaKeyboardBindings)13);
		((GorillaComputer)GorillaComputer.instance).UpdateScreen();
		ReflectionCompat.SetField(GorillaComputer.instance, "redValue", value);
		ReflectionCompat.SetField(GorillaComputer.instance, "greenValue", value2);
		ReflectionCompat.SetField(GorillaComputer.instance, "blueValue", value3);
		((GorillaComputer)GorillaComputer.instance).PressButton((GorillaKeyboardBindings)13);
		((GorillaComputer)GorillaComputer.instance).UpdateColor(value, value2, value3);
		ReflectionCompat.Invoke(GorillaComputer.instance, "NameScreen");
		if ((int)((GorillaComputer)GorillaComputer.instance).currentState != 2)
		{
			do
			{
				((GorillaComputer)GorillaComputer.instance).PressButton((GorillaKeyboardBindings)11);
			}
			while ((int)((GorillaComputer)GorillaComputer.instance).currentState != 2);
		}
		if (((GorillaComputer)GorillaComputer.instance).currentName != "")
		{
			do
			{
				int num = 0;
				if (num < ((GorillaComputer)GorillaComputer.instance).currentName.Count())
				{
					do
					{
						((GorillaComputer)GorillaComputer.instance).PressButton((GorillaKeyboardBindings)12);
						num++;
					}
					while (num < ((GorillaComputer)GorillaComputer.instance).currentName.Count());
				}
			}
			while (((GorillaComputer)GorillaComputer.instance).currentName != "");
		}
		((GorillaComputer)GorillaComputer.instance).PressButton((GorillaKeyboardBindings)23);
		((GorillaComputer)GorillaComputer.instance).PressButton((GorillaKeyboardBindings)31);
		((GorillaComputer)GorillaComputer.instance).PressButton((GorillaKeyboardBindings)34);
		((GorillaComputer)GorillaComputer.instance).PressButton((GorillaKeyboardBindings)28);
		((GorillaComputer)GorillaComputer.instance).PressButton((GorillaKeyboardBindings)28);
		((GorillaComputer)GorillaComputer.instance).PressButton((GorillaKeyboardBindings)17);
		int num2 = 0;
		if (num2 < 5)
		{
			do
			{
				((GorillaComputer)GorillaComputer.instance).PressButton((GorillaKeyboardBindings)Enum.Parse(typeof(GorillaKeyboardBindings), Safety_Text_01[Random.Range(0, Safety_Text_01.Count())]));
				num2++;
			}
			while (num2 < 5);
		}
		((GorillaComputer)GorillaComputer.instance).PressButton((GorillaKeyboardBindings)13);
		NetworkSystem.Instance.SetMyNickName($"GORILLA{Random.Range(1, 10)}{Random.Range(1, 10)}{Random.Range(1, 10)}{Random.Range(1, 10)}{Random.Range(1, 10)}");
		CustomMapsTerminal.RequestDriverNickNameRefresh();
		PlayerPrefs.SetString("playerName", ((GorillaComputer)GorillaComputer.instance).currentName);
		PlayerPrefs.Save();
		GorillaTagger.Instance.myVRRig.SendRPC("RPC_InitializeNoobMaterial", (RpcTarget)0, new object[3] { value, value2, value3 });
		((Behaviour)GorillaTagger.Instance.offlineVRRig).enabled = true;
	}
}
