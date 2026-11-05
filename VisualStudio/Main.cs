
namespace AutumnComforts
{
	internal class Main : MelonMod
	{
        private static AssetBundle? assetBundle;
        internal static AssetBundle PumpkinAssetBundle
        {
            get => assetBundle ?? throw new System.NullReferenceException(nameof(assetBundle));
        }
        public override void OnInitializeMelon()
        {
            MelonLogger.Msg(System.ConsoleColor.DarkYellow,"Carving Pumpkins...");

            assetBundle = Utilities.LoadFromStream("AutumnComforts.Resources.Assets.pumpkinpieassets");
        }
    }
}
