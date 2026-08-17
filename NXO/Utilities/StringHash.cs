namespace NXO.Utilities;

internal static class StringHash
{
	internal static uint Compute(string value)
	{
		unchecked
		{
			uint hash = 2166136261u;
			if (value != null)
			{
				foreach (char character in value)
				{
					hash = (hash ^ character) * 16777619u;
				}
			}
			return hash;
		}
	}
}
