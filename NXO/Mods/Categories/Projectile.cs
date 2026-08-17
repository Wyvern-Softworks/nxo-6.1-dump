using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using ExitGames.Client.Photon;
using GorillaLocomotion;
using GorillaNetworking;
using GorillaTag;
using GorillaTag.CosmeticSystem;
using NXO.Menu;
using NXO.Utilities;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;

namespace NXO.Mods.Categories;

public class Projectile
{
	private static Dictionary<int, string> GetThrowableMaterialIds(bool leftHand)
	{
		string fieldName = leftHand ? "materialIndexToSnowballThrowablePlayfabIdStringLeft" : "materialIndexToSnowballThrowablePlayfabIdStringRight";
		return ReflectionCompat.GetStaticField<Dictionary<int, string>>(typeof(CosmeticsV2Spawner_Dirty), fieldName) ?? new Dictionary<int, string>();
	}

	public enum Projectiles
	{
		None,
		SnowBall,
		BigSnowball,
		PishFood,
		WaterBalloons,
		EasterEggs,
		Books,
		Apple,
		GoldCoin,
		HotDog,
		IceCream,
		Fireworks,
		Paper,
		IceCreamScoop,
		Chips,
		RedBall,
		TacoBall,
		MashedPotatos,
		BreadBall,
		PurpleBall,
		OrangeBall,
		Stuffing,
		Turkey,
		BrokenLavaRocks,
		WorkingLavaRocks,
		Corn,
		FireStick,
		FoodBall,
		PineApple,
		PopCorn,
		FlamingNuts,
		Presents,
		Confeti,
		GiantMeatball,
		CandyBuket,
		slingshot
	}

	[CompilerGenerated]
	private sealed class CapturedVariables440
	{
		public NetPlayer p;

		internal bool SendGrowing_Lambda1(VRRig x)
		{
			return x.Creator.UserId == p.UserId;
		}

		internal bool SendGrowing_Lambda0(VRRig x)
		{
			return x.Creator.UserId == p.UserId;
		}
	}

	public static int CapturedVariables440_Index_01 = 0;

	private static float CapturedVariables440_Value_03 = 0f;

	public static Dictionary<Projectiles, int> CapturedVariables440_Lookup_01 = new Dictionary<Projectiles, int>
	{
		{
			Projectiles.SnowBall,
			32
		},
		{
			Projectiles.BigSnowball,
			339
		},
		{
			Projectiles.PishFood,
			333
		},
		{
			Projectiles.WaterBalloons,
			204
		},
		{
			Projectiles.EasterEggs,
			311
		},
		{
			Projectiles.Books,
			312
		},
		{
			Projectiles.Apple,
			288
		},
		{
			Projectiles.GoldCoin,
			313
		},
		{
			Projectiles.HotDog,
			314
		},
		{
			Projectiles.IceCream,
			315
		},
		{
			Projectiles.Fireworks,
			316
		},
		{
			Projectiles.Paper,
			317
		},
		{
			Projectiles.IceCreamScoop,
			318
		},
		{
			Projectiles.Chips,
			322
		},
		{
			Projectiles.RedBall,
			323
		},
		{
			Projectiles.TacoBall,
			324
		},
		{
			Projectiles.MashedPotatos,
			325
		},
		{
			Projectiles.BreadBall,
			326
		},
		{
			Projectiles.PurpleBall,
			327
		},
		{
			Projectiles.OrangeBall,
			328
		},
		{
			Projectiles.Stuffing,
			329
		},
		{
			Projectiles.Turkey,
			330
		},
		{
			Projectiles.BrokenLavaRocks,
			231
		},
		{
			Projectiles.WorkingLavaRocks,
			287
		},
		{
			Projectiles.Corn,
			331
		},
		{
			Projectiles.FireStick,
			332
		},
		{
			Projectiles.FoodBall,
			333
		},
		{
			Projectiles.PineApple,
			334
		},
		{
			Projectiles.PopCorn,
			335
		},
		{
			Projectiles.FlamingNuts,
			340
		},
		{
			Projectiles.Presents,
			240
		},
		{
			Projectiles.Confeti,
			249
		},
		{
			Projectiles.GiantMeatball,
			252
		},
		{
			Projectiles.CandyBuket,
			286
		},
		{
			Projectiles.slingshot,
			-1
		}
	};

	public static GameObject CapturedVariables440_Object_01;

	public static Projectiles CapturedVariables440_Reference_03;

	private static bool CapturedVariables440_State_01 = false;

	private static float CapturedVariables440_Value_01 = 0f;

	private static object[] CapturedVariables440_Values_02 = new object[9];

	private static object[] CapturedVariables440_Values_01 = new object[3];

	private static float CapturedVariables440_Value_04;

	public static bool CapturedVariables440_State_03 = false;

	public static Dictionary<string, SnowballThrowable>? CapturedVariables440_Reference_01;

	public static bool CapturedVariables440_State_02;

	public static RaiseEventOptions? CapturedVariables440_Reference_02 = null;

	public static float CapturedVariables440_Value_02;

	public static string CurrentProjectileName
	{
		get
		{
			return Enum.GetName(typeof(Projectiles), CapturedVariables440_Reference_03);
		}
	}

	public Projectile()
	{
	}

	private static void SendPlayerEffect(int actorNumber)
	{
		NetworkSystemRaiseEvent.RaiseEvent((byte)3, (object)new object[3]
		{
			NetworkSystem.Instance.ServerTimestamp,
			6,
			new object[2]
			{
				actorNumber,
				(object)(PlayerEffect)0
			}
		}, new NetEventOptions
		{
			Reciever = (RecieverTarget)1
		}, false);
	}

	private static void SimulateSlingshotDraw()
	{
		Slingshot component = ((Component)GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/rig/body_pivot/Slingshot Chest Snap/body_AnchorFront_StowSlot").transform.GetChild(1)).gameObject.GetComponent<Slingshot>();
		((TransferrableObject)component).currentState = (PositionState)8;
		((TransferrableObject)component).itemState = (ItemStates)8;
		component.drawingHand = ((EquipmentInteractor)EquipmentInteractor.instance).rightHand;
		component.center.position = component.centerOrigin.position - CalculateVelocity(GorillaTagger.Instance.rightHandTransform.position, GorillaTagger.Instance.rightHandTransform.position + ((((ControllerInputPoller)ControllerInputPoller.instance).rightControllerGripFloat > 0.5f) ? GorillaTagger.Instance.rightHandTransform.forward : ((Component)GorillaTagger.Instance.bodyCollider).transform.forward) * 10f, 15f) * (component.maxDraw * 1.2f);
		ReflectionCompat.SetField(component, "minTimeToLaunch", Time.time - 1f);
		if (!ReflectionCompat.GetField(component, "hasDummyProjectile", false))
		{
			ReflectionCompat.Invoke((TransferrableObject)component, "LateUpdateShared");
			ReflectionCompat.Invoke((ProjectileWeapon)component, "LaunchProjectile");
			((TransferrableObject)component).currentState = (PositionState)16;
			((TransferrableObject)component).itemState = (ItemStates)1;
		}
		else
		{
			ReflectionCompat.Invoke((ProjectileWeapon)component, "LaunchProjectile");
			((TransferrableObject)component).currentState = (PositionState)16;
			((TransferrableObject)component).itemState = (ItemStates)1;
		}
	}

	private static void EnsureProjectileGrabZone(Projectiles Projectile)
	{
		if (!((Object)(object)CapturedVariables440_Object_01 != (Object)null))
		{
			CapturedVariables440_Object_01 = GameObject.CreatePrimitive((PrimitiveType)3);
			CapturedVariables440_Object_01.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
			CapturedVariables440_Object_01.transform.position = GorillaTagger.Instance.rightHandTransform.position;
			CapturedVariables440_Object_01.GetComponent<Collider>().enabled = false;
			CapturedVariables440_Object_01.AddComponent<GorillaSurfaceOverride>();
			CapturedVariables440_Object_01.GetComponent<GorillaSurfaceOverride>().overrideIndex = CapturedVariables440_Lookup_01[Projectile];
			CapturedVariables440_Object_01.AddComponent<SnowballGrabZone>();
			CapturedVariables440_Object_01.GetComponent<SnowballGrabZone>().materialIndex = CapturedVariables440_Lookup_01[Projectile];
		}
	}

	public static void GetTouchedToSnowballFling()
	{
		Transform[] targets = (Transform[])(object)new Transform[2]
		{
			Variables.Variables_Reference_09.offlineVRRig.headMesh.transform,
			Variables.Variables_Reference_09.offlineVRRig.bodyTransform
		};
		using IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			while (true)
			{
				VRRig current = enumerator.Current;
				if ((Object)(object)current == (Object)null || current.isOfflineVRRig || current.isMyPlayer)
				{
					break;
				}
				if (RigManager.AreHandsNearTargets((Transform[])(object)new Transform[2] { current.leftHandTransform, current.rightHandTransform }, targets))
				{
					HitRigWithProjectile(current);
					return;
				}
				if (!enumerator.MoveNext())
				{
					return;
				}
			}
		}
	}

	public static void SnowballEffectGun()
	{
		if (GunLib.TrySelectRig() && !(Time.time <= CapturedVariables440_Value_03) && PhotonNetwork.InRoom && (Object)(object)GunLib.GunLib_Reference_06 != (Object)null && (Object)(object)GunLib.GunLib_Reference_06 != (Object)(object)RigManager.FindRig((NetPlayer)PhotonNetwork.LocalPlayer))
		{
			SendPlayerEffect(GunLib.GunLib_Reference_06.OwningNetPlayer.ActorNumber);
			CapturedVariables440_Value_03 = Time.time + 0.1f;
		}
	}

	public static void SnowballFlingGun()
	{
		if (GunLib.TrySelectRig())
		{
			HitRigWithProjectile(GunLib.GunLib_Reference_06);
		}
	}

	public static void SnowballEffectAll()
	{
		if (InputHandler.IsRightTriggerPressed())
		{
			MenuPatches.SerializationPatch.SerializationPatch_State_01 = delegate
			{
				if (!PhotonNetwork.InRoom)
				{
					return true;
				}
				if (Time.time < CapturedVariables440_Value_03)
				{
					return false;
				}
				CapturedVariables440_Value_03 = Time.time + 0.1f;
				PhotonSerializer.SerializeAllViews(exclude: true, (PhotonView[])(object)new PhotonView[1] { Variables.Variables_Reference_09.myVRRig.GetView });
				Vector3 position = ((Component)VRRig.LocalRig).transform.position;
				((Behaviour)VRRig.LocalRig).enabled = false;
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
						SendPlayerEffect(val3.ActorNumber);
						num++;
					}
					else
					{
						num++;
					}
				}
				((Component)VRRig.LocalRig).transform.position = position;
				((Behaviour)VRRig.LocalRig).enabled = true;
				Safety.ResetNetworkLimits();
				return false;
			};
		}
		else
		{
			MenuPatches.SerializationPatch.SerializationPatch_State_01 = null;
		}
	}

	public static void GetTouchedToSnowballEffect()
	{
		Transform[] targets = (Transform[])(object)new Transform[2]
		{
			Variables.Variables_Reference_09.offlineVRRig.headMesh.transform,
			Variables.Variables_Reference_09.offlineVRRig.bodyTransform
		};
		using IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			while (true)
			{
				VRRig current = enumerator.Current;
				if ((Object)(object)current == (Object)null || current.isOfflineVRRig || current.isMyPlayer)
				{
					break;
				}
				if (RigManager.AreHandsNearTargets((Transform[])(object)new Transform[2] { current.leftHandTransform, current.rightHandTransform }, targets))
				{
					if (Time.time > CapturedVariables440_Value_03)
					{
						SendPlayerEffect(current.OwningNetPlayer.ActorNumber);
						CapturedVariables440_Value_03 = Time.time + 0.1f;
					}
					return;
				}
				if (!enumerator.MoveNext())
				{
					return;
				}
			}
		}
	}

	public static SnowballThrowable? FindProjectileThrowable(string projectileName)
	{
		List<string> list = (from x in ((AllCosmeticsArraySO)((AssetReference)((CosmeticsController)CosmeticsController.instance).v2_allCosmeticsInfoAssetRef).Asset).sturdyAssetRefs
			where (Object)(object)x.obj != (Object)null && x.obj.info.isThrowable
			select x.obj.info.playFabID).Distinct().ToList();
		SnowballThrowable value;
		if (CapturedVariables440_Reference_01 == null || CapturedVariables440_Reference_01.Count != list.Count - 3)
		{
			if (!CosmeticsV2Spawner_Dirty.isPrepared)
			{
				return null;
			}
			if (!((GorillaComputer)GorillaComputer.instance).isConnectedToMaster)
			{
				return null;
			}
			if (!CapturedVariables440_State_02)
			{
				if (GetThrowableMaterialIds(true).Count >= 1)
				{
					if (GetThrowableMaterialIds(false).Count >= 1)
					{
						goto Branch_0196;
					}
				}
			}
			CapturedVariables440_Reference_01 = new Dictionary<string, SnowballThrowable>();
			SnowballMaker[] array = (SnowballMaker[])(object)new SnowballMaker[2]
			{
				SnowballMaker.leftHandInstance,
				SnowballMaker.rightHandInstance
			};
			for (int num = 0; num < array.Length; num++)
			{
				SnowballThrowable[] snowballs = array[num].snowballs;
				foreach (SnowballThrowable val in snowballs)
				{
					try
					{
						CapturedVariables440_Reference_01.Add(((Object)((Component)((Component)val).transform.parent).gameObject).name, val);
					}
					catch (Exception)
					{
					}
				}
			}
			string text = projectileName + "(Clone)";
			projectileName = text;
			if (!CapturedVariables440_Reference_01.TryGetValue(projectileName, out value))
			{
				goto Branch_0364;
			}
		}
		else
		{
			string text2 = projectileName + "(Clone)";
			projectileName = text2;
			if (!CapturedVariables440_Reference_01.TryGetValue(projectileName, out value))
			{
				goto Branch_0364;
			}
		}
		return value;
		Branch_0196:
		CapturedVariables440_State_02 = true;
		LinqUtils.ForEach<KeyValuePair<int, string>>((IEnumerable<KeyValuePair<int, string>>)GetThrowableMaterialIds(true), (Action<KeyValuePair<int, string>>)delegate(KeyValuePair<int, string> v)
		{
			VRRig.LocalRig.cosmeticsObjectRegistry.Cosmetic(v.Value);
		});
		LinqUtils.ForEach<KeyValuePair<int, string>>((IEnumerable<KeyValuePair<int, string>>)GetThrowableMaterialIds(false), (Action<KeyValuePair<int, string>>)delegate(KeyValuePair<int, string> v)
		{
			VRRig.LocalRig.cosmeticsObjectRegistry.Cosmetic(v.Value);
		});
		return null;
		Branch_0364:
		return null;
	}

	public static bool IsPlayerNull(Player p)
	{
		if (p == null)
		{
			return true;
		}
		return false;
	}

	public static void FireSelectedProjectile(Projectiles Proj, Vector3? velocity, float delay = 0.1f)
	{
		if (CapturedVariables440_Reference_03 != Proj)
		{
			CapturedVariables440_State_01 = true;
			CapturedVariables440_Reference_03 = Proj;
			if ((Object)(object)CapturedVariables440_Object_01 == (Object)null)
			{
				goto Branch_006f;
			}
		}
		else
		{
			CapturedVariables440_Reference_03 = Proj;
			if ((Object)(object)CapturedVariables440_Object_01 == (Object)null)
			{
				goto Branch_006f;
			}
		}
		if (UpdateProjectileGrabZone(CapturedVariables440_Reference_03, CapturedVariables440_State_01))
		{
			goto Branch_00cf;
		}
		return;
		Branch_00cf:
		if (CapturedVariables440_State_01)
		{
			CapturedVariables440_State_01 = false;
			if (Time.time - CapturedVariables440_Value_01 < delay)
			{
				return;
			}
		}
		else if (Time.time - CapturedVariables440_Value_01 < delay)
		{
			return;
		}
		CapturedVariables440_Value_01 = Time.time;
		switch (Proj)
		{
		case Projectiles.slingshot:
			SimulateSlingshotDraw();
			break;
		case Projectiles.BigSnowball:
			SpawnProjectile(GorillaTagger.Instance.rightHandTransform.position, CalculateVelocity(GorillaTagger.Instance.rightHandTransform.position, GorillaTagger.Instance.rightHandTransform.position + ((((ControllerInputPoller)ControllerInputPoller.instance).rightControllerGripFloat > 0.5f) ? (-GorillaTagger.Instance.rightHandTransform.up) : ((Component)GorillaTagger.Instance.bodyCollider).transform.forward) * 10f, 15f));
			break;
		default:
			LaunchProjectile(Proj, velocity);
			break;
		}
		return;
		Branch_006f:
		EnsureProjectileGrabZone(CapturedVariables440_Reference_03);
		if (UpdateProjectileGrabZone(CapturedVariables440_Reference_03, CapturedVariables440_State_01))
		{
			goto Branch_00cf;
		}
	}

	public static void SelectProjectile(string name)
	{
		if (Enum.TryParse<Projectiles>(name, out var result))
		{
			CapturedVariables440_Reference_03 = result;
			ModButtons.buttons.Where((ButtonHandler.Button x) => x.buttonText.Contains("Current Projectile")).FirstOrDefault()?.SetText("Current Projectile : " + CurrentProjectileName);
		}
	}

	public static Vector3 CalculateVelocity(Vector3 currentPos, Vector3 targetPos, float speed)
	{
		Vector3 val = targetPos - currentPos;
		return ((Vector3)val).normalized * speed;
	}

	public static IEnumerator RestoreProjectileThrowablesDelayed(bool rigDisabled)
	{
		yield return (object)new WaitForSeconds(0.3f);
		Dictionary<string, SnowballThrowable>.ValueCollection.Enumerator enumerator;
		if (rigDisabled)
		{
			((Behaviour)VRRig.LocalRig).enabled = true;
			enumerator = CapturedVariables440_Reference_01.Values.GetEnumerator();
		}
		else
		{
			enumerator = CapturedVariables440_Reference_01.Values.GetEnumerator();
		}
		try
		{
			if (!enumerator.MoveNext())
			{
				yield break;
			}
			do
			{
				SnowballThrowable snowball = enumerator.Current;
				try
				{
					snowball.SetSnowballActiveLocal(false);
				}
				catch (Exception)
				{
				}
			}
			while (enumerator.MoveNext());
		}
		finally
		{
			((IDisposable)enumerator/*cast due to constrained. prefix*/).Dispose();
		}
	}

	public static void ShootBigSnowballs()
	{
		if (((ControllerInputPoller)ControllerInputPoller.instance).rightControllerGripFloat <= 0.5f)
		{
			if (Mouse.current == null || !Mouse.current.rightButton.isPressed)
			{
				return;
			}
		}
		Transform val = GorillaTagger.Instance.rightHandTransform;
		SpawnProjectile(GorillaTagger.Instance.rightHandTransform.position, -val.up * 100f);
	}

	public static void ClearProjectileCache()
	{
		CapturedVariables440_Reference_01 = null;
		CapturedVariables440_State_02 = false;
		CapturedVariables440_State_03 = false;
	}

	public static RaiseEventOptions TargetActorOptions(int act)
	{
		RaiseEventOptions val = new RaiseEventOptions();
		val.TargetActors = new int[1] { act };
		CapturedVariables440_Reference_02 = val;
		return CapturedVariables440_Reference_02;
	}

	private static bool UpdateProjectileGrabZone(Projectiles Projectile, bool shouldRecreate)
	{
		if (shouldRecreate)
		{
			Object.Destroy((Object)(object)CapturedVariables440_Object_01);
			CapturedVariables440_Object_01 = null;
			EnsureProjectileGrabZone(Projectile);
			return true;
		}
		CapturedVariables440_Object_01.transform.position = GorillaTagger.Instance.rightHandTransform.position;
		return true;
	}

	public static RaiseEventOptions ReceiverOptions(bool all)
	{
		CapturedVariables440_Reference_02 = new RaiseEventOptions
		{
			Receivers = (ReceiverGroup)(all ? 1 : 0)
		};
		return CapturedVariables440_Reference_02;
	}

	private static void HitRigWithProjectile(VRRig rig)
	{
		if (rig == null)
		{
			if ((Object)null == (Object)null)
			{
				return;
			}
		}
		else if ((Object)(object)rig.head?.rigTarget == (Object)null)
		{
			return;
		}
		if ((Object)(object)Variables.Variables_Reference_09.offlineVRRig?.rightHand?.rigTarget == (Object)null || rig.Creator == null)
		{
			return;
		}
		((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = false;
		((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position = ((Component)rig).transform.position + new Vector3(1f, 0f, 0f);
		int num = 0;
		if (num < 3)
		{
			do
			{
				SpawnProjectile(rig.head.rigTarget.position - new Vector3(0f, 0.3f, 0f), CalculateVelocity(GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.position, ((Component)rig).transform.up, 103f), Fling: false, rig.Creator);
				num++;
			}
			while (num < 3);
		}
	}

	public static void FireProjectileOnGrip(Projectiles Proj, Vector3? velocity, float delay = 0.1f)
	{
		CapturedVariables440_Reference_03 = Proj;
		if (((ControllerInputPoller)ControllerInputPoller.instance).rightControllerGripFloat > 0.5f || Mouse.current.rightButton.isPressed)
		{
			FireSelectedProjectile(CapturedVariables440_Reference_03, null);
		}
	}

	public static void DisableGrowingSnowball()
	{
		try
		{
			SnowballThrowable val = FindProjectileThrowable("GrowingSnowballRightAnchor");
			if ((Object)(object)val != (Object)null && (Object)(object)((Component)val).gameObject != (Object)null)
			{
				((Component)val).gameObject.SetActive(false);
			}
		}
		catch (Exception)
		{
		}
	}

	private static void LaunchProjectile(Projectiles Proj, Vector3? vel = null)
	{
		Color val5 = default(Color);
		if (Time.time < CapturedVariables440_Value_04)
		{
			return;
		}
		CapturedVariables440_Value_04 = Time.time + 0.2f;
		if (!CapturedVariables440_Lookup_01.TryGetValue(Proj, out var value))
		{
			return;
		}
		if (!CapturedVariables440_State_02)
		{
			if (GetThrowableMaterialIds(true).Count >= 1)
			{
				if (GetThrowableMaterialIds(false).Count >= 1)
				{
					goto Branch_00db;
				}
			}
		}
		SnowballMaker rightHandInstance = SnowballMaker.rightHandInstance;
		HandState rightHand = GTPlayer.Instance.RightHand;
		rightHand.materialTouchIndex = value;
		ReflectionCompat.SetField(GTPlayer.Instance, "rightHand", rightHand);
		SnowballThrowable val = default(SnowballThrowable);
		if (!rightHandInstance.TryCreateSnowball(value, out val))
		{
			return;
		}
		goto Branch_01e3;
		Branch_00db:
		CapturedVariables440_State_02 = true;
		LinqUtils.ForEach<KeyValuePair<int, string>>((IEnumerable<KeyValuePair<int, string>>)GetThrowableMaterialIds(true), (Action<KeyValuePair<int, string>>)delegate(KeyValuePair<int, string> v)
		{
			VRRig.LocalRig.cosmeticsObjectRegistry.Cosmetic(v.Value);
		});
		LinqUtils.ForEach<KeyValuePair<int, string>>((IEnumerable<KeyValuePair<int, string>>)GetThrowableMaterialIds(false), (Action<KeyValuePair<int, string>>)delegate(KeyValuePair<int, string> v)
		{
			VRRig.LocalRig.cosmeticsObjectRegistry.Cosmetic(v.Value);
		});
		rightHandInstance = SnowballMaker.rightHandInstance;
		rightHand = GTPlayer.Instance.RightHand;
		rightHand.materialTouchIndex = value;
		ReflectionCompat.SetField(GTPlayer.Instance, "rightHand", rightHand);
		if (!rightHandInstance.TryCreateSnowball(value, out val))
		{
			return;
		}
		goto Branch_01e3;
		Branch_02e8:
		Vector3 val2 = GTPlayer.Instance.GetHandVelocityTracker(false).GetAverageVelocity(false, 0.15f, false);
		float x = GorillaTagger.Instance.rightHandTransform.lossyScale.x;
		Color32 val3 = (Color32)((Color32)(VRRig.LocalRig.GetThrowableProjectileColor(false)));
		Vector3 position;
		val5 = default(Color);
		SlingshotProjectile val4 = ReflectionCompat.Invoke(val, "LaunchSnowballLocal", position, val2, x, val.randomizeColor, val5) as SlingshotProjectile;
		if (!PhotonNetwork.InRoom)
		{
			goto Branch_0579;
		}
		Branch_03cc:
		try
		{
			int myProjectileCount = val4.myProjectileCount;
			ReflectionCompat.InvokeStatic(ReflectionCompat.FindType("RoomSystem"), "SendLaunchProjectile", position, val2, 2, myProjectileCount, val.randomizeColor, val3.r, val3.g, val3.b, val3.a);
			CapturedVariables440_Values_02[0] = position;
			CapturedVariables440_Values_02[1] = val2;
			CapturedVariables440_Values_02[2] = 2;
			CapturedVariables440_Values_02[3] = myProjectileCount;
			CapturedVariables440_Values_02[4] = val.randomizeColor;
			CapturedVariables440_Values_02[5] = val3.r;
			CapturedVariables440_Values_02[6] = val3.g;
			CapturedVariables440_Values_02[7] = val3.b;
			CapturedVariables440_Values_02[8] = val3.a;
			CapturedVariables440_Values_01[0] = NetworkSystem.Instance.ServerTimestamp;
			CapturedVariables440_Values_01[1] = 0;
			CapturedVariables440_Values_01[2] = CapturedVariables440_Values_02;
			object[] nA5Q1MMQ = CapturedVariables440_Values_01;
			RaiseEventOptions val6 = new RaiseEventOptions
			{
				Receivers = (ReceiverGroup)0
			};
			SendOptions val7 = default(SendOptions);
			val7.Reliability = false;
			PhotonNetwork.RaiseEvent((byte)3, (object)nA5Q1MMQ, val6, val7);
			Debug.Log((object)("Launched packed (" + ((Object)val4).name + ")"));
		}
		catch (Exception)
		{
		}
		VRRig.LocalRig.RightThrowableProjectileIndex = -1;
		((EquipmentInteractor)EquipmentInteractor.instance).ForceDropEquipment((IHoldableObject)(object)val);
		ReflectionCompat.SetField(val, "targetRig", VRRig.LocalRig);
		val.isLeftHanded = false;
		Array.Clear(CapturedVariables440_Values_02, 0, CapturedVariables440_Values_02.Length);
		Array.Clear(CapturedVariables440_Values_01, 0, CapturedVariables440_Values_01.Length);
		return;
		Branch_01e3:
		if ((Object)(object)val == (Object)null)
		{
			return;
		}
		((EquipmentInteractor)EquipmentInteractor.instance).ForceDropAnyEquipment();
		val.isLeftHanded = false;
		ReflectionCompat.SetField(val, "targetRig", VRRig.LocalRig);
		val.SetSnowballActiveLocal(true);
		VRRig.LocalRig.RightThrowableProjectileIndex = val.throwableMakerIndex;
		if (val.randomizeColor)
		{
			VRRig.LocalRig.SetThrowableProjectileColor(false, (Color32)(GTColor.RandomHSV(val.randomColorHSVRanges)));
			position = GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.position;
			if (!vel.HasValue)
			{
				goto Branch_02e8;
			}
		}
		else
		{
			position = GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.position;
			if (!vel.HasValue)
			{
				goto Branch_02e8;
			}
		}
		val2 = vel.Value;
		x = GorillaTagger.Instance.rightHandTransform.lossyScale.x;
		val3 = (Color32)((Color32)(VRRig.LocalRig.GetThrowableProjectileColor(false)));
		val4 = ReflectionCompat.Invoke(val, "LaunchSnowballLocal", position, val2, x, val.randomizeColor, val5) as SlingshotProjectile;
		if (!PhotonNetwork.InRoom)
		{
			goto Branch_0579;
		}
		goto Branch_03cc;
		Branch_0579:
		VRRig.LocalRig.RightThrowableProjectileIndex = -1;
		((EquipmentInteractor)EquipmentInteractor.instance).ForceDropEquipment((IHoldableObject)(object)val);
		ReflectionCompat.SetField(val, "targetRig", VRRig.LocalRig);
		val.isLeftHanded = false;
		Array.Clear(CapturedVariables440_Values_02, 0, CapturedVariables440_Values_02.Length);
		Array.Clear(CapturedVariables440_Values_01, 0, CapturedVariables440_Values_01.Length);
	}

	public static void TouchToSnowballFling()
	{
		Transform[] hands = (Transform[])(object)new Transform[2]
		{
			Variables.Variables_Reference_09.leftHandTransform,
			Variables.Variables_Reference_09.rightHandTransform
		};
		using IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			while (true)
			{
				VRRig current = enumerator.Current;
				if ((Object)(object)current == (Object)null || current.isOfflineVRRig || current.isLocal)
				{
					break;
				}
				Transform[] targets = (Transform[])(object)new Transform[2]
				{
					current.headMesh.transform,
					current.bodyTransform
				};
				if (RigManager.AreHandsNearTargets(hands, targets))
				{
					HitRigWithProjectile(current);
					return;
				}
				if (!enumerator.MoveNext())
				{
					return;
				}
			}
		}
	}

	public static void TouchToSnowballEffect()
	{
		Transform[] hands = (Transform[])(object)new Transform[2]
		{
			Variables.Variables_Reference_09.leftHandTransform,
			Variables.Variables_Reference_09.rightHandTransform
		};
		using IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			while (true)
			{
				VRRig current = enumerator.Current;
				if ((Object)(object)current == (Object)null || current.isOfflineVRRig || (Object)(object)current == (Object)(object)Variables.Variables_Reference_09.offlineVRRig)
				{
					break;
				}
				Transform[] targets = (Transform[])(object)new Transform[2]
				{
					current.headMesh.transform,
					current.bodyTransform
				};
				if (RigManager.AreHandsNearTargets(hands, targets))
				{
					if (Time.time > CapturedVariables440_Value_03)
					{
						SendPlayerEffect(current.OwningNetPlayer.ActorNumber);
						CapturedVariables440_Value_03 = Time.time + 0.1f;
					}
					return;
				}
				if (!enumerator.MoveNext())
				{
					return;
				}
			}
		}
	}

	public static void SnowballFlingAll()
	{
		MenuPatches.SerializationPatch.SerializationPatch_State_01 = delegate
		{
			PhotonView component = ((Component)Variables.Variables_Reference_09.myVRRig).GetComponent<PhotonView>();
			if ((Object)(object)component == (Object)null)
			{
				return false;
			}
			PhotonSerializer.SerializeAllViews(exclude: true, (PhotonView[])(object)new PhotonView[1] { component });
			if (NetworkSystem.Instance.PlayerListOthers.Where((NetPlayer pl) => (Object)(object)RigManager.FindRig(pl) != (Object)null).ToList().Count == 0)
			{
				return false;
			}
			List<NetPlayer> list = NetworkSystem.Instance.PlayerListOthers
				.Where((NetPlayer pl) => (Object)(object)RigManager.FindRig(pl) != (Object)null)
				.ToList();
			CapturedVariables440_Index_01 %= list.Count;
			NetPlayer val = list[CapturedVariables440_Index_01];
			CapturedVariables440_Index_01 = (CapturedVariables440_Index_01 + 1) % list.Count;
			VRRig val2 = RigManager.FindRig(val);
			if ((Object)(object)val2 == (Object)null)
			{
				return false;
			}
			if ((Object)(object)val2.head?.rigTarget == (Object)null || (Object)(object)VRRig.LocalRig?.rightHand?.rigTarget == (Object)null)
			{
				return false;
			}
			Vector3 position = ((Component)VRRig.LocalRig).transform.position;
			Quaternion rotation = ((Component)VRRig.LocalRig).transform.rotation;
			Vector3 position2 = VRRig.LocalRig.leftHand.rigTarget.position;
			Quaternion rotation2 = VRRig.LocalRig.leftHand.rigTarget.rotation;
			Vector3 position3 = VRRig.LocalRig.rightHand.rigTarget.position;
			Quaternion rotation3 = VRRig.LocalRig.rightHand.rigTarget.rotation;
			((Component)VRRig.LocalRig).transform.position = ((Component)val2).transform.position + new Vector3(1f, 0f, 0f);
			SpawnProjectile(val2.head.rigTarget.position - new Vector3(0f, 0.3f, 0f), CalculateVelocity(VRRig.LocalRig.rightHand.rigTarget.position, ((Component)val2).transform.up, 103f), Fling: false, val);
			RaiseEventOptions val3 = new RaiseEventOptions();
			val3.TargetActors = new int[1] { val.ActorNumber };
			PhotonSerializer.SerializePhotonView(component, val3);
			Safety.ResetNetworkLimits();
			((Component)VRRig.LocalRig).transform.position = position;
			((Component)VRRig.LocalRig).transform.rotation = rotation;
			VRRig.LocalRig.leftHand.rigTarget.position = position2;
			VRRig.LocalRig.leftHand.rigTarget.rotation = rotation2;
			VRRig.LocalRig.rightHand.rigTarget.position = position3;
			VRRig.LocalRig.rightHand.rigTarget.rotation = rotation3;
			return false;
		};
	}

	public static void ShootBigSnowballsGun()
	{
		if (GunLib.IsGunTriggerPressed())
		{
			Vector3 position = GorillaTagger.Instance.rightHandTransform.position;
			Vector3 val = ((RaycastHit)GunLib.GunLib_Reference_07).point - position;
			SpawnProjectile(position, ((Vector3)val).normalized * 100f);
		}
	}

	public static void CycleProjectile(bool forward)
	{
		Projectiles[] array = (Projectiles[])Enum.GetValues(typeof(Projectiles));
		int num = Array.IndexOf(array, CapturedVariables440_Reference_03);
		if (!forward)
		{
			num = (num - 1 + array.Length) % array.Length;
			CapturedVariables440_Reference_03 = array[num];
			ModButtons.buttons.Where((ButtonHandler.Button x) => x.buttonText.Contains("Current Projectile")).FirstOrDefault()?.SetText("Current Projectile : " + CurrentProjectileName);
		}
		else
		{
			num = (num + 1) % array.Length;
			CapturedVariables440_Reference_03 = array[num];
			ModButtons.buttons.Where((ButtonHandler.Button x) => x.buttonText.Contains("Current Projectile")).FirstOrDefault()?.SetText("Current Projectile : " + CurrentProjectileName);
		}
	}

	public static void SpawnProjectile(Vector3 Position, Vector3 Velocity, bool Fling = false, NetPlayer? p = null)
	{
		CapturedVariables440 LocalScope7 = new CapturedVariables440();
		LocalScope7.p = p;
		if (!PhotonNetwork.InRoom)
		{
			return;
		}
		GrowingSnowballThrowable val = null;
		try
		{
			SnowballThrowable? obj = FindProjectileThrowable("GrowingSnowballRightAnchor");
			val = ((obj != null) ? ((Component)obj).GetComponent<GrowingSnowballThrowable>() : null);
		}
		catch (Exception)
		{
		}
		if ((Object)(object)val == (Object)null)
		{
			return;
		}
		PhotonEvent changeSizeEvent = ReflectionCompat.GetField<PhotonEvent>(val, "changeSizeEvent");
		PhotonEvent snowballThrowEvent = ReflectionCompat.GetField<PhotonEvent>(val, "snowballThrowEvent");
		RaiseEventOptions val2;
		if (!CapturedVariables440_State_03)
		{
			try
			{
				((SnowballThrowable)val).SetSnowballActiveLocal(true);
			}
			catch (Exception)
			{
			}
			val.IncreaseSize(5);
			((Component)val).transform.position = Position;
			CapturedVariables440_State_03 = true;
			val2 = new RaiseEventOptions();
			if (LocalScope7.p != null)
			{
				goto Branch_0137;
			}
		}
		else
		{
			val2 = new RaiseEventOptions();
			if (LocalScope7.p != null)
			{
				goto Branch_0137;
			}
		}
		val2.Receivers = (ReceiverGroup)1;
		if (!Fling)
		{
			goto Branch_0291;
		}
		Branch_018c:
		if (LocalScope7.p == null)
		{
			goto Branch_0291;
		}
		if (Vector3.Distance(((Component)VRRig.LocalRig).transform.position, ((Component)VRRigCache.ActiveRigs.Where((VRRig x) => x.Creator.UserId == LocalScope7.p.UserId).FirstOrDefault()).transform.position) > 4f)
		{
			((Behaviour)VRRig.LocalRig).enabled = false;
			((Component)VRRig.LocalRig).transform.position = ((Component)VRRigCache.ActiveRigs.Where((VRRig x) => x.Creator.UserId == LocalScope7.p.UserId).FirstOrDefault()).transform.position + new Vector3(0f, 0.2f, 0f);
			if (CapturedVariables440_State_03)
			{
				goto Branch_02aa;
			}
		}
		else if (CapturedVariables440_State_03)
		{
			goto Branch_02aa;
		}
		goto Branch_04d9;
		Branch_04fc:
		try
		{
			DisableGrowingSnowball();
			((MonoBehaviour)GorillaTagger.Instance).StartCoroutine(RestoreProjectileThrowablesDelayed(rigDisabled: true));
		}
		catch (Exception)
		{
		}
		CapturedVariables440_State_03 = false;
		if (!((Object)(object)VRRig.LocalRig != (Object)null))
		{
			return;
		}
		goto Branch_056b;
		Branch_054c:
		if (!((Object)(object)VRRig.LocalRig != (Object)null))
		{
			return;
		}
		goto Branch_056b;
		Branch_02aa:
		if (Time.time <= CapturedVariables440_Value_02 || changeSizeEvent == null || snowballThrowEvent == null)
		{
			goto Branch_04d9;
		}
		int num = default(int);
		try
		{
			object projectileIndex = ReflectionCompat.InvokeStatic(ReflectionCompat.FindType("ProjectileTracker"), "AddAndIncrementLocalProjectile", null, Velocity, Position, 1f);
			if (projectileIndex is int index)
			{
				num = index;
			}
		}
		catch (Exception)
		{
		}
		object[] obj2 = new object[2]
		{
			ReflectionCompat.GetField(changeSizeEvent, "_eventId", -1),
			5
		};
		RaiseEventOptions obj3 = val2;
		SendOptions val4 = default(SendOptions);
		val4.Reliability = false;
		PhotonNetwork.RaiseEvent((byte)176, (object)obj2, obj3, val4);
		object[] obj4 = new object[5]
		{
			ReflectionCompat.GetField(snowballThrowEvent, "_eventId", -1),
			Position,
			Velocity,
			num,
			null
		};
		RaiseEventOptions obj5 = val2;
		val4 = default(SendOptions);
		val4.Reliability = false;
		PhotonNetwork.RaiseEvent((byte)176, (object)obj4, obj5, val4);
		CapturedVariables440_Value_02 = Time.time;
		if (!CapturedVariables440_State_03)
		{
			goto Branch_054c;
		}
		goto Branch_04fc;
		Branch_04d9:
		if (!CapturedVariables440_State_03)
		{
			goto Branch_054c;
		}
		goto Branch_04fc;
		Branch_0291:
		if (!CapturedVariables440_State_03)
		{
			goto Branch_04d9;
		}
		goto Branch_02aa;
		Branch_0137:
		val2.TargetActors = new int[1] { LocalScope7.p.ActorNumber };
		if (!Fling)
		{
			goto Branch_0291;
		}
		goto Branch_018c;
		Branch_056b:
		if (!((Behaviour)VRRig.LocalRig).enabled)
		{
			((Behaviour)VRRig.LocalRig).enabled = true;
		}
	}
}
