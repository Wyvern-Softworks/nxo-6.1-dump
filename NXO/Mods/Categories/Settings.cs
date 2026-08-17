using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using NXO.Menu;
using NXO.Utilities;
using UnityEngine;

namespace NXO.Mods.Categories;

public class Settings
{
	public enum ColorMode
	{
		Solid,
		Lerp,
		Rainbow,
		Strobe,
		Pinwheel,
		Gradient
	}

	public enum GradientDirection
	{
		Horizontal,
		Vertical,
		Diagonal
	}

	public enum ColorElement
	{
		Pinwheel,
		Outline,
		Background,
		Button,
		Title,
		AccentStrip,
		Boards
	}

	public enum AccentStripType
	{
		Off,
		Top,
		Both
	}

	public delegate bool ControllerInput();

	[CompilerGenerated]
	private sealed class CapturedVariables3760
	{
		public string value;

		internal bool ApplyKeyValue_Lambda0((string name, string zone, string pos) m)
		{
			return m.name == value;
		}
	}

	public static bool CapturedVariables3760_State_06;

	public static ButtonHandler.Button CapturedVariables3760_Button_40;

	public static ButtonHandler.Button CapturedVariables3760_Button_55;

	private static readonly (float v, string d)[] Recovered_Reference_20;

	private static int CapturedVariables3760_Index_04;

	public static float CapturedVariables3760_Value_05;

	public static string CapturedVariables3760_Text_20;

	private static readonly (float v, string d)[] Recovered_Reference_12;

	private static int CapturedVariables3760_Index_38;

	public static float CapturedVariables3760_Value_21;

	public static string CapturedVariables3760_Text_55;

	public static readonly string[] CapturedVariables3760_Text_02;

	public static ColorMode CapturedVariables3760_Color_10;

	public static ColorMode CapturedVariables3760_Color_18;

	public static ColorMode CapturedVariables3760_Color_05;

	public static ColorMode CapturedVariables3760_Color_15;

	public static ColorMode CapturedVariables3760_Color_04;

	public static ColorMode CapturedVariables3760_Color_12;

	public static ButtonHandler.Button CapturedVariables3760_Button_13;

	public static ButtonHandler.Button CapturedVariables3760_Button_56;

	public static ButtonHandler.Button CapturedVariables3760_Button_03;

	public static ButtonHandler.Button CapturedVariables3760_Button_47;

	public static ButtonHandler.Button CapturedVariables3760_Button_09;

	public static GradientDirection CapturedVariables3760_Reference_02;

	private static readonly string[] CapturedVariables3760_Text_34;

	private static int CapturedVariables3760_Index_12;

	public static string CapturedVariables3760_Text_23;

	public static ButtonHandler.Button CapturedVariables3760_Button_28;

	public static ColorElement CapturedVariables3760_Color_24;

	private static readonly Color32[] CapturedVariables3760_Color_01;

	private static readonly string[] CapturedVariables3760_Text_49;

	public static ButtonHandler.Button CapturedVariables3760_Button_02;

	private static int CapturedVariables3760_Index_56;

	public static Color32 CapturedVariables3760_Color_25;

	public static string CapturedVariables3760_Text_22;

	public static ButtonHandler.Button CapturedVariables3760_Button_36;

	private static int CapturedVariables3760_Index_52;

	public static Color32 CapturedVariables3760_Color_11;

	public static string CapturedVariables3760_Text_29;

	public static ButtonHandler.Button CapturedVariables3760_Button_52;

	private static readonly (float speed, string desc)[] Recovered_Reference_22;

	private static int CapturedVariables3760_Index_22;

	public static string CapturedVariables3760_Text_63;

	public static ButtonHandler.Button CapturedVariables3760_Button_20;

	private static int CapturedVariables3760_Index_09;

	public static float CapturedVariables3760_Value_04;

	public static string CapturedVariables3760_Text_19;

	public static ButtonHandler.Button CapturedVariables3760_Button_33;

	public static ButtonHandler.Button CapturedVariables3760_Button_10;

	public static ButtonHandler.Button CapturedVariables3760_Button_26;

	private static int CapturedVariables3760_Index_15;

	private static int CapturedVariables3760_Index_01;

	public static Color32 CapturedVariables3760_Color_14;

	public static Color32 CapturedVariables3760_Color_21;

	public static string CapturedVariables3760_Text_60;

	public static string CapturedVariables3760_Text_41;

	private static readonly (float speed, string desc)[] Recovered_Reference_09;

	public static ButtonHandler.Button CapturedVariables3760_Button_58;

	private static int CapturedVariables3760_Index_24;

	public static float CapturedVariables3760_Value_03;

	public static string CapturedVariables3760_Text_15;

	public static ButtonHandler.Button CapturedVariables3760_Button_08;

	private static int CapturedVariables3760_Index_20;

	public static float CapturedVariables3760_Value_12;

	public static string CapturedVariables3760_Text_28;

	public static ButtonHandler.Button CapturedVariables3760_Button_15;

	private static int CapturedVariables3760_Index_48;

	public static float CapturedVariables3760_Value_15;

	public static string CapturedVariables3760_Text_40;

	public static ButtonHandler.Button CapturedVariables3760_Button_46;

	private static int CapturedVariables3760_Index_02;

	public static float CapturedVariables3760_Value_13;

	public static string CapturedVariables3760_Text_51;

	public static ButtonHandler.Button CapturedVariables3760_Button_12;

	private static int CapturedVariables3760_Index_21;

	public static float CapturedVariables3760_Value_10;

	public static string CapturedVariables3760_Text_09;

	public static ButtonHandler.Button CapturedVariables3760_Button_27;

	public static ButtonHandler.Button CapturedVariables3760_Button_04;

	private static int CapturedVariables3760_Index_07;

	private static int CapturedVariables3760_Index_19;

	public static Color32 CapturedVariables3760_Color_28;

	public static Color32 CapturedVariables3760_Color_09;

	public static string CapturedVariables3760_Text_03;

	public static string CapturedVariables3760_Text_16;

	public static int CapturedVariables3760_Index_32;

	public static ButtonHandler.Button CapturedVariables3760_Button_16;

	public static readonly string[] CapturedVariables3760_Text_07;

	public static ButtonHandler.Button CapturedVariables3760_Button_39;

	private static readonly (float scale, string desc)[] Recovered_Reference_23;

	private static int CapturedVariables3760_Index_53;

	public static string CapturedVariables3760_Text_30;

	public static ButtonHandler.Button CapturedVariables3760_Button_19;

	private static readonly (float value, string desc)[] Recovered_Reference_27;

	private static int CapturedVariables3760_Index_54;

	public static float CapturedVariables3760_Value_01;

	public static string CapturedVariables3760_Text_50;

	public static ButtonHandler.Button CapturedVariables3760_Button_24;

	private static int CapturedVariables3760_Index_08;

	public static float CapturedVariables3760_Value_17;

	public static string CapturedVariables3760_Text_18;

	public static ColorMode CapturedVariables3760_Color_03;

	public static ButtonHandler.Button CapturedVariables3760_Button_53;

	public static ButtonHandler.Button CapturedVariables3760_Button_43;

	public static ButtonHandler.Button CapturedVariables3760_Button_34;

	private static int CapturedVariables3760_Index_41;

	private static int CapturedVariables3760_Index_36;

	public static Color32 CapturedVariables3760_Color_22;

	public static Color32 CapturedVariables3760_Color_27;

	public static string CapturedVariables3760_Text_01;

	public static string CapturedVariables3760_Text_14;

	public static ButtonHandler.Button CapturedVariables3760_Button_01;

	public static AccentStripType CapturedVariables3760_Reference_03;

	private static readonly string[] CapturedVariables3760_Text_08;

	private static int CapturedVariables3760_Index_11;

	public static string CapturedVariables3760_Text_24;

	public static ButtonHandler.Button CapturedVariables3760_Button_11;

	public static ButtonHandler.Button CapturedVariables3760_Button_59;

	private static int CapturedVariables3760_Index_17;

	private static int CapturedVariables3760_Index_33;

	public static Color32 CapturedVariables3760_Color_19;

	public static Color32 CapturedVariables3760_Color_02;

	public static string CapturedVariables3760_Text_61;

	public static string CapturedVariables3760_Text_59;

	public static ButtonHandler.Button CapturedVariables3760_Button_14;

	public static ButtonHandler.Button CapturedVariables3760_Button_50;

	private static int CapturedVariables3760_Index_45;

	private static int CapturedVariables3760_Index_06;

	public static Color32 CapturedVariables3760_Color_26;

	public static Color32 CapturedVariables3760_Color_23;

	public static string CapturedVariables3760_Text_32;

	public static string CapturedVariables3760_Text_11;

	public static ButtonHandler.Button CapturedVariables3760_Button_18;

	public static ButtonHandler.Button CapturedVariables3760_Button_61;

	private static int CapturedVariables3760_Index_03;

	private static int CapturedVariables3760_Index_30;

	public static Color32 CapturedVariables3760_Color_08;

	public static Color32 CapturedVariables3760_Color_29;

	public static string CapturedVariables3760_Text_27;

	public static string CapturedVariables3760_Text_45;

	public static ButtonHandler.Button CapturedVariables3760_Button_54;

	public static ButtonHandler.Button CapturedVariables3760_Button_31;

	private static int CapturedVariables3760_Index_46;

	private static int CapturedVariables3760_Index_13;

	public static Color32 CapturedVariables3760_Color_17;

	public static Color32 CapturedVariables3760_Color_16;

	public static string CapturedVariables3760_Text_48;

	public static string CapturedVariables3760_Text_36;

	public static bool CapturedVariables3760_State_03;

	public static ButtonHandler.Button CapturedVariables3760_Button_35;

	public static ButtonHandler.Button CapturedVariables3760_Button_62;

	private static readonly string[] CapturedVariables3760_Text_25;

	public static int CapturedVariables3760_Index_50;

	public static string CapturedVariables3760_Text_57;

	public static ButtonHandler.Button CapturedVariables3760_Button_63;

	private static readonly (float speed, string desc)[] Recovered_Reference_11;

	private static int CapturedVariables3760_Index_42;

	public static float CapturedVariables3760_Value_06;

	public static float CapturedVariables3760_Value_16;

	public static string CapturedVariables3760_Text_10;

	public static ButtonHandler.Button CapturedVariables3760_Button_48;

	private static readonly (float speed, string desc)[] Recovered_Reference_29;

	private static int CapturedVariables3760_Index_23;

	public static float CapturedVariables3760_Value_14;

	public static string CapturedVariables3760_Text_21;

	public static ButtonHandler.Button CapturedVariables3760_Button_06;

	private static readonly (float value, string desc)[] Recovered_Reference_21;

	private static int CapturedVariables3760_Index_25;

	public static float CapturedVariables3760_Value_11;

	public static string CapturedVariables3760_Text_43;

	public static ButtonHandler.Button CapturedVariables3760_Button_60;

	private static readonly (float strength, string desc)[] Recovered_Reference_31;

	private static int CapturedVariables3760_Index_47;

	public static float CapturedVariables3760_Value_20;

	public static string CapturedVariables3760_Text_39;

	public static ButtonHandler.Button CapturedVariables3760_Button_17;

	private static int CapturedVariables3760_Index_05;

	private static readonly string[] CapturedVariables3760_Text_53;

	public static string CapturedVariables3760_Text_13;

	public static ButtonHandler.Button CapturedVariables3760_Button_21;

	private static readonly (Vector3 length, string desc)[] Recovered_Reference_05;

	private static int CapturedVariables3760_Index_55;

	public static Vector3 CapturedVariables3760_Position_01;

	public static string CapturedVariables3760_Text_05;

	public static ButtonHandler.Button CapturedVariables3760_Button_41;

	private static readonly string[] CapturedVariables3760_Text_26;

	private static int CapturedVariables3760_Index_35;

	public static string CapturedVariables3760_Text_35;

	public static int CapturedVariables3760_Index_31;

	public static ControllerInput CapturedVariables3760_Reference_01;

	public static string CapturedVariables3760_Text_33;

	public static ControllerInput[] CapturedVariables3760_Values_01;

	public static ButtonHandler.Button CapturedVariables3760_Button_05;

	public static ButtonHandler.Button CapturedVariables3760_Button_44;

	private static readonly (float radius, string desc)[] Recovered_Reference_24;

	private static int CapturedVariables3760_Index_28;

	public static float CapturedVariables3760_Value_18;

	public static string CapturedVariables3760_Text_47;

	public static ButtonHandler.Button CapturedVariables3760_Button_51;

	public static ButtonHandler.Button CapturedVariables3760_Button_29;

	private static readonly (float speed, string desc)[] Recovered_Reference_26;

	private static readonly (int scale, string desc)[] Recovered_Reference_15;

	private static int CapturedVariables3760_Index_39;

	private static int CapturedVariables3760_Index_44;

	public static float CapturedVariables3760_Value_02;

	public static int CapturedVariables3760_Index_29;

	public static string CapturedVariables3760_Text_12;

	public static string CapturedVariables3760_Text_58;

	public static ButtonHandler.Button CapturedVariables3760_Button_64;

	public static ButtonHandler.Button CapturedVariables3760_Button_49;

	private static readonly (int packets, float cooldown, string desc)[] Recovered_Reference_17;

	private static int CapturedVariables3760_Index_51;

	public static int CapturedVariables3760_Index_26;

	public static float CapturedVariables3760_Value_19;

	public static string CapturedVariables3760_Text_52;

	public static ButtonHandler.Button CapturedVariables3760_Button_25;

	private static readonly string[] CapturedVariables3760_Text_37;

	public static int CapturedVariables3760_Index_18;

	public static string CapturedVariables3760_Text_44;

	public static ButtonHandler.Button CapturedVariables3760_Button_37;

	public static int CapturedVariables3760_Index_43;

	public static int CapturedVariables3760_Index_14;

	public static ButtonHandler.Button CapturedVariables3760_Button_45;

	public static ButtonHandler.Button CapturedVariables3760_Button_57;

	private static readonly string[] CapturedVariables3760_Text_46;

	private static int CapturedVariables3760_Index_34;

	public static string CapturedVariables3760_Text_42;

	public static ButtonHandler.Button CapturedVariables3760_Button_32;

	public static ButtonHandler.Button CapturedVariables3760_Button_23;

	public static ButtonHandler.Button CapturedVariables3760_Button_22;

	public static ButtonHandler.Button CapturedVariables3760_Button_38;

	private static int CapturedVariables3760_Index_49;

	private static int CapturedVariables3760_Index_40;

	private static int CapturedVariables3760_Index_16;

	private static int CapturedVariables3760_Index_37;

	public static Color32 CapturedVariables3760_Color_20;

	public static Color32 CapturedVariables3760_Color_06;

	public static Color32 CapturedVariables3760_Color_13;

	public static Color32 CapturedVariables3760_Color_07;

	public static bool CapturedVariables3760_State_05;

	public static bool CapturedVariables3760_State_02;

	public static bool CapturedVariables3760_State_04;

	public static bool CapturedVariables3760_State_01;

	public static string CapturedVariables3760_Text_17;

	public static string CapturedVariables3760_Text_06;

	public static string CapturedVariables3760_Text_38;

	public static string CapturedVariables3760_Text_54;

	public static ButtonHandler.Button CapturedVariables3760_Button_30;

	public static ButtonHandler.Button CapturedVariables3760_Button_07;

	private static readonly (float v, string d)[] Recovered_Reference_30;

	private static int CapturedVariables3760_Index_27;

	public static float CapturedVariables3760_Value_08;

	public static string CapturedVariables3760_Text_04;

	private static readonly (float v, string d)[] Recovered_Reference_19;

	private static int CapturedVariables3760_Index_10;

	public static float CapturedVariables3760_Value_09;

	public static string CapturedVariables3760_Text_31;

	public static ButtonHandler.Button CapturedVariables3760_Button_42;

	private static readonly (float value, string desc)[] Recovered_Reference_28;

	private static int CapturedVariables3760_Index_57;

	public static float CapturedVariables3760_Value_07;

	public static string CapturedVariables3760_Text_62;

	private static string[] CapturedVariables3760_Text_56;

	public static int CurrentFontIndex
	{
		get;
		private set;
	}

	public static string CurrentFontDescription
	{
		get
		{
			if (Main.CapturedVariables1950_Reference_02.Count <= 0)
			{
				return "Arial";
			}
			return Main.CapturedVariables1950_Reference_02[CurrentFontIndex].Description;
		}
	}

	public static string ClickSoundDescription
	{
		get
		{
			return ButtonHandler.CapturedVariables570_Items_03[CapturedVariables3760_Index_43].Description;
		}
	}

	public static string MapDescription
	{
		get
		{
			return Movement.Recovered_Reference_06[CapturedVariables3760_Index_14].name;
		}
	}

	public static string SettingsFilePath
	{
		get
		{
			return Path.Combine(Variables.Variables_Text_01, "Settings.txt");
		}
	}

	public static void CycleAccentColor2(bool forward)
	{
		CyclePaletteColor(ref CapturedVariables3760_Index_01, ref CapturedVariables3760_Color_21, ref CapturedVariables3760_Text_41, CapturedVariables3760_Button_26, "Accent Color 2", forward);
	}

	public static void CycleTPTo(bool forward)
	{
		if (!forward)
		{
			CapturedVariables3760_Index_14 = (CapturedVariables3760_Index_14 - 1 + Movement.Recovered_Reference_06.Length) % Movement.Recovered_Reference_06.Length;
			CapturedVariables3760_Button_45?.SetText("TP To : " + Movement.Recovered_Reference_06[CapturedVariables3760_Index_14].name);
		}
		else
		{
			CapturedVariables3760_Index_14 = (CapturedVariables3760_Index_14 + 1) % Movement.Recovered_Reference_06.Length;
			CapturedVariables3760_Button_45?.SetText("TP To : " + Movement.Recovered_Reference_06[CapturedVariables3760_Index_14].name);
		}
	}

	public static void CycleOutline(bool forward)
	{
		if (!forward)
		{
			CapturedVariables3760_Index_32 = (CapturedVariables3760_Index_32 - 1 + CapturedVariables3760_Text_07.Length) % CapturedVariables3760_Text_07.Length;
			ButtonHandler.Button i6QFE3JE = CapturedVariables3760_Button_16;
			if (i6QFE3JE != null)
			{
				i6QFE3JE.SetText("Outline : " + CapturedVariables3760_Text_07[CapturedVariables3760_Index_32]);
				Main.RebuildMenu();
			}
		}
		else
		{
			CapturedVariables3760_Index_32 = (CapturedVariables3760_Index_32 + 1) % CapturedVariables3760_Text_07.Length;
			ButtonHandler.Button i6QFE3JE2 = CapturedVariables3760_Button_16;
			if (i6QFE3JE2 != null)
			{
				i6QFE3JE2.SetText("Outline : " + CapturedVariables3760_Text_07[CapturedVariables3760_Index_32]);
				Main.RebuildMenu();
			}
		}
		Main.RebuildMenu();
	}

	public static void SetUseTriggersForPlatformsEnabled(bool setActive)
	{
		Movement.Movement_State_04 = setActive;
	}

	public static void CyclePlatformType(bool forward)
	{
		if (!forward)
		{
			CapturedVariables3760_Index_35 = (CapturedVariables3760_Index_35 - 1 + CapturedVariables3760_Text_26.Length) % CapturedVariables3760_Text_26.Length;
			CapturedVariables3760_Text_35 = CapturedVariables3760_Text_26[CapturedVariables3760_Index_35];
			CapturedVariables3760_Button_41?.SetText("Platform Type : " + CapturedVariables3760_Text_35);
		}
		else
		{
			CapturedVariables3760_Index_35 = (CapturedVariables3760_Index_35 + 1) % CapturedVariables3760_Text_26.Length;
			CapturedVariables3760_Text_35 = CapturedVariables3760_Text_26[CapturedVariables3760_Index_35];
			CapturedVariables3760_Button_41?.SetText("Platform Type : " + CapturedVariables3760_Text_35);
		}
	}

	public static void CycleOpacity(bool forward)
	{
		if (!forward)
		{
			CapturedVariables3760_Index_57 = (CapturedVariables3760_Index_57 - 1 + Recovered_Reference_28.Length) % Recovered_Reference_28.Length;
			(float value, string desc) tuple = Recovered_Reference_28[CapturedVariables3760_Index_57];
			CapturedVariables3760_Value_07 = tuple.value;
			CapturedVariables3760_Text_62 = tuple.desc;
			ButtonHandler.Button yMPC3HCV = CapturedVariables3760_Button_42;
			if (yMPC3HCV != null)
			{
				yMPC3HCV.SetText("Opacity : " + CapturedVariables3760_Text_62);
				Main.RebuildMenu();
			}
		}
		else
		{
			CapturedVariables3760_Index_57 = (CapturedVariables3760_Index_57 + 1) % Recovered_Reference_28.Length;
			(float value, string desc) tuple2 = Recovered_Reference_28[CapturedVariables3760_Index_57];
			CapturedVariables3760_Value_07 = tuple2.value;
			CapturedVariables3760_Text_62 = tuple2.desc;
			ButtonHandler.Button yMPC3HCV2 = CapturedVariables3760_Button_42;
			if (yMPC3HCV2 != null)
			{
				yMPC3HCV2.SetText("Opacity : " + CapturedVariables3760_Text_62);
				Main.RebuildMenu();
			}
		}
		Main.RebuildMenu();
	}

	public static void LoadSettings()
	{
		LoadSettings(SettingsFilePath);
	}

	public static void CycleGunPointerSize(bool forward)
	{
		if (!forward)
		{
			CapturedVariables3760_Index_04 = (CapturedVariables3760_Index_04 - 1 + Recovered_Reference_20.Length) % Recovered_Reference_20.Length;
			(CapturedVariables3760_Value_05, CapturedVariables3760_Text_20) = Recovered_Reference_20[CapturedVariables3760_Index_04];
			CapturedVariables3760_Button_40?.SetText("Gun Pointer Size : " + CapturedVariables3760_Text_20);
		}
		else
		{
			CapturedVariables3760_Index_04 = (CapturedVariables3760_Index_04 + 1) % Recovered_Reference_20.Length;
			(CapturedVariables3760_Value_05, CapturedVariables3760_Text_20) = Recovered_Reference_20[CapturedVariables3760_Index_04];
			CapturedVariables3760_Button_40?.SetText("Gun Pointer Size : " + CapturedVariables3760_Text_20);
		}
	}

	public Settings()
	{
	}

	public static void SetUseGripForSpeedboostEnabled(bool setActive)
	{
		Movement.Movement_State_11 = setActive;
	}

	public static void SetGunLineEnabled(bool setActive)
	{
		GunLib.GunLib_State_03 = setActive;
	}

	public static void CycleSoundInput(bool forward = true)
	{
		CapturedVariables3760_Index_31 = Array.IndexOf(CapturedVariables3760_Values_01, CapturedVariables3760_Reference_01);
		if (CapturedVariables3760_Index_31 == -1)
		{
			Debug.LogError((object)"Error: Current input is not found in the array!");
		}
		else if (!forward)
		{
			CapturedVariables3760_Index_31 = (CapturedVariables3760_Index_31 - 1 + CapturedVariables3760_Values_01.Length) % CapturedVariables3760_Values_01.Length;
			CapturedVariables3760_Reference_01 = CapturedVariables3760_Values_01[CapturedVariables3760_Index_31];
			CapturedVariables3760_Text_33 = CapturedVariables3760_Reference_01.Method.Name;
			CapturedVariables3760_Button_05?.SetText("Sound Input : " + CapturedVariables3760_Text_33);
		}
		else
		{
			CapturedVariables3760_Index_31 = (CapturedVariables3760_Index_31 + 1) % CapturedVariables3760_Values_01.Length;
			CapturedVariables3760_Reference_01 = CapturedVariables3760_Values_01[CapturedVariables3760_Index_31];
			CapturedVariables3760_Text_33 = CapturedVariables3760_Reference_01.Method.Name;
			CapturedVariables3760_Button_05?.SetText("Sound Input : " + CapturedVariables3760_Text_33);
		}
	}

	public static void CyclePinwheelColor2(bool forward)
	{
		CyclePaletteColor(ref CapturedVariables3760_Index_52, ref CapturedVariables3760_Color_11, ref CapturedVariables3760_Text_29, CapturedVariables3760_Button_36, "Pinwheel Color 2", forward);
		Main.CapturedVariables1950_Color_06 = (Color32)(CapturedVariables3760_Color_11);
	}

	public static void CycleMenuFont(bool forward)
	{
		if (Main.CapturedVariables1950_Reference_02.Count == 0)
		{
			return;
		}
		if (!forward)
		{
			CurrentFontIndex = (CurrentFontIndex - 1 + Main.CapturedVariables1950_Reference_02.Count) % Main.CapturedVariables1950_Reference_02.Count;
			ButtonHandler.Button button = CapturedVariables3760_Button_64;
			if (button != null)
			{
				button.SetText("Menu Font : " + CurrentFontDescription);
				Main.RebuildMenu();
			}
		}
		else
		{
			CurrentFontIndex = (CurrentFontIndex + 1) % Main.CapturedVariables1950_Reference_02.Count;
			ButtonHandler.Button button2 = CapturedVariables3760_Button_64;
			if (button2 != null)
			{
				button2.SetText("Menu Font : " + CurrentFontDescription);
				Main.RebuildMenu();
			}
		}
		Main.RebuildMenu();
	}

	public static Color GetAnimatedColor(ColorMode mode, Color c1, Color c2, float speed = 1f, int seed = 0)
	{
		float num = Time.time * speed;
		int num2 = (int)mode;
		num2 = (((uint)num2 <= 5u) ? num2 : 6) + 35;
		int num3 = num2;
		if (num3 != 36)
		{
			return c1;
		}
		return Color.Lerp(c1, c2, (Mathf.Sin(num + (float)seed * 0.5f) + 1f) * 0.5f);
	}

	public static void CycleTimeOfDay(bool forward)
	{
		if (!forward)
		{
			CapturedVariables3760_Index_05 = (CapturedVariables3760_Index_05 - 1 + CapturedVariables3760_Text_53.Length) % CapturedVariables3760_Text_53.Length;
			CapturedVariables3760_Text_13 = CapturedVariables3760_Text_53[CapturedVariables3760_Index_05];
			((BetterDayNightManager)BetterDayNightManager.instance).SetTimeOfDay(CapturedVariables3760_Index_05);
			CapturedVariables3760_Button_17?.SetText("Time Of Day : " + CapturedVariables3760_Text_13);
		}
		else
		{
			CapturedVariables3760_Index_05 = (CapturedVariables3760_Index_05 + 1) % CapturedVariables3760_Text_53.Length;
			CapturedVariables3760_Text_13 = CapturedVariables3760_Text_53[CapturedVariables3760_Index_05];
			((BetterDayNightManager)BetterDayNightManager.instance).SetTimeOfDay(CapturedVariables3760_Index_05);
			CapturedVariables3760_Button_17?.SetText("Time Of Day : " + CapturedVariables3760_Text_13);
		}
	}

	public static void CycleFPCFOV(bool forward)
	{
		if (!forward)
		{
			CapturedVariables3760_Index_25 = (CapturedVariables3760_Index_25 - 1 + Recovered_Reference_21.Length) % Recovered_Reference_21.Length;
			(CapturedVariables3760_Value_11, CapturedVariables3760_Text_43) = Recovered_Reference_21[CapturedVariables3760_Index_25];
			CapturedVariables3760_Button_06?.SetText("FPC FOV : " + CapturedVariables3760_Text_43);
		}
		else
		{
			CapturedVariables3760_Index_25 = (CapturedVariables3760_Index_25 + 1) % Recovered_Reference_21.Length;
			(CapturedVariables3760_Value_11, CapturedVariables3760_Text_43) = Recovered_Reference_21[CapturedVariables3760_Index_25];
			CapturedVariables3760_Button_06?.SetText("FPC FOV : " + CapturedVariables3760_Text_43);
		}
	}

	public static void SaveSettings()
	{
		SaveSettingsToFile(SettingsFilePath);
	}

	public static void CycleTitleMode(bool forward)
	{
		int num = CapturedVariables3760_Text_02.Length;
		int num2 = (int)CapturedVariables3760_Color_15;
		if (!forward)
		{
			goto Branch_002e;
		}
		goto Branch_0049;
		Branch_002e:
		num2 = (num2 - 1 + num) % num;
		if (num2 == 4)
		{
			goto Branch_001e;
		}
		goto Branch_0062;
		Branch_001e:
		if (!forward)
		{
			goto Branch_002e;
		}
		Branch_0049:
		num2 = (num2 + 1) % num;
		if (num2 == 4)
		{
			goto Branch_001e;
		}
		Branch_0062:
		CapturedVariables3760_Color_15 = (ColorMode)num2;
		CapturedVariables3760_Button_47?.SetText("Title Mode : " + CapturedVariables3760_Text_02[num2]);
	}

	public static void CycleGunAnimation(bool forward)
	{
		if (!forward)
		{
			CapturedVariables3760_Index_34 = (CapturedVariables3760_Index_34 - 1 + CapturedVariables3760_Text_46.Length) % CapturedVariables3760_Text_46.Length;
			CapturedVariables3760_Text_42 = CapturedVariables3760_Text_46[CapturedVariables3760_Index_34];
			CapturedVariables3760_Button_57?.SetText("Gun Animation : " + CapturedVariables3760_Text_42);
		}
		else
		{
			CapturedVariables3760_Index_34 = (CapturedVariables3760_Index_34 + 1) % CapturedVariables3760_Text_46.Length;
			CapturedVariables3760_Text_42 = CapturedVariables3760_Text_46[CapturedVariables3760_Index_34];
			CapturedVariables3760_Button_57?.SetText("Gun Animation : " + CapturedVariables3760_Text_42);
		}
	}

	public static void CycleRoundness(bool forward)
	{
		if (!forward)
		{
			CapturedVariables3760_Index_54 = (CapturedVariables3760_Index_54 - 1 + Recovered_Reference_27.Length) % Recovered_Reference_27.Length;
			(float value, string desc) tuple = Recovered_Reference_27[CapturedVariables3760_Index_54];
			CapturedVariables3760_Value_01 = tuple.value;
			CapturedVariables3760_Text_50 = tuple.desc;
			ButtonHandler.Button jWA9ZSJO = CapturedVariables3760_Button_19;
			if (jWA9ZSJO != null)
			{
				jWA9ZSJO.SetText("Roundness : " + CapturedVariables3760_Text_50);
				Main.RebuildMenu();
			}
		}
		else
		{
			CapturedVariables3760_Index_54 = (CapturedVariables3760_Index_54 + 1) % Recovered_Reference_27.Length;
			(float value, string desc) tuple2 = Recovered_Reference_27[CapturedVariables3760_Index_54];
			CapturedVariables3760_Value_01 = tuple2.value;
			CapturedVariables3760_Text_50 = tuple2.desc;
			ButtonHandler.Button jWA9ZSJO2 = CapturedVariables3760_Button_19;
			if (jWA9ZSJO2 != null)
			{
				jWA9ZSJO2.SetText("Roundness : " + CapturedVariables3760_Text_50);
				Main.RebuildMenu();
			}
		}
		Main.RebuildMenu();
	}

	public static void CycleOutlineColor(bool forward)
	{
		CyclePaletteColor(ref CapturedVariables3760_Index_07, ref CapturedVariables3760_Color_28, ref CapturedVariables3760_Text_03, CapturedVariables3760_Button_27, "Outline Color", forward);
	}

	public static void CyclePinwheelColor1(bool forward)
	{
		CyclePaletteColor(ref CapturedVariables3760_Index_56, ref CapturedVariables3760_Color_25, ref CapturedVariables3760_Text_22, CapturedVariables3760_Button_02, "Pinwheel Color 1", forward);
		Main.CapturedVariables1950_Color_02 = (Color32)(CapturedVariables3760_Color_25);
	}

	public static void CycleBoardsColor2(bool forward)
	{
		CyclePaletteColor(ref CapturedVariables3760_Index_36, ref CapturedVariables3760_Color_27, ref CapturedVariables3760_Text_14, CapturedVariables3760_Button_34, "Boards Color 2", forward);
	}

	public static void CycleButtonAnimationSpeed(bool forward)
	{
		CycleAnimationSpeed(ref CapturedVariables3760_Index_48, ref CapturedVariables3760_Value_15, ref CapturedVariables3760_Text_40, CapturedVariables3760_Button_15, forward);
	}

	public static void SetToggleNotificationsEnabled(bool setActive)
	{
		Variables.Variables_State_12 = setActive;
	}

	public static void CycleProjectileSize(bool forward)
	{
		if (!forward)
		{
			CapturedVariables3760_Index_44 = (CapturedVariables3760_Index_44 - 1 + Recovered_Reference_15.Length) % Recovered_Reference_15.Length;
			(CapturedVariables3760_Index_29, CapturedVariables3760_Text_58) = Recovered_Reference_15[CapturedVariables3760_Index_44];
			CapturedVariables3760_Button_29?.SetText("Projectile Size : " + CapturedVariables3760_Text_58);
		}
		else
		{
			CapturedVariables3760_Index_44 = (CapturedVariables3760_Index_44 + 1) % Recovered_Reference_15.Length;
			(CapturedVariables3760_Index_29, CapturedVariables3760_Text_58) = Recovered_Reference_15[CapturedVariables3760_Index_44];
			CapturedVariables3760_Button_29?.SetText("Projectile Size : " + CapturedVariables3760_Text_58);
		}
	}

	public static void CycleTracerPosition(bool forward)
	{
		if (!forward)
		{
			CapturedVariables3760_Index_50 = (CapturedVariables3760_Index_50 - 1 + CapturedVariables3760_Text_25.Length) % CapturedVariables3760_Text_25.Length;
			CapturedVariables3760_Text_57 = CapturedVariables3760_Text_25[CapturedVariables3760_Index_50];
			CapturedVariables3760_Button_62?.SetText("Tracer Position : " + CapturedVariables3760_Text_57);
		}
		else
		{
			CapturedVariables3760_Index_50 = (CapturedVariables3760_Index_50 + 1) % CapturedVariables3760_Text_25.Length;
			CapturedVariables3760_Text_57 = CapturedVariables3760_Text_25[CapturedVariables3760_Index_50];
			CapturedVariables3760_Button_62?.SetText("Tracer Position : " + CapturedVariables3760_Text_57);
		}
	}

	private static (Vector3 length, string desc)[] BuildLongArmsLengthOptions()
	{
		(Vector3, string)[] array = new(Vector3, string)[20];
		int num = 0;
		if (num < 20)
		{
			do
			{
				float num2 = Mathf.Round((float)(21 + num) * 0.05f * 1000f) / 1000f;
				array[num] = (new Vector3(num2, num2, num2), num2.ToString("0.0##", CultureInfo.InvariantCulture));
				num++;
			}
			while (num < 20);
		}
		return array;
	}

	private static int FindColorModeIndex(string name)
	{
		int num = Array.IndexOf(CapturedVariables3760_Text_02, name);
		if (num < 0)
		{
			return -1;
		}
		return num;
	}

	public static void CycleWiggleSpeed(bool forward)
	{
		if (!forward)
		{
			CapturedVariables3760_Index_10 = (CapturedVariables3760_Index_10 - 1 + Recovered_Reference_19.Length) % Recovered_Reference_19.Length;
			(CapturedVariables3760_Value_09, CapturedVariables3760_Text_31) = Recovered_Reference_19[CapturedVariables3760_Index_10];
			CapturedVariables3760_Button_07?.SetText("Wiggle Speed : " + CapturedVariables3760_Text_31);
		}
		else
		{
			CapturedVariables3760_Index_10 = (CapturedVariables3760_Index_10 + 1) % Recovered_Reference_19.Length;
			(CapturedVariables3760_Value_09, CapturedVariables3760_Text_31) = Recovered_Reference_19[CapturedVariables3760_Index_10];
			CapturedVariables3760_Button_07?.SetText("Wiggle Speed : " + CapturedVariables3760_Text_31);
		}
	}

	public static void CycleAccentAnimationSpeed(bool forward)
	{
		CycleAnimationSpeed(ref CapturedVariables3760_Index_09, ref CapturedVariables3760_Value_04, ref CapturedVariables3760_Text_19, CapturedVariables3760_Button_20, forward);
	}

	public static void SetOpenAndCloseSoundsEnabled(bool setActive)
	{
		Variables.Variables_State_14 = setActive;
	}

	public static void ToggleAnimatedGradient()
	{
		CapturedVariables3760_State_03 = !CapturedVariables3760_State_03;
		CapturedVariables3760_Button_35?.SetText("Animated Gradient : " + (CapturedVariables3760_State_03 ? "On" : "Off"));
	}

	public static void CycleBackgroundMode(bool forward)
	{
		CycleColorMode(ref CapturedVariables3760_Color_10, delegate(string v)
		{
			CapturedVariables3760_Button_13?.SetText("Background Mode : " + v);
		}, forward);
	}

	public static void CycleWiggleIntensity(bool forward)
	{
		if (!forward)
		{
			CapturedVariables3760_Index_27 = (CapturedVariables3760_Index_27 - 1 + Recovered_Reference_30.Length) % Recovered_Reference_30.Length;
			(CapturedVariables3760_Value_08, CapturedVariables3760_Text_04) = Recovered_Reference_30[CapturedVariables3760_Index_27];
			CapturedVariables3760_Button_30?.SetText("Wiggle Intensity : " + CapturedVariables3760_Text_04);
		}
		else
		{
			CapturedVariables3760_Index_27 = (CapturedVariables3760_Index_27 + 1) % Recovered_Reference_30.Length;
			(CapturedVariables3760_Value_08, CapturedVariables3760_Text_04) = Recovered_Reference_30[CapturedVariables3760_Index_27];
			CapturedVariables3760_Button_30?.SetText("Wiggle Intensity : " + CapturedVariables3760_Text_04);
		}
	}

	public static void CycleClickSound(bool forward)
	{
		if (!forward)
		{
			CapturedVariables3760_Index_43 = (CapturedVariables3760_Index_43 - 1 + ButtonHandler.CapturedVariables570_Items_03.Count) % ButtonHandler.CapturedVariables570_Items_03.Count;
			CapturedVariables3760_Button_37?.SetText("Click Sound : " + ClickSoundDescription);
		}
		else
		{
			CapturedVariables3760_Index_43 = (CapturedVariables3760_Index_43 + 1) % ButtonHandler.CapturedVariables570_Items_03.Count;
			CapturedVariables3760_Button_37?.SetText("Click Sound : " + ClickSoundDescription);
		}
	}

	public static void CycleEnabledButtonColor(bool forward)
	{
		CyclePaletteColor(ref CapturedVariables3760_Index_03, ref CapturedVariables3760_Color_08, ref CapturedVariables3760_Text_27, CapturedVariables3760_Button_18, "Enabled Button Color", forward);
	}

	public static void CycleMenuSize(bool forward)
	{
		if (!forward)
		{
			CapturedVariables3760_Index_53 = (CapturedVariables3760_Index_53 - 1 + Recovered_Reference_23.Length) % Recovered_Reference_23.Length;
			(float scale, string desc) tuple = Recovered_Reference_23[CapturedVariables3760_Index_53];
			Main.CapturedVariables1950_Value_04 = tuple.scale;
			CapturedVariables3760_Text_30 = tuple.desc;
			ButtonHandler.Button xS1CQMPU = CapturedVariables3760_Button_39;
			if (xS1CQMPU != null)
			{
				xS1CQMPU.SetText("Menu Size : " + CapturedVariables3760_Text_30);
				Main.RebuildMenu();
			}
		}
		else
		{
			CapturedVariables3760_Index_53 = (CapturedVariables3760_Index_53 + 1) % Recovered_Reference_23.Length;
			(float scale, string desc) tuple2 = Recovered_Reference_23[CapturedVariables3760_Index_53];
			Main.CapturedVariables1950_Value_04 = tuple2.scale;
			CapturedVariables3760_Text_30 = tuple2.desc;
			ButtonHandler.Button xS1CQMPU2 = CapturedVariables3760_Button_39;
			if (xS1CQMPU2 != null)
			{
				xS1CQMPU2.SetText("Menu Size : " + CapturedVariables3760_Text_30);
				Main.RebuildMenu();
			}
		}
		Main.RebuildMenu();
	}

	public static void CycleBoostSpeed(bool forward)
	{
		if (!forward)
		{
			CapturedVariables3760_Index_42 = (CapturedVariables3760_Index_42 - 1 + Recovered_Reference_11.Length) % Recovered_Reference_11.Length;
			(float speed, string desc) tuple = Recovered_Reference_11[CapturedVariables3760_Index_42];
			CapturedVariables3760_Value_06 = tuple.speed;
			CapturedVariables3760_Text_10 = tuple.desc;
			CapturedVariables3760_Value_16 = CapturedVariables3760_Value_06 / 5f;
			CapturedVariables3760_Button_63?.SetText("Boost Speed : " + CapturedVariables3760_Text_10);
		}
		else
		{
			CapturedVariables3760_Index_42 = (CapturedVariables3760_Index_42 + 1) % Recovered_Reference_11.Length;
			(float speed, string desc) tuple2 = Recovered_Reference_11[CapturedVariables3760_Index_42];
			CapturedVariables3760_Value_06 = tuple2.speed;
			CapturedVariables3760_Text_10 = tuple2.desc;
			CapturedVariables3760_Value_16 = CapturedVariables3760_Value_06 / 5f;
			CapturedVariables3760_Button_63?.SetText("Boost Speed : " + CapturedVariables3760_Text_10);
		}
	}

	public static void CycleTitleAnimationSpeed(bool forward)
	{
		CycleAnimationSpeed(ref CapturedVariables3760_Index_21, ref CapturedVariables3760_Value_10, ref CapturedVariables3760_Text_09, CapturedVariables3760_Button_12, forward);
	}

	public static void CycleWallWalkStrength(bool forward)
	{
		if (!forward)
		{
			CapturedVariables3760_Index_47 = (CapturedVariables3760_Index_47 - 1 + Recovered_Reference_31.Length) % Recovered_Reference_31.Length;
			(CapturedVariables3760_Value_20, CapturedVariables3760_Text_39) = Recovered_Reference_31[CapturedVariables3760_Index_47];
			CapturedVariables3760_Button_60?.SetText("Wall Walk Strength : " + CapturedVariables3760_Text_39);
		}
		else
		{
			CapturedVariables3760_Index_47 = (CapturedVariables3760_Index_47 + 1) % Recovered_Reference_31.Length;
			(CapturedVariables3760_Value_20, CapturedVariables3760_Text_39) = Recovered_Reference_31[CapturedVariables3760_Index_47];
			CapturedVariables3760_Button_60?.SetText("Wall Walk Strength : " + CapturedVariables3760_Text_39);
		}
	}

	public static void CycleGunIdleColor(bool forward)
	{
		CycleGunColor(ref CapturedVariables3760_Index_49, ref CapturedVariables3760_Color_20, ref CapturedVariables3760_Text_17, ref CapturedVariables3760_State_05, CapturedVariables3760_Button_32, "Gun Idle Color", forward);
	}

	public static void CycleEnabledButtonColor2(bool forward)
	{
		CyclePaletteColor(ref CapturedVariables3760_Index_30, ref CapturedVariables3760_Color_29, ref CapturedVariables3760_Text_45, CapturedVariables3760_Button_61, "Enabled Button Color 2", forward);
	}

	public static void CycleEnabledButtonAnimationSpeed(bool forward)
	{
		CycleAnimationSpeed(ref CapturedVariables3760_Index_02, ref CapturedVariables3760_Value_13, ref CapturedVariables3760_Text_51, CapturedVariables3760_Button_46, forward);
	}

	public static void CycleGunLockColor(bool forward)
	{
		CycleGunColor(ref CapturedVariables3760_Index_37, ref CapturedVariables3760_Color_07, ref CapturedVariables3760_Text_54, ref CapturedVariables3760_State_01, CapturedVariables3760_Button_38, "Gun Lock Color", forward);
	}

	private static void CycleColorMode(ref ColorMode mode, Action<string> setText, bool forward = true)
	{
		int num = CapturedVariables3760_Text_02.Length;
		int num2 = (int)mode;
		ColorMode colorMode = mode;
		if (!forward)
		{
			num2 = (int)(mode = (ColorMode)((num2 - 1 + num) % num));
			setText(CapturedVariables3760_Text_02[num2]);
			if (colorMode != ColorMode.Gradient)
			{
				goto Branch_0072;
			}
		}
		else
		{
			num2 = (int)(mode = (ColorMode)((num2 + 1) % num));
			setText(CapturedVariables3760_Text_02[num2]);
			if (colorMode != ColorMode.Gradient)
			{
				goto Branch_0072;
			}
		}
		Branch_009f:
		Main.RebuildMenu();
		return;
		Branch_0072:
		if (mode != ColorMode.Gradient)
		{
			return;
		}
		goto Branch_009f;
	}

	public static void CycleAccentMode(bool forward)
	{
		CycleColorMode(ref CapturedVariables3760_Color_12, delegate(string v)
		{
			CapturedVariables3760_Button_33?.SetText("Accent Mode : " + v);
		}, forward);
	}

	public static void EnsureDefaultSettingsCached()
	{
		if (CapturedVariables3760_Text_56 == null)
		{
			CapturedVariables3760_Text_56 = SerializeSettings();
		}
	}

	public static void CycleBackgroundAnimationSpeed(bool forward)
	{
		CycleAnimationSpeed(ref CapturedVariables3760_Index_20, ref CapturedVariables3760_Value_12, ref CapturedVariables3760_Text_28, CapturedVariables3760_Button_08, forward);
	}

	public static bool IsElementSettingVisible(string buttonText)
	{
		if (string.IsNullOrEmpty(buttonText))
		{
			return true;
		}
		if (buttonText.StartsWith("Background Color 2"))
		{
			if (CapturedVariables3760_Color_10 != ColorMode.Lerp && CapturedVariables3760_Color_10 != ColorMode.Strobe)
			{
				return CapturedVariables3760_Color_10 == ColorMode.Gradient;
			}
			return true;
		}
		if (buttonText.StartsWith("Button Color 2"))
		{
			if (CapturedVariables3760_Color_18 != ColorMode.Lerp && CapturedVariables3760_Color_18 != ColorMode.Strobe)
			{
				return CapturedVariables3760_Color_18 == ColorMode.Gradient;
			}
			return true;
		}
		if (buttonText.StartsWith("Enabled Button Color 2"))
		{
			if (CapturedVariables3760_Color_05 != ColorMode.Lerp && CapturedVariables3760_Color_05 != ColorMode.Strobe)
			{
				return CapturedVariables3760_Color_05 == ColorMode.Gradient;
			}
			return true;
		}
		if (buttonText.StartsWith("Title Color 2"))
		{
			if (CapturedVariables3760_Color_15 != ColorMode.Lerp && CapturedVariables3760_Color_15 != ColorMode.Strobe)
			{
				return CapturedVariables3760_Color_15 == ColorMode.Gradient;
			}
			return true;
		}
		if (buttonText.StartsWith("Outline Color 2"))
		{
			if (CapturedVariables3760_Color_04 != ColorMode.Lerp && CapturedVariables3760_Color_04 != ColorMode.Strobe)
			{
				return CapturedVariables3760_Color_04 == ColorMode.Gradient;
			}
			return true;
		}
		if (buttonText.StartsWith("Accent Color 2"))
		{
			if (CapturedVariables3760_Color_12 != ColorMode.Lerp && CapturedVariables3760_Color_12 != ColorMode.Strobe)
			{
				return CapturedVariables3760_Color_12 == ColorMode.Gradient;
			}
			return true;
		}
		if (buttonText.StartsWith("Boards Color 2"))
		{
			if (CapturedVariables3760_Color_03 != ColorMode.Lerp)
			{
				return CapturedVariables3760_Color_03 == ColorMode.Strobe;
			}
			return true;
		}
		if (buttonText.StartsWith("Outline Color "))
		{
			if (CapturedVariables3760_Color_04 != ColorMode.Rainbow)
			{
				return CapturedVariables3760_Color_04 != ColorMode.Pinwheel;
			}
			return false;
		}
		if (buttonText.StartsWith("Background Color "))
		{
			if (CapturedVariables3760_Color_10 != ColorMode.Rainbow)
			{
				return CapturedVariables3760_Color_10 != ColorMode.Pinwheel;
			}
			return false;
		}
		if (buttonText.StartsWith("Button Color "))
		{
			if (CapturedVariables3760_Color_18 != ColorMode.Rainbow)
			{
				return CapturedVariables3760_Color_18 != ColorMode.Pinwheel;
			}
			return false;
		}
		if (buttonText.StartsWith("Enabled Button Color "))
		{
			if (CapturedVariables3760_Color_05 != ColorMode.Rainbow)
			{
				return CapturedVariables3760_Color_05 != ColorMode.Pinwheel;
			}
			return false;
		}
		if (buttonText.StartsWith("Title Color "))
		{
			if (CapturedVariables3760_Color_15 != ColorMode.Rainbow)
			{
				return CapturedVariables3760_Color_15 != ColorMode.Pinwheel;
			}
			return false;
		}
		if (buttonText.StartsWith("Accent Color "))
		{
			if (CapturedVariables3760_Color_12 != ColorMode.Rainbow)
			{
				return CapturedVariables3760_Color_12 != ColorMode.Pinwheel;
			}
			return false;
		}
		if (buttonText.StartsWith("Boards Color "))
		{
			if (CapturedVariables3760_Color_03 != ColorMode.Rainbow)
			{
				return CapturedVariables3760_Color_03 != ColorMode.Pinwheel;
			}
			return false;
		}
		if (buttonText.StartsWith("Pinwheel Speed"))
		{
			return CapturedVariables3760_Index_32 != 0;
		}
		if (buttonText.StartsWith("Wiggle Intensity"))
		{
			if (CapturedVariables3760_Text_42 != "None")
			{
				return CapturedVariables3760_Text_42 != "Pulse";
			}
			return false;
		}
		if (buttonText.StartsWith("Wiggle Speed"))
		{
			return CapturedVariables3760_Text_42 != "None";
		}
		return true;
	}

	public static void DefaultSettings()
	{
		if (CapturedVariables3760_Text_56 == null)
		{
			return;
		}
		string[] array = CapturedVariables3760_Text_56;
		int num = 0;
		while (num < array.Length)
		{
			string[] array2 = array[num].Split(':');
			if (array2.Length >= 2)
			{
				ApplySetting(array2[0].Trim(), string.Join(":", array2.Skip(1)).Trim());
				num++;
			}
			else
			{
				num++;
			}
		}
		SaveSettings();
		Main.RebuildMenu();
		NotificationLib.ShowNotification(NotificationLib.NotificationType.Loaded, "Default Settings");
	}

	public static void CycleButtonColor(bool forward)
	{
		CyclePaletteColor(ref CapturedVariables3760_Index_45, ref CapturedVariables3760_Color_26, ref CapturedVariables3760_Text_32, CapturedVariables3760_Button_14, "Button Color", forward);
	}

	public static void CycleAccentColor(bool forward)
	{
		CyclePaletteColor(ref CapturedVariables3760_Index_15, ref CapturedVariables3760_Color_14, ref CapturedVariables3760_Text_60, CapturedVariables3760_Button_10, "Accent Color", forward);
	}

	private static void CycleAnimationSpeed(ref int index, ref float speed, ref string desc, ButtonHandler.Button btn, bool forward)
	{
		index = (forward ? ((index + 1) % Recovered_Reference_09.Length) : ((index - 1 + Recovered_Reference_09.Length) % Recovered_Reference_09.Length));
		(speed, desc) = Recovered_Reference_09[index];
		btn?.SetText("Animation Speed : " + desc);
	}

	public static void SaveSettingsToFile(string path)
	{
		try
		{
			File.WriteAllLines(path, SerializeSettings());
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("Error saving settings: " + ex.Message));
		}
	}

	public static void LoadSettings(string path)
	{
		if (!File.Exists(path))
		{
			return;
		}
		try
		{
			string[] array = File.ReadAllLines(path);
			int num = 0;
			while (num < array.Length)
			{
				string[] array2 = array[num].Split(':');
				if (array2.Length >= 2)
				{
					ApplySetting(array2[0].Trim(), string.Join(":", array2.Skip(1)).Trim());
					num++;
				}
				else
				{
					num++;
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("Error loading settings: " + ex.Message));
		}
	}

	public static void CycleProjectileSpeed(bool forward)
	{
		if (!forward)
		{
			CapturedVariables3760_Index_39 = (CapturedVariables3760_Index_39 - 1 + Recovered_Reference_26.Length) % Recovered_Reference_26.Length;
			(CapturedVariables3760_Value_02, CapturedVariables3760_Text_12) = Recovered_Reference_26[CapturedVariables3760_Index_39];
			CapturedVariables3760_Button_51?.SetText("Projectile Speed : " + CapturedVariables3760_Text_12);
		}
		else
		{
			CapturedVariables3760_Index_39 = (CapturedVariables3760_Index_39 + 1) % Recovered_Reference_26.Length;
			(CapturedVariables3760_Value_02, CapturedVariables3760_Text_12) = Recovered_Reference_26[CapturedVariables3760_Index_39];
			CapturedVariables3760_Button_51?.SetText("Projectile Speed : " + CapturedVariables3760_Text_12);
		}
	}

	public static void CycleBoardsMode(bool forward)
	{
		CycleColorMode(ref CapturedVariables3760_Color_03, delegate(string v)
		{
			CapturedVariables3760_Button_53?.SetText("Boards Mode : " + v);
		}, forward);
	}

	public static void CyclePinwheelSpeed(bool forward)
	{
		if (!forward)
		{
			CapturedVariables3760_Index_22 = (CapturedVariables3760_Index_22 - 1 + Recovered_Reference_22.Length) % Recovered_Reference_22.Length;
			(Main.CapturedVariables1950_Value_06, CapturedVariables3760_Text_63) = Recovered_Reference_22[CapturedVariables3760_Index_22];
			CapturedVariables3760_Button_52?.SetText("Pinwheel Speed : " + CapturedVariables3760_Text_63);
		}
		else
		{
			CapturedVariables3760_Index_22 = (CapturedVariables3760_Index_22 + 1) % Recovered_Reference_22.Length;
			(Main.CapturedVariables1950_Value_06, CapturedVariables3760_Text_63) = Recovered_Reference_22[CapturedVariables3760_Index_22];
			CapturedVariables3760_Button_52?.SetText("Pinwheel Speed : " + CapturedVariables3760_Text_63);
		}
	}

	public static void CycleAccentStrip(bool forward)
	{
		if (!forward)
		{
			CapturedVariables3760_Index_11 = (CapturedVariables3760_Index_11 - 1 + CapturedVariables3760_Text_08.Length) % CapturedVariables3760_Text_08.Length;
			CapturedVariables3760_Reference_03 = (AccentStripType)CapturedVariables3760_Index_11;
			CapturedVariables3760_Text_24 = CapturedVariables3760_Text_08[CapturedVariables3760_Index_11];
			ButtonHandler.Button aO3DWTCN = CapturedVariables3760_Button_01;
			if (aO3DWTCN != null)
			{
				aO3DWTCN.SetText("Accent Strip : " + CapturedVariables3760_Text_24);
				Main.RebuildMenu();
			}
		}
		else
		{
			CapturedVariables3760_Index_11 = (CapturedVariables3760_Index_11 + 1) % CapturedVariables3760_Text_08.Length;
			CapturedVariables3760_Reference_03 = (AccentStripType)CapturedVariables3760_Index_11;
			CapturedVariables3760_Text_24 = CapturedVariables3760_Text_08[CapturedVariables3760_Index_11];
			ButtonHandler.Button aO3DWTCN2 = CapturedVariables3760_Button_01;
			if (aO3DWTCN2 != null)
			{
				aO3DWTCN2.SetText("Accent Strip : " + CapturedVariables3760_Text_24);
				Main.RebuildMenu();
			}
		}
		Main.RebuildMenu();
	}

	public static void CycleAntiReportRadius(bool forward)
	{
		if (!forward)
		{
			CapturedVariables3760_Index_28 = (CapturedVariables3760_Index_28 - 1 + Recovered_Reference_24.Length) % Recovered_Reference_24.Length;
			(CapturedVariables3760_Value_18, CapturedVariables3760_Text_47) = Recovered_Reference_24[CapturedVariables3760_Index_28];
			CapturedVariables3760_Button_44?.SetText("Anti Report Radius : " + CapturedVariables3760_Text_47);
		}
		else
		{
			CapturedVariables3760_Index_28 = (CapturedVariables3760_Index_28 + 1) % Recovered_Reference_24.Length;
			(CapturedVariables3760_Value_18, CapturedVariables3760_Text_47) = Recovered_Reference_24[CapturedVariables3760_Index_28];
			CapturedVariables3760_Button_44?.SetText("Anti Report Radius : " + CapturedVariables3760_Text_47);
		}
	}

	private static string[] SerializeSettings()
	{
		return new string[67]
		{
			"Pinwheel Speed : " + CapturedVariables3760_Text_63,
			"Tracer Position : " + CapturedVariables3760_Text_57,
			"Boost Speed : " + CapturedVariables3760_Text_10,
			"Fly Speed : " + CapturedVariables3760_Text_21,
			"FPC FOV : " + CapturedVariables3760_Text_43,
			"Wall Walk Strength : " + CapturedVariables3760_Text_39,
			"Long Arms Length : " + CapturedVariables3760_Text_05,
			"Platform Type : " + CapturedVariables3760_Text_35,
			"Sound Input : " + CapturedVariables3760_Text_33,
			"Anti Report Radius : " + CapturedVariables3760_Text_47,
			"Projectile Speed : " + CapturedVariables3760_Text_12,
			"Projectile Size : " + CapturedVariables3760_Text_58,
			"Menu Font : " + CurrentFontDescription,
			"Click Sound : " + ClickSoundDescription,
			"Lag Type : " + CapturedVariables3760_Text_52,
			"Nametag Type : " + CapturedVariables3760_Text_44,
			"Gun Animation : " + CapturedVariables3760_Text_42,
			"Gun Idle Color : " + CapturedVariables3760_Text_17,
			"Gun Fire Color : " + CapturedVariables3760_Text_06,
			"Gun Hover Color : " + CapturedVariables3760_Text_38,
			"Gun Lock Color : " + CapturedVariables3760_Text_54,
			"Gun Pointer Size : " + CapturedVariables3760_Text_20,
			"Gun Line Thickness : " + CapturedVariables3760_Text_55,
			"Gun Wiggle Intensity : " + CapturedVariables3760_Text_04,
			"Gun Wiggle Speed : " + CapturedVariables3760_Text_31,
			"Nextbot Speed : " + Nextbots.SpawnRoutine_StateMachine22_Text_01,
			$"Nextbot Behaviour : {Nextbots.SpawnRoutine_StateMachine22_Reference_01}",
			"Current Projectile : " + Projectile.CurrentProjectileName,
			"Background Color : " + CapturedVariables3760_Text_61,
			"Background Color 2 : " + CapturedVariables3760_Text_59,
			"Pinwheel Color 1 : " + CapturedVariables3760_Text_22,
			"Pinwheel Color 2 : " + CapturedVariables3760_Text_29,
			"Button Color : " + CapturedVariables3760_Text_32,
			"Button Color 2 : " + CapturedVariables3760_Text_11,
			"Enabled Button Color : " + CapturedVariables3760_Text_27,
			"Enabled Button Color 2 : " + CapturedVariables3760_Text_45,
			"Outline : " + CapturedVariables3760_Text_07[CapturedVariables3760_Index_32],
			"Title Color : " + CapturedVariables3760_Text_48,
			"Title Color 2 : " + CapturedVariables3760_Text_36,
			"Title Animated Gradient : " + (CapturedVariables3760_State_03 ? "On" : "Off"),
			"Background Mode : " + CapturedVariables3760_Text_02[(int)CapturedVariables3760_Color_10],
			"Button Mode : " + CapturedVariables3760_Text_02[(int)CapturedVariables3760_Color_18],
			"Enabled Mode : " + CapturedVariables3760_Text_02[(int)CapturedVariables3760_Color_05],
			"Title Mode : " + CapturedVariables3760_Text_02[(int)CapturedVariables3760_Color_15],
			"Outline Color : " + CapturedVariables3760_Text_03,
			"Outline Color 2 : " + CapturedVariables3760_Text_16,
			"Outline Mode : " + CapturedVariables3760_Text_02[(int)CapturedVariables3760_Color_04],
			"Outline Anim Speed : " + CapturedVariables3760_Text_15,
			"Background Anim Speed : " + CapturedVariables3760_Text_28,
			"Button Anim Speed : " + CapturedVariables3760_Text_40,
			"Enabled Button Anim Speed : " + CapturedVariables3760_Text_51,
			"Title Anim Speed : " + CapturedVariables3760_Text_09,
			"Accent Color : " + CapturedVariables3760_Text_60,
			"Accent Color 2 : " + CapturedVariables3760_Text_41,
			"Accent Mode : " + CapturedVariables3760_Text_02[(int)CapturedVariables3760_Color_12],
			"Accent Anim Speed : " + CapturedVariables3760_Text_19,
			"Menu Size : " + CapturedVariables3760_Text_30,
			"Roundness : " + CapturedVariables3760_Text_50,
			"Accent Strip Type : " + CapturedVariables3760_Text_24,
			"Time Of Day : " + CapturedVariables3760_Text_13,
			"Opacity : " + CapturedVariables3760_Text_62,
			"Boards Color : " + CapturedVariables3760_Text_01,
			"Boards Color 2 : " + CapturedVariables3760_Text_14,
			"Boards Mode : " + CapturedVariables3760_Text_02[(int)CapturedVariables3760_Color_03],
			"Boards Anim Speed : " + CapturedVariables3760_Text_18,
			"Gradient Direction : " + CapturedVariables3760_Text_23,
			"TP Map : " + MapDescription
		};
	}

	public static void CycleBoardsAnimationSpeed(bool forward)
	{
		CycleAnimationSpeed(ref CapturedVariables3760_Index_08, ref CapturedVariables3760_Value_17, ref CapturedVariables3760_Text_18, CapturedVariables3760_Button_24, forward);
	}

	private static ButtonHandler.Button CreateIncrementalSettingButton(string text, Action up, Action down)
	{
		return new ButtonHandler.Button(text, Category.Element_Settings, isToggle: false, isActive: false, null, null, incremental: true, up, down);
	}

	public static void CycleTitleColor2(bool forward)
	{
		CyclePaletteColor(ref CapturedVariables3760_Index_13, ref CapturedVariables3760_Color_16, ref CapturedVariables3760_Text_36, CapturedVariables3760_Button_31, "Title Color 2", forward);
	}

	public static void CycleGunLineThickness(bool forward)
	{
		if (!forward)
		{
			CapturedVariables3760_Index_38 = (CapturedVariables3760_Index_38 - 1 + Recovered_Reference_12.Length) % Recovered_Reference_12.Length;
			(CapturedVariables3760_Value_21, CapturedVariables3760_Text_55) = Recovered_Reference_12[CapturedVariables3760_Index_38];
			CapturedVariables3760_Button_55?.SetText("Gun Line Thickness : " + CapturedVariables3760_Text_55);
		}
		else
		{
			CapturedVariables3760_Index_38 = (CapturedVariables3760_Index_38 + 1) % Recovered_Reference_12.Length;
			(CapturedVariables3760_Value_21, CapturedVariables3760_Text_55) = Recovered_Reference_12[CapturedVariables3760_Index_38];
			CapturedVariables3760_Button_55?.SetText("Gun Line Thickness : " + CapturedVariables3760_Text_55);
		}
	}

	static Settings()
	{
		Recovered_Reference_20 = new(float, string)[5]
		{
			(0.06f, "Tiny"),
			(0.1f, "Small"),
			(0.15f, "Medium"),
			(0.2f, "Large"),
			(0.3f, "Huge")
		};
		CapturedVariables3760_Index_04 = 1;
		CapturedVariables3760_Value_05 = 0.1f;
		CapturedVariables3760_Text_20 = "Small";
		Recovered_Reference_12 = new(float, string)[4]
		{
			(0.015f, "Thin"),
			(0.03f, "Normal"),
			(0.05f, "Thick"),
			(0.08f, "Chunky")
		};
		CapturedVariables3760_Index_38 = 1;
		CapturedVariables3760_Value_21 = 0.03f;
		CapturedVariables3760_Text_55 = "Normal";
		CapturedVariables3760_Text_02 = new string[6] { "Solid", "Lerp", "Rainbow", "Strobe", "Pinwheel", "Gradient" };
		CapturedVariables3760_Color_10 = ColorMode.Solid;
		CapturedVariables3760_Color_18 = ColorMode.Solid;
		CapturedVariables3760_Color_05 = ColorMode.Solid;
		CapturedVariables3760_Color_15 = ColorMode.Gradient;
		CapturedVariables3760_Color_04 = ColorMode.Pinwheel;
		CapturedVariables3760_Color_12 = ColorMode.Solid;
		CapturedVariables3760_Reference_02 = GradientDirection.Horizontal;
		CapturedVariables3760_Text_34 = new string[3] { "Horizontal", "Vertical", "Diagonal" };
		CapturedVariables3760_Index_12 = 0;
		CapturedVariables3760_Text_23 = "Horizontal";
		CapturedVariables3760_Color_01 = (Color32[])(object)new Color32[90]
		{
			new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue),
			new Color32((byte)230, (byte)230, (byte)230, byte.MaxValue),
			new Color32((byte)180, (byte)180, (byte)180, byte.MaxValue),
			new Color32((byte)128, (byte)128, (byte)128, byte.MaxValue),
			new Color32((byte)80, (byte)80, (byte)80, byte.MaxValue),
			new Color32((byte)40, (byte)40, (byte)40, byte.MaxValue),
			new Color32((byte)15, (byte)15, (byte)15, byte.MaxValue),
			new Color32((byte)0, (byte)0, (byte)0, byte.MaxValue),
			new Color32(byte.MaxValue, (byte)0, (byte)0, byte.MaxValue),
			new Color32((byte)220, (byte)20, (byte)60, byte.MaxValue),
			new Color32((byte)178, (byte)34, (byte)34, byte.MaxValue),
			new Color32(byte.MaxValue, (byte)99, (byte)99, byte.MaxValue),
			new Color32(byte.MaxValue, (byte)105, (byte)180, byte.MaxValue),
			new Color32(byte.MaxValue, (byte)20, (byte)147, byte.MaxValue),
			new Color32(byte.MaxValue, (byte)182, (byte)193, byte.MaxValue),
			new Color32(byte.MaxValue, (byte)192, (byte)203, byte.MaxValue),
			new Color32(byte.MaxValue, (byte)0, (byte)127, byte.MaxValue),
			new Color32((byte)231, (byte)84, (byte)128, byte.MaxValue),
			new Color32(byte.MaxValue, (byte)127, (byte)80, byte.MaxValue),
			new Color32((byte)250, (byte)128, (byte)114, byte.MaxValue),
			new Color32(byte.MaxValue, (byte)165, (byte)0, byte.MaxValue),
			new Color32(byte.MaxValue, (byte)140, (byte)0, byte.MaxValue),
			new Color32(byte.MaxValue, (byte)69, (byte)0, byte.MaxValue),
			new Color32(byte.MaxValue, (byte)215, (byte)80, byte.MaxValue),
			new Color32(byte.MaxValue, (byte)99, (byte)71, byte.MaxValue),
			new Color32((byte)210, (byte)105, (byte)30, byte.MaxValue),
			new Color32((byte)139, (byte)69, (byte)19, byte.MaxValue),
			new Color32((byte)160, (byte)82, (byte)45, byte.MaxValue),
			new Color32(byte.MaxValue, byte.MaxValue, (byte)0, byte.MaxValue),
			new Color32(byte.MaxValue, (byte)220, (byte)80, byte.MaxValue),
			new Color32(byte.MaxValue, (byte)215, (byte)0, byte.MaxValue),
			new Color32((byte)240, (byte)230, (byte)140, byte.MaxValue),
			new Color32(byte.MaxValue, (byte)250, (byte)205, byte.MaxValue),
			new Color32((byte)0, byte.MaxValue, (byte)0, byte.MaxValue),
			new Color32((byte)50, (byte)205, (byte)50, byte.MaxValue),
			new Color32((byte)34, (byte)139, (byte)34, byte.MaxValue),
			new Color32((byte)0, (byte)128, (byte)0, byte.MaxValue),
			new Color32((byte)124, (byte)252, (byte)0, byte.MaxValue),
			new Color32((byte)173, byte.MaxValue, (byte)47, byte.MaxValue),
			new Color32((byte)152, (byte)251, (byte)152, byte.MaxValue),
			new Color32((byte)60, (byte)179, (byte)113, byte.MaxValue),
			new Color32((byte)46, (byte)204, (byte)113, byte.MaxValue),
			new Color32((byte)85, (byte)107, (byte)47, byte.MaxValue),
			new Color32((byte)0, byte.MaxValue, byte.MaxValue, byte.MaxValue),
			new Color32((byte)64, (byte)224, (byte)208, byte.MaxValue),
			new Color32((byte)72, (byte)209, (byte)204, byte.MaxValue),
			new Color32((byte)0, (byte)206, (byte)209, byte.MaxValue),
			new Color32((byte)127, byte.MaxValue, (byte)212, byte.MaxValue),
			new Color32((byte)0, (byte)128, (byte)128, byte.MaxValue),
			new Color32((byte)176, (byte)224, (byte)230, byte.MaxValue),
			new Color32((byte)0, (byte)0, byte.MaxValue, byte.MaxValue),
			new Color32((byte)30, (byte)144, byte.MaxValue, byte.MaxValue),
			new Color32((byte)0, (byte)191, byte.MaxValue, byte.MaxValue),
			new Color32((byte)135, (byte)206, (byte)235, byte.MaxValue),
			new Color32((byte)70, (byte)130, (byte)180, byte.MaxValue),
			new Color32((byte)100, (byte)149, (byte)237, byte.MaxValue),
			new Color32((byte)25, (byte)25, (byte)112, byte.MaxValue),
			new Color32((byte)65, (byte)105, (byte)225, byte.MaxValue),
			new Color32((byte)180, (byte)220, byte.MaxValue, byte.MaxValue),
			new Color32((byte)128, (byte)0, (byte)128, byte.MaxValue),
			new Color32((byte)186, (byte)85, (byte)211, byte.MaxValue),
			new Color32((byte)218, (byte)112, (byte)214, byte.MaxValue),
			new Color32((byte)221, (byte)160, (byte)221, byte.MaxValue),
			new Color32((byte)147, (byte)112, (byte)219, byte.MaxValue),
			new Color32((byte)138, (byte)43, (byte)226, byte.MaxValue),
			new Color32((byte)75, (byte)0, (byte)130, byte.MaxValue),
			new Color32((byte)139, (byte)0, (byte)139, byte.MaxValue),
			new Color32(byte.MaxValue, (byte)0, byte.MaxValue, byte.MaxValue),
			new Color32((byte)199, (byte)21, (byte)133, byte.MaxValue),
			new Color32((byte)72, (byte)61, (byte)139, byte.MaxValue),
			new Color32((byte)57, byte.MaxValue, (byte)20, byte.MaxValue),
			new Color32(byte.MaxValue, (byte)0, (byte)234, byte.MaxValue),
			new Color32((byte)13, byte.MaxValue, byte.MaxValue, byte.MaxValue),
			new Color32(byte.MaxValue, (byte)244, (byte)0, byte.MaxValue),
			new Color32(byte.MaxValue, (byte)92, (byte)0, byte.MaxValue),
			new Color32((byte)191, (byte)0, byte.MaxValue, byte.MaxValue),
			new Color32(byte.MaxValue, (byte)20, (byte)100, byte.MaxValue),
			new Color32((byte)0, (byte)100, byte.MaxValue, byte.MaxValue),
			new Color32(byte.MaxValue, (byte)209, (byte)220, byte.MaxValue),
			new Color32((byte)200, (byte)245, (byte)220, byte.MaxValue),
			new Color32(byte.MaxValue, (byte)253, (byte)184, byte.MaxValue),
			new Color32((byte)196, (byte)197, byte.MaxValue, byte.MaxValue),
			new Color32(byte.MaxValue, (byte)228, (byte)196, byte.MaxValue),
			new Color32((byte)200, (byte)235, (byte)245, byte.MaxValue),
			new Color32((byte)245, (byte)240, (byte)230, byte.MaxValue),
			new Color32((byte)192, (byte)192, (byte)192, byte.MaxValue),
			new Color32((byte)212, (byte)175, (byte)55, byte.MaxValue),
			new Color32((byte)176, (byte)141, (byte)87, byte.MaxValue),
			new Color32((byte)184, (byte)115, (byte)51, byte.MaxValue),
			new Color32((byte)229, (byte)228, (byte)226, byte.MaxValue)
		};
		CapturedVariables3760_Text_49 = new string[90]
		{
			"White", "Off-White", "Light Gray", "Gray", "Medium Gray", "Dark Gray", "Near Black", "Black", "Red", "Crimson",
			"Firebrick", "Salmon", "Hot Pink", "Deep Pink", "Light Pink", "Pink", "Bubblegum", "Rose", "Coral", "Salmon Pink",
			"Orange", "Dark Orange", "Orange Red", "Tangerine", "Tomato", "Chocolate", "Saddle Brown", "Sienna", "Yellow", "Sunny Yellow",
			"Gold", "Khaki", "Cream", "Green", "Lime Green", "Forest Green", "Dark Green", "Lawn Green", "Green Yellow", "Pale Green",
			"Medium Sea Green", "Emerald", "Dark Olive", "Cyan", "Turquoise", "Medium Turquoise", "Dark Turquoise", "Aquamarine", "Teal", "Powder Blue",
			"Blue", "Dodger Blue", "Deep Sky Blue", "Sky Blue", "Steel Blue", "Cornflower", "Midnight Blue", "Royal Blue", "Pastel Blue", "Purple",
			"Medium Orchid", "Orchid", "Plum", "Medium Purple", "Blue Violet", "Indigo", "Dark Magenta", "Magenta", "Medium Violet Red", "Dark Slate Blue",
			"Neon Green", "Neon Pink", "Neon Cyan", "Neon Yellow", "Neon Orange", "Neon Purple", "Neon Red", "Neon Blue", "Pastel Pink", "Pastel Mint",
			"Pastel Yellow", "Pastel Lavender", "Pastel Peach", "Pastel Sky", "Pastel Cream", "Silver", "Antique Gold", "Bronze", "Copper", "Platinum"
		};
		CapturedVariables3760_Index_56 = 16;
		CapturedVariables3760_Color_25 = new Color32(byte.MaxValue, (byte)0, (byte)127, byte.MaxValue);
		CapturedVariables3760_Text_22 = "Bubblegum";
		CapturedVariables3760_Index_52 = 0;
		CapturedVariables3760_Color_11 = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
		CapturedVariables3760_Text_29 = "White";
		Recovered_Reference_22 = new(float, string)[5]
		{
			(0.25f, "Slow"),
			(0.5f, "Normal"),
			(1f, "Fast"),
			(2f, "Very Fast"),
			(4f, "Strobe")
		};
		CapturedVariables3760_Index_22 = 1;
		CapturedVariables3760_Text_63 = "Normal";
		CapturedVariables3760_Index_09 = 2;
		CapturedVariables3760_Value_04 = 1f;
		CapturedVariables3760_Text_19 = "Normal";
		CapturedVariables3760_Index_15 = 5;
		CapturedVariables3760_Index_01 = 1;
		CapturedVariables3760_Color_14 = new Color32((byte)40, (byte)40, (byte)40, byte.MaxValue);
		CapturedVariables3760_Color_21 = new Color32((byte)230, (byte)230, (byte)230, byte.MaxValue);
		CapturedVariables3760_Text_60 = "Dark Gray";
		CapturedVariables3760_Text_41 = "Off-White";
		Recovered_Reference_09 = new(float, string)[5]
		{
			(0.25f, "Very Slow"),
			(0.5f, "Slow"),
			(1f, "Normal"),
			(2f, "Fast"),
			(4f, "Very Fast")
		};
		CapturedVariables3760_Index_24 = 2;
		CapturedVariables3760_Value_03 = 1f;
		CapturedVariables3760_Text_15 = "Normal";
		CapturedVariables3760_Index_20 = 2;
		CapturedVariables3760_Value_12 = 1f;
		CapturedVariables3760_Text_28 = "Normal";
		CapturedVariables3760_Index_48 = 2;
		CapturedVariables3760_Value_15 = 1f;
		CapturedVariables3760_Text_40 = "Normal";
		CapturedVariables3760_Index_02 = 2;
		CapturedVariables3760_Value_13 = 1f;
		CapturedVariables3760_Text_51 = "Normal";
		CapturedVariables3760_Index_21 = 2;
		CapturedVariables3760_Value_10 = 1f;
		CapturedVariables3760_Text_09 = "Normal";
		CapturedVariables3760_Index_07 = 7;
		CapturedVariables3760_Index_19 = 1;
		CapturedVariables3760_Color_28 = new Color32((byte)0, (byte)0, (byte)0, byte.MaxValue);
		CapturedVariables3760_Color_09 = new Color32((byte)230, (byte)230, (byte)230, byte.MaxValue);
		CapturedVariables3760_Text_03 = "Black";
		CapturedVariables3760_Text_16 = "Off-White";
		CapturedVariables3760_Index_32 = 1;
		CapturedVariables3760_Text_07 = new string[3] { "None", "Minimal", "Everything" };
		Recovered_Reference_23 = new(float, string)[5]
		{
			(0.45f, "Tiny"),
			(0.6f, "Small"),
			(0.75f, "Medium"),
			(0.9f, "Large"),
			(1.1f, "Massive")
		};
		CapturedVariables3760_Index_53 = 1;
		CapturedVariables3760_Text_30 = "Small";
		Recovered_Reference_27 = new(float, string)[5]
		{
			(0f, "None"),
			(0.3f, "Low"),
			(0.5f, "Medium"),
			(0.75f, "High"),
			(1f, "Roundest")
		};
		CapturedVariables3760_Index_54 = 4;
		CapturedVariables3760_Value_01 = 1f;
		CapturedVariables3760_Text_50 = "Roundest";
		CapturedVariables3760_Index_08 = 2;
		CapturedVariables3760_Value_17 = 1f;
		CapturedVariables3760_Text_18 = "Normal";
		CapturedVariables3760_Color_03 = ColorMode.Solid;
		CapturedVariables3760_Index_41 = 16;
		CapturedVariables3760_Index_36 = 1;
		CapturedVariables3760_Color_22 = new Color32(byte.MaxValue, (byte)0, (byte)127, byte.MaxValue);
		CapturedVariables3760_Color_27 = new Color32((byte)230, (byte)230, (byte)230, byte.MaxValue);
		CapturedVariables3760_Text_01 = "Bubblegum";
		CapturedVariables3760_Text_14 = "Off-White";
		CapturedVariables3760_Reference_03 = AccentStripType.Both;
		CapturedVariables3760_Text_08 = new string[3] { "Off", "Top", "Both" };
		CapturedVariables3760_Index_11 = 2;
		CapturedVariables3760_Text_24 = "Both";
		CapturedVariables3760_Index_17 = 6;
		CapturedVariables3760_Index_33 = 5;
		CapturedVariables3760_Color_19 = new Color32((byte)15, (byte)15, (byte)15, byte.MaxValue);
		CapturedVariables3760_Color_02 = new Color32((byte)40, (byte)40, (byte)40, byte.MaxValue);
		CapturedVariables3760_Text_61 = "Near Black";
		CapturedVariables3760_Text_59 = "Dark Gray";
		CapturedVariables3760_Index_45 = 5;
		CapturedVariables3760_Index_06 = 4;
		CapturedVariables3760_Color_26 = new Color32((byte)40, (byte)40, (byte)40, byte.MaxValue);
		CapturedVariables3760_Color_23 = new Color32((byte)80, (byte)80, (byte)80, byte.MaxValue);
		CapturedVariables3760_Text_32 = "Dark Gray";
		CapturedVariables3760_Text_11 = "Medium Gray";
		CapturedVariables3760_Index_03 = 4;
		CapturedVariables3760_Index_30 = 3;
		CapturedVariables3760_Color_08 = new Color32((byte)80, (byte)80, (byte)80, byte.MaxValue);
		CapturedVariables3760_Color_29 = new Color32((byte)128, (byte)128, (byte)128, byte.MaxValue);
		CapturedVariables3760_Text_27 = "Medium Gray";
		CapturedVariables3760_Text_45 = "Gray";
		CapturedVariables3760_Index_46 = 16;
		CapturedVariables3760_Index_13 = 0;
		CapturedVariables3760_Color_17 = new Color32(byte.MaxValue, (byte)0, (byte)127, byte.MaxValue);
		CapturedVariables3760_Color_16 = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
		CapturedVariables3760_Text_48 = "Bubblegum";
		CapturedVariables3760_Text_36 = "White";
		CapturedVariables3760_State_03 = true;
		CapturedVariables3760_Text_25 = new string[4] { "Right Hand", "Left Hand", "Head", "Button" };
		CapturedVariables3760_Index_50 = 0;
		CapturedVariables3760_Text_57 = CapturedVariables3760_Text_25[0];
		Recovered_Reference_11 = new(float, string)[6]
		{
			(10f, "Medium"),
			(14f, "Fast"),
			(18f, "Very Fast"),
			(7.5f, "Mosa"),
			(7f, "Comp"),
			(6f, "Slow")
		};
		CapturedVariables3760_Index_42 = 0;
		CapturedVariables3760_Value_06 = 10f;
		CapturedVariables3760_Value_16 = 2f;
		CapturedVariables3760_Text_10 = "Medium";
		Recovered_Reference_29 = new(float, string)[5]
		{
			(12f, "Medium"),
			(16f, "Fast"),
			(20f, "Very Fast"),
			(4f, "Very Slow"),
			(8f, "Slow")
		};
		CapturedVariables3760_Index_23 = 0;
		CapturedVariables3760_Value_14 = 12f;
		CapturedVariables3760_Text_21 = "Medium";
		Recovered_Reference_21 = new(float, string)[5]
		{
			(100f, "100"),
			(110f, "110"),
			(120f, "120"),
			(80f, "80"),
			(90f, "90")
		};
		CapturedVariables3760_Index_25 = 0;
		CapturedVariables3760_Value_11 = 100f;
		CapturedVariables3760_Text_43 = "100";
		Recovered_Reference_31 = new(float, string)[3]
		{
			(-7.5f, "Medium"),
			(-12.5f, "Strong"),
			(-3f, "Weak")
		};
		CapturedVariables3760_Index_47 = 0;
		CapturedVariables3760_Value_20 = -7.5f;
		CapturedVariables3760_Text_39 = "Medium";
		CapturedVariables3760_Index_05 = 0;
		CapturedVariables3760_Text_53 = new string[10] { "Dawn", "Morning", "Late Morning", "Midday", "Afternoon", "Late Afternoon", "Dusk", "Evening", "Night", "Late Night" };
		CapturedVariables3760_Text_13 = "Dawn";
		Recovered_Reference_05 = BuildLongArmsLengthOptions();
		CapturedVariables3760_Index_55 = 3;
		CapturedVariables3760_Position_01 = new Vector3(1.2f, 1.2f, 1.2f);
		CapturedVariables3760_Text_05 = "1.2";
		CapturedVariables3760_Text_26 = new string[4] { "Normal", "Sticky", "Invisible", "Invisible Sticky" };
		CapturedVariables3760_Index_35 = 0;
		CapturedVariables3760_Text_35 = "Normal";
		CapturedVariables3760_Index_31 = 0;
		CapturedVariables3760_Reference_01 = InputHandler.AlwaysPressed;
		CapturedVariables3760_Text_33 = "None";
		CapturedVariables3760_Values_01 = new ControllerInput[9]
		{
			InputHandler.AlwaysPressed,
			InputHandler.IsRightPrimaryPressed,
			InputHandler.IsLeftPrimaryPressed,
			InputHandler.IsRightSecondaryPressed,
			InputHandler.IsLeftSecondaryPressed,
			InputHandler.IsRightTriggerPressed,
			InputHandler.IsLeftTriggerPressed,
			InputHandler.IsRightGripPressed,
			InputHandler.IsLeftGripPressed
		};
		Recovered_Reference_24 = new(float, string)[4]
		{
			(0.55f, "Default"),
			(0.65f, "Large"),
			(0.75f, "Very Large"),
			(0.45f, "Small")
		};
		CapturedVariables3760_Index_28 = 0;
		CapturedVariables3760_Value_18 = 0.55f;
		CapturedVariables3760_Text_47 = "Default";
		Recovered_Reference_26 = new(float, string)[5]
		{
			(10f, "Slow"),
			(25f, "Medium"),
			(50f, "Fast"),
			(75f, "Very Fast"),
			(150f, "Extreme")
		};
		Recovered_Reference_15 = new(int, string)[3]
		{
			(0, "Regular"),
			(3, "Large"),
			(5, "Max")
		};
		CapturedVariables3760_Index_39 = 1;
		CapturedVariables3760_Index_44 = 2;
		CapturedVariables3760_Value_02 = 25f;
		CapturedVariables3760_Index_29 = 5;
		CapturedVariables3760_Text_12 = "Medium";
		CapturedVariables3760_Text_58 = "Max";
		Recovered_Reference_17 = new(int, float, string)[4]
		{
			(450, 1f, "Regular"),
			(1350, 3f, "Heavy"),
			(3600, 8f, "Extreme"),
			(225, 0.5f, "Light")
		};
		CapturedVariables3760_Index_51 = 0;
		CapturedVariables3760_Index_26 = 450;
		CapturedVariables3760_Value_19 = 1f;
		CapturedVariables3760_Text_52 = "Regular";
		CapturedVariables3760_Text_37 = new string[3] { "Full", "Medium", "Minimal" };
		CapturedVariables3760_Index_18 = 0;
		CapturedVariables3760_Text_44 = "Full";
		CapturedVariables3760_Index_14 = 0;
		CapturedVariables3760_Text_46 = new string[5] { "None", "Wiggly", "Spiral", "Pulse", "Electric" };
		CapturedVariables3760_Index_34 = 2;
		CapturedVariables3760_Text_42 = "Spiral";
		CapturedVariables3760_Index_49 = 8;
		CapturedVariables3760_Index_40 = 33;
		CapturedVariables3760_Index_16 = 50;
		CapturedVariables3760_Index_37 = CapturedVariables3760_Color_01.Length;
		CapturedVariables3760_Color_20 = new Color32(byte.MaxValue, (byte)0, (byte)0, byte.MaxValue);
		CapturedVariables3760_Color_06 = new Color32((byte)0, byte.MaxValue, (byte)0, byte.MaxValue);
		CapturedVariables3760_Color_13 = new Color32((byte)0, (byte)0, byte.MaxValue, byte.MaxValue);
		CapturedVariables3760_Color_07 = new Color32((byte)0, (byte)0, byte.MaxValue, byte.MaxValue);
		CapturedVariables3760_State_01 = true;
		CapturedVariables3760_Text_17 = "Red";
		CapturedVariables3760_Text_06 = "Green";
		CapturedVariables3760_Text_38 = "Blue";
		CapturedVariables3760_Text_54 = "Rainbow";
		Recovered_Reference_30 = new(float, string)[4]
		{
			(0.02f, "Subtle"),
			(0.05f, "Normal"),
			(0.09f, "Intense"),
			(0.15f, "Extreme")
		};
		CapturedVariables3760_Index_27 = 1;
		CapturedVariables3760_Value_08 = 0.05f;
		CapturedVariables3760_Text_04 = "Normal";
		Recovered_Reference_19 = new(float, string)[4]
		{
			(4f, "Slow"),
			(7.5f, "Normal"),
			(12f, "Fast"),
			(20f, "Insane")
		};
		CapturedVariables3760_Index_10 = 1;
		CapturedVariables3760_Value_09 = 7.5f;
		CapturedVariables3760_Text_31 = "Normal";
		Recovered_Reference_28 = new(float, string)[5]
		{
			(1f, "Solid"),
			(0.25f, "Glass"),
			(0.35f, "Low"),
			(0.45f, "Medium"),
			(0.55f, "High")
		};
		CapturedVariables3760_Index_57 = 0;
		CapturedVariables3760_Value_07 = 1f;
		CapturedVariables3760_Text_62 = "Solid";
	}

	public static void CycleLagType(bool forward)
	{
		if (!forward)
		{
			CapturedVariables3760_Index_51 = (CapturedVariables3760_Index_51 - 1 + Recovered_Reference_17.Length) % Recovered_Reference_17.Length;
			(CapturedVariables3760_Index_26, CapturedVariables3760_Value_19, CapturedVariables3760_Text_52) = Recovered_Reference_17[CapturedVariables3760_Index_51];
			CapturedVariables3760_Button_49?.SetText("Lag Type : " + CapturedVariables3760_Text_52);
		}
		else
		{
			CapturedVariables3760_Index_51 = (CapturedVariables3760_Index_51 + 1) % Recovered_Reference_17.Length;
			(CapturedVariables3760_Index_26, CapturedVariables3760_Value_19, CapturedVariables3760_Text_52) = Recovered_Reference_17[CapturedVariables3760_Index_51];
			CapturedVariables3760_Button_49?.SetText("Lag Type : " + CapturedVariables3760_Text_52);
		}
	}

	public static void SetTeamCheckedESPEnabled(bool setActive)
	{
		Variables.Variables_State_07 = setActive;
	}

	public static void CycleTitleColor(bool forward)
	{
		CyclePaletteColor(ref CapturedVariables3760_Index_46, ref CapturedVariables3760_Color_17, ref CapturedVariables3760_Text_48, CapturedVariables3760_Button_54, "Title Color", forward);
	}

	private static void CyclePaletteColor(ref int index, ref Color32 color, ref string desc, ButtonHandler.Button btn, string prefix, bool forward)
	{
		index = (forward ? ((index + 1) % CapturedVariables3760_Color_01.Length) : ((index - 1 + CapturedVariables3760_Color_01.Length) % CapturedVariables3760_Color_01.Length));
		color = CapturedVariables3760_Color_01[index];
		desc = CapturedVariables3760_Text_49[index];
		if (btn != null)
		{
			btn.SetText(prefix + " : " + desc);
			Main.MarkColorsDirty();
		}
		else
		{
			Main.MarkColorsDirty();
		}
	}

	public static void CycleGradientDirection(bool forward)
	{
		if (!forward)
		{
			CapturedVariables3760_Index_12 = (CapturedVariables3760_Index_12 - 1 + CapturedVariables3760_Text_34.Length) % CapturedVariables3760_Text_34.Length;
			CapturedVariables3760_Reference_02 = (GradientDirection)CapturedVariables3760_Index_12;
			CapturedVariables3760_Text_23 = CapturedVariables3760_Text_34[CapturedVariables3760_Index_12];
			ButtonHandler.Button rS5VH6E = CapturedVariables3760_Button_28;
			if (rS5VH6E != null)
			{
				rS5VH6E.SetText("Gradient Direction : " + CapturedVariables3760_Text_23);
				Main.RebuildMenu();
			}
		}
		else
		{
			CapturedVariables3760_Index_12 = (CapturedVariables3760_Index_12 + 1) % CapturedVariables3760_Text_34.Length;
			CapturedVariables3760_Reference_02 = (GradientDirection)CapturedVariables3760_Index_12;
			CapturedVariables3760_Text_23 = CapturedVariables3760_Text_34[CapturedVariables3760_Index_12];
			ButtonHandler.Button rS5VH6E2 = CapturedVariables3760_Button_28;
			if (rS5VH6E2 != null)
			{
				rS5VH6E2.SetText("Gradient Direction : " + CapturedVariables3760_Text_23);
				Main.RebuildMenu();
			}
		}
		Main.RebuildMenu();
	}

	public static void CycleBoardsColor(bool forward)
	{
		CyclePaletteColor(ref CapturedVariables3760_Index_41, ref CapturedVariables3760_Color_22, ref CapturedVariables3760_Text_01, CapturedVariables3760_Button_43, "Boards Color", forward);
	}

	public static void CycleNametagType(bool forward)
	{
		if (!forward)
		{
			CapturedVariables3760_Index_18 = (CapturedVariables3760_Index_18 - 1 + CapturedVariables3760_Text_37.Length) % CapturedVariables3760_Text_37.Length;
			CapturedVariables3760_Text_44 = CapturedVariables3760_Text_37[CapturedVariables3760_Index_18];
			CapturedVariables3760_Button_25?.SetText("Nametag Type : " + CapturedVariables3760_Text_44);
		}
		else
		{
			CapturedVariables3760_Index_18 = (CapturedVariables3760_Index_18 + 1) % CapturedVariables3760_Text_37.Length;
			CapturedVariables3760_Text_44 = CapturedVariables3760_Text_37[CapturedVariables3760_Index_18];
			CapturedVariables3760_Button_25?.SetText("Nametag Type : " + CapturedVariables3760_Text_44);
		}
	}

	public static void CycleBackgroundColor2(bool forward)
	{
		CyclePaletteColor(ref CapturedVariables3760_Index_33, ref CapturedVariables3760_Color_02, ref CapturedVariables3760_Text_59, CapturedVariables3760_Button_59, "Background Color 2", forward);
	}

	private static int FindPaletteColorIndex(string name)
	{
		int num = Array.IndexOf(CapturedVariables3760_Text_49, name);
		if (num < 0)
		{
			return -1;
		}
		return num;
	}

	public static void SetRightHandMenuEnabled(bool setActive)
	{
		Variables.Variables_State_05 = setActive;
	}

	public static void CycleButtonMode(bool forward)
	{
		CycleColorMode(ref CapturedVariables3760_Color_18, delegate(string v)
		{
			CapturedVariables3760_Button_56?.SetText("Button Mode : " + v);
		}, forward);
	}

	private static void AddGradientDirectionSetting(List<ButtonHandler.Button> list, bool show)
	{
		if (show)
		{
			CapturedVariables3760_Button_28 = CreateIncrementalSettingButton("Gradient Direction : " + CapturedVariables3760_Text_23, delegate
			{
				CycleGradientDirection(forward: true);
			}, delegate
			{
				CycleGradientDirection(forward: false);
			});
			list.Add(CapturedVariables3760_Button_28);
		}
	}

	public static void CycleGunHoverColor(bool forward)
	{
		CycleGunColor(ref CapturedVariables3760_Index_16, ref CapturedVariables3760_Color_13, ref CapturedVariables3760_Text_38, ref CapturedVariables3760_State_04, CapturedVariables3760_Button_22, "Gun Hover Color", forward);
	}

	private static void ApplySetting(string key, string value)
	{
		CapturedVariables3760 LocalScope69 = new CapturedVariables3760();
		LocalScope69.value = value;
		string text = key;
		switch (StringHash.Compute(key))
		{
		case 2291700572u:
		{
			if (!(text == "Pinwheel Speed"))
			{
				break;
			}
			int num3 = 0;
			if (num3 >= Recovered_Reference_22.Length)
			{
				break;
			}
			do
			{
				if (Recovered_Reference_22[num3].desc == LocalScope69.value)
				{
					CapturedVariables3760_Index_22 = num3;
					(Main.CapturedVariables1950_Value_06, CapturedVariables3760_Text_63) = Recovered_Reference_22[num3];
					CapturedVariables3760_Button_52?.SetText("Pinwheel Speed : " + CapturedVariables3760_Text_63);
					break;
				}
				num3++;
			}
			while (num3 < Recovered_Reference_22.Length);
			break;
		}
		case 2577346559u:
			if (text == "Tracer Position")
			{
				CapturedVariables3760_Index_50 = Array.IndexOf(CapturedVariables3760_Text_25, LocalScope69.value);
				if (CapturedVariables3760_Index_50 >= 0)
				{
					CapturedVariables3760_Text_57 = CapturedVariables3760_Text_25[CapturedVariables3760_Index_50];
					CapturedVariables3760_Button_62?.SetText("Tracer Position : " + CapturedVariables3760_Text_57);
				}
			}
			break;
		case 1762217679u:
		{
			if (!(text == "Boost Speed"))
			{
				break;
			}
			int num15 = 0;
			if (num15 >= Recovered_Reference_11.Length)
			{
				break;
			}
			do
			{
				if (Recovered_Reference_11[num15].desc == LocalScope69.value)
				{
					CapturedVariables3760_Index_42 = num15;
					(float speed, string desc) tuple6 = Recovered_Reference_11[num15];
					CapturedVariables3760_Value_06 = tuple6.speed;
					CapturedVariables3760_Text_10 = tuple6.desc;
					CapturedVariables3760_Value_16 = CapturedVariables3760_Value_06 / 5f;
					CapturedVariables3760_Button_63?.SetText("Boost Speed : " + CapturedVariables3760_Text_10);
					break;
				}
				num15++;
			}
			while (num15 < Recovered_Reference_11.Length);
			break;
		}
		case 2686507609u:
		{
			if (!(text == "Fly Speed"))
			{
				break;
			}
			int num27 = 0;
			if (num27 >= Recovered_Reference_29.Length)
			{
				break;
			}
			do
			{
				if (Recovered_Reference_29[num27].desc == LocalScope69.value)
				{
					CapturedVariables3760_Index_23 = num27;
					(CapturedVariables3760_Value_14, CapturedVariables3760_Text_21) = Recovered_Reference_29[num27];
					CapturedVariables3760_Button_48?.SetText("Fly Speed : " + CapturedVariables3760_Text_21);
					break;
				}
				num27++;
			}
			while (num27 < Recovered_Reference_29.Length);
			break;
		}
		case 1068355979u:
		{
			if (!(text == "FPC FOV"))
			{
				break;
			}
			int num42 = 0;
			if (num42 >= Recovered_Reference_21.Length)
			{
				break;
			}
			do
			{
				if (Recovered_Reference_21[num42].desc == LocalScope69.value)
				{
					CapturedVariables3760_Index_25 = num42;
					(CapturedVariables3760_Value_11, CapturedVariables3760_Text_43) = Recovered_Reference_21[num42];
					CapturedVariables3760_Button_06?.SetText("FPC FOV : " + CapturedVariables3760_Text_43);
					break;
				}
				num42++;
			}
			while (num42 < Recovered_Reference_21.Length);
			break;
		}
		case 3721364487u:
		{
			if (!(text == "Wall Walk Strength"))
			{
				break;
			}
			int num17 = 0;
			if (num17 >= Recovered_Reference_31.Length)
			{
				break;
			}
			do
			{
				if (Recovered_Reference_31[num17].desc == LocalScope69.value)
				{
					CapturedVariables3760_Index_47 = num17;
					(CapturedVariables3760_Value_20, CapturedVariables3760_Text_39) = Recovered_Reference_31[num17];
					CapturedVariables3760_Button_60?.SetText("Wall Walk Strength : " + CapturedVariables3760_Text_39);
					break;
				}
				num17++;
			}
			while (num17 < Recovered_Reference_31.Length);
			break;
		}
		case 2717963726u:
		{
			if (!(text == "Long Arms Length"))
			{
				break;
			}
			int num45 = 0;
			if (num45 >= Recovered_Reference_05.Length)
			{
				break;
			}
			do
			{
				if (Recovered_Reference_05[num45].desc == LocalScope69.value)
				{
					CapturedVariables3760_Index_55 = num45;
					(CapturedVariables3760_Position_01, CapturedVariables3760_Text_05) = Recovered_Reference_05[num45];
					CapturedVariables3760_Button_21?.SetText("Long Arms Length : " + CapturedVariables3760_Text_05);
					break;
				}
				num45++;
			}
			while (num45 < Recovered_Reference_05.Length);
			break;
		}
		case 2096043750u:
			if (text == "Platform Type")
			{
				CapturedVariables3760_Index_35 = Array.IndexOf(CapturedVariables3760_Text_26, LocalScope69.value);
				if (CapturedVariables3760_Index_35 >= 0)
				{
					CapturedVariables3760_Text_35 = CapturedVariables3760_Text_26[CapturedVariables3760_Index_35];
					CapturedVariables3760_Button_41?.SetText("Platform Type : " + CapturedVariables3760_Text_35);
				}
			}
			break;
		case 228428944u:
		{
			if (!(text == "Sound Input"))
			{
				break;
			}
			int num7 = 0;
			if (num7 >= CapturedVariables3760_Values_01.Length)
			{
				break;
			}
			do
			{
				if (CapturedVariables3760_Values_01[num7].Method.Name == LocalScope69.value)
				{
					CapturedVariables3760_Index_31 = num7;
					CapturedVariables3760_Reference_01 = CapturedVariables3760_Values_01[num7];
					CapturedVariables3760_Text_33 = LocalScope69.value;
					CapturedVariables3760_Button_05?.SetText("Sound Input : " + CapturedVariables3760_Text_33);
					break;
				}
				num7++;
			}
			while (num7 < CapturedVariables3760_Values_01.Length);
			break;
		}
		case 1661479433u:
		{
			if (!(text == "Anti Report Radius"))
			{
				break;
			}
			int num48 = 0;
			if (num48 >= Recovered_Reference_24.Length)
			{
				break;
			}
			do
			{
				if (Recovered_Reference_24[num48].desc == LocalScope69.value)
				{
					CapturedVariables3760_Index_28 = num48;
					(CapturedVariables3760_Value_18, CapturedVariables3760_Text_47) = Recovered_Reference_24[num48];
					CapturedVariables3760_Button_44?.SetText("Anti Report Radius : " + CapturedVariables3760_Text_47);
					break;
				}
				num48++;
			}
			while (num48 < Recovered_Reference_24.Length);
			break;
		}
		case 2137873809u:
		{
			if (!(text == "Projectile Speed"))
			{
				break;
			}
			int num34 = 0;
			if (num34 >= Recovered_Reference_26.Length)
			{
				break;
			}
			do
			{
				if (Recovered_Reference_26[num34].desc == LocalScope69.value)
				{
					CapturedVariables3760_Index_39 = num34;
					(CapturedVariables3760_Value_02, CapturedVariables3760_Text_12) = Recovered_Reference_26[num34];
					CapturedVariables3760_Button_51?.SetText("Projectile Speed : " + CapturedVariables3760_Text_12);
					break;
				}
				num34++;
			}
			while (num34 < Recovered_Reference_26.Length);
			break;
		}
		case 1779075983u:
		{
			if (!(text == "Projectile Size"))
			{
				break;
			}
			int num24 = 0;
			if (num24 >= Recovered_Reference_15.Length)
			{
				break;
			}
			do
			{
				if (Recovered_Reference_15[num24].desc == LocalScope69.value)
				{
					CapturedVariables3760_Index_44 = num24;
					(CapturedVariables3760_Index_29, CapturedVariables3760_Text_58) = Recovered_Reference_15[num24];
					CapturedVariables3760_Button_29?.SetText("Projectile Size : " + CapturedVariables3760_Text_58);
					break;
				}
				num24++;
			}
			while (num24 < Recovered_Reference_15.Length);
			break;
		}
		case 650562767u:
		{
			if (!(text == "Menu Font"))
			{
				break;
			}
			int num13 = 0;
			if (num13 >= Main.CapturedVariables1950_Reference_02.Count)
			{
				break;
			}
			do
			{
				if (Main.CapturedVariables1950_Reference_02[num13].Description == LocalScope69.value)
				{
					CurrentFontIndex = num13;
					CapturedVariables3760_Button_64?.SetText("Menu Font : " + CurrentFontDescription);
					break;
				}
				num13++;
			}
			while (num13 < Main.CapturedVariables1950_Reference_02.Count);
			break;
		}
		case 3411391708u:
		{
			if (!(text == "Click Sound"))
			{
				break;
			}
			bool flag = false;
			int num40 = 0;
			if (num40 < ButtonHandler.CapturedVariables570_Items_03.Count)
			{
				do
				{
					if (ButtonHandler.CapturedVariables570_Items_03[num40].Description == LocalScope69.value)
					{
						CapturedVariables3760_Index_43 = num40;
						ButtonHandler.Button w5I2B2XX = CapturedVariables3760_Button_37;
						if (w5I2B2XX != null)
						{
							w5I2B2XX.SetText("Click Sound : " + ClickSoundDescription);
							flag = true;
						}
						else
						{
							flag = true;
						}
						break;
					}
					num40++;
				}
				while (num40 < ButtonHandler.CapturedVariables570_Items_03.Count);
			}
			if (!flag)
			{
				CapturedVariables3760_Index_43 = 0;
				CapturedVariables3760_Button_37?.SetText("Click Sound : " + ClickSoundDescription);
			}
			break;
		}
		case 910628169u:
		{
			if (!(text == "Lag Type"))
			{
				break;
			}
			int num31 = 0;
			if (num31 >= Recovered_Reference_17.Length)
			{
				break;
			}
			do
			{
				if (Recovered_Reference_17[num31].desc == LocalScope69.value)
				{
					CapturedVariables3760_Index_51 = num31;
					(CapturedVariables3760_Index_26, CapturedVariables3760_Value_19, CapturedVariables3760_Text_52) = Recovered_Reference_17[num31];
					CapturedVariables3760_Button_49?.SetText("Lag Type : " + CapturedVariables3760_Text_52);
					break;
				}
				num31++;
			}
			while (num31 < Recovered_Reference_17.Length);
			break;
		}
		case 2660151666u:
			if (text == "Nametag Type")
			{
				CapturedVariables3760_Index_18 = Array.IndexOf(CapturedVariables3760_Text_37, LocalScope69.value);
				if (CapturedVariables3760_Index_18 >= 0)
				{
					CapturedVariables3760_Text_44 = CapturedVariables3760_Text_37[CapturedVariables3760_Index_18];
					CapturedVariables3760_Button_25?.SetText("Nametag Type : " + CapturedVariables3760_Text_44);
				}
			}
			break;
		case 4268863127u:
			if (text == "Gun Animation")
			{
				CapturedVariables3760_Index_34 = Array.IndexOf(CapturedVariables3760_Text_46, LocalScope69.value);
				if (CapturedVariables3760_Index_34 >= 0)
				{
					CapturedVariables3760_Text_42 = CapturedVariables3760_Text_46[CapturedVariables3760_Index_34];
					CapturedVariables3760_Button_57?.SetText("Gun Animation : " + CapturedVariables3760_Text_42);
				}
			}
			break;
		case 3760116134u:
			if (text == "Gun Idle Color")
			{
				LoadGunColor(LocalScope69.value, ref CapturedVariables3760_Index_49, ref CapturedVariables3760_Color_20, ref CapturedVariables3760_Text_17, ref CapturedVariables3760_State_05, CapturedVariables3760_Button_32, "Gun Idle Color");
			}
			break;
		case 3870959124u:
			if (text == "Gun Fire Color")
			{
				LoadGunColor(LocalScope69.value, ref CapturedVariables3760_Index_40, ref CapturedVariables3760_Color_06, ref CapturedVariables3760_Text_06, ref CapturedVariables3760_State_02, CapturedVariables3760_Button_23, "Gun Fire Color");
			}
			break;
		case 1927230048u:
			if (text == "Gun Hover Color")
			{
				LoadGunColor(LocalScope69.value, ref CapturedVariables3760_Index_16, ref CapturedVariables3760_Color_13, ref CapturedVariables3760_Text_38, ref CapturedVariables3760_State_04, CapturedVariables3760_Button_22, "Gun Hover Color");
			}
			break;
		case 985328415u:
			if (text == "Gun Lock Color")
			{
				LoadGunColor(LocalScope69.value, ref CapturedVariables3760_Index_37, ref CapturedVariables3760_Color_07, ref CapturedVariables3760_Text_54, ref CapturedVariables3760_State_01, CapturedVariables3760_Button_38, "Gun Lock Color");
			}
			break;
		case 3745122429u:
		{
			if (!(text == "Gun Pointer Size"))
			{
				break;
			}
			int num39 = 0;
			if (num39 >= Recovered_Reference_20.Length)
			{
				break;
			}
			do
			{
				if (Recovered_Reference_20[num39].d == LocalScope69.value)
				{
					CapturedVariables3760_Index_04 = num39;
					(CapturedVariables3760_Value_05, CapturedVariables3760_Text_20) = Recovered_Reference_20[num39];
					CapturedVariables3760_Button_40?.SetText("Gun Pointer Size : " + CapturedVariables3760_Text_20);
					break;
				}
				num39++;
			}
			while (num39 < Recovered_Reference_20.Length);
			break;
		}
		case 2806915215u:
		{
			if (!(text == "Gun Line Thickness"))
			{
				break;
			}
			int num32 = 0;
			if (num32 >= Recovered_Reference_12.Length)
			{
				break;
			}
			do
			{
				if (Recovered_Reference_12[num32].d == LocalScope69.value)
				{
					CapturedVariables3760_Index_38 = num32;
					(CapturedVariables3760_Value_21, CapturedVariables3760_Text_55) = Recovered_Reference_12[num32];
					CapturedVariables3760_Button_55?.SetText("Gun Line Thickness : " + CapturedVariables3760_Text_55);
					break;
				}
				num32++;
			}
			while (num32 < Recovered_Reference_12.Length);
			break;
		}
		case 4034050019u:
		{
			if (!(text == "Gun Wiggle Intensity"))
			{
				break;
			}
			int num26 = 0;
			if (num26 >= Recovered_Reference_30.Length)
			{
				break;
			}
			do
			{
				if (Recovered_Reference_30[num26].d == LocalScope69.value)
				{
					CapturedVariables3760_Index_27 = num26;
					(CapturedVariables3760_Value_08, CapturedVariables3760_Text_04) = Recovered_Reference_30[num26];
					CapturedVariables3760_Button_30?.SetText("Wiggle Intensity : " + CapturedVariables3760_Text_04);
					break;
				}
				num26++;
			}
			while (num26 < Recovered_Reference_30.Length);
			break;
		}
		case 1810651109u:
		{
			if (!(text == "Gun Wiggle Speed"))
			{
				break;
			}
			int num22 = 0;
			if (num22 >= Recovered_Reference_19.Length)
			{
				break;
			}
			do
			{
				if (Recovered_Reference_19[num22].d == LocalScope69.value)
				{
					CapturedVariables3760_Index_10 = num22;
					(CapturedVariables3760_Value_09, CapturedVariables3760_Text_31) = Recovered_Reference_19[num22];
					CapturedVariables3760_Button_07?.SetText("Wiggle Speed : " + CapturedVariables3760_Text_31);
					break;
				}
				num22++;
			}
			while (num22 < Recovered_Reference_19.Length);
			break;
		}
		case 1359913322u:
			if (text == "Nextbot Speed")
			{
				Nextbots.SelectNextbotSpeed(LocalScope69.value);
			}
			break;
		case 136723274u:
			if (text == "Nextbot Behaviour")
			{
				Nextbots.SelectNextbotBehaviour(LocalScope69.value);
			}
			break;
		case 2776092301u:
			if (text == "Current Projectile")
			{
				Projectile.SelectProjectile(LocalScope69.value);
			}
			break;
		case 2885792086u:
			if (text == "Background Color")
			{
				int num6 = FindPaletteColorIndex(LocalScope69.value);
				if (num6 >= 0)
				{
					CapturedVariables3760_Index_17 = num6;
					CapturedVariables3760_Color_19 = CapturedVariables3760_Color_01[num6];
					CapturedVariables3760_Text_61 = CapturedVariables3760_Text_49[num6];
					CapturedVariables3760_Button_11?.SetText("Background Color : " + CapturedVariables3760_Text_61);
				}
			}
			break;
		case 1882213840u:
			if (text == "Background Color 2")
			{
				int num49 = FindPaletteColorIndex(LocalScope69.value);
				if (num49 >= 0)
				{
					CapturedVariables3760_Index_33 = num49;
					CapturedVariables3760_Color_02 = CapturedVariables3760_Color_01[num49];
					CapturedVariables3760_Text_59 = CapturedVariables3760_Text_49[num49];
					CapturedVariables3760_Button_59?.SetText("Background Color 2 : " + CapturedVariables3760_Text_59);
				}
			}
			break;
		case 1536338919u:
		{
			if (!(text == "Pinwheel Color 1"))
			{
				break;
			}
			int num43 = FindPaletteColorIndex(LocalScope69.value);
			if (num43 >= 0)
			{
				CapturedVariables3760_Index_56 = num43;
				CapturedVariables3760_Color_25 = CapturedVariables3760_Color_01[num43];
				CapturedVariables3760_Text_22 = CapturedVariables3760_Text_49[num43];
				ButtonHandler.Button bCEN84FX = CapturedVariables3760_Button_02;
				if (bCEN84FX != null)
				{
					bCEN84FX.SetText("Pinwheel Color 1 : " + CapturedVariables3760_Text_22);
					Main.CapturedVariables1950_Color_02 = (Color32)(CapturedVariables3760_Color_25);
				}
				else
				{
					Main.CapturedVariables1950_Color_02 = (Color32)(CapturedVariables3760_Color_25);
				}
			}
			break;
		}
		case 1553116538u:
		{
			if (!(text == "Pinwheel Color 2"))
			{
				break;
			}
			int num37 = FindPaletteColorIndex(LocalScope69.value);
			if (num37 >= 0)
			{
				CapturedVariables3760_Index_52 = num37;
				CapturedVariables3760_Color_11 = CapturedVariables3760_Color_01[num37];
				CapturedVariables3760_Text_29 = CapturedVariables3760_Text_49[num37];
				ButtonHandler.Button vQMZHJDL = CapturedVariables3760_Button_36;
				if (vQMZHJDL != null)
				{
					vQMZHJDL.SetText("Pinwheel Color 2 : " + CapturedVariables3760_Text_29);
					Main.CapturedVariables1950_Color_06 = (Color32)(CapturedVariables3760_Color_11);
				}
				else
				{
					Main.CapturedVariables1950_Color_06 = (Color32)(CapturedVariables3760_Color_11);
				}
			}
			break;
		}
		case 2426569570u:
			if (text == "Button Color")
			{
				int num28 = FindPaletteColorIndex(LocalScope69.value);
				if (num28 >= 0)
				{
					CapturedVariables3760_Index_45 = num28;
					CapturedVariables3760_Color_26 = CapturedVariables3760_Color_01[num28];
					CapturedVariables3760_Text_32 = CapturedVariables3760_Text_49[num28];
					CapturedVariables3760_Button_14?.SetText("Button Color : " + CapturedVariables3760_Text_32);
				}
			}
			break;
		case 2243505596u:
			if (text == "Button Color 2")
			{
				int num23 = FindPaletteColorIndex(LocalScope69.value);
				if (num23 >= 0)
				{
					CapturedVariables3760_Index_06 = num23;
					CapturedVariables3760_Color_23 = CapturedVariables3760_Color_01[num23];
					CapturedVariables3760_Text_11 = CapturedVariables3760_Text_49[num23];
					CapturedVariables3760_Button_50?.SetText("Button Color 2 : " + CapturedVariables3760_Text_11);
				}
			}
			break;
		case 3171427017u:
			if (text == "Enabled Button Color")
			{
				int num21 = FindPaletteColorIndex(LocalScope69.value);
				if (num21 >= 0)
				{
					CapturedVariables3760_Index_03 = num21;
					CapturedVariables3760_Color_08 = CapturedVariables3760_Color_01[num21];
					CapturedVariables3760_Text_27 = CapturedVariables3760_Text_49[num21];
					CapturedVariables3760_Button_18?.SetText("Enabled Button Color : " + CapturedVariables3760_Text_27);
				}
			}
			break;
		case 1925948411u:
			if (text == "Enabled Button Color 2")
			{
				int num16 = FindPaletteColorIndex(LocalScope69.value);
				if (num16 >= 0)
				{
					CapturedVariables3760_Index_30 = num16;
					CapturedVariables3760_Color_29 = CapturedVariables3760_Color_01[num16];
					CapturedVariables3760_Text_45 = CapturedVariables3760_Text_49[num16];
					CapturedVariables3760_Button_61?.SetText("Enabled Button Color 2 : " + CapturedVariables3760_Text_45);
				}
			}
			break;
		case 1385757802u:
			if (text == "Title Color")
			{
				int num11 = FindPaletteColorIndex(LocalScope69.value);
				if (num11 >= 0)
				{
					CapturedVariables3760_Index_46 = num11;
					CapturedVariables3760_Color_17 = CapturedVariables3760_Color_01[num11];
					CapturedVariables3760_Text_48 = CapturedVariables3760_Text_49[num11];
					CapturedVariables3760_Button_54?.SetText("Title Color : " + CapturedVariables3760_Text_48);
				}
			}
			break;
		case 2341367716u:
			if (text == "Title Color 2")
			{
				int num5 = FindPaletteColorIndex(LocalScope69.value);
				if (num5 >= 0)
				{
					CapturedVariables3760_Index_13 = num5;
					CapturedVariables3760_Color_16 = CapturedVariables3760_Color_01[num5];
					CapturedVariables3760_Text_36 = CapturedVariables3760_Text_49[num5];
					CapturedVariables3760_Button_31?.SetText("Title Color 2 : " + CapturedVariables3760_Text_36);
				}
			}
			break;
		case 2441448630u:
			if (text == "Outline Color")
			{
				int num51 = FindPaletteColorIndex(LocalScope69.value);
				if (num51 >= 0)
				{
					CapturedVariables3760_Index_07 = num51;
					CapturedVariables3760_Color_28 = CapturedVariables3760_Color_01[num51];
					CapturedVariables3760_Text_03 = CapturedVariables3760_Text_49[num51];
					CapturedVariables3760_Button_27?.SetText("Outline Color : " + CapturedVariables3760_Text_03);
				}
			}
			break;
		case 2683439408u:
			if (text == "Outline Color 2")
			{
				int num46 = FindPaletteColorIndex(LocalScope69.value);
				if (num46 >= 0)
				{
					CapturedVariables3760_Index_19 = num46;
					CapturedVariables3760_Color_09 = CapturedVariables3760_Color_01[num46];
					CapturedVariables3760_Text_16 = CapturedVariables3760_Text_49[num46];
					CapturedVariables3760_Button_04?.SetText("Outline Color 2 : " + CapturedVariables3760_Text_16);
				}
			}
			break;
		case 277209181u:
			if (text == "Outline")
			{
				CapturedVariables3760_Index_32 = Array.IndexOf(CapturedVariables3760_Text_07, LocalScope69.value);
				if (CapturedVariables3760_Index_32 < 0)
				{
					CapturedVariables3760_Index_32 = 1;
					CapturedVariables3760_Button_16?.SetText("Outline : " + CapturedVariables3760_Text_07[CapturedVariables3760_Index_32]);
				}
				else
				{
					CapturedVariables3760_Button_16?.SetText("Outline : " + CapturedVariables3760_Text_07[CapturedVariables3760_Index_32]);
				}
			}
			break;
		case 1337498208u:
			if (text == "Background Mode")
			{
				int num35 = FindColorModeIndex(LocalScope69.value);
				if (num35 >= 0)
				{
					CapturedVariables3760_Color_10 = (ColorMode)num35;
					CapturedVariables3760_Button_13?.SetText("Background Mode : " + CapturedVariables3760_Text_02[num35]);
				}
			}
			break;
		case 3132752780u:
			if (text == "Button Mode")
			{
				int num29 = FindColorModeIndex(LocalScope69.value);
				if (num29 >= 0)
				{
					CapturedVariables3760_Color_18 = (ColorMode)num29;
					CapturedVariables3760_Button_56?.SetText("Button Mode : " + CapturedVariables3760_Text_02[num29]);
				}
			}
			break;
		case 611552929u:
			if (text == "Enabled Mode")
			{
				int num25 = FindColorModeIndex(LocalScope69.value);
				if (num25 >= 0)
				{
					CapturedVariables3760_Color_05 = (ColorMode)num25;
					CapturedVariables3760_Button_03?.SetText("Enabled Mode : " + CapturedVariables3760_Text_02[num25]);
				}
			}
			break;
		case 1324701876u:
		{
			if (!(text == "Title Mode"))
			{
				break;
			}
			int num19 = FindColorModeIndex(LocalScope69.value);
			if (num19 >= 0)
			{
				if (num19 != 4)
				{
					CapturedVariables3760_Color_15 = (ColorMode)num19;
					CapturedVariables3760_Button_47?.SetText("Title Mode : " + CapturedVariables3760_Text_02[(int)CapturedVariables3760_Color_15]);
				}
				else
				{
					CapturedVariables3760_Color_15 = ColorMode.Gradient;
					CapturedVariables3760_Button_47?.SetText("Title Mode : " + CapturedVariables3760_Text_02[(int)CapturedVariables3760_Color_15]);
				}
			}
			break;
		}
		case 3086143062u:
			if (text == "Title Animated Gradient")
			{
				CapturedVariables3760_State_03 = LocalScope69.value == "On";
				CapturedVariables3760_Button_35?.SetText("Animated Gradient : " + (CapturedVariables3760_State_03 ? "On" : "Off"));
			}
			break;
		case 3679710528u:
			if (text == "Outline Mode")
			{
				int num10 = FindColorModeIndex(LocalScope69.value);
				if (num10 >= 0)
				{
					CapturedVariables3760_Color_04 = (ColorMode)num10;
					CapturedVariables3760_Button_09?.SetText("Outline Mode : " + CapturedVariables3760_Text_02[num10]);
				}
			}
			break;
		case 2784139485u:
		{
			if (!(text == "Outline Anim Speed"))
			{
				break;
			}
			int num8 = 0;
			if (num8 >= Recovered_Reference_09.Length)
			{
				break;
			}
			do
			{
				if (Recovered_Reference_09[num8].desc == LocalScope69.value)
				{
					CapturedVariables3760_Index_24 = num8;
					(CapturedVariables3760_Value_03, CapturedVariables3760_Text_15) = Recovered_Reference_09[num8];
					CapturedVariables3760_Button_58?.SetText("Animation Speed : " + CapturedVariables3760_Text_15);
					break;
				}
				num8++;
			}
			while (num8 < Recovered_Reference_09.Length);
			break;
		}
		case 3430161597u:
		{
			if (!(text == "Background Anim Speed"))
			{
				break;
			}
			int num4 = 0;
			if (num4 >= Recovered_Reference_09.Length)
			{
				break;
			}
			do
			{
				if (Recovered_Reference_09[num4].desc == LocalScope69.value)
				{
					CapturedVariables3760_Index_20 = num4;
					(CapturedVariables3760_Value_12, CapturedVariables3760_Text_28) = Recovered_Reference_09[num4];
					CapturedVariables3760_Button_08?.SetText("Animation Speed : " + CapturedVariables3760_Text_28);
					break;
				}
				num4++;
			}
			while (num4 < Recovered_Reference_09.Length);
			break;
		}
		case 3463069697u:
		{
			if (!(text == "Button Anim Speed"))
			{
				break;
			}
			int num2 = 0;
			if (num2 >= Recovered_Reference_09.Length)
			{
				break;
			}
			do
			{
				if (Recovered_Reference_09[num2].desc == LocalScope69.value)
				{
					CapturedVariables3760_Index_48 = num2;
					(CapturedVariables3760_Value_15, CapturedVariables3760_Text_40) = Recovered_Reference_09[num2];
					CapturedVariables3760_Button_15?.SetText("Animation Speed : " + CapturedVariables3760_Text_40);
					break;
				}
				num2++;
			}
			while (num2 < Recovered_Reference_09.Length);
			break;
		}
		case 1099148184u:
		{
			if (!(text == "Enabled Button Anim Speed"))
			{
				break;
			}
			int num50 = 0;
			if (num50 >= Recovered_Reference_09.Length)
			{
				break;
			}
			do
			{
				if (Recovered_Reference_09[num50].desc == LocalScope69.value)
				{
					CapturedVariables3760_Index_02 = num50;
					(CapturedVariables3760_Value_13, CapturedVariables3760_Text_51) = Recovered_Reference_09[num50];
					CapturedVariables3760_Button_46?.SetText("Animation Speed : " + CapturedVariables3760_Text_51);
					break;
				}
				num50++;
			}
			while (num50 < Recovered_Reference_09.Length);
			break;
		}
		case 4095354297u:
		{
			if (!(text == "Title Anim Speed"))
			{
				break;
			}
			int num47 = 0;
			if (num47 >= Recovered_Reference_09.Length)
			{
				break;
			}
			do
			{
				if (Recovered_Reference_09[num47].desc == LocalScope69.value)
				{
					CapturedVariables3760_Index_21 = num47;
					(CapturedVariables3760_Value_10, CapturedVariables3760_Text_09) = Recovered_Reference_09[num47];
					CapturedVariables3760_Button_12?.SetText("Animation Speed : " + CapturedVariables3760_Text_09);
					break;
				}
				num47++;
			}
			while (num47 < Recovered_Reference_09.Length);
			break;
		}
		case 2986032072u:
			if (text == "Accent Color")
			{
				int num44 = FindPaletteColorIndex(LocalScope69.value);
				if (num44 >= 0)
				{
					CapturedVariables3760_Index_15 = num44;
					CapturedVariables3760_Color_14 = CapturedVariables3760_Color_01[num44];
					CapturedVariables3760_Text_60 = CapturedVariables3760_Text_49[num44];
					CapturedVariables3760_Button_10?.SetText("Accent Color : " + CapturedVariables3760_Text_60);
				}
			}
			break;
		case 1952963006u:
			if (text == "Accent Color 2")
			{
				int num41 = FindPaletteColorIndex(LocalScope69.value);
				if (num41 >= 0)
				{
					CapturedVariables3760_Index_01 = num41;
					CapturedVariables3760_Color_21 = CapturedVariables3760_Color_01[num41];
					CapturedVariables3760_Text_41 = CapturedVariables3760_Text_49[num41];
					CapturedVariables3760_Button_26?.SetText("Accent Color 2 : " + CapturedVariables3760_Text_41);
				}
			}
			break;
		case 2372487234u:
			if (text == "Accent Mode")
			{
				int num38 = FindColorModeIndex(LocalScope69.value);
				if (num38 >= 0)
				{
					CapturedVariables3760_Color_12 = (ColorMode)num38;
					CapturedVariables3760_Button_33?.SetText("Accent Mode : " + CapturedVariables3760_Text_02[num38]);
				}
			}
			break;
		case 2024505291u:
		{
			if (!(text == "Accent Anim Speed"))
			{
				break;
			}
			int num36 = 0;
			if (num36 >= Recovered_Reference_09.Length)
			{
				break;
			}
			do
			{
				if (Recovered_Reference_09[num36].desc == LocalScope69.value)
				{
					CapturedVariables3760_Index_09 = num36;
					(CapturedVariables3760_Value_04, CapturedVariables3760_Text_19) = Recovered_Reference_09[num36];
					CapturedVariables3760_Button_20?.SetText("Animation Speed : " + CapturedVariables3760_Text_19);
					break;
				}
				num36++;
			}
			while (num36 < Recovered_Reference_09.Length);
			break;
		}
		case 2834318447u:
		{
			if (!(text == "Menu Size"))
			{
				break;
			}
			int num33 = 0;
			if (num33 >= Recovered_Reference_23.Length)
			{
				break;
			}
			do
			{
				if (Recovered_Reference_23[num33].desc == LocalScope69.value)
				{
					CapturedVariables3760_Index_53 = num33;
					(Main.CapturedVariables1950_Value_04, CapturedVariables3760_Text_30) = Recovered_Reference_23[num33];
					CapturedVariables3760_Button_39?.SetText("Menu Size : " + CapturedVariables3760_Text_30);
					break;
				}
				num33++;
			}
			while (num33 < Recovered_Reference_23.Length);
			break;
		}
		case 3088227852u:
		{
			if (!(text == "Roundness"))
			{
				break;
			}
			int num30 = 0;
			if (num30 >= Recovered_Reference_27.Length)
			{
				break;
			}
			do
			{
				if (Recovered_Reference_27[num30].desc == LocalScope69.value)
				{
					CapturedVariables3760_Index_54 = num30;
					(CapturedVariables3760_Value_01, CapturedVariables3760_Text_50) = Recovered_Reference_27[num30];
					CapturedVariables3760_Button_19?.SetText("Roundness : " + CapturedVariables3760_Text_50);
					break;
				}
				num30++;
			}
			while (num30 < Recovered_Reference_27.Length);
			break;
		}
		case 3453248331u:
			if (text == "Accent Strip Type")
			{
				CapturedVariables3760_Index_11 = Array.IndexOf(CapturedVariables3760_Text_08, LocalScope69.value);
				if (CapturedVariables3760_Index_11 >= 0)
				{
					CapturedVariables3760_Reference_03 = (AccentStripType)CapturedVariables3760_Index_11;
					CapturedVariables3760_Text_24 = CapturedVariables3760_Text_08[CapturedVariables3760_Index_11];
					CapturedVariables3760_Button_01?.SetText("Accent Strip : " + CapturedVariables3760_Text_24);
				}
			}
			break;
		case 3367343891u:
			if (!(text == "Time Of Day"))
			{
				break;
			}
			CapturedVariables3760_Index_05 = Array.IndexOf(CapturedVariables3760_Text_53, LocalScope69.value);
			if (CapturedVariables3760_Index_05 >= 0)
			{
				CapturedVariables3760_Text_13 = CapturedVariables3760_Text_53[CapturedVariables3760_Index_05];
				BetterDayNightManager instance = BetterDayNightManager.instance;
				if (instance != null)
				{
					((BetterDayNightManager)instance).SetTimeOfDay(CapturedVariables3760_Index_05);
					CapturedVariables3760_Button_17?.SetText("Time Of Day : " + CapturedVariables3760_Text_13);
				}
				else
				{
					CapturedVariables3760_Button_17?.SetText("Time Of Day : " + CapturedVariables3760_Text_13);
				}
			}
			break;
		case 3726574598u:
		{
			if (!(text == "Opacity"))
			{
				break;
			}
			int num20 = 0;
			if (num20 >= Recovered_Reference_28.Length)
			{
				break;
			}
			do
			{
				if (Recovered_Reference_28[num20].desc == LocalScope69.value)
				{
					CapturedVariables3760_Index_57 = num20;
					(CapturedVariables3760_Value_07, CapturedVariables3760_Text_62) = Recovered_Reference_28[num20];
					CapturedVariables3760_Button_42?.SetText("Opacity : " + CapturedVariables3760_Text_62);
					break;
				}
				num20++;
			}
			while (num20 < Recovered_Reference_28.Length);
			break;
		}
		case 457583183u:
			if (text == "Boards Color")
			{
				int num18 = FindPaletteColorIndex(LocalScope69.value);
				if (num18 >= 0)
				{
					CapturedVariables3760_Index_41 = num18;
					CapturedVariables3760_Color_22 = CapturedVariables3760_Color_01[num18];
					CapturedVariables3760_Text_01 = CapturedVariables3760_Text_49[num18];
					CapturedVariables3760_Button_43?.SetText("Boards Color : " + CapturedVariables3760_Text_01);
				}
			}
			break;
		case 1088292125u:
			if (text == "Boards Color 2")
			{
				int num14 = FindPaletteColorIndex(LocalScope69.value);
				if (num14 >= 0)
				{
					CapturedVariables3760_Index_36 = num14;
					CapturedVariables3760_Color_27 = CapturedVariables3760_Color_01[num14];
					CapturedVariables3760_Text_14 = CapturedVariables3760_Text_49[num14];
					CapturedVariables3760_Button_34?.SetText("Boards Color 2 : " + CapturedVariables3760_Text_14);
				}
			}
			break;
		case 3920340235u:
			if (text == "Boards Mode")
			{
				int num12 = FindColorModeIndex(LocalScope69.value);
				if (num12 >= 0)
				{
					CapturedVariables3760_Color_03 = (ColorMode)num12;
					CapturedVariables3760_Button_53?.SetText("Boards Mode : " + CapturedVariables3760_Text_02[num12]);
				}
			}
			break;
		case 3868235010u:
		{
			if (!(text == "Boards Anim Speed"))
			{
				break;
			}
			int num9 = 0;
			if (num9 >= Recovered_Reference_09.Length)
			{
				break;
			}
			do
			{
				if (Recovered_Reference_09[num9].desc == LocalScope69.value)
				{
					CapturedVariables3760_Index_08 = num9;
					(CapturedVariables3760_Value_17, CapturedVariables3760_Text_18) = Recovered_Reference_09[num9];
					CapturedVariables3760_Button_24?.SetText("Animation Speed : " + CapturedVariables3760_Text_18);
					break;
				}
				num9++;
			}
			while (num9 < Recovered_Reference_09.Length);
			break;
		}
		case 1168659672u:
			if (text == "Gradient Direction")
			{
				CapturedVariables3760_Index_12 = Array.IndexOf(CapturedVariables3760_Text_34, LocalScope69.value);
				if (CapturedVariables3760_Index_12 < 0)
				{
					CapturedVariables3760_Index_12 = 0;
					CapturedVariables3760_Reference_02 = (GradientDirection)CapturedVariables3760_Index_12;
					CapturedVariables3760_Text_23 = CapturedVariables3760_Text_34[CapturedVariables3760_Index_12];
					CapturedVariables3760_Button_28?.SetText("Gradient Direction : " + CapturedVariables3760_Text_23);
				}
				else
				{
					CapturedVariables3760_Reference_02 = (GradientDirection)CapturedVariables3760_Index_12;
					CapturedVariables3760_Text_23 = CapturedVariables3760_Text_34[CapturedVariables3760_Index_12];
					CapturedVariables3760_Button_28?.SetText("Gradient Direction : " + CapturedVariables3760_Text_23);
				}
			}
			break;
		case 1520472891u:
			if (text == "TP Map")
			{
				int num = Array.FindIndex(Movement.Recovered_Reference_06, ((string name, string zone, string pos) m) => m.name == LocalScope69.value);
				if (num >= 0)
				{
					CapturedVariables3760_Index_14 = num;
					CapturedVariables3760_Button_45?.SetText("TP To : " + Movement.Recovered_Reference_06[CapturedVariables3760_Index_14].name);
				}
				else
				{
					CapturedVariables3760_Button_45?.SetText("TP To : " + Movement.Recovered_Reference_06[CapturedVariables3760_Index_14].name);
				}
			}
			break;
		}
	}

	public static void CycleOutlineAnimationSpeed(bool forward)
	{
		CycleAnimationSpeed(ref CapturedVariables3760_Index_24, ref CapturedVariables3760_Value_03, ref CapturedVariables3760_Text_15, CapturedVariables3760_Button_58, forward);
	}

	public static void SetGhostRigEnabled(bool setActive)
	{
		RigManager.RigManager_State_01 = setActive;
	}

	public static void SetRoomNotificationsEnabled(bool setActive)
	{
		Main.CapturedVariables1950_State_07 = setActive;
	}

	public static void SetClassicButtonsEnabled(bool setActive)
	{
		if (CapturedVariables3760_State_06 != setActive)
		{
			CapturedVariables3760_State_06 = setActive;
			if ((Object)(object)Variables.Variables_Object_14 != (Object)null)
			{
				Main.RebuildMenu();
			}
		}
	}

	public static void CycleGunFireColor(bool forward)
	{
		CycleGunColor(ref CapturedVariables3760_Index_40, ref CapturedVariables3760_Color_06, ref CapturedVariables3760_Text_06, ref CapturedVariables3760_State_02, CapturedVariables3760_Button_23, "Gun Fire Color", forward);
	}

	public static void CycleBackgroundColor(bool forward)
	{
		CyclePaletteColor(ref CapturedVariables3760_Index_17, ref CapturedVariables3760_Color_19, ref CapturedVariables3760_Text_61, CapturedVariables3760_Button_11, "Background Color", forward);
	}

	public static List<ButtonHandler.Button> BuildElementSettings()
	{
		List<ButtonHandler.Button> list = new List<ButtonHandler.Button>
		{
			new ButtonHandler.Button("Return", Category.Element_Settings, isToggle: false, isActive: false, delegate
			{
				ButtonHandler.NavigateToCategory(Category.Color_Settings);
			})
			{
				isCategory = true
			}
		};
		int num = (int)CapturedVariables3760_Color_24;
		num = (((uint)num <= 6u) ? num : 7) + 116;
		int num2 = num;
		if (num2 != 117)
		{
			CapturedVariables3760_Button_02 = CreateIncrementalSettingButton("Pinwheel Color 1 : " + CapturedVariables3760_Text_22, delegate
			{
				CyclePinwheelColor1(forward: true);
			}, delegate
			{
				CyclePinwheelColor1(forward: false);
			});
			CapturedVariables3760_Button_36 = CreateIncrementalSettingButton("Pinwheel Color 2 : " + CapturedVariables3760_Text_29, delegate
			{
				CyclePinwheelColor2(forward: true);
			}, delegate
			{
				CyclePinwheelColor2(forward: false);
			});
			CapturedVariables3760_Button_52 = CreateIncrementalSettingButton("Pinwheel Speed : " + CapturedVariables3760_Text_63, delegate
			{
				CyclePinwheelSpeed(forward: true);
			}, delegate
			{
				CyclePinwheelSpeed(forward: false);
			});
			list.Add(CapturedVariables3760_Button_02);
			list.Add(CapturedVariables3760_Button_36);
			list.Add(CapturedVariables3760_Button_52);
		}
		else
		{
			CapturedVariables3760_Button_09 = CreateIncrementalSettingButton("Outline Mode : " + CapturedVariables3760_Text_02[(int)CapturedVariables3760_Color_04], delegate
			{
				CycleOutlineMode(forward: true);
			}, delegate
			{
				CycleOutlineMode(forward: false);
			});
			CapturedVariables3760_Button_27 = CreateIncrementalSettingButton("Outline Color : " + CapturedVariables3760_Text_03, delegate
			{
				CycleOutlineColor(forward: true);
			}, delegate
			{
				CycleOutlineColor(forward: false);
			});
			CapturedVariables3760_Button_04 = CreateIncrementalSettingButton("Outline Color 2 : " + CapturedVariables3760_Text_16, delegate
			{
				CycleOutlineColor2(forward: true);
			}, delegate
			{
				CycleOutlineColor2(forward: false);
			});
			list.Add(CapturedVariables3760_Button_09);
			if (CapturedVariables3760_Color_04 == ColorMode.Lerp || CapturedVariables3760_Color_04 == ColorMode.Rainbow || CapturedVariables3760_Color_04 == ColorMode.Strobe)
			{
				CapturedVariables3760_Button_58 = CreateIncrementalSettingButton("Animation Speed : " + CapturedVariables3760_Text_15, delegate
				{
					CycleOutlineAnimationSpeed(forward: true);
				}, delegate
				{
					CycleOutlineAnimationSpeed(forward: false);
				});
				list.Add(CapturedVariables3760_Button_58);
				list.Add(CapturedVariables3760_Button_27);
				list.Add(CapturedVariables3760_Button_04);
				AddGradientDirectionSetting(list, CapturedVariables3760_Color_04 == ColorMode.Gradient);
			}
			else
			{
				list.Add(CapturedVariables3760_Button_27);
				list.Add(CapturedVariables3760_Button_04);
				AddGradientDirectionSetting(list, CapturedVariables3760_Color_04 == ColorMode.Gradient);
			}
		}
		return list;
	}

	public static void CycleOutlineMode(bool forward)
	{
		CycleColorMode(ref CapturedVariables3760_Color_04, delegate(string v)
		{
			CapturedVariables3760_Button_09?.SetText("Outline Mode : " + v);
		}, forward);
	}

	public static void CycleFlySpeed(bool forward)
	{
		if (!forward)
		{
			CapturedVariables3760_Index_23 = (CapturedVariables3760_Index_23 - 1 + Recovered_Reference_29.Length) % Recovered_Reference_29.Length;
			(CapturedVariables3760_Value_14, CapturedVariables3760_Text_21) = Recovered_Reference_29[CapturedVariables3760_Index_23];
			CapturedVariables3760_Button_48?.SetText("Fly Speed : " + CapturedVariables3760_Text_21);
		}
		else
		{
			CapturedVariables3760_Index_23 = (CapturedVariables3760_Index_23 + 1) % Recovered_Reference_29.Length;
			(CapturedVariables3760_Value_14, CapturedVariables3760_Text_21) = Recovered_Reference_29[CapturedVariables3760_Index_23];
			CapturedVariables3760_Button_48?.SetText("Fly Speed : " + CapturedVariables3760_Text_21);
		}
	}

	public static void CycleOutlineColor2(bool forward)
	{
		CyclePaletteColor(ref CapturedVariables3760_Index_19, ref CapturedVariables3760_Color_09, ref CapturedVariables3760_Text_16, CapturedVariables3760_Button_04, "Outline Color 2", forward);
	}

	private static void CycleGunColor(ref int index, ref Color32 color, ref string desc, ref bool rainbow, ButtonHandler.Button btn, string prefix, bool forward)
	{
		int num = CapturedVariables3760_Color_01.Length + 1;
		index = (forward ? ((index + 1) % num) : ((index - 1 + num) % num));
		rainbow = index == CapturedVariables3760_Color_01.Length;
		if (rainbow)
		{
			desc = "Rainbow";
			if (btn == null)
			{
				return;
			}
		}
		else
		{
			color = CapturedVariables3760_Color_01[index];
			desc = CapturedVariables3760_Text_49[index];
			if (btn == null)
			{
				return;
			}
		}
		btn.SetText(prefix + " : " + desc);
	}

	public static void CycleEnabledButtonMode(bool forward)
	{
		CycleColorMode(ref CapturedVariables3760_Color_05, delegate(string v)
		{
			CapturedVariables3760_Button_03?.SetText("Enabled Mode : " + v);
		}, forward);
	}

	public static void CycleLongArmsLength(bool forward)
	{
		if (!forward)
		{
			CapturedVariables3760_Index_55 = (CapturedVariables3760_Index_55 - 1 + Recovered_Reference_05.Length) % Recovered_Reference_05.Length;
			(CapturedVariables3760_Position_01, CapturedVariables3760_Text_05) = Recovered_Reference_05[CapturedVariables3760_Index_55];
			CapturedVariables3760_Button_21?.SetText("Long Arms Length : " + CapturedVariables3760_Text_05);
		}
		else
		{
			CapturedVariables3760_Index_55 = (CapturedVariables3760_Index_55 + 1) % Recovered_Reference_05.Length;
			(CapturedVariables3760_Position_01, CapturedVariables3760_Text_05) = Recovered_Reference_05[CapturedVariables3760_Index_55];
			CapturedVariables3760_Button_21?.SetText("Long Arms Length : " + CapturedVariables3760_Text_05);
		}
	}

	public static void SetRoomNotificationsSoundEnabled(bool setActive)
	{
		Variables.Variables_State_09 = setActive;
	}

	public static void CycleButtonColor2(bool forward)
	{
		CyclePaletteColor(ref CapturedVariables3760_Index_06, ref CapturedVariables3760_Color_23, ref CapturedVariables3760_Text_11, CapturedVariables3760_Button_50, "Button Color 2", forward);
	}

	private static void LoadGunColor(string value, ref int index, ref Color32 color, ref string desc, ref bool rainbow, ButtonHandler.Button btn, string prefix)
	{
		if (value == "Rainbow")
		{
			index = CapturedVariables3760_Color_01.Length;
			rainbow = true;
			desc = "Rainbow";
			if (btn == null)
			{
				return;
			}
		}
		else
		{
			int num = FindPaletteColorIndex(value);
			if (num < 0)
			{
				return;
			}
			index = num;
			color = CapturedVariables3760_Color_01[num];
			rainbow = false;
			desc = CapturedVariables3760_Text_49[num];
			if (btn == null)
			{
				return;
			}
		}
		btn.SetText(prefix + " : " + desc);
	}

	public static void SelectColorElement(ColorElement element)
	{
		CapturedVariables3760_Color_24 = element;
		ButtonHandler.NavigateToCategory(Category.Element_Settings);
	}
}
