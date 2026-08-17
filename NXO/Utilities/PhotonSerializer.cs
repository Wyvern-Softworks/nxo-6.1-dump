using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using ExitGames.Client.Photon;
using HarmonyLib;
using NXO.Mods.Categories;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace NXO.Utilities;

internal class PhotonSerializer
{
	public struct RigPose
	{
		private Vector3 pos;

		private Quaternion rot;

		private Vector3 lPos;

		private Quaternion lRot;

		private Vector3 rPos;

		private Quaternion rRot;

		private Quaternion headRot;

		public static RigPose Capture(VRRig r)
		{
			return new RigPose
			{
				pos = ((Component)r).transform.position,
				rot = ((Component)r).transform.rotation,
				lPos = r.leftHand.rigTarget.position,
				lRot = r.leftHand.rigTarget.rotation,
				rPos = r.rightHand.rigTarget.position,
				rRot = r.rightHand.rigTarget.rotation,
				headRot = ((Component)r.head.rigTarget).transform.rotation
			};
		}

		public void Restore(VRRig r)
		{
			((Component)r).transform.position = pos;
			((Component)r).transform.rotation = rot;
			r.leftHand.rigTarget.position = lPos;
			r.leftHand.rigTarget.rotation = lRot;
			r.rightHand.rigTarget.position = rPos;
			r.rightHand.rigTarget.rotation = rRot;
			((Component)r.head.rigTarget).transform.rotation = headRot;
		}
	}

	[CompilerGenerated]
	private sealed class CapturedVariables10
	{
		public bool disableRig;

		public Action<VRRig> poseForTarget;

		internal bool PerPlayerRigOverride_Lambda0()
		{
			bool result;
			if (!PhotonNetwork.InRoom)
			{
				result = true;
			}
			else
			{
				VRRig localRig = VRRig.LocalRig;
				if ((Object)(object)localRig == (Object)null || (Object)(object)Variables.Variables_Reference_09 == (Object)null || (Object)(object)Variables.Variables_Reference_09.myVRRig == (Object)null)
				{
					result = true;
				}
				else
				{
					PhotonView component = ((Component)Variables.Variables_Reference_09.myVRRig).GetComponent<PhotonView>();
					if ((Object)(object)component == (Object)null)
					{
						result = true;
					}
					else
					{
						SerializeAllViews(exclude: true, (PhotonView[])(object)new PhotonView[1] { component });
						RigPose rigPose = RigPose.Capture(localRig);
						bool enabled = ((Behaviour)localRig).enabled;
						if (disableRig)
						{
							((Behaviour)localRig).enabled = false;
						}
						try
						{
							NetPlayer[] playerListOthers = NetworkSystem.Instance.PlayerListOthers;
							int num = 0;
							while (num < playerListOthers.Length)
							{
								NetPlayer val3 = playerListOthers[num];
								VRRig val = RigManager.FindRig(val3);
								if (!((Object)(object)val == (Object)null))
								{
									try
									{
										poseForTarget(val);
										RaiseEventOptions val2 = new RaiseEventOptions();
										val2.TargetActors = new int[1] { val3.ActorNumber };
										SerializePhotonView(component, val2);
									}
									catch (Exception)
									{
									}
									num++;
								}
								else
								{
									num++;
								}
							}
							Safety.ResetNetworkLimits();
						}
						catch (Exception)
						{
							rigPose.Restore(localRig);
							((Behaviour)localRig).enabled = enabled;
							bool result2 = default(bool);
							return result2;
						}
						result = false;
					}
				}
			}
			return result;
		}
	}

	public static void SerializePhotonView(PhotonView pv, RaiseEventOptions options = null)
	{
		if (!PhotonNetwork.InRoom || (Object)(object)pv == (Object)null)
		{
			return;
		}
		List<object> list = AccessTools.Method(typeof(PhotonNetwork), "OnSerializeWrite", new Type[1] { typeof(PhotonView) })?.Invoke(null, new object[1] { pv }) as List<object>;
		if (list == null)
		{
			return;
		}

		bool reliable = pv.Synchronization == ViewSynchronization.ReliableDeltaCompressed || ReflectionCompat.GetField(pv, "mixedModeIsReliable", false);
		byte levelPrefix = ReflectionCompat.GetStaticField(typeof(PhotonNetwork), "currentLevelPrefix", (byte)0);
		List<object> objectUpdates = new List<object>(3)
		{
			PhotonNetwork.ServerTimestamp,
			levelPrefix == 0 ? null : levelPrefix,
			list
		};
		RaiseEventOptions eventOptions = options ?? new RaiseEventOptions();
		eventOptions.InterestGroup = pv.Group;
		PhotonNetwork.NetworkingClient.OpRaiseEvent((byte)(reliable ? 206 : 201), objectUpdates, eventOptions, reliable ? SendOptions.SendReliable : SendOptions.SendUnreliable);
	}

	public static void SerializeAllViews(bool exclude = false, PhotonView[] viewFilter = null)
	{
		if (!PhotonNetwork.InRoom)
		{
			return;
		}
		HashSet<int> viewIds = viewFilter == null ? null : viewFilter.Select((PhotonView view) => view.ViewID).ToHashSet();
		HashSet<byte> blockedGroups = ReflectionCompat.GetStaticField<HashSet<byte>>(typeof(PhotonNetwork), "blockedSendingGroups");
		var enumerator = PhotonNetwork.PhotonViewCollection;
		try
		{
			while (enumerator.MoveNext())
			{
				PhotonView val = enumerator.Current;
				if (!val.IsMine || val.Synchronization == ViewSynchronization.Off || !val.isActiveAndEnabled || (blockedGroups?.Contains(val.Group) ?? false))
				{
					continue;
				}

				bool selected = viewIds == null || (exclude ? !viewIds.Contains(val.ViewID) : viewIds.Contains(val.ViewID));
				if (selected)
				{
					SerializePhotonView(val);
				}
			}
		}
		finally
		{
			enumerator.Dispose();
		}
	}

	public static void OverrideSerializedPose(Action<VRRig> poseForTarget, bool disableRig = false)
	{
		CapturedVariables10 LocalScope4 = new CapturedVariables10();
		LocalScope4.disableRig = disableRig;
		LocalScope4.poseForTarget = poseForTarget;
		MenuPatches.SerializationPatch.SerializationPatch_State_01 = delegate
		{
			bool result;
			if (!PhotonNetwork.InRoom)
			{
				result = true;
			}
			else
			{
				VRRig localRig = VRRig.LocalRig;
				if ((Object)(object)localRig == (Object)null || (Object)(object)Variables.Variables_Reference_09 == (Object)null || (Object)(object)Variables.Variables_Reference_09.myVRRig == (Object)null)
				{
					result = true;
				}
				else
				{
					PhotonView component = ((Component)Variables.Variables_Reference_09.myVRRig).GetComponent<PhotonView>();
					if ((Object)(object)component == (Object)null)
					{
						result = true;
					}
					else
					{
						SerializeAllViews(exclude: true, (PhotonView[])(object)new PhotonView[1] { component });
						RigPose rigPose = RigPose.Capture(localRig);
						bool enabled = ((Behaviour)localRig).enabled;
						if (LocalScope4.disableRig)
						{
							((Behaviour)localRig).enabled = false;
						}
						try
						{
							NetPlayer[] playerListOthers = NetworkSystem.Instance.PlayerListOthers;
							int num = 0;
							while (num < playerListOthers.Length)
							{
								NetPlayer val3 = playerListOthers[num];
								VRRig val = RigManager.FindRig(val3);
								if (!((Object)(object)val == (Object)null))
								{
									try
									{
										LocalScope4.poseForTarget(val);
										RaiseEventOptions val2 = new RaiseEventOptions();
										val2.TargetActors = new int[1] { val3.ActorNumber };
										SerializePhotonView(component, val2);
									}
									catch (Exception)
									{
									}
									num++;
								}
								else
								{
									num++;
								}
							}
							Safety.ResetNetworkLimits();
						}
						catch (Exception)
						{
							rigPose.Restore(localRig);
							((Behaviour)localRig).enabled = enabled;
							bool result2 = default(bool);
							return result2;
						}
						result = false;
					}
				}
			}
			return result;
		};
	}

	public static void SendViewUpdate()
	{
		AccessTools.Method(typeof(PhotonNetwork), "RunViewUpdate")?.Invoke(null, null);
	}
}
