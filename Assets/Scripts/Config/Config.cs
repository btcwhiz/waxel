using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Config : MonoBehaviour
{
    public static ConfigData configData;
    private static string jsonPath = "Configs/config";

    [System.Serializable]
    public class ConfigData
    {
        // General
        public float TIME_LOADING_PANEL;
        // Collection and schemas
        public string COLLECTION_NAME;
        public string SCHEMA_WAXEL_NINJAS;
        public string SCHEMA_UPGRADES;
        public string SCHEMA_CITIZENS;
        public string SCHEMA_PROFESSIONS;
        public string SCHEMA_MATERIALS;
        public string SCHEMA_ITEMS;
        // Secondary market
        public string MARKET_SECONDARY_URL;
        public string MARKET_SECONDARY_ALL;
        public string MARKET_SECONDARY_WAXEL_NINJAS;
        public string MARKET_SECONDARY_SETTLEMENT_UPGRADES;
        public string MARKET_SECONDARY_CITIZENS;
        public string MARKET_SECONDARY_PROFESSIONS;
        public string MARKET_SECONDARY_MATERIALS;
        public string MARKET_SECONDARY_ITEMS;
        // Drops
        public string DROP_MARKET_URL;
        public string DROP_WAXEL_NINJAS_PACK;
        public string DROP_SETTLEMENT_UPGRADE_ALL;
        public string DROP_SETTLEMENT_UPGRADE_CAMP;
        public string DROP_SETTLEMENT_UPGRADE_MINE;
        public string DROP_SETTLEMENT_UPGRADE_FOREST;
        public string DROP_SETTLEMENT_UPGRADE_FIELD;
        public string DROP_BOOK_MINER;
        public string DROP_BOOK_LUMBERJACK;
        public string DROP_BOOK_FARMER;
        public string DROP_BOOK_BLACKSMITH;
        public string DROP_BOOK_CARPENTER;
        public string DROP_BOOK_TAILOR;
        public string DROP_BOOK_ENGINEER;
    }

    public static ConfigData LoadConfigData() {
        var jsonTextFile = Resources.Load<TextAsset>(jsonPath);
        return configData = JsonUtility.FromJson<ConfigData>(jsonTextFile.text);
    }

}
