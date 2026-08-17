using System;
using System.Collections.Generic;
using GorillaLocomotion;
using NXO.Menu;
using NXO.Mods;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace NXO.Utilities;

public class Variables
{
	public static GameObject Variables_Object_14;

	public static GameObject Variables_Object_09;

	public static GameObject Variables_Object_03;

	public static GameObject Variables_Object_10;

	public static GameObject Variables_Object_07;

	public static GameObject Variables_Object_01;

	public static GameObject Variables_Object_11;

	public static GameObject Variables_Object_12;

	public static GameObject Variables_Object_02;

	public static Text Variables_Reference_08;

	public static Text Variables_Reference_03;

	private static Category Variables_Reference_04;

	public static int Variables_Index_04;

	public static int Variables_Index_01;

	public static float Variables_Value_04;

	public static bool Variables_State_01;

	public static bool Variables_State_06;

	public static bool Variables_State_05;

	public static bool Variables_State_12;

	public static bool Variables_State_13;

	public static bool Variables_State_10;

	public static bool Variables_State_07;

	public static bool Variables_State_08;

	public static bool Variables_State_03;

	public static KeyCode Variables_Reference_01;

	public static bool Variables_State_04;

	public static bool Variables_State_16;

	public static bool Variables_State_02;

	public static bool Variables_State_15;

	public static bool Variables_State_14;

	public static bool Variables_State_09;

	public static AudioClip Variables_Audio_03;

	public static AudioClip Variables_Audio_02;

	public static AudioClip Variables_Audio_01;

	public static string Variables_Text_01;

	public static GTPlayer Variables_Reference_06;

	public static GorillaTagger Variables_Reference_09;

	public static GameObject Variables_Object_13;

	public static GameObject Variables_Object_08;

	public static GameObject Variables_Object_05;

	public static GameObject Variables_Object_04;

	public static bool Variables_State_11;

	public static float Variables_Value_03;

	public static float Variables_Value_02;

	public static int Variables_Index_02;

	public static float Variables_Value_01;

	public static string Variables_Text_02;

	private static readonly Dictionary<Type, Array> Variables_Reference_07;

	public static readonly Dictionary<string, GameObject> Variables_Object_06;

	public static Shader Variables_Reference_02;

	public static Shader Variables_Reference_10;

	public static Shader Variables_Reference_11;

	public static Shader Variables_Reference_05;

	private static int? Variables_Index_03;

	public static Category currentPage
	{
		get
		{
			return Variables_Reference_04;
		}
		set
		{
			Variables_Reference_04 = value;
			Main.CapturedVariables1950_Reference_09 = Category.Home;
			Main.CapturedVariables1950_Index_01 = -1;
		}
	}

	static Variables()
	{
		Variables_Index_04 = 0;
		Variables_Index_01 = 7;
		Variables_Value_04 = 0.09f;
		Variables_State_01 = true;
		Variables_State_06 = true;
		Variables_State_05 = false;
		Variables_State_12 = true;
		Variables_State_10 = false;
		Variables_State_07 = false;
		Variables_State_08 = false;
		Variables_State_03 = false;
		Variables_Reference_01 = (KeyCode)308;
		Variables_State_14 = true;
		Variables_State_09 = true;
		Variables_State_11 = false;
		Variables_Value_03 = 0f;
		Variables_Value_02 = 0f;
		Variables_Value_01 = 0f;
		Variables_Text_02 = "https://nxo.lol/";
		Variables_Reference_07 = new Dictionary<Type, Array>();
		Variables_Object_06 = new Dictionary<string, GameObject>();
		Variables_Reference_02 = Shader.Find("GUI/Text Shader");
		Variables_Reference_10 = Shader.Find("GorillaTag/UberShader");
		Variables_Reference_11 = Shader.Find("UI/Default");
		Variables_Reference_05 = Shader.Find("Universal Render Pipeline/Lit");
	}

	public static Color ColorFromHue(float hue)
	{
		return Color.HSVToRGB(hue % 1f, 1f, 1f);
	}

	public static Color RandomColor()
	{
		return (Color32)(new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), byte.MaxValue));
	}

	public static bool IsMasterClient()
	{
		if (PhotonNetwork.InRoom)
		{
			return PhotonNetwork.IsMasterClient;
		}
		return false;
	}

	public static GameObject FindCachedGameObject(string find)
	{
		if (Variables_Object_06.TryGetValue(find, out GameObject value) && (Object)(object)value != (Object)null)
		{
			return value;
		}
		Variables_Object_06.Remove(find);
		GameObject val = GameObject.Find(find);
		if ((Object)(object)val != (Object)null)
		{
			Variables_Object_06[find] = val;
			return val;
		}
		return val;
	}

	public static string GetGameMode()
	{
		Room currentRoom = PhotonNetwork.CurrentRoom;
		if (currentRoom?.CustomProperties == null || !((Dictionary<object, object>)(object)currentRoom.CustomProperties).ContainsKey("gameMode"))
		{
			return "ERROR";
		}
		return currentRoom.CustomProperties["gameMode"].ToString();
	}

	public static Quaternion RandomRotation()
	{
		return Random.rotationUniform;
	}

	public static Vector3 RandomPosition(float range = 1f)
	{
		return Random.insideUnitSphere * range;
	}

	public static bool IsGameMode(string gamemodeName)
	{
		return GorillaGameManager.instance.GameModeName().IndexOf(gamemodeName, StringComparison.OrdinalIgnoreCase) >= 0;
	}

	public static int GetInteractionLayerMask()
	{
		int valueOrDefault = Variables_Index_03.GetValueOrDefault();
		if (!Variables_Index_03.HasValue)
		{
			Variables_Index_03 = ~((1 << LayerMask.NameToLayer("TransparentFX")) | (1 << LayerMask.NameToLayer("Ignore Raycast")) | (1 << LayerMask.NameToLayer("Zone")) | (1 << LayerMask.NameToLayer("Gorilla Trigger")) | (1 << LayerMask.NameToLayer("Gorilla Boundary")) | (1 << LayerMask.NameToLayer("GorillaCosmetics")) | (1 << LayerMask.NameToLayer("GorillaParticle")));
			return valueOrDefault;
		}
		return valueOrDefault;
	}

	public static T[] FindObjectsCached<T>(bool refresh = false) where T : UnityEngine.Object
	{
		if (!refresh && Variables_Reference_07.TryGetValue(typeof(T), out Array value))
		{
			return (T[])value;
		}
		Variables_Reference_07.Remove(typeof(T));
		T[] array = Object.FindObjectsOfType<T>(true);
		Variables_Reference_07[typeof(T)] = array;
		return array;
	}
}
