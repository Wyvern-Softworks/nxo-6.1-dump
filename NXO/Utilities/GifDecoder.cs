using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

namespace NXO.Utilities;

public static class GifDecoder
{
	public class Frame
	{
		public Texture2D texture;

		public float delay;
	}

	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct CapturedVariables60
	{
		public List<int[]> dict;

		public int clearCode;
	}

	private static readonly int[] CapturedVariables60_Index_02 = new int[4] { 0, 4, 2, 1 };

	private static readonly int[] CapturedVariables60_Index_01 = new int[4] { 8, 8, 4, 2 };

	private static int ReadUInt16(byte[] d, ref int p)
	{
		int result = d[p] | (d[p + 1] << 8);
		p += 2;
		return result;
	}

	private static byte[] DecodeLzwData(byte[] d, ref int p, int minCodeSize, int pixelCount)
	{
		List<byte> list = new List<byte>();
		int num;
		while ((num = d[p++]) != 0)
		{
			int num2 = 0;
			if (num2 < num)
			{
				do
				{
					list.Add(d[p++]);
					num2++;
				}
				while (num2 < num);
			}
		}
		List<byte> list2 = new List<byte>(pixelCount);
		CapturedVariables60 obj = new CapturedVariables60();
		obj.clearCode = 1 << minCodeSize;
		int num3 = obj.clearCode + 1;
		int num4 = minCodeSize + 1;
		obj.dict = new List<int[]>();
		ResetLzwDictionary(ref obj);
		int num5 = 0;
		int[] array = null;
		for (int num6 = list.Count * 8; num5 + num4 <= num6; num4 = minCodeSize + 1, ResetLzwDictionary(ref obj), array = null)
		{
			do
			{
				Branch_00c8:
				int num7 = 0;
				int num8 = 0;
				if (num8 < num4)
				{
					do
					{
						int num9 = num5 + num8;
						num7 |= ((list[num9 >> 3] >> (num9 & 7)) & 1) << num8;
						num8++;
					}
					while (num8 < num4);
				}
				num5 += num4;
				if (num7 == num3)
				{
					break;
				}
				if (num7 == obj.clearCode)
				{
					goto Branch_0178;
				}
				int[] array2;
				if (num7 < obj.dict.Count && obj.dict[num7] != null)
				{
					array2 = obj.dict[num7];
				}
				else
				{
					if (array == null)
					{
						break;
					}
					array2 = new int[array.Length + 1];
					Array.Copy(array, array2, array.Length);
					array2[array.Length] = array[0];
				}
				for (int i = 0; i < array2.Length; i++)
				{
					list2.Add((byte)array2[i]);
				}
				if (array != null)
				{
					int[] array3 = new int[array.Length + 1];
					Array.Copy(array, array3, array.Length);
					array3[array.Length] = array2[0];
					obj.dict.Add(array3);
					array = array2;
					if (obj.dict.Count == 1 << num4)
					{
						goto Branch_0328;
					}
				}
				else
				{
					array = array2;
					if (obj.dict.Count == 1 << num4)
					{
						goto Branch_0328;
					}
				}
				continue;
				Branch_0328:
				if (num4 >= 12)
				{
					continue;
				}
				num4++;
				if (num5 + num4 > num6)
				{
					break;
				}
				goto Branch_00c8;
			}
			while (num5 + num4 <= num6);
			break;
			Branch_0178:;
		}
		return list2.ToArray();
	}

	[CompilerGenerated]
	private static void ResetLzwDictionary(ref CapturedVariables60 P_0)
	{
		P_0.dict.Clear();
		int num = 0;
		if (num < P_0.clearCode)
		{
			do
			{
				P_0.dict.Add(new int[1] { num });
				num++;
			}
			while (num < P_0.clearCode);
		}
		P_0.dict.Add(null);
		P_0.dict.Add(null);
	}

	private static int DrawScanline(Color32[] canvas, int W, int H, byte[] idx, int src, int ix, int cy, int iw, Color32[] palette, int transparentIndex)
	{
		int num = 0;
		if (num < iw)
		{
			while (true)
			{
				if (src >= idx.Length)
				{
					return src;
				}
				int num2 = src;
				int num3 = num2 + 1;
				src = num3;
				int num4 = idx[num2];
				if (num4 != transparentIndex && num4 < palette.Length)
				{
					int num5 = ix + num;
					if (num5 >= 0 && num5 < W && cy >= 0 && cy < H)
					{
						canvas[cy * W + num5] = palette[num4];
						num++;
						if (num >= iw)
						{
							break;
						}
					}
					else
					{
						num++;
						if (num >= iw)
						{
							break;
						}
					}
				}
				else
				{
					num++;
					if (num >= iw)
					{
						break;
					}
				}
			}
		}
		return src;
	}

	private static void RenderImage(Color32[] canvas, int W, int H, byte[] idx, int ix, int iy, int iw, int ih, bool interlace, Color32[] palette, int transparentIndex)
	{
		int src = 0;
		if (interlace)
		{
			int num = 0;
			if (num >= 4)
			{
				return;
			}
			do
			{
				int num2 = CapturedVariables60_Index_02[num];
				if (num2 < ih)
				{
					do
					{
						src = DrawScanline(canvas, W, H, idx, src, ix, iy + num2, iw, palette, transparentIndex);
						num2 += CapturedVariables60_Index_01[num];
					}
					while (num2 < ih);
				}
				num++;
			}
			while (num < 4);
			return;
		}
		int num3 = 0;
		if (num3 < ih)
		{
			do
			{
				src = DrawScanline(canvas, W, H, idx, src, ix, iy + num3, iw, palette, transparentIndex);
				num3++;
			}
			while (num3 < ih);
		}
	}

	public static bool IsGif(byte[] d)
	{
		if (d != null && d.Length > 5 && d[0] == 71 && d[1] == 73)
		{
			return d[2] == 70;
		}
		return false;
	}

	private static void SkipSubBlocks(byte[] d, ref int p)
	{
		int num;
		if ((num = d[p++]) != 0)
		{
			do
			{
				p += num;
			}
			while ((num = d[p++]) != 0);
		}
	}

	private static Color32[] ReadColorTable(byte[] d, ref int p, int size)
	{
		Color32[] array = (Color32[])(object)new Color32[size];
		int num = 0;
		if (num < size)
		{
			do
			{
				int num2 = num;
				int num3 = p;
				int num4 = num3;
				p = num4 + 1;
				byte num5 = d[num4];
				num3 = p;
				num4 = num3;
				p = num4 + 1;
				byte num6 = d[num4];
				num3 = p;
				num4 = num3;
				p = num4 + 1;
				array[num2] = new Color32(num5, num6, d[num4], byte.MaxValue);
				num++;
			}
			while (num < size);
		}
		return array;
	}

	public static List<Frame> DecodeFrames(byte[] bytes, int pixelBudget = 40000000)
	{
		List<Frame> list = new List<Frame>();
		try
		{
			if (!IsGif(bytes))
			{
				return list;
			}
			int num = 6;
			int p = num;
			int num2 = ReadUInt16(bytes, ref p);
			int num3 = ReadUInt16(bytes, ref p);
			int num4 = p;
			num = num4 + 1;
			p = num;
			int num5 = bytes[num4];
			bool flag = (num5 & 0x80) != 0;
			int size = 2 << (num5 & 7);
			num = p + 2;
			p = num;
			if (num2 <= 0 || num3 <= 0)
			{
				return list;
			}
			int num6 = Mathf.Clamp(pixelBudget / (num2 * num3), 6, 500);
			Color32[] array;
			Color32 val = default(Color32);
			Color32[] array2;
			int num7;
			int transparentIndex;
			int num8;
			if (!flag)
			{
				array = null;
				val = new Color32((byte)0, (byte)0, (byte)0, (byte)0);
				array2 = (Color32[])(object)new Color32[num2 * num3];
				num7 = 10;
				transparentIndex = -1;
				num8 = 0;
			}
			else
			{
				array = ReadColorTable(bytes, ref p, size);
				val = new Color32((byte)0, (byte)0, (byte)0, (byte)0);
				array2 = (Color32[])(object)new Color32[num2 * num3];
				num7 = 10;
				transparentIndex = -1;
				num8 = 0;
			}
			if (p < bytes.Length)
			{
				do
				{
					int num14;
					int num15;
					int num16;
					int num17;
					bool interlace;
					Color32[] array3;
					byte[] idx;
					Color32[] array4;
					Texture2D val2;
					float num18;
					int num20;
					switch ((int)bytes[p++])
					{
					case 44:
					{
						num14 = ReadUInt16(bytes, ref p);
						num15 = ReadUInt16(bytes, ref p);
						num16 = ReadUInt16(bytes, ref p);
						num17 = ReadUInt16(bytes, ref p);
						int num19 = bytes[p++];
						bool flag2 = (num19 & 0x80) != 0;
						interlace = (num19 & 0x40) != 0;
						int size2 = 2 << (num19 & 7);
						if (!flag2)
						{
							array3 = array;
							int minCodeSize = bytes[p++];
							idx = DecodeLzwData(bytes, ref p, minCodeSize, num16 * num17);
							if (num8 != 3)
							{
								goto Branch_0248;
							}
						}
						else
						{
							array3 = ReadColorTable(bytes, ref p, size2);
							int minCodeSize = bytes[p++];
							idx = DecodeLzwData(bytes, ref p, minCodeSize, num16 * num17);
							if (num8 != 3)
							{
								goto Branch_0248;
							}
						}
						array4 = (Color32[])array2.Clone();
						if (array3 == null)
						{
							goto Branch_02c3;
						}
						goto Branch_028b;
					}
					default:
						goto EndBranch_0155;
					case 33:
						{
							if (bytes[p++] == 249)
							{
								int num9 = p + 1;
								p = num9;
								int num10 = p;
								num9 = num10 + 1;
								p = num9;
								int num11 = bytes[num10];
								num8 = (num11 >> 2) & 7;
								num7 = ReadUInt16(bytes, ref p);
								int num12 = p;
								num9 = num12 + 1;
								p = num9;
								int num13 = bytes[num12];
								if ((num11 & 1) == 0)
								{
									transparentIndex = -1;
									p++;
								}
								else
								{
									transparentIndex = num13;
									p++;
								}
							}
							else
							{
								SkipSubBlocks(bytes, ref p);
							}
							break;
						}
						Branch_02c3:
						if (list.Count >= num6)
						{
							goto Branch_03a5;
						}
						goto Branch_02e1;
						Branch_028b:
						RenderImage(array2, num2, num3, idx, num14, num15, num16, num17, interlace, array3, transparentIndex);
						if (list.Count >= num6)
						{
							goto Branch_03a5;
						}
						goto Branch_02e1;
						Branch_03a5:
						if (num8 != 2)
						{
							goto Branch_04b3;
						}
						goto Branch_03be;
						Branch_02e1:
						val2 = new Texture2D(num2, num3, (TextureFormat)4, false);
						val2.SetPixels32(FlipVertically(array2, num2, num3));
						((Texture)val2).wrapMode = (TextureWrapMode)1;
						val2.Apply(false);
						num18 = (float)num7 / 100f;
						if (num18 < 0.02f)
						{
							num18 = 0.1f;
							list.Add(new Frame
							{
								texture = val2,
								delay = num18
							});
							if (num8 == 2)
							{
								goto Branch_03be;
							}
						}
						else
						{
							list.Add(new Frame
							{
								texture = val2,
								delay = num18
							});
							if (num8 == 2)
							{
								goto Branch_03be;
							}
						}
						goto Branch_04b3;
						Branch_0515:
						transparentIndex = -1;
						num8 = 0;
						if (list.Count < num6)
						{
							break;
						}
						goto EndBranch_0155;
						Branch_04b3:
						if (num8 != 3 || array4 == null)
						{
							goto Branch_0515;
						}
						array2 = array4;
						transparentIndex = -1;
						num8 = 0;
						if (list.Count < num6)
						{
							break;
						}
						goto EndBranch_0155;
						Branch_0248:
						array4 = null;
						if (array3 == null)
						{
							goto Branch_02c3;
						}
						goto Branch_028b;
						Branch_03be:
						num20 = 0;
						if (num20 < num17)
						{
							do
							{
								int num21 = 0;
								if (num21 < num16)
								{
									while (true)
									{
										int num22 = num14 + num21;
										int num23 = num15 + num20;
										if (num22 < num2 && num23 < num3)
										{
											array2[num23 * num2 + num22] = val;
											num21++;
											if (num21 >= num16)
											{
												break;
											}
										}
										else
										{
											num21++;
											if (num21 >= num16)
											{
												break;
											}
										}
									}
								}
								num20++;
							}
							while (num20 < num17);
						}
						goto Branch_0515;
					}
					continue;
					EndBranch_0155:
					break;
				}
				while (p < bytes.Length);
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("GIF decode error: " + ex.Message));
		}
		return list;
	}

	private static Color32[] FlipVertically(Color32[] src, int w, int h)
	{
		Color32[] array = (Color32[])(object)new Color32[src.Length];
		int num = 0;
		if (num < h)
		{
			do
			{
				Array.Copy(src, num * w, array, (h - 1 - num) * w, w);
				num++;
			}
			while (num < h);
		}
		return array;
	}
}
