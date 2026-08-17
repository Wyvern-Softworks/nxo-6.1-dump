using System;

using HarmonyLib;
using UnityEngine;

namespace NXO.Initialization;

public class Loader
{
	public Loader()
	{
	}

	public static void Load()
	{
		try
		{
			new Harmony("com.nxo.nxomodmenu.org").PatchAll();
			Debug.Log((object)"NXO v6.1 initialized.");
		}
		catch (Exception arg)
		{
			Debug.LogError((object)string.Format("Failed to initialize {0}: {1}", "NXO", arg));
		}
	}
}
