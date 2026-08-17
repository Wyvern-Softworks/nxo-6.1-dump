using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using BepInEx;
using GorillaLocomotion;
using GorillaTag;
using NXO.Mods;
using NXO.Mods.Categories;
using NXO.Utilities;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

namespace NXO.Menu;

public class Main : MonoBehaviour
{
	private struct FadeItem
	{
		public Renderer r;

		public Text txt;

		public float origA;

		public Color target;
	}

	public enum ColorRole
	{
		None,
		Background,
		Button,
		EnabledButton,
		Outline,
		AccentStrip
	}

	private class TrackedColorGroup
	{
		public ColorRole role;

		public List<Renderer> renderers = new List<Renderer>(8);
	}

	public class FontEntry
	{
		public string Description;

		public Font Font;
	}

	[CompilerGenerated]
	private sealed class CapturedVariables1950
	{
		public ButtonHandler.Button parent;

		internal void CreateQuickActionButton_Lambda0()
		{
			ButtonHandler.OpenGearMenu(parent);
		}
	}

	public static readonly Vector3 CapturedVariables1950_Position_12;

	public static readonly Vector3 CapturedVariables1950_Position_09;

	public static readonly Vector3 CapturedVariables1950_Position_16;

	public const float FaceX = 0.07525f;

	public const float IconX = 0.07225f;

	public static readonly Vector3 CapturedVariables1950_Position_04;

	public static readonly Vector3 CapturedVariables1950_Position_15;

	public static readonly Vector3 CapturedVariables1950_Position_06;

	public static readonly Vector3 CapturedVariables1950_Position_01;

	public const float DisconnectIconY = -0.25f;

	public const float SearchIconY = -0.35f;

	public const float HeaderIconZ = 0.45f;

	public const float FpsWidthFactor = 3.3f;

	public static readonly Vector2 CapturedVariables1950_Position_02;

	public static readonly Vector3 CapturedVariables1950_Position_08;

	public static readonly Vector3 CapturedVariables1950_Position_05;

	private static int? CapturedVariables1950_Index_02;

	public static Camera CapturedVariables1950_Reference_07;

	private static string CapturedVariables1950_Text_01;

	public static Category CapturedVariables1950_Reference_09;

	private static int CapturedVariables1950_Index_03;

	public static int CapturedVariables1950_Index_01;

	private static readonly List<ButtonHandler.Button> CapturedVariables1950_Button_02;

	private static bool CapturedVariables1950_State_05;

	public const float ButtonFullWidth = 0.82f;

	public const float ButtonWidthWithGear = 0.695f;

	public const float ButtonWidthIncremental = 0.555f;

	public const float ButtonHeight = 0.08f;

	public const float ButtonDepth = 0.0075f;

	public const float ButtonTextX = 0.05975f;

	public const float ModLabelX = 0.054f;

	public const float ButtonYWithGear = 0.0625f;

	public const float ButtonBaseZ = 0.335f;

	public const float SubButtonWidth = 0.12f;

	public const float IncrementUpY = -0.3525f;

	public const float IncrementDownY = -0.22f;

	public const float GearY = -0.3525f;

	public const float ButtonTextYRatio = 0.288f;

	public const float CompactButtonY = -0.3525f;

	public const float CompactLabelX = 0.054f;

	public const float CompactLabelY = 0.035f;

	public const float CompactLabelWidth = 0.125f;

	public const float ToggleWidth = 0.15f;

	public const float ToggleY = -0.3375f;

	public const float ToggleHeight = 0.058f;

	public const float NavRowZ = -0.33f;

	public const float ReturnHeight = 0.2925f;

	public const float PageHeight = 0.25f;

	public const float PageLeftY = 0.285f;

	public const float PageRightY = -0.285f;

	public static readonly Vector2 CapturedVariables1950_Position_13;

	public static readonly Vector2 CapturedVariables1950_Position_18;

	public static readonly Vector2 CapturedVariables1950_Position_14;

	public static readonly Vector2 CapturedVariables1950_Position_03;

	public static readonly Vector2 CapturedVariables1950_Position_07;

	public static readonly Vector3 CapturedVariables1950_Position_10;

	public const float OutlineThickness = 0.0125f;

	public const float ButtonOutlineThickness = 0.0065f;

	public const float ClickerSize = 0.0035f;

	public static readonly Vector3 CapturedVariables1950_Position_17;

	public static List<Material> CapturedVariables1950_Material_05;

	public static readonly List<GameObject> CapturedVariables1950_Object_01;

	private static readonly List<GameObject> CapturedVariables1950_Object_05;

	private static readonly List<Material> CapturedVariables1950_Material_04;

	private static readonly List<Mesh> CapturedVariables1950_Reference_05;

	private static bool CapturedVariables1950_State_01;

	private static readonly List<GameObject> CapturedVariables1950_Object_03;

	private static readonly List<Material> CapturedVariables1950_Material_03;

	private static readonly List<Mesh> CapturedVariables1950_Reference_01;

	private static bool CapturedVariables1950_State_10;

	private static TrackedColorGroup CapturedVariables1950_Color_04;

	private static readonly List<FadeItem> CapturedVariables1950_Reference_08;

	private static readonly HashSet<Material> CapturedVariables1950_Material_06;

	private static Coroutine CapturedVariables1950_Routine_01;

	private const float PageFadeDuration = 0.24f;

	private static readonly List<TrackedColorGroup> CapturedVariables1950_Color_03;

	private static readonly List<ButtonHandler.Button> CapturedVariables1950_Button_03;

	private static readonly List<(ButtonHandler.Button b, int score)> Recovered_Reference_10;

	private static bool CapturedVariables1950_State_12;

	private static bool CapturedVariables1950_State_08;

	public static float CapturedVariables1950_Value_04;

	public static bool CapturedVariables1950_State_09;

	public static bool CapturedVariables1950_State_06;

	private const float MenuSmoothingSpeed = 25f;

	private static bool CapturedVariables1950_State_02;

	public static GameObject CapturedVariables1950_Object_04;

	public static GameObject CapturedVariables1950_Object_02;

	public static Text CapturedVariables1950_Reference_10;

	private static float CapturedVariables1950_Value_05;

	private static float CapturedVariables1950_Value_01;

	public static Color CapturedVariables1950_Color_02;

	public static Color CapturedVariables1950_Color_06;

	public static float CapturedVariables1950_Value_06;

	public static Material CapturedVariables1950_Material_02;

	private static Material CapturedVariables1950_Material_01;

	private static float CapturedVariables1950_Value_02;

	private const float AUTO_SAVE_INTERVAL = 60f;

	private static float CapturedVariables1950_Value_03;

	private static Settings.ColorMode CapturedVariables1950_Color_01;

	private static Color32 CapturedVariables1950_Color_07;

	private static Color32 CapturedVariables1950_Color_05;

	private const float TITLE_UPDATE_INTERVAL = 0.03f;

	private static readonly StringBuilder CapturedVariables1950_Text_02;

	public static readonly List<FontEntry> CapturedVariables1950_Reference_02;

	private static readonly List<Mesh> CapturedVariables1950_Reference_04;

	private static readonly List<Material> CapturedVariables1950_Material_08;

	private static Material CapturedVariables1950_Material_07;

	public static readonly List<Font> CapturedVariables1950_Reference_06;

	private static readonly List<ButtonHandler.Button> CapturedVariables1950_Button_01;

	private static bool CapturedVariables1950_State_11;

	public static float CapturedVariables1950_Value_07;

	public static Vector3 CapturedVariables1950_Position_11;

	private static bool CapturedVariables1950_State_03;

	private static Shader CapturedVariables1950_Reference_03;

	public static bool CapturedVariables1950_State_04;

	public static bool CapturedVariables1950_State_07;

	private static int UILayerMask
	{
		get
		{
			int valueOrDefault = CapturedVariables1950_Index_02.GetValueOrDefault();
			if (!CapturedVariables1950_Index_02.HasValue)
			{
				CapturedVariables1950_Index_02 = 1 << LayerMask.NameToLayer("UI");
				return valueOrDefault;
			}
			return valueOrDefault;
		}
	}

	public static float TextOffset
	{
		get
		{
			if (!(Settings.CurrentFontDescription == "Comic Sans"))
			{
				return 0.15f;
			}
			return 0.1525f;
		}
	}

	public static float IncrementTextOffset => Settings.CurrentFontDescription switch
	{
		"Minecraft" => 0.15f, 
		"Arial" => 0.148f, 
		"Comic Sans" => 0.152f, 
		_ => 0.155f, 
	};

	public static float PageZOffset
	{
		get
		{
			string text = Settings.CurrentFontDescription;
			if (!(text == "Minecraft"))
			{
				if (text == "Arial")
				{
					return -0.14875f;
				}
				return -23f / 160f;
			}
			return -0.14875f;
		}
	}

	public static (float y, float z) TitleOffset
	{
		get
		{
			string text = Settings.CurrentFontDescription;
			if (!(text == "Comic Sans"))
			{
				if (text == "Minecraft")
				{
					return (y: 0.057f, z: 0.2025f);
				}
				return (y: 0.057f, z: 0.2025f);
			}
			return (y: 0.057f, z: 0.2075f);
		}
	}

	private static List<GameObject> ObjectSink
	{
		get
		{
			if (!CapturedVariables1950_State_10)
			{
				if (!CapturedVariables1950_State_01)
				{
					return CapturedVariables1950_Object_01;
				}
				return CapturedVariables1950_Object_05;
			}
			return CapturedVariables1950_Object_03;
		}
	}

	private static List<Material> MaterialSink
	{
		get
		{
			if (!CapturedVariables1950_State_10)
			{
				if (!CapturedVariables1950_State_01)
				{
					return CapturedVariables1950_Material_05;
				}
				return CapturedVariables1950_Material_04;
			}
			return CapturedVariables1950_Material_03;
		}
	}

	private static List<Mesh> MeshSink
	{
		get
		{
			if (!CapturedVariables1950_State_10)
			{
				if (!CapturedVariables1950_State_01)
				{
					return CapturedVariables1950_Reference_04;
				}
				return CapturedVariables1950_Reference_05;
			}
			return CapturedVariables1950_Reference_01;
		}
	}

	public static Font CurrentFont
	{
		get
		{
			if (CapturedVariables1950_Reference_02.Count <= 0)
			{
				return null;
			}
			return CapturedVariables1950_Reference_02[Settings.CurrentFontIndex].Font;
		}
	}

	private static Shader VertexColorShader
	{
		get
		{
			if (!((Object)(object)CapturedVariables1950_Reference_03 != (Object)null))
			{
				return CapturedVariables1950_Reference_03 = Shader.Find("Sprites/Default");
			}
			return CapturedVariables1950_Reference_03;
		}
	}

	public static void ClearMenuPageObjects()
	{
		int num = 0;
		if (num < CapturedVariables1950_Object_03.Count)
		{
			while (true)
			{
				if ((Object)(object)CapturedVariables1950_Object_03[num] != (Object)null)
				{
					Object.Destroy((Object)(object)CapturedVariables1950_Object_03[num]);
					num++;
					if (num >= CapturedVariables1950_Object_03.Count)
					{
						break;
					}
				}
				else
				{
					num++;
					if (num >= CapturedVariables1950_Object_03.Count)
					{
						break;
					}
				}
			}
		}
		CapturedVariables1950_Object_03.Clear();
		int num2 = 0;
		if (num2 < CapturedVariables1950_Material_03.Count)
		{
			while (true)
			{
				if ((Object)(object)CapturedVariables1950_Material_03[num2] != (Object)null)
				{
					Object.Destroy((Object)(object)CapturedVariables1950_Material_03[num2]);
					num2++;
					if (num2 >= CapturedVariables1950_Material_03.Count)
					{
						break;
					}
				}
				else
				{
					num2++;
					if (num2 >= CapturedVariables1950_Material_03.Count)
					{
						break;
					}
				}
			}
		}
		CapturedVariables1950_Material_03.Clear();
		int num3 = 0;
		if (num3 < CapturedVariables1950_Reference_01.Count)
		{
			while (true)
			{
				if ((Object)(object)CapturedVariables1950_Reference_01[num3] != (Object)null)
				{
					Object.Destroy((Object)(object)CapturedVariables1950_Reference_01[num3]);
					num3++;
					if (num3 >= CapturedVariables1950_Reference_01.Count)
					{
						break;
					}
				}
				else
				{
					num3++;
					if (num3 >= CapturedVariables1950_Reference_01.Count)
					{
						break;
					}
				}
			}
		}
		CapturedVariables1950_Reference_01.Clear();
		CapturedVariables1950_Color_03.RemoveAll((TrackedColorGroup g) => g.renderers.Count == 0 || g.renderers.All((Renderer r) => (Object)(object)r == (Object)null));
		Variables.Variables_Object_02 = null;
		SearchAndKeyboard.KeyCollider_Reference_01 = null;
		UpdateSearchButtonColorRole();
		ApplyMenuScale();
		RedrawMenu();
	}

	private static void UpdateSearchButtonColorRole()
	{
		if (CapturedVariables1950_Color_04 != null)
		{
			CapturedVariables1950_Color_04.role = (SearchAndKeyboard.KeyCollider_State_02 ? ColorRole.EnabledButton : ColorRole.Button);
			CapturedVariables1950_State_03 = true;
		}
		else
		{
			CapturedVariables1950_State_03 = true;
		}
	}

	private static void RebuildVisibleButtons()
	{
		bool flag;
		bool flag2;
		List<ButtonHandler.Button> list = new List<ButtonHandler.Button>();
		if (SearchAndKeyboard.KeyCollider_State_02 && !SearchAndKeyboard.KeyCollider_State_04)
		{
			flag = !string.IsNullOrEmpty(SearchAndKeyboard.KeyCollider_Text_02);
			flag2 = flag != CapturedVariables1950_State_05;
			if (flag)
			{
				goto Branch_007f;
			}
		}
		else
		{
			flag = false;
			flag2 = flag != CapturedVariables1950_State_05;
			if (flag)
			{
				goto Branch_007f;
			}
		}
		bool flag3 = false;
		if (!flag)
		{
			goto Branch_00e1;
		}
		goto Branch_0101;
		Branch_05d5:
		int num;
		if (num < CapturedVariables1950_Button_02.Count)
		{
			do
			{
				CreateButton((float)num * Variables.Variables_Value_04, CapturedVariables1950_Button_02[num]);
				num++;
			}
			while (num < CapturedVariables1950_Button_02.Count);
		}
		CapturedVariables1950_State_01 = false;
		return;
		Branch_0515:
		CapturedVariables1950_Button_02.Clear();
		list = new List<ButtonHandler.Button>();
		CapturedVariables1950_Button_02.AddRange(list);
		CapturedVariables1950_Text_01 = SearchAndKeyboard.KeyCollider_Text_02;
		CapturedVariables1950_Reference_09 = Variables.currentPage;
		CapturedVariables1950_Index_03 = Variables.Variables_Index_04;
		CapturedVariables1950_Index_01 = NXOUI.CapturedVariables1190_Index_01;
		CapturedVariables1950_State_05 = flag;
		CapturedVariables1950_State_01 = true;
		num = 0;
		goto Branch_05d5;
		Branch_0199:
		CapturedVariables1950_Button_03.Clear();
		if (flag)
		{
			Recovered_Reference_10.Clear();
			ButtonHandler.Button[] array = ModButtons.buttons;
			int num2 = 0;
			List<(ButtonHandler.Button, int)> list2 = new List<(ButtonHandler.Button, int)>();
			while (num2 < array.Length)
			{
				ButtonHandler.Button button = array[num2];
				if (button != null && button.Page != Category.Home)
				{
					int num3 = SearchAndKeyboard.CalculateSearchScore(button.buttonText, SearchAndKeyboard.KeyCollider_Text_02);
					if (num3 > 0)
					{
						list2.Add((button, num3));
						num2++;
					}
					else
					{
						num2++;
					}
				}
				else
				{
					num2++;
				}
			}
			list2.Sort(((ButtonHandler.Button b, int score) a, (ButtonHandler.Button b, int score) z) => z.score.CompareTo(a.score));
			int num4 = Variables.Variables_Index_04 * Variables.Variables_Index_01;
			int num5 = num4;
			if (num5 < Mathf.Min(num4 + Variables.Variables_Index_01, list2.Count))
			{
				do
				{
					list.Add(list2[num5].Item1);
					num5++;
				}
				while (num5 < Mathf.Min(num4 + Variables.Variables_Index_01, list2.Count));
			}
			if (list.Count <= 0)
			{
				goto Branch_04e9;
			}
		}
		else
		{
			List<ButtonHandler.Button> list3 = ButtonHandler.GetButtonsForPage(Variables.currentPage);
			int num6 = 0;
			int num7 = Variables.Variables_Index_04 * Variables.Variables_Index_01;
			using (List<ButtonHandler.Button>.Enumerator enumerator = list3.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					while (true)
					{
						ButtonHandler.Button current = enumerator.Current;
						if (current == null || !Settings.IsElementSettingVisible(current.buttonText))
						{
							break;
						}
						if (num6 >= num7 && list.Count < Variables.Variables_Index_01)
						{
							list.Add(current);
							num6++;
							if (!enumerator.MoveNext())
							{
								goto EndBranch_0484;
							}
						}
						else
						{
							num6++;
							if (!enumerator.MoveNext())
							{
								goto EndBranch_0484;
							}
						}
					}
					continue;
					EndBranch_0484:
					break;
				}
			}
			if (list.Count <= 0)
			{
				goto Branch_04e9;
			}
		}
		goto Branch_0515;
		Branch_007f:
		if (!(SearchAndKeyboard.KeyCollider_Text_02 != CapturedVariables1950_Text_01))
		{
			flag3 = Variables.Variables_Index_04 != CapturedVariables1950_Index_03;
			if (!flag)
			{
				goto Branch_00e1;
			}
		}
		else
		{
			flag3 = true;
			if (!flag)
			{
				goto Branch_00e1;
			}
		}
		goto Branch_0101;
		Branch_04e9:
		if (!flag)
		{
			goto Branch_0515;
		}
		CapturedVariables1950_State_05 = flag;
		CapturedVariables1950_State_01 = true;
		num = 0;
		goto Branch_05d5;
		Branch_0585:
		CapturedVariables1950_State_01 = true;
		num = 0;
		goto Branch_05d5;
		Branch_0147:
		bool flag4 = NXOUI.CapturedVariables1190_Index_01 != CapturedVariables1950_Index_01;
		bool flag5;
		bool flag6;
		if (!(flag2 | flag3 | flag5 | flag6 | flag4))
		{
			goto Branch_0585;
		}
		goto Branch_0199;
		Branch_0101:
		flag5 = false;
		if (!flag)
		{
			goto Branch_0113;
		}
		goto Branch_0134;
		Branch_00e1:
		flag5 = Variables.currentPage != CapturedVariables1950_Reference_09;
		if (!flag)
		{
			goto Branch_0113;
		}
		Branch_0134:
		flag6 = false;
		if (!flag)
		{
			goto Branch_0147;
		}
		goto Branch_0177;
		Branch_0113:
		flag6 = Variables.Variables_Index_04 != CapturedVariables1950_Index_03;
		if (!flag)
		{
			goto Branch_0147;
		}
		Branch_0177:
		flag4 = false;
		if (!(flag2 | flag3 | flag5 | flag6 | flag4))
		{
			goto Branch_0585;
		}
		goto Branch_0199;
	}

	private static IEnumerator ScaleObjectCoroutine(GameObject obj, Vector3 from, Vector3 to, float duration)
	{
		float elapsed = 0f;
		if (elapsed < duration)
		{
			do
			{
				if ((Object)(object)obj == (Object)null)
				{
					yield break;
				}
				float t = elapsed / duration;
				obj.transform.localScale = Vector3.Lerp(from, to, 1f - Mathf.Pow(1f - t, 3f));
				elapsed += Time.deltaTime;
				yield return null;
			}
			while (elapsed < duration);
		}
		if ((Object)(object)obj != (Object)null)
		{
			obj.transform.localScale = to;
		}
	}

	public static void UpdateFpsDisplay(bool force = false)
	{
		if (!((Object)(object)CapturedVariables1950_Reference_10 == (Object)null))
		{
			CapturedVariables1950_Value_05 = Mathf.Lerp(CapturedVariables1950_Value_05, 1f / Time.unscaledDeltaTime, Time.unscaledDeltaTime * 4f);
			CapturedVariables1950_Value_01 -= Time.unscaledDeltaTime;
			if (force || !(CapturedVariables1950_Value_01 > 0f))
			{
				CapturedVariables1950_Value_01 = 0.5f;
				CapturedVariables1950_Reference_10.text = string.Format("v{0} | FPS: {1}", "6.1", Mathf.RoundToInt(CapturedVariables1950_Value_05));
			}
		}
	}

	private void Start()
	{
		CapturedVariables1950_Material_05.Clear();
		CapturedVariables1950_Object_01.Clear();
		CapturedVariables1950_Color_03.Clear();
		Variables.Variables_Text_01 = Path.Combine(Directory.GetCurrentDirectory(), "NXO Mod Menu");
		Directory.CreateDirectory(Variables.Variables_Text_01);
		NetworkSystem instance = NetworkSystem.Instance;
		instance.OnJoinedRoomEvent = (DelegateListProcessorPlusMinus<DelegateListProcessor, Action>)(object)instance.OnJoinedRoomEvent + (Action)HandleJoinedRoom;
		NetworkSystem instance2 = NetworkSystem.Instance;
		instance2.OnReturnedToSinglePlayer = (DelegateListProcessorPlusMinus<DelegateListProcessor, Action>)(object)instance2.OnReturnedToSinglePlayer + (Action)HandleLeftRoom;
		NetworkSystem instance3 = NetworkSystem.Instance;
		instance3.OnPlayerJoined = (DelegateListProcessorPlusMinus<DelegateListProcessor<NetPlayer>, Action<NetPlayer>>)(object)instance3.OnPlayerJoined + (Action<NetPlayer>)HandlePlayerJoined;
		NetworkSystem instance4 = NetworkSystem.Instance;
		instance4.OnPlayerLeft = (DelegateListProcessorPlusMinus<DelegateListProcessor<NetPlayer>, Action<NetPlayer>>)(object)instance4.OnPlayerLeft + (Action<NetPlayer>)HandlePlayerLeft;
		LoadFonts();
		Variables.Variables_Reference_09 = GorillaTagger.Instance;
		Variables.Variables_Reference_06 = GTPlayer.Instance;
		Variables.Variables_Object_13 = GameObject.Find("Player Objects/Third Person Camera/Shoulder Camera");
		Variables.Variables_Object_04 = GameObject.Find("Player Objects/Third Person Camera/Shoulder Camera/CM vcam1");
		ButtonHandler.PreloadClickSounds();
		ButtonHandler.LoadCustomClickSounds();
		((MonoBehaviour)this).StartCoroutine(AssetHandler.LoadEmbeddedAudioClip("NXO.Resources.SteamOpen.wav", delegate(AudioClip clip)
		{
			Variables.Variables_Audio_03 = clip;
		}));
		((MonoBehaviour)this).StartCoroutine(AssetHandler.LoadEmbeddedAudioClip("NXO.Resources.SteamClose.wav", delegate(AudioClip clip)
		{
			Variables.Variables_Audio_02 = clip;
		}));
		((MonoBehaviour)this).StartCoroutine(AssetHandler.LoadEmbeddedAudioClip("NXO.Resources.NotificationSound.wav", delegate(AudioClip clip)
		{
			Variables.Variables_Audio_01 = clip;
		}));
		((MonoBehaviour)this).StartCoroutine(CustomBoards.PollPlayerCount(delegate(int count)
		{
			Debug.Log((object)$"NXO Player Count: {count}");
		}));
		ButtonHandler.ReloadPresets();
		Macros.RebuildMacroButtons();
		CustomNextbots.InitializeCustomNextbots();
		ButtonHandler.LoadFavoritedMods();
		Settings.EnsureDefaultSettingsCached();
		ButtonHandler.LoadAutoSavePreference();
		ButtonHandler.LoadAutosavedStuff();
		CapturedVariables1950_Value_02 = Time.time;
		ButtonHandler.Button button = ModButtons.buttons.FirstOrDefault((ButtonHandler.Button b) => b != null && b.buttonText == "Auto Save");
		if (button != null)
		{
			button.Enabled = Variables.Variables_State_13;
			if (!Variables.Variables_State_13)
			{
				NXOUI.TrackModDisabled("Auto Save");
			}
		}
	}

	private static void CreateGearButton(ButtonHandler.Button parent, float offset)
	{
		CapturedVariables1950 LocalScope5 = new CapturedVariables1950();
		LocalScope5.parent = parent;
		GameObject val = CreateCube("Gear: " + LocalScope5.parent.buttonText, null, keepCollider: true);
		val.transform.localScale = new Vector3(0.0075f, 0.12f, 0.08f);
		val.transform.localPosition = new Vector3(0.07525f, -0.3525f, 0.335f - offset);
		ButtonHandler.BtnCollider btnCollider = val.AddComponent<ButtonHandler.BtnCollider>();
		btnCollider.clickedButton = new ButtonHandler.Button("_GEAR_" + LocalScope5.parent.buttonText, LocalScope5.parent.Page, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.OpenGearMenu(LocalScope5.parent);
		});
		ApplyColorMaterial(val, (Color32)(Settings.CapturedVariables3760_Color_26));
		List<GameObject> outline = AddOutline(val, 2, 0.0065f, animated: false);
		List<GameObject> list = null;
		if (Settings.CapturedVariables3760_Value_01 > 0f)
		{
			list = RoundObject(val, null, null, Settings.CapturedVariables3760_Value_01);
			RegisterColorGroup(ColorRole.Button, val, list);
			Text val2 = CreateText();
			val2.text = "⚙";
			((Component)val2).gameObject.transform.localScale = new Vector3(1.1f, 1.1f, 1f);
			RectTransform component = ((Component)val2).GetComponent<RectTransform>();
			((Transform)component).localPosition = new Vector3(0.05975f, -0.1055f, TextOffset - offset / 2.225f);
			((Transform)component).localRotation = Quaternion.Euler(180f, 90f, 90f);
			component.sizeDelta = CapturedVariables1950_Position_07;
			RegisterButtonTransforms(btnCollider, val, list, outline, ((Component)val2).transform);
		}
		else
		{
			RegisterColorGroup(ColorRole.Button, val, list);
			Text val2 = CreateText();
			val2.text = "⚙";
			((Component)val2).gameObject.transform.localScale = new Vector3(1.1f, 1.1f, 1f);
			RectTransform component = ((Component)val2).GetComponent<RectTransform>();
			((Transform)component).localPosition = new Vector3(0.05975f, -0.1055f, TextOffset - offset / 2.225f);
			((Transform)component).localRotation = Quaternion.Euler(180f, 90f, 90f);
			component.sizeDelta = CapturedVariables1950_Position_07;
			RegisterButtonTransforms(btnCollider, val, list, outline, ((Component)val2).transform);
		}
	}

	private static string BuildAnimatedTitleText()
	{
		CapturedVariables1950_Text_02.Clear();
		CapturedVariables1950_Text_02.Append("<b>");
		string text = "NXO";
		int num = 0;
		if (num < text.Length)
		{
			do
			{
				float num2 = (Mathf.Sin(Time.time * Settings.CapturedVariables3760_Value_10 * 2f - (float)num * 0.5f) + 1f) / 2f;
				Color val = Color.Lerp((Color32)(Settings.CapturedVariables3760_Color_17), (Color32)(Settings.CapturedVariables3760_Color_16), num2);
				CapturedVariables1950_Text_02.Append("<color=#").Append(ColorUtility.ToHtmlStringRGB(val)).Append('>')
					.Append(text[num])
					.Append("</color>");
				num++;
			}
			while (num < text.Length);
		}
		CapturedVariables1950_Text_02.Append("</b>");
		return CapturedVariables1950_Text_02.ToString();
	}

	public static void CreateReturnButton()
	{
		Variables.Variables_Object_07 = CreateCube(null, null, keepCollider: true);
		Variables.Variables_Object_07.transform.localScale = new Vector3(0.0075f, 0.2925f, 0.08f);
		Variables.Variables_Object_07.transform.localPosition = new Vector3(0.07525f, 0f, -0.33f);
		ButtonHandler.BtnCollider btnCollider = Variables.Variables_Object_07.AddComponent<ButtonHandler.BtnCollider>();
		btnCollider.clickedButton = new ButtonHandler.Button("ReturnButton", Category.Home, isToggle: false, isActive: false, null);
		ApplyColorMaterial(Variables.Variables_Object_07, (Color32)(Settings.CapturedVariables3760_Color_26));
		List<GameObject> outline = AddOutline(Variables.Variables_Object_07, 2, 0.0065f);
		List<GameObject> list = null;
		GameObject val;
		if (Settings.CapturedVariables3760_Value_01 > 0f)
		{
			list = RoundObject(Variables.Variables_Object_07, null, null, Settings.CapturedVariables3760_Value_01);
			RegisterColorGroup(ColorRole.Button, Variables.Variables_Object_07, list);
			val = CreateTriggerCube(null, Variables.Variables_Object_03.transform);
			val.transform.localScale = CapturedVariables1950_Position_10;
			val.transform.localPosition = new Vector3(0.05975f, 0f, -0.14875f);
			val.transform.localRotation = Quaternion.Euler(0f, -90f, -90f);
			ApplyEmbeddedTexture(val, "NXO.Resources.homeicon.png");
			RegisterButtonTransforms(btnCollider, Variables.Variables_Object_07, list, outline, val.transform);
		}
		else
		{
			RegisterColorGroup(ColorRole.Button, Variables.Variables_Object_07, list);
			val = CreateTriggerCube(null, Variables.Variables_Object_03.transform);
			val.transform.localScale = CapturedVariables1950_Position_10;
			val.transform.localPosition = new Vector3(0.05975f, 0f, -0.14875f);
			val.transform.localRotation = Quaternion.Euler(0f, -90f, -90f);
			ApplyEmbeddedTexture(val, "NXO.Resources.homeicon.png");
			RegisterButtonTransforms(btnCollider, Variables.Variables_Object_07, list, outline, val.transform);
		}
	}

	public static void HandleJoinedRoom()
	{
		if (!CapturedVariables1950_State_04)
		{
			CapturedVariables1950_State_04 = true;
			NetworkingLibrary.BroadcastPresence();
			Room.ForceCreateRoom_StateMachine11_Text_01 = PhotonNetwork.CurrentRoom.Name;
			ShowRoomNotification("Joined Code `" + Room.ForceCreateRoom_StateMachine11_Text_01 + "`");
			((MonoBehaviour)Variables.Variables_Reference_06).StartCoroutine(PlayersActionList.RefreshPlayersListDelayed());
		}
	}

	private static void DestroyClickerObjects()
	{
		if ((Object)(object)Variables.Variables_Object_10 != (Object)null)
		{
			Object.Destroy((Object)(object)Variables.Variables_Object_10);
			Variables.Variables_Object_10 = null;
			if (!((Object)(object)CapturedVariables1950_Material_01 != (Object)null))
			{
				return;
			}
		}
		else if (!((Object)(object)CapturedVariables1950_Material_01 != (Object)null))
		{
			return;
		}
		Object.Destroy((Object)(object)CapturedVariables1950_Material_01);
		CapturedVariables1950_Material_01 = null;
	}

	public static void BuildMenu(bool openCall = false)
	{
		CapturedVariables1950_State_02 = true;
		CreateMenuRoot();
		CreateMenuBackground();
		CreateCanvas();
		CreateAccentStrip();
		CreateSearchButton();
		CreateSearchField();
		CreateDisconnectButton();
		CreateFpsPanel();
		CreateReturnButton();
		CreatePageArrow("<", 0.285f);
		CreatePageArrow(">", -0.285f);
		if (PlayersActionList.CapturedVariables70_Reference_03 != null && Variables.currentPage == Category.Player_Action)
		{
			PlayersActionList.CreatePlayerCameraDisplay();
			RebuildVisibleButtons();
			Transform transform = Variables.Variables_Object_14.transform;
			transform.localScale *= CapturedVariables1950_Value_04;
			if (openCall)
			{
				goto Branch_0108;
			}
		}
		else
		{
			RebuildVisibleButtons();
			Transform transform2 = Variables.Variables_Object_14.transform;
			transform2.localScale *= CapturedVariables1950_Value_04;
			if (openCall)
			{
				goto Branch_0108;
			}
		}
		SetUiLayerRecursively(Variables.Variables_Object_14);
		return;
		Branch_0108:
		Vector3 localScale = Variables.Variables_Object_14.transform.localScale;
		Variables.Variables_Object_14.transform.localScale = Vector3.zero;
		((MonoBehaviour)Variables.Variables_Reference_06).StartCoroutine(ScaleObject(Variables.Variables_Object_14, localScale));
		SetUiLayerRecursively(Variables.Variables_Object_14);
	}

	private static Text CreateText(int fontSize = 3)
	{
		GameObject val = new GameObject();
		val.transform.SetParent(Variables.Variables_Object_03.transform, false);
		ObjectSink.Add(val);
		Text val2 = val.AddComponent<Text>();
		val2.font = CurrentFont;
		val2.fontStyle = (FontStyle)0;
		((Graphic)val2).color = (Color32)((Color32)(Color.white));
		val2.fontSize = fontSize;
		val2.alignment = (TextAnchor)4;
		val2.resizeTextForBestFit = true;
		val2.resizeTextMinSize = 0;
		return val2;
	}

	private static void UpdatePcMenu()
	{
		bool shouldOpen = Variables.Variables_State_03 && !Variables.Variables_State_02 && !InputHandler.IsLeftPrimaryPressed() && !InputHandler.IsRightPrimaryPressed() && !Variables.Variables_State_16;
		if (shouldOpen && !CapturedVariables1950_State_08)
		{
			Variables.Variables_State_15 = true;
			CapturedVariables1950_State_08 = true;
			if (!((Object)(object)Variables.Variables_Object_14))
			{
				BuildMenu(openCall: true);
				if (Variables.Variables_State_14)
				{
					AssetHandler.PlayAudioClip(Variables.Variables_Object_13, Variables.Variables_Audio_03, 1.25f);
				}
			}
			return;
		}
		if (!shouldOpen && CapturedVariables1950_State_08)
		{
			Variables.Variables_State_15 = false;
			CapturedVariables1950_State_08 = false;
			if (!((Object)(object)Variables.Variables_Object_14 != (Object)null))
			{
				return;
			}
			if (Variables.Variables_State_14)
			{
				AssetHandler.PlayAudioClip(Variables.Variables_Object_13, Variables.Variables_Audio_02, 1.25f);
				((MonoBehaviour)Variables.Variables_Reference_06).StartCoroutine(ScaleObject(Variables.Variables_Object_14, Vector3.zero, cleanupAfter: true));
				DestroyClickerObjects();
				if (!SearchAndKeyboard.KeyCollider_State_02)
				{
					return;
				}
			}
			else
			{
				((MonoBehaviour)Variables.Variables_Reference_06).StartCoroutine(ScaleObject(Variables.Variables_Object_14, Vector3.zero, cleanupAfter: true));
				DestroyClickerObjects();
				if (!SearchAndKeyboard.KeyCollider_State_02)
				{
					return;
				}
			}
			SearchAndKeyboard.CloseSearch();
			return;
		}
		if (!Variables.Variables_State_15 || !((Object)(object)Variables.Variables_Object_14 != (Object)null))
		{
			return;
		}
		GameObject obj = Variables.Variables_Object_13;
		UpdateClickerTransform((obj != null) ? obj.transform : null);
		UpdateTitleText();
		UpdateDisconnectIcon();
		UpdateFpsDisplay();
		if (!((Object)(object)Variables.Variables_Object_13 != (Object)null))
		{
			return;
		}
		Transform transform = Variables.Variables_Object_13.transform;
		Variables.Variables_Object_14.transform.SetParent(transform, true);
		Variables.Variables_Object_14.transform.position = transform.position + transform.rotation * Vector3.forward * 0.5f + Vector3.down * 0.025f;
		Variables.Variables_Object_14.transform.rotation = Quaternion.LookRotation(-transform.forward, Vector3.up);
		Variables.Variables_Object_14.transform.Rotate(Vector3.up, -90f);
		Variables.Variables_Object_14.transform.Rotate(Vector3.right, -90f);
		RaycastHit[] array2;
		int i;
		Ray val;
		RaycastHit[] array;
		if (Mouse.current != null && Mouse.current.leftButton.isPressed)
		{
			if ((Object)(object)Variables.Variables_Object_10 != (Object)null)
			{
				MeshRenderer component = Variables.Variables_Object_10.GetComponent<MeshRenderer>();
				if ((Object)(object)component != (Object)null && !((Renderer)component).enabled)
				{
					((Renderer)component).enabled = true;
					if ((Object)(object)CapturedVariables1950_Reference_07 == (Object)null)
					{
						goto Branch_049c;
					}
				}
				else if ((Object)(object)CapturedVariables1950_Reference_07 == (Object)null)
				{
					goto Branch_049c;
				}
			}
			else if ((Object)(object)CapturedVariables1950_Reference_07 == (Object)null)
			{
				goto Branch_049c;
			}
			val = CapturedVariables1950_Reference_07.ScreenPointToRay((Vector2)(((InputControl<Vector2>)(object)((Pointer)Mouse.current).position).ReadValue()));
			array = Physics.RaycastAll(val, 100f, UILayerMask);
			array2 = array;
			i = 0;
			goto Branch_05c5;
		}
		if ((Object)(object)Variables.Variables_Object_10 != (Object)null && Mouse.current != null && !Mouse.current.leftButton.isPressed)
		{
			MeshRenderer component2 = Variables.Variables_Object_10.GetComponent<MeshRenderer>();
			if ((Object)(object)component2 != (Object)null && ((Renderer)component2).enabled)
			{
				((Renderer)component2).enabled = false;
			}
		}
		return;
		Branch_049c:
		CapturedVariables1950_Reference_07 = Variables.Variables_Object_13.GetComponent<Camera>();
		val = CapturedVariables1950_Reference_07.ScreenPointToRay((Vector2)(((InputControl<Vector2>)(object)((Pointer)Mouse.current).position).ReadValue()));
		array = Physics.RaycastAll(val, 100f, UILayerMask);
		array2 = array;
		i = 0;
		Branch_05c5:
		for (; i < array2.Length; i++)
		{
			RaycastHit val2 = array2[i];
			Collider collider = ((RaycastHit)val2).collider;
			ButtonHandler.BtnCollider btnCollider = ((collider != null) ? ((Component)collider).GetComponent<ButtonHandler.BtnCollider>() : null);
			if ((Object)(object)btnCollider != (Object)null && (Object)(object)Variables.Variables_Object_10 != (Object)null)
			{
				btnCollider.OnTriggerEnter(Variables.Variables_Object_10.GetComponent<Collider>());
				break;
			}
		}
	}

	private static GameObject CreateTriggerCube(string name = null, Transform parent = null)
	{
		GameObject result = CreatePrimitive((PrimitiveType)5, name, parent);
		((Collider)result.AddComponent<BoxCollider>()).isTrigger = true;
		return result;
	}

	public static void RegisterFont(string description, Font font)
	{
		if ((Object)(object)font != (Object)null)
		{
			CapturedVariables1950_Reference_02.Add(new FontEntry
			{
				Description = description,
				Font = font
			});
		}
	}

	private static List<GameObject> AddOutline(GameObject target, int requiredMode, float zGrow = 0.0125f, bool animated = true, float roundness = -1f, int seg = 5)
	{
		if (Settings.CapturedVariables3760_Index_32 < requiredMode)
		{
			return null;
		}
		GameObject val;
		if (!animated)
		{
			Color color = Color.Lerp(CapturedVariables1950_Color_02, CapturedVariables1950_Color_06, 0.5f);
			val = CreateOutlinePart(target, Variables.Variables_Object_14.transform, zGrow, color);
			if (!(roundness < 0f))
			{
				goto Branch_00ae;
			}
		}
		else
		{
			Color color = (Color32)(Settings.CapturedVariables3760_Color_28);
			val = CreateOutlinePart(target, Variables.Variables_Object_14.transform, zGrow, color);
			if (!(roundness < 0f))
			{
				goto Branch_00ae;
			}
		}
		float num = Settings.CapturedVariables3760_Value_01;
		List<GameObject> list = null;
		if (!(num > 0f))
		{
			goto Branch_011c;
		}
		goto Branch_00f5;
		Branch_0131:
		RegisterColorGroup(ColorRole.Outline, val, list);
		List<GameObject> list2 = new List<GameObject>(2) { val };
		if (list == null)
		{
			goto Branch_01a0;
		}
		goto Branch_018a;
		Branch_011c:
		if (!animated)
		{
			goto Branch_0162;
		}
		goto Branch_0131;
		Branch_01a0:
		return list2;
		Branch_018a:
		list2.AddRange(list);
		return list2;
		Branch_0162:
		list2 = new List<GameObject>(2) { val };
		if (list == null)
		{
			goto Branch_01a0;
		}
		goto Branch_018a;
		Branch_00ae:
		num = roundness;
		list = null;
		if (!(num > 0f))
		{
			goto Branch_011c;
		}
		Branch_00f5:
		list = RoundObject(val, null, null, num, 2455, seg);
		if (!animated)
		{
			goto Branch_0162;
		}
		goto Branch_0131;
	}

	public static Material CreateMaterial(Color color, int renderQueue = 2460)
	{
		if (Settings.CapturedVariables3760_Value_07 >= 1f)
		{
			return new Material(Variables.Variables_Reference_10)
			{
				color = color
			};
		}
		Material val = new Material(Variables.Variables_Reference_11);
		Color color2 = color;
		color2.a = Settings.CapturedVariables3760_Value_07;
		val.color = color2;
		val.renderQueue = renderQueue;
		return val;
	}

	public static void HandlePlayerJoined(NetPlayer player)
	{
		NetworkingLibrary.BroadcastPresence();
		if (player != NetworkSystem.Instance.LocalPlayer)
		{
			ShowRoomNotification("`" + player.NickName + "` Joined");
			if (Variables.currentPage != Category.Players)
			{
				return;
			}
		}
		else if (Variables.currentPage != Category.Players)
		{
			return;
		}
		PlayersActionList.ResetPlayersList();
	}

	public static void ShowRoomNotification(string message)
	{
		if (CapturedVariables1950_State_07)
		{
			NotificationLib.ShowNotification(NotificationLib.NotificationType.Room, message);
			if ((Object)(object)Variables.Variables_Audio_01 != (Object)null && Variables.Variables_State_09)
			{
				AssetHandler.PlayAudioClip(Variables.Variables_Object_13, Variables.Variables_Audio_01);
			}
		}
	}

	public static void LoadBundledFont(string description, string bundlePath, string fontName)
	{
		AssetBundle val = AssetHandler.LoadAssetBundle(bundlePath);
		if (!((Object)(object)val == (Object)null))
		{
			Font val2 = val.LoadAsset<Font>(fontName);
			if ((Object)(object)val2 != (Object)null)
			{
				RegisterFont(description, val2);
			}
		}
	}

	private void LateUpdate()
	{
		if (NetworkingLibrary.PollCommands_StateMachine31_State_01 || (Object)(object)Variables.Variables_Reference_06 == (Object)null || (Object)(object)Variables.Variables_Reference_09 == (Object)null)
		{
			return;
		}
		if (Variables.Variables_State_13 && Time.time - CapturedVariables1950_Value_02 >= 60f)
		{
			CapturedVariables1950_Value_02 = Time.time;
			ButtonHandler.SaveAutoState();
			TickEnabledMods();
			if (Keyboard.current != null)
			{
				goto Branch_0111;
			}
		}
		else
		{
			TickEnabledMods();
			if (Keyboard.current != null)
			{
				goto Branch_0111;
			}
		}
		goto Branch_016e;
		Branch_0191:
		Variables.Variables_State_03 = !Variables.Variables_State_03;
		ControllerEmulator.UpdateEmulatedInput();
		Macros.UpdateMacroPlayback();
		RigManager.UpdateGhostRig();
		UpdateRoomState();
		UpdatePcMenu();
		UpdateVrMenu();
		SearchAndKeyboard.PollPhysicalKeyboard();
		UpdateMenuColors();
		SearchAndKeyboard.UpdateCursorBlink();
		return;
		Branch_01d5:
		ControllerEmulator.UpdateEmulatedInput();
		Macros.UpdateMacroPlayback();
		RigManager.UpdateGhostRig();
		UpdateRoomState();
		UpdatePcMenu();
		UpdateVrMenu();
		SearchAndKeyboard.PollPhysicalKeyboard();
		UpdateMenuColors();
		SearchAndKeyboard.UpdateCursorBlink();
		return;
		Branch_0111:
		if (!((ButtonControl)Keyboard.current.rightAltKey).wasPressedThisFrame)
		{
			goto Branch_016e;
		}
		NXOUI.CapturedVariables1190_State_04 = !NXOUI.CapturedVariables1190_State_04;
		if (!UnityInput.Current.GetKeyDown(Variables.Variables_Reference_01))
		{
			goto Branch_01d5;
		}
		goto Branch_0191;
		Branch_016e:
		if (!UnityInput.Current.GetKeyDown(Variables.Variables_Reference_01))
		{
			goto Branch_01d5;
		}
		goto Branch_0191;
	}

	private static GameObject CreateCube(string name = null, Transform parent = null, bool keepCollider = false)
	{
		return CreatePrimitive((PrimitiveType)3, name, parent, keepCollider);
	}

	private static bool ColorsEqual(Color32 a, Color32 b)
	{
		if (a.r == b.r && a.g == b.g)
		{
			return a.b == b.b;
		}
		return false;
	}

	private static void ApplyEmbeddedTexture(GameObject obj, string resourcePath, Color? tint = null)
	{
		Texture2D val = AssetHandler.LoadEmbeddedTexture(resourcePath);
		if (!((Object)(object)val == (Object)null))
		{
			Material val2 = new Material(Variables.Variables_Reference_11)
			{
				mainTexture = (Texture)(object)val
			};
			if (tint.HasValue)
			{
				val2.color = tint.Value;
				obj.GetComponent<Renderer>().sharedMaterial = TrackMaterial(val2);
			}
			else
			{
				obj.GetComponent<Renderer>().sharedMaterial = TrackMaterial(val2);
			}
		}
	}

	private static void ApplyColorMaterial(GameObject obj, Color color)
	{
		Renderer component = obj.GetComponent<Renderer>();
		if (!((Object)(object)component == (Object)null))
		{
			component.sharedMaterial = TrackMaterial(CreateMaterial(color));
		}
	}

	private static IEnumerator FadeInMenuItems()
	{
		float elapsed = 0f;
		if (elapsed < 0.24f)
		{
			do
			{
				float f = elapsed / 0.24f;
				int i = 0;
				if (i < CapturedVariables1950_Reference_08.Count)
				{
					do
					{
						ApplyFadeFactor(i, f);
						i++;
					}
					while (i < CapturedVariables1950_Reference_08.Count);
				}
				elapsed += Time.deltaTime;
				yield return null;
			}
			while (elapsed < 0.24f);
		}
		int i2 = 0;
		if (i2 < CapturedVariables1950_Reference_08.Count)
		{
			do
			{
				ApplyFadeFactor(i2, 1f);
				i2++;
			}
			while (i2 < CapturedVariables1950_Reference_08.Count);
		}
		CapturedVariables1950_Routine_01 = null;
	}

	private static void CreatePageArrow(string direction, float yPos)
	{
		GameObject val = CreateCube(null, null, keepCollider: true);
		val.transform.localScale = new Vector3(0.0075f, 0.25f, 0.08f);
		val.transform.localPosition = new Vector3(0.07525f, yPos, -0.33f);
		ButtonHandler.BtnCollider btnCollider = val.AddComponent<ButtonHandler.BtnCollider>();
		btnCollider.clickedButton = new ButtonHandler.Button(direction, Category.Home, isToggle: false, isActive: false, null);
		ApplyColorMaterial(val, (Color32)(Settings.CapturedVariables3760_Color_26));
		List<GameObject> outline = AddOutline(val, 2, 0.0065f);
		List<GameObject> list = null;
		RectTransform val3;
		if (Settings.CapturedVariables3760_Value_01 > 0f)
		{
			list = RoundObject(val, null, null, Settings.CapturedVariables3760_Value_01);
			RegisterColorGroup(ColorRole.Button, val, list);
			Text val2 = CreateText();
			val2.text = direction;
			((Component)val2).gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			val3 = ((Component)val2).GetComponent<RectTransform>();
			((Transform)val3).localPosition = Vector3.zero;
			val3.sizeDelta = CapturedVariables1950_Position_03;
			((Transform)val3).localRotation = Quaternion.Euler(180f, 90f, 90f);
			((Transform)val3).localPosition = new Vector3(0.05975f, (direction == "<") ? 0.083f : (-0.083f), PageZOffset);
			RegisterButtonTransforms(btnCollider, val, list, outline, ((Component)val2).transform);
		}
		else
		{
			RegisterColorGroup(ColorRole.Button, val, list);
			Text val2 = CreateText();
			val2.text = direction;
			((Component)val2).gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			val3 = ((Component)val2).GetComponent<RectTransform>();
			((Transform)val3).localPosition = Vector3.zero;
			val3.sizeDelta = CapturedVariables1950_Position_03;
			((Transform)val3).localRotation = Quaternion.Euler(180f, 90f, 90f);
			((Transform)val3).localPosition = new Vector3(0.05975f, (direction == "<") ? 0.083f : (-0.083f), PageZOffset);
			RegisterButtonTransforms(btnCollider, val, list, outline, ((Component)val2).transform);
		}
	}

	private static void ApplyMenuScale()
	{
		if ((Object)(object)Variables.Variables_Object_14 != (Object)null)
		{
			Variables.Variables_Object_14.transform.localScale = CapturedVariables1950_Position_12 * CapturedVariables1950_Value_04;
		}
	}

	private static void ApplyTransparentColorMaterial(GameObject obj, Color color)
	{
		Renderer component = obj.GetComponent<Renderer>();
		if (!((Object)(object)component == (Object)null))
		{
			Material mat;
			if (Settings.CapturedVariables3760_Value_07 >= 1f)
			{
				mat = new Material(Variables.Variables_Reference_10)
				{
					color = color
				};
				component.sharedMaterial = TrackMaterial(mat);
				return;
			}
			mat = new Material(Variables.Variables_Reference_11);
			Color color2 = color;
			color2.a = Settings.CapturedVariables3760_Value_07;
			mat.color = color2;
			mat.renderQueue = 2460;
			component.sharedMaterial = TrackMaterial(mat);
		}
	}

	private static string BuildGradientTitleText()
	{
		CapturedVariables1950_Text_02.Clear();
		CapturedVariables1950_Text_02.Append("<b>");
		string text = "NXO";
		int num = 0;
		if (num < text.Length)
		{
			while (true)
			{
				if (text.Length <= 1)
				{
					float num2 = 0f;
					Color val = Color.Lerp((Color32)(Settings.CapturedVariables3760_Color_17), (Color32)(Settings.CapturedVariables3760_Color_16), num2);
					CapturedVariables1950_Text_02.Append("<color=#").Append(ColorUtility.ToHtmlStringRGB(val)).Append('>')
						.Append(text[num])
						.Append("</color>");
					num++;
					if (num >= text.Length)
					{
						break;
					}
				}
				else
				{
					float num2 = (float)num / (float)(text.Length - 1);
					Color val = Color.Lerp((Color32)(Settings.CapturedVariables3760_Color_17), (Color32)(Settings.CapturedVariables3760_Color_16), num2);
					CapturedVariables1950_Text_02.Append("<color=#").Append(ColorUtility.ToHtmlStringRGB(val)).Append('>')
						.Append(text[num])
						.Append("</color>");
					num++;
					if (num >= text.Length)
					{
						break;
					}
				}
			}
		}
		CapturedVariables1950_Text_02.Append("</b>");
		return CapturedVariables1950_Text_02.ToString();
	}

	public static void CreateDisconnectButton()
	{
		GameObject val = CreateCube("DisconnectBg", null, keepCollider: true);
		val.transform.localScale = new Vector3(0.0075f, CapturedVariables1950_Position_01.x * 2.504f, CapturedVariables1950_Position_01.x * 1.6f);
		val.transform.localPosition = new Vector3(0.07525f, -0.2375f, 0.45f);
		ButtonHandler.BtnCollider btnCollider = val.AddComponent<ButtonHandler.BtnCollider>();
		btnCollider.clickedButton = new ButtonHandler.Button("Disconnect Button", Category.Home, isToggle: false, isActive: false, delegate
		{
			if (PhotonNetwork.InRoom)
			{
				Room.Disconnect();
			}
			else
			{
				Room.JoinRandomPublic();
			}
		});
		ApplyColorMaterial(val, (Color32)(Settings.CapturedVariables3760_Color_26));
		List<GameObject> outline = AddOutline(val, 2, 0.0065f, animated: false);
		List<GameObject> list = null;
		if (Settings.CapturedVariables3760_Value_01 > 0f)
		{
			list = RoundObject(val, null, null, Settings.CapturedVariables3760_Value_01);
			RegisterColorGroup(ColorRole.Button, val, list);
			CapturedVariables1950_Object_04 = CreateTriggerCube();
			CapturedVariables1950_Object_04.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
			CapturedVariables1950_Object_04.transform.localScale = CapturedVariables1950_Position_01;
			CapturedVariables1950_Object_04.transform.localPosition = new Vector3(0.079535715f, -0.2375f, 0.45f);
			ApplyEmbeddedTexture(CapturedVariables1950_Object_04, "NXO.Resources.disconnecticon.png", Color.white);
			UpdateDisconnectIcon();
			RegisterButtonTransforms(btnCollider, val, list, outline, CapturedVariables1950_Object_04.transform);
		}
		else
		{
			RegisterColorGroup(ColorRole.Button, val, list);
			CapturedVariables1950_Object_04 = CreateTriggerCube();
			CapturedVariables1950_Object_04.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
			CapturedVariables1950_Object_04.transform.localScale = CapturedVariables1950_Position_01;
			CapturedVariables1950_Object_04.transform.localPosition = new Vector3(0.079535715f, -0.2375f, 0.45f);
			ApplyEmbeddedTexture(CapturedVariables1950_Object_04, "NXO.Resources.disconnecticon.png", Color.white);
			UpdateDisconnectIcon();
			RegisterButtonTransforms(btnCollider, val, list, outline, CapturedVariables1950_Object_04.transform);
		}
	}

	private static Material TrackMaterial(Material mat)
	{
		if ((Object)(object)mat != (Object)null)
		{
			MaterialSink.Add(mat);
			return mat;
		}
		return mat;
	}

	public static void EnsureSearchField()
	{
		if (!((Object)(object)Variables.Variables_Object_14 == (Object)null) && !((Object)(object)Variables.Variables_Object_02 != (Object)null))
		{
			CreateSearchField();
			SetUiLayerRecursively(Variables.Variables_Object_14);
			UpdateSearchButtonColorRole();
			ApplyMenuScale();
		}
	}

	public static void MarkColorsDirty()
	{
		CapturedVariables1950_State_03 = true;
	}

	private static void ApplyFadeFactor(int i, float factor)
	{
		FadeItem fadeItem = CapturedVariables1950_Reference_08[i];
		if ((Object)(object)fadeItem.txt != (Object)null)
		{
			Color color = ((Graphic)fadeItem.txt).color;
			color.a = fadeItem.origA * factor;
			((Graphic)fadeItem.txt).color = color;
		}
		else if ((Object)(object)fadeItem.r != (Object)null && (Object)(object)fadeItem.r.sharedMaterial != (Object)null)
		{
			Color color2 = Color.Lerp((Color32)(Settings.CapturedVariables3760_Color_19), fadeItem.target, factor);
			color2.a = fadeItem.target.a;
			fadeItem.r.sharedMaterial.color = color2;
		}
	}

	public static void HandlePlayerLeft(NetPlayer player)
	{
		if (player != NetworkSystem.Instance.LocalPlayer)
		{
			ShowRoomNotification("`" + player.NickName + "` Left");
			if (Variables.currentPage != Category.Players)
			{
				return;
			}
		}
		else if (Variables.currentPage != Category.Players)
		{
			return;
		}
		PlayersActionList.ResetPlayersList();
	}

	public static void CreateSearchButton()
	{
		if ((Object)(object)Variables.Variables_Object_12 != (Object)null)
		{
			return;
		}
		GameObject val = CreateCube("SearchBg", null, keepCollider: true);
		val.transform.localScale = new Vector3(0.0075f, CapturedVariables1950_Position_01.x * 2.504f, CapturedVariables1950_Position_01.x * 1.6f);
		val.transform.localPosition = new Vector3(0.07525f, -0.35f, 0.45f);
		ButtonHandler.BtnCollider btnCollider = val.AddComponent<ButtonHandler.BtnCollider>();
		btnCollider.clickedButton = new ButtonHandler.Button("Toggle Search Button", Category.Home, isToggle: false, isActive: false, null);
		ApplyColorMaterial(val, (Color32)(SearchAndKeyboard.KeyCollider_State_02 ? Settings.CapturedVariables3760_Color_08 : Settings.CapturedVariables3760_Color_26));
		List<GameObject> outline = AddOutline(val, 2, 0.0065f, animated: false);
		List<GameObject> list = null;
		if (Settings.CapturedVariables3760_Value_01 > 0f)
		{
			list = RoundObject(val, null, null, Settings.CapturedVariables3760_Value_01);
			if (!SearchAndKeyboard.KeyCollider_State_02)
			{
				goto Branch_013d;
			}
		}
		else if (!SearchAndKeyboard.KeyCollider_State_02)
		{
			goto Branch_013d;
		}
		RegisterColorGroup(ColorRole.EnabledButton, val, list);
		if (CapturedVariables1950_Color_03.Count <= 0)
		{
			goto Branch_0185;
		}
		goto Branch_0230;
		Branch_0185:
		CapturedVariables1950_Color_04 = null;
		Variables.Variables_Object_12 = CreateTriggerCube();
		Variables.Variables_Object_12.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
		Variables.Variables_Object_12.transform.localScale = CapturedVariables1950_Position_01;
		Variables.Variables_Object_12.transform.localPosition = new Vector3(0.079535715f, -0.35f, 0.45f);
		ApplyEmbeddedTexture(Variables.Variables_Object_12, "NXO.Resources.searchicon.png");
		RegisterButtonTransforms(btnCollider, val, list, outline, Variables.Variables_Object_12.transform);
		return;
		Branch_013d:
		RegisterColorGroup(ColorRole.Button, val, list);
		if (CapturedVariables1950_Color_03.Count <= 0)
		{
			goto Branch_0185;
		}
		Branch_0230:
		CapturedVariables1950_Color_04 = CapturedVariables1950_Color_03[CapturedVariables1950_Color_03.Count - 1];
		Variables.Variables_Object_12 = CreateTriggerCube();
		Variables.Variables_Object_12.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
		Variables.Variables_Object_12.transform.localScale = CapturedVariables1950_Position_01;
		Variables.Variables_Object_12.transform.localPosition = new Vector3(0.079535715f, -0.35f, 0.45f);
		ApplyEmbeddedTexture(Variables.Variables_Object_12, "NXO.Resources.searchicon.png");
		RegisterButtonTransforms(btnCollider, val, list, outline, Variables.Variables_Object_12.transform);
	}

	private static void CreateButtonBackdrop(float offset)
	{
		GameObject val = CreateCube("Backdrop");
		val.transform.localScale = new Vector3(0.0015f, 0.82f, 0.08f);
		val.transform.localPosition = new Vector3(0.0685f, 0f, 0.335f - offset);
		ApplyColorMaterial(val, (Color32)(Settings.CapturedVariables3760_Color_26));
		List<GameObject> extraParts = RoundObject(val, null, null, 1.6f, 2461, 12);
		RegisterColorGroup(ColorRole.Button, val, extraParts);
	}

	private static GameObject CreatePrimitive(PrimitiveType type, string name = null, Transform parent = null, bool keepCollider = false, bool triggerCollider = true)
	{
		GameObject val = GameObject.CreatePrimitive(type);
		Rigidbody component;
		if (name != null)
		{
			((Object)val).name = name;
			component = val.GetComponent<Rigidbody>();
			if ((Object)(object)component != (Object)null)
			{
				goto Branch_006a;
			}
		}
		else
		{
			component = val.GetComponent<Rigidbody>();
			if ((Object)(object)component != (Object)null)
			{
				goto Branch_006a;
			}
		}
		Collider component2 = val.GetComponent<Collider>();
		if (!((Object)(object)component2 != (Object)null))
		{
			goto Branch_01a9;
		}
		goto Branch_00b4;
		Branch_01a9:
		Transform transform = val.transform;
		object obj = parent;
		if (obj == null)
		{
			GameObject obj2 = Variables.Variables_Object_14;
			obj = ((obj2 != null) ? obj2.transform : null);
		}
		transform.SetParent((Transform)obj, true);
		val.transform.localRotation = Quaternion.identity;
		ObjectSink.Add(val);
		return val;
		Branch_00b4:
		if (!keepCollider)
		{
			Object.Destroy((Object)(object)component2);
		}
		else
		{
			BoxCollider val2 = (BoxCollider)(object)((component2 is BoxCollider) ? component2 : null);
			if (val2 != null)
			{
				((Collider)val2).isTrigger = triggerCollider;
				Transform transform2 = val.transform;
				object obj3 = parent;
				if (obj3 == null)
				{
					GameObject obj4 = Variables.Variables_Object_14;
					obj3 = ((obj4 != null) ? obj4.transform : null);
				}
				transform2.SetParent((Transform)obj3, true);
				val.transform.localRotation = Quaternion.identity;
				ObjectSink.Add(val);
				return val;
			}
		}
		Transform transform3 = val.transform;
		object obj5 = parent;
		if (obj5 == null)
		{
			GameObject obj6 = Variables.Variables_Object_14;
			obj5 = ((obj6 != null) ? obj6.transform : null);
		}
		transform3.SetParent((Transform)obj5, true);
		val.transform.localRotation = Quaternion.identity;
		ObjectSink.Add(val);
		return val;
		Branch_006a:
		Object.Destroy((Object)(object)component);
		component2 = val.GetComponent<Collider>();
		if (!((Object)(object)component2 != (Object)null))
		{
			goto Branch_01a9;
		}
		goto Branch_00b4;
	}

	public static void DestroyAndClear<T>(ref T obj, float delay = 0f) where T : UnityEngine.Object
	{
		if (!((Object)(object)obj == (Object)null))
		{
			object obj2 = obj;
			Component val = (Component)((obj2 is Component) ? obj2 : null);
			if (val != null && (Object)(object)val != (Object)null)
			{
				Object.Destroy((Object)(object)val.gameObject, delay);
				obj = default(T);
			}
			else
			{
				Object.Destroy((Object)(object)obj, delay);
				obj = default(T);
			}
		}
	}

	public static void RedrawMenu(int animate = 0)
	{
		NXOUI.CapturedVariables1190_Index_02++;
		if ((Object)(object)Variables.Variables_Object_14 == (Object)null)
		{
			return;
		}
		int num;
		if (CapturedVariables1950_Routine_01 != null && (Object)(object)Variables.Variables_Reference_06 != (Object)null)
		{
			((MonoBehaviour)Variables.Variables_Reference_06).StopCoroutine(CapturedVariables1950_Routine_01);
			CapturedVariables1950_Routine_01 = null;
			CapturedVariables1950_Reference_08.Clear();
			num = 0;
		}
		else
		{
			CapturedVariables1950_Reference_08.Clear();
			num = 0;
		}
		if (num < CapturedVariables1950_Object_05.Count)
		{
			while (true)
			{
				if ((Object)(object)CapturedVariables1950_Object_05[num] != (Object)null)
				{
					Object.Destroy((Object)(object)CapturedVariables1950_Object_05[num]);
					num++;
					if (num >= CapturedVariables1950_Object_05.Count)
					{
						break;
					}
				}
				else
				{
					num++;
					if (num >= CapturedVariables1950_Object_05.Count)
					{
						break;
					}
				}
			}
		}
		CapturedVariables1950_Object_05.Clear();
		int num2 = 0;
		if (num2 < CapturedVariables1950_Material_04.Count)
		{
			while (true)
			{
				if ((Object)(object)CapturedVariables1950_Material_04[num2] != (Object)null)
				{
					Object.Destroy((Object)(object)CapturedVariables1950_Material_04[num2]);
					num2++;
					if (num2 >= CapturedVariables1950_Material_04.Count)
					{
						break;
					}
				}
				else
				{
					num2++;
					if (num2 >= CapturedVariables1950_Material_04.Count)
					{
						break;
					}
				}
			}
		}
		CapturedVariables1950_Material_04.Clear();
		int num3 = 0;
		if (num3 < CapturedVariables1950_Reference_05.Count)
		{
			while (true)
			{
				if ((Object)(object)CapturedVariables1950_Reference_05[num3] != (Object)null)
				{
					Object.Destroy((Object)(object)CapturedVariables1950_Reference_05[num3]);
					num3++;
					if (num3 >= CapturedVariables1950_Reference_05.Count)
					{
						break;
					}
				}
				else
				{
					num3++;
					if (num3 >= CapturedVariables1950_Reference_05.Count)
					{
						break;
					}
				}
			}
		}
		CapturedVariables1950_Reference_05.Clear();
		CapturedVariables1950_Color_03.RemoveAll((TrackedColorGroup g) => g.renderers.Count == 0 || g.renderers.All((Renderer r) => (Object)(object)r == (Object)null));
		CapturedVariables1950_Index_01 = -1;
		CapturedVariables1950_State_05 = false;
		CapturedVariables1950_Text_01 = null;
		RebuildVisibleButtons();
		SetUiLayerRecursively(Variables.Variables_Object_14);
		if (animate != 0 && CapturedVariables1950_State_09)
		{
			PrepareMenuFadeIn();
		}
	}

	private static void CreateIncrementalArrow(string name, ButtonHandler.Button parent, float offset, float yPos, string symbol, string suffix)
	{
		GameObject val = CreateCube(name, null, keepCollider: true);
		val.transform.localScale = new Vector3(0.0075f, 0.12f, 0.08f);
		val.transform.localPosition = new Vector3(0.07525f, yPos, 0.335f - offset);
		ButtonHandler.BtnCollider btnCollider = val.AddComponent<ButtonHandler.BtnCollider>();
		btnCollider.clickedButton = new ButtonHandler.Button(parent.buttonText + suffix, parent.Page, isToggle: false, isActive: false, null);
		ApplyColorMaterial(val, (Color32)(Settings.CapturedVariables3760_Color_26));
		List<GameObject> outline = AddOutline(val, 2, 0.0065f, animated: false);
		List<GameObject> list = null;
		if (Settings.CapturedVariables3760_Value_01 > 0f)
		{
			list = RoundObject(val, null, null, Settings.CapturedVariables3760_Value_01);
			RegisterColorGroup(ColorRole.Button, val, list);
			Text val2 = CreateText();
			val2.text = symbol;
			((Component)val2).gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1f);
			RectTransform component = ((Component)val2).GetComponent<RectTransform>();
			((Transform)component).localPosition = new Vector3(0.05975f, yPos * 0.288f, TextOffset - offset / 2.225f);
			component.sizeDelta = CapturedVariables1950_Position_14;
			((Transform)component).localRotation = Quaternion.Euler(180f, 90f, 90f);
			RegisterButtonTransforms(btnCollider, val, list, outline, ((Component)val2).transform);
			if (parent != ButtonHandler.CapturedVariables570_Button_03)
			{
				return;
			}
		}
		else
		{
			RegisterColorGroup(ColorRole.Button, val, list);
			Text val2 = CreateText();
			val2.text = symbol;
			((Component)val2).gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1f);
			RectTransform component = ((Component)val2).GetComponent<RectTransform>();
			((Transform)component).localPosition = new Vector3(0.05975f, yPos * 0.288f, TextOffset - offset / 2.225f);
			component.sizeDelta = CapturedVariables1950_Position_14;
			((Transform)component).localRotation = Quaternion.Euler(180f, 90f, 90f);
			RegisterButtonTransforms(btnCollider, val, list, outline, ((Component)val2).transform);
			if (parent != ButtonHandler.CapturedVariables570_Button_03)
			{
				return;
			}
		}
		if (suffix == "_UP" == ButtonHandler.CapturedVariables570_State_02)
		{
			ButtonHandler.CapturedVariables570_Button_03 = null;
			btnCollider.PlayBounce();
		}
	}

	private static void UpdateMenuColors()
	{
		if ((Object)(object)Variables.Variables_Object_14 == (Object)null && !SearchAndKeyboard.KeyCollider_State_02)
		{
			return;
		}
		bool flag = Settings.CapturedVariables3760_Color_10 == Settings.ColorMode.Pinwheel;
		bool flag2 = Settings.CapturedVariables3760_Color_18 == Settings.ColorMode.Pinwheel;
		bool flag3 = Settings.CapturedVariables3760_Color_05 == Settings.ColorMode.Pinwheel;
		bool flag4 = Settings.CapturedVariables3760_Color_04 == Settings.ColorMode.Pinwheel;
		bool flag5 = Settings.CapturedVariables3760_Color_12 == Settings.ColorMode.Pinwheel;
		bool flag6 = flag | flag2 | flag3 | flag4 | flag5;
		bool flag7 = Settings.CapturedVariables3760_Color_10 == Settings.ColorMode.Gradient;
		bool flag8 = Settings.CapturedVariables3760_Color_18 == Settings.ColorMode.Gradient;
		_ = Settings.CapturedVariables3760_Color_05 == Settings.ColorMode.Gradient;
		_ = Settings.CapturedVariables3760_Color_04 == Settings.ColorMode.Gradient;
		_ = Settings.CapturedVariables3760_Color_12 == Settings.ColorMode.Gradient;
		bool flag9 = Settings.CapturedVariables3760_Color_10 != Settings.ColorMode.Solid;
		bool flag10 = Settings.CapturedVariables3760_Color_18 != Settings.ColorMode.Solid;
		bool flag11 = Settings.CapturedVariables3760_Color_05 != Settings.ColorMode.Solid;
		bool flag12 = Settings.CapturedVariables3760_Color_04 != Settings.ColorMode.Solid;
		bool flag13 = Settings.CapturedVariables3760_Color_12 != Settings.ColorMode.Solid;
		bool flag14 = Settings.CapturedVariables3760_Color_15 != Settings.ColorMode.Solid;
		if (!(flag9 | flag10 | flag11 | flag12 | flag13 | flag14) && !CapturedVariables1950_State_03)
		{
			return;
		}
		if (flag6)
		{
			if ((Object)(object)CapturedVariables1950_Material_07 == (Object)null)
			{
				CapturedVariables1950_Material_07 = CreatePinwheelMaterial();
				CapturedVariables1950_Material_08.Add(CapturedVariables1950_Material_07);
			}
			else if (CapturedVariables1950_State_03)
			{
				AssetHandler.SetMaterialProperty(CapturedVariables1950_Material_07, "_Speed", 0f - CapturedVariables1950_Value_06);
				AssetHandler.SetMaterialProperty(CapturedVariables1950_Material_07, "_COLOR1", CapturedVariables1950_Color_02);
				AssetHandler.SetMaterialProperty(CapturedVariables1950_Material_07, "_COLOR2", CapturedVariables1950_Color_06);
				if (!flag)
				{
					goto Branch_0233;
				}
				goto Branch_0269;
			}
			if (!flag)
			{
				goto Branch_0233;
			}
		}
		else if (!flag)
		{
			goto Branch_0233;
		}
		goto Branch_0269;
		Branch_0280:
		Color val = Settings.GetAnimatedColor(Settings.CapturedVariables3760_Color_18, (Color32)(Settings.CapturedVariables3760_Color_26), (Color32)(Settings.CapturedVariables3760_Color_23), Settings.CapturedVariables3760_Value_15, 1);
		if (!flag3)
		{
			goto Branch_02cd;
		}
		goto Branch_0303;
		Branch_02b6:
		val = Color.white;
		if (!flag3)
		{
			goto Branch_02cd;
		}
		goto Branch_0303;
		Branch_02cd:
		Settings.GetAnimatedColor(Settings.CapturedVariables3760_Color_05, (Color32)(Settings.CapturedVariables3760_Color_08), (Color32)(Settings.CapturedVariables3760_Color_29), Settings.CapturedVariables3760_Value_13, 2);
		if (!flag4)
		{
			goto Branch_031a;
		}
		goto Branch_0350;
		Branch_0303:
		_ = Color.white;
		if (!flag4)
		{
			goto Branch_031a;
		}
		goto Branch_0350;
		Branch_031a:
		Settings.GetAnimatedColor(Settings.CapturedVariables3760_Color_04, (Color32)(Settings.CapturedVariables3760_Color_28), (Color32)(Settings.CapturedVariables3760_Color_09), Settings.CapturedVariables3760_Value_03, 4);
		if (!flag5)
		{
			goto Branch_0367;
		}
		goto Branch_0399;
		Branch_074e:
		int num;
		Color val3;
		if (num < CapturedVariables1950_Color_03.Count)
		{
			do
			{
				Branch_03ac:
				TrackedColorGroup trackedColorGroup = CapturedVariables1950_Color_03[num];
				int num2 = (int)(trackedColorGroup.role - 1);
				num2 = (((uint)num2 <= 4u) ? num2 : 5) + 336;
				int num3 = num2;
				Color val2;
				bool flag15;
				bool flag16;
				bool flag17;
				Color c;
				Color c2;
				if (num3 != 337)
				{
					val2 = val3;
					flag15 = flag;
					flag16 = flag9;
					flag17 = flag7;
					c = (Color32)(Settings.CapturedVariables3760_Color_19);
					c2 = (Color32)(Settings.CapturedVariables3760_Color_02);
				}
				else
				{
					val2 = val;
					flag15 = flag2;
					flag16 = flag10;
					flag17 = flag8;
					c = (Color32)(Settings.CapturedVariables3760_Color_26);
					c2 = (Color32)(Settings.CapturedVariables3760_Color_23);
				}
				if (flag17)
				{
					if (CapturedVariables1950_State_03)
					{
						int num4 = 0;
						if (num4 < trackedColorGroup.renderers.Count)
						{
							do
							{
								ApplyGradientTexture(trackedColorGroup.renderers[num4], c, c2);
								num4++;
							}
							while (num4 < trackedColorGroup.renderers.Count);
						}
					}
				}
				else if ((flag16 || CapturedVariables1950_State_03) && (!flag15 || CapturedVariables1950_State_03))
				{
					int num5 = 0;
					if (num5 < trackedColorGroup.renderers.Count)
					{
						while (true)
						{
							Renderer val4 = trackedColorGroup.renderers[num5];
							if (!((Object)(object)val4 == (Object)null))
							{
								if (flag15)
								{
									if ((Object)(object)CapturedVariables1950_Material_07 != (Object)null)
									{
										val4.sharedMaterial = CapturedVariables1950_Material_07;
									}
									num5++;
									if (num5 >= trackedColorGroup.renderers.Count)
									{
										break;
									}
									continue;
								}
								Color color = val2;
								color.a = Settings.CapturedVariables3760_Value_07;
								if ((Object)(object)val4.sharedMaterial == (Object)(object)CapturedVariables1950_Material_07 || (Object)(object)val4.sharedMaterial == (Object)null)
								{
									Material val5 = CreateMaterial(color);
									CapturedVariables1950_Material_05.Add(val5);
									val4.sharedMaterial = val5;
									num5++;
									if (num5 >= trackedColorGroup.renderers.Count)
									{
										break;
									}
								}
								else
								{
									val4.sharedMaterial.color = color;
									num5++;
									if (num5 >= trackedColorGroup.renderers.Count)
									{
										break;
									}
								}
							}
							else
							{
								num5++;
								if (num5 >= trackedColorGroup.renderers.Count)
								{
									break;
								}
							}
						}
					}
					num++;
					if (num >= CapturedVariables1950_Color_03.Count)
					{
						break;
					}
					goto Branch_03ac;
				}
				num++;
			}
			while (num < CapturedVariables1950_Color_03.Count);
		}
		if ((Object)(object)Variables.Variables_Reference_08 != (Object)null)
		{
			if (flag14 || CapturedVariables1950_State_03)
			{
				goto Branch_07d3;
			}
		}
		CapturedVariables1950_State_03 = false;
		return;
		Branch_0367:
		Settings.GetAnimatedColor(Settings.CapturedVariables3760_Color_12, (Color32)(Settings.CapturedVariables3760_Color_14), (Color32)(Settings.CapturedVariables3760_Color_21), Settings.CapturedVariables3760_Value_04, 5);
		num = 0;
		goto Branch_074e;
		Branch_07d3:
		if (Settings.CapturedVariables3760_Color_15 == Settings.ColorMode.Gradient)
		{
			Color white = Color.white;
			((Graphic)Variables.Variables_Reference_08).color = white;
			CapturedVariables1950_State_03 = false;
		}
		else
		{
			Color white = Settings.GetAnimatedColor(Settings.CapturedVariables3760_Color_15, (Color32)(Settings.CapturedVariables3760_Color_17), (Color32)(Settings.CapturedVariables3760_Color_16), Settings.CapturedVariables3760_Value_10, 3);
			((Graphic)Variables.Variables_Reference_08).color = white;
			CapturedVariables1950_State_03 = false;
		}
		return;
		Branch_0399:
		_ = Color.white;
		num = 0;
		goto Branch_074e;
		Branch_0350:
		_ = Color.white;
		if (!flag5)
		{
			goto Branch_0367;
		}
		goto Branch_0399;
		Branch_0269:
		val3 = Color.white;
		if (!flag2)
		{
			goto Branch_0280;
		}
		goto Branch_02b6;
		Branch_0233:
		val3 = Settings.GetAnimatedColor(Settings.CapturedVariables3760_Color_10, (Color32)(Settings.CapturedVariables3760_Color_19), (Color32)(Settings.CapturedVariables3760_Color_02), Settings.CapturedVariables3760_Value_12);
		if (!flag2)
		{
			goto Branch_0280;
		}
		goto Branch_02b6;
	}

	public static void CreateFpsPanel()
	{
		float num = CapturedVariables1950_Position_01.x * 2.504f;
		float num2 = 0.1125f - num;
		float num3 = num * 3.3f;
		float num4 = -0.2375f + num * 0.5f + num2 + num3 * 0.5f;
		GameObject val = CreateCube("FpsBg");
		val.transform.localScale = new Vector3(0.0075f, num3, CapturedVariables1950_Position_01.x * 1.6f);
		val.transform.localPosition = new Vector3(0.07525f, num4, 0.45f);
		ApplyColorMaterial(val, (Color32)(Settings.CapturedVariables3760_Color_26));
		AddOutline(val, 2, 0.0065f, animated: false);
		List<GameObject> extraParts = null;
		RectTransform val2;
		if (Settings.CapturedVariables3760_Value_01 > 0f)
		{
			extraParts = RoundObject(val, null, null, Settings.CapturedVariables3760_Value_01);
			RegisterColorGroup(ColorRole.Button, val, extraParts);
			CapturedVariables1950_Object_02 = val;
			CapturedVariables1950_Reference_10 = CreateText();
			((Object)CapturedVariables1950_Reference_10).name = "FPS Text";
			CapturedVariables1950_Reference_10.alignment = (TextAnchor)4;
			val2 = ((Component)CapturedVariables1950_Reference_10).GetComponent<RectTransform>();
			((Transform)val2).localPosition = Vector3.zero;
			((Transform)val2).position = new Vector3(0.079535715f * CapturedVariables1950_Position_12.x, num4 * CapturedVariables1950_Position_12.y, 0.45f * CapturedVariables1950_Position_12.z);
			((Transform)val2).localRotation = Quaternion.Euler(180f, 90f, 90f);
			val2.sizeDelta = new Vector2(0.12f, 0.013f);
			UpdateFpsDisplay(force: true);
		}
		else
		{
			RegisterColorGroup(ColorRole.Button, val, extraParts);
			CapturedVariables1950_Object_02 = val;
			CapturedVariables1950_Reference_10 = CreateText();
			((Object)CapturedVariables1950_Reference_10).name = "FPS Text";
			CapturedVariables1950_Reference_10.alignment = (TextAnchor)4;
			val2 = ((Component)CapturedVariables1950_Reference_10).GetComponent<RectTransform>();
			((Transform)val2).localPosition = Vector3.zero;
			((Transform)val2).position = new Vector3(0.079535715f * CapturedVariables1950_Position_12.x, num4 * CapturedVariables1950_Position_12.y, 0.45f * CapturedVariables1950_Position_12.z);
			((Transform)val2).localRotation = Quaternion.Euler(180f, 90f, 90f);
			val2.sizeDelta = new Vector2(0.12f, 0.013f);
			UpdateFpsDisplay(force: true);
		}
	}

	static Main()
	{
		CapturedVariables1950_Position_12 = new Vector3(0.75f, 0.2875f, 0.45f);
		CapturedVariables1950_Position_09 = new Vector3(0.01f, 0.875f, 0.9f);
		CapturedVariables1950_Position_16 = new Vector3(0.05f, 0f, 0.025f);
		CapturedVariables1950_Position_04 = new Vector3(0.01f, 0.8625f, 0.0625f);
		CapturedVariables1950_Position_15 = new Vector3(0.0667f, 0f, 0.56f);
		CapturedVariables1950_Position_06 = new Vector3(0.054f, 0f, 0.2525f);
		CapturedVariables1950_Position_01 = new Vector3(0.039375f, 0.0590625f, 0f);
		CapturedVariables1950_Position_02 = new Vector2(0.128f, 0.0296f);
		CapturedVariables1950_Position_08 = new Vector3(0.001f, 0.875f, 0.0045f);
		CapturedVariables1950_Position_05 = new Vector3(0.0715f, 0f, 0.4f);
		CapturedVariables1950_Index_01 = -1;
		CapturedVariables1950_Button_02 = new List<ButtonHandler.Button>(7);
		CapturedVariables1950_Position_13 = new Vector2(0.22f, 1f / 64f);
		CapturedVariables1950_Position_18 = new Vector2(0.15f, 0.0155f);
		CapturedVariables1950_Position_14 = new Vector2(0.1f, 0.015f);
		CapturedVariables1950_Position_03 = new Vector2(0.15f, 0.0225f);
		CapturedVariables1950_Position_07 = new Vector2(0.1f, 0.02f);
		CapturedVariables1950_Position_10 = new Vector3(0.015f, 0.015f, 0f);
		CapturedVariables1950_Position_17 = new Vector3(0f, -0.1f, 0f);
		CapturedVariables1950_Material_05 = new List<Material>();
		CapturedVariables1950_Object_01 = new List<GameObject>();
		CapturedVariables1950_Object_05 = new List<GameObject>();
		CapturedVariables1950_Material_04 = new List<Material>();
		CapturedVariables1950_Reference_05 = new List<Mesh>();
		CapturedVariables1950_Object_03 = new List<GameObject>();
		CapturedVariables1950_Material_03 = new List<Material>();
		CapturedVariables1950_Reference_01 = new List<Mesh>();
		CapturedVariables1950_Reference_08 = new List<FadeItem>(96);
		CapturedVariables1950_Material_06 = new HashSet<Material>();
		CapturedVariables1950_Color_03 = new List<TrackedColorGroup>(32);
		CapturedVariables1950_Button_03 = new List<ButtonHandler.Button>(7);
		Recovered_Reference_10 = new List<(ButtonHandler.Button, int)>(32);
		CapturedVariables1950_Value_04 = 0.6f;
		CapturedVariables1950_State_09 = true;
		CapturedVariables1950_Color_02 = (Color32)(new Color32(byte.MaxValue, (byte)0, (byte)127, byte.MaxValue));
		CapturedVariables1950_Color_06 = (Color32)(new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue));
		CapturedVariables1950_Value_06 = 0.5f;
		CapturedVariables1950_Color_01 = (Settings.ColorMode)(-1);
		CapturedVariables1950_Text_02 = new StringBuilder(256);
		CapturedVariables1950_Reference_02 = new List<FontEntry>(8);
		CapturedVariables1950_Reference_04 = new List<Mesh>();
		CapturedVariables1950_Material_08 = new List<Material>();
		CapturedVariables1950_Reference_06 = new List<Font>(4);
		CapturedVariables1950_Button_01 = new List<ButtonHandler.Button>(32);
		CapturedVariables1950_State_11 = true;
		CapturedVariables1950_Value_07 = float.MinValue;
		CapturedVariables1950_Position_11 = Vector3.zero;
		CapturedVariables1950_State_03 = true;
	}

	private static void ConfigureClassicButton(ButtonHandler.Button button, float offset, ButtonHandler.BtnCollider col, GameObject pill, List<GameObject> pillParts)
	{
		float num = 0.0464f;
		float num2 = num * (CapturedVariables1950_Position_12.z / CapturedVariables1950_Position_12.y);
		float num3 = (0.15f - num2) * 0.5f - 0.0069f;
		float num4 = -0.3375f + num3;
		float num5 = -0.3375f - num3;
		GameObject val = CreateCube("Knob: " + button.buttonText);
		val.transform.localScale = new Vector3(0.0075f, num2, num);
		val.transform.localPosition = new Vector3(0.07825f, button.Enabled ? num5 : num4, 0.335f - offset);
		List<GameObject> list;
		if (!button.Enabled)
		{
			Color val2 = (Color32)(Settings.CapturedVariables3760_Color_26);
			ApplyColorMaterial(val, Color.Lerp(val2, Color.white, 0.6f));
			list = RoundObject(val, null, null, 2f, 2466, 14);
			if (list != null)
			{
				goto Branch_0157;
			}
		}
		else
		{
			Color val2 = (Color32)(Settings.CapturedVariables3760_Color_08);
			ApplyColorMaterial(val, Color.Lerp(val2, Color.white, 0.6f));
			list = RoundObject(val, null, null, 2f, 2466, 14);
			if (list != null)
			{
				goto Branch_0157;
			}
		}
		goto Branch_0170;
		Branch_0157:
		if (list.Count <= 0)
		{
			goto Branch_0170;
		}
		Transform transform = list[0].transform;
		if (!((Object)(object)col != (Object)null))
		{
			return;
		}
		Branch_01be:
		List<Renderer> list2 = new List<Renderer>(2);
		if (pillParts != null)
		{
			using List<GameObject>.Enumerator enumerator = pillParts.GetEnumerator();
			if (enumerator.MoveNext())
			{
				do
				{
					Branch_01fc:
					GameObject current = enumerator.Current;
					Renderer val3;
					if (!((Object)(object)current != (Object)null))
					{
						val3 = null;
						if ((Object)(object)val3 != (Object)null)
						{
							goto Branch_0260;
						}
					}
					else
					{
						val3 = current.GetComponent<Renderer>();
						if ((Object)(object)val3 != (Object)null)
						{
							goto Branch_0260;
						}
					}
					if (!enumerator.MoveNext())
					{
						break;
					}
					goto Branch_01fc;
					Branch_0260:
					list2.Add(val3);
				}
				while (enumerator.MoveNext());
			}
		}
		if (list2.Count == 0 && (Object)(object)pill != (Object)null)
		{
			Renderer component = pill.GetComponent<Renderer>();
			if ((Object)(object)component != (Object)null)
			{
				list2.Add(component);
				col.SetupPulse(transform, ((Component)transform).GetComponent<Renderer>(), num4, num5, list2, (Color32)(Settings.CapturedVariables3760_Color_26), (Color32)(Settings.CapturedVariables3760_Color_08));
			}
			else
			{
				col.SetupPulse(transform, ((Component)transform).GetComponent<Renderer>(), num4, num5, list2, (Color32)(Settings.CapturedVariables3760_Color_26), (Color32)(Settings.CapturedVariables3760_Color_08));
			}
		}
		else
		{
			col.SetupPulse(transform, ((Component)transform).GetComponent<Renderer>(), num4, num5, list2, (Color32)(Settings.CapturedVariables3760_Color_26), (Color32)(Settings.CapturedVariables3760_Color_08));
		}
		return;
		Branch_0170:
		transform = val.transform;
		if (!((Object)(object)col != (Object)null))
		{
			return;
		}
		goto Branch_01be;
	}

	public static IEnumerator ScaleObject(GameObject obj, Vector3 target, bool cleanupAfter = false)
	{
		if ((Object)(object)obj == (Object)null)
		{
			yield break;
		}

		Vector3 start = obj.transform.localScale;
		if (!CapturedVariables1950_State_09)
		{
			obj.transform.localScale = target;
		}
		else if (cleanupAfter)
		{
			yield return ScaleObjectCoroutine(obj, start, target, 0.2f);
		}
		else
		{
			const float duration = 0.8f;
			for (float elapsed = 0f; elapsed < duration; elapsed += Time.deltaTime)
			{
				if ((Object)(object)obj == (Object)null)
				{
					yield break;
				}
				obj.transform.localScale = Vector3.LerpUnclamped(start, target, EaseOutElastic(elapsed / duration));
				yield return null;
			}
			if ((Object)(object)obj != (Object)null)
			{
				obj.transform.localScale = target;
			}
		}

		if (cleanupAfter)
		{
			DestroyMenuContents();
			CapturedVariables1950_State_12 = false;
			CapturedVariables1950_State_08 = false;
			Variables.Variables_State_02 = false;
			Variables.Variables_State_15 = false;
		}
	}

	private static float EaseOutElastic(float t)
	{
		if (t <= 0f)
		{
			return 0f;
		}
		if (t >= 1f)
		{
			return 1f;
		}
		return 1.05f * Mathf.Pow(2f, -10f * t) * Mathf.Sin((t - 0.075f) * (MathF.PI * 2f) / 0.3f) + 1f;
	}

	private static void UpdateRoomState()
	{
		if (PhotonNetwork.InRoom)
		{
			if (Variables.currentPage == Category.Player_Action && PlayersActionList.CapturedVariables70_Reference_03 != null)
			{
				PlayersActionList.UpdatePlayerCamera();
			}
		}
		else
		{
			Variables.Variables_State_10 = false;
			PlayersActionList.ClearPlayerCamera();
		}
	}

	public static void LoadFonts()
	{
		ClearFonts();
		RegisterFont("Arial", Resources.GetBuiltinResource<Font>("Arial.ttf"));
		Font val = Font.CreateDynamicFontFromOSFont("Comic Sans MS", 22);
		if ((Object)(object)val != (Object)null)
		{
			CapturedVariables1950_Reference_06.Add(val);
			RegisterFont("Comic Sans", val);
			LoadBundledFont("Minecraft", "NXO.Resources.minecraftfont", "Minecraftia-Regular");
		}
		else
		{
			LoadBundledFont("Minecraft", "NXO.Resources.minecraftfont", "Minecraftia-Regular");
		}
	}

	public static void ShowTooltip()
	{
		CapturedVariables1950_State_11 = true;
	}

	private static void ApplyGradientTexture(Renderer r, Color c1, Color c2)
	{
		if ((Object)(object)r == (Object)null || (Object)(object)VertexColorShader == (Object)null)
		{
			return;
		}
		MeshFilter component = ((Component)r).GetComponent<MeshFilter>();
		if ((Object)(object)component == (Object)null || (Object)(object)component.sharedMesh == (Object)null)
		{
			return;
		}
		Vector3[] vertices = component.mesh.vertices;
		if (vertices.Length == 0)
		{
			return;
		}
		Mesh val = component.mesh;
		Bounds bounds = val.bounds;
		float fFU9RG = Settings.CapturedVariables3760_Value_07;
		Settings.GradientDirection yTJG533I = Settings.CapturedVariables3760_Reference_02;
		Color[] array = (Color[])(object)new Color[vertices.Length];
		int num = 0;
		while (num < vertices.Length)
		{
			float num2;
			if (!(((Bounds)bounds).size.y > 1E-05f))
			{
				num2 = 0f;
				if (!(((Bounds)bounds).size.z > 1E-05f))
				{
					goto Branch_01a2;
				}
			}
			else
			{
				num2 = Mathf.InverseLerp(((Bounds)bounds).min.y, ((Bounds)bounds).max.y, vertices[num].y);
				if (!(((Bounds)bounds).size.z > 1E-05f))
				{
					goto Branch_01a2;
				}
			}
			float num3 = Mathf.InverseLerp(((Bounds)bounds).min.z, ((Bounds)bounds).max.z, vertices[num].z);
			if (yTJG533I != Settings.GradientDirection.Vertical)
			{
				goto Branch_01fd;
			}
			goto Branch_0280;
			Branch_01a2:
			num3 = 0f;
			if (yTJG533I != Settings.GradientDirection.Vertical)
			{
				goto Branch_01fd;
			}
			goto Branch_0280;
			Branch_01fd:
			float num4;
			Color val2;
			if (yTJG533I != Settings.GradientDirection.Diagonal)
			{
				num4 = num2;
				val2 = Color.Lerp(c1, c2, num4);
				val2.a = fFU9RG;
				array[num] = val2;
				num++;
			}
			else
			{
				num4 = (num2 + num3) * 0.5f;
				val2 = Color.Lerp(c1, c2, num4);
				val2.a = fFU9RG;
				array[num] = val2;
				num++;
			}
			continue;
			Branch_0280:
			num4 = num3;
			val2 = Color.Lerp(c1, c2, num4);
			val2.a = fFU9RG;
			array[num] = val2;
			num++;
		}
		val.colors = array;
		if (!CapturedVariables1950_Reference_04.Contains(val))
		{
			CapturedVariables1950_Reference_04.Add(val);
			if (!((Object)(object)r.sharedMaterial == (Object)null))
			{
				goto Branch_0334;
			}
		}
		else if (!((Object)(object)r.sharedMaterial == (Object)null))
		{
			goto Branch_0334;
		}
		Branch_0371:
		Material val3 = new Material(VertexColorShader)
		{
			color = Color.white,
			renderQueue = 2460
		};
		CapturedVariables1950_Material_05.Add(val3);
		r.sharedMaterial = val3;
		return;
		Branch_0334:
		if (!((Object)(object)r.sharedMaterial.shader != (Object)(object)VertexColorShader))
		{
			return;
		}
		goto Branch_0371;
	}

	private static void RegisterButtonTransforms(ButtonHandler.BtnCollider col, GameObject body, List<GameObject> rounded, List<GameObject> outline, params Transform[] uniform)
	{
		if ((Object)(object)col == (Object)null)
		{
			return;
		}
		if ((Object)(object)body != (Object)null)
		{
			col.RegisterBody(body.transform);
			if (rounded != null)
			{
				goto Branch_007c;
			}
		}
		else if (rounded != null)
		{
			goto Branch_007c;
		}
		goto Branch_010b;
		Branch_007c:
		int num = 0;
		if (num < rounded.Count)
		{
			while (true)
			{
				if ((Object)(object)rounded[num] != (Object)null)
				{
					col.RegisterBody(rounded[num].transform);
					num++;
					if (num >= rounded.Count)
					{
						break;
					}
				}
				else
				{
					num++;
					if (num >= rounded.Count)
					{
						break;
					}
				}
			}
		}
		Branch_010b:
		if (outline != null)
		{
			int num2 = 0;
			if (num2 < outline.Count)
			{
				while (true)
				{
					if ((Object)(object)outline[num2] != (Object)null)
					{
						col.RegisterBody(outline[num2].transform);
						num2++;
						if (num2 >= outline.Count)
						{
							break;
						}
					}
					else
					{
						num2++;
						if (num2 >= outline.Count)
						{
							break;
						}
					}
				}
			}
		}
		if (uniform == null)
		{
			return;
		}
		int num3 = 0;
		if (num3 >= uniform.Length)
		{
			return;
		}
		while (true)
		{
			if ((Object)(object)uniform[num3] != (Object)null)
			{
				col.RegisterUniform(uniform[num3]);
				num3++;
				if (num3 >= uniform.Length)
				{
					break;
				}
			}
			else
			{
				num3++;
				if (num3 >= uniform.Length)
				{
					break;
				}
			}
		}
	}

	public static void UpdateTitleText()
	{
		if ((Object)(object)Variables.Variables_Reference_08 == (Object)null)
		{
			return;
		}
		Settings.ColorMode uV821U0H = Settings.CapturedVariables3760_Color_15;
		if (uV821U0H == Settings.ColorMode.Gradient && Settings.CapturedVariables3760_State_03)
		{
			if (!(Time.time - CapturedVariables1950_Value_03 < 0.03f))
			{
				CapturedVariables1950_Value_03 = Time.time;
				Variables.Variables_Reference_08.text = BuildAnimatedTitleText();
				CapturedVariables1950_Color_01 = (Settings.ColorMode)(-1);
			}
		}
		else if (uV821U0H == Settings.ColorMode.Gradient)
		{
			Color32 x2GA31ZR = Settings.CapturedVariables3760_Color_17;
			Color32 v59ADUQX = Settings.CapturedVariables3760_Color_16;
			if (CapturedVariables1950_Color_01 != uV821U0H || !ColorsEqual(x2GA31ZR, CapturedVariables1950_Color_07) || !ColorsEqual(v59ADUQX, CapturedVariables1950_Color_05))
			{
				Variables.Variables_Reference_08.text = BuildGradientTitleText();
				CapturedVariables1950_Color_01 = uV821U0H;
				CapturedVariables1950_Color_07 = x2GA31ZR;
				CapturedVariables1950_Color_05 = v59ADUQX;
			}
		}
		else if (CapturedVariables1950_Color_01 != uV821U0H)
		{
			Variables.Variables_Reference_08.text = "<b>NXO</b>";
			CapturedVariables1950_Color_01 = uV821U0H;
		}
	}

	private static void CreateClassicIncrementalArrow(string name, ButtonHandler.Button parent, float offset, float yPos, string symbol, string suffix)
	{
		GameObject val = CreateCube(name, null, keepCollider: true);
		val.transform.localScale = new Vector3(0.0075f, 0.12f, 0.08f);
		val.transform.localPosition = new Vector3(0.07525f, yPos, 0.335f - offset);
		ButtonHandler.BtnCollider btnCollider = val.AddComponent<ButtonHandler.BtnCollider>();
		btnCollider.clickedButton = new ButtonHandler.Button(parent.buttonText + suffix, parent.Page, isToggle: false, isActive: false, null);
		ApplyColorMaterial(val, (Color32)(Settings.CapturedVariables3760_Color_26));
		List<GameObject> outline = AddOutline(val, 2, 0.0065f);
		List<GameObject> list = null;
		Text val2;
		RectTransform component;
		float num;
		if (Settings.CapturedVariables3760_Value_01 > 0f)
		{
			list = RoundObject(val, null, null, Settings.CapturedVariables3760_Value_01);
			RegisterColorGroup(ColorRole.Button, val, list);
			val2 = CreateText();
			val2.text = symbol;
			((Component)val2).gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1f);
			component = ((Component)val2).GetComponent<RectTransform>();
			num = IncrementTextOffset;
			if (!(yPos < 0f))
			{
				goto Branch_0193;
			}
		}
		else
		{
			RegisterColorGroup(ColorRole.Button, val, list);
			val2 = CreateText();
			val2.text = symbol;
			((Component)val2).gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1f);
			component = ((Component)val2).GetComponent<RectTransform>();
			num = IncrementTextOffset;
			if (!(yPos < 0f))
			{
				goto Branch_0193;
			}
		}
		float num2 = -0.1025f;
		((Transform)component).localPosition = new Vector3(0.05975f, num2, num - offset / 2.225f);
		component.sizeDelta = CapturedVariables1950_Position_14;
		((Transform)component).localRotation = Quaternion.Euler(180f, 90f, 90f);
		RegisterButtonTransforms(btnCollider, val, list, outline, ((Component)val2).transform);
		if (parent == ButtonHandler.CapturedVariables570_Button_03)
		{
			goto Branch_0289;
		}
		return;
		Branch_0289:
		if (suffix == "_UP" == ButtonHandler.CapturedVariables570_State_02)
		{
			ButtonHandler.CapturedVariables570_Button_03 = null;
			btnCollider.PlayBounce();
		}
		return;
		Branch_0193:
		num2 = 0.1015f;
		((Transform)component).localPosition = new Vector3(0.05975f, num2, num - offset / 2.225f);
		component.sizeDelta = CapturedVariables1950_Position_14;
		((Transform)component).localRotation = Quaternion.Euler(180f, 90f, 90f);
		RegisterButtonTransforms(btnCollider, val, list, outline, ((Component)val2).transform);
		if (parent == ButtonHandler.CapturedVariables570_Button_03)
		{
			goto Branch_0289;
		}
	}

	private static void CreateAccentStrip()
	{
		if (Settings.CapturedVariables3760_Reference_03 != Settings.AccentStripType.Off)
		{
			GameObject val = CreateCube();
			val.transform.localScale = CapturedVariables1950_Position_08;
			val.transform.localPosition = CapturedVariables1950_Position_05;
			ApplyTransparentColorMaterial(val, (Color32)(Settings.CapturedVariables3760_Color_14));
			RegisterColorGroup(ColorRole.AccentStrip, val, null);
			if (Settings.CapturedVariables3760_Reference_03 == Settings.AccentStripType.Both)
			{
				GameObject val2 = CreateCube();
				val2.transform.localScale = CapturedVariables1950_Position_08;
				val2.transform.localPosition = new Vector3(CapturedVariables1950_Position_05.x, CapturedVariables1950_Position_05.y, -0.27f);
				ApplyTransparentColorMaterial(val2, (Color32)(Settings.CapturedVariables3760_Color_21));
				RegisterColorGroup(ColorRole.AccentStrip, val2, null);
			}
		}
	}

	private static void UpdateVrMenu()
	{
		if (!Variables.Variables_State_05)
		{
			Variables.Variables_State_04 = InputHandler.IsLeftSecondaryPressed();
			if (!Variables.Variables_State_04)
			{
				goto Branch_0050;
			}
		}
		else
		{
			Variables.Variables_State_04 = InputHandler.IsRightSecondaryPressed();
			if (!Variables.Variables_State_04)
			{
				goto Branch_0050;
			}
		}
		goto Branch_0069;
		Branch_0385:
		if (!((Object)(object)SearchAndKeyboard.KeyCollider_Object_01 != (Object)null))
		{
			goto Branch_03bd;
		}
		UpdateKeyboardMenuTransform();
		UpdateTitleText();
		UpdateDisconnectIcon();
		UpdateFpsDisplay();
		return;
		Branch_03bd:
		UpdateWristMenuTransform();
		UpdateTitleText();
		UpdateDisconnectIcon();
		UpdateFpsDisplay();
		return;
		Branch_0050:
		if (!SearchAndKeyboard.KeyCollider_State_02)
		{
			goto Branch_01d2;
		}
		Branch_0069:
		if (Variables.Variables_State_15 || CapturedVariables1950_State_12)
		{
			goto Branch_01d2;
		}
		Variables.Variables_State_02 = true;
		CapturedVariables1950_State_12 = true;
		if (!((Object)(object)Variables.Variables_Object_14))
		{
			BuildMenu(openCall: true);
			if (Variables.Variables_State_14)
			{
				AssetHandler.PlayAudioClip(RigManager.GetHandObject, Variables.Variables_Audio_03, 0.625f);
				if (((Object)(object)Variables.Variables_Object_10))
				{
					return;
				}
			}
			else if (((Object)(object)Variables.Variables_Object_10))
			{
				return;
			}
		}
		else if (((Object)(object)Variables.Variables_Object_10))
		{
			return;
		}
		if (!Variables.Variables_State_05)
		{
			UpdateClickerTransform(Variables.Variables_Reference_06.RightHand.controllerTransform);
		}
		else
		{
			UpdateClickerTransform(Variables.Variables_Reference_06.LeftHand.controllerTransform);
		}
		return;
		Branch_01d2:
		bool flag = Variables.Variables_State_04 || SearchAndKeyboard.KeyCollider_State_02;
		if (!flag && CapturedVariables1950_State_12)
		{
			Variables.Variables_State_02 = false;
			CapturedVariables1950_State_12 = false;
			if ((Object)(object)Variables.Variables_Object_14 != (Object)null)
			{
				((MonoBehaviour)Variables.Variables_Reference_06).StartCoroutine(ScaleObject(Variables.Variables_Object_14, Vector3.zero, cleanupAfter: true));
				DestroyClickerObjects();
				if (Variables.Variables_State_14)
				{
					AssetHandler.PlayAudioClip(RigManager.GetHandObject, Variables.Variables_Audio_02, 0.625f);
				}
			}
			return;
		}
		if (!Variables.Variables_State_02 || !((Object)(object)Variables.Variables_Object_14 != (Object)null))
		{
			return;
		}
		if (!((Object)(object)Variables.Variables_Object_10))
		{
			if (!Variables.Variables_State_05)
			{
				UpdateClickerTransform(Variables.Variables_Reference_06.RightHand.controllerTransform);
				if (SearchAndKeyboard.KeyCollider_State_02)
				{
					goto Branch_0385;
				}
			}
			else
			{
				UpdateClickerTransform(Variables.Variables_Reference_06.LeftHand.controllerTransform);
				if (SearchAndKeyboard.KeyCollider_State_02)
				{
					goto Branch_0385;
				}
			}
		}
		else if (SearchAndKeyboard.KeyCollider_State_02)
		{
			goto Branch_0385;
		}
		goto Branch_03bd;
	}

	public static void UpdateClickerTransform(Transform parentTransform)
	{
		if ((Object)(object)Variables.Variables_Object_10 != (Object)null)
		{
			return;
		}
		Variables.Variables_Object_10 = new GameObject("buttonclicker");
		((Collider)Variables.Variables_Object_10.AddComponent<BoxCollider>()).isTrigger = true;
		Variables.Variables_Object_10.layer = LayerMask.NameToLayer("UI");
		Variables.Variables_Object_10.AddComponent<MeshFilter>().mesh = Resources.GetBuiltinResource<Mesh>("Sphere.fbx");
		if ((Object)(object)CapturedVariables1950_Material_01 == (Object)null)
		{
			CapturedVariables1950_Material_01 = new Material(Variables.Variables_Reference_10)
			{
				color = Color.white
			};
			((Renderer)Variables.Variables_Object_10.AddComponent<MeshRenderer>()).sharedMaterial = CapturedVariables1950_Material_01;
			if ((Object)(object)parentTransform == (Object)null)
			{
				return;
			}
		}
		else
		{
			((Renderer)Variables.Variables_Object_10.AddComponent<MeshRenderer>()).sharedMaterial = CapturedVariables1950_Material_01;
			if ((Object)(object)parentTransform == (Object)null)
			{
				return;
			}
		}
		Variables.Variables_Object_10.transform.SetParent(parentTransform);
		Variables.Variables_Object_10.transform.localScale = Vector3.one * 0.0035f;
		Variables.Variables_Object_10.transform.localPosition = CapturedVariables1950_Position_17;
	}

	public static void DestroyMenuContents()
	{
		PlayersActionList.ClearPlayerCamera();
		CapturedVariables1950_Color_03.Clear();
		CapturedVariables1950_State_03 = true;
		using (List<GameObject>.Enumerator enumerator = CapturedVariables1950_Object_05.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				while (true)
				{
					GameObject current = enumerator.Current;
					if (!((Object)(object)current != (Object)null))
					{
						break;
					}
					Object.Destroy((Object)(object)current);
					if (!enumerator.MoveNext())
					{
						goto EndBranch_0074;
					}
				}
				continue;
				EndBranch_0074:
				break;
			}
		}
		CapturedVariables1950_Object_05.Clear();
		using (List<GameObject>.Enumerator enumerator2 = CapturedVariables1950_Object_01.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				while (true)
				{
					GameObject current2 = enumerator2.Current;
					if (!((Object)(object)current2 != (Object)null))
					{
						break;
					}
					Object.Destroy((Object)(object)current2);
					if (!enumerator2.MoveNext())
					{
						goto EndBranch_010f;
					}
				}
				continue;
				EndBranch_010f:
				break;
			}
		}
		CapturedVariables1950_Object_01.Clear();
		Main.DestroyAndClear<GameObject>(ref Variables.Variables_Object_14, 0f);
		DestroyClickerObjects();
		using (List<Material>.Enumerator enumerator3 = CapturedVariables1950_Material_04.GetEnumerator())
		{
			while (enumerator3.MoveNext())
			{
				while (true)
				{
					Material current3 = enumerator3.Current;
					if (!((Object)(object)current3 != (Object)null))
					{
						break;
					}
					Object.Destroy((Object)(object)current3);
					if (!enumerator3.MoveNext())
					{
						goto EndBranch_01c2;
					}
				}
				continue;
				EndBranch_01c2:
				break;
			}
		}
		CapturedVariables1950_Material_04.Clear();
		using (List<Material>.Enumerator enumerator4 = CapturedVariables1950_Material_05.GetEnumerator())
		{
			while (enumerator4.MoveNext())
			{
				while (true)
				{
					Material current4 = enumerator4.Current;
					if (!((Object)(object)current4 != (Object)null))
					{
						break;
					}
					Object.Destroy((Object)(object)current4);
					if (!enumerator4.MoveNext())
					{
						goto EndBranch_0261;
					}
				}
				continue;
				EndBranch_0261:
				break;
			}
		}
		CapturedVariables1950_Material_05.Clear();
		using (List<Material>.Enumerator enumerator5 = CapturedVariables1950_Material_08.GetEnumerator())
		{
			while (enumerator5.MoveNext())
			{
				while (true)
				{
					Material current5 = enumerator5.Current;
					if (!((Object)(object)current5 != (Object)null))
					{
						break;
					}
					Object.Destroy((Object)(object)current5);
					if (!enumerator5.MoveNext())
					{
						goto EndBranch_0300;
					}
				}
				continue;
				EndBranch_0300:
				break;
			}
		}
		CapturedVariables1950_Material_08.Clear();
		CapturedVariables1950_Material_07 = null;
		using (List<Mesh>.Enumerator enumerator6 = CapturedVariables1950_Reference_05.GetEnumerator())
		{
			while (enumerator6.MoveNext())
			{
				while (true)
				{
					Mesh current6 = enumerator6.Current;
					if (!((Object)(object)current6 != (Object)null))
					{
						break;
					}
					Object.Destroy((Object)(object)current6);
					if (!enumerator6.MoveNext())
					{
						goto EndBranch_03a5;
					}
				}
				continue;
				EndBranch_03a5:
				break;
			}
		}
		CapturedVariables1950_Reference_05.Clear();
		using (List<Mesh>.Enumerator enumerator7 = CapturedVariables1950_Reference_04.GetEnumerator())
		{
			while (enumerator7.MoveNext())
			{
				while (true)
				{
					Mesh current7 = enumerator7.Current;
					if (!((Object)(object)current7 != (Object)null))
					{
						break;
					}
					Object.Destroy((Object)(object)current7);
					if (!enumerator7.MoveNext())
					{
						goto EndBranch_0444;
					}
				}
				continue;
				EndBranch_0444:
				break;
			}
		}
		CapturedVariables1950_Reference_04.Clear();
		using (List<GameObject>.Enumerator enumerator8 = CapturedVariables1950_Object_03.GetEnumerator())
		{
			while (enumerator8.MoveNext())
			{
				while (true)
				{
					GameObject current8 = enumerator8.Current;
					if (!((Object)(object)current8 != (Object)null))
					{
						break;
					}
					Object.Destroy((Object)(object)current8);
					if (!enumerator8.MoveNext())
					{
						goto EndBranch_04e3;
					}
				}
				continue;
				EndBranch_04e3:
				break;
			}
		}
		CapturedVariables1950_Object_03.Clear();
		using (List<Material>.Enumerator enumerator9 = CapturedVariables1950_Material_03.GetEnumerator())
		{
			while (enumerator9.MoveNext())
			{
				while (true)
				{
					Material current9 = enumerator9.Current;
					if (!((Object)(object)current9 != (Object)null))
					{
						break;
					}
					Object.Destroy((Object)(object)current9);
					if (!enumerator9.MoveNext())
					{
						goto EndBranch_0582;
					}
				}
				continue;
				EndBranch_0582:
				break;
			}
		}
		CapturedVariables1950_Material_03.Clear();
		using (List<Mesh>.Enumerator enumerator10 = CapturedVariables1950_Reference_01.GetEnumerator())
		{
			while (enumerator10.MoveNext())
			{
				while (true)
				{
					Mesh current10 = enumerator10.Current;
					if (!((Object)(object)current10 != (Object)null))
					{
						break;
					}
					Object.Destroy((Object)(object)current10);
					if (!enumerator10.MoveNext())
					{
						goto EndBranch_0621;
					}
				}
				continue;
				EndBranch_0621:
				break;
			}
		}
		CapturedVariables1950_Reference_01.Clear();
		Variables.Variables_Object_09 = null;
		Variables.Variables_Object_02 = null;
		Variables.Variables_Object_12 = null;
		CapturedVariables1950_Object_04 = null;
		CapturedVariables1950_Object_02 = null;
		CapturedVariables1950_Reference_10 = null;
		Variables.Variables_Object_03 = null;
		Variables.Variables_Object_07 = null;
		Variables.Variables_Reference_08 = null;
		SearchAndKeyboard.KeyCollider_Reference_01 = null;
		CapturedVariables1950_Color_04 = null;
		CapturedVariables1950_Button_02.Clear();
		CapturedVariables1950_Index_01 = -1;
		CapturedVariables1950_State_05 = false;
		CapturedVariables1950_Text_01 = null;
		CapturedVariables1950_State_01 = false;
		CapturedVariables1950_State_10 = false;
	}

	private static void CreateMenuRoot()
	{
		Variables.Variables_Object_14 = GameObject.CreatePrimitive((PrimitiveType)3);
		((Object)Variables.Variables_Object_14).name = "menu";
		Object.Destroy((Object)(object)Variables.Variables_Object_14.GetComponent<Rigidbody>());
		Object.Destroy((Object)(object)Variables.Variables_Object_14.GetComponent<BoxCollider>());
		Object.Destroy((Object)(object)Variables.Variables_Object_14.GetComponent<Renderer>());
		Variables.Variables_Object_14.transform.localScale = CapturedVariables1950_Position_12;
	}

	private static void PrepareMenuFadeIn()
	{
		if ((Object)(object)Variables.Variables_Object_14 == (Object)null || (Object)(object)Variables.Variables_Reference_06 == (Object)null)
		{
			return;
		}
		CapturedVariables1950_Reference_08.Clear();
		CapturedVariables1950_Material_06.Clear();
		int num = 0;
		if (num < CapturedVariables1950_Object_05.Count)
		{
			do
			{
				Branch_0077:
				GameObject val = CapturedVariables1950_Object_05[num];
				Renderer component2;
				Material val2;
				if (!((Object)(object)val == (Object)null))
				{
					Text component = val.GetComponent<Text>();
					if (!((Object)(object)component != (Object)null))
					{
						component2 = val.GetComponent<Renderer>();
						if (!((Object)(object)component2 != (Object)null))
						{
							val2 = null;
							if ((Object)(object)val2 != (Object)null)
							{
								goto Branch_017a;
							}
						}
						else
						{
							val2 = component2.sharedMaterial;
							if ((Object)(object)val2 != (Object)null)
							{
								goto Branch_017a;
							}
						}
						goto Branch_0204;
					}
					CapturedVariables1950_Reference_08.Add(new FadeItem
					{
						txt = component,
						origA = ((Graphic)component).color.a
					});
					ApplyFadeFactor(CapturedVariables1950_Reference_08.Count - 1, 0f);
				}
				num++;
				if (num >= CapturedVariables1950_Object_05.Count)
				{
					break;
				}
				goto Branch_0077;
				Branch_0204:
				num++;
				continue;
				Branch_017a:
				if (!CapturedVariables1950_Material_06.Add(val2))
				{
					goto Branch_0204;
				}
				CapturedVariables1950_Reference_08.Add(new FadeItem
				{
					r = component2,
					target = val2.color
				});
				ApplyFadeFactor(CapturedVariables1950_Reference_08.Count - 1, 0f);
				num++;
				if (num >= CapturedVariables1950_Object_05.Count)
				{
					break;
				}
				goto Branch_0077;
			}
			while (num < CapturedVariables1950_Object_05.Count);
		}
		if (CapturedVariables1950_Reference_08.Count > 0)
		{
			CapturedVariables1950_Routine_01 = ((MonoBehaviour)Variables.Variables_Reference_06).StartCoroutine(FadeInMenuItems());
		}
	}

	public static void CreateClassicButton(float offset, ButtonHandler.Button button)
	{
		if (button.incremental)
		{
			CreateClassicIncrementalArrow("DOWN : " + button.buttonText, button, offset, 0.3525f, "<", "_DOWN");
			CreateClassicIncrementalArrow("UP : " + button.buttonText, button, offset, -0.3525f, ">", "_UP");
			if (!button.incremental)
			{
				goto Branch_0090;
			}
		}
		else if (!button.incremental)
		{
			goto Branch_0090;
		}
		float num = 0.555f;
		if (!button.showGear)
		{
			goto Branch_011f;
		}
		goto Branch_0105;
		Branch_02c7:
		if (!button.Enabled)
		{
			goto Branch_02dc;
		}
		goto Branch_02ff;
		Branch_029d:
		GameObject val;
		List<GameObject> list = RoundObject(val, null, null, Settings.CapturedVariables3760_Value_01);
		if (!button.Enabled)
		{
			goto Branch_02dc;
		}
		goto Branch_02ff;
		Branch_0438:
		Text val2 = CreateText();
		((Object)val2).name = "Button: Text - " + button.buttonText;
		val2.text = (ButtonHandler.CapturedVariables570_Items_02.Contains(button) ? ("<color=blue>★</color> " + button.buttonText) : button.buttonText);
		float num2 = 1.2f;
		((Component)val2).gameObject.transform.localScale = new Vector3(num2, num2, 1f);
		RectTransform component = ((Component)val2).GetComponent<RectTransform>();
		((Transform)component).localPosition = new Vector3(0.05975f, button.showGear ? 0.015f : 0f, TextOffset - offset / 2.225f);
		((Transform)component).localRotation = Quaternion.Euler(180f, 90f, 90f);
		if (!button.incremental)
		{
			goto Branch_0520;
		}
		goto Branch_0561;
		Branch_0090:
		if (!button.showGear)
		{
			num = 0.82f;
			if (button.showGear)
			{
				goto Branch_0105;
			}
		}
		else
		{
			num = 0.695f;
			if (button.showGear)
			{
				goto Branch_0105;
			}
		}
		goto Branch_011f;
		Branch_02dc:
		RegisterColorGroup(ColorRole.Button, val, list);
		if (!button.showGear)
		{
			goto Branch_0438;
		}
		goto Branch_0322;
		Branch_02ff:
		RegisterColorGroup(ColorRole.EnabledButton, val, list);
		if (!button.showGear)
		{
			goto Branch_0438;
		}
		goto Branch_0322;
		Branch_0105:
		if (button.incremental)
		{
			goto Branch_011f;
		}
		float num3 = 0.0625f;
		val = CreateCube("Button: " + button.buttonText, null, keepCollider: true);
		val.transform.localScale = new Vector3(0.0075f, num, 0.08f);
		val.transform.localPosition = new Vector3(0.07525f, num3, 0.335f - offset);
		ButtonHandler.BtnCollider btnCollider = val.AddComponent<ButtonHandler.BtnCollider>();
		btnCollider.clickedButton = button;
		ApplyColorMaterial(val, (Color32)(button.Enabled ? Settings.CapturedVariables3760_Color_08 : Settings.CapturedVariables3760_Color_26));
		List<GameObject> outline = AddOutline(val, 2, 0.0065f);
		list = null;
		if (!(Settings.CapturedVariables3760_Value_01 > 0f))
		{
			goto Branch_02c7;
		}
		goto Branch_029d;
		Branch_0322:
		if (button.incremental)
		{
			goto Branch_0438;
		}
		CreateGearButton(button, offset);
		val2 = CreateText();
		((Object)val2).name = "Button: Text - " + button.buttonText;
		val2.text = (ButtonHandler.CapturedVariables570_Items_02.Contains(button) ? ("<color=blue>★</color> " + button.buttonText) : button.buttonText);
		num2 = 1.2f;
		((Component)val2).gameObject.transform.localScale = new Vector3(num2, num2, 1f);
		component = ((Component)val2).GetComponent<RectTransform>();
		((Transform)component).localPosition = new Vector3(0.05975f, button.showGear ? 0.015f : 0f, TextOffset - offset / 2.225f);
		((Transform)component).localRotation = Quaternion.Euler(180f, 90f, 90f);
		if (!button.incremental)
		{
			goto Branch_0520;
		}
		goto Branch_0561;
		Branch_0520:
		Vector2 wZRSHBN = CapturedVariables1950_Position_13;
		component.sizeDelta = new Vector2(wZRSHBN.x / num2, wZRSHBN.y);
		Transform val3 = null;
		if (!ButtonHandler.IsCategoryButton(button))
		{
			goto Branch_06f4;
		}
		goto Branch_05a2;
		Branch_0561:
		wZRSHBN = CapturedVariables1950_Position_18;
		component.sizeDelta = new Vector2(wZRSHBN.x / num2, wZRSHBN.y);
		val3 = null;
		if (!ButtonHandler.IsCategoryButton(button))
		{
			goto Branch_06f4;
		}
		Branch_05a2:
		Text val4 = CreateText();
		((Object)val4).name = "Button: Arrow - " + button.buttonText;
		val4.text = ((button.buttonText == "Return") ? "<color=red><</color>" : ("<color=#" + ColorUtility.ToHtmlStringRGB((Color32)(Settings.CapturedVariables3760_Color_08)) + ">></color>"));
		val4.alignment = (TextAnchor)3;
		((Component)val4).gameObject.transform.localScale = new Vector3(1.1f, 1.1f, 1f);
		RectTransform component2 = ((Component)val4).GetComponent<RectTransform>();
		((Transform)component2).localPosition = new Vector3(0.05975f, button.showGear ? 0.012f : (-0.01125f), TextOffset - offset / 2.225f);
		((Transform)component2).localRotation = Quaternion.Euler(180f, 90f, 90f);
		component2.sizeDelta = (button.incremental ? CapturedVariables1950_Position_18 : CapturedVariables1950_Position_13);
		val3 = ((Component)val4).transform;
		RegisterButtonTransforms(btnCollider, val, list, outline, ((Component)val2).transform, val3);
		if (button != ButtonHandler.CapturedVariables570_Button_02)
		{
			return;
		}
		goto Branch_0730;
		Branch_011f:
		num3 = 0f;
		val = CreateCube("Button: " + button.buttonText, null, keepCollider: true);
		val.transform.localScale = new Vector3(0.0075f, num, 0.08f);
		val.transform.localPosition = new Vector3(0.07525f, num3, 0.335f - offset);
		btnCollider = val.AddComponent<ButtonHandler.BtnCollider>();
		btnCollider.clickedButton = button;
		ApplyColorMaterial(val, (Color32)(button.Enabled ? Settings.CapturedVariables3760_Color_08 : Settings.CapturedVariables3760_Color_26));
		outline = AddOutline(val, 2, 0.0065f);
		list = null;
		if (!(Settings.CapturedVariables3760_Value_01 > 0f))
		{
			goto Branch_02c7;
		}
		goto Branch_029d;
		Branch_06f4:
		RegisterButtonTransforms(btnCollider, val, list, outline, ((Component)val2).transform, val3);
		if (button != ButtonHandler.CapturedVariables570_Button_02)
		{
			return;
		}
		Branch_0730:
		ButtonHandler.CapturedVariables570_Button_02 = null;
		btnCollider.PlayBounce();
	}

	public static List<GameObject> RoundObject(GameObject toRound, Transform parent = null, List<Material> materialList = null, float roundness = 0.5f, int transparentQueue = 2460, int seg = 5)
	{
		List<GameObject> list = new List<GameObject>(1);
		if ((Object)(object)toRound == (Object)null)
		{
			return list;
		}
		Renderer component = toRound.GetComponent<Renderer>();
		if ((Object)(object)component == (Object)null)
		{
			return list;
		}
		if ((Object)(object)parent == (Object)null)
		{
			Transform transform = Variables.Variables_Object_14.transform;
			parent = transform;
			if (materialList == null)
			{
				goto Branch_00ce;
			}
		}
		else if (materialList == null)
		{
			goto Branch_00ce;
		}
		Vector3 localScale = toRound.transform.localScale;
		Vector3 lossyScale = parent.lossyScale;
		float num = localScale.x * 0.5f;
		float num2 = localScale.y * 0.5f * lossyScale.y;
		float num3 = localScale.z * 0.5f * lossyScale.z;
		float num4 = Mathf.Min(0.0175f * roundness, Mathf.Min(num2, num3) * 0.9f);
		float num5 = (num2 - num4) / lossyScale.y;
		float num6 = (num3 - num4) / lossyScale.z;
		float num7 = num4 / lossyScale.y;
		float num8 = num4 / lossyScale.z;
		List<Vector2> list2 = new List<Vector2>((seg + 1) * 4);
		float[] array = new float[4]
		{
			num5,
			0f - num5,
			0f - num5,
			num5
		};
		float[] array2 = new float[4]
		{
			num6,
			num6,
			0f - num6,
			0f - num6
		};
		int num9 = 0;
		Branch_0392:
		if (num9 < 4)
		{
			do
			{
				float num10 = (float)num9 * MathF.PI * 0.5f;
				float num11 = array[num9];
				float num12 = array2[num9];
				int num13 = 0;
				if (num13 <= seg)
				{
					do
					{
						float num14 = Mathf.Lerp(num10, num10 + MathF.PI / 2f, (float)num13 / (float)seg);
						list2.Add(new Vector2(num11 + Mathf.Cos(num14) * num7, num12 + Mathf.Sin(num14) * num8));
						num13++;
					}
					while (num13 <= seg);
				}
				num9++;
			}
			while (num9 < 4);
		}
		int count = list2.Count;
		List<Vector3> list3 = new List<Vector3>(count * 4 + 4);
		List<Vector3> list4 = new List<Vector3>(count * 4 + 4);
		List<int> list5 = new List<int>(count * 12);
		int count2 = list3.Count;
		list3.Add(new Vector3(num, 0f, 0f));
		list4.Add(Vector3.right);
		int count3 = list3.Count;
		int num15 = 0;
		if (num15 < count)
		{
			do
			{
				list3.Add(new Vector3(num, list2[num15].x, list2[num15].y));
				list4.Add(Vector3.right);
				num15++;
			}
			while (num15 < count);
		}
		int num16 = 0;
		if (num16 < count)
		{
			do
			{
				list5.Add(count2);
				list5.Add(count3 + (num16 + 1) % count);
				list5.Add(count3 + num16);
				num16++;
			}
			while (num16 < count);
		}
		int count4 = list3.Count;
		list3.Add(new Vector3(0f - num, 0f, 0f));
		list4.Add(Vector3.left);
		int count5 = list3.Count;
		int num17 = 0;
		if (num17 < count)
		{
			do
			{
				list3.Add(new Vector3(0f - num, list2[num17].x, list2[num17].y));
				list4.Add(Vector3.left);
				num17++;
			}
			while (num17 < count);
		}
		int num18 = 0;
		if (num18 < count)
		{
			do
			{
				list5.Add(count4);
				list5.Add(count5 + num18);
				list5.Add(count5 + (num18 + 1) % count);
				num18++;
			}
			while (num18 < count);
		}
		int count6 = list3.Count;
		int num19 = 0;
		if (num19 < count)
		{
			do
			{
				Vector3 val = new Vector3(0f, list2[num19].x, list2[num19].y);
				Vector3 normalized = ((Vector3)val).normalized;
				list3.Add(new Vector3(num, list2[num19].x, list2[num19].y));
				list4.Add(normalized);
				list3.Add(new Vector3(0f - num, list2[num19].x, list2[num19].y));
				list4.Add(normalized);
				num19++;
			}
			while (num19 < count);
		}
		int num20 = 0;
		if (num20 < count)
		{
			do
			{
				int num21 = (num20 + 1) % count;
				int num22 = count6 + num20 * 2;
				int item = num22 + 1;
				int num23 = count6 + num21 * 2;
				int item2 = num23 + 1;
				list5.Add(num22);
				list5.Add(num23);
				list5.Add(item);
				list5.Add(num23);
				list5.Add(item2);
				list5.Add(item);
				num20++;
			}
			while (num20 < count);
		}
		int count7 = list5.Count;
		int num24 = 0;
		if (num24 < count7)
		{
			do
			{
				list5.Add(list5[num24]);
				list5.Add(list5[num24 + 2]);
				list5.Add(list5[num24 + 1]);
				num24 += 3;
			}
			while (num24 < count7);
		}
		Mesh val2 = new Mesh
		{
			name = "RoundedMesh"
		};
		val2.SetVertices(list3);
		val2.SetNormals(list4);
		val2.SetTriangles(list5, 0);
		val2.RecalculateBounds();
		MeshSink.Add(val2);
		GameObject val3 = new GameObject("rounded_" + ((Object)toRound).name);
		val3.transform.SetParent(parent, false);
		val3.transform.localPosition = toRound.transform.localPosition;
		val3.transform.localRotation = toRound.transform.localRotation;
		val3.transform.localScale = Vector3.one;
		val3.AddComponent<MeshFilter>().mesh = val2;
		Material val4;
		if (Settings.CapturedVariables3760_Value_07 >= 1f)
		{
			val4 = new Material(Variables.Variables_Reference_10)
			{
				color = component.sharedMaterial.color
			};
			((Renderer)val3.AddComponent<MeshRenderer>()).sharedMaterial = val4;
			materialList.Add(val4);
			ObjectSink.Add(val3);
			component.enabled = false;
			list.Add(val3);
			return list;
		}
		val4 = new Material(Variables.Variables_Reference_11);
		Color color = component.sharedMaterial.color;
		color.a = Settings.CapturedVariables3760_Value_07;
		val4.color = color;
		val4.renderQueue = transparentQueue;
		val4.SetInt("_Cull", 0);
		((Renderer)val3.AddComponent<MeshRenderer>()).sharedMaterial = val4;
		materialList.Add(val4);
		ObjectSink.Add(val3);
		component.enabled = false;
		list.Add(val3);
		return list;
		Branch_00ce:
		List<Material> list6 = MaterialSink;
		materialList = list6;
		localScale = toRound.transform.localScale;
		lossyScale = parent.lossyScale;
		num = localScale.x * 0.5f;
		num2 = localScale.y * 0.5f * lossyScale.y;
		num3 = localScale.z * 0.5f * lossyScale.z;
		num4 = Mathf.Min(0.0175f * roundness, Mathf.Min(num2, num3) * 0.9f);
		num5 = (num2 - num4) / lossyScale.y;
		num6 = (num3 - num4) / lossyScale.z;
		num7 = num4 / lossyScale.y;
		num8 = num4 / lossyScale.z;
		list2 = new List<Vector2>((seg + 1) * 4);
		array = new float[4]
		{
			num5,
			0f - num5,
			0f - num5,
			num5
		};
		array2 = new float[4]
		{
			num6,
			num6,
			0f - num6,
			0f - num6
		};
		num9 = 0;
		goto Branch_0392;
	}

	private static void CreateCanvas()
	{
		Variables.Variables_Object_03 = new GameObject("canvas");
		Variables.Variables_Object_03.transform.parent = Variables.Variables_Object_14.transform;
		CapturedVariables1950_Object_01.Add(Variables.Variables_Object_03);
		Canvas val = Variables.Variables_Object_03.AddComponent<Canvas>();
		val.renderMode = (RenderMode)2;
		val.sortingOrder = 100;
		Variables.Variables_Object_03.AddComponent<CanvasScaler>().dynamicPixelsPerUnit = 3000f;
		Variables.Variables_Object_03.AddComponent<GraphicRaycaster>();
		Variables.Variables_Reference_08 = CreateText(4);
		Variables.Variables_Reference_08.alignment = (TextAnchor)3;
		Variables.Variables_Reference_08.supportRichText = true;
		((Graphic)Variables.Variables_Reference_08).color = (Color32)(Settings.CapturedVariables3760_Color_17);
		(float, float) tuple = TitleOffset;
		RectTransform val2 = ((Component)Variables.Variables_Reference_08).GetComponent<RectTransform>();
		((Transform)val2).localPosition = Vector3.zero;
		((Transform)val2).position = new Vector3(0.054f, tuple.Item1 - 0.0065f, tuple.Item2);
		((Transform)val2).localRotation = Quaternion.Euler(180f, 90f, 90f);
		val2.sizeDelta = CapturedVariables1950_Position_02;
	}

	public static void ClearFonts()
	{
		CapturedVariables1950_Reference_02.Clear();
		using (List<Font>.Enumerator enumerator = CapturedVariables1950_Reference_06.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				while (true)
				{
					Font current = enumerator.Current;
					if (!((Object)(object)current != (Object)null))
					{
						break;
					}
					Object.Destroy((Object)(object)current);
					if (!enumerator.MoveNext())
					{
						goto EndBranch_0068;
					}
				}
				continue;
				EndBranch_0068:
				break;
			}
		}
		CapturedVariables1950_Reference_06.Clear();
	}

	private static GameObject CreateOutlinePart(GameObject target, Transform parent, float zGrow, Color color)
	{
		GameObject val = CreateCube(null, parent);
		val.transform.position = target.transform.position;
		val.transform.rotation = target.transform.rotation;
		Vector3 localScale = target.transform.localScale;
		Vector3 lossyScale = parent.lossyScale;
		float num = zGrow * (lossyScale.z / lossyScale.y);
		val.transform.localScale = new Vector3(localScale.x - 0.0025f, localScale.y + num, localScale.z + zGrow);
		ApplyColorMaterial(val, color);
		return val;
	}

	public static void CreateButton(float offset, ButtonHandler.Button button)
	{
		if (button == null)
		{
			return;
		}
		if (Settings.CapturedVariables3760_State_06)
		{
			CreateClassicButton(offset, button);
			return;
		}
		CreateButtonBackdrop(offset);
		bool flag;
		GameObject val;
		ButtonHandler.BtnCollider btnCollider;
		List<GameObject> list;
		List<GameObject> outline;
		if (button.incremental)
		{
			CreateIncrementalArrow("DOWN : " + button.buttonText, button, offset, -0.22f, "<", "_DOWN");
			CreateIncrementalArrow("UP : " + button.buttonText, button, offset, -0.3525f, ">", "_UP");
			flag = !button.showGear;
			val = null;
			btnCollider = null;
			list = null;
			outline = null;
			if (!button.incremental)
			{
				goto Branch_0118;
			}
		}
		else
		{
			flag = !button.showGear;
			val = null;
			btnCollider = null;
			list = null;
			outline = null;
			if (!button.incremental)
			{
				goto Branch_0118;
			}
		}
		Text val2 = CreateText();
		((Object)val2).name = "Button: Text - " + button.buttonText;
		val2.text = (ButtonHandler.CapturedVariables570_Items_02.Contains(button) ? ("<color=blue>★</color> " + button.buttonText) : button.buttonText);
		if (!flag)
		{
			goto Branch_0569;
		}
		goto Branch_0522;
		Branch_0569:
		float num = 1.2f;
		((Component)val2).gameObject.transform.localScale = new Vector3(num, num, 1f);
		RectTransform component = ((Component)val2).GetComponent<RectTransform>();
		if (!flag)
		{
			goto Branch_05a8;
		}
		Branch_0705:
		float num2 = 0.035f;
		((Transform)component).localPosition = new Vector3(flag ? 0.054f : 0.054f, num2, TextOffset - offset / 2.225f);
		((Transform)component).localRotation = Quaternion.Euler(180f, 90f, 90f);
		Vector2 wZRSHBN = CapturedVariables1950_Position_13;
		component.sizeDelta = new Vector2(flag ? 0.125f : (wZRSHBN.x / num), wZRSHBN.y);
		Transform val3 = null;
		if (!((Object)(object)btnCollider != (Object)null))
		{
			return;
		}
		goto Branch_07a9;
		Branch_03e6:
		ConfigureClassicButton(button, offset, btnCollider, val, list);
		val2 = CreateText();
		((Object)val2).name = "Button: Text - " + button.buttonText;
		val2.text = (ButtonHandler.CapturedVariables570_Items_02.Contains(button) ? ("<color=blue>★</color> " + button.buttonText) : button.buttonText);
		if (!flag)
		{
			goto Branch_0569;
		}
		goto Branch_0522;
		Branch_0391:
		RegisterColorGroup(ColorRole.Button, val, list);
		if (!button.showGear)
		{
			goto Branch_03e6;
		}
		goto Branch_03d5;
		Branch_03b3:
		RegisterColorGroup(ColorRole.EnabledButton, val, list);
		if (!button.showGear)
		{
			goto Branch_03e6;
		}
		goto Branch_03d5;
		Branch_0118:
		float num3;
		if (!button.showGear)
		{
			num3 = 0.15f;
			if (!button.showGear)
			{
				goto Branch_0165;
			}
		}
		else
		{
			num3 = 0.695f;
			if (!button.showGear)
			{
				goto Branch_0165;
			}
		}
		float num4 = 0.0625f;
		if (!button.showGear)
		{
			goto Branch_019d;
		}
		goto Branch_023e;
		Branch_05a8:
		if (!button.showGear)
		{
			num2 = 0f;
			((Transform)component).localPosition = new Vector3(flag ? 0.054f : 0.054f, num2, TextOffset - offset / 2.225f);
			((Transform)component).localRotation = Quaternion.Euler(180f, 90f, 90f);
			wZRSHBN = CapturedVariables1950_Position_13;
			component.sizeDelta = new Vector2(flag ? 0.125f : (wZRSHBN.x / num), wZRSHBN.y);
			val3 = null;
			if (!((Object)(object)btnCollider != (Object)null))
			{
				return;
			}
		}
		else
		{
			num2 = 0.015f;
			((Transform)component).localPosition = new Vector3(flag ? 0.054f : 0.054f, num2, TextOffset - offset / 2.225f);
			((Transform)component).localRotation = Quaternion.Euler(180f, 90f, 90f);
			wZRSHBN = CapturedVariables1950_Position_13;
			component.sizeDelta = new Vector2(flag ? 0.125f : (wZRSHBN.x / num), wZRSHBN.y);
			val3 = null;
			if (!((Object)(object)btnCollider != (Object)null))
			{
				return;
			}
		}
		Branch_07a9:
		RegisterButtonTransforms(btnCollider, val, list, outline, flag ? null : ((Component)val2).transform, val3);
		if (CapturedVariables1950_State_09 && button.isToggle && button.Enabled && button.showGear)
		{
			btnCollider.ApplyFactor(0.92f);
		}
		return;
		Branch_03d5:
		CreateGearButton(button, offset);
		val2 = CreateText();
		((Object)val2).name = "Button: Text - " + button.buttonText;
		val2.text = (ButtonHandler.CapturedVariables570_Items_02.Contains(button) ? ("<color=blue>★</color> " + button.buttonText) : button.buttonText);
		if (!flag)
		{
			goto Branch_0569;
		}
		goto Branch_0522;
		Branch_0165:
		num4 = -0.3375f;
		if (!button.showGear)
		{
			goto Branch_019d;
		}
		Branch_023e:
		float num5 = 0.08f;
		CreateCube("Button: " + button.buttonText, null, keepCollider: true).transform.localScale = new Vector3(0.0075f, num3, num5);
		val.transform.localPosition = new Vector3(0.07525f, num4, 0.335f - offset);
		btnCollider = val.AddComponent<ButtonHandler.BtnCollider>();
		btnCollider.clickedButton = button;
		ApplyColorMaterial(val, (Color32)(button.Enabled ? Settings.CapturedVariables3760_Color_08 : Settings.CapturedVariables3760_Color_26));
		if (!button.showGear)
		{
			goto Branch_033c;
		}
		Branch_02df:
		outline = AddOutline(val, 2, 0.0065f, animated: false);
		if (Settings.CapturedVariables3760_Value_01 > 0f)
		{
			list = RoundObject(val, null, null, Settings.CapturedVariables3760_Value_01);
		}
		if (!button.Enabled)
		{
			goto Branch_0391;
		}
		goto Branch_03b3;
		Branch_033c:
		outline = AddOutline(val, 2, 0.0065f, animated: false, 1.6f, 12);
		list = RoundObject(val, null, null, 1.6f, 2462, 12);
		if (!button.Enabled)
		{
			goto Branch_0391;
		}
		goto Branch_03b3;
		Branch_0522:
		val2.alignment = (TextAnchor)3;
		num = 1.2f;
		((Component)val2).gameObject.transform.localScale = new Vector3(num, num, 1f);
		component = ((Component)val2).GetComponent<RectTransform>();
		if (!flag)
		{
			goto Branch_05a8;
		}
		goto Branch_0705;
		Branch_019d:
		num5 = 0.058f;
		CreateCube("Button: " + button.buttonText, null, keepCollider: true).transform.localScale = new Vector3(0.0075f, num3, num5);
		val.transform.localPosition = new Vector3(0.07525f, num4, 0.335f - offset);
		btnCollider = val.AddComponent<ButtonHandler.BtnCollider>();
		btnCollider.clickedButton = button;
		ApplyColorMaterial(val, (Color32)(button.Enabled ? Settings.CapturedVariables3760_Color_08 : Settings.CapturedVariables3760_Color_26));
		if (!button.showGear)
		{
			goto Branch_033c;
		}
		goto Branch_02df;
	}

	private static void UpdateKeyboardMenuTransform()
	{
		float y;
		Vector3 position;
		Transform transform2;
		if ((Object)(object)SearchAndKeyboard.KeyCollider_Object_01 != (Object)null && (Object)(object)Variables.Variables_Reference_06 != (Object)null)
		{
			y = ((Component)Variables.Variables_Reference_06.headCollider).transform.eulerAngles.y;
			position = ((Component)Variables.Variables_Reference_06.headCollider).transform.position;
			if (CapturedVariables1950_Value_07 == float.MinValue)
			{
				CapturedVariables1950_Value_07 = y;
				CapturedVariables1950_Position_11 = position;
				if (Variables.Variables_State_06)
				{
					goto Branch_00d5;
				}
			}
			else if (Variables.Variables_State_06)
			{
				goto Branch_00d5;
			}
			SearchAndKeyboard.KeyCollider_Object_01.transform.position = CapturedVariables1950_Position_11;
			SearchAndKeyboard.KeyCollider_Object_01.transform.rotation = Quaternion.Euler(0f, CapturedVariables1950_Value_07, 0f);
			Transform transform = SearchAndKeyboard.KeyCollider_Object_01.transform;
			transform.position += SearchAndKeyboard.KeyCollider_Object_01.transform.forward * 0.35f - Vector3.up * 0.35f;
			SearchAndKeyboard.KeyCollider_Object_01.transform.Rotate(Vector3.right, 70f);
			transform2 = SearchAndKeyboard.KeyCollider_Object_01.transform;
			Variables.Variables_Object_14.transform.position = transform2.position - transform2.forward * 0.17f + transform2.up * 0.2f;
			Variables.Variables_Object_14.transform.rotation = transform2.rotation * Quaternion.Euler(-75f, 0f, 0f);
			Variables.Variables_Object_14.transform.Rotate(Vector3.up, 90f);
			Variables.Variables_Object_14.transform.Rotate(Vector3.right, -90f);
		}
		else
		{
			transform2 = SearchAndKeyboard.KeyCollider_Object_01.transform;
			Variables.Variables_Object_14.transform.position = transform2.position - transform2.forward * 0.17f + transform2.up * 0.2f;
			Variables.Variables_Object_14.transform.rotation = transform2.rotation * Quaternion.Euler(-75f, 0f, 0f);
			Variables.Variables_Object_14.transform.Rotate(Vector3.up, 90f);
			Variables.Variables_Object_14.transform.Rotate(Vector3.right, -90f);
		}
		return;
		Branch_00d5:
		float num = Mathf.Abs(Mathf.DeltaAngle(CapturedVariables1950_Value_07, y));
		float num2 = Vector3.Distance(position, CapturedVariables1950_Position_11);
		if (!(num < 45f) || num2 >= 1.5f)
		{
			CapturedVariables1950_Value_07 = y;
			CapturedVariables1950_Position_11 = position;
			SearchAndKeyboard.KeyCollider_Object_01.transform.position = position;
			SearchAndKeyboard.KeyCollider_Object_01.transform.rotation = Quaternion.Euler(0f, CapturedVariables1950_Value_07, 0f);
			Transform transform3 = SearchAndKeyboard.KeyCollider_Object_01.transform;
			transform3.position += SearchAndKeyboard.KeyCollider_Object_01.transform.forward * 0.35f - Vector3.up * 0.35f;
			SearchAndKeyboard.KeyCollider_Object_01.transform.Rotate(Vector3.right, 70f);
		}
		else
		{
			SearchAndKeyboard.KeyCollider_Object_01.transform.position = position;
			SearchAndKeyboard.KeyCollider_Object_01.transform.rotation = Quaternion.Euler(0f, CapturedVariables1950_Value_07, 0f);
			Transform transform4 = SearchAndKeyboard.KeyCollider_Object_01.transform;
			transform4.position += SearchAndKeyboard.KeyCollider_Object_01.transform.forward * 0.35f - Vector3.up * 0.35f;
			SearchAndKeyboard.KeyCollider_Object_01.transform.Rotate(Vector3.right, 70f);
		}
		transform2 = SearchAndKeyboard.KeyCollider_Object_01.transform;
		Variables.Variables_Object_14.transform.position = transform2.position - transform2.forward * 0.17f + transform2.up * 0.2f;
		Variables.Variables_Object_14.transform.rotation = transform2.rotation * Quaternion.Euler(-75f, 0f, 0f);
		Variables.Variables_Object_14.transform.Rotate(Vector3.up, 90f);
		Variables.Variables_Object_14.transform.Rotate(Vector3.right, -90f);
	}

	public static void CreateSearchField()
	{
		if (SearchAndKeyboard.KeyCollider_State_02)
		{
			CapturedVariables1950_State_10 = true;
			Variables.Variables_Object_02 = CreateCube(null, null, keepCollider: true);
			Variables.Variables_Object_02.transform.localScale = CapturedVariables1950_Position_04;
			Variables.Variables_Object_02.transform.localPosition = CapturedVariables1950_Position_15;
			ApplyColorMaterial(Variables.Variables_Object_02, (Color32)(Settings.CapturedVariables3760_Color_19));
			AddOutline(Variables.Variables_Object_02, 1);
			List<GameObject> extraParts = null;
			if (Settings.CapturedVariables3760_Value_01 > 0f)
			{
				extraParts = RoundObject(Variables.Variables_Object_02, null, null, Settings.CapturedVariables3760_Value_01);
				RegisterColorGroup(ColorRole.Background, Variables.Variables_Object_02, extraParts);
				SearchAndKeyboard.KeyCollider_Reference_01 = CreateText();
				SearchAndKeyboard.KeyCollider_Reference_01.alignment = (TextAnchor)4;
				RectTransform component = ((Component)SearchAndKeyboard.KeyCollider_Reference_01).GetComponent<RectTransform>();
				component.sizeDelta = new Vector2(0.2f, 0.02f);
				((Transform)component).localScale = new Vector3(0.9f, 0.9f, 0.9f);
				((Transform)component).localRotation = Quaternion.Euler(180f, 90f, 90f);
				((Transform)component).localPosition = CapturedVariables1950_Position_06;
				SearchAndKeyboard.KeyCollider_Reference_01.text = ((!string.IsNullOrEmpty(SearchAndKeyboard.KeyCollider_Text_02)) ? SearchAndKeyboard.KeyCollider_Text_02 : SearchAndKeyboard.KeyCollider_Text_03);
				CapturedVariables1950_State_10 = false;
			}
			else
			{
				RegisterColorGroup(ColorRole.Background, Variables.Variables_Object_02, extraParts);
				SearchAndKeyboard.KeyCollider_Reference_01 = CreateText();
				SearchAndKeyboard.KeyCollider_Reference_01.alignment = (TextAnchor)4;
				RectTransform component = ((Component)SearchAndKeyboard.KeyCollider_Reference_01).GetComponent<RectTransform>();
				component.sizeDelta = new Vector2(0.2f, 0.02f);
				((Transform)component).localScale = new Vector3(0.9f, 0.9f, 0.9f);
				((Transform)component).localRotation = Quaternion.Euler(180f, 90f, 90f);
				((Transform)component).localPosition = CapturedVariables1950_Position_06;
				SearchAndKeyboard.KeyCollider_Reference_01.text = ((!string.IsNullOrEmpty(SearchAndKeyboard.KeyCollider_Text_02)) ? SearchAndKeyboard.KeyCollider_Text_02 : SearchAndKeyboard.KeyCollider_Text_03);
				CapturedVariables1950_State_10 = false;
			}
		}
	}

	public static void RegisterColorGroup(ColorRole role, GameObject baseObj, List<GameObject> extraParts)
	{
		if (role == ColorRole.None || (Object)(object)baseObj == (Object)null)
		{
			return;
		}
		TrackedColorGroup trackedColorGroup = new TrackedColorGroup
		{
			role = role
		};
		Renderer component = baseObj.GetComponent<Renderer>();
		if ((Object)(object)component != (Object)null)
		{
			trackedColorGroup.renderers.Add(component);
			if (extraParts != null)
			{
				goto Branch_00b9;
			}
		}
		else if (extraParts != null)
		{
			goto Branch_00b9;
		}
		CapturedVariables1950_Color_03.Add(trackedColorGroup);
		CapturedVariables1950_State_03 = true;
		return;
		Branch_00b9:
		int num = 0;
		if (num < extraParts.Count)
		{
			while (true)
			{
				if (!((Object)(object)extraParts[num] == (Object)null))
				{
					Renderer component2 = extraParts[num].GetComponent<Renderer>();
					if ((Object)(object)component2 != (Object)null)
					{
						trackedColorGroup.renderers.Add(component2);
						num++;
						if (num >= extraParts.Count)
						{
							break;
						}
					}
					else
					{
						num++;
						if (num >= extraParts.Count)
						{
							break;
						}
					}
				}
				else
				{
					num++;
					if (num >= extraParts.Count)
					{
						break;
					}
				}
			}
		}
		CapturedVariables1950_Color_03.Add(trackedColorGroup);
		CapturedVariables1950_State_03 = true;
	}

	private static void TickEnabledMods()
	{
		int num2;
		if (CapturedVariables1950_State_11)
		{
			CapturedVariables1950_Button_01.Clear();
			ButtonHandler.Button[] array = ModButtons.buttons;
			int num = 0;
			while (num < array.Length)
			{
				ButtonHandler.Button button = array[num];
				if (button != null && button.Enabled && button.onEnable != null)
				{
					CapturedVariables1950_Button_01.Add(button);
					num++;
				}
				else
				{
					num++;
				}
			}
			CapturedVariables1950_State_11 = false;
			num2 = 0;
		}
		else
		{
			num2 = 0;
		}
		if (num2 >= CapturedVariables1950_Button_01.Count)
		{
			return;
		}
		while (true)
		{
			ButtonHandler.Button button2 = CapturedVariables1950_Button_01[num2];
			if (button2 != null && button2.Enabled && button2.onEnable != null)
			{
				try
				{
					button2.onEnable();
				}
				catch (Exception ex)
				{
					Debug.LogError((object)("Button Tick Error '" + button2.buttonText + "': " + ex.Message + "\n" + ex.StackTrace));
				}
				num2++;
				if (num2 >= CapturedVariables1950_Button_01.Count)
				{
					break;
				}
			}
			else
			{
				num2++;
				if (num2 >= CapturedVariables1950_Button_01.Count)
				{
					break;
				}
			}
		}
	}

	private static void UpdateWristMenuTransform()
	{
		if ((Object)(object)Variables.Variables_Object_14 == (Object)null || (Object)(object)Variables.Variables_Reference_06 == (Object)null)
		{
			return;
		}
		Vector3 val;
		Transform val2;
		Quaternion rotation;
		Vector3 eulerAngles;
		if (!Variables.Variables_State_05)
		{
			HandState leftHand = Variables.Variables_Reference_06.LeftHand;
			val2 = leftHand.controllerTransform;
			val = leftHand.controllerTransform.position - val2.up * 0.035f + val2.right * 0.006f;
			rotation = val2.rotation;
			eulerAngles = ((Quaternion)rotation).eulerAngles;
			if (Variables.Variables_State_05)
			{
				goto Branch_013f;
			}
		}
		else
		{
			HandState leftHand = Variables.Variables_Reference_06.RightHand;
			val2 = leftHand.controllerTransform;
			val = leftHand.controllerTransform.position - val2.up * 0.035f + val2.right * 0.006f;
			rotation = val2.rotation;
			eulerAngles = ((Quaternion)rotation).eulerAngles;
			if (Variables.Variables_State_05)
			{
				goto Branch_013f;
			}
		}
		Quaternion val3 = Quaternion.Euler(eulerAngles);
		if (!CapturedVariables1950_State_06)
		{
			goto Branch_0231;
		}
		Branch_019c:
		if (CapturedVariables1950_State_02)
		{
			goto Branch_0231;
		}
		float num = 1f - Mathf.Exp(-25f * Time.deltaTime);
		Variables.Variables_Object_14.transform.position = Vector3.Lerp(Variables.Variables_Object_14.transform.position, val, num);
		Variables.Variables_Object_14.transform.rotation = Quaternion.Slerp(Variables.Variables_Object_14.transform.rotation, val3, num);
		return;
		Branch_013f:
		val3 = Quaternion.Euler(eulerAngles + new Vector3(0f, 0f, 180f));
		if (!CapturedVariables1950_State_06)
		{
			goto Branch_0231;
		}
		goto Branch_019c;
		Branch_0231:
		Variables.Variables_Object_14.transform.position = val;
		Variables.Variables_Object_14.transform.rotation = val3;
		CapturedVariables1950_State_02 = false;
	}

	public static void HandleLeftRoom()
	{
		if (CapturedVariables1950_State_04)
		{
			CapturedVariables1950_State_04 = false;
			MenuPatches.SerializationPatch.SerializationPatch_State_01 = null;
			NetworkingLibrary.ClearKnownUsers();
			ShowRoomNotification("Left Code `" + Room.ForceCreateRoom_StateMachine11_Text_01 + "`");
			PlayersActionList.ResetPlayersList();
			Sound.ResetMicrophone();
		}
	}

	public static void UpdateDisconnectIcon()
	{
		if (!((Object)(object)CapturedVariables1950_Object_04 == (Object)null))
		{
			Renderer component = CapturedVariables1950_Object_04.GetComponent<Renderer>();
			if (!((Object)(object)component == (Object)null) && !((Object)(object)component.sharedMaterial == (Object)null))
			{
				component.sharedMaterial.color = (Color32)(PhotonNetwork.InRoom ? new Color32((byte)235, (byte)70, (byte)60, byte.MaxValue) : new Color32((byte)70, (byte)210, (byte)95, byte.MaxValue));
			}
		}
	}

	public static Material CreatePinwheelMaterial()
	{
		if ((Object)(object)CapturedVariables1950_Material_02 == (Object)null)
		{
			CapturedVariables1950_Material_02 = AssetHandler.LoadMaterial("NXO.Resources.pinwheelshader", "outline2");
			if ((Object)(object)CapturedVariables1950_Material_02 == (Object)null)
			{
				goto Branch_0071;
			}
		}
		else if ((Object)(object)CapturedVariables1950_Material_02 == (Object)null)
		{
			goto Branch_0071;
		}
		Material val = new Material(CapturedVariables1950_Material_02);
		AssetHandler.SetMaterialProperty(val, "_Speed", 0f - CapturedVariables1950_Value_06);
		AssetHandler.SetMaterialProperty(val, "_COLOR1", CapturedVariables1950_Color_02);
		AssetHandler.SetMaterialProperty(val, "_COLOR2", CapturedVariables1950_Color_06);
		return val;
		Branch_0071:
		Debug.LogError((object)"Failed to load pinwheel material");
		return null;
	}

	private static void CreateMenuBackground()
	{
		Variables.Variables_Object_09 = CreateCube("menucolor");
		Variables.Variables_Object_09.transform.localScale = CapturedVariables1950_Position_09;
		Variables.Variables_Object_09.transform.position = CapturedVariables1950_Position_16;
		ApplyColorMaterial(Variables.Variables_Object_09, (Color32)(Settings.CapturedVariables3760_Color_19));
		AddOutline(Variables.Variables_Object_09, 1);
		List<GameObject> extraParts = null;
		if (Settings.CapturedVariables3760_Value_01 > 0f)
		{
			extraParts = RoundObject(Variables.Variables_Object_09, null, null, Settings.CapturedVariables3760_Value_01, 2450);
			RegisterColorGroup(ColorRole.Background, Variables.Variables_Object_09, extraParts);
		}
		else
		{
			RegisterColorGroup(ColorRole.Background, Variables.Variables_Object_09, extraParts);
		}
	}

	public static void RebuildMenu()
	{
		NXOUI.CapturedVariables1190_Index_02++;
		DestroyMenuContents();
		BuildMenu();
		SearchAndKeyboard.RefreshKeyboardColors();
	}

	public static void SetUiLayerRecursively(GameObject obj)
	{
		if ((Object)(object)obj == (Object)null)
		{
			return;
		}
		obj.layer = LayerMask.NameToLayer("UI");
		IEnumerator enumerator = obj.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				while (true)
				{
					Transform val = (Transform)enumerator.Current;
					if (!((Object)(object)val != (Object)null))
					{
						break;
					}
					SetUiLayerRecursively(((Component)val).gameObject);
					if (!enumerator.MoveNext())
					{
						return;
					}
				}
			}
		}
		finally
		{
			IDisposable disposable = enumerator as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
	}
}
