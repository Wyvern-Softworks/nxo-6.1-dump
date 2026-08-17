using System;
using GorillaNetworking;
using NXO.Mods.Categories;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace NXO.Utilities;

public static class RigManager
{
	private static VRRig RigManager_Reference_01;

	private static Material RigManager_Material_01;

	public static bool RigManager_State_01 = true;

	public static GameObject GetHandObject
	{
		get
		{
			if (!Variables.Variables_State_05)
			{
				return ((Component)Variables.Variables_Reference_09.offlineVRRig.leftHandPlayer).gameObject;
			}
			return ((Component)Variables.Variables_Reference_09.offlineVRRig.rightHandPlayer).gameObject;
		}
	}

	public static NetworkView GetNetworkView(VRRig vrrig)
	{
		if (!((Object)(object)vrrig == (Object)null))
		{
			return ReflectionCompat.GetField<NetworkView>(vrrig, "netView");
		}
		return null;
	}

	public static VRRig FindRig(NetPlayer netPlayer)
	{
		if (netPlayer != null)
		{
			return GorillaGameManager.StaticFindRigForPlayer(netPlayer);
		}
		return null;
	}

	public static void UpdateGhostRig()
	{
		if (!RigManager_State_01)
		{
			if ((Object)(object)RigManager_Reference_01 != (Object)null)
			{
				((Component)RigManager_Reference_01).gameObject.SetActive(false);
			}
			return;
		}
		try
		{
			if (!((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled)
			{
				if ((Object)(object)RigManager_Reference_01 == (Object)null)
				{
					RigManager_Reference_01 = Object.Instantiate<VRRig>(Variables.Variables_Reference_09.offlineVRRig);
					RigManager_Reference_01.headBodyOffset = Vector3.zero;
					((Component)RigManager_Reference_01).transform.SetParent(((Component)Variables.Variables_Reference_09.offlineVRRig).transform.parent);
					Transform obj = ((Component)RigManager_Reference_01).transform.Find("VR Constraints/LeftArm/Left Arm IK/SlideAudio");
					if (obj != null)
					{
						((Component)obj).gameObject.SetActive(false);
						Transform obj2 = ((Component)RigManager_Reference_01).transform.Find("VR Constraints/RightArm/Right Arm IK/SlideAudio");
						if (obj2 == null)
						{
							goto Branch_022c;
						}
						((Component)obj2).gameObject.SetActive(false);
						Transform obj3 = ((Component)RigManager_Reference_01).transform.Find("GorillaPlayerNetworkedRigAnchor/rig/bodySlideAudio");
						if (obj3 == null)
						{
							goto Branch_0280;
						}
						((Component)obj3).gameObject.SetActive(false);
						Visuals.EnsureWhiteVertexColors(RigManager_Reference_01);
						if ((Object)(object)RigManager_Material_01 == (Object)null)
						{
							goto Branch_02c8;
						}
					}
					else
					{
						Transform obj4 = ((Component)RigManager_Reference_01).transform.Find("VR Constraints/RightArm/Right Arm IK/SlideAudio");
						if (obj4 == null)
						{
							goto Branch_022c;
						}
						((Component)obj4).gameObject.SetActive(false);
						Transform obj5 = ((Component)RigManager_Reference_01).transform.Find("GorillaPlayerNetworkedRigAnchor/rig/bodySlideAudio");
						if (obj5 == null)
						{
							goto Branch_0280;
						}
						((Component)obj5).gameObject.SetActive(false);
						Visuals.EnsureWhiteVertexColors(RigManager_Reference_01);
						if ((Object)(object)RigManager_Material_01 == (Object)null)
						{
							goto Branch_02c8;
						}
					}
				}
				else if ((Object)(object)RigManager_Material_01 == (Object)null)
				{
					goto Branch_02c8;
				}
				goto Branch_0322;
			}
			if ((Object)(object)RigManager_Reference_01 != (Object)null && ((Component)RigManager_Reference_01).gameObject.activeSelf)
			{
				((Component)RigManager_Reference_01).gameObject.SetActive(false);
			}
			return;
			Branch_02c8:
			RigManager_Material_01 = new Material(Variables.Variables_Reference_02);
			RigManager_Material_01.color = (Color32)(new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, (byte)50));
			if (((Component)RigManager_Reference_01).gameObject.activeSelf)
			{
				goto Branch_0379;
			}
			goto Branch_0348;
			Branch_0322:
			if (((Component)RigManager_Reference_01).gameObject.activeSelf)
			{
				goto Branch_0379;
			}
			goto Branch_0348;
			Branch_0379:
			if (((Behaviour)RigManager_Reference_01).enabled)
			{
				goto Branch_046d;
			}
			goto Branch_039a;
			Branch_022c:
			Transform obj6 = ((Component)RigManager_Reference_01).transform.Find("GorillaPlayerNetworkedRigAnchor/rig/bodySlideAudio");
			if (obj6 == null)
			{
				goto Branch_0280;
			}
			((Component)obj6).gameObject.SetActive(false);
			Visuals.EnsureWhiteVertexColors(RigManager_Reference_01);
			if ((Object)(object)RigManager_Material_01 == (Object)null)
			{
				goto Branch_02c8;
			}
			goto Branch_0322;
			Branch_0280:
			Visuals.EnsureWhiteVertexColors(RigManager_Reference_01);
			if (!((Object)(object)RigManager_Material_01 == (Object)null))
			{
				goto Branch_0322;
			}
			goto Branch_02c8;
			Branch_0348:
			((Component)RigManager_Reference_01).gameObject.SetActive(true);
			if (((Behaviour)RigManager_Reference_01).enabled)
			{
				goto Branch_046d;
			}
			Branch_039a:
			((Behaviour)RigManager_Reference_01).enabled = true;
			((Renderer)RigManager_Reference_01.mainSkin).material = RigManager_Material_01;
			((Component)RigManager_Reference_01.headConstraint).transform.SetPositionAndRotation(((Component)Variables.Variables_Reference_06.headCollider).transform.position, ((Component)Variables.Variables_Reference_06.headCollider).transform.rotation);
			RigManager_Reference_01.leftHandTransform.SetPositionAndRotation(Variables.Variables_Reference_06.LeftHand.controllerTransform.position, Variables.Variables_Reference_06.LeftHand.controllerTransform.rotation);
			RigManager_Reference_01.rightHandTransform.SetPositionAndRotation(Variables.Variables_Reference_06.RightHand.controllerTransform.position, Variables.Variables_Reference_06.RightHand.controllerTransform.rotation);
			return;
			Branch_046d:
			((Renderer)RigManager_Reference_01.mainSkin).material = RigManager_Material_01;
			((Component)RigManager_Reference_01.headConstraint).transform.SetPositionAndRotation(((Component)Variables.Variables_Reference_06.headCollider).transform.position, ((Component)Variables.Variables_Reference_06.headCollider).transform.rotation);
			RigManager_Reference_01.leftHandTransform.SetPositionAndRotation(Variables.Variables_Reference_06.LeftHand.controllerTransform.position, Variables.Variables_Reference_06.LeftHand.controllerTransform.rotation);
			RigManager_Reference_01.rightHandTransform.SetPositionAndRotation(Variables.Variables_Reference_06.RightHand.controllerTransform.position, Variables.Variables_Reference_06.RightHand.controllerTransform.rotation);
		}
		catch (Exception arg)
		{
			Debug.LogError((object)$"Error updating ghost rig: {arg}");
		}
	}

	public static NetPlayer GetPlayer(VRRig vrrig)
	{
		NetPlayer player = vrrig.Creator ?? vrrig.OwningNetPlayer;
		if (player != null)
		{
			return player;
		}
		Component rigSerializer = ReflectionCompat.GetField<Component>(vrrig, "rigSerializer");
		return (rigSerializer != null) ? NetworkSystem.Instance.GetPlayer(NetworkSystem.Instance.GetOwningPlayerID(rigSerializer.gameObject)) : null;
	}

	public static bool IsRemoteRig(VRRig rig)
	{
		if ((Object)(object)rig != (Object)null && !rig.isOfflineVRRig)
		{
			return (Object)(object)rig != (Object)(object)VRRig.LocalRig;
		}
		return false;
	}

	public static bool IsTagged(VRRig rig)
	{
		if ((Object)(object)rig == (Object)null || !PhotonNetwork.InRoom || (Object)(object)GorillaGameManager.instance == (Object)null)
		{
			return false;
		}
		NetPlayer val = GetPlayer(rig);
		if (val == null)
		{
			return false;
		}
		int num = (int)GorillaGameManager.instance.GameType();
		num = (((uint)num <= 11u) ? num : 12) + 57;
		int num2 = num;
		if (num2 != 58)
		{
			return false;
		}
		GorillaGameManager instance = GorillaGameManager.instance;
		GorillaTagManager val2 = (GorillaTagManager)(object)((instance is GorillaTagManager) ? instance : null);
		if (val2 != null)
		{
			if (!val2.isCurrentlyTag)
			{
				return val2.currentInfected?.Contains(val) ?? false;
			}
			return val2.currentIt == val;
		}
		SkinnedMeshRenderer mainSkin = rig.mainSkin;
		if ((Object)(object)((mainSkin != null) ? ((Renderer)mainSkin).material : null) == (Object)null)
		{
			return false;
		}
		string name = ((Object)((Renderer)rig.mainSkin).material).name;
		if (!name.Contains("fected"))
		{
			return name.Contains("It");
		}
		return true;
	}

	public static bool AreHandsNearTargets(Transform[] hands, Transform[] targets)
	{
		if (hands == null || targets == null)
		{
			return false;
		}
		int num = 0;
		while (num < hands.Length)
		{
			Transform val = hands[num];
			if (!((Object)(object)val == (Object)null))
			{
				foreach (Transform val2 in targets)
				{
					if ((Object)(object)val2 != (Object)null && Vector3.Distance(val.position, val2.position) < 0.25f)
					{
						return true;
					}
				}
				num++;
			}
			else
			{
				num++;
			}
		}
		return false;
	}

	public static Photon.Realtime.Player GetPhotonPlayer(NetPlayer p)
	{
		return p.GetPlayerRef();
	}

	public static void SetPlayerColor(Color color, object target = null)
	{
		PlayerPrefs.SetFloat("redValue", Mathf.Clamp(color.r, 0f, 1f));
		PlayerPrefs.SetFloat("greenValue", Mathf.Clamp(color.g, 0f, 1f));
		PlayerPrefs.SetFloat("blueValue", Mathf.Clamp(color.b, 0f, 1f));
		GorillaTagger.Instance.UpdateColor(color.r, color.g, color.b);
		PlayerPrefs.Save();
		try
		{
			if (target != null)
			{
				NetPlayer val = (NetPlayer)((target is NetPlayer) ? target : null);
				if (val == null)
				{
					if (target is RpcTarget val2)
					{
						GorillaTagger.Instance.myVRRig.SendRPC("RPC_InitializeNoobMaterial", val2, new object[3] { color.r, color.g, color.b });
					}
				}
				else
				{
					GorillaTagger.Instance.myVRRig.SendRPC("RPC_InitializeNoobMaterial", val, new object[3] { color.r, color.g, color.b });
				}
			}
			else
			{
				GorillaTagger.Instance.myVRRig.SendRPC("RPC_InitializeNoobMaterial", (RpcTarget)0, new object[3] { color.r, color.g, color.b });
			}
			Safety.ResetNetworkLimits();
		}
		catch (Exception)
		{
		}
	}

	public static PhotonView GetPhotonView(this VRRig rig)
	{
		NetworkView netView = ReflectionCompat.GetField<NetworkView>(rig, "netView");
		return (netView != null) ? netView.GetView : null;
	}

	public static bool IsLocalOrGhostRig(this VRRig rig)
	{
		if ((Object)(object)rig != (Object)null)
		{
			if (!rig.isLocal)
			{
				if ((Object)(object)RigManager_Reference_01 != (Object)null)
				{
					return (Object)(object)rig == (Object)(object)RigManager_Reference_01;
				}
				return false;
			}
			return true;
		}
		return false;
	}

	public static void SetPlayerName(string PlayerName, bool noColor = false)
	{
		((GorillaComputer)GorillaComputer.instance).currentName = PlayerName;
		((GorillaComputer)GorillaComputer.instance).SetLocalNameTagText(((GorillaComputer)GorillaComputer.instance).currentName);
		((GorillaComputer)GorillaComputer.instance).savedName = ((GorillaComputer)GorillaComputer.instance).currentName;
		PlayerPrefs.SetString("playerName", ((GorillaComputer)GorillaComputer.instance).currentName);
		PlayerPrefs.Save();
		PhotonNetwork.LocalPlayer.NickName = PlayerName;
		if (noColor)
		{
			return;
		}
		try
		{
			if (((GorillaComputer)GorillaComputer.instance).friendJoinCollider.playerIDsCurrentlyTouching.Contains(PhotonNetwork.LocalPlayer.UserId) || CosmeticWardrobeProximityDetector.IsUserNearWardrobe(PhotonNetwork.LocalPlayer.ActorNumber))
			{
				GorillaTagger.Instance.myVRRig.SendRPC("RPC_InitializeNoobMaterial", (RpcTarget)0, new object[3]
				{
					VRRig.LocalRig.playerColor.r,
					VRRig.LocalRig.playerColor.g,
					VRRig.LocalRig.playerColor.b
				});
				Safety.ResetNetworkLimits();
			}
		}
		catch (Exception)
		{
		}
	}
}
