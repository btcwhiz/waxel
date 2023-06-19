using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameObjectHandler : MonoBehaviour
{

    public static void OpenScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void OpenUrl(string url)
    {
        Application.OpenURL(url);
    }

    public static void TogglePanel(GameObject panel)
    {
        if(panel != null)
        {
            bool isActive = panel.activeSelf;

            panel.SetActive(!isActive);
        }
    }

    public static void ClosePanel(GameObject panel)
    {
        if(panel != null)
        {
            panel.SetActive(false);
        }
    }

    public static void OpenPanel(GameObject panel)
    {
        if(panel != null)
        {
            panel.SetActive(true);
        }
    }
    public void OnLogoutButtonClick()
    {
        MessageHandler.LogoutRequest();
    }

    public static void BuyDrop(string drop)
    {
        switch (drop)
        {
            case ("WaxelNinjasPack"):
                Application.OpenURL(Config.configData.DROP_WAXEL_NINJAS_PACK);
                break;
            case ("SettlementUpgradeAll"):
                Application.OpenURL(Config.configData.DROP_SETTLEMENT_UPGRADE_ALL);
                break;
            case ("SettlementUpgradeCamp"):
                Application.OpenURL(Config.configData.DROP_SETTLEMENT_UPGRADE_CAMP);
                break;
            case ("BookMiner"):
                Application.OpenURL(Config.configData.DROP_BOOK_MINER);
                break;
            case ("BookLumberjack"):
                Application.OpenURL(Config.configData.DROP_BOOK_LUMBERJACK);
                break;
            case ("BookFarmer"):
                Application.OpenURL(Config.configData.DROP_BOOK_FARMER);
                break;
            case ("BookBlacksmith"):
                Application.OpenURL(Config.configData.DROP_BOOK_BLACKSMITH);
                break;
            case ("BookCarpenter"):
                Application.OpenURL(Config.configData.DROP_BOOK_CARPENTER);
                break;
            case ("BookTailor"):
                Application.OpenURL(Config.configData.DROP_BOOK_TAILOR);
                break;
            case ("BookEngineer"):
                Application.OpenURL(Config.configData.DROP_BOOK_ENGINEER);
                break;
            default:
                break;
        }
    }

    public void SellMarket(string type)
    {
        switch (type)
        {
            case ("All"):
                Application.OpenURL(Config.configData.MARKET_SECONDARY_ALL);
                break;
            case ("WaxelNinjas"):
                Application.OpenURL(Config.configData.MARKET_SECONDARY_WAXEL_NINJAS);
                break;
            case ("SettlementUpgrades"):
                Application.OpenURL(Config.configData.MARKET_SECONDARY_SETTLEMENT_UPGRADES);
                break;
            case ("Citizens"):
                Application.OpenURL(Config.configData.MARKET_SECONDARY_CITIZENS);
                break;
            case ("Professions"):
                Application.OpenURL(Config.configData.MARKET_SECONDARY_PROFESSIONS);
                break;
            case ("Materials"):
                Application.OpenURL(Config.configData.MARKET_SECONDARY_MATERIALS);
                break;
            case ("Items"):
                Application.OpenURL(Config.configData.MARKET_SECONDARY_ITEMS);
                break;
            default:
                break;
        }
    }
}
