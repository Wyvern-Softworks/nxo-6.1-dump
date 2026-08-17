using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using GorillaExtensions;
using GorillaLocomotion;
using GorillaLocomotion.Swimming;
using GorillaNetworking;
using NXO.Menu;
using NXO.Utilities;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using CosmeticItem = GorillaNetworking.CosmeticsController.CosmeticItem;
using WeatherType = BetterDayNightManager.WeatherType;

namespace NXO.Mods.Categories;

public class Fun
{
	[CompilerGenerated]
	private sealed class CapturedVariables520
	{
		public Color color;

		internal bool HoverboardScreenAll_Lambda0()
		{
			if (!PhotonNetwork.InRoom)
			{
				return true;
			}
			PhotonSerializer.SerializeAllViews(exclude: true, (PhotonView[])(object)new PhotonView[1] { Variables.Variables_Reference_09.myVRRig.GetView });
			Vector3 position = ((Component)VRRig.LocalRig).transform.position;
			NetPlayer[] playerListOthers = NetworkSystem.Instance.PlayerListOthers;
			int num = 0;
			while (num < playerListOthers.Length)
			{
				NetPlayer val3 = playerListOthers[num];
				VRRig val = RigManager.FindRig(val3);
				if (!((Object)(object)val == (Object)null))
				{
					AttachHoverboardToRig(val, color);
					PhotonView getView = Variables.Variables_Reference_09.myVRRig.GetView;
					RaiseEventOptions val2 = new RaiseEventOptions();
					val2.TargetActors = new int[1] { val3.ActorNumber };
					PhotonSerializer.SerializePhotonView(getView, val2);
					num++;
				}
				else
				{
					num++;
				}
			}
			Safety.ResetNetworkLimits();
			((Behaviour)VRRig.LocalRig).enabled = true;
			((Component)VRRig.LocalRig).transform.position = position;
			return false;
		}
	}

	[CompilerGenerated]
	private sealed class CapturedVariables530
	{
		public Color color;

		internal bool HoverboardScreenGun_Lambda0()
		{
			if ((Object)(object)GunLib.GunLib_Reference_06 == (Object)null)
			{
				return false;
			}
			AttachHoverboardToRig(GunLib.GunLib_Reference_06, color);
			PhotonSerializer.SerializePhotonView(Variables.Variables_Reference_09.myVRRig.GetView);
			return false;
		}
	}

	[CompilerGenerated]
	private sealed class ServerSideEquipRoutine_StateMachine101 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int State;

		private object Current;

		private Vector3 originCaptured1;

		private GTZone[] savedZonesCaptured2;

		private List<GTZone> withCityCaptured3;

		private float timeoutCaptured4;

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
		void IDisposable.Dispose()
		{
			int num = State;
			if (num == -3 || (uint)(num - 1) <= 4u)
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
			savedZonesCaptured2 = null;
			withCityCaptured3 = null;
			State = -2;
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		private void Finally1()
		{
			State = -1;
			ServerSideEquipRoutine_StateMachine101_State_10 = false;
		}

		private bool MoveNext()
		{
			bool result;
			try
			{
				int num = State;
				num = (((uint)num <= 5u) ? num : 6) + 48;
				int num2 = num;
				if (num2 == 49)
				{
					State = -3;
					if (!ZoneManagement.IsZoneLoaded((GTZone)1))
					{
						goto Branch_01a9;
					}
					goto Branch_01cf;
				}
				State = -1;
				State = -3;
				if ((Object)(object)Variables.Variables_Reference_09 == (Object)null || (Object)(object)Variables.Variables_Reference_09.bodyCollider == (Object)null)
				{
					result = false;
					Finally1();
				}
				else
				{
					originCaptured1 = ((Component)Variables.Variables_Reference_09.bodyCollider).transform.position;
					savedZonesCaptured2 = null;
					if ((Object)(object)ZoneManagement.instance != (Object)null && !ZoneManagement.IsInZone((GTZone)1))
					{
						savedZonesCaptured2 = ZoneManagement.instance.activeZones.ToArray();
						withCityCaptured3 = new List<GTZone>(ZoneManagement.instance.activeZones) { (GTZone)1 };
						ZoneManagement.SetActiveZones(withCityCaptured3.ToArray());
						timeoutCaptured4 = Time.time + 5f;
						if (!ZoneManagement.IsZoneLoaded((GTZone)1))
						{
							goto Branch_01a9;
						}
						goto Branch_01cf;
					}
					Movement.TeleportToPosition(ServerSideEquipRoutine_StateMachine101_Position_01);
					Current = (object)new WaitForSeconds(1f);
					State = 3;
					result = true;
				}
				goto EndBranch_0000;
				Branch_01a9:
				if (!(Time.time < timeoutCaptured4))
				{
					goto Branch_01cf;
				}
				Current = null;
				State = 1;
				result = true;
				goto EndBranch_0000;
				Branch_01cf:
				Current = (object)new WaitForSeconds(0.5f);
				State = 2;
				result = true;
				EndBranch_0000:;
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
		public ServerSideEquipRoutine_StateMachine101(int State)
		{
			this.State = State;
		}
	}

	private static readonly Vector3[] ServerSideEquipRoutine_StateMachine101_Position_03;

	private static readonly Vector3[] ServerSideEquipRoutine_StateMachine101_Position_04;

	private static GameObject ServerSideEquipRoutine_StateMachine101_Object_02;

	private static GameObject ServerSideEquipRoutine_StateMachine101_Object_05;

	private static readonly HashSet<GameObject> ServerSideEquipRoutine_StateMachine101_Object_01;

	private static bool ServerSideEquipRoutine_StateMachine101_State_09;

	private static int ServerSideEquipRoutine_StateMachine101_Index_06;

	private static Color ServerSideEquipRoutine_StateMachine101_Color_01;

	private static GameObject ServerSideEquipRoutine_StateMachine101_Object_06;

	private static float ServerSideEquipRoutine_StateMachine101_Value_06;

	private static float ServerSideEquipRoutine_StateMachine101_Value_03;

	private static float ServerSideEquipRoutine_StateMachine101_Value_04;

	private static bool ServerSideEquipRoutine_StateMachine101_State_14;

	private static bool ServerSideEquipRoutine_StateMachine101_State_05;

	private static float ServerSideEquipRoutine_StateMachine101_Value_07;

	private static Coroutine ServerSideEquipRoutine_StateMachine101_Routine_02;

	public static Coroutine ServerSideEquipRoutine_StateMachine101_Routine_01;

	private static bool ServerSideEquipRoutine_StateMachine101_State_04;

	private static int ServerSideEquipRoutine_StateMachine101_Index_05;

	public static CosmeticsController.CosmeticItem ServerSideEquipRoutine_StateMachine101_Reference_05;

	public static string ServerSideEquipRoutine_StateMachine101_Text_01;

	public static ButtonHandler.Button ServerSideEquipRoutine_StateMachine101_Button_01;

	private static float ServerSideEquipRoutine_StateMachine101_Value_01;

	private static bool ServerSideEquipRoutine_StateMachine101_State_01;

	private static float ServerSideEquipRoutine_StateMachine101_Value_05;

	private static BetterDayNightManager.WeatherType[] ServerSideEquipRoutine_StateMachine101_Values_02;

	private static GameObject[] ServerSideEquipRoutine_StateMachine101_Object_08;

	private static float ServerSideEquipRoutine_StateMachine101_Value_02;

	private static float ServerSideEquipRoutine_StateMachine101_Value_08;

	private static WaterVolume[] ServerSideEquipRoutine_StateMachine101_Values_01;

	public static GameObject ServerSideEquipRoutine_StateMachine101_Object_09;

	private static int[] ServerSideEquipRoutine_StateMachine101_Index_01;

	private static int[] ServerSideEquipRoutine_StateMachine101_Index_02;

	private static bool ServerSideEquipRoutine_StateMachine101_State_12;

	public static readonly HashSet<string> ServerSideEquipRoutine_StateMachine101_Text_02;

	public static bool ServerSideEquipRoutine_StateMachine101_State_06;

	private static readonly Vector3 ServerSideEquipRoutine_StateMachine101_Position_01;

	private static bool ServerSideEquipRoutine_StateMachine101_State_10;

	private static float ServerSideEquipRoutine_StateMachine101_Value_09;

	private static GameObject ServerSideEquipRoutine_StateMachine101_Object_10;

	private static GameObject ServerSideEquipRoutine_StateMachine101_Object_04;

	private static LineRenderer ServerSideEquipRoutine_StateMachine101_Reference_04;

	private static LineRenderer ServerSideEquipRoutine_StateMachine101_Reference_02;

	private static SpringJoint ServerSideEquipRoutine_StateMachine101_Index_04;

	private static SpringJoint ServerSideEquipRoutine_StateMachine101_Index_03;

	private static bool ServerSideEquipRoutine_StateMachine101_State_11;

	private static bool ServerSideEquipRoutine_StateMachine101_State_03;

	private static bool ServerSideEquipRoutine_StateMachine101_State_13;

	private const float maxDistance = 100f;

	private const float Spring = 5000f;

	private const float Damper = 4000f;

	private const float MassScale = 6f;

	private const float pullspeed = 3f;

	private const float speedtopull = 2.5f;

	private static GameObject ServerSideEquipRoutine_StateMachine101_Object_07;

	private static GameObject ServerSideEquipRoutine_StateMachine101_Object_03;

	private static LineRenderer ServerSideEquipRoutine_StateMachine101_Reference_03;

	private static LineRenderer ServerSideEquipRoutine_StateMachine101_Reference_01;

	private static Vector3 ServerSideEquipRoutine_StateMachine101_Position_02;

	private static Vector3 ServerSideEquipRoutine_StateMachine101_Position_05;

	private static bool ServerSideEquipRoutine_StateMachine101_State_07;

	private static bool ServerSideEquipRoutine_StateMachine101_State_02;

	private static bool ServerSideEquipRoutine_StateMachine101_State_08;

	public static void SpawnHoverboard()
	{
		DropHoverboard(((Component)VRRig.LocalRig).transform.position, ((Component)VRRig.LocalRig).transform.rotation, Vector3.zero, Vector3.zero, Variables.RandomColor());
		GTPlayer.Instance.SetHoverAllowed(true, false);
	}

	public static void GliderAnnoyAll()
	{
		GliderHoldable[] array = Variables.FindObjectsCached<GliderHoldable>(false);
		int num = 0;
		if (!InputHandler.IsRightTriggerPressed())
		{
			return;
		}
		using IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.Where((VRRig r) => !r.isLocal).GetEnumerator();
		if (!enumerator.MoveNext())
		{
			return;
		}
		while (true)
		{
			VRRig current = enumerator.Current;
			if (num >= array.Length)
			{
				break;
			}
			GliderHoldable val = array[num];
			if (((NetworkView)array[num]).GetView.Owner == PhotonNetwork.LocalPlayer)
			{
				((Component)val).gameObject.transform.position = current.headMesh.transform.position;
				((Component)val).gameObject.transform.rotation = Variables.RandomRotation();
				num++;
				if (!enumerator.MoveNext())
				{
					break;
				}
			}
			else
			{
				((NetworkHoldableObject)val).OnHover((InteractionPoint)null, (GameObject)null);
				num++;
				if (!enumerator.MoveNext())
				{
					break;
				}
			}
		}
	}

	public static void SplashAura()
	{
		if (!InputHandler.IsRightTriggerPressed() || Time.time < ServerSideEquipRoutine_StateMachine101_Value_08)
		{
			return;
		}
		ServerSideEquipRoutine_StateMachine101_Value_08 = Time.time + ServerSideEquipRoutine_StateMachine101_Value_02;
		Safety.ResetNetworkLimits();
		using IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			while (true)
			{
				VRRig current = enumerator.Current;
				if ((Object)(object)current == (Object)(object)Variables.Variables_Reference_09.offlineVRRig || (Object)(object)current == (Object)(object)Variables.Variables_Reference_09.myVRRig)
				{
					break;
				}
				PlaySplashEffect(((Component)current).transform.position, ((Component)current).transform.rotation, 125f);
				if (!enumerator.MoveNext())
				{
					return;
				}
			}
		}
	}

	public static void GrabGliders()
	{
		if (!InputHandler.IsRightGripPressed())
		{
			return;
		}
		GliderHoldable[] array = Variables.FindObjectsCached<GliderHoldable>(false);
		int num = 0;
		while (num < array.Length)
		{
			GliderHoldable val = array[num];
			if (((NetworkView)array[num]).GetView.Owner == PhotonNetwork.LocalPlayer)
			{
				((Component)val).gameObject.transform.position = Variables.Variables_Reference_09.rightHandTransform.position;
				((Component)val).gameObject.transform.rotation = Variables.Variables_Reference_09.rightHandTransform.rotation;
				num++;
			}
			else
			{
				((NetworkHoldableObject)val).OnHover((InteractionPoint)null, (GameObject)null);
				num++;
			}
		}
	}

	public static void MoveObjectGun(string name)
	{
		if (!GunLib.GunGrips)
		{
			GunLib.SetGunVisualsVisible(isVisible: false);
			return;
		}
		GunLib.UpdateGunRaycast();
		if (GunLib.GunTriggers)
		{
			Variables.FindCachedGameObject(name).transform.position = ((RaycastHit)GunLib.GunLib_Reference_07).point;
		}
	}

	public static void SetRainEnabled(bool setActive)
	{
		if (setActive)
		{
			ServerSideEquipRoutine_StateMachine101_Values_02 = (WeatherType[])((BetterDayNightManager)BetterDayNightManager.instance).weatherCycle.Clone();
			int num = 1;
			if (num < ((BetterDayNightManager)BetterDayNightManager.instance).weatherCycle.Length)
			{
				do
				{
					((BetterDayNightManager)BetterDayNightManager.instance).weatherCycle[num] = (WeatherType)1;
					num++;
				}
				while (num < ((BetterDayNightManager)BetterDayNightManager.instance).weatherCycle.Length);
			}
		}
		else if (ServerSideEquipRoutine_StateMachine101_Values_02 != null)
		{
			((BetterDayNightManager)BetterDayNightManager.instance).weatherCycle = ServerSideEquipRoutine_StateMachine101_Values_02;
			ServerSideEquipRoutine_StateMachine101_Values_02 = null;
		}
		else
		{
			ReflectionCompat.Invoke(BetterDayNightManager.instance, "GenerateWeatherEventTimes");
			ServerSideEquipRoutine_StateMachine101_Values_02 = null;
		}
	}

	public static void SpamBracelet()
	{
		if (Time.time > ServerSideEquipRoutine_StateMachine101_Value_09)
		{
			AlternateNonCosmeticHandItem(Time.frameCount % 2 == 0);
			ServerSideEquipRoutine_StateMachine101_Value_09 = Time.time + 0.1f;
		}
	}

	public static void ApplyScreenEffectGun(Color color)
	{
		CapturedVariables530 LocalScope2 = new CapturedVariables530();
		LocalScope2.color = color;
		if (GunLib.TrySelectRig())
		{
			MenuPatches.SerializationPatch.SerializationPatch_State_01 = delegate
			{
				if ((Object)(object)GunLib.GunLib_Reference_06 == (Object)null)
				{
					return false;
				}
				AttachHoverboardToRig(GunLib.GunLib_Reference_06, LocalScope2.color);
				PhotonSerializer.SerializePhotonView(Variables.Variables_Reference_09.myVRRig.GetView);
				return false;
			};
		}
		else
		{
			MenuPatches.SerializationPatch.SerializationPatch_State_01 = null;
			((Behaviour)VRRig.LocalRig).enabled = true;
		}
	}

	public static void SetTryOnCosmeticsAnywhereEnabled(bool enable)
	{
		if (enable == ServerSideEquipRoutine_StateMachine101_State_12 || (Object)(object)CosmeticsController.instance == (Object)null || (Object)(object)Variables.Variables_Reference_09 == (Object)null || (Object)(object)Variables.Variables_Reference_09.myVRRig == (Object)null)
		{
			return;
		}
		ServerSideEquipRoutine_StateMachine101_State_12 = enable;
		int[] array;
		if (!enable)
		{
			array = ServerSideEquipRoutine_StateMachine101_Index_01;
			if (enable)
			{
				goto Branch_010d;
			}
		}
		else
		{
			array = ServerSideEquipRoutine_StateMachine101_Index_02 ?? (ServerSideEquipRoutine_StateMachine101_Index_02 = PackCosmeticIds(Enumerable.Repeat("LMAJU.", 16).ToArray()));
			if (enable)
			{
				goto Branch_010d;
			}
		}
		CosmeticSet val = new CosmeticSet(array, CosmeticsController.instance);
		((CosmeticsController)CosmeticsController.instance).currentWornSet = val;
		Variables.Variables_Reference_09.offlineVRRig.cosmeticSet = val;
		Variables.Variables_Reference_09.myVRRig.SendRPC("RPC_UpdateCosmeticsWithTryonPacked", (RpcTarget)0, new object[3]
		{
			array,
			((CosmeticsController)CosmeticsController.instance).tryOnSet.ToPackedIDArray(),
			false
		});
		Safety.ResetNetworkLimits();
		return;
		Branch_010d:
		ServerSideEquipRoutine_StateMachine101_Index_01 = ((CosmeticsController)CosmeticsController.instance).currentWornSet.ToPackedIDArray();
		val = new CosmeticSet(array, CosmeticsController.instance);
		((CosmeticsController)CosmeticsController.instance).currentWornSet = val;
		Variables.Variables_Reference_09.offlineVRRig.cosmeticSet = val;
		Variables.Variables_Reference_09.myVRRig.SendRPC("RPC_UpdateCosmeticsWithTryonPacked", (RpcTarget)0, new object[3]
		{
			array,
			((CosmeticsController)CosmeticsController.instance).tryOnSet.ToPackedIDArray(),
			false
		});
		Safety.ResetNetworkLimits();
	}

	public static void OrbitObjectAroundHead(string name, float offset = 0f)
	{
		GameObject val = GameObject.Find(name);
		if ((Object)(object)val != (Object)null)
		{
			val.transform.position = ((Component)Variables.Variables_Reference_09.headCollider).transform.position + new Vector3(MathF.Cos(offset + (float)Time.frameCount / 30f), 1f, MathF.Sin(offset + (float)Time.frameCount / 30f));
		}
	}

	private static string ToTitleCase(string text)
	{
		if (string.IsNullOrEmpty(text))
		{
			return text;
		}
		string[] array = text.ToLower().Split(' ');
		int num = 0;
		while (num < array.Length)
		{
			if (array[num].Length > 0)
			{
				array[num] = char.ToUpper(array[num][0]) + array[num].Substring(1);
				num++;
			}
			else
			{
				num++;
			}
		}
		return string.Join(" ", array);
	}

	private static void DestroyGrapplingHookObjects()
	{
		if ((Object)(object)ServerSideEquipRoutine_StateMachine101_Reference_03 != (Object)null)
		{
			Object.Destroy((Object)(object)((Renderer)ServerSideEquipRoutine_StateMachine101_Reference_03).material);
			if ((Object)(object)ServerSideEquipRoutine_StateMachine101_Reference_01 != (Object)null)
			{
				goto Branch_006c;
			}
		}
		else if ((Object)(object)ServerSideEquipRoutine_StateMachine101_Reference_01 != (Object)null)
		{
			goto Branch_006c;
		}
		Main.DestroyAndClear<GameObject>(ref ServerSideEquipRoutine_StateMachine101_Object_07, 0f);
		Main.DestroyAndClear<GameObject>(ref ServerSideEquipRoutine_StateMachine101_Object_03, 0f);
		Main.DestroyAndClear<LineRenderer>(ref ServerSideEquipRoutine_StateMachine101_Reference_03, 0f);
		Main.DestroyAndClear<LineRenderer>(ref ServerSideEquipRoutine_StateMachine101_Reference_01, 0f);
		ServerSideEquipRoutine_StateMachine101_State_07 = (ServerSideEquipRoutine_StateMachine101_State_02 = (ServerSideEquipRoutine_StateMachine101_State_08 = false));
		return;
		Branch_006c:
		Object.Destroy((Object)(object)((Renderer)ServerSideEquipRoutine_StateMachine101_Reference_01).material);
		Main.DestroyAndClear<GameObject>(ref ServerSideEquipRoutine_StateMachine101_Object_07, 0f);
		Main.DestroyAndClear<GameObject>(ref ServerSideEquipRoutine_StateMachine101_Object_03, 0f);
		Main.DestroyAndClear<LineRenderer>(ref ServerSideEquipRoutine_StateMachine101_Reference_03, 0f);
		Main.DestroyAndClear<LineRenderer>(ref ServerSideEquipRoutine_StateMachine101_Reference_01, 0f);
		ServerSideEquipRoutine_StateMachine101_State_07 = (ServerSideEquipRoutine_StateMachine101_State_02 = (ServerSideEquipRoutine_StateMachine101_State_08 = false));
	}

	public static void HoldObjectInRightHand(string name)
	{
		if (InputHandler.IsRightGripPressed())
		{
			GameObject val = GameObject.Find(name);
			if ((Object)(object)val != (Object)null)
			{
				val.transform.position = Variables.Variables_Reference_09.rightHandTransform.position;
				val.transform.rotation = Variables.Variables_Reference_09.rightHandTransform.rotation;
			}
		}
	}

	public static void SpazGliders()
	{
		if (!InputHandler.IsRightTriggerPressed())
		{
			return;
		}
		GliderHoldable[] array = Variables.FindObjectsCached<GliderHoldable>(false);
		int num = 0;
		while (num < array.Length)
		{
			GliderHoldable val = array[num];
			if (((NetworkView)array[num]).GetView.Owner == PhotonNetwork.LocalPlayer)
			{
				((Component)val).gameObject.transform.rotation = Variables.RandomRotation();
				num++;
			}
			else
			{
				((NetworkHoldableObject)val).OnHover((InteractionPoint)null, (GameObject)null);
				num++;
			}
		}
	}


	public static void SplashSelf()
	{
		if (InputHandler.IsRightTriggerPressed() && !(Time.time < ServerSideEquipRoutine_StateMachine101_Value_08))
		{
			ServerSideEquipRoutine_StateMachine101_Value_08 = Time.time + ServerSideEquipRoutine_StateMachine101_Value_02;
			Safety.ResetNetworkLimits();
			PlaySplashEffect(((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position, ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.rotation, 125f);
		}
	}

	public static void CycleSnowballEffectAll(bool forward)
	{
		List<CosmeticItem> allCosmetics = ReflectionCompat.GetField(CosmeticsController.instance, "_allCosmetics", new List<CosmeticItem>());
		int num = ServerSideEquipRoutine_StateMachine101_Index_05;
		ServerSideEquipRoutine_StateMachine101_Index_05 += (forward ? 1 : (-1));
		if (ServerSideEquipRoutine_StateMachine101_Index_05 < allCosmetics.Count)
		{
			goto Branch_00ad;
		}
		goto Branch_008b;
		Branch_00c9:
		ServerSideEquipRoutine_StateMachine101_Index_05 = allCosmetics.Count - 1;
		if (!allCosmetics[ServerSideEquipRoutine_StateMachine101_Index_05].canTryOn)
		{
			goto Branch_0159;
		}
		Branch_011e:
		if (allCosmetics[ServerSideEquipRoutine_StateMachine101_Index_05].isNullItem)
		{
			goto Branch_0159;
		}
		Branch_0173:
		ServerSideEquipRoutine_StateMachine101_Reference_05 = allCosmetics[ServerSideEquipRoutine_StateMachine101_Index_05];
		ServerSideEquipRoutine_StateMachine101_Text_01 = ToTitleCase(((CosmeticsController)CosmeticsController.instance).GetItemDisplayName(ServerSideEquipRoutine_StateMachine101_Reference_05));
		ServerSideEquipRoutine_StateMachine101_Button_01?.SetText($"{ServerSideEquipRoutine_StateMachine101_Text_01} ({ServerSideEquipRoutine_StateMachine101_Reference_05.cost})");
		return;
		Branch_00ad:
		if (ServerSideEquipRoutine_StateMachine101_Index_05 >= 0)
		{
			goto Branch_00fa;
		}
		goto Branch_00c9;
		Branch_00fa:
		if (!allCosmetics[ServerSideEquipRoutine_StateMachine101_Index_05].canTryOn)
		{
			goto Branch_0159;
		}
		goto Branch_011e;
		Branch_0159:
		if (ServerSideEquipRoutine_StateMachine101_Index_05 != num)
		{
			ServerSideEquipRoutine_StateMachine101_Index_05 += (forward ? 1 : (-1));
			if (ServerSideEquipRoutine_StateMachine101_Index_05 < allCosmetics.Count)
			{
				goto Branch_00ad;
			}
			goto Branch_008b;
		}
		goto Branch_0173;
		Branch_008b:
		ServerSideEquipRoutine_StateMachine101_Index_05 = 0;
		if (ServerSideEquipRoutine_StateMachine101_Index_05 >= 0)
		{
			goto Branch_00fa;
		}
		goto Branch_00c9;
	}

	public static void WaterBarrage()
	{
		if (InputHandler.IsRightTriggerPressed() && !(Time.time < ServerSideEquipRoutine_StateMachine101_Value_08))
		{
			ServerSideEquipRoutine_StateMachine101_Value_08 = Time.time + ServerSideEquipRoutine_StateMachine101_Value_02;
			Safety.ResetNetworkLimits();
			Vector3 insideUnitSphere = Random.insideUnitSphere;
			Vector3 val = ((Vector3)insideUnitSphere).normalized * Random.Range(1.5f, 1.6f);
			Vector3 val2 = ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position + val;
			PlaySplashEffect(val2, Quaternion.LookRotation(((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position - val2), 125f);
		}
	}

	private static void SetNonCosmeticHandItemEnabled(bool enable, bool left)
	{
		Variables.Variables_Reference_09.myVRRig.SendRPC("EnableNonCosmeticHandItemRPC", (RpcTarget)0, new object[2] { enable, left });
	}

	public static void SetAirSwimEnabled(bool active)
	{
		if (active)
		{
			if ((Object)(object)ServerSideEquipRoutine_StateMachine101_Object_09 == (Object)null)
			{
				GameObject val = Variables.FindCachedGameObject("Environment Objects/LocalObjects_Prefab/ForestToBeach/ForestToBeach_Prefab_V4/ForestToBeach_Geo/CaveWaterVolume");
				if (!((Object)(object)val == (Object)null))
				{
					ServerSideEquipRoutine_StateMachine101_Object_09 = Object.Instantiate<GameObject>(val);
					ServerSideEquipRoutine_StateMachine101_Object_09.transform.localScale = new Vector3(5f, 5f, 5f);
					ServerSideEquipRoutine_StateMachine101_Object_09.GetComponent<Renderer>().enabled = false;
					Variables.Variables_Reference_06.audioManager.UnsetMixerSnapshot(0.1f);
					ServerSideEquipRoutine_StateMachine101_Object_09.transform.position = ((Component)GorillaTagger.Instance.headCollider).transform.position + new Vector3(0f, 2.5f, 0f);
				}
			}
			else
			{
				Variables.Variables_Reference_06.audioManager.UnsetMixerSnapshot(0.1f);
				ServerSideEquipRoutine_StateMachine101_Object_09.transform.position = ((Component)GorillaTagger.Instance.headCollider).transform.position + new Vector3(0f, 2.5f, 0f);
			}
		}
		else if ((Object)(object)ServerSideEquipRoutine_StateMachine101_Object_09 != (Object)null)
		{
			Object.Destroy((Object)(object)ServerSideEquipRoutine_StateMachine101_Object_09);
			ServerSideEquipRoutine_StateMachine101_Object_09 = null;
		}
	}

	public static void PossessObject(string name)
	{
		bool isPressed = InputHandler.IsRightPrimaryPressed();
		if (isPressed && !ServerSideEquipRoutine_StateMachine101_State_05)
		{
			ServerSideEquipRoutine_StateMachine101_State_14 = !ServerSideEquipRoutine_StateMachine101_State_14;
			if (ServerSideEquipRoutine_StateMachine101_State_14)
			{
				((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = false;
				((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position = new Vector3(999f, 999f, 999f);
				ServerSideEquipRoutine_StateMachine101_State_05 = true;
			}
			else
			{
				((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = true;
				ServerSideEquipRoutine_StateMachine101_State_05 = true;
			}
		}
		else if (!isPressed)
		{
			ServerSideEquipRoutine_StateMachine101_State_05 = false;
			if (!ServerSideEquipRoutine_StateMachine101_State_14)
			{
				return;
			}
			goto Branch_0127;
		}
		if (!ServerSideEquipRoutine_StateMachine101_State_14)
		{
			return;
		}
		Branch_0127:
		Transform transform = ((Component)Variables.Variables_Reference_09.headCollider).transform;
		Variables.FindCachedGameObject(name).transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0f, transform.eulerAngles.y, 0f));
	}

	public static void ConfigureGliders(float pull, float drag)
	{
		GliderHoldable[] array = Variables.FindObjectsCached<GliderHoldable>(false);
		foreach (GliderHoldable val in array)
		{
			val.pullUpLiftBonus = pull;
			val.dragVsSpeedDragFactor = drag;
		}
	}

	public static void SilentHandTaps()
	{
		MenuPatches.GrabPatch_State_06 = true;
		MenuPatches.GrabPatch_State_07 = false;
		MenuPatches.GrabPatch_State_03 = false;
		MenuPatches.GrabPatch_Value_01 = 0f;
		MenuPatches.GrabPatch_Index_01 = 0;
		Variables.Variables_Reference_09.handTapVolume = 0f;
	}

	public static void StrobeHoverboard()
	{
		if (!((Object)(object)VRRig.LocalRig.hoverboardVisual == (Object)null) && VRRig.LocalRig.hoverboardVisual.IsHeld)
		{
			if (Time.time > ServerSideEquipRoutine_StateMachine101_Value_05)
			{
				ServerSideEquipRoutine_StateMachine101_Value_05 = Time.time + 0.1f;
				ServerSideEquipRoutine_StateMachine101_State_01 = !ServerSideEquipRoutine_StateMachine101_State_01;
				VRRig.LocalRig.hoverboardVisual.SetIsHeld(VRRig.LocalRig.hoverboardVisual.IsLeftHanded, VRRig.LocalRig.hoverboardVisual.NominalLocalPosition, VRRig.LocalRig.hoverboardVisual.NominalLocalRotation, ServerSideEquipRoutine_StateMachine101_State_01 ? Color.white : Color.black);
			}
			else
			{
				VRRig.LocalRig.hoverboardVisual.SetIsHeld(VRRig.LocalRig.hoverboardVisual.IsLeftHanded, VRRig.LocalRig.hoverboardVisual.NominalLocalPosition, VRRig.LocalRig.hoverboardVisual.NominalLocalRotation, ServerSideEquipRoutine_StateMachine101_State_01 ? Color.white : Color.black);
			}
		}
	}

	private static void DestroySpiderWebObjects()
	{
		if ((Object)(object)ServerSideEquipRoutine_StateMachine101_Reference_04 != (Object)null)
		{
			Object.Destroy((Object)(object)((Renderer)ServerSideEquipRoutine_StateMachine101_Reference_04).material);
			if ((Object)(object)ServerSideEquipRoutine_StateMachine101_Reference_02 != (Object)null)
			{
				goto Branch_006c;
			}
		}
		else if ((Object)(object)ServerSideEquipRoutine_StateMachine101_Reference_02 != (Object)null)
		{
			goto Branch_006c;
		}
		Main.DestroyAndClear<GameObject>(ref ServerSideEquipRoutine_StateMachine101_Object_10, 0f);
		Main.DestroyAndClear<GameObject>(ref ServerSideEquipRoutine_StateMachine101_Object_04, 0f);
		Main.DestroyAndClear<LineRenderer>(ref ServerSideEquipRoutine_StateMachine101_Reference_04, 0f);
		Main.DestroyAndClear<LineRenderer>(ref ServerSideEquipRoutine_StateMachine101_Reference_02, 0f);
		Main.DestroyAndClear<SpringJoint>(ref ServerSideEquipRoutine_StateMachine101_Index_04, 0f);
		Main.DestroyAndClear<SpringJoint>(ref ServerSideEquipRoutine_StateMachine101_Index_03, 0f);
		ServerSideEquipRoutine_StateMachine101_State_13 = false;
		ServerSideEquipRoutine_StateMachine101_State_11 = true;
		ServerSideEquipRoutine_StateMachine101_State_03 = true;
		return;
		Branch_006c:
		Object.Destroy((Object)(object)((Renderer)ServerSideEquipRoutine_StateMachine101_Reference_02).material);
		Main.DestroyAndClear<GameObject>(ref ServerSideEquipRoutine_StateMachine101_Object_10, 0f);
		Main.DestroyAndClear<GameObject>(ref ServerSideEquipRoutine_StateMachine101_Object_04, 0f);
		Main.DestroyAndClear<LineRenderer>(ref ServerSideEquipRoutine_StateMachine101_Reference_04, 0f);
		Main.DestroyAndClear<LineRenderer>(ref ServerSideEquipRoutine_StateMachine101_Reference_02, 0f);
		Main.DestroyAndClear<SpringJoint>(ref ServerSideEquipRoutine_StateMachine101_Index_04, 0f);
		Main.DestroyAndClear<SpringJoint>(ref ServerSideEquipRoutine_StateMachine101_Index_03, 0f);
		ServerSideEquipRoutine_StateMachine101_State_13 = false;
		ServerSideEquipRoutine_StateMachine101_State_11 = true;
		ServerSideEquipRoutine_StateMachine101_State_03 = true;
	}

	public static void TeleportToObject(string name)
	{
		GameObject val = GameObject.Find(name);
		if (!((Object)(object)val == (Object)null))
		{
			Movement.TeleportToPosition(val.transform.position);
			((Component)Variables.Variables_Reference_09).GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
		}
	}

	public static void RespawnGliders()
	{
		GliderHoldable[] array = Variables.FindObjectsCached<GliderHoldable>(false);
		int num = 0;
		while (num < array.Length)
		{
			GliderHoldable val = array[num];
			if (((NetworkView)array[num]).GetView.Owner == PhotonNetwork.LocalPlayer)
			{
				val.Respawn();
				num++;
			}
			else
			{
				((NetworkHoldableObject)val).OnHover((InteractionPoint)null, (GameObject)null);
				num++;
			}
		}
	}

	public static void RandomizeObjectRotation(string name)
	{
		GameObject val = GameObject.Find(name);
		if ((Object)(object)val != (Object)null)
		{
			val.transform.rotation = Variables.RandomRotation();
		}
	}

	public static void SetGrapplingHookEnabled(bool active)
	{
		Color startColor;
		float startWidth;
		if (!active)
		{
			DestroyGrapplingHookObjects();
		}
		else if (!ServerSideEquipRoutine_StateMachine101_State_08)
		{
			if ((Object)(object)ServerSideEquipRoutine_StateMachine101_Reference_03 == (Object)null)
			{
				ServerSideEquipRoutine_StateMachine101_Reference_03 = new GameObject("RGrappleLine").AddComponent<LineRenderer>();
				((Renderer)ServerSideEquipRoutine_StateMachine101_Reference_03).material = new Material(Shader.Find("Sprites/Default"));
				LineRenderer dB3HH5DN = ServerSideEquipRoutine_StateMachine101_Reference_03;
				startColor = (ServerSideEquipRoutine_StateMachine101_Reference_03.endColor = Color.white);
				dB3HH5DN.startColor = startColor;
				LineRenderer dB3HH5DN2 = ServerSideEquipRoutine_StateMachine101_Reference_03;
				startWidth = (ServerSideEquipRoutine_StateMachine101_Reference_03.endWidth = 0.02f);
				dB3HH5DN2.startWidth = startWidth;
				ServerSideEquipRoutine_StateMachine101_Reference_03.positionCount = 0;
				((Component)ServerSideEquipRoutine_StateMachine101_Reference_03).transform.SetParent(((Component)Variables.Variables_Reference_06).transform);
				if ((Object)(object)ServerSideEquipRoutine_StateMachine101_Reference_01 == (Object)null)
				{
					goto Branch_0125;
				}
			}
			else if ((Object)(object)ServerSideEquipRoutine_StateMachine101_Reference_01 == (Object)null)
			{
				goto Branch_0125;
			}
			ServerSideEquipRoutine_StateMachine101_State_08 = true;
			UpdateGrapplingHookHand(ref ServerSideEquipRoutine_StateMachine101_Object_07, ref ServerSideEquipRoutine_StateMachine101_Reference_03, ref ServerSideEquipRoutine_StateMachine101_Position_02, ref ServerSideEquipRoutine_StateMachine101_State_07, Variables.Variables_Reference_06.RightHand.controllerTransform, ((ControllerInputPoller)ControllerInputPoller.instance).rightControllerIndexFloat, isRight: false);
			UpdateGrapplingHookHand(ref ServerSideEquipRoutine_StateMachine101_Object_03, ref ServerSideEquipRoutine_StateMachine101_Reference_01, ref ServerSideEquipRoutine_StateMachine101_Position_05, ref ServerSideEquipRoutine_StateMachine101_State_02, Variables.Variables_Reference_06.LeftHand.controllerTransform, ((ControllerInputPoller)ControllerInputPoller.instance).leftControllerIndexFloat, isRight: true);
		}
		else
		{
			UpdateGrapplingHookHand(ref ServerSideEquipRoutine_StateMachine101_Object_07, ref ServerSideEquipRoutine_StateMachine101_Reference_03, ref ServerSideEquipRoutine_StateMachine101_Position_02, ref ServerSideEquipRoutine_StateMachine101_State_07, Variables.Variables_Reference_06.RightHand.controllerTransform, ((ControllerInputPoller)ControllerInputPoller.instance).rightControllerIndexFloat, isRight: false);
			UpdateGrapplingHookHand(ref ServerSideEquipRoutine_StateMachine101_Object_03, ref ServerSideEquipRoutine_StateMachine101_Reference_01, ref ServerSideEquipRoutine_StateMachine101_Position_05, ref ServerSideEquipRoutine_StateMachine101_State_02, Variables.Variables_Reference_06.LeftHand.controllerTransform, ((ControllerInputPoller)ControllerInputPoller.instance).leftControllerIndexFloat, isRight: true);
		}
		return;
		Branch_0125:
		ServerSideEquipRoutine_StateMachine101_Reference_01 = new GameObject("LGrappleLine").AddComponent<LineRenderer>();
		((Renderer)ServerSideEquipRoutine_StateMachine101_Reference_01).material = new Material(Shader.Find("Sprites/Default"));
		LineRenderer bZ0991GX = ServerSideEquipRoutine_StateMachine101_Reference_01;
		startColor = (ServerSideEquipRoutine_StateMachine101_Reference_01.endColor = Color.white);
		bZ0991GX.startColor = startColor;
		LineRenderer bZ0991GX2 = ServerSideEquipRoutine_StateMachine101_Reference_01;
		startWidth = (ServerSideEquipRoutine_StateMachine101_Reference_01.endWidth = 0.02f);
		bZ0991GX2.startWidth = startWidth;
		ServerSideEquipRoutine_StateMachine101_Reference_01.positionCount = 0;
		((Component)ServerSideEquipRoutine_StateMachine101_Reference_01).transform.SetParent(((Component)Variables.Variables_Reference_06).transform);
		ServerSideEquipRoutine_StateMachine101_State_08 = true;
		UpdateGrapplingHookHand(ref ServerSideEquipRoutine_StateMachine101_Object_07, ref ServerSideEquipRoutine_StateMachine101_Reference_03, ref ServerSideEquipRoutine_StateMachine101_Position_02, ref ServerSideEquipRoutine_StateMachine101_State_07, Variables.Variables_Reference_06.RightHand.controllerTransform, ((ControllerInputPoller)ControllerInputPoller.instance).rightControllerIndexFloat, isRight: false);
		UpdateGrapplingHookHand(ref ServerSideEquipRoutine_StateMachine101_Object_03, ref ServerSideEquipRoutine_StateMachine101_Reference_01, ref ServerSideEquipRoutine_StateMachine101_Position_05, ref ServerSideEquipRoutine_StateMachine101_State_02, Variables.Variables_Reference_06.LeftHand.controllerTransform, ((ControllerInputPoller)ControllerInputPoller.instance).leftControllerIndexFloat, isRight: true);
	}

	public static void RemoveBracelets()
	{
		SetNonCosmeticHandItemEnabled(enable: false, left: true);
		SetNonCosmeticHandItemEnabled(enable: false, left: false);
		Safety.ResetNetworkLimits();
	}

	public static int[] PackCosmeticIds(string[] cosmetics)
	{
		return new CosmeticSet(cosmetics, CosmeticsController.instance).ToPackedIDArray();
	}

	public static void AddBarrelToCart()
	{
		((CosmeticsController)CosmeticsController.instance).currentCart.Insert(0, ((CosmeticsController)CosmeticsController.instance).GetItemFromDict("LMAPE."));
	}

	public static void MoveObjectToGunTarget(string name)
	{
		if (GunLib.IsGunTriggerPressed())
		{
			GameObject val = GameObject.Find(name);
			if ((Object)(object)val != (Object)null)
			{
				val.transform.position = ((RaycastHit)GunLib.GunLib_Reference_07).point + Vector3.up;
			}
		}
	}

	public static void HoverboardGun()
	{
		if (!GunLib.GunGrips)
		{
			GunLib.SetGunVisualsVisible(isVisible: false);
			return;
		}
		GunLib.UpdateGunRaycast();
		if (GunLib.GunTriggers && Time.time > ServerSideEquipRoutine_StateMachine101_Value_07)
		{
			ServerSideEquipRoutine_StateMachine101_Value_07 = Time.time + 0.25f;
			DropHoverboard(((RaycastHit)GunLib.GunLib_Reference_07).point + Vector3.up, Variables.RandomRotation(), Vector3.zero, Vector3.zero, Variables.RandomColor());
		}
	}

	public static void SetPlaceBombEnabled(bool active)
	{
		if (active)
		{
			if (InputHandler.IsRightGripPressed())
			{
				if ((Object)(object)ServerSideEquipRoutine_StateMachine101_Object_06 == (Object)null)
				{
					ServerSideEquipRoutine_StateMachine101_Object_06 = GameObject.CreatePrimitive((PrimitiveType)3);
					ServerSideEquipRoutine_StateMachine101_Object_06.transform.localScale = Vector3.one * 0.2f;
					ServerSideEquipRoutine_StateMachine101_Object_06.GetComponent<Renderer>().material.color = Color.red;
					Object.Destroy((Object)(object)ServerSideEquipRoutine_StateMachine101_Object_06.GetComponent<BoxCollider>());
					ServerSideEquipRoutine_StateMachine101_Object_06.transform.position = Variables.Variables_Reference_06.RightHand.controllerTransform.position;
				}
				else
				{
					ServerSideEquipRoutine_StateMachine101_Object_06.transform.position = Variables.Variables_Reference_06.RightHand.controllerTransform.position;
				}
			}
			else if (InputHandler.IsRightTriggerPressed() && (Object)(object)ServerSideEquipRoutine_StateMachine101_Object_06 != (Object)null)
			{
				((Collider)Variables.Variables_Reference_06.bodyCollider).attachedRigidbody.AddExplosionForce(50000f, ServerSideEquipRoutine_StateMachine101_Object_06.transform.position, 5f);
				Object.Destroy((Object)(object)ServerSideEquipRoutine_StateMachine101_Object_06);
				ServerSideEquipRoutine_StateMachine101_Object_06 = null;
			}
		}
		else if ((Object)(object)ServerSideEquipRoutine_StateMachine101_Object_06 != (Object)null)
		{
			Object.Destroy((Object)(object)ServerSideEquipRoutine_StateMachine101_Object_06);
			ServerSideEquipRoutine_StateMachine101_Object_06 = null;
		}
	}

	public static void ConfigureWater(bool solid, bool transparent)
	{
		if (ServerSideEquipRoutine_StateMachine101_Values_01 == null)
		{
			ServerSideEquipRoutine_StateMachine101_Values_01 = Object.FindObjectsOfType<WaterVolume>();
			if (!solid)
			{
				goto Branch_003e;
			}
		}
		else if (!solid)
		{
			goto Branch_003e;
		}
		int num = LayerMask.NameToLayer("Default");
		if (num != -1)
		{
			goto Branch_00c1;
		}
		return;
		Branch_00c1:
		WaterVolume[] cUPUW3TB = ServerSideEquipRoutine_StateMachine101_Values_01;
		int num2 = 0;
		while (num2 < cUPUW3TB.Length)
		{
			WaterVolume val = cUPUW3TB[num2];
			if ((Object)(object)val != (Object)null)
			{
				((Component)val).gameObject.layer = num;
				num2++;
			}
			else
			{
				num2++;
			}
		}
		return;
		Branch_003e:
		if (!transparent)
		{
			num = LayerMask.NameToLayer("Water");
			if (num == -1)
			{
				return;
			}
		}
		else
		{
			num = LayerMask.NameToLayer("TransparentFX");
			if (num == -1)
			{
				return;
			}
		}
		goto Branch_00c1;
	}

	public static void BecomeHoverboard()
	{
		Vector3 position = ((Component)Variables.Variables_Reference_09.bodyCollider).transform.position;
		Quaternion rotation = ((Component)Variables.Variables_Reference_09.headCollider).transform.rotation;
		((Behaviour)VRRig.LocalRig).enabled = false;
		((Component)VRRig.LocalRig).transform.position = position - Vector3.up;
		GTPlayer.Instance.SetHoverAllowed(true, false);
		GTPlayer.Instance.SetHoverActive(true);
		Transform nominalParentTransform = ReflectionCompat.GetField<Transform>(VRRig.LocalRig.hoverboardVisual, "NominalParentTransform");
		HoverboardVisual val = VRRig.LocalRig.hoverboardVisual;
		val.SetIsHeld(true, nominalParentTransform.InverseTransformPoint(position), GTExt.InverseTransformRotation(nominalParentTransform, rotation), VRRig.LocalRig.playerColor);
		ReflectionCompat.SetField(val, "interpolatedLocalPosition", val.NominalLocalPosition);
		ReflectionCompat.SetField(val, "interpolatedLocalRotation", val.NominalLocalRotation);
		GTPlayer.Instance.SetHoverboardPosRot(position, rotation);
	}

	public static void SetWebShootersEnabled(bool active)
	{
		Color startColor;
		float startWidth;
		if (!active)
		{
			DestroySpiderWebObjects();
		}
		else if (!ServerSideEquipRoutine_StateMachine101_State_13)
		{
			if ((Object)(object)ServerSideEquipRoutine_StateMachine101_Reference_04 == (Object)null)
			{
				ServerSideEquipRoutine_StateMachine101_Reference_04 = new GameObject("RightWebLine").AddComponent<LineRenderer>();
				((Renderer)ServerSideEquipRoutine_StateMachine101_Reference_04).material = new Material(Shader.Find("Sprites/Default"));
				LineRenderer tSJ02X7C = ServerSideEquipRoutine_StateMachine101_Reference_04;
				startColor = (ServerSideEquipRoutine_StateMachine101_Reference_04.endColor = Color.white);
				tSJ02X7C.startColor = startColor;
				LineRenderer tSJ02X7C2 = ServerSideEquipRoutine_StateMachine101_Reference_04;
				startWidth = (ServerSideEquipRoutine_StateMachine101_Reference_04.endWidth = 0.02f);
				tSJ02X7C2.startWidth = startWidth;
				ServerSideEquipRoutine_StateMachine101_Reference_04.positionCount = 0;
				((Component)ServerSideEquipRoutine_StateMachine101_Reference_04).transform.SetParent(((Component)Variables.Variables_Reference_06).transform);
				if ((Object)(object)ServerSideEquipRoutine_StateMachine101_Reference_02 == (Object)null)
				{
					goto Branch_0125;
				}
			}
			else if ((Object)(object)ServerSideEquipRoutine_StateMachine101_Reference_02 == (Object)null)
			{
				goto Branch_0125;
			}
			ServerSideEquipRoutine_StateMachine101_State_13 = true;
			UpdateSpiderWebHand(ref ServerSideEquipRoutine_StateMachine101_Index_04, ref ServerSideEquipRoutine_StateMachine101_Reference_04, ref ServerSideEquipRoutine_StateMachine101_Object_10, ref ServerSideEquipRoutine_StateMachine101_State_11, Variables.Variables_Reference_06.RightHand.controllerTransform, ((ControllerInputPoller)ControllerInputPoller.instance).rightControllerIndexFloat, (Controller)2, isRight: false);
			UpdateSpiderWebHand(ref ServerSideEquipRoutine_StateMachine101_Index_03, ref ServerSideEquipRoutine_StateMachine101_Reference_02, ref ServerSideEquipRoutine_StateMachine101_Object_04, ref ServerSideEquipRoutine_StateMachine101_State_03, Variables.Variables_Reference_06.LeftHand.controllerTransform, ((ControllerInputPoller)ControllerInputPoller.instance).leftControllerIndexFloat, (Controller)1, isRight: true);
		}
		else
		{
			UpdateSpiderWebHand(ref ServerSideEquipRoutine_StateMachine101_Index_04, ref ServerSideEquipRoutine_StateMachine101_Reference_04, ref ServerSideEquipRoutine_StateMachine101_Object_10, ref ServerSideEquipRoutine_StateMachine101_State_11, Variables.Variables_Reference_06.RightHand.controllerTransform, ((ControllerInputPoller)ControllerInputPoller.instance).rightControllerIndexFloat, (Controller)2, isRight: false);
			UpdateSpiderWebHand(ref ServerSideEquipRoutine_StateMachine101_Index_03, ref ServerSideEquipRoutine_StateMachine101_Reference_02, ref ServerSideEquipRoutine_StateMachine101_Object_04, ref ServerSideEquipRoutine_StateMachine101_State_03, Variables.Variables_Reference_06.LeftHand.controllerTransform, ((ControllerInputPoller)ControllerInputPoller.instance).leftControllerIndexFloat, (Controller)1, isRight: true);
		}
		return;
		Branch_0125:
		ServerSideEquipRoutine_StateMachine101_Reference_02 = new GameObject("LeftWebLine").AddComponent<LineRenderer>();
		((Renderer)ServerSideEquipRoutine_StateMachine101_Reference_02).material = new Material(Shader.Find("Sprites/Default"));
		LineRenderer cEZM0C6L = ServerSideEquipRoutine_StateMachine101_Reference_02;
		startColor = (ServerSideEquipRoutine_StateMachine101_Reference_02.endColor = Color.white);
		cEZM0C6L.startColor = startColor;
		LineRenderer cEZM0C6L2 = ServerSideEquipRoutine_StateMachine101_Reference_02;
		startWidth = (ServerSideEquipRoutine_StateMachine101_Reference_02.endWidth = 0.02f);
		cEZM0C6L2.startWidth = startWidth;
		ServerSideEquipRoutine_StateMachine101_Reference_02.positionCount = 0;
		((Component)ServerSideEquipRoutine_StateMachine101_Reference_02).transform.SetParent(((Component)Variables.Variables_Reference_06).transform);
		ServerSideEquipRoutine_StateMachine101_State_13 = true;
		UpdateSpiderWebHand(ref ServerSideEquipRoutine_StateMachine101_Index_04, ref ServerSideEquipRoutine_StateMachine101_Reference_04, ref ServerSideEquipRoutine_StateMachine101_Object_10, ref ServerSideEquipRoutine_StateMachine101_State_11, Variables.Variables_Reference_06.RightHand.controllerTransform, ((ControllerInputPoller)ControllerInputPoller.instance).rightControllerIndexFloat, (Controller)2, isRight: false);
		UpdateSpiderWebHand(ref ServerSideEquipRoutine_StateMachine101_Index_03, ref ServerSideEquipRoutine_StateMachine101_Reference_02, ref ServerSideEquipRoutine_StateMachine101_Object_04, ref ServerSideEquipRoutine_StateMachine101_State_03, Variables.Variables_Reference_06.LeftHand.controllerTransform, ((ControllerInputPoller)ControllerInputPoller.instance).leftControllerIndexFloat, (Controller)1, isRight: true);
	}

	public static void AttachHoverboardToRig(VRRig rig, Color color)
	{
		HoverboardVisual val3 = VRRig.LocalRig.hoverboardVisual;
		if (ServerSideEquipRoutine_StateMachine101_Routine_02 != null)
		{
			((MonoBehaviour)CoroutineHelper.Instance).StopCoroutine(ServerSideEquipRoutine_StateMachine101_Routine_02);
			ServerSideEquipRoutine_StateMachine101_Routine_02 = ((MonoBehaviour)CoroutineHelper.Instance).StartCoroutine(ReleaseHoverboardDelayed());
			Vector3 angularVelocity = GTExt.GetOrAddComponent<GorillaVelocityEstimator>(rig.headMesh).angularVelocity;
			Vector3 val = rig.headMesh.transform.TransformPoint(-0.3f, 0.1f, 0.3725f) + rig.LatestVelocity() * 0.5f;
			Quaternion val2 = rig.headMesh.transform.rotation * Quaternion.Euler(angularVelocity * (18f / MathF.PI)) * Quaternion.Euler(0f, 90f, 270f);
			((Behaviour)VRRig.LocalRig).enabled = false;
			((Component)VRRig.LocalRig).transform.position = val - Vector3.up * 0.5f;
			GTPlayer.Instance.SetHoverAllowed(true, false);
			Transform nominalParentTransform = ReflectionCompat.GetField<Transform>(VRRig.LocalRig.hoverboardVisual, "NominalParentTransform");
			val3.SetIsHeld(true, nominalParentTransform.InverseTransformPoint(val), GTExt.InverseTransformRotation(nominalParentTransform, val2), color);
			GTPlayer.Instance.SetHoverActive(false);
			ReflectionCompat.SetField(val3, "interpolatedLocalPosition", val3.NominalLocalPosition);
			ReflectionCompat.SetField(val3, "interpolatedLocalRotation", val3.NominalLocalRotation);
			GTPlayer.Instance.SetHoverboardPosRot(val, val2);
		}
		else
		{
			ServerSideEquipRoutine_StateMachine101_Routine_02 = ((MonoBehaviour)CoroutineHelper.Instance).StartCoroutine(ReleaseHoverboardDelayed());
			Vector3 angularVelocity = GTExt.GetOrAddComponent<GorillaVelocityEstimator>(rig.headMesh).angularVelocity;
			Vector3 val = rig.headMesh.transform.TransformPoint(-0.3f, 0.1f, 0.3725f) + rig.LatestVelocity() * 0.5f;
			Quaternion val2 = rig.headMesh.transform.rotation * Quaternion.Euler(angularVelocity * (18f / MathF.PI)) * Quaternion.Euler(0f, 90f, 270f);
			((Behaviour)VRRig.LocalRig).enabled = false;
			((Component)VRRig.LocalRig).transform.position = val - Vector3.up * 0.5f;
			GTPlayer.Instance.SetHoverAllowed(true, false);
			Transform nominalParentTransform = ReflectionCompat.GetField<Transform>(VRRig.LocalRig.hoverboardVisual, "NominalParentTransform");
			val3.SetIsHeld(true, nominalParentTransform.InverseTransformPoint(val), GTExt.InverseTransformRotation(nominalParentTransform, val2), color);
			GTPlayer.Instance.SetHoverActive(false);
			ReflectionCompat.SetField(val3, "interpolatedLocalPosition", val3.NominalLocalPosition);
			ReflectionCompat.SetField(val3, "interpolatedLocalRotation", val3.NominalLocalRotation);
			GTPlayer.Instance.SetHoverboardPosRot(val, val2);
		}
	}

	public Fun()
	{
	}

	public static void HoverboardAura()
	{
		if (Time.time < ServerSideEquipRoutine_StateMachine101_Value_01)
		{
			return;
		}
		ServerSideEquipRoutine_StateMachine101_Value_01 = Time.time + 0.25f;
		int num = 0;
		if (num < 2)
		{
			do
			{
				DropHoverboard(((Component)Variables.Variables_Reference_09.headCollider).transform.position + Variables.RandomPosition(), Variables.RandomRotation(), Variables.RandomPosition(20f), Variables.RandomPosition(20f), Variables.RandomColor());
				num++;
			}
			while (num < 2);
		}
	}

	public static void SetDrawModEnabled(bool active)
	{
		if (active)
		{
			if ((Object)(object)ServerSideEquipRoutine_StateMachine101_Object_02 == (Object)null)
			{
				ServerSideEquipRoutine_StateMachine101_Object_02 = GameObject.CreatePrimitive((PrimitiveType)0);
				ServerSideEquipRoutine_StateMachine101_Object_02.transform.localScale = Vector3.one * 0.1f;
				Object.Destroy((Object)(object)ServerSideEquipRoutine_StateMachine101_Object_02.GetComponent<Rigidbody>());
				Object.Destroy((Object)(object)ServerSideEquipRoutine_StateMachine101_Object_02.GetComponent<SphereCollider>());
				if ((Object)(object)ServerSideEquipRoutine_StateMachine101_Object_05 == (Object)null)
				{
					goto Branch_00b9;
				}
			}
			else if ((Object)(object)ServerSideEquipRoutine_StateMachine101_Object_05 == (Object)null)
			{
				goto Branch_00b9;
			}
			ServerSideEquipRoutine_StateMachine101_Object_02.transform.position = Variables.Variables_Reference_06.RightHand.controllerTransform.position;
			ServerSideEquipRoutine_StateMachine101_Object_05.transform.position = Variables.Variables_Reference_06.LeftHand.controllerTransform.position;
			ServerSideEquipRoutine_StateMachine101_Object_02.GetComponent<Renderer>().material.color = ServerSideEquipRoutine_StateMachine101_Color_01;
			ServerSideEquipRoutine_StateMachine101_Object_05.GetComponent<Renderer>().material.color = ServerSideEquipRoutine_StateMachine101_Color_01;
			if (!InputHandler.IsRightGripPressed())
			{
				goto Branch_02b0;
			}
			goto Branch_0222;
		}
		if ((Object)(object)ServerSideEquipRoutine_StateMachine101_Object_02 != (Object)null)
		{
			Object.Destroy((Object)(object)ServerSideEquipRoutine_StateMachine101_Object_02);
			ServerSideEquipRoutine_StateMachine101_Object_02 = null;
			if ((Object)(object)ServerSideEquipRoutine_StateMachine101_Object_05 != (Object)null)
			{
				goto Branch_04ae;
			}
		}
		else if ((Object)(object)ServerSideEquipRoutine_StateMachine101_Object_05 != (Object)null)
		{
			goto Branch_04ae;
		}
		ClearDrawObjects();
		return;
		Branch_0370:
		if (ServerSideEquipRoutine_StateMachine101_State_09)
		{
			goto Branch_040b;
		}
		ServerSideEquipRoutine_StateMachine101_Index_06 = (ServerSideEquipRoutine_StateMachine101_Index_06 + 1) % 13;
		int num = ServerSideEquipRoutine_StateMachine101_Index_06;
		int num2 = num;
		num2 = (((uint)num2 <= 12u) ? num2 : 13) + 501;
		int num3 = num2;
		Color eDVHTSLS = ((num3 == 502) ? Color.blue : Color.white);
		ServerSideEquipRoutine_StateMachine101_Color_01 = eDVHTSLS;
		ServerSideEquipRoutine_StateMachine101_State_09 = true;
		return;
		Branch_0357:
		if (!InputHandler.IsRightPrimaryPressed())
		{
			goto Branch_040b;
		}
		goto Branch_0370;
		Branch_04ae:
		Object.Destroy((Object)(object)ServerSideEquipRoutine_StateMachine101_Object_05);
		ServerSideEquipRoutine_StateMachine101_Object_05 = null;
		ClearDrawObjects();
		return;
		Branch_00b9:
		ServerSideEquipRoutine_StateMachine101_Object_05 = GameObject.CreatePrimitive((PrimitiveType)0);
		ServerSideEquipRoutine_StateMachine101_Object_05.transform.localScale = Vector3.one * 0.1f;
		Object.Destroy((Object)(object)ServerSideEquipRoutine_StateMachine101_Object_05.GetComponent<Rigidbody>());
		Object.Destroy((Object)(object)ServerSideEquipRoutine_StateMachine101_Object_05.GetComponent<SphereCollider>());
		ServerSideEquipRoutine_StateMachine101_Object_02.transform.position = Variables.Variables_Reference_06.RightHand.controllerTransform.position;
		ServerSideEquipRoutine_StateMachine101_Object_05.transform.position = Variables.Variables_Reference_06.LeftHand.controllerTransform.position;
		ServerSideEquipRoutine_StateMachine101_Object_02.GetComponent<Renderer>().material.color = ServerSideEquipRoutine_StateMachine101_Color_01;
		ServerSideEquipRoutine_StateMachine101_Object_05.GetComponent<Renderer>().material.color = ServerSideEquipRoutine_StateMachine101_Color_01;
		if (!InputHandler.IsRightGripPressed())
		{
			goto Branch_02b0;
		}
		goto Branch_0222;
		Branch_040b:
		if (!InputHandler.IsRightPrimaryPressed())
		{
			ServerSideEquipRoutine_StateMachine101_State_09 = false;
		}
		return;
		Branch_0222:
		GameObject val = GameObject.CreatePrimitive((PrimitiveType)0);
		val.transform.localScale = Vector3.one * 0.1f;
		val.transform.position = Variables.Variables_Reference_06.RightHand.controllerTransform.position;
		val.GetComponent<Renderer>().material.color = ServerSideEquipRoutine_StateMachine101_Color_01;
		Object.Destroy((Object)(object)val.GetComponent<Rigidbody>());
		Object.Destroy((Object)(object)val.GetComponent<SphereCollider>());
		ServerSideEquipRoutine_StateMachine101_Object_01.Add(val);
		if (!InputHandler.IsLeftGripPressed())
		{
			goto Branch_0357;
		}
		goto Branch_02c9;
		Branch_02b0:
		if (!InputHandler.IsLeftGripPressed())
		{
			goto Branch_0357;
		}
		Branch_02c9:
		GameObject val2 = GameObject.CreatePrimitive((PrimitiveType)0);
		val2.transform.localScale = Vector3.one * 0.1f;
		val2.transform.position = Variables.Variables_Reference_06.LeftHand.controllerTransform.position;
		val2.GetComponent<Renderer>().material.color = ServerSideEquipRoutine_StateMachine101_Color_01;
		Object.Destroy((Object)(object)val2.GetComponent<Rigidbody>());
		Object.Destroy((Object)(object)val2.GetComponent<SphereCollider>());
		ServerSideEquipRoutine_StateMachine101_Object_01.Add(val2);
		if (!InputHandler.IsRightPrimaryPressed())
		{
			goto Branch_040b;
		}
		goto Branch_0370;
	}

	public static void GliderAura()
	{
		GliderHoldable[] array = Variables.FindObjectsCached<GliderHoldable>(false);
		int num = 0;
		while (num < array.Length)
		{
			GliderHoldable val = array[num];
			if (((NetworkView)array[num]).GetView.Owner == PhotonNetwork.LocalPlayer)
			{
				((Component)val).gameObject.transform.position = ((Component)Variables.Variables_Reference_09.headCollider).transform.position + Variables.RandomPosition();
				((Component)val).gameObject.transform.rotation = Variables.RandomRotation();
				num++;
			}
			else
			{
				((NetworkHoldableObject)val).OnHover((InteractionPoint)null, (GameObject)null);
				num++;
			}
		}
	}

	public static void DropHoverboard(Vector3 pos, Quaternion rot, Vector3 vel, Vector3 angVel, Color color)
	{
		if (Vector3.Distance(((Component)Variables.Variables_Reference_09.bodyCollider).transform.position, pos) > 5f)
		{
			((Behaviour)VRRig.LocalRig).enabled = false;
			((Component)VRRig.LocalRig).transform.position = pos + Vector3.down * 4f;
			if (ServerSideEquipRoutine_StateMachine101_Routine_02 != null)
			{
				((MonoBehaviour)CoroutineHelper.Instance).StopCoroutine(ServerSideEquipRoutine_StateMachine101_Routine_02);
				ServerSideEquipRoutine_StateMachine101_Routine_02 = ((MonoBehaviour)CoroutineHelper.Instance).StartCoroutine(RestoreLocalRigDelayed());
				FreeHoverboardManager.instance.SendDropBoardRPC(pos, rot, vel, angVel, color);
				Safety.ResetNetworkLimits();
			}
			else
			{
				ServerSideEquipRoutine_StateMachine101_Routine_02 = ((MonoBehaviour)CoroutineHelper.Instance).StartCoroutine(RestoreLocalRigDelayed());
				FreeHoverboardManager.instance.SendDropBoardRPC(pos, rot, vel, angVel, color);
				Safety.ResetNetworkLimits();
			}
		}
		else
		{
			FreeHoverboardManager.instance.SendDropBoardRPC(pos, rot, vel, angVel, color);
			Safety.ResetNetworkLimits();
		}
	}

	public static void SplashAnnoyGun()
	{
		if (GunLib.TrySelectRig())
		{
			if (!(Time.time < ServerSideEquipRoutine_StateMachine101_Value_08))
			{
				ServerSideEquipRoutine_StateMachine101_Value_08 = Time.time + ServerSideEquipRoutine_StateMachine101_Value_02;
				((Behaviour)VRRig.LocalRig).enabled = false;
				((Component)VRRig.LocalRig).transform.position = ((Component)GunLib.GunLib_Reference_06).transform.position;
				PlaySplashEffect(GunLib.GunLib_Reference_06.headMesh.transform.position + GunLib.GunLib_Reference_06.headMesh.transform.forward * 0.2f, GunLib.GunLib_Reference_06.headMesh.transform.rotation, 999f);
				((Behaviour)VRRig.LocalRig).enabled = true;
				Safety.ResetNetworkLimits();
			}
		}
		else
		{
			((Behaviour)VRRig.LocalRig).enabled = true;
		}
	}

	public static void SplashGun()
	{
		if (GunLib.IsGunTriggerPressed())
		{
			if (!(Time.time < ServerSideEquipRoutine_StateMachine101_Value_08))
			{
				ServerSideEquipRoutine_StateMachine101_Value_08 = Time.time + ServerSideEquipRoutine_StateMachine101_Value_02;
				((Behaviour)VRRig.LocalRig).enabled = false;
				((Component)VRRig.LocalRig).transform.position = ((RaycastHit)GunLib.GunLib_Reference_07).point - Vector3.up * 2f;
				PlaySplashEffect(((RaycastHit)GunLib.GunLib_Reference_07).point, (((RaycastHit)GunLib.GunLib_Reference_07).normal != Vector3.zero) ? Quaternion.LookRotation(((RaycastHit)GunLib.GunLib_Reference_07).normal) : Quaternion.identity, 125f);
				((Behaviour)VRRig.LocalRig).enabled = true;
				Safety.ResetNetworkLimits();
			}
		}
		else
		{
			((Behaviour)VRRig.LocalRig).enabled = true;
		}
	}

	public static void AlternateNonCosmeticHandItem(bool state)
	{
		if (InputHandler.IsLeftGripPressed())
		{
			SetNonCosmeticHandItemEnabled(enable: false, left: false);
			SetNonCosmeticHandItemEnabled(state, left: true);
			if (InputHandler.IsRightGripPressed())
			{
				goto Branch_0059;
			}
		}
		else if (InputHandler.IsRightGripPressed())
		{
			goto Branch_0059;
		}
		if (!InputHandler.IsLeftGripPressed())
		{
			goto Branch_008f;
		}
		Branch_00bc:
		Safety.ResetNetworkLimits();
		return;
		Branch_008f:
		if (!InputHandler.IsRightGripPressed())
		{
			return;
		}
		goto Branch_00bc;
		Branch_0059:
		SetNonCosmeticHandItemEnabled(state, left: false);
		SetNonCosmeticHandItemEnabled(enable: false, left: true);
		if (!InputHandler.IsLeftGripPressed())
		{
			goto Branch_008f;
		}
		goto Branch_00bc;
	}

	private static void ClearDrawObjects()
	{
		using (HashSet<GameObject>.Enumerator enumerator = ServerSideEquipRoutine_StateMachine101_Object_01.GetEnumerator())
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
						goto EndBranch_005e;
					}
				}
				continue;
				EndBranch_005e:
				break;
			}
		}
		ServerSideEquipRoutine_StateMachine101_Object_01.Clear();
	}

	public static void SetBugSpeed(string name, float speed)
	{
		GameObject val = GameObject.Find(name);
		if (!((Object)(object)val == (Object)null))
		{
			ThrowableBug component = val.GetComponent<ThrowableBug>();
			if (!((Object)(object)component == (Object)null))
			{
				component.maxNaturalSpeed = speed;
			}
		}
	}

	public static void SetMuteElevatorEnabled(bool enable)
	{
		GameObject[] array;
		int num;
		if (ServerSideEquipRoutine_StateMachine101_Object_08 == null)
		{
			ServerSideEquipRoutine_StateMachine101_Object_08 = (GameObject[])(object)new GameObject[3]
			{
				GameObject.Find("Environment Objects/05Maze_PersistentObjects/GhostReactorElevatorManager/GhostReactorElevator/HasVisualComponents/Audio/Ambient"),
				GameObject.Find("Environment Objects/05Maze_PersistentObjects/GhostReactorElevatorManager/CityElevator/HasVisualComponents/Audio/Ambient"),
				GameObject.Find("Environment Objects/05Maze_PersistentObjects/GhostReactorElevatorManager/StumpElevator/HasVisualComponents/Audio/Ambient")
			};
			array = ServerSideEquipRoutine_StateMachine101_Object_08;
			num = 0;
		}
		else
		{
			array = ServerSideEquipRoutine_StateMachine101_Object_08;
			num = 0;
		}
		while (num < array.Length)
		{
			GameObject val = array[num];
			if ((Object)(object)val != (Object)null)
			{
				val.SetActive(enable);
				num++;
			}
			else
			{
				num++;
			}
		}
	}

	public static void HoldCachedObject(string name)
	{
		if (InputHandler.IsRightGripPressed())
		{
			Variables.FindCachedGameObject(name).transform.position = Variables.Variables_Reference_09.rightHandTransform.position;
		}
	}

    public static void StartServerSideEquip()
    {
        if (isEquippingCosmetic ||
            GorillaTagger.Instance == null ||
            GorillaTagger.Instance.bodyCollider == null)
        {
            return;
        }

        isEquippingCosmetic = true;
        GorillaTagger.Instance.StartCoroutine(ServerSideEquipRoutine());
    }


    private static bool isEquippingCosmetic;
    private static bool isCustomCosmeticSetApplied;
    private static int[] savedOriginalCosmeticSet;
    private static int[] customCosmeticSet;

    public static void EquipCustomCosmeticSet(bool enable)
    {
        if (enable == isCustomCosmeticSetApplied ||
            CosmeticsController.instance == null ||
            GTPlayer.Instance == null ||
            GorillaTagger.Instance.myVRRig == null)
        {
            return;
        }

        isCustomCosmeticSetApplied = enable;
        int[] packedSet;

        if (enable)
        {
            savedOriginalCosmeticSet = CosmeticsController.instance.currentWornSet.ToPackedIDArray();
            if (customCosmeticSet == null)
            {
                customCosmeticSet = PackCosmeticStrings(Enumerable.Repeat("LMAJU.", 16).ToArray());
            }
            packedSet = customCosmeticSet;
        }
        else
        {
            packedSet = savedOriginalCosmeticSet;
        }

        if (packedSet == null) return;

        CosmeticsController.CosmeticSet newSet = new CosmeticsController.CosmeticSet(packedSet, CosmeticsController.instance);
        CosmeticsController.instance.currentWornSet = newSet;
        VRRig.LocalRig.cosmeticSet = newSet;

        GorillaTagger.Instance.myVRRig.SendRPC("RPC_UpdateCosmeticsWithTryonPacked", RpcTarget.All, new object[]
        {
                packedSet,
                CosmeticsController.instance.tryOnSet.ToPackedIDArray(),
                false
        });
    }

    public static int[] PackCosmeticStrings(string[] cosmetics)
    {
        if (CosmeticsController.instance == null) return Array.Empty<int>();
        return new CosmeticsController.CosmeticSet(cosmetics, CosmeticsController.instance).ToPackedIDArray();
    }

    private static readonly Vector3 CosmeticEquipPosition = new Vector3(-51.7836f, 16.7865f, -119.6697f);

    private static IEnumerator ServerSideEquipRoutine()
    {
        try
        {
            if (GTPlayer.Instance == null || GTPlayer.Instance.bodyCollider == null)
                yield break;

            Vector3 origin = GTPlayer.Instance.bodyCollider.transform.position;
            GTZone[] savedZones = null;

            if (ZoneManagement.instance != null && !ZoneManagement.IsInZone(GTZone.city))
            {
                savedZones = ZoneManagement.instance.activeZones.ToArray();
                List<GTZone> withCity = new List<GTZone>(ZoneManagement.instance.activeZones) { GTZone.city };
                ZoneManagement.SetActiveZones(withCity.ToArray());

                float timeout = Time.time + 5f;
                while (!ZoneManagement.IsZoneLoaded(GTZone.city) && Time.time < timeout)
                {
                    yield return null;
                }
                yield return new WaitForSeconds(0.5f);
            }

            TeleportTo(CosmeticEquipPosition);
            yield return new WaitForSeconds(1f);

            isCustomCosmeticSetApplied = false;
            EquipCustomCosmeticSet(true);
            yield return new WaitForSeconds(0.5f);
			
            TeleportTo(origin);
            if (savedZones != null && ZoneManagement.instance != null)
            {
                ZoneManagement.SetActiveZones(savedZones);
            }
        }
        finally
        {
            isEquippingCosmetic = false;
        }
    }

    public static void TeleportTo(Vector3 position)
    {
        if (GorillaTagger.Instance == null ||
            GorillaTagger.Instance.bodyCollider == null ||
            GTPlayer.Instance == null)
        {
            return;
        }

        Vector3 teleportPosition =
            position -
            GorillaTagger.Instance.bodyCollider.transform.position +
            GorillaTagger.Instance.transform.position;

        GTPlayer.Instance.TeleportTo(
            teleportPosition,
            GTPlayer.Instance.transform.rotation,
            false,
            false);
    }


    public static void AddCosmeticToCart()
	{
		((CosmeticsController)CosmeticsController.instance).currentCart.Insert(0, ServerSideEquipRoutine_StateMachine101_Reference_05);
		((CosmeticsController)CosmeticsController.instance).UpdateShoppingCart();
	}

	public static void DestroyGliders()
	{
		GliderHoldable[] array = Variables.FindObjectsCached<GliderHoldable>(false);
		int num = 0;
		while (num < array.Length)
		{
			GliderHoldable val = array[num];
			if (((NetworkView)array[num]).GetView.Owner == PhotonNetwork.LocalPlayer)
			{
				((Component)val).gameObject.transform.position = new Vector3(99999f, 99999f, 99999f);
				num++;
			}
			else
			{
				((NetworkHoldableObject)val).OnHover((InteractionPoint)null, (GameObject)null);
				num++;
			}
		}
	}

	public static void SetDisableWindBarriersEnabled(bool enable)
	{
		MenuPatches.GrabPatch_State_05 = enable;
		Variables.FindCachedGameObject("Environment Objects/LocalObjects_Prefab/Forest/Environment/Forest_ForceVolumes/").SetActive(!enable);
		Variables.FindCachedGameObject("Environment Objects/LocalObjects_Prefab/ForestToHoverboard/TurnOnInForestAndHoverboard/ForestDome_CollisionOnly").SetActive(!enable);
	}

	private static void UpdateSpiderWebHand(ref SpringJoint jnt, ref LineRenderer line, ref GameObject aim, ref bool canGrap, Transform hand, float trigger, OVRInput.Controller ctrl, bool isRight)
	{
		RaycastHit val = default(RaycastHit);
		bool hasHit = Physics.Raycast(hand.position, hand.forward, out val, 100f);
		if (hasHit)
		{
			if ((Object)(object)aim == (Object)null)
			{
				aim = GameObject.CreatePrimitive((PrimitiveType)0);
				Object.Destroy((Object)(object)aim.GetComponent<Rigidbody>());
				Object.Destroy((Object)(object)aim.GetComponent<SphereCollider>());
				aim.transform.localScale = Vector3.one * 0.2f;
				aim.GetComponent<Renderer>().material.color = Color.green;
				aim.SetActive(true);
				aim.transform.position = ((RaycastHit)val).point;
				if (trigger > 0.1f)
				{
					goto Branch_0128;
				}
			}
			else
			{
				aim.SetActive(true);
				aim.transform.position = ((RaycastHit)val).point;
				if (trigger > 0.1f)
				{
					goto Branch_0128;
				}
			}
		}
		else if (trigger > 0.1f)
		{
			goto Branch_0128;
		}
		if ((Object)(object)jnt != (Object)null)
		{
			Object.Destroy((Object)(object)jnt);
			jnt = null;
			line.positionCount = 0;
			canGrap = true;
		}
		else
		{
			line.positionCount = 0;
			canGrap = true;
		}
		return;
		Branch_0128:
		if (canGrap && hasHit)
		{
			jnt = ((Component)Variables.Variables_Reference_06).gameObject.AddComponent<SpringJoint>();
			((Joint)jnt).autoConfigureConnectedAnchor = false;
			((Joint)jnt).connectedAnchor = ((RaycastHit)val).point;
			float num = Vector3.Distance(((Component)Variables.Variables_Reference_06).transform.position, ((RaycastHit)val).point);
			jnt.maxDistance = num * 0.8f;
			jnt.minDistance = num * 0.25f;
			jnt.spring = 5000f;
			jnt.damper = 4000f;
			((Joint)jnt).massScale = 6f;
			line.positionCount = 2;
			line.SetPosition(1, ((RaycastHit)val).point);
			Variables.Variables_Reference_09.offlineVRRig.PlayHandTapLocal(82, isRight, 1f);
			canGrap = false;
			if (!((Object)(object)jnt != (Object)null))
			{
				return;
			}
		}
		else if (!((Object)(object)jnt != (Object)null))
		{
			return;
		}
		line.positionCount = 2;
		line.SetPosition(0, hand.position);
		line.SetPosition(1, ((Joint)jnt).connectedAnchor);
		Vector3 val2;
		if ((Object)(object)aim != (Object)null)
		{
			aim.transform.position = ((Joint)jnt).connectedAnchor;
			val2 = OVRInput.GetLocalControllerVelocity(ctrl);
			if (!(((Vector3)val2).magnitude >= 2.5f))
			{
				return;
			}
		}
		else
		{
			val2 = OVRInput.GetLocalControllerVelocity(ctrl);
			if (!(((Vector3)val2).magnitude >= 2.5f))
			{
				return;
			}
		}
		Rigidbody component = ((Component)Variables.Variables_Reference_06).GetComponent<Rigidbody>();
		val2 = ((Joint)jnt).connectedAnchor - ((Component)Variables.Variables_Reference_06).transform.position;
		component.AddForce(((Vector3)val2).normalized * 3f, (ForceMode)2);
	}

	public static void SpazHoverboard()
	{
		if ((Object)(object)VRRig.LocalRig.hoverboardVisual != (Object)null && VRRig.LocalRig.hoverboardVisual.IsHeld)
		{
			VRRig.LocalRig.hoverboardVisual.SetIsHeld(VRRig.LocalRig.hoverboardVisual.IsLeftHanded, VRRig.LocalRig.hoverboardVisual.NominalLocalPosition, Variables.RandomRotation(), Variables.RandomColor());
		}
	}

	private static IEnumerator RestoreLocalRigDelayed()
	{
		yield return (object)new WaitForSeconds(0.3f);
		((Behaviour)VRRig.LocalRig).enabled = true;
	}

	public static void RainbowHoverboard()
	{
		if (!((Object)(object)VRRig.LocalRig.hoverboardVisual == (Object)null) && VRRig.LocalRig.hoverboardVisual.IsHeld)
		{
			VRRig.LocalRig.hoverboardVisual.SetIsHeld(VRRig.LocalRig.hoverboardVisual.IsLeftHanded, VRRig.LocalRig.hoverboardVisual.NominalLocalPosition, VRRig.LocalRig.hoverboardVisual.NominalLocalRotation, Color.HSVToRGB((float)Time.frameCount / 180f % 1f, 1f, 1f));
		}
	}

	private static IEnumerator ReleaseHoverboardDelayed()
	{
		yield return (object)new WaitForSeconds(0.3f);
		GTPlayer.Instance.SetHoverActive(false);
		VRRig.LocalRig.hoverboardVisual.SetNotHeld();
	}

	public static void ControlObjectAsBody(string name)
	{
		GameObject val = GameObject.Find(name);
		if ((Object)(object)val != (Object)null)
		{
			Player.SetLocalRigEnabled(rigStatus: false);
			((Component)VRRig.LocalRig).transform.position = ((Component)Variables.Variables_Reference_09.bodyCollider).transform.position - Vector3.up * 99999f;
			val.transform.position = ((Component)Variables.Variables_Reference_09.bodyCollider).transform.position;
			val.transform.rotation = ((Component)Variables.Variables_Reference_09.headCollider).transform.rotation;
		}
		else if (ServerSideEquipRoutine_StateMachine101_State_04)
		{
			Player.SetLocalRigEnabled(rigStatus: true);
			ServerSideEquipRoutine_StateMachine101_State_04 = (Object)(object)val != (Object)null;
			return;
		}
		ServerSideEquipRoutine_StateMachine101_State_04 = (Object)(object)val != (Object)null;
	}

	public static void SplashAnnoyAll()
	{
		if (InputHandler.IsRightTriggerPressed())
		{
			MenuPatches.SerializationPatch.SerializationPatch_State_01 = delegate
			{
				if (!PhotonNetwork.InRoom)
				{
					return true;
				}
				if (Time.time < ServerSideEquipRoutine_StateMachine101_Value_08)
				{
					return false;
				}
				ServerSideEquipRoutine_StateMachine101_Value_08 = Time.time + ServerSideEquipRoutine_StateMachine101_Value_02;
				PhotonSerializer.SerializeAllViews(exclude: true, (PhotonView[])(object)new PhotonView[1] { Variables.Variables_Reference_09.myVRRig.GetView });
				Vector3 position = ((Component)VRRig.LocalRig).transform.position;
				NetPlayer[] playerListOthers = NetworkSystem.Instance.PlayerListOthers;
				int num = 0;
				while (num < playerListOthers.Length)
				{
					NetPlayer val3 = playerListOthers[num];
					VRRig val = RigManager.FindRig(val3);
					if (!((Object)(object)val == (Object)null))
					{
						((Component)VRRig.LocalRig).transform.position = ((Component)val).transform.position;
						PhotonView getView = Variables.Variables_Reference_09.myVRRig.GetView;
						RaiseEventOptions val2 = new RaiseEventOptions();
						val2.TargetActors = new int[1] { val3.ActorNumber };
						PhotonSerializer.SerializePhotonView(getView, val2);
						Variables.Variables_Reference_09.myVRRig.SendRPC("RPC_PlaySplashEffect", val3, new object[6]
						{
							val.headMesh.transform.position + val.headMesh.transform.forward * 0.2f,
							val.headMesh.transform.rotation,
							999f,
							999f,
							true,
							true
						});
						num++;
					}
					else
					{
						num++;
					}
				}
				((Component)VRRig.LocalRig).transform.position = position;
				Safety.ResetNetworkLimits();
				return false;
			};
		}
		else
		{
			MenuPatches.SerializationPatch.SerializationPatch_State_01 = null;
		}
	}

	public static void OrbitObject(string name)
	{
		ServerSideEquipRoutine_StateMachine101_Value_04 += ServerSideEquipRoutine_StateMachine101_Value_06 * Time.deltaTime;
		float num = ServerSideEquipRoutine_StateMachine101_Value_04 * (MathF.PI / 180f);
		Variables.FindCachedGameObject(name).transform.position = ((Component)Variables.Variables_Reference_09.headCollider).transform.position + new Vector3(Mathf.Cos(num), 0f, Mathf.Sin(num)) * ServerSideEquipRoutine_StateMachine101_Value_03;
	}

	public static void LoudHandTaps()
	{
		MenuPatches.GrabPatch_State_06 = true;
		MenuPatches.GrabPatch_State_07 = true;
		MenuPatches.GrabPatch_State_03 = true;
		MenuPatches.GrabPatch_Value_01 = 99999f;
		MenuPatches.GrabPatch_Index_01 = 10;
		Variables.Variables_Reference_09.handTapVolume = 99999f;
	}

	public static void HideObject(string name)
	{
		GameObject val = GameObject.Find(name);
		if ((Object)(object)val != (Object)null)
		{
			val.transform.position = new Vector3(99999f, 99999f, 99999f);
		}
	}

	public static void SetSSCosmetiXEnabled(bool enable)
	{
		ServerSideEquipRoutine_StateMachine101_State_06 = enable;
		if (!enable)
		{
			return;
		}
		CosmeticsController instance = CosmeticsController.instance;
		if ((Object)(object)instance == (Object)null || instance.allCosmetics == null || instance.unlockedCosmetics == null)
		{
			return;
		}
		HashSet<string> hashSet = new HashSet<string>();
		using (List<CosmeticItem>.Enumerator enumerator = instance.unlockedCosmetics.GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				do
				{
					CosmeticItem current = enumerator.Current;
					hashSet.Add(current.itemName);
				}
				while (enumerator.MoveNext());
			}
		}
		using (List<CosmeticItem>.Enumerator enumerator2 = instance.allCosmetics.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				while (true)
				{
					CosmeticItem current2 = enumerator2.Current;
					if (current2.isNullItem || !current2.canTryOn || string.IsNullOrEmpty(current2.itemName) || !hashSet.Add(current2.itemName))
					{
						break;
					}
					instance.unlockedCosmetics.Add(current2);
					ServerSideEquipRoutine_StateMachine101_Text_02.Add(current2.itemName);
					int num = (int)current2.itemCategory - 1;
					num = (((uint)num <= 10u) ? num : 11) + 418;
					int num2 = num;
					if (num2 != 419)
					{
						instance.unlockedHats.Add(current2);
					}
					else
					{
						instance.unlockedBadges.Add(current2);
					}
					if (!enumerator2.MoveNext())
					{
						goto EndBranch_025a;
					}
				}
				continue;
				EndBranch_025a:
				break;
			}
		}
		instance.UpdateWardrobeModelsAndButtons();
	}

	public static void PunchMod()
	{
		int num = 0;
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
				Rigidbody component = ((Component)Variables.Variables_Reference_06).GetComponent<Rigidbody>();
				Vector3 position = ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position;
				Vector3 position2 = current.rightHandTransform.position;
				Vector3 position3;
				if (Vector3.Distance(position2, position) < 0.25f)
				{
					component.AddForce(Vector3.Normalize(position2 - ServerSideEquipRoutine_StateMachine101_Position_03[num]) * 9f + Vector3.up * 2f, (ForceMode)2);
					ServerSideEquipRoutine_StateMachine101_Position_03[num] = position2;
					position3 = current.leftHandTransform.position;
					if (Vector3.Distance(position3, position) < 0.25f)
					{
						goto Branch_0160;
					}
				}
				else
				{
					ServerSideEquipRoutine_StateMachine101_Position_03[num] = position2;
					position3 = current.leftHandTransform.position;
					if (Vector3.Distance(position3, position) < 0.25f)
					{
						goto Branch_0160;
					}
				}
				ServerSideEquipRoutine_StateMachine101_Position_04[num] = position3;
				num++;
				if (!enumerator.MoveNext())
				{
					return;
				}
				continue;
				Branch_0160:
				component.AddForce(Vector3.Normalize(position3 - ServerSideEquipRoutine_StateMachine101_Position_04[num]) * 9f + Vector3.up * 2f, (ForceMode)2);
				ServerSideEquipRoutine_StateMachine101_Position_04[num] = position3;
				num++;
				if (!enumerator.MoveNext())
				{
					return;
				}
			}
		}
	}

	public static IEnumerator RestoreRigDelayed()
	{
		yield return (object)new WaitForSeconds(0.2f);
		((Behaviour)VRRig.LocalRig).enabled = true;
		ServerSideEquipRoutine_StateMachine101_Routine_01 = null;
	}

	public static void GliderAnnoyGun()
	{
		if (!GunLib.TrySelectRig())
		{
			return;
		}
		GliderHoldable[] array = Variables.FindObjectsCached<GliderHoldable>(false);
		int num = 0;
		while (num < array.Length)
		{
			GliderHoldable val = array[num];
			if (((NetworkView)array[num]).GetView.Owner == PhotonNetwork.LocalPlayer)
			{
				((Component)val).gameObject.transform.position = GunLib.GunLib_Reference_06.headMesh.transform.position;
				((Component)val).gameObject.transform.rotation = Variables.RandomRotation();
				num++;
			}
			else
			{
				((NetworkHoldableObject)val).OnHover((InteractionPoint)null, (GameObject)null);
				num++;
			}
		}
	}

	public static void ApplyScreenEffectToAll(Color color)
	{
		CapturedVariables520 LocalScope2 = new CapturedVariables520();
		LocalScope2.color = color;
		if (InputHandler.IsRightTriggerPressed())
		{
			MenuPatches.SerializationPatch.SerializationPatch_State_01 = delegate
			{
				if (!PhotonNetwork.InRoom)
				{
					return true;
				}
				PhotonSerializer.SerializeAllViews(exclude: true, (PhotonView[])(object)new PhotonView[1] { Variables.Variables_Reference_09.myVRRig.GetView });
				Vector3 position = ((Component)VRRig.LocalRig).transform.position;
				NetPlayer[] playerListOthers = NetworkSystem.Instance.PlayerListOthers;
				int num = 0;
				while (num < playerListOthers.Length)
				{
					NetPlayer val3 = playerListOthers[num];
					VRRig val = RigManager.FindRig(val3);
					if (!((Object)(object)val == (Object)null))
					{
						AttachHoverboardToRig(val, LocalScope2.color);
						PhotonView getView = Variables.Variables_Reference_09.myVRRig.GetView;
						RaiseEventOptions val2 = new RaiseEventOptions();
						val2.TargetActors = new int[1] { val3.ActorNumber };
						PhotonSerializer.SerializePhotonView(getView, val2);
						num++;
					}
					else
					{
						num++;
					}
				}
				Safety.ResetNetworkLimits();
				((Behaviour)VRRig.LocalRig).enabled = true;
				((Component)VRRig.LocalRig).transform.position = position;
				return false;
			};
		}
		else
		{
			MenuPatches.SerializationPatch.SerializationPatch_State_01 = null;
		}
	}

	public static void StopBug(string name)
	{
		GameObject val = GameObject.Find(name);
		if (!((Object)(object)val == (Object)null))
		{
			ThrowableBug component = val.GetComponent<ThrowableBug>();
			if (!((Object)(object)component == (Object)null))
			{
				component.bugRotationalVelocity = Quaternion.identity;
				component.targetVelocity = Vector3.zero;
				component.thrownVeloicity = Vector3.zero;
				component.thrownYVelocity = 0f;
				component.reliableState.travelingDirection = Vector3.zero;
			}
		}
	}

	public static void PlaySplashEffect(Vector3 pos, Quaternion rot, float size)
	{
		Variables.Variables_Reference_09.myVRRig.GetView.RPC("RPC_PlaySplashEffect", (RpcTarget)0, new object[6] { pos, rot, size, size, true, true });
	}

	public static void OrbitGliders()
	{
		if (!InputHandler.IsRightTriggerPressed())
		{
			return;
		}
		GliderHoldable[] array = Variables.FindObjectsCached<GliderHoldable>(false);
		int num = 0;
		GliderHoldable[] array2 = array;
		int num2 = 0;
		while (num2 < array2.Length)
		{
			GliderHoldable val = array2[num2];
			if (((NetworkView)array2[num2]).GetView.Owner == PhotonNetwork.LocalPlayer)
			{
				float num3 = 360f / (float)array.Length * (float)num;
				((Component)val).gameObject.transform.position = ((Component)Variables.Variables_Reference_09.headCollider).transform.position + new Vector3(MathF.Cos(num3 + (float)Time.frameCount / 30f) * 5f, 2f, MathF.Sin(num3 + (float)Time.frameCount / 30f) * 5f);
				num++;
				num2++;
			}
			else
			{
				((NetworkHoldableObject)val).OnHover((InteractionPoint)null, (GameObject)null);
				num++;
				num2++;
			}
		}
	}

	public static void ShootHoverboards()
	{
		if (InputHandler.IsRightGripPressed() && !(Time.time < ServerSideEquipRoutine_StateMachine101_Value_07))
		{
			ServerSideEquipRoutine_StateMachine101_Value_07 = Time.time + 0.5f;
			Transform val = Variables.Variables_Reference_09.rightHandTransform;
			DropHoverboard(Variables.Variables_Reference_09.rightHandTransform.position, val.rotation, val.forward * 10f, Vector3.zero, Variables.RandomColor());
		}
	}

	public static void OrbitHoverboards()
	{
		if (Time.time < ServerSideEquipRoutine_StateMachine101_Value_07)
		{
			return;
		}
		ServerSideEquipRoutine_StateMachine101_Value_07 = Time.time + 0.25f;
		Vector3 position = ((Component)Variables.Variables_Reference_09.headCollider).transform.position;
		float num = (float)Time.frameCount / 30f;
		int num2 = 0;
		if (num2 < 2)
		{
			Vector3 val = default(Vector3);
			Vector3 val2 = default(Vector3);
			do
			{
				float num3 = (float)num2 * 180f;
				float num4 = num3 - 25f;
				val = new Vector3(MathF.Cos(num3 + num) * 2f, 1f, MathF.Sin(num3 + num) * 2f);
				val2 = new Vector3(MathF.Cos(num4 + num) * 2f, 1f, MathF.Sin(num4 + num) * 2f);
				Vector3 pos = position + val;
				Vector3 val3 = position - val;
				Vector3 val4 = val3;
				Quaternion rot = Quaternion.Euler(((Vector3)val4).normalized);
				val3 = val2 - val;
				val4 = val3;
				DropHoverboard(pos, rot, ((Vector3)val4).normalized * 6.5f, new Vector3(0f, 360f, 0f), Variables.RandomColor());
				num2++;
			}
			while (num2 < 2);
		}
	}

	private static void UpdateGrapplingHookHand(ref GameObject aim, ref LineRenderer line, ref Vector3 grapPt, ref bool attached, Transform hand, float trigger, bool isRight)
	{
		RaycastHit val = default(RaycastHit);
		bool hasHit = Physics.Raycast(hand.position, hand.forward, out val, 100f);
		if (hasHit)
		{
			if ((Object)(object)aim == (Object)null)
			{
				aim = GameObject.CreatePrimitive((PrimitiveType)0);
				Object.Destroy((Object)(object)aim.GetComponent<Rigidbody>());
				Object.Destroy((Object)(object)aim.GetComponent<SphereCollider>());
				aim.transform.localScale = Vector3.one * 0.2f;
				aim.GetComponent<Renderer>().material.color = Color.green;
				aim.SetActive(true);
				aim.transform.position = ((RaycastHit)val).point;
				if (trigger > 0.1f)
				{
					goto Branch_0128;
				}
			}
			else
			{
				aim.SetActive(true);
				aim.transform.position = ((RaycastHit)val).point;
				if (trigger > 0.1f)
				{
					goto Branch_0128;
				}
			}
		}
		else if (trigger > 0.1f)
		{
			goto Branch_0128;
		}
		attached = false;
		line.positionCount = 0;
		return;
		Branch_0128:
		if (!attached && hasHit)
		{
			grapPt = ((RaycastHit)val).point;
			attached = true;
			line.positionCount = 2;
			line.SetPosition(1, grapPt);
			Variables.Variables_Reference_09.offlineVRRig.PlayHandTapLocal(82, isRight, 1f);
			if (!attached)
			{
				return;
			}
		}
		else if (!attached)
		{
			return;
		}
		line.positionCount = 2;
		line.SetPosition(0, hand.position);
		line.SetPosition(1, grapPt);
		Vector3 val2;
		Vector3 normalized;
		if ((Object)(object)aim != (Object)null)
		{
			aim.transform.position = grapPt;
			val2 = grapPt - ((Component)Variables.Variables_Reference_06.bodyCollider).transform.position;
			normalized = ((Vector3)val2).normalized;
			float num = Vector3.Distance(((Component)Variables.Variables_Reference_06.bodyCollider).transform.position, grapPt);
			if (!(num > 1f))
			{
				return;
			}
		}
		else
		{
			val2 = grapPt - ((Component)Variables.Variables_Reference_06.bodyCollider).transform.position;
			normalized = ((Vector3)val2).normalized;
			float num = Vector3.Distance(((Component)Variables.Variables_Reference_06.bodyCollider).transform.position, grapPt);
			if (!(num > 1f))
			{
				return;
			}
		}
		((Component)Variables.Variables_Reference_06).GetComponent<Rigidbody>().velocity = Vector3.Lerp(((Component)Variables.Variables_Reference_06).GetComponent<Rigidbody>().velocity, normalized * 15f, Time.deltaTime * 10f);
	}

	public static void WaterBender()
	{
		if (Time.time < ServerSideEquipRoutine_StateMachine101_Value_08)
		{
			return;
		}
		ServerSideEquipRoutine_StateMachine101_Value_08 = Time.time + ServerSideEquipRoutine_StateMachine101_Value_02;
		Safety.ResetNetworkLimits();
		if (InputHandler.IsRightGripPressed() && InputHandler.IsLeftGripPressed())
		{
			Vector3 pos = (Variables.Variables_Reference_06.RightHand.controllerTransform.position + Variables.Variables_Reference_06.LeftHand.controllerTransform.position) / 2f;
			float num = Vector3.Distance(Variables.Variables_Reference_06.RightHand.controllerTransform.position, Variables.Variables_Reference_06.LeftHand.controllerTransform.position);
			PlaySplashEffect(pos, Quaternion.Lerp(Variables.Variables_Reference_06.RightHand.controllerTransform.rotation, Variables.Variables_Reference_06.LeftHand.controllerTransform.rotation, 0.5f), Mathf.Clamp(num * 100f, 75f, 500f));
			if (!InputHandler.IsRightPrimaryPressed())
			{
				return;
			}
		}
		else
		{
			if (InputHandler.IsRightGripPressed())
			{
				PlaySplashEffect(Variables.Variables_Reference_06.RightHand.controllerTransform.position, ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.rotation, 125f);
				if (InputHandler.IsLeftGripPressed())
				{
					goto Branch_01b6;
				}
			}
			else if (InputHandler.IsLeftGripPressed())
			{
				goto Branch_01b6;
			}
			if (!InputHandler.IsRightPrimaryPressed())
			{
				return;
			}
		}
		goto Branch_0233;
		Branch_01b6:
		PlaySplashEffect(Variables.Variables_Reference_06.LeftHand.controllerTransform.position, ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.rotation, 125f);
		if (!InputHandler.IsRightPrimaryPressed())
		{
			return;
		}
		Branch_0233:
		PlaySplashEffect(Variables.Variables_Reference_09.offlineVRRig.headMesh.transform.position + Vector3.up * 0.5f, Quaternion.Euler(90f, 0f, 0f), 125f);
	}

	static Fun()
	{
		ServerSideEquipRoutine_StateMachine101_Position_03 = (Vector3[])(object)new Vector3[10];
		ServerSideEquipRoutine_StateMachine101_Position_04 = (Vector3[])(object)new Vector3[10];
		ServerSideEquipRoutine_StateMachine101_Object_01 = new HashSet<GameObject>();
		ServerSideEquipRoutine_StateMachine101_State_09 = false;
		ServerSideEquipRoutine_StateMachine101_Index_06 = 0;
		ServerSideEquipRoutine_StateMachine101_Color_01 = Color.white;
		ServerSideEquipRoutine_StateMachine101_Value_06 = 100f;
		ServerSideEquipRoutine_StateMachine101_Value_03 = 1.5f;
		ServerSideEquipRoutine_StateMachine101_Value_04 = 0f;
		ServerSideEquipRoutine_StateMachine101_Index_05 = 0;
		ServerSideEquipRoutine_StateMachine101_Text_01 = "No Cosmetic Selected";
		ServerSideEquipRoutine_StateMachine101_Value_02 = 0.25f;
		ServerSideEquipRoutine_StateMachine101_Text_02 = new HashSet<string>();
		ServerSideEquipRoutine_StateMachine101_Position_01 = new Vector3(-51.7836f, 16.7865f, -119.6697f);
		ServerSideEquipRoutine_StateMachine101_State_11 = true;
		ServerSideEquipRoutine_StateMachine101_State_03 = true;
		ServerSideEquipRoutine_StateMachine101_State_13 = false;
	}

	public static void GliderGun()
	{
		if (!GunLib.IsGunTriggerPressed())
		{
			return;
		}
		GliderHoldable[] array = Variables.FindObjectsCached<GliderHoldable>(false);
		int num = 0;
		while (num < array.Length)
		{
			GliderHoldable val = array[num];
			if (((NetworkView)array[num]).GetView.Owner == PhotonNetwork.LocalPlayer)
			{
				((Component)val).gameObject.transform.position = ((RaycastHit)GunLib.GunLib_Reference_07).point + Vector3.up;
				num++;
			}
			else
			{
				((NetworkHoldableObject)val).OnHover((InteractionPoint)null, (GameObject)null);
				num++;
			}
		}
	}

	public static void RestoreHandTapAudio()
	{
		MenuPatches.GrabPatch_State_06 = false;
		MenuPatches.GrabPatch_State_07 = true;
		MenuPatches.GrabPatch_State_03 = false;
		MenuPatches.GrabPatch_Value_01 = 0.1f;
		MenuPatches.GrabPatch_Index_01 = 1;
		Variables.Variables_Reference_09.handTapVolume = 0.1f;
	}

	public static void SetPlayerName(string name)
	{
		((GorillaComputer)GorillaComputer.instance).currentName = name;
		PhotonNetwork.LocalPlayer.NickName = name;
		((GorillaComputer)GorillaComputer.instance).SetLocalNameTagText(name);
		((GorillaComputer)GorillaComputer.instance).savedName = name;
		PlayerPrefs.SetString("playerName", name);
		PlayerPrefs.Save();
	}
}
