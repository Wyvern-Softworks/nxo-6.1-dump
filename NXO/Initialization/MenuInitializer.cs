using GorillaLocomotion;
using HarmonyLib;
using NXO.Menu;
using NXO.Utilities;
using UnityEngine;

namespace NXO.Initialization;

[HarmonyPatch(typeof(GTPlayer), "LateUpdate")]
internal class MenuInitializer
{
	private static GameObject MenuInitializer_Object_01;

	[HarmonyPostfix]
	private static void Postfix()
	{
		if (!((Object)(object)MenuInitializer_Object_01 != (Object)null))
		{
			MenuInitializer_Object_01 = new GameObject("NXO");
			MenuInitializer_Object_01.AddComponent<Main>();
			MenuInitializer_Object_01.AddComponent<NotificationLib>();
			MenuInitializer_Object_01.AddComponent<CustomBoards>();
			MenuInitializer_Object_01.AddComponent<NXOUI>();
			MenuInitializer_Object_01.AddComponent<NetworkingLibrary>();
			MenuInitializer_Object_01.AddComponent<CoroutineHelper>();
			Object.DontDestroyOnLoad((Object)(object)MenuInitializer_Object_01);
			Debug.Log((object)"NXO initialized.");
		}
	}
}
