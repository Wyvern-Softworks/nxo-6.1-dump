using System;
using System.Collections;
using UnityEngine;

namespace NXO.Utilities;

public class CoroutineHelper : MonoBehaviour
{
	public static CoroutineHelper Instance { get; private set; }

	public static IEnumerator DestroyAfterDelay(GameObject obj, float delay)
	{
		yield return (object)new WaitForSeconds(delay);
		if ((Object)(object)obj != (Object)null)
		{
			Object.Destroy((Object)(object)obj);
		}
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

	private static IEnumerator InvokeAfterDelayCoroutine(float time, Action afterDelay)
	{
		yield return (object)new WaitForSeconds(time);
		try
		{
			afterDelay();
		}
		catch (Exception ex)
		{
			Exception e = ex;
			Debug.LogError((object)$"Delay coroutine caught exception: {e}");
		}
	}

	public static void InvokeAfterDelay(float time, Action afterDelay)
	{
		if (afterDelay == null)
		{
			Debug.LogError((object)"Delay called with null action!");
		}
		else
		{
			((MonoBehaviour)Instance).StartCoroutine(InvokeAfterDelayCoroutine(time, afterDelay));
		}
	}
}
