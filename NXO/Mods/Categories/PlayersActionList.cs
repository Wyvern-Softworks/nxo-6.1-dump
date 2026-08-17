using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using NXO.Menu;
using NXO.Utilities;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace NXO.Mods.Categories;

public class PlayersActionList
{
	[CompilerGenerated]
	private sealed class CapturedVariables60
	{
		public NetPlayer capturedPlayer;

		internal void GeneratePlayerButtons_Lambda1()
		{
			CapturedVariables70_Reference_03 = capturedPlayer;
			BuildPlayerActions();
			ButtonHandler.NavigateToCategory(Category.Player_Action);
		}
	}

	[CompilerGenerated]
	private sealed class CapturedVariables70
	{
		public VRRig targetRig;

		internal void GeneratePlayer_ActionButtons_Lambda9()
		{
			Movement.TeleportToPosition(((Component)targetRig).transform.position);
		}

		internal void GeneratePlayer_ActionButtons_Lambda2()
		{
			Overpowered.SendLagEvents(new int[1] { targetRig.Creator.ActorNumber });
		}

		internal void GeneratePlayer_ActionButtons_Lambda3()
		{
			Overpowered.SendDeafenEvent(targetRig);
		}

		internal void GeneratePlayer_ActionButtons_Lambda1()
		{
			if ((Object)(object)targetRig != (Object)null)
			{
				if (!RigManager.IsTagged(VRRig.LocalRig))
				{
					NotificationLib.ShowNotification(NotificationLib.NotificationType.Error, "Must Be Tagged");
				}
				else if (RigManager.IsTagged(targetRig))
				{
					NotificationLib.ShowNotification(NotificationLib.NotificationType.Error, "`" + targetRig.Creator.NickName + "` Is Already Tagged");
				}
				else
				{
					Gamemode.TagRig(targetRig);
				}
			}
		}

		internal void GeneratePlayer_ActionButtons_Lambda6()
		{
			if ((Object)(object)targetRig != (Object)null)
			{
				Movement.TeleportToPosition(((Component)targetRig).transform.position + ((Component)targetRig).transform.up * 0.3f - ((Component)targetRig).transform.forward * 0.4f);
			}
		}

		internal void GeneratePlayer_ActionButtons_Lambda4()
		{
			Player.CopyRigPose(targetRig);
		}
	}

	public static NetPlayer CapturedVariables70_Reference_03;

	public static Camera CapturedVariables70_Reference_02;

	public static GameObject CapturedVariables70_Object_02;

	public static RenderTexture CapturedVariables70_Reference_01;

	public static bool CapturedVariables70_State_01;

	public static GameObject CapturedVariables70_Object_01;

	public static IEnumerator RefreshPlayersListDelayed()
	{
		yield return (object)new WaitForSeconds(0.5f);
		if (Variables.currentPage == Category.Players)
		{
			ResetPlayersList();
		}
	}

	public static void SpectateSelectedPlayer()
	{
		InputHandler.UpdateToggleOnPress(InputHandler.IsLeftPrimaryPressed, ref InputHandler.InputHandler_State_02, ref InputHandler.InputHandler_State_01);
		if (!InputHandler.InputHandler_State_02)
		{
			Visuals.ResetCamera();
			return;
		}
		VRRig val = RigManager.FindRig(CapturedVariables70_Reference_03);
		if ((Object)(object)val == (Object)null)
		{
			Visuals.ResetCamera();
		}
		else if ((Object)(object)Visuals.CapturedVariables580_Object_01 == (Object)null)
		{
			Visuals.CapturedVariables580_Object_01 = new GameObject("NXO_Cam");
			Visuals.CapturedVariables580_Object_01.transform.position = ((Component)Variables.Variables_Reference_09.headCollider).transform.position;
			Camera val2 = Visuals.CapturedVariables580_Object_01.GetComponent<Camera>() ?? Visuals.CapturedVariables580_Object_01.AddComponent<Camera>();
			val2.nearClipPlane = 0.01f;
			val2.cameraType = (CameraType)1;
			Visuals.CapturedVariables580_Object_01.transform.position = val.bodyTransform.TransformPoint(new Vector3(0f, 0.8f, -1.5f));
			Visuals.CapturedVariables580_Object_01.transform.rotation = val.headMesh.transform.rotation;
		}
		else
		{
			Camera val2 = Visuals.CapturedVariables580_Object_01.GetComponent<Camera>() ?? Visuals.CapturedVariables580_Object_01.AddComponent<Camera>();
			val2.nearClipPlane = 0.01f;
			val2.cameraType = (CameraType)1;
			Visuals.CapturedVariables580_Object_01.transform.position = val.bodyTransform.TransformPoint(new Vector3(0f, 0.8f, -1.5f));
			Visuals.CapturedVariables580_Object_01.transform.rotation = val.headMesh.transform.rotation;
		}
	}

	public static void ResetPlayersList()
	{
		List<ButtonHandler.Button> list = ModButtons.buttons.ToList();
		list.RemoveAll((ButtonHandler.Button b) => b.Page == Category.Players);
		PhotonPlayer[] playerList = PhotonNetwork.PlayerList;
		int num = 0;
		while (num < playerList.Length)
		{
			PhotonPlayer val = playerList[num];
			CapturedVariables60 LocalScope5 = new CapturedVariables60();
			LocalScope5.capturedPlayer = (NetPlayer)val;
			VRRig val2 = RigManager.FindRig((NetPlayer)val);
			string text = val.NickName;
			if ((Object)(object)val2 != (Object)null)
			{
				SkinnedMeshRenderer mainSkin = val2.mainSkin;
				if ((Object)(object)((mainSkin != null) ? ((Renderer)mainSkin).material : null) != (Object)null)
				{
					string text2 = ColorUtility.ToHtmlStringRGB(((Renderer)val2.mainSkin).material.color);
					text = "<color=#" + text2 + ">" + val.NickName + "</color>";
					if (!RigManager.IsTagged(RigManager.FindRig(LocalScope5.capturedPlayer)))
					{
						goto Branch_01a6;
					}
					goto Branch_0161;
				}
			}
			if (!RigManager.IsTagged(RigManager.FindRig(LocalScope5.capturedPlayer)))
			{
				goto Branch_01a6;
			}
			Branch_0161:
			text += " : <color=red>Tagged</color>";
			list.Add(new ButtonHandler.Button(text, Category.Players, isToggle: false, isActive: false, delegate
			{
				CapturedVariables70_Reference_03 = LocalScope5.capturedPlayer;
				BuildPlayerActions();
				ButtonHandler.NavigateToCategory(Category.Player_Action);
			})
			{
				isCategory = true
			});
			num++;
			continue;
			Branch_01a6:
			list.Add(new ButtonHandler.Button(text, Category.Players, isToggle: false, isActive: false, delegate
			{
				CapturedVariables70_Reference_03 = LocalScope5.capturedPlayer;
				BuildPlayerActions();
				ButtonHandler.NavigateToCategory(Category.Player_Action);
			})
			{
				isCategory = true
			});
			num++;
		}
		ModButtons.buttons = list.ToArray();
		Main.RebuildMenu();
	}

	public static void CreatePlayerCameraDisplay()
	{
		if (CapturedVariables70_Reference_03 == null || (Object)(object)Variables.Variables_Object_14 == (Object)null)
		{
			return;
		}
		if ((Object)(object)RigManager.FindRig(CapturedVariables70_Reference_03) == (Object)null)
		{
			NotificationLib.ShowNotification(NotificationLib.NotificationType.Error, "Players Rig Is Null, Please Refresh");
			return;
		}
		if ((Object)(object)CapturedVariables70_Object_02 != (Object)null)
		{
			Object.Destroy((Object)(object)CapturedVariables70_Object_02);
			CapturedVariables70_Object_02 = null;
			if ((Object)(object)CapturedVariables70_Reference_02 == (Object)null)
			{
				goto Branch_00fc;
			}
		}
		else if ((Object)(object)CapturedVariables70_Reference_02 == (Object)null)
		{
			goto Branch_00fc;
		}
		CapturedVariables70_Object_02 = GameObject.CreatePrimitive((PrimitiveType)3);
		((Object)CapturedVariables70_Object_02).name = "NXO Player Camera Screen";
		CapturedVariables70_Object_02.transform.SetParent(Variables.Variables_Object_14.transform, false);
		CapturedVariables70_Object_02.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
		CapturedVariables70_Object_02.transform.localScale = new Vector3(0.01f, 0.5f, 0.75f);
		CapturedVariables70_Object_02.transform.localPosition = new Vector3(0.064f, 0.85f, 0.05f);
		Renderer component = CapturedVariables70_Object_02.GetComponent<Renderer>();
		if (!((Object)(object)component != (Object)null))
		{
			goto Branch_037e;
		}
		Branch_032b:
		Material val = new Material(Shader.Find("Unlit/Texture"));
		val.mainTexture = (Texture)(object)CapturedVariables70_Reference_01;
		component.material = val;
		Main.CapturedVariables1950_Material_05.Add(val);
		Collider component2 = CapturedVariables70_Object_02.GetComponent<Collider>();
		if (!((Object)(object)component2 != (Object)null))
		{
			goto Branch_03cb;
		}
		goto Branch_03a4;
		Branch_037e:
		component2 = CapturedVariables70_Object_02.GetComponent<Collider>();
		if (!((Object)(object)component2 != (Object)null))
		{
			goto Branch_03cb;
		}
		goto Branch_03a4;
		Branch_03cb:
		Material val2 = Main.CreatePinwheelMaterial();
		if (!((Object)(object)val2 != (Object)null))
		{
			goto Branch_0617;
		}
		goto Branch_03ec;
		Branch_03a4:
		Object.Destroy((Object)(object)component2);
		val2 = Main.CreatePinwheelMaterial();
		if (!((Object)(object)val2 != (Object)null))
		{
			goto Branch_0617;
		}
		goto Branch_03ec;
		Branch_0476:
		BoxCollider component3;
		Object.Destroy((Object)(object)component3);
		CapturedVariables70_Object_01.transform.SetParent(Variables.Variables_Object_14.transform, false);
		CapturedVariables70_Object_01.transform.localPosition = CapturedVariables70_Object_02.transform.localPosition;
		CapturedVariables70_Object_01.transform.localRotation = CapturedVariables70_Object_02.transform.localRotation;
		Vector3 localScale = CapturedVariables70_Object_02.transform.localScale;
		CapturedVariables70_Object_01.transform.localScale = new Vector3(localScale.x - 0.0025f, localScale.y + 0.015f, localScale.z + 0.015f);
		CapturedVariables70_Object_01.GetComponent<Renderer>().material = val2;
		Main.CapturedVariables1950_Material_05.Add(val2);
		CapturedVariables70_State_01 = true;
		return;
		Branch_0617:
		CapturedVariables70_State_01 = true;
		return;
		Branch_00fc:
		CapturedVariables70_Reference_02 = new GameObject("NXO Player Camera").AddComponent<Camera>();
		CapturedVariables70_Reference_01 = new RenderTexture(512, 512, 16, (RenderTextureFormat)0);
		((Texture)CapturedVariables70_Reference_01).filterMode = (FilterMode)1;
		((Texture)CapturedVariables70_Reference_01).wrapMode = (TextureWrapMode)1;
		CapturedVariables70_Reference_01.Create();
		CapturedVariables70_Reference_02.targetTexture = CapturedVariables70_Reference_01;
		CapturedVariables70_Reference_02.fieldOfView = 90f;
		CapturedVariables70_Reference_02.nearClipPlane = 0.01f;
		CapturedVariables70_Reference_02.farClipPlane = 1000f;
		CapturedVariables70_Reference_02.clearFlags = (CameraClearFlags)1;
		((Behaviour)CapturedVariables70_Reference_02).enabled = true;
		CapturedVariables70_Reference_02.cullingMask = -1;
		CapturedVariables70_Object_02 = GameObject.CreatePrimitive((PrimitiveType)3);
		((Object)CapturedVariables70_Object_02).name = "NXO Player Camera Screen";
		CapturedVariables70_Object_02.transform.SetParent(Variables.Variables_Object_14.transform, false);
		CapturedVariables70_Object_02.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
		CapturedVariables70_Object_02.transform.localScale = new Vector3(0.01f, 0.5f, 0.75f);
		CapturedVariables70_Object_02.transform.localPosition = new Vector3(0.064f, 0.85f, 0.05f);
		component = CapturedVariables70_Object_02.GetComponent<Renderer>();
		if (!((Object)(object)component != (Object)null))
		{
			goto Branch_037e;
		}
		goto Branch_032b;
		Branch_03ec:
		CapturedVariables70_Object_01 = GameObject.CreatePrimitive((PrimitiveType)3);
		Rigidbody component4 = CapturedVariables70_Object_01.GetComponent<Rigidbody>();
		if ((Object)(object)component4 != (Object)null)
		{
			Object.Destroy((Object)(object)component4);
			component3 = CapturedVariables70_Object_01.GetComponent<BoxCollider>();
			if ((Object)(object)component3 != (Object)null)
			{
				goto Branch_0476;
			}
		}
		else
		{
			component3 = CapturedVariables70_Object_01.GetComponent<BoxCollider>();
			if ((Object)(object)component3 != (Object)null)
			{
				goto Branch_0476;
			}
		}
		CapturedVariables70_Object_01.transform.SetParent(Variables.Variables_Object_14.transform, false);
		CapturedVariables70_Object_01.transform.localPosition = CapturedVariables70_Object_02.transform.localPosition;
		CapturedVariables70_Object_01.transform.localRotation = CapturedVariables70_Object_02.transform.localRotation;
		localScale = CapturedVariables70_Object_02.transform.localScale;
		CapturedVariables70_Object_01.transform.localScale = new Vector3(localScale.x - 0.0025f, localScale.y + 0.015f, localScale.z + 0.015f);
		CapturedVariables70_Object_01.GetComponent<Renderer>().material = val2;
		Main.CapturedVariables1950_Material_05.Add(val2);
		CapturedVariables70_State_01 = true;
	}

	public static void UpdatePlayerCamera()
	{
		if (CapturedVariables70_State_01 && !((Object)(object)CapturedVariables70_Reference_02 == (Object)null) && CapturedVariables70_Reference_03 != null && Variables.currentPage == Category.Player_Action)
		{
			VRRig val = RigManager.FindRig(CapturedVariables70_Reference_03);
			if ((Object)(object)val == (Object)null)
			{
				ClearPlayerCamera(clearAll: true);
			}
			else if (!((Object)(object)val.head?.rigTarget == (Object)null))
			{
				Transform val2 = val.head.rigTarget;
				Vector3 position = val.head.rigTarget.position - val2.forward * 1f + Vector3.up * 0.25f;
				((Component)CapturedVariables70_Reference_02).transform.position = position;
				((Component)CapturedVariables70_Reference_02).transform.rotation = Quaternion.LookRotation(val2.forward, Vector3.up);
			}
		}
	}

	public PlayersActionList()
	{
	}

	public static void BuildPlayerActions()
	{
		CapturedVariables70 LocalScope17 = new CapturedVariables70();
		List<ButtonHandler.Button> list = ModButtons.buttons.ToList();
		list.RemoveAll((ButtonHandler.Button b) => b.Page == Category.Player_Action);
		LocalScope17.targetRig = RigManager.FindRig(CapturedVariables70_Reference_03);
		list.Add(new ButtonHandler.Button("Return", Category.Player_Action, isToggle: false, isActive: false, delegate
		{
			ClearPlayerCamera(clearAll: true);
			ButtonHandler.NavigateToCategory(Category.Players);
		})
		{
			isCategory = true
		});
		list.Add(new ButtonHandler.Button("Name : " + CapturedVariables70_Reference_03.NickName, Category.Player_Action, isToggle: false, isActive: false, null));
		list.Add(new ButtonHandler.Button("Platform : " + Visuals.GetPlatformLabel(LocalScope17.targetRig), Category.Player_Action, isToggle: false, isActive: false, null));
		list.Add(new ButtonHandler.Button("ID : " + CapturedVariables70_Reference_03.UserId.ToUpper(), Category.Player_Action, isToggle: false, isActive: false, null));
		string[] obj = new string[6] { "Color : ", null, null, null, null, null };
		float num = ((Renderer)LocalScope17.targetRig.mainSkin).material.color.r * 9f;
		float num2 = num;
		obj[1] = num2.ToString();
		obj[2] = ", ";
		num = ((Renderer)LocalScope17.targetRig.mainSkin).material.color.g * 9f;
		num2 = num;
		obj[3] = num2.ToString();
		obj[4] = ", ";
		num = ((Renderer)LocalScope17.targetRig.mainSkin).material.color.b * 9f;
		num2 = num;
		obj[5] = num2.ToString();
		list.Add(new ButtonHandler.Button(string.Concat(obj), Category.Player_Action, isToggle: false, isActive: false, null));
		list.Add(new ButtonHandler.Button("Mod(s) : " + (((Dictionary<object, object>)(object)CapturedVariables70_Reference_03.GetPlayerRef().CustomProperties).Count - 1), Category.Player_Action, isToggle: false, isActive: false, null));
		list.Add(new ButtonHandler.Button("Tag Player", Category.Player_Action, isToggle: true, isActive: false, delegate
		{
			if ((Object)(object)LocalScope17.targetRig != (Object)null)
			{
				if (!RigManager.IsTagged(VRRig.LocalRig))
				{
					NotificationLib.ShowNotification(NotificationLib.NotificationType.Error, "Must Be Tagged");
				}
				else if (RigManager.IsTagged(LocalScope17.targetRig))
				{
					NotificationLib.ShowNotification(NotificationLib.NotificationType.Error, "`" + LocalScope17.targetRig.Creator.NickName + "` Is Already Tagged");
				}
				else
				{
					Gamemode.TagRig(LocalScope17.targetRig);
				}
			}
		}));
		list.Add(new ButtonHandler.Button("Lag Player", Category.Player_Action, isToggle: true, isActive: false, delegate
		{
			Overpowered.SendLagEvents(new int[1] { LocalScope17.targetRig.Creator.ActorNumber });
		}));
		list.Add(new ButtonHandler.Button("Deafen Player", Category.Player_Action, isToggle: true, isActive: false, delegate
		{
			Overpowered.SendDeafenEvent(LocalScope17.targetRig);
		}));
		list.Add(new ButtonHandler.Button("Copy Player", Category.Player_Action, isToggle: true, isActive: false, delegate
		{
			Player.CopyRigPose(LocalScope17.targetRig);
		}, delegate
		{
			Player.SetLocalRigEnabled(rigStatus: true);
		}));
		list.Add(new ButtonHandler.Button("Piggyback Player", Category.Player_Action, isToggle: true, isActive: false, delegate
		{
			if ((Object)(object)LocalScope17.targetRig != (Object)null)
			{
				Movement.TeleportToPosition(((Component)LocalScope17.targetRig).transform.position + ((Component)LocalScope17.targetRig).transform.up * 0.3f - ((Component)LocalScope17.targetRig).transform.forward * 0.4f);
			}
		}));
		list.Add(new ButtonHandler.Button("Spectate Player (X)", Category.Player_Action, isToggle: true, isActive: false, delegate
		{
			SpectateSelectedPlayer();
		}, delegate
		{
			Visuals.ResetCamera();
		}));
		list.Add(new ButtonHandler.Button("Teleport To Player", Category.Player_Action, isToggle: false, isActive: false, delegate
		{
			Movement.TeleportToPosition(((Component)LocalScope17.targetRig).transform.position);
		}, delegate
		{
			Player.SetLocalRigEnabled(rigStatus: true);
		}));
		ModButtons.buttons = list.ToArray();
	}

	public static void ClearPlayerCamera(bool clearAll = false)
	{
		if (clearAll)
		{
			if ((Object)(object)CapturedVariables70_Reference_02 != (Object)null)
			{
				Object.Destroy((Object)(object)((Component)CapturedVariables70_Reference_02).gameObject);
				CapturedVariables70_Reference_02 = null;
				if ((Object)(object)CapturedVariables70_Reference_01 != (Object)null)
				{
					goto Branch_0087;
				}
			}
			else if ((Object)(object)CapturedVariables70_Reference_01 != (Object)null)
			{
				goto Branch_0087;
			}
			CapturedVariables70_Reference_03 = null;
			CapturedVariables70_State_01 = false;
			if ((Object)(object)CapturedVariables70_Object_02 != (Object)null)
			{
				goto Branch_016f;
			}
		}
		else if ((Object)(object)CapturedVariables70_Object_02 != (Object)null)
		{
			goto Branch_016f;
		}
		Branch_019e:
		if (!((Object)(object)CapturedVariables70_Object_01 != (Object)null))
		{
			return;
		}
		goto Branch_01bd;
		Branch_016f:
		Object.Destroy((Object)(object)CapturedVariables70_Object_02);
		CapturedVariables70_Object_02 = null;
		if (!((Object)(object)CapturedVariables70_Object_01 != (Object)null))
		{
			return;
		}
		Branch_01bd:
		Object.Destroy((Object)(object)CapturedVariables70_Object_01);
		CapturedVariables70_Object_01 = null;
		return;
		Branch_0087:
		if (CapturedVariables70_Reference_01.IsCreated())
		{
			CapturedVariables70_Reference_01.Release();
			Object.Destroy((Object)(object)CapturedVariables70_Reference_01);
			CapturedVariables70_Reference_01 = null;
			CapturedVariables70_Reference_03 = null;
			CapturedVariables70_State_01 = false;
			if ((Object)(object)CapturedVariables70_Object_02 != (Object)null)
			{
				goto Branch_016f;
			}
		}
		else
		{
			Object.Destroy((Object)(object)CapturedVariables70_Reference_01);
			CapturedVariables70_Reference_01 = null;
			CapturedVariables70_Reference_03 = null;
			CapturedVariables70_State_01 = false;
			if ((Object)(object)CapturedVariables70_Object_02 != (Object)null)
			{
				goto Branch_016f;
			}
		}
		goto Branch_019e;
	}
}
