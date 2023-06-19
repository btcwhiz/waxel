using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NinjaShow : BaseView
{
    public TMP_Text[] ninja_each_count;

    [Space]
    [Header("PanelManager")]
    public GameObject NinjaInfoPanel;
    public TMP_Text RegisterInfoText;
    public GameObject NinjsEmptyPanel;
    public GameObject ContentPanel;
    public Transform ContentTransform;
    public GameObject OneNinjaPrefab;
    public GameObject CampEmptyPanel;
    public GameObject OneCampPrefab;

    [Space]
    [Header("Popup Bulk Search Result")]
    public GameObject PopupBulkSearchResult;
    public TMP_Text PopupBulkSearchResultSuccessAmount;
    public TMP_Text PopupBulkSearchResultFailAmount;
    public RawImage PopupBulkSearchResultNFTImage;
    public TMP_Text PopupBulkSearchResultNFTName;
    public TMP_Text PopupBulkSearchResultNFTAmount;
    public ImgObjectView[] images;

    [Space]
    [Header("SceneAsset")]
    public GameObject foundPanel;
    public GameObject SettlementParentPanel;
    public GameObject SettlementTextPanel;
    public GameObject SettlementChildPanel;
    public GameObject UnregisteredSettlementChildPanel;
    public GameObject RegisteredSettlementChildPanel;
    public GameObject SettlementBuyBtn;
    public GameObject SettlementDeregButton;
    public GameObject ActionGroup;
    public GameObject BulkButton;
    public GameObject CheckAllButton;
    public GameObject NoCamp_text;

    private List<NinjaDataModel> Human = new List<NinjaDataModel>();
    private List<NinjaDataModel> Orc = new List<NinjaDataModel>();
    private List<NinjaDataModel> Undead = new List<NinjaDataModel>();
    private List<NinjaDataModel> Elf = new List<NinjaDataModel>();
    private List<NinjaDataModel> Demon = new List<NinjaDataModel>();
    private List<SettlementsModel> Camp = new List<SettlementsModel>();

    private List<AssetModel> oldExtra = new List<AssetModel>();
    private List<string> checked_items = new List<string>();

    private Dictionary<string, DelayDataModel> DelayValues = new Dictionary<string, DelayDataModel>();

    private bool bulkActionVisible = false;
    private IEnumerator coroutine;

    private int success_cnt = 0;
    private int failed_cnt = 0;
    private string new_nft_name = "";
    private bool init_flag = false;
    private bool checkAllClicked = false;
    private bool itemCheckClicked = false;

    // setting the header
    public delegate void SetHeader();
    public static SetHeader onSetHeaderElements;

    protected override void Start()
    {
        init_flag = true;
        base.Start();
        SetModels(MessageHandler.userModel.ninjas);
        SetSettlementsModel(MessageHandler.userModel.settlements);
        SetUIElements();
        MessageHandler.OnNinjaData += OnNinjaData;
        MessageHandler.OnCallBackData += OnCallBackData;
        MessageHandler.OnSettlementData += OnSettlementData;
        ErrorHandler.OnErrorData += OnErrorData;
        init_flag = false;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        MessageHandler.OnNinjaData -= OnNinjaData;
        MessageHandler.OnCallBackData -= OnCallBackData;
        MessageHandler.OnSettlementData -= OnSettlementData;
        ErrorHandler.OnErrorData -= OnErrorData;
    }
    private void SetUIElements()
    {
        if (MessageHandler.userModel.account != null)
        {
            RegisteredCountShow("ninja");
            foreach (DelayDataModel data in MessageHandler.userModel.config.race_delay_values)
            {
                if (!DelayValues.ContainsKey(data.key)) DelayValues.Add(data.key, data);
            }
            for (int i = 0; i < ninja_each_count.Length; i++)
            {
                switch (ninja_each_count[i].gameObject.name)
                {
                    case ("Human"):
                        ninja_each_count[i].text = "x" + Human.Count.ToString();
                        break;
                    case ("Elf"):
                        ninja_each_count[i].text = "x" + Elf.Count.ToString();
                        break;
                    case ("Orc"):
                        ninja_each_count[i].text = "x" + Orc.Count.ToString();
                        break;
                    case ("Undead"):
                        ninja_each_count[i].text = "x" + Undead.Count.ToString();
                        break;
                    case ("Demon"):
                        ninja_each_count[i].text = "x" + Demon.Count.ToString();
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private void SetModels(NinjaDataModel[] ninjas)
    {
        Human.Clear();
        Elf.Clear();
        Orc.Clear();
        Undead.Clear();
        Demon.Clear();

        foreach (NinjaDataModel ninja in ninjas)
        {
            switch (ninja.race)
            {
                case ("Human"):
                    Human.Add(ninja);
                    break;
                case ("Elf"):
                    Elf.Add(ninja);
                    break;
                case ("Orc"):
                    Orc.Add(ninja);
                    break;
                case ("Undead"):
                    Undead.Add(ninja);
                    break;
                case ("Demon"):
                    Demon.Add(ninja);
                    break;
                default:
                    break;
            }
        }
    }

    private void SetSettlementsModel(SettlementsModel[] settlements)
    {
        Camp.Clear();
        foreach (SettlementsModel data in settlements)
        {
            switch (data.name)
            {
                case ("Camp"):
                    Camp.Add(data);
                    break;
                default:
                    break;
            }
        }
    }

    public void NinjaButtonClick(string name)
    {
        NinjaInfoPanel.SetActive(false);
        BulkButton.SetActive(true);
        CheckAllButton.SetActive(true);
        switch (name)
        {
            case ("Human"):
                NinjaActionShow(Human);
                success_cnt = GetFoundCitizens(Human);
                failed_cnt = GetNoFoundCitizens(Human);
                break;
            case ("Elf"):
                NinjaActionShow(Elf);
                success_cnt = GetFoundCitizens(Elf);
                failed_cnt = GetNoFoundCitizens(Elf);
                break;
            case ("Orc"):
                NinjaActionShow(Orc);
                success_cnt = GetFoundCitizens(Orc);
                failed_cnt = GetNoFoundCitizens(Orc);
                break;
            case ("Undead"):
                NinjaActionShow(Undead);
                success_cnt = GetFoundCitizens(Undead);
                failed_cnt = GetNoFoundCitizens(Undead);
                break;
            case ("Demon"):
                NinjaActionShow(Demon);
                success_cnt = GetFoundCitizens(Demon);
                failed_cnt = GetNoFoundCitizens(Demon);
                break;
            default:
                break;
        }
        CheckAllButton.GetComponent<Toggle>().isOn = false;
    }
    public void RegisteredCountShow(string type){
        if (type == "ninja")
        {
            string maxCount = "10";
            foreach (MaxNftDataModel nftData in MessageHandler.userModel.nft_count)
            {
                if (nftData.name == "Max Ninja")
                {
                    maxCount = nftData.count;
                    break;
                }
            }

            int registeredCount = 0;
            foreach (NinjaDataModel a in MessageHandler.userModel.ninjas)
            {
                if (a.reg == "1")
                {
                    registeredCount += 1;
                }
            }
            RegisterInfoText.text = "Waxel Ninjas - " + registeredCount.ToString() + "/" + maxCount;
        }
        else
        {
            string maxCount = Camp.Count.ToString();
            int registeredCount = 0;
            foreach (SettlementsModel c in Camp)
            {
                if (c.reg == "1")
                {
                    registeredCount += 1;
                }
            }
            RegisterInfoText.text = "Camp - " + registeredCount.ToString() + "/" + maxCount;
        }

    }
    public void NinjaActionShow(List<NinjaDataModel> ninjaModel)
    {
        RegisteredCountShow("ninja");
        if (ninjaModel.Count > 0)
        {
            NinjsEmptyPanel.SetActive(false);
            ContentPanel.SetActive(true);
            BulkButton.gameObject.GetComponent<Button>().interactable = false;
            ActionGroup.SetActive(false);
            if (ContentTransform.childCount >= 1)
            {
                foreach (Transform child in ContentTransform)
                {
                    GameObject.Destroy(child.gameObject);
                }
            }

            foreach (NinjaDataModel ninja in ninjaModel)
            {
                var ins = Instantiate(OneNinjaPrefab);
                ins.transform.SetParent(ContentTransform);
                ins.transform.localScale = new Vector3(1, 1, 1);
                var child = ins.gameObject.GetComponent<NinjaStatus>();
                child.race = ninja.race;
                child.assetId = ninja.asset_id;
                child.status = ninja.status;
                child.name.text = "Waxel Ninja #" + ninja.mint_id;
                child.GetComponent<Toggle>().onValueChanged.AddListener(delegate { ChangeNinjaStatus(); });
                child.Check.gameObject.GetComponent<Button>().onClick.AddListener(delegate { CheckButtonClick(ninja.asset_id, ninja.race); });
                child.Register.gameObject.GetComponent<Button>().onClick.AddListener(delegate { RegisterButtonClick(); });
                child.Registered.gameObject.GetComponent<Button>().onClick.AddListener(delegate { UnregisterButtonClick(); });
                child.Search.gameObject.GetComponent<Button>().onClick.AddListener(delegate { SearchButtonClick(); });
                child.img.texture = Resources.Load("Images/WaxelNinjas/" + ninja.mint_id + "_waxel_ninja") as Texture2D;
                if (ninja.reg == "0")
                {
                    child.Register.SetActive(true);
                }
                else
                {
                    child.SellBtn.gameObject.GetComponent<Button>().interactable = false;
                    if (ninja.last_search != "1970-01-01T00:00:00")
                    {
                        switch (ninja.status)
                        {
                            case ("Searching"):
                                child.StartTimer(ninja.last_search, DelayValues[ninja.race].value);
                                break;
                            case ("holdup"):
                            case ("holdup1"):
                                child.Check.SetActive(true);
                                break;
                            default:
                                break;
                        }
                    }
                    else if (ninja.last_search == "1970-01-01T00:00:00")
                    {
                        child.Registered.SetActive(true);
                    }
                }
            }
        }
        else
        {
            ContentPanel.SetActive(false);
            NinjsEmptyPanel.SetActive(true);
            BulkButton.SetActive(false);
            CheckAllButton.SetActive(false);
        }
    }

    public int GetFoundCitizens(List<NinjaDataModel> ninjaModel) 
    {
        int cnt = 0;
        foreach (NinjaDataModel ninja in ninjaModel)
        {
            if(ninja.status == "Search successful") cnt++;
        }
        return cnt;
    }

    public int GetNoFoundCitizens(List<NinjaDataModel> ninjaModel) 
    {
        int cnt = 0;
        foreach (NinjaDataModel ninja in ninjaModel)
        {
            if(ninja.status == "Search failed") cnt++;
        }
        return cnt;
    }


    public void ChangeBulkActions()
    {
        bulkActionVisible = !bulkActionVisible;
        if(bulkActionVisible)
        {
            ActionGroup.SetActive(true);
        }
        else
        {
            ActionGroup.SetActive(false);
        }
    }

    public void RegisterNinjas()
    {
        bulkActionVisible = false;
        ActionGroup.SetActive(false);
        List<string> id_arr = new List<string>();
        string race = "";
        bool state_flag = true;
        foreach (Transform child in ContentTransform)
        {
            var obj = child.gameObject.GetComponent<NinjaStatus>();
            if(child.gameObject.GetComponent<Toggle>().isOn)
            {
                if(obj.Register.activeSelf == false)
                {
                    state_flag = false;
                    break;
                }
                id_arr.Add(obj.assetId);
                race = obj.race;
            }
        }
        if(state_flag == false) 
        {
            DisplayInfoPopup(Constants.INFO_TYPE_DEFAULT, Constants.INFO_TITLE_DEFAULT, "Unable to \"Register\" as not all selected Waxel Ninjas have the same status.");
        }
        else
        {
            LoadingPanel.SetActive(true);
            coroutine = WaitAndClose(Config.configData.TIME_LOADING_PANEL);
            StartCoroutine(coroutine);
            BulkButton.gameObject.GetComponent<Button>().interactable = false;
            MessageHandler.RegisterNFTRequest(String.Join(",", id_arr), race);
        }
    }

    public void UnregisterNinjas()
    {
        bulkActionVisible = false;
        ActionGroup.SetActive(false);
        List<string> id_arr = new List<string>();
        string race = "";
        bool state_flag = true;
        foreach (Transform child in ContentTransform)
        {
            var obj = child.gameObject.GetComponent<NinjaStatus>();
            if(child.gameObject.GetComponent<Toggle>().isOn)
            {
                if(obj.Timer.activeSelf == true || obj.Check.activeSelf == true || obj.Register.activeSelf == true)
                {
                    state_flag = false;
                    break;
                }
                id_arr.Add(obj.assetId);
                race = obj.race;
            }
        }
        if(state_flag == false) 
        {
            DisplayInfoPopup(Constants.INFO_TYPE_DEFAULT, Constants.INFO_TITLE_DEFAULT, "Unable to \"Unregister\" as not all selected Waxel Ninjas have the same status.");
        }
        else
        {
            LoadingPanel.SetActive(true);
            coroutine = WaitAndClose(Config.configData.TIME_LOADING_PANEL);
            StartCoroutine(coroutine);
            BulkButton.gameObject.GetComponent<Button>().interactable = false;
            MessageHandler.Server_UnregisterNFT(String.Join(",", id_arr), race);
        }
    }

    public void SearchNinjas()
    {
        bulkActionVisible = false;
        ActionGroup.SetActive(false);
        List<string> id_arr = new List<string>();
        string race = "";
        bool state_flag = true;
        foreach (Transform child in ContentTransform)
        {
            var obj = child.gameObject.GetComponent<NinjaStatus>();
            if(child.gameObject.GetComponent<Toggle>().isOn)
            {
                if(obj.Timer.activeSelf == true || obj.Check.activeSelf == true || obj.Register.activeSelf == true)
                {
                    state_flag = false;
                    break;
                }
                id_arr.Add(obj.assetId);
                race = obj.race;
            }
        }
        if(state_flag == false)
        {
            DisplayInfoPopup(Constants.INFO_TYPE_DEFAULT, Constants.INFO_TITLE_DEFAULT, "Unable to \"Search\" as not all selected Waxel Ninjas have the same status.");
        }
        else
        {
            LoadingPanel.SetActive(true);
            coroutine = WaitAndClose(Config.configData.TIME_LOADING_PANEL);
            StartCoroutine(coroutine);
            BulkButton.gameObject.GetComponent<Button>().interactable = false;
            MessageHandler.Server_SearchCitizen(String.Join(",", id_arr), "1", race);
        }
    }

    public void CheckNinjas()
    {
        bulkActionVisible = false;
        ActionGroup.SetActive(false);
        List<string> id_arr = new List<string>();
        string race = "";
        bool state_flag = true;
        foreach (Transform child in ContentTransform)
        {
            var obj = child.gameObject.GetComponent<NinjaStatus>();
            if(child.gameObject.GetComponent<Toggle>().isOn)
            {
                if(obj.Check.activeSelf == false)
                {
                    state_flag = false;
                    break;
                }
                id_arr.Add(obj.assetId);
                race = obj.race;
            }
        }

        checked_items.Clear();
        foreach (Transform child in ContentTransform) 
        {
            var obj = child.gameObject.GetComponent<NinjaStatus>();
            if(child.gameObject.GetComponent<Toggle>().isOn)
            {
                checked_items.Add(obj.assetId);
            }
        }

        if(state_flag == false)
        {
            DisplayInfoPopup(Constants.INFO_TYPE_DEFAULT, Constants.INFO_TITLE_DEFAULT, "Unable to \"Check\" as not all selected Waxel Ninjas have the same status.");
        }
        else
        {
            LoadingPanel.SetActive(true);
            coroutine = WaitAndClose(Config.configData.TIME_LOADING_PANEL);
            StartCoroutine(coroutine);
            BulkButton.gameObject.GetComponent<Button>().interactable = false;
            oldExtra.Clear();
            AddExtraData();
            MessageHandler.Server_SearchCitizen(String.Join(",", id_arr), "2", race);
        }
    }

    public void CheckAllButtonClick()
    {
        if(itemCheckClicked) return;
        checkAllClicked = true;
        bool checkState = CheckAllButton.GetComponent<Toggle>().isOn;
        foreach (Transform child in ContentTransform)
        {
            child.gameObject.GetComponent<Toggle>().isOn = checkState;
        }

        if(checkState == true)
        {
            BulkButton.gameObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            BulkButton.gameObject.GetComponent<Button>().interactable = false;
            ActionGroup.SetActive(false);
        }

        checkAllClicked = false;
    }

    public void CheckButtonClick(string assetId, string race)
    {
        LoadingPanel.SetActive(true);
        coroutine = WaitAndClose(Config.configData.TIME_LOADING_PANEL);
        StartCoroutine(coroutine);
        checked_items.Clear();
        checked_items.Add(assetId);

        oldExtra.Clear();
        AddExtraData();

        MessageHandler.Server_SearchCitizen(assetId, "2",race);
    }

    public void RegisterButtonClick()
    {
        LoadingPanel.SetActive(true);
        coroutine = WaitAndClose(Config.configData.TIME_LOADING_PANEL);
        StartCoroutine(coroutine);
    }

    public void UnregisterButtonClick()
    {
        LoadingPanel.SetActive(true);
        coroutine = WaitAndClose(Config.configData.TIME_LOADING_PANEL);
        StartCoroutine(coroutine);
    }

    public void SearchButtonClick()
    {
        LoadingPanel.SetActive(true);
        coroutine = WaitAndClose(Config.configData.TIME_LOADING_PANEL);
        StartCoroutine(coroutine);
    }

    public void ShowSettlements(List<SettlementsModel> camp)
    {
        RegisteredCountShow("camp");
        ContentPanel.SetActive(true);
        if (ContentTransform.childCount >= 1)
        {
            foreach (Transform child in ContentTransform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        foreach (SettlementsModel data in camp)
        {
            var ins = Instantiate(OneCampPrefab);
            ins.transform.SetParent(ContentTransform);
            ins.transform.localScale = new Vector3(1, 1, 1);
            var child = ins.gameObject.GetComponent<OneCampStatus>();
            child.name.text = "#" + data.asset_id;
            child.assetId = data.asset_id;
            if (data.reg == "0")
            {
                child.Register.SetActive(true);
                child.Register.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                child.Register.gameObject.GetComponent<Button>().onClick.AddListener(delegate { Register_Settlement(data.asset_id); });
            }
            else if (data.reg == "1")
            {
                child.SellBtn.gameObject.GetComponent<Button>().interactable = false;
                child.Unregister.SetActive(true);
                child.Unregister.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                child.Unregister.gameObject.GetComponent<Button>().onClick.AddListener(delegate { DeRegister_Settlement(data.asset_id); });
            }
        }
    }

    public void Register_Settlement(string asset_id)
    {
        LoadingPanel.SetActive(true);
        coroutine = WaitAndClose(Config.configData.TIME_LOADING_PANEL);
        StartCoroutine(coroutine);
        MessageHandler.Server_TransferAsset(asset_id, "regupgrade","Camp");
    }

    public void DeRegister_Settlement(string asset_id)
    {
        LoadingPanel.SetActive(true);
        coroutine = WaitAndClose(Config.configData.TIME_LOADING_PANEL);
        StartCoroutine(coroutine);
        MessageHandler.Server_WithdrawAsset(asset_id,"Camp");
    }

    public void okButton()
    {
        foundPanel.SetActive(false);
    }

    public void OnNinjaData(NinjaDataModel[] ninjas)
    {
        SetModels(ninjas);
        SetUIElements();
    }

    public void OnSettlementData(SettlementsModel[] settlements)
    {
        SetSettlementsModel(settlements);
    }

    public void OnCallBackData(CallBackDataModel[] callback)
    {
        CallBackDataModel callBack = callback[0];
        int foundCnt = 0, noFoundCnt = 0;
        List<NinjaDataModel> ninjas = new List<NinjaDataModel>();
        switch (callBack.name)
        {
            case ("Human"):
                ninjas = Human;
                NinjaActionShow(Human);
                foundCnt = GetFoundCitizens(Human);
                noFoundCnt = GetNoFoundCitizens(Human);
                break;
            case ("Elf"):
                ninjas = Elf;
                NinjaActionShow(Elf);
                foundCnt = GetFoundCitizens(Elf);
                noFoundCnt = GetNoFoundCitizens(Elf);
                break;
            case ("Orc"):
                ninjas = Orc;
                NinjaActionShow(Orc);
                foundCnt = GetFoundCitizens(Orc);
                noFoundCnt = GetNoFoundCitizens(Orc);
                break;
            case ("Undead"):
                ninjas = Undead;
                NinjaActionShow(Undead);
                foundCnt = GetFoundCitizens(Undead);
                noFoundCnt = GetNoFoundCitizens(Undead);
                break;
            case ("Demon"):
                ninjas = Demon;
                NinjaActionShow(Demon);
                foundCnt = GetFoundCitizens(Demon);
                noFoundCnt = GetNoFoundCitizens(Demon);
                break;
            case ("Camp"):
                ShowSettlements(Camp);
                break;
            default:
                break;
        }
        LoadingPanel.SetActive(false);
        StopCoroutine(coroutine);
        switch (callBack.status)
        {
            case ("Search Started"):
                DisplayInfoPopup(Constants.INFO_TYPE_SUCCESS, Constants.INFO_TITLE_SUCCESS, "Search process was successfully started.");
                break;
            case ("Search successful"):
                // MessageHandler.userModel.citizens = callBack.totalCitizensCount;
                // // citizens.text = MessageHandler.userModel.citizens;
                // FoundCzPopup.SetActive(true);
                // NoFoundCzPopup.SetActive(false);
                // break;
            case ("Search failed"):
                string checked_str = String.Join(",", checked_items);
                bool check_flag = true;
                // Debug.Log("Checked list ===> " + checked_str);
                foreach (NinjaDataModel ninja in ninjas)
                {
                    if(checked_str.IndexOf(ninja.asset_id) >= 0) {
                        // Debug.Log(ninja.asset_id);
                        if(ninja.status == "holdup" || ninja.status == "holdup1") {
                            check_flag = false;
                            break;
                        }
                    }
                }

                if(check_flag == false) {
                    DisplayInfoPopup(Constants.INFO_TYPE_ERROR, 
                        Constants.INFO_TITLE_ERROR, 
                        "There were some errors while checking.\nPlease try again.");
                }
                else {
                    MessageHandler.userModel.citizens = callBack.totalCitizensCount;
                    PopupBulkSearchResultSuccessAmount.text = (foundCnt - success_cnt).ToString() + "x";
                    PopupBulkSearchResultFailAmount.text = (noFoundCnt - failed_cnt).ToString() + "x";

                    new_nft_name = "";
                    int new_nft_cnt = 0;

                    List<string> new_nft_names = new List<string>();
                    List<int> new_nft_cnts = new List<int>();


                    foreach(ExtraDataModel extra in MessageHandler.userModel.extraData) {
                        List<AssetModel> temp = new List<AssetModel>();
                        foreach(AssetModel data in oldExtra)
                        {
                            if(data.schema == extra.schema_name && data.template == extra.template_id) {
                                temp.Add(data);
                            }
                        }
                        AssetModel[] old_data = temp.ToArray();
                        temp.Clear();
                        for(int i = 0; i < MessageHandler.userModel.assets.Length; i++)
                        {
                            if(MessageHandler.userModel.assets[i].schema == extra.schema_name &&
                                MessageHandler.userModel.assets[i].template == extra.template_id) {
                                temp.Add(MessageHandler.userModel.assets[i]);
                            }
                        }
                        AssetModel[] new_data = temp.ToArray();
                        if(new_data.Length > old_data.Length)
                        {
                            new_nft_names.Add(new_data[0].name);
                            new_nft_cnts.Add(new_data.Length - old_data.Length);
                        }
                    }

                    if(new_nft_names.Count != 0)
                    {
                        PopupBulkSearchResultNFTName.transform.position = new Vector3(704, 288, 0);
                        PopupBulkSearchResultNFTName.text = "Found extra NFTs:";
                        string out_str = "";
                        for(int i = 0; i < new_nft_names.Count; i++){
                            if(i != 0) out_str += "\n";
                            out_str += "\"" + new_nft_names[i] + "\" " + new_nft_cnts[i].ToString() + "x";
                        }
                        PopupBulkSearchResultNFTAmount.text = out_str;
                    }
                    else
                    {
                        PopupBulkSearchResultNFTName.transform.position = new Vector3(704, 233, 0);
                        PopupBulkSearchResultNFTName.text = "Not found any\nextra NFTs";
                        PopupBulkSearchResultNFTAmount.text = "";
                    }
                    PopupBulkSearchResult.SetActive(true);
                }

                break;
            case ("Registered Successfully"):
                DisplayInfoPopup(Constants.INFO_TYPE_SUCCESS, Constants.INFO_TITLE_SUCCESS, "NFT was successfully registered.");
                break;
            case ("De-Registered Successfully"):
                DisplayInfoPopup(Constants.INFO_TYPE_SUCCESS, Constants.INFO_TITLE_SUCCESS, "NFT was successfully unregistered.");
                break;
            default:
                break;
        }

        CheckAllButton.GetComponent<Toggle>().isOn = false;
        success_cnt = foundCnt;
        failed_cnt = noFoundCnt;
    }

    public void BuyBtn()
    {
        Application.OpenURL(MessageHandler.userModel.drop);
    }
    public void FoundCzPopupOkayButtonClick()
    {
        onSetHeaderElements();
    }

    public void BuyUpgrades()
    {
        Application.OpenURL("https://wax-test.atomichub.io/market?collection_name=laxewneftyyy&schema_name=upgrades&template_id=282656");
    }


    public void CrossBtn()
    {
        SettlementParentPanel.SetActive(false);
        SettlementChildPanel.SetActive(false);
        UnregisteredSettlementChildPanel.SetActive(false);
        SettlementTextPanel.SetActive(false);
        SettlementBuyBtn.SetActive(false);
        SettlementDeregButton.SetActive(false);
        RegisteredSettlementChildPanel.SetActive(false);
        NoCamp_text.SetActive(true);
    }

    public void ChangeNinjaStatus()
    {
        if(checkAllClicked) return;
        itemCheckClicked = true;
        bool sel_flag = false;
        bool check_flag = true;
        foreach (Transform child in ContentTransform)
        {
            var obj = child.gameObject.GetComponent<NinjaStatus>();
            if(child.gameObject.GetComponent<Toggle>().isOn)
            {
                sel_flag = true;
                break;
            }
        }

        foreach (Transform child in ContentTransform)
        {
            check_flag = check_flag && child.gameObject.GetComponent<Toggle>().isOn;
        }

        if(sel_flag == true)
        {
            BulkButton.gameObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            BulkButton.gameObject.GetComponent<Button>().interactable = false;
            ActionGroup.SetActive(false);
        }

        CheckAllButton.GetComponent<Toggle>().isOn = check_flag;
        itemCheckClicked = false;
    }

    public void UpgradeButtonClick()
    {
        BulkButton.SetActive(false);
        CheckAllButton.SetActive(false);
        if(Camp.Count < 1){
            CampEmptyPanel.SetActive(true);
        } else {
            NinjaInfoPanel.SetActive(false);
            NinjsEmptyPanel.SetActive(false);
            ShowSettlements(Camp);
        }
    }

    private IEnumerator WaitAndClose(float waitTime) {
        float diff = 0;
        while(diff < waitTime)
        {
            yield return new WaitForSeconds(1f);
            diff++;
        }
        LoadingPanel.SetActive(false);
    }

    public void AddExtraData() 
    {
        ExtraDataModel[] extraData = MessageHandler.userModel.extraData;
        string extra_str = ":";
        // Debug.Log(extraData.Length.ToString());
        for(int j = 0; j < extraData.Length; j++)
        {
            // AssetModel[] asset = Array.FindAll(MessageHandler.userModel.assets, data => data.schema == extra.schema_name && data.template == extra.template_id);
            // AssetModel[] asset = MessageHandler.userModel.assets.Where(data => (data.schema == extra.schema_name && data.template == extra.template_id)).ToArray();
            for(int i = 0; i < MessageHandler.userModel.assets.Length; i++)
            {
                if(MessageHandler.userModel.assets[i].schema == extraData[j].schema_name &&
                    MessageHandler.userModel.assets[i].template == extraData[j].template_id) {
                    oldExtra.Add(MessageHandler.userModel.assets[i]);
                }
            }
            // oldExtra.AddRange(asset);
        }
    }

    private void OnErrorData(string errorData)
    {
        DisplayInfoPopup(Constants.INFO_TYPE_DEFAULT, Constants.INFO_TITLE_DEFAULT, errorData);
    }
}
