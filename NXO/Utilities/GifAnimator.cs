using UnityEngine;

namespace NXO.Utilities;

public class GifAnimator : MonoBehaviour
{
	public GifDecoder.Frame[] frames;

	public Material mat;

	private int _i;

	private float _t;

	private void Update()
	{
		if (frames != null && frames.Length > 1 && !((Object)(object)mat == (Object)null))
		{
			_t += Time.deltaTime;
			if (!(_t < frames[_i].delay))
			{
				_t = 0f;
				_i = (_i + 1) % frames.Length;
				mat.mainTexture = (Texture)(object)frames[_i].texture;
			}
		}
	}

	private void OnDestroy()
	{
		if (frames != null)
		{
			GifDecoder.Frame[] array = frames;
			int num = 0;
			while (num < array.Length)
			{
				GifDecoder.Frame frame = array[num];
				if ((Object)(object)frame.texture != (Object)null)
				{
					Object.Destroy((Object)(object)frame.texture);
					num++;
				}
				else
				{
					num++;
				}
			}
		}
		if ((Object)(object)mat != (Object)null)
		{
			Object.Destroy((Object)(object)mat);
		}
	}
}
