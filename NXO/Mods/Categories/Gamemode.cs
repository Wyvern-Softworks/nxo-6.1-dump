using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using ExitGames.Client.Photon;
using GorillaGameModes;
using NXO.Utilities;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NXO.Mods.Categories;

public class Gamemode
{
	[CompilerGenerated]
	private sealed class CapturedVariables460
	{
		public int actorNumber;

		internal bool PaintbrawlKillPlayer_Lambda0(PhotonPlayer p)
		{
			return p.ActorNumber == actorNumber;
		}
	}

	public static float CapturedVariables460_Value_01;

	private static float CapturedVariables460_Value_02;

	private static float CapturedVariables460_Value_04;

	public static float CapturedVariables460_Value_03;

	public static int CapturedVariables460_Index_01;

	public static readonly Dictionary<int, float> CapturedVariables460_Lookup_01 = new Dictionary<int, float>();

	public static void PaintbrawlGodmode()
	{
		if (Variables.IsMasterClient())
		{
			((GorillaPaintbrawlManager)GorillaGameManager.instance).playerLives[PhotonNetwork.LocalPlayer.ActorNumber] = 4;
			Variables.Variables_Reference_06.disableMovement = false;
		}
	}

	public static void UntagAll()
	{
		if (Variables.IsMasterClient())
		{
			PhotonPlayer[] playerList = PhotonNetwork.PlayerList;
			for (int i = 0; i < playerList.Length; i++)
			{
				UntagPlayer((NetPlayer)playerList[i]);
			}
		}
	}

	public static void ApplyGuardianThrow(NetPlayer victim, Vector3 velocity)
	{
		if (victim == null)
		{
			return;
		}
		if (!(((Vector3)velocity).sqrMagnitude > 400f))
		{
			Vector3 val = velocity;
			velocity = val;
			GorillaGuardianManager val2 = (GorillaGuardianManager)GorillaGameManager.instance;
			if (!val2.IsPlayerGuardian(NetworkSystem.Instance.LocalPlayer))
			{
				goto Branch_00cd;
			}
		}
		else
		{
			Vector3 val3 = ((Vector3)velocity).normalized * 20f;
			velocity = val3;
			GorillaGuardianManager val2 = (GorillaGuardianManager)GorillaGameManager.instance;
			if (!val2.IsPlayerGuardian(NetworkSystem.Instance.LocalPlayer))
			{
				goto Branch_00cd;
			}
		}
		VRRig val4 = RigManager.FindRig(victim);
		if (!((Object)(object)val4 == (Object)null))
		{
			RigManager.GetNetworkView(val4).SendRPC("GrabbedByPlayer", victim, new object[3] { true, false, false });
			RigManager.GetNetworkView(val4).SendRPC("DroppedByPlayer", victim, new object[1] { velocity });
			Safety.ResetNetworkLimits();
		}
		return;
		Branch_00cd:
		NotificationLib.ShowNotification(NotificationLib.NotificationType.Error, "Must Be Guardian");
	}

	public static void GuardianBringAllToPointer()
	{
		if (GunLib.GunGrips)
		{
			GunLib.UpdateGunRaycast();
			if (GunLib.GunTriggers)
			{
				if (!(Time.time > CapturedVariables460_Value_04))
				{
					return;
				}
				CapturedVariables460_Value_04 = Time.time + 0.1f;
				using IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.Where((VRRig plr) => !plr.isLocal).GetEnumerator();
				if (enumerator.MoveNext())
				{
					do
					{
						VRRig val2 = enumerator.Current;
						NetPlayer victim = RigManager.GetPlayer(enumerator.Current);
						Vector3 val = ((RaycastHit)GunLib.GunLib_Reference_07).point - ((Component)val2).transform.position;
						ApplyGuardianThrow(victim, ((Vector3)val).normalized * 20f);
						Safety.ResetNetworkLimits();
					}
					while (enumerator.MoveNext());
				}
				return;
			}
			((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = true;
		}
		else
		{
			GunLib.SetGunVisualsVisible(isVisible: false);
		}
	}

	public static void PaintbrawlKillSelf()
	{
		EliminatePaintbrawlPlayer(PhotonNetwork.LocalPlayer.ActorNumber, Variables.Variables_Reference_09.offlineVRRig);
	}

	public static void GuardianGrabAll()
	{
		if (!InputHandler.IsRightGripPressed() || Time.time <= CapturedVariables460_Value_02)
		{
			return;
		}
		CapturedVariables460_Value_02 = Time.time + 0.1f;
		using IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.Where((VRRig r) => !r.isLocal).GetEnumerator();
		if (enumerator.MoveNext())
		{
			do
			{
				VRRig val2 = enumerator.Current;
				NetPlayer victim = RigManager.GetPlayer(enumerator.Current);
				Vector3 val = ((Component)Variables.Variables_Reference_09.bodyCollider).transform.position - ((Component)val2).transform.position;
				ApplyGuardianThrow(victim, ((Vector3)val).normalized * 20f);
			}
			while (enumerator.MoveNext());
		}
	}

	public static void TagAura()
	{
		using IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.GetEnumerator();
		if (!enumerator.MoveNext())
		{
			return;
		}
		while (true)
		{
			VRRig current = enumerator.Current;
			if (RigManager.IsTagged(VRRig.LocalRig) && !RigManager.IsTagged(current) && Vector3.Distance(current.bodyTransform.position, ((Component)Variables.Variables_Reference_09.bodyCollider).transform.position) < 4f)
			{
				Variables.Variables_Reference_06.RightHand.controllerTransform.position = current.headMesh.transform.position;
				PhotonSerializer.SendViewUpdate();
				PunExtensions.GetPhotonView(GameObject.Find("Player Objects/RigCache/Network Parent/GameMode(Clone)")).RPC("RPC_ReportTag", (RpcTarget)0, new object[1] { current.Creator.ActorNumber });
				PhotonNetwork.SendAllOutgoingCommands();
				((PhotonPeer)PhotonNetwork.NetworkingClient.LoadBalancingPeer).SendAcksOnly();
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

	public static void TagRig(VRRig rig)
	{
		if (!((Object)(object)Variables.Variables_Reference_09 == (Object)null))
		{
			if ((Object)(object)rig == (Object)null || (Object)(object)rig == (Object)(object)VRRig.LocalRig || RigManager.IsTagged(rig) || !RigManager.IsTagged(VRRig.LocalRig))
			{
				((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = true;
				return;
			}
			((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = false;
			((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position = ((Component)rig).transform.position;
			PhotonSerializer.SendViewUpdate();
			PunExtensions.GetPhotonView(GameObject.Find("Player Objects/RigCache/Network Parent/GameMode(Clone)")).RPC("RPC_ReportTag", (RpcTarget)0, new object[1] { rig.Creator.ActorNumber });
			PhotonNetwork.SendAllOutgoingCommands();
			((PhotonPeer)PhotonNetwork.NetworkingClient.LoadBalancingPeer).SendAcksOnly();
			((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = true;
			Safety.ResetNetworkLimits();
		}
	}

	public static void PaintbrawlKillAll()
	{
		if (Variables.IsMasterClient())
		{
			PhotonPlayer[] playerList = PhotonNetwork.PlayerList;
			int num = 0;
			while (num < playerList.Length)
			{
				PhotonPlayer val = playerList[num];
				if (playerList[num].ActorNumber != PhotonNetwork.LocalPlayer.ActorNumber)
				{
					EliminatePaintbrawlPlayer(val.ActorNumber, null);
					num++;
				}
				else
				{
					num++;
				}
			}
		}
		else
		{
			VRRig val2 = (from r in VRRigCache.ActiveRigs
				where !r.isLocal
				orderby Random.value
				select r).FirstOrDefault();
			if ((Object)(object)val2 != (Object)null)
			{
				EliminatePaintbrawlPlayer(val2.Creator.ActorNumber, val2);
			}
		}
	}

	public static void PaintbrawlRestartGame()
	{
		if (Variables.IsMasterClient())
		{
			((GorillaPaintbrawlManager)GorillaGameManager.instance).BattleEnd();
			GorillaPaintbrawlManager val = (GorillaPaintbrawlManager)GorillaGameManager.instance;
			val.StartBattle();
		}
	}

	public static void AntiTag()
	{
		if (RigManager.IsTagged(VRRig.LocalRig))
		{
			return;
		}
		using IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.GetEnumerator();
		if (!enumerator.MoveNext())
		{
			return;
		}
		while (true)
		{
			VRRig val = enumerator.Current;
			if (RigManager.IsTagged(enumerator.Current))
			{
				if (Vector3.Distance(((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position, ((Component)val).transform.position) < 3f)
				{
					((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = false;
					((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position = new Vector3(999f, 999f, 999f);
					if (!enumerator.MoveNext())
					{
						break;
					}
				}
				else
				{
					((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = true;
					if (!enumerator.MoveNext())
					{
						break;
					}
				}
			}
			else if (!enumerator.MoveNext())
			{
				break;
			}
		}
	}

	public static void PaintbrawlReviveSelf()
	{
		RestorePaintbrawlLives(PhotonNetwork.LocalPlayer.ActorNumber);
	}

	public static void PaintbrawlMatGun()
	{
		if (!Variables.IsMasterClient())
		{
			return;
		}
		if (GunLib.GunGrips)
		{
			GunLib.UpdateGunRaycast();
			Collider collider = ((RaycastHit)GunLib.GunLib_Reference_07).collider;
			GunLib.GunLib_Reference_02 = ((collider != null) ? ((Component)collider).GetComponentInParent<VRRig>() : null);
			if (GunLib.GunTriggers)
			{
				if ((Object)(object)GunLib.GunLib_Reference_06 == (Object)null)
				{
					GunLib.GunLib_Reference_06 = GunLib.GunLib_Reference_02;
					return;
				}
				GorillaPaintbrawlManager val = (GorillaPaintbrawlManager)GorillaGameManager.instance;
				val.playerLives[GunLib.GunLib_Reference_06.Creator.ActorNumber] = 0;
				val.playerLives[GunLib.GunLib_Reference_06.Creator.ActorNumber] = 4;
			}
			else
			{
				GunLib.GunLib_Reference_06 = null;
			}
		}
		else
		{
			GunLib.GunLib_Reference_06 = null;
			GunLib.SetGunVisualsVisible(isVisible: false);
		}
	}

	public static void FlickTag()
	{
		if (!Mouse.current.rightButton.isPressed && !Mouse.current.leftButton.isPressed)
		{
			Ray val = new Ray(Variables.Variables_Reference_06.RightHand.controllerTransform.position, Variables.Variables_Reference_06.RightHand.controllerTransform.forward);
			Physics.Raycast(val, out GunLib.GunLib_Reference_07, 100f);
			GunLib.UpdateGunVisuals(((RaycastHit)GunLib.GunLib_Reference_07).point);
			Collider collider = ((RaycastHit)GunLib.GunLib_Reference_07).collider;
			GunLib.GunLib_Reference_02 = ((collider != null) ? ((Component)collider).GetComponentInParent<VRRig>() : null);
			if (!InputHandler.IsRightTriggerPressed())
			{
				return;
			}
		}
		else
		{
			Ray val = Variables.Variables_Object_13.GetComponent<Camera>().ScreenPointToRay((Vector2)(((InputControl<Vector2>)(object)((Pointer)Mouse.current).position).ReadValue()));
			Physics.Raycast(val, out GunLib.GunLib_Reference_07, 100f);
			GunLib.UpdateGunVisuals(((RaycastHit)GunLib.GunLib_Reference_07).point);
			Collider collider2 = ((RaycastHit)GunLib.GunLib_Reference_07).collider;
			GunLib.GunLib_Reference_02 = ((collider2 != null) ? ((Component)collider2).GetComponentInParent<VRRig>() : null);
			if (!InputHandler.IsRightTriggerPressed())
			{
				return;
			}
		}
		if (RigManager.IsTagged(VRRig.LocalRig))
		{
			Variables.Variables_Reference_06.RightHand.controllerTransform.position = ((RaycastHit)GunLib.GunLib_Reference_07).point;
			PhotonSerializer.SendViewUpdate();
			PunExtensions.GetPhotonView(GameObject.Find("Player Objects/RigCache/Network Parent/GameMode(Clone)")).RPC("RPC_ReportTag", (RpcTarget)0, new object[1] { GunLib.GunLib_Reference_02.Creator.ActorNumber });
			PhotonNetwork.SendAllOutgoingCommands();
			((PhotonPeer)PhotonNetwork.NetworkingClient.LoadBalancingPeer).SendAcksOnly();
		}
	}

	public static void PaintbrawlReviveGun()
	{
		if (!Variables.IsMasterClient())
		{
			return;
		}
		if (GunLib.GunGrips)
		{
			GunLib.UpdateGunRaycast();
			Collider collider = ((RaycastHit)GunLib.GunLib_Reference_07).collider;
			GunLib.GunLib_Reference_02 = ((collider != null) ? ((Component)collider).GetComponentInParent<VRRig>() : null);
			if (GunLib.GunTriggers)
			{
				if ((Object)(object)GunLib.GunLib_Reference_06 == (Object)null)
				{
					GunLib.GunLib_Reference_06 = GunLib.GunLib_Reference_02;
				}
				else
				{
					RestorePaintbrawlLives(GunLib.GunLib_Reference_06.Creator.ActorNumber);
				}
			}
			else
			{
				GunLib.GunLib_Reference_06 = null;
			}
		}
		else
		{
			GunLib.GunLib_Reference_06 = null;
			GunLib.SetGunVisualsVisible(isVisible: false);
		}
	}

	public static void GuardianSelf()
	{
		if (!Variables.IsMasterClient() || ((GorillaGuardianManager)GorillaGameManager.instance).IsPlayerGuardian(NetworkSystem.Instance.LocalPlayer))
		{
			return;
		}
		TappableGuardianIdol[] array = Variables.FindObjectsCached<TappableGuardianIdol>(false);
		int num = 0;
		while (num < array.Length)
		{
			TappableGuardianIdol val = array[num];
			GorillaGuardianZoneManager val2 = ReflectionCompat.GetField<GorillaGuardianZoneManager>(val, "zoneManager");
			if (((Object)(object)((Tappable)val).manager) && ((Object)(object)((NetworkSceneObject)((Tappable)val).manager).photonView) && !val.isChangingPositions)
			{
				if (val2 != null && val2.IsZoneValid() && ((Object)(object)((Tappable)val).manager) && val2.CurrentGuardian == null)
				{
					val2.SetGuardian(NetworkSystem.Instance.LocalPlayer);
					break;
				}
				num++;
			}
			else
			{
				num++;
			}
		}
	}

	public static void PaintbrawlKillGun()
	{
		if (GunLib.GunGrips)
		{
			GunLib.UpdateGunRaycast();
			Collider collider = ((RaycastHit)GunLib.GunLib_Reference_07).collider;
			GunLib.GunLib_Reference_02 = ((collider != null) ? ((Component)collider).GetComponentInParent<VRRig>() : null);
			if (GunLib.GunTriggers)
			{
				if ((Object)(object)GunLib.GunLib_Reference_06 == (Object)null)
				{
					GunLib.GunLib_Reference_06 = GunLib.GunLib_Reference_02;
				}
				else
				{
					EliminatePaintbrawlPlayer(GunLib.GunLib_Reference_06.Creator.ActorNumber, GunLib.GunLib_Reference_06);
				}
			}
			else
			{
				GunLib.GunLib_Reference_06 = null;
			}
		}
		else
		{
			GunLib.GunLib_Reference_06 = null;
			GunLib.SetGunVisualsVisible(isVisible: false);
		}
	}

	public Gamemode()
	{
	}

	public static void PaintbrawlStartGame()
	{
		if (Variables.IsMasterClient())
		{
			((GorillaPaintbrawlManager)GorillaGameManager.instance).StartBattle();
		}
	}

	public static void TryRemoveTagSelf()
	{
		if (!PhotonNetwork.InRoom)
		{
			NotificationLib.ShowNotification(NotificationLib.NotificationType.Error, "Not In A Room");
			return;
		}
		if (!RigManager.IsTagged(VRRig.LocalRig))
		{
			NotificationLib.ShowNotification(NotificationLib.NotificationType.Error, "You're Not Tagged");
			return;
		}
		Room.Reconnect();
		SetNoTagOnJoinEnabled(setActive: true);
	}

	public static void PaintbrawlEndGame()
	{
		if (Variables.IsMasterClient())
		{
			((GorillaPaintbrawlManager)GorillaGameManager.instance).BattleEnd();
		}
	}

	public static void RestorePaintbrawlLives(int actorNumber)
	{
		if (Variables.IsMasterClient())
		{
			((GorillaPaintbrawlManager)GorillaGameManager.instance).playerLives[actorNumber] = 4;
		}
	}

	public static void RemoveGuardianAll()
	{
		if (!Variables.IsMasterClient())
		{
			return;
		}
		using IEnumerator<GorillaGuardianZoneManager> enumerator = GorillaGuardianZoneManager.zoneManagers.Where((GorillaGuardianZoneManager gorillaGuardianZoneManager) => ((Behaviour)gorillaGuardianZoneManager).enabled && gorillaGuardianZoneManager.IsZoneValid()).GetEnumerator();
		if (enumerator.MoveNext())
		{
			do
			{
				enumerator.Current.SetGuardian((NetPlayer)null);
			}
			while (enumerator.MoveNext());
		}
	}

	public static void PaintbrawlSpamBalloonSelf()
	{
		if (Variables.IsMasterClient() && !(Time.time < CapturedVariables460_Value_03))
		{
			CapturedVariables460_Value_03 = Time.time + 0.1f;
			SetRandomPaintbrawlLives(PhotonNetwork.LocalPlayer.ActorNumber);
		}
	}

	public static void GuardianReleaseAll()
	{
		if (!InputHandler.IsRightTriggerPressed() || Time.time <= CapturedVariables460_Value_04)
		{
			return;
		}
		CapturedVariables460_Value_04 = Time.time + 0.1f;
		if (!((GorillaGuardianManager)GorillaGameManager.instance).IsPlayerGuardian(NetworkSystem.Instance.LocalPlayer))
		{
			NotificationLib.ShowNotification(NotificationLib.NotificationType.Error, "Must Be Guardian");
			return;
		}
		using (IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.Where((VRRig r) => !r.isLocal).GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				do
				{
					RigManager.GetNetworkView(enumerator.Current).SendRPC("DroppedByPlayer", (RpcTarget)1, new object[1] { Vector3.zero });
				}
				while (enumerator.MoveNext());
			}
		}
		Safety.ResetNetworkLimits();
	}

	public static void PaintbrawlSpamBalloonAll()
	{
		if (Variables.IsMasterClient() && !(Time.time < CapturedVariables460_Value_03))
		{
			CapturedVariables460_Value_03 = Time.time + 0.1f;
			PhotonPlayer[] playerList = PhotonNetwork.PlayerList;
			for (int i = 0; i < playerList.Length; i++)
			{
				SetRandomPaintbrawlLives(playerList[i].ActorNumber);
			}
		}
	}

	public static void RemoveGuardianSelf()
	{
		if (!Variables.IsMasterClient())
		{
			return;
		}
		using IEnumerator<GorillaGuardianZoneManager> enumerator = (from gorillaGuardianZoneManager in GorillaGuardianZoneManager.zoneManagers
			where ((Behaviour)gorillaGuardianZoneManager).enabled && gorillaGuardianZoneManager.IsZoneValid()
			where gorillaGuardianZoneManager.CurrentGuardian == NetworkSystem.Instance.LocalPlayer
			select gorillaGuardianZoneManager).GetEnumerator();
		if (enumerator.MoveNext())
		{
			do
			{
				enumerator.Current.SetGuardian((NetPlayer)null);
			}
			while (enumerator.MoveNext());
		}
	}

	public static void SetNoTagOnJoinEnabled(bool setActive)
	{
		Hashtable customProperties = PhotonNetwork.LocalPlayer.CustomProperties;
		if (customProperties != null && ((Dictionary<object, object>)(object)customProperties).ContainsKey((object)"didTutorial"))
		{
			object obj = customProperties[(object)"didTutorial"];
			if (obj is bool && (bool)obj == !setActive)
			{
				return;
			}
		}
		Hashtable val = new Hashtable();
		((Dictionary<object, object>)(object)val).Add((object)"didTutorial", (object)(!setActive));
		PhotonNetwork.LocalPlayer.SetCustomProperties(val, (Hashtable)null, (WebFlags)null);
		PlayerPrefs.SetString("didTutorial", setActive ? "" : "done");
		PlayerPrefs.Save();
	}

	public static void PaintbrawlReviveAll()
	{
		if (Variables.IsMasterClient())
		{
			PhotonPlayer[] playerList = PhotonNetwork.PlayerList;
			for (int i = 0; i < playerList.Length; i++)
			{
				RestorePaintbrawlLives(playerList[i].ActorNumber);
			}
		}
	}

	public static void GuardianOrbitAll()
	{
		if (InputHandler.IsRightTriggerPressed() && !(Time.time <= CapturedVariables460_Value_04))
		{
			CapturedVariables460_Value_04 = Time.time + 0.2f;
			VRRig[] array = VRRigCache.ActiveRigs.Where((VRRig r) => !r.isLocal).ToArray();
			Vector3 val = default(Vector3);
			for (int num = 0; num < array.Length; num++)
			{
				float num2 = (360f / (float)array.Length * (float)num + Time.time) * (MathF.PI / 180f);
				val = new Vector3(Mathf.Cos(num2) * 5f, 2f, Mathf.Sin(num2) * 5f);
				Vector3 val2 = ((Component)Variables.Variables_Reference_09.headCollider).transform.position + val;
				ApplyGuardianThrow(RigManager.GetPlayer(array[num]), val2 - ((Component)array[num]).transform.position);
			}
			Safety.ResetNetworkLimits();
		}
	}

	public static void UntagGun()
	{
		if (!Variables.IsMasterClient())
		{
			return;
		}
		if (GunLib.GunGrips)
		{
			GunLib.UpdateGunRaycast();
			Collider collider = ((RaycastHit)GunLib.GunLib_Reference_07).collider;
			GunLib.GunLib_Reference_02 = ((collider != null) ? ((Component)collider).GetComponentInParent<VRRig>() : null);
			if (GunLib.GunTriggers)
			{
				if ((Object)(object)GunLib.GunLib_Reference_06 == (Object)null)
				{
					GunLib.GunLib_Reference_06 = GunLib.GunLib_Reference_02;
				}
				else
				{
					UntagPlayer(RigManager.GetPlayer(GunLib.GunLib_Reference_06));
				}
			}
			else
			{
				GunLib.GunLib_Reference_06 = null;
			}
		}
		else
		{
			GunLib.GunLib_Reference_06 = null;
			GunLib.SetGunVisualsVisible(isVisible: false);
		}
	}

	public static void GuardianFlingGun()
	{
		if (GunLib.GunGrips)
		{
			GunLib.UpdateGunRaycast();
			Collider collider = ((RaycastHit)GunLib.GunLib_Reference_07).collider;
			GunLib.GunLib_Reference_02 = ((collider != null) ? ((Component)collider).GetComponentInParent<VRRig>() : null);
			if (GunLib.GunTriggers)
			{
				if ((Object)(object)GunLib.GunLib_Reference_06 == (Object)null)
				{
					GunLib.GunLib_Reference_06 = GunLib.GunLib_Reference_02;
				}
				else
				{
					ApplyGuardianThrow(RigManager.GetPlayer(GunLib.GunLib_Reference_06), new Vector3(0f, 99999f, 0f));
				}
			}
			else
			{
				GunLib.GunLib_Reference_06 = null;
			}
		}
		else
		{
			GunLib.GunLib_Reference_06 = null;
			GunLib.SetGunVisualsVisible(isVisible: false);
		}
	}

	public static void TagSelf()
	{
		if (!RigManager.IsTagged(VRRig.LocalRig))
		{
			using IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.GetEnumerator();
			if (!enumerator.MoveNext())
			{
				return;
			}
			while (true)
			{
				VRRig val = enumerator.Current;
				if (RigManager.IsTagged(enumerator.Current))
				{
					((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = false;
					((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position = val.rightHandTransform.position;
					Vector3 position = ((Component)val).transform.position;
					Vector3 position2 = Variables.Variables_Reference_09.offlineVRRig.head.rigTarget.position;
					if (Vector3.Distance(position, position2) < 1.667f)
					{
						Variables.Variables_Reference_06.LeftHand.controllerTransform.position = position;
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
				else if (!enumerator.MoveNext())
				{
					break;
				}
			}
			return;
		}
		((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = true;
	}

	public static void SetPaintbrawlNoDelayEnabled(bool enable)
	{
		if (Variables.IsMasterClient())
		{
			GorillaPaintbrawlManager val = (GorillaPaintbrawlManager)GorillaGameManager.instance;
			val.hitCooldown = (enable ? 0f : 3f);
			val.tagCoolDown = (enable ? 0f : 5f);
			val.stunGracePeriod = (enable ? 0f : 2f);
		}
	}

	public static void TagAll()
	{
		if (!InputHandler.IsRightTriggerPressed())
		{
			return;
		}
		using IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.GetEnumerator();
		if (enumerator.MoveNext())
		{
			do
			{
				TagRig(enumerator.Current);
			}
			while (enumerator.MoveNext());
		}
	}

	public static void TagGun()
	{
		if (GunLib.TrySelectRig())
		{
			TagRig(GunLib.GunLib_Reference_06);
		}
	}

	public static void UntagPlayer(NetPlayer plr)
	{
		if (!PhotonNetwork.InRoom || (Object)(object)GorillaGameManager.instance == (Object)null)
		{
			return;
		}
		int num = (int)GorillaGameManager.instance.GameType() - 1;
		num = (((uint)num <= 10u) ? num : 11) + 118;
		int num2 = num;
		if (num2 != 119)
		{
			GorillaTagManager val = (GorillaTagManager)GorillaGameManager.instance;
			if (val.isCurrentlyTag && val.currentIt == plr)
			{
				val.currentIt = null;
			}
			else if (!val.isCurrentlyTag && val.currentInfected.Contains(plr))
			{
				val.currentInfected.Remove(plr);
			}
		}
	}

	public static void SetRandomPaintbrawlLives(int actorNumber)
	{
		if (Variables.IsMasterClient())
		{
			((GorillaPaintbrawlManager)GorillaGameManager.instance).playerLives[actorNumber] = Random.Range(0, 4);
		}
	}

	public static void EliminatePaintbrawlPlayer(int actorNumber, VRRig rig)
	{
		CapturedVariables460 LocalScope7 = new CapturedVariables460();
		LocalScope7.actorNumber = actorNumber;
		if (Variables.IsMasterClient())
		{
			((GorillaPaintbrawlManager)GorillaGameManager.instance).playerLives[LocalScope7.actorNumber] = 0;
			return;
		}
		PhotonPlayer val;
		int num;
		if (CapturedVariables460_Lookup_01.TryGetValue(LocalScope7.actorNumber, out var value))
		{
			if (Time.time < value)
			{
				return;
			}
			CapturedVariables460_Lookup_01[LocalScope7.actorNumber] = Time.time + 3.1f;
			val = PhotonNetwork.PlayerList.First((PhotonPlayer p) => p.ActorNumber == LocalScope7.actorNumber);
			num = 0;
		}
		else
		{
			CapturedVariables460_Lookup_01[LocalScope7.actorNumber] = Time.time + 3.1f;
			val = PhotonNetwork.PlayerList.First((PhotonPlayer p) => p.ActorNumber == LocalScope7.actorNumber);
			num = 0;
		}
		if (num < 10)
		{
			do
			{
				object networkHandler = ReflectionCompat.GetStaticField<object>(typeof(GameMode), "activeNetworkHandler");
				ReflectionCompat.Invoke(networkHandler, "SendRPC", "RPC_ReportSlingshotHit", false, new object[3]
				{
					val,
					((Component)rig).transform.position,
					CapturedVariables460_Index_01
				});
				Safety.ResetNetworkLimits();
				CapturedVariables460_Index_01++;
				num++;
			}
			while (num < 10);
		}
	}

	public static void PaintbrawlSpamBalloonGun()
	{
		if (!Variables.IsMasterClient())
		{
			return;
		}
		if (GunLib.GunGrips)
		{
			GunLib.UpdateGunRaycast();
			Collider collider = ((RaycastHit)GunLib.GunLib_Reference_07).collider;
			GunLib.GunLib_Reference_02 = ((collider != null) ? ((Component)collider).GetComponentInParent<VRRig>() : null);
			if (GunLib.GunTriggers)
			{
				if ((Object)(object)GunLib.GunLib_Reference_06 == (Object)null)
				{
					GunLib.GunLib_Reference_06 = GunLib.GunLib_Reference_02;
				}
				else if (Time.time >= CapturedVariables460_Value_03)
				{
					CapturedVariables460_Value_03 = Time.time + 0.1f;
					SetRandomPaintbrawlLives(GunLib.GunLib_Reference_06.Creator.ActorNumber);
				}
			}
			else
			{
				GunLib.GunLib_Reference_06 = null;
			}
		}
		else
		{
			GunLib.GunLib_Reference_06 = null;
			GunLib.SetGunVisualsVisible(isVisible: false);
		}
	}

	public static void PaintbrawlMatAll()
	{
		if (Variables.IsMasterClient())
		{
			PhotonPlayer[] playerList = PhotonNetwork.PlayerList;
			foreach (PhotonPlayer val in playerList)
			{
				GorillaPaintbrawlManager val2 = (GorillaPaintbrawlManager)GorillaGameManager.instance;
				val2.playerLives[val.ActorNumber] = 0;
				val2.playerLives[val.ActorNumber] = 4;
			}
		}
	}

	public static void GuardianGun()
	{
		if (!Variables.IsMasterClient() || ((GorillaGuardianManager)GorillaGameManager.instance).IsPlayerGuardian(RigManager.GetPlayer(GunLib.GunLib_Reference_06)))
		{
			return;
		}
		TappableGuardianIdol[] array = Variables.FindObjectsCached<TappableGuardianIdol>(false);
		int num = 0;
		while (num < array.Length)
		{
			TappableGuardianIdol val = array[num];
			GorillaGuardianZoneManager val2 = ReflectionCompat.GetField<GorillaGuardianZoneManager>(val, "zoneManager");
			if (((Object)(object)((Tappable)val).manager) && ((Object)(object)((NetworkSceneObject)((Tappable)val).manager).photonView) && !val.isChangingPositions)
			{
				if (val2 != null && val2.IsZoneValid() && ((Object)(object)((Tappable)val).manager) && val2.CurrentGuardian == null)
				{
					val2.SetGuardian(RigManager.GetPlayer(GunLib.GunLib_Reference_06));
					break;
				}
				num++;
			}
			else
			{
				num++;
			}
		}
	}

	public static void GuardianFlingAll()
	{
		if (InputHandler.IsRightTriggerPressed() && !(Time.time <= CapturedVariables460_Value_04))
		{
			CapturedVariables460_Value_04 = Time.time + 0.1f;
			ApplyGuardianThrowToGroup((RpcTarget)1, new Vector3(0f, 100000000f, 0f));
			Safety.ResetNetworkLimits();
		}
	}

	public static void RemoveGuardianGun()
	{
		if (!Variables.IsMasterClient() || !GunLib.TrySelectRig())
		{
			return;
		}
		using IEnumerator<GorillaGuardianZoneManager> enumerator = (from gorillaGuardianZoneManager in GorillaGuardianZoneManager.zoneManagers
			where ((Behaviour)gorillaGuardianZoneManager).enabled && gorillaGuardianZoneManager.IsZoneValid()
			where gorillaGuardianZoneManager.CurrentGuardian == RigManager.GetPlayer(GunLib.GunLib_Reference_06)
			select gorillaGuardianZoneManager).GetEnumerator();
		if (enumerator.MoveNext())
		{
			do
			{
				enumerator.Current.SetGuardian((NetPlayer)null);
			}
			while (enumerator.MoveNext());
		}
	}

	public static void AutoGuardian()
	{
		if (!PhotonNetwork.InRoom || (int)GorillaGameManager.instance.GameType() != 8)
		{
			((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = true;
			return;
		}
		if (((GorillaGuardianManager)GorillaGameManager.instance).IsPlayerGuardian(NetworkSystem.Instance.LocalPlayer))
		{
			((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = true;
			return;
		}
		TappableGuardianIdol[] array = Variables.FindObjectsCached<TappableGuardianIdol>(false);
		foreach (TappableGuardianIdol val in array)
		{
			GorillaGuardianZoneManager val2 = ReflectionCompat.GetField<GorillaGuardianZoneManager>(val, "zoneManager");
		if (((Object)(object)((NetworkSceneObject)((Tappable)val).manager).photonView) && !val.isChangingPositions && val2 != null && val2.IsZoneValid())
			{
				((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = false;
				((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position = ((Component)val).transform.position + Random.insideUnitSphere * 0.1f;
				((Component)Variables.Variables_Reference_09.offlineVRRig.leftHand.rigTarget).transform.position = ((Component)val).transform.position;
				((Component)Variables.Variables_Reference_09.offlineVRRig.rightHand.rigTarget).transform.position = ((Component)val).transform.position;
				if (Time.time > CapturedVariables460_Value_01)
				{
					float currentActivationTime = ReflectionCompat.GetField(val2, "_currentActivationTime", 0f);
					float requiredActivationTime = ReflectionCompat.GetField(val2, "requiredActivationTime", 0f);
					CapturedVariables460_Value_01 = Time.time + ((currentActivationTime >= requiredActivationTime - 1f) ? 0f : 0.2f);
					((Tappable)val).OnTap(Random.Range(0f, 1f));
					Safety.ResetNetworkLimits();
				}
				return;
			}
		}
		((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = true;
	}

	public static void ApplyGuardianThrowToGroup(RpcTarget target, Vector3 velocity)
	{
		if (!(((Vector3)velocity).sqrMagnitude > 400f))
		{
			Vector3 val = velocity;
			velocity = val;
			GorillaGuardianManager val2 = (GorillaGuardianManager)GorillaGameManager.instance;
			if (!val2.IsPlayerGuardian(NetworkSystem.Instance.LocalPlayer))
			{
				goto Branch_00af;
			}
		}
		else
		{
			Vector3 val3 = ((Vector3)velocity).normalized * 20f;
			velocity = val3;
			GorillaGuardianManager val2 = (GorillaGuardianManager)GorillaGameManager.instance;
			if (!val2.IsPlayerGuardian(NetworkSystem.Instance.LocalPlayer))
			{
				goto Branch_00af;
			}
		}
		int num = (int)target;
		num = (((uint)num <= 2u) ? num : 3) + 204;
		int num2 = num;
		IEnumerable<VRRig> source = ((num2 == 205) ? VRRigCache.ActiveRigs.Where((VRRig r) => !r.isLocal) : VRRigCache.ActiveRigs);
		using (IEnumerator<VRRig> enumerator = source.Where((VRRig r) => (Object)(object)r != (Object)null).GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				while (true)
				{
					VRRig vrrig = enumerator.Current;
					NetPlayer val4 = RigManager.GetPlayer(enumerator.Current);
					if (val4 == null)
					{
						break;
					}
					RigManager.GetNetworkView(vrrig).SendRPC("GrabbedByPlayer", val4, new object[3] { true, false, false });
					RigManager.GetNetworkView(vrrig).SendRPC("DroppedByPlayer", val4, new object[1] { velocity });
					if (!enumerator.MoveNext())
					{
						goto EndBranch_01ee;
					}
				}
				continue;
				EndBranch_01ee:
				break;
			}
		}
		Safety.ResetNetworkLimits();
		return;
		Branch_00af:
		NotificationLib.ShowNotification(NotificationLib.NotificationType.Error, "Must Be Guardian");
	}

	public static void GuardianAll()
	{
		if (!Variables.IsMasterClient())
		{
			return;
		}
		int num = 0;
		using IEnumerator<GorillaGuardianZoneManager> enumerator = GorillaGuardianZoneManager.zoneManagers.Where((GorillaGuardianZoneManager gorillaGuardianZoneManager) => ((Behaviour)gorillaGuardianZoneManager).enabled && gorillaGuardianZoneManager.IsZoneValid()).GetEnumerator();
		if (enumerator.MoveNext())
		{
			do
			{
				enumerator.Current.SetGuardian((NetPlayer)PhotonNetwork.PlayerList[num]);
				num++;
			}
			while (enumerator.MoveNext());
		}
	}
}
