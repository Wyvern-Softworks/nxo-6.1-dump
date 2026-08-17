
using BepInEx;
using GorillaLocomotion.Climbing;
using NXO.Menu;
using NXO.Utilities;
using UnityEngine;
using Valve.VR;

namespace NXO.Mods.Categories;

public class Movement
{
	public static bool Movement_State_11;

	public static bool Movement_State_05;

	public static bool Movement_State_02;

	public static bool Movement_State_10;

	public static float Movement_Value_02;

	public static bool Movement_State_01;

	public static Vector3 Movement_Position_03;

	public static float Movement_Value_01;

	public static bool Movement_State_06;

	public static bool Movement_State_04;

	private static Vector3 Movement_Position_07;

	private static Vector3 Movement_Position_01;

	private static GameObject Movement_Object_04;

	private static GameObject Movement_Object_05;

	private static GameObject Movement_Object_03;

	private static GameObject Movement_Object_01;

	private static GameObject Movement_Object_06;

	private static GameObject[] Movement_Object_02;

	private static GameObject[] Movement_Object_07;

	private static Material Movement_Material_01;

	private static readonly Vector3 Movement_Position_06;

	private static readonly Vector3 Movement_Position_04;

	private static readonly Vector3 Movement_Position_05;

	private static readonly Vector3 Movement_Position_02;

	public static (string name, string zone, string pos)[] Recovered_Reference_06;

	private static bool Movement_State_09;

	private static bool Movement_State_03;

	private static bool Movement_State_08;

	private static bool Movement_State_07;

	private static GameObject Movement_Object_08;

	public static void ZeroGravity()
	{
		Variables.Variables_Reference_09.rigidbody.AddForce(Vector3.up * 9.81f, (ForceMode)5);
	}

	public Movement()
	{
	}

	public static void WASDMovement()
	{
		Transform transform = ((Component)Variables.Variables_Reference_09.headCollider).transform;
		float num;
		Vector3 val;
		if (!UnityInput.Current.GetKey((KeyCode)304))
		{
			num = Settings.CapturedVariables3760_Value_14;
			((Component)Variables.Variables_Reference_06).GetComponent<Rigidbody>().linearVelocity = new Vector3(0f, 0.065f, 0f);
			val = Vector3.zero;
			if (UnityInput.Current.GetKey((KeyCode)119))
			{
				goto Branch_00d2;
			}
		}
		else
		{
			num = Settings.CapturedVariables3760_Value_14 + 3f;
			((Component)Variables.Variables_Reference_06).GetComponent<Rigidbody>().linearVelocity = new Vector3(0f, 0.065f, 0f);
			val = Vector3.zero;
			if (UnityInput.Current.GetKey((KeyCode)119))
			{
				goto Branch_00d2;
			}
		}
		if (!UnityInput.Current.GetKey((KeyCode)115))
		{
			goto Branch_014c;
		}
		goto Branch_011f;
		Branch_0381:
		Movement_Position_03 = UnityInput.Current.mousePosition;
		return;
		Branch_0310:
		Vector3 val2 = UnityInput.Current.mousePosition - Movement_Position_03;
		float num2 = 0.1f;
		Transform transform2 = Variables.Variables_Reference_09.mainCamera.transform;
		transform2.localEulerAngles += new Vector3((0f - val2.y) * num2, val2.x * num2, 0f);
		Movement_Position_03 = UnityInput.Current.mousePosition;
		return;
		Branch_00d2:
		val += transform.forward;
		if (!UnityInput.Current.GetKey((KeyCode)115))
		{
			goto Branch_014c;
		}
		goto Branch_011f;
		Branch_02bb:
		Transform transform3 = ((Component)Variables.Variables_Reference_09.headCollider).transform;
		transform3.position += ((Vector3)val).normalized * num * Time.deltaTime;
		if (!UnityInput.Current.GetMouseButton(1))
		{
			goto Branch_0381;
		}
		goto Branch_0310;
		Branch_011f:
		val -= transform.forward;
		if (!UnityInput.Current.GetKey((KeyCode)97))
		{
			goto Branch_0199;
		}
		goto Branch_016c;
		Branch_014c:
		if (!UnityInput.Current.GetKey((KeyCode)97))
		{
			goto Branch_0199;
		}
		Branch_016c:
		val -= transform.right;
		if (!UnityInput.Current.GetKey((KeyCode)100))
		{
			goto Branch_01e6;
		}
		goto Branch_01b9;
		Branch_0199:
		if (!UnityInput.Current.GetKey((KeyCode)100))
		{
			goto Branch_01e6;
		}
		Branch_01b9:
		val += transform.right;
		if (!UnityInput.Current.GetKey((KeyCode)32))
		{
			goto Branch_0236;
		}
		goto Branch_0206;
		Branch_01e6:
		if (!UnityInput.Current.GetKey((KeyCode)32))
		{
			goto Branch_0236;
		}
		Branch_0206:
		val += transform.up;
		if (!UnityInput.Current.GetKey((KeyCode)306))
		{
			goto Branch_02bb;
		}
		goto Branch_0259;
		Branch_0236:
		if (!UnityInput.Current.GetKey((KeyCode)306))
		{
			goto Branch_02bb;
		}
		Branch_0259:
		val -= transform.up;
		Transform transform4 = ((Component)Variables.Variables_Reference_09.headCollider).transform;
		transform4.position += ((Vector3)val).normalized * num * Time.deltaTime;
		if (!UnityInput.Current.GetMouseButton(1))
		{
			goto Branch_0381;
		}
		goto Branch_0310;
	}

	public static void SetWallClimberEnabled(bool enable)
	{
		if (!enable)
		{
			Main.DestroyAndClear<GameObject>(ref Movement_Object_08, 0f);
			return;
		}
		if ((Object)(object)Movement_Object_08 == (Object)null)
		{
			Movement_Object_08 = new GameObject("GR");
			Movement_Object_08.AddComponent<GorillaClimbable>();
			if (InputHandler.IsLeftGripPressed())
			{
				goto Branch_009c;
			}
		}
		else if (InputHandler.IsLeftGripPressed())
		{
			goto Branch_009c;
		}
		Movement_State_07 = false;
		if (!InputHandler.IsRightGripPressed())
		{
			goto Branch_0216;
		}
		goto Branch_0175;
		Branch_009c:
		if (Variables.Variables_Reference_06.IsHandTouching(true) && !Movement_State_07)
		{
			Movement_Object_08.transform.position = Variables.Variables_Reference_09.leftHandTransform.position;
			Movement_State_07 = true;
			Variables.Variables_Reference_06.BeginClimbing(Movement_Object_08.AddComponent<GorillaClimbable>(), Variables.FindCachedGameObject("Player Objects/Player VR Controller/GorillaPlayer/TurnParent/LeftHand Controller/GorillaHandClimber").GetComponent<GorillaHandClimber>(), (GorillaClimbableRef)null);
		}
		if (!InputHandler.IsRightGripPressed())
		{
			goto Branch_0216;
		}
		goto Branch_0175;
		Branch_0216:
		Movement_State_08 = false;
		return;
		Branch_0175:
		if (Variables.Variables_Reference_06.IsHandTouching(false) && !Movement_State_08)
		{
			Movement_Object_08.transform.position = Variables.Variables_Reference_09.rightHandTransform.position;
			Movement_State_08 = true;
			Variables.Variables_Reference_06.BeginClimbing(Movement_Object_08.AddComponent<GorillaClimbable>(), Variables.FindCachedGameObject("Player Objects/Player VR Controller/GorillaPlayer/TurnParent/RightHand Controller/GorillaHandClimber").GetComponent<GorillaHandClimber>(), (GorillaClimbableRef)null);
		}
	}

	public static void SetAntiSlipEnabled(bool setActive)
	{
		Movement_State_10 = setActive;
	}

	private static void SpawnIceTrail(ref GameObject ice, bool grabbing, Transform hand)
	{
		if (grabbing)
		{
			ice = GameObject.CreatePrimitive((PrimitiveType)3);
			ice.transform.localScale = Movement_Position_06;
			ice.transform.position = hand.position + new Vector3(0f, -0.06f, 0f);
			ice.transform.rotation = hand.rotation * Quaternion.Euler(0f, 0f, -90f);
			ice.GetComponent<Renderer>().material.color = new Color(0.525f, 0.839f, 0.847f);
			ice.AddComponent<GorillaSurfaceOverride>().overrideIndex = 59;
			Object.Destroy((Object)(object)ice, 0.75f);
		}
	}

	public static void FlyPlusNoclip()
	{
		bool flag = InputHandler.IsRightTriggerPressed();
		if (InputHandler.IsRightPrimaryPressed())
		{
			Transform transform = ((Component)Variables.Variables_Reference_06).transform;
			transform.position += ((Component)Variables.Variables_Reference_06.headCollider).transform.forward * Time.deltaTime * Settings.CapturedVariables3760_Value_14;
			((Component)Variables.Variables_Reference_06).GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
			if (flag == Movement_State_05)
			{
				return;
			}
		}
		else if (flag == Movement_State_05)
		{
			return;
		}
		Movement_State_05 = flag;
		MeshCollider[] array = Object.FindObjectsOfType<MeshCollider>();
		for (int i = 0; i < array.Length; i++)
		{
			((Collider)array[i]).enabled = !Movement_State_05;
		}
	}

	public static void FastSwimSpeed()
	{
		if (Variables.Variables_Reference_06.InWater)
		{
			Rigidbody component = ((Component)Variables.Variables_Reference_06).gameObject.GetComponent<Rigidbody>();
			component.velocity *= 1.03f;
		}
	}

	public static void UpAndDown()
	{
		Rigidbody component = ((Component)Variables.Variables_Reference_06).GetComponent<Rigidbody>();
		Transform transform = ((Component)Variables.Variables_Reference_06.bodyCollider).transform;
		if (InputHandler.IsRightTriggerPressed())
		{
			component.velocity += transform.up * 20f * Time.deltaTime;
			if (!InputHandler.IsLeftTriggerPressed())
			{
				return;
			}
		}
		else if (!InputHandler.IsLeftTriggerPressed())
		{
			return;
		}
		component.velocity -= transform.up * 20f * Time.deltaTime;
	}

	public static void TeleportToPosition(Vector3 position)
	{
		Vector3 val = position - ((Component)Variables.Variables_Reference_09.bodyCollider).transform.position + ((Component)Variables.Variables_Reference_09).transform.position;
		Variables.Variables_Reference_06.TeleportTo(val, ((Component)Variables.Variables_Reference_06).transform.rotation, false, false);
	}

	public static void SetCheckpointEnabled(bool setActive)
	{
		if (!setActive)
		{
			if ((Object)(object)Movement_Object_04 != (Object)null)
			{
				Object.Destroy((Object)(object)Movement_Object_04);
				Movement_Object_04 = null;
			}
			return;
		}
		if ((Object)(object)Movement_Object_04 == (Object)null)
		{
			Movement_Object_04 = GameObject.CreatePrimitive((PrimitiveType)0);
			Movement_Object_04.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
			Renderer component = Movement_Object_04.GetComponent<Renderer>();
			if ((Object)(object)component != (Object)null)
			{
				component.material.shader = Shader.Find("GUI/Text Shader");
				component.material.color = Color.red;
				Movement_Object_04.GetComponent<Collider>().enabled = false;
				if (InputHandler.IsRightGripPressed())
				{
					goto Branch_015e;
				}
			}
			else
			{
				Movement_Object_04.GetComponent<Collider>().enabled = false;
				if (InputHandler.IsRightGripPressed())
				{
					goto Branch_015e;
				}
			}
		}
		else if (InputHandler.IsRightGripPressed())
		{
			goto Branch_015e;
		}
		if (InputHandler.IsRightTriggerPressed())
		{
			if ((Object)(object)Movement_Object_04 != (Object)null)
			{
				Renderer component2 = Movement_Object_04.GetComponent<Renderer>();
				if ((Object)(object)component2 != (Object)null)
				{
					component2.material.color = Color.green;
					((Component)Variables.Variables_Reference_06).GetComponent<Rigidbody>().velocity = Vector3.zero;
					((Component)Variables.Variables_Reference_06).transform.position = Movement_Object_04.transform.position;
				}
				else
				{
					((Component)Variables.Variables_Reference_06).GetComponent<Rigidbody>().velocity = Vector3.zero;
					((Component)Variables.Variables_Reference_06).transform.position = Movement_Object_04.transform.position;
				}
			}
		}
		else
		{
			Renderer component3 = Movement_Object_04.GetComponent<Renderer>();
			if ((Object)(object)component3 != (Object)null)
			{
				component3.material.color = Color.red;
			}
		}
		return;
		Branch_015e:
		Movement_Object_04.transform.position = Variables.Variables_Reference_06.RightHand.controllerTransform.position;
		Movement_Object_04.GetComponent<Renderer>().material.color = Color.red;
	}

	public static void TeleportToMap()
	{
		(string, string, string) tuple = Recovered_Reference_06[Settings.CapturedVariables3760_Index_14];
		GameObject obj = Variables.FindCachedGameObject(tuple.Item2);
		if (obj != null)
		{
			GorillaSetZoneTrigger component = obj.GetComponent<GorillaSetZoneTrigger>();
			if (component != null)
			{
				((GorillaTriggerBox)component).OnBoxTriggered();
				GameObject obj2 = Variables.FindCachedGameObject(tuple.Item3);
				TeleportToPosition((obj2 != null) ? obj2.transform.position : ((Component)VRRig.LocalRig).transform.position);
			}
		}
		GameObject obj3 = Variables.FindCachedGameObject(tuple.Item3);
		TeleportToPosition((obj3 != null) ? obj3.transform.position : ((Component)VRRig.LocalRig).transform.position);
	}

	public static void SetSlippyHandsEnabled(bool setActive)
	{
		Movement_State_01 = setActive;
	}

	public static void DashMonke(bool isDashEnabled, bool isAirJumpEnabled)
	{
		if (InputHandler.IsRightPrimaryPressed() && !Movement_State_02)
		{
			Movement_State_02 = true;
			if (isDashEnabled)
			{
				((Component)Variables.Variables_Reference_06).GetComponent<Rigidbody>().velocity = ((Component)Variables.Variables_Reference_06.headCollider).transform.forward * 9f;
				if (isAirJumpEnabled)
				{
					goto Branch_00b1;
				}
			}
			else if (isAirJumpEnabled)
			{
				goto Branch_00b1;
			}
			if (InputHandler.IsRightPrimaryPressed())
			{
				return;
			}
		}
		else if (InputHandler.IsRightPrimaryPressed())
		{
			return;
		}
		Branch_012e:
		Movement_State_02 = false;
		return;
		Branch_00b1:
		Rigidbody component = ((Component)Variables.Variables_Reference_06).GetComponent<Rigidbody>();
		component.velocity += Vector3.up * 7f;
		if (InputHandler.IsRightPrimaryPressed())
		{
			return;
		}
		goto Branch_012e;
	}

	private static void UpdateHandPlatform(ref GameObject platform, bool grabbing, Transform hand, bool invisible)
	{
		if (grabbing)
		{
			if ((Object)(object)platform == (Object)null)
			{
				EnsurePlatformMaterial();
				platform = CreatePlatform(Movement_Position_06, hand.position + new Vector3(0f, -0.02f, 0f), hand.rotation * Quaternion.Euler(0f, 0f, -90f), Movement_Material_01, invisible);
			}
		}
		else if ((Object)(object)platform != (Object)null)
		{
			Object.Destroy((Object)(object)platform);
			platform = null;
		}
	}

	static Movement()
	{
		Movement_State_11 = false;
		Movement_State_02 = false;
		Movement_State_10 = false;
		Movement_Value_02 = 0f;
		Movement_State_01 = false;
		Movement_Value_01 = 0.3f;
		Movement_State_06 = false;
		Movement_State_04 = false;
		Movement_Position_06 = new Vector3(0.28f, 0.015f, 0.28f);
		Movement_Position_04 = new Vector3(0.0075f, 0.15f, 0.15f);
		Movement_Position_05 = new Vector3(0.15f, 0.0075f, 0.15f);
		Movement_Position_02 = new Vector3(0.15f, 0.15f, 0.0075f);
		Recovered_Reference_06 = new(string, string, string)[16]
		{
			("Forest", "Environment Objects/TriggerZones_Prefab/ZoneTransitions_Prefab/Regional Transition/TreeRoomSpawnForestZone", "Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Forest, Tree Exit"),
			("City", "Environment Objects/TriggerZones_Prefab/ZoneTransitions_Prefab/Regional Transition/ForestToCity", "Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - City Front"),
			("Canyons", "Environment Objects/TriggerZones_Prefab/ZoneTransitions_Prefab/Regional Transition/ForestCanyonTransition", "Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Canyon"),
			("Clouds", "Environment Objects/TriggerZones_Prefab/ZoneTransitions_Prefab/Regional Transition/CityToSkyJungle", "Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Clouds From Computer"),
			("Caves", "Environment Objects/TriggerZones_Prefab/ZoneTransitions_Prefab/Regional Transition/ForestToCave", "Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Cave"),
			("Beach", "Environment Objects/TriggerZones_Prefab/ZoneTransitions_Prefab/Regional Transition/BeachToForest", "Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Beach for Computer"),
			("Mountains", "Environment Objects/TriggerZones_Prefab/ZoneTransitions_Prefab/Regional Transition/CityToMountain", "Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Mountain"),
			("Basement", "Environment Objects/TriggerZones_Prefab/ZoneTransitions_Prefab/Regional Transition/CityToBasement", "Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Basement For Computer"),
			("Metropolis", "Environment Objects/TriggerZones_Prefab/ZoneTransitions_Prefab/Regional Transition/MetropolisOnly", "Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Metropolis from Computer"),
			("Arcade", "Environment Objects/TriggerZones_Prefab/ZoneTransitions_Prefab/Regional Transition/CityToArcade", "Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - City frm Arcade"),
			("Critters", "Environment Objects/TriggerZones_Prefab/ZoneTransitions_Prefab/Regional Transition/CityCrittersTransition", "Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - City from Critters"),
			("Skate Park", "Environment Objects/TriggerZones_Prefab/ZoneTransitions_Prefab/Regional Transition/ForestToHoverboard", "Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Hoverboard from Forest"),
			("Monke Blocks", "Environment Objects/TriggerZones_Prefab/ZoneTransitions_Prefab/Regional Transition/MonkeBlocksElevatorExit", "Environment Objects/05Maze_PersistentObjects/GhostReactorElevatorManager/MonkeBlocksElevator/Triggers/JoinRoomTrigger"),
			("Rotating", "Environment Objects/TriggerZones_Prefab/ZoneTransitions_Prefab/Regional Transition/CityToRotating", "Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Rotating Map"),
			("Bayou", "Environment Objects/TriggerZones_Prefab/ZoneTransitions_Prefab/Regional Transition/BayouOnly", "Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - BayouComputer2"),
			("Lava Forest", "Environment Objects/05Maze_PersistentObjects/GhostReactorElevatorManager/VIMForestLavaElevator/Triggers/VIMExp1_SetZoneTrigger", "Environment Objects/05Maze_PersistentObjects/GhostReactorElevatorManager/VIMForestLavaElevator/Triggers/JoinRoomTrigger")
		};
		Movement_State_09 = false;
		Movement_State_03 = false;
	}

	private static GameObject CreatePlatform(Vector3 scale, Vector3 pos, Quaternion rot, Material mat, bool invisible)
	{
		GameObject val = GameObject.CreatePrimitive((PrimitiveType)3);
		val.transform.localScale = scale;
		val.transform.position = pos;
		val.transform.rotation = rot;
		Renderer component = val.GetComponent<Renderer>();
		component.material = mat;
		component.enabled = !invisible;
		return val;
	}

	public static void Frozone()
	{
		SpawnIceTrail(ref Movement_Object_01, IsPlatformInputPressed(rightHand: false), Variables.Variables_Reference_06.LeftHand.controllerTransform);
		SpawnIceTrail(ref Movement_Object_06, IsPlatformInputPressed(rightHand: true), Variables.Variables_Reference_06.RightHand.controllerTransform);
	}

	public static void SetSlideControlEnabled(bool setActive)
	{
		Variables.Variables_Reference_06.slideControl = (setActive ? 0.04f : 0.00425f);
	}

	public static void JoystickFly()
	{
		Rigidbody component = ((Component)Variables.Variables_Reference_06).GetComponent<Rigidbody>();
		Transform transform = ((Component)Variables.Variables_Reference_06.headCollider).transform;
		component.useGravity = false;
		component.linearVelocity = Vector3.zero;
		Vector2 axis = SteamVR_Actions.gorillaTag_LeftJoystick2DAxis.axis;
		if (((Vector2)axis).magnitude > 0.1f)
		{
			Vector3 val = transform.forward * axis.y + transform.right * axis.x;
			Transform transform2 = ((Component)Variables.Variables_Reference_06).transform;
			transform2.position += val * Time.deltaTime * Settings.CapturedVariables3760_Value_14;
		}
	}

	public static void Noclip()
	{
		bool flag = InputHandler.IsRightTriggerPressed();
		if (flag != Movement_State_05)
		{
			Movement_State_05 = flag;
			MeshCollider[] array = Object.FindObjectsOfType<MeshCollider>();
			for (int i = 0; i < array.Length; i++)
			{
				((Collider)array[i]).enabled = !Movement_State_05;
			}
		}
	}

	public static void EnableMovement()
	{
		Variables.Variables_Reference_06.disableMovement = false;
	}

	public static void SpeedBoost()
	{
		if (!Movement_State_11 || InputHandler.IsRightGripPressed())
		{
			Variables.Variables_Reference_06.maxJumpSpeed = Settings.CapturedVariables3760_Value_06;
			Variables.Variables_Reference_06.jumpMultiplier = Settings.CapturedVariables3760_Value_16;
		}
	}

	public static void LowGravity()
	{
		Variables.Variables_Reference_09.rigidbody.AddForce(Vector3.up * 6.5f, (ForceMode)5);
	}

	private static void UpdateHandPlatformWalls(ref GameObject[] sides, bool grabbing, Transform hand, bool invisible)
	{
		if (grabbing)
		{
			if (sides != null)
			{
				return;
			}
			EnsurePlatformMaterial();
			sides = (GameObject[])(object)new GameObject[6];
			Vector3[] array = (Vector3[])(object)new Vector3[6]
			{
				hand.right * -0.075f,
				hand.right * 0.075f,
				hand.up * 0.075f,
				hand.up * -0.075f,
				hand.forward * 0.075f,
				hand.forward * -0.075f
			};
			Vector3[] array2 = (Vector3[])(object)new Vector3[6] { Movement_Position_04, Movement_Position_04, Movement_Position_05, Movement_Position_05, Movement_Position_02, Movement_Position_02 };
			int num = 0;
			if (num < 6)
			{
				do
				{
					sides[num] = CreatePlatform(array2[num], hand.position + array[num], hand.rotation, Movement_Material_01, invisible);
					num++;
				}
				while (num < 6);
			}
		}
		else
		{
			if (sides == null)
			{
				return;
			}
			GameObject[] array3 = sides;
			for (int i = 0; i < array3.Length; i++)
			{
				while (true)
				{
					GameObject val = array3[i];
					if (!((Object)(object)val != (Object)null))
					{
						break;
					}
					Object.Destroy((Object)(object)val);
					i++;
					if (i >= array3.Length)
					{
						goto EndBranch_0211;
					}
				}
				continue;
				EndBranch_0211:
				break;
			}
			sides = null;
		}
	}

	private static bool IsPlatformInputPressed(bool rightHand)
	{
		if (!Movement_State_04)
		{
			if (!rightHand)
			{
				return InputHandler.IsLeftGripPressed();
			}
			return InputHandler.IsRightGripPressed();
		}
		if (!rightHand)
		{
			return InputHandler.IsLeftTriggerPressed();
		}
		return InputHandler.IsRightTriggerPressed();
	}

	public static void IronMonke(int flySpeed)
	{
		if (InputHandler.IsLeftGripPressed())
		{
			((Collider)Variables.Variables_Reference_06.bodyCollider).attachedRigidbody.AddForce((float)flySpeed * -Variables.Variables_Reference_09.leftHandTransform.right, (ForceMode)5);
			if (!InputHandler.IsRightGripPressed())
			{
				return;
			}
		}
		else if (!InputHandler.IsRightGripPressed())
		{
			return;
		}
		((Collider)Variables.Variables_Reference_06.bodyCollider).attachedRigidbody.AddForce((float)flySpeed * Variables.Variables_Reference_09.rightHandTransform.right, (ForceMode)5);
	}

	public static void Fly(bool useVelocity, float velocityMultiplier = 1.15f)
	{
		Rigidbody component = ((Component)Variables.Variables_Reference_06).GetComponent<Rigidbody>();
		if (InputHandler.IsRightPrimaryPressed())
		{
			if (useVelocity)
			{
				component.linearVelocity += ((Component)Variables.Variables_Reference_06.headCollider).transform.forward * Settings.CapturedVariables3760_Value_14 * velocityMultiplier * Time.deltaTime;
				return;
			}
			Transform transform = ((Component)Variables.Variables_Reference_06).transform;
			transform.position += ((Component)Variables.Variables_Reference_06.headCollider).transform.forward * Time.deltaTime * Settings.CapturedVariables3760_Value_14;
			component.linearVelocity = Vector3.zero;
		}
	}

	private static void EnsurePlatformMaterial()
	{
		if ((Object)(object)Movement_Material_01 == (Object)null)
		{
			Movement_Material_01 = new Material(Variables.Variables_Reference_11);
			Movement_Material_01.color = Color.black;
		}
	}

	public static void HighGravity()
	{
		Variables.Variables_Reference_09.rigidbody.AddForce(Vector3.down * 8f, (ForceMode)5);
	}

	public static void PullMod()
	{
		if (Variables.Variables_Reference_06.IsHandTouching(true) || !Movement_State_09)
		{
			if (!Variables.Variables_Reference_06.IsHandTouching(false))
			{
				if (Movement_State_03)
				{
					goto Branch_008a;
				}
			}
			Movement_State_09 = Variables.Variables_Reference_06.IsHandTouching(true);
			Movement_State_03 = Variables.Variables_Reference_06.IsHandTouching(false);
			return;
		}
		Branch_008a:
		Vector3 velocity = Variables.Variables_Reference_09.rigidbody.velocity;
		Transform transform = ((Component)Variables.Variables_Reference_06).transform;
		transform.position += new Vector3(velocity.x * 0.05f, 0f, velocity.z * 0.05f);
		Movement_State_09 = Variables.Variables_Reference_06.IsHandTouching(true);
		Movement_State_03 = Variables.Variables_Reference_06.IsHandTouching(false);
	}

	public static void WallWalk()
	{
		if (Variables.Variables_Reference_06.LeftHand.wasColliding || Variables.Variables_Reference_06.RightHand.wasColliding)
		{
			RaycastHit lastHitInfoHand = ReflectionCompat.GetField(Variables.Variables_Reference_06, "lastHitInfoHand", default(RaycastHit));
			Movement_Position_07 = ((RaycastHit)lastHitInfoHand).point;
			Movement_Position_01 = ((RaycastHit)lastHitInfoHand).normal;
			if (!(Movement_Position_07 != Vector3.zero))
			{
				return;
			}
		}
		else if (!(Movement_Position_07 != Vector3.zero))
		{
			return;
		}
		if (!InputHandler.IsRightGripPressed())
		{
			if (!InputHandler.IsLeftGripPressed())
			{
				return;
			}
		}
		((Collider)Variables.Variables_Reference_06.bodyCollider).attachedRigidbody.AddForce(Movement_Position_01 * Settings.CapturedVariables3760_Value_20, (ForceMode)5);
	}

	public static void ReverseGravity()
	{
		Variables.Variables_Reference_09.rigidbody.AddForce(Vector3.up * 19.62f, (ForceMode)5);
	}

	public static void Platforms()
	{
		Transform controllerTransform = Variables.Variables_Reference_06.LeftHand.controllerTransform;
		Transform controllerTransform2 = Variables.Variables_Reference_06.RightHand.controllerTransform;
		bool grabbing = IsPlatformInputPressed(rightHand: false);
		bool grabbing2 = IsPlatformInputPressed(rightHand: true);
		switch (Settings.CapturedVariables3760_Text_35.ToLower())
		{
		case "normal":
			UpdateHandPlatform(ref Movement_Object_05, grabbing, controllerTransform, invisible: false);
			UpdateHandPlatform(ref Movement_Object_03, grabbing2, controllerTransform2, invisible: false);
			break;
		case "sticky":
			UpdateHandPlatformWalls(ref Movement_Object_02, grabbing, controllerTransform, invisible: false);
			UpdateHandPlatformWalls(ref Movement_Object_07, grabbing2, controllerTransform2, invisible: false);
			break;
		case "invisible":
			UpdateHandPlatform(ref Movement_Object_05, grabbing, controllerTransform, invisible: true);
			UpdateHandPlatform(ref Movement_Object_03, grabbing2, controllerTransform2, invisible: true);
			break;
		case "invisible sticky":
			UpdateHandPlatformWalls(ref Movement_Object_02, grabbing, controllerTransform, invisible: true);
			UpdateHandPlatformWalls(ref Movement_Object_07, grabbing2, controllerTransform2, invisible: true);
			break;
		}
	}
}
