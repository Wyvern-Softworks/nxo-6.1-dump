using System;
using System.Collections.Generic;
using System.Linq;
using NXO.Mods.Categories;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NXO.Utilities;

public class GunLib
{
	public static bool GunLib_State_03;

	public static bool GunLib_State_02;

	public static VRRig GunLib_Reference_06;

	public static VRRig GunLib_Reference_02;

	public static GameObject GunLib_Object_01;

	public static RaycastHit GunLib_Reference_07;

	private static GameObject GunLib_Object_02;

	private static LineRenderer GunLib_Reference_05;

	private static Material GunLib_Material_02;

	private static Renderer GunLib_Reference_04;

	private static Material GunLib_Material_01;

	private static readonly GradientColorKey[] GunLib_Color_01;

	private static readonly GradientColorKey[] GunLib_Color_02;

	private static readonly GradientAlphaKey[] GunLib_Values_01;

	private static readonly Gradient GunLib_Reference_01;

	private static bool GunLib_State_01;

	private static Camera GunLib_Reference_03;

	private const float WaveDensity = 4f;

	private const float PointsPerCoil = 24f;

	private const int MaxLinePoints = 3000;

	private const float EndTaper = 0.15f;

	private const float SpiralRadius = 0.05f;

	private const float SpiralCoilScale = 80f;

	public static bool GunGrips
	{
		get
		{
			if (Mouse.current == null || !Mouse.current.rightButton.isPressed)
			{
				if (!GunLib_State_02)
				{
					return InputHandler.IsRightGripPressed();
				}
				return InputHandler.IsLeftGripPressed();
			}
			return true;
		}
	}

	public static bool GunTriggers
	{
		get
		{
			if (Mouse.current == null || !Mouse.current.leftButton.isPressed)
			{
				if (!GunLib_State_02)
				{
					return InputHandler.IsRightTriggerPressed();
				}
				return InputHandler.IsLeftTriggerPressed();
			}
			return true;
		}
	}

	private static Transform GunHand
	{
		get
		{
			if ((Object)(object)GorillaTagger.Instance != (Object)null)
			{
				if (!GunLib_State_02)
				{
					return GorillaTagger.Instance.rightHandTransform;
				}
				return GorillaTagger.Instance.leftHandTransform;
			}
			if ((Object)(object)Variables.Variables_Reference_09 != (Object)null)
			{
				if (!GunLib_State_02)
				{
					return Variables.Variables_Reference_09.rightHandTransform;
				}
				return Variables.Variables_Reference_09.leftHandTransform;
			}
			if ((Object)(object)Variables.Variables_Reference_06 != (Object)null)
			{
				if (!GunLib_State_02)
				{
					return Variables.Variables_Reference_06.RightHand.controllerTransform;
				}
				return Variables.Variables_Reference_06.LeftHand.controllerTransform;
			}
			return null;
		}
	}

	private static Vector3 CalculateBeamOffset(string style, float p, float f, float amp, Vector3 right, Vector3 up, float t, float spd)
	{
		if (!(style == "Spiral"))
		{
			if (style == "Electric")
			{
				float num = Mathf.PerlinNoise(f * 15f, t * spd * 0.3f) - 0.5f;
				float num2 = Mathf.PerlinNoise(f * 15f + 50f, t * spd * 0.3f) - 0.5f;
				return (right * num + up * num2) * (amp * 3f);
			}
			return right * (Mathf.Sin(p) * amp);
		}
		return right * (Mathf.Cos(p) * amp) - up * (Mathf.Sin(p) * amp);
	}

	public static void UpdateGunVisuals(Vector3 pos)
	{
		if ((Object)(object)GunLib_Object_01 == (Object)null)
		{
			GunLib_Object_01 = GameObject.CreatePrimitive((PrimitiveType)0);
			Rigidbody rigidbody = GunLib_Object_01.GetComponent<Rigidbody>();
			if ((Object)(object)rigidbody != (Object)null)
			{
				Object.Destroy((Object)(object)rigidbody);
			}

			SphereCollider collider = GunLib_Object_01.GetComponent<SphereCollider>();
			if ((Object)(object)collider != (Object)null)
			{
				Object.Destroy((Object)(object)collider);
			}

			GunLib_Reference_04 = GunLib_Object_01.GetComponent<Renderer>();
			GunLib_Material_01 = new Material(Variables.Variables_Reference_02);
			if ((Object)(object)GunLib_Reference_04 != (Object)null)
			{
				GunLib_Reference_04.sharedMaterial = GunLib_Material_01;
			}
		}

		GunLib_Object_01.transform.localScale = Vector3.one * Settings.CapturedVariables3760_Value_05;
		GunLib_Object_01.transform.position = pos;
		if ((Object)(object)GunLib_Material_01 != (Object)null)
		{
			GunLib_Material_01.color = (Color32)(GetGunColor(0f));
		}

		if (!GunLib_State_03)
		{
			if ((Object)(object)GunLib_Object_02 != (Object)null && GunLib_Object_02.activeSelf)
			{
				GunLib_Object_02.SetActive(false);
			}
			SetGunVisualsVisible(isVisible: true);
			return;
		}

		Transform gunHand = GunHand;
		if ((Object)(object)gunHand == (Object)null)
		{
			return;
		}

		if ((Object)(object)GunLib_Object_02 == (Object)null)
		{
			GunLib_Object_02 = new GameObject("GunLine");
			GunLib_Reference_05 = GunLib_Object_02.AddComponent<LineRenderer>();
			GunLib_Material_02 = new Material(Shader.Find("Sprites/Default"))
			{
				color = Color.white
			};
			GunLib_Material_02.mainTexture = (Texture)(object)Texture2D.whiteTexture;
			((Renderer)GunLib_Reference_05).material = GunLib_Material_02;
			GunLib_Reference_05.useWorldSpace = true;
			GunLib_Reference_05.numCapVertices = 4;
			GunLib_Reference_05.numCornerVertices = 4;
			GunLib_Reference_05.alignment = (LineAlignment)0;
		}

		Vector3 start = gunHand.position;
		string style = Settings.CapturedVariables3760_Text_42;
		float time = Time.time;
		float speed = Settings.CapturedVariables3760_Value_09;
		float width = Settings.CapturedVariables3760_Value_21;
		if (style == "Pulse")
		{
			width *= 1f + 0.6f * Mathf.Abs(Mathf.Sin(time * speed));
		}
		GunLib_Reference_05.startWidth = width;
		GunLib_Reference_05.endWidth = width;

		if (style == "None" || style == "Pulse")
		{
			GunLib_Reference_05.positionCount = 2;
			GunLib_Reference_05.SetPosition(0, start);
			GunLib_Reference_05.SetPosition(1, pos);
		}
		else
		{
			bool isSpiral = style == "Spiral";
			float amplitude = isSpiral ? 0.05f : Settings.CapturedVariables3760_Value_08;
			float coilsPerUnit = isSpiral ? Settings.CapturedVariables3760_Value_08 * 80f : 4f;
			Vector3 direction = (pos - start).normalized;
			Vector3 right = Vector3.Cross(direction, Vector3.up);
			if (right.sqrMagnitude < 0.0001f)
			{
				right = Vector3.Cross(direction, Vector3.forward);
			}
			right.Normalize();
			Vector3 up = Vector3.Cross(direction, right).normalized;
			float coilCount = Vector3.Distance(start, pos) * coilsPerUnit;
			float phaseRange = coilCount * MathF.PI * 2f;
			int pointCount = Mathf.Clamp(Mathf.CeilToInt(coilCount * 24f), 16, 3000);
			GunLib_Reference_05.positionCount = pointCount;

			for (int i = 0; i < pointCount; i++)
			{
				float fraction = (float)i / (pointCount - 1);
				float phase = time * speed + fraction * phaseRange;
				float taper = Mathf.Clamp01((1f - fraction) / 0.15f);
				Vector3 offset = CalculateBeamOffset(style, phase, fraction, amplitude, right, up, time, speed);
				GunLib_Reference_05.SetPosition(i, Vector3.Lerp(start, pos, fraction) + offset * taper);
			}
		}

		UpdateBeamColors();
		if (!GunLib_Object_02.activeSelf)
		{
			GunLib_Object_02.SetActive(true);
		}
		SetGunVisualsVisible(isVisible: true);
	}

	private static Color32 GetGunColor(float flow)
	{
		if (UseRainbowGunColors())
		{
			return (Color32)(Color.HSVToRGB(Mathf.Repeat(Time.time * 0.4f + flow, 1f), 1f, 1f));
		}
		if ((Object)(object)GunLib_Reference_06 != (Object)null)
		{
			return Settings.CapturedVariables3760_Color_07;
		}
		if (GunTriggers)
		{
			return Settings.CapturedVariables3760_Color_06;
		}
		if ((Object)(object)GunLib_Reference_02 != (Object)null)
		{
			return Settings.CapturedVariables3760_Color_13;
		}
		return Settings.CapturedVariables3760_Color_20;
	}

	public static void UpdateGunRaycast()
	{
		bool flag;
		if (Mouse.current != null)
		{
			if (!Mouse.current.rightButton.isPressed)
			{
				flag = Mouse.current.leftButton.isPressed;
				if ((Object)(object)GunLib_Reference_03 == (Object)null)
				{
					goto Branch_00a8;
				}
			}
			else
			{
				flag = true;
				if ((Object)(object)GunLib_Reference_03 == (Object)null)
				{
					goto Branch_00a8;
				}
			}
		}
		else
		{
			flag = false;
			if ((Object)(object)GunLib_Reference_03 == (Object)null)
			{
				goto Branch_00a8;
			}
		}
		goto Branch_00f6;
		Branch_0141:
		Ray val;
		Transform val2;
		IEnumerator<RaycastHit> enumerator;
		Vector3 val3;
		if (!flag || !((Object)(object)GunLib_Reference_03 != (Object)null))
		{
			val = new Ray(val2.position - val2.up, -val2.up);
			GunLib_State_01 = false;
			GunLib_Reference_02 = null;
			val3 = ((Ray)val).origin + ((Ray)val).direction * 100f;
			GunLib_Reference_07 = default(RaycastHit);
			RaycastHit fallbackHit = GunLib_Reference_07;
			fallbackHit.point = val3;
			GunLib_Reference_07 = fallbackHit;
			enumerator = (from x in Physics.RaycastAll(val, 100f, Variables.GetInteractionLayerMask())
				orderby ((RaycastHit)x).distance
				select x).GetEnumerator();
		}
		else
		{
			val = GunLib_Reference_03.ScreenPointToRay((Vector2)(((InputControl<Vector2>)(object)((Pointer)Mouse.current).position).ReadValue()));
			GunLib_State_01 = false;
			GunLib_Reference_02 = null;
			val3 = ((Ray)val).origin + ((Ray)val).direction * 100f;
			GunLib_Reference_07 = default(RaycastHit);
			RaycastHit fallbackHit = GunLib_Reference_07;
			fallbackHit.point = val3;
			GunLib_Reference_07 = fallbackHit;
			enumerator = (from x in Physics.RaycastAll(val, 100f, Variables.GetInteractionLayerMask())
				orderby ((RaycastHit)x).distance
				select x).GetEnumerator();
		}
		try
		{
			RaycastHit current;
			VRRig val4;
			if (enumerator.MoveNext())
			{
				current = enumerator.Current;
				if (!((Object)(object)((RaycastHit)current).collider != (Object)null))
				{
					val4 = null;
					if ((Object)(object)val4 != (Object)null)
					{
						goto Branch_0339;
					}
				}
				else
				{
					val4 = ((Component)((RaycastHit)current).collider).GetComponentInParent<VRRig>();
					if ((Object)(object)val4 != (Object)null)
					{
						goto Branch_0339;
					}
				}
				goto Branch_03ae;
			}
			goto EndBranch_02c1;
			Branch_0339:
			if (!((Object)(object)val4 != (Object)(object)VRRig.LocalRig) || (!((Object)(object)Variables.Variables_Reference_09 == (Object)null) && !((Object)(object)val4 != (Object)(object)Variables.Variables_Reference_09.offlineVRRig)) || val4.Creator == null)
			{
				goto Branch_03ae;
			}
			GunLib_Reference_02 = val4;
			GunLib_Reference_07 = current;
			GunLib_State_01 = true;
			val3 = ((RaycastHit)current).point;
			goto EndBranch_02c1;
			Branch_03ae:
			GunLib_Reference_02 = null;
			GunLib_Reference_07 = current;
			GunLib_State_01 = true;
			val3 = ((RaycastHit)current).point;
			EndBranch_02c1:;
		}
		finally
		{
			enumerator?.Dispose();
		}
		if ((Object)(object)GunLib_Reference_06 != (Object)null)
		{
			val3 = ((Component)GunLib_Reference_06).transform.position;
			RaycastHit selectedHit = GunLib_Reference_07;
			selectedHit.point = val3;
			GunLib_Reference_07 = selectedHit;
			UpdateGunVisuals(val3);
		}
		else
		{
			UpdateGunVisuals(val3);
		}
		return;
		Branch_010c:
		if (!((Object)(object)val2 == (Object)null))
		{
			goto Branch_0141;
		}
		SetGunVisualsVisible(isVisible: false);
		return;
		Branch_00f6:
		val2 = GunHand;
		if (!flag)
		{
			goto Branch_010c;
		}
		goto Branch_0141;
		Branch_00a8:
		if (!((Object)(object)Variables.Variables_Object_13 != (Object)null))
		{
			goto Branch_00f6;
		}
		GunLib_Reference_03 = Variables.Variables_Object_13.GetComponent<Camera>();
		val2 = GunHand;
		if (!flag)
		{
			goto Branch_010c;
		}
		goto Branch_0141;
	}

	public static bool TrySelectRig()
	{
		if (!GunGrips)
		{
			GunLib_Reference_06 = null;
			GunLib_Reference_02 = null;
			SetGunVisualsVisible(isVisible: false);
			return false;
		}
		if ((Object)(object)GunLib_Reference_06 != (Object)null && GunLib_Reference_06.Creator == null)
		{
			GunLib_Reference_06 = null;
			UpdateGunRaycast();
			if (GunTriggers)
			{
				goto Branch_00c6;
			}
		}
		else
		{
			UpdateGunRaycast();
			if (GunTriggers)
			{
				goto Branch_00c6;
			}
		}
		GunLib_Reference_06 = null;
		return false;
		Branch_00c6:
		if ((Object)(object)GunLib_Reference_06 == (Object)null && GunLib_State_01 && (Object)(object)GunLib_Reference_02 != (Object)null)
		{
			GunLib_Reference_06 = GunLib_Reference_02;
			return (Object)(object)GunLib_Reference_06 != (Object)null;
		}
		return (Object)(object)GunLib_Reference_06 != (Object)null;
	}

	public static void SetGunVisualsVisible(bool isVisible)
	{
		if ((Object)(object)GunLib_Object_01 != (Object)null)
		{
			GunLib_Object_01.SetActive(isVisible);
			if (!((Object)(object)GunLib_Object_02 != (Object)null))
			{
				return;
			}
		}
		else if (!((Object)(object)GunLib_Object_02 != (Object)null))
		{
			return;
		}
		GunLib_Object_02.SetActive(isVisible && GunLib_State_03);
	}

	private static bool UseRainbowGunColors()
	{
		if ((Object)(object)GunLib_Reference_06 != (Object)null)
		{
			return Settings.CapturedVariables3760_State_01;
		}
		if (GunTriggers)
		{
			return Settings.CapturedVariables3760_State_02;
		}
		if ((Object)(object)GunLib_Reference_02 != (Object)null)
		{
			return Settings.CapturedVariables3760_State_04;
		}
		return Settings.CapturedVariables3760_State_05;
	}

	private static void UpdateBeamColors()
	{
		if ((Object)(object)GunLib_Reference_05 == (Object)null)
		{
			return;
		}
		if (UseRainbowGunColors())
		{
			float num = Time.time * 0.4f;
			int num2 = 0;
			if (num2 < 8)
			{
				do
				{
					GunLib_Color_01[num2] = new GradientColorKey(Color.HSVToRGB(Mathf.Repeat(num - (float)num2 / 8f, 1f), 1f, 1f), (float)num2 / 7f);
					num2++;
				}
				while (num2 < 8);
			}
			GunLib_Reference_01.SetKeys(GunLib_Color_01, GunLib_Values_01);
			GunLib_Reference_05.colorGradient = GunLib_Reference_01;
		}
		else
		{
			Color val = (Color32)(GetGunColor(0f));
			GunLib_Color_02[0] = new GradientColorKey(val, 0f);
			GunLib_Color_02[1] = new GradientColorKey(val, 1f);
			GunLib_Reference_01.SetKeys(GunLib_Color_02, GunLib_Values_01);
			GunLib_Reference_05.colorGradient = GunLib_Reference_01;
		}
	}

	static GunLib()
	{
		GunLib_Color_01 = (GradientColorKey[])(object)new GradientColorKey[8];
		GunLib_Color_02 = (GradientColorKey[])(object)new GradientColorKey[2];
		GunLib_Values_01 = (GradientAlphaKey[])(object)new GradientAlphaKey[2]
		{
			new GradientAlphaKey(1f, 0f),
			new GradientAlphaKey(1f, 1f)
		};
		GunLib_Reference_01 = new Gradient();
	}

	public static bool IsGunTriggerPressed()
	{
		if (!GunGrips)
		{
			SetGunVisualsVisible(isVisible: false);
			return false;
		}
		UpdateGunRaycast();
		return GunTriggers;
	}
}
