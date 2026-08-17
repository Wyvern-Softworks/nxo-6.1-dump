using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using NXO.Menu;
using NXO.Utilities;
using UnityEngine;

namespace NXO.Mods.Categories;

public class Macros
{
	public struct MacroFrame
	{
		public Vector3 rigPosition;

		public Quaternion rigRotation;

		public Vector3 headPosition;

		public Quaternion headRotation;

		public Vector3 leftHandPosition;

		public Quaternion leftHandRotation;

		public Vector3 rightHandPosition;

		public Quaternion rightHandRotation;

		public float leftIndexT;

		public float leftMiddleT;

		public float leftThumbT;

		public float rightIndexT;

		public float rightMiddleT;

		public float rightThumbT;
	}

	[CompilerGenerated]
	private sealed class CapturedVariables280
	{
		public string macroName;

		internal void GenerateMacroButtons_Lambda2()
		{
			BuildMacroActions(macroName);
		}
	}

	[CompilerGenerated]
	private sealed class CapturedVariables290
	{
		public string macroName;

		internal void OpenMacroOptions_Lambda5()
		{
			DeleteMacro(macroName);
		}

		internal void OpenMacroOptions_Lambda0()
		{
			PlayMacro(macroName);
		}

		internal void OpenMacroOptions_Lambda2()
		{
			CapturedVariables310_State_03 = true;
			PlayMacro(macroName);
		}

		internal void OpenMacroOptions_Lambda4()
		{
			RenameMacro(macroName);
		}
	}

	[CompilerGenerated]
	private sealed class CapturedVariables310
	{
		public string macroName;

		internal void BeginRenameMacro_Lambda0(string newName)
		{
			string text = newName.Trim();
			newName = text;
			if (string.IsNullOrEmpty(newName) || newName == macroName)
			{
				return;
			}
			string text2 = Path.Combine(macroSavePath, macroName + ".macro");
			string text3 = Path.Combine(macroSavePath, newName + ".macro");
			if (!File.Exists(text2))
			{
				return;
			}
			if (File.Exists(text3))
			{
				NotificationLib.ShowNotification(NotificationLib.NotificationType.Error, "Macro Name Already Exists");
				return;
			}
			File.Move(text2, text3);
			if (CapturedVariables310_Lookup_01.ContainsKey(macroName))
			{
				CapturedVariables310_Lookup_01[newName] = CapturedVariables310_Lookup_01[macroName];
				CapturedVariables310_Lookup_01.Remove(macroName);
				CapturedVariables310_Text_01 = null;
				RebuildMacroButtons();
				NotificationLib.ShowNotification(NotificationLib.NotificationType.Saved, "Renamed " + macroName + " → " + newName);
				Main.RedrawMenu();
			}
			else
			{
				CapturedVariables310_Text_01 = null;
				RebuildMacroButtons();
				NotificationLib.ShowNotification(NotificationLib.NotificationType.Saved, "Renamed " + macroName + " → " + newName);
				Main.RedrawMenu();
			}
		}
	}

	public static bool CapturedVariables310_State_04 = false;

	public static bool CapturedVariables310_State_01 = false;

	private static bool CapturedVariables310_State_03 = false;

	private static int CapturedVariables310_Index_01 = 0;

	private static List<MacroFrame> CapturedVariables310_Items_01 = new List<MacroFrame>();

	private static List<MacroFrame> CapturedVariables310_Reference_01 = null;

	public static string CapturedVariables310_Text_01 = null;

	public static Dictionary<string, List<ButtonHandler.Button>> CapturedVariables310_Lookup_01 = new Dictionary<string, List<ButtonHandler.Button>>();

	private const int MAX_FRAMES = 10800;

	private static bool CapturedVariables310_State_02 = false;

	private static string macroSavePath
	{
		get
		{
			return Path.Combine(Variables.Variables_Text_01, "Macros");
		}
	}

	private static void RenameMacro(string macroName)
	{
		CapturedVariables310 LocalScope9 = new CapturedVariables310();
		LocalScope9.macroName = macroName;
		SearchAndKeyboard.OpenTextInput(LocalScope9.macroName, "Enter macro name...");
		SearchAndKeyboard.KeyCollider_Text_01 = delegate(string newName)
		{
			string text = newName.Trim();
			newName = text;
			if (!string.IsNullOrEmpty(newName) && !(newName == LocalScope9.macroName))
			{
				string text2 = Path.Combine(macroSavePath, LocalScope9.macroName + ".macro");
				string text3 = Path.Combine(macroSavePath, newName + ".macro");
				if (File.Exists(text2))
				{
					if (File.Exists(text3))
					{
						NotificationLib.ShowNotification(NotificationLib.NotificationType.Error, "Macro Name Already Exists");
					}
					else
					{
						File.Move(text2, text3);
						if (CapturedVariables310_Lookup_01.ContainsKey(LocalScope9.macroName))
						{
							CapturedVariables310_Lookup_01[newName] = CapturedVariables310_Lookup_01[LocalScope9.macroName];
							CapturedVariables310_Lookup_01.Remove(LocalScope9.macroName);
							CapturedVariables310_Text_01 = null;
							RebuildMacroButtons();
							NotificationLib.ShowNotification(NotificationLib.NotificationType.Saved, "Renamed " + LocalScope9.macroName + " → " + newName);
							Main.RedrawMenu();
						}
						else
						{
							CapturedVariables310_Text_01 = null;
							RebuildMacroButtons();
							NotificationLib.ShowNotification(NotificationLib.NotificationType.Saved, "Renamed " + LocalScope9.macroName + " → " + newName);
							Main.RedrawMenu();
						}
					}
				}
			}
		};
	}

	private static void DeleteMacro(string macroName)
	{
		string path = Path.Combine(macroSavePath, macroName + ".macro");
		if (File.Exists(path))
		{
			File.Delete(path);
			CapturedVariables310_Lookup_01.Remove(macroName);
			CapturedVariables310_Text_01 = null;
			RebuildMacroButtons();
			NotificationLib.ShowNotification(NotificationLib.NotificationType.Deleted, macroName);
			Main.RebuildMenu();
		}
		else
		{
			CapturedVariables310_Lookup_01.Remove(macroName);
			CapturedVariables310_Text_01 = null;
			RebuildMacroButtons();
			NotificationLib.ShowNotification(NotificationLib.NotificationType.Deleted, macroName);
			Main.RebuildMenu();
		}
	}

	private static void WriteVector3(BinaryWriter w, Vector3 v)
	{
		w.Write(v.x);
		w.Write(v.y);
		w.Write(v.z);
	}

	public static void StopRecordingMacro()
	{
		if (CapturedVariables310_State_01)
		{
			StopMacroPlayback();
			if (!CapturedVariables310_State_04)
			{
				return;
			}
		}
		else if (!CapturedVariables310_State_04)
		{
			return;
		}
		CapturedVariables310_State_04 = false;
		CapturedVariables310_Items_01.Clear();
	}

	public static void PlayMacro(string macroName)
	{
		List<MacroFrame> list = LoadMacro(macroName);
		if (list == null || list.Count == 0)
		{
			NotificationLib.ShowNotification(NotificationLib.NotificationType.Error, "Failed To Load Macro");
			return;
		}
		CapturedVariables310_Reference_01 = list;
		CapturedVariables310_Index_01 = 0;
		CapturedVariables310_State_01 = true;
		NotificationLib.ShowNotification(NotificationLib.NotificationType.Loaded, macroName);
	}

	private static void SaveMacroFrames(string name, List<MacroFrame> frames)
	{
		BinaryWriter binaryWriter;
		if (!Directory.Exists(macroSavePath))
		{
			Directory.CreateDirectory(macroSavePath);
			string path = Path.Combine(macroSavePath, name + ".macro");
			binaryWriter = new BinaryWriter(File.Open(path, FileMode.Create));
		}
		else
		{
			string path = Path.Combine(macroSavePath, name + ".macro");
			binaryWriter = new BinaryWriter(File.Open(path, FileMode.Create));
		}
		try
		{
			binaryWriter.Write(frames.Count);
			using List<MacroFrame>.Enumerator enumerator = frames.GetEnumerator();
			if (enumerator.MoveNext())
			{
				do
				{
					MacroFrame current = enumerator.Current;
					WriteVector3(binaryWriter, current.rigPosition);
					WriteQuaternion(binaryWriter, current.rigRotation);
					WriteVector3(binaryWriter, current.headPosition);
					WriteQuaternion(binaryWriter, current.headRotation);
					WriteVector3(binaryWriter, current.leftHandPosition);
					WriteQuaternion(binaryWriter, current.leftHandRotation);
					WriteVector3(binaryWriter, current.rightHandPosition);
					WriteQuaternion(binaryWriter, current.rightHandRotation);
					binaryWriter.Write(current.leftIndexT);
					binaryWriter.Write(current.leftMiddleT);
					binaryWriter.Write(current.leftThumbT);
					binaryWriter.Write(current.rightIndexT);
					binaryWriter.Write(current.rightMiddleT);
					binaryWriter.Write(current.rightThumbT);
				}
				while (enumerator.MoveNext());
			}
		}
		finally
		{
			((IDisposable)binaryWriter)?.Dispose();
		}
	}

	public static void RecordMacro()
	{
		if (CapturedVariables310_State_01)
		{
			return;
		}
		bool isPressed = InputHandler.IsRightPrimaryPressed();
		bool wasPressed = CapturedVariables310_State_02;
		CapturedVariables310_State_02 = isPressed;
		if (isPressed && !wasPressed)
		{
			if (CapturedVariables310_State_04)
			{
				FinishMacroRecording();
			}
			else
			{
				BeginMacroRecording();
			}
		}

		if (CapturedVariables310_State_04)
		{
			CaptureMacroFrame();
		}
	}

	private static Quaternion ReadQuaternion(BinaryReader r)
	{
		return new Quaternion(r.ReadSingle(), r.ReadSingle(), r.ReadSingle(), r.ReadSingle());
	}

	private static void WriteQuaternion(BinaryWriter w, Quaternion q)
	{
		w.Write(q.x);
		w.Write(q.y);
		w.Write(q.z);
		w.Write(q.w);
	}

	public static void UpdateMacroPlayback()
	{
		if (CapturedVariables310_State_01)
		{
			ApplyNextMacroFrame();
		}
	}

	private static void ApplyNextMacroFrame()
	{
		if (CapturedVariables310_Reference_01 == null || CapturedVariables310_Index_01 >= CapturedVariables310_Reference_01.Count)
		{
			if (CapturedVariables310_State_03 && CapturedVariables310_Reference_01 != null)
			{
				CapturedVariables310_Index_01 = 0;
			}
			else
			{
				StopMacroPlayback();
			}
			return;
		}
		VRRig offlineVRRig = Variables.Variables_Reference_09.offlineVRRig;
		((Behaviour)offlineVRRig).enabled = false;
		MacroFrame macroFrame = CapturedVariables310_Reference_01[CapturedVariables310_Index_01];
		((Component)offlineVRRig).transform.SetPositionAndRotation(macroFrame.rigPosition, macroFrame.rigRotation);
		offlineVRRig.head.rigTarget.SetPositionAndRotation(macroFrame.headPosition, macroFrame.headRotation);
		offlineVRRig.leftHand.rigTarget.SetPositionAndRotation(macroFrame.leftHandPosition, macroFrame.leftHandRotation);
		offlineVRRig.rightHand.rigTarget.SetPositionAndRotation(macroFrame.rightHandPosition, macroFrame.rightHandRotation);
		if (offlineVRRig.leftIndex != null)
		{
			((VRMap)offlineVRRig.leftIndex).calcT = macroFrame.leftIndexT;
			((VRMap)offlineVRRig.leftIndex).LerpFinger(1f, false);
			if (offlineVRRig.leftMiddle != null)
			{
				goto Branch_01b9;
			}
		}
		else if (offlineVRRig.leftMiddle != null)
		{
			goto Branch_01b9;
		}
		if (offlineVRRig.leftThumb == null)
		{
			goto Branch_0254;
		}
		goto Branch_0215;
		Branch_02cd:
		((VRMap)offlineVRRig.rightMiddle).calcT = macroFrame.rightMiddleT;
		((VRMap)offlineVRRig.rightMiddle).LerpFinger(1f, false);
		if (offlineVRRig.rightThumb == null)
		{
			goto Branch_0361;
		}
		goto Branch_0329;
		Branch_030c:
		if (offlineVRRig.rightThumb == null)
		{
			goto Branch_0361;
		}
		goto Branch_0329;
		Branch_02b0:
		if (offlineVRRig.rightMiddle == null)
		{
			goto Branch_030c;
		}
		goto Branch_02cd;
		Branch_0361:
		CapturedVariables310_Index_01++;
		return;
		Branch_01b9:
		((VRMap)offlineVRRig.leftMiddle).calcT = macroFrame.leftMiddleT;
		((VRMap)offlineVRRig.leftMiddle).LerpFinger(1f, false);
		if (offlineVRRig.leftThumb == null)
		{
			goto Branch_0254;
		}
		goto Branch_0215;
		Branch_0329:
		((VRMap)offlineVRRig.rightThumb).calcT = macroFrame.rightThumbT;
		((VRMap)offlineVRRig.rightThumb).LerpFinger(1f, false);
		CapturedVariables310_Index_01++;
		return;
		Branch_0215:
		((VRMap)offlineVRRig.leftThumb).calcT = macroFrame.leftThumbT;
		((VRMap)offlineVRRig.leftThumb).LerpFinger(1f, false);
		if (offlineVRRig.rightIndex == null)
		{
			goto Branch_02b0;
		}
		goto Branch_0271;
		Branch_0254:
		if (offlineVRRig.rightIndex == null)
		{
			goto Branch_02b0;
		}
		Branch_0271:
		((VRMap)offlineVRRig.rightIndex).calcT = macroFrame.rightIndexT;
		((VRMap)offlineVRRig.rightIndex).LerpFinger(1f, false);
		if (offlineVRRig.rightMiddle == null)
		{
			goto Branch_030c;
		}
		goto Branch_02cd;
	}

	private static void BeginMacroRecording()
	{
		CapturedVariables310_Items_01.Clear();
		CapturedVariables310_State_04 = true;
		NotificationLib.ShowNotification(NotificationLib.NotificationType.Enabled, "Macro Recording...");
	}

	private static void FinishMacroRecording()
	{
		CapturedVariables310_State_04 = false;
		if (CapturedVariables310_Items_01.Count == 0)
		{
			NotificationLib.ShowNotification(NotificationLib.NotificationType.Error, "No Frames Recorded");
			return;
		}
		string text = $"Macro_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}";
		SaveMacroFrames(text, CapturedVariables310_Items_01);
		RebuildMacroButtons();
		NotificationLib.ShowNotification(NotificationLib.NotificationType.Saved, $"{text} ({CapturedVariables310_Items_01.Count} frames)");
		CapturedVariables310_Items_01.Clear();
		Main.RebuildMenu();
	}

	private static void BuildMacroActions(string macroName)
	{
		CapturedVariables290 LocalScope7 = new CapturedVariables290();
		LocalScope7.macroName = macroName;
		CapturedVariables310_Text_01 = LocalScope7.macroName;
		CapturedVariables310_Lookup_01[LocalScope7.macroName] = new List<ButtonHandler.Button>
		{
			new ButtonHandler.Button("Return", Category.Recorded_Macros, isToggle: false, isActive: false, delegate
			{
				CapturedVariables310_Text_01 = null;
				Main.RebuildMenu();
			})
			{
				isCategory = true
			},
			new ButtonHandler.Button("Play Macro", Category.Recorded_Macros, isToggle: true, isActive: false, delegate
			{
				PlayMacro(LocalScope7.macroName);
			}, delegate
			{
				StopMacroPlayback();
			}),
			new ButtonHandler.Button("Loop Macro", Category.Recorded_Macros, isToggle: true, isActive: false, delegate
			{
				CapturedVariables310_State_03 = true;
				PlayMacro(LocalScope7.macroName);
			}, delegate
			{
				CapturedVariables310_State_03 = false;
				if (CapturedVariables310_State_01)
				{
					StopMacroPlayback();
				}
			}),
			new ButtonHandler.Button("Rename Macro", Category.Recorded_Macros, isToggle: false, isActive: false, delegate
			{
				RenameMacro(LocalScope7.macroName);
			}),
			new ButtonHandler.Button("Delete Macro", Category.Recorded_Macros, isToggle: false, isActive: false, delegate
			{
				DeleteMacro(LocalScope7.macroName);
			})
		};
		Main.CapturedVariables1950_Reference_09 = Category.Home;
		Main.CapturedVariables1950_Index_01 = -1;
		Main.RedrawMenu();
	}

	private static Vector3 ReadVector3(BinaryReader r)
	{
		return new Vector3(r.ReadSingle(), r.ReadSingle(), r.ReadSingle());
	}

	private static void StopMacroPlayback()
	{
		CapturedVariables310_State_01 = false;
		CapturedVariables310_State_03 = false;
		if (CapturedVariables310_Reference_01 != null && CapturedVariables310_Reference_01.Count > 0 && CapturedVariables310_Index_01 > 0)
		{
			Movement.TeleportToPosition(CapturedVariables310_Reference_01[CapturedVariables310_Index_01 - 1].rigPosition);
			CapturedVariables310_Reference_01 = null;
			CapturedVariables310_Index_01 = 0;
			if ((Object)(object)Variables.Variables_Reference_09?.offlineVRRig != (Object)null)
			{
				goto Branch_00fc;
			}
		}
		else
		{
			CapturedVariables310_Reference_01 = null;
			CapturedVariables310_Index_01 = 0;
			if ((Object)(object)Variables.Variables_Reference_09?.offlineVRRig != (Object)null)
			{
				goto Branch_00fc;
			}
		}
		NotificationLib.ShowNotification(NotificationLib.NotificationType.Disabled, "Macro Stopped");
		return;
		Branch_00fc:
		((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = true;
		NotificationLib.ShowNotification(NotificationLib.NotificationType.Disabled, "Macro Stopped");
	}

	public static void RebuildMacroButtons()
	{
		List<ButtonHandler.Button> list = ModButtons.buttons.ToList();
		list.RemoveAll((ButtonHandler.Button b) => b.Page == Category.Recorded_Macros);
		list.Add(new ButtonHandler.Button("Return", Category.Recorded_Macros, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.NavigateToCategory(Category.Macros);
		})
		{
			isCategory = true
		});
		if (Directory.Exists(macroSavePath))
		{
			string[] files = Directory.GetFiles(macroSavePath, "*.macro");
			foreach (string path in files)
			{
				CapturedVariables280 LocalScope3 = new CapturedVariables280();
				LocalScope3.macroName = Path.GetFileNameWithoutExtension(path);
				list.Add(new ButtonHandler.Button(LocalScope3.macroName, Category.Recorded_Macros, isToggle: false, isActive: false, delegate
				{
					BuildMacroActions(LocalScope3.macroName);
				}));
			}
			ModButtons.buttons = list.ToArray();
		}
		else
		{
			ModButtons.buttons = list.ToArray();
		}
	}

	private static void CaptureMacroFrame()
	{
		if (CapturedVariables310_Items_01.Count >= 10800)
		{
			FinishMacroRecording();
			return;
		}
		VRRig offlineVRRig = Variables.Variables_Reference_09.offlineVRRig;
		if (!((Object)(object)offlineVRRig == (Object)null))
		{
			CapturedVariables310_Items_01.Add(new MacroFrame
			{
				rigPosition = ((Component)offlineVRRig).transform.position,
				rigRotation = ((Component)offlineVRRig).transform.rotation,
				headPosition = offlineVRRig.head.rigTarget.position,
				headRotation = offlineVRRig.head.rigTarget.rotation,
				leftHandPosition = offlineVRRig.leftHand.rigTarget.position,
				leftHandRotation = offlineVRRig.leftHand.rigTarget.rotation,
				rightHandPosition = offlineVRRig.rightHand.rigTarget.position,
				rightHandRotation = offlineVRRig.rightHand.rigTarget.rotation,
				leftIndexT = ((offlineVRRig.leftIndex != null) ? ((VRMap)offlineVRRig.leftIndex).calcT : 0f),
				leftMiddleT = ((offlineVRRig.leftMiddle != null) ? ((VRMap)offlineVRRig.leftMiddle).calcT : 0f),
				leftThumbT = ((offlineVRRig.leftThumb != null) ? ((VRMap)offlineVRRig.leftThumb).calcT : 0f),
				rightIndexT = ((offlineVRRig.rightIndex != null) ? ((VRMap)offlineVRRig.rightIndex).calcT : 0f),
				rightMiddleT = ((offlineVRRig.rightMiddle != null) ? ((VRMap)offlineVRRig.rightMiddle).calcT : 0f),
				rightThumbT = ((offlineVRRig.rightThumb != null) ? ((VRMap)offlineVRRig.rightThumb).calcT : 0f)
			});
		}
	}

	public Macros()
	{
	}

	private static List<MacroFrame> LoadMacro(string name)
	{
		string path = Path.Combine(macroSavePath, name + ".macro");
		if (!File.Exists(path))
		{
			return null;
		}
		List<MacroFrame> list = new List<MacroFrame>();
		using (BinaryReader binaryReader = new BinaryReader(File.Open(path, FileMode.Open)))
		{
			int num = binaryReader.ReadInt32();
			int num2 = 0;
			if (num2 < num)
			{
				do
				{
					list.Add(new MacroFrame
					{
						rigPosition = ReadVector3(binaryReader),
						rigRotation = ReadQuaternion(binaryReader),
						headPosition = ReadVector3(binaryReader),
						headRotation = ReadQuaternion(binaryReader),
						leftHandPosition = ReadVector3(binaryReader),
						leftHandRotation = ReadQuaternion(binaryReader),
						rightHandPosition = ReadVector3(binaryReader),
						rightHandRotation = ReadQuaternion(binaryReader),
						leftIndexT = binaryReader.ReadSingle(),
						leftMiddleT = binaryReader.ReadSingle(),
						leftThumbT = binaryReader.ReadSingle(),
						rightIndexT = binaryReader.ReadSingle(),
						rightMiddleT = binaryReader.ReadSingle(),
						rightThumbT = binaryReader.ReadSingle()
					});
					num2++;
				}
				while (num2 < num);
			}
		}
		return list;
	}
}
