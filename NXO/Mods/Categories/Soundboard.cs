using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NXO.Menu;
using NXO.Utilities;
using Photon.Voice.PUN;
using Photon.Voice.Unity;
using UnityEngine;
using UnityEngine.Networking;

namespace NXO.Mods.Categories;

public static class Soundboard
{
	[CompilerGenerated]
	private sealed class CapturedVariables370
	{
		public string pathOrUrl;

		internal void AddSoundButtons_Lambda0()
		{
			PlaySound(pathOrUrl);
		}

		internal void AddSoundButtons_Lambda1()
		{
			DisableAllSounds(pathOrUrl);
		}
	}

	[CompilerGenerated]
	private sealed class CapturedVariables460
	{
		public TaskCompletionSource<bool> tcs;

		internal void GetRawFrom_Lambda0(AsyncOperation _)
		{
			tcs.SetResult(result: true);
		}
	}

	[CompilerGenerated]
	private sealed class DownloadAndPlaySound_StateMachine39 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int State;

		private object Current;

		public string pathOrUrl;

		private AudioClip audioClipCaptured1;

		private AudioSource staleCaptured2;

		private GameObject soundObjectCaptured3;

		private AudioSource audioSourceCaptured4;

		private bool isLocalFileCaptured5;

		private AudioType audioTypeCaptured6;

		private string uriCaptured7;

		private UnityWebRequest requestCaptured8;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return Current;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return Current;
			}
		}

		private void Finally1()
		{
			State = -1;
			if (requestCaptured8 != null)
			{
				((IDisposable)requestCaptured8).Dispose();
			}
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		[DebuggerHidden]
		public DownloadAndPlaySound_StateMachine39(int State)
		{
			this.State = State;
		}

		private bool MoveNext()
		{
			bool result;
			try
			{
				int num = State;
				if (num != 0)
				{
					if (num != 1)
					{
						result = false;
					}
					else
					{
						State = -3;
						if ((int)requestCaptured8.result != 1)
						{
							Debug.LogError((object)("Failed to load audio: " + requestCaptured8.error));
							MarkPlaybackStopped(pathOrUrl);
							result = false;
						}
						else
						{
							audioClipCaptured1 = DownloadHandlerAudioClip.GetContent(requestCaptured8);
							if (!((Object)(object)audioClipCaptured1 == (Object)null))
							{
								LoadSoundboardSounds_StateMachine45_Lookup_05[pathOrUrl] = audioClipCaptured1;
								Finally1();
								requestCaptured8 = null;
								uriCaptured7 = null;
								if (!LoadSoundboardSounds_StateMachine45_Lookup_06.TryGetValue(pathOrUrl, out staleCaptured2))
								{
									goto Branch_0376;
								}
								goto Branch_02a1;
							}
							Debug.LogError((object)"Audio clip is null");
							MarkPlaybackStopped(pathOrUrl);
							result = false;
						}
						Finally1();
					}
				}
				else
				{
					State = -1;
					if (LoadSoundboardSounds_StateMachine45_Lookup_05.TryGetValue(pathOrUrl, out audioClipCaptured1) && !((Object)(object)audioClipCaptured1 == (Object)null))
					{
						if (!LoadSoundboardSounds_StateMachine45_Lookup_06.TryGetValue(pathOrUrl, out staleCaptured2))
						{
							goto Branch_0376;
						}
						goto Branch_02a1;
					}
					isLocalFileCaptured5 = File.Exists(pathOrUrl);
					audioTypeCaptured6 = GetAudioType(pathOrUrl);
					uriCaptured7 = (isLocalFileCaptured5 ? ("file://" + pathOrUrl) : pathOrUrl);
					requestCaptured8 = UnityWebRequestMultimedia.GetAudioClip(uriCaptured7, audioTypeCaptured6);
					State = -3;
					Current = requestCaptured8.SendWebRequest();
					State = 1;
					result = true;
				}
				goto EndBranch_0000;
				Branch_0376:
				soundObjectCaptured3 = new GameObject("SoundFromFile");
				audioSourceCaptured4 = soundObjectCaptured3.AddComponent<AudioSource>();
				audioSourceCaptured4.clip = audioClipCaptured1;
				audioSourceCaptured4.volume = 1f;
				audioSourceCaptured4.loop = LoadSoundboardSounds_StateMachine45_State_03;
				LoadSoundboardSounds_StateMachine45_Lookup_06[pathOrUrl] = audioSourceCaptured4;
				LoadSoundboardSounds_StateMachine45_Audio_01 = audioSourceCaptured4;
				StartAudioSourcePlayback(audioSourceCaptured4, pathOrUrl);
				result = false;
				goto EndBranch_0000;
				Branch_02a1:
				if (!((Object)(object)staleCaptured2 != (Object)null))
				{
					goto Branch_0376;
				}
				staleCaptured2.Stop();
				Object.Destroy((Object)(object)((Component)staleCaptured2).gameObject);
				soundObjectCaptured3 = new GameObject("SoundFromFile");
				audioSourceCaptured4 = soundObjectCaptured3.AddComponent<AudioSource>();
				audioSourceCaptured4.clip = audioClipCaptured1;
				audioSourceCaptured4.volume = 1f;
				audioSourceCaptured4.loop = LoadSoundboardSounds_StateMachine45_State_03;
				LoadSoundboardSounds_StateMachine45_Lookup_06[pathOrUrl] = audioSourceCaptured4;
				LoadSoundboardSounds_StateMachine45_Audio_01 = audioSourceCaptured4;
				StartAudioSourcePlayback(audioSourceCaptured4, pathOrUrl);
				result = false;
				EndBranch_0000:;
			}
			catch (Exception)
			{
				((IDisposable)this).Dispose();
				bool result2 = default(bool);
				return result2;
			}
			return result;
		}

		bool IEnumerator.MoveNext()
		{
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			int num = State;
			if (num == -3 || num == 1)
			{
				try
				{
				}
				catch (Exception)
				{
					Finally1();
					return;
				}
			}
			audioClipCaptured1 = null;
			staleCaptured2 = null;
			soundObjectCaptured3 = null;
			audioSourceCaptured4 = null;
			uriCaptured7 = null;
			requestCaptured8 = null;
			State = -2;
		}
	}

	[CompilerGenerated]
	private sealed class GetRawFrom_StateMachine46 : IAsyncStateMachine
	{
		public int State;

		public AsyncTaskMethodBuilder<string> Builder;

		public string url;

		private UnityWebRequest requestCaptured1;

		private CapturedVariables460 CachedDelegate2;

		private TaskAwaiter<bool> Awaiter1;

		private void MoveNext()
		{
			int num = State;
			string result;
			try
			{
				if (num != 0)
				{
					requestCaptured1 = UnityWebRequest.Get(url);
				}
				try
				{
					if (num != 0)
					{
						CachedDelegate2 = new CapturedVariables460();
						CachedDelegate2.tcs = new TaskCompletionSource<bool>();
						((AsyncOperation)requestCaptured1.SendWebRequest()).completed += delegate
						{
							CachedDelegate2.tcs.SetResult(result: true);
						};
						TaskAwaiter<bool> awaiter = CachedDelegate2.tcs.Task.GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (State = 0);
							Awaiter1 = awaiter;
							GetRawFrom_StateMachine46 stateMachine = this;
							Builder.AwaitUnsafeOnCompleted(ref awaiter, ref stateMachine);
							return;
						}
						awaiter.GetResult();
						if ((int)requestCaptured1.result != 1)
						{
							goto Branch_0181;
						}
					}
					else
					{
						TaskAwaiter<bool> awaiter = Awaiter1;
						Awaiter1 = default(TaskAwaiter<bool>);
						num = (State = -1);
						awaiter.GetResult();
						if ((int)requestCaptured1.result != 1)
						{
							goto Branch_0181;
						}
					}
					result = requestCaptured1.downloadHandler.text;
					goto EndBranch_005f;
					Branch_0181:
					result = null;
					EndBranch_005f:;
				}
				finally
				{
					if (num < 0 && requestCaptured1 != null)
					{
						((IDisposable)requestCaptured1).Dispose();
					}
				}
			}
			catch (Exception exception)
			{
				State = -2;
				Builder.SetException(exception);
				return;
			}
			State = -2;
			Builder.SetResult(result);
		}

		void IAsyncStateMachine.MoveNext()
		{
			this.MoveNext();
		}

		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			this.SetStateMachine(stateMachine);
		}
	}

	[CompilerGenerated]
	private sealed class LoadAllSoundboardSounds_StateMachine43 : IAsyncStateMachine
	{
		public int State;

		public AsyncTaskMethodBuilder Builder;

		private Dictionary<string, string>.Enumerator IteratorTemp1;

		private KeyValuePair<string, string> entryCaptured2;

		private TaskAwaiter Awaiter1;

		private void MoveNext()
		{
			int num = State;
			try
			{
				if (num != 0)
				{
					IteratorTemp1 = LoadSoundboardSounds_StateMachine45_Lookup_03.GetEnumerator();
				}
				try
				{
					if (num == 0)
					{
						TaskAwaiter taskAwaiter = Awaiter1;
						Awaiter1 = default(TaskAwaiter);
						num = (State = -1);
						taskAwaiter.GetResult();
						entryCaptured2 = default(KeyValuePair<string, string>);
						if (IteratorTemp1.MoveNext())
						{
							goto Branch_0091;
						}
					}
					else if (IteratorTemp1.MoveNext())
					{
						goto Branch_0091;
					}
					goto EndBranch_005e;
					Branch_0091:
					do
					{
						entryCaptured2 = IteratorTemp1.Current;
						TaskAwaiter taskAwaiter = LoadSoundboardSounds(entryCaptured2.Value, entryCaptured2.Key).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							num = (State = 0);
							Awaiter1 = taskAwaiter;
							LoadAllSoundboardSounds_StateMachine43 stateMachine = this;
							Builder.AwaitUnsafeOnCompleted(ref taskAwaiter, ref stateMachine);
							return;
						}
						taskAwaiter.GetResult();
						entryCaptured2 = default(KeyValuePair<string, string>);
					}
					while (IteratorTemp1.MoveNext());
					EndBranch_005e:;
				}
				finally
				{
					if (num < 0)
					{
						((IDisposable)IteratorTemp1/*cast due to constrained. prefix*/).Dispose();
					}
				}
				IteratorTemp1 = default(Dictionary<string, string>.Enumerator);
				LoadCustom_Sounds();
			}
			catch (Exception exception)
			{
				State = -2;
				Builder.SetException(exception);
				return;
			}
			State = -2;
			Builder.SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			this.MoveNext();
		}

		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			this.SetStateMachine(stateMachine);
		}
	}

	[CompilerGenerated]
	private sealed class LoadAndPlay_StateMachine32 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int State;

		private object Current;

		public string fullPath;

		public string soundName;

		public AudioType audioType;

		private AudioClip cachedCaptured1;

		private string uriCaptured2;

		private UnityWebRequest wwwCaptured3;

		private AudioClip clipCaptured4;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return Current;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return Current;
			}
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			int num = State;
			if (num == -3 || (uint)(num - 2) <= 1u)
			{
				try
				{
				}
				catch (Exception)
				{
					Finally1();
					return;
				}
			}
			cachedCaptured1 = null;
			uriCaptured2 = null;
			wwwCaptured3 = null;
			clipCaptured4 = null;
			State = -2;
		}

		private void Finally1()
		{
			State = -1;
			if (wwwCaptured3 != null)
			{
				((IDisposable)wwwCaptured3).Dispose();
			}
		}

		[DebuggerHidden]
		public LoadAndPlay_StateMachine32(int State)
		{
			this.State = State;
		}

		private bool MoveNext()
		{
			bool result;
			try
			{
				int num = State;
				num = (((uint)num <= 3u) ? num : 4) + 33;
				int num2 = num;
				if (num2 != 34)
				{
					State = -1;
					if (LoadSoundboardSounds_StateMachine45_Lookup_05.TryGetValue(fullPath, out cachedCaptured1) && (Object)(object)cachedCaptured1 != (Object)null)
					{
						Current = PlayClipCoroutine(cachedCaptured1);
						State = 1;
						result = true;
					}
					else
					{
						uriCaptured2 = "file:///" + fullPath.Replace("\\", "/");
						wwwCaptured3 = UnityWebRequestMultimedia.GetAudioClip(uriCaptured2, audioType);
						State = -3;
						Current = wwwCaptured3.SendWebRequest();
						State = 2;
						result = true;
					}
				}
				else
				{
					State = -1;
					result = false;
				}
			}
			catch (Exception)
			{
				((IDisposable)this).Dispose();
				bool result2 = default(bool);
				return result2;
			}
			return result;
		}

		bool IEnumerator.MoveNext()
		{
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}
	}

	[CompilerGenerated]
	private sealed class LoadSoundboardSounds_StateMachine45 : IAsyncStateMachine
	{
		public int State;

		public AsyncTaskMethodBuilder Builder;

		public string url;

		public string category;

		private string rawDataCaptured1;

		private Dictionary<string, string> targetDictCaptured2;

		private string[] linesCaptured3;

		private string IteratorTemp4;

		private string[] IteratorTemp5;

		private int IteratorTemp6;

		private string lineCaptured7;

		private string[] partsCaptured8;

		private string nameCaptured9;

		private string soundUrlCaptured10;

		private TaskAwaiter<string> Awaiter1;

		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			this.SetStateMachine(stateMachine);
		}

		private void MoveNext()
		{
			int num = State;
			try
			{
				if (num != 0)
				{
					TaskAwaiter<string> awaiter = GetRawTextAsync(url).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						State = 0;
						Awaiter1 = awaiter;
						LoadSoundboardSounds_StateMachine45 stateMachine = this;
						Builder.AwaitUnsafeOnCompleted(ref awaiter, ref stateMachine);
						return;
					}
					IteratorTemp4 = awaiter.GetResult();
					rawDataCaptured1 = IteratorTemp4;
					IteratorTemp4 = null;
					if (string.IsNullOrEmpty(rawDataCaptured1))
					{
						goto Branch_0122;
					}
				}
				else
				{
					TaskAwaiter<string> awaiter = Awaiter1;
					Awaiter1 = default(TaskAwaiter<string>);
					State = -1;
					IteratorTemp4 = awaiter.GetResult();
					rawDataCaptured1 = IteratorTemp4;
					IteratorTemp4 = null;
					if (string.IsNullOrEmpty(rawDataCaptured1))
					{
						goto Branch_0122;
					}
				}
				targetDictCaptured2 = category switch
				{
					"SFX" => LoadSoundboardSounds_StateMachine45_Lookup_01, 
					"Trolling" => LoadSoundboardSounds_StateMachine45_Lookup_07, 
					"Songs" => LoadSoundboardSounds_StateMachine45_Lookup_08, 
					_ => null, 
				};
				if (targetDictCaptured2 == null)
				{
					Debug.LogError((object)("Invalid category: " + category));
				}
				else
				{
					linesCaptured3 = rawDataCaptured1.Split(new char[2] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
					IteratorTemp5 = linesCaptured3;
					IteratorTemp6 = 0;
					if (IteratorTemp6 < IteratorTemp5.Length)
					{
						while (true)
						{
							lineCaptured7 = IteratorTemp5[IteratorTemp6];
							partsCaptured8 = lineCaptured7.Split(';');
							if (partsCaptured8.Length == 2)
							{
								nameCaptured9 = partsCaptured8[0].Trim();
								soundUrlCaptured10 = partsCaptured8[1].Trim();
								if (!targetDictCaptured2.ContainsKey(nameCaptured9))
								{
									targetDictCaptured2[nameCaptured9] = soundUrlCaptured10;
									partsCaptured8 = null;
									nameCaptured9 = null;
									soundUrlCaptured10 = null;
									lineCaptured7 = null;
									IteratorTemp6++;
									if (IteratorTemp6 >= IteratorTemp5.Length)
									{
										break;
									}
								}
								else
								{
									partsCaptured8 = null;
									nameCaptured9 = null;
									soundUrlCaptured10 = null;
									lineCaptured7 = null;
									IteratorTemp6++;
									if (IteratorTemp6 >= IteratorTemp5.Length)
									{
										break;
									}
								}
							}
							else
							{
								IteratorTemp6++;
								if (IteratorTemp6 >= IteratorTemp5.Length)
								{
									break;
								}
							}
						}
					}
					IteratorTemp5 = null;
				}
				goto EndBranch_0011;
				Branch_0122:
				Debug.LogError((object)("Failed to fetch sounds for " + category));
				EndBranch_0011:;
			}
			catch (Exception exception)
			{
				State = -2;
				rawDataCaptured1 = null;
				targetDictCaptured2 = null;
				linesCaptured3 = null;
				Builder.SetException(exception);
				return;
			}
			State = -2;
			rawDataCaptured1 = null;
			targetDictCaptured2 = null;
			linesCaptured3 = null;
			Builder.SetResult();
		}

		void IAsyncStateMachine.MoveNext()
		{
			this.MoveNext();
		}
	}

	private static readonly Dictionary<string, bool> LoadSoundboardSounds_StateMachine45_Lookup_09 = new Dictionary<string, bool>();

	private static readonly Dictionary<string, Coroutine> LoadSoundboardSounds_StateMachine45_Lookup_02 = new Dictionary<string, Coroutine>();

	private static readonly Dictionary<string, AudioSource> LoadSoundboardSounds_StateMachine45_Lookup_06 = new Dictionary<string, AudioSource>();

	private static readonly Dictionary<string, AudioClip> LoadSoundboardSounds_StateMachine45_Lookup_05 = new Dictionary<string, AudioClip>();

	private static readonly Dictionary<string, string> LoadSoundboardSounds_StateMachine45_Lookup_03 = new Dictionary<string, string>
	{
		{ "SFX", "https://github.com/NuggetGT/NXO-Resources/raw/refs/heads/main/Sounds/SoundEffects/SoundEffectsDownloadPath.txt" },
		{ "Trolling", "https://github.com/NuggetGT/NXO-Resources/raw/refs/heads/main/Sounds/Trolling/TrollingDownloadPath.txt" },
		{ "Songs", "https://github.com/NuggetGT/NXO-Resources/raw/refs/heads/main/Sounds/Songs/SongsDownloadPath.txt" }
	};

	public static Dictionary<string, string> LoadSoundboardSounds_StateMachine45_Lookup_01 = new Dictionary<string, string>();

	public static Dictionary<string, string> LoadSoundboardSounds_StateMachine45_Lookup_07 = new Dictionary<string, string>();

	public static Dictionary<string, string> LoadSoundboardSounds_StateMachine45_Lookup_08 = new Dictionary<string, string>();

	public static Dictionary<string, string> LoadSoundboardSounds_StateMachine45_Lookup_04 = new Dictionary<string, string>();

	private static bool LoadSoundboardSounds_StateMachine45_State_03;

	private static AudioSource LoadSoundboardSounds_StateMachine45_Audio_01;

	private static string LoadSoundboardSounds_StateMachine45_Text_02;

	private static bool LoadSoundboardSounds_StateMachine45_State_02;

	private static readonly string LoadSoundboardSounds_StateMachine45_Path_02 = Path.Combine(Application.dataPath, "NXO Mod Menu");

	private static readonly string LoadSoundboardSounds_StateMachine45_Path_01 = Path.Combine(LoadSoundboardSounds_StateMachine45_Path_02, "SoundBoardSounds");

	public static string LoadSoundboardSounds_StateMachine45_Text_04;

	public static bool LoadSoundboardSounds_StateMachine45_State_01 = false;

	public static List<string> LoadSoundboardSounds_StateMachine45_Text_03;

	private static GameObject LoadSoundboardSounds_StateMachine45_Object_01;

	private static List<string> LoadSoundboardSounds_StateMachine45_Items_01 = new List<string>();

	private static string LoadSoundboardSounds_StateMachine45_Text_01 = string.Empty;

	public static Dictionary<string, bool> SoundboardSoundsActive
	{
		get
		{
			return LoadSoundboardSounds_StateMachine45_Lookup_09;
		}
	}

	public static bool SoundsInitialized
	{
		get;
		private set;
	}

	public static GameObject SoundSRC
	{
		get
		{
			if (!((Object)(object)LoadSoundboardSounds_StateMachine45_Object_01 != (Object)null))
			{
				GameObject val = new GameObject("SoundSource");
				LoadSoundboardSounds_StateMachine45_Object_01 = val;
				return val;
			}
			return LoadSoundboardSounds_StateMachine45_Object_01;
		}
	}

	private static void MarkPlaybackStopped(string pathOrUrl)
	{
		LoadSoundboardSounds_StateMachine45_Text_01 = null;
		LoadSoundboardSounds_StateMachine45_Lookup_09[pathOrUrl] = false;
		LoadSoundboardSounds_StateMachine45_State_02 = false;
	}

	public static void DisableAllSounds(string pathOrUrl)
	{
		Recorder primaryRecorder = ((VoiceConnection)PhotonVoiceNetwork.Instance).PrimaryRecorder;
		primaryRecorder.AudioClip = null;
		primaryRecorder.SourceType = (InputSourceType)0;
		primaryRecorder.RestartRecording(true);
		AudioSource component = SoundSRC.GetComponent<AudioSource>();
		Coroutine value;
		if ((Object)(object)component != (Object)null)
		{
			component.Stop();
			LoadSoundboardSounds_StateMachine45_Lookup_09[pathOrUrl] = false;
			if (LoadSoundboardSounds_StateMachine45_Lookup_02.TryGetValue(pathOrUrl, out value))
			{
				goto Branch_00a6;
			}
		}
		else
		{
			LoadSoundboardSounds_StateMachine45_Lookup_09[pathOrUrl] = false;
			if (LoadSoundboardSounds_StateMachine45_Lookup_02.TryGetValue(pathOrUrl, out value))
			{
				goto Branch_00a6;
			}
		}
		goto Branch_0100;
		Branch_01d0:
		if (!LoadSoundboardSounds_StateMachine45_Items_01.Contains(LoadSoundboardSounds_StateMachine45_Text_01))
		{
			goto Branch_021d;
		}
		goto Branch_01f3;
		Branch_01a1:
		LoadSoundboardSounds_StateMachine45_Items_01.Remove(pathOrUrl);
		if (!LoadSoundboardSounds_StateMachine45_Items_01.Contains(LoadSoundboardSounds_StateMachine45_Text_01))
		{
			goto Branch_021d;
		}
		goto Branch_01f3;
		Branch_00a6:
		if (value == null)
		{
			goto Branch_0100;
		}
		((MonoBehaviour)CoroutineHelper.Instance).StopCoroutine(value);
		LoadSoundboardSounds_StateMachine45_Lookup_02.Remove(pathOrUrl);
		if (!LoadSoundboardSounds_StateMachine45_Lookup_06.TryGetValue(pathOrUrl, out AudioSource value2))
		{
			goto Branch_0182;
		}
		goto Branch_0121;
		Branch_01f3:
		LoadSoundboardSounds_StateMachine45_Items_01.Remove(LoadSoundboardSounds_StateMachine45_Text_01);
		LoadSoundboardSounds_StateMachine45_Text_01 = string.Empty;
		LoadSoundboardSounds_StateMachine45_State_01 = false;
		return;
		Branch_0100:
		if (!LoadSoundboardSounds_StateMachine45_Lookup_06.TryGetValue(pathOrUrl, out value2))
		{
			goto Branch_0182;
		}
		Branch_0121:
		if (!((Object)(object)value2 != (Object)null))
		{
			goto Branch_0182;
		}
		value2.Stop();
		Object.Destroy((Object)(object)((Component)value2).gameObject);
		LoadSoundboardSounds_StateMachine45_Lookup_06.Remove(pathOrUrl);
		if (!LoadSoundboardSounds_StateMachine45_Items_01.Contains(pathOrUrl))
		{
			goto Branch_01d0;
		}
		goto Branch_01a1;
		Branch_021d:
		LoadSoundboardSounds_StateMachine45_Text_01 = string.Empty;
		LoadSoundboardSounds_StateMachine45_State_01 = false;
		return;
		Branch_0182:
		if (!LoadSoundboardSounds_StateMachine45_Items_01.Contains(pathOrUrl))
		{
			goto Branch_01d0;
		}
		goto Branch_01a1;
	}

	[IteratorStateMachine(typeof(DownloadAndPlaySound_StateMachine39))]
	public static IEnumerator DownloadAndPlaySoundCoroutine(string pathOrUrl)
	{
		return new DownloadAndPlaySound_StateMachine39(0)
		{
			pathOrUrl = pathOrUrl
		};
	}

	private static void StartAudioSourcePlayback(AudioSource source, string pathOrUrl)
	{
		if ((Object)(object)source == (Object)null)
		{
			Debug.LogError((object)"AudioSource is null.");
			return;
		}
		source.Play();
		((Component)source).transform.parent = ((Component)Variables.Variables_Reference_06).transform;
		ConfigureVoiceRecorder(source);
		if (LoadSoundboardSounds_StateMachine45_State_03)
		{
			Coroutine value = ((MonoBehaviour)CoroutineHelper.Instance).StartCoroutine(LoopAudioSource(source, pathOrUrl));
			LoadSoundboardSounds_StateMachine45_Lookup_02[pathOrUrl] = value;
		}
	}

	public static IEnumerator LoopAudioSource(AudioSource source, string pathOrUrl)
	{
		if (!source.isPlaying)
		{
			source.Play();
		}
		if (LoadSoundboardSounds_StateMachine45_State_03)
		{
			while (LoadSoundboardSounds_StateMachine45_Lookup_09.ContainsKey(pathOrUrl) && LoadSoundboardSounds_StateMachine45_Lookup_09[pathOrUrl])
			{
				yield return (object)new WaitForSeconds(source.clip.length);
				if (LoadSoundboardSounds_StateMachine45_State_03)
				{
					source.Play();
					if (!LoadSoundboardSounds_StateMachine45_State_03)
					{
						break;
					}
				}
				else if (!LoadSoundboardSounds_StateMachine45_State_03)
				{
					break;
				}
			}
		}
		DisableAllSounds(pathOrUrl);
	}

	public static IEnumerator BuildSoundboardButtonsCoroutine()
	{
		List<ButtonHandler.Button> buttonList = new List<ButtonHandler.Button>(ModButtons.buttons);
		yield return LoadAllSoundsCoroutine();
		buttonList.Add(new ButtonHandler.Button("Custom Sounds", Category.Soundboard, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.NavigateToCategory(Category.Custom_Sounds);
		})
		{
			isCategory = true
		});
		buttonList.Add(new ButtonHandler.Button("SFX", Category.Soundboard, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.NavigateToCategory(Category.SFX);
		})
		{
			isCategory = true
		});
		buttonList.Add(new ButtonHandler.Button("Trolling", Category.Soundboard, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.NavigateToCategory(Category.Trolling);
		})
		{
			isCategory = true
		});
		buttonList.Add(new ButtonHandler.Button("Songs", Category.Soundboard, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.NavigateToCategory(Category.Songs);
		})
		{
			isCategory = true
		});
		AddSoundButtons(buttonList, LoadSoundboardSounds_StateMachine45_Lookup_01, Category.SFX);
		AddSoundButtons(buttonList, LoadSoundboardSounds_StateMachine45_Lookup_07, Category.Trolling);
		AddSoundButtons(buttonList, LoadSoundboardSounds_StateMachine45_Lookup_08, Category.Songs);
		buttonList.Add(new ButtonHandler.Button("Return", Category.Custom_Sounds, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.NavigateToCategory(Category.Soundboard);
		})
		{
			isCategory = true
		});
		buttonList.Add(new ButtonHandler.Button("Open Custom Sounds Folder", Category.Custom_Sounds, isToggle: false, isActive: false, delegate
		{
			ButtonHandler.OpenFolder(Path.Combine(LoadSoundboardSounds_StateMachine45_Path_02, "Custom_Sounds"));
		}));
		buttonList.Add(new ButtonHandler.Button("Reload Custom Sounds", Category.Custom_Sounds, isToggle: false, isActive: false, ReloadCustomSounds));
		AddSoundButtons(buttonList, LoadSoundboardSounds_StateMachine45_Lookup_04, Category.Custom_Sounds);
		ModButtons.buttons = buttonList.ToArray();
	}

	public static void LoopSounds()
	{
		LoadSoundboardSounds_StateMachine45_State_03 = true;
	}

	private static void ConfigureVoiceRecorder(AudioSource source)
	{
		Recorder primaryRecorder = ((VoiceConnection)PhotonVoiceNetwork.Instance).PrimaryRecorder;
		primaryRecorder.SourceType = (InputSourceType)1;
		primaryRecorder.AudioClip = source.clip;
		primaryRecorder.RestartRecording(true);
		primaryRecorder.LoopAudioClip = LoadSoundboardSounds_StateMachine45_State_03;
	}

	public static void StopAllSounds()
	{
		using (List<string>.Enumerator enumerator = LoadSoundboardSounds_StateMachine45_Lookup_09.Keys.ToList().GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				while (true)
				{
					string current = enumerator.Current;
					if (LoadSoundboardSounds_StateMachine45_Lookup_09[current])
					{
						DisableAllSounds(current);
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
		using (List<KeyValuePair<string, AudioSource>>.Enumerator enumerator2 = LoadSoundboardSounds_StateMachine45_Lookup_06.ToList().GetEnumerator())
		{
			if (enumerator2.MoveNext())
			{
				do
				{
					Branch_00d9:
					KeyValuePair<string, AudioSource> current2 = enumerator2.Current;
					if ((Object)(object)current2.Value != (Object)null)
					{
						current2.Value.Stop();
						if ((Object)(object)current2.Value.clip != (Object)null)
						{
							Object.Destroy((Object)(object)current2.Value.clip);
							if ((Object)(object)((Component)current2.Value).gameObject != (Object)null)
							{
								goto Branch_0191;
							}
						}
						else if ((Object)(object)((Component)current2.Value).gameObject != (Object)null)
						{
							goto Branch_0191;
						}
						if (!enumerator2.MoveNext())
						{
							break;
						}
					}
					else if (!enumerator2.MoveNext())
					{
						break;
					}
					goto Branch_00d9;
					Branch_0191:
					Object.Destroy((Object)(object)((Component)current2.Value).gameObject);
				}
				while (enumerator2.MoveNext());
			}
		}
		LoadSoundboardSounds_StateMachine45_Lookup_06.Clear();
		LoadSoundboardSounds_StateMachine45_Lookup_05.Clear();
		LoadSoundboardSounds_StateMachine45_Lookup_09.Clear();
		LoadSoundboardSounds_StateMachine45_Lookup_02.Clear();
		LoadSoundboardSounds_StateMachine45_Audio_01 = null;
		LoadSoundboardSounds_StateMachine45_Text_02 = null;
		LoadSoundboardSounds_StateMachine45_State_02 = false;
	}

	public static void ResetLoopSounds()
	{
		LoadSoundboardSounds_StateMachine45_State_03 = false;
	}

	private static IEnumerator PlayClipCoroutine(AudioClip clip)
	{
		if (!LoadSoundboardSounds_StateMachine45_State_01)
		{
			LoadSoundboardSounds_StateMachine45_State_01 = true;
			SoundSRC.transform.position = ((Component)GorillaTagger.Instance.bodyCollider).transform.position;
			AudioSource src = SoundSRC.GetComponent<AudioSource>();
			src.clip = clip;
			src.loop = LoadSoundboardSounds_StateMachine45_State_03;
			Recorder recorder = ((VoiceConnection)PhotonVoiceNetwork.Instance).PrimaryRecorder;
			recorder.StopRecording();
			recorder.SourceType = (InputSourceType)1;
			recorder.AudioClip = clip;
			recorder.LoopAudioClip = LoadSoundboardSounds_StateMachine45_State_03;
			src.Play();
			recorder.StartRecording();
			yield return (object)new WaitForSeconds(clip.length);
			recorder.SourceType = (InputSourceType)0;
			recorder.RestartRecording(true);
			LoadSoundboardSounds_StateMachine45_State_01 = false;
			src.Stop();
			LoadSoundboardSounds_StateMachine45_Items_01.Remove(LoadSoundboardSounds_StateMachine45_Text_01);
			LoadSoundboardSounds_StateMachine45_Text_01 = string.Empty;
		}
	}

	[AsyncStateMachine(typeof(LoadAllSoundboardSounds_StateMachine43))]
	[DebuggerStepThrough]
	public static Task LoadAllSoundboardSounds()
	{
		LoadAllSoundboardSounds_StateMachine43 stateMachine = new LoadAllSoundboardSounds_StateMachine43();
		stateMachine.Builder = AsyncTaskMethodBuilder.Create();
		stateMachine.State = -1;
		stateMachine.Builder.Start(ref stateMachine);
		return stateMachine.Builder.Task;
	}

	private static void EnsureCustomSoundsInstructions()
	{
		try
		{
			string path = Path.Combine(LoadSoundboardSounds_StateMachine45_Path_02, "Custom_Sounds", "Instructions.txt");
			if (!Directory.Exists(Path.Combine(LoadSoundboardSounds_StateMachine45_Path_02, "Custom_Sounds")))
			{
				Directory.CreateDirectory(Path.Combine(LoadSoundboardSounds_StateMachine45_Path_02, "Custom_Sounds"));
				if (File.Exists(path))
				{
					return;
				}
			}
			else if (File.Exists(path))
			{
				return;
			}
			File.WriteAllText(path, "=== HOW TO ADD CUSTOM SOUNDS ===\r\n\r\n1. Find an audio file (.mp3, .wav, or .ogg)\r\n   - You can download sounds from YouTube using online converters\r\n   - Make sure it's one of these file types: MP3, WAV, or OGG\r\n\r\n2. Copy your audio file into this folder (Custom_Sounds)\r\n   - The file name will be the button name in the menu\r\n   - Example: \"explosion.mp3\" will show as \"explosion\" in the menu\r\n\r\n3. In-game, go to Soundboard > Custom Sounds > Reload Custom Sounds\r\n   - This will refresh the list and add your new sounds\r\n\r\n4. Click any sound button to play it in-game\r\n   - Other players will hear it through your mic\r\n   - Press the button again to stop the sound\r\n\r\nTIPS:\r\n- Keep file names short and simple\r\n- Don't use special characters in file names\r\n- Sounds can be any length\r\n- You can add as many sounds as you want\r\n\r\nThat's it! Have fun!");
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("Error creating instructions file: " + ex.Message));
		}
	}

	public static void ResetVoiceRecorder()
	{
		Recorder primaryRecorder = ((VoiceConnection)PhotonVoiceNetwork.Instance).PrimaryRecorder;
		primaryRecorder.SourceType = (InputSourceType)0;
		primaryRecorder.AudioClip = null;
		primaryRecorder.RestartRecording(true);
		primaryRecorder.LoopAudioClip = false;
		LoadSoundboardSounds_StateMachine45_Text_02 = null;
		LoadSoundboardSounds_StateMachine45_State_02 = false;
	}

	public static IEnumerator LoadAllSoundsCoroutine()
	{
		Task task = LoadAllSoundboardSounds();
		if (!task.IsCompleted)
		{
			do
			{
				yield return null;
			}
			while (!task.IsCompleted);
		}
	}

	[AsyncStateMachine(typeof(GetRawFrom_StateMachine46))]
	[DebuggerStepThrough]
	public static Task<string> GetRawTextAsync(string url)
	{
		GetRawFrom_StateMachine46 stateMachine = new GetRawFrom_StateMachine46();
		stateMachine.Builder = AsyncTaskMethodBuilder<string>.Create();
		stateMachine.url = url;
		stateMachine.State = -1;
		stateMachine.Builder.Start(ref stateMachine);
		return stateMachine.Builder.Task;
	}

	[AsyncStateMachine(typeof(LoadSoundboardSounds_StateMachine45))]
	[DebuggerStepThrough]
	public static Task LoadSoundboardSounds(string url, string category)
	{
		LoadSoundboardSounds_StateMachine45 stateMachine = new LoadSoundboardSounds_StateMachine45();
		stateMachine.Builder = AsyncTaskMethodBuilder.Create();
		stateMachine.url = url;
		stateMachine.category = category;
		stateMachine.State = -1;
		stateMachine.Builder.Start(ref stateMachine);
		return stateMachine.Builder.Task;
	}

	private static AudioType GetAudioType(string path)
	{
		return (AudioType)(Path.GetExtension(path).ToLower() switch
		{
			".mp3" => 13, 
			".wav" => 20, 
			".ogg" => 14, 
			_ => 13, 
		});
	}

	private static void AddSoundButtons(List<ButtonHandler.Button> buttonList, Dictionary<string, string> sounds, Category category)
	{
		using Dictionary<string, string>.Enumerator enumerator = sounds.GetEnumerator();
		if (!enumerator.MoveNext())
		{
			return;
		}
		do
		{
			KeyValuePair<string, string> current = enumerator.Current;
			CapturedVariables370 LocalScope3 = new CapturedVariables370();
			LocalScope3.pathOrUrl = current.Value;
			buttonList.Add(new ButtonHandler.Button(current.Key, category, isToggle: true, isActive: false, delegate
			{
				PlaySound(LocalScope3.pathOrUrl);
			}, delegate
			{
				DisableAllSounds(LocalScope3.pathOrUrl);
			}));
		}
		while (enumerator.MoveNext());
	}

	public static void LoadSoundboard()
	{
		if (!SoundsInitialized)
		{
			if ((Object)(object)CoroutineHelper.Instance == (Object)null)
			{
				Debug.LogError((object)"CoroutineHelper.Instance is null. Make sure CoroutineHelper is initialized in the scene.");
				NotificationLib.ShowNotification(NotificationLib.NotificationType.Error, "Coroutine Helper Not Initialized");
				return;
			}
			SoundsInitialized = true;
			Debug.Log((object)"Sounds Initialized");
			((MonoBehaviour)CoroutineHelper.Instance).StartCoroutine(BuildSoundboardButtonsCoroutine());
			CoroutineHelper.InvokeAfterDelay(2f, Main.RebuildMenu);
			EnsureCustomSoundsInstructions();
		}
	}

	public static void LoadCustom_Sounds()
	{
		try
		{
			string customSoundsPath = Path.Combine(LoadSoundboardSounds_StateMachine45_Path_02, "Custom_Sounds");
			if (!Directory.Exists(customSoundsPath))
			{
				Directory.CreateDirectory(customSoundsPath);
				Debug.Log((object)("Created Custom_Sounds folder at: " + customSoundsPath));
				NotificationLib.ShowNotification(NotificationLib.NotificationType.Saved, "Custom Sounds Folder Created");
				return;
			}
			string[] array = (from file in Directory.GetFiles(customSoundsPath, "*.*")
				where file.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase) || file.EndsWith(".wav", StringComparison.OrdinalIgnoreCase) || file.EndsWith(".ogg", StringComparison.OrdinalIgnoreCase)
				select file).ToArray();
			int num = 0;
			foreach (string value in array)
			{
				string soundName = Path.GetFileNameWithoutExtension(value);
				if (!string.IsNullOrEmpty(soundName))
				{
					if (!LoadSoundboardSounds_StateMachine45_Lookup_04.ContainsKey(soundName))
					{
						LoadSoundboardSounds_StateMachine45_Lookup_04[soundName] = value;
						num++;
						continue;
					}
					Debug.LogWarning((object)("Duplicate custom sound: " + soundName));
				}
			}
			if (num > 0)
			{
				Debug.Log((object)$"Loaded {num} custom sounds from folder");
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)("Error loading custom sounds: " + ex.Message));
			NotificationLib.ShowNotification(NotificationLib.NotificationType.Error, $"Custom Sounds Cannot Load `{ex}`");
		}
	}

	public static void PlaySound(string soundName)
	{
		AudioSource val = SoundSRC.GetComponent<AudioSource>() ?? SoundSRC.AddComponent<AudioSource>();
		if ((Object)(object)val == (Object)null)
		{
			return;
		}
		if ((Object)(object)((Component)val).GetComponent<Renderer>() != (Object)null)
		{
			Object.Destroy((Object)(object)((Component)val).GetComponent<Renderer>());
			if ((Object)(object)((Component)val).GetComponent<Collider>() != (Object)null)
			{
				goto Branch_00ac;
			}
		}
		else if ((Object)(object)((Component)val).GetComponent<Collider>() != (Object)null)
		{
			goto Branch_00ac;
		}
		if (!LoadSoundboardSounds_StateMachine45_Items_01.Contains(soundName))
		{
			goto Branch_00ff;
		}
		return;
		Branch_00ff:
		string customSoundPath = Path.Combine(LoadSoundboardSounds_StateMachine45_Path_02, "Custom_Sounds", soundName);
		if (!File.Exists(customSoundPath))
		{
			LoadSoundboardSounds_StateMachine45_Items_01.Add(soundName);
			LoadSoundboardSounds_StateMachine45_Text_01 = soundName;
			LoadSoundboardSounds_StateMachine45_Lookup_09[soundName] = true;
			((MonoBehaviour)CoroutineHelper.Instance).StartCoroutine(DownloadAndPlaySoundCoroutine(soundName));
			return;
		}
		AudioType val2 = GetAudioType(Path.GetExtension(soundName).ToLowerInvariant());
		if ((int)val2 != 0)
		{
			LoadSoundboardSounds_StateMachine45_Items_01.Add(soundName);
			LoadSoundboardSounds_StateMachine45_Text_01 = soundName;
			((MonoBehaviour)CoroutineHelper.Instance).StartCoroutine(LoadAndPlay(customSoundPath, soundName, val2));
		}
		return;
		Branch_00ac:
		Object.Destroy((Object)(object)((Component)val).GetComponent<Collider>());
		if (!LoadSoundboardSounds_StateMachine45_Items_01.Contains(soundName))
		{
			goto Branch_00ff;
		}
	}

	public static void ReloadCustomSounds()
	{
		LoadSoundboardSounds_StateMachine45_Lookup_04.Clear();
		LoadCustom_Sounds();
		List<ButtonHandler.Button> list = new List<ButtonHandler.Button>();
		ButtonHandler.Button[] array = ModButtons.buttons;
		int num = 0;
		while (num < array.Length)
		{
			ButtonHandler.Button button = array[num];
			if (array[num].Page != Category.Custom_Sounds || !button.isToggle)
			{
				list.Add(button);
				num++;
			}
			else
			{
				num++;
			}
		}
		AddSoundButtons(list, LoadSoundboardSounds_StateMachine45_Lookup_04, Category.Custom_Sounds);
		ModButtons.buttons = list.ToArray();
		Main.RebuildMenu();
		int count = LoadSoundboardSounds_StateMachine45_Lookup_04.Count;
		NotificationLib.ShowNotification(NotificationLib.NotificationType.Loaded, string.Format("Custom Sounds Reloaded `{0}` `{1}`", count, (count != 1) ? "s" : ""));
	}

	[IteratorStateMachine(typeof(LoadAndPlay_StateMachine32))]
	private static IEnumerator LoadAndPlay(string fullPath, string soundName, AudioType audioType)
	{
		return new LoadAndPlay_StateMachine32(0)
		{
			fullPath = fullPath,
			soundName = soundName,
			audioType = audioType
		};
	}
}
