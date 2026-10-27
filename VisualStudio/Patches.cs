using Il2CppTLD.Gear;
using Il2CppTLD.IntBackedUnit;

namespace AutumnComforts
{
    internal class Patches
    {
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
                    cookable.m_CookingPotRawMaterialsList = new Material[1] { Utilities.UncookedPumpkinPieMaterial() };
                    cookable.m_CookingPotMaterialsList = new Material[1] { Utilities.CookedPumpkinPieMaterial() };
                    cookable.m_RuinedMaterials = new Material[1] { Utilities.BurntMaterial() };
                    cookable.m_MeshFryingPanStyle = Utilities.CookedPumpkinPieMesh();
                    cookable.m_MeshRawFryingPanStyle = Utilities.UncookedPumpkinPieMesh();
                }

                if (name.Contains("GEAR_UncookedPumpkinSeeds"))
                {
                    cookable.m_CanBePickedUpWhileCooking = false;
                    cookable.m_CookingPotRawMaterialsList = new Material[1] { Utilities.UncookedPumpkinPieMaterial() };
                    cookable.m_CookingPotMaterialsList = new Material[1] { Utilities.CookedPumpkinSeedsMaterial() };
                    cookable.m_RuinedMaterials = new Material[1] { Utilities.BurntMaterial() };
                    cookable.m_MeshFryingPanStyle = Utilities.UncookedPumpkinSeedsMesh();
                    cookable.m_MeshRawFryingPanStyle = Utilities.UncookedPumpkinSeedsMesh();
                }

                if (name.Contains("GEAR_UncookedPumpkinSoup"))
                {
                    cookable.m_CanBePickedUpWhileCooking = false;
                    cookable.m_CookingPotRawMaterialsList = new Material[1] { Utilities.SoupRawLiquidMaterial() };
                    cookable.m_CookingPotMaterialsList = new Material[1] { Utilities.SoupCookedLiquidMaterial() };
                    cookable.m_RuinedMaterials = new Material[1] { Utilities.BurntMaterial() };
                }

                if (name.Contains("GEAR_UncookedPumpkinSpiceLatte"))
                {
                    cookable.m_CanBePickedUpWhileCooking = false;
                    cookable.m_CookingPotMaterialsList = new Material[1] { Utilities.CoffeeLiquidMaterial() };
                    cookable.m_RuinedMaterials = new Material[1] { Utilities.BurntMaterial() };
                }
            }
        }
        // ----------------------------------------CookingTextures END----------------------------------------//
        [HarmonyPatch(typeof(GearItem), nameof(GearItem.Awake))]
        internal class FoodBuffs
        {
            private static void Postfix(GearItem __instance)
            {
                if (__instance == null) return;

                string name = __instance.name;

                if (name.Contains("GEAR_CookedPumpkinPie"))
                {
                    FreezingBuff freezing = __instance.gameObject.GetComponent<FreezingBuff>() ?? __instance.gameObject.AddComponent<FreezingBuff>();
                    if (freezing == null) return;

                    FoodStatEffect foodStat = __instance.gameObject.GetComponent<FoodStatEffect>() ?? __instance.gameObject.AddComponent<FoodStatEffect>();
                    if (foodStat == null) return;

                    FoodMaxStatBuff foodMaxStat = __instance.gameObject.GetComponent<FoodMaxStatBuff>() ?? __instance.gameObject.AddComponent<FoodMaxStatBuff>();
                    if (foodMaxStat == null) return;

                    IngestedCarryCapacityBuff carrybuff = __instance.gameObject.GetComponent<IngestedCarryCapacityBuff>() ?? __instance.gameObject.AddComponent<IngestedCarryCapacityBuff>();
                    if (carrybuff == null) return;

                    CabinFeverReductionBuff cabinfever = __instance.gameObject.GetComponent<CabinFeverReductionBuff>() ?? __instance.gameObject.AddComponent<CabinFeverReductionBuff>();
                    if (cabinfever == null) return;

                    freezing.m_DurationHours = 2;
                    freezing.m_InitialPercentDecrease = 45;
                    freezing.m_RateOfIncreaseScale = 0.65f;

                    carrybuff.m_CarryCapacityBuffDurationInHours = 4;
                    carrybuff.m_CarryCapacityChange = ItemWeight.FromKilograms(5);

                    foodMaxStat.m_Stat = FoodMaxStatBuff.StatType.Stamina;
                    foodMaxStat.m_CanStack = false;
                    foodMaxStat.m_TimeActiveHours = 3;
                    foodMaxStat.m_Buff = 10;

                    cabinfever.m_BuffAppliedText = new LocalizedString { m_LocalizationID = "GAMEPLAY_CabinFeverReductionApplied" };
                    cabinfever.m_BuffTypeText = new LocalizedString { m_LocalizationID = "GAMEPLAY_CabinFeverReductionType" };
                    cabinfever.m_BuffIcon = "ico_injury_cabinFever";
                    cabinfever.m_BuffCooldownInHours = 144;
                    cabinfever.m_FeverReductionInHours = 6;
                    cabinfever.m_RiskReductionInHours = 36;

                    foodStat.m_Stat = FoodStatEffect.StatType.Fatigue;
                    foodStat.m_Effect = -20;
                }

                if (name.Contains("GEAR_CookedPumpkinSeeds"))
                {
                    FoodStatEffect foodStat = __instance.gameObject.GetComponent<FoodStatEffect>() ?? __instance.gameObject.AddComponent<FoodStatEffect>();
                    if (foodStat == null) return;

                    FoodMaxStatBuff foodMaxStat = __instance.gameObject.GetComponent<FoodMaxStatBuff>() ?? __instance.gameObject.AddComponent<FoodMaxStatBuff>();
                    if (foodMaxStat == null) return;

                    IngestedCarryCapacityBuff carrybuff = __instance.gameObject.GetComponent<IngestedCarryCapacityBuff>() ?? __instance.gameObject.AddComponent<IngestedCarryCapacityBuff>();
                    if (carrybuff == null) return;

                    foodStat.m_Stat = FoodStatEffect.StatType.Stamina;
                    foodStat.m_Effect = 25;

                    foodMaxStat.m_Stat = FoodMaxStatBuff.StatType.Stamina;
                    foodMaxStat.m_CanStack = false;
                    foodMaxStat.m_TimeActiveHours = 3;
                    foodMaxStat.m_Buff = 15;

                    carrybuff.m_CarryCapacityBuffDurationInHours = 3;
                    carrybuff.m_CarryCapacityChange = ItemWeight.FromKilograms(1.5f);
                }

                if (name.Contains("GEAR_CookedPumpkinSoup"))
                {
                    Scent scent = __instance.gameObject.GetComponent<Scent>() ?? __instance.gameObject.AddComponent<Scent>();
                    if (scent == null) return;

                    FreezingBuff freezing = __instance.gameObject.GetComponent<FreezingBuff>() ?? __instance.gameObject.AddComponent<FreezingBuff>();
                    if (freezing == null) return;

                    FoodStatEffect foodStat = __instance.gameObject.GetComponent<FoodStatEffect>() ?? __instance.gameObject.AddComponent<FoodStatEffect>();
                    if (foodStat == null) return;

                    ConditionRestBuff restbuff = __instance.gameObject.GetComponent<ConditionRestBuff>() ?? __instance.gameObject.AddComponent<ConditionRestBuff>();
                    if (restbuff == null) return;

                    freezing.m_DurationHours = 1.5f;
                    freezing.m_InitialPercentDecrease = 60;
                    freezing.m_RateOfIncreaseScale = 0.33f;

                    restbuff.m_NumHoursRestAffected = 6;
                    restbuff.m_ConditionRestBonus = 3f;

                    foodStat.m_Stat = FoodStatEffect.StatType.Fatigue;
                    foodStat.m_Effect = 10;

                    scent.m_ScentCategory = ScentRangeCategory.COOKED_MEAT;

                    Cookable cookable = __instance.gameObject.GetComponent<Cookable>();
                    if (cookable == null) return;

                    cookable.m_CookingPotMaterialsList = new Material[1] { Utilities.SoupCookedLiquidMaterial() };
                    cookable.m_CookingPotRawMaterialsList = new Material[1] { Utilities.SoupCookedLiquidMaterial() };
                    cookable.m_RuinedMaterials = new Material[1] { Utilities.BurntMaterial() };
                }

                if (name.Contains("GEAR_CookedPumpkinSpiceLatte"))
                {
                    FreezingBuff freezing = __instance.gameObject.GetComponent<FreezingBuff>() ?? __instance.gameObject.AddComponent<FreezingBuff>();
                    if (freezing == null) return;

                    FatigueBuff fb = __instance.gameObject.GetComponent<FatigueBuff>() ?? __instance.gameObject.AddComponent<FatigueBuff>();
                    if (fb == null) return;

                    FoodMaxStatBuff foodMaxStat = __instance.gameObject.GetComponent<FoodMaxStatBuff>() ?? __instance.gameObject.AddComponent<FoodMaxStatBuff>();
                    if (foodMaxStat == null) return;

                    CausesHeadacheDebuff headache = __instance.gameObject.GetComponent<CausesHeadacheDebuff>() ?? __instance.gameObject.AddComponent<CausesHeadacheDebuff>();
                    if (headache == null) return;

                    HeadacheData headacheData = GearItem.LoadGearItemPrefab("GEAR_CookedPieRoseHip").gameObject.GetComponent<CausesHeadacheDebuff>().m_HeadacheSettings;
                    if (headacheData == null) return;

                    freezing.m_DurationHours = 1f;
                    freezing.m_InitialPercentDecrease = 33;
                    freezing.m_RateOfIncreaseScale = 0.5f;

                    fb.m_DurationHours = 2.5f;
                    fb.m_RateOfIncreaseScale = 0.45f;
                    fb.m_InitialPercentDecrease = 30;

                    foodMaxStat.m_Stat = FoodMaxStatBuff.StatType.Stamina;
                    foodMaxStat.m_CanStack = false;
                    foodMaxStat.m_TimeActiveHours = 2f;
                    foodMaxStat.m_Buff = 25;

                    headache.m_CausesHeadacheThreshold = 0.5f;
                    headache.m_HeadacheSettings = headacheData;
                    headacheData.m_HeadacheDurationHours = 0.33f;

                    Cookable cookable = __instance.gameObject.GetComponent<Cookable>();
                    if (cookable == null) return;

                    cookable.m_CookingPotMaterialsList = new Material[1] { Utilities.CoffeeLiquidMaterial() };
                    cookable.m_CookingPotRawMaterialsList = new Material[1] { Utilities.CoffeeLiquidMaterial() };
                    cookable.m_RuinedMaterials = new Material[1] { Utilities.BurntMaterial() };
                }
            }
        }
    }
}
