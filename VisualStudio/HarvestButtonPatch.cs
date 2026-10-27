
namespace AutumnComforts
{
    public class HarvestButton
    {
        private static GameObject harvestButton;
        private static GameObject carveButton;
        internal static GearItem currentGearItem;
        private static string? harvestedItemName;
        private static bool isCarveAction;

        private static string harvestButtonTextKey = "GAMEPLAY_HarvestButton";
        private static string carveButtonTextKey = "GAMEPLAY_CarveButton";

        private static GearItem? GetBestKnife()
        {
            Inventory inv = GameManager.GetInventoryComponent();
            if (inv.GearInInventory("GEAR_Knife", 1)) return inv.GetBestGearItemWithName("GEAR_Knife");
            if (inv.GearInInventory("GEAR_CougarClawKnife", 1)) return inv.GetBestGearItemWithName("GEAR_CougarClawKnife");
            if (inv.GearInInventory("GEAR_JeremiahKnife", 1)) return inv.GetBestGearItemWithName("GEAR_JeremiahKnife");
            if (inv.GearInInventory("GEAR_SurvivalKnife", 1)) return inv.GetBestGearItemWithName("GEAR_SurvivalKnife");
            if (inv.GearInInventory("GEAR_KnifeImprovised", 1)) return inv.GetBestGearItemWithName("GEAR_KnifeImprovised");
            if (inv.GearInInventory("GEAR_KnifeScrapMetal", 1)) return inv.GetBestGearItemWithName("GEAR_KnifeScrapMetal");
            return null;
        }

        private static void DamageTool(GearItem? tool, float amount)
        {
            if (tool == null) return;
            tool.Degrade(amount);
        }

        internal static void InInitializeItemDescriptionPage(ItemDescriptionPage itemDescriptionPage)
        {
            GameObject equipButton = itemDescriptionPage.m_MouseButtonEquip;

            harvestButton = UnityEngine.Object.Instantiate<GameObject>(equipButton, equipButton.transform.parent, true);
            harvestButton.transform.Translate(0.4f, 0, 0);
            AddAction(harvestButton, new System.Action(OnHarvest));

            carveButton = UnityEngine.Object.Instantiate<GameObject>(equipButton, equipButton.transform.parent, true);
            carveButton.transform.Translate(0, 0, 0);
            AddAction(carveButton, new System.Action(OnCarve));

            SetHarvestActive(false);
            SetCarveActive(false);
        }

        private static void AddAction(GameObject button, System.Action action)
        {
            Il2CppSystem.Collections.Generic.List<EventDelegate> list = new Il2CppSystem.Collections.Generic.List<EventDelegate>();
            list.Add(new EventDelegate(action));
            Utils.GetComponentInChildren<UIButton>(button).onClick = list;
        }

        internal static void SetHarvestActive(bool active)
        {
            if (harvestButton != null)
            {
                UILabel label = Utils.GetComponentInChildren<UILabel>(harvestButton);
                if (label != null) label.text = Localization.Get(harvestButtonTextKey);
            }
            NGUITools.SetActive(harvestButton, active);
        }

        internal static void SetCarveActive(bool active)
        {
            if (carveButton != null)
            {
                UILabel label = Utils.GetComponentInChildren<UILabel>(carveButton);
                if (label != null) label.text = Localization.Get(carveButtonTextKey);
            }
            NGUITools.SetActive(carveButton, active);
        }

        internal static bool AllowHarvestButton(string gearName)
        {
            return gearName == "GEAR_Pumpkin" || gearName == "GEAR_PumpkinDeco";
        }

        internal static bool AllowCarveButton(string gearName)
        {
            return gearName == "GEAR_Pumpkin";
        }

        private static void OnHarvest()
        {
            if (GetBestKnife() == null)
            {
                GameAudioManager.PlayGUIError();
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_HarvestNoToolsMsg"));
                return;
            }
            harvestedItemName = currentGearItem?.name;
            isCarveAction = false;
            LaunchProgressBarHarvest();
        }

        private static void OnCarve()
        {
            if (GetBestKnife() == null)
            {
                GameAudioManager.PlayGUIError();
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_CarveNoToolsMsg"));
                return;
            }
            harvestedItemName = currentGearItem?.name;
            isCarveAction = true;
            LaunchProgressBarCarve();
        }

        private static void LaunchProgressBarHarvest()
        {
            GameAudioManager.PlayGuiConfirm();
            InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_ProgressBarHarvest"),
                4f, 0, 0, "Play_CraftingBurdockPrepared", null, false, true,
                new System.Action<bool, bool, float>(OnActionFinished));
        }

        private static void LaunchProgressBarCarve()
        {
            GameAudioManager.PlayGuiConfirm();
            InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_ProgressBarCarve"),
                4f, 0, 0, "Play_CraftingBurdockPrepared", null, false, true,
                new System.Action<bool, bool, float>(OnActionFinished));
        }

        private static void OnActionFinished(bool success, bool playerCancel, float progress)
        {
            if (!success || playerCancel) return;

            PlayerManager? pm = GameManager.GetPlayerManagerComponent();
            if (pm == null) return;

            DamageTool(GetBestKnife(), 3f);

            if (isCarveAction)
            {
                GameManager.GetInventoryComponent().RemoveGearFromInventory("GEAR_Pumpkin", 1);

                GearItem? pumpkinDeco = GearItem.LoadGearItemPrefab("GEAR_PumpkinDeco");
                GearItem? preparedPumpkin = GearItem.LoadGearItemPrefab("GEAR_PreparedPumpkin");
                GearItem? rawSeeds = GearItem.LoadGearItemPrefab("GEAR_RawPumpkinSeeds");

                if (pumpkinDeco != null) pm.InstantiateItemInPlayerInventory(pumpkinDeco, 1);
                if (preparedPumpkin != null) pm.InstantiateItemInPlayerInventory(preparedPumpkin, 4);
                if (rawSeeds != null) pm.InstantiateItemInPlayerInventory(rawSeeds, 6);
            }
            else
            {
                if (harvestedItemName != null && harvestedItemName.Contains("GEAR_PumpkinDeco"))
                {
                    GameManager.GetInventoryComponent().RemoveGearFromInventory("GEAR_PumpkinDeco", 1);

                    GearItem? preparedPumpkin = GearItem.LoadGearItemPrefab("GEAR_PreparedPumpkin");
                    if (preparedPumpkin != null) pm.InstantiateItemInPlayerInventory(preparedPumpkin, 4);
                }
                else
                {
                    GameManager.GetInventoryComponent().RemoveGearFromInventory("GEAR_Pumpkin", 1);

                    GearItem? preparedPumpkin = GearItem.LoadGearItemPrefab("GEAR_PreparedPumpkin");
                    GearItem? rawSeeds = GearItem.LoadGearItemPrefab("GEAR_RawPumpkinSeeds");

                    if (preparedPumpkin != null) pm.InstantiateItemInPlayerInventory(preparedPumpkin, 8);
                    if (rawSeeds != null) pm.InstantiateItemInPlayerInventory(rawSeeds, 6);
                }
            }
            harvestedItemName = null;
        }
    }
    [HarmonyPatch(typeof(Panel_Inventory), nameof(Panel_Inventory.Initialize))]
    internal class HarvestInitialization
    {
        private static void Postfix(Panel_Inventory __instance)
        {
            HarvestButton.InInitializeItemDescriptionPage(__instance.m_ItemDescriptionPage);
        }
    }
    [HarmonyPatch(typeof(ItemDescriptionPage), nameof(ItemDescriptionPage.UpdateGearItemDescription))]
    internal class UpdateHarvestButton
    {
        private static void Postfix(ItemDescriptionPage __instance, GearItem gi)
        {
            if (__instance != InterfaceManager.GetPanel<Panel_Inventory>()?.m_ItemDescriptionPage) return;

            HarvestButton.currentGearItem = gi?.GetComponent<GearItem>();
            string gearName = gi?.name ?? "";

            HarvestButton.SetHarvestActive(gi != null && HarvestButton.AllowHarvestButton(gearName));
            HarvestButton.SetCarveActive(gi != null && HarvestButton.AllowCarveButton(gearName));
        }
    }
}