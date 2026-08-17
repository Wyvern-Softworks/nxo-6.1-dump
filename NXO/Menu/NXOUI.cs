using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using BepInEx;
using GorillaNetworking;
using NXO.Mods;
using NXO.Mods.Categories;
using NXO.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NXO.Menu;

[DefaultExecutionOrder(10000)]
public class NXOUI : MonoBehaviour
{
	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct CapturedVariables1190
	{
		public long key;
	}

	public static bool CapturedVariables1190_State_04 = true;

	public static readonly List<string> CapturedVariables1190_Items_01 = new List<string>();

	public static bool CapturedVariables1190_State_01;

	public static bool CapturedVariables1190_State_03;

	public static bool CapturedVariables1190_State_05;

	public static float CapturedVariables1190_Value_01 = 400f;

	public static float CapturedVariables1190_Value_02 = 540f;

	public static int CapturedVariables1190_Index_01;

	public static int CapturedVariables1190_Index_02;

	private static bool CapturedVariables1190_State_02;

	private static Vector3? CapturedVariables1190_Position_01;

	private static readonly Dictionary<string, float> CapturedVariables1190_Lookup_03 = new Dictionary<string, float>();

	private static readonly Dictionary<string, float> CapturedVariables1190_Lookup_02 = new Dictionary<string, float>();

	private static readonly List<string> CapturedVariables1190_Items_02 = new List<string>();

	private static readonly HashSet<string> CapturedVariables1190_Set_01 = new HashSet<string>();

	private static readonly List<(string mod, float w)> Recovered_Reference_16 = new List<(string, float)>(32);

	private readonly Dictionary<int, Texture2D> _modBarTexCache;

	private readonly Dictionary<int, float> _modBarTexUsed;

	private const int ModBarCacheCap = 48;

	private int _lastWatermarkFps;

	private string _cachedWatermarkText;

	private float _cachedWatermarkW;

	private Texture2D _watermarkTex;

	private Texture2D _bgTex;

	private Rect _win;

	private string _inputUpper;

	private bool _pcPage;

	private int _lastW;

	private int _lastH;

	private bool _dirty;

	private Font _font;

	private int _btnIndex;

	private readonly Dictionary<string, Texture2D> _tex;

	private readonly Dictionary<long, Texture2D> _roundCache;

	private readonly Dictionary<long, float> _roundUsed;

	private const int RoundCacheCap = 256;

	private Texture2D _pixel;

	private float _smoothFps;

	private float _fpsUpdateTimer;

	private int _displayedFps;

	private GUIStyle _sField;

	private GUIStyle _sBtn;

	private GUIStyle _sBtnOn;

	private GUIStyle _sMod;

	private GUIStyle _sWatermark;

	private GUIStyle _sTitle;

	private GUIStyle _sDim;

	private GUIStyle _sSmall;

	private GUIStyle _sCardTitle;

	private GUIStyle _sCardDesc;

	private GUIStyle _sPill;

	private readonly GUIContent _gc;

	private readonly List<ButtonHandler.Button> _catsCache;

	private readonly List<ButtonHandler.Button> _contentCache;

	private int _catsKey;

	private int _contentPageKey;

	private string _contentSearchKey;

	private int _contentModKey;

	private int _contentMenuKey;

	private int _scrollDragId;

	private float _scrollDragOffset;

	private int _lastSidebarPage;

	private readonly Dictionary<int, float> _clickAnim;

	private readonly Dictionary<int, float> _toggleAnim;

	private readonly Dictionary<int, float> _hoverAnim;

	private const float ClickAnimDur = 0.16f;

	private const float WatermarkH = 26f;

	private const float Pad = 12f;

	private const float BtnH = 30f;

	private const float BtnGap = 6f;

	private const float SideW = 124f;

	private const float ScrollBarW = 7f;

	private float _contentScroll;

	private float _categoryScroll;

	private float _animT;

	private bool _animatingIn;

	private bool _animatingOut;

	private bool _wasVisible;

	private static readonly Dictionary<string, string> CapturedVariables1190_Lookup_01 = new Dictionary<string, string>(64);

	public static int originalWidth { get; private set; }

	public static int originalHeight { get; private set; }

	private void DrawToggle(float x, float y, bool on, int id, Action click)
	{
		Rect val = default(Rect);
		val = new Rect(x, y, 52f, 20f);
		bool flag = ((Rect)val).Contains(Event.current.mousePosition);
		float num;
		float value;
		if (!on)
		{
			num = 0f;
			if (!_toggleAnim.TryGetValue(id, out value))
			{
				goto Branch_008d;
			}
		}
		else
		{
			num = 1f;
			if (!_toggleAnim.TryGetValue(id, out value))
			{
				goto Branch_008d;
			}
		}
		if ((int)Event.current.type != 7)
		{
			goto Branch_0222;
		}
		goto Branch_00d1;
		Branch_0222:
		float num2 = Mathf.SmoothStep(0f, 1f, value);
		bool flag2 = num2 >= 0.5f;
		DrawRound(val, 10, flag2 ? new Color32((byte)33, (byte)82, (byte)92, (byte)250) : (flag ? new Color32((byte)40, (byte)40, (byte)40, (byte)248) : new Color32((byte)25, (byte)25, (byte)25, (byte)245)), flag2 ? new Color32((byte)100, (byte)226, (byte)240, (byte)220) : new Color32((byte)92, (byte)92, (byte)92, (byte)190));
		float num3 = Mathf.Lerp(x + 3f, x + 35f, num2);
		DrawRound(new Rect(num3, y + 3f, 14f, 14f), 7, new Color32((byte)235, (byte)235, (byte)235, byte.MaxValue), new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, (byte)200));
		if (!GUI.Button(val, GUIContent.none, GUIStyle.none))
		{
			return;
		}
		Branch_0352:
		click();
		return;
		Branch_008d:
		value = num;
		if ((int)Event.current.type != 7)
		{
			goto Branch_0222;
		}
		Branch_00d1:
		value = Mathf.MoveTowards(value, num, Time.unscaledDeltaTime * 8f);
		_toggleAnim[id] = value;
		num2 = Mathf.SmoothStep(0f, 1f, value);
		flag2 = num2 >= 0.5f;
		DrawRound(val, 10, flag2 ? new Color32((byte)33, (byte)82, (byte)92, (byte)250) : (flag ? new Color32((byte)40, (byte)40, (byte)40, (byte)248) : new Color32((byte)25, (byte)25, (byte)25, (byte)245)), flag2 ? new Color32((byte)100, (byte)226, (byte)240, (byte)220) : new Color32((byte)92, (byte)92, (byte)92, (byte)190));
		num3 = Mathf.Lerp(x + 3f, x + 35f, num2);
		DrawRound(new Rect(num3, y + 3f, 14f, 14f), 7, new Color32((byte)235, (byte)235, (byte)235, byte.MaxValue), new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, (byte)200));
		if (!GUI.Button(val, GUIContent.none, GUIStyle.none))
		{
			return;
		}
		goto Branch_0352;
	}

	private void DrawModCard(float x, ref float y, float w, ButtonHandler.Button b)
	{
		Rect r = default(Rect);
		r = new Rect(x, y, w, 30f);
		int num = RuntimeHelpers.GetHashCode(b) * 17;
		string text = StripRichText(b.buttonText);
		if (FavoriteModsSafeContains(b))
		{
			text = "★ " + text;
			if (b.incremental)
			{
				goto Branch_0086;
			}
		}
		else if (b.incremental)
		{
			goto Branch_0086;
		}
		if (b.isToggle)
		{
			if (RowToggle(r, text, b.Enabled, num))
			{
				ButtonHandler.PlayClickSound();
				ButtonHandler.HandleButtonClick(b);
			}
			goto Branch_02ff;
		}
		if (RowButton(r, text, num))
		{
			ButtonHandler.PlayClickSound();
			ButtonHandler.HandleButtonClick(b);
			y += 36f;
			_btnIndex++;
		}
		else
		{
			y += 36f;
			_btnIndex++;
		}
		return;
		Branch_01f1:
		ButtonHandler.PlayClickSound();
		ButtonHandler.HandleButtonClick(new ButtonHandler.Button(b.buttonText + "_UP", b.Page, isToggle: false, isActive: false, null));
		Branch_02ff:
		y += 36f;
		_btnIndex++;
		return;
		Branch_0086:
		bool flag = ((Rect)r).Contains(Event.current.mousePosition);
		DrawRound(r, 8, flag ? new Color32((byte)30, (byte)30, (byte)30, (byte)246) : new Color32((byte)20, (byte)20, (byte)20, (byte)242), new Color32((byte)62, (byte)62, (byte)62, (byte)120));
		_sCardTitle.normal.textColor = Color.white;
		GUI.Label(new Rect(x + 12f, y, w - 88f, 30f), text, _sCardTitle);
		float y2 = y + 4f;
		if (MiniButton(x + w - 76f, y2, 28f, 22f, "<", num + 3))
		{
			ButtonHandler.PlayClickSound();
			ButtonHandler.HandleButtonClick(new ButtonHandler.Button(b.buttonText + "_DOWN", b.Page, isToggle: false, isActive: false, null));
			if (MiniButton(x + w - 40f, y2, 28f, 22f, ">", num + 4))
			{
				goto Branch_01f1;
			}
		}
		else if (MiniButton(x + w - 40f, y2, 28f, 22f, ">", num + 4))
		{
			goto Branch_01f1;
		}
		goto Branch_02ff;
	}

	private void DrawModList()
	{
		float unscaledTime = Time.unscaledTime;
		float num = 8f;
		CapturedVariables1190_Items_02.Clear();
		using (Dictionary<string, float>.Enumerator enumerator = CapturedVariables1190_Lookup_02.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				while (true)
				{
					KeyValuePair<string, float> current = enumerator.Current;
					if (!(unscaledTime - current.Value >= 0.4f))
					{
						break;
					}
					CapturedVariables1190_Items_02.Add(current.Key);
					if (!enumerator.MoveNext())
					{
						goto EndBranch_008b;
					}
				}
				continue;
				EndBranch_008b:
				break;
			}
		}
		int num2 = 0;
		if (num2 < CapturedVariables1190_Items_02.Count)
		{
			do
			{
				CapturedVariables1190_Items_01.Remove(CapturedVariables1190_Items_02[num2]);
				CapturedVariables1190_Set_01.Remove(CapturedVariables1190_Items_02[num2]);
				CapturedVariables1190_Lookup_03.Remove(CapturedVariables1190_Items_02[num2]);
				CapturedVariables1190_Lookup_02.Remove(CapturedVariables1190_Items_02[num2]);
				CapturedVariables1190_Index_01++;
				num2++;
			}
			while (num2 < CapturedVariables1190_Items_02.Count);
		}
		int count = CapturedVariables1190_Items_01.Count;
		if (count == 0)
		{
			return;
		}
		Recovered_Reference_16.Clear();
		int num3 = 0;
		if (num3 < count)
		{
			do
			{
				string text = CapturedVariables1190_Items_01[num3];
				_gc.text = text;
				Recovered_Reference_16.Add((text, _sMod.CalcSize(_gc).x));
				num3++;
			}
			while (num3 < count);
		}
		Recovered_Reference_16.Sort(((string mod, float w) a, (string mod, float w) b) => b.w.CompareTo(a.w));
		int num4 = 0;
		if (num4 < Recovered_Reference_16.Count)
		{
			do
			{
				Branch_0264:
				string item = Recovered_Reference_16[num4].mod;
				bool flag = !CapturedVariables1190_Set_01.Contains(item);
				float item2 = Recovered_Reference_16[num4].w;
				float num5;
				float value2;
				if (!CapturedVariables1190_Lookup_03.TryGetValue(item, out var value))
				{
					num5 = 999f;
					if (!CapturedVariables1190_Lookup_02.TryGetValue(item, out value2))
					{
						goto Branch_02ee;
					}
				}
				else
				{
					num5 = unscaledTime - value;
					if (!CapturedVariables1190_Lookup_02.TryGetValue(item, out value2))
					{
						goto Branch_02ee;
					}
				}
				float num6 = unscaledTime - value2;
				float num7 = 0f;
				float num8 = 1f;
				float num9 = 1f;
				if (!flag)
				{
					goto Branch_03a0;
				}
				goto Branch_0351;
				Branch_03a0:
				if (!(num5 < 0.4f))
				{
					goto Branch_051d;
				}
				float num10 = num5 / 0.4f;
				float num11 = Mathf.Sin(num10 * MathF.PI * 2.5f) * (1f - num10);
				num7 = num11 * 8f;
				num8 = 1f + num11 * 0.15f;
				float num12 = 6f + num7;
				float num13 = num + (float)num4 * 24f;
				float num14 = item2 + 18f;
				Matrix4x4 matrix = GUI.matrix;
				GUIUtility.ScaleAroundPivot(new Vector2(num8, 1f), new Vector2(num12, num13 + 10f));
				Texture2D modBarTex = GetModBarTex((int)num14);
				Color val = GetAnimatedGradientColor((float)num4 * 0.15f);
				GUI.color = new Color(1f, 1f, 1f, num9);
				GUI.DrawTexture(new Rect(num12, num13, num14, 20f), (Texture)(object)modBarTex);
				_sMod.normal.textColor = new Color(val.r, val.g, val.b, num9);
				GUI.Label(new Rect(num12 + 10f, num13, item2 + 10f, 20f), item, _sMod);
				GUI.matrix = matrix;
				GUI.color = Color.white;
				num4++;
				if (num4 >= Recovered_Reference_16.Count)
				{
					break;
				}
				goto Branch_0264;
				Branch_051d:
				num12 = 6f + num7;
				num13 = num + (float)num4 * 24f;
				num14 = item2 + 18f;
				matrix = GUI.matrix;
				GUIUtility.ScaleAroundPivot(new Vector2(num8, 1f), new Vector2(num12, num13 + 10f));
				modBarTex = GetModBarTex((int)num14);
				val = GetAnimatedGradientColor((float)num4 * 0.15f);
				GUI.color = new Color(1f, 1f, 1f, num9);
				GUI.DrawTexture(new Rect(num12, num13, num14, 20f), (Texture)(object)modBarTex);
				_sMod.normal.textColor = new Color(val.r, val.g, val.b, num9);
				GUI.Label(new Rect(num12 + 10f, num13, item2 + 10f, 20f), item, _sMod);
				GUI.matrix = matrix;
				GUI.color = Color.white;
				num4++;
				continue;
				Branch_02ee:
				num6 = 999f;
				num7 = 0f;
				num8 = 1f;
				num9 = 1f;
				if (!flag)
				{
					goto Branch_03a0;
				}
				Branch_0351:
				if (!(num6 < 0.4f))
				{
					goto Branch_03a0;
				}
				float num15 = num6 / 0.4f;
				num7 = num15 * 30f;
				num9 = 1f - num15;
				goto Branch_051d;
			}
			while (num4 < Recovered_Reference_16.Count);
		}
		_sMod.normal.textColor = Color.white;
	}

	private List<ButtonHandler.Button> GetSidebarCats()
	{
		if (_catsKey != CapturedVariables1190_Index_02)
		{
			_catsKey = CapturedVariables1190_Index_02;
			_catsCache.Clear();
			ButtonHandler.Button[] array = ModButtons.buttons;
			int num = 0;
			while (num < array.Length)
			{
				ButtonHandler.Button button = array[num];
				if (button != null && button.Page == Category.Home && button.isCategory)
				{
					string text = StripRichText(button.buttonText);
					if (!(text == "Home") && !(text == "Main"))
					{
						_catsCache.Add(button);
						num++;
						continue;
					}
				}
				num++;
			}
			return _catsCache;
		}
		return _catsCache;
	}

	private Texture2D Tex(string k)
	{
		if (!_tex.TryGetValue(k, out Texture2D value))
		{
			return _pixel;
		}
		return value;
	}

	public static void TrackModDisabled(string t)
	{
		if (CapturedVariables1190_Set_01.Contains(t))
		{
			CapturedVariables1190_Lookup_02[t] = Time.unscaledTime;
			CapturedVariables1190_Index_01++;
		}
	}

	private string GetDescription(ButtonHandler.Button b)
	{
		if (!string.IsNullOrWhiteSpace(b.tooltip))
		{
			return StripRichText(b.tooltip);
		}
		if (b.incremental)
		{
			return "Use the arrows to change this setting.";
		}
		if (b.isCategory)
		{
			return "Open this category.";
		}
		if (b.isToggle)
		{
			if (!b.Enabled)
			{
				return "Disabled — click the switch to turn it on.";
			}
			return "Enabled — click the switch to turn it off.";
		}
		return "Run this action.";
	}

	private long RoundKey(int w, int h, int r, int bw, Color32 fill, Color32 border)
	{
		CapturedVariables1190 obj = new CapturedVariables1190();
		obj.key = 1469598103934665603L;
		MixHashValue(w, ref obj);
		MixHashValue(h, ref obj);
		MixHashValue(r, ref obj);
		MixHashValue(bw, ref obj);
		MixHashValue((fill.r << 24) | (fill.g << 16) | (fill.b << 8) | fill.a, ref obj);
		MixHashValue((border.r << 24) | (border.g << 16) | (border.b << 8) | border.a, ref obj);
		return obj.key;
	}

	private float ClickScale(int id)
	{
		if (_clickAnim.TryGetValue(id, out var value))
		{
			float num = (Time.unscaledTime - value) / 0.16f;
			if (num >= 1f)
			{
				_clickAnim.Remove(id);
				return 1f;
			}
			return 1f - Mathf.Sin(num * MathF.PI) * 0.12f;
		}
		return 1f;
	}

	private void Update()
	{
		UpdateFreecam();
	}

	private void EnsureBgTexture()
	{
		int num = Mathf.Max(1, (int)CapturedVariables1190_Value_02);
		int num2 = Mathf.Max(1, (int)CapturedVariables1190_Value_01);
		if (!((Object)(object)_bgTex != (Object)null) || ((Texture)_bgTex).width != num || ((Texture)_bgTex).height != num2 || !_tex.ContainsKey("bg"))
		{
			if ((Object)(object)_bgTex != (Object)null)
			{
				Object.Destroy((Object)(object)_bgTex);
				_bgTex = CreateRoundedTexture(num, num2, new Color32((byte)10, (byte)10, (byte)10, (byte)226), new Color32((byte)80, (byte)80, (byte)80, (byte)155), 1, 16);
				_tex["bg"] = _bgTex;
			}
			else
			{
				_bgTex = CreateRoundedTexture(num, num2, new Color32((byte)10, (byte)10, (byte)10, (byte)226), new Color32((byte)80, (byte)80, (byte)80, (byte)155), 1, 16);
				_tex["bg"] = _bgTex;
			}
		}
	}

	public NXOUI()
	{
		_modBarTexCache = new Dictionary<int, Texture2D>();
		_modBarTexUsed = new Dictionary<int, float>();
		_lastWatermarkFps = -1;
		_inputUpper = "";
		_pcPage = true;
		_dirty = true;
		_tex = new Dictionary<string, Texture2D>(16);
		_roundCache = new Dictionary<long, Texture2D>(64);
		_roundUsed = new Dictionary<long, float>(64);
		_gc = new GUIContent();
		_catsCache = new List<ButtonHandler.Button>(16);
		_contentCache = new List<ButtonHandler.Button>(64);
		_catsKey = int.MinValue;
		_contentPageKey = int.MinValue;
		_contentModKey = int.MinValue;
		_contentMenuKey = int.MinValue;
		_scrollDragId = -1;
		_lastSidebarPage = -1;
		_clickAnim = new Dictionary<int, float>();
		_toggleAnim = new Dictionary<int, float>();
		_hoverAnim = new Dictionary<int, float>();
		_animT = 1f;
		_wasVisible = true;
	}

	private void DrawWatermark()
	{
		_smoothFps = Mathf.Lerp(_smoothFps, 1f / Time.unscaledDeltaTime, Time.unscaledDeltaTime * 4f);
		_fpsUpdateTimer -= Time.unscaledDeltaTime;
		if (_fpsUpdateTimer <= 0f)
		{
			_displayedFps = Mathf.RoundToInt(_smoothFps);
			_fpsUpdateTimer = 0.5f;
			if (_displayedFps != _lastWatermarkFps)
			{
				goto Branch_00c3;
			}
		}
		else if (_displayedFps != _lastWatermarkFps)
		{
			goto Branch_00c3;
		}
		float cachedWatermarkW = _cachedWatermarkW;
		float num = (float)originalWidth / 4f - cachedWatermarkW / 2f;
		if (!((Object)(object)_watermarkTex == (Object)null))
		{
			goto Branch_0195;
		}
		Branch_01cf:
		if ((Object)(object)_watermarkTex != (Object)null)
		{
			Object.Destroy((Object)(object)_watermarkTex);
			_watermarkTex = CreateRoundedTexture((int)cachedWatermarkW, 26, new Color32((byte)0, (byte)0, (byte)0, (byte)140), default(Color32), 0, 8);
			GUI.color = Color.white;
			GUI.DrawTexture(new Rect(num, 8f, cachedWatermarkW, 26f), (Texture)(object)_watermarkTex);
			_sWatermark.normal.textColor = GetAnimatedGradientColor();
			GUI.Label(new Rect(num + 10f, 8f, cachedWatermarkW, 26f), _cachedWatermarkText, _sWatermark);
			_sWatermark.normal.textColor = Color.white;
		}
		else
		{
			_watermarkTex = CreateRoundedTexture((int)cachedWatermarkW, 26, new Color32((byte)0, (byte)0, (byte)0, (byte)140), default(Color32), 0, 8);
			GUI.color = Color.white;
			GUI.DrawTexture(new Rect(num, 8f, cachedWatermarkW, 26f), (Texture)(object)_watermarkTex);
			_sWatermark.normal.textColor = GetAnimatedGradientColor();
			GUI.Label(new Rect(num + 10f, 8f, cachedWatermarkW, 26f), _cachedWatermarkText, _sWatermark);
			_sWatermark.normal.textColor = Color.white;
		}
		return;
		Branch_0195:
		if (((Texture)_watermarkTex).width != (int)cachedWatermarkW)
		{
			goto Branch_01cf;
		}
		GUI.color = Color.white;
		GUI.DrawTexture(new Rect(num, 8f, cachedWatermarkW, 26f), (Texture)(object)_watermarkTex);
		_sWatermark.normal.textColor = GetAnimatedGradientColor();
		GUI.Label(new Rect(num + 10f, 8f, cachedWatermarkW, 26f), _cachedWatermarkText, _sWatermark);
		_sWatermark.normal.textColor = Color.white;
		return;
		Branch_00c3:
		_lastWatermarkFps = _displayedFps;
		_cachedWatermarkText = string.Format("{0} | v{1} | FPS: {2} | (L-Alt) Menu (R-Alt) GUI", "NXO", "6.1", _displayedFps);
		_gc.text = _cachedWatermarkText;
		_cachedWatermarkW = _sWatermark.CalcSize(_gc).x + 20f;
		cachedWatermarkW = _cachedWatermarkW;
		num = (float)originalWidth / 4f - cachedWatermarkW / 2f;
		if (!((Object)(object)_watermarkTex == (Object)null))
		{
			goto Branch_0195;
		}
		goto Branch_01cf;
	}

	private void LateUpdate()
	{
		UpdateMouseClicker();
	}

	private void DrawSidebar(float x, float y, float w, float h)
	{
		DrawRound(new Rect(x, y, w, h), 11, new Color32((byte)15, (byte)15, (byte)15, (byte)238), new Color32((byte)64, (byte)64, (byte)64, (byte)135));
		List<ButtonHandler.Button> sidebarCats = GetSidebarCats();
		float num = x + 7f;
		float num2 = y + 8f;
		float num3 = w - 20f;
		float num4 = h - 16f;
		float num5 = 26f;
		float num6 = (float)(sidebarCats.Count + 1) * num5;
		float num7 = Mathf.Max(0f, num6 - num4);
		Rect val;
		if (!_pcPage && _lastSidebarPage != (int)Variables.currentPage)
		{
			_lastSidebarPage = (int)Variables.currentPage;
			int num8 = -1;
			int num9 = 0;
			if (num9 < sidebarCats.Count)
			{
				do
				{
					if (IsSidebarActive(sidebarCats[num9]))
					{
						num8 = num9 + 1;
						break;
					}
					num9++;
				}
				while (num9 < sidebarCats.Count);
			}
			if (num8 >= 0)
			{
				float num10 = (float)num8 * num5;
				if (num10 < _categoryScroll)
				{
					_categoryScroll = num10;
				}
				else if (num10 + num5 > _categoryScroll + num4)
				{
					_categoryScroll = num10 + num5 - num4;
					val = new Rect(x, y, w, h);
					if (!((Rect)val).Contains(Event.current.mousePosition))
					{
						goto Branch_0347;
					}
					goto Branch_0294;
				}
				val = new Rect(x, y, w, h);
				if (((Rect)val).Contains(Event.current.mousePosition))
				{
					goto Branch_0294;
				}
			}
			else
			{
				val = new Rect(x, y, w, h);
				if (((Rect)val).Contains(Event.current.mousePosition))
				{
					goto Branch_0294;
				}
			}
		}
		else
		{
			val = new Rect(x, y, w, h);
			if (((Rect)val).Contains(Event.current.mousePosition))
			{
				goto Branch_0294;
			}
		}
		goto Branch_0347;
		Branch_0397:
		List<ButtonHandler.Button>.Enumerator enumerator;
		float y2;
		try
		{
			if (enumerator.MoveNext())
			{
				do
				{
					ButtonHandler.Button current = enumerator.Current;
					DrawSidebarButton(0f, ref y2, num3, current);
				}
				while (enumerator.MoveNext());
			}
		}
		finally
		{
			((IDisposable)enumerator/*cast due to constrained. prefix*/).Dispose();
		}
		GUI.EndGroup();
		if (num7 > 0f)
		{
			_categoryScroll = DrawScrollBar(1, x + w - 10f, num2, 5f, num4, _categoryScroll, num7);
		}
		return;
		Branch_0294:
		if ((int)Event.current.type != 6)
		{
			goto Branch_0347;
		}
		_categoryScroll = Mathf.Clamp(_categoryScroll + Event.current.delta.y * 14f, 0f, num7);
		Event.current.Use();
		_categoryScroll = Mathf.Clamp(_categoryScroll, 0f, num7);
		GUI.BeginGroup(new Rect(num, num2, num3, num4));
		y2 = 0f - _categoryScroll;
		DrawPcSidebarButton(0f, ref y2, num3);
		enumerator = sidebarCats.GetEnumerator();
		goto Branch_0397;
		Branch_0347:
		_categoryScroll = Mathf.Clamp(_categoryScroll, 0f, num7);
		GUI.BeginGroup(new Rect(num, num2, num3, num4));
		y2 = 0f - _categoryScroll;
		DrawPcSidebarButton(0f, ref y2, num3);
		enumerator = sidebarCats.GetEnumerator();
		goto Branch_0397;
	}

	private void DrawHoverGlow(Rect r, int radius, float alpha)
	{
		if (!(alpha <= 0.004f))
		{
			Color color = GUI.color;
			GUI.color = new Color(1f, 1f, 1f, alpha);
			GUI.DrawTexture(r, (Texture)(object)RoundTex(Mathf.CeilToInt(((Rect)r).width), Mathf.CeilToInt(((Rect)r).height), radius, new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue), default(Color32), 0));
			GUI.color = color;
		}
	}

	[CompilerGenerated]
	private static void MixHashValue(int v, ref CapturedVariables1190 P_1)
	{
		P_1.key ^= v;
		P_1.key *= 1099511628211L;
	}

	private bool RowButton(Rect r, string label, int id)
	{
		bool hovered = ((Rect)r).Contains(Event.current.mousePosition);
		float num = ClickScale(id);
		Matrix4x4 matrix = GUI.matrix;
		bool clicked;
		if (num < 1f)
		{
			GUIUtility.ScaleAroundPivot(new Vector2(num, num), ((Rect)r).center);
			DrawRound(r, 8, new Color32((byte)24, (byte)24, (byte)24, (byte)242), new Color32((byte)62, (byte)62, (byte)62, (byte)130));
			DrawHoverGlow(r, 8, HoverT(id, hovered) * 0.06f);
			_sCardTitle.normal.textColor = Color.white;
			GUI.Label(new Rect(((Rect)r).x + 12f, ((Rect)r).y, ((Rect)r).width - 24f, ((Rect)r).height), label, _sCardTitle);
			if (num < 1f)
			{
				goto Branch_01b9;
			}
		}
		else
		{
			DrawRound(r, 8, new Color32((byte)24, (byte)24, (byte)24, (byte)242), new Color32((byte)62, (byte)62, (byte)62, (byte)130));
			DrawHoverGlow(r, 8, HoverT(id, hovered) * 0.06f);
			_sCardTitle.normal.textColor = Color.white;
			GUI.Label(new Rect(((Rect)r).x + 12f, ((Rect)r).y, ((Rect)r).width - 24f, ((Rect)r).height), label, _sCardTitle);
			if (num < 1f)
			{
				goto Branch_01b9;
			}
		}
		clicked = GUI.Button(r, GUIContent.none, GUIStyle.none);
		GUI.matrix = matrix;
		if (clicked)
		{
			_clickAnim[id] = Time.unscaledTime;
		}
		return clicked;
		Branch_01b9:
		GUI.matrix = matrix;
		clicked = GUI.Button(r, GUIContent.none, GUIStyle.none);
		if (clicked)
		{
			_clickAnim[id] = Time.unscaledTime;
		}
		return clicked;
	}

	private void Awake()
	{
		_font = Resources.GetBuiltinResource<Font>("Arial.ttf");
		_pixel = new Texture2D(1, 1, (TextureFormat)4, false)
		{
			filterMode = (FilterMode)0
		};
		_pixel.SetPixels32((Color32[])(object)new Color32[1]
		{
			new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue)
		});
		_pixel.Apply(false, true);
		ButtonHandler.Button[] array;
		int num;
		if (!CapturedVariables1190_State_02)
		{
			Resolution currentResolution = Screen.currentResolution;
			Resolution val = currentResolution;
			originalWidth = ((Resolution)val).width * 2;
			currentResolution = Screen.currentResolution;
			val = currentResolution;
			originalHeight = ((Resolution)val).height * 2;
			CapturedVariables1190_State_02 = true;
			array = ModButtons.buttons;
			num = 0;
		}
		else
		{
			array = ModButtons.buttons;
			num = 0;
		}
		while (num < array.Length)
		{
			ButtonHandler.Button button = array[num];
			if (array[num].Enabled && button.isToggle && !CapturedVariables1190_Set_01.Contains(button.buttonText))
			{
				TrackModEnabled(button.buttonText);
				num++;
			}
			else
			{
				num++;
			}
		}
		_win = new Rect((float)originalWidth / 2f - CapturedVariables1190_Value_02 - 8f, 8f, CapturedVariables1190_Value_02, CapturedVariables1190_Value_01);
	}

	private List<ButtonHandler.Button> SearchButtons(string q)
	{
		string query = q.Trim();
		return (from b in ModButtons.buttons
			where b != null && b.Page != Category.Home && Settings.IsElementSettingVisible(b.buttonText)
			select new
			{
				Button = b,
				Score = SearchAndKeyboard.CalculateSearchScore(StripRichText(b.buttonText), query)
			} into x
			where x.Score > 0 || StripRichText(x.Button.buttonText).IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0
			orderby x.Score descending, StripRichText(x.Button.buttonText)
			select x.Button).ToList();
	}

	private void DrawRound(Rect r, int radius, Color32 fill, Color32 border, int borderWidth = 1)
	{
		Color color = GUI.color;
		GUI.color = Color.white;
		GUI.DrawTexture(r, (Texture)(object)RoundTex(Mathf.CeilToInt(((Rect)r).width), Mathf.CeilToInt(((Rect)r).height), radius, fill, border, borderWidth));
		GUI.color = color;
	}

	private List<ButtonHandler.Button> GetContent(bool searching)
	{
		if (_contentPageKey != (int)Variables.currentPage || _contentSearchKey != _inputUpper || _contentModKey != CapturedVariables1190_Index_01 || _contentMenuKey != CapturedVariables1190_Index_02)
		{
			_contentPageKey = (int)Variables.currentPage;
			_contentSearchKey = _inputUpper;
			_contentModKey = CapturedVariables1190_Index_01;
			_contentMenuKey = CapturedVariables1190_Index_02;
			_contentCache.Clear();
			if (searching)
			{
				_contentCache.AddRange(SearchButtons(_inputUpper));
				return _contentCache;
			}
			List<ButtonHandler.Button> list = ButtonHandler.GetButtonsForPage(Variables.currentPage);
			int num = 0;
			if (num < list.Count)
			{
				while (true)
				{
					ButtonHandler.Button button = list[num];
					if (button != null && Settings.IsElementSettingVisible(button.buttonText))
					{
						_contentCache.Add(button);
						num++;
						if (num >= list.Count)
						{
							break;
						}
					}
					else
					{
						num++;
						if (num >= list.Count)
						{
							break;
						}
					}
				}
			}
			return _contentCache;
		}
		return _contentCache;
	}

	private bool RowToggle(Rect r, string label, bool on, int id)
	{
		bool hovered = ((Rect)r).Contains(Event.current.mousePosition);
		float num;
		float value;
		if (!on)
		{
			num = 0f;
			if (!_toggleAnim.TryGetValue(id, out value))
			{
				goto Branch_007a;
			}
		}
		else
		{
			num = 1f;
			if (!_toggleAnim.TryGetValue(id, out value))
			{
				goto Branch_007a;
			}
		}
		if ((int)Event.current.type != 7)
		{
			goto Branch_030a;
		}
		goto Branch_00be;
		Branch_030a:
		float num2 = Mathf.SmoothStep(0f, 1f, value);
		bool flag = num2 >= 0.5f;
		DrawRound(r, 8, flag ? new Color32((byte)40, (byte)40, (byte)40, (byte)248) : new Color32((byte)20, (byte)20, (byte)20, (byte)242), flag ? new Color32((byte)120, (byte)120, (byte)120, (byte)180) : new Color32((byte)62, (byte)62, (byte)62, (byte)120));
		DrawHoverGlow(r, 8, HoverT(id, hovered) * 0.06f);
		_sCardTitle.normal.textColor = Color.white;
		GUI.Label(new Rect(((Rect)r).x + 12f, ((Rect)r).y, ((Rect)r).width - 80f, ((Rect)r).height), label, _sCardTitle);
		float num3 = ((Rect)r).x + ((Rect)r).width - 58f;
		float num4 = ((Rect)r).y + (((Rect)r).height - 20f) * 0.5f;
		DrawRound(new Rect(num3, num4, 46f, 20f), 10, flag ? new Color32((byte)70, (byte)70, (byte)70, (byte)250) : new Color32((byte)35, (byte)35, (byte)35, (byte)245), flag ? new Color32((byte)180, (byte)180, (byte)180, (byte)220) : new Color32((byte)92, (byte)92, (byte)92, (byte)190));
		float num5 = Mathf.Lerp(num3 + 3f, num3 + 29f, num2);
		DrawRound(new Rect(num5, num4 + 3f, 14f, 14f), 7, new Color32((byte)235, (byte)235, (byte)235, byte.MaxValue), new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, (byte)200));
		return GUI.Button(r, GUIContent.none, GUIStyle.none);
		Branch_00be:
		value = Mathf.MoveTowards(value, num, Time.unscaledDeltaTime * 8f);
		_toggleAnim[id] = value;
		num2 = Mathf.SmoothStep(0f, 1f, value);
		flag = num2 >= 0.5f;
		DrawRound(r, 8, flag ? new Color32((byte)40, (byte)40, (byte)40, (byte)248) : new Color32((byte)20, (byte)20, (byte)20, (byte)242), flag ? new Color32((byte)120, (byte)120, (byte)120, (byte)180) : new Color32((byte)62, (byte)62, (byte)62, (byte)120));
		DrawHoverGlow(r, 8, HoverT(id, hovered) * 0.06f);
		_sCardTitle.normal.textColor = Color.white;
		GUI.Label(new Rect(((Rect)r).x + 12f, ((Rect)r).y, ((Rect)r).width - 80f, ((Rect)r).height), label, _sCardTitle);
		num3 = ((Rect)r).x + ((Rect)r).width - 58f;
		num4 = ((Rect)r).y + (((Rect)r).height - 20f) * 0.5f;
		DrawRound(new Rect(num3, num4, 46f, 20f), 10, flag ? new Color32((byte)70, (byte)70, (byte)70, (byte)250) : new Color32((byte)35, (byte)35, (byte)35, (byte)245), flag ? new Color32((byte)180, (byte)180, (byte)180, (byte)220) : new Color32((byte)92, (byte)92, (byte)92, (byte)190));
		num5 = Mathf.Lerp(num3 + 3f, num3 + 29f, num2);
		DrawRound(new Rect(num5, num4 + 3f, 14f, 14f), 7, new Color32((byte)235, (byte)235, (byte)235, byte.MaxValue), new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, (byte)200));
		return GUI.Button(r, GUIContent.none, GUIStyle.none);
		Branch_007a:
		value = num;
		if ((int)Event.current.type != 7)
		{
			goto Branch_030a;
		}
		goto Branch_00be;
	}

	private Texture2D RoundTex(int w, int h, int r, Color32 fill, Color32 border, int bw)
	{
		int num = Mathf.Max(1, w);
		w = num;
		int num2 = Mathf.Max(1, h);
		h = num2;
		int num3 = Mathf.Clamp(r, 0, Mathf.Min(w, h) / 2);
		r = num3;
		long key = RoundKey(w, h, r, bw, fill, border);
		if (_roundCache.TryGetValue(key, out Texture2D value) && (Object)(object)value != (Object)null)
		{
			_roundUsed[key] = Time.unscaledTime;
			return value;
		}
		Texture2D val;
		if (_roundCache.Count >= 256)
		{
			long key2 = 0L;
			float num4 = float.MaxValue;
			using (Dictionary<long, float>.Enumerator enumerator = _roundUsed.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					while (true)
					{
						KeyValuePair<long, float> current = enumerator.Current;
						if (!(current.Value < num4))
						{
							break;
						}
						num4 = current.Value;
						key2 = current.Key;
						if (!enumerator.MoveNext())
						{
							goto EndBranch_0186;
						}
					}
					continue;
					EndBranch_0186:
					break;
				}
			}
			if (_roundCache.TryGetValue(key2, out Texture2D value2) && (Object)(object)value2 != (Object)null)
			{
				Object.Destroy((Object)(object)value2);
				_roundCache.Remove(key2);
				_roundUsed.Remove(key2);
				val = CreateRoundedTexture(w, h, fill, border, bw, r);
				_roundCache[key] = val;
				_roundUsed[key] = Time.unscaledTime;
				return val;
			}
			_roundCache.Remove(key2);
			_roundUsed.Remove(key2);
			val = CreateRoundedTexture(w, h, fill, border, bw, r);
			_roundCache[key] = val;
			_roundUsed[key] = Time.unscaledTime;
			return val;
		}
		val = CreateRoundedTexture(w, h, fill, border, bw, r);
		_roundCache[key] = val;
		_roundUsed[key] = Time.unscaledTime;
		return val;
	}

	private void PcAction(ref float y, float w, string label, Action click)
	{
		if (RowButton(new Rect(0f, y, w, 30f), label, label.GetHashCode() * 31 + 7))
		{
			ButtonHandler.PlayClickSound();
			click();
			y += 36f;
		}
		else
		{
			y += 36f;
		}
	}

	public static void TrackModEnabled(string t)
	{
		if (!CapturedVariables1190_Set_01.Contains(t))
		{
			CapturedVariables1190_Items_01.Add(t);
			CapturedVariables1190_Set_01.Add(t);
			CapturedVariables1190_Lookup_03[t] = Time.unscaledTime;
			if (CapturedVariables1190_Items_01.Count > 100)
			{
				CapturedVariables1190_Items_01.RemoveAt(0);
				CapturedVariables1190_Index_01++;
			}
			else
			{
				CapturedVariables1190_Index_01++;
			}
		}
	}

	private static void SetStyleTextColor(GUIStyle s, Color c)
	{
		GUIStyleState normal = s.normal;
		GUIStyleState active = s.active;
		GUIStyleState hover = s.hover;
		Color val = (s.focused.textColor = c);
		Color val3 = (hover.textColor = val);
		Color textColor = (active.textColor = val3);
		normal.textColor = textColor;
	}

	private static Texture2D CreateRoundedTexture(int w, int h, Color32 fill, Color32 bord, int bw, int r)
	{
		int num = Mathf.Max(1, w);
		w = num;
		int num2 = Mathf.Max(1, h);
		h = num2;
		int num3 = Mathf.Clamp(r, 0, Mathf.Min(w, h) / 2);
		r = num3;
		int num4 = Mathf.Max(0, bw);
		bw = num4;
		Texture2D val = new Texture2D(w, h, (TextureFormat)4, false)
		{
			filterMode = (FilterMode)1,
			wrapMode = (TextureWrapMode)1
		};
		Color32[] array = (Color32[])(object)new Color32[w * h];
		float num5 = r;
		float num6 = 1.25f;
		int num7 = 0;
		if (num7 < h)
		{
			do
			{
				int num8 = 0;
				if (num8 < w)
				{
					while (true)
					{
						float num9 = (float)num8 + 0.5f;
						float num10 = (float)num7 + 0.5f;
						float num11 = Mathf.Abs(num9 - (float)w * 0.5f) - ((float)w * 0.5f - num5);
						float num12 = Mathf.Abs(num10 - (float)h * 0.5f) - ((float)h * 0.5f - num5);
						float num13 = Mathf.Max(num11, 0f);
						float num14 = Mathf.Max(num12, 0f);
						float num15 = Mathf.Sqrt(num13 * num13 + num14 * num14) + Mathf.Min(Mathf.Max(num11, num12), 0f) - num5;
						float num16 = Mathf.Clamp01(0.5f - num15 / num6);
						Color val2 = (Color32)(fill);
						if (bw > 0)
						{
							float num17 = Mathf.Max(0f, num5 - (float)bw);
							float num18 = Mathf.Abs(num9 - (float)w * 0.5f) - ((float)w * 0.5f - num5 + (float)bw);
							float num19 = Mathf.Abs(num10 - (float)h * 0.5f) - ((float)h * 0.5f - num5 + (float)bw);
							float num20 = Mathf.Max(num18, 0f);
							float num21 = Mathf.Max(num19, 0f);
							float num22 = Mathf.Sqrt(num20 * num20 + num21 * num21) + Mathf.Min(Mathf.Max(num18, num19), 0f) - num17;
							float num23 = Mathf.Clamp01(0.5f - num22 / num6);
							val2 = Color.Lerp((Color32)(bord), (Color32)(fill), num23);
							val2.a *= num16;
							array[num7 * w + num8] = (Color32)(val2);
							num8++;
							if (num8 >= w)
							{
								break;
							}
						}
						else
						{
							val2.a *= num16;
							array[num7 * w + num8] = (Color32)(val2);
							num8++;
							if (num8 >= w)
							{
								break;
							}
						}
					}
				}
				num7++;
			}
			while (num7 < h);
		}
		val.SetPixels32(array);
		val.Apply(false, true);
		return val;
	}

	private void Sep(float x, float y, float w)
	{
		GUI.color = (Color32)(new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, (byte)20));
		GUI.DrawTexture(new Rect(x, y, w, 1f), (Texture)(object)_pixel);
		GUI.color = Color.white;
	}

	private static void RestoreRightHandCollider()
	{
		if (CapturedVariables1190_Position_01.HasValue)
		{
			Variables.Variables_Reference_09.rightHandTriggerCollider.transform.position = CapturedVariables1190_Position_01.Value;
			CapturedVariables1190_Position_01 = null;
		}
	}

	private float HoverT(int id, bool hovered)
	{
		_hoverAnim.TryGetValue(id, out var value);
		if ((int)Event.current.type == 7)
		{
			value = Mathf.MoveTowards(value, hovered ? 1f : 0f, Time.unscaledDeltaTime * 10f);
			if (value <= 0f && !hovered)
			{
				_hoverAnim.Remove(id);
				return value;
			}
			_hoverAnim[id] = value;
			return value;
		}
		return value;
	}

	private bool FavoriteModsSafeContains(ButtonHandler.Button b)
	{
		try
		{
			if (ButtonHandler.CapturedVariables570_Items_02 != null)
			{
				return ButtonHandler.CapturedVariables570_Items_02.Contains(b);
			}
			return false;
		}
		catch (Exception)
		{
			return false;
		}
	}

	public static void UpdateMouseClicker()
	{
		RaycastHit val2 = default(RaycastHit);
		if (CapturedVariables1190_State_05)
		{
			TrackModEnabled("Button Clicker");
			if (Mouse.current.leftButton.isPressed)
			{
				if (!((Object)(object)Main.CapturedVariables1950_Reference_07 != (Object)null))
				{
					Ray val = (Main.CapturedVariables1950_Reference_07 = Variables.Variables_Object_13.GetComponent<Camera>()).ScreenPointToRay((Vector2)(((InputControl<Vector2>)(object)((Pointer)Mouse.current).position).ReadValue()));
					Physics.Raycast(val, out val2, 512f, Variables.GetInteractionLayerMask());
					CapturedVariables1190_Position_01.GetValueOrDefault();
					if (!CapturedVariables1190_Position_01.HasValue)
					{
						goto Branch_0117;
					}
				}
				else
				{
					Ray val = Main.CapturedVariables1950_Reference_07.ScreenPointToRay((Vector2)(((InputControl<Vector2>)(object)((Pointer)Mouse.current).position).ReadValue()));
					Physics.Raycast(val, out val2, 512f, Variables.GetInteractionLayerMask());
					CapturedVariables1190_Position_01.GetValueOrDefault();
					if (!CapturedVariables1190_Position_01.HasValue)
					{
						goto Branch_0117;
					}
				}
				goto Branch_013f;
			}
			RestoreRightHandCollider();
			return;
		}
		RestoreRightHandCollider();
		return;
		Branch_0117:
		CapturedVariables1190_Position_01 = Variables.Variables_Reference_09.rightHandTriggerCollider.transform.localPosition;
		Branch_013f:
		Variables.Variables_Reference_09.rightHandTriggerCollider.transform.position = ((RaycastHit)val2).point;
	}

	private void DrawSearch(float x, float y, float w)
	{
		DrawRound(new Rect(x, y, w, 26f), 7, new Color32((byte)18, (byte)18, (byte)18, (byte)245), new Color32((byte)74, (byte)74, (byte)74, (byte)170));
		string text;
		if (string.IsNullOrEmpty(_inputUpper))
		{
			float num = w - 24f;
			text = GUI.TextField(new Rect(x + 12f, y + 3f, num, 20f), _inputUpper, _sField).ToUpperInvariant();
			if (text != _inputUpper)
			{
				goto Branch_0102;
			}
		}
		else
		{
			float num = w - 44f;
			text = GUI.TextField(new Rect(x + 12f, y + 3f, num, 20f), _inputUpper, _sField).ToUpperInvariant();
			if (text != _inputUpper)
			{
				goto Branch_0102;
			}
		}
		if (!string.IsNullOrWhiteSpace(_inputUpper))
		{
			goto Branch_01ee;
		}
		Branch_01a0:
		GUI.Label(new Rect(x + 12f, y + 3f, w - 24f, 20f), _pcPage ? "Room code / name..." : "Search mods...", _sDim);
		return;
		Branch_01ee:
		if (MiniButton(x + w - 26f, y + 4f, 18f, 18f, "×", 387660))
		{
			_inputUpper = "";
			Variables.Variables_Index_04 = 0;
		}
		return;
		Branch_0102:
		_inputUpper = text;
		Variables.Variables_Index_04 = 0;
		GorillaTagger obj = Variables.Variables_Reference_09;
		if (obj != null)
		{
			VRRig offlineVRRig = obj.offlineVRRig;
			if (offlineVRRig != null)
			{
				offlineVRRig.PlayHandTapLocal(66, false, 1f);
				if (string.IsNullOrWhiteSpace(_inputUpper))
				{
					goto Branch_01a0;
				}
				goto Branch_01ee;
			}
		}
		if (!string.IsNullOrWhiteSpace(_inputUpper))
		{
			goto Branch_01ee;
		}
		goto Branch_01a0;
	}

	private float DrawScrollBar(int id, float x, float y, float w, float h, float scroll, float maxScroll)
	{
		DrawRound(new Rect(x, y, w, h), 3, new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, (byte)22), new Color32((byte)0, (byte)0, (byte)0, (byte)0), 0);
		float num = Mathf.Clamp(h * (h / (h + maxScroll)), 28f, h);
		float num2 = h - num;
		float num3 = y + num2 * (scroll / Mathf.Max(1f, maxScroll));
		Event current = Event.current;
		Rect val = default(Rect);
		val = new Rect(x - 3f, num3, w + 6f, num);
		bool flag;
		if ((int)current.type == 0 && ((Rect)val).Contains(current.mousePosition))
		{
			_scrollDragId = id;
			_scrollDragOffset = current.mousePosition.y - num3;
			current.Use();
		}
		else if (_scrollDragId == id)
		{
			if ((int)current.type == 3 && num2 > 0f)
			{
				float num4 = Mathf.Clamp01((current.mousePosition.y - _scrollDragOffset - y) / num2) * maxScroll;
				scroll = num4;
				num3 = y + num2 * (scroll / Mathf.Max(1f, maxScroll));
				current.Use();
			}
			else if ((int)current.type == 1)
			{
				_scrollDragId = -1;
				current.Use();
				flag = _scrollDragId == id;
				DrawRound(new Rect(x, num3, w, num), 3, flag ? new Color32((byte)240, (byte)240, (byte)240, (byte)220) : new Color32((byte)215, (byte)215, (byte)215, (byte)175), new Color32((byte)0, (byte)0, (byte)0, (byte)0), 0);
				return scroll;
			}
			flag = _scrollDragId == id;
			DrawRound(new Rect(x, num3, w, num), 3, flag ? new Color32((byte)240, (byte)240, (byte)240, (byte)220) : new Color32((byte)215, (byte)215, (byte)215, (byte)175), new Color32((byte)0, (byte)0, (byte)0, (byte)0), 0);
			return scroll;
		}
		flag = _scrollDragId == id;
		DrawRound(new Rect(x, num3, w, num), 3, flag ? new Color32((byte)240, (byte)240, (byte)240, (byte)220) : new Color32((byte)215, (byte)215, (byte)215, (byte)175), new Color32((byte)0, (byte)0, (byte)0, (byte)0), 0);
		return scroll;
	}

	private void DrawContent(float x, float y, float w, float h)
	{
		if (_pcPage)
		{
			DrawPcContent(x, y, w, h);
			return;
		}
		DrawRound(new Rect(x, y, w, h), 11, new Color32((byte)15, (byte)15, (byte)15, (byte)238), new Color32((byte)64, (byte)64, (byte)64, (byte)135));
		bool flag = !string.IsNullOrWhiteSpace(_inputUpper);
		List<ButtonHandler.Button> list = GetContent(flag);
		int count = list.Count;
		float num = y + 10f;
		float num2;
		float num3;
		float num4;
		float num5;
		if (!flag)
		{
			string text = StripRichText(Variables.currentPage.ToString().Replace("_", " "));
			_sCardTitle.normal.textColor = Color.white;
			GUI.Label(new Rect(x + 12f, num, w - 24f, 19f), text, _sCardTitle);
			_sDim.alignment = (TextAnchor)5;
			GUI.Label(new Rect(x + 12f, num, w - 24f, 19f), $"{count} items", _sDim);
			_sDim.alignment = (TextAnchor)3;
			Sep(x + 12f, num + 25f, w - 24f);
			num2 = x + 12f;
			num3 = num + 36f;
			num4 = w - 31f;
			num5 = h - 48f;
			if (count != 0)
			{
				goto Branch_028a;
			}
		}
		else
		{
			string text = "Search: " + _inputUpper;
			_sCardTitle.normal.textColor = Color.white;
			GUI.Label(new Rect(x + 12f, num, w - 24f, 19f), text, _sCardTitle);
			_sDim.alignment = (TextAnchor)5;
			GUI.Label(new Rect(x + 12f, num, w - 24f, 19f), $"{count} items", _sDim);
			_sDim.alignment = (TextAnchor)3;
			Sep(x + 12f, num + 25f, w - 24f);
			num2 = x + 12f;
			num3 = num + 36f;
			num4 = w - 31f;
			num5 = h - 48f;
			if (count != 0)
			{
				goto Branch_028a;
			}
		}
		float num6 = 40f;
		float num7 = Mathf.Max(0f, num6 - num5);
		Rect val = new Rect(x, y, w, h);
		if (!((Rect)val).Contains(Event.current.mousePosition))
		{
			goto Branch_03d3;
		}
		goto Branch_0325;
		Branch_0581:
		_contentScroll = DrawScrollBar(2, x + w - 12f, num3, 5f, num5, _contentScroll, num7);
		return;
		Branch_0325:
		if ((int)Event.current.type != 6)
		{
			goto Branch_03d3;
		}
		_contentScroll = Mathf.Clamp(_contentScroll + Event.current.delta.y * 22f, 0f, num7);
		Event.current.Use();
		_contentScroll = Mathf.Clamp(_contentScroll, 0f, num7);
		GUI.BeginGroup(new Rect(num2, num3, num4, num5));
		float y2 = 0f - _contentScroll;
		if (count != 0)
		{
			goto Branch_045e;
		}
		goto Branch_041e;
		Branch_045e:
		_btnIndex = 0;
		int num8 = 0;
		if (num8 < count)
		{
			while (!(y2 > num5))
			{
				if (y2 + 30f >= 0f)
				{
					DrawModCard(0f, ref y2, num4, list[num8]);
					num8++;
					if (num8 >= count)
					{
						break;
					}
					continue;
				}
				y2 += 36f;
				_btnIndex++;
				num8++;
				if (num8 >= count)
				{
					break;
				}
			}
		}
		GUI.EndGroup();
		if (!(num7 > 0f))
		{
			return;
		}
		goto Branch_0581;
		Branch_041e:
		GUI.Label(new Rect(0f, 8f, num4, 24f), flag ? "No mods matched your search." : "Nothing in this category.", _sDim);
		GUI.EndGroup();
		if (!(num7 > 0f))
		{
			return;
		}
		goto Branch_0581;
		Branch_028a:
		num6 = (float)count * 36f - 6f;
		num7 = Mathf.Max(0f, num6 - num5);
		val = new Rect(x, y, w, h);
		if (!((Rect)val).Contains(Event.current.mousePosition))
		{
			goto Branch_03d3;
		}
		goto Branch_0325;
		Branch_03d3:
		_contentScroll = Mathf.Clamp(_contentScroll, 0f, num7);
		GUI.BeginGroup(new Rect(num2, num3, num4, num5));
		y2 = 0f - _contentScroll;
		if (count != 0)
		{
			goto Branch_045e;
		}
		goto Branch_041e;
	}

	private void DrawPcContent(float x, float y, float w, float h)
	{
		DrawRound(new Rect(x, y, w, h), 11, new Color32((byte)15, (byte)15, (byte)15, (byte)238), new Color32((byte)64, (byte)64, (byte)64, (byte)135));
		float num = y + 10f;
		_sCardTitle.normal.textColor = Color.white;
		GUI.Label(new Rect(x + 12f, num, w - 24f, 19f), "PC", _sCardTitle);
		Sep(x + 12f, num + 25f, w - 24f);
		float num2 = x + 12f;
		float num3 = num + 36f;
		float num4 = w - 31f;
		float num5 = h - 48f;
		float num6 = Mathf.Max(0f, 354f - num5);
		Rect val = new Rect(x, y, w, h);
		if (((Rect)val).Contains(Event.current.mousePosition) && (int)Event.current.type == 6)
		{
			_contentScroll = Mathf.Clamp(_contentScroll + Event.current.delta.y * 22f, 0f, num6);
			Event.current.Use();
			_contentScroll = Mathf.Clamp(_contentScroll, 0f, num6);
			GUI.BeginGroup(new Rect(num2, num3, num4, num5));
			float y2 = 0f - _contentScroll;
			PcAction(ref y2, num4, "Join Specific Room", delegate
			{
				((PhotonNetworkController)PhotonNetworkController.Instance).AttemptToJoinSpecificRoom(_inputUpper, (JoinType)0);
			});
			PcAction(ref y2, num4, "Join Random Room", delegate
			{
				Room.JoinRandomPublic();
			});
			PcAction(ref y2, num4, "Change Name", delegate
			{
				RigManager.SetPlayerName(_inputUpper);
			});
			PcAction(ref y2, num4, "Disconnect", delegate
			{
				NetworkSystem.Instance.ReturnToSinglePlayer();
			});
			PcAction(ref y2, num4, "Reconnect", delegate
			{
				Room.Reconnect();
			});
			PcToggle(ref y2, num4, "Freecam", CapturedVariables1190_State_01, delegate
			{
				CapturedVariables1190_State_01 = !CapturedVariables1190_State_01;
				if (!CapturedVariables1190_State_01)
				{
					TrackModDisabled("Freecam");
				}
			});
			PcToggle(ref y2, num4, "Noclip", Movement.Movement_State_05, delegate
			{
				Movement.Movement_State_05 = !Movement.Movement_State_05;
				MeshCollider[] array = Object.FindObjectsOfType<MeshCollider>();
				for (int i = 0; i < array.Length; i++)
				{
					((Collider)array[i]).enabled = !Movement.Movement_State_05;
				}
				if (Movement.Movement_State_05)
				{
					TrackModEnabled("Noclip");
				}
				else
				{
					TrackModDisabled("Noclip");
				}
			});
			PcToggle(ref y2, num4, "First Person", CapturedVariables1190_State_03, delegate
			{
				CapturedVariables1190_State_03 = !CapturedVariables1190_State_03;
				if (CapturedVariables1190_State_03)
				{
					TrackModEnabled("First Person");
					Visuals.SetFirstPersonCamEnabled(enable: true);
				}
				else
				{
					TrackModDisabled("First Person");
					Visuals.SetFirstPersonCamEnabled(enable: false);
				}
			});
			PcToggle(ref y2, num4, "Button Clicker", CapturedVariables1190_State_05, delegate
			{
				CapturedVariables1190_State_05 = !CapturedVariables1190_State_05;
				if (!CapturedVariables1190_State_05)
				{
					TrackModDisabled("Button Clicker");
				}
			});
			PcToggle(ref y2, num4, "Controller Emulator", ControllerEmulator.ControllerEmulator_State_08, delegate
			{
				ControllerEmulator.ControllerEmulator_State_08 = !ControllerEmulator.ControllerEmulator_State_08;
			});
			GUI.EndGroup();
			if (!(num6 > 0f))
			{
				return;
			}
		}
		else
		{
			_contentScroll = Mathf.Clamp(_contentScroll, 0f, num6);
			GUI.BeginGroup(new Rect(num2, num3, num4, num5));
			float y2 = 0f - _contentScroll;
			PcAction(ref y2, num4, "Join Specific Room", delegate
			{
				((PhotonNetworkController)PhotonNetworkController.Instance).AttemptToJoinSpecificRoom(_inputUpper, (JoinType)0);
			});
			PcAction(ref y2, num4, "Join Random Room", delegate
			{
				Room.JoinRandomPublic();
			});
			PcAction(ref y2, num4, "Change Name", delegate
			{
				RigManager.SetPlayerName(_inputUpper);
			});
			PcAction(ref y2, num4, "Disconnect", delegate
			{
				NetworkSystem.Instance.ReturnToSinglePlayer();
			});
			PcAction(ref y2, num4, "Reconnect", delegate
			{
				Room.Reconnect();
			});
			PcToggle(ref y2, num4, "Freecam", CapturedVariables1190_State_01, delegate
			{
				CapturedVariables1190_State_01 = !CapturedVariables1190_State_01;
				if (!CapturedVariables1190_State_01)
				{
					TrackModDisabled("Freecam");
				}
			});
			PcToggle(ref y2, num4, "Noclip", Movement.Movement_State_05, delegate
			{
				Movement.Movement_State_05 = !Movement.Movement_State_05;
				MeshCollider[] array = Object.FindObjectsOfType<MeshCollider>();
				for (int i = 0; i < array.Length; i++)
				{
					((Collider)array[i]).enabled = !Movement.Movement_State_05;
				}
				if (Movement.Movement_State_05)
				{
					TrackModEnabled("Noclip");
				}
				else
				{
					TrackModDisabled("Noclip");
				}
			});
			PcToggle(ref y2, num4, "First Person", CapturedVariables1190_State_03, delegate
			{
				CapturedVariables1190_State_03 = !CapturedVariables1190_State_03;
				if (CapturedVariables1190_State_03)
				{
					TrackModEnabled("First Person");
					Visuals.SetFirstPersonCamEnabled(enable: true);
				}
				else
				{
					TrackModDisabled("First Person");
					Visuals.SetFirstPersonCamEnabled(enable: false);
				}
			});
			PcToggle(ref y2, num4, "Button Clicker", CapturedVariables1190_State_05, delegate
			{
				CapturedVariables1190_State_05 = !CapturedVariables1190_State_05;
				if (!CapturedVariables1190_State_05)
				{
					TrackModDisabled("Button Clicker");
				}
			});
			PcToggle(ref y2, num4, "Controller Emulator", ControllerEmulator.ControllerEmulator_State_08, delegate
			{
				ControllerEmulator.ControllerEmulator_State_08 = !ControllerEmulator.ControllerEmulator_State_08;
			});
			GUI.EndGroup();
			if (!(num6 > 0f))
			{
				return;
			}
		}
		_contentScroll = DrawScrollBar(2, x + w - 12f, num3, 5f, num5, _contentScroll, num6);
	}

	private void BuildStyles()
	{
		GUIStyle val = new GUIStyle
		{
			fontSize = 13,
			font = _font,
			fontStyle = (FontStyle)1,
			alignment = (TextAnchor)3,
			wordWrap = false
		};
		val.normal.textColor = Color.white;
		_sMod = val;
		GUIStyle val2 = new GUIStyle(GUI.skin.textField)
		{
			fontSize = 11,
			font = _font,
			fontStyle = (FontStyle)0,
			alignment = (TextAnchor)3,
			border = new RectOffset(),
			padding = new RectOffset(0, 0, 0, 0),
			margin = new RectOffset()
		};
		val2.normal.background = null;
		val2.hover.background = null;
		val2.active.background = null;
		val2.focused.background = null;
		_sField = val2;
		SetStyleTextColor(_sField, Color.white);
		_sBtn = new GUIStyle(GUI.skin.button)
		{
			fontSize = 10,
			font = _font,
			fontStyle = (FontStyle)1,
			alignment = (TextAnchor)4,
			richText = true
		};
		_sBtnOn = new GUIStyle(_sBtn);
		GUIStyle val3 = new GUIStyle
		{
			fontSize = 16,
			font = _font,
			fontStyle = (FontStyle)1,
			alignment = (TextAnchor)3,
			richText = true
		};
		val3.normal.textColor = Color.white;
		_sTitle = val3;
		GUIStyle val4 = new GUIStyle
		{
			fontSize = 10,
			font = _font,
			alignment = (TextAnchor)3,
			richText = true
		};
		val4.normal.textColor = (Color32)(new Color32((byte)155, (byte)155, (byte)155, byte.MaxValue));
		_sDim = val4;
		GUIStyle val5 = new GUIStyle
		{
			fontSize = 10,
			font = _font,
			alignment = (TextAnchor)3,
			richText = true
		};
		val5.normal.textColor = (Color32)(new Color32((byte)180, (byte)180, (byte)180, byte.MaxValue));
		_sSmall = val5;
		GUIStyle val6 = new GUIStyle
		{
			fontSize = 12,
			font = _font,
			fontStyle = (FontStyle)1,
			alignment = (TextAnchor)3,
			richText = true
		};
		val6.normal.textColor = Color.white;
		_sCardTitle = val6;
		GUIStyle val7 = new GUIStyle
		{
			fontSize = 9,
			font = _font,
			alignment = (TextAnchor)3,
			richText = true,
			clipping = (TextClipping)1
		};
		val7.normal.textColor = (Color32)(new Color32((byte)145, (byte)145, (byte)145, byte.MaxValue));
		_sCardDesc = val7;
		GUIStyle val8 = new GUIStyle
		{
			fontSize = 10,
			font = _font,
			alignment = (TextAnchor)4,
			richText = true
		};
		val8.normal.textColor = (Color32)(new Color32((byte)220, (byte)220, (byte)220, byte.MaxValue));
		_sPill = val8;
		GUIStyle val9 = new GUIStyle
		{
			fontSize = 16,
			font = _font,
			alignment = (TextAnchor)3,
			richText = true
		};
		val9.normal.textColor = Color.white;
		_sWatermark = val9;
	}

	private void DrawSidebarButton(float x, ref float y, float w, ButtonHandler.Button b)
	{
		int num = b.buttonText.GetHashCode() * 17 + 1;
		Rect val = default(Rect);
		val = new Rect(x, y, w, 22f);
		bool flag;
		bool hovered;
		float num2;
		Matrix4x4 matrix;
		if (!_pcPage)
		{
			flag = IsSidebarActive(b);
			hovered = ((Rect)val).Contains(Event.current.mousePosition);
			num2 = ClickScale(num);
			matrix = GUI.matrix;
			if (num2 < 1f)
			{
				goto Branch_00c0;
			}
		}
		else
		{
			flag = false;
			hovered = ((Rect)val).Contains(Event.current.mousePosition);
			num2 = ClickScale(num);
			matrix = GUI.matrix;
			if (num2 < 1f)
			{
				goto Branch_00c0;
			}
		}
		DrawRound(val, 7, flag ? new Color32((byte)62, (byte)62, (byte)62, (byte)245) : new Color32((byte)23, (byte)23, (byte)23, (byte)220), flag ? new Color32((byte)120, (byte)120, (byte)120, (byte)80) : new Color32((byte)0, (byte)0, (byte)0, (byte)0), flag ? 1 : 0);
		DrawHoverGlow(val, 7, HoverT(num, hovered) * 0.07f);
		if (!flag)
		{
			goto Branch_02e9;
		}
		goto Branch_01ed;
		Branch_037b:
		GUI.matrix = matrix;
		if (!GUI.Button(val, GUIContent.none, GUIStyle.none))
		{
			goto Branch_041e;
		}
		goto Branch_03ca;
		Branch_01ed:
		GUI.color = GetAnimatedGradientColor();
		GUI.DrawTexture(new Rect(x + 3f, y + 5f, 2f, 12f), (Texture)(object)RoundTex(2, 12, 1, new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue), default(Color32), 0));
		GUI.color = Color.white;
		_sSmall.normal.textColor = (flag ? Color.white : (Color32)(new Color32((byte)168, (byte)168, (byte)168, byte.MaxValue)));
		GUI.Label(new Rect(x + 11f, y + 1f, w - 21f, 20f), StripRichText(b.buttonText), _sSmall);
		if (!(num2 < 1f))
		{
			goto Branch_03a6;
		}
		goto Branch_037b;
		Branch_00c0:
		GUIUtility.ScaleAroundPivot(new Vector2(num2, num2), ((Rect)val).center);
		DrawRound(val, 7, flag ? new Color32((byte)62, (byte)62, (byte)62, (byte)245) : new Color32((byte)23, (byte)23, (byte)23, (byte)220), flag ? new Color32((byte)120, (byte)120, (byte)120, (byte)80) : new Color32((byte)0, (byte)0, (byte)0, (byte)0), flag ? 1 : 0);
		DrawHoverGlow(val, 7, HoverT(num, hovered) * 0.07f);
		if (!flag)
		{
			goto Branch_02e9;
		}
		goto Branch_01ed;
		Branch_02e9:
		_sSmall.normal.textColor = (flag ? Color.white : (Color32)(new Color32((byte)168, (byte)168, (byte)168, byte.MaxValue)));
		GUI.Label(new Rect(x + 11f, y + 1f, w - 21f, 20f), StripRichText(b.buttonText), _sSmall);
		if (!(num2 < 1f))
		{
			goto Branch_03a6;
		}
		goto Branch_037b;
		Branch_03a6:
		if (!GUI.Button(val, GUIContent.none, GUIStyle.none))
		{
			goto Branch_041e;
		}
		goto Branch_03ca;
		Branch_041e:
		y += 26f;
		return;
		Branch_03ca:
		_clickAnim[num] = Time.unscaledTime;
		ButtonHandler.PlayClickSound();
		_pcPage = false;
		_inputUpper = "";
		GUIUtility.keyboardControl = 0;
		ButtonHandler.HandleButtonClick(b);
		_contentScroll = 0f;
		y += 26f;
	}

	private void Rebuild()
	{
		using (Dictionary<string, Texture2D>.Enumerator enumerator = _tex.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				while (true)
				{
					KeyValuePair<string, Texture2D> current = enumerator.Current;
					if (!((Object)(object)current.Value != (Object)null))
					{
						break;
					}
					Object.Destroy((Object)(object)current.Value);
					if (!enumerator.MoveNext())
					{
						goto EndBranch_006b;
					}
				}
				continue;
				EndBranch_006b:
				break;
			}
		}
		_tex.Clear();
		using (Dictionary<long, Texture2D>.Enumerator enumerator2 = _roundCache.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				while (true)
				{
					KeyValuePair<long, Texture2D> current2 = enumerator2.Current;
					if (!((Object)(object)current2.Value != (Object)null))
					{
						break;
					}
					Object.Destroy((Object)(object)current2.Value);
					if (!enumerator2.MoveNext())
					{
						goto EndBranch_0114;
					}
				}
				continue;
				EndBranch_0114:
				break;
			}
		}
		_roundCache.Clear();
		_roundUsed.Clear();
		_bgTex = null;
		using (Dictionary<int, Texture2D>.Enumerator enumerator3 = _modBarTexCache.GetEnumerator())
		{
			while (enumerator3.MoveNext())
			{
				while (true)
				{
					KeyValuePair<int, Texture2D> current3 = enumerator3.Current;
					if (!((Object)(object)current3.Value != (Object)null))
					{
						break;
					}
					Object.Destroy((Object)(object)current3.Value);
					if (!enumerator3.MoveNext())
					{
						goto EndBranch_01d1;
					}
				}
				continue;
				EndBranch_01d1:
				break;
			}
		}
		_modBarTexCache.Clear();
		_modBarTexUsed.Clear();
		if ((Object)(object)_watermarkTex != (Object)null)
		{
			Object.Destroy((Object)(object)_watermarkTex);
			_watermarkTex = null;
			_lastWatermarkFps = -1;
			int w = Mathf.Max(1, (int)(CapturedVariables1190_Value_02 - 24f));
			Put("field", CreateRoundedTexture(w, 26, new Color32((byte)18, (byte)18, (byte)18, (byte)245), new Color32((byte)70, (byte)70, (byte)70, (byte)210), 1, 8));
			Put("panel", CreateRoundedTexture(180, 180, new Color32((byte)14, (byte)14, (byte)14, (byte)232), new Color32((byte)58, (byte)58, (byte)58, (byte)175), 1, 14));
			Put("side", CreateRoundedTexture(120, 24, new Color32((byte)23, (byte)23, (byte)23, (byte)210), default(Color32), 0, 9));
			Put("sideH", CreateRoundedTexture(120, 24, new Color32((byte)37, (byte)37, (byte)37, (byte)235), default(Color32), 0, 9));
			Put("sideOn", CreateRoundedTexture(120, 24, new Color32((byte)62, (byte)62, (byte)62, (byte)245), new Color32((byte)120, (byte)120, (byte)120, (byte)80), 1, 9));
			Put("card", CreateRoundedTexture(380, 30, new Color32((byte)20, (byte)20, (byte)20, (byte)236), new Color32((byte)62, (byte)62, (byte)62, (byte)155), 1, 11));
			Put("cardH", CreateRoundedTexture(380, 30, new Color32((byte)32, (byte)32, (byte)32, (byte)246), new Color32((byte)92, (byte)92, (byte)92, (byte)190), 1, 11));
			Put("cardOn", CreateRoundedTexture(380, 30, new Color32((byte)36, (byte)42, (byte)46, (byte)250), new Color32((byte)110, (byte)160, (byte)170, (byte)210), 1, 11));
			Put("mini", CreateRoundedTexture(62, 22, new Color32((byte)30, (byte)30, (byte)30, (byte)230), new Color32((byte)76, (byte)76, (byte)76, (byte)170), 1, 6));
			Put("miniH", CreateRoundedTexture(62, 22, new Color32((byte)48, (byte)48, (byte)48, (byte)245), new Color32((byte)125, (byte)125, (byte)125, (byte)200), 1, 6));
			Put("toggle", CreateRoundedTexture(52, 20, new Color32((byte)26, (byte)26, (byte)26, (byte)245), new Color32((byte)88, (byte)88, (byte)88, (byte)205), 1, 10));
			Put("toggleH", CreateRoundedTexture(52, 20, new Color32((byte)40, (byte)40, (byte)40, (byte)248), new Color32((byte)120, (byte)120, (byte)120, (byte)220), 1, 10));
			Put("toggleOn", CreateRoundedTexture(52, 20, new Color32((byte)27, (byte)75, (byte)85, (byte)250), new Color32((byte)90, (byte)225, (byte)240, (byte)230), 1, 10));
			Put("knob", CreateRoundedTexture(14, 14, new Color32((byte)235, (byte)235, (byte)235, byte.MaxValue), new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, (byte)210), 1, 7));
			Put("scrollTrack", CreateRoundedTexture(5, 120, new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, (byte)28), default(Color32), 0, 3));
			Put("scrollThumb", CreateRoundedTexture(5, 38, new Color32((byte)210, (byte)210, (byte)210, (byte)185), default(Color32), 0, 3));
			BuildStyles();
		}
		else
		{
			_lastWatermarkFps = -1;
			int w = Mathf.Max(1, (int)(CapturedVariables1190_Value_02 - 24f));
			Put("field", CreateRoundedTexture(w, 26, new Color32((byte)18, (byte)18, (byte)18, (byte)245), new Color32((byte)70, (byte)70, (byte)70, (byte)210), 1, 8));
			Put("panel", CreateRoundedTexture(180, 180, new Color32((byte)14, (byte)14, (byte)14, (byte)232), new Color32((byte)58, (byte)58, (byte)58, (byte)175), 1, 14));
			Put("side", CreateRoundedTexture(120, 24, new Color32((byte)23, (byte)23, (byte)23, (byte)210), default(Color32), 0, 9));
			Put("sideH", CreateRoundedTexture(120, 24, new Color32((byte)37, (byte)37, (byte)37, (byte)235), default(Color32), 0, 9));
			Put("sideOn", CreateRoundedTexture(120, 24, new Color32((byte)62, (byte)62, (byte)62, (byte)245), new Color32((byte)120, (byte)120, (byte)120, (byte)80), 1, 9));
			Put("card", CreateRoundedTexture(380, 30, new Color32((byte)20, (byte)20, (byte)20, (byte)236), new Color32((byte)62, (byte)62, (byte)62, (byte)155), 1, 11));
			Put("cardH", CreateRoundedTexture(380, 30, new Color32((byte)32, (byte)32, (byte)32, (byte)246), new Color32((byte)92, (byte)92, (byte)92, (byte)190), 1, 11));
			Put("cardOn", CreateRoundedTexture(380, 30, new Color32((byte)36, (byte)42, (byte)46, (byte)250), new Color32((byte)110, (byte)160, (byte)170, (byte)210), 1, 11));
			Put("mini", CreateRoundedTexture(62, 22, new Color32((byte)30, (byte)30, (byte)30, (byte)230), new Color32((byte)76, (byte)76, (byte)76, (byte)170), 1, 6));
			Put("miniH", CreateRoundedTexture(62, 22, new Color32((byte)48, (byte)48, (byte)48, (byte)245), new Color32((byte)125, (byte)125, (byte)125, (byte)200), 1, 6));
			Put("toggle", CreateRoundedTexture(52, 20, new Color32((byte)26, (byte)26, (byte)26, (byte)245), new Color32((byte)88, (byte)88, (byte)88, (byte)205), 1, 10));
			Put("toggleH", CreateRoundedTexture(52, 20, new Color32((byte)40, (byte)40, (byte)40, (byte)248), new Color32((byte)120, (byte)120, (byte)120, (byte)220), 1, 10));
			Put("toggleOn", CreateRoundedTexture(52, 20, new Color32((byte)27, (byte)75, (byte)85, (byte)250), new Color32((byte)90, (byte)225, (byte)240, (byte)230), 1, 10));
			Put("knob", CreateRoundedTexture(14, 14, new Color32((byte)235, (byte)235, (byte)235, byte.MaxValue), new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, (byte)210), 1, 7));
			Put("scrollTrack", CreateRoundedTexture(5, 120, new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, (byte)28), default(Color32), 0, 3));
			Put("scrollThumb", CreateRoundedTexture(5, 38, new Color32((byte)210, (byte)210, (byte)210, (byte)185), default(Color32), 0, 3));
			BuildStyles();
		}
	}

	private void PcToggle(ref float y, float w, string label, bool on, Action click)
	{
		if (RowToggle(new Rect(0f, y, w, 30f), label, on, label.GetHashCode() * 31 + 9))
		{
			ButtonHandler.PlayClickSound();
			click();
			y += 36f;
		}
		else
		{
			y += 36f;
		}
	}

	private static void SetStyleBackground(GUIStyle s, Texture2D t)
	{
		if ((Object)(object)t != (Object)null)
		{
			GUIStyleState normal = s.normal;
			GUIStyleState active = s.active;
			GUIStyleState hover = s.hover;
			Texture2D val = (s.focused.background = t);
			Texture2D val3 = (hover.background = val);
			Texture2D background = (active.background = val3);
			normal.background = background;
			GUIStyleState normal2 = s.normal;
			GUIStyleState active2 = s.active;
			GUIStyleState hover2 = s.hover;
			Color val6 = (s.focused.textColor = Color.white);
			Color val7 = (hover2.textColor = val6);
			Color textColor = (active2.textColor = val7);
			normal2.textColor = textColor;
		}
		else
		{
			GUIStyleState normal3 = s.normal;
			GUIStyleState active3 = s.active;
			GUIStyleState hover3 = s.hover;
			Color val6 = (s.focused.textColor = Color.white);
			Color val7 = (hover3.textColor = val6);
			Color textColor = (active3.textColor = val7);
			normal3.textColor = textColor;
		}
	}

	private static string StripRichText(string text)
	{
		if (string.IsNullOrEmpty(text))
		{
			return string.Empty;
		}
		if (CapturedVariables1190_Lookup_01.TryGetValue(text, out string value))
		{
			return value;
		}
		value = Regex.Replace(text, "<.*?>", string.Empty);
		CapturedVariables1190_Lookup_01[text] = value;
		return value;
	}

	private void Put(string k, Texture2D t)
	{
		if (_tex.TryGetValue(k, out Texture2D value) && (Object)(object)value != (Object)null)
		{
			Object.Destroy((Object)(object)value);
			_tex[k] = t;
		}
		else
		{
			_tex[k] = t;
		}
	}

	private bool IsSidebarActive(ButtonHandler.Button b)
	{
		string b2 = StripRichText(b.buttonText).Replace(" ", "_");
		if (b.buttonText == "Home")
		{
			return Variables.currentPage == Category.Home;
		}
		if (b.buttonText == "VRRig")
		{
			return Variables.currentPage == Category.Player;
		}
		if (b.buttonText == "Favorites")
		{
			return Variables.currentPage == Category.Favorited;
		}
		return string.Equals(Variables.currentPage.ToString(), b2, StringComparison.OrdinalIgnoreCase);
	}

	private void OnGUI()
	{
		int width = Screen.width;
		int height = Screen.height;
		if (width != _lastW || height != _lastH)
		{
			_lastW = width;
			_lastH = height;
			_dirty = true;
			GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one * ((float)width / (float)originalWidth * 2f));
			if (_dirty)
			{
				goto Branch_00fb;
			}
		}
		else
		{
			GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one * ((float)width / (float)originalWidth * 2f));
			if (_dirty)
			{
				goto Branch_00fb;
			}
		}
		if (!CapturedVariables1190_State_04)
		{
			goto Branch_018e;
		}
		goto Branch_013a;
		Branch_02b4:
		if (!_animatingOut)
		{
			goto Branch_034d;
		}
		goto Branch_02ce;
		Branch_034d:
		if (!CapturedVariables1190_State_04)
		{
			goto Branch_0361;
		}
		goto Branch_0392;
		Branch_02ce:
		_animT = Mathf.MoveTowards(_animT, 0f, Time.unscaledDeltaTime * 5f);
		if (_animT <= 0f)
		{
			_animT = 0f;
			_animatingOut = false;
			if (!CapturedVariables1190_State_04)
			{
				goto Branch_0361;
			}
		}
		else if (!CapturedVariables1190_State_04)
		{
			goto Branch_0361;
		}
		goto Branch_0392;
		Branch_00fb:
		Rebuild();
		_dirty = false;
		if (!CapturedVariables1190_State_04)
		{
			goto Branch_018e;
		}
		goto Branch_013a;
		Branch_0361:
		if (_animatingOut)
		{
			goto Branch_0392;
		}
		return;
		Branch_013a:
		if (_wasVisible)
		{
			goto Branch_018e;
		}
		_animatingIn = true;
		_animatingOut = false;
		_animT = 0f;
		if (!CapturedVariables1190_State_04)
		{
			goto Branch_01a2;
		}
		goto Branch_0204;
		Branch_054b:
		_win = new Rect((float)originalWidth / 2f - CapturedVariables1190_Value_02 - 8f, 8f, CapturedVariables1190_Value_02, CapturedVariables1190_Value_01);
		_win = GUI.Window(1, _win, new WindowFunction(DrawWin), "", GUIStyle.none);
		Matrix4x4 matrix;
		GUI.matrix = matrix;
		GUI.color = Color.white;
		ControllerEmulator.DrawInputHud();
		return;
		Branch_018e:
		if (!CapturedVariables1190_State_04)
		{
			goto Branch_01a2;
		}
		goto Branch_0204;
		Branch_01a2:
		if (!_wasVisible)
		{
			goto Branch_0204;
		}
		_animatingOut = true;
		_animatingIn = false;
		_animT = 1f;
		_wasVisible = CapturedVariables1190_State_04;
		if (!_animatingIn)
		{
			goto Branch_02b4;
		}
		goto Branch_0229;
		Branch_0392:
		float num = 1f - Mathf.Pow(1f - _animT, 3f);
		float num2 = Mathf.Lerp(-400f, 0f, num);
		float num3 = Mathf.Lerp(400f, 0f, num);
		GUI.color = new Color(1f, 1f, 1f, num);
		matrix = GUI.matrix;
		GUI.matrix = matrix * Matrix4x4.Translate(new Vector3(num2, 0f, 0f));
		if (CapturedVariables1190_Items_01.Count > 0)
		{
			DrawModList();
			float num4 = Mathf.Lerp(-40f, 0f, num);
			GUI.matrix = matrix * Matrix4x4.Translate(new Vector3(0f, num4, 0f));
			DrawWatermark();
			EnsureBgTexture();
			GUI.matrix = matrix * Matrix4x4.Translate(new Vector3(num3, 0f, 0f));
			if (((Rect)_win).width < 1f)
			{
				goto Branch_054b;
			}
		}
		else
		{
			float num4 = Mathf.Lerp(-40f, 0f, num);
			GUI.matrix = matrix * Matrix4x4.Translate(new Vector3(0f, num4, 0f));
			DrawWatermark();
			EnsureBgTexture();
			GUI.matrix = matrix * Matrix4x4.Translate(new Vector3(num3, 0f, 0f));
			if (((Rect)_win).width < 1f)
			{
				goto Branch_054b;
			}
		}
		_win = GUI.Window(1, _win, new WindowFunction(DrawWin), "", GUIStyle.none);
		GUI.matrix = matrix;
		GUI.color = Color.white;
		ControllerEmulator.DrawInputHud();
		return;
		Branch_0204:
		_wasVisible = CapturedVariables1190_State_04;
		if (!_animatingIn)
		{
			goto Branch_02b4;
		}
		Branch_0229:
		_animT = Mathf.MoveTowards(_animT, 1f, Time.unscaledDeltaTime * 5f);
		if (_animT >= 1f)
		{
			_animT = 1f;
			_animatingIn = false;
			if (_animatingOut)
			{
				goto Branch_02ce;
			}
		}
		else if (_animatingOut)
		{
			goto Branch_02ce;
		}
		goto Branch_034d;
	}

	private void DrawWin(int id)
	{
		DrawRound(new Rect(0f, 0f, ((Rect)_win).width, ((Rect)_win).height), 14, new Color32((byte)10, (byte)10, (byte)10, (byte)236), new Color32((byte)92, (byte)92, (byte)92, (byte)150));
		float num = 12f;
		float num2 = 8f;
		float num3 = ((Rect)_win).width - 24f;
		DrawSearch(num + 124f + 12f, num2, num3 - 124f - 12f);
		_sTitle.normal.textColor = GetAnimatedGradientColor();
		GUI.Label(new Rect(num + 6f, num2, 124f, 26f), "NXO v6.1", _sTitle);
		_sTitle.normal.textColor = Color.white;
		float num4 = num2 + 34f;
		float h = ((Rect)_win).height - num4 - 12f;
		DrawSidebar(num, num4, 124f, h);
		DrawContent(num + 124f + 12f, num4, num3 - 124f - 12f, h);
		GUI.DragWindow(new Rect(0f, 0f, 136f, num4));
	}

	public static void UpdateFreecam()
	{
		if (!CapturedVariables1190_State_01)
		{
			return;
		}
		TrackModEnabled("Freecam");
		Transform transform = ((Component)Variables.Variables_Reference_09.headCollider).transform;
		float num;
		Vector3 val;
		if (!UnityInput.Current.GetKey((KeyCode)304))
		{
			num = Settings.CapturedVariables3760_Value_14 / 2f;
			((Component)Variables.Variables_Reference_06).GetComponent<Rigidbody>().linearVelocity = new Vector3(0f, 0.065f, 0f);
			val = Vector3.zero;
			if (UnityInput.Current.GetKey((KeyCode)119))
			{
				goto Branch_0101;
			}
		}
		else
		{
			num = Settings.CapturedVariables3760_Value_14 / 2f + 3f;
			((Component)Variables.Variables_Reference_06).GetComponent<Rigidbody>().linearVelocity = new Vector3(0f, 0.065f, 0f);
			val = Vector3.zero;
			if (UnityInput.Current.GetKey((KeyCode)119))
			{
				goto Branch_0101;
			}
		}
		if (!UnityInput.Current.GetKey((KeyCode)115))
		{
			goto Branch_017b;
		}
		goto Branch_014e;
		Branch_0288:
		val -= transform.up;
		transform.position += ((Vector3)val).normalized * num * Time.deltaTime;
		if (!UnityInput.Current.GetMouseButton(1))
		{
			goto Branch_0393;
		}
		goto Branch_0323;
		Branch_0393:
		Movement.Movement_Position_03 = UnityInput.Current.mousePosition;
		return;
		Branch_0323:
		Vector3 val2 = UnityInput.Current.mousePosition - Movement.Movement_Position_03;
		Transform transform2 = Variables.Variables_Reference_09.mainCamera.transform;
		transform2.localEulerAngles += new Vector3((0f - val2.y) * 0.1f, val2.x * 0.1f, 0f);
		Movement.Movement_Position_03 = UnityInput.Current.mousePosition;
		return;
		Branch_0101:
		val += transform.forward;
		if (!UnityInput.Current.GetKey((KeyCode)115))
		{
			goto Branch_017b;
		}
		goto Branch_014e;
		Branch_02dc:
		transform.position += ((Vector3)val).normalized * num * Time.deltaTime;
		if (!UnityInput.Current.GetMouseButton(1))
		{
			goto Branch_0393;
		}
		goto Branch_0323;
		Branch_014e:
		val -= transform.forward;
		if (!UnityInput.Current.GetKey((KeyCode)97))
		{
			goto Branch_01c8;
		}
		goto Branch_019b;
		Branch_017b:
		if (!UnityInput.Current.GetKey((KeyCode)97))
		{
			goto Branch_01c8;
		}
		Branch_019b:
		val -= transform.right;
		if (!UnityInput.Current.GetKey((KeyCode)100))
		{
			goto Branch_0215;
		}
		goto Branch_01e8;
		Branch_01c8:
		if (!UnityInput.Current.GetKey((KeyCode)100))
		{
			goto Branch_0215;
		}
		Branch_01e8:
		val += transform.right;
		if (!UnityInput.Current.GetKey((KeyCode)32))
		{
			goto Branch_0265;
		}
		goto Branch_0235;
		Branch_0215:
		if (!UnityInput.Current.GetKey((KeyCode)32))
		{
			goto Branch_0265;
		}
		Branch_0235:
		val += transform.up;
		if (!UnityInput.Current.GetKey((KeyCode)306))
		{
			goto Branch_02dc;
		}
		goto Branch_0288;
		Branch_0265:
		if (!UnityInput.Current.GetKey((KeyCode)306))
		{
			goto Branch_02dc;
		}
		goto Branch_0288;
	}

	private bool MiniButton(float x, float y, float w, float h, string label, int id)
	{
		Rect val = default(Rect);
		val = new Rect(x, y, w, h);
		bool flag = ((Rect)val).Contains(Event.current.mousePosition);
		float num = ClickScale(id);
		Matrix4x4 matrix = GUI.matrix;
		bool clicked;
		if (num < 1f)
		{
			GUIUtility.ScaleAroundPivot(new Vector2(num, num), ((Rect)val).center);
			DrawRound(val, 5, flag ? new Color32((byte)48, (byte)48, (byte)48, (byte)245) : new Color32((byte)30, (byte)30, (byte)30, (byte)235), flag ? new Color32((byte)125, (byte)125, (byte)125, (byte)190) : new Color32((byte)76, (byte)76, (byte)76, (byte)150));
			GUI.Label(val, label, _sPill);
			if (num < 1f)
			{
				goto Branch_016e;
			}
		}
		else
		{
			DrawRound(val, 5, flag ? new Color32((byte)48, (byte)48, (byte)48, (byte)245) : new Color32((byte)30, (byte)30, (byte)30, (byte)235), flag ? new Color32((byte)125, (byte)125, (byte)125, (byte)190) : new Color32((byte)76, (byte)76, (byte)76, (byte)150));
			GUI.Label(val, label, _sPill);
			if (num < 1f)
			{
				goto Branch_016e;
			}
		}
		clicked = GUI.Button(val, GUIContent.none, GUIStyle.none);
		GUI.matrix = matrix;
		if (clicked)
		{
			_clickAnim[id] = Time.unscaledTime;
		}
		return clicked;
		Branch_016e:
		GUI.matrix = matrix;
		clicked = GUI.Button(val, GUIContent.none, GUIStyle.none);
		if (clicked)
		{
			_clickAnim[id] = Time.unscaledTime;
		}
		return clicked;
	}

	private static Color GetAnimatedGradientColor(float phaseOffset = 0f)
	{
		return Color.Lerp(Main.CapturedVariables1950_Color_02, Main.CapturedVariables1950_Color_06, Mathf.PingPong(Time.unscaledTime * 1.5f - phaseOffset, 1f));
	}

	private void DrawPcSidebarButton(float x, ref float y, float w)
	{
		int num = 1517;
		Rect val = default(Rect);
		val = new Rect(x, y, w, 22f);
		bool pcPage = _pcPage;
		bool hovered = ((Rect)val).Contains(Event.current.mousePosition);
		float num2 = ClickScale(num);
		Matrix4x4 matrix = GUI.matrix;
		if (num2 < 1f)
		{
			GUIUtility.ScaleAroundPivot(new Vector2(num2, num2), ((Rect)val).center);
			DrawRound(val, 7, pcPage ? new Color32((byte)62, (byte)62, (byte)62, (byte)245) : new Color32((byte)23, (byte)23, (byte)23, (byte)220), pcPage ? new Color32((byte)120, (byte)120, (byte)120, (byte)80) : new Color32((byte)0, (byte)0, (byte)0, (byte)0), pcPage ? 1 : 0);
			DrawHoverGlow(val, 7, HoverT(num, hovered) * 0.07f);
			if (pcPage)
			{
				goto Branch_0189;
			}
		}
		else
		{
			DrawRound(val, 7, pcPage ? new Color32((byte)62, (byte)62, (byte)62, (byte)245) : new Color32((byte)23, (byte)23, (byte)23, (byte)220), pcPage ? new Color32((byte)120, (byte)120, (byte)120, (byte)80) : new Color32((byte)0, (byte)0, (byte)0, (byte)0), pcPage ? 1 : 0);
			DrawHoverGlow(val, 7, HoverT(num, hovered) * 0.07f);
			if (pcPage)
			{
				goto Branch_0189;
			}
		}
		_sSmall.normal.textColor = (pcPage ? Color.white : (Color32)(new Color32((byte)168, (byte)168, (byte)168, byte.MaxValue)));
		GUI.Label(new Rect(x + 11f, y + 1f, w - 21f, 20f), "PC", _sSmall);
		if (!(num2 < 1f))
		{
			goto Branch_0334;
		}
		Branch_0309:
		GUI.matrix = matrix;
		if (!GUI.Button(val, GUIContent.none, GUIStyle.none))
		{
			goto Branch_03a5;
		}
		goto Branch_0358;
		Branch_03a5:
		y += 26f;
		return;
		Branch_0334:
		if (!GUI.Button(val, GUIContent.none, GUIStyle.none))
		{
			goto Branch_03a5;
		}
		goto Branch_0358;
		Branch_0189:
		GUI.color = GetAnimatedGradientColor();
		GUI.DrawTexture(new Rect(x + 3f, y + 5f, 2f, 12f), (Texture)(object)RoundTex(2, 12, 1, new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue), default(Color32), 0));
		GUI.color = Color.white;
		_sSmall.normal.textColor = (pcPage ? Color.white : (Color32)(new Color32((byte)168, (byte)168, (byte)168, byte.MaxValue)));
		GUI.Label(new Rect(x + 11f, y + 1f, w - 21f, 20f), "PC", _sSmall);
		if (!(num2 < 1f))
		{
			goto Branch_0334;
		}
		goto Branch_0309;
		Branch_0358:
		_clickAnim[num] = Time.unscaledTime;
		ButtonHandler.PlayClickSound();
		_pcPage = true;
		_inputUpper = "";
		GUIUtility.keyboardControl = 0;
		_contentScroll = 0f;
		y += 26f;
	}

	private Texture2D GetModBarTex(int barW)
	{
		if (_modBarTexCache.TryGetValue(barW, out Texture2D value) && (Object)(object)value != (Object)null)
		{
			_modBarTexUsed[barW] = Time.unscaledTime;
			return value;
		}
		if (_modBarTexCache.Count >= 48)
		{
			int key = 0;
			float num = float.MaxValue;
			using (Dictionary<int, float>.Enumerator enumerator = _modBarTexUsed.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					while (true)
					{
						KeyValuePair<int, float> current = enumerator.Current;
						if (!(current.Value < num))
						{
							break;
						}
						num = current.Value;
						key = current.Key;
						if (!enumerator.MoveNext())
						{
							goto EndBranch_0100;
						}
					}
					continue;
					EndBranch_0100:
					break;
				}
			}
			if (_modBarTexCache.TryGetValue(key, out Texture2D value2) && (Object)(object)value2 != (Object)null)
			{
				Object.Destroy((Object)(object)value2);
				_modBarTexCache.Remove(key);
				_modBarTexUsed.Remove(key);
				value = CreateRoundedTexture(barW, 20, new Color32((byte)0, (byte)0, (byte)0, (byte)180), default(Color32), 0, 6);
				_modBarTexCache[barW] = value;
				_modBarTexUsed[barW] = Time.unscaledTime;
				return value;
			}
			_modBarTexCache.Remove(key);
			_modBarTexUsed.Remove(key);
			value = CreateRoundedTexture(barW, 20, new Color32((byte)0, (byte)0, (byte)0, (byte)180), default(Color32), 0, 6);
			_modBarTexCache[barW] = value;
			_modBarTexUsed[barW] = Time.unscaledTime;
			return value;
		}
		value = CreateRoundedTexture(barW, 20, new Color32((byte)0, (byte)0, (byte)0, (byte)180), default(Color32), 0, 6);
		_modBarTexCache[barW] = value;
		_modBarTexUsed[barW] = Time.unscaledTime;
		return value;
	}
}
