using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using NXO.Utilities;
using Photon.Pun;
using UnityEngine;

namespace NXO.Mods.Categories;

public class SuperInfection
{
	public enum GadgetTypes
	{
		Thrusters,
		LongArms,
		Dash,
		Platforms,
		Blasters
	}

	private static float GadgetTypes_Value_01;

	public static SIPlayer GadgetTypes_Reference_01;

	public static Transform GadgetTypes_Transform_01;

	public const int WeirdGear = 1573124711;

	public const int StrangeWood = -894667703;

	public const int BouncySand = -1111610567;

	public const int FloppyMetal = -1409076879;

	public const int VibratingSpring = 1618940484;

	public const int MonkeyIdol = 1880272606;

	public const int WristJetJet = 1551901997;

	public const int WristJetPropellor = -1912435955;

	public const int StiltFixed = 1447779317;

	public const int StiltTurkey = 686793174;

	public const int StiltFixedShort = -827046453;

	public const int StiltMotorized2 = 1428761418;

	public const int StiltMotorized3 = 1996041101;

	public const int StiltFixedLong = -1906115882;

	public const int StiltExtendo = 683567723;

	public const int DashYoyo = 1799386883;

	public const int PlatformDeployer = -1236344563;

	public const int PlatformDeployerBouncy = 1657474495;

	public const int TentacleArm = 621310034;

	public const int TentacleArmCrawler = 1814413281;

	public const int TentacleArmStrider = 2060634971;

	public const int AirJuke = -1196783306;

	public const int AirGrab = -2029993207;

	public const int LaserZipline = -1581486942;

	public const int WeakBlaster = 1312505709;

	public const int ChargeBlaster = 1469243263;

	public const int MegaChargeBlaster = -1529067748;

	public const int BlastLobber = -122499862;

	public const int LongBlaster = -108912318;

	private static SuperInfectionManager SIM
	{
		get
		{
			return SuperInfectionManager.activeSuperInfectionManager;
		}
	}

	public static List<SIGadget> GetAllGadgets()
	{
		List<SIGadget> list = new List<SIGadget>();
		using (List<GameEntity>.Enumerator enumerator = SIM.gameEntityManager.GetGameEntities().GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				while (true)
				{
					SIGadget component = ((Component)enumerator.Current).GetComponent<SIGadget>();
					if ((Object)(object)component != (Object)null)
					{
						list.Add(component);
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
		return list;
	}

	public static void ResetNoCooldown(SIGadgetDashYoyo instance_)
	{
		object state = ReflectionCompat.GetField<object>(instance_, "_state");
		if (state != null && Convert.ToInt32(state) == 1)
		{
			ReflectionCompat.SetField(instance_, "_state", 0);
		}
	}

	public static GameEntity CreateGadgetForPlayer(SIPlayer player, string gadgetName)
	{
		if ((Object)(object)player == (Object)null)
		{
			return null;
		}
		GameEntityManager gameEntityManager = SIM.gameEntityManager;
		int staticHash = StaticHashExt.GetStaticHash(gadgetName);
		if (!gameEntityManager.FactoryHasEntity(staticHash))
		{
			return null;
		}
		GameEntityId val = gameEntityManager.RequestCreateItem(staticHash, player.gamePlayer.rig.rightHandTransform.position, Quaternion.identity, (long)SIPlayer.LocalPlayer.ActorNr);
		return gameEntityManager.GetGameEntity(val);
	}

	public static void SendPositionRpc(Vector3 position)
	{
		SIM.CallRPC((AuthorityToClientRPC)5, new object[1] { position });
	}

	public static void SpawnBuiltInEntity(SIPlayer player, int entityTypeId, Vector3 spawnPosition, bool shoot = false)
	{
		GameEntityManager gameEntityManager = SIM.gameEntityManager;
		object createdNetId = ReflectionCompat.Invoke(gameEntityManager, "CreateNetId", 1 + gameEntityManager.FactoryGetBuiltInEntityCountById(entityTypeId));
		int num = createdNetId is int netId ? netId : 0;
		gameEntityManager.photonView.RPC("CreateItemRPC", (RpcTarget)1, new object[6]
		{
			new int[1] { num },
			new int[1] { entityTypeId },
			new long[1] { BitPackUtils.PackWorldPosForNetwork(spawnPosition) },
			new int[1] { BitPackUtils.PackQuaternionForNetwork(Quaternion.identity) },
			new long[1],
			new int[1] { -1 }
		});
		if (shoot)
		{
			gameEntityManager.photonView.RPC("ThrowEntityRPC", (RpcTarget)0, new object[8]
			{
				num,
				false,
				player.gamePlayer.rig.rightHandTransform.position,
				player.gamePlayer.rig.rightHandTransform.rotation,
				player.gamePlayer.rig.rightHandTransform.forward * 20f,
				Vector3.zero,
				player.gamePlayer.rig.OwningNetPlayer.GetPlayerRef(),
				PhotonNetwork.Time
			});
		}
		else
		{
			gameEntityManager.photonView.RPC("GrabEntityRPC", (RpcTarget)0, new object[4]
			{
				num,
				false,
				BitPackUtils.PackHandPosRotForNetwork(Vector3.zero, Quaternion.identity),
				player.gamePlayer.rig.OwningNetPlayer.GetPlayerRef()
			});
		}
	}

	public static void ApplyRainbowStilt(SIGadgetStilt instance_)
	{
		Color color = Color.HSVToRGB(Time.time * 0.2f % 1f, 1f, 1f);
		Renderer val = default(Renderer);
		Renderer val2 = default(Renderer);
		if (instance_.tip.TryGetComponent<Renderer>(out val))
		{
			val.material.color = color;
			if (!instance_.midpoint.TryGetComponent<Renderer>(out val2))
			{
				return;
			}
		}
		else if (!instance_.midpoint.TryGetComponent<Renderer>(out val2))
		{
			return;
		}
		val2.material.color = color;
	}

	public static void UnlockAllGadgets()
	{
		bool[][] unlockedTechTreeData = SIProgression.Instance.unlockedTechTreeData;
		foreach (bool[] array in unlockedTechTreeData)
		{
			for (int j = 0; j < array.Length; j++)
			{
				array[j] = true;
			}
		}
	}

	public static void FloatGun()
	{
		if (GunLib.TrySelectRig())
		{
			GadgetTypes_Reference_01 = ((Component)GunLib.GunLib_Reference_06).gameObject.GetComponent<SIPlayer>();
			if (!((Object)(object)GadgetTypes_Reference_01 != (Object)null))
			{
				return;
			}
			((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = false;
			((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position = ((Component)GunLib.GunLib_Reference_06).transform.position + new Vector3(0f, 0.2f, 0f);
			Variables.Variables_Reference_09.rightHandTransform.position = GunLib.GunLib_Reference_06.bodyTransform.position;
			Variables.Variables_Reference_09.leftHandTransform.position = GunLib.GunLib_Reference_06.bodyTransform.position;
			Variables.Variables_Reference_09.leftHandTransform.rotation = Quaternion.Euler(-90f, 0f, 0f);
			Variables.Variables_Reference_09.rightHandTransform.rotation = Quaternion.Euler(-90f, 0f, 0f);
			IEnumerable<SIGadgetBlaster> blasters = from x in ResetGadgetOverrides()
				select ((Component)x).GetComponent<SIGadgetBlaster>() into x
				where (Object)(object)x != (Object)null
				select x;
			foreach (SIGadgetBlaster blaster in blasters)
			{
				ResetNoBlasterCooldown(blaster);
				RedirectBlasterProjectilesToPlayer(blaster, GadgetTypes_Reference_01, CalculateVelocityTowards(Variables.Variables_Reference_09.rightHandTransform.position, ((Component)GadgetTypes_Reference_01).transform.position, 2f), Float: true);
			}
			return;
		}
		((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = true;
	}

	public static void ClearExclusionZones()
	{
		using (IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				while (true)
				{
					SIPlayer val = SIPlayer.Get(enumerator.Current);
					if ((Object)(object)val != (Object)null)
					{
						val.exclusionZoneCount = 0;
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
		SIPlayer.LocalPlayer.exclusionZoneCount = 0;
	}

	public SuperInfection()
	{
	}

	public static void ExtendStilt(SIGadgetStilt instance_)
	{
		if (instance_.TriggerToExtend)
		{
			ReflectionCompat.SetField(instance_, "maxLength", 5f);
			ReflectionCompat.SetField(instance_, "targetLength", 5f);
		}
	}

	public static void DespawnBlasterProjectiles(SIGadgetBlaster blaster)
	{
		using (List<SIGadgetBlasterProjectile>.Enumerator enumerator = blaster.activeProjectiles.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				while (true)
				{
					SIGadgetBlasterProjectile current = enumerator.Current;
					if (!((Object)(object)current != (Object)null))
					{
						break;
					}
					blaster.DespawnProjectile(current);
					if (!enumerator.MoveNext())
					{
						goto EndBranch_0060;
					}
				}
				continue;
				EndBranch_0060:
				break;
			}
		}
		blaster.activeProjectiles.Clear();
	}

	public static void RainBlastersOnPlayer(SIPlayer shooter, SIPlayer target, int blastersPerType = 3)
	{
		int[] array = new int[5] { 1312505709, 1469243263, -1529067748, -122499862, -108912318 };
		GameEntityManager gameEntityManager = SIM.gameEntityManager;
		Vector3 position = target.gamePlayer.rig.headMesh.transform.position;
		int[] array2 = array;
		foreach (int num in array2)
		{
			int num2 = 0;
			if (num2 < blastersPerType)
			{
				do
				{
					Vector3 val = position + new Vector3(Random.Range(-5f, 5f), 6f, Random.Range(-5f, 5f));
					Vector3 val2 = position - val;
					Vector3 normalized = ((Vector3)val2).normalized;
					object createdNetId = ReflectionCompat.Invoke(gameEntityManager, "CreateNetId", 1 + gameEntityManager.FactoryGetBuiltInEntityCountById(num));
					int num3 = createdNetId is int netId ? netId : 0;
					gameEntityManager.photonView.RPC("CreateItemRPC", (RpcTarget)0, new object[6]
					{
						new int[1] { num3 },
						new int[1] { num },
						new long[1] { BitPackUtils.PackWorldPosForNetwork(val) },
						new int[1] { BitPackUtils.PackQuaternionForNetwork(Quaternion.LookRotation(normalized)) },
						new long[1],
						new int[1] { -1 }
					});
					gameEntityManager.photonView.RPC("ThrowEntityRPC", (RpcTarget)0, new object[8]
					{
						num3,
						false,
						val,
						Quaternion.LookRotation(normalized),
						normalized * 20f,
						Vector3.zero,
						shooter.gamePlayer.rig.OwningNetPlayer.GetPlayerRef(),
						PhotonNetwork.Time
					});
					num2++;
				}
				while (num2 < blastersPerType);
			}
		}
		PhotonNetwork.SendAllOutgoingCommands();
	}

	public static void RedirectProjectiles(SIGadgetBlaster blaster, Transform target)
	{
		using List<SIGadgetBlasterProjectile>.Enumerator enumerator = blaster.activeProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			while (true)
			{
				SIGadgetBlasterProjectile current = enumerator.Current;
				if ((Object)(object)current == (Object)null)
				{
					break;
				}
				Rigidbody rb = current.rb;
				Vector3 val = target.position - ((Component)current).transform.position;
				Vector3 val2 = val;
				Vector3 normalized = ((Vector3)val2).normalized;
				val = current.rb.linearVelocity;
				val2 = val;
				rb.linearVelocity = normalized * ((Vector3)val2).magnitude;
				if (!enumerator.MoveNext())
				{
					return;
				}
			}
		}
	}

	public static void ClearPlayerGadgets(SIPlayer player)
	{
		if (!((Object)(object)player == (Object)null))
		{
			SIM.ClearPlayerGadgets(player);
		}
	}

	public static void AddBonusProgress(SIPlayer player, int amount)
	{
		if (!((Object)(object)player == (Object)null) && amount > 0)
		{
			ProgressionData currentProgression = player.CurrentProgression;
			int[] array = new int[currentProgression.currentQuestProgresses.Length + 1];
			Array.Copy(currentProgression.currentQuestProgresses, array, currentProgression.currentQuestProgresses.Length);
			array[^1] = amount;
			player.UpdateProgression(currentProgression.resourceArray, currentProgression.limitedDepositTimeArray, currentProgression.techTreeData, currentProgression.stashedQuests, currentProgression.stashedBonusPoints, currentProgression.bonusProgress + amount, currentProgression.currentQuestIds, array);
			SIPlayer.SetAndBroadcastProgression();
		}
	}

	public static void EnableEntityPhysics()
	{
		using List<GameEntity>.Enumerator enumerator = SIM.gameEntityManager.GetGameEntities().GetEnumerator();
		if (!enumerator.MoveNext())
		{
			return;
		}
		while (true)
		{
			Rigidbody component = ((Component)enumerator.Current).GetComponent<Rigidbody>();
			if ((Object)(object)component != (Object)null)
			{
				component.isKinematic = false;
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

	public static void SpawnGadgetInHand(string gadgetName, bool leftHand = false)
	{
		GameEntityManager gameEntityManager = SIM.gameEntityManager;
		int staticHash = StaticHashExt.GetStaticHash(gadgetName);
		if (gameEntityManager.FactoryHasEntity(staticHash))
		{
			if (!leftHand)
			{
				Vector3 position = Variables.Variables_Reference_09.rightHandTransform.position;
				gameEntityManager.RequestCreateItem(staticHash, position, Quaternion.identity, (long)SIPlayer.LocalPlayer.ActorNr);
			}
			else
			{
				Vector3 position = Variables.Variables_Reference_09.leftHandTransform.position;
				gameEntityManager.RequestCreateItem(staticHash, position, Quaternion.identity, (long)SIPlayer.LocalPlayer.ActorNr);
			}
		}
	}

	public static void ClearPlayerExclusionZone(SIPlayer player)
	{
		player.exclusionZoneCount = 0;
	}

	public static void RegrabFirstGadget(SIPlayer target)
	{
		if ((Object)(object)target == (Object)null || target.activePlayerGadgets.Count == 0)
		{
			return;
		}
		GameEntityManager gameEntityManager = SIM.gameEntityManager;
		GameEntity gameEntityFromNetId = gameEntityManager.GetGameEntityFromNetId(target.activePlayerGadgets[0]);
		if (!((Object)(object)gameEntityFromNetId == (Object)null))
		{
			SIGadget component = ((Component)gameEntityFromNetId).GetComponent<SIGadget>();
			bool flag = false;
			if ((Object)(object)component != (Object)null)
			{
				component.FindAttachedHand(out flag);
				gameEntityManager.RequestGrabEntity(gameEntityFromNetId.id, flag, ((Component)gameEntityFromNetId).transform.localPosition, ((Component)gameEntityFromNetId).transform.localRotation);
			}
			else
			{
				gameEntityManager.RequestGrabEntity(gameEntityFromNetId.id, flag, ((Component)gameEntityFromNetId).transform.localPosition, ((Component)gameEntityFromNetId).transform.localRotation);
			}
		}
	}

	public static void KnockbackAllPlayersAway(float strength)
	{
		foreach (VRRig rig in VRRigCache.ActiveRigs)
		{
			SIPlayer val = SIPlayer.Get(rig);
			if ((Object)(object)val == (Object)null || (Object)(object)val == (Object)(object)SIPlayer.LocalPlayer)
			{
				continue;
			}
			Vector3 val2 = ((Component)rig).transform.position - ((Component)VRRig.LocalRig).transform.position;
			val.PlayerKnockback(((Vector3)val2).normalized * strength, true, false);
		}
	}

	public static void MaxCharge(SIGadgetChargeBlaster instance)
	{
		if (!((Object)(object)instance == (Object)null) && instance.chargeLevels != null && instance.chargeLevels.Length != 0)
		{
			ReflectionCompat.SetField(instance, "currentCharge", instance.chargeLevels[^1].chargeThreshold + 1f);
		}
	}

	public static void RequestGadgetStateRepeatedly(SIGadget gadget, long state, int times)
	{
		if ((Object)(object)gadget == (Object)null)
		{
			return;
		}
		int num = 0;
		if (num < times)
		{
			do
			{
				gadget.gameEntity.RequestState(gadget.gameEntity.id, state);
				num++;
			}
			while (num < times);
		}
	}

	public static void UnlockTechNode(SIPlayer player, int tierIndex, int nodeIndex)
	{
		if (!((Object)(object)player == (Object)null))
		{
			ProgressionData currentProgression = player.CurrentProgression;
			if (!currentProgression.techTreeData[tierIndex][nodeIndex])
			{
				currentProgression.techTreeData[tierIndex][nodeIndex] = true;
				player.UpdateProgression(currentProgression.resourceArray, currentProgression.limitedDepositTimeArray, currentProgression.techTreeData, currentProgression.stashedQuests, currentProgression.stashedBonusPoints, currentProgression.bonusProgress, currentProgression.currentQuestIds, currentProgression.currentQuestProgresses);
				SIPlayer.SetAndBroadcastProgression();
			}
		}
	}

	public static void DispenseGadget(string gadgetNodeName)
	{
		SICombinedTerminal val = Object.FindObjectOfType<SICombinedTerminal>();
		if ((Object)(object)val == (Object)null)
		{
			return;
		}
		int num = -1;
		int num2 = 0;
		if (num2 < val.dispenser.CurrentPage.AllNodes.Count)
		{
			do
			{
				if (val.dispenser.CurrentPage.AllNodes[num2].Value.nickName == gadgetNodeName)
				{
					num = num2;
					break;
				}
				num2++;
			}
			while (num2 < val.dispenser.CurrentPage.AllNodes.Count);
		}
		if (num != -1)
		{
			val.dispenser._currentNode = num;
			val.dispenser.gadgetDispensePosition.position = Variables.Variables_Reference_09.rightHandTransform.position;
			val.dispenser.AuthorityDispenseGadgetForPlayer(SIPlayer.LocalPlayer);
		}
	}

	public static void BroadcastFakeProgression(int[] fakeResources, bool[][] fakeTechTree)
	{
		ProgressionData currentProgression = SIPlayer.LocalPlayer.CurrentProgression;
		SIM.CallRPC((ClientToClientRPC)0, new object[8]
		{
			fakeResources,
			new int[2],
			fakeTechTree,
			255,
			255,
			255,
			currentProgression.currentQuestIds,
			currentProgression.currentQuestProgresses
		});
	}

	public static void ResetPlatformCooldown(SIGadgetPlatformDeployer instance_)
	{
		ReflectionCompat.SetField(instance_, "remainingRechargeTime", 0f);
	}

	public static void MoveGadgetsToPlayer(SIPlayer player)
	{
		if ((Object)(object)player == (Object)null)
		{
			return;
		}
		Vector3 position = ((Component)player.gamePlayer.rig).transform.position;
		using List<GameEntity>.Enumerator enumerator = SIM.gameEntityManager.GetGameEntities().GetEnumerator();
		while (enumerator.MoveNext())
		{
			while (true)
			{
				GameEntity current = enumerator.Current;
				if ((Object)(object)current == (Object)null || (Object)(object)((Component)current).GetComponent<SIGadget>() == (Object)null)
				{
					break;
				}
				((Component)current).transform.position = position + Random.insideUnitSphere * 0.5f;
				if (!enumerator.MoveNext())
				{
					return;
				}
			}
		}
	}

	public static void DestroyPlayerGadgets(SIPlayer player)
	{
		GameEntityManager gameEntityManager = SIM.gameEntityManager;
		int num = 0;
		if (num < player.activePlayerGadgets.Count)
		{
			do
			{
				gameEntityManager.RequestDestroyItem(gameEntityManager.GetEntityIdFromNetId(player.activePlayerGadgets[num]));
				num++;
			}
			while (num < player.activePlayerGadgets.Count);
		}
		player.activePlayerGadgets.Clear();
	}

	public static Vector3 CalculateVelocityTowards(Vector3 currentPos, Vector3 targetPos, float speed)
	{
		Vector3 val = targetPos - currentPos;
		return ((Vector3)val).normalized * speed;
	}

	public static void MaxStashedBonusPoints()
	{
		SIProgression.Instance.stashedBonusPoints = 255;
		SIPlayer.SetAndBroadcastProgression();
	}

	public static void SetBonusProgress(int amount)
	{
		SIProgression.Instance.bonusProgress = amount;
		SIPlayer.SetAndBroadcastProgression();
	}

	private static void RapidFireBlaster(SIGadgetBlaster instance_)
	{
		if ((Object)(object)instance_ == (Object)null || !instance_.LocalEquippedOrActivated || (!InputHandler.IsRightTriggerPressed() && !InputHandler.IsLeftTriggerPressed()))
		{
			return;
		}
		instance_.lastFired = -1f;
		instance_.projectileCount = 0;
		GadgetTypes_Value_01 += Time.deltaTime;
		if (GadgetTypes_Value_01 >= 4E-07f)
		{
			do
			{
				GadgetTypes_Value_01 -= 4E-07f;
				((ControllerInputPoller)ControllerInputPoller.instance).rightControllerIndexFloat = (InputHandler.IsRightTriggerPressed() ? 0f : 0.6f);
				((ControllerInputPoller)ControllerInputPoller.instance).leftControllerIndexFloat = (InputHandler.IsLeftTriggerPressed() ? 0f : 0.6f);
			}
			while (GadgetTypes_Value_01 >= 4E-07f);
		}
	}

	public static void AddTechPoints(SIResource.ResourceType resourceType, int amount)
	{
		if (SIProgression.Instance.resourceDict.ContainsKey(resourceType))
		{
			if ((int)resourceType == 0)
			{
				SIProgression.Instance.resourceDict[resourceType] += amount;
				SIProgression.Instance.AttemptIncrementResource(resourceType);
			}
			else
			{
				SIProgression.Instance.resourceDict[resourceType] += Math.Min(amount, SIProgression.Instance.GetResourceMaxCap(resourceType) - SIProgression.Instance.resourceDict[resourceType]);
				SIProgression.Instance.AttemptIncrementResource(resourceType);
			}
		}
	}

	public static void SpawnUnlockedGadgets(SIPlayer player)
	{
		if ((Object)(object)player == (Object)null)
		{
			return;
		}
		GameEntityManager gameEntityManager = SIM.gameEntityManager;
		SITechTreeSO progressionSO = SIPlayer.progressionSO;
		int num = 0;
		if (num >= player.CurrentProgression.techTreeData.Length)
		{
			return;
		}
		do
		{
			int num2 = 0;
			if (num2 < player.CurrentProgression.techTreeData[num].Length)
			{
				do
				{
					Branch_004e:
					if (player.CurrentProgression.techTreeData[num][num2] && progressionSO.IsValidNode(num, num2))
					{
						SITechTreeNode treeNode = progressionSO.GetTreeNode(num, num2);
						if (treeNode != null && treeNode.IsDispensableGadget)
						{
							int staticHash = StaticHashExt.GetStaticHash(((Object)((Component)treeNode.unlockedGadgetPrefab).gameObject).name);
							if (gameEntityManager.FactoryHasEntity(staticHash))
							{
								gameEntityManager.RequestCreateItem(staticHash, player.gamePlayer.rig.rightHandTransform.position + Random.insideUnitSphere * 0.3f, Quaternion.identity, (long)player.ActorNr);
								num2++;
								if (num2 >= player.CurrentProgression.techTreeData[num].Length)
								{
									break;
								}
								goto Branch_004e;
							}
						}
					}
					num2++;
				}
				while (num2 < player.CurrentProgression.techTreeData[num].Length);
			}
			num++;
		}
		while (num < player.CurrentProgression.techTreeData.Length);
	}

	public static void UnlockFullTree()
	{
		bool[][] unlockedTechTreeData = SIProgression.Instance.unlockedTechTreeData;
		for (int i = 0; i < unlockedTechTreeData.Length; i++)
		{
			int num = 0;
			if (num < unlockedTechTreeData[i].Length)
			{
				do
				{
					unlockedTechTreeData[i][num] = true;
					num++;
				}
				while (num < unlockedTechTreeData[i].Length);
			}
		}
		SIPlayer.SetAndBroadcastProgression();
	}

	public static void ResetFastCharge(SIGadgetChargeBlaster instance_)
	{
		instance_.maxChargeDiff = 9999f;
		((Component)instance_).SendMessage("OnUpdateAuthority", (object)Time.deltaTime, (SendMessageOptions)1);
	}

	public static void CompleteQuests()
	{
		int[] activeQuestIds = SIProgression.Instance.ActiveQuestIds;
		int num = 0;
		if (num < activeQuestIds.Length)
		{
			do
			{
				int num2 = activeQuestIds[num];
				RotatingQuest questById = SIProgression.Instance.questSourceList.GetQuestById(num2);
				ReflectionCompat.Invoke(questById, "SetProgress", questById.requiredOccurenceCount);
				num++;
			}
			while (num < activeQuestIds.Length);
		}
		SIProgression.Instance.SaveQuestProgress();
	}

	public static void GiveAllResources(SIPlayer player)
	{
		if ((Object)(object)player == (Object)null)
		{
			return;
		}
		IEnumerator enumerator = Enum.GetValues(typeof(SIResource.ResourceType)).GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				while (true)
				{
					SIResource.ResourceType val = (SIResource.ResourceType)enumerator.Current;
					if (!SIProgression.Instance.resourceDict.ContainsKey(val))
					{
						break;
					}
					SIProgression.Instance.resourceDict[val] = SIProgression.Instance.GetResourceMaxCap(val);
					if (!enumerator.MoveNext())
					{
						goto EndBranch_00bb;
					}
				}
				continue;
				EndBranch_00bb:
				break;
			}
		}
		finally
		{
			IDisposable disposable = enumerator as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
		SIPlayer.SetAndBroadcastProgression();
	}

	public static void SetProjectileVelocity(SIGadgetBlaster blaster, Vector3 velocity)
	{
		using List<SIGadgetBlasterProjectile>.Enumerator enumerator = blaster.activeProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			while (true)
			{
				SIGadgetBlasterProjectile current = enumerator.Current;
				if (!((Object)(object)current != (Object)null))
				{
					break;
				}
				current.rb.linearVelocity = velocity;
				if (!enumerator.MoveNext())
				{
					return;
				}
			}
		}
	}

	public static void MaxStashedQuests()
	{
		SIProgression.Instance.stashedQuests = 255;
		SIPlayer.SetAndBroadcastProgression();
	}

	public static void ThrowPlayerGadgets(SIPlayer player)
	{
		if ((Object)(object)player == (Object)null || (Object)(object)player.gamePlayer?.rig == (Object)null || !SIM.gameEntityManager.IsAuthority())
		{
			return;
		}
		using List<int>.Enumerator enumerator = new List<int>(player.activePlayerGadgets).GetEnumerator();
		GameEntityManager val = SIM.gameEntityManager;
		while (enumerator.MoveNext())
		{
			while (true)
			{
				int current = enumerator.Current;
				GameEntity gameEntityFromNetId = val.GetGameEntityFromNetId(current);
				if ((Object)(object)gameEntityFromNetId == (Object)null || !val.IsValidNetId(current))
				{
					break;
				}
				bool flag = player.gamePlayer.IsHoldingEntity(gameEntityFromNetId.id, true);
				Vector3 val2 = new Vector3(Random.Range(-1f, 1f), 2.5f, Random.Range(-1f, 1f));
				Vector3 val3 = ((Vector3)val2).normalized * 3f;
				val.photonView.RPC("ThrowEntityRPC", (RpcTarget)0, new object[8]
				{
					current,
					flag,
					((Component)gameEntityFromNetId).transform.position,
					((Component)gameEntityFromNetId).transform.rotation,
					val3,
					Vector3.zero,
					PhotonNetwork.LocalPlayer,
					PhotonNetwork.Time
				});
				if (!enumerator.MoveNext())
				{
					return;
				}
			}
		}
	}

	public static void CompleteAndClaimQuests(SIPlayer player)
	{
		if ((Object)(object)player == (Object)null)
		{
			return;
		}
		ProgressionData currentProgression = player.CurrentProgression;
		SIQuestsList questSourceList = SIProgression.Instance.questSourceList;
		int num = 0;
		if (num < currentProgression.currentQuestIds.Length)
		{
			while (true)
			{
				RotatingQuest questById = questSourceList.GetQuestById(currentProgression.currentQuestIds[num]);
				if (questById != null)
				{
					currentProgression.currentQuestProgresses[num] = questById.requiredOccurenceCount;
					currentProgression.bonusProgress += 100;
					GorillaQuestManager questManager = questById.questManager;
					if (questManager != null)
					{
						questManager.HandleQuestCompleted(questById.questID);
						num++;
						if (num >= currentProgression.currentQuestIds.Length)
						{
							break;
						}
					}
					else
					{
						num++;
						if (num >= currentProgression.currentQuestIds.Length)
						{
							break;
						}
					}
				}
				else
				{
					num++;
					if (num >= currentProgression.currentQuestIds.Length)
					{
						break;
					}
				}
			}
		}
		player.UpdateProgression(currentProgression.resourceArray, currentProgression.limitedDepositTimeArray, currentProgression.techTreeData, currentProgression.stashedQuests, currentProgression.stashedBonusPoints, currentProgression.bonusProgress, currentProgression.currentQuestIds, currentProgression.currentQuestProgresses);
		SIPlayer.SetAndBroadcastProgression();
	}

	public static List<SIGadget> ResetGadgetOverrides()
	{
		List<SIGadget> list = new List<SIGadget>();
		if ((Object)(object)SIPlayer.LocalPlayer == (Object)null)
		{
			return list;
		}
		GameEntityManager gameEntityManager = SIM.gameEntityManager;
		using (List<int>.Enumerator enumerator = SIPlayer.LocalPlayer.activePlayerGadgets.GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				while (true)
				{
					int current = enumerator.Current;
					GameEntity gameEntity = gameEntityManager.GetGameEntity(gameEntityManager.GetEntityIdFromNetId(current));
					SIGadget val = ((gameEntity != null) ? ((Component)gameEntity).GetComponent<SIGadget>() : null);
					if ((Object)(object)val != (Object)null)
					{
						list.Add(val);
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
		return list;
	}

	public static void ResetNoBlasterCooldown(SIGadgetBlaster instance_)
	{
		if ((Object)(object)instance_ == (Object)null || !instance_.LocalEquippedOrActivated || (!InputHandler.IsRightTriggerPressed() && !InputHandler.IsLeftTriggerPressed()))
		{
			return;
		}
		instance_.lastFired = -1f;
		instance_.projectileCount = 0;
		GadgetTypes_Value_01 += Time.deltaTime;
		if (GadgetTypes_Value_01 >= 3E-07f)
		{
			do
			{
				GadgetTypes_Value_01 -= 3E-07f;
				((ControllerInputPoller)ControllerInputPoller.instance).rightControllerIndexFloat = (InputHandler.IsRightTriggerPressed() ? 0f : 0.6f);
				((ControllerInputPoller)ControllerInputPoller.instance).leftControllerIndexFloat = (InputHandler.IsLeftTriggerPressed() ? 0f : 0.6f);
			}
			while (GadgetTypes_Value_01 >= 3E-07f);
		}
		RapidFireBlaster(instance_);
		RapidFireBlaster(instance_);
	}

	public static void PushEntitiesAway(float force)
	{
		Vector3 position = ((Component)VRRig.LocalRig).transform.position;
		foreach (GameEntity entity in SIM.gameEntityManager.GetGameEntities())
		{
			if ((Object)(object)entity != (Object)null)
			{
				Rigidbody component = ((Component)entity).GetComponent<Rigidbody>();
				if ((Object)(object)component == (Object)null)
				{
					continue;
				}
				component.isKinematic = false;
				Vector3 val = ((Component)entity).transform.position - position;
				component.linearVelocity = ((Vector3)val).normalized * force;
			}
		}
	}

	public static void FreezeAllEntities()
	{
		using List<GameEntity>.Enumerator enumerator = SIM.gameEntityManager.GetGameEntities().GetEnumerator();
		while (enumerator.MoveNext())
		{
			while (true)
			{
				GameEntity current = enumerator.Current;
				if ((Object)(object)current == (Object)null)
				{
					break;
				}
				Rigidbody component = ((Component)current).GetComponent<Rigidbody>();
				if ((Object)(object)component != (Object)null)
				{
					component.isKinematic = true;
					if (!enumerator.MoveNext())
					{
						return;
					}
				}
				else if (!enumerator.MoveNext())
				{
					return;
				}
			}
		}
	}

	public static void KnockbackAllPlayersUp(float strength)
	{
		using IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.GetEnumerator();
		if (!enumerator.MoveNext())
		{
			return;
		}
		while (true)
		{
			SIPlayer val = SIPlayer.Get(enumerator.Current);
			if ((Object)(object)val != (Object)null)
			{
				val.PlayerKnockback(Vector3.up * strength, true, false);
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

	public static void SpawnGadgetForAllPlayers(string gadgetName)
	{
		GameEntityManager gameEntityManager = SIM.gameEntityManager;
		int staticHash = StaticHashExt.GetStaticHash(gadgetName);
		if (!gameEntityManager.FactoryHasEntity(staticHash))
		{
			return;
		}
		foreach (VRRig rig in VRRigCache.ActiveRigs)
		{
			SIPlayer val = SIPlayer.Get(rig);
			if ((Object)(object)val == (Object)null)
			{
				continue;
			}
			gameEntityManager.RequestCreateItem(staticHash, rig.rightHandTransform.position, Quaternion.identity, (long)val.ActorNr);
		}
	}

	public static void ClearAllPlayerGadgets()
	{
		using IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.GetEnumerator();
		if (!enumerator.MoveNext())
		{
			return;
		}
		while (true)
		{
			SIPlayer val = SIPlayer.Get(enumerator.Current);
			if ((Object)(object)val != (Object)null)
			{
				SIM.ClearPlayerGadgets(val);
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

	public static void ResetMaxCharge(SIGadgetChargeBlaster instance_)
	{
		instance_.maxChargeDiff = 9999f;
		ReflectionCompat.SetField(instance_, "currentCharge", instance_.chargeLevels[^1].chargeThreshold + 1f);
	}

	public static void FirePlayerBlastersAtTarget(SIPlayer shooter, SIPlayer target)
	{
		GameEntityManager gameEntityManager = SIM.gameEntityManager;
		using List<int>.Enumerator enumerator = shooter.activePlayerGadgets.GetEnumerator();
		while (enumerator.MoveNext())
		{
			while (true)
			{
				int current = enumerator.Current;
				GameEntity gameEntityFromNetId = gameEntityManager.GetGameEntityFromNetId(current);
				if ((Object)(object)gameEntityFromNetId == (Object)null)
				{
					break;
				}
				SIGadgetBlaster component = ((Component)gameEntityFromNetId).GetComponent<SIGadgetBlaster>();
				if ((Object)(object)component == (Object)null)
				{
					break;
				}
				Vector3 val = target.gamePlayer.rig.headMesh.transform.position - component.firingPosition.position;
				Vector3 normalized = ((Vector3)val).normalized;
				SIM.CallRPC((ClientToClientRPC)3, new object[3]
				{
					current,
					0,
					new object[3]
					{
						component.NextFireId(),
						component.firingPosition.position,
						normalized * 25f
					}
				});
				if (!enumerator.MoveNext())
				{
					return;
				}
			}
		}
	}

	public static void KnockbackPlayer(SIPlayer player, Vector3 direction, float strength)
	{
		if (!((Object)(object)player == (Object)null))
		{
			player.PlayerKnockback(((Vector3)direction).normalized * strength, true, false);
		}
	}

	public static GameEntity CreateGadgetEntity(GameObject gadgetPrefab, Vector3 position)
	{
		GameEntityManager activeManager = GameEntityManager.activeManager;
		int staticHash = StaticHashExt.GetStaticHash(((Object)gadgetPrefab).name);
		GameEntityId val = activeManager.RequestCreateItem(staticHash, position, Quaternion.identity, 0L);
		return activeManager.GetGameEntity(val);
	}

	public static void RedirectBlasterProjectilesToPlayer(SIGadgetBlaster instance_, SIPlayer target, Vector3 velocity, bool Float = false)
	{
		if (instance_.activeProjectiles.Count <= 0)
		{
			return;
		}
		using List<SIGadgetBlasterProjectile>.Enumerator enumerator = instance_.activeProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			while (true)
			{
				SIGadgetBlasterProjectile current = enumerator.Current;
				if ((Object)(object)current.parentBlaster != (Object)(object)instance_ || (Object)(object)current.firedByPlayer != (Object)(object)SIPlayer.LocalPlayer)
				{
					break;
				}
				Transform bodyTransform = ((Component)target).gameObject.GetComponent<VRRig>().bodyTransform;
				instance_.firingPosition.position = bodyTransform.position - (Vector3)(Float ? new Vector3(0f, 0.3f, 0f) : Vector3.zero);
				instance_.firingPosition.rotation = Quaternion.LookRotation(bodyTransform.forward, bodyTransform.up);
				current.rb.velocity = velocity;
				if (!enumerator.MoveNext())
				{
					return;
				}
			}
		}
	}
}
