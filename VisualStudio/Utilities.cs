using Il2CppTLD.Gear;
using UnityEngine.AddressableAssets;

namespace AutumnComforts
{
    internal static class Utilities
    {
        public static HeadacheData HeadacheData = Addressables.LoadAssetAsync<GameObject>("GEAR_CookedPieRoseHip").WaitForCompletion().gameObject.GetComponent<CausesHeadacheDebuff>().m_HeadacheSettings;
        public static GameObject MeatMat = Addressables.LoadAssetAsync<GameObject>("GEAR_UncookedBannock").WaitForCompletion();
        public static GameObject Liquid = Addressables.LoadAssetAsync<GameObject>("GEAR_CoffeeCup").WaitForCompletion();

        public static Mesh LoadMesh(string name)
        {
            return Main.PumpkinAssetBundle.LoadAsset<Mesh>(name);
        }

        public static Texture2D Texture(string textureName)
        {
            return Main.PumpkinAssetBundle.LoadAsset<Texture2D>(textureName);
        }

        public static Material LiquidMaterial(string textureName)
        {
            Material liquid = new Material(Liquid.gameObject.GetComponent<Cookable>().m_CookingPotMaterialsList[0]);
            liquid.SetTexture("_Main_texture2", Texture(textureName));
            return liquid;
        }

        public static Material MeatMaterial(string textureName)
        {
            Material meat = new Material(MeatMat.gameObject.GetComponent<Cookable>().m_CookingPotMaterialsList[0]);
            meat.SetTexture("_MainTex", Texture(textureName));
            return meat;
        }

        public static Material BurntMaterial() => MeatMaterial("CKN_Burnt_PumpkinPie");
        public static Material UncookedPumpkinPieMaterial() => MeatMaterial("CKN_Tex_UncookedPumpkinPie");
        public static Material CookedPumpkinPieMaterial() => MeatMaterial("CKN_Tex_CookedPumpkinPie");
        public static Material CookedPumpkinSeedsMaterial() => MeatMaterial("CKN_Tex_CookedPumpkinSeeds");
        public static Material SoupCookedLiquidMaterial() => LiquidMaterial("T_Soup_Cooking");
        public static Material SoupRawLiquidMaterial() => LiquidMaterial("T_Soup_Raw");
        public static Material CoffeeLiquidMaterial() => LiquidMaterial("T_Coffee_CKN");

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
        // ----------------------------------------CookingTextures----------------------------------------//

        [HarmonyPatch(typeof(GearItem), nameof(GearItem.Awake))]
        internal class CookingPotMaterials
        {
            private static void Postfix(GearItem __instance)
            {
                if (__instance == null) return;

                Cookable cookable = __instance.gameObject.GetComponent<Cookable>();
                if (cookable == null) return;

                string name = __instance.name;

                if (name.Contains("GEAR_UncookedPumpkinPie"))
                {
                    cookable.m_CanBePickedUpWhileCooking = false;
                    cookable.m_CookingPotRawMaterialsList = new Material[1] { UncookedPumpkinPieMaterial() };
                    cookable.m_CookingPotMaterialsList = new Material[1] { CookedPumpkinPieMaterial() };
                    cookable.m_RuinedMaterials = new Material[1] { BurntMaterial() };
                    cookable.m_MeshFryingPanStyle = LoadMesh("OBJ_CookedPumpkinPie");
                    cookable.m_MeshRawFryingPanStyle = LoadMesh("OBJ_UncookedPumpkinPie");
                }

                if (name.Contains("GEAR_UncookedPumpkinSeeds"))
                {
                    cookable.m_CanBePickedUpWhileCooking = false;
                    cookable.m_CookingPotRawMaterialsList = new Material[1] { UncookedPumpkinPieMaterial() };
                    cookable.m_CookingPotMaterialsList = new Material[1] { CookedPumpkinSeedsMaterial() };
                    cookable.m_RuinedMaterials = new Material[1] { BurntMaterial() };
                    cookable.m_MeshFryingPanStyle = LoadMesh("OBJ_PumpkinSeeds");
                    cookable.m_MeshRawFryingPanStyle = LoadMesh("OBJ_PumpkinSeeds");
                }

                if (name.Contains("GEAR_UncookedPumpkinSoup"))
                {
                    cookable.m_CanBePickedUpWhileCooking = false;
                    cookable.m_CookingPotRawMaterialsList = new Material[1] { SoupRawLiquidMaterial() };
                    cookable.m_CookingPotMaterialsList = new Material[1] { SoupCookedLiquidMaterial() };
                    cookable.m_RuinedMaterials = new Material[1] { BurntMaterial() };
                }

                if (name.Contains("GEAR_CookedPumpkinSoup"))
                {
                    cookable.m_CookingPotMaterialsList = new Material[1] { SoupCookedLiquidMaterial() };
                    cookable.m_CookingPotRawMaterialsList = new Material[1] { SoupCookedLiquidMaterial() };
                    cookable.m_RuinedMaterials = new Material[1] { BurntMaterial() };
                }

                if (name.ToLowerInvariant().Contains("pumpkinspicelatte"))
                {
                    cookable.m_CanBePickedUpWhileCooking = false;
                    cookable.m_CookingPotMaterialsList = new Material[1] { CoffeeLiquidMaterial() };
                    cookable.m_RuinedMaterials = new Material[1] { BurntMaterial() };
                }
            }
        }
        // ----------------------------------------CookingTextures END----------------------------------------//
    }
}
