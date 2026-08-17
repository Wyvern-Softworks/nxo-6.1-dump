using System;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

namespace NXO.Utilities;

public static class InputHandler
{
	public static bool InputHandler_State_02;

	public static bool InputHandler_State_01;

	public static bool InputHandler_State_04;

	public static bool InputHandler_State_03;

	public static bool IsRightPrimaryPressed()
	{
		if (!ControllerEmulator.ControllerEmulator_State_08)
		{
			return ((ControllerInputPoller)ControllerInputPoller.instance).rightControllerPrimaryButton;
		}
		return ControllerEmulator.GetRightPrimaryButton();
	}

	public static bool IsLeftPrimaryPressed()
	{
		if (!ControllerEmulator.ControllerEmulator_State_08)
		{
			return ((ControllerInputPoller)ControllerInputPoller.instance).leftControllerPrimaryButton;
		}
		return ControllerEmulator.GetLeftPrimaryButton();
	}

	public static bool IsLeftJoystickPressed()
	{
		if (ControllerEmulator.ControllerEmulator_State_08)
		{
			return false;
		}
		bool result = false;
		((InputDevice)((ControllerInputPoller)ControllerInputPoller.instance).leftControllerDevice).TryGetFeatureValue(CommonUsages.primary2DAxisClick, out result);
		return result;
	}

	public static bool IsRightTriggerPressed()
	{
		if (!ControllerEmulator.ControllerEmulator_State_08)
		{
			return ((ControllerInputPoller)ControllerInputPoller.instance).rightControllerIndexFloat > 0.1f;
		}
		return ControllerEmulator.GetRightTrigger();
	}

	public static Vector2 GetJoystickAxis(bool left)
	{
		if (SteamVR.active)
		{
			if (!left)
			{
				return SteamVR_Actions.gorillaTag_RightJoystick2DAxis.GetAxis((SteamVR_Input_Sources)2);
			}
			return SteamVR_Actions.gorillaTag_LeftJoystick2DAxis.GetAxis((SteamVR_Input_Sources)1);
		}
		Vector2 zero = Vector2.zero;
		InputDevice val;
		if (!left)
		{
			val = ((ControllerInputPoller)ControllerInputPoller.instance).rightControllerDevice;
			((InputDevice)val).TryGetFeatureValue(CommonUsages.primary2DAxis, out zero);
			return zero;
		}
		val = ((ControllerInputPoller)ControllerInputPoller.instance).leftControllerDevice;
		((InputDevice)val).TryGetFeatureValue(CommonUsages.primary2DAxis, out zero);
		return zero;
	}

	public static bool AlwaysPressed()
	{
		return true;
	}

	public static void UpdateToggleOnPress(Func<bool> buttonCheck, ref bool toggle, ref bool lastState)
	{
		bool flag = buttonCheck();
		if (!lastState & flag)
		{
			toggle = !toggle;
			lastState = flag;
		}
		else
		{
			lastState = flag;
		}
	}

	public static bool IsLeftSecondaryPressed()
	{
		if (!ControllerEmulator.ControllerEmulator_State_08)
		{
			return ((ControllerInputPoller)ControllerInputPoller.instance).leftControllerSecondaryButton;
		}
		return ControllerEmulator.GetLeftSecondaryButton();
	}

	public static bool IsRightJoystickPressed()
	{
		if (ControllerEmulator.ControllerEmulator_State_08)
		{
			return false;
		}
		bool result = false;
		((InputDevice)((ControllerInputPoller)ControllerInputPoller.instance).rightControllerDevice).TryGetFeatureValue(CommonUsages.primary2DAxisClick, out result);
		return result;
	}

	public static bool IsLeftTriggerPressed()
	{
		if (!ControllerEmulator.ControllerEmulator_State_08)
		{
			return ((ControllerInputPoller)ControllerInputPoller.instance).leftControllerIndexFloat > 0.1f;
		}
		return ControllerEmulator.GetLeftTrigger();
	}

	public static bool IsRightGripPressed()
	{
		if (!ControllerEmulator.ControllerEmulator_State_08)
		{
			return ((ControllerInputPoller)ControllerInputPoller.instance).rightGrab;
		}
		return ControllerEmulator.GetRightGrip();
	}

	public static bool IsRightSecondaryPressed()
	{
		if (!ControllerEmulator.ControllerEmulator_State_08)
		{
			return ((ControllerInputPoller)ControllerInputPoller.instance).rightControllerSecondaryButton;
		}
		return ControllerEmulator.GetRightSecondaryButton();
	}

	public static bool IsLeftGripPressed()
	{
		if (!ControllerEmulator.ControllerEmulator_State_08)
		{
			return ((ControllerInputPoller)ControllerInputPoller.instance).leftGrab;
		}
		return ControllerEmulator.GetLeftGrip();
	}
}
