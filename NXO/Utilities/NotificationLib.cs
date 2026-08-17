using System;
using System.Collections;
using System.Collections.Generic;
using NXO.Menu;
using NXO.Mods;
using UnityEngine;
using UnityEngine.UI;

namespace NXO.Utilities;

internal class NotificationLib : MonoBehaviour
{
	private class ArrayItem
	{
		public string mod;

		public GameObject root;

		public RectTransform rect;

		public Image bar;

		public Text text;

		public float width;

		public float entryTime;

		public float removeTime;
	}

	public enum NotificationType
	{
		Enabled,
		Disabled,
		Saved,
		Loaded,
		Deleted,
		Room,
		Error,
		Alert,
		Info
	}

	private static readonly Dictionary<string, float> NotificationType_Lookup_02 = new Dictionary<string, float>();

	private GameObject _hudObj;

	private GameObject _hudObj2;

	private GameObject _mainCamera;

	private Text _notificationText;

	private Material _notificationMaterial;

	private readonly List<GameObject> _trackedObjects = new List<GameObject>();

	private bool _hasInitialized;

	private float _fadeAlpha = 1f;

	private bool _isFading;

	private const float NOTIFICATION_DELAY = 3f;

	private const float FADE_DURATION = 0.5f;

	private Sprite _barSprite;

	private Material _arrayMat;

	private GameObject _arrayRoot;

	private readonly List<ArrayItem> _arrayItems = new List<ArrayItem>(32);

	private readonly Dictionary<string, ArrayItem> _arrayByName = new Dictionary<string, ArrayItem>(32);

	private readonly HashSet<string> _currentEnabledSet = new HashSet<string>(32);

	private const float ArrayAnimDur = 0.4f;

	private const float ArrayRowStep = 0.125f;

	private const float ArraySlideOut = 0.15f;

	private const float ArrayBounce = 0.03f;

	private const float ArrayPadX = 8f;

	private const float ArrayPadY = 3f;

	private const float ArrayForward = 2.2f;

	private const float ArrayRight = -1.35f;

	private const float ArrayUp = 0.55f;

	public static bool NotificationType_State_03;

	public static bool NotificationType_State_01 = true;

	public static bool NotificationType_State_02;

	private static readonly Dictionary<NotificationType, string> NotificationType_Lookup_01 = new Dictionary<NotificationType, string>
	{
		{
			NotificationType.Enabled,
			"#00FF00"
		},
		{
			NotificationType.Disabled,
			"#FF4040"
		},
		{
			NotificationType.Saved,
			"#00AAFF"
		},
		{
			NotificationType.Loaded,
			"#00FFFF"
		},
		{
			NotificationType.Deleted,
			"#FF8C00"
		},
		{
			NotificationType.Room,
			"#C040FF"
		},
		{
			NotificationType.Error,
			"#FF0000"
		},
		{
			NotificationType.Alert,
			"#FFD700"
		},
		{
			NotificationType.Info,
			"#B0B0B0"
		}
	};

	public static string PreviousNotification { get; private set; }

	public static bool IsEnabled { get; set; } = true;

	public static NotificationLib Instance { get; private set; }

	private static Sprite CreateRoundedSprite(int size, int radius)
	{
		Texture2D val = new Texture2D(size, size, (TextureFormat)4, false)
		{
			filterMode = (FilterMode)1,
			wrapMode = (TextureWrapMode)1
		};
		Color32[] array = (Color32[])(object)new Color32[size * size];
		float num = radius;
		float num2 = 1.25f;
		int num3 = 0;
		if (num3 < size)
		{
			do
			{
				int num4 = 0;
				if (num4 < size)
				{
					do
					{
						float num5 = Mathf.Abs((float)num4 + 0.5f - (float)size * 0.5f) - ((float)size * 0.5f - num);
						float num6 = Mathf.Abs((float)num3 + 0.5f - (float)size * 0.5f) - ((float)size * 0.5f - num);
						float num7 = Mathf.Max(num5, 0f);
						float num8 = Mathf.Max(num6, 0f);
						float num9 = Mathf.Sqrt(num7 * num7 + num8 * num8) + Mathf.Min(Mathf.Max(num5, num6), 0f) - num;
						float num10 = Mathf.Clamp01(0.5f - num9 / num2);
						array[num3 * size + num4] = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, (byte)(num10 * 255f));
						num4++;
					}
					while (num4 < size);
				}
				num3++;
			}
			while (num3 < size);
		}
		val.SetPixels32(array);
		val.Apply(false, true);
		return Sprite.Create(val, new Rect(0f, 0f, (float)size, (float)size), new Vector2(0.5f, 0.5f), 100f, 0u, (SpriteMeshType)0, new Vector4((float)radius, (float)radius, (float)radius, (float)radius));
	}

	private void DestroyAllArrayItems()
	{
		int num = 0;
		if (num < _arrayItems.Count)
		{
			while (true)
			{
				if ((Object)(object)_arrayItems[num].root != (Object)null)
				{
					Object.Destroy((Object)(object)_arrayItems[num].root);
					num++;
					if (num >= _arrayItems.Count)
					{
						break;
					}
				}
				else
				{
					num++;
					if (num >= _arrayItems.Count)
					{
						break;
					}
				}
			}
		}
		_arrayItems.Clear();
		_arrayByName.Clear();
	}

	public void Init()
	{
		if (!_hasInitialized)
		{
			_mainCamera = GameObject.Find("Main Camera");
			if (!((Object)(object)_mainCamera == (Object)null))
			{
				_hudObj2 = CreateAndTrackHUDObject("HUD_Notification_Parent");
				_hudObj2.transform.position = _mainCamera.transform.position + new Vector3(-1.5f, 0f, -4.5f);
				_hudObj = CreateAndTrackHUDObject("HUD_Notification", _hudObj2.transform);
				Canvas val = _hudObj.AddComponent<Canvas>();
				val.renderMode = (RenderMode)2;
				val.worldCamera = _mainCamera.GetComponent<Camera>();
				_hudObj.AddComponent<CanvasScaler>().dynamicPixelsPerUnit = 10f;
				_hudObj.AddComponent<GraphicRaycaster>();
				RectTransform component = _hudObj.GetComponent<RectTransform>();
				component.sizeDelta = new Vector2(5f, 5f);
				((Transform)component).localScale = Vector3.one;
				((Transform)component).localPosition = new Vector3(0f, 0f, 1.6f);
				((Transform)component).rotation = Quaternion.Euler(0f, -250f, 0f);
				_notificationText = CreateTextElement("NotificationText", _hudObj, new Vector3(-1.2f, -0.75f, 0f), new Vector2(300f, 70f), 7);
				_notificationText.font = Main.CurrentFont;
				_notificationText.fontStyle = (FontStyle)1;
				_notificationText.alignment = (TextAnchor)6;
				_notificationMaterial = new Material(Shader.Find("GUI/Text Shader"));
				((Graphic)_notificationText).material = _notificationMaterial;
				_barSprite = CreateRoundedSprite(32, 12);
				_arrayMat = new Material(Shader.Find("GUI/Text Shader"));
				_arrayRoot = new GameObject("NXO_ArrayList");
				Canvas val2 = _arrayRoot.AddComponent<Canvas>();
				val2.renderMode = (RenderMode)2;
				val2.worldCamera = _mainCamera.GetComponent<Camera>();
				_arrayRoot.AddComponent<CanvasScaler>().dynamicPixelsPerUnit = 10f;
				_arrayRoot.GetComponent<RectTransform>().sizeDelta = new Vector2(5f, 5f);
				_trackedObjects.Add(_arrayRoot);
				_hasInitialized = true;
			}
		}
	}

	private IEnumerator FadeInNotification()
	{
		if (_isFading)
		{
			yield break;
		}
		float elapsed = 0f;
		if (elapsed < 0.5f)
		{
			do
			{
				elapsed += Time.deltaTime;
				_fadeAlpha = Mathf.Lerp(0f, 1f, elapsed / 0.5f);
				UpdateTextAlpha();
				yield return null;
			}
			while (elapsed < 0.5f);
		}
		_fadeAlpha = 1f;
		UpdateTextAlpha();
	}

	private ArrayItem CreateArrayItem(string mod, float now)
	{
		GameObject val2 = new GameObject("ArrayItem", new Type[1] { typeof(RectTransform) });
		val2.transform.SetParent(_arrayRoot.transform, false);
		RectTransform val = (RectTransform)val2.transform;
		((Transform)val).localScale = new Vector3(0.01f, 0.01f, 1f);
		GameObject val4 = new GameObject("Bar");
		val4.transform.SetParent(val2.transform, false);
		Image val3 = val4.AddComponent<Image>();
		val3.sprite = _barSprite;
		((Graphic)val3).material = _arrayMat;
		val3.type = Image.Type.Sliced;
		((Graphic)val3).color = new Color(0f, 0f, 0f, 0.7f);
		RectTransform rectTransform = ((Graphic)val3).rectTransform;
		Vector2 val5 = new Vector2(0.5f, 0.5f);
		rectTransform.anchorMax = val5;
		rectTransform.anchorMin = val5;
		rectTransform.pivot = new Vector2(0f, 0.5f);
		GameObject val6 = new GameObject("Text");
		val6.transform.SetParent(val2.transform, false);
		Text val7 = val6.AddComponent<Text>();
		val7.font = Main.CurrentFont;
		val7.fontStyle = (FontStyle)1;
		val7.fontSize = 7;
		val7.alignment = (TextAnchor)3;
		val7.horizontalOverflow = (HorizontalWrapMode)1;
		val7.verticalOverflow = (VerticalWrapMode)1;
		((Graphic)val7).material = _arrayMat;
		val7.text = mod;
		RectTransform rectTransform2 = ((Graphic)val7).rectTransform;
		val5 = new Vector2(0.5f, 0.5f);
		rectTransform2.anchorMax = val5;
		rectTransform2.anchorMin = val5;
		rectTransform2.pivot = new Vector2(0f, 0.5f);
		float preferredWidth = val7.preferredWidth;
		float preferredHeight = val7.preferredHeight;
		rectTransform2.sizeDelta = new Vector2(preferredWidth, preferredHeight);
		rectTransform2.anchoredPosition = new Vector2(8f, 0f);
		rectTransform.sizeDelta = new Vector2(preferredWidth + 16f, preferredHeight + 6f);
		rectTransform.anchoredPosition = Vector2.zero;
		return new ArrayItem
		{
			mod = mod,
			root = val2,
			rect = val,
			bar = val3,
			text = val7,
			width = preferredWidth,
			entryTime = now,
			removeTime = -1f
		};
	}

	public static void ShowNotification(NotificationType type, string content)
	{
		if (!Variables.Variables_State_12 || !IsEnabled || string.IsNullOrEmpty(content) || (Object)(object)Instance?._notificationText == (Object)null)
		{
			return;
		}
		string color = NotificationType_Lookup_01.TryGetValue(type, out string value) ? value : "#FFFFFF";
		string text = $"<color={color}>{type}</color> : {content}";
		if (text == PreviousNotification)
		{
			return;
		}
		NotificationType_Lookup_02[text] = Time.time;
		PreviousNotification = text;
		Instance.UpdateNotificationText();
		((MonoBehaviour)Instance).StartCoroutine(Instance.FadeInNotification());
	}

	private void Awake()
	{
		if ((Object)(object)Instance != (Object)null)
		{
			Object.Destroy((Object)(object)((Component)this).gameObject);
			return;
		}
		Instance = this;
		Object.DontDestroyOnLoad((Object)(object)((Component)this).gameObject);
	}

	public void UpdateArrayList()
	{
		if (!_hasInitialized)
		{
			Init();
			if ((Object)(object)_arrayRoot == (Object)null)
			{
				return;
			}
		}
		else if ((Object)(object)_arrayRoot == (Object)null)
		{
			return;
		}
		if (!NotificationType_State_02)
		{
			if (_arrayItems.Count > 0)
			{
				DestroyAllArrayItems();
			}
			return;
		}
		float unscaledTime;
		ButtonHandler.Button[] array;
		int num;
		if ((Object)(object)_mainCamera != (Object)null && (Object)(object)_arrayRoot != (Object)null)
		{
			Transform transform = _mainCamera.transform;
			_arrayRoot.transform.position = transform.position + transform.forward * 2.2f + transform.right * -1.35f + transform.up * 0.55f;
			_arrayRoot.transform.rotation = Quaternion.LookRotation(_arrayRoot.transform.position - transform.position, transform.up);
			unscaledTime = Time.unscaledTime;
			_currentEnabledSet.Clear();
			array = ModButtons.buttons;
			num = 0;
		}
		else
		{
			unscaledTime = Time.unscaledTime;
			_currentEnabledSet.Clear();
			array = ModButtons.buttons;
			num = 0;
		}
		while (num < array.Length)
		{
			if (array[num] != null && array[num].Enabled)
			{
				_currentEnabledSet.Add(array[num].buttonText);
				num++;
			}
			else
			{
				num++;
			}
		}
		using (HashSet<string>.Enumerator enumerator = _currentEnabledSet.GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				while (true)
				{
					string current = enumerator.Current;
					if (_arrayByName.TryGetValue(current, out ArrayItem value))
					{
						if (value.removeTime >= 0f)
						{
							value.removeTime = -1f;
							value.entryTime = unscaledTime;
						}
						if (!enumerator.MoveNext())
						{
							break;
						}
					}
					else
					{
						ArrayItem arrayItem = CreateArrayItem(current, unscaledTime);
						_arrayItems.Add(arrayItem);
						_arrayByName[current] = arrayItem;
						if (!enumerator.MoveNext())
						{
							break;
						}
					}
				}
			}
		}
		foreach (ArrayItem item in _arrayItems)
		{
			if (item.removeTime < 0f && !_currentEnabledSet.Contains(item.mod))
			{
				item.removeTime = unscaledTime;
			}
		}

		for (int i = _arrayItems.Count - 1; i >= 0; i--)
		{
			ArrayItem item = _arrayItems[i];
			if (item.removeTime >= 0f && unscaledTime - item.removeTime >= 0.4f)
			{
				Object.Destroy((Object)(object)item.root);
				_arrayByName.Remove(item.mod);
				_arrayItems.RemoveAt(i);
			}
		}

		_arrayItems.Sort((ArrayItem a, ArrayItem b) => b.width.CompareTo(a.width));
		for (int i = 0; i < _arrayItems.Count; i++)
		{
			ArrayItem item = _arrayItems[i];
			float entryAge = unscaledTime - item.entryTime;
			float horizontalOffset = 0f;
			float scale = 1f;
			float alpha = 1f;

			if (item.removeTime >= 0f)
			{
				float removalAge = unscaledTime - item.removeTime;
				if (removalAge < 0.4f)
				{
					float progress = removalAge / 0.4f;
					horizontalOffset = progress * 0.15f;
					alpha = 1f - progress;
				}
			}
			else if (entryAge < 0.4f)
			{
				float progress = entryAge / 0.4f;
				float bounce = Mathf.Sin(progress * MathF.PI * 2.5f) * (1f - progress);
				horizontalOffset = bounce * 0.03f;
				scale = 1f + bounce * 0.15f;
			}

			((Transform)item.rect).localPosition = new Vector3(horizontalOffset, -i * 0.125f, 0f);
			((Transform)item.rect).localScale = new Vector3(0.01f * scale, 0.01f * scale, 1f);
			Color accent = GetAnimatedAccentColor(i * 0.15f);
			((Graphic)item.text).color = new Color(accent.r, accent.g, accent.b, alpha);
			Color barColor = ((Graphic)item.bar).color;
			barColor.a = 0.7f * alpha;
			((Graphic)item.bar).color = barColor;
		}
	}

	private static Color GetAnimatedAccentColor(float phase = 0f)
	{
		return Color.Lerp(Main.CapturedVariables1950_Color_02, Main.CapturedVariables1950_Color_06, Mathf.PingPong(Time.unscaledTime * 1.5f - phase, 1f));
	}

	public void UpdateTextAlpha()
	{
		if ((Object)(object)_notificationText != (Object)null)
		{
			Color color = ((Graphic)_notificationText).color;
			color.a = _fadeAlpha;
			((Graphic)_notificationText).color = color;
		}
	}

	private GameObject CreateAndTrackHUDObject(string name, Transform parent = null)
	{
		GameObject val = new GameObject(name);
		if ((Object)(object)parent != (Object)null)
		{
			val.transform.parent = parent;
			_trackedObjects.Add(val);
			return val;
		}
		_trackedObjects.Add(val);
		return val;
	}

	public void UpdateNotifications()
	{
		if (!_hasInitialized)
		{
			Init();
			if ((Object)(object)_hudObj2 != (Object)null)
			{
				goto Branch_0063;
			}
		}
		else if ((Object)(object)_hudObj2 != (Object)null)
		{
			goto Branch_0063;
		}
		Branch_00cd:
		ProcessExpiredNotifications();
		return;
		Branch_0063:
		if (!((Object)(object)_mainCamera != (Object)null))
		{
			goto Branch_00cd;
		}
		_hudObj2.transform.SetPositionAndRotation(_mainCamera.transform.position, _mainCamera.transform.rotation);
		ProcessExpiredNotifications();
	}

	public void ProcessExpiredNotifications()
	{
		if (NotificationType_Lookup_02.Count == 0)
		{
			return;
		}
		float time = Time.time;
		float num = float.MaxValue;
		using (Dictionary<string, float>.ValueCollection.Enumerator enumerator = NotificationType_Lookup_02.Values.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				while (true)
				{
					float current = enumerator.Current;
					if (!(current < num))
					{
						break;
					}
					num = current;
					if (!enumerator.MoveNext())
					{
						goto EndBranch_0093;
					}
				}
				continue;
				EndBranch_0093:
				break;
			}
		}
		if (time - num >= 3f)
		{
			if (!_isFading)
			{
				_isFading = true;
				((MonoBehaviour)this).StartCoroutine(FadeOutNotification());
			}
		}
		else
		{
			_fadeAlpha = 1f;
			_isFading = false;
			UpdateTextAlpha();
		}
	}

	private Text CreateTextElement(string name, GameObject parent, Vector3 position, Vector2 size, int fontSize)
	{
		GameObject val2 = new GameObject(name);
		val2.transform.parent = parent.transform;
		Text val = val2.AddComponent<Text>();
		val.fontSize = fontSize;
		val.alignment = (TextAnchor)4;
		((Graphic)val).rectTransform.sizeDelta = size;
		((Transform)((Graphic)val).rectTransform).localScale = new Vector3(0.01f, 0.01f, 1f);
		((Transform)((Graphic)val).rectTransform).localPosition = position;
		_trackedObjects.Add(val2);
		return val;
	}

	public static void ClearNotifications()
	{
		NotificationType_Lookup_02.Clear();
		if ((Object)(object)Instance._notificationText != (Object)null)
		{
			Instance.UpdateNotificationText();
		}
	}

	private IEnumerator FadeOutNotification()
	{
		float elapsed = 0f;
		if (elapsed < 0.5f)
		{
			do
			{
				elapsed += Time.deltaTime;
				_fadeAlpha = Mathf.Lerp(1f, 0f, elapsed / 0.5f);
				UpdateTextAlpha();
				yield return null;
			}
			while (elapsed < 0.5f);
		}
		_fadeAlpha = 0f;
		UpdateTextAlpha();
		string oldestKey = null;
		float oldestVal = float.MaxValue;
		using (Dictionary<string, float>.Enumerator enumerator = NotificationType_Lookup_02.GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				while (true)
				{
					KeyValuePair<string, float> kvp = enumerator.Current;
					if (kvp.Value < oldestVal)
					{
						oldestVal = kvp.Value;
						oldestKey = kvp.Key;
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
		NotificationType_Lookup_02.Remove(oldestKey);
		UpdateNotificationText();
		_isFading = false;
		_fadeAlpha = 1f;
	}

	private void Update()
	{
		UpdateNotifications();
		UpdateArrayList();
	}

	public void UpdateNotificationText()
	{
		if ((Object)(object)_notificationText != (Object)null)
		{
			_notificationText.text = string.Join(Environment.NewLine, NotificationType_Lookup_02.Keys);
		}
	}
}
