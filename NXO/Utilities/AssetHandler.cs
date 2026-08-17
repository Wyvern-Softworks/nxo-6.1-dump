using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;

namespace NXO.Utilities;

public static class AssetHandler
{
	public static readonly Dictionary<string, AssetBundle> AssetBundleCache = new Dictionary<string, AssetBundle>();

	private static readonly Dictionary<string, Material> MaterialCache = new Dictionary<string, Material>();

	private static readonly Dictionary<string, Texture2D> TextureCache = new Dictionary<string, Texture2D>();

	public static void SetMaterialProperty(Material material, string property, object value)
	{
		if (material == null || !material.HasProperty(property))
		{
			return;
		}

		switch (value)
		{
		case float floatValue:
			material.SetFloat(property, floatValue);
			break;
		case Color colorValue:
			material.SetColor(property, colorValue);
			break;
		case int intValue:
			material.SetInt(property, intValue);
			break;
		case Texture textureValue:
			material.SetTexture(property, textureValue);
			break;
		}
	}

	public static IEnumerator LoadEmbeddedAudioClip(string resourceName, Action<AudioClip> callback)
	{
		AudioType audioType = GetAudioType(resourceName);
		if (audioType == AudioType.UNKNOWN)
		{
			Debug.LogWarning("[NXO] Unsupported audio format: " + Path.GetExtension(resourceName));
			callback?.Invoke(null);
			yield break;
		}

		string tempPath = Path.Combine(
			Application.temporaryCachePath,
			$"{Guid.NewGuid()}_{Path.GetFileName(resourceName)}");

		try
		{
			using (Stream resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
			{
				if (resourceStream == null)
				{
					Debug.LogWarning("[NXO] Could not find embedded resource: " + resourceName);
					callback?.Invoke(null);
					yield break;
				}

				try
				{
					using (FileStream fileStream = new FileStream(tempPath, FileMode.Create, FileAccess.Write, FileShare.None))
					{
						resourceStream.CopyTo(fileStream);
					}
				}
				catch (Exception exception)
				{
					Debug.LogWarning("[NXO] Failed to write temp audio file: " + exception.Message);
					callback?.Invoke(null);
					yield break;
				}
			}

			UnityWebRequest request;
			try
			{
				request = UnityWebRequestMultimedia.GetAudioClip("file://" + tempPath, audioType);
				((DownloadHandlerAudioClip)request.downloadHandler).streamAudio = true;
			}
			catch (Exception exception)
			{
				Debug.LogWarning("[NXO] Failed to create web request: " + exception.Message);
				callback?.Invoke(null);
				yield break;
			}

			using (request)
			{
				yield return request.SendWebRequest();

				if (request.result != UnityWebRequest.Result.Success)
				{
					Debug.LogWarning($"[NXO] Failed to load audio clip '{resourceName}': {request.error}");
					callback?.Invoke(null);
					yield break;
				}

				AudioClip clip = DownloadHandlerAudioClip.GetContent(request);
				if (clip == null)
				{
					callback?.Invoke(null);
					yield break;
				}

				if (clip.loadState != AudioDataLoadState.Loaded)
				{
					Debug.LogWarning("[NXO] Clip loaded but data not ready: " + resourceName);
					UnityEngine.Object.Destroy(clip);
					callback?.Invoke(null);
					yield break;
				}

				clip.name = Path.GetFileNameWithoutExtension(resourceName);
				callback?.Invoke(clip);
			}
		}
		finally
		{
			DeleteTempFile(tempPath);
		}
	}

	public static Texture2D LoadEmbeddedTexture(string fileName)
	{
		if (TextureCache.TryGetValue(fileName, out Texture2D cachedTexture))
		{
			return cachedTexture;
		}

		using (Stream resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fileName))
		{
			if (resourceStream == null)
			{
				return null;
			}

			byte[] imageData;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				resourceStream.CopyTo(memoryStream);
				imageData = memoryStream.ToArray();
			}

			Texture2D texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
			if (!ImageConversion.LoadImage(texture, imageData))
			{
				UnityEngine.Object.Destroy(texture);
				return null;
			}

			TextureCache[fileName] = texture;
			return texture;
		}
	}

	public static IEnumerator LoadTextureFromUrl(GameObject target, string url)
	{
		if (target == null || string.IsNullOrEmpty(url))
		{
			yield break;
		}

		using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
		{
			yield return request.SendWebRequest();

			if (request.result != UnityWebRequest.Result.Success || target == null)
			{
				yield break;
			}

			Renderer renderer = target.GetComponent<Renderer>();
			if (renderer == null)
			{
				yield break;
			}

			Texture2D texture = DownloadHandlerTexture.GetContent(request);
			if (renderer.material != null)
			{
				UnityEngine.Object.Destroy(renderer.material);
			}

			renderer.material = new Material(Variables.Variables_Reference_11)
			{
				mainTexture = texture
			};
		}
	}

	public static void ClearCaches()
	{
		foreach (Texture2D texture in TextureCache.Values)
		{
			if (texture != null)
			{
				UnityEngine.Object.Destroy(texture);
			}
		}
		TextureCache.Clear();

		foreach (Material material in MaterialCache.Values)
		{
			if (material != null)
			{
				UnityEngine.Object.Destroy(material);
			}
		}
		MaterialCache.Clear();

		foreach (AssetBundle assetBundle in AssetBundleCache.Values)
		{
			if (assetBundle != null)
			{
				assetBundle.Unload(true);
			}
		}
		AssetBundleCache.Clear();
	}

	public static Material LoadMaterial(string bundleName, string materialName)
	{
		string cacheKey = bundleName + "_" + materialName;
		if (MaterialCache.TryGetValue(cacheKey, out Material cachedMaterial) && cachedMaterial != null)
		{
			return cachedMaterial;
		}

		AssetBundle assetBundle = LoadAssetBundle(bundleName);
		if (assetBundle == null)
		{
			return null;
		}

		Material material = null;
		foreach (string assetName in assetBundle.GetAllAssetNames())
		{
			if (!string.Equals(Path.GetFileNameWithoutExtension(assetName), materialName, StringComparison.OrdinalIgnoreCase))
			{
				continue;
			}

			UnityEngine.Object asset = assetBundle.LoadAsset(assetName);
			if (asset is Material loadedMaterial)
			{
				material = loadedMaterial;
				break;
			}

			if (asset is GameObject gameObject)
			{
				Renderer renderer = gameObject.GetComponent<Renderer>();
				if (renderer != null && renderer.sharedMaterial != null)
				{
					material = renderer.sharedMaterial;
					break;
				}
			}
		}

		if (material == null)
		{
			foreach (string assetName in assetBundle.GetAllAssetNames())
			{
				UnityEngine.Object asset = assetBundle.LoadAsset(assetName);
				if (asset is Material loadedMaterial)
				{
					material = loadedMaterial;
					break;
				}

				if (asset is Shader shader)
				{
					material = new Material(shader);
					break;
				}
			}
		}

		if (material == null)
		{
			Debug.LogWarning($"[NXO] No usable material for '{materialName}' in bundle '{bundleName}'");
			return null;
		}

		MaterialCache[cacheKey] = material;
		return material;
	}

	public static void PlayAudioClip(GameObject target, AudioClip clip, float volume = 1f)
	{
		if (target == null || clip == null)
		{
			return;
		}

		AudioSource audioSource = target.GetComponent<AudioSource>();
		if (audioSource == null)
		{
			audioSource = target.AddComponent<AudioSource>();
			audioSource.hideFlags = HideFlags.HideAndDontSave;
		}
		audioSource.clip = clip;
		audioSource.volume = volume;
		audioSource.loop = false;
		audioSource.Play();
	}

	public static AssetBundle LoadAssetBundle(string resourceName)
	{
		if (AssetBundleCache.TryGetValue(resourceName, out AssetBundle cachedBundle))
		{
			return cachedBundle;
		}

		using (Stream resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
		{
			if (resourceStream == null)
			{
				return null;
			}

			byte[] bundleData;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				resourceStream.CopyTo(memoryStream);
				bundleData = memoryStream.ToArray();
			}

			AssetBundle assetBundle = AssetBundle.LoadFromMemory(bundleData);
			if (assetBundle != null)
			{
				AssetBundleCache[resourceName] = assetBundle;
			}
			return assetBundle;
		}
	}

	private static AudioType GetAudioType(string resourceName)
	{
		switch (Path.GetExtension(resourceName)?.ToLowerInvariant())
		{
		case ".wav":
			return AudioType.WAV;
		case ".mp3":
			return AudioType.MPEG;
		default:
			return AudioType.UNKNOWN;
		}
	}

	private static void DeleteTempFile(string path)
	{
		if (string.IsNullOrEmpty(path) || !File.Exists(path))
		{
			return;
		}

		try
		{
			File.Delete(path);
		}
		catch (Exception exception)
		{
			Debug.LogWarning("[NXO] Failed to delete temp file: " + exception.Message);
		}
	}
}
