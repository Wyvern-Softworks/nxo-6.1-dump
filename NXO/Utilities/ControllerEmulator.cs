using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace NXO.Utilities;

public static class ControllerEmulator
{
	public static bool ControllerEmulator_State_08 = false;

	private static bool ControllerEmulator_State_01 = false;

	private static bool ControllerEmulator_State_07 = false;

	private static bool ControllerEmulator_State_02 = false;

	private static bool ControllerEmulator_State_04 = false;

	private static bool ControllerEmulator_State_10 = false;

	private static bool ControllerEmulator_State_06 = false;

	private static bool ControllerEmulator_State_05 = false;

	private static bool ControllerEmulator_State_09 = false;

	private static Texture2D ControllerEmulator_Reference_07;

	private static GUIStyle ControllerEmulator_Reference_05;

	private static GUIStyle ControllerEmulator_Reference_06;

	private static GUIStyle ControllerEmulator_Reference_03;

	private static GUIStyle ControllerEmulator_Reference_02;

	private static GUIStyle ControllerEmulator_Reference_04;

	private static GUIStyle ControllerEmulator_Reference_01;

	private static bool ControllerEmulator_State_03 = false;

	private const float HUD_WIDTH = 280f;

	private const float HUD_HEIGHT = 210f;

	private const float ROW_HEIGHT = 20f;

	private static readonly string[] ControllerEmulator_Text_01 = new string[8] { "Right Trigger", "Left Trigger", "Right Bumper", "Left Bumper", "A Button", "B Button", "X Button", "Y Button" };

	private static readonly string[] ControllerEmulator_Text_02 = new string[8] { "T", "R", "G", "F", "Y", "U", "E", "Q" };

	public static bool GetRightGrip()
	{
		return ControllerEmulator_State_02;
	}

	public static void UpdateEmulatedInput()
	{
		if (ControllerEmulator_State_08)
		{
			ControllerEmulator_State_01 = ((ButtonControl)Keyboard.current[(Key)34]).isPressed;
			ControllerEmulator_State_07 = ((ButtonControl)Keyboard.current[(Key)32]).isPressed;
			ControllerEmulator_State_02 = ((ButtonControl)Keyboard.current[(Key)21]).isPressed;
			ControllerEmulator_State_04 = ((ButtonControl)Keyboard.current[(Key)20]).isPressed;
			ControllerEmulator_State_10 = ((ButtonControl)Keyboard.current[(Key)39]).isPressed;
			ControllerEmulator_State_05 = ((ButtonControl)Keyboard.current[(Key)35]).isPressed;
			ControllerEmulator_State_06 = ((ButtonControl)Keyboard.current[(Key)19]).isPressed;
			ControllerEmulator_State_09 = ((ButtonControl)Keyboard.current[(Key)31]).isPressed;
		}
	}

	private static Texture2D CreateHudBackgroundTexture()
	{
		int num = 280;
		int num2 = 210;
		int num3 = 12;
		Texture2D val = new Texture2D(num, num2, (TextureFormat)4, false)
		{
			filterMode = (FilterMode)1
		};
		Color32 val2 = new Color32((byte)0, (byte)0, (byte)0, (byte)180);
		Color32 val3 = new Color32((byte)0, (byte)0, (byte)0, (byte)0);
		Color32[] array = (Color32[])(object)new Color32[num * num2];
		int num4 = 0;
		if (num4 < num2)
		{
			do
			{
				int num5 = 0;
				if (num5 < num)
				{
					do
					{
						Branch_005e:
						int num6 = num4 * num + num5;
						int num7 = num5;
						int num8 = num4;
						bool flag = false;
						if (num5 < num3 && num4 >= num2 - num3)
						{
							num7 = num3;
							num8 = num2 - num3;
							flag = true;
						}
						else if (num5 >= num - num3 && num4 >= num2 - num3)
						{
							num7 = num - num3 - 1;
							num8 = num2 - num3;
							flag = true;
						}
						else if (num5 < num3 && num4 < num3)
						{
							num7 = num3;
							num8 = num3;
							flag = true;
						}
						else if (num5 >= num - num3 && num4 < num3)
						{
							num7 = num - num3 - 1;
							num8 = num3;
							goto Branch_01d1;
						}
						if (flag)
						{
							goto Branch_01d1;
						}
						array[num6] = val2;
						num5++;
						if (num5 >= num)
						{
							break;
						}
						goto Branch_005e;
						Branch_01d1:
						float num9 = Mathf.Sqrt((float)((num5 - num7) * (num5 - num7) + (num4 - num8) * (num4 - num8)));
						array[num6] = ((num9 > (float)num3) ? val3 : val2);
						num5++;
					}
					while (num5 < num);
				}
				num4++;
			}
			while (num4 < num2);
		}
		val.SetPixels32(array);
		val.Apply(false);
		return val;
	}

	public static void DrawInputHud()
	{
		if (!ControllerEmulator_State_08)
		{
			return;
		}
		Rect val = default(Rect);
		bool[] array;
		int num3;
		float num2;
		if (!ControllerEmulator_State_03)
		{
			InitializeHudStyles();
			val = new Rect(15f, (float)Screen.height - 210f - 15f, 280f, 210f);
			GUI.Box(val, "", ControllerEmulator_Reference_05);
			float num = ((Rect)val).y + 15f;
			num2 = num;
			GUI.Label(new Rect(((Rect)val).x + 20f, num2, 140f, 20f), "Controller", ControllerEmulator_Reference_03);
			GUI.Label(new Rect(((Rect)val).x + 145f, num2, 40f, 20f), "Key", ControllerEmulator_Reference_03);
			GUI.Label(new Rect(((Rect)val).x + 210f, num2, 60f, 20f), "Status", ControllerEmulator_Reference_03);
			num = num2 + 22f;
			num2 = num;
			GUI.color = new Color(1f, 1f, 1f, 0.1f);
			GUI.DrawTexture(new Rect(((Rect)val).x + 20f, num2, ((Rect)val).width - 40f, 1f), (Texture)(object)Texture2D.whiteTexture);
			GUI.color = Color.white;
			num = num2 + 6f;
			num2 = num;
			array = new bool[8] { ControllerEmulator_State_01, ControllerEmulator_State_07, ControllerEmulator_State_02, ControllerEmulator_State_04, ControllerEmulator_State_10, ControllerEmulator_State_05, ControllerEmulator_State_06, ControllerEmulator_State_09 };
			num3 = 0;
		}
		else
		{
			val = new Rect(15f, (float)Screen.height - 210f - 15f, 280f, 210f);
			GUI.Box(val, "", ControllerEmulator_Reference_05);
			float num4 = ((Rect)val).y + 15f;
			num2 = num4;
			GUI.Label(new Rect(((Rect)val).x + 20f, num2, 140f, 20f), "Controller", ControllerEmulator_Reference_03);
			GUI.Label(new Rect(((Rect)val).x + 145f, num2, 40f, 20f), "Key", ControllerEmulator_Reference_03);
			GUI.Label(new Rect(((Rect)val).x + 210f, num2, 60f, 20f), "Status", ControllerEmulator_Reference_03);
			num4 = num2 + 22f;
			num2 = num4;
			GUI.color = new Color(1f, 1f, 1f, 0.1f);
			GUI.DrawTexture(new Rect(((Rect)val).x + 20f, num2, ((Rect)val).width - 40f, 1f), (Texture)(object)Texture2D.whiteTexture);
			GUI.color = Color.white;
			num4 = num2 + 6f;
			num2 = num4;
			array = new bool[8] { ControllerEmulator_State_01, ControllerEmulator_State_07, ControllerEmulator_State_02, ControllerEmulator_State_04, ControllerEmulator_State_10, ControllerEmulator_State_05, ControllerEmulator_State_06, ControllerEmulator_State_09 };
			num3 = 0;
		}
		if (num3 < 8)
		{
			do
			{
				DrawInputRow(((Rect)val).x + 20f, num2, ControllerEmulator_Text_01[num3], ControllerEmulator_Text_02[num3], array[num3]);
				num2 += 20f;
				num3++;
			}
			while (num3 < 8);
		}
	}

	public static bool GetLeftGrip()
	{
		return ControllerEmulator_State_04;
	}

	public static bool GetLeftPrimaryButton()
	{
		return ControllerEmulator_State_06;
	}

	public static bool GetLeftTrigger()
	{
		return ControllerEmulator_State_07;
	}

	private static void DrawInputRow(float x, float y, string controller, string key, bool isActive)
	{
		GUI.Label(new Rect(x, y, 140f, 19f), controller, isActive ? ControllerEmulator_Reference_04 : ControllerEmulator_Reference_02);
		GUI.Label(new Rect(x + 117.5f, y, 40f, 19f), key, ControllerEmulator_Reference_01);
		GUI.Label(new Rect(x + 190f, y, 60f, 19f), isActive ? "ON" : "OFF", isActive ? ControllerEmulator_Reference_04 : ControllerEmulator_Reference_02);
	}

	public static bool GetRightTrigger()
	{
		return ControllerEmulator_State_01;
	}

	public static void ResetHudResources()
	{
		if ((Object)(object)ControllerEmulator_Reference_07 != (Object)null)
		{
			Object.DestroyImmediate((Object)(object)ControllerEmulator_Reference_07);
			ControllerEmulator_Reference_07 = null;
			ControllerEmulator_Reference_05 = null;
			ControllerEmulator_Reference_06 = null;
			ControllerEmulator_Reference_03 = null;
			ControllerEmulator_Reference_02 = null;
			ControllerEmulator_Reference_04 = null;
			ControllerEmulator_Reference_01 = null;
			ControllerEmulator_State_03 = false;
		}
		else
		{
			ControllerEmulator_Reference_05 = null;
			ControllerEmulator_Reference_06 = null;
			ControllerEmulator_Reference_03 = null;
			ControllerEmulator_Reference_02 = null;
			ControllerEmulator_Reference_04 = null;
			ControllerEmulator_Reference_01 = null;
			ControllerEmulator_State_03 = false;
		}
	}

	public static bool GetLeftSecondaryButton()
	{
		return ControllerEmulator_State_09;
	}

	public static bool GetRightPrimaryButton()
	{
		return ControllerEmulator_State_10;
	}

	public static bool GetRightSecondaryButton()
	{
		return ControllerEmulator_State_05;
	}

	private static void InitializeHudStyles()
	{
		ControllerEmulator_Reference_07 = CreateHudBackgroundTexture();
		GUIStyle val = new GUIStyle();
		val.normal.background = ControllerEmulator_Reference_07;
		ControllerEmulator_Reference_05 = val;
		GUIStyle val2 = new GUIStyle
		{
			fontSize = 18,
			fontStyle = (FontStyle)1
		};
		val2.normal.textColor = Color.white;
		val2.alignment = (TextAnchor)4;
		ControllerEmulator_Reference_06 = val2;
		GUIStyle val3 = new GUIStyle
		{
			fontSize = 14,
			fontStyle = (FontStyle)1
		};
		val3.normal.textColor = Color.white;
		val3.alignment = (TextAnchor)3;
		ControllerEmulator_Reference_03 = val3;
		GUIStyle val4 = new GUIStyle
		{
			fontSize = 12
		};
		val4.normal.textColor = (Color32)(new Color32((byte)180, (byte)180, (byte)180, byte.MaxValue));
		val4.alignment = (TextAnchor)3;
		ControllerEmulator_Reference_02 = val4;
		GUIStyle val5 = new GUIStyle
		{
			fontSize = 12,
			fontStyle = (FontStyle)1
		};
		val5.normal.textColor = Color.white;
		val5.alignment = (TextAnchor)3;
		ControllerEmulator_Reference_04 = val5;
		GUIStyle val6 = new GUIStyle
		{
			fontSize = 12
		};
		val6.normal.textColor = (Color32)(new Color32((byte)180, (byte)180, (byte)180, byte.MaxValue));
		val6.alignment = (TextAnchor)4;
		ControllerEmulator_Reference_01 = val6;
		ControllerEmulator_State_03 = true;
	}
}
