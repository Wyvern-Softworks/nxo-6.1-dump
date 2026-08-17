using BepInEx;

namespace NXO.Initialization;

[BepInPlugin("com.nxo.nxomodmenu.org", "NXO", "6.1")]
public class MenuPlugin : BaseUnityPlugin
{
	private void Awake()
	{
		Loader.Load();
	}
}
