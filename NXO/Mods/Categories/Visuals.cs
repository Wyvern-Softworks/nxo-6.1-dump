using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using GorillaLocomotion;
using GorillaTag.Rendering;
using NXO.Menu;
using NXO.Utilities;
using UnityEngine;

namespace NXO.Mods.Categories;

public class Visuals
{
	private class ESPData
	{
		public Dictionary<VRRig, GameObject> singleObjects = new Dictionary<VRRig, GameObject>();

		public Dictionary<VRRig, List<GameObject>> multiObjects = new Dictionary<VRRig, List<GameObject>>();

		public Material material;

		public void Clear()
		{
			using (Dictionary<VRRig, GameObject>.ValueCollection.Enumerator enumerator = singleObjects.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					while (true)
					{
						GameObject current = enumerator.Current;
						if (!((Object)(object)current != (Object)null))
						{
							break;
						}
						Object.Destroy((Object)(object)current);
						if (!enumerator.MoveNext())
						{
							goto EndBranch_0064;
						}
					}
					continue;
					EndBranch_0064:
					break;
				}
			}
			singleObjects.Clear();
			foreach (List<GameObject> value in multiObjects.Values)
			{
				using List<GameObject>.Enumerator enumerator3 = value.GetEnumerator();
				while (enumerator3.MoveNext())
				{
					while (true)
					{
						GameObject current2 = enumerator3.Current;
						if (!((Object)(object)current2 != (Object)null))
						{
							break;
						}
						Object.Destroy((Object)(object)current2);
						if (!enumerator3.MoveNext())
						{
							goto EndBranch_012a;
						}
					}
					continue;
					EndBranch_012a:
					break;
				}
			}
			multiObjects.Clear();
			if ((Object)(object)material != (Object)null)
			{
				Object.Destroy((Object)(object)material);
				material = null;
			}
		}
	}

	[CompilerGenerated]
	private sealed class CapturedVariables270
	{
		public List<VRRig> activeRigs;

		internal bool PredictionESP_Lambda0(VRRig rig)
		{
			if (!((Object)(object)rig == (Object)null))
			{
				return !activeRigs.Contains(rig);
			}
			return true;
		}
	}

	[CompilerGenerated]
	private sealed class CapturedVariables570
	{
		public List<VRRig> activeRigs;

		internal bool Nametags_Lambda0(VRRig r)
		{
			if (!((Object)(object)r == (Object)null) && activeRigs.Contains(r))
			{
				return r.isOfflineVRRig;
			}
			return true;
		}
	}

	[CompilerGenerated]
	private sealed class CapturedVariables580
	{
		public List<VRRig> activeRigs;

		internal bool NXONametags_Lambda0(VRRig r)
		{
			if (!((Object)(object)r == (Object)null) && activeRigs.Contains(r) && !r.isOfflineVRRig)
			{
				NetPlayer creator = r.Creator;
				return !NetworkingLibrary.IsNxoUser((creator != null) ? creator.UserId : null);
			}
			return true;
		}
	}

	private static readonly List<VRRig> CapturedVariables580_Items_02 = new List<VRRig>();

	private static ESPData CapturedVariables580_Reference_02 = new ESPData();

	private static ESPData CapturedVariables580_Reference_05 = new ESPData();

	private static ESPData CapturedVariables580_Reference_04 = new ESPData();

	private static ESPData CapturedVariables580_Reference_03 = new ESPData();

	private static ESPData CapturedVariables580_Reference_08 = new ESPData();

	private static ESPData CapturedVariables580_Reference_06 = new ESPData();

	private static Dictionary<VRRig, GameObject> CapturedVariables580_Lookup_02 = new Dictionary<VRRig, GameObject>();

	private static Dictionary<VRRig, GameObject> CapturedVariables580_Lookup_05 = new Dictionary<VRRig, GameObject>();

	private static bool CapturedVariables580_State_02 = false;

	private static bool CapturedVariables580_State_05 = false;

	private static readonly int[][] CapturedVariables580_Index_02 = new int[12][]
	{
		new int[2] { 0, 1 },
		new int[2] { 1, 2 },
		new int[2] { 2, 3 },
		new int[2] { 3, 0 },
		new int[2] { 4, 5 },
		new int[2] { 5, 6 },
		new int[2] { 6, 7 },
		new int[2] { 7, 4 },
		new int[2] { 0, 4 },
		new int[2] { 1, 5 },
		new int[2] { 2, 6 },
		new int[2] { 3, 7 }
	};

	public static int[] CapturedVariables580_Index_01 = new int[38]
	{
		4, 3, 5, 4, 19, 18, 20, 19, 3, 18,
		21, 20, 22, 21, 25, 21, 29, 21, 31, 29,
		27, 25, 24, 22, 6, 5, 7, 6, 10, 6,
		14, 6, 16, 14, 12, 10, 9, 7
	};

	public static int CapturedVariables580_Index_03 = 0;

	public static bool CapturedVariables580_State_07 = false;

	private static bool CapturedVariables580_State_01 = false;

	private static readonly Dictionary<Renderer, Material> CapturedVariables580_Lookup_04 = new Dictionary<Renderer, Material>();

	private static ESPData CapturedVariables580_Reference_07 = new ESPData();

	private static Dictionary<VRRig, Vector3> CapturedVariables580_Lookup_03 = new Dictionary<VRRig, Vector3>();

	private static Dictionary<VRRig, Vector3> CapturedVariables580_Lookup_01 = new Dictionary<VRRig, Vector3>();

	private static ESPData CapturedVariables580_Reference_01 = new ESPData();

	private static ESPData CapturedVariables580_Reference_09 = new ESPData();

	private static Dictionary<VRRig, Material> CapturedVariables580_Lookup_06 = new Dictionary<VRRig, Material>();

	private static bool CapturedVariables580_State_04 = false;

	public static GameObject CapturedVariables580_Object_01;

	private static Vector3 CapturedVariables580_Position_01;

	private static bool CapturedVariables580_State_03 = false;

	private static float CapturedVariables580_Value_01;

	private static readonly List<Renderer> CapturedVariables580_Items_01 = new List<Renderer>();

	private static List<GameObject> CapturedVariables580_Items_03 = new List<GameObject>();

	private static AudioSource CapturedVariables580_Audio_01 = null;

	private static float CapturedVariables580_Value_02 = 0f;

	private const float SENSE_FAR = 15f;

	private const float SENSE_CLOSE = 8f;

	private const float SENSE_DANGER = 3f;

	public static bool CapturedVariables580_State_06 = false;

	public static void SetFuckColorsEnabled(bool enable)
	{
		((BetterDayNightManager)BetterDayNightManager.instance).AnimateLightFlash(2, (float)((!enable) ? 2 : 0), (float)((!enable) ? 2 : 0), 2f);
	}

	public static void EnsureWhiteVertexColors(VRRig rig)
	{
		if ((Object)(object)rig == (Object)null || (Object)(object)rig.mainSkin == (Object)null || (Object)(object)rig.mainSkin.sharedMesh == (Object)null || CapturedVariables580_Items_02.Contains(rig))
		{
			return;
		}
		CapturedVariables580_Items_02.Add(rig);
		if (rig.mainSkin.sharedMesh.colors32 != null && rig.mainSkin.sharedMesh.colors32.Length != 0)
		{
			Color32[] array = (Color32[])(object)new Color32[rig.mainSkin.sharedMesh.colors32.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (Color32)(Color.white);
			}
			rig.mainSkin.sharedMesh.colors32 = array;
			if (rig.mainSkin.sharedMesh.colors == null)
			{
				return;
			}
		}
		else if (rig.mainSkin.sharedMesh.colors == null)
		{
			return;
		}
		if (rig.mainSkin.sharedMesh.colors.Length != 0)
		{
			Color[] array2 = (Color[])(object)new Color[rig.mainSkin.sharedMesh.colors.Length];
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j] = Color.white;
			}
			rig.mainSkin.sharedMesh.colors = array2;
		}
	}

	public static void VR3rdPerson()
	{
		InputHandler.UpdateToggleOnPress(InputHandler.IsLeftPrimaryPressed, ref InputHandler.InputHandler_State_02, ref CapturedVariables580_State_03);
		if (!InputHandler.InputHandler_State_02)
		{
			ResetCamera();
		}
		else if ((Object)(object)CapturedVariables580_Object_01 == (Object)null)
		{
			CapturedVariables580_Object_01 = new GameObject("NXO_Cam");
			CapturedVariables580_Object_01.transform.position = ((Component)Variables.Variables_Reference_09.headCollider).transform.position;
			Camera val = CapturedVariables580_Object_01.GetComponent<Camera>() ?? CapturedVariables580_Object_01.AddComponent<Camera>();
			val.nearClipPlane = 0.01f;
			val.cameraType = (CameraType)1;
			CapturedVariables580_Object_01.transform.position = ((Component)Variables.Variables_Reference_09.bodyCollider).transform.TransformPoint(new Vector3(0f, 0.8f, -1.5f));
			CapturedVariables580_Object_01.transform.rotation = ((Component)Variables.Variables_Reference_09.headCollider).transform.rotation;
		}
		else
		{
			Camera val = CapturedVariables580_Object_01.GetComponent<Camera>() ?? CapturedVariables580_Object_01.AddComponent<Camera>();
			val.nearClipPlane = 0.01f;
			val.cameraType = (CameraType)1;
			CapturedVariables580_Object_01.transform.position = ((Component)Variables.Variables_Reference_09.bodyCollider).transform.TransformPoint(new Vector3(0f, 0.8f, -1.5f));
			CapturedVariables580_Object_01.transform.rotation = ((Component)Variables.Variables_Reference_09.headCollider).transform.rotation;
		}
	}

	public static void SetToggleSnowEnabled(bool enable)
	{
		GameObject obj = GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Environment/WeatherDayNight");
		object obj2;
		if (obj == null)
		{
			obj2 = null;
		}
		else
		{
			Transform transform = obj.transform;
			if (transform == null)
			{
				obj2 = null;
			}
			else
			{
				Transform obj3 = transform.Find("snow");
				obj2 = ((obj3 != null) ? ((Component)obj3).gameObject : null);
			}
		}
		GameObject val = (GameObject)obj2;
		if (!((Object)(object)val == (Object)null))
		{
			val.SetActive(enable);
			((Behaviour)val.GetComponent<TimeOfDayDependentAudio>()).enabled = !enable;
			Transform val2 = val.transform.Find("snow partic");
			if ((Object)(object)val2 != (Object)null)
			{
				((Component)val2).gameObject.SetActive(enable);
			}
		}
	}

	private static Vector3[] GetBoxCorners(Vector3 center, Vector3 halfSize, Quaternion rotation)
	{
		Vector3[] array = (Vector3[])(object)new Vector3[8]
		{
			new Vector3(0f - halfSize.x, 0f - halfSize.y, 0f - halfSize.z),
			new Vector3(halfSize.x, 0f - halfSize.y, 0f - halfSize.z),
			new Vector3(halfSize.x, 0f - halfSize.y, halfSize.z),
			new Vector3(0f - halfSize.x, 0f - halfSize.y, halfSize.z),
			new Vector3(0f - halfSize.x, halfSize.y, 0f - halfSize.z),
			new Vector3(halfSize.x, halfSize.y, 0f - halfSize.z),
			new Vector3(halfSize.x, halfSize.y, halfSize.z),
			new Vector3(0f - halfSize.x, halfSize.y, halfSize.z)
		};
		Vector3[] array2 = (Vector3[])(object)new Vector3[8];
		int num = 0;
		if (num < 8)
		{
			do
			{
				array2[num] = center + rotation * array[num];
				num++;
			}
			while (num < 8);
		}
		return array2;
	}

	public static string GetPlatformLabel(VRRig rig)
	{
		if (ReflectionCompat.Invoke(rig, "HasCosmetic", "S. FIRST LOGIN") is bool hasSteamCosmetic && hasSteamCosmetic)
		{
			return "<color=#001b96>STEAM</color>";
		}
		if (ReflectionCompat.Invoke(rig, "HasCosmetic", "FIRST LOGIN") is bool hasPcCosmetic && hasPcCosmetic)
		{
			return "<color=#4CFF4C>PC</color>";
		}
		return "<color=#FFA500>QUEST</color>";
	}

	public static void ResetCamera()
	{
		InputHandler.InputHandler_State_02 = false;
		if ((Object)(object)CapturedVariables580_Object_01 != (Object)null)
		{
			Object.Destroy((Object)(object)CapturedVariables580_Object_01.GetComponent<Camera>());
			Object.Destroy((Object)(object)CapturedVariables580_Object_01);
			CapturedVariables580_Object_01 = null;
		}
	}

	public static void SetTrippyMonkesEnabled(bool enable)
	{
		if (!enable)
		{
			if (!CapturedVariables580_State_04)
			{
				return;
			}
			using (Dictionary<VRRig, Material>.Enumerator enumerator = CapturedVariables580_Lookup_06.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					while (true)
					{
						KeyValuePair<VRRig, Material> current = enumerator.Current;
						VRRig key = current.Key;
						Material value = current.Value;
						if ((Object)(object)key != (Object)null && (Object)(object)key.mainSkin != (Object)null && (Object)(object)value != (Object)null)
						{
							((Renderer)key.mainSkin).material = value;
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
			CapturedVariables580_Lookup_06.Clear();
			CapturedVariables580_State_04 = false;
		}
		else
		{
			if (CapturedVariables580_State_04)
			{
				return;
			}
			Material val = AssetHandler.LoadMaterial("NXO.Resources.acidtrip", "AcidTrip");
			if ((Object)(object)val == (Object)null)
			{
				return;
			}
			using (IEnumerator<VRRig> enumerator2 = VRRigCache.ActiveRigs.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					while (true)
					{
						VRRig current2 = enumerator2.Current;
						if ((Object)(object)current2 == (Object)null || (Object)(object)current2.mainSkin == (Object)null || (Object)(object)((Renderer)current2.mainSkin).material == (Object)null || current2.isOfflineVRRig || (Object)(object)current2 == (Object)(object)Variables.Variables_Reference_09.offlineVRRig)
						{
							break;
						}
						if (!CapturedVariables580_Lookup_06.ContainsKey(current2))
						{
							CapturedVariables580_Lookup_06[current2] = ((Renderer)current2.mainSkin).material;
							((Renderer)current2.mainSkin).material = val;
							if (!enumerator2.MoveNext())
							{
								goto EndBranch_032d;
							}
						}
						else
						{
							((Renderer)current2.mainSkin).material = val;
							if (!enumerator2.MoveNext())
							{
								goto EndBranch_032d;
							}
						}
					}
					continue;
					EndBranch_032d:
					break;
				}
			}
			CapturedVariables580_State_04 = true;
		}
	}

	public static void SetFirstPersonCamEnabled(bool enable)
	{
		if ((Object)(object)Variables.Variables_Object_08 == (Object)null)
		{
			Variables.Variables_Object_08 = GameObject.Find("Shoulder Camera");
			if ((Object)(object)Variables.Variables_Object_13 == (Object)null)
			{
				goto Branch_006c;
			}
		}
		else if ((Object)(object)Variables.Variables_Object_13 == (Object)null)
		{
			goto Branch_006c;
		}
		if (!enable)
		{
			goto Branch_01c6;
		}
		goto Branch_00a5;
		Branch_0114:
		Variables.Variables_Object_05.SetActive(false);
		if (!((Object)(object)Variables.Variables_Object_08 != (Object)null))
		{
			return;
		}
		Branch_015d:
		Variables.Variables_Object_08.transform.SetParent(((Component)Camera.main).transform);
		Variables.Variables_Object_08.transform.localPosition = Vector3.zero;
		Variables.Variables_Object_08.transform.localRotation = Quaternion.identity;
		Variables.Variables_Object_08.GetComponent<Camera>().fieldOfView = Settings.CapturedVariables3760_Value_11;
		return;
		Branch_006c:
		Variables.Variables_Object_13 = GameObject.Find("Third Person Camera");
		if (!enable)
		{
			goto Branch_01c6;
		}
		goto Branch_00a5;
		Branch_01c6:
		if ((Object)(object)Variables.Variables_Object_05 != (Object)null)
		{
			Variables.Variables_Object_05.SetActive(true);
			if (!((Object)(object)Variables.Variables_Object_08 != (Object)null))
			{
				return;
			}
		}
		else if (!((Object)(object)Variables.Variables_Object_08 != (Object)null))
		{
			return;
		}
		if ((Object)(object)Variables.Variables_Object_13 != (Object)null)
		{
			Variables.Variables_Object_08.transform.SetParent(Variables.Variables_Object_13.transform);
			Variables.Variables_Object_08.transform.localPosition = Vector3.zero;
			Variables.Variables_Object_08.transform.localRotation = Quaternion.identity;
		}
		return;
		Branch_00a5:
		if (!Variables.Variables_State_11)
		{
			Variables.Variables_State_11 = true;
			Variables.Variables_Object_05 = GameObject.Find("CM vcam1");
			if ((Object)(object)Variables.Variables_Object_05 != (Object)null)
			{
				goto Branch_0114;
			}
		}
		else if ((Object)(object)Variables.Variables_Object_05 != (Object)null)
		{
			goto Branch_0114;
		}
		if (!((Object)(object)Variables.Variables_Object_08 != (Object)null))
		{
			return;
		}
		goto Branch_015d;
	}

	public static void SpectateGun()
	{
		if (GunLib.TrySelectRig())
		{
			if ((Object)(object)CapturedVariables580_Object_01 == (Object)null)
			{
				CapturedVariables580_Object_01 = new GameObject("NXO_Cam");
				CapturedVariables580_Object_01.transform.position = ((Component)Variables.Variables_Reference_09.headCollider).transform.position;
				Camera val = CapturedVariables580_Object_01.GetComponent<Camera>() ?? CapturedVariables580_Object_01.AddComponent<Camera>();
				val.nearClipPlane = 0.01f;
				val.cameraType = (CameraType)1;
				CapturedVariables580_Object_01.transform.position = GunLib.GunLib_Reference_06.bodyTransform.TransformPoint(new Vector3(0f, 0.8f, -1.5f));
				CapturedVariables580_Object_01.transform.rotation = GunLib.GunLib_Reference_06.headMesh.transform.rotation;
			}
			else
			{
				Camera val = CapturedVariables580_Object_01.GetComponent<Camera>() ?? CapturedVariables580_Object_01.AddComponent<Camera>();
				val.nearClipPlane = 0.01f;
				val.cameraType = (CameraType)1;
				CapturedVariables580_Object_01.transform.position = GunLib.GunLib_Reference_06.bodyTransform.TransformPoint(new Vector3(0f, 0.8f, -1.5f));
				CapturedVariables580_Object_01.transform.rotation = GunLib.GunLib_Reference_06.headMesh.transform.rotation;
			}
		}
		else
		{
			ResetCamera();
		}
	}

	private static Color32 GetEspColor(bool isInfected, bool teamChecked, VRRig rig)
	{
		if (teamChecked)
		{
			if (!isInfected)
			{
				return new Color32((byte)0, byte.MaxValue, (byte)0, (byte)155);
			}
			return new Color32(byte.MaxValue, (byte)0, (byte)0, (byte)155);
		}
		if (isInfected)
		{
			return new Color32(byte.MaxValue, (byte)0, (byte)0, (byte)155);
		}
		Color playerColor = rig.playerColor;
		return new Color32((byte)(playerColor.r * 255f), (byte)(playerColor.g * 255f), (byte)(playerColor.b * 255f), (byte)155);
	}

	public static void DrunkCam()
	{
		InputHandler.UpdateToggleOnPress(InputHandler.IsLeftPrimaryPressed, ref InputHandler.InputHandler_State_02, ref CapturedVariables580_State_03);
		if (!InputHandler.InputHandler_State_02)
		{
			ResetCamera();
		}
		else if ((Object)(object)CapturedVariables580_Object_01 == (Object)null)
		{
			CapturedVariables580_Object_01 = new GameObject("NXO_Cam");
			CapturedVariables580_Object_01.transform.position = ((Component)Variables.Variables_Reference_09.headCollider).transform.position;
			float num = 15f;
			Camera val = CapturedVariables580_Object_01.GetComponent<Camera>() ?? CapturedVariables580_Object_01.AddComponent<Camera>();
			val.nearClipPlane = 0.01f;
			val.cameraType = (CameraType)1;
			CapturedVariables580_Object_01.transform.position = ((Component)Variables.Variables_Reference_09.headCollider).transform.position;
			CapturedVariables580_Object_01.transform.rotation = ((Component)Variables.Variables_Reference_09.headCollider).transform.rotation * Quaternion.Euler(Mathf.Sin(Time.time) * num, Mathf.Cos(Time.time * 0.7f) * num, Mathf.Sin(Time.time * 1.3f) * num);
		}
		else
		{
			float num = 15f;
			Camera val = CapturedVariables580_Object_01.GetComponent<Camera>() ?? CapturedVariables580_Object_01.AddComponent<Camera>();
			val.nearClipPlane = 0.01f;
			val.cameraType = (CameraType)1;
			CapturedVariables580_Object_01.transform.position = ((Component)Variables.Variables_Reference_09.headCollider).transform.position;
			CapturedVariables580_Object_01.transform.rotation = ((Component)Variables.Variables_Reference_09.headCollider).transform.rotation * Quaternion.Euler(Mathf.Sin(Time.time) * num, Mathf.Cos(Time.time * 0.7f) * num, Mathf.Sin(Time.time * 1.3f) * num);
		}
	}

	public static void SetSkeletonESPEnabled(bool enable)
	{
		if (enable)
		{
			using IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				while (true)
				{
					VRRig current = enumerator.Current;
					if ((Object)(object)current == (Object)(object)Variables.Variables_Reference_09.offlineVRRig)
					{
						break;
					}
					Color32 val = GetEspColor(RigManager.IsTagged(current), Variables.Variables_State_07, current);
					((SyncToPlayerColor)current.skeleton).UpdateColor(current.playerColor);
					((Renderer)current.skeleton.renderer).sharedMaterial.shader = Shader.Find("GUI/Text Shader");
					((Renderer)current.skeleton.renderer).sharedMaterial.color = (Color32)(val);
					((Behaviour)current.skeleton).enabled = true;
					((Renderer)current.skeleton.renderer).enabled = true;
					if (!enumerator.MoveNext())
					{
						return;
					}
				}
			}
			return;
		}
		using IEnumerator<VRRig> enumerator2 = VRRigCache.ActiveRigs.GetEnumerator();
		while (enumerator2.MoveNext())
		{
			while (true)
			{
				VRRig current2 = enumerator2.Current;
				if ((Object)(object)current2 == (Object)(object)Variables.Variables_Reference_09.offlineVRRig)
				{
					break;
				}
				((Behaviour)current2.skeleton).enabled = false;
				((Renderer)current2.skeleton.renderer).enabled = false;
				if (!enumerator2.MoveNext())
				{
					return;
				}
			}
		}
	}

	public static void SetFilledBoxESPEnabled(bool enable)
	{
		if (!enable)
		{
			CapturedVariables580_Reference_01.Clear();
			return;
		}
		List<VRRig> list = VRRigCache.ActiveRigs.ToList();
		RemoveStaleEspObjects(CapturedVariables580_Reference_01, list);
		using List<VRRig>.Enumerator enumerator = list.GetEnumerator();
		while (enumerator.MoveNext())
		{
			while (true)
			{
				VRRig current = enumerator.Current;
				if (current.isOfflineVRRig)
				{
					break;
				}
				if (!CapturedVariables580_Reference_01.singleObjects.TryGetValue(current, out GameObject value) || (Object)(object)value == (Object)null)
				{
					value = GameObject.CreatePrimitive((PrimitiveType)3);
					((Object)value).name = "FilledBox";
					Object.Destroy((Object)(object)value.GetComponent<Collider>());
					Material material = new Material(Variables.Variables_Reference_02);
					value.GetComponent<Renderer>().material = material;
					CapturedVariables580_Reference_01.singleObjects[current] = value;
					Color32 val = GetEspColor(RigManager.IsTagged(current), Variables.Variables_State_07, current);
					value.GetComponent<Renderer>().material.color = (Color32)(new Color32(val.r, val.g, val.b, (byte)80));
					value.transform.position = ((Component)current).transform.position - new Vector3(0f, 0.075f, 0f);
					value.transform.rotation = ((Component)current).transform.rotation;
					value.transform.localScale = new Vector3(((Component)current).transform.localScale.x * 0.75f, ((Component)current).transform.localScale.y * 1.05f, ((Component)current).transform.localScale.z * 0.75f);
					if (!enumerator.MoveNext())
					{
						return;
					}
				}
				else
				{
					Color32 val = GetEspColor(RigManager.IsTagged(current), Variables.Variables_State_07, current);
					value.GetComponent<Renderer>().material.color = (Color32)(new Color32(val.r, val.g, val.b, (byte)80));
					value.transform.position = ((Component)current).transform.position - new Vector3(0f, 0.075f, 0f);
					value.transform.rotation = ((Component)current).transform.rotation;
					value.transform.localScale = new Vector3(((Component)current).transform.localScale.x * 0.75f, ((Component)current).transform.localScale.y * 1.05f, ((Component)current).transform.localScale.z * 0.75f);
					if (!enumerator.MoveNext())
					{
						return;
					}
				}
			}
		}
	}

	public static void UpsideDownCam()
	{
		InputHandler.UpdateToggleOnPress(InputHandler.IsLeftPrimaryPressed, ref InputHandler.InputHandler_State_02, ref CapturedVariables580_State_03);
		if (!InputHandler.InputHandler_State_02)
		{
			ResetCamera();
		}
		else if ((Object)(object)CapturedVariables580_Object_01 == (Object)null)
		{
			CapturedVariables580_Object_01 = new GameObject("NXO_Cam");
			CapturedVariables580_Object_01.transform.position = ((Component)Variables.Variables_Reference_09.headCollider).transform.position;
			Camera val = CapturedVariables580_Object_01.GetComponent<Camera>() ?? CapturedVariables580_Object_01.AddComponent<Camera>();
			val.nearClipPlane = 0.01f;
			val.cameraType = (CameraType)1;
			CapturedVariables580_Object_01.transform.position = ((Component)Variables.Variables_Reference_09.headCollider).transform.position;
			CapturedVariables580_Object_01.transform.rotation = ((Component)Variables.Variables_Reference_09.headCollider).transform.rotation * Quaternion.Euler(0f, 0f, 180f);
		}
		else
		{
			Camera val = CapturedVariables580_Object_01.GetComponent<Camera>() ?? CapturedVariables580_Object_01.AddComponent<Camera>();
			val.nearClipPlane = 0.01f;
			val.cameraType = (CameraType)1;
			CapturedVariables580_Object_01.transform.position = ((Component)Variables.Variables_Reference_09.headCollider).transform.position;
			CapturedVariables580_Object_01.transform.rotation = ((Component)Variables.Variables_Reference_09.headCollider).transform.rotation * Quaternion.Euler(0f, 0f, 180f);
		}
	}

	public static void SetMonkeSenseEnabled(bool enable)
	{
		bool flag = RigManager.IsTagged(VRRig.LocalRig);
		if (!enable)
		{
			using (List<GameObject>.Enumerator enumerator = CapturedVariables580_Items_03.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					while (true)
					{
						GameObject current = enumerator.Current;
						if (!((Object)(object)current != (Object)null))
						{
							break;
						}
						Object.Destroy((Object)(object)current);
						if (!enumerator.MoveNext())
						{
							goto EndBranch_0081;
						}
					}
					continue;
					EndBranch_0081:
					break;
				}
			}
			CapturedVariables580_Items_03.Clear();
			if ((Object)(object)CapturedVariables580_Audio_01 != (Object)null)
			{
				CapturedVariables580_Audio_01.Stop();
				Object.Destroy((Object)(object)((Component)CapturedVariables580_Audio_01).gameObject);
				CapturedVariables580_Audio_01 = null;
			}
			return;
		}
		List<VRRig> list = VRRigCache.ActiveRigs.ToList();
		List<(VRRig, float)> list2 = new List<(VRRig, float)>();
		float num = float.MaxValue;
		using (List<VRRig>.Enumerator enumerator2 = list.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				while (true)
				{
					VRRig current2 = enumerator2.Current;
					if (current2.isOfflineVRRig)
					{
						break;
					}
					bool flag2 = RigManager.IsTagged(current2);
					if (!flag)
					{
						if (!flag2)
						{
							break;
						}
					}
					else if (flag2)
					{
						break;
					}
					float num2 = Vector3.Distance(((Component)Variables.Variables_Reference_09.bodyCollider).transform.position, ((Component)current2).transform.position);
					if (num2 <= 15f)
					{
						list2.Add((current2, num2));
						if (num2 < num)
						{
							num = num2;
							if (!enumerator2.MoveNext())
							{
								goto EndBranch_028a;
							}
						}
						else if (!enumerator2.MoveNext())
						{
							goto EndBranch_028a;
						}
					}
					else if (!enumerator2.MoveNext())
					{
						goto EndBranch_028a;
					}
				}
				continue;
				EndBranch_028a:
				break;
			}
		}
		int num3 = 8;
		Transform transform = ((Component)Camera.main).transform;
		if (CapturedVariables580_Items_03.Count < num3)
		{
			do
			{
				LineRenderer val = new GameObject("MonkeSenseArrow").AddComponent<LineRenderer>();
				val.positionCount = 2;
				val.useWorldSpace = true;
				((Renderer)val).material = new Material(Variables.Variables_Reference_02);
				CapturedVariables580_Items_03.Add(((Component)val).gameObject);
			}
			while (CapturedVariables580_Items_03.Count < num3);
		}
		float[] array = new float[num3];
		using (List<(VRRig, float)>.Enumerator enumerator3 = list2.GetEnumerator())
		{
			if (enumerator3.MoveNext())
			{
				do
				{
					(VRRig, float) current3 = enumerator3.Current;
					VRRig item2 = current3.Item1;
					float item3 = current3.Item2;
					Vector3 val2 = ((Component)item2).transform.position - transform.position;
					val2.y = 0f;
					Vector3 forward = transform.forward;
					forward.y = 0f;
					float num4 = 0f - Vector3.SignedAngle(((Vector3)forward).normalized, ((Vector3)val2).normalized, Vector3.up);
					float num5 = 1f - Mathf.Clamp01((item3 - 3f) / 12f);
					float num6 = Mathf.Lerp(0.15f, 1f, num5);
					int num7 = 0;
					if (num7 >= num3)
					{
						continue;
					}
					while (true)
					{
						float num8 = Mathf.Abs(Mathf.DeltaAngle(360f / (float)num3 * (float)num7, num4));
						if (num8 < 360f / (float)num3)
						{
							float num9 = Mathf.Lerp(num6, 0f, num8 / (360f / (float)num3));
							if (num9 > array[num7])
							{
								array[num7] = num9;
								num7++;
								if (num7 >= num3)
								{
									break;
								}
							}
							else
							{
								num7++;
								if (num7 >= num3)
								{
									break;
								}
							}
						}
						else
						{
							num7++;
							if (num7 >= num3)
							{
								break;
							}
						}
					}
				}
				while (enumerator3.MoveNext());
			}
		}
		Color val3;
		int num10;
		if (!flag)
		{
			val3 = Color.red;
			num10 = 0;
		}
		else
		{
			val3 = Color.green;
			num10 = 0;
		}
		if (num10 < num3)
		{
			while (true)
			{
				float num11 = (360f / (float)num3 * (float)num10 + 90f) * (MathF.PI / 180f);
				Vector3 val4 = transform.right * Mathf.Cos(num11) + transform.up * Mathf.Sin(num11);
				Vector3 val5 = transform.right * Mathf.Cos(num11 + MathF.PI / 2f) + transform.up * Mathf.Sin(num11 + MathF.PI / 2f);
				LineRenderer component = CapturedVariables580_Items_03[num10].GetComponent<LineRenderer>();
				bool flag3 = array[num10] > 0f;
				int num12 = (component.positionCount = 20);
				float num14 = 6f;
				float num15;
				if (!flag3)
				{
					num15 = 0.008f;
					if (!flag3)
					{
						goto Branch_0682;
					}
				}
				else
				{
					num15 = Mathf.Lerp(0.01f, 0.025f, array[num10]);
					if (!flag3)
					{
						goto Branch_0682;
					}
				}
				float num16 = 4f;
				int num17 = 0;
				goto Branch_0751;
				Branch_0682:
				num16 = 1.5f;
				num17 = 0;
				Branch_0751:
				if (num17 < num12)
				{
					do
					{
						float num18 = (float)num17 / (float)(num12 - 1);
						Vector3 val6 = transform.position + transform.forward * 0.6f + val4 * Mathf.Lerp(0.25f, 0.55f, num18);
						float num19 = Mathf.Sin(num18 * num14 * MathF.PI + Time.time * num16) * num15 * Mathf.Sin(num18 * MathF.PI);
						component.SetPosition(num17, val6 + val5 * num19);
						num17++;
					}
					while (num17 < num12);
				}
				component.startWidth = (flag3 ? Mathf.Lerp(0.03f, 0.07f, array[num10]) : 0.025f);
				component.endWidth = 0f;
				if (flag3)
				{
					component.startColor = new Color(val3.r, val3.g, val3.b, array[num10]);
					component.endColor = new Color(val3.r, val3.g, val3.b, 0f);
					CapturedVariables580_Items_03[num10].SetActive(true);
					num10++;
					if (num10 >= num3)
					{
						break;
					}
				}
				else
				{
					component.startColor = new Color(1f, 1f, 1f, 0.4f);
					component.endColor = new Color(1f, 1f, 1f, 0f);
					CapturedVariables580_Items_03[num10].SetActive(true);
					num10++;
					if (num10 >= num3)
					{
						break;
					}
				}
			}
		}
		if (list2.Count == 0)
		{
			using (List<GameObject>.Enumerator enumerator4 = CapturedVariables580_Items_03.GetEnumerator())
			{
				while (enumerator4.MoveNext())
				{
					while (true)
					{
						GameObject current4 = enumerator4.Current;
						if (!((Object)(object)current4 != (Object)null))
						{
							break;
						}
						current4.SetActive(false);
						if (!enumerator4.MoveNext())
						{
							goto EndBranch_093a;
						}
					}
					continue;
					EndBranch_093a:
					break;
				}
			}
			if ((Object)(object)CapturedVariables580_Audio_01 != (Object)null)
			{
				CapturedVariables580_Audio_01.Stop();
			}
			return;
		}
		float num20;
		if (num > 3f)
		{
			if (num > 8f)
			{
				num20 = 1.2f;
				if (CapturedVariables580_State_06)
				{
					return;
				}
			}
			else
			{
				num20 = 0.4f;
				if (CapturedVariables580_State_06)
				{
					return;
				}
			}
		}
		else
		{
			num20 = 0.15f;
			if (CapturedVariables580_State_06)
			{
				return;
			}
		}
		if (!(Time.time >= CapturedVariables580_Value_02))
		{
			return;
		}
		CapturedVariables580_Value_02 = Time.time + num20;
		int num21;
		int num22;
		if ((Object)(object)CapturedVariables580_Audio_01 == (Object)null)
		{
			CapturedVariables580_Audio_01 = new GameObject("MonkeSenseAudio").AddComponent<AudioSource>();
			CapturedVariables580_Audio_01.spatialBlend = 0f;
			CapturedVariables580_Audio_01.volume = 0.6f;
			num21 = 44100;
			num22 = num21 / 10;
			if (num > 3f)
			{
				goto Branch_0b02;
			}
		}
		else
		{
			num21 = 44100;
			num22 = num21 / 10;
			if (num > 3f)
			{
				goto Branch_0b02;
			}
		}
		float num23 = 880f;
		AudioClip val7 = AudioClip.Create("SenseBeep", num22, 1, num21, false);
		float[] array2 = new float[num22];
		int num24 = 0;
		Branch_0bf8:
		if (num24 < num22)
		{
			do
			{
				float num25 = (float)num24 / (float)num21;
				float num26 = 1f - (float)num24 / (float)num22;
				array2[num24] = Mathf.Sin(MathF.PI * 2f * num23 * num25) * num26 * 0.5f;
				num24++;
			}
			while (num24 < num22);
		}
		val7.SetData(array2, 0);
		CapturedVariables580_Audio_01.PlayOneShot(val7);
		return;
		Branch_0b02:
		if (num > 8f)
		{
			num23 = 440f;
			val7 = AudioClip.Create("SenseBeep", num22, 1, num21, false);
			array2 = new float[num22];
			num24 = 0;
		}
		else
		{
			num23 = 660f;
			val7 = AudioClip.Create("SenseBeep", num22, 1, num21, false);
			array2 = new float[num22];
			num24 = 0;
		}
		goto Branch_0bf8;
	}

	public static void LimitFrameRate(int fps)
	{
		float num = 1f / (float)fps;
		float num2 = Time.realtimeSinceStartup - CapturedVariables580_Value_01;
		if (num2 < num)
		{
			int num3 = Mathf.FloorToInt((num - num2) * 1000f);
			if (num3 > 0)
			{
				Thread.Sleep(num3);
				CapturedVariables580_Value_01 = Time.realtimeSinceStartup;
			}
			else
			{
				CapturedVariables580_Value_01 = Time.realtimeSinceStartup;
			}
		}
		else
		{
			CapturedVariables580_Value_01 = Time.realtimeSinceStartup;
		}
	}

	public static void OrbitCam()
	{
		InputHandler.UpdateToggleOnPress(InputHandler.IsLeftPrimaryPressed, ref InputHandler.InputHandler_State_02, ref CapturedVariables580_State_03);
		if (!InputHandler.InputHandler_State_02)
		{
			ResetCamera();
		}
		else if ((Object)(object)CapturedVariables580_Object_01 == (Object)null)
		{
			CapturedVariables580_Object_01 = new GameObject("NXO_Cam");
			CapturedVariables580_Object_01.transform.position = ((Component)Variables.Variables_Reference_09.headCollider).transform.position;
			Camera val = CapturedVariables580_Object_01.GetComponent<Camera>() ?? CapturedVariables580_Object_01.AddComponent<Camera>();
			val.nearClipPlane = 0.01f;
			val.cameraType = (CameraType)1;
			float num = Time.time * 45f;
			Vector3 val2 = Quaternion.Euler(0f, num, 0f) * new Vector3(2f, 0.5f, 0f);
			CapturedVariables580_Object_01.transform.position = ((Component)Variables.Variables_Reference_09.bodyCollider).transform.position + val2;
			CapturedVariables580_Object_01.transform.LookAt(((Component)Variables.Variables_Reference_09.headCollider).transform.position);
		}
		else
		{
			Camera val = CapturedVariables580_Object_01.GetComponent<Camera>() ?? CapturedVariables580_Object_01.AddComponent<Camera>();
			val.nearClipPlane = 0.01f;
			val.cameraType = (CameraType)1;
			float num = Time.time * 45f;
			Vector3 val2 = Quaternion.Euler(0f, num, 0f) * new Vector3(2f, 0.5f, 0f);
			CapturedVariables580_Object_01.transform.position = ((Component)Variables.Variables_Reference_09.bodyCollider).transform.position + val2;
			CapturedVariables580_Object_01.transform.LookAt(((Component)Variables.Variables_Reference_09.headCollider).transform.position);
		}
	}

	public static void SetNXONametagsEnabled(bool enable)
	{
		CapturedVariables580 LocalScope3 = new CapturedVariables580();
		CapturedVariables580_State_05 = enable;
		if (!enable || CapturedVariables580_State_02)
		{
			using (Dictionary<VRRig, GameObject>.ValueCollection.Enumerator enumerator = CapturedVariables580_Lookup_05.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					while (true)
					{
						GameObject current = enumerator.Current;
						if (!((Object)(object)current != (Object)null))
						{
							break;
						}
						Object.Destroy((Object)(object)current);
						if (!enumerator.MoveNext())
						{
							goto EndBranch_00b1;
						}
					}
					continue;
					EndBranch_00b1:
					break;
				}
			}
			CapturedVariables580_Lookup_05.Clear();
			return;
		}
		LocalScope3.activeRigs = VRRigCache.ActiveRigs.ToList();
		using (List<VRRig>.Enumerator enumerator2 = CapturedVariables580_Lookup_05.Keys.Where(delegate(VRRig r)
		{
			if (!((Object)(object)r == (Object)null) && LocalScope3.activeRigs.Contains(r) && !r.isOfflineVRRig)
			{
				NetPlayer creator = r.Creator;
				return !NetworkingLibrary.IsNxoUser((creator != null) ? creator.UserId : null);
			}
			return true;
		}).ToList().GetEnumerator())
		{
			if (enumerator2.MoveNext())
			{
				while (true)
				{
					VRRig current2 = enumerator2.Current;
					if (CapturedVariables580_Lookup_05.TryGetValue(current2, out GameObject value))
					{
						Object.Destroy((Object)(object)value);
						CapturedVariables580_Lookup_05.Remove(current2);
						if (!enumerator2.MoveNext())
						{
							break;
						}
					}
					else
					{
						CapturedVariables580_Lookup_05.Remove(current2);
						if (!enumerator2.MoveNext())
						{
							break;
						}
					}
				}
			}
		}
		using List<VRRig>.Enumerator enumerator3 = LocalScope3.activeRigs.GetEnumerator();
		while (enumerator3.MoveNext())
		{
			while (true)
			{
				VRRig current3 = enumerator3.Current;
				if ((Object)(object)current3 == (Object)null || current3.isMyPlayer || current3.isOfflineVRRig || current3.Creator == null || !NetworkingLibrary.IsNxoUser(current3.Creator.UserId))
				{
					break;
				}
				if (!CapturedVariables580_Lookup_05.TryGetValue(current3, out GameObject value2) || (Object)(object)value2 == (Object)null)
				{
					value2 = new GameObject("NXONametag");
					GameObject val = new GameObject("Shadow");
					val.transform.SetParent(value2.transform);
					val.transform.localPosition = new Vector3(0.003f, -0.003f, 0.001f);
					val.transform.localScale = Vector3.one;
					TextMesh val2 = val.AddComponent<TextMesh>();
					val2.fontSize = 280;
					val2.fontStyle = (FontStyle)1;
					val2.font = Main.CurrentFont;
					val2.color = new Color(0f, 0f, 0f, 0.7f);
					val2.anchor = (TextAnchor)4;
					val2.alignment = (TextAlignment)1;
					((Component)val2).GetComponent<Renderer>().material = val2.font.material;
					val2.text = "<size=320>NXO</size>";
					TextMesh val3 = value2.AddComponent<TextMesh>();
					val3.fontSize = 280;
					val3.fontStyle = (FontStyle)1;
					val3.font = Main.CurrentFont;
					val3.anchor = (TextAnchor)4;
					val3.alignment = (TextAlignment)1;
					((Component)val3).GetComponent<Renderer>().material = val3.font.material;
					val3.text = "<size=320><color=#00FFFF>NXO User</color></size>";
					CapturedVariables580_Lookup_05[current3] = value2;
					float num = Vector3.Distance(((Component)Camera.main).transform.position, ((Component)current3).transform.position);
					float num2 = Mathf.Min(num, 20f);
					float num3 = 0.0035f + num2 * 0.00025f;
					value2.transform.localScale = Vector3.one * num3;
					value2.transform.position = current3.headMesh.transform.position + Vector3.up * 0.55f;
					value2.transform.LookAt(((Component)Camera.main).transform);
					value2.transform.Rotate(0f, 180f, 0f);
					if (!enumerator3.MoveNext())
					{
						return;
					}
				}
				else
				{
					float num = Vector3.Distance(((Component)Camera.main).transform.position, ((Component)current3).transform.position);
					float num2 = Mathf.Min(num, 20f);
					float num3 = 0.0035f + num2 * 0.00025f;
					value2.transform.localScale = Vector3.one * num3;
					value2.transform.position = current3.headMesh.transform.position + Vector3.up * 0.55f;
					value2.transform.LookAt(((Component)Camera.main).transform);
					value2.transform.Rotate(0f, 180f, 0f);
					if (!enumerator3.MoveNext())
					{
						return;
					}
				}
			}
		}
	}

	public static void SetPredictionESPEnabled(bool enable)
	{
		CapturedVariables270 LocalScope4 = new CapturedVariables270();
		if (!enable)
		{
			CapturedVariables580_Reference_07.Clear();
			CapturedVariables580_Lookup_03.Clear();
			CapturedVariables580_Lookup_01.Clear();
			return;
		}
		CapturedVariables580_Reference_07.material = EnsureMaterialShader(ref CapturedVariables580_Reference_07.material, Variables.Variables_Reference_02);
		LocalScope4.activeRigs = VRRigCache.ActiveRigs.ToList();
		RemoveStaleEspObjects(CapturedVariables580_Reference_07, LocalScope4.activeRigs);
		using (List<VRRig>.Enumerator enumerator = CapturedVariables580_Lookup_03.Keys.Where((VRRig rig) => (Object)(object)rig == (Object)null || !LocalScope4.activeRigs.Contains(rig)).ToList().GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				do
				{
					VRRig current = enumerator.Current;
					CapturedVariables580_Lookup_03.Remove(current);
					CapturedVariables580_Lookup_01.Remove(current);
				}
				while (enumerator.MoveNext());
			}
		}
		using List<VRRig>.Enumerator enumerator2 = LocalScope4.activeRigs.GetEnumerator();
		while (enumerator2.MoveNext())
		{
			while (true)
			{
				VRRig current2 = enumerator2.Current;
				if (current2.isMyPlayer || current2.isOfflineVRRig)
				{
					break;
				}
				Vector3 position = ((Component)current2).transform.position;
				Vector3 val = Vector3.zero;
				Vector3 val2;
				Vector3 value2;
				if (CapturedVariables580_Lookup_03.TryGetValue(current2, out var value))
				{
					val2 = (position - value) / Time.deltaTime;
					if (((Vector3)val2).magnitude > 30f)
					{
						val2 = ((Vector3)val2).normalized * 30f;
						if (CapturedVariables580_Lookup_01.TryGetValue(current2, out value2))
						{
							goto Branch_0264;
						}
					}
					else if (CapturedVariables580_Lookup_01.TryGetValue(current2, out value2))
					{
						goto Branch_0264;
					}
					val = val2;
					CapturedVariables580_Lookup_01[current2] = val;
					CapturedVariables580_Lookup_03[current2] = position;
					if (((Vector3)val).magnitude < 0.3f)
					{
						break;
					}
				}
				else
				{
					CapturedVariables580_Lookup_03[current2] = position;
					if (((Vector3)val).magnitude < 0.3f)
					{
						break;
					}
				}
				goto Branch_0338;
				Branch_0264:
				val = Vector3.Lerp(value2, val2, 0.3f);
				CapturedVariables580_Lookup_01[current2] = val;
				CapturedVariables580_Lookup_03[current2] = position;
				if (!(((Vector3)val).magnitude < 0.3f))
				{
					goto Branch_0338;
				}
				break;
				Branch_0338:
				LineRenderer component;
				float num;
				Vector3 val5;
				if (!CapturedVariables580_Reference_07.singleObjects.TryGetValue(current2, out GameObject value3) || (Object)(object)value3 == (Object)null)
				{
					LineRenderer val3 = new GameObject("PredictionLine").AddComponent<LineRenderer>();
					val3.positionCount = 2;
					val3.startWidth = 0.02f;
					val3.endWidth = 0.08f;
					val3.useWorldSpace = true;
					((Renderer)val3).material = CapturedVariables580_Reference_07.material;
					CapturedVariables580_Reference_07.singleObjects[current2] = value3;
					component = value3.GetComponent<LineRenderer>();
					Color32 val4 = GetEspColor(RigManager.IsTagged(current2), Variables.Variables_State_07, current2);
					component.startColor = new Color((float)(int)val4.r / 255f, (float)(int)val4.g / 255f, (float)(int)val4.b / 255f, 0.3f);
					component.endColor = new Color((float)(int)val4.r / 255f, (float)(int)val4.g / 255f, (float)(int)val4.b / 255f, 0.8f);
					num = 0.25f;
					val5 = position + val * num;
					if (val.y > 0f)
					{
						goto Branch_058a;
					}
				}
				else
				{
					component = value3.GetComponent<LineRenderer>();
					Color32 val4 = GetEspColor(RigManager.IsTagged(current2), Variables.Variables_State_07, current2);
					component.startColor = new Color((float)(int)val4.r / 255f, (float)(int)val4.g / 255f, (float)(int)val4.b / 255f, 0.3f);
					component.endColor = new Color((float)(int)val4.r / 255f, (float)(int)val4.g / 255f, (float)(int)val4.b / 255f, 0.8f);
					num = 0.25f;
					val5 = position + val * num;
					if (val.y > 0f)
					{
						goto Branch_058a;
					}
				}
				component.SetPosition(0, position);
				component.SetPosition(1, val5);
				if (!enumerator2.MoveNext())
				{
					return;
				}
				continue;
				Branch_058a:
				val5 += Physics.gravity * (num * num * 0.5f);
				component.SetPosition(0, position);
				component.SetPosition(1, val5);
				if (!enumerator2.MoveNext())
				{
					return;
				}
			}
		}
	}

	public static void SetXRayEnabled(bool enabled)
	{
		if (enabled)
		{
			if (CapturedVariables580_Items_01.Count != 0)
			{
				return;
			}
			Renderer[] array = Variables.FindObjectsCached<Renderer>(false);
			int num = 0;
			while (num < array.Length)
			{
				Renderer val = array[num];
				if (!((Object)(object)val == (Object)null) && !(val is SkinnedMeshRenderer) && val.enabled && ((Component)val).gameObject.activeSelf)
				{
					val.enabled = false;
					CapturedVariables580_Items_01.Add(val);
					num++;
				}
				else
				{
					num++;
				}
			}
			return;
		}
		using (List<Renderer>.Enumerator enumerator = CapturedVariables580_Items_01.GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				while (true)
				{
					Renderer current = enumerator.Current;
					if ((Object)(object)current != (Object)null)
					{
						current.enabled = true;
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
		CapturedVariables580_Items_01.Clear();
	}

	public Visuals()
	{
	}

	private static void RemoveStaleEspObjects(ESPData data, List<VRRig> activeRigs)
	{
		List<VRRig> list = new List<VRRig>();
		using (Dictionary<VRRig, GameObject>.KeyCollection.Enumerator enumerator = data.singleObjects.Keys.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				while (true)
				{
					VRRig current = enumerator.Current;
					if (!((Object)(object)current == (Object)null) && activeRigs.Contains(current) && !current.isOfflineVRRig)
					{
						break;
					}
					list.Add(current);
					if (!enumerator.MoveNext())
					{
						goto EndBranch_00af;
					}
				}
				continue;
				EndBranch_00af:
				break;
			}
		}
		using (Dictionary<VRRig, List<GameObject>>.KeyCollection.Enumerator enumerator2 = data.multiObjects.Keys.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				while (true)
				{
					VRRig current2 = enumerator2.Current;
					if (!((Object)(object)current2 == (Object)null) && activeRigs.Contains(current2) && !current2.isOfflineVRRig)
					{
						break;
					}
					list.Add(current2);
					if (!enumerator2.MoveNext())
					{
						goto EndBranch_0190;
					}
				}
				continue;
				EndBranch_0190:
				break;
			}
		}
		using List<VRRig>.Enumerator enumerator3 = list.GetEnumerator();
		if (!enumerator3.MoveNext())
		{
			return;
		}
		do
		{
			Branch_01df:
			VRRig current3 = enumerator3.Current;
			List<GameObject> value2;
			if (data.singleObjects.TryGetValue(current3, out GameObject value))
			{
				if ((Object)(object)value != (Object)null)
				{
					Object.Destroy((Object)(object)value);
					data.singleObjects.Remove(current3);
					if (data.multiObjects.TryGetValue(current3, out value2))
					{
						goto Branch_02b3;
					}
				}
				else
				{
					data.singleObjects.Remove(current3);
					if (data.multiObjects.TryGetValue(current3, out value2))
					{
						goto Branch_02b3;
					}
				}
			}
			else if (data.multiObjects.TryGetValue(current3, out value2))
			{
				goto Branch_02b3;
			}
			if (!enumerator3.MoveNext())
			{
				break;
			}
			goto Branch_01df;
			Branch_02b3:
			using (List<GameObject>.Enumerator enumerator4 = value2.GetEnumerator())
			{
				while (enumerator4.MoveNext())
				{
					while (true)
					{
						GameObject current4 = enumerator4.Current;
						if (!((Object)(object)current4 != (Object)null))
						{
							break;
						}
						Object.Destroy((Object)(object)current4);
						if (!enumerator4.MoveNext())
						{
							goto EndBranch_0312;
						}
					}
					continue;
					EndBranch_0312:
					break;
				}
			}
			data.multiObjects.Remove(current3);
		}
		while (enumerator3.MoveNext());
	}

	public static void SetToggleFogEnabled(bool enable)
	{
		if (enable)
		{
			ZoneShaderSettings.activeInstance.SetGroundFogValue(new Color(0.9569f, 0.6941f, 0.502f, 0.1216f), 40f, 10f, 40f);
		}
		else
		{
			ZoneShaderSettings.activeInstance.SetGroundFogValue(Color.clear, 0f, 0f, 0f);
		}
	}

	public static void SetNameTagsEnabled(bool enable)
	{
		CapturedVariables570 LocalScope3 = new CapturedVariables570();
		CapturedVariables580_State_02 = enable;
		if (!enable)
		{
			using (Dictionary<VRRig, GameObject>.ValueCollection.Enumerator enumerator = CapturedVariables580_Lookup_02.Values.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					while (true)
					{
						GameObject current = enumerator.Current;
						if (!((Object)(object)current != (Object)null))
						{
							break;
						}
						Object.Destroy((Object)(object)current);
						if (!enumerator.MoveNext())
						{
							goto EndBranch_0087;
						}
					}
					continue;
					EndBranch_0087:
					break;
				}
			}
			CapturedVariables580_Lookup_02.Clear();
			return;
		}
		LocalScope3.activeRigs = VRRigCache.ActiveRigs.ToList();
		using (List<VRRig>.Enumerator enumerator2 = CapturedVariables580_Lookup_02.Keys.Where((VRRig r) => (Object)(object)r == (Object)null || !LocalScope3.activeRigs.Contains(r) || r.isOfflineVRRig).ToList().GetEnumerator())
		{
			if (enumerator2.MoveNext())
			{
				while (true)
				{
					VRRig current2 = enumerator2.Current;
					if (CapturedVariables580_Lookup_02.TryGetValue(current2, out GameObject value))
					{
						Object.Destroy((Object)(object)value);
						CapturedVariables580_Lookup_02.Remove(current2);
						if (!enumerator2.MoveNext())
						{
							break;
						}
					}
					else
					{
						CapturedVariables580_Lookup_02.Remove(current2);
						if (!enumerator2.MoveNext())
						{
							break;
						}
					}
				}
			}
		}
		using List<VRRig>.Enumerator enumerator3 = LocalScope3.activeRigs.GetEnumerator();
		while (enumerator3.MoveNext())
		{
			while (true)
			{
				VRRig current3 = enumerator3.Current;
				Color val5 = current3.playerColor;
				if ((Object)(object)current3 == (Object)null || current3.isMyPlayer || current3.isOfflineVRRig || current3.Creator == null)
				{
					break;
				}
				TextMesh component;
				TextMesh val4;
				string text;
				string text2;
				string text3;
				int fps;
				if (!CapturedVariables580_Lookup_02.TryGetValue(current3, out GameObject value2) || (Object)(object)value2 == (Object)null)
				{
					value2 = new GameObject("Nametag");
					GameObject val = new GameObject("Shadow");
					val.transform.SetParent(value2.transform);
					val.transform.localPosition = new Vector3(0.003f, -0.003f, 0.001f);
					val.transform.localScale = Vector3.one;
					TextMesh val2 = val.AddComponent<TextMesh>();
					val2.fontSize = 280;
					val2.fontStyle = (FontStyle)1;
					val2.font = Main.CurrentFont;
					val2.color = new Color(0f, 0f, 0f, 0.7f);
					val2.anchor = (TextAnchor)4;
					val2.alignment = (TextAlignment)1;
					((Component)val2).GetComponent<Renderer>().material = val2.font.material;
					TextMesh val3 = value2.AddComponent<TextMesh>();
					val3.fontSize = 280;
					val3.fontStyle = (FontStyle)1;
					val3.font = Main.CurrentFont;
					val3.anchor = (TextAnchor)4;
					val3.alignment = (TextAlignment)1;
					((Component)val3).GetComponent<Renderer>().material = val3.font.material;
					CapturedVariables580_Lookup_02[current3] = value2;
					component = value2.GetComponent<TextMesh>();
					Transform child = value2.transform.GetChild(0);
					val4 = ((child != null) ? ((Component)child).GetComponent<TextMesh>() : null);
					text = ColorUtility.ToHtmlStringRGB(current3.playerColor);
					text2 = GetPlatformLabel(current3);
					text3 = $"({(int)(val5.r * 9f)}, {(int)(val5.g * 9f)}, {(int)(val5.b * 9f)})";
					fps = ReflectionCompat.GetField(current3, "fps", 0);
					if (fps < 90)
					{
						goto Branch_052f;
					}
				}
				else
				{
					component = value2.GetComponent<TextMesh>();
					Transform child2 = value2.transform.GetChild(0);
					val4 = ((child2 != null) ? ((Component)child2).GetComponent<TextMesh>() : null);
					text = ColorUtility.ToHtmlStringRGB(current3.playerColor);
					text2 = GetPlatformLabel(current3);
					text3 = $"({(int)(val5.r * 9f)}, {(int)(val5.g * 9f)}, {(int)(val5.b * 9f)})";
					fps = ReflectionCompat.GetField(current3, "fps", 0);
					if (fps < 90)
					{
						goto Branch_052f;
					}
				}
				string text4 = "00FF00";
				string text5 = $"<color=#{text4}>{fps}</color>";
				if (!CapturedVariables580_State_05)
				{
					goto Branch_0697;
				}
				goto Branch_0677;
				Branch_0697:
				string text6 = "";
				string text7 = text6 + "<color=#" + text + ">" + current3.Creator.NickName + "</color>";
				int kQSIF8WU = Settings.CapturedVariables3760_Index_18;
				Branch_076b:
				int num = kQSIF8WU;
				num = (((uint)num <= 2u) ? num : 3) + 513;
				int num2 = num;
				string text8 = ((num2 == 514) ? ("<size=320>" + text7 + "</size>\n<size=220>" + text2 + " | " + text5 + "</size>") : ("<size=320>" + text7 + "</size>\n<size=220>" + text2 + " | " + text3 + " | " + text5 + "</size>\n<size=200><color=#AAAAAA>" + current3.Creator.UserId + "</color></size>"));
				string text9 = (component.text = text8);
				if ((Object)(object)val4 != (Object)null)
				{
					val4.text = text9.Replace("<color=#" + text + ">", "<color=#000000>").Replace("<color=#AAAAAA>", "<color=#000000>").Replace("<color=#00FFFF>", "<color=#000000>")
						.Replace("<color=#" + text4 + ">", "<color=#000000>");
					float num3 = Vector3.Distance(((Component)Camera.main).transform.position, ((Component)current3).transform.position);
					float num4 = Mathf.Min(num3, 20f);
					float num5 = 0.0035f + num4 * 0.00025f;
					value2.transform.localScale = Vector3.one * num5;
					value2.transform.position = current3.headMesh.transform.position + Vector3.up * 0.55f;
					value2.transform.LookAt(((Component)Camera.main).transform);
					value2.transform.Rotate(0f, 180f, 0f);
					if (!enumerator3.MoveNext())
					{
						return;
					}
				}
				else
				{
					float num3 = Vector3.Distance(((Component)Camera.main).transform.position, ((Component)current3).transform.position);
					float num4 = Mathf.Min(num3, 20f);
					float num5 = 0.0035f + num4 * 0.00025f;
					value2.transform.localScale = Vector3.one * num5;
					value2.transform.position = current3.headMesh.transform.position + Vector3.up * 0.55f;
					value2.transform.LookAt(((Component)Camera.main).transform);
					value2.transform.Rotate(0f, 180f, 0f);
					if (!enumerator3.MoveNext())
					{
						return;
					}
				}
				continue;
				Branch_052f:
				if (fps < 72)
				{
					if (fps < 60)
					{
						if (fps < 45)
						{
							text4 = "FF0000";
							text5 = $"<color=#{text4}>{fps}</color>";
							if (CapturedVariables580_State_05)
							{
								goto Branch_0677;
							}
						}
						else
						{
							text4 = "FFA500";
							text5 = $"<color=#{text4}>{fps}</color>";
							if (CapturedVariables580_State_05)
							{
								goto Branch_0677;
							}
						}
					}
					else
					{
						text4 = "FFFF00";
						text5 = $"<color=#{text4}>{fps}</color>";
						if (CapturedVariables580_State_05)
						{
							goto Branch_0677;
						}
					}
				}
				else
				{
					text4 = "7FFF00";
					text5 = $"<color=#{text4}>{fps}</color>";
					if (CapturedVariables580_State_05)
					{
						goto Branch_0677;
					}
				}
				goto Branch_0697;
				Branch_0677:
				if (!NetworkingLibrary.IsNxoUser(current3.Creator.UserId))
				{
					goto Branch_0697;
				}
				text6 = "<color=#" + text + ">(</color><color=#00FFFF>NXO</color><color=#" + text + ">)</color> ";
				text7 = text6 + "<color=#" + text + ">" + current3.Creator.NickName + "</color>";
				kQSIF8WU = Settings.CapturedVariables3760_Index_18;
				goto Branch_076b;
			}
		}
	}

	public static void SetFilledBoxESP2DEnabled(bool enable)
	{
		if (!enable)
		{
			CapturedVariables580_Reference_09.Clear();
			return;
		}
		CapturedVariables580_Reference_09.material = EnsureMaterialShader(ref CapturedVariables580_Reference_09.material, Variables.Variables_Reference_02);
		List<VRRig> list = VRRigCache.ActiveRigs.ToList();
		RemoveStaleEspObjects(CapturedVariables580_Reference_09, list);
		using List<VRRig>.Enumerator enumerator = list.GetEnumerator();
		while (enumerator.MoveNext())
		{
			while (true)
			{
				VRRig current = enumerator.Current;
				if (current.isOfflineVRRig)
				{
					break;
				}
				if (!CapturedVariables580_Reference_09.singleObjects.TryGetValue(current, out GameObject value) || (Object)(object)value == (Object)null)
				{
					value = GameObject.CreatePrimitive((PrimitiveType)5);
					((Object)value).name = "FilledBox2D";
					Object.Destroy((Object)(object)value.GetComponent<Collider>());
					Material material = new Material(Variables.Variables_Reference_02);
					value.GetComponent<Renderer>().material = material;
					CapturedVariables580_Reference_09.singleObjects[current] = value;
					Color32 val = GetEspColor(RigManager.IsTagged(current), Variables.Variables_State_07, current);
					value.GetComponent<Renderer>().material.color = (Color32)(new Color32(val.r, val.g, val.b, (byte)80));
					Vector3 position = ((Component)current).transform.position;
					value.transform.position = position;
					value.transform.rotation = Quaternion.LookRotation(position - ((Component)Camera.main).transform.position);
					value.transform.localScale = new Vector3(1.06f, 1.06f, 1f);
					if (!enumerator.MoveNext())
					{
						return;
					}
				}
				else
				{
					Color32 val = GetEspColor(RigManager.IsTagged(current), Variables.Variables_State_07, current);
					value.GetComponent<Renderer>().material.color = (Color32)(new Color32(val.r, val.g, val.b, (byte)80));
					Vector3 position = ((Component)current).transform.position;
					value.transform.position = position;
					value.transform.rotation = Quaternion.LookRotation(position - ((Component)Camera.main).transform.position);
					value.transform.localScale = new Vector3(1.06f, 1.06f, 1f);
					if (!enumerator.MoveNext())
					{
						return;
					}
				}
			}
		}
	}

	public static void SetUnfilledBoxESPEnabled(bool enable)
	{
		if (!enable)
		{
			CapturedVariables580_Reference_05.Clear();
			return;
		}
		CapturedVariables580_Reference_05.material = EnsureMaterialShader(ref CapturedVariables580_Reference_05.material, Variables.Variables_Reference_02);
		List<VRRig> list = VRRigCache.ActiveRigs.ToList();
		RemoveStaleEspObjects(CapturedVariables580_Reference_05, list);
		using List<VRRig>.Enumerator enumerator = list.GetEnumerator();
		Vector3 halfSize = default(Vector3);
		while (enumerator.MoveNext())
		{
			while (true)
			{
				VRRig current = enumerator.Current;
				if (current.isOfflineVRRig)
				{
					break;
				}
				Vector3[] array;
				Color32 val2;
				int num2;
				if (!CapturedVariables580_Reference_05.multiObjects.TryGetValue(current, out List<GameObject> value))
				{
					value = new List<GameObject>();
					int[][] tXDC49T = CapturedVariables580_Index_02;
					for (int i = 0; i < tXDC49T.Length; i++)
					{
						_ = tXDC49T[i];
						LineRenderer val = new GameObject("WireframeLine").AddComponent<LineRenderer>();
						val.positionCount = 2;
						float startWidth = (val.endWidth = 0.06f);
						val.startWidth = startWidth;
						val.useWorldSpace = true;
						((Renderer)val).material = CapturedVariables580_Reference_05.material;
						value.Add(((Component)val).gameObject);
					}
					CapturedVariables580_Reference_05.multiObjects[current] = value;
					halfSize = new Vector3(((Component)current).transform.localScale.x * 0.375f, ((Component)current).transform.localScale.y * 0.525f, ((Component)current).transform.localScale.z * 0.375f);
					Vector3 center = ((Component)current).transform.position - new Vector3(0f, 0.075f, 0f);
					array = GetBoxCorners(center, halfSize, ((Component)current).transform.rotation);
					val2 = GetEspColor(RigManager.IsTagged(current), Variables.Variables_State_07, current);
					num2 = 0;
				}
				else
				{
					halfSize = new Vector3(((Component)current).transform.localScale.x * 0.375f, ((Component)current).transform.localScale.y * 0.525f, ((Component)current).transform.localScale.z * 0.375f);
					Vector3 center = ((Component)current).transform.position - new Vector3(0f, 0.075f, 0f);
					array = GetBoxCorners(center, halfSize, ((Component)current).transform.rotation);
					val2 = GetEspColor(RigManager.IsTagged(current), Variables.Variables_State_07, current);
					num2 = 0;
				}
				if (num2 < value.Count)
				{
					while (true)
					{
						LineRenderer component = value[num2].GetComponent<LineRenderer>();
						if (!((Object)(object)component == (Object)null))
						{
							Color startColor = (component.endColor = (Color32)(val2));
							component.startColor = startColor;
							component.SetPositions((Vector3[])(object)new Vector3[2]
							{
								array[CapturedVariables580_Index_02[num2][0]],
								array[CapturedVariables580_Index_02[num2][1]]
							});
							num2++;
							if (num2 >= value.Count)
							{
								break;
							}
						}
						else
						{
							num2++;
							if (num2 >= value.Count)
							{
								break;
							}
						}
					}
				}
				if (!enumerator.MoveNext())
				{
					return;
				}
			}
		}
	}

	public static void VR3rdPersonInFront()
	{
		InputHandler.UpdateToggleOnPress(InputHandler.IsLeftPrimaryPressed, ref InputHandler.InputHandler_State_02, ref CapturedVariables580_State_03);
		if (!InputHandler.InputHandler_State_02)
		{
			ResetCamera();
		}
		else if ((Object)(object)CapturedVariables580_Object_01 == (Object)null)
		{
			CapturedVariables580_Object_01 = new GameObject("NXO_Cam");
			CapturedVariables580_Object_01.transform.position = ((Component)Variables.Variables_Reference_09.headCollider).transform.position;
			Camera val = CapturedVariables580_Object_01.GetComponent<Camera>() ?? CapturedVariables580_Object_01.AddComponent<Camera>();
			val.nearClipPlane = 0.01f;
			val.cameraType = (CameraType)1;
			CapturedVariables580_Object_01.transform.position = ((Component)Variables.Variables_Reference_09.headCollider).transform.position + ((Component)Variables.Variables_Reference_09.headCollider).transform.forward * 0.8f;
			CapturedVariables580_Object_01.transform.LookAt(((Component)Variables.Variables_Reference_09.headCollider).transform.position);
		}
		else
		{
			Camera val = CapturedVariables580_Object_01.GetComponent<Camera>() ?? CapturedVariables580_Object_01.AddComponent<Camera>();
			val.nearClipPlane = 0.01f;
			val.cameraType = (CameraType)1;
			CapturedVariables580_Object_01.transform.position = ((Component)Variables.Variables_Reference_09.headCollider).transform.position + ((Component)Variables.Variables_Reference_09.headCollider).transform.forward * 0.8f;
			CapturedVariables580_Object_01.transform.LookAt(((Component)Variables.Variables_Reference_09.headCollider).transform.position);
		}
	}

	public static void SetTracersESPEnabled(bool enable)
	{
		if (!enable)
		{
			CapturedVariables580_Reference_02.Clear();
			return;
		}
		CapturedVariables580_Reference_02.material = EnsureMaterialShader(ref CapturedVariables580_Reference_02.material, Variables.Variables_Reference_02);
		List<VRRig> list = VRRigCache.ActiveRigs.ToList();
		RemoveStaleEspObjects(CapturedVariables580_Reference_02, list);
		using List<VRRig>.Enumerator enumerator = list.GetEnumerator();
		while (enumerator.MoveNext())
		{
			while (true)
			{
				VRRig current = enumerator.Current;
				if (current.isMyPlayer || current.isOfflineVRRig)
				{
					break;
				}
				LineRenderer component;
				int num2;
				if (!CapturedVariables580_Reference_02.singleObjects.TryGetValue(current, out GameObject value) || (Object)(object)value == (Object)null)
				{
					LineRenderer val = new GameObject("Tracer").AddComponent<LineRenderer>();
					val.positionCount = 2;
					float startWidth = (val.endWidth = 0.015f);
					val.startWidth = startWidth;
					val.useWorldSpace = true;
					((Renderer)val).material = CapturedVariables580_Reference_02.material;
					CapturedVariables580_Reference_02.singleObjects[current] = value;
					component = value.GetComponent<LineRenderer>();
					Color32 val2 = GetEspColor(RigManager.IsTagged(current), Variables.Variables_State_07, current);
					LineRenderer obj = component;
					Color startColor = (component.endColor = (Color32)(val2));
					obj.startColor = startColor;
					num2 = Settings.CapturedVariables3760_Index_50;
				}
				else
				{
					component = value.GetComponent<LineRenderer>();
					Color32 val2 = GetEspColor(RigManager.IsTagged(current), Variables.Variables_State_07, current);
					LineRenderer obj2 = component;
					Color startColor = (component.endColor = (Color32)(val2));
					obj2.startColor = startColor;
					num2 = Settings.CapturedVariables3760_Index_50;
				}
				int num3 = num2 - 1;
				num3 = (((uint)num3 <= 2u) ? num3 : 3) + 110;
				int num4 = num3;
				Vector3 val5 = ((num4 == 111) ? (((Component)Variables.Variables_Reference_09.headCollider).transform.position + new Vector3(0f, 0.4f, 0f)) : Variables.Variables_Reference_09.leftHandTransform.position);
				component.SetPosition(0, val5);
				component.SetPosition(1, ((Component)current).transform.position);
				if (!enumerator.MoveNext())
				{
					return;
				}
			}
		}
	}

	public static void SetFPSBoostEnabled(bool enable)
	{
		if (enable != CapturedVariables580_State_07)
		{
			CapturedVariables580_State_07 = enable;
			if (enable)
			{
				Screen.SetResolution(NXOUI.originalWidth / 6, NXOUI.originalHeight / 6, true);
				QualitySettings.SetQualityLevel(0, true);
				QualitySettings.pixelLightCount = 0;
				QualitySettings.shadows = (ShadowQuality)0;
				QualitySettings.realtimeReflectionProbes = false;
				QualitySettings.softParticles = false;
				QualitySettings.lodBias = 0.5f;
				QualitySettings.antiAliasing = 0;
				QualitySettings.vSyncCount = 0;
				QualitySettings.anisotropicFiltering = (AnisotropicFiltering)0;
				QualitySettings.globalTextureMipmapLimit = 2;
				QualitySettings.skinWeights = (SkinWeights)1;
			}
			else
			{
				Screen.SetResolution(NXOUI.originalWidth, NXOUI.originalHeight, true);
				QualitySettings.SetQualityLevel(QualitySettings.names.Length - 1, true);
				QualitySettings.pixelLightCount = 4;
				QualitySettings.shadows = (ShadowQuality)2;
				QualitySettings.realtimeReflectionProbes = true;
				QualitySettings.softParticles = true;
				QualitySettings.lodBias = 1.5f;
				QualitySettings.antiAliasing = 2;
				QualitySettings.vSyncCount = 1;
				QualitySettings.anisotropicFiltering = (AnisotropicFiltering)2;
				QualitySettings.globalTextureMipmapLimit = 0;
				QualitySettings.skinWeights = (SkinWeights)4;
			}
		}
	}

	public static void SetShinyMonkesEnabled(bool enable)
	{
		using IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			while (true)
			{
				VRRig current = enumerator.Current;
				if (current.isOfflineVRRig)
				{
					break;
				}
				SkinnedMeshRenderer mainSkin = current.mainSkin;
				if ((Object)(object)((mainSkin != null) ? ((Renderer)mainSkin).material : null) == (Object)null)
				{
					break;
				}
				Material material = ((Renderer)current.mainSkin).material;
				if (enable)
				{
					material.shader = Variables.Variables_Reference_05;
					material.SetFloat("_Smoothness", 0.95f);
					material.SetFloat("_Metallic", 0.85f);
					if (!enumerator.MoveNext())
					{
						return;
					}
				}
				else
				{
					material.shader = Variables.Variables_Reference_10;
					if (!enumerator.MoveNext())
					{
						return;
					}
				}
			}
		}
	}

	public static void SetTrailsESPEnabled(bool enable)
	{
		if (!enable)
		{
			CapturedVariables580_Reference_06.Clear();
			return;
		}
		CapturedVariables580_Reference_06.material = EnsureMaterialShader(ref CapturedVariables580_Reference_06.material, Variables.Variables_Reference_02);
		List<VRRig> list = VRRigCache.ActiveRigs.ToList();
		RemoveStaleEspObjects(CapturedVariables580_Reference_06, list);
		using List<VRRig>.Enumerator enumerator = list.GetEnumerator();
		while (enumerator.MoveNext())
		{
			while (true)
			{
				VRRig current = enumerator.Current;
				if ((Object)(object)current == (Object)(object)Variables.Variables_Reference_09.offlineVRRig)
				{
					break;
				}
				TrailRenderer component;
				if (!CapturedVariables580_Reference_06.singleObjects.TryGetValue(current, out GameObject value) || (Object)(object)value == (Object)null)
				{
					new GameObject("Trail").transform.position = ((Component)current).transform.position;
					value.transform.SetParent(((Component)current).transform);
					TrailRenderer val = value.AddComponent<TrailRenderer>();
					val.time = 2f;
					val.startWidth = 0.2f;
					val.endWidth = 0f;
					((Renderer)val).material = CapturedVariables580_Reference_06.material;
					CapturedVariables580_Reference_06.singleObjects[current] = value;
					value.transform.position = ((Component)current).transform.position;
					component = value.GetComponent<TrailRenderer>();
					if ((Object)(object)component != (Object)null)
					{
						goto Branch_01f8;
					}
				}
				else
				{
					value.transform.position = ((Component)current).transform.position;
					component = value.GetComponent<TrailRenderer>();
					if ((Object)(object)component != (Object)null)
					{
						goto Branch_01f8;
					}
				}
				if (!enumerator.MoveNext())
				{
					return;
				}
				continue;
				Branch_01f8:
				Color32 val2 = GetEspColor(RigManager.IsTagged(current), Variables.Variables_State_07, current);
				TrailRenderer obj = component;
				Color startColor = (component.endColor = (Color32)(val2));
				obj.startColor = startColor;
				if (!enumerator.MoveNext())
				{
					return;
				}
			}
		}
	}

	private static Material EnsureMaterialShader(ref Material material, Shader shader)
	{
		if ((Object)(object)material == (Object)null || (Object)(object)material.shader != (Object)(object)shader)
		{
			if ((Object)(object)material != (Object)null)
			{
				Object.Destroy((Object)(object)material);
				material = new Material(shader);
				return material;
			}
			material = new Material(shader);
			return material;
		}
		return material;
	}

	public static void SetBoneESPEnabled(bool enable)
	{
		if (!enable)
		{
			CapturedVariables580_Reference_03.Clear();
			return;
		}
		CapturedVariables580_Reference_03.material = EnsureMaterialShader(ref CapturedVariables580_Reference_03.material, Variables.Variables_Reference_02);
		List<VRRig> list = VRRigCache.ActiveRigs.ToList();
		RemoveStaleEspObjects(CapturedVariables580_Reference_03, list);
		using List<VRRig>.Enumerator enumerator = list.GetEnumerator();
		while (enumerator.MoveNext())
		{
			while (true)
			{
				VRRig current = enumerator.Current;
				if (current.isOfflineVRRig)
				{
					break;
				}
				Color32 val2;
				int num3;
				if (!CapturedVariables580_Reference_03.multiObjects.TryGetValue(current, out List<GameObject> value))
				{
					value = new List<GameObject>();
					int num = 0;
					if (num <= CapturedVariables580_Index_01.Length / 2)
					{
						do
						{
							LineRenderer val = new GameObject("BoneLine").AddComponent<LineRenderer>();
							val.positionCount = 2;
							float startWidth = (val.endWidth = 0.04f);
							val.startWidth = startWidth;
							((Renderer)val).material = CapturedVariables580_Reference_03.material;
							value.Add(((Component)val).gameObject);
							num++;
						}
						while (num <= CapturedVariables580_Index_01.Length / 2);
					}
					CapturedVariables580_Reference_03.multiObjects[current] = value;
					val2 = GetEspColor(RigManager.IsTagged(current), Variables.Variables_State_07, current);
					LineRenderer component = value[0].GetComponent<LineRenderer>();
					LineRenderer obj = component;
					Color startColor = (component.endColor = (Color32)(val2));
					obj.startColor = startColor;
					component.SetPositions((Vector3[])(object)new Vector3[2]
					{
						((Component)current.head.rigTarget).transform.position + new Vector3(0f, 0.16f, 0f),
						((Component)current.head.rigTarget).transform.position - new Vector3(0f, 0.4f, 0f)
					});
					num3 = 0;
				}
				else
				{
					val2 = GetEspColor(RigManager.IsTagged(current), Variables.Variables_State_07, current);
					LineRenderer component = value[0].GetComponent<LineRenderer>();
					LineRenderer obj2 = component;
					Color startColor = (component.endColor = (Color32)(val2));
					obj2.startColor = startColor;
					component.SetPositions((Vector3[])(object)new Vector3[2]
					{
						((Component)current.head.rigTarget).transform.position + new Vector3(0f, 0.16f, 0f),
						((Component)current.head.rigTarget).transform.position - new Vector3(0f, 0.4f, 0f)
					});
					num3 = 0;
				}
				if (num3 < CapturedVariables580_Index_01.Length)
				{
					do
					{
						LineRenderer component2 = value[1 + num3 / 2].GetComponent<LineRenderer>();
						Color startColor = (component2.endColor = (Color32)(val2));
						component2.startColor = startColor;
						component2.SetPositions((Vector3[])(object)new Vector3[2]
						{
							current.mainSkin.bones[CapturedVariables580_Index_01[num3]].position,
							current.mainSkin.bones[CapturedVariables580_Index_01[num3 + 1]].position
						});
						num3 += 2;
					}
					while (num3 < CapturedVariables580_Index_01.Length);
				}
				if (!enumerator.MoveNext())
				{
					return;
				}
			}
		}
	}

	public static void SetAcidTripEnabled(bool enable)
	{
		if (!enable)
		{
			if (!CapturedVariables580_State_01)
			{
				return;
			}
			using (Dictionary<Renderer, Material>.Enumerator enumerator = CapturedVariables580_Lookup_04.GetEnumerator())
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
							goto EndBranch_00af;
						}
					}
					continue;
					EndBranch_00af:
					break;
				}
			}
			CapturedVariables580_Lookup_04.Clear();
			CapturedVariables580_State_01 = false;
		}
		else
		{
			if (CapturedVariables580_State_01)
			{
				return;
			}
			Material val = AssetHandler.LoadMaterial("NXO.Resources.acidtrip", "AcidTrip");
			if ((Object)(object)val == (Object)null)
			{
				return;
			}
			Renderer[] array = Object.FindObjectsOfType<Renderer>();
			int num = 0;
			while (num < array.Length)
			{
				Renderer val2 = array[num];
				if (!((Object)(object)val2 == (Object)null) && val2.enabled && ((Component)val2).gameObject.activeSelf)
				{
					CapturedVariables580_Lookup_04[val2] = val2.sharedMaterial;
					val2.sharedMaterial = val;
					num++;
				}
				else
				{
					num++;
				}
			}
			CapturedVariables580_State_01 = true;
		}
	}

	public static void SetBeaconsESPEnabled(bool enable)
	{
		if (!enable)
		{
			CapturedVariables580_Reference_08.Clear();
			return;
		}
		CapturedVariables580_Reference_08.material = EnsureMaterialShader(ref CapturedVariables580_Reference_08.material, Variables.Variables_Reference_02);
		List<VRRig> list = VRRigCache.ActiveRigs.ToList();
		RemoveStaleEspObjects(CapturedVariables580_Reference_08, list);
		using List<VRRig>.Enumerator enumerator = list.GetEnumerator();
		while (enumerator.MoveNext())
		{
			while (true)
			{
				VRRig current = enumerator.Current;
				if (current.isOfflineVRRig || current.isMyPlayer)
				{
					break;
				}
				LineRenderer component;
				if (!CapturedVariables580_Reference_08.singleObjects.TryGetValue(current, out GameObject value) || (Object)(object)value == (Object)null)
				{
					LineRenderer val = new GameObject("Beacon").AddComponent<LineRenderer>();
					val.positionCount = 2;
					float startWidth = (val.endWidth = 0.15f);
					val.startWidth = startWidth;
					val.useWorldSpace = true;
					((Renderer)val).material = CapturedVariables580_Reference_08.material;
					CapturedVariables580_Reference_08.singleObjects[current] = value;
					component = value.GetComponent<LineRenderer>();
					if ((Object)(object)component == (Object)null)
					{
						break;
					}
				}
				else
				{
					component = value.GetComponent<LineRenderer>();
					if ((Object)(object)component == (Object)null)
					{
						break;
					}
				}
				Vector3 position = ((Component)current).transform.position;
				Color32 val2 = GetEspColor(RigManager.IsTagged(current), Variables.Variables_State_07, current);
				LineRenderer obj = component;
				Color startColor = (component.endColor = (Color32)(val2));
				obj.startColor = startColor;
				component.SetPositions((Vector3[])(object)new Vector3[2]
				{
					position + Vector3.down * 50f,
					position + Vector3.up * 50f
				});
				if (!enumerator.MoveNext())
				{
					return;
				}
			}
		}
	}

	public static void SetShinySelfEnabled(bool enable)
	{
		Material material = ((Renderer)Variables.Variables_Reference_09.offlineVRRig.mainSkin).material;
		if (enable)
		{
			material.shader = Variables.Variables_Reference_05;
			material.SetFloat("_Smoothness", 0.95f);
			material.SetFloat("_Metallic", 0.85f);
		}
		else
		{
			material.shader = Variables.Variables_Reference_10;
		}
	}

	public static void SetUncapFPSEnabled(bool enabled)
	{
		if (enabled)
		{
			QualitySettings.vSyncCount = 0;
			Application.targetFrameRate = int.MaxValue;
		}
		else
		{
			Application.targetFrameRate = 144;
		}
	}

	public static void FreeCam()
	{
		InputHandler.UpdateToggleOnPress(InputHandler.IsLeftPrimaryPressed, ref InputHandler.InputHandler_State_02, ref CapturedVariables580_State_03);
		Vector3 val4 = default(Vector3);
		if (!InputHandler.InputHandler_State_02)
		{
			ResetCamera();
		}
		else if ((Object)(object)CapturedVariables580_Object_01 == (Object)null)
		{
			CapturedVariables580_Object_01 = new GameObject("NXO_Cam");
			CapturedVariables580_Object_01.transform.position = ((Component)Variables.Variables_Reference_09.headCollider).transform.position;
			Camera val = CapturedVariables580_Object_01.GetComponent<Camera>() ?? CapturedVariables580_Object_01.AddComponent<Camera>();
			val.nearClipPlane = 0.01f;
			val.cameraType = (CameraType)1;
			Vector2 val2 = InputHandler.GetJoystickAxis(left: true);
			Vector2 val3 = InputHandler.GetJoystickAxis(left: false);
			val4 = new Vector3(val2.x, val3.y, val2.y);
			Vector3 val5 = GTVector3Extensions.X_Z(((Component)GTPlayer.Instance.bodyCollider).transform.forward);
			Vector3 val6 = GTVector3Extensions.X_Z(((Component)GTPlayer.Instance.bodyCollider).transform.right);
			Vector3 val7 = val4.x * val6 + val4.y * Vector3.up + val4.z * val5;
			Vector3 val8 = val7;
			val7 = val8 * Settings.CapturedVariables3760_Value_14;
			val8 = val7;
			CapturedVariables580_Position_01 = Vector3.Lerp(CapturedVariables580_Position_01, val8, 0.12875f);
			Transform transform = CapturedVariables580_Object_01.transform;
			transform.position += CapturedVariables580_Position_01 * Time.unscaledDeltaTime;
			CapturedVariables580_Object_01.transform.rotation = ((Component)Variables.Variables_Reference_09.headCollider).transform.rotation;
		}
		else
		{
			Camera val = CapturedVariables580_Object_01.GetComponent<Camera>() ?? CapturedVariables580_Object_01.AddComponent<Camera>();
			val.nearClipPlane = 0.01f;
			val.cameraType = (CameraType)1;
			Vector2 val2 = InputHandler.GetJoystickAxis(left: true);
			Vector2 val3 = InputHandler.GetJoystickAxis(left: false);
			val4 = new Vector3(val2.x, val3.y, val2.y);
			Vector3 val5 = GTVector3Extensions.X_Z(((Component)GTPlayer.Instance.bodyCollider).transform.forward);
			Vector3 val6 = GTVector3Extensions.X_Z(((Component)GTPlayer.Instance.bodyCollider).transform.right);
			Vector3 val9 = val4.x * val6 + val4.y * Vector3.up + val4.z * val5;
			Vector3 val8 = val9;
			val9 = val8 * Settings.CapturedVariables3760_Value_14;
			val8 = val9;
			CapturedVariables580_Position_01 = Vector3.Lerp(CapturedVariables580_Position_01, val8, 0.12875f);
			Transform transform2 = CapturedVariables580_Object_01.transform;
			transform2.position += CapturedVariables580_Position_01 * Time.unscaledDeltaTime;
			CapturedVariables580_Object_01.transform.rotation = ((Component)Variables.Variables_Reference_09.headCollider).transform.rotation;
		}
	}

	public static void SetChamsESPEnabled(bool enable)
	{
		using IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			while (true)
			{
				VRRig current = enumerator.Current;
				if (current.isOfflineVRRig)
				{
					break;
				}
				SkinnedMeshRenderer mainSkin = current.mainSkin;
				if ((Object)(object)((mainSkin != null) ? ((Renderer)mainSkin).material : null) == (Object)null)
				{
					break;
				}
				Material material = ((Renderer)current.mainSkin).material;
				if (enable)
				{
					EnsureWhiteVertexColors(current);
					material.shader = Variables.Variables_Reference_02;
					material.color = (Color32)(GetEspColor(RigManager.IsTagged(current), Variables.Variables_State_07, current));
				}
				else if ((Object)(object)material.shader == (Object)(object)Variables.Variables_Reference_02)
				{
					material.shader = Variables.Variables_Reference_10;
					if (!enumerator.MoveNext())
					{
						return;
					}
					continue;
				}
				if (!enumerator.MoveNext())
				{
					return;
				}
			}
		}
	}

	public static void SetUnfilledBoxESP2DEnabled(bool enable)
	{
		if (!enable)
		{
			CapturedVariables580_Reference_04.Clear();
			return;
		}
		CapturedVariables580_Reference_04.material = EnsureMaterialShader(ref CapturedVariables580_Reference_04.material, Variables.Variables_Reference_02);
		List<VRRig> list = VRRigCache.ActiveRigs.ToList();
		RemoveStaleEspObjects(CapturedVariables580_Reference_04, list);
		using List<VRRig>.Enumerator enumerator = list.GetEnumerator();
		while (enumerator.MoveNext())
		{
			while (true)
			{
				VRRig current = enumerator.Current;
				Vector3 val4 = ((Component)current).transform.position;
				if (current.isOfflineVRRig)
				{
					break;
				}
				Color32 val2;
				float num3;
				Vector3[] array;
				int num4;
				if (!CapturedVariables580_Reference_04.multiObjects.TryGetValue(current, out List<GameObject> value))
				{
					value = new List<GameObject>();
					int num = 0;
					if (num < 4)
					{
						do
						{
							LineRenderer val = new GameObject("2DBoxLine").AddComponent<LineRenderer>();
							val.positionCount = 2;
							float startWidth = (val.endWidth = 0.07f);
							val.startWidth = startWidth;
							val.useWorldSpace = true;
							((Renderer)val).material = CapturedVariables580_Reference_04.material;
							value.Add(((Component)val).gameObject);
							num++;
						}
						while (num < 4);
					}
					CapturedVariables580_Reference_04.multiObjects[current] = value;
					val2 = GetEspColor(RigManager.IsTagged(current), Variables.Variables_State_07, current);
					Quaternion val3 = Quaternion.LookRotation(((Component)current).transform.position - ((Component)Camera.main).transform.position);
					num3 = 0.03f;
					array = (Vector3[])(object)new Vector3[4]
					{
						val4 + val3 * new Vector3(-0.5f - num3, 0.5f + num3, 0f),
						val4 + val3 * new Vector3(0.5f + num3, 0.5f + num3, 0f),
						val4 + val3 * new Vector3(0.5f + num3, -0.5f - num3, 0f),
						val4 + val3 * new Vector3(-0.5f - num3, -0.5f - num3, 0f)
					};
					num4 = 0;
				}
				else
				{
					val2 = GetEspColor(RigManager.IsTagged(current), Variables.Variables_State_07, current);
					Quaternion val3 = Quaternion.LookRotation(((Component)current).transform.position - ((Component)Camera.main).transform.position);
					num3 = 0.03f;
					array = (Vector3[])(object)new Vector3[4]
					{
						val4 + val3 * new Vector3(-0.5f - num3, 0.5f + num3, 0f),
						val4 + val3 * new Vector3(0.5f + num3, 0.5f + num3, 0f),
						val4 + val3 * new Vector3(0.5f + num3, -0.5f - num3, 0f),
						val4 + val3 * new Vector3(-0.5f - num3, -0.5f - num3, 0f)
					};
					num4 = 0;
				}
				if (num4 < value.Count)
				{
					while (true)
					{
						LineRenderer component = value[num4].GetComponent<LineRenderer>();
						if (!((Object)(object)component == (Object)null))
						{
							Color startColor = (component.endColor = (Color32)(val2));
							component.startColor = startColor;
							Vector3 val6 = array[(num4 + 1) % 4] - array[num4];
							Vector3 normalized = ((Vector3)val6).normalized;
							component.SetPositions((Vector3[])(object)new Vector3[2]
							{
								array[num4] - normalized * num3,
								array[(num4 + 1) % 4] + normalized * num3
							});
							num4++;
							if (num4 >= value.Count)
							{
								break;
							}
						}
						else
						{
							num4++;
							if (num4 >= value.Count)
							{
								break;
							}
						}
					}
				}
				if (!enumerator.MoveNext())
				{
					return;
				}
			}
		}
	}
}
