using NXO.Utilities;
using Photon.Pun;
using UnityEngine;

namespace NXO.Mods.Categories;

public static class Networked
{
	public static class SizeChanger
	{
		private static float SizeChanger_Value_01 = 1f;

		private static float SizeChanger_Value_02 = 1f;

		public static void UpdateNetworkedSize(bool enable)
		{
			if (!PhotonNetwork.InRoom)
			{
				return;
			}
			if (enable)
			{
				if (InputHandler.IsLeftSecondaryPressed())
				{
					SizeChanger_Value_01 = 1f;
					if (InputHandler.IsLeftTriggerPressed())
					{
						goto Branch_0090;
					}
				}
				else if (InputHandler.IsLeftTriggerPressed())
				{
					goto Branch_0090;
				}
				if (!InputHandler.IsRightTriggerPressed())
				{
					goto Branch_012d;
				}
				goto Branch_00d2;
			}
			NetworkingLibrary.RaiseNetworkEvent(NetworkingLibrary.NetworkingType.SizeChanger, new object[1] { 1f });
			return;
			Branch_0090:
			SizeChanger_Value_01 -= 0.05f;
			if (!InputHandler.IsRightTriggerPressed())
			{
				goto Branch_012d;
			}
			Branch_00d2:
			SizeChanger_Value_01 += 0.05f;
			SizeChanger_Value_01 = Mathf.Clamp(SizeChanger_Value_01, 0.375f, 2.75f);
			ReflectionCompat.SetField(Variables.Variables_Reference_06, "nativeScale", SizeChanger_Value_01);
			if (SizeChanger_Value_01 == SizeChanger_Value_02)
			{
				return;
			}
			goto Branch_0178;
			Branch_012d:
			SizeChanger_Value_01 = Mathf.Clamp(SizeChanger_Value_01, 0.375f, 2.75f);
			ReflectionCompat.SetField(Variables.Variables_Reference_06, "nativeScale", SizeChanger_Value_01);
			if (SizeChanger_Value_01 == SizeChanger_Value_02)
			{
				return;
			}
			Branch_0178:
			SizeChanger_Value_02 = SizeChanger_Value_01;
			NetworkingLibrary.RaiseNetworkEvent(NetworkingLibrary.NetworkingType.SizeChanger, new object[1] { SizeChanger_Value_01 });
		}
	}
}
