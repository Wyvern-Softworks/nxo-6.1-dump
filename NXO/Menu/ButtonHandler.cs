using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using NXO.Mods;
using NXO.Mods.Categories;
using NXO.Utilities;
using UnityEngine;
using UnityEngine.Networking;

namespace NXO.Menu;

public class ButtonHandler
{
	public class Button
	{
		private bool _enabled;

		public string buttonText { get; set; }

		public bool isToggle { get; set; }

		public bool Enabled
		{
			get
			{
				return _enabled;
			}
			set
			{
				if (_enabled != value)
				{
					_enabled = value;
					Main.ShowTooltip();
				}
			}
		}

		public Action onEnable { get; set; }

		public Action onDisable { get; set; }

		public bool incremental { get; set; }

		public Action up { get; set; }

		public Action down { get; set; }

		public Category Page { get; set; }

		public bool showGear { get; set; }

		public string tooltip { get; set; }

		public bool isCategory { get; set; }

		public Button(string label, Category page, bool isToggle, bool isActive, Action onClick, Action onDisable = null, bool incremental = false, Action incrementUp = null, Action incrementDown = null)
		{
			buttonText = label;
			this.isToggle = isToggle;
			onEnable = onClick;
			Page = page;
			this.onDisable = onDisable;
			Enabled = isActive;
			this.incremental = incremental;
			up = incrementUp;
			down = incrementDown;
		}

		public void SetText(string newText)
		{
			buttonText = newText;
		}
	}

	public class BtnCollider : MonoBehaviour
	{
		public Button clickedButton;

		public readonly List<Transform> animBody = new List<Transform>(6);

		public readonly List<Vector3> animBodyBase = new List<Vector3>(6);

		public readonly List<Transform> animUniform = new List<Transform>(2);

		public readonly List<Vector3> animUniformBase = new List<Vector3>(2);

		private Coroutine _pressRoutine;

		private float _factor = 1f;

		private Transform _pulseKnob;

		private Renderer _pulseKnobRend;

		private readonly List<Renderer> _pulsePill = new List<Renderer>(2);

		private float _pulseOffY;

		private float _pulseOnY;

		private Color _pulsePillOff;

		private Color _pulsePillOn;

		private bool _hasPulse;

		public const float ToggleScale = 0.92f;

		private const float PressScale = 0.85f;

		private const float ShrinkDuration = 0.05f;

		private const float ReleaseDuration = 0.12f;

		private IEnumerator BounceRoutine()
		{
			yield return ScaleTo(0.85f, 0.05f);
			yield return ScaleTo(1f, 0.12f, overshoot: true);
			_pressRoutine = null;
		}

		private IEnumerator PressRoutine()
		{
			Button btn = clickedButton;
			if (btn == null)
			{
				_pressRoutine = null;
				yield break;
			}

			if (btn.isToggle)
			{
				if (_hasPulse)
				{
					bool wasOn = btn.Enabled;
					ExecuteButton(btn, redraw: false);
					yield return PulseLerp(wasOn ? 1f : 0f, wasOn ? 0f : 1f, 0.15f);
					Main.RedrawMenu();
				}
				else
				{
					if (!Settings.CapturedVariables3760_State_06)
					{
						yield return ScaleTo(btn.Enabled ? 1f : 0.92f, 0.05f);
					}
					CapturedVariables570_Button_02 = btn;
					HandleButtonClick(btn);
				}
				_pressRoutine = null;
				yield break;
			}

			bool isUp = btn.buttonText.EndsWith("_UP");
			bool isDown = btn.buttonText.EndsWith("_DOWN");
			if (isUp || isDown)
			{
				CapturedVariables570_Button_03 = FindButton(btn.buttonText.Substring(0, btn.buttonText.Length - (isUp ? 3 : 5)));
				CapturedVariables570_State_02 = isUp;
				HandleButtonClick(btn);
				_pressRoutine = null;
				yield break;
			}

			HandleButtonClick(btn);
			if (_hasPulse)
			{
				((MonoBehaviour)this).StartCoroutine(PulseRoutine());
			}
			yield return ScaleTo(0.85f, 0.05f);
			yield return ScaleTo(1f, 0.12f, overshoot: true);
			_pressRoutine = null;
		}

		public void SetupPulse(Transform knob, Renderer knobRend, float offY, float onY, List<Renderer> pill, Color pillOff, Color pillOn)
		{
			_pulseKnob = knob;
			_pulseKnobRend = knobRend;
			_pulseOffY = offY;
			_pulseOnY = onY;
			_pulsePill.Clear();
			if (pill != null)
			{
				_pulsePill.AddRange(pill);
				_pulsePillOff = pillOff;
				_pulsePillOn = pillOn;
				_hasPulse = (Object)(object)knob != (Object)null;
			}
			else
			{
				_pulsePillOff = pillOff;
				_pulsePillOn = pillOn;
				_hasPulse = (Object)(object)knob != (Object)null;
			}
		}

		private IEnumerator PulseRoutine()
		{
			yield return PulseLerp(0f, 1f, 0.12f);
			yield return (object)new WaitForSecondsRealtime(0.1f);
			yield return PulseLerp(1f, 0f, 0.12f);
		}

		private static float EaseOutBack(float t)
		{
			float num = t - 1f;
			return 1f + 4f * num * num * num + 3f * num * num;
		}

		private IEnumerator PulseLerp(float from, float to, float dur)
		{
			float e = 0f;
			if (e < dur)
			{
				do
				{
					ApplyPulse(Mathf.Lerp(from, to, e / dur));
					e += Time.unscaledDeltaTime;
					yield return null;
				}
				while (e < dur);
			}
			ApplyPulse(to);
		}

		public void PlayBounce()
		{
			if (Main.CapturedVariables1950_State_09 && animBody.Count != 0)
			{
				if (_pressRoutine != null)
				{
					((MonoBehaviour)this).StopCoroutine(_pressRoutine);
					_pressRoutine = ((MonoBehaviour)this).StartCoroutine(BounceRoutine());
				}
				else
				{
					_pressRoutine = ((MonoBehaviour)this).StartCoroutine(BounceRoutine());
				}
			}
		}

		private void ApplyPulse(float p)
		{
			Color val;
			int num;
			if ((Object)(object)_pulseKnob != (Object)null)
			{
				Vector3 localPosition = _pulseKnob.localPosition;
				localPosition.y = Mathf.Lerp(_pulseOffY, _pulseOnY, p);
				_pulseKnob.localPosition = localPosition;
				val = Color.Lerp(_pulsePillOff, _pulsePillOn, p);
				num = 0;
			}
			else
			{
				val = Color.Lerp(_pulsePillOff, _pulsePillOn, p);
				num = 0;
			}
			if (num < _pulsePill.Count)
			{
				while (true)
				{
					Renderer val2 = _pulsePill[num];
					if (!((Object)(object)val2 == (Object)null) && !((Object)(object)val2.sharedMaterial == (Object)null))
					{
						Color color = val;
						color.a = val2.sharedMaterial.color.a;
						val2.sharedMaterial.color = color;
						num++;
						if (num >= _pulsePill.Count)
						{
							break;
						}
					}
					else
					{
						num++;
						if (num >= _pulsePill.Count)
						{
							break;
						}
					}
				}
			}
			if ((Object)(object)_pulseKnobRend != (Object)null && (Object)(object)_pulseKnobRend.sharedMaterial != (Object)null)
			{
				Color color2 = Color.Lerp(Color.Lerp(_pulsePillOff, Color.white, 0.6f), Color.Lerp(_pulsePillOn, Color.white, 0.6f), p);
				color2.a = _pulseKnobRend.sharedMaterial.color.a;
				_pulseKnobRend.sharedMaterial.color = color2;
			}
		}

		public void ApplyFactor(float f)
		{
			_factor = f;
			int num = 0;
			if (num < animBody.Count)
			{
				while (true)
				{
					Transform val = animBody[num];
					if (!((Object)(object)val == (Object)null))
					{
						Vector3 val2 = animBodyBase[num];
						val.localScale = new Vector3(val2.x, val2.y * f, val2.z * f);
						num++;
						if (num >= animBody.Count)
						{
							break;
						}
					}
					else
					{
						num++;
						if (num >= animBody.Count)
						{
							break;
						}
					}
				}
			}
			int num2 = 0;
			if (num2 >= animUniform.Count)
			{
				return;
			}
			while (true)
			{
				Transform val3 = animUniform[num2];
				if (!((Object)(object)val3 == (Object)null))
				{
					val3.localScale = animUniformBase[num2] * f;
					num2++;
					if (num2 >= animUniform.Count)
					{
						break;
					}
				}
				else
				{
					num2++;
					if (num2 >= animUniform.Count)
					{
						break;
					}
				}
			}
		}

		public void RegisterUniform(Transform t)
		{
			if (!((Object)(object)t == (Object)null))
			{
				animUniform.Add(t);
				animUniformBase.Add(t.localScale);
			}
		}

		public static void ToggleFavorite(Button button)
		{
			if (button == null || string.IsNullOrEmpty(button.buttonText) || button.isCategory || (!button.isToggle && button.onEnable == null))
			{
				return;
			}
			if (CapturedVariables570_Items_02.Contains(button))
			{
				CapturedVariables570_Items_02.Remove(button);
				CapturedVariables570_Items_04.Remove(button.buttonText);
				GorillaTagger obj = Variables.Variables_Reference_09;
				if (obj != null)
				{
					VRRig offlineVRRig = obj.offlineVRRig;
					if (offlineVRRig != null)
					{
						offlineVRRig.PlayHandTapLocal(28, !Variables.Variables_State_05, 1f);
						NotificationLib.ShowNotification(NotificationLib.NotificationType.Deleted, "Unfavorited `" + button.buttonText + "`");
						SaveFavorites();
						Main.RebuildMenu();
						return;
					}
				}
				NotificationLib.ShowNotification(NotificationLib.NotificationType.Deleted, "Unfavorited `" + button.buttonText + "`");
				SaveFavorites();
				Main.RebuildMenu();
				return;
			}
			CapturedVariables570_Items_02.Add(button);
			CapturedVariables570_Items_04.Add(button.buttonText);
			GorillaTagger obj2 = Variables.Variables_Reference_09;
			if (obj2 != null)
			{
				VRRig offlineVRRig2 = obj2.offlineVRRig;
				if (offlineVRRig2 != null)
				{
					offlineVRRig2.PlayHandTapLocal(28, !Variables.Variables_State_05, 1f);
					NotificationLib.ShowNotification(NotificationLib.NotificationType.Saved, "Favorited `" + button.buttonText + "`");
					SaveFavorites();
					Main.RebuildMenu();
				}
			}
			NotificationLib.ShowNotification(NotificationLib.NotificationType.Saved, "Favorited `" + button.buttonText + "`");
			SaveFavorites();
			Main.RebuildMenu();
		}

		public void OnTriggerEnter(Collider collider)
		{
			if (collider == null)
			{
				if ((Object)null == (Object)null)
				{
					return;
				}
			}
			else if ((Object)(object)((Component)collider).gameObject == (Object)null)
			{
				return;
			}
			if (((Object)((Component)collider).gameObject).name != "buttonclicker" || Time.time - CapturedVariables570_Value_01 < 0.25f)
			{
				return;
			}
			CapturedVariables570_Value_01 = Time.time;
			GorillaTagger obj = Variables.Variables_Reference_09;
			if (obj != null)
			{
				obj.StartVibration(Variables.Variables_State_05, Variables.Variables_Reference_09.tagHapticStrength / 2f, Variables.Variables_Reference_09.tagHapticDuration / 2f);
				PlayClickSound();
				if (InputHandler.IsLeftGripPressed())
				{
					goto Branch_0139;
				}
			}
			else
			{
				PlayClickSound();
				if (InputHandler.IsLeftGripPressed())
				{
					goto Branch_0139;
				}
			}
			if (!Main.CapturedVariables1950_State_09 || animBody.Count == 0)
			{
				HandleButtonClick(clickedButton);
			}
			else if (_pressRoutine != null)
			{
				((MonoBehaviour)this).StopCoroutine(_pressRoutine);
				_pressRoutine = ((MonoBehaviour)this).StartCoroutine(PressRoutine());
			}
			else
			{
				_pressRoutine = ((MonoBehaviour)this).StartCoroutine(PressRoutine());
			}
			return;
			Branch_0139:
			ToggleFavorite(clickedButton);
			HandleButtonClick(clickedButton);
		}

		private IEnumerator ScaleTo(float target, float duration, bool overshoot = false)
		{
			float from = _factor;
			float e = 0f;
			if (e < duration)
			{
				do
				{
					float t = e / duration;
					float k = (overshoot ? EaseOutBack(t) : (1f - (1f - t) * (1f - t) * (1f - t)));
					ApplyFactor(Mathf.LerpUnclamped(from, target, k));
					e += Time.unscaledDeltaTime;
					yield return null;
				}
				while (e < duration);
			}
			ApplyFactor(target);
		}

		public void RegisterBody(Transform t)
		{
			if (!((Object)(object)t == (Object)null))
			{
				animBody.Add(t);
				animBodyBase.Add(t.localScale);
			}
		}
	}

	public enum SoundType
	{
		AssetBundle,
		HandTap,
		EmbeddedWav,
		CustomFile
	}

	public class SoundEntry
	{
		public SoundType Type;

		public string AssetPath;

		public string ClipName;

		public string Description;

		public int HandTapIndex;

		public SoundEntry(SoundType type, string resourcePath, string clipName, string description)
		{
			Type = type;
			AssetPath = resourcePath;
			ClipName = clipName;
			Description = description;
		}

		public SoundEntry(SoundType type, int index, string desc)
		{
			Type = type;
			HandTapIndex = index;
			Description = desc;
		}
	}

	[CompilerGenerated]
	private sealed class CapturedVariables280
	{
		public string targetName;

		internal bool FindButtonInCurrentContext_Lambda0(Button b)
		{
			if (b != null)
			{
				return b.buttonText == targetName;
			}
			return false;
		}

		internal bool FindButtonInCurrentContext_Lambda1(Button b)
		{
			if (b != null)
			{
				return b.buttonText == targetName;
			}
			return false;
		}
	}

	[CompilerGenerated]
	private sealed class CapturedVariables350
	{
		public Category page;

		internal bool GetButtonInfoByPage_Lambda1(Button b)
		{
			if (b != null)
			{
				return b.Page == page;
			}
			return false;
		}
	}

	[CompilerGenerated]
	private sealed class CapturedVariables450
	{
		public string modName;

		internal bool LoadFavoritedMods_Lambda0(Button b)
		{
			if (b != null)
			{
				return b.buttonText == modName;
			}
			return false;
		}
	}

	[CompilerGenerated]
	private sealed class CapturedVariables520
	{
		public string presetName;

		internal void GeneratePresetButtons_Lambda2()
		{
			OpenPreset(presetName);
		}
	}

	[CompilerGenerated]
	private sealed class CapturedVariables540
	{
		public string presetName;

		internal void GeneratePresetCategoryButtons_Lambda0()
		{
			LoadSpecificPreset(presetName);
		}

		internal void GeneratePresetCategoryButtons_Lambda3()
		{
			OpenPresetMods(presetName);
		}

		internal void GeneratePresetCategoryButtons_Lambda4()
		{
			OpenPresetSettings(presetName);
		}

		internal void GeneratePresetCategoryButtons_Lambda2()
		{
			DeletePreset(presetName);
		}

		internal void GeneratePresetCategoryButtons_Lambda1()
		{
			RenamePreset(presetName);
		}
	}

	[CompilerGenerated]
	private sealed class CapturedVariables550
	{
		public string presetName;

		internal void BeginRenamePreset_Lambda0(string newName)
		{
			string text = newName.Trim();
			newName = text;
			if (string.IsNullOrEmpty(newName) || newName == presetName)
			{
				return;
			}
			string text2 = Path.Combine(PresetsDirectoryPath, presetName);
			string text3 = Path.Combine(PresetsDirectoryPath, newName);
			if (Directory.Exists(text2))
			{
				if (Directory.Exists(text3))
				{
					NotificationLib.ShowNotification(NotificationLib.NotificationType.Error, "Preset Name Already Exists");
					return;
				}
				Directory.Move(text2, text3);
				CapturedVariables570_Lookup_01.Remove(presetName);
				CapturedVariables570_Lookup_01.Remove(newName);
				CapturedVariables570_Text_02 = null;
				CapturedVariables570_Text_01 = null;
				ReloadPresets();
				NotificationLib.ShowNotification(NotificationLib.NotificationType.Saved, "Renamed " + presetName + " → " + newName);
				Main.RedrawMenu();
			}
		}
	}

	[CompilerGenerated]
	private sealed class CapturedVariables560
	{
		public string presetName;

		internal void ViewPresetMods_Lambda0()
		{
			CapturedVariables570_Text_01 = null;
			BuildPresetMenu(presetName);
			Main.CapturedVariables1950_Reference_09 = Category.Home;
			Main.CapturedVariables1950_Index_01 = -1;
			Main.RedrawMenu();
		}
	}

	[CompilerGenerated]
	private sealed class CapturedVariables570
	{
		public string presetName;

		internal void ViewPresetSettings_Lambda0()
		{
			CapturedVariables570_Text_01 = null;
			BuildPresetMenu(presetName);
			Main.CapturedVariables1950_Reference_09 = Category.Home;
			Main.CapturedVariables1950_Index_01 = -1;
			Main.RedrawMenu();
		}
	}

	private static float CapturedVariables570_Value_01;

	private static Coroutine CapturedVariables570_Routine_01;

	private const float CLICK_COOLDOWN = 0.25f;

	public static List<string> CapturedVariables570_Items_04 = new List<string>();

	public static List<Button> CapturedVariables570_Items_02 = new List<Button>();

	public static readonly List<SoundEntry> CapturedVariables570_Items_03 = new List<SoundEntry>
	{
		new SoundEntry(SoundType.AssetBundle, "NXO.Resources.thocky", "thocky", "Thocky"),
		new SoundEntry(SoundType.AssetBundle, "NXO.Resources.click1", "click", "Pop"),
		new SoundEntry(SoundType.AssetBundle, "NXO.Resources.minecraftclick", "minecraftclick", "Minecraft"),
		new SoundEntry(SoundType.HandTap, 67, "OG Button"),
		new SoundEntry(SoundType.HandTap, 66, "Key Switch")
	};

	private static readonly Dictionary<string, AudioClip> CapturedVariables570_Lookup_03 = new Dictionary<string, AudioClip>();

	private static string CapturedVariables570_Text_02;

	private static string CapturedVariables570_Text_01;

	private static readonly Dictionary<string, List<Button>> CapturedVariables570_Lookup_01 = new Dictionary<string, List<Button>>();

	public static Button CapturedVariables570_Button_01;

	public static Button CapturedVariables570_Button_02;

	public static Button CapturedVariables570_Button_03;

	public static bool CapturedVariables570_State_02;

	private const string AutoSavePrefKey = "NXO_AutoSave";

	public static bool CapturedVariables570_State_01 = false;

	private static readonly Dictionary<string, AudioClip> CapturedVariables570_Lookup_02 = new Dictionary<string, AudioClip>();

	private static readonly List<AssetBundle> CapturedVariables570_Items_01 = new List<AssetBundle>();

	private static string CustomClickSoundsFolderPath => Path.Combine(Variables.Variables_Text_01, "Custom Click Sounds");

	private static string AutoSaveDirectoryPath => Path.Combine(Variables.Variables_Text_01, "Auto Save");

	private static string FavoriteModsFilePath => Path.Combine(Variables.Variables_Text_01, "FavoriteMods.txt");

	private static string PresetsDirectoryPath => Path.Combine(Variables.Variables_Text_01, "Presets");

	public static void SetAutoSaveEnabled(bool on)
	{
		Variables.Variables_State_13 = on;
		PlayerPrefs.SetInt("NXO_AutoSave", on ? 1 : 0);
		PlayerPrefs.Save();
	}

	public static void InvokeIncrementalButtonAction(Button button)
	{
		if (button == null)
		{
			return;
		}
		if (button.buttonText.EndsWith("_DOWN"))
		{
			Button button2 = FindButton(button.buttonText.Substring(0, button.buttonText.Length - 5));
			if (button2?.down == null)
			{
				return;
			}
			InvokeButtonAction(button2.down, button2.buttonText);
		}
		else
		{
			if (!button.buttonText.EndsWith("_UP"))
			{
				return;
			}
			Button button3 = FindButton(button.buttonText.Substring(0, button.buttonText.Length - 3));
			if (button3?.up == null)
			{
				return;
			}
			InvokeButtonAction(button3.up, button3.buttonText);
		}
	}

	private static void OpenPresetSettings(string presetName)
	{
		CapturedVariables570 LocalScope4 = new CapturedVariables570();
		LocalScope4.presetName = presetName;
		CapturedVariables570_Text_01 = LocalScope4.presetName + "_Settings";
		string path = Path.Combine(PresetsDirectoryPath, LocalScope4.presetName, "Settings.txt");
		List<Button> list = new List<Button>
		{
			new Button("Return", Category.Saved_Presets, isToggle: false, isActive: false, delegate
			{
				CapturedVariables570_Text_01 = null;
				BuildPresetMenu(LocalScope4.presetName);
				Main.CapturedVariables1950_Reference_09 = Category.Home;
				Main.CapturedVariables1950_Index_01 = -1;
				Main.RedrawMenu();
			})
			{
				isCategory = true
			}
		};
		if (File.Exists(path))
		{
			string[] array = File.ReadAllLines(path);
			foreach (string label in array)
			{
				list.Add(new Button(label, Category.Saved_Presets, isToggle: false, isActive: false, null));
			}
		}
		CapturedVariables570_Lookup_01[CapturedVariables570_Text_01] = list;
		Variables.currentPage = Category.Saved_Presets;
		Main.CapturedVariables1950_Reference_09 = Category.Home;
		Main.CapturedVariables1950_Index_01 = -1;
		Main.RedrawMenu();
	}

	private static void DeletePreset(string presetName)
	{
		string path = Path.Combine(PresetsDirectoryPath, presetName);
		if (Directory.Exists(path))
		{
			Directory.Delete(path, recursive: true);
			CapturedVariables570_Lookup_01.Remove(presetName);
			CapturedVariables570_Text_02 = null;
			CapturedVariables570_Text_01 = null;
			ReloadPresets();
			NotificationLib.ShowNotification(NotificationLib.NotificationType.Deleted, "Preset `" + presetName + "`");
			Main.RedrawMenu();
		}
	}

	public static void OpenFolder(string path)
	{
		Directory.CreateDirectory(path);
		Application.OpenURL("file:///" + path.Replace("\\", "/"));
	}

	public static void LoadFavoritedMods()
	{
		if (!File.Exists(FavoriteModsFilePath))
		{
			return;
		}
		CapturedVariables570_Items_04 = File.ReadAllLines(FavoriteModsFilePath).ToList();
		using List<string>.Enumerator enumerator = CapturedVariables570_Items_04.GetEnumerator();
		if (!enumerator.MoveNext())
		{
			return;
		}
		while (true)
		{
			CapturedVariables450 LocalScope2 = new CapturedVariables450();
			LocalScope2.modName = enumerator.Current;
			Button button = ModButtons.buttons.FirstOrDefault((Button b) => b != null && b.buttonText == LocalScope2.modName);
			if (button != null && !CapturedVariables570_Items_02.Contains(button))
			{
				CapturedVariables570_Items_02.Add(button);
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

	public static void PreloadClickSounds()
	{
		foreach (SoundEntry current in CapturedVariables570_Items_03)
		{
			if (current.Type != SoundType.AssetBundle || CapturedVariables570_Lookup_02.ContainsKey(current.ClipName))
			{
				continue;
			}

			AssetBundle val = AssetHandler.LoadAssetBundle(current.AssetPath);
			if ((Object)(object)val == (Object)null)
			{
				Debug.LogError((object)("[NXO] Failed to preload bundle: '" + current.AssetPath + "'"));
				continue;
			}

			AudioClip val2 = val.LoadAsset<AudioClip>(current.ClipName);
			if ((Object)(object)val2 != (Object)null)
			{
				CapturedVariables570_Lookup_02[current.ClipName] = val2;
				CapturedVariables570_Items_01.Add(val);
			}
			else
			{
				Debug.LogError((object)("[NXO] Failed to preload clip '" + current.ClipName + "'"));
				val.Unload(true);
			}
		}
	}

	public static void SaveFavorites()
	{
		Directory.CreateDirectory(Variables.Variables_Text_01);
		File.WriteAllLines(FavoriteModsFilePath, CapturedVariables570_Items_04);
		NotificationLib.ShowNotification(NotificationLib.NotificationType.Saved, "Favorite");
	}

	private static void EnsureCustomClickSoundsInstructions()
	{
		string path = Path.Combine(CustomClickSoundsFolderPath, "Instructions.txt");
		if (!File.Exists(path))
		{
			File.WriteAllText(path, "=== HOW TO ADD CUSTOM CLICK SOUNDS ===\r\n\r\n1. Find an audio file (.mp3 or .wav)\r\n   - You can download sounds from YouTube using online converters\r\n   - Make sure it's one of these file types: MP3 or WAV\r\n\r\n2. Copy your audio file into this folder (Custom Click Sounds)\r\n   - The file name will be shown as the sound name in the menu\r\n   - Example: \"thock.wav\" will show as \"thock\" in the menu\r\n\r\n3. In-game, go to Settings > Click Sound and cycle to your custom sound\r\n   - It will appear at the end of the sound list\r\n\r\nTIPS:\r\n- Keep file names short and simple\r\n- Short sounds work best as click sounds\r\n- Don't use special characters in file names\r\n- You can add as many sounds as you want, each file becomes its own option\r\n\r\nThat's it! Have fun!");
		}
	}

	public static void PlayClickSound()
	{
		if (Settings.CapturedVariables3760_Index_43 < 0 || Settings.CapturedVariables3760_Index_43 >= CapturedVariables570_Items_03.Count)
		{
			return;
		}
		SoundEntry soundEntry = CapturedVariables570_Items_03[Settings.CapturedVariables3760_Index_43];
		if (soundEntry.Type == SoundType.AssetBundle)
		{
			if (CapturedVariables570_Lookup_02.TryGetValue(soundEntry.ClipName, out AudioClip value))
			{
				AssetHandler.PlayAudioClip(RigManager.GetHandObject, value, 0.625f);
			}
			else
			{
				Debug.LogError((object)("[NXO] Clip not preloaded: '" + soundEntry.ClipName + "'"));
			}
			return;
		}
		if (soundEntry.Type == SoundType.CustomFile)
		{
			if (string.IsNullOrEmpty(soundEntry.AssetPath))
			{
				NotificationLib.ShowNotification(NotificationLib.NotificationType.Error, "Custom Sound Is Empty. To Add One Go To `Gorilla Tag > NXO Mod Menu > Custom Click Sounds`, Paste Your Sound There And Restart Your Game.");
				return;
			}
			if (CapturedVariables570_State_01)
			{
				if (!((Object)(object)Variables.Variables_Reference_09 != (Object)null))
				{
					return;
				}
				((MonoBehaviour)Variables.Variables_Reference_09).StartCoroutine(LoadCustomClickClipCoroutine(soundEntry.AssetPath, delegate(AudioClip clip)
				{
					if (!((Object)(object)clip == (Object)null) && !((Object)(object)RigManager.GetHandObject == (Object)null))
					{
						GameObject val = new GameObject("CustomClickTemp");
						val.transform.SetParent(RigManager.GetHandObject.transform);
						val.transform.localPosition = Vector3.zero;
						AudioSource val2 = val.AddComponent<AudioSource>();
						val2.clip = clip;
						val2.volume = 0.45f;
						val2.spatialBlend = 1f;
						val2.Play();
						((MonoBehaviour)Variables.Variables_Reference_09).StartCoroutine(CoroutineHelper.DestroyAfterDelay(val, clip.length + 0.1f));
					}
				}));
				return;
			}
			if (CapturedVariables570_Routine_01 != null)
			{
				GorillaTagger obj = Variables.Variables_Reference_09;
				if (obj != null)
				{
					((MonoBehaviour)obj).StopCoroutine(CapturedVariables570_Routine_01);
					if ((Object)(object)Variables.Variables_Reference_09 != (Object)null)
					{
						goto Branch_0242;
					}
					return;
				}
			}
			if (!((Object)(object)Variables.Variables_Reference_09 != (Object)null))
			{
				return;
			}
			goto Branch_0242;
		}
		if (soundEntry.Type == SoundType.EmbeddedWav)
		{
			if (CapturedVariables570_Routine_01 != null)
			{
				GorillaTagger obj2 = Variables.Variables_Reference_09;
				if (obj2 != null)
				{
					((MonoBehaviour)obj2).StopCoroutine(CapturedVariables570_Routine_01);
					if ((Object)(object)Variables.Variables_Reference_09 != (Object)null)
					{
						goto Branch_032e;
					}
					return;
				}
			}
			if (!((Object)(object)Variables.Variables_Reference_09 != (Object)null))
			{
				return;
			}
			goto Branch_032e;
		}
		if ((Object)(object)Variables.Variables_Reference_09?.offlineVRRig != (Object)null)
		{
			Variables.Variables_Reference_09.offlineVRRig.PlayHandTapLocal(soundEntry.HandTapIndex, !Variables.Variables_State_05, 0.625f);
		}
		return;
		Branch_0242:
		CapturedVariables570_Routine_01 = ((MonoBehaviour)Variables.Variables_Reference_09).StartCoroutine(LoadCustomClickClipCoroutine(soundEntry.AssetPath, delegate(AudioClip clip)
		{
			CapturedVariables570_Routine_01 = null;
			if ((Object)(object)clip != (Object)null)
			{
				AssetHandler.PlayAudioClip(RigManager.GetHandObject, clip, 0.625f);
			}
		}));
		return;
		Branch_032e:
		CapturedVariables570_Routine_01 = ((MonoBehaviour)Variables.Variables_Reference_09).StartCoroutine(AssetHandler.LoadEmbeddedAudioClip(soundEntry.AssetPath, delegate(AudioClip clip)
		{
			CapturedVariables570_Routine_01 = null;
			if ((Object)(object)clip != (Object)null)
			{
				AssetHandler.PlayAudioClip(RigManager.GetHandObject, clip);
			}
		}));
	}

	public static void SaveAutoState()
	{
		Directory.CreateDirectory(AutoSaveDirectoryPath);
		List<string> contents = (from b in ModButtons.buttons
			where b?.Enabled ?? false
			select b.buttonText).ToList();
		File.WriteAllLines(Path.Combine(AutoSaveDirectoryPath, "Mods.txt"), contents);
		Settings.SaveSettingsToFile(Path.Combine(AutoSaveDirectoryPath, "Settings.txt"));
	}

	public static void SetMenuOpen(bool on)
	{
		Main.CapturedVariables1950_State_09 = on;
	}

	public static List<Button> GetVisibleButtons()
	{
		if (SearchAndKeyboard.KeyCollider_State_02 && !string.IsNullOrWhiteSpace(SearchAndKeyboard.KeyCollider_Text_02))
		{
			return ModButtons.buttons.Where((Button b) => b != null && b.Page != Category.Home && b.buttonText.Contains(SearchAndKeyboard.KeyCollider_Text_02, StringComparison.OrdinalIgnoreCase)).ToList();
		}
		return GetButtonsForPage(Variables.currentPage);
	}

	public static void ChangePage(bool forward)
	{
		int num;
		if (!SearchAndKeyboard.KeyCollider_State_02 || string.IsNullOrWhiteSpace(SearchAndKeyboard.KeyCollider_Text_02))
		{
			List<Button> source = GetVisibleButtons();
			num = source.Count((Button b) => b != null && Settings.IsElementSettingVisible(b.buttonText));
			if (num == 0)
			{
				return;
			}
		}
		else
		{
			List<Button> source = ModButtons.buttons.Where((Button b) => b?.buttonText.Contains(SearchAndKeyboard.KeyCollider_Text_02, StringComparison.OrdinalIgnoreCase) ?? false).ToList();
			num = source.Count((Button b) => b != null && Settings.IsElementSettingVisible(b.buttonText));
			if (num == 0)
			{
				return;
			}
		}
		int num2 = Mathf.CeilToInt((float)num / (float)Variables.Variables_Index_01);
		if (!forward)
		{
			Variables.Variables_Index_04 = (Variables.Variables_Index_04 - 1 + num2) % num2;
			Main.RedrawMenu();
		}
		else
		{
			Variables.Variables_Index_04 = (Variables.Variables_Index_04 + 1) % num2;
			Main.RedrawMenu();
		}
	}

	public static void LoadAutosavedStuff()
	{
		if (!Variables.Variables_State_13)
		{
			return;
		}
		string path = Path.Combine(AutoSaveDirectoryPath, "Mods.txt");
		string path2 = Path.Combine(AutoSaveDirectoryPath, "Settings.txt");
		if (File.Exists(path))
		{
			HashSet<string> enabledMods = File.ReadAllLines(path).ToHashSet();
			foreach (Button button in ModButtons.buttons.Where((Button b) => b != null))
			{
				bool shouldEnable = enabledMods.Contains(button.buttonText);
				if (button.Enabled == shouldEnable)
				{
					continue;
				}

				button.Enabled = shouldEnable;
				if (shouldEnable)
				{
					button.onEnable?.Invoke();
					NXOUI.TrackModEnabled(button.buttonText);
				}
				else
				{
					button.onDisable?.Invoke();
					NXOUI.TrackModDisabled(button.buttonText);
				}
			}
		}

		if (File.Exists(path2))
		{
			Settings.LoadSettings(path2);
		}
	}

	private static void OpenPreset(string presetName)
	{
		CapturedVariables570_Text_02 = presetName;
		CapturedVariables570_Text_01 = null;
		if (!CapturedVariables570_Lookup_01.ContainsKey(presetName))
		{
			BuildPresetMenu(presetName);
			Variables.currentPage = Category.Saved_Presets;
			Main.CapturedVariables1950_Reference_09 = Category.Home;
			Main.CapturedVariables1950_Index_01 = -1;
			Main.RedrawMenu();
		}
		else
		{
			Variables.currentPage = Category.Saved_Presets;
			Main.CapturedVariables1950_Reference_09 = Category.Home;
			Main.CapturedVariables1950_Index_01 = -1;
			Main.RedrawMenu();
		}
	}

	public static IEnumerator LoadCustomClickClipCoroutine(string filePath, Action<AudioClip> callback)
	{
		if (CapturedVariables570_Lookup_03.TryGetValue(filePath, out AudioClip cachedClip))
		{
			callback?.Invoke(cachedClip);
			yield break;
		}

		AudioType audioType = Path.GetExtension(filePath).Equals(".wav", StringComparison.OrdinalIgnoreCase)
			? AudioType.WAV
			: AudioType.MPEG;
		using UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip("file://" + filePath, audioType);
		yield return request.SendWebRequest();
		if (request.result != UnityWebRequest.Result.Success)
		{
			yield break;
		}

		AudioClip clip = DownloadHandlerAudioClip.GetContent(request);
		if ((Object)(object)clip == (Object)null)
		{
			yield break;
		}

		((Object)clip).name = Path.GetFileNameWithoutExtension(filePath);
		CapturedVariables570_Lookup_03[filePath] = clip;
		callback?.Invoke(clip);
	}

	public static void OpenGearMenu(Button button)
	{
		CapturedVariables570_Button_01 = button;
		NavigateToCategory(Category.Gear_Menu);
	}

	private static void OpenPresetMods(string presetName)
	{
		CapturedVariables560 LocalScope4 = new CapturedVariables560();
		LocalScope4.presetName = presetName;
		CapturedVariables570_Text_01 = LocalScope4.presetName + "_Mods";
		string path = Path.Combine(PresetsDirectoryPath, LocalScope4.presetName, "Mods.txt");
		List<Button> list = new List<Button>
		{
			new Button("Return", Category.Saved_Presets, isToggle: false, isActive: false, delegate
			{
				CapturedVariables570_Text_01 = null;
				BuildPresetMenu(LocalScope4.presetName);
				Main.CapturedVariables1950_Reference_09 = Category.Home;
				Main.CapturedVariables1950_Index_01 = -1;
				Main.RedrawMenu();
			})
			{
				isCategory = true
			}
		};
		if (File.Exists(path))
		{
			string[] array = File.ReadAllLines(path);
			foreach (string label in array)
			{
				list.Add(new Button(label, Category.Saved_Presets, isToggle: false, isActive: false, null));
			}
		}
		CapturedVariables570_Lookup_01[CapturedVariables570_Text_01] = list;
		Variables.currentPage = Category.Saved_Presets;
		Main.CapturedVariables1950_Reference_09 = Category.Home;
		Main.CapturedVariables1950_Index_01 = -1;
		Main.RedrawMenu();
	}

	private static List<Button> BuildFavoritesPage()
	{
		List<Button> list = new List<Button>
		{
			new Button("<color=blue>LG On Press To Favorite</color>", Category.Favorited, isToggle: false, isActive: false, null)
		};
		list.AddRange(CapturedVariables570_Items_02.Where((Button b) => b != null));
		return list;
	}

	public static bool IsCategoryButton(Button button)
	{
		return button.isCategory;
	}

	public static void HandleButtonClick(Button button)
	{
		if (button == null)
		{
			return;
		}
		if (button.buttonText.EndsWith("_DOWN"))
		{
			Button button2 = FindButton(button.buttonText.Substring(0, button.buttonText.Length - 5));
			if (button2?.down == null)
			{
				return;
			}
			InvokeButtonAction(button2.down, button2.buttonText);
			Main.RedrawMenu();
			return;
		}
		if (button.buttonText.EndsWith("_UP"))
		{
			Button button3 = FindButton(button.buttonText.Substring(0, button.buttonText.Length - 3));
			if (button3?.up == null)
			{
				return;
			}
			InvokeButtonAction(button3.up, button3.buttonText);
			Main.RedrawMenu();
			return;
		}
		switch (button.buttonText)
		{
		case "<":
			ChangePage(forward: false);
			break;
		case ">":
			ChangePage(forward: true);
			break;
		case "ReturnButton":
			ReturnHome();
			break;
		case "Toggle Search Button":
			SearchAndKeyboard.ToggleSearch();
			break;
		case "Disconnect Button":
			InvokeButtonAction(button.onEnable, button.buttonText);
			break;
		default:
			ExecuteButton(button);
			break;
		}
	}

	public static List<Button> GetButtonsForPage(Category page)
	{
		CapturedVariables350 LocalScope8 = new CapturedVariables350();
		LocalScope8.page = page;
		if (LocalScope8.page == Category.Element_Settings)
		{
			return Settings.BuildElementSettings();
		}
		if (LocalScope8.page == Category.Saved_Presets)
		{
			string text = CapturedVariables570_Text_01 ?? CapturedVariables570_Text_02;
			if (text != null)
			{
				if (!CapturedVariables570_Lookup_01.TryGetValue(text, out List<Button> value))
				{
					return new List<Button>();
				}
				return value;
			}
			if (LocalScope8.page == Category.Recorded_Macros)
			{
				goto Branch_00ea;
			}
		}
		else if (LocalScope8.page == Category.Recorded_Macros)
		{
			goto Branch_00ea;
		}
		goto Branch_014d;
		Branch_00ea:
		if (Macros.CapturedVariables310_Text_01 == null)
		{
			goto Branch_014d;
		}
		if (!Macros.CapturedVariables310_Lookup_01.TryGetValue(Macros.CapturedVariables310_Text_01, out List<Button> value2))
		{
			return new List<Button>();
		}
		return value2;
		Branch_014d:
		if (LocalScope8.page == Category.Custom_Nextbots && CustomNextbots.CapturedVariables181_Text_01 != null)
		{
			return CustomNextbots.BuildCustomNextbotActions();
		}
		return LocalScope8.page switch
		{
			Category.Enabled => ModButtons.buttons.Where((Button b) => b?.Enabled ?? false).ToList(), 
			Category.Favorited => BuildFavoritesPage(), 
			_ => ModButtons.buttons.Where((Button b) => b != null && b.Page == LocalScope8.page).ToList(), 
		};
	}

	private static void BuildPresetMenu(string presetName)
	{
		CapturedVariables540 LocalScope7 = new CapturedVariables540();
		LocalScope7.presetName = presetName;
		CapturedVariables570_Lookup_01[LocalScope7.presetName] = new List<Button>
		{
			new Button("Return", Category.Saved_Presets, isToggle: false, isActive: false, delegate
			{
				CapturedVariables570_Text_02 = null;
				CapturedVariables570_Text_01 = null;
				Main.CapturedVariables1950_Reference_09 = Category.Home;
				Main.CapturedVariables1950_Index_01 = -1;
				Main.RedrawMenu();
			})
			{
				isCategory = true
			},
			new Button("Load This Preset", Category.Saved_Presets, isToggle: false, isActive: false, delegate
			{
				LoadSpecificPreset(LocalScope7.presetName);
			}),
			new Button("Rename Preset", Category.Saved_Presets, isToggle: false, isActive: false, delegate
			{
				RenamePreset(LocalScope7.presetName);
			}),
			new Button("Delete This Preset", Category.Saved_Presets, isToggle: false, isActive: false, delegate
			{
				DeletePreset(LocalScope7.presetName);
			}),
			new Button("View Saved Mods", Category.Saved_Presets, isToggle: false, isActive: false, delegate
			{
				OpenPresetMods(LocalScope7.presetName);
			}),
			new Button("View Saved Settings", Category.Saved_Presets, isToggle: false, isActive: false, delegate
			{
				OpenPresetSettings(LocalScope7.presetName);
			})
		};
	}

	public static void LoadAutoSavePreference()
	{
		Variables.Variables_State_13 = PlayerPrefs.GetInt("NXO_AutoSave", 1) == 1;
	}

	public static void ReloadPresets()
	{
		List<Button> list = ModButtons.buttons.Where((Button b) => b.Page != Category.Saved_Presets).ToList();
		list.Add(new Button("Return", Category.Saved_Presets, isToggle: false, isActive: false, delegate
		{
			NavigateToCategory(Category.Presets);
		})
		{
			isCategory = true
		});
		if (Directory.Exists(PresetsDirectoryPath))
		{
			string[] directories = Directory.GetDirectories(PresetsDirectoryPath);
			foreach (string path in directories)
			{
				CapturedVariables520 LocalScope3 = new CapturedVariables520();
				LocalScope3.presetName = Path.GetFileName(path);
				list.Add(new Button(LocalScope3.presetName, Category.Saved_Presets, isToggle: false, isActive: false, delegate
				{
					OpenPreset(LocalScope3.presetName);
				}));
			}
			ModButtons.buttons = list.ToArray();
		}
		else
		{
			ModButtons.buttons = list.ToArray();
		}
	}

	public static void ExecuteButton(Button button, bool redraw = true)
	{
		if (button == null)
		{
			return;
		}
		if (!button.isToggle)
		{
			InvokeButtonAction(button.onEnable, button.buttonText);
			if (button.Page != Variables.currentPage)
			{
				NotificationLib.ShowNotification(NotificationLib.NotificationType.Info, button.buttonText);
			}
			else
			{
				NotificationLib.ShowNotification(NotificationLib.NotificationType.Enabled, button.buttonText);
			}
			return;
		}
		button.Enabled = !button.Enabled;
		if (button.Enabled)
		{
			InvokeButtonAction(button.onEnable, button.buttonText);
			NotificationLib.ShowNotification(NotificationLib.NotificationType.Enabled, button.buttonText);
			NXOUI.TrackModEnabled(button.buttonText);
			if (!redraw)
			{
				return;
			}
		}
		else
		{
			InvokeButtonAction(button.onDisable, button.buttonText);
			NotificationLib.ShowNotification(NotificationLib.NotificationType.Disabled, button.buttonText);
			NXOUI.TrackModDisabled(button.buttonText);
			if (!redraw)
			{
				return;
			}
		}
		Main.RedrawMenu();
	}

	private static void LoadSpecificPreset(string presetName)
	{
		string path = Path.Combine(PresetsDirectoryPath, presetName);
		string path2 = Path.Combine(path, "Mods.txt");
		string path3 = Path.Combine(path, "Settings.txt");
		if (File.Exists(path2))
		{
			HashSet<string> enabledMods = File.ReadAllLines(path2).ToHashSet();
			foreach (Button button in ModButtons.buttons.Where((Button b) => b != null))
			{
				bool shouldEnable = enabledMods.Contains(button.buttonText);
				if (button.Enabled == shouldEnable)
				{
					continue;
				}

				button.Enabled = shouldEnable;
				if (shouldEnable)
				{
					button.onEnable?.Invoke();
					NXOUI.TrackModEnabled(button.buttonText);
				}
				else
				{
					button.onDisable?.Invoke();
					NXOUI.TrackModDisabled(button.buttonText);
				}
			}
		}

		if (File.Exists(path3))
		{
			Settings.LoadSettings(path3);
		}
		Main.RebuildMenu();
		NotificationLib.ShowNotification(NotificationLib.NotificationType.Loaded, "Preset `" + presetName + "`");
	}

	public static void LoadCustomClickSounds()
	{
		foreach (AudioClip clip in CapturedVariables570_Lookup_03.Values)
		{
			if ((Object)(object)clip != (Object)null)
			{
				Object.Destroy((Object)(object)clip);
			}
		}
		CapturedVariables570_Lookup_03.Clear();
		CapturedVariables570_Items_03.RemoveAll((SoundEntry s) => s.Type == SoundType.CustomFile);
		if (!Directory.Exists(CustomClickSoundsFolderPath))
		{
			Directory.CreateDirectory(CustomClickSoundsFolderPath);
			EnsureCustomClickSoundsInstructions();
		}

		string[] files = Directory.GetFiles(CustomClickSoundsFolderPath, "*.*")
			.Where(f => f.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase) ||
				f.EndsWith(".wav", StringComparison.OrdinalIgnoreCase))
			.ToArray();
		foreach (string resourcePath in files)
		{
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(resourcePath);
			CapturedVariables570_Items_03.Add(new SoundEntry(SoundType.CustomFile, resourcePath, "", fileNameWithoutExtension));
		}

		if (files.Length == 0)
		{
			CapturedVariables570_Items_03.Add(new SoundEntry(SoundType.CustomFile, "", "", "Custom Placeholder"));
		}
	}

	private static Button FindButton(string targetName)
	{
		CapturedVariables280 LocalScope4 = new CapturedVariables280();
		LocalScope4.targetName = targetName;
		if (Variables.currentPage == Category.Element_Settings)
		{
			Button button = Settings.BuildElementSettings().FirstOrDefault((Button b) => b != null && b.buttonText == LocalScope4.targetName);
			if (button != null)
			{
				return button;
			}
			return ModButtons.buttons.FirstOrDefault((Button b) => b != null && b.buttonText == LocalScope4.targetName);
		}
		return ModButtons.buttons.FirstOrDefault((Button b) => b != null && b.buttonText == LocalScope4.targetName);
	}

	private static void InvokeButtonAction(Action action, string label)
	{
		if (action == null)
		{
			return;
		}
		try
		{
			action();
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("NXO action error '" + label + "': " + ex.Message + "\n" + ex.StackTrace));
			NotificationLib.ShowNotification(NotificationLib.NotificationType.Error, "`" + label + "` failed : " + ex.Message);
		}
	}

	private static void ReturnHome()
	{
		Variables.currentPage = Category.Home;
		Variables.Variables_Index_04 = 0;
		PlayersActionList.ClearPlayerCamera(clearAll: true);
		Main.RedrawMenu(-1);
	}

	public static void ClearCustomClickSoundCache()
	{
		using (Dictionary<string, AudioClip>.Enumerator enumerator = CapturedVariables570_Lookup_03.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				while (true)
				{
					KeyValuePair<string, AudioClip> current = enumerator.Current;
					if (!((Object)(object)current.Value != (Object)null))
					{
						break;
					}
					Object.Destroy((Object)(object)current.Value);
					if (!enumerator.MoveNext())
					{
						goto EndBranch_006a;
					}
				}
				continue;
				EndBranch_006a:
				break;
			}
		}
		CapturedVariables570_Lookup_03.Clear();
	}

	private static void RenamePreset(string presetName)
	{
		CapturedVariables550 LocalScope6 = new CapturedVariables550();
		LocalScope6.presetName = presetName;
		SearchAndKeyboard.OpenTextInput(LocalScope6.presetName, "Enter preset name...");
		SearchAndKeyboard.KeyCollider_Text_01 = delegate(string newName)
		{
			string text = newName.Trim();
			newName = text;
			if (!string.IsNullOrEmpty(newName) && !(newName == LocalScope6.presetName))
			{
				string text2 = Path.Combine(PresetsDirectoryPath, LocalScope6.presetName);
				string text3 = Path.Combine(PresetsDirectoryPath, newName);
				if (Directory.Exists(text2))
				{
					if (Directory.Exists(text3))
					{
						NotificationLib.ShowNotification(NotificationLib.NotificationType.Error, "Preset Name Already Exists");
					}
					else
					{
						Directory.Move(text2, text3);
						CapturedVariables570_Lookup_01.Remove(LocalScope6.presetName);
						CapturedVariables570_Lookup_01.Remove(newName);
						CapturedVariables570_Text_02 = null;
						CapturedVariables570_Text_01 = null;
						ReloadPresets();
						NotificationLib.ShowNotification(NotificationLib.NotificationType.Saved, "Renamed " + LocalScope6.presetName + " → " + newName);
						Main.RedrawMenu();
					}
				}
			}
		};
	}

	public static void DisableAllMods()
	{
		using (IEnumerator<Button> enumerator = ModButtons.buttons.Where((Button b) => b?.Enabled ?? false).GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				while (true)
				{
					Button current = enumerator.Current;
					current.Enabled = false;
					Action onDisable = current.onDisable;
					if (onDisable != null)
					{
						onDisable();
						NXOUI.TrackModDisabled(current.buttonText);
						if (!enumerator.MoveNext())
						{
							break;
						}
					}
					else
					{
						NXOUI.TrackModDisabled(current.buttonText);
						if (!enumerator.MoveNext())
						{
							break;
						}
					}
				}
			}
		}
		Main.RebuildMenu();
	}

	public static void SavePreset()
	{
		Directory.CreateDirectory(PresetsDirectoryPath);
		string text = $"Preset_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}";
		string path = Path.Combine(PresetsDirectoryPath, text);
		Directory.CreateDirectory(path);
		List<string> contents = (from b in ModButtons.buttons
			where b?.Enabled ?? false
			select b.buttonText).ToList();
		File.WriteAllLines(Path.Combine(path, "Mods.txt"), contents);
		Settings.SaveSettingsToFile(Path.Combine(path, "Settings.txt"));
		NotificationLib.ShowNotification(NotificationLib.NotificationType.Saved, "Preset `" + text + "`");
		ReloadPresets();
	}

	public static void NavigateToCategory(Category newPage)
	{
		Variables.currentPage = newPage;
		Variables.Variables_Index_04 = 0;
		Main.CapturedVariables1950_Reference_09 = Category.Home;
		Main.CapturedVariables1950_Index_01 = -1;
		switch (newPage)
		{
		case Category.Players:
			PlayersActionList.ResetPlayersList();
			break;
		case Category.Player_Action:
			if (PlayersActionList.CapturedVariables70_Reference_03 == null)
			{
				break;
			}
			PlayersActionList.BuildPlayerActions();
			PlayersActionList.CreatePlayerCameraDisplay();
			Main.RedrawMenu(1);
			return;
		}
		Main.RedrawMenu(1);
	}
}
