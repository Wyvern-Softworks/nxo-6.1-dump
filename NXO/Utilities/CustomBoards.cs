using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GorillaNetworking;
using NXO.Menu;
using NXO.Mods.Categories;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace NXO.Utilities;

public class CustomBoards : MonoBehaviour
{
	private struct JoinScreens
	{
		public JoinTriggerUITemplate Template;

		public Material A;

		public Material B;

		public Material C;

		public Material D;

		public Material E;

		public Material F;

		public Material G;

		public Material H;
	}

	private struct BoardConfig
	{
		public readonly string Path;

		public readonly Vector3 Pos;

		public readonly Vector3 Rot;

		public readonly Vector3 Scale;

		public BoardConfig(string p, Vector3 pos, Vector3 rot, Vector3 s)
		{
			Path = p;
			Pos = pos;
			Rot = rot;
			Scale = s;
		}
	}

	[CompilerGenerated]
	private sealed class CapturedVariables270
	{
		public TextMeshPro body;

		public CustomBoards Owner;

		internal void SetupCoCText_Lambda0(int playerCount)
		{
			if (!((Object)(object)body == (Object)null))
			{
				if (playerCount < 0)
				{
					string text = "";
					((TMP_Text)body).text = "THANK YOU FOR BUYING NXO PAID!\n\nNXO IS NOT RESPONSIBLE FOR ANY ACTIONS\nTAKEN AGAINST YOUR ACCOUNT.\n\n" + text + Owner._message + "\n\nCREATED BY: NUGGET\nDEVELOPERS: CATLICKER AND LIEX\nDISCORD.GG/Recovered_Reference_13";
				}
				else
				{
					string text = $"NXO Users Online: {playerCount}\n\n";
					((TMP_Text)body).text = "THANK YOU FOR BUYING NXO PAID!\n\nNXO IS NOT RESPONSIBLE FOR ANY ACTIONS\nTAKEN AGAINST YOUR ACCOUNT.\n\n" + text + Owner._message + "\n\nCREATED BY: NUGGET\nDEVELOPERS: CATLICKER AND LIEX\nDISCORD.GG/Recovered_Reference_13";
				}
			}
		}
	}

	[CompilerGenerated]
	private sealed class CustomFeaturedMap_StateMachine42 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int State;

		private object Current;

		public CustomBoards Owner;

		private Texture2D newTexCaptured1;

		private Sprite newSpriteCaptured2;

		private Texture2D oldTexCaptured3;

		private Sprite oldSpriteCaptured4;

		private GameObject loadingCaptured5;

		private NewMapsDisplay displayCaptured6;

		private UnityWebRequest reqCaptured7;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return Current;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return Current;
			}
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		private bool MoveNext()
		{
			bool result;
			try
			{
				GameObject obj2;
				GameObject obj3;
				switch (State)
				{
				default:
					result = false;
					break;
				case 0:
				{
					State = -1;
					CustomBoards customBoards = Owner;
					GameObject obj = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/ModIOFeaturedMapsDisplay/FeaturedMapImage");
					customBoards._featuredSr = ((obj != null) ? obj.GetComponent<SpriteRenderer>() : null);
					if ((Object)(object)Owner._featuredSr == (Object)null)
					{
						result = false;
						break;
					}
					newTexCaptured1 = null;
					reqCaptured7 = UnityWebRequestTexture.GetTexture("https://github.com/NuggetGT/NXO-Resources/blob/main/NXO%20is%20sooooo%20awesome.png?raw=true");
					State = -3;
					Current = reqCaptured7.SendWebRequest();
					State = 1;
					result = true;
					break;
				}
				case 1:
					{
						State = -3;
						if ((int)reqCaptured7.result != 1)
						{
							result = false;
							Finally1();
							break;
						}
						newTexCaptured1 = DownloadHandlerTexture.GetContent(reqCaptured7);
						Finally1();
						reqCaptured7 = null;
						newSpriteCaptured2 = Sprite.Create(newTexCaptured1, new Rect(0f, 0f, (float)((Texture)newTexCaptured1).width, (float)((Texture)newTexCaptured1).height), new Vector2(0.5f, 0.5f), 100f);
						oldTexCaptured3 = Owner._featuredTex;
						oldSpriteCaptured4 = Owner._featuredSprite;
						Owner._featuredTex = newTexCaptured1;
						Owner._featuredSprite = newSpriteCaptured2;
						if ((Object)(object)oldSpriteCaptured4 != (Object)null)
						{
							Object.Destroy((Object)(object)oldSpriteCaptured4);
							if ((Object)(object)oldTexCaptured3 != (Object)null)
							{
								goto Branch_0271;
							}
						}
						else if ((Object)(object)oldTexCaptured3 != (Object)null)
						{
							goto Branch_0271;
						}
						Owner._featuredSr.sprite = Owner._featuredSprite;
						((Component)Owner._featuredSr).gameObject.SetActive(true);
						loadingCaptured5 = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/LoadingText");
						if (!((Object)(object)loadingCaptured5 != (Object)null))
						{
							goto Branch_038c;
						}
						goto Branch_033e;
					}
					Branch_0434:
					result = false;
					break;
					Branch_033e:
					loadingCaptured5.SetActive(false);
					obj2 = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/ModIOFeaturedMapsDisplay");
					displayCaptured6 = ((obj2 != null) ? obj2.GetComponent<NewMapsDisplay>() : null);
					if (!((Object)(object)displayCaptured6 != (Object)null))
					{
						goto Branch_0434;
					}
					goto Branch_03ce;
					Branch_0271:
					Object.Destroy((Object)(object)oldTexCaptured3);
					Owner._featuredSr.sprite = Owner._featuredSprite;
					((Component)Owner._featuredSr).gameObject.SetActive(true);
					loadingCaptured5 = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/LoadingText");
					if (!((Object)(object)loadingCaptured5 != (Object)null))
					{
						goto Branch_038c;
					}
					goto Branch_033e;
					Branch_038c:
					obj3 = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/ModIOFeaturedMapsDisplay");
					displayCaptured6 = ((obj3 != null) ? obj3.GetComponent<NewMapsDisplay>() : null);
					if (!((Object)(object)displayCaptured6 != (Object)null))
					{
						goto Branch_0434;
					}
					Branch_03ce:
					if (!((Object)(object)ReflectionCompat.GetField<TMP_Text>(displayCaptured6, "mapInfoTMP") != (Object)null))
					{
						goto Branch_0434;
					}
					((Component)ReflectionCompat.GetField<TMP_Text>(displayCaptured6, "mapInfoTMP")).gameObject.SetActive(true);
					ReflectionCompat.GetField<TMP_Text>(displayCaptured6, "mapInfoTMP").text = "NXO ON TOP!";
					result = false;
					break;
				}
			}
			catch (Exception)
			{
				((IDisposable)this).Dispose();
				bool result2 = default(bool);
				return result2;
			}
			return result;
		}

		bool IEnumerator.MoveNext()
		{
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			int num = State;
			if (num == -3 || num == 1)
			{
				try
				{
				}
				catch (Exception)
				{
					Finally1();
					return;
				}
			}
			newTexCaptured1 = null;
			newSpriteCaptured2 = null;
			oldTexCaptured3 = null;
			oldSpriteCaptured4 = null;
			loadingCaptured5 = null;
			displayCaptured6 = null;
			reqCaptured7 = null;
			State = -2;
		}

		[DebuggerHidden]
		public CustomFeaturedMap_StateMachine42(int State)
		{
			this.State = State;
		}

		private void Finally1()
		{
			State = -1;
			if (reqCaptured7 != null)
			{
				((IDisposable)reqCaptured7).Dispose();
			}
		}
	}

	[CompilerGenerated]
	private sealed class FetchMotd_StateMachine26 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int State;

		private object Current;

		public CustomBoards Owner;

		private UnityWebRequest reqCaptured1;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return Current;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return Current;
			}
		}

		private void Finally1()
		{
			State = -1;
			if (reqCaptured1 != null)
			{
				((IDisposable)reqCaptured1).Dispose();
			}
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			int num = State;
			if (num == -3 || num == 1)
			{
				try
				{
				}
				catch (Exception)
				{
					Finally1();
					return;
				}
			}
			reqCaptured1 = null;
			State = -2;
		}

		private bool MoveNext()
		{
			bool result;
			try
			{
				int num = State;
				num = (((uint)num <= 2u) ? num : 3) + 21;
				int num2 = num;
				if (num2 != 22)
				{
					State = -1;
					reqCaptured1 = UnityWebRequest.Get("https://api.github.com/repos/NuggetGT/NXO-Resources/contents/NXO-Menu-Status.txt");
					State = -3;
					reqCaptured1.SetRequestHeader("Accept", "application/vnd.github.v3.raw");
					Current = reqCaptured1.SendWebRequest();
					State = 1;
					result = true;
				}
				else
				{
					State = -3;
					Owner._message = (((int)reqCaptured1.result == 1) ? reqCaptured1.downloadHandler.text.Trim() : "THIS VERSION IS Recovered_Reference_14! CHECK DISCORD.GG/Recovered_Reference_13 FOR UPDATES.");
					Finally1();
					reqCaptured1 = null;
					Current = (object)new WaitForSeconds(2f);
					State = 2;
					result = true;
				}
			}
			catch (Exception)
			{
				((IDisposable)this).Dispose();
				bool result2 = default(bool);
				return result2;
			}
			return result;
		}

		bool IEnumerator.MoveNext()
		{
			return this.MoveNext();
		}

		[DebuggerHidden]
		public FetchMotd_StateMachine26(int State)
		{
			this.State = State;
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class GetPlayersCoroutine_StateMachine28 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int State;

		private object Current;

		public Action<int> callback;

		private string urlCaptured1;

		private UnityWebRequest reqCaptured2;

		private int playerCountCaptured3;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return Current;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return Current;
			}
		}

		[DebuggerHidden]
		public GetPlayersCoroutine_StateMachine28(int State)
		{
			this.State = State;
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			int num = State;
			if (num == -3 || num == 1)
			{
				try
				{
				}
				catch (Exception)
				{
					Finally1();
					return;
				}
			}
			urlCaptured1 = null;
			reqCaptured2 = null;
			State = -2;
		}

		private void Finally1()
		{
			State = -1;
			if (reqCaptured2 != null)
			{
				((IDisposable)reqCaptured2).Dispose();
			}
		}

		private bool MoveNext()
		{
			bool result = default(bool);
			try
			{
				switch (State)
				{
				default:
					result = false;
					break;
				case 0:
					State = -1;
					urlCaptured1 = Variables.Variables_Text_02 + "/api/ping";
					reqCaptured2 = UnityWebRequest.Get(urlCaptured1);
					State = -3;
					Current = reqCaptured2.SendWebRequest();
					State = 1;
					result = true;
					break;
				case 1:
				{
					State = -3;
					if ((int)reqCaptured2.result == 1 && int.TryParse(reqCaptured2.downloadHandler.text, out playerCountCaptured3))
					{
						Action<int> action = callback;
						if (action != null)
						{
							action(playerCountCaptured3);
							result = false;
						}
						else
						{
							result = false;
						}
						Finally1();
						break;
					}
					Finally1();
					int num = 25;
					reqCaptured2 = null;
					Action<int> action2 = callback;
					if (action2 == null)
					{
						num = 26;
					}
					else
					{
						action2(-1);
						result = false;
					}
					if (num == 26)
					{
						result = false;
					}
					break;
				}
				}
			}
			catch (Exception)
			{
				((IDisposable)this).Dispose();
				bool result2 = default(bool);
				return result2;
			}
			return result;
		}

		bool IEnumerator.MoveNext()
		{
			return this.MoveNext();
		}
	}

	private const int TreeRoomBoardIndex = 3;

	private const int ForestBoardIndex = 6;

	private Material _mat;

	private Renderer _monitor;

	private readonly List<TextMeshPro> _texts = new List<TextMeshPro>(32);

	private readonly Dictionary<string, GameObject> _scenePlanes = new Dictionary<string, GameObject>();

	private readonly Dictionary<Renderer, Material> _originalMats = new Dictionary<Renderer, Material>();

	private readonly List<Renderer> _pinned = new List<Renderer>(8);

	private string _message = "Loading...";

	private bool _boardsDone;

	private bool _matApplied;

	public static bool GetPlayersCoroutine_StateMachine28_State_01;

	private Material _cachedBoardPinwheelMat;

	private readonly List<JoinScreens> _joinScreens = new List<JoinScreens>();

	private bool _joinCached;

	private Texture2D _featuredTex;

	private SpriteRenderer _featuredSr;

	private Sprite _featuredSprite;

	private static readonly Dictionary<string, BoardConfig> GetPlayersCoroutine_StateMachine28_Reference_01;

	public static CustomBoards Instance { get; private set; }

	static CustomBoards()
	{
		GetPlayersCoroutine_StateMachine28_State_01 = true;
		GetPlayersCoroutine_StateMachine28_Reference_01 = new Dictionary<string, BoardConfig>
		{
			["Canyon2"] = new BoardConfig("Canyon/CanyonScoreboardAnchor/GorillaScoreBoard", new Vector3(-24.5019f, -28.7746f, 0.1f), new Vector3(270f, 0f, 0f), new Vector3(21.5946f, 1f, 22.1782f)),
			["Skyjungle"] = new BoardConfig("skyjungle/UI/Scoreboard/GorillaScoreBoard", new Vector3(-21.2764f, -32.1928f, 0f), new Vector3(270.2987f, 0.2f, 359.9f), new Vector3(21.6f, 0.1f, 20.4909f)),
			["Mountain"] = new BoardConfig("Mountain/MountainScoreboardAnchor/GorillaScoreBoard", Vector3.zero, Vector3.zero, Vector3.one),
			["Metropolis"] = new BoardConfig("MetroMain/ComputerArea/Scoreboard/GorillaScoreBoard", new Vector3(-25.1f, -31f, 0.1502f), new Vector3(270.1958f, 0.2086f, 0f), new Vector3(21f, 102.9727f, 21.4f)),
			["Bayou"] = new BoardConfig("BayouMain/ComputerArea/GorillaScoreBoardPhysical", new Vector3(-28.3419f, -26.851f, 0.3f), new Vector3(270f, 0f, 0f), new Vector3(21.3636f, 38f, 21f)),
			["Beach"] = new BoardConfig("BeachScoreboardAnchor/GorillaScoreBoard", new Vector3(-22.1964f, -33.7126f, 0.1f), new Vector3(270.056f, 0f, 0f), new Vector3(21.2f, 2f, 21.6f)),
			["Cave"] = new BoardConfig("Cave_Main_Prefab/CrystalCaveScoreboardAnchor/GorillaScoreBoard", new Vector3(-22.1964f, -33.7126f, 0.1f), new Vector3(270.056f, 0f, 0f), new Vector3(21.2f, 2f, 21.6f)),
			["Rotating"] = new BoardConfig("RotatingPermanentEntrance/UI (1)/RotatingScoreboard/RotatingScoreboardAnchor/GorillaScoreBoard", new Vector3(-22.1964f, -33.7126f, 0.1f), new Vector3(270.056f, 0f, 0f), new Vector3(21.2f, 2f, 21.6f)),
			["MonkeBlocks"] = new BoardConfig("Environment Objects/MonkeBlocksRoomPersistent/AtticScoreBoard/AtticScoreboardAnchor/GorillaScoreBoard", new Vector3(-22.1964f, -24.5091f, 0.57f), new Vector3(270.1856f, 0.1f, 0f), new Vector3(21.6f, 1.2f, 20.8f)),
			["Basement"] = new BoardConfig("Basement/BasementScoreboardAnchor/GorillaScoreBoard/", new Vector3(-22.1964f, -24.5091f, 0.57f), new Vector3(270.1856f, 0.1f, 0f), new Vector3(21.6f, 1.2f, 20.8f)),
			["City"] = new BoardConfig("City_Pretty/CosmeticsScoreboardAnchor/GorillaScoreBoard", new Vector3(-22.1964f, -34.9f, 0.57f), new Vector3(270f, 0f, 0f), new Vector3(21.6f, 2.4f, 22f))
		};
	}

	private void Awake()
	{
		if ((Object)(object)Instance != (Object)null && (Object)(object)Instance != (Object)(object)this)
		{
			Object.Destroy((Object)(object)((Component)this).gameObject);
			return;
		}
		Instance = this;
		Object.DontDestroyOnLoad((Object)(object)((Component)this).gameObject);
		SceneManager.sceneLoaded += OnSceneLoaded;
		SceneManager.sceneUnloaded += OnSceneUnloaded;
		_mat = new Material(Shader.Find("GorillaTag/UberShader"));
		((MonoBehaviour)this).StartCoroutine(FetchMotd());
	}

	[IteratorStateMachine(typeof(GetPlayersCoroutine_StateMachine28))]
	public static IEnumerator GetPlayerCountCoroutine(Action<int> callback)
	{
		return new GetPlayersCoroutine_StateMachine28(0)
		{
			callback = callback
		};
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (!GetPlayersCoroutine_StateMachine28_Reference_01.TryGetValue(((Scene)scene).name, out var value))
		{
			return;
		}
		try
		{
			GameObject val;
			if (_scenePlanes.TryGetValue(((Scene)scene).name, out GameObject value2) && (Object)(object)value2 != (Object)null)
			{
				Object.Destroy((Object)(object)value2);
				val = GameObject.Find(value.Path);
				if ((Object)(object)val == (Object)null)
				{
					return;
				}
			}
			else
			{
				val = GameObject.Find(value.Path);
				if ((Object)(object)val == (Object)null)
				{
					return;
				}
			}
			GameObject val2 = GameObject.CreatePrimitive((PrimitiveType)4);
			val2.transform.SetParent(val.transform, false);
			val2.transform.localPosition = value.Pos;
			val2.transform.localRotation = Quaternion.Euler(value.Rot);
			val2.transform.localScale = value.Scale;
			Object.Destroy((Object)(object)val2.GetComponent<Collider>());
			if (Settings.CapturedVariables3760_Color_03 != Settings.ColorMode.Pinwheel || !((Object)(object)_cachedBoardPinwheelMat != (Object)null))
			{
				Material mat = _mat;
				val2.GetComponent<Renderer>().sharedMaterial = mat;
				val2.SetActive(GetPlayersCoroutine_StateMachine28_State_01);
				_scenePlanes[((Scene)scene).name] = val2;
			}
			else
			{
				Material mat = _cachedBoardPinwheelMat;
				val2.GetComponent<Renderer>().sharedMaterial = mat;
				val2.SetActive(GetPlayersCoroutine_StateMachine28_State_01);
				_scenePlanes[((Scene)scene).name] = val2;
			}
		}
		catch (Exception ex)
		{
			Debug.LogException(ex);
		}
	}

	private void CacheText(TextMeshPro tmp)
	{
		if ((Object)(object)tmp != (Object)null && !_texts.Contains(tmp))
		{
			_texts.Add(tmp);
		}
	}

	private void OnSceneUnloaded(Scene scene)
	{
		if (_scenePlanes.TryGetValue(((Scene)scene).name, out GameObject value))
		{
			if ((Object)(object)value != (Object)null)
			{
				Object.Destroy((Object)(object)value);
				_scenePlanes.Remove(((Scene)scene).name);
			}
			else
			{
				_scenePlanes.Remove(((Scene)scene).name);
			}
		}
	}

	private void ApplyJoinScreens()
	{
		if (_joinScreens.Count == 0)
		{
			return;
		}
		Material val;
		List<JoinScreens>.Enumerator enumerator;
		if (Settings.CapturedVariables3760_Color_03 != Settings.ColorMode.Pinwheel || !((Object)(object)_cachedBoardPinwheelMat != (Object)null))
		{
			val = _mat;
			enumerator = _joinScreens.GetEnumerator();
		}
		else
		{
			val = _cachedBoardPinwheelMat;
			enumerator = _joinScreens.GetEnumerator();
		}
		try
		{
			while (enumerator.MoveNext())
			{
				while (true)
				{
					JoinScreens current = enumerator.Current;
					if ((Object)(object)current.Template == (Object)null)
					{
						break;
					}
					current.Template.ScreenBG_AbandonPartyAndSoloJoin = val;
					current.Template.ScreenBG_AlreadyInRoom = val;
					current.Template.ScreenBG_ChangingGameModeSoloJoin = val;
					current.Template.ScreenBG_Error = val;
					current.Template.ScreenBG_InPrivateRoom = val;
					current.Template.ScreenBG_LeaveRoomAndGroupJoin = val;
					current.Template.ScreenBG_LeaveRoomAndSoloJoin = val;
					current.Template.ScreenBG_NotConnectedSoloJoin = val;
					if (!enumerator.MoveNext())
					{
						goto EndBranch_014a;
					}
				}
				continue;
				EndBranch_014a:
				break;
			}
		}
		finally
		{
			((IDisposable)enumerator/*cast due to constrained. prefix*/).Dispose();
		}
		PhotonNetworkController instance = PhotonNetworkController.Instance;
		if (instance != null)
		{
			((PhotonNetworkController)instance).UpdateTriggerScreens();
		}
	}

	[IteratorStateMachine(typeof(FetchMotd_StateMachine26))]
	private IEnumerator FetchMotd()
	{
		return new FetchMotd_StateMachine26(0)
		{
			Owner = this
		};
	}

	private void RevertJoinScreens()
	{
		if (_joinScreens.Count == 0)
		{
			return;
		}
		using (List<JoinScreens>.Enumerator enumerator = _joinScreens.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				while (true)
				{
					JoinScreens current = enumerator.Current;
					if ((Object)(object)current.Template == (Object)null)
					{
						break;
					}
					current.Template.ScreenBG_AbandonPartyAndSoloJoin = current.A;
					current.Template.ScreenBG_AlreadyInRoom = current.B;
					current.Template.ScreenBG_ChangingGameModeSoloJoin = current.C;
					current.Template.ScreenBG_Error = current.D;
					current.Template.ScreenBG_InPrivateRoom = current.E;
					current.Template.ScreenBG_LeaveRoomAndGroupJoin = current.F;
					current.Template.ScreenBG_LeaveRoomAndSoloJoin = current.G;
					current.Template.ScreenBG_NotConnectedSoloJoin = current.H;
					if (!enumerator.MoveNext())
					{
						goto EndBranch_011c;
					}
				}
				continue;
				EndBranch_011c:
				break;
			}
		}
		PhotonNetworkController instance = PhotonNetworkController.Instance;
		if (instance != null)
		{
			((PhotonNetworkController)instance).UpdateTriggerScreens();
		}
	}

	public static IEnumerator PollPlayerCount(Action<int> callback)
	{
		while (true)
		{
			yield return GetPlayerCountCoroutine(callback);
			yield return (object)new WaitForSeconds(60f);
		}
	}

	private void OnDestroy()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
		SceneManager.sceneUnloaded -= OnSceneUnloaded;
		if ((Object)(object)_mat != (Object)null)
		{
			Object.Destroy((Object)(object)_mat);
			if ((Object)(object)_cachedBoardPinwheelMat != (Object)null)
			{
				goto Branch_008d;
			}
		}
		else if ((Object)(object)_cachedBoardPinwheelMat != (Object)null)
		{
			goto Branch_008d;
		}
		if (!((Object)(object)_featuredSprite != (Object)null))
		{
			goto Branch_0103;
		}
		goto Branch_00d8;
		Branch_0123:
		Object.Destroy((Object)(object)_featuredTex);
		return;
		Branch_0103:
		if (!((Object)(object)_featuredTex != (Object)null))
		{
			return;
		}
		goto Branch_0123;
		Branch_008d:
		Object.Destroy((Object)(object)_cachedBoardPinwheelMat);
		if (!((Object)(object)_featuredSprite != (Object)null))
		{
			goto Branch_0103;
		}
		Branch_00d8:
		Object.Destroy((Object)(object)_featuredSprite);
		if (!((Object)(object)_featuredTex != (Object)null))
		{
			return;
		}
		goto Branch_0123;
	}

	private void SetupCoCText()
	{
		CapturedVariables270 LocalScope18 = new CapturedVariables270();
		LocalScope18.Owner = this;
		GameObject obj = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/CodeOfConductHeadingText");
		TextMeshPro val = ((obj != null) ? obj.GetComponent<TextMeshPro>() : null);
		if ((Object)(object)val != (Object)null)
		{
			((TMP_Text)val).text = "NXO PAID 6.1";
			((Graphic)val).color = Color.white;
			((TMP_Text)val).richText = true;
			GameObject obj2 = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/COCBodyText_TitleData");
			LocalScope18.body = ((obj2 != null) ? obj2.GetComponent<TextMeshPro>() : null);
			if ((Object)(object)LocalScope18.body == (Object)null)
			{
				return;
			}
		}
		else
		{
			GameObject obj3 = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/COCBodyText_TitleData");
			LocalScope18.body = ((obj3 != null) ? obj3.GetComponent<TextMeshPro>() : null);
			if ((Object)(object)LocalScope18.body == (Object)null)
			{
				return;
			}
		}
		((Graphic)LocalScope18.body).color = Color.white;
		((TMP_Text)LocalScope18.body).richText = true;
		PlayFabTitleDataTextDisplay component = ((Component)LocalScope18.body).GetComponent<PlayFabTitleDataTextDisplay>();
		if ((Object)(object)component != (Object)null)
		{
			((Behaviour)component).enabled = false;
			((MonoBehaviour)this).StartCoroutine(GetPlayerCountCoroutine(delegate(int playerCount)
			{
				if (!((Object)(object)LocalScope18.body == (Object)null))
				{
					if (playerCount < 0)
					{
						string text = "";
						((TMP_Text)LocalScope18.body).text = "THANK YOU FOR BUYING NXO PAID!\n\nNXO IS NOT RESPONSIBLE FOR ANY ACTIONS\nTAKEN AGAINST YOUR ACCOUNT.\n\n" + text + LocalScope18.Owner._message + "\n\nCREATED BY: NUGGET\nDEVELOPERS: CATLICKER AND LIEX\nDISCORD.GG/Recovered_Reference_13";
					}
					else
					{
						string text = $"NXO Users Online: {playerCount}\n\n";
						((TMP_Text)LocalScope18.body).text = "THANK YOU FOR BUYING NXO PAID!\n\nNXO IS NOT RESPONSIBLE FOR ANY ACTIONS\nTAKEN AGAINST YOUR ACCOUNT.\n\n" + text + LocalScope18.Owner._message + "\n\nCREATED BY: NUGGET\nDEVELOPERS: CATLICKER AND LIEX\nDISCORD.GG/Recovered_Reference_13";
					}
				}
			}));
			((MonoBehaviour)this).StartCoroutine(CustomFeaturedMap());
			return;
		}
		((MonoBehaviour)this).StartCoroutine(GetPlayerCountCoroutine(delegate(int playerCount)
		{
			if (!((Object)(object)LocalScope18.body == (Object)null))
			{
				if (playerCount < 0)
				{
					string text = "";
					((TMP_Text)LocalScope18.body).text = "THANK YOU FOR BUYING NXO PAID!\n\nNXO IS NOT RESPONSIBLE FOR ANY ACTIONS\nTAKEN AGAINST YOUR ACCOUNT.\n\n" + text + LocalScope18.Owner._message + "\n\nCREATED BY: NUGGET\nDEVELOPERS: CATLICKER AND LIEX\nDISCORD.GG/Recovered_Reference_13";
				}
				else
				{
					string text = $"NXO Users Online: {playerCount}\n\n";
					((TMP_Text)LocalScope18.body).text = "THANK YOU FOR BUYING NXO PAID!\n\nNXO IS NOT RESPONSIBLE FOR ANY ACTIONS\nTAKEN AGAINST YOUR ACCOUNT.\n\n" + text + LocalScope18.Owner._message + "\n\nCREATED BY: NUGGET\nDEVELOPERS: CATLICKER AND LIEX\nDISCORD.GG/Recovered_Reference_13";
				}
			}
		}));
		((MonoBehaviour)this).StartCoroutine(CustomFeaturedMap());
	}

	public static void SetCustomBoardColorsEnabled(bool enabled)
	{
		GetPlayersCoroutine_StateMachine28_State_01 = enabled;
	}

	[IteratorStateMachine(typeof(CustomFeaturedMap_StateMachine42))]
	private IEnumerator CustomFeaturedMap()
	{
		return new CustomFeaturedMap_StateMachine42(0)
		{
			Owner = this
		};
	}

	private void FindBoards()
	{
		try
		{
			List<GorillaNetworkJoinTrigger> joinTriggers = ReflectionCompat.GetField<List<GorillaNetworkJoinTrigger>>(PhotonNetworkController.Instance, "allJoinTriggers");
			GameObject obj = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom");
			Transform val = ((obj != null) ? obj.transform : null);
			if ((Object)(object)val == (Object)null)
			{
				return;
			}
			ApplyToNthTempFile(val, 3);
			GameObject obj2 = GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest");
			Transform val2 = ((obj2 != null) ? obj2.transform : null);
			if ((Object)(object)val2 != (Object)null)
			{
				ApplyToNthTempFile(val2, 6);
				if (joinTriggers != null)
				{
					goto Branch_00f4;
				}
			}
			else if (joinTriggers != null)
			{
				goto Branch_00f4;
			}
			CacheText("Environment Objects/LocalObjects_Prefab/TreeRoom/CodeOfConductHeadingText");
			CacheText("Environment Objects/LocalObjects_Prefab/TreeRoom/COCBodyText_TitleData");
			CacheText("Environment Objects/LocalObjects_Prefab/TreeRoom/Data");
			CacheText("Environment Objects/LocalObjects_Prefab/TreeRoom/FunctionSelect");
			GameObject obj3 = GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/ForestScoreboardAnchor/GorillaScoreBoard");
			Transform val3 = ((obj3 != null) ? obj3.transform : null);
			if (!((Object)(object)val3 != (Object)null))
			{
				goto Branch_047f;
			}
			Branch_0341:
			int num = 0;
			if (num < val3.childCount)
			{
				do
				{
					Branch_034e:
					GameObject val4 = ((Component)val3.GetChild(num)).gameObject;
					if (((Component)val3.GetChild(num)).gameObject.activeSelf &&
						(((Object)val4).name.Contains("Board Text") || ((Object)val4).name.Contains("Scoreboard_OfflineText")))
					{
						goto Branch_03d9;
					}
					num++;
					if (num >= val3.childCount)
					{
						break;
					}
					goto Branch_034e;
					Branch_03d9:
					CacheText(val4.GetComponent<TextMeshPro>());
					num++;
				}
				while (num < val3.childCount);
			}
			GameObject obj4 = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/GorillaComputerObject/ComputerUI/monitor/monitorScreen");
			_monitor = ((obj4 != null) ? obj4.GetComponent<Renderer>() : null);
			CacheMat(_monitor);
			_boardsDone = true;
			return;
			Branch_047f:
			GameObject obj5 = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/GorillaComputerObject/ComputerUI/monitor/monitorScreen");
			_monitor = ((obj5 != null) ? obj5.GetComponent<Renderer>() : null);
			CacheMat(_monitor);
			_boardsDone = true;
			return;
			Branch_00f4:
			using (List<GorillaNetworkJoinTrigger>.Enumerator enumerator = joinTriggers.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					do
					{
						GorillaNetworkJoinTrigger current = enumerator.Current;
						try
						{
							JoinTriggerUI ui = ReflectionCompat.GetField<JoinTriggerUI>(current, "ui");
							JoinTriggerUITemplate template = ReflectionCompat.GetField<JoinTriggerUITemplate>(ui, "template");
							if (!_joinCached)
							{
								_joinScreens.Add(new JoinScreens
								{
									Template = template,
									A = template.ScreenBG_AbandonPartyAndSoloJoin,
									B = template.ScreenBG_AlreadyInRoom,
									C = template.ScreenBG_ChangingGameModeSoloJoin,
									D = template.ScreenBG_Error,
									E = template.ScreenBG_InPrivateRoom,
									F = template.ScreenBG_LeaveRoomAndGroupJoin,
									G = template.ScreenBG_LeaveRoomAndSoloJoin,
									H = template.ScreenBG_NotConnectedSoloJoin
								});
								CacheText(ReflectionCompat.GetField<TMPro.TextMeshPro>(ui, "screenText"));
							}
							else
							{
								CacheText(ReflectionCompat.GetField<TMPro.TextMeshPro>(ui, "screenText"));
							}
						}
						catch (Exception)
						{
						}
					}
					while (enumerator.MoveNext());
				}
			}
			_joinCached = true;
			CacheText("Environment Objects/LocalObjects_Prefab/TreeRoom/CodeOfConductHeadingText");
			CacheText("Environment Objects/LocalObjects_Prefab/TreeRoom/COCBodyText_TitleData");
			CacheText("Environment Objects/LocalObjects_Prefab/TreeRoom/Data");
			CacheText("Environment Objects/LocalObjects_Prefab/TreeRoom/FunctionSelect");
			GameObject obj6 = GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/ForestScoreboardAnchor/GorillaScoreBoard");
			val3 = ((obj6 != null) ? obj6.transform : null);
			if (!((Object)(object)val3 != (Object)null))
			{
				goto Branch_047f;
			}
			goto Branch_0341;
		}
		catch (Exception ex2)
		{
			Debug.LogException(ex2);
		}
	}

	private void CacheMat(Renderer r)
	{
		if (!((Object)(object)r == (Object)null) && !_originalMats.ContainsKey(r))
		{
			_originalMats[r] = r.sharedMaterial;
			_pinned.Add(r);
		}
	}

	private void CacheText(string path)
	{
		GameObject obj = GameObject.Find(path);
		CacheText((obj != null) ? obj.GetComponent<TextMeshPro>() : null);
	}

	private void ApplyToNthTempFile(Transform parent, int n)
	{
		int num = 0;
		int num2 = 0;
		if (num2 >= parent.childCount)
		{
			return;
		}
		while (true)
		{
			if (((Object)parent.GetChild(num2)).name.Contains("UnityTempFile"))
			{
				if (num++ == n)
				{
					Renderer component = ((Component)parent.GetChild(num2)).GetComponent<Renderer>();
					if ((Object)(object)component != (Object)null)
					{
						CacheMat(component);
					}
					break;
				}
				num2++;
				if (num2 >= parent.childCount)
				{
					break;
				}
			}
			else
			{
				num2++;
				if (num2 >= parent.childCount)
				{
					break;
				}
			}
		}
	}

	private void Update()
	{
		if (!GetPlayersCoroutine_StateMachine28_State_01)
		{
			if (!_matApplied)
			{
				return;
			}
			using (Dictionary<Renderer, Material>.Enumerator enumerator = _originalMats.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					while (true)
					{
						KeyValuePair<Renderer, Material> current = enumerator.Current;
						if (!((Object)(object)current.Key != (Object)null))
						{
							break;
						}
						current.Key.sharedMaterial = current.Value;
						if (!enumerator.MoveNext())
						{
							goto EndBranch_00a8;
						}
					}
					continue;
					EndBranch_00a8:
					break;
				}
			}
			RevertJoinScreens();
			using (Dictionary<string, GameObject>.ValueCollection.Enumerator enumerator2 = _scenePlanes.Values.GetEnumerator())
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
						current2.SetActive(false);
						if (!enumerator2.MoveNext())
						{
							goto EndBranch_0146;
						}
					}
					continue;
					EndBranch_0146:
					break;
				}
			}
			_matApplied = false;
			return;
		}
		if (!_boardsDone)
		{
			FindBoards();
			if (Settings.CapturedVariables3760_Color_03 == Settings.ColorMode.Pinwheel)
			{
				goto Branch_01ef;
			}
		}
		else if (Settings.CapturedVariables3760_Color_03 == Settings.ColorMode.Pinwheel)
		{
			goto Branch_01ef;
		}
		Material val2;
		int num;
		if ((Object)(object)_cachedBoardPinwheelMat != (Object)null)
		{
			Object.Destroy((Object)(object)_cachedBoardPinwheelMat);
			_cachedBoardPinwheelMat = null;
			Color val = Settings.GetAnimatedColor(Settings.CapturedVariables3760_Color_03, (Color32)(Settings.CapturedVariables3760_Color_22), (Color32)(Settings.CapturedVariables3760_Color_27), Settings.CapturedVariables3760_Value_17, 6);
			_mat.color = new Color(val.r * 0.75f, val.g * 0.75f, val.b * 0.75f, val.a);
			val2 = _mat;
			num = 0;
		}
		else
		{
			Color val = Settings.GetAnimatedColor(Settings.CapturedVariables3760_Color_03, (Color32)(Settings.CapturedVariables3760_Color_22), (Color32)(Settings.CapturedVariables3760_Color_27), Settings.CapturedVariables3760_Value_17, 6);
			_mat.color = new Color(val.r * 0.75f, val.g * 0.75f, val.b * 0.75f, val.a);
			val2 = _mat;
			num = 0;
		}
		Branch_0470:
		if (num < _pinned.Count)
		{
			while (true)
			{
				Renderer val3 = _pinned[num];
				if ((Object)(object)val3 != (Object)null && (Object)(object)val3.sharedMaterial != (Object)(object)val2)
				{
					val3.sharedMaterial = val2;
					num++;
					if (num >= _pinned.Count)
					{
						break;
					}
				}
				else
				{
					num++;
					if (num >= _pinned.Count)
					{
						break;
					}
				}
			}
		}
		using (Dictionary<string, GameObject>.ValueCollection.Enumerator enumerator3 = _scenePlanes.Values.GetEnumerator())
		{
			if (enumerator3.MoveNext())
			{
				while (true)
				{
					GameObject current3 = enumerator3.Current;
					if ((Object)(object)current3 != (Object)null)
					{
						Renderer component = current3.GetComponent<Renderer>();
						if ((Object)(object)component != (Object)null && (Object)(object)component.sharedMaterial != (Object)(object)val2)
						{
							component.sharedMaterial = val2;
							if (!enumerator3.MoveNext())
							{
								break;
							}
						}
						else if (!enumerator3.MoveNext())
						{
							break;
						}
					}
					else if (!enumerator3.MoveNext())
					{
						break;
					}
				}
			}
		}
		using (List<JoinScreens>.Enumerator enumerator4 = _joinScreens.GetEnumerator())
		{
			while (enumerator4.MoveNext())
			{
				while (true)
				{
					JoinScreens current4 = enumerator4.Current;
					if ((Object)(object)current4.Template == (Object)null)
					{
						break;
					}
					if ((Object)(object)current4.Template.ScreenBG_AlreadyInRoom != (Object)(object)val2)
					{
						current4.Template.ScreenBG_AbandonPartyAndSoloJoin = val2;
						current4.Template.ScreenBG_AlreadyInRoom = val2;
						current4.Template.ScreenBG_ChangingGameModeSoloJoin = val2;
						current4.Template.ScreenBG_Error = val2;
						current4.Template.ScreenBG_InPrivateRoom = val2;
						current4.Template.ScreenBG_LeaveRoomAndGroupJoin = val2;
						current4.Template.ScreenBG_LeaveRoomAndSoloJoin = val2;
						current4.Template.ScreenBG_NotConnectedSoloJoin = val2;
						PhotonNetworkController instance = PhotonNetworkController.Instance;
						if (instance != null)
						{
							((PhotonNetworkController)instance).UpdateTriggerScreens();
							if (!enumerator4.MoveNext())
							{
								goto EndBranch_06f3;
							}
						}
						else if (!enumerator4.MoveNext())
						{
							goto EndBranch_06f3;
						}
					}
					else if (!enumerator4.MoveNext())
					{
						goto EndBranch_06f3;
					}
				}
				continue;
				EndBranch_06f3:
				break;
			}
		}
		if (_matApplied)
		{
			return;
		}
		using (Dictionary<string, GameObject>.ValueCollection.Enumerator enumerator5 = _scenePlanes.Values.GetEnumerator())
		{
			while (enumerator5.MoveNext())
			{
				while (true)
				{
					GameObject current5 = enumerator5.Current;
					if (!((Object)(object)current5 != (Object)null))
					{
						break;
					}
					current5.SetActive(true);
					if (!enumerator5.MoveNext())
					{
						goto EndBranch_07ac;
					}
				}
				continue;
				EndBranch_07ac:
				break;
			}
		}
		_matApplied = true;
		return;
		Branch_01ef:
		if ((Object)(object)_cachedBoardPinwheelMat == (Object)null)
		{
			_cachedBoardPinwheelMat = Main.CreatePinwheelMaterial();
			val2 = _cachedBoardPinwheelMat;
		}
		else
		{
			AssetHandler.SetMaterialProperty(_cachedBoardPinwheelMat, "_Speed", 0f - Main.CapturedVariables1950_Value_06);
			AssetHandler.SetMaterialProperty(_cachedBoardPinwheelMat, "_COLOR1", Main.CapturedVariables1950_Color_02);
			AssetHandler.SetMaterialProperty(_cachedBoardPinwheelMat, "_COLOR2", Main.CapturedVariables1950_Color_06);
			val2 = _cachedBoardPinwheelMat;
		}
		num = 0;
		goto Branch_0470;
	}
}
