using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BepInEx;
using NXO.Mods.Categories;
using NXO.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace NXO.Menu;

public class SearchAndKeyboard : MonoBehaviour
{
	public class KeyCollider : MonoBehaviour
	{
		public string key;

		public Vector3 baseScale;

		public GameObject roundedHolder;

		private Coroutine _bounceRoutine;

		private static float KeyCollider_Value_02;

		private void OnTriggerEnter(Collider collider)
		{
			if (!(((Object)((Component)collider).gameObject).name != "keyclicker") && !((float)Time.frameCount < KeyCollider_Value_02 + 12.5f))
			{
				KeyCollider_Value_02 = Time.frameCount;
				HandleKeyPress(key);
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
			return Mathf.Pow(2f, -10f * t) * Mathf.Sin((t - 0.1f) * (MathF.PI * 2f) / 0.4f) + 1f;
		}

		private IEnumerator BounceRoutine()
		{
			Transform target = ((Object)(object)roundedHolder != (Object)null) ? roundedHolder.transform : ((Component)this).transform;
			Vector3 targetBase = ((Object)(object)roundedHolder != (Object)null) ? Vector3.one : baseScale;
			Vector3 punched = new Vector3(targetBase.x * 1.25f, targetBase.y * 1.25f, targetBase.z * 0.7f);
			const float pressDuration = 0.063f;
			float elapsed = 0f;

			while (elapsed < pressDuration)
			{
				if ((Object)(object)this == (Object)null)
				{
					yield break;
				}

				float progress = elapsed / pressDuration;
				Vector3 scale = Vector3.LerpUnclamped(targetBase, punched, 1f - Mathf.Pow(1f - progress, 3f));
				target.localScale = scale;
				ApplyHolderScale(scale);
				elapsed += Time.deltaTime;
				yield return null;
			}

			const float releaseDuration = 0.117f;
			elapsed = 0f;
			while (elapsed < releaseDuration)
			{
				if ((Object)(object)this == (Object)null)
				{
					yield break;
				}

				float progress = elapsed / releaseDuration;
				Vector3 scale = Vector3.LerpUnclamped(punched, targetBase, EaseOutElastic(progress));
				target.localScale = scale;
				ApplyHolderScale(scale);
				elapsed += Time.deltaTime;
				yield return null;
			}

			if ((Object)(object)this != (Object)null)
			{
				target.localScale = targetBase;
				((Component)this).transform.localScale = baseScale;
			}
			_bounceRoutine = null;
		}

		private void ApplyHolderScale(Vector3 scale)
		{
			if ((Object)(object)roundedHolder != (Object)null)
			{
				((Component)this).transform.localScale = new Vector3(scale.x * baseScale.x, scale.y * baseScale.y, scale.z * baseScale.z);
			}
		}

		public void TriggerBounce()
		{
			if (((Component)this).gameObject.activeInHierarchy)
			{
				if (_bounceRoutine != null)
				{
					((MonoBehaviour)this).StopCoroutine(_bounceRoutine);
					_bounceRoutine = ((MonoBehaviour)this).StartCoroutine(BounceRoutine());
				}
				else
				{
					_bounceRoutine = ((MonoBehaviour)this).StartCoroutine(BounceRoutine());
				}
			}
		}
	}

	public static Text KeyCollider_Reference_01;

	public static string KeyCollider_Text_02 = "";

	public static GameObject KeyCollider_Object_01;

	public static bool KeyCollider_State_02;

	public static bool KeyCollider_State_01;

	public static bool KeyCollider_State_04;

	private static Material KeyCollider_Material_03;

	private static Material KeyCollider_Material_02;

	private static readonly List<Material> KeyCollider_Items_04 = new List<Material>();

	private static readonly List<GameObject> KeyCollider_Items_02 = new List<GameObject>();

	private static readonly List<Mesh> KeyCollider_Items_01 = new List<Mesh>();

	public static List<KeyCode> KeyCollider_Items_03 = new List<KeyCode>();

	private static readonly Dictionary<string, KeyCollider> KeyCollider_Lookup_01 = new Dictionary<string, KeyCollider>();

	private static readonly List<(Main.ColorRole role, GameObject obj, List<GameObject> parts)> Recovered_Reference_08 = new List<(Main.ColorRole, GameObject, List<GameObject>)>();

	private const float KeyBouncePunch = 1.25f;

	private const float KeyBounceDuration = 0.18f;

	private static readonly KeyCode[] KeyCollider_Values_01;

	public static string KeyCollider_Text_03;

	public static float KeyCollider_Value_03;

	private static float KeyCollider_Value_01;

	private static bool KeyCollider_State_03;

	public static Action<string> KeyCollider_Text_01;

	public static Action KeyCollider_Callback_01;

	private static Material KeyCollider_Material_01;

	private static Material KeyCollider_Material_04;

	public static KeyCode[] allowedKeys => KeyCollider_Values_01;

	private static Material CreateKeyboardBackgroundMaterial()
	{
		Material val2;
		if (Settings.CapturedVariables3760_Color_10 == Settings.ColorMode.Pinwheel)
		{
			Material val = Main.CreatePinwheelMaterial();
			if ((Object)(object)val != (Object)null)
			{
				KeyCollider_Items_04.Add(val);
				return val;
			}
			val2 = CreateUiMaterial((Color32)(Settings.CapturedVariables3760_Color_19), 2450);
			KeyCollider_Items_04.Add(val2);
			return val2;
		}
		val2 = CreateUiMaterial((Color32)(Settings.CapturedVariables3760_Color_19), 2450);
		KeyCollider_Items_04.Add(val2);
		return val2;
	}

	private static Material CreateUiMaterial(Color color, int renderQueue)
	{
		return Main.CreateMaterial(color, renderQueue);
	}

	public static void CloseSearch()
	{
		KeyCollider_State_02 = false;
		KeyCollider_State_04 = false;
		KeyCollider_Text_01 = null;
		KeyCollider_Callback_01 = null;
		KeyCollider_State_01 = false;
		Main.CapturedVariables1950_Value_07 = float.MinValue;
		Main.CapturedVariables1950_Position_11 = Vector3.zero;
		if (KeyCollider_Value_03 > 0f)
		{
			Main.CapturedVariables1950_Value_04 = KeyCollider_Value_03;
			KeyCollider_Value_03 = -1f;
			DestroyKeyboard();
			Main.ClearMenuPageObjects();
		}
		else
		{
			DestroyKeyboard();
			Main.ClearMenuPageObjects();
		}
	}

	public static void CreateLeftKeyClicker(Transform parentTransform)
	{
		CreateKeyClicker(ref Variables.Variables_Object_01, parentTransform, ref KeyCollider_Material_01);
	}

	public static int CalculateSearchScore(string text, string query)
	{
		if (string.IsNullOrEmpty(query))
		{
			return 0;
		}
		if (string.IsNullOrEmpty(text))
		{
			return int.MinValue;
		}
		string text2 = text.ToLowerInvariant();
		string text3 = query.ToLowerInvariant();
		int num = text2.IndexOf(text3);
		if (num == 0)
		{
			return 10000;
		}
		if (num > 0)
		{
			return 5000 - num;
		}
		int num2 = 0;
		int num3 = 0;
		int num4 = -1;
		int num5 = 0;
		int num6 = 0;
		if (num6 < text2.Length)
		{
			while (num3 < text3.Length)
			{
				if (text2[num6] == text3[num3])
				{
					if (num4 == num6 - 1)
					{
						num5++;
						num2 += 10 + num5 * 8;
						if (num6 != 0)
						{
							goto Branch_016a;
						}
					}
					else
					{
						num5 = 0;
						num2 += 10 + num5 * 8;
						if (num6 != 0)
						{
							goto Branch_016a;
						}
					}
					goto Branch_01a0;
				}
				num6++;
				if (num6 < text2.Length)
				{
					continue;
				}
				break;
				Branch_01a0:
				num2 += 15;
				num4 = num6;
				num3++;
				num6++;
				if (num6 < text2.Length)
				{
					continue;
				}
				break;
				Branch_016a:
				if (text2[num6 - 1] == ' ')
				{
					goto Branch_01a0;
				}
				num4 = num6;
				num3++;
				num6++;
				if (num6 < text2.Length)
				{
					continue;
				}
				break;
			}
		}
		if (num3 < text3.Length)
		{
			return int.MinValue;
		}
		return num2 - (text2.Length - text3.Length);
	}

	private static Material CreateKeyMaterial()
	{
		Material val2;
		if (Settings.CapturedVariables3760_Color_18 == Settings.ColorMode.Pinwheel)
		{
			Material val = Main.CreatePinwheelMaterial();
			if ((Object)(object)val != (Object)null)
			{
				KeyCollider_Items_04.Add(val);
				return val;
			}
			val2 = CreateUiMaterial((Color32)(Settings.CapturedVariables3760_Color_26), 2460);
			KeyCollider_Items_04.Add(val2);
			return val2;
		}
		val2 = CreateUiMaterial((Color32)(Settings.CapturedVariables3760_Color_26), 2460);
		KeyCollider_Items_04.Add(val2);
		return val2;
	}

	private static void CreateKeyClicker(ref GameObject keyclickerObj, Transform parentTransform, ref Material clickerMaterial)
	{
		if (!((Object)(object)keyclickerObj != (Object)null))
		{
			keyclickerObj = new GameObject("keyclicker");
			((Collider)keyclickerObj.AddComponent<BoxCollider>()).isTrigger = true;
			keyclickerObj.layer = LayerMask.NameToLayer("UI");
			keyclickerObj.AddComponent<MeshFilter>().mesh = Resources.GetBuiltinResource<Mesh>("Sphere.fbx");
			clickerMaterial = new Material(Variables.Variables_Reference_10)
			{
				color = Color.white
			};
			((Renderer)keyclickerObj.AddComponent<MeshRenderer>()).material = clickerMaterial;
			if ((Object)(object)parentTransform != (Object)null)
			{
				keyclickerObj.transform.SetParent(parentTransform);
				keyclickerObj.transform.localScale = new Vector3(0.0035f, 0.0035f, 0.0035f);
				keyclickerObj.transform.localPosition = new Vector3(0f, -0.1f, 0f);
			}
		}
	}

	public static void CreateRightKeyClicker(Transform parentTransform)
	{
		CreateKeyClicker(ref Variables.Variables_Object_11, parentTransform, ref KeyCollider_Material_04);
	}

	static SearchAndKeyboard()
	{
		KeyCollider_Values_01 = new KeyCode[39]
		{
			KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.I, KeyCode.J,
			KeyCode.K, KeyCode.L, KeyCode.M, KeyCode.N, KeyCode.O, KeyCode.P, KeyCode.Q, KeyCode.R, KeyCode.S, KeyCode.T,
			KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X, KeyCode.Y, KeyCode.Z, KeyCode.Space, KeyCode.Backspace, KeyCode.Return,
			KeyCode.Alpha0, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6,
			KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9
		};
		KeyCollider_Text_03 = "Type to search...";
		KeyCollider_Value_03 = -1f;
		KeyCollider_State_03 = true;
	}

	public static void DestroyKeyboard()
	{
		if ((Object)(object)KeyCollider_Object_01 != (Object)null)
		{
			Object.Destroy((Object)(object)KeyCollider_Object_01);
			KeyCollider_Object_01 = null;
			if ((Object)(object)Variables.Variables_Object_01 != (Object)null)
			{
				goto Branch_006d;
			}
		}
		else if ((Object)(object)Variables.Variables_Object_01 != (Object)null)
		{
			goto Branch_006d;
		}
		if (!((Object)(object)Variables.Variables_Object_11 != (Object)null))
		{
			goto Branch_00e0;
		}
		goto Branch_00bb;
		Branch_0322:
		Object.Destroy((Object)(object)KeyCollider_Material_04);
		KeyCollider_Material_04 = null;
		KeyCollider_Material_03 = null;
		KeyCollider_Material_02 = null;
		Recovered_Reference_08.Clear();
		KeyCollider_Lookup_01.Clear();
		KeyCollider_Text_02 = "";
		return;
		Branch_00f5:
		List<Material>.Enumerator enumerator;
		try
		{
			while (enumerator.MoveNext())
			{
				while (true)
				{
					Material current = enumerator.Current;
					if (!((Object)(object)current != (Object)null))
					{
						break;
					}
					Object.Destroy((Object)(object)current);
					if (!enumerator.MoveNext())
					{
						goto EndBranch_013e;
					}
				}
				continue;
				EndBranch_013e:
				break;
			}
		}
		finally
		{
			((IDisposable)enumerator/*cast due to constrained. prefix*/).Dispose();
		}
		KeyCollider_Items_04.Clear();
		using (List<GameObject>.Enumerator enumerator2 = KeyCollider_Items_02.GetEnumerator())
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
						goto EndBranch_01d9;
					}
				}
				continue;
				EndBranch_01d9:
				break;
			}
		}
		KeyCollider_Items_02.Clear();
		using (List<Mesh>.Enumerator enumerator3 = KeyCollider_Items_01.GetEnumerator())
		{
			while (enumerator3.MoveNext())
			{
				while (true)
				{
					Mesh current3 = enumerator3.Current;
					if (!((Object)(object)current3 != (Object)null))
					{
						break;
					}
					Object.Destroy((Object)(object)current3);
					if (!enumerator3.MoveNext())
					{
						goto EndBranch_0278;
					}
				}
				continue;
				EndBranch_0278:
				break;
			}
		}
		KeyCollider_Items_01.Clear();
		if ((Object)(object)KeyCollider_Material_01 != (Object)null)
		{
			Object.Destroy((Object)(object)KeyCollider_Material_01);
			KeyCollider_Material_01 = null;
			if ((Object)(object)KeyCollider_Material_04 != (Object)null)
			{
				goto Branch_0322;
			}
		}
		else if ((Object)(object)KeyCollider_Material_04 != (Object)null)
		{
			goto Branch_0322;
		}
		KeyCollider_Material_03 = null;
		KeyCollider_Material_02 = null;
		Recovered_Reference_08.Clear();
		KeyCollider_Lookup_01.Clear();
		KeyCollider_Text_02 = "";
		return;
		Branch_006d:
		Object.Destroy((Object)(object)Variables.Variables_Object_01);
		Variables.Variables_Object_01 = null;
		if (!((Object)(object)Variables.Variables_Object_11 != (Object)null))
		{
			goto Branch_00e0;
		}
		goto Branch_00bb;
		Branch_00e0:
		enumerator = KeyCollider_Items_04.GetEnumerator();
		goto Branch_00f5;
		Branch_00bb:
		Object.Destroy((Object)(object)Variables.Variables_Object_11);
		Variables.Variables_Object_11 = null;
		enumerator = KeyCollider_Items_04.GetEnumerator();
		goto Branch_00f5;
	}

	public static void SubmitKeyboardInput(bool cancelled = false)
	{
		string obj = KeyCollider_Text_02.Trim();
		KeyCollider_State_02 = false;
		KeyCollider_State_04 = false;
		KeyCollider_State_01 = false;
		Main.CapturedVariables1950_Value_07 = float.MinValue;
		Main.CapturedVariables1950_Position_11 = Vector3.zero;
		Action<string> eBLJE9FY;
		Action iEOQF15M;
		if (KeyCollider_Value_03 > 0f)
		{
			Main.CapturedVariables1950_Value_04 = KeyCollider_Value_03;
			KeyCollider_Value_03 = -1f;
			eBLJE9FY = KeyCollider_Text_01;
			iEOQF15M = KeyCollider_Callback_01;
			KeyCollider_Text_01 = null;
			KeyCollider_Callback_01 = null;
			DestroyKeyboard();
			Main.RebuildMenu();
			if (cancelled)
			{
				goto Branch_00d3;
			}
		}
		else
		{
			eBLJE9FY = KeyCollider_Text_01;
			iEOQF15M = KeyCollider_Callback_01;
			KeyCollider_Text_01 = null;
			KeyCollider_Callback_01 = null;
			DestroyKeyboard();
			Main.RebuildMenu();
			if (cancelled)
			{
				goto Branch_00d3;
			}
		}
		eBLJE9FY?.Invoke(obj);
		return;
		Branch_00d3:
		iEOQF15M?.Invoke();
	}

	public static void PollPhysicalKeyboard()
	{
		if (!Variables.Variables_State_15)
		{
			return;
		}
		if (!KeyCollider_State_02)
		{
			if (!KeyCollider_State_04)
			{
				return;
			}
		}
		KeyCollider_Items_03.Clear();
		int num = 0;
		if (num >= allowedKeys.Length)
		{
			return;
		}
		while (true)
		{
			if (UnityInput.Current.GetKeyDown(allowedKeys[num]))
			{
				HandleKeyPress(KeyCodeToText(allowedKeys[num]));
				KeyCollider_Items_03.Add(allowedKeys[num]);
				if (num + 1 >= allowedKeys.Length)
				{
					break;
				}
			}
			else if (num + 1 >= allowedKeys.Length)
			{
				break;
			}
		}
	}

	public static void HandleKeyPress(string key)
	{
		if ((Object)(object)KeyCollider_Reference_01 == (Object)null)
		{
			return;
		}
		string text2;
		if (KeyCollider_Lookup_01.TryGetValue(key, out KeyCollider value) && (Object)(object)value != (Object)null)
		{
			value.TriggerBounce();
			string text = key;
			text2 = text;
			if (!(text2 == "SPACE"))
			{
				goto Branch_00b1;
			}
		}
		else
		{
			string text = key;
			text2 = text;
			if (!(text2 == "SPACE"))
			{
				goto Branch_00b1;
			}
		}
		KeyCollider_Text_02 += " ";
		Branch_01ae:
		Variables.Variables_Reference_09.offlineVRRig.PlayHandTapLocal(66, true, 0.625f);
		Variables.Variables_Reference_09.offlineVRRig.PlayHandTapLocal(66, false, 0.625f);
		KeyCollider_Reference_01.text = KeyCollider_Text_02;
		Variables.Variables_Index_04 = 0;
		if (KeyCollider_State_02 && !KeyCollider_State_04)
		{
			Main.RedrawMenu();
		}
		return;
		Branch_00b1:
		if (!(text2 == "BACK"))
		{
			if (text2 == "ENTER")
			{
				if (KeyCollider_State_04)
				{
					SubmitKeyboardInput();
				}
				else
				{
					CloseSearch();
				}
				return;
			}
			KeyCollider_Text_02 += key;
		}
		else if (KeyCollider_Text_02.Length > 0)
		{
			string y0HFWAXU = KeyCollider_Text_02;
			KeyCollider_Text_02 = y0HFWAXU.Substring(0, y0HFWAXU.Length - 1);
		}
		goto Branch_01ae;
	}

	public static void RefreshKeyboardColors()
	{
		using List<(Main.ColorRole, GameObject, List<GameObject>)>.Enumerator enumerator = Recovered_Reference_08.GetEnumerator();
		while (enumerator.MoveNext())
		{
			while (true)
			{
				var (role, val, extraParts) = enumerator.Current;
				if (!((Object)(object)val != (Object)null))
				{
					break;
				}
				Main.RegisterColorGroup(role, val, extraParts);
				if (!enumerator.MoveNext())
				{
					return;
				}
			}
		}
	}

	public static void BuildKeyboard()
	{
		List<Material>.Enumerator enumerator;
		if ((Object)(object)KeyCollider_Object_01 != (Object)null)
		{
			Object.Destroy((Object)(object)KeyCollider_Object_01);
			KeyCollider_Object_01 = null;
			enumerator = KeyCollider_Items_04.GetEnumerator();
		}
		else
		{
			enumerator = KeyCollider_Items_04.GetEnumerator();
		}
		try
		{
			while (enumerator.MoveNext())
			{
				while (true)
				{
					Material current = enumerator.Current;
					if (!((Object)(object)current != (Object)null))
					{
						break;
					}
					Object.Destroy((Object)(object)current);
					if (!enumerator.MoveNext())
					{
						goto EndBranch_00a2;
					}
				}
				continue;
				EndBranch_00a2:
				break;
			}
		}
		finally
		{
			((IDisposable)enumerator/*cast due to constrained. prefix*/).Dispose();
		}
		KeyCollider_Items_04.Clear();
		using (List<GameObject>.Enumerator enumerator2 = KeyCollider_Items_02.GetEnumerator())
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
						goto EndBranch_013d;
					}
				}
				continue;
				EndBranch_013d:
				break;
			}
		}
		KeyCollider_Items_02.Clear();
		using (List<Mesh>.Enumerator enumerator3 = KeyCollider_Items_01.GetEnumerator())
		{
			while (enumerator3.MoveNext())
			{
				while (true)
				{
					Mesh current3 = enumerator3.Current;
					if (!((Object)(object)current3 != (Object)null))
					{
						break;
					}
					Object.Destroy((Object)(object)current3);
					if (!enumerator3.MoveNext())
					{
						goto EndBranch_01dc;
					}
				}
				continue;
				EndBranch_01dc:
				break;
			}
		}
		KeyCollider_Items_01.Clear();
		Recovered_Reference_08.Clear();
		KeyCollider_Lookup_01.Clear();
		KeyCollider_Object_01 = new GameObject("Keyboard");
		GameObject val2;
		List<GameObject> parts2;
		if (Settings.CapturedVariables3760_Index_32 > 0)
		{
			GameObject val = CreatePanelPrimitive(KeyCollider_Object_01.transform);
			((Object)val).name = "KeyboardOutline";
			val.transform.localScale = new Vector3(0.5f, 0.255f, 0.004f);
			val.transform.localPosition = new Vector3(0f, 0.002f, 0.006f);
			KeyCollider_Material_02 = CreateKeyboardOutlineMaterial();
			KeyCollider_Items_04.Add(KeyCollider_Material_02);
			val.GetComponent<Renderer>().sharedMaterial = KeyCollider_Material_02;
			List<GameObject> parts = null;
			if (Settings.CapturedVariables3760_Value_01 > 0f)
			{
				parts = RoundPanel(val, 0.015f * Settings.CapturedVariables3760_Value_01);
				RegisterColorTarget(Main.ColorRole.Outline, val, parts);
				val2 = CreatePanelPrimitive(KeyCollider_Object_01.transform);
				((Object)val2).name = "KeyboardBackground";
				val2.transform.localScale = new Vector3(0.485f, 0.24f, 0.005f);
				val2.transform.localPosition = new Vector3(0f, 0.002f, 0.005f);
				KeyCollider_Material_03 = CreateKeyboardBackgroundMaterial();
				val2.GetComponent<Renderer>().sharedMaterial = KeyCollider_Material_03;
				parts2 = null;
				if (Settings.CapturedVariables3760_Value_01 > 0f)
				{
					goto Branch_04fb;
				}
			}
			else
			{
				RegisterColorTarget(Main.ColorRole.Outline, val, parts);
				val2 = CreatePanelPrimitive(KeyCollider_Object_01.transform);
				((Object)val2).name = "KeyboardBackground";
				val2.transform.localScale = new Vector3(0.485f, 0.24f, 0.005f);
				val2.transform.localPosition = new Vector3(0f, 0.002f, 0.005f);
				KeyCollider_Material_03 = CreateKeyboardBackgroundMaterial();
				val2.GetComponent<Renderer>().sharedMaterial = KeyCollider_Material_03;
				parts2 = null;
				if (Settings.CapturedVariables3760_Value_01 > 0f)
				{
					goto Branch_04fb;
				}
			}
		}
		else
		{
			val2 = CreatePanelPrimitive(KeyCollider_Object_01.transform);
			((Object)val2).name = "KeyboardBackground";
			val2.transform.localScale = new Vector3(0.485f, 0.24f, 0.005f);
			val2.transform.localPosition = new Vector3(0f, 0.002f, 0.005f);
			KeyCollider_Material_03 = CreateKeyboardBackgroundMaterial();
			val2.GetComponent<Renderer>().sharedMaterial = KeyCollider_Material_03;
			parts2 = null;
			if (Settings.CapturedVariables3760_Value_01 > 0f)
			{
				goto Branch_04fb;
			}
		}
		RegisterColorTarget(Main.ColorRole.Background, val2, parts2);
		string[] array = new string[4] { "1234567890", "QWERTYUIOP", "ASDFGHJKL", "ZXCVBNM" };
		float num = 0.04f;
		float num2 = 0.005f;
		float num3 = 0.092f;
		string[] array2 = array;
		int i = 0;
		goto Branch_0688;
		Branch_04fb:
		parts2 = RoundPanel(val2, 0.015f * Settings.CapturedVariables3760_Value_01);
		RegisterColorTarget(Main.ColorRole.Background, val2, parts2);
		array = new string[4] { "1234567890", "QWERTYUIOP", "ASDFGHJKL", "ZXCVBNM" };
		num = 0.04f;
		num2 = 0.005f;
		num3 = 0.092f;
		array2 = array;
		i = 0;
		Branch_0688:
		for (; i < array2.Length; i++)
		{
			string text = array2[i];
			float num4 = 0f - ((float)array2[i].Length * num + (float)(text.Length - 1) * num2) / 2f + num / 2f;
			string text2 = text;
			int num5 = 0;
			if (num5 < text2.Length)
			{
				do
				{
					CreateKey(text2[num5].ToString(), new Vector3(num4, num3, 0f), num);
					num4 += num + num2;
					num5++;
				}
				while (num5 < text2.Length);
			}
			num3 -= num + num2;
		}
		float num6 = num3;
		float num7 = num * 5f;
		float num8 = num * 2.1f;
		float num9 = num * 2.1f;
		float num10 = 0f - (num7 + num2 + num8 + num2 + num9) / 2f;
		CreateKey("SPACE", new Vector3(num10 + num7 / 2f, num6, 0f), num7);
		CreateKey("BACK", new Vector3(num10 + num7 + num2 + num8 / 2f, num6, 0f), num8);
		CreateKey("ENTER", new Vector3(num10 + num7 + num2 + num8 + num2 + num9 / 2f, num6, 0f), num9);
	}

	private static List<GameObject> RoundPanel(GameObject panel, float roundness)
	{
		List<GameObject> list = new List<GameObject>(1);
		if ((Object)(object)panel == (Object)null)
		{
			return list;
		}
		Renderer component = panel.GetComponent<Renderer>();
		if ((Object)(object)component == (Object)null)
		{
			return list;
		}
		Transform val = panel.transform.parent ?? KeyCollider_Object_01.transform;
		GameObject val2 = new GameObject("rounded_" + ((Object)panel).name);
		val2.transform.SetParent(val, false);
		val2.transform.localPosition = panel.transform.localPosition;
		val2.transform.localRotation = panel.transform.localRotation;
		val2.transform.localScale = Vector3.one;
		val2.layer = panel.layer;
		Mesh val3 = CreateRoundedBoxMesh(panel.transform.localScale, roundness);
		KeyCollider_Items_01.Add(val3);
		val2.AddComponent<MeshFilter>().mesh = val3;
		Material val4 = new Material(component.sharedMaterial);
		val4.SetInt("_Cull", 0);
		((Renderer)val2.AddComponent<MeshRenderer>()).sharedMaterial = val4;
		KeyCollider_Items_04.Add(val4);
		KeyCollider_Items_02.Add(val2);
		component.enabled = false;
		list.Add(val2);
		return list;
	}

	private static Mesh CreateRoundedBoxMesh(Vector3 localScale, float r, int seg = 5)
	{
		float num = localScale.x * 0.5f;
		float num2 = localScale.y * 0.5f;
		float num3 = localScale.z * 0.5f;
		float num4 = Mathf.Clamp(r, 0.0001f, Mathf.Min(num, num2) * 0.9f);
		r = num4;
		float num5 = num - r;
		float num6 = num2 - r;
		List<Vector2> list = new List<Vector2>((seg + 1) * 4);
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
		int num7 = 0;
		if (num7 < 4)
		{
			do
			{
				float num8 = (float)num7 * MathF.PI * 0.5f;
				float num9 = array[num7];
				float num10 = array2[num7];
				int num11 = 0;
				if (num11 <= seg)
				{
					do
					{
						float num12 = Mathf.Lerp(num8, num8 + MathF.PI / 2f, (float)num11 / (float)seg);
						list.Add(new Vector2(num9 + Mathf.Cos(num12) * r, num10 + Mathf.Sin(num12) * r));
						num11++;
					}
					while (num11 <= seg);
				}
				num7++;
			}
			while (num7 < 4);
		}
		int count = list.Count;
		List<Vector3> list2 = new List<Vector3>(count * 4 + 4);
		List<Vector3> list3 = new List<Vector3>(count * 4 + 4);
		List<int> list4 = new List<int>(count * 12);
		int count2 = list2.Count;
		list2.Add(new Vector3(0f, 0f, num3));
		list3.Add(Vector3.forward);
		int count3 = list2.Count;
		int num13 = 0;
		if (num13 < count)
		{
			do
			{
				list2.Add(new Vector3(list[num13].x, list[num13].y, num3));
				list3.Add(Vector3.forward);
				num13++;
			}
			while (num13 < count);
		}
		int num14 = 0;
		if (num14 < count)
		{
			do
			{
				list4.Add(count2);
				list4.Add(count3 + num14);
				list4.Add(count3 + (num14 + 1) % count);
				num14++;
			}
			while (num14 < count);
		}
		int count4 = list2.Count;
		list2.Add(new Vector3(0f, 0f, 0f - num3));
		list3.Add(Vector3.back);
		int count5 = list2.Count;
		int num15 = 0;
		if (num15 < count)
		{
			do
			{
				list2.Add(new Vector3(list[num15].x, list[num15].y, 0f - num3));
				list3.Add(Vector3.back);
				num15++;
			}
			while (num15 < count);
		}
		int num16 = 0;
		if (num16 < count)
		{
			do
			{
				list4.Add(count4);
				list4.Add(count5 + (num16 + 1) % count);
				list4.Add(count5 + num16);
				num16++;
			}
			while (num16 < count);
		}
		int count6 = list2.Count;
		int num17 = 0;
		if (num17 < count)
		{
			do
			{
				Vector3 val = new Vector3(list[num17].x, list[num17].y, 0f);
				Vector3 normalized = ((Vector3)val).normalized;
				list2.Add(new Vector3(list[num17].x, list[num17].y, num3));
				list3.Add(normalized);
				list2.Add(new Vector3(list[num17].x, list[num17].y, 0f - num3));
				list3.Add(normalized);
				num17++;
			}
			while (num17 < count);
		}
		int num18 = 0;
		if (num18 < count)
		{
			do
			{
				int num19 = (num18 + 1) % count;
				int num20 = count6 + num18 * 2;
				int item = num20 + 1;
				int num21 = count6 + num19 * 2;
				int item2 = num21 + 1;
				list4.Add(num20);
				list4.Add(item);
				list4.Add(num21);
				list4.Add(num21);
				list4.Add(item);
				list4.Add(item2);
				num18++;
			}
			while (num18 < count);
		}
		int count7 = list4.Count;
		int num22 = 0;
		if (num22 < count7)
		{
			do
			{
				list4.Add(list4[num22]);
				list4.Add(list4[num22 + 2]);
				list4.Add(list4[num22 + 1]);
				num22 += 3;
			}
			while (num22 < count7);
		}
		Mesh val2 = new Mesh
		{
			name = "KeyboardRoundedMesh"
		};
		val2.SetVertices(list2);
		val2.SetNormals(list3);
		val2.SetTriangles(list4, 0);
		val2.RecalculateBounds();
		return val2;
	}

	private static GameObject CreatePanelPrimitive(Transform parent)
	{
		GameObject val = GameObject.CreatePrimitive((PrimitiveType)3);
		Object.Destroy((Object)(object)val.GetComponent<Rigidbody>());
		Object.Destroy((Object)(object)val.GetComponent<BoxCollider>());
		val.layer = LayerMask.NameToLayer("UI");
		val.transform.SetParent(parent, false);
		val.transform.localRotation = Quaternion.identity;
		KeyCollider_Items_02.Add(val);
		return val;
	}

	private static void RegisterColorTarget(Main.ColorRole role, GameObject obj, List<GameObject> parts)
	{
		Recovered_Reference_08.Add((role, obj, parts));
		Main.RegisterColorGroup(role, obj, parts);
	}

	private static void CreateKey(string key, Vector3 position, float width, float height = 0.04f)
	{
		GameObject val = GameObject.CreatePrimitive((PrimitiveType)3);
		((Collider)val.GetComponent<BoxCollider>()).isTrigger = true;
		val.layer = LayerMask.NameToLayer("UI");
		val.transform.SetParent(KeyCollider_Object_01.transform, false);
		val.transform.localScale = new Vector3(width - 0.0025f, height - 0.0025f, 0.012f);
		val.transform.localPosition = position;
		val.transform.localRotation = Quaternion.identity;
		KeyCollider_Items_02.Add(val);
		Rigidbody val2 = val.AddComponent<Rigidbody>();
		val2.isKinematic = true;
		val2.useGravity = false;
		Material sharedMaterial = CreateKeyMaterial();
		val.GetComponent<Renderer>().sharedMaterial = sharedMaterial;
		GameObject val3 = new GameObject("KeyCanvas");
		val3.transform.SetParent(val.transform, false);
		val3.transform.localPosition = new Vector3(0f, 0f, -0.52f);
		val3.transform.localRotation = Quaternion.identity;
		float num = width - 0.0025f;
		float num2 = height - 0.0025f;
		float num3 = 0.01f;
		val3.transform.localScale = new Vector3(num3 / num * num2, num3, num3);
		val3.layer = LayerMask.NameToLayer("UI");
		KeyCollider_Items_02.Add(val3);
		Canvas val4 = val3.AddComponent<Canvas>();
		val4.renderMode = (RenderMode)2;
		val4.sortingOrder = 10;
		Text val5 = new GameObject("Label").AddComponent<Text>();
		((Component)val5).transform.SetParent(val3.transform, false);
		val5.font = Main.CurrentFont;
		val5.text = key;
		val5.fontSize = 50;
		((Graphic)val5).color = Color.white;
		val5.alignment = (TextAnchor)4;
		val5.resizeTextForBestFit = true;
		val5.resizeTextMinSize = 10;
		val5.resizeTextMaxSize = 50;
		RectTransform component = ((Component)val5).GetComponent<RectTransform>();
		component.sizeDelta = new Vector2(200f, 200f);
		((Transform)component).localPosition = Vector3.zero;
		((Transform)component).localRotation = Quaternion.identity;
		List<GameObject> list;
		if (Settings.CapturedVariables3760_Index_32 >= 2)
		{
			GameObject val6 = CreatePanelPrimitive(KeyCollider_Object_01.transform);
			((Object)val6).name = "KeyOutline";
			val6.transform.localScale = new Vector3(width - 0.0025f + 0.004f, height - 0.0025f + 0.004f, 0.011f);
			val6.transform.localPosition = new Vector3(position.x, position.y, position.z + 0.0005f);
			Material val7 = CreateKeyboardOutlineMaterial();
			KeyCollider_Items_04.Add(val7);
			val6.GetComponent<Renderer>().sharedMaterial = val7;
			List<GameObject> parts = null;
			if (Settings.CapturedVariables3760_Value_01 > 0f)
			{
				parts = RoundPanel(val6, 0.007f * Settings.CapturedVariables3760_Value_01);
				RegisterColorTarget(Main.ColorRole.Outline, val6, parts);
				list = null;
				if (Settings.CapturedVariables3760_Value_01 > 0f)
				{
					goto Branch_0367;
				}
			}
			else
			{
				RegisterColorTarget(Main.ColorRole.Outline, val6, parts);
				list = null;
				if (Settings.CapturedVariables3760_Value_01 > 0f)
				{
					goto Branch_0367;
				}
			}
		}
		else
		{
			list = null;
			if (Settings.CapturedVariables3760_Value_01 > 0f)
			{
				goto Branch_0367;
			}
		}
		RegisterColorTarget(Main.ColorRole.Button, val, list);
		KeyCollider keyCollider = val.AddComponent<KeyCollider>();
		keyCollider.key = key;
		keyCollider.baseScale = val.transform.localScale;
		if (list == null)
		{
			goto Branch_044a;
		}
		goto Branch_03fc;
		Branch_0367:
		list = RoundPanel(val, 0.007f * Settings.CapturedVariables3760_Value_01);
		RegisterColorTarget(Main.ColorRole.Button, val, list);
		keyCollider = val.AddComponent<KeyCollider>();
		keyCollider.key = key;
		keyCollider.baseScale = val.transform.localScale;
		if (list == null)
		{
			goto Branch_044a;
		}
		goto Branch_03fc;
		Branch_044a:
		KeyCollider_Lookup_01[key] = keyCollider;
		return;
		Branch_03fc:
		if (list.Count <= 0)
		{
			goto Branch_044a;
		}
		keyCollider.roundedHolder = list[0];
		KeyCollider_Lookup_01[key] = keyCollider;
	}

	public static void UpdateCursorBlink()
	{
		if (!KeyCollider_State_01 || (Object)(object)KeyCollider_Reference_01 == (Object)null)
		{
			return;
		}
		KeyCollider_Value_01 += Time.deltaTime;
		if (!(KeyCollider_Value_01 < 0.5f))
		{
			KeyCollider_Value_01 = 0f;
			KeyCollider_State_03 = !KeyCollider_State_03;
			if (!string.IsNullOrEmpty(KeyCollider_Text_02))
			{
				string y0HFWAXU = KeyCollider_Text_02;
				KeyCollider_Reference_01.text = y0HFWAXU + (KeyCollider_State_03 ? "|" : "");
			}
			else
			{
				string y0HFWAXU = KeyCollider_Text_03;
				KeyCollider_Reference_01.text = y0HFWAXU + (KeyCollider_State_03 ? "|" : "");
			}
		}
	}

	public static string KeyCodeToText(KeyCode keyCode)
	{
		KeyCode val = keyCode;
		if ((int)val != 8)
		{
			if ((int)val != 13)
			{
				if ((int)val == 32)
				{
					return "SPACE";
				}
				if ((int)val >= 48 && (int)val <= 57)
				{
					return (val - 48).ToString();
				}
				return keyCode.ToString();
			}
			return "ENTER";
		}
		return "BACK";
	}

	public static void OpenSearch(bool typingMode, string prefill = "", string placeholder = "Type to search...")
	{
		KeyCollider_State_02 = true;
		KeyCollider_State_04 = typingMode;
		KeyCollider_State_01 = true;
		KeyCollider_Text_02 = prefill;
		KeyCollider_Text_03 = placeholder;
		if (Variables.Variables_State_02 && Main.CapturedVariables1950_Value_04 != 0.75f)
		{
			KeyCollider_Value_03 = Main.CapturedVariables1950_Value_04;
			Main.CapturedVariables1950_Value_04 = 0.75f;
			if (!Variables.Variables_State_15)
			{
				goto Branch_00b0;
			}
		}
		else if (!Variables.Variables_State_15)
		{
			goto Branch_00b0;
		}
		Main.EnsureSearchField();
		return;
		Branch_00b0:
		BuildKeyboard();
		CreateLeftKeyClicker(Variables.Variables_Reference_06.RightHand.controllerTransform);
		CreateRightKeyClicker(Variables.Variables_Reference_06.LeftHand.controllerTransform);
		Main.EnsureSearchField();
	}

	public static void OpenTextInput(string prefill = "", string placeholder = "Type here...")
	{
		KeyCollider_Text_01 = null;
		KeyCollider_Callback_01 = null;
		KeyCollider_State_02 = true;
		KeyCollider_State_04 = true;
		KeyCollider_State_01 = true;
		KeyCollider_Text_02 = prefill;
		KeyCollider_Text_03 = placeholder;
		if (Variables.Variables_State_02 && Main.CapturedVariables1950_Value_04 != 0.75f)
		{
			KeyCollider_Value_03 = Main.CapturedVariables1950_Value_04;
			Main.CapturedVariables1950_Value_04 = 0.75f;
			if (!Variables.Variables_State_15)
			{
				goto Branch_00bc;
			}
		}
		else if (!Variables.Variables_State_15)
		{
			goto Branch_00bc;
		}
		Main.RebuildMenu();
		return;
		Branch_00bc:
		BuildKeyboard();
		CreateLeftKeyClicker(Variables.Variables_Reference_06.RightHand.controllerTransform);
		CreateRightKeyClicker(Variables.Variables_Reference_06.LeftHand.controllerTransform);
		Main.RebuildMenu();
	}

	private static Material CreateKeyboardOutlineMaterial()
	{
		if (Settings.CapturedVariables3760_Color_04 == Settings.ColorMode.Pinwheel)
		{
			Material val = Main.CreatePinwheelMaterial();
			if ((Object)(object)val != (Object)null)
			{
				KeyCollider_Items_04.Add(val);
				return val;
			}
			return CreateUiMaterial((Color32)(Settings.CapturedVariables3760_Color_28), 2455);
		}
		return CreateUiMaterial((Color32)(Settings.CapturedVariables3760_Color_28), 2455);
	}

	public static void ToggleSearch()
	{
		if (KeyCollider_State_02)
		{
			CloseSearch();
		}
		else
		{
			OpenSearch(typingMode: false);
		}
	}
}
