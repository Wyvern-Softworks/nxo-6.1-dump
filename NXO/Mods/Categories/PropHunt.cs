using System;
using System.Collections.Generic;

using ExitGames.Client.Photon;
using NXO.Menu;
using NXO.Utilities;
using Photon.Pun;
using UnityEngine;

namespace NXO.Mods.Categories;

internal class PropHunt
{
	private static int PropHunt_Index_01;

	private static string[] PropHunt_Text_01;

	public static ButtonHandler.Button PropHunt_Button_01;

	public static void CycleProp(bool forward)
	{
		if (PropHunt_Text_01 == null || PropHunt_Text_01.Length == 0)
		{
			PropHunt_Text_01 = PropHuntPools.AllPropCosmeticIds;
			if (forward)
			{
				goto Branch_007e;
			}
		}
		else if (forward)
		{
			goto Branch_007e;
		}
		PropHunt_Index_01 = (PropHunt_Index_01 - 1 + PropHunt_Text_01.Length) % PropHunt_Text_01.Length;
		PropHunt_Button_01?.SetText("Prop : " + UpdatePropSelectionLabel());
		return;
		Branch_007e:
		PropHunt_Index_01 = (PropHunt_Index_01 + 1) % PropHunt_Text_01.Length;
		PropHunt_Button_01?.SetText("Prop : " + UpdatePropSelectionLabel());
	}

	public static string GetCosmeticDisplayName(string cosmeticId)
	{
		if (PropHuntPools.propCosmeticId_to_cosmeticSO.TryGetValue(cosmeticId, out var value))
		{
			return value.info.displayName;
		}
		return cosmeticId;
	}

	public PropHunt()
	{
	}

	public static void ForceRoundStart()
	{
		if (PhotonNetwork.IsMasterClient)
		{
			ReflectionCompat.Invoke(GetGameManager(), "InfectionRoundStartCheck");
		}
	}

	public static void SkipSeekerBlindfold()
	{
		GameObject ph_blindfold_forCamera_1p = ReflectionCompat.GetField<GameObject>(GetGameManager(), "_ph_blindfold_forCamera_1p");
		if (ph_blindfold_forCamera_1p != null)
		{
			ph_blindfold_forCamera_1p.SetActive(false);
			GameObject ph_blindfold_forCamera_3p = ReflectionCompat.GetField<GameObject>(GetGameManager(), "_ph_blindfold_forCamera_3p");
			if (ph_blindfold_forCamera_3p != null)
			{
				ph_blindfold_forCamera_3p.SetActive(false);
			}
		}
		else
		{
			GameObject ph_blindfold_forCamera_3p2 = ReflectionCompat.GetField<GameObject>(GetGameManager(), "_ph_blindfold_forCamera_3p");
			if (ph_blindfold_forCamera_3p2 != null)
			{
				ph_blindfold_forCamera_3p2.SetActive(false);
			}
		}
	}

	public static void BecomeProp()
	{
		if (((GorillaTagManager)GetGameManager()).IsInfected(NetworkSystem.Instance.LocalPlayer))
		{
			NotificationLib.ShowNotification(NotificationLib.NotificationType.Error, "You're A Seeker");
		}
		else if (PropHunt_Text_01 == null || PropHunt_Text_01.Length == 0)
		{
			PropHunt_Text_01 = PropHuntPools.AllPropCosmeticIds;
			EquipPropForPlayer(PropHunt_Text_01[PropHunt_Index_01]);
		}
		else
		{
			EquipPropForPlayer(PropHunt_Text_01[PropHunt_Index_01]);
		}
	}

	public static GorillaPropHuntGameManager GetGameManager()
	{
		return (GorillaPropHuntGameManager)GorillaGameManager.instance;
	}

	public static void EquipPropForPlayer(string playfabId)
	{
		if (!PropHuntPools.IsReady || string.IsNullOrEmpty(playfabId))
		{
			return;
		}
		PropHuntGrabbableProp val = default(PropHuntGrabbableProp);
		PropHuntPools.TryGetGrabbableProp(playfabId, out val);
		PropHuntTaggableProp taggableProp = default(PropHuntTaggableProp);
		PropHuntPools.TryGetTaggableProp(playfabId, out taggableProp);
		if ((Object)(object)val == (Object)null)
		{
			return;
		}
		VRRig.LocalRig.propHuntHandFollower.DestroyProp();
		PropHuntHandFollower val2 = VRRig.LocalRig.propHuntHandFollower;
		ReflectionCompat.SetField(val2, "_grabbableProp", val);
		ReflectionCompat.SetField(val2, "_taggableProp", taggableProp);
		ReflectionCompat.SetField(val2, "_prop", ((Component)val).gameObject);
		ReflectionCompat.SetField(val2, "_propOffset", val.offset);
		ReflectionCompat.SetField(val2, "_hasProp", true);
		val.handFollower = val2;
		((Component)val).gameObject.SetActive(true);
		int num = 0;
		if (num < val.interactionPoints.Count)
		{
			do
			{
				val.interactionPoints[num].OnSpawn(VRRig.LocalRig);
				num++;
			}
			while (num < val.interactionPoints.Count);
		}
	}

	public static void BecomeHider()
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			return;
		}
		GorillaPropHuntGameManager val = GetGameManager();
		if (((GorillaTagManager)val).currentInfected.Contains(NetworkSystem.Instance.LocalPlayer))
		{
			do
			{
				((GorillaTagManager)val).currentInfected.Remove(NetworkSystem.Instance.LocalPlayer);
			}
			while (((GorillaTagManager)val).currentInfected.Contains(NetworkSystem.Instance.LocalPlayer));
		}
		((GorillaTagManager)val).UpdateInfectionState();
	}

	public static void SetHidersESPEnabled(bool enable)
	{
		GorillaPropHuntGameManager val = GetGameManager();
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
				if (enable)
				{
					if (((GorillaTagManager)val).IsInfected(current.Creator))
					{
						break;
					}
					current.SetInvisibleToLocalPlayer(false);
					current.bodyRenderer.SetSkeletonBodyActive(true);
					((Renderer)current.skeleton.renderer).sharedMaterial.shader = Variables.Variables_Reference_02;
					((Renderer)current.skeleton.renderer).sharedMaterial.color = new Color(0.4f, 1f, 0.6f, 0.35f);
					((Renderer)current.skeleton.renderer).sharedMaterial.renderQueue = 4000;
					((Behaviour)current.skeleton).enabled = true;
					((Renderer)current.skeleton.renderer).enabled = true;
					if (!enumerator.MoveNext())
					{
						return;
					}
				}
				else
				{
					((Behaviour)current.skeleton).enabled = false;
					((Renderer)current.skeleton.renderer).enabled = false;
					((Renderer)current.skeleton.renderer).sharedMaterial.shader = Variables.Variables_Reference_10;
					if (!enumerator.MoveNext())
					{
						return;
					}
				}
			}
		}
	}

	public static void CollapseBoundary()
	{
		if (PhotonNetwork.IsMasterClient)
		{
			PlayableBoundaryManager boundary = ReflectionCompat.GetField<PlayableBoundaryManager>(GetGameManager(), "_ph_playBoundary");
			if (boundary != null)
			{
				boundary.radiusScale = 0.001f;
			}
		}
	}

	public static string UpdatePropSelectionLabel()
	{
		if (PropHunt_Text_01 == null || PropHunt_Text_01.Length == 0)
		{
			return "None";
		}
		return GetCosmeticDisplayName(PropHunt_Text_01[PropHunt_Index_01]);
	}

	public static void SpamGamemode()
	{
		if (PhotonNetwork.IsMasterClient)
		{
			GetGameManager().PH_OnRoundEnd();
			GorillaPropHuntGameManager val = GetGameManager();
			ReflectionCompat.Invoke(val, "InfectionRoundEndCheck");
			ReflectionCompat.Invoke(val, "InfectionRoundStartCheck");
		}
	}

	public static void SetSeekersESPEnabled(bool enable)
	{
		GorillaPropHuntGameManager val = GetGameManager();
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
				bool flag = ((GorillaTagManager)val).IsInfected(current.Creator);
				if (enable)
				{
					if (!flag)
					{
						((Behaviour)current.skeleton).enabled = false;
						((Renderer)current.skeleton.renderer).enabled = false;
						break;
					}
					((Renderer)current.mainSkin).material.shader = Variables.Variables_Reference_02;
					((Renderer)current.mainSkin).material.color = new Color(1f, 0.15f, 0.15f, 0.85f);
					((Renderer)current.mainSkin).material.renderQueue = 4000;
					((Behaviour)current.skeleton).enabled = false;
					((Renderer)current.skeleton.renderer).enabled = false;
					if (!enumerator.MoveNext())
					{
						return;
					}
				}
				else
				{
					if (!flag)
					{
						break;
					}
					((Renderer)current.mainSkin).material.shader = Variables.Variables_Reference_10;
					if (!enumerator.MoveNext())
					{
						return;
					}
				}
			}
		}
	}

	public static void PropTagAll()
	{
		GorillaPropHuntGameManager val = GetGameManager();
		object gameState = ReflectionCompat.GetField<object>(val, "_ph_gameState");
		if (gameState == null || Convert.ToInt32(gameState) != 6)
		{
			return;
		}
		using IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			VRRig val2 = enumerator.Current;
			while (!((Object)(object)enumerator.Current == (Object)(object)VRRig.LocalRig) && !((GorillaTagManager)val).IsInfected(val2.Creator))
			{
				((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = false;
				((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position = ((Component)val2).transform.position;
				PhotonSerializer.SendViewUpdate();
				PunExtensions.GetPhotonView(GameObject.Find("Player Objects/RigCache/Network Parent/GameMode(Clone)")).RPC("RPC_ReportTag", (RpcTarget)0, new object[1] { val2.Creator.ActorNumber });
				PhotonNetwork.SendAllOutgoingCommands();
				((PhotonPeer)PhotonNetwork.NetworkingClient.LoadBalancingPeer).SendAcksOnly();
				((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = true;
				if (!enumerator.MoveNext())
				{
					return;
				}
			}
		}
	}

	public static void BecomeSeeker()
	{
		if (PhotonNetwork.IsMasterClient)
		{
			((GorillaTagManager)GetGameManager()).AddInfectedPlayer(NetworkSystem.Instance.LocalPlayer, true);
		}
	}

	public static void ForceRoundEnd()
	{
		if (PhotonNetwork.IsMasterClient)
		{
			GetGameManager().PH_OnRoundEnd();
			ReflectionCompat.Invoke(GetGameManager(), "InfectionRoundEndCheck");
		}
	}
}
