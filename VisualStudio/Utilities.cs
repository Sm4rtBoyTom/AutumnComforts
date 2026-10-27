
namespace AutumnComforts
{
    internal static class Utilities
    {
        public static bool IsScenePlayable(string scene)
        {
            return !(string.IsNullOrEmpty(scene) || scene.Contains("MainMenu") || scene == "Boot" || scene == "Empty");
        }

        public static Material BurntMaterial()
        {
            Texture2D MeatTexture = Main.PumpkinAssetBundle.LoadAsset<Texture2D>("CKN_Burnt_PumpkinPie");
            Material MeatMaterial = new Material(GearItem.LoadGearItemPrefab("GEAR_UncookedBannock").gameObject.GetComponent<Cookable>().m_CookingPotMaterialsList[0]);
            MeatMaterial.SetTexture("_MainTex", MeatTexture);
            return MeatMaterial;
        }
        public static Material UncookedPumpkinPieMaterial()
        {
            Texture2D MeatTexture = Main.PumpkinAssetBundle.LoadAsset<Texture2D>("CKN_Tex_UncookedPumpkinPie");
            Material MeatMaterial = new Material(GearItem.LoadGearItemPrefab("GEAR_UncookedBannock").gameObject.GetComponent<Cookable>().m_CookingPotMaterialsList[0]);
            MeatMaterial.SetTexture("_MainTex", MeatTexture);
            return MeatMaterial;
        }
        public static Material CookedPumpkinPieMaterial()
        {
            Texture2D MeatTexture = Main.PumpkinAssetBundle.LoadAsset<Texture2D>("CKN_Tex_CookedPumpkinPie");
            Material MeatMaterial = new Material(GearItem.LoadGearItemPrefab("GEAR_UncookedBannock").gameObject.GetComponent<Cookable>().m_CookingPotMaterialsList[0]);
            MeatMaterial.SetTexture("_MainTex", MeatTexture);
            return MeatMaterial;
        }
        public static Material CookedPumpkinSeedsMaterial()
        {
            Texture2D MeatTexture = Main.PumpkinAssetBundle.LoadAsset<Texture2D>("CKN_Tex_CookedPumpkinSeeds");
            Material MeatMaterial = new Material(GearItem.LoadGearItemPrefab("GEAR_UncookedBannock").gameObject.GetComponent<Cookable>().m_CookingPotMaterialsList[0]);
            MeatMaterial.SetTexture("_MainTex", MeatTexture);
            return MeatMaterial;
        }
        public static Material SoupCookedLiquidMaterial()
        {
            Texture2D LiquidTexture = Main.PumpkinAssetBundle.LoadAsset<Texture2D>("T_Soup_Cooking");
            Material LiquidMaterial = new Material(GearItem.LoadGearItemPrefab("GEAR_CoffeeCup").gameObject.GetComponent<Cookable>().m_CookingPotMaterialsList[0]);
            LiquidMaterial.SetTexture("_Main_texture2", LiquidTexture);
            return LiquidMaterial;
        }
        public static Material SoupRawLiquidMaterial()
        {
            Texture2D LiquidTexture = Main.PumpkinAssetBundle.LoadAsset<Texture2D>("T_Soup_Raw");
            Material LiquidMaterial = new Material(GearItem.LoadGearItemPrefab("GEAR_CoffeeCup").gameObject.GetComponent<Cookable>().m_CookingPotMaterialsList[0]);
            LiquidMaterial.SetTexture("_Main_texture2", LiquidTexture);
            return LiquidMaterial;
        }
        public static Material CoffeeLiquidMaterial()
        {
            Texture2D LiquidTexture = Main.PumpkinAssetBundle.LoadAsset<Texture2D>("T_Coffee_CKN");
            Material LiquidMaterial = new Material(GearItem.LoadGearItemPrefab("GEAR_CoffeeCup").gameObject.GetComponent<Cookable>().m_CookingPotMaterialsList[0]);
            LiquidMaterial.SetTexture("_Main_texture2", LiquidTexture);
            return LiquidMaterial;
        }

        public static Mesh UncookedPumpkinPieMesh()
        {
            return Main.PumpkinAssetBundle.LoadAsset<Mesh>("OBJ_UncookedPumpkinPie");
        }
        public static Mesh CookedPumpkinPieMesh()
        {
            return Main.PumpkinAssetBundle.LoadAsset<Mesh>("OBJ_CookedPumpkinPie");
        }
        public static Mesh UncookedPumpkinSeedsMesh()
        {
            return Main.PumpkinAssetBundle.LoadAsset<Mesh>("OBJ_PumpkinSeeds");
        }

        public static AssetBundle LoadFromStream(string name)
        {
            using (Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name))
            {
                MemoryStream? memory = new((int)stream.Length);
                stream!.CopyTo(memory);

                Il2CppSystem.IO.MemoryStream memoryStream = new Il2CppSystem.IO.MemoryStream(memory.ToArray());

                AssetBundle loadFromMemoryInternal = AssetBundle.LoadFromStream(memoryStream);
                return loadFromMemoryInternal;
            }
        }
    }
}
