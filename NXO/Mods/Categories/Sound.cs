using System;
using System.Collections;
using System.Collections.Generic;

using GorillaExtensions;
using NXO.Utilities;
using POpusCodec.Enums;
using Photon.Pun;
using Photon.Voice;
using Photon.Voice.Unity;
using Photon.Voice.Unity.UtilityScripts;
using UnityEngine;

namespace NXO.Mods.Categories;

public class Sound
{
	public class MicPitchShifter : VoiceComponent
	{
		public class PitchProcessor : IProcessor<float>, IDisposable
		{
			private readonly float pitch;

			public float[] Process(float[] buf)
			{
				int num = buf.Length;
				float[] array = new float[num];
				float num2 = 0f;
				int num3 = 0;
				if (num3 < num)
				{
					do
					{
						int num4 = Mathf.FloorToInt(num2);
						int num5 = Mathf.Min(num4 + 1, num - 1);
						float num6 = num2 - (float)num4;
						float num7 = Mathf.Lerp(buf[num4], buf[num5], num6);
						array[num3] = num7;
						num2 += pitch;
						if (num2 >= (float)(num - 1))
						{
							break;
						}
						num3++;
					}
					while (num3 < num);
				}
				return array;
			}

			public void Dispose()
			{
			}

			public PitchProcessor(float pitchFactor)
			{
				pitch = Mathf.Clamp(pitchFactor, 0.5f, 2f);
			}
		}

		public float PitchFactor = 1.5f;

		public PitchProcessor floatProcessor;

		public void PhotonVoiceCreated(PhotonVoiceCreatedParams p)
		{
			LocalVoice voice = p.Voice;
			LocalVoiceAudioFloat val = (LocalVoiceAudioFloat)(object)((voice is LocalVoiceAudioFloat) ? voice : null);
			if (val != null)
			{
				floatProcessor = new PitchProcessor(PitchFactor);
				((LocalVoiceFramed<float>)(object)val).AddPostProcessor(new IProcessor<float>[1] { floatProcessor });
			}
		}
	}

	public class EchoEffect : VoiceComponent
	{
		public class EchoProcessor : IProcessor<float>, IDisposable
		{
			private readonly float decay;

			private readonly float[] buffer;

			private int bufferIndex = 0;

			private const int sampleRate = 48000;

			public void Dispose()
			{
			}

			public EchoProcessor(float delaySeconds, float decayAmount)
			{
				decay = Mathf.Clamp01(decayAmount);
				buffer = new float[Mathf.CeilToInt(48000f * delaySeconds)];
			}

			public float[] Process(float[] buf)
			{
				float[] array = new float[buf.Length];
				int num = 0;
				if (num < buf.Length)
				{
					do
					{
						float num2 = buffer[bufferIndex];
						float num3 = buf[num] + num2 * decay;
						array[num] = Mathf.Clamp(num3, -1f, 1f);
						buffer[bufferIndex] = num3;
						bufferIndex = (bufferIndex + 1) % buffer.Length;
						num++;
					}
					while (num < buf.Length);
				}
				return array;
			}
		}

		public float echoDelay = 0.3f;

		public float echoDecay = 0.6f;

		private EchoProcessor processor;

		public void PhotonVoiceCreated(PhotonVoiceCreatedParams p)
		{
			LocalVoice voice = p.Voice;
			LocalVoiceAudioFloat val = (LocalVoiceAudioFloat)(object)((voice is LocalVoiceAudioFloat) ? voice : null);
			if (val != null)
			{
				processor = new EchoProcessor(echoDelay, echoDecay);
				((LocalVoiceFramed<float>)(object)val).AddPostProcessor(new IProcessor<float>[1] { processor });
			}
		}
	}

	public class RadioEffect : VoiceComponent
	{
		public class RadioProcessor : IProcessor<float>, IDisposable
		{
			private float phase = 0f;

			private const int sampleRate = 48000;

			public void Dispose()
			{
			}

			public float[] Process(float[] buf)
			{
				float[] array = new float[buf.Length];
				int num = 0;
				if (num < buf.Length)
				{
					while (true)
					{
						float num2 = buf[num];
						if (num > 0)
						{
							num2 -= buf[num - 1] * 0.9f;
							float num3 = (Random.value - 0.5f) * 0.05f;
							array[num] = Mathf.Clamp(num2 + num3, -1f, 1f);
							phase += 2.0833333E-05f;
							num++;
							if (num >= buf.Length)
							{
								break;
							}
						}
						else
						{
							float num3 = (Random.value - 0.5f) * 0.05f;
							array[num] = Mathf.Clamp(num2 + num3, -1f, 1f);
							phase += 2.0833333E-05f;
							num++;
							if (num >= buf.Length)
							{
								break;
							}
						}
					}
				}
				return array;
			}
		}

		private RadioProcessor processor;

		public void PhotonVoiceCreated(PhotonVoiceCreatedParams p)
		{
			LocalVoice voice = p.Voice;
			LocalVoiceAudioFloat val = (LocalVoiceAudioFloat)(object)((voice is LocalVoiceAudioFloat) ? voice : null);
			if (val != null)
			{
				processor = new RadioProcessor();
				((LocalVoiceFramed<float>)(object)val).AddPostProcessor(new IProcessor<float>[1] { processor });
			}
		}
	}

	public class UnderwaterEffect : VoiceComponent
	{
		public class UnderwaterProcessor : IProcessor<float>, IDisposable
		{
			private float[] lowPassBuffer = new float[5];

			private int bufferIndex = 0;

			public float[] Process(float[] buf)
			{
				float[] array = new float[buf.Length];
				int num = 0;
				if (num < buf.Length)
				{
					do
					{
						lowPassBuffer[bufferIndex] = buf[num];
						float num2 = 0f;
						int num3 = 0;
						if (num3 < lowPassBuffer.Length)
						{
							do
							{
								num2 += lowPassBuffer[num3];
								num3++;
							}
							while (num3 < lowPassBuffer.Length);
						}
						array[num] = num2 / (float)lowPassBuffer.Length * 0.7f;
						bufferIndex = (bufferIndex + 1) % lowPassBuffer.Length;
						num++;
					}
					while (num < buf.Length);
				}
				return array;
			}

			public void Dispose()
			{
			}
		}

		private UnderwaterProcessor processor;

		public void PhotonVoiceCreated(PhotonVoiceCreatedParams p)
		{
			LocalVoice voice = p.Voice;
			LocalVoiceAudioFloat val = (LocalVoiceAudioFloat)(object)((voice is LocalVoiceAudioFloat) ? voice : null);
			if (val != null)
			{
				processor = new UnderwaterProcessor();
				((LocalVoiceFramed<float>)(object)val).AddPostProcessor(new IProcessor<float>[1] { processor });
			}
		}
	}

	public class ReverbEffect : VoiceComponent
	{
		public class ReverbProcessor : IProcessor<float>, IDisposable
		{
			private readonly float amount;

			private float[] buffer1;

			private float[] buffer2;

			private float[] buffer3;

			private int index1 = 0;

			private int index2 = 0;

			private int index3 = 0;

			public float[] Process(float[] buf)
			{
				float[] array = new float[buf.Length];
				int num = 0;
				if (num < buf.Length)
				{
					do
					{
						float num2 = buffer1[index1];
						float num3 = buffer2[index2];
						float num4 = buffer3[index3];
						float num5 = (num2 + num3 + num4) / 3f * amount;
						array[num] = buf[num] + num5 * 0.4f;
						buffer1[index1] = buf[num] + num2 * 0.5f;
						buffer2[index2] = buf[num] + num3 * 0.6f;
						buffer3[index3] = buf[num] + num4 * 0.7f;
						index1 = (index1 + 1) % buffer1.Length;
						index2 = (index2 + 1) % buffer2.Length;
						index3 = (index3 + 1) % buffer3.Length;
						num++;
					}
					while (num < buf.Length);
				}
				return array;
			}

			public void Dispose()
			{
			}

			public ReverbProcessor(float reverbAmount)
			{
				amount = Mathf.Clamp01(reverbAmount);
				buffer1 = new float[1789];
				buffer2 = new float[2357];
				buffer3 = new float[3137];
			}
		}

		public float reverbAmount = 0.5f;

		private ReverbProcessor processor;

		public void PhotonVoiceCreated(PhotonVoiceCreatedParams p)
		{
			LocalVoice voice = p.Voice;
			LocalVoiceAudioFloat val = (LocalVoiceAudioFloat)(object)((voice is LocalVoiceAudioFloat) ? voice : null);
			if (val != null)
			{
				processor = new ReverbProcessor(reverbAmount);
				((LocalVoiceFramed<float>)(object)val).AddPostProcessor(new IProcessor<float>[1] { processor });
			}
		}
	}

	public class SquareWaveEffect : VoiceComponent
	{
		public class SquareProcessor : IProcessor<float>, IDisposable
		{
			private float phase = 0f;

			private const float SAMPLE_RATE = 48000f;

			private const float FREQUENCY = 3500f;

			public void Dispose()
			{
			}

			public float[] Process(float[] buf)
			{
				float[] array = new float[buf.Length];
				int num = 0;
				if (num < buf.Length)
				{
					while (true)
					{
						phase += 7f / 96f;
						if (phase >= 1f)
						{
							phase--;
							array[num] = ((phase < 0.5f) ? 1f : (-1f)) * 0.5f;
							num++;
							if (num >= buf.Length)
							{
								break;
							}
						}
						else
						{
							array[num] = ((phase < 0.5f) ? 1f : (-1f)) * 0.5f;
							num++;
							if (num >= buf.Length)
							{
								break;
							}
						}
					}
				}
				return array;
			}
		}

		private SquareProcessor processor;

		public void PhotonVoiceCreated(PhotonVoiceCreatedParams p)
		{
			LocalVoice voice = p.Voice;
			LocalVoiceAudioFloat val = (LocalVoiceAudioFloat)(object)((voice is LocalVoiceAudioFloat) ? voice : null);
			if (val != null)
			{
				processor = new SquareProcessor();
				((LocalVoiceFramed<float>)(object)val).AddPostProcessor(new IProcessor<float>[1] { processor });
			}
		}
	}

	public class AutotuneEffect : VoiceComponent
	{
		public class AutotuneProcessor : IProcessor<float>, IDisposable
		{
			private const int SAMPLE_RATE = 48000;

			private const float MIN_FREQ = 80f;

			private const float MAX_FREQ = 1100f;

			private readonly float[] pitchBuffer = new float[2048];

			private static readonly float[] AutotuneProcessor_Value_02 = BuildMidiFrequencies();

			private float SnapToNote(float freq)
			{
				float result = AutotuneProcessor_Value_02[0];
				float num = Mathf.Abs(freq - AutotuneProcessor_Value_02[0]);
				float[] v8ILUWD = AutotuneProcessor_Value_02;
				int num2 = 0;
				while (num2 < v8ILUWD.Length)
				{
					float num3 = v8ILUWD[num2];
					float num4 = Mathf.Abs(freq - num3);
					if (num4 < num)
					{
						num = num4;
						result = num3;
						num2++;
					}
					else
					{
						num2++;
					}
				}
				return result;
			}

			private float DetectPitch(float[] buf)
			{
				int num = 43;
				int num2 = 600;
				int num3 = num2;
				num2 = Mathf.Min(num3, buf.Length / 2);
				num3 = num2;
				float num4 = -1f;
				int num5 = num;
				int num6 = num;
				if (num6 < num3)
				{
					do
					{
						Branch_0032:
						float num7 = 0f;
						float num8 = 0f;
						int num9 = 0;
						if (num9 < buf.Length - num6)
						{
							do
							{
								num7 += buf[num9] * buf[num9 + num6];
								num8 += buf[num9] * buf[num9] + buf[num9 + num6] * buf[num9 + num6];
								num9++;
							}
							while (num9 < buf.Length - num6);
						}
						if (num8 > 0f)
						{
							num7 = 2f * num7 / num8;
							if (num7 > num4)
							{
								goto Branch_0110;
							}
						}
						else if (num7 > num4)
						{
							goto Branch_0110;
						}
						num6++;
						if (num6 >= num3)
						{
							break;
						}
						goto Branch_0032;
						Branch_0110:
						num4 = num7;
						num5 = num6;
						num6++;
					}
					while (num6 < num3);
				}
				if (!(num4 > 0.4f))
				{
					return -1f;
				}
				return 48000f / (float)num5;
			}

			private static float[] BuildMidiFrequencies()
			{
				float[] array = new float[128];
				int num = 0;
				if (num < 128)
				{
					do
					{
						array[num] = 440f * Mathf.Pow(2f, (float)(num - 69) / 12f);
						num++;
					}
					while (num < 128);
				}
				return array;
			}

			public float[] Process(float[] buf)
			{
				float num = DetectPitch(buf);
				if (num < 0f)
				{
					return buf;
				}
				float num2 = SnapToNote(num) / num;
				float num3 = num2;
				num2 = Mathf.Clamp(num3, 0.5f, 2f);
				num3 = num2;
				float[] array = new float[buf.Length];
				for (int num4 = 0; num4 < buf.Length; num4++)
				{
					float samplePosition = num4 * num3;
					int num5 = Mathf.FloorToInt(samplePosition);
					int num6 = Mathf.Min(num5 + 1, buf.Length - 1);
					if (num5 >= buf.Length)
					{
						array[num4] = buf[^1];
					}
					else
					{
						array[num4] = Mathf.Lerp(buf[num5], buf[num6], samplePosition - num5);
					}
				}
				return array;
			}

			public void Dispose()
			{
			}
		}

		private AutotuneProcessor processor;

		public void PhotonVoiceCreated(PhotonVoiceCreatedParams p)
		{
			LocalVoice voice = p.Voice;
			LocalVoiceAudioFloat val = (LocalVoiceAudioFloat)(object)((voice is LocalVoiceAudioFloat) ? voice : null);
			if (val != null)
			{
				processor = new AutotuneProcessor();
				((LocalVoiceFramed<float>)(object)val).AddPostProcessor(new IProcessor<float>[1] { processor });
			}
		}
	}

	public static bool AutotuneProcessor_State_03 = false;

	public static bool AutotuneProcessor_State_04 = false;

	public static int AutotuneProcessor_Index_01 = 0;

	private static bool AutotuneProcessor_State_02 = false;

	private static bool AutotuneProcessor_State_01 = false;

	private static float AutotuneProcessor_Value_01 = 0.175f;

	public static void ReloadMicrophone()
	{
		GorillaTagger instance = GorillaTagger.Instance;
		if ((Object)(object)((instance != null) ? instance.myRecorder : null) != (Object)null)
		{
			GorillaTagger.Instance.myRecorder.RestartRecording(true);
		}
	}

	public static void SetMuffledMicrophoneEnabled(bool enable)
	{
		if (!PhotonNetwork.InRoom)
		{
			return;
		}
		Recorder myRecorder = GorillaTagger.Instance.myRecorder;
		if (enable)
		{
			if (!((Object)(object)((Component)myRecorder).gameObject.GetComponent<UnderwaterEffect>() != (Object)null))
			{
				GTExt.GetOrAddComponent<UnderwaterEffect>(((Component)myRecorder).gameObject);
				((MonoBehaviour)CoroutineHelper.Instance).StartCoroutine(ReloadMicrophoneAfterDelay());
			}
		}
		else if (((Object)(object)((Component)myRecorder).gameObject.GetComponent<UnderwaterEffect>()))
		{
			((Behaviour)((Component)myRecorder).gameObject.GetComponent<UnderwaterEffect>()).enabled = false;
			Object.Destroy((Object)(object)((Component)myRecorder).gameObject.GetComponent<UnderwaterEffect>());
			((MonoBehaviour)CoroutineHelper.Instance).StartCoroutine(ReloadMicrophoneAfterDelay());
		}
	}

	public static void SetMicrophonePitch(float pitch)
	{
		if (!PhotonNetwork.InRoom)
		{
			return;
		}
		Recorder myRecorder = GorillaTagger.Instance.myRecorder;
		if (!Mathf.Approximately(pitch, 1f))
		{
			MicPitchShifter component = ((Component)myRecorder).gameObject.GetComponent<MicPitchShifter>();
			if (!((Object)(object)component != (Object)null) || !Mathf.Approximately(component.PitchFactor, pitch))
			{
				GTExt.GetOrAddComponent<MicPitchShifter>(((Component)myRecorder).gameObject).PitchFactor = pitch;
				((MonoBehaviour)CoroutineHelper.Instance).StartCoroutine(ReloadMicrophoneAfterDelay());
			}
		}
		else if (((Object)(object)((Component)myRecorder).gameObject.GetComponent<MicPitchShifter>()))
		{
			((Behaviour)((Component)myRecorder).gameObject.GetComponent<MicPitchShifter>()).enabled = false;
			Object.Destroy((Object)(object)((Component)myRecorder).gameObject.GetComponent<MicPitchShifter>());
			((MonoBehaviour)CoroutineHelper.Instance).StartCoroutine(ReloadMicrophoneAfterDelay());
		}
	}

	public static string GetHandTapSoundName(int index)
	{
		int num = index;
		num = (((uint)num <= 228u) ? num : 229) + 233;
		int num2 = num;
		if (num2 != 234)
		{
			return "Default";
		}
		return "Rock Wall";
	}

	public static void SetMicrophoneQuality(int bitrate, int samplingRate)
	{
		Recorder val = GorillaTagger.Instance.myRecorder;
		if (PhotonNetwork.InRoom && ((int)GorillaTagger.Instance.myRecorder.SamplingRate != samplingRate || val.Bitrate != bitrate))
		{
			val.SamplingRate = (SamplingRate)samplingRate;
			val.Bitrate = bitrate;
			((MonoBehaviour)CoroutineHelper.Instance).StartCoroutine(ReloadMicrophoneAfterDelay());
		}
	}

	public static void SetEchoMicrophoneEnabled(bool enable)
	{
		if (!PhotonNetwork.InRoom)
		{
			return;
		}
		Recorder myRecorder = GorillaTagger.Instance.myRecorder;
		if ((Object)(object)myRecorder == (Object)null)
		{
			return;
		}
		if (enable)
		{
			if (!((Object)(object)((Component)myRecorder).gameObject.GetComponent<EchoEffect>() != (Object)null))
			{
				EchoEffect orAddComponent = GTExt.GetOrAddComponent<EchoEffect>(((Component)myRecorder).gameObject);
				orAddComponent.echoDelay = 0.3f;
				orAddComponent.echoDecay = 0.6f;
				myRecorder.VoiceDetection = false;
				myRecorder.VoiceDetectionThreshold = 0f;
				myRecorder.TransmitEnabled = true;
				((MonoBehaviour)CoroutineHelper.Instance).StartCoroutine(ReloadMicrophoneAfterDelay());
			}
		}
		else
		{
			EchoEffect component = ((Component)myRecorder).gameObject.GetComponent<EchoEffect>();
			if (!((Object)(object)component == (Object)null))
			{
				((Behaviour)component).enabled = false;
				Object.Destroy((Object)(object)component);
				myRecorder.VoiceDetection = true;
				myRecorder.VoiceDetectionThreshold = 0.02f;
				((MonoBehaviour)CoroutineHelper.Instance).StartCoroutine(ReloadMicrophoneAfterDelay());
			}
		}
	}

	public static void ResetMicrophone()
	{
		GorillaTagger obj = Variables.Variables_Reference_09;
		Recorder val = ((obj != null) ? obj.myRecorder : null);
		if (!((Object)(object)val == (Object)null))
		{
			Soundboard.StopAllSounds();
			Object.Destroy((Object)(object)((Component)val).gameObject.GetComponent<MicPitchShifter>());
			Object.Destroy((Object)(object)((Component)val).gameObject.GetComponent<EchoEffect>());
			Object.Destroy((Object)(object)((Component)val).gameObject.GetComponent<ReverbEffect>());
			Object.Destroy((Object)(object)((Component)val).gameObject.GetComponent<RadioEffect>());
			Object.Destroy((Object)(object)((Component)val).gameObject.GetComponent<UnderwaterEffect>());
			Object.Destroy((Object)(object)((Component)val).gameObject.GetComponent<SquareWaveEffect>());
			Object.Destroy((Object)(object)((Component)val).gameObject.GetComponent<MicAmplifier>());
			val.SourceType = (InputSourceType)0;
			val.AudioClip = null;
			val.LoopAudioClip = false;
			val.IsRecording = true;
			val.VoiceDetection = true;
			val.VoiceDetectionThreshold = 0.02f;
			val.TransmitEnabled = true;
			val.Bitrate = 20000;
			val.SamplingRate = (SamplingRate)16000;
			val.RestartRecording(true);
		}
	}

	public static void SetStaticMicrophoneEnabled(bool enable)
	{
		if (!PhotonNetwork.InRoom)
		{
			return;
		}
		Recorder myRecorder = GorillaTagger.Instance.myRecorder;
		if (enable)
		{
			if (!((Object)(object)((Component)myRecorder).gameObject.GetComponent<RadioEffect>() != (Object)null))
			{
				GTExt.GetOrAddComponent<RadioEffect>(((Component)myRecorder).gameObject);
				((MonoBehaviour)CoroutineHelper.Instance).StartCoroutine(ReloadMicrophoneAfterDelay());
			}
		}
		else if (((Object)(object)((Component)myRecorder).gameObject.GetComponent<RadioEffect>()))
		{
			((Behaviour)((Component)myRecorder).gameObject.GetComponent<RadioEffect>()).enabled = false;
			Object.Destroy((Object)(object)((Component)myRecorder).gameObject.GetComponent<RadioEffect>());
			((MonoBehaviour)CoroutineHelper.Instance).StartCoroutine(ReloadMicrophoneAfterDelay());
		}
	}

	public static void HandTapSoundGun()
	{
		if (Time.time < Variables.Variables_Value_03)
		{
			return;
		}
		Variables.Variables_Value_03 = Time.time + AutotuneProcessor_Value_01;
		Safety.ResetNetworkLimits();
		if (InputHandler.IsRightTriggerPressed())
		{
			if (PhotonNetwork.InRoom)
			{
				Variables.Variables_Reference_09.myVRRig.GetView.RPC("RPC_PlayHandTap", (RpcTarget)0, new object[3]
				{
					AutotuneProcessor_Index_01,
					Variables.Variables_State_05,
					99999f
				});
				if (InputHandler.IsRightPrimaryPressed())
				{
					goto Branch_012b;
				}
			}
			else
			{
				Variables.Variables_Reference_09.offlineVRRig.PlayHandTapLocal(AutotuneProcessor_Index_01, Variables.Variables_State_05, 99999f);
				if (InputHandler.IsRightPrimaryPressed())
				{
					goto Branch_012b;
				}
			}
		}
		else if (InputHandler.IsRightPrimaryPressed())
		{
			goto Branch_012b;
		}
		Branch_01c4:
		if (InputHandler.IsRightPrimaryPressed())
		{
			goto Branch_01ff;
		}
		goto Branch_01e0;
		Branch_01ff:
		if (!InputHandler.IsRightSecondaryPressed())
		{
			goto Branch_02b1;
		}
		goto Branch_0218;
		Branch_02cd:
		AutotuneProcessor_State_04 = false;
		return;
		Branch_0218:
		if (AutotuneProcessor_State_04)
		{
			goto Branch_02b1;
		}
		AutotuneProcessor_Index_01--;
		string text = GetHandTapSoundName(AutotuneProcessor_Index_01);
		NotificationLib.ShowNotification(NotificationLib.NotificationType.Info, "Set Index To: " + text + " [" + AutotuneProcessor_Index_01 + "]");
		AutotuneProcessor_State_04 = true;
		if (InputHandler.IsRightSecondaryPressed())
		{
			return;
		}
		goto Branch_02cd;
		Branch_01e0:
		AutotuneProcessor_State_03 = false;
		if (!InputHandler.IsRightSecondaryPressed())
		{
			goto Branch_02b1;
		}
		goto Branch_0218;
		Branch_012b:
		if (AutotuneProcessor_State_03)
		{
			goto Branch_01c4;
		}
		AutotuneProcessor_Index_01++;
		string text2 = GetHandTapSoundName(AutotuneProcessor_Index_01);
		NotificationLib.ShowNotification(NotificationLib.NotificationType.Info, "Set Index To: " + text2 + " [" + AutotuneProcessor_Index_01 + "]");
		AutotuneProcessor_State_03 = true;
		if (InputHandler.IsRightPrimaryPressed())
		{
			goto Branch_01ff;
		}
		goto Branch_01e0;
		Branch_02b1:
		if (InputHandler.IsRightSecondaryPressed())
		{
			return;
		}
		goto Branch_02cd;
	}

	public static void PlayContactSound(int SoundIndex)
	{
		if (Time.time < Variables.Variables_Value_03)
		{
			return;
		}
		bool flag = false;
		bool flag2 = false;
		float num = 0.325f;
		using (IEnumerator<VRRig> enumerator = VRRigCache.ActiveRigs.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				VRRig val = enumerator.Current;
				while (RigManager.IsRemoteRig(enumerator.Current))
				{
					if (!flag)
					{
						flag = Vector3.Distance(Variables.Variables_Reference_09.leftHandTransform.position, val.headMesh.transform.position) < num;
						if (!flag2)
						{
							goto Branch_00f0;
						}
					}
					else if (!flag2)
					{
						goto Branch_00f0;
					}
					if (!(flag & flag2))
					{
						goto Branch_0151;
					}
					goto EndBranch_0166;
					Branch_0151:
					if (!enumerator.MoveNext())
					{
						goto EndBranch_0166;
					}
					continue;
					Branch_00f0:
					flag2 = Vector3.Distance(Variables.Variables_Reference_09.rightHandTransform.position, val.headMesh.transform.position) < num;
					if (!(flag & flag2))
					{
						goto Branch_0151;
					}
					goto EndBranch_0166;
				}
				continue;
				EndBranch_0166:
				break;
			}
		}
		if (flag && !AutotuneProcessor_State_02)
		{
			Variables.Variables_Value_03 = Time.time + AutotuneProcessor_Value_01;
			Variables.Variables_Reference_09.myVRRig.GetView.RPC("RPC_PlayHandTap", (RpcTarget)0, new object[3] { SoundIndex, true, 99999f });
			Safety.ResetNetworkLimits();
			if (flag2)
			{
				goto Branch_0265;
			}
		}
		else if (flag2)
		{
			goto Branch_0265;
		}
		goto Branch_02f5;
		Branch_0265:
		if (AutotuneProcessor_State_01)
		{
			goto Branch_02f5;
		}
		Variables.Variables_Value_03 = Time.time + AutotuneProcessor_Value_01;
		Variables.Variables_Reference_09.myVRRig.GetView.RPC("RPC_PlayHandTap", (RpcTarget)0, new object[3] { SoundIndex, false, 99999f });
		Safety.ResetNetworkLimits();
		AutotuneProcessor_State_02 = flag;
		AutotuneProcessor_State_01 = flag2;
		return;
		Branch_02f5:
		AutotuneProcessor_State_02 = flag;
		AutotuneProcessor_State_01 = flag2;
	}

	public static void SetLoudMicrophoneEnabled(bool enable)
	{
		if (!PhotonNetwork.InRoom)
		{
			return;
		}
		Recorder myRecorder = GorillaTagger.Instance.myRecorder;
		if (enable)
		{
			if (!((Object)(object)((Component)myRecorder).gameObject.GetComponent<MicAmplifier>() != (Object)null))
			{
				MicAmplifier orAddComponent = GTExt.GetOrAddComponent<MicAmplifier>(((Component)myRecorder).gameObject);
				orAddComponent.AmplificationFactor = 13f;
				orAddComponent.AmplificationFactor = 13f;
				((MonoBehaviour)CoroutineHelper.Instance).StartCoroutine(ReloadMicrophoneAfterDelay());
			}
		}
		else if (((Object)(object)((Component)myRecorder).gameObject.GetComponent<MicAmplifier>()))
		{
			if (!((Object)(object)((Component)myRecorder).gameObject.GetComponent<MicAmplifier>() == (Object)null))
			{
				((Behaviour)((Component)myRecorder).gameObject.GetComponent<MicAmplifier>()).enabled = false;
				Object.Destroy((Object)(object)((Component)myRecorder).gameObject.GetComponent<MicAmplifier>());
				((MonoBehaviour)CoroutineHelper.Instance).StartCoroutine(ReloadMicrophoneAfterDelay());
			}
		}
		else
		{
			((MonoBehaviour)CoroutineHelper.Instance).StartCoroutine(ReloadMicrophoneAfterDelay());
		}
	}

	public static void SpamHandTapSound(int SoundIndex)
	{
		if (Time.time < Variables.Variables_Value_03)
		{
			return;
		}
		Variables.Variables_Value_03 = Time.time + AutotuneProcessor_Value_01;
		Safety.ResetNetworkLimits();
		if (InputHandler.IsRightTriggerPressed())
		{
			if (PhotonNetwork.InRoom)
			{
				Variables.Variables_Reference_09.myVRRig.GetView.RPC("RPC_PlayHandTap", (RpcTarget)0, new object[3]
				{
					SoundIndex,
					Variables.Variables_State_05,
					99999f
				});
			}
			else
			{
				Variables.Variables_Reference_09.offlineVRRig.PlayHandTapLocal(SoundIndex, Variables.Variables_State_05, 1f);
			}
		}
	}

	public static void SetMuteMicrophoneEnabled(bool mute)
	{
		if (PhotonNetwork.InRoom && GorillaTagger.Instance.myRecorder.IsRecording == mute)
		{
			Recorder val = GorillaTagger.Instance.myRecorder;
			val.IsRecording = !mute;
		}
	}

	public static void SetSquareWaveMicrophoneEnabled(bool enable)
	{
		if (!PhotonNetwork.InRoom)
		{
			return;
		}
		Recorder myRecorder = GorillaTagger.Instance.myRecorder;
		if (enable)
		{
			if (!((Object)(object)((Component)myRecorder).gameObject.GetComponent<SquareWaveEffect>() != (Object)null))
			{
				myRecorder.TransmitEnabled = true;
				myRecorder.VoiceDetection = false;
				myRecorder.VoiceDetectionThreshold = 0f;
				GTExt.GetOrAddComponent<SquareWaveEffect>(((Component)myRecorder).gameObject);
				((MonoBehaviour)CoroutineHelper.Instance).StartCoroutine(ReloadMicrophoneAfterDelay());
			}
		}
		else if (((Object)(object)((Component)myRecorder).gameObject.GetComponent<SquareWaveEffect>()))
		{
			myRecorder.VoiceDetection = true;
			myRecorder.VoiceDetectionThreshold = 0.01f;
			((Behaviour)((Component)myRecorder).gameObject.GetComponent<SquareWaveEffect>()).enabled = false;
			Object.Destroy((Object)(object)((Component)myRecorder).gameObject.GetComponent<SquareWaveEffect>());
			((MonoBehaviour)CoroutineHelper.Instance).StartCoroutine(ReloadMicrophoneAfterDelay());
		}
	}

	public Sound()
	{
	}

	private static IEnumerator ReloadMicrophoneAfterDelay()
	{
		yield return (object)new WaitForSeconds(0.25f);
		ReloadMicrophone();
	}

	public static void SetReverbMicrophoneEnabled(bool enable)
	{
		if (!PhotonNetwork.InRoom)
		{
			return;
		}
		Recorder myRecorder = GorillaTagger.Instance.myRecorder;
		if ((Object)(object)myRecorder == (Object)null)
		{
			return;
		}
		if (enable)
		{
			if (!((Object)(object)((Component)myRecorder).gameObject.GetComponent<ReverbEffect>() != (Object)null))
			{
				GTExt.GetOrAddComponent<ReverbEffect>(((Component)myRecorder).gameObject);
				myRecorder.VoiceDetection = false;
				myRecorder.VoiceDetectionThreshold = 0f;
				myRecorder.TransmitEnabled = true;
				((MonoBehaviour)CoroutineHelper.Instance).StartCoroutine(ReloadMicrophoneAfterDelay());
			}
			return;
		}
		ReverbEffect component = ((Component)myRecorder).gameObject.GetComponent<ReverbEffect>();
		if (!((Object)(object)component == (Object)null))
		{
			((Behaviour)component).enabled = false;
			Object.Destroy((Object)(object)component);
			myRecorder.VoiceDetection = true;
			myRecorder.VoiceDetectionThreshold = 0.02f;
			((MonoBehaviour)CoroutineHelper.Instance).StartCoroutine(ReloadMicrophoneAfterDelay());
		}
	}
}
