using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using ExitGames.Client.Photon;
using UnityEngine;

namespace NXO.Utilities;

public static class Logger
{
	[CompilerGenerated]
	private sealed class CapturedVariables40
	{
		public Hashtable hash;

		internal string FormatData_Lambda2(object k)
		{
			return $"{k}={FormatData(hash[k])}";
		}
	}

	public static bool CapturedVariables40_State_01 = false;

	public static HashSet<byte> CapturedVariables40_Set_01 = new HashSet<byte> { 3, 200, 201, 203, 206 };

	public static HashSet<string> CapturedVariables40_Set_02 = new HashSet<string> { "OnHandTapRPCShared", "TransferOwnershipFromToRPC", "PieceDestroyedRPC", "TriggerAttractAnim" };

	public static string FormatData(object data)
	{
		if (data == null)
		{
			return "null";
		}
		try
		{
			CapturedVariables40 LocalScope4 = new CapturedVariables40();
			if (data is Dictionary<byte, object> source)
			{
				IEnumerable<string> values = source.Select((KeyValuePair<byte, object> kvp) => $"[{kvp.Key}]={FormatData(kvp.Value)}");
				return "{" + string.Join(", ", values) + "}";
			}
			if (data is object[] array)
			{
				IEnumerable<string> values2 = from x in array.Take(10)
					select FormatData(x);
				string text = "[" + string.Join(", ", values2) + "]";
				if (array.Length > 10)
				{
					return text + $" +{array.Length - 10}";
				}
				return text;
			}
			LocalScope4.hash = (Hashtable)((data is Hashtable) ? data : null);
			if (LocalScope4.hash != null)
			{
				IEnumerable<string> values3 = from object k in ((Dictionary<object, object>)(object)LocalScope4.hash).Keys
					select $"{k}={FormatData(LocalScope4.hash[k])}";
				return "{" + string.Join(", ", values3) + "}";
			}
			if (data is byte[] array2)
			{
				return $"byte[{array2.Length}]";
			}
			return data.ToString();
		}
		catch (Exception)
		{
			if (data == null)
			{
				return null ?? "null";
			}
			return data.ToString() ?? "null";
		}
	}

	public static void SetLoggingEnabled(bool enabled)
	{
		if (CapturedVariables40_State_01 != enabled)
		{
			CapturedVariables40_State_01 = enabled;
			Debug.Log((object)("[UltimateLogger] Logging " + (enabled ? "ENABLED" : "Recovered_Reference_07")));
		}
	}
}
