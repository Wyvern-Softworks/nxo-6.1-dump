using System;
using System.Linq;
using System.Runtime.CompilerServices;
using NXO.Utilities;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace NXO.Mods.Categories;

public class Player
{
	[CompilerGenerated]
	private sealed class CapturedVariables360
	{
		public NetPlayer target;

		internal bool OnlyInvisibleToPlayerGun_Lambda1(PhotonPlayer plr)
		{
			return plr.ActorNumber != target.ActorNumber;
		}
	}

	[CompilerGenerated]
	private sealed class CapturedVariables370
	{
		public NetPlayer target;

		internal bool OnlyVisibleToPlayerGun_Lambda1(PhotonPlayer plr)
		{
			return plr.ActorNumber != target.ActorNumber;
		}
	}

	private static float CapturedVariables370_Value_06 = 100f;

	private static float CapturedVariables370_Value_13 = 2f;

	private static bool CapturedVariables370_State_02 = false;

	private static Vector3? CapturedVariables370_Position_06 = null;

	public static float CapturedVariables370_Value_09 = 1f;

	public static float CapturedVariables370_Value_10 = -1f;

	private static bool CapturedVariables370_State_01 = false;

	private static float CapturedVariables370_Value_05 = 0f;

	private static Vector3 CapturedVariables370_Position_02;

	private static bool CapturedVariables370_State_03;

	private static Vector3 CapturedVariables370_Position_03;

	private static Vector3 CapturedVariables370_Position_01;

	private static float CapturedVariables370_Value_04;

	private static float CapturedVariables370_Value_07;

	private static float CapturedVariables370_Value_12;

	private static float CapturedVariables370_Value_03;

	private static float CapturedVariables370_Value_11;

	private static Vector3 CapturedVariables370_Position_05;

	private static bool CapturedVariables370_State_04;

	private static float CapturedVariables370_Value_02;

	private static float CapturedVariables370_Value_08;

	private static Vector3 CapturedVariables370_Position_04;

	private static float CapturedVariables370_Value_01;

	private static GameObject CapturedVariables370_Object_01;

	private static Rigidbody CapturedVariables370_Reference_01;

	private static BoxCollider CapturedVariables370_Reference_02;

	private static float HeadY
	{
		get
		{
			Quaternion rotation = ((Component)Variables.Variables_Reference_09.headCollider).transform.rotation;
			return ((Quaternion)rotation).eulerAngles.y;
		}
	}

	public static void PiggybackPlayerGun()
	{
		if (GunLib.TrySelectRig())
		{
			if (!CapturedVariables370_State_03)
			{
				CapturedVariables370_State_03 = true;
				CapturedVariables370_Position_02 = ((Component)VRRig.LocalRig).transform.position - ((Component)Variables.Variables_Reference_09.bodyCollider).transform.position;
				Vector3 val = ((Component)GunLib.GunLib_Reference_06).transform.position + ((Component)GunLib.GunLib_Reference_06).transform.up * 0.3f - ((Component)GunLib.GunLib_Reference_06).transform.forward * 0.4f;
				((Behaviour)VRRig.LocalRig).enabled = false;
				((Component)VRRig.LocalRig).transform.position = val + CapturedVariables370_Position_02;
			}
			else
			{
				Vector3 val = ((Component)GunLib.GunLib_Reference_06).transform.position + ((Component)GunLib.GunLib_Reference_06).transform.up * 0.3f - ((Component)GunLib.GunLib_Reference_06).transform.forward * 0.4f;
				((Behaviour)VRRig.LocalRig).enabled = false;
				((Component)VRRig.LocalRig).transform.position = val + CapturedVariables370_Position_02;
			}
		}
		else
		{
			CapturedVariables370_State_03 = false;
			((Behaviour)VRRig.LocalRig).enabled = true;
		}
	}

	public static void ResetBackflip()
	{
		CapturedVariables370_Value_04 = 0f;
		SetLocalRigEnabled(rigStatus: true);
	}

	public static void GawkGawkAll()
	{
		PhotonSerializer.OverrideSerializedPose(delegate(VRRig targetRig)
		{
			((Component)VRRig.LocalRig).transform.position = ((Component)targetRig).transform.position + ((Component)targetRig).transform.forward * (0.2f + Mathf.Sin((float)Time.frameCount / 8f) * 0.1f) + ((Component)targetRig).transform.up * -0.4f;
			Transform transform = ((Component)VRRig.LocalRig).transform;
			Quaternion rotation = ((Component)targetRig).transform.rotation;
			Quaternion val = rotation;
			transform.rotation = Quaternion.Euler(((Quaternion)val).eulerAngles + new Vector3(0f, 180f, 0f));
			((Component)VRRig.LocalRig.leftHand.rigTarget).transform.position = ((Component)targetRig).transform.position + ((Component)targetRig).transform.right * 0.2f + ((Component)targetRig).transform.up * -0.4f;
			((Component)VRRig.LocalRig.rightHand.rigTarget).transform.position = ((Component)targetRig).transform.position + ((Component)targetRig).transform.right * -0.2f + ((Component)targetRig).transform.up * -0.4f;
			Transform transform2 = ((Component)VRRig.LocalRig.leftHand.rigTarget).transform;
			rotation = ((Component)targetRig).transform.rotation;
			val = rotation;
			transform2.rotation = Quaternion.Euler(((Quaternion)val).eulerAngles + new Vector3(0f, 180f, 0f));
			Transform transform3 = ((Component)VRRig.LocalRig.rightHand.rigTarget).transform;
			rotation = ((Component)targetRig).transform.rotation;
			val = rotation;
			transform3.rotation = Quaternion.Euler(((Quaternion)val).eulerAngles + new Vector3(0f, 180f, 0f));
			Transform transform4 = ((Component)VRRig.LocalRig.head.rigTarget).transform;
			rotation = ((Component)targetRig).transform.rotation;
			val = rotation;
			transform4.rotation = Quaternion.Euler(((Quaternion)val).eulerAngles + new Vector3(0f, 180f, 0f));
		});
	}

	public static void ResetWobblyMonke()
	{
		CapturedVariables370_Value_01 = 0f;
		SetLocalRigEnabled(rigStatus: true);
	}

	public static void Backflip()
	{
		if (!InputHandler.IsRightTriggerPressed())
		{
			CapturedVariables370_Value_04 = 0f;
			((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = true;
			return;
		}
		((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = false;
		((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position = ((Component)Variables.Variables_Reference_09.bodyCollider).transform.position + new Vector3(0f, 0.15f, 0f);
		CapturedVariables370_Value_04 = (CapturedVariables370_Value_04 - 360f * Time.deltaTime + 360f) % 360f;
		SetRigHeadRotation(CapturedVariables370_Value_04, HeadY, 0f);
		SetRigHandPoses(((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.right * -0.6f + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.up * 0.2f, ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.right * 0.6f + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.up * 0.2f);
	}

	public static void ResetSpiderMonke()
	{
		CapturedVariables370_Value_03 = 0f;
		CapturedVariables370_Value_11 = 0f;
		CapturedVariables370_State_04 = false;
		SetLocalRigEnabled(rigStatus: true);
	}

	public static void ResetRagdollMonke()
	{
		if ((Object)(object)CapturedVariables370_Object_01 != (Object)null)
		{
			Object.Destroy((Object)(object)CapturedVariables370_Object_01);
			CapturedVariables370_Object_01 = null;
			CapturedVariables370_Reference_01 = null;
			CapturedVariables370_Reference_02 = null;
			SetLocalRigEnabled(rigStatus: true);
		}
		else
		{
			SetLocalRigEnabled(rigStatus: true);
		}
	}

	public static void FakeBodyTracking()
	{
		((Component)Variables.Variables_Reference_09.offlineVRRig).transform.rotation = ((Component)Camera.main).transform.rotation;
		((Component)Variables.Variables_Reference_09.offlineVRRig.leftHand.rigTarget).transform.position = ((Component)Variables.Variables_Reference_06.LeftHand.handFollower).transform.position;
		((Component)Variables.Variables_Reference_09.offlineVRRig.rightHand.rigTarget).transform.position = ((Component)Variables.Variables_Reference_06.RightHand.handFollower).transform.position;
	}

	public static void NoFingerMovement()
	{
		VRRig offlineVRRig = Variables.Variables_Reference_09.offlineVRRig;
		((VRMap)offlineVRRig.leftIndex).calcT = 0f;
		((VRMap)offlineVRRig.leftIndex).LerpFinger(1f, false);
		((VRMap)offlineVRRig.leftMiddle).calcT = 0f;
		((VRMap)offlineVRRig.leftMiddle).LerpFinger(1f, false);
		((VRMap)offlineVRRig.leftThumb).calcT = 0f;
		((VRMap)offlineVRRig.leftThumb).LerpFinger(1f, false);
		((VRMap)offlineVRRig.rightIndex).calcT = 0f;
		((VRMap)offlineVRRig.rightIndex).LerpFinger(1f, false);
		((VRMap)offlineVRRig.rightMiddle).calcT = 0f;
		((VRMap)offlineVRRig.rightMiddle).LerpFinger(1f, false);
		((VRMap)offlineVRRig.rightThumb).calcT = 0f;
		((VRMap)offlineVRRig.rightThumb).LerpFinger(1f, false);
	}

	public static void SetLocalRigEnabled(bool rigStatus)
	{
		((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = rigStatus;
		if (rigStatus)
		{
			ResetHeadPose();
		}
	}

	public static void SizeChange()
	{
		if (InputHandler.IsLeftSecondaryPressed())
		{
			CapturedVariables370_Value_09 = 1f;
			if (InputHandler.IsLeftTriggerPressed())
			{
				goto Branch_0055;
			}
		}
		else if (InputHandler.IsLeftTriggerPressed())
		{
			goto Branch_0055;
		}
		if (!InputHandler.IsRightTriggerPressed())
		{
			goto Branch_00c0;
		}
		goto Branch_0097;
		Branch_00c0:
		ReflectionCompat.SetField(Variables.Variables_Reference_06, "nativeScale", CapturedVariables370_Value_09);
		return;
		Branch_0097:
		CapturedVariables370_Value_09 += 0.05f;
		ReflectionCompat.SetField(Variables.Variables_Reference_06, "nativeScale", CapturedVariables370_Value_09);
		return;
		Branch_0055:
		CapturedVariables370_Value_09 -= 0.05f;
		if (!InputHandler.IsRightTriggerPressed())
		{
			goto Branch_00c0;
		}
		goto Branch_0097;
	}

	public static void BackshotPlayerGun()
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
					return;
				}
				((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = false;
				((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position = ((Component)GunLib.GunLib_Reference_06).transform.position + ((Component)GunLib.GunLib_Reference_06).transform.forward * (0f - (0.2f + Mathf.Sin((float)Time.frameCount / 8f) * 0.1f));
				((Component)Variables.Variables_Reference_09.offlineVRRig).transform.rotation = ((Component)GunLib.GunLib_Reference_06).transform.rotation;
				((Component)Variables.Variables_Reference_09.offlineVRRig.leftHand.rigTarget).transform.position = ((Component)GunLib.GunLib_Reference_06).transform.position + ((Component)GunLib.GunLib_Reference_06).transform.right * -0.2f + ((Component)GunLib.GunLib_Reference_06).transform.up * -0.4f;
				((Component)Variables.Variables_Reference_09.offlineVRRig.rightHand.rigTarget).transform.position = ((Component)GunLib.GunLib_Reference_06).transform.position + ((Component)GunLib.GunLib_Reference_06).transform.right * 0.2f + ((Component)GunLib.GunLib_Reference_06).transform.up * -0.4f;
				((Component)Variables.Variables_Reference_09.offlineVRRig.leftHand.rigTarget).transform.rotation = ((Component)GunLib.GunLib_Reference_06).transform.rotation;
				((Component)Variables.Variables_Reference_09.offlineVRRig.rightHand.rigTarget).transform.rotation = ((Component)GunLib.GunLib_Reference_06).transform.rotation;
				((Component)Variables.Variables_Reference_09.offlineVRRig.head.rigTarget).transform.rotation = ((Component)GunLib.GunLib_Reference_06).transform.rotation;
			}
			else
			{
				((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = true;
				GunLib.GunLib_Reference_06 = null;
			}
		}
		else
		{
			GunLib.GunLib_Reference_06 = null;
			GunLib.SetGunVisualsVisible(isVisible: false);
		}
	}

	public Player()
	{
	}

	public static void AscendMonke()
	{
		if (InputHandler.IsRightTriggerPressed())
		{
			((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = false;
			Transform transform = ((Component)Variables.Variables_Reference_09.offlineVRRig).transform;
			transform.position += new Vector3(0f, 0.01f, 0f);
			((Component)Variables.Variables_Reference_09.offlineVRRig.head.rigTarget).transform.rotation = ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.rotation;
			((Component)Variables.Variables_Reference_09.offlineVRRig.leftHand.rigTarget).transform.position = ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.right * -1f;
			((Component)Variables.Variables_Reference_09.offlineVRRig.rightHand.rigTarget).transform.position = ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.right * 1f;
			SetHeadPose((Vector3?)new Vector3(180f, 0f, 0f), false);
		}
		else
		{
			((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = true;
		}
	}

	public static void RigGun()
	{
		if (GunLib.GunGrips)
		{
			GunLib.UpdateGunRaycast();
			if (GunLib.GunTriggers)
			{
				((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = false;
				((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position = ((RaycastHit)GunLib.GunLib_Reference_07).point + new Vector3(0f, 1f, 0f);
			}
			else
			{
				((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = true;
			}
		}
		else
		{
			GunLib.SetGunVisualsVisible(isVisible: false);
		}
	}

	public static void MirrorAll()
	{
		PhotonSerializer.OverrideSerializedPose(delegate(VRRig targetRig)
		{
			((Component)VRRig.LocalRig).transform.position = ((Component)targetRig).transform.position + ((Component)targetRig).transform.forward * 1.5f;
			((Component)VRRig.LocalRig).transform.rotation = Quaternion.Euler(0f, ((Component)targetRig).transform.eulerAngles.y + 180f, 0f);
			Vector3 val = ((Component)targetRig).transform.InverseTransformPoint(targetRig.leftHand.rigTarget.position);
			Vector3 val2 = ((Component)targetRig).transform.InverseTransformPoint(targetRig.rightHand.rigTarget.position);
			val.x *= -1f;
			val2.x *= -1f;
			((Component)VRRig.LocalRig.rightHand.rigTarget).transform.position = ((Component)VRRig.LocalRig).transform.TransformPoint(val);
			((Component)VRRig.LocalRig.leftHand.rigTarget).transform.position = ((Component)VRRig.LocalRig).transform.TransformPoint(val2);
			Quaternion rotation = targetRig.leftHand.rigTarget.rotation;
			Quaternion rotation2 = targetRig.rightHand.rigTarget.rotation;
			((Component)VRRig.LocalRig.rightHand.rigTarget).transform.rotation = new Quaternion(0f - rotation.x, rotation.y, rotation.z, 0f - rotation.w);
			((Component)VRRig.LocalRig.leftHand.rigTarget).transform.rotation = new Quaternion(0f - rotation2.x, rotation2.y, rotation2.z, 0f - rotation2.w);
			VRRig.LocalRig.head.rigTarget.rotation = Quaternion.Euler(targetRig.head.rigTarget.eulerAngles.x, targetRig.head.rigTarget.eulerAngles.y + 180f, 0f - targetRig.head.rigTarget.eulerAngles.z);
		});
	}

	public static void InvisibleMonke()
	{
		InputHandler.UpdateToggleOnPress(InputHandler.IsRightSecondaryPressed, ref InputHandler.InputHandler_State_02, ref InputHandler.InputHandler_State_01);
		if (InputHandler.InputHandler_State_02)
		{
			((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = false;
			((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position = new Vector3(999f, 999f, 999f);
		}
		else
		{
			((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = true;
		}
	}

	public static void PiggybackAll()
	{
		PhotonSerializer.OverrideSerializedPose(delegate(VRRig targetRig)
		{
			((Component)VRRig.LocalRig).transform.position = ((Component)targetRig).transform.position + ((Component)targetRig).transform.up * 0.3f - ((Component)targetRig).transform.forward * 0.4f;
			((Component)VRRig.LocalRig).transform.rotation = ((Component)targetRig).transform.rotation;
			((Component)VRRig.LocalRig.leftHand.rigTarget).transform.position = ((Component)VRRig.LocalRig).transform.position + ((Component)targetRig).transform.right * -0.3f + ((Component)targetRig).transform.up * 0.1f;
			((Component)VRRig.LocalRig.rightHand.rigTarget).transform.position = ((Component)VRRig.LocalRig).transform.position + ((Component)targetRig).transform.right * 0.3f + ((Component)targetRig).transform.up * 0.1f;
			((Component)VRRig.LocalRig.leftHand.rigTarget).transform.rotation = ((Component)targetRig).transform.rotation;
			((Component)VRRig.LocalRig.rightHand.rigTarget).transform.rotation = ((Component)targetRig).transform.rotation;
			((Component)VRRig.LocalRig.head.rigTarget).transform.rotation = ((Component)targetRig).transform.rotation;
		}, disableRig: true);
	}

	public static void DanceMonke()
	{
		if (!InputHandler.IsRightTriggerPressed())
		{
			((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = true;
			ResetHeadPose();
			return;
		}
		((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = false;
		float num = Time.time * 2f;
		float num2 = Time.time * 4f;
		((Component)Variables.Variables_Reference_09.offlineVRRig).transform.rotation = Quaternion.Euler(0f, HeadY + Mathf.Sin(num) * 30f, 0f);
		((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position = ((Component)Variables.Variables_Reference_09.bodyCollider).transform.position + new Vector3(0f, 0.15f - Mathf.Abs(Mathf.Sin(num * 2f)) * 0.3f, 0f);
		SetRigHandPoses(((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.right * -0.8f + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.up * (0.3f + Mathf.Sin(num2) * 0.4f) + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.forward * Mathf.Cos(num2) * 0.3f, ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.right * 0.8f + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.up * (0.3f + Mathf.Sin(num2 + MathF.PI) * 0.4f) + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.forward * Mathf.Cos(num2 + MathF.PI) * 0.3f);
		((Component)Variables.Variables_Reference_09.offlineVRRig.leftHand.rigTarget).transform.rotation = Quaternion.Euler(90f + Mathf.Sin(num2) * 45f, 0f, Mathf.Cos(num2) * 30f);
		((Component)Variables.Variables_Reference_09.offlineVRRig.rightHand.rigTarget).transform.rotation = Quaternion.Euler(90f + Mathf.Sin(num2 + MathF.PI) * 45f, 0f, Mathf.Cos(num2 + MathF.PI) * 30f);
		SetHeadPose((Vector3?)new Vector3(Mathf.Sin(num) * 15f, HeadY, Mathf.Cos(num2) * 10f), false);
	}

	public static void ResetGlitchMonke()
	{
		CapturedVariables370_Value_02 = 0f;
		CapturedVariables370_Value_08 = 0f;
		CapturedVariables370_Position_04 = Vector3.zero;
		SetLocalRigEnabled(rigStatus: true);
	}

	public static void UpsideDown()
	{
		if (!InputHandler.IsRightTriggerPressed())
		{
			((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = true;
			return;
		}
		((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = false;
		((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position = ((Component)Variables.Variables_Reference_09.bodyCollider).transform.position;
		((Component)Variables.Variables_Reference_09.offlineVRRig).transform.rotation = Quaternion.Euler(0f, HeadY, 180f);
		Transform transform = ((Component)Variables.Variables_Reference_09.offlineVRRig.head.rigTarget).transform;
		Quaternion rotation = ((Component)Variables.Variables_Reference_09.headCollider).transform.rotation;
		Quaternion val = rotation;
		float num = 0f - ((Quaternion)val).eulerAngles.x;
		rotation = ((Component)Variables.Variables_Reference_09.headCollider).transform.rotation;
		val = rotation;
		transform.rotation = Quaternion.Euler(num, ((Quaternion)val).eulerAngles.y, 180f);
		Vector3 position = Variables.Variables_Reference_06.LeftHand.controllerTransform.position;
		Vector3 position2 = Variables.Variables_Reference_06.RightHand.controllerTransform.position;
		Vector3 position3 = ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position;
		Vector3 position4 = position3 + (position - position3) * -1f;
		Vector3 position5 = position3 + (position2 - position3) * -1f;
		((Component)Variables.Variables_Reference_09.offlineVRRig.leftHand.rigTarget).transform.position = position4;
		((Component)Variables.Variables_Reference_09.offlineVRRig.rightHand.rigTarget).transform.position = position5;
		Quaternion rotation2 = Variables.Variables_Reference_06.LeftHand.controllerTransform.rotation;
		Quaternion rotation3 = Variables.Variables_Reference_06.RightHand.controllerTransform.rotation;
		((Component)Variables.Variables_Reference_09.offlineVRRig.leftHand.rigTarget).transform.rotation = new Quaternion(0f - rotation2.x, rotation2.y, rotation2.z, 0f - rotation2.w);
		((Component)Variables.Variables_Reference_09.offlineVRRig.rightHand.rigTarget).transform.rotation = new Quaternion(0f - rotation3.x, rotation3.y, rotation3.z, 0f - rotation3.w);
	}

	public static void Cartwheel()
	{
		if (!InputHandler.IsRightTriggerPressed())
		{
			CapturedVariables370_Value_12 = 0f;
			((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = true;
			return;
		}
		((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = false;
		((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position = ((Component)Variables.Variables_Reference_09.bodyCollider).transform.position + new Vector3(0f, 0.15f, 0f);
		CapturedVariables370_Value_12 = (CapturedVariables370_Value_12 + 270f * Time.deltaTime) % 360f;
		SetRigHeadRotation(0f, HeadY, CapturedVariables370_Value_12);
		SetRigHandPoses(((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.right * -1.2f, ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.right * 1.2f);
	}

	public static void GrabRig()
	{
		Transform val;
		if (!InputHandler.IsRightGripPressed() || InputHandler.IsLeftGripPressed())
		{
			if (!InputHandler.IsLeftGripPressed() || InputHandler.IsRightGripPressed())
			{
				val = null;
				if ((Object)(object)val != (Object)null)
				{
					goto Branch_00cd;
				}
			}
			else
			{
				val = Variables.Variables_Reference_09.leftHandTransform;
				if ((Object)(object)val != (Object)null)
				{
					goto Branch_00cd;
				}
			}
		}
		else
		{
			val = Variables.Variables_Reference_09.rightHandTransform;
			if ((Object)(object)val != (Object)null)
			{
				goto Branch_00cd;
			}
		}
		((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = true;
		return;
		Branch_00cd:
		((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = false;
		((Component)Variables.Variables_Reference_09.offlineVRRig).transform.SetPositionAndRotation(val.position, val.rotation);
		((Component)Variables.Variables_Reference_09.offlineVRRig.head.rigTarget).transform.rotation = val.rotation;
		SetRigHandPoses(val.position + val.right * -0.3f, val.position + val.right * 0.3f, val.rotation);
	}

	public static void SetLongArmsEnabled(bool setActive)
	{
		((Component)Variables.Variables_Reference_06).transform.localScale = (setActive ? Settings.CapturedVariables3760_Position_01 : Vector3.one);
	}

	public static void LayOnStomach()
	{
		InputHandler.UpdateToggleOnPress(InputHandler.IsRightSecondaryPressed, ref InputHandler.InputHandler_State_02, ref InputHandler.InputHandler_State_01);
		if (InputHandler.InputHandler_State_02)
		{
			if (!CapturedVariables370_State_01)
			{
				CapturedVariables370_Position_01 = ((Component)Variables.Variables_Reference_09.bodyCollider).transform.position + new Vector3(0f, -0.15f, 0f);
				CapturedVariables370_State_01 = true;
				((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = false;
				((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position = CapturedVariables370_Position_01;
				((Component)Variables.Variables_Reference_09.offlineVRRig).transform.rotation = Quaternion.Euler(90f, 0f, 0f);
				((Component)Variables.Variables_Reference_09.offlineVRRig.head.rigTarget).transform.rotation = Quaternion.Euler(0f, 90f, 90f);
				SetRigHandPoses(((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.right * -1f + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.up * 0.25f, ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.right * 1f + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.up * -0.5f);
			}
			else
			{
				((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = false;
				((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position = CapturedVariables370_Position_01;
				((Component)Variables.Variables_Reference_09.offlineVRRig).transform.rotation = Quaternion.Euler(90f, 0f, 0f);
				((Component)Variables.Variables_Reference_09.offlineVRRig.head.rigTarget).transform.rotation = Quaternion.Euler(0f, 90f, 90f);
				SetRigHandPoses(((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.right * -1f + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.up * 0.25f, ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.right * 1f + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.up * -0.5f);
			}
		}
		else
		{
			CapturedVariables370_State_01 = false;
			SetLocalRigEnabled(rigStatus: true);
			ResetHeadPose();
		}
	}

	public static void BackshotAll()
	{
		PhotonSerializer.OverrideSerializedPose(delegate(VRRig targetRig)
		{
			((Component)VRRig.LocalRig).transform.position = ((Component)targetRig).transform.position + ((Component)targetRig).transform.forward * (0f - (0.2f + Mathf.Sin((float)Time.frameCount / 8f) * 0.1f));
			((Component)VRRig.LocalRig).transform.rotation = ((Component)targetRig).transform.rotation;
			((Component)VRRig.LocalRig.leftHand.rigTarget).transform.position = ((Component)targetRig).transform.position + ((Component)targetRig).transform.right * -0.2f + ((Component)targetRig).transform.up * -0.4f;
			((Component)VRRig.LocalRig.rightHand.rigTarget).transform.position = ((Component)targetRig).transform.position + ((Component)targetRig).transform.right * 0.2f + ((Component)targetRig).transform.up * -0.4f;
			((Component)VRRig.LocalRig.leftHand.rigTarget).transform.rotation = ((Component)targetRig).transform.rotation;
			((Component)VRRig.LocalRig.rightHand.rigTarget).transform.rotation = ((Component)targetRig).transform.rotation;
			((Component)VRRig.LocalRig.head.rigTarget).transform.rotation = ((Component)targetRig).transform.rotation;
		});
	}

	public static void CopyPlayerGun()
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
					CopyRigPose(GunLib.GunLib_Reference_06);
				}
			}
			else
			{
				((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = true;
				GunLib.GunLib_Reference_06 = null;
			}
		}
		else
		{
			GunLib.GunLib_Reference_06 = null;
			GunLib.SetGunVisualsVisible(isVisible: false);
		}
	}

	private static void SetRigHandPoses(Vector3 left, Vector3 right, Quaternion? rot = null)
	{
		Quaternion? val = rot;
		if (!val.HasValue)
		{
			Quaternion rotation = ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.rotation;
			((Component)Variables.Variables_Reference_09.offlineVRRig.leftHand.rigTarget).transform.position = left;
			((Component)Variables.Variables_Reference_09.offlineVRRig.rightHand.rigTarget).transform.position = right;
			((Component)Variables.Variables_Reference_09.offlineVRRig.leftHand.rigTarget).transform.rotation = rotation;
			((Component)Variables.Variables_Reference_09.offlineVRRig.rightHand.rigTarget).transform.rotation = rotation;
		}
		else
		{
			Quaternion rotation = val.GetValueOrDefault();
			((Component)Variables.Variables_Reference_09.offlineVRRig.leftHand.rigTarget).transform.position = left;
			((Component)Variables.Variables_Reference_09.offlineVRRig.rightHand.rigTarget).transform.position = right;
			((Component)Variables.Variables_Reference_09.offlineVRRig.leftHand.rigTarget).transform.rotation = rotation;
			((Component)Variables.Variables_Reference_09.offlineVRRig.rightHand.rigTarget).transform.rotation = rotation;
		}
	}

	public static void FreezeRig()
	{
		InputHandler.UpdateToggleOnPress(InputHandler.IsRightSecondaryPressed, ref InputHandler.InputHandler_State_02, ref InputHandler.InputHandler_State_01);
		if (InputHandler.InputHandler_State_02)
		{
			((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = false;
			((Component)Variables.Variables_Reference_09.offlineVRRig).transform.SetPositionAndRotation(((Component)Variables.Variables_Reference_09.headCollider).transform.position, ((Component)Variables.Variables_Reference_09.headCollider).transform.rotation);
		}
		else
		{
			((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = true;
		}
	}

	private static void UpdateWalkingHand(Transform hand, float phase, float side)
	{
		Transform transform = ((Component)Variables.Variables_Reference_09.offlineVRRig).transform;
		if (phase < 0.3f)
		{
			float num = phase / 0.3f;
			float num2 = Mathf.Lerp(-0.5f, 0.7f, num);
			float num3 = Mathf.Sin(num * MathF.PI) * 0.25f;
			((Component)hand).transform.position = transform.position + transform.forward * num2 + transform.right * side + transform.up * (-0.4f + num3);
		}
		else
		{
			float num4 = (phase - 0.3f) / 0.7f;
			float num2 = Mathf.Lerp(0.7f, -0.5f, num4);
			float num3 = 0f;
			((Component)hand).transform.position = transform.position + transform.forward * num2 + transform.right * side + transform.up * (-0.4f + num3);
		}
	}

	public static void CopyAll()
	{
		PhotonSerializer.OverrideSerializedPose(delegate(VRRig targetRig)
		{
			((Component)VRRig.LocalRig).transform.SetPositionAndRotation(((Component)targetRig).transform.position, ((Component)targetRig).transform.rotation);
			VRRig.LocalRig.head.rigTarget.SetPositionAndRotation(targetRig.head.rigTarget.position, targetRig.head.rigTarget.rotation);
			VRRig.LocalRig.leftHand.rigTarget.SetPositionAndRotation(targetRig.leftHand.rigTarget.position, targetRig.leftHand.rigTarget.rotation);
			VRRig.LocalRig.rightHand.rigTarget.SetPositionAndRotation(targetRig.rightHand.rigTarget.position, targetRig.rightHand.rigTarget.rotation);
		});
	}

	public static void GhostMonke()
	{
		InputHandler.UpdateToggleOnPress(InputHandler.IsRightSecondaryPressed, ref InputHandler.InputHandler_State_02, ref InputHandler.InputHandler_State_01);
		((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = !InputHandler.InputHandler_State_02;
	}

	public static void TPose()
	{
		if (!InputHandler.IsRightTriggerPressed())
		{
			((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = true;
			return;
		}
		((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = false;
		((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position = ((Component)Variables.Variables_Reference_09.bodyCollider).transform.position + new Vector3(0f, 0.15f, 0f);
		SetRigHeadRotation(0f, HeadY, 0f);
		SetRigHandPoses(((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.right * -1.2f, ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.right * 1.2f);
	}

	public static void CopyRigPose(VRRig target)
	{
		if ((Object)(object)target == (Object)null || (Object)(object)Variables.Variables_Reference_09?.offlineVRRig == (Object)null)
		{
			return;
		}
		VRRig offlineVRRig = Variables.Variables_Reference_09.offlineVRRig;
		((Behaviour)offlineVRRig).enabled = false;
		((Component)offlineVRRig).transform.SetPositionAndRotation(((Component)target).transform.position, ((Component)target).transform.rotation);
		Transform[] array = (Transform[])(object)new Transform[3]
		{
			offlineVRRig.head.rigTarget,
			offlineVRRig.leftHand.rigTarget,
			offlineVRRig.rightHand.rigTarget
		};
		Transform[] array2 = (Transform[])(object)new Transform[3]
		{
			target.head.rigTarget,
			target.leftHand.rigTarget,
			target.rightHand.rigTarget
		};
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetPositionAndRotation(array2[i].position, array2[i].rotation);
		}
		if (target.leftIndex != null)
		{
			((VRMap)offlineVRRig.leftIndex).calcT = ((VRMap)target.leftIndex).calcT;
			((VRMap)offlineVRRig.leftIndex).LerpFinger(1f, false);
			if (target.leftMiddle != null)
			{
				goto Branch_01bf;
			}
		}
		else if (target.leftMiddle != null)
		{
			goto Branch_01bf;
		}
		if (target.leftThumb == null)
		{
			goto Branch_0264;
		}
		goto Branch_0220;
		Branch_0326:
		if (target.rightThumb == null)
		{
			return;
		}
		goto Branch_0343;
		Branch_02c5:
		if (target.rightMiddle == null)
		{
			goto Branch_0326;
		}
		goto Branch_02e2;
		Branch_01bf:
		((VRMap)offlineVRRig.leftMiddle).calcT = ((VRMap)target.leftMiddle).calcT;
		((VRMap)offlineVRRig.leftMiddle).LerpFinger(1f, false);
		if (target.leftThumb == null)
		{
			goto Branch_0264;
		}
		goto Branch_0220;
		Branch_02e2:
		((VRMap)offlineVRRig.rightMiddle).calcT = ((VRMap)target.rightMiddle).calcT;
		((VRMap)offlineVRRig.rightMiddle).LerpFinger(1f, false);
		if (target.rightThumb == null)
		{
			return;
		}
		goto Branch_0343;
		Branch_0220:
		((VRMap)offlineVRRig.leftThumb).calcT = ((VRMap)target.leftThumb).calcT;
		((VRMap)offlineVRRig.leftThumb).LerpFinger(1f, false);
		if (target.rightIndex == null)
		{
			goto Branch_02c5;
		}
		goto Branch_0281;
		Branch_0264:
		if (target.rightIndex == null)
		{
			goto Branch_02c5;
		}
		goto Branch_0281;
		Branch_0343:
		((VRMap)offlineVRRig.rightThumb).calcT = ((VRMap)target.rightThumb).calcT;
		((VRMap)offlineVRRig.rightThumb).LerpFinger(1f, false);
		return;
		Branch_0281:
		((VRMap)offlineVRRig.rightIndex).calcT = ((VRMap)target.rightIndex).calcT;
		((VRMap)offlineVRRig.rightIndex).LerpFinger(1f, false);
		if (target.rightMiddle == null)
		{
			goto Branch_0326;
		}
		goto Branch_02e2;
	}

	public static void LayOnBack()
	{
		InputHandler.UpdateToggleOnPress(InputHandler.IsRightSecondaryPressed, ref InputHandler.InputHandler_State_02, ref InputHandler.InputHandler_State_01);
		if (InputHandler.InputHandler_State_02)
		{
			if (!CapturedVariables370_State_01)
			{
				CapturedVariables370_Position_03 = ((Component)Variables.Variables_Reference_09.bodyCollider).transform.position + new Vector3(0f, -0.15f, 0f);
				CapturedVariables370_State_01 = true;
				((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = false;
				((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position = CapturedVariables370_Position_03;
				((Component)Variables.Variables_Reference_09.offlineVRRig).transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
				((Component)Variables.Variables_Reference_09.offlineVRRig.head.rigTarget).transform.rotation = Quaternion.Euler(0f, 90f, -90f);
				SetRigHandPoses(((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.right * -1f + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.up * 0.25f, ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.right * 1f + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.up * -0.5f);
			}
			else
			{
				((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = false;
				((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position = CapturedVariables370_Position_03;
				((Component)Variables.Variables_Reference_09.offlineVRRig).transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
				((Component)Variables.Variables_Reference_09.offlineVRRig.head.rigTarget).transform.rotation = Quaternion.Euler(0f, 90f, -90f);
				SetRigHandPoses(((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.right * -1f + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.up * 0.25f, ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.right * 1f + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.up * -0.5f);
			}
		}
		else
		{
			CapturedVariables370_State_01 = false;
			SetLocalRigEnabled(rigStatus: true);
			ResetHeadPose();
		}
	}

	public static void GawkGawkGun()
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
					return;
				}
				((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position = ((Component)GunLib.GunLib_Reference_06).transform.position + ((Component)GunLib.GunLib_Reference_06).transform.forward * (0.2f + Mathf.Sin((float)Time.frameCount / 8f) * 0.1f) + ((Component)GunLib.GunLib_Reference_06).transform.up * -0.4f;
				Transform transform = ((Component)Variables.Variables_Reference_09.offlineVRRig).transform;
				Quaternion rotation = ((Component)GunLib.GunLib_Reference_06).transform.rotation;
				Quaternion val = rotation;
				transform.rotation = Quaternion.Euler(((Quaternion)val).eulerAngles + new Vector3(0f, 180f, 0f));
				((Component)Variables.Variables_Reference_09.offlineVRRig.leftHand.rigTarget).transform.position = ((Component)GunLib.GunLib_Reference_06).transform.position + ((Component)GunLib.GunLib_Reference_06).transform.right * 0.2f + ((Component)GunLib.GunLib_Reference_06).transform.up * -0.4f;
				((Component)Variables.Variables_Reference_09.offlineVRRig.rightHand.rigTarget).transform.position = ((Component)GunLib.GunLib_Reference_06).transform.position + ((Component)GunLib.GunLib_Reference_06).transform.right * -0.2f + ((Component)GunLib.GunLib_Reference_06).transform.up * -0.4f;
				Transform transform2 = ((Component)Variables.Variables_Reference_09.offlineVRRig.leftHand.rigTarget).transform;
				rotation = ((Component)GunLib.GunLib_Reference_06).transform.rotation;
				val = rotation;
				transform2.rotation = Quaternion.Euler(((Quaternion)val).eulerAngles + new Vector3(0f, 180f, 0f));
				Transform transform3 = ((Component)Variables.Variables_Reference_09.offlineVRRig.rightHand.rigTarget).transform;
				rotation = ((Component)GunLib.GunLib_Reference_06).transform.rotation;
				val = rotation;
				transform3.rotation = Quaternion.Euler(((Quaternion)val).eulerAngles + new Vector3(0f, 180f, 0f));
				Transform transform4 = ((Component)Variables.Variables_Reference_09.offlineVRRig.head.rigTarget).transform;
				rotation = ((Component)GunLib.GunLib_Reference_06).transform.rotation;
				val = rotation;
				transform4.rotation = Quaternion.Euler(((Quaternion)val).eulerAngles + new Vector3(0f, 180f, 0f));
			}
			else
			{
				((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = true;
				GunLib.GunLib_Reference_06 = null;
			}
		}
		else
		{
			GunLib.GunLib_Reference_06 = null;
			GunLib.SetGunVisualsVisible(isVisible: false);
		}
	}

	public static void ChaseGun()
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
					return;
				}
				((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = false;
				((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position = Vector3.MoveTowards(((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position, ((Component)GunLib.GunLib_Reference_06).transform.position, Time.deltaTime * 10f);
				((Component)Variables.Variables_Reference_09.offlineVRRig).transform.LookAt(((Component)GunLib.GunLib_Reference_06).transform);
				((Component)Variables.Variables_Reference_09.offlineVRRig.leftHand.rigTarget).transform.position = ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.right * -1.5f;
				((Component)Variables.Variables_Reference_09.offlineVRRig.rightHand.rigTarget).transform.position = ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.right * 1.5f;
				((Component)Variables.Variables_Reference_09.offlineVRRig.leftHand.rigTarget).transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
				((Component)Variables.Variables_Reference_09.offlineVRRig.rightHand.rigTarget).transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
				Variables.Variables_Reference_09.offlineVRRig.head.rigTarget.eulerAngles = new Vector3((float)Random.Range(0, 360), (float)Random.Range(0, 180), (float)Random.Range(0, 180));
				Variables.Variables_Reference_09.offlineVRRig.head.rigTarget.eulerAngles = new Vector3((float)Random.Range(0, 360), (float)Random.Range(0, 360), (float)Random.Range(0, 360));
				((Component)Variables.Variables_Reference_09).GetComponent<Rigidbody>().velocity = Vector3.zero;
			}
			else
			{
				((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = true;
				GunLib.GunLib_Reference_06 = null;
			}
		}
		else
		{
			GunLib.GunLib_Reference_06 = null;
			GunLib.SetGunVisualsVisible(isVisible: false);
		}
	}

	public static void ResetHeadPose()
	{
		if (!((Object)(object)Variables.Variables_Reference_09?.offlineVRRig?.head?.rigTarget == (Object)null) && CapturedVariables370_Position_06.HasValue)
		{
			Variables.Variables_Reference_09.offlineVRRig.head.rigTarget.eulerAngles = CapturedVariables370_Position_06.Value;
			CapturedVariables370_Position_06 = null;
		}
	}

	public static void OrbitGun()
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
					return;
				}
				((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = false;
				((Component)Variables.Variables_Reference_09.offlineVRRig).transform.LookAt(((Component)GunLib.GunLib_Reference_06).transform.position);
				((Component)Variables.Variables_Reference_09.offlineVRRig.leftHand.rigTarget).transform.position = ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.right * -1.5f;
				((Component)Variables.Variables_Reference_09.offlineVRRig.rightHand.rigTarget).transform.position = ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.right * 1.5f;
				((Component)Variables.Variables_Reference_09.offlineVRRig.leftHand.rigTarget).transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
				((Component)Variables.Variables_Reference_09.offlineVRRig.rightHand.rigTarget).transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
				((Component)Variables.Variables_Reference_09.offlineVRRig).transform.RotateAround(((Component)GunLib.GunLib_Reference_06).transform.position, Vector3.up, CapturedVariables370_Value_06 * Time.deltaTime);
				Vector3 val = ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position - ((Component)GunLib.GunLib_Reference_06).transform.position;
				Vector3 position = ((Vector3)val).normalized * CapturedVariables370_Value_13 + ((Component)GunLib.GunLib_Reference_06).transform.position;
				((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position = position;
			}
			else
			{
				((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = true;
				GunLib.GunLib_Reference_06 = null;
			}
		}
		else
		{
			GunLib.GunLib_Reference_06 = null;
			GunLib.SetGunVisualsVisible(isVisible: false);
		}
	}

	public static void JumpscareGun()
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
					return;
				}
				((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = false;
				((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position = GunLib.GunLib_Reference_06.headMesh.transform.position + GunLib.GunLib_Reference_06.headMesh.transform.forward * Random.Range(0.1f, 0.5f);
				((Component)Variables.Variables_Reference_09.offlineVRRig.head.rigTarget).transform.LookAt(GunLib.GunLib_Reference_06.headMesh.transform.position);
				((Component)Variables.Variables_Reference_09.offlineVRRig).transform.rotation = ((Component)Variables.Variables_Reference_09.offlineVRRig.head.rigTarget).transform.rotation;
				((Component)Variables.Variables_Reference_09.offlineVRRig.leftHand.rigTarget).transform.position = GunLib.GunLib_Reference_06.headMesh.transform.position + GunLib.GunLib_Reference_06.headMesh.transform.right * 0.2f;
				((Component)Variables.Variables_Reference_09.offlineVRRig.rightHand.rigTarget).transform.position = GunLib.GunLib_Reference_06.headMesh.transform.position + GunLib.GunLib_Reference_06.headMesh.transform.right * -0.2f;
				((Component)Variables.Variables_Reference_09.offlineVRRig.head.rigTarget).transform.rotation = ((Component)Variables.Variables_Reference_09.offlineVRRig.head.rigTarget).transform.rotation;
				Transform transform = ((Component)Variables.Variables_Reference_09.offlineVRRig.leftHand.rigTarget).transform;
				Quaternion rotation = ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.rotation;
				Quaternion val = rotation;
				transform.rotation = Quaternion.Euler(((Quaternion)val).eulerAngles + new Vector3(0f, 180f, 0f));
				Transform transform2 = ((Component)Variables.Variables_Reference_09.offlineVRRig.rightHand.rigTarget).transform;
				rotation = ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.rotation;
				val = rotation;
				transform2.rotation = Quaternion.Euler(((Quaternion)val).eulerAngles + new Vector3(0f, 180f, 0f));
			}
			else
			{
				((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = true;
				GunLib.GunLib_Reference_06 = null;
			}
		}
		else
		{
			GunLib.GunLib_Reference_06 = null;
			GunLib.SetGunVisualsVisible(isVisible: false);
		}
	}

	public static void Griddy()
	{
		if (!InputHandler.IsRightTriggerPressed())
		{
			((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = true;
			return;
		}
		((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = false;
		((Component)Variables.Variables_Reference_09.offlineVRRig.head.rigTarget).transform.rotation = ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.rotation;
		Vector3 val = ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.right * -0.25f + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.forward * 0.5f * Mathf.Cos((float)Time.frameCount / 10f) + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.up * -0.3f * Mathf.Abs(Mathf.Sin((float)Time.frameCount / 7f));
		Vector3 val2 = ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.right * 0.25f + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.forward * 0.5f * Mathf.Cos((float)Time.frameCount / 10f) + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.up * -0.3f * Mathf.Abs(Mathf.Sin((float)Time.frameCount / 7f));
		SetRigHandPoses(((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position + val, ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position + val2);
	}

	public static void ChaseAll()
	{
		PhotonSerializer.OverrideSerializedPose(delegate(VRRig targetRig)
		{
			((Component)VRRig.LocalRig).transform.position = Vector3.MoveTowards(((Component)VRRig.LocalRig).transform.position, ((Component)targetRig).transform.position, Time.deltaTime * 10f);
			((Component)VRRig.LocalRig).transform.LookAt(((Component)targetRig).transform);
			((Component)VRRig.LocalRig.leftHand.rigTarget).transform.position = ((Component)VRRig.LocalRig).transform.position + ((Component)VRRig.LocalRig).transform.right * -1.5f;
			((Component)VRRig.LocalRig.rightHand.rigTarget).transform.position = ((Component)VRRig.LocalRig).transform.position + ((Component)VRRig.LocalRig).transform.right * 1.5f;
			((Component)VRRig.LocalRig.leftHand.rigTarget).transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
			((Component)VRRig.LocalRig.rightHand.rigTarget).transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
			((Component)VRRig.LocalRig.head.rigTarget).transform.rotation = Quaternion.Euler((float)Random.Range(0, 360), (float)Random.Range(0, 360), (float)Random.Range(0, 360));
		});
	}

	public static void ResetFrontflip()
	{
		CapturedVariables370_Value_07 = 0f;
		SetLocalRigEnabled(rigStatus: true);
	}

	public static void JumpscareAll()
	{
		PhotonSerializer.OverrideSerializedPose(delegate(VRRig targetRig)
		{
			((Component)VRRig.LocalRig).transform.position = targetRig.headMesh.transform.position + targetRig.headMesh.transform.forward * Random.Range(0.1f, 0.5f);
			((Component)VRRig.LocalRig.head.rigTarget).transform.LookAt(targetRig.headMesh.transform.position);
			Quaternion rotation = ((Component)VRRig.LocalRig.head.rigTarget).transform.rotation;
			((Component)VRRig.LocalRig).transform.rotation = rotation;
			((Component)VRRig.LocalRig.leftHand.rigTarget).transform.position = targetRig.headMesh.transform.position + targetRig.headMesh.transform.right * 0.2f;
			((Component)VRRig.LocalRig.rightHand.rigTarget).transform.position = targetRig.headMesh.transform.position + targetRig.headMesh.transform.right * -0.2f;
			Transform transform = ((Component)VRRig.LocalRig.leftHand.rigTarget).transform;
			Quaternion rotation2 = ((Component)VRRig.LocalRig).transform.rotation;
			Quaternion val = rotation2;
			transform.rotation = Quaternion.Euler(((Quaternion)val).eulerAngles + new Vector3(0f, 180f, 0f));
			Transform transform2 = ((Component)VRRig.LocalRig.rightHand.rigTarget).transform;
			rotation2 = ((Component)VRRig.LocalRig).transform.rotation;
			val = rotation2;
			transform2.rotation = Quaternion.Euler(((Quaternion)val).eulerAngles + new Vector3(0f, 180f, 0f));
			((Component)VRRig.LocalRig.head.rigTarget).transform.rotation = rotation;
		});
	}

	public static void SpiderMonke()
	{
		InputHandler.UpdateToggleOnPress(InputHandler.IsRightSecondaryPressed, ref InputHandler.InputHandler_State_02, ref InputHandler.InputHandler_State_01);
		if (!InputHandler.InputHandler_State_02)
		{
			ResetSpiderMonke();
			return;
		}
		((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = false;
		Vector3 position = ((Component)Variables.Variables_Reference_09.bodyCollider).transform.position;
		Vector3 val;
		if (!CapturedVariables370_State_04)
		{
			CapturedVariables370_Position_05 = position;
			CapturedVariables370_State_04 = true;
			val = position - CapturedVariables370_Position_05;
			float num = ((Vector3)val).magnitude / Mathf.Max(Time.deltaTime, 0.0001f);
			CapturedVariables370_Position_05 = position;
			CapturedVariables370_Value_03 += num * Time.deltaTime * 1.6f;
			CapturedVariables370_Value_11 += Time.deltaTime;
			((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position = position + new Vector3(0f, 0.02f, 0f);
			SetRigHeadRotation(80f, HeadY, Mathf.Sin(CapturedVariables370_Value_11 * 4f) * 5f);
			if (num > 0.4f)
			{
				goto Branch_0215;
			}
		}
		else
		{
			val = position - CapturedVariables370_Position_05;
			float num = ((Vector3)val).magnitude / Mathf.Max(Time.deltaTime, 0.0001f);
			CapturedVariables370_Position_05 = position;
			CapturedVariables370_Value_03 += num * Time.deltaTime * 1.6f;
			CapturedVariables370_Value_11 += Time.deltaTime;
			((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position = position + new Vector3(0f, 0.02f, 0f);
			SetRigHeadRotation(80f, HeadY, Mathf.Sin(CapturedVariables370_Value_11 * 4f) * 5f);
			if (num > 0.4f)
			{
				goto Branch_0215;
			}
		}
		((Component)Variables.Variables_Reference_09.offlineVRRig.head.rigTarget).transform.rotation = Quaternion.Euler(-20f, HeadY + Mathf.Sin(CapturedVariables370_Value_11 * 0.8f) * 35f, Mathf.Sin(CapturedVariables370_Value_11 * 0.6f) * 10f);
		float phase = CapturedVariables370_Value_03 % 1f;
		float phase2 = (CapturedVariables370_Value_03 + 0.5f) % 1f;
		UpdateWalkingHand(Variables.Variables_Reference_09.offlineVRRig.leftHand.rigTarget, phase, -0.45f);
		UpdateWalkingHand(Variables.Variables_Reference_09.offlineVRRig.rightHand.rigTarget, phase2, 0.45f);
		((Component)Variables.Variables_Reference_09.offlineVRRig.leftHand.rigTarget).transform.rotation = ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.rotation;
		((Component)Variables.Variables_Reference_09.offlineVRRig.rightHand.rigTarget).transform.rotation = ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.rotation;
		return;
		Branch_0215:
		((Component)Variables.Variables_Reference_09.offlineVRRig.head.rigTarget).transform.rotation = Quaternion.Euler(-25f, HeadY + Mathf.Sin(CapturedVariables370_Value_03 * 5f) * 10f, Mathf.Sin(CapturedVariables370_Value_03 * 6f) * 8f);
		phase = CapturedVariables370_Value_03 % 1f;
		phase2 = (CapturedVariables370_Value_03 + 0.5f) % 1f;
		UpdateWalkingHand(Variables.Variables_Reference_09.offlineVRRig.leftHand.rigTarget, phase, -0.45f);
		UpdateWalkingHand(Variables.Variables_Reference_09.offlineVRRig.rightHand.rigTarget, phase2, 0.45f);
		((Component)Variables.Variables_Reference_09.offlineVRRig.leftHand.rigTarget).transform.rotation = ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.rotation;
		((Component)Variables.Variables_Reference_09.offlineVRRig.rightHand.rigTarget).transform.rotation = ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.rotation;
	}

	public static void MirrorPlayerGun()
	{
		VRRig offlineVRRig;
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
				if ((Object)(object)GunLib.GunLib_Reference_06 == (Object)null || (Object)(object)Variables.Variables_Reference_09?.offlineVRRig == (Object)null)
				{
					return;
				}
				offlineVRRig = Variables.Variables_Reference_09.offlineVRRig;
				((Behaviour)offlineVRRig).enabled = false;
				Vector3 position = ((Component)GunLib.GunLib_Reference_06).transform.position + ((Component)GunLib.GunLib_Reference_06).transform.forward * 1.5f;
				((Component)offlineVRRig).transform.position = position;
				((Component)offlineVRRig).transform.rotation = Quaternion.Euler(0f, ((Component)GunLib.GunLib_Reference_06).transform.eulerAngles.y + 180f, 0f);
				Vector3 val = ((Component)GunLib.GunLib_Reference_06).transform.InverseTransformPoint(GunLib.GunLib_Reference_06.leftHand.rigTarget.position);
				Vector3 val2 = ((Component)GunLib.GunLib_Reference_06).transform.InverseTransformPoint(GunLib.GunLib_Reference_06.rightHand.rigTarget.position);
				val.x *= -1f;
				val2.x *= -1f;
				((Component)offlineVRRig.rightHand.rigTarget).transform.position = ((Component)offlineVRRig).transform.TransformPoint(val);
				((Component)offlineVRRig.leftHand.rigTarget).transform.position = ((Component)offlineVRRig).transform.TransformPoint(val2);
				Quaternion rotation = GunLib.GunLib_Reference_06.leftHand.rigTarget.rotation;
				Quaternion rotation2 = GunLib.GunLib_Reference_06.rightHand.rigTarget.rotation;
				((Component)offlineVRRig.rightHand.rigTarget).transform.rotation = new Quaternion(0f - rotation.x, rotation.y, rotation.z, 0f - rotation.w);
				((Component)offlineVRRig.leftHand.rigTarget).transform.rotation = new Quaternion(0f - rotation2.x, rotation2.y, rotation2.z, 0f - rotation2.w);
				offlineVRRig.head.rigTarget.rotation = Quaternion.Euler(GunLib.GunLib_Reference_06.head.rigTarget.eulerAngles.x, GunLib.GunLib_Reference_06.head.rigTarget.eulerAngles.y + 180f, 0f - GunLib.GunLib_Reference_06.head.rigTarget.eulerAngles.z);
				if (GunLib.GunLib_Reference_06.leftIndex != null)
				{
					((VRMap)offlineVRRig.rightIndex).calcT = ((VRMap)GunLib.GunLib_Reference_06.leftIndex).calcT;
					((VRMap)offlineVRRig.rightIndex).LerpFinger(1f, false);
					if (GunLib.GunLib_Reference_06.leftMiddle != null)
					{
						goto Branch_03af;
					}
				}
				else if (GunLib.GunLib_Reference_06.leftMiddle != null)
				{
					goto Branch_03af;
				}
				if (GunLib.GunLib_Reference_06.leftThumb == null)
				{
					goto Branch_0468;
				}
				goto Branch_041c;
			}
			((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = true;
			GunLib.GunLib_Reference_06 = null;
			return;
		}
		GunLib.GunLib_Reference_06 = null;
		GunLib.SetGunVisualsVisible(isVisible: false);
		return;
		Branch_04d5:
		if (GunLib.GunLib_Reference_06.rightMiddle == null)
		{
			goto Branch_0542;
		}
		Branch_04f6:
		((VRMap)offlineVRRig.leftMiddle).calcT = ((VRMap)GunLib.GunLib_Reference_06.rightMiddle).calcT;
		((VRMap)offlineVRRig.leftMiddle).LerpFinger(1f, false);
		if (GunLib.GunLib_Reference_06.rightThumb == null)
		{
			return;
		}
		goto Branch_0563;
		Branch_0542:
		if (GunLib.GunLib_Reference_06.rightThumb == null)
		{
			return;
		}
		goto Branch_0563;
		Branch_0489:
		((VRMap)offlineVRRig.leftIndex).calcT = ((VRMap)GunLib.GunLib_Reference_06.rightIndex).calcT;
		((VRMap)offlineVRRig.leftIndex).LerpFinger(1f, false);
		if (GunLib.GunLib_Reference_06.rightMiddle == null)
		{
			goto Branch_0542;
		}
		goto Branch_04f6;
		Branch_03af:
		((VRMap)offlineVRRig.rightMiddle).calcT = ((VRMap)GunLib.GunLib_Reference_06.leftMiddle).calcT;
		((VRMap)offlineVRRig.rightMiddle).LerpFinger(1f, false);
		if (GunLib.GunLib_Reference_06.leftThumb == null)
		{
			goto Branch_0468;
		}
		Branch_041c:
		((VRMap)offlineVRRig.rightThumb).calcT = ((VRMap)GunLib.GunLib_Reference_06.leftThumb).calcT;
		((VRMap)offlineVRRig.rightThumb).LerpFinger(1f, false);
		if (GunLib.GunLib_Reference_06.rightIndex == null)
		{
			goto Branch_04d5;
		}
		goto Branch_0489;
		Branch_0563:
		((VRMap)offlineVRRig.leftThumb).calcT = ((VRMap)GunLib.GunLib_Reference_06.rightThumb).calcT;
		((VRMap)offlineVRRig.leftThumb).LerpFinger(1f, false);
		return;
		Branch_0468:
		if (GunLib.GunLib_Reference_06.rightIndex == null)
		{
			goto Branch_04d5;
		}
		goto Branch_0489;
	}

	public static void RagdollMonke()
	{
		InputHandler.UpdateToggleOnPress(InputHandler.IsRightSecondaryPressed, ref InputHandler.InputHandler_State_02, ref InputHandler.InputHandler_State_01);
		if (!InputHandler.InputHandler_State_02)
		{
			if ((Object)(object)CapturedVariables370_Reference_01 != (Object)null)
			{
				ResetRagdollMonke();
			}
			return;
		}
		((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = false;
		if ((Object)(object)CapturedVariables370_Reference_01 == (Object)null)
		{
			CapturedVariables370_Object_01 = new GameObject("RagdollPhysics");
			CapturedVariables370_Reference_02 = CapturedVariables370_Object_01.AddComponent<BoxCollider>();
			CapturedVariables370_Reference_02.size = new Vector3(0.4f, 0.9f, 0.4f);
			CapturedVariables370_Reference_02.center = new Vector3(0f, -0.3f, 0f);
			CapturedVariables370_Reference_01 = CapturedVariables370_Object_01.AddComponent<Rigidbody>();
			CapturedVariables370_Reference_01.isKinematic = false;
			CapturedVariables370_Reference_01.mass = 1f;
			CapturedVariables370_Reference_01.drag = 0.5f;
			CapturedVariables370_Reference_01.angularDrag = 0.5f;
			CapturedVariables370_Reference_01.position = ((Component)Variables.Variables_Reference_09.bodyCollider).transform.position;
			CapturedVariables370_Object_01.layer = 8;
			((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position = CapturedVariables370_Object_01.transform.position;
			((Component)Variables.Variables_Reference_09.offlineVRRig).transform.rotation = CapturedVariables370_Object_01.transform.rotation;
		}
		else
		{
			((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position = CapturedVariables370_Object_01.transform.position;
			((Component)Variables.Variables_Reference_09.offlineVRRig).transform.rotation = CapturedVariables370_Object_01.transform.rotation;
		}
	}

	public static void GlitchMonke()
	{
		InputHandler.UpdateToggleOnPress(InputHandler.IsRightSecondaryPressed, ref InputHandler.InputHandler_State_02, ref InputHandler.InputHandler_State_01);
		if (!InputHandler.InputHandler_State_02)
		{
			ResetGlitchMonke();
			return;
		}
		((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = false;
		CapturedVariables370_Value_02 += Time.deltaTime;
		CapturedVariables370_Value_08 += Time.deltaTime;
		if (CapturedVariables370_Value_08 > 0.05f)
		{
			CapturedVariables370_Value_08 = 0f;
			CapturedVariables370_Position_04 = new Vector3(Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f));
			((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position = ((Component)Variables.Variables_Reference_09.bodyCollider).transform.position + new Vector3(0f, 0.15f, 0f) + CapturedVariables370_Position_04;
			SetRigHeadRotation(Random.Range(-5f, 5f), HeadY + Random.Range(-8f, 8f), Random.Range(-5f, 5f));
			SetRigHandPoses(((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.right * -0.4f + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.up * 0.1f, ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.right * 0.4f + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.up * 0.1f);
		}
		else
		{
			((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position = ((Component)Variables.Variables_Reference_09.bodyCollider).transform.position + new Vector3(0f, 0.15f, 0f) + CapturedVariables370_Position_04;
			SetRigHeadRotation(Random.Range(-5f, 5f), HeadY + Random.Range(-8f, 8f), Random.Range(-5f, 5f));
			SetRigHandPoses(((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.right * -0.4f + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.up * 0.1f, ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.right * 0.4f + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.up * 0.1f);
		}
	}

	public static void ResetCartwheel()
	{
		CapturedVariables370_Value_12 = 0f;
		SetLocalRigEnabled(rigStatus: true);
	}

	private static void SetRigHeadRotation(float x, float y, float z)
	{
		Transform transform = ((Component)Variables.Variables_Reference_09.offlineVRRig).transform;
		Quaternion rotation = (((Component)Variables.Variables_Reference_09.offlineVRRig.head.rigTarget).transform.rotation = Quaternion.Euler(x, y, z));
		transform.rotation = rotation;
	}

	public static void OnlyVisibleToPlayerGun()
	{
		if (GunLib.TrySelectRig())
		{
			MenuPatches.SerializationPatch.SerializationPatch_State_01 = delegate
			{
				CapturedVariables370 LocalScope3 = new CapturedVariables370();
				LocalScope3.target = RigManager.GetPlayer(GunLib.GunLib_Reference_06);
				PhotonSerializer.SerializeAllViews(exclude: true, (PhotonView[])(object)new PhotonView[1] { VRRig.LocalRig.GetPhotonView() });
				Vector3 position = ((Component)VRRig.LocalRig).transform.position;
				PhotonView pv = VRRig.LocalRig.GetPhotonView();
				RaiseEventOptions val = new RaiseEventOptions();
				val.TargetActors = new int[1] { LocalScope3.target.ActorNumber };
				PhotonSerializer.SerializePhotonView(pv, val);
				((Component)VRRig.LocalRig).transform.position = new Vector3(Random.Range(-99999f, 99999f), 99999f, Random.Range(-99999f, 99999f));
				PhotonSerializer.SerializePhotonView(VRRig.LocalRig.GetPhotonView(), new RaiseEventOptions
				{
					TargetActors = (from plr in PhotonNetwork.PlayerList
						where plr.ActorNumber != LocalScope3.target.ActorNumber
						select plr.ActorNumber).ToArray()
				});
				Safety.ResetNetworkLimits();
				((Component)VRRig.LocalRig).transform.position = position;
				return false;
			};
		}
		else
		{
			MenuPatches.SerializationPatch.SerializationPatch_State_01 = null;
		}
	}

	public static void OnlyInvisibleToPlayerGun()
	{
		if (GunLib.TrySelectRig())
		{
			MenuPatches.SerializationPatch.SerializationPatch_State_01 = delegate
			{
				CapturedVariables360 LocalScope3 = new CapturedVariables360();
				LocalScope3.target = RigManager.GetPlayer(GunLib.GunLib_Reference_06);
				PhotonSerializer.SerializeAllViews(exclude: true, (PhotonView[])(object)new PhotonView[1] { VRRig.LocalRig.GetPhotonView() });
				Vector3 position = ((Component)VRRig.LocalRig).transform.position;
				PhotonSerializer.SerializePhotonView(VRRig.LocalRig.GetPhotonView(), new RaiseEventOptions
				{
					TargetActors = (from plr in PhotonNetwork.PlayerList
						where plr.ActorNumber != LocalScope3.target.ActorNumber
						select plr.ActorNumber).ToArray()
				});
				((Component)VRRig.LocalRig).transform.position = new Vector3(Random.Range(-99999f, 99999f), 99999f, Random.Range(-99999f, 99999f));
				PhotonView pv = VRRig.LocalRig.GetPhotonView();
				RaiseEventOptions val = new RaiseEventOptions();
				val.TargetActors = new int[1] { LocalScope3.target.ActorNumber };
				PhotonSerializer.SerializePhotonView(pv, val);
				Safety.ResetNetworkLimits();
				((Component)VRRig.LocalRig).transform.position = position;
				return false;
			};
		}
		else
		{
			MenuPatches.SerializationPatch.SerializationPatch_State_01 = null;
		}
	}

	public static void TeleportGun()
	{
		if (GunLib.GunGrips)
		{
			GunLib.UpdateGunRaycast();
			if (GunLib.GunTriggers)
			{
				if (!CapturedVariables370_State_02 && (Object)(object)((RaycastHit)GunLib.GunLib_Reference_07).collider != (Object)null)
				{
					Movement.TeleportToPosition(((RaycastHit)GunLib.GunLib_Reference_07).point + new Vector3(0f, 1f, -0.5f));
					Variables.Variables_Reference_09.rigidbody.velocity = Vector3.zero;
					CapturedVariables370_State_02 = true;
				}
			}
			else
			{
				CapturedVariables370_State_02 = false;
			}
		}
		else
		{
			GunLib.SetGunVisualsVisible(isVisible: false);
		}
	}

	public static void HelicopterMonke()
	{
		if (InputHandler.IsRightTriggerPressed())
		{
			((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = false;
			Transform transform = ((Component)Variables.Variables_Reference_09.offlineVRRig).transform;
			transform.position += new Vector3(0f, 0.075f, 0f);
			Transform transform2 = ((Component)Variables.Variables_Reference_09.offlineVRRig).transform;
			Quaternion rotation = ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.rotation;
			transform2.rotation = Quaternion.Euler(((Quaternion)rotation).eulerAngles + new Vector3(0f, 10f, 0f));
			((Component)Variables.Variables_Reference_09.offlineVRRig.leftHand.rigTarget).transform.position = ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.right * -1f;
			((Component)Variables.Variables_Reference_09.offlineVRRig.rightHand.rigTarget).transform.position = ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.right * 1f;
		}
		else
		{
			((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = true;
		}
	}

	public static void SpazMonke()
	{
		InputHandler.UpdateToggleOnPress(InputHandler.IsRightSecondaryPressed, ref InputHandler.InputHandler_State_02, ref InputHandler.InputHandler_State_01);
		if (!InputHandler.InputHandler_State_02)
		{
			ResetHeadPose();
			return;
		}
		Vector3 val = new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
		SetHeadPose(val);
		Variables.Variables_Reference_09.offlineVRRig.rightHand.rigTarget.eulerAngles = val;
		Variables.Variables_Reference_09.offlineVRRig.leftHand.rigTarget.eulerAngles = val;
	}

	public static void FlyTowardsGun()
	{
		if (GunLib.TrySelectRig())
		{
			Vector3 val = ((Component)GunLib.GunLib_Reference_06).transform.position - ((Component)Variables.Variables_Reference_06).transform.position;
			Vector3 normalized = ((Vector3)val).normalized;
			Transform transform = ((Component)Variables.Variables_Reference_06).transform;
			transform.position += normalized * Time.deltaTime * 10f;
			((Component)Variables.Variables_Reference_06).GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
		}
	}

	public static void GhostAndInvisibility()
	{
		InputHandler.UpdateToggleOnPress(InputHandler.IsRightPrimaryPressed, ref InputHandler.InputHandler_State_02, ref InputHandler.InputHandler_State_01);
		InputHandler.UpdateToggleOnPress(InputHandler.IsRightSecondaryPressed, ref InputHandler.InputHandler_State_04, ref InputHandler.InputHandler_State_03);
		((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = !InputHandler.InputHandler_State_02 && !InputHandler.InputHandler_State_04;
		if (InputHandler.InputHandler_State_04)
		{
			((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position = new Vector3(999f, 999f, 999f);
		}
	}

	public static void OrbitAll()
	{
		PhotonSerializer.OverrideSerializedPose(delegate(VRRig targetRig)
		{
			((Component)VRRig.LocalRig).transform.LookAt(((Component)targetRig).transform.position);
			((Component)VRRig.LocalRig).transform.RotateAround(((Component)targetRig).transform.position, Vector3.up, CapturedVariables370_Value_06 * Time.deltaTime);
			Vector3 val = ((Component)VRRig.LocalRig).transform.position - ((Component)targetRig).transform.position;
			Vector3 position = ((Vector3)val).normalized * CapturedVariables370_Value_13 + ((Component)targetRig).transform.position;
			((Component)VRRig.LocalRig).transform.position = position;
			((Component)VRRig.LocalRig.leftHand.rigTarget).transform.position = ((Component)VRRig.LocalRig).transform.position + ((Component)VRRig.LocalRig).transform.right * -1.5f;
			((Component)VRRig.LocalRig.rightHand.rigTarget).transform.position = ((Component)VRRig.LocalRig).transform.position + ((Component)VRRig.LocalRig).transform.right * 1.5f;
			((Component)VRRig.LocalRig.leftHand.rigTarget).transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
			((Component)VRRig.LocalRig.rightHand.rigTarget).transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
		});
	}

	public static void Frontflip()
	{
		if (!InputHandler.IsRightTriggerPressed())
		{
			CapturedVariables370_Value_07 = 0f;
			((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = true;
			return;
		}
		((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = false;
		((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position = ((Component)Variables.Variables_Reference_09.bodyCollider).transform.position + new Vector3(0f, 0.15f, 0f);
		CapturedVariables370_Value_07 = (CapturedVariables370_Value_07 + 360f * Time.deltaTime) % 360f;
		SetRigHeadRotation(CapturedVariables370_Value_07, HeadY, 0f);
		SetRigHandPoses(((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.right * -0.6f + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.up * 0.2f, ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.right * 0.6f + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.up * 0.2f);
	}

	public static void SetHeadPose(Vector3? headRotation = null, bool spin = false)
	{
		if (!CapturedVariables370_Position_06.HasValue)
		{
			CapturedVariables370_Position_06 = Variables.Variables_Reference_09.offlineVRRig.head.rigTarget.eulerAngles;
			if (spin)
			{
				goto Branch_006e;
			}
		}
		else if (spin)
		{
			goto Branch_006e;
		}
		CapturedVariables370_Value_05 = 0f;
		if (headRotation.HasValue)
		{
			Variables.Variables_Reference_09.offlineVRRig.head.rigTarget.eulerAngles = headRotation.Value;
		}
		return;
		Branch_006e:
		CapturedVariables370_Value_05 += 5f;
		Variables.Variables_Reference_09.offlineVRRig.head.rigTarget.eulerAngles = new Vector3(0f, CapturedVariables370_Value_05, 0f);
	}

	public static void WobblyMonke()
	{
		InputHandler.UpdateToggleOnPress(InputHandler.IsRightSecondaryPressed, ref InputHandler.InputHandler_State_02, ref InputHandler.InputHandler_State_01);
		if (!InputHandler.InputHandler_State_02)
		{
			ResetWobblyMonke();
			return;
		}
		((Behaviour)Variables.Variables_Reference_09.offlineVRRig).enabled = false;
		CapturedVariables370_Value_01 += Time.deltaTime;
		((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position = ((Component)Variables.Variables_Reference_09.bodyCollider).transform.position + new Vector3(0f, 0.15f + Mathf.Sin(CapturedVariables370_Value_01 * 3f) * 0.05f, 0f);
		SetRigHeadRotation(Mathf.Sin(CapturedVariables370_Value_01 * 2.5f) * 15f, HeadY, Mathf.Cos(CapturedVariables370_Value_01 * 2f) * 15f);
		float num = Mathf.Sin(CapturedVariables370_Value_01 * 3f) * 0.15f;
		float num2 = Mathf.Cos(CapturedVariables370_Value_01 * 3f) * 0.15f;
		SetRigHandPoses(((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.right * -0.5f + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.up * (0.2f + num), ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.position + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.right * 0.5f + ((Component)Variables.Variables_Reference_09.offlineVRRig).transform.up * (0.2f + num2));
	}
}
