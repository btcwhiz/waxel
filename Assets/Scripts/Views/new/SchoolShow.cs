using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEditor;

public class SchoolShow : BaseView
{
    //public GameObject LoadingPanel;
    [Space]
    [Header("Button Manage")]
    public GameObject UpdateButton;

    [Space]
    [Header("BlendAction")]
    public GameObject ProfessionBlendingPanel;
    public GameObject TopBlendButton;
    public RawImage BlendPanelImage;
    public GameObject BlendButton;
    public GameObject ActionGroup;
    public GameObject BulkButton;
    public GameObject GroupWorkButton;
    public GameObject GroupCheckButton;
    public GameObject CheckAllButton;
    public TMP_Text BlendingInfoText;
    public TMP_Text BlendingTypeText;
    public TMP_Text BlendingUsesText;
    public TMP_Text BlendingCitizenText;
    public TMP_Text BlendingBooksText;

    [Space]
    [Header("BlendPopupAction")]
    public GameObject PopupBlendProfession;
    public GameObject PopupBlendButton;
    public GameObject PopupBlendBuyBookButton;
    public RawImage PopupBlendProfessionImage;
    public RawImage PopupBlendTopBookImage;
    public RawImage PopupBlendBottomBookImage;
    public TMP_Text PopupBlendCitizenCount;
    public TMP_Text PopupBlendBooksCount;
    public TMP_Text PopupBlendCitizenInfo;
    public TMP_Text PopupBlendBooksInfo;
    public TMP_Text PopupBlendProfessionInfo;

    [Space]
    [Header("BlendedPopupAction")]
    public GameObject PopupBlendedPanel;
    public RawImage PopupBlendedImage;
    public TMP_Text PopupBlendedInfo;

    [Space]
    [Header("Profession's Info")]
    public TMP_Text miner_count;
    public TMP_Text lumberjack_count;
    public TMP_Text farmer_count;
    public TMP_Text blacksmith_count;
    public TMP_Text carpenter_count;
    public TMP_Text tailor_count;
    public TMP_Text engineer_count;
    public GameObject ProfessionInfoPanel;
    public GameObject ProfessionShowPanel;
    public Transform ActionsPanel;
    public GameObject OneProfessionPrefab;
    public TMP_Text CountInfoText;
    public GameObject ProfessionEmptyPanel;
    public TMP_Text EmptyInfoText;

    [Space]
    [Header("Popup Work Result")]
    public GameObject WorkSuccessPopup;
    public TMP_Text WorkTitle;
    public TMP_Text WorkResultText;
    public RawImage WorkResultImage;

    [Space]
    [Header("Popup Bulk Work Result")]
    public GameObject PopupBulkWorkResult;
    public RawImage PopupBulkWorkResultCommonImage;
    public RawImage PopupBulkWorkResultUncommonImage;
    public RawImage PopupBulkWorkResultRareImage;
    public TMP_Text PopupBulkWorkResultCommonInfo;
    public TMP_Text PopupBulkWorkResultUncommonInfo;
    public TMP_Text PopupBulkWorkResultRareInfo;
    public TMP_Text PopupBulkWorkResultCommonAmount;
    public TMP_Text PopupBulkWorkResultUncommonAmount;
    public TMP_Text PopupBulkWorkResultRareAmount;
    public RawImage PopupBulkWorkResultNFTImage;
    public TMP_Text PopupBulkWorkResultNFTName;
    public TMP_Text PopupBulkWorkResultNFTAmount;

    [Space]
    [Header("Items")]
    public GameObject ItemsPopup;
    public Transform ItemParent;
    public GameObject ProfessionItemPrefab;
    public GameObject EquipItemPopup;
    public Transform EquipableItemParent;
    public GameObject EquipItemPrefab;
    public GameObject PopupEquipButtonActive;
    public GameObject PopupEquipButtonInactive;
    public GameObject EmptyEquipItemPopup;
    public GameObject UnequipAllPopup;
    public GameObject UnequipAllYesButton;
    public TMP_Text EmptyEquipItemPopupInfo1;
    public TMP_Text EmptyEquipItemPopupInfo2;
    public GameObject EmptyEquipItemPopupCraftButton;

    [Space]
    [Header("Refiner Action")]
    public GameObject RefinePanel;
    public Transform RefineParent;
    public GameObject RefineMaterialPrefab;
    public GameObject RefineHelloInfo;
    public GameObject RefineActionInfo;
    public RawImage RefineMaterialImage;
    public RawImage RefinedMaterialImage;
    public TMP_Text RefineMaterialText;
    public TMP_Text RefinedMaterialText;
    public GameObject PopupRefineButton;
    public RefineDataModel[] refineData;

    [Space]
    [Header("Crafter Action")]
    public GameObject CraftPanel;
    public GameObject CraftBlackActionTop;
    public GameObject CraftEngineerActionTop;
    public GameObject CraftHelloInfo;
    public GameObject CraftActionSeries;
    public GameObject CraftActionInfo;
    public GameObject PopupCraftButton;
    public GameObject CraftMaterialPrefab;

    [Space]
    [Header("Upgrade Action")]
    public GameObject ProfessionPanelParent;
    public GameObject SettlementEmptyPopup;
    public TMP_Text SettlementEmptyTitleText;
    public TMP_Text SettlementEmptyInfoText;
    public GameObject OneSettlementPrefab;
    public TMP_Text profession_text;
    public TMP_Text details_text;
    public TMP_Text types_text;
    public TMP_Text citizens_text;
    public TMP_Text uses_text;
    public TMP_Text books_text;
    public TMP_Text name_text;
    public TMP_Text text_permission;
    public TMP_Text done_panel_text;
    public GameObject NoForest_Text;
    public GameObject PermissionPanel;
    public GameObject DonePanel;
    public RawImage BlendingPanel_img;
    public RawImage done_panel_img;

    [Space]
    [Header("Data Models and Scripts")]
    public List<BlendingModel> BlendingData = new List<BlendingModel>();
    public ImgObjectView[] images;
    public ImgObjectView[] bookImages;
    public AbbreviationsHelper helper;
    private List<ProfessionDataModel> Engineer = new List<ProfessionDataModel>();
    private List<ProfessionDataModel> Miners = new List<ProfessionDataModel>();
    private List<ProfessionDataModel> Farmers = new List<ProfessionDataModel>();
    private List<ProfessionDataModel> Tailor = new List<ProfessionDataModel>();
    private List<ProfessionDataModel> Carpenter = new List<ProfessionDataModel>();
    private List<ProfessionDataModel> LumberJacks = new List<ProfessionDataModel>();
    private List<ProfessionDataModel> Blacksmith = new List<ProfessionDataModel>();
    private List<SettlementsModel> Mine = new List<SettlementsModel>();
    private List<SettlementsModel> Forest = new List<SettlementsModel>();
    private List<SettlementsModel> Field = new List<SettlementsModel>();

    private List<AssetModel> oldExtra = new List<AssetModel>();
    private List<string> checked_items = new List<string>();

    private bool bulkActionVisible = false;
    private IEnumerator coroutine;

    [Space]
    [Header("Other")]
    private string burn_asset_id;
    private string burn_type;
    private string professionSelectedFromWorkshop;

    private float common_cnt = 0;
    private float uncommon_cnt = 0;
    private float rare_cnt = 0;

    private bool init_flag = false;
    private string new_nft_name = "";
    private bool checkAllClicked = false;
    private bool itemCheckClicked = false;

    // setting the header
    public delegate void SetHeader();
    public static SetHeader onSetHeaderElements;

    protected override void Start()
    {
        init_flag = true;
        base.Start();
        SetModels(MessageHandler.userModel.professions);
        SetSettlementsModel(MessageHandler.userModel.settlements);
        SetUIElements();
        MessageHandler.OnProfessionData += OnProfessionData;
        MessageHandler.OnCallBackData += OnCallBackData;
        MessageHandler.OnSettlementData += OnSettlementData;
        MessageHandler.OnTransactionData += OnTransactionData;
        MessageHandler.OnItemData += OnItemData;
        ErrorHandler.OnErrorData += OnErrorData;

        if (WorkshopShow.SelectedProfession != null)
        {
            professionSelectedFromWorkshop = WorkshopShow.SelectedProfession;
            ProfessionButtonClick(professionSelectedFromWorkshop);
            WorkshopShow.SelectedProfession = null;
        }
        init_flag = false;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        MessageHandler.OnProfessionData -= OnProfessionData;
        MessageHandler.OnCallBackData -= OnCallBackData;
        MessageHandler.OnSettlementData -= OnSettlementData;
        MessageHandler.OnTransactionData -= OnTransactionData;
        MessageHandler.OnItemData -= OnItemData;
        ErrorHandler.OnErrorData -= OnErrorData;
    }

    private void SetModels(ProfessionDataModel[] professions)
    {
        Engineer.Clear();
        Miners.Clear();
        LumberJacks.Clear();
        Tailor.Clear();
        Farmers.Clear();
        Carpenter.Clear();
        Blacksmith.Clear();

        foreach (ProfessionDataModel profess in professions)
        {
            if (profess.name == "Engineer")
            {
                Engineer.Add(profess);
            }
            else if(profess.name == "Tailor")
            {
                Tailor.Add(profess);
            }
            else if (profess.name == "Miner")
            {
                Miners.Add(profess);
            }
            else if (profess.name == "Blacksmith")
            {
                Blacksmith.Add(profess);
            }
            else if (profess.name == "Lumberjack")
            {
                LumberJacks.Add(profess);
            }
            else if (profess.name == "Farmer")
            {
                Farmers.Add(profess);
            }
            else if (profess.name == "Carpenter")
            {
                Carpenter.Add(profess);
            }
        }

        // new_nft_founded = false;

        // if(init_flag == false)
        // {
        //     if(miner_count.text != "x" + Miners.Count.ToString()) new_nft_founded = true;
        //     if(lumberjack_count.text != "x" + LumberJacks.Count.ToString()) new_nft_founded = true;
        //     if(farmer_count.text != "x" + Farmers.Count.ToString()) new_nft_founded = true;
        //     if(blacksmith_count.text != "x" +Blacksmith.Count.ToString()) new_nft_founded = true;
        //     if(carpenter_count.text != "x" + Carpenter.Count.ToString()) new_nft_founded = true;
        //     if(tailor_count.text != "x" + Tailor.Count.ToString()) new_nft_founded = true;
        //     if(engineer_count.text != "x" + Engineer.Count.ToString()) new_nft_founded = true;
        // }
    }

    private void SetSettlementsModel(SettlementsModel[] settlements)
    {
        Mine.Clear();
        Forest.Clear();
        Field.Clear();

        foreach (SettlementsModel data in settlements)
        {
            switch (data.name)
            {
                case ("Mine"):
                    Mine.Add(data);
                    break;
                case ("Forest"):
                    Forest.Add(data);
                    break;
                case ("Field"):
                    Field.Add(data);
                    break;
                default:
                    break;
            }
        }
    }

    private void SetUIElements()
    {
        if (MessageHandler.userModel.account != null)
        {
            miner_count.text = "x" + Miners.Count.ToString();
            lumberjack_count.text = "x" + LumberJacks.Count.ToString();
            farmer_count.text = "x" + Farmers.Count.ToString();
            blacksmith_count.text = "x" +Blacksmith.Count.ToString();
            carpenter_count.text = "x" + Carpenter.Count.ToString();
            tailor_count.text = "x" + Tailor.Count.ToString();
            engineer_count.text = "x" + Engineer.Count.ToString();
        }
    }

    public void TopBlendButtonClick(string type)
    {
        ProfessionInfoPanel.SetActive(false);
        ProfessionShowPanel.SetActive(false);
        ProfessionEmptyPanel.SetActive(false);
        ProfessionBlendingPanel.SetActive(true);
        BulkButton.SetActive(false);
        CheckAllButton.SetActive(false);
        for (int j = 0; j < images.Length; j++)
        {
            if (images[j].name == type)
            {
                BlendPanelImage.texture = images[j].img;
                break;
            }
        }

        string citizens = "0 Citizens";

        foreach(ProfessionComboDataModel p in  MessageHandler.userModel.config.profession_combo)
        {
            if(p.profession == type) {
                citizens = p.citizens + " Citizens";
                break;
            }
        }

        foreach (BlendingModel m in BlendingData)
        {
            if(m.profession == type)
            {
                BlendingInfoText.text = m.info;
                BlendingTypeText.text = m.types;
                BlendingUsesText.text = m.uses;
                BlendingCitizenText.text = citizens;
                BlendingBooksText.text = m.books;
                break;
            }
        }
        BlendButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        BlendButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { BlendButtonClick(type); });

    }

    public void BlendButtonClick(string type)
    {
        PopupBlendProfession.SetActive(true);
        PopupBlendCitizenCount.text = "x" + MessageHandler.userModel.citizens;
        int bookCount = 0;
        string bookAssetID = "";
        string assetName = "";
        foreach(AssetModel data in MessageHandler.userModel.assets)
        {
            if(data.schema == "books"){
                if(data.name.IndexOf(type) != -1) {
                    if(bookCount == 0) bookAssetID = data.asset_id;
                    bookCount++;
                }
            }
        }

        PopupBlendBooksCount.text = "x" + bookCount.ToString();

        for (int j = 0; j < images.Length; j++)
        {
            if (images[j].name == type)
            {
                PopupBlendProfessionImage.texture = images[j].img;
                break;
            }
        }

        for (int j = 0; j < bookImages.Length; j++)
        {
            if (bookImages[j].name == type)
            {
                PopupBlendTopBookImage.texture = bookImages[j].img;
                PopupBlendBottomBookImage.texture = bookImages[j].img;
                break;
            }
        }

        string citizens = "0";

        foreach(ProfessionComboDataModel p in  MessageHandler.userModel.config.profession_combo)
        {
            if(p.profession == type) {
                citizens = p.citizens;
                break;
            }
        }

        foreach (BlendingModel m in BlendingData)
        {
            if(m.profession == type)
            {
                string citizenCount = m.citizens;
                citizenCount = citizenCount.Replace("Citizens", "");
                PopupBlendCitizenInfo.text = "Citizen\n" + citizens + "x";
                PopupBlendBooksInfo.text = "H.t.b. a " + type + "\n1x";
                PopupBlendProfessionInfo.text = type + "\n1x";
                PopupBlendBuyBookButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                PopupBlendBuyBookButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { GameObjectHandler.BuyDrop("Book"+ type); });
                if(bookCount >= 1 && Int64.Parse(MessageHandler.userModel.citizens) >= Int64.Parse(citizenCount)) {
                    PopupBlendButton.gameObject.GetComponent<Button>().interactable = true;
                    PopupBlendButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                    PopupBlendButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { Blend_Profession(bookAssetID, type); });
                }
                else{
                    PopupBlendButton.gameObject.GetComponent<Button>().interactable = false;
                }
                break;
            }
        }
    }

    public void ProfessionButtonClick(string type)
    {
        BulkButton.SetActive(true);
        ProfessionInfoPanel.SetActive(false);
        ProfessionBlendingPanel.SetActive(false);
        EmptyEquipItemPopup.SetActive(false);
        ItemsPopup.SetActive(false);
        ProfessionPanelParent.GetComponent<ProfessionUpgradeIndex>().upgrade_indexer = type;
        TopBlendButton.SetActive(true);
        TopBlendButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        TopBlendButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { TopBlendButtonClick(type); });
        switch (type)
        {
            case "Miner":
                UpdateButton.SetActive(true);
                SetProfessions(Miners, type);
                break;
            case "Lumberjack":
                UpdateButton.SetActive(true);
                SetProfessions(LumberJacks, type);
                break;
            case "Farmer":
                UpdateButton.SetActive(true);
                SetProfessions(Farmers, type);
                break;
            case "Engineer":
                SetProfessions(Engineer, type);
                UpdateButton.SetActive(false);
                break;
            case "Carpenter":
                UpdateButton.SetActive(false);
                SetProfessions(Carpenter, type);
                break;
            case "Tailor":
                UpdateButton.SetActive(false);
                SetProfessions(Tailor, type);
                break;
            case "Blacksmith":
                UpdateButton.SetActive(false);
                SetProfessions(Blacksmith, type);
                break;
            default:
                break;
        }

        if(type == "Miner" || type == "Lumberjack" || type == "Farmer") 
        {
            common_cnt = GetCommonAmount(type);
            uncommon_cnt = GetUnCommonAmount(type);
            rare_cnt = GetRareAmount(type);
        }
    }

    public void SetBlendingData(List<BlendingModel> blendingModel,string name)
    {
        var obj = GameObject.Find("ProfessionAsset(Clone)");
        if (obj != null)
        {
            Destroy(obj);
        }
        string maxCount = "";
        foreach (MaxNftDataModel nftData in MessageHandler.userModel.nft_count)
        {
            if (nftData.name == name)
            {
                maxCount = nftData.count;
                break;
            }
        }
        if(!string.IsNullOrEmpty(maxCount))
            profession_text.text = name + " - " + "0" + "/" + maxCount;
        else 
            profession_text.text = name + " - " + "0" + "/" + "10";
        foreach (BlendingModel bmodel in blendingModel)
        {
            if(bmodel.profession == name)
            {
                int temp = 0;
                for (int j = 0; j < images.Length; j++)
                {
                    if (images[j].name == name)
                    {
                        temp = j;
                        break;
                    }
                }
                BlendingPanel_img.texture = images[temp].img;
                name_text.text = name;
                details_text.text = "Every couple of hours you will be able to refine raw materials like \"Copper Ore\" (common), \"Tin Ore\" (uncommon) and \"Iron Ore\" (rare) and craft items. You will then be able to either trade, sell or refine these raw materials.";
                uses_text.text = bmodel.uses;
                types_text.text = bmodel.types;
                citizens_text.text = bmodel.citizens;
                books_text.text = bmodel.books;
                break;
            }
        }
    }

    public void WorkButtonClick(string assetId, string type)
    {
        LoadingPanel.SetActive(true);
        coroutine = WaitAndClose(Config.configData.TIME_LOADING_PANEL);
        StartCoroutine(coroutine);
        MessageHandler.Server_SearchCitizen(assetId, "1", type);
    }

    public void BurnButtonClick(string assetId, string type, int item_count)
    {
        // LoadingPanel.SetActive(true);
        // coroutine = WaitAndClose(Config.configData.TIME_LOADING_PANEL);
        // StartCoroutine(coroutine);
        // MessageHandler.Server_BurnProfession(type,assetId);
        if (item_count == 0) {
            Show_BurnPanel(assetId, type);
        }
        else {
            DisplayInfoPopup(Constants.INFO_TYPE_DEFAULT, Constants.INFO_TITLE_DEFAULT, "Unable to \"Burn\" as not all selected professions have the same status.");
        }
    }

    public void SetWorkResult(string title, string result, Texture image){
        WorkSuccessPopup.SetActive(true);
        WorkTitle.text = title;
        WorkResultImage.texture = image;
        WorkResultText.text = result;
    }

    public void SetProfessions(List<ProfessionDataModel> professionModel, string type)
    {
        BulkButton.SetActive(true);
        CheckAllButton.SetActive(true);
        if(type == "Miner" || type == "Lumberjack" || type == "Farmer") 
        {
            GroupWorkButton.SetActive(true);
            GroupCheckButton.SetActive(true);
        }
        else
        {
            GroupWorkButton.SetActive(false);
            GroupCheckButton.SetActive(false);
        }
        string maxCount = "10";
        int registeredCount = 0;
        foreach (MaxNftDataModel nftData in MessageHandler.userModel.nft_count)
        {
            if (nftData.name == type)
            {
                maxCount = nftData.count;
                break;
            }
        }
        if (professionModel.Count < 1)
        {
            ProfessionShowPanel.SetActive(false);
            ProfessionBlendingPanel.SetActive(false);
            ProfessionEmptyPanel.SetActive(true);
            BulkButton.SetActive(false);
            CheckAllButton.SetActive(false);
            EmptyInfoText.text = "Unfortunately you don't have any \"" + type + "s\".\n\nYou could create one by using the \"Blend\" button or obtain one by using the \"Buy\" button at the top right of this menu.";
        }
        else
        {
            ProfessionEmptyPanel.SetActive(false);
            ProfessionBlendingPanel.SetActive(false);
            ProfessionShowPanel.SetActive(true);
            BulkButton.gameObject.GetComponent<Button>().interactable = false;
            ActionGroup.SetActive(false);
            if (ActionsPanel.childCount >= 1)
            {
                foreach (Transform child in ActionsPanel)
                {
                    GameObject.Destroy(child.gameObject);
                }
            }
            for (int i = 0; i < professionModel.Count; i++)
            {
                var ins = Instantiate(OneProfessionPrefab);
                ins.transform.SetParent(ActionsPanel);
                ins.transform.localScale = new Vector3(1, 1, 1);
                var child = ins.gameObject.GetComponent<OneProfessionStatus>();
                child.assetId = professionModel[i].asset_id;
                child.AssetIdText.text = "#" + professionModel[i].asset_id.ToString();
                child.type = professionModel[i].name;
                child.status = professionModel[i].status;
                // Update equiped item count of current profession
                child.item_count = professionModel[i].items.Length;
                child.GetComponent<Toggle>().onValueChanged.RemoveAllListeners();
                child.GetComponent<Toggle>().onValueChanged.AddListener(delegate { ChangeProfessionStatus(); });
                child.Check.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                child.Check.gameObject.GetComponent<Button>().onClick.AddListener(delegate { CheckButtonClick(child); });
                child.Register.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                child.Register.gameObject.GetComponent<Button>().onClick.AddListener(delegate { RegisterButtonClick(); });
                child.Registered.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                child.Registered.gameObject.GetComponent<Button>().onClick.AddListener(delegate { UnregisterButtonClick(); });
                // child.ActionBtn.gameObject.GetComponent<Button>().onClick.AddListener(delegate { ActionButtonClick(); });
                for (int j = 0; j < images.Length; j++)
                {
                    if (images[j].name == child.type)
                    {
                        child.img.texture = images[j].img;
                        break;
                    }
                }
                child.UseLeftCount.text = professionModel[i].uses_left.ToString();
                if(professionModel[i].items.Length == 0) 
                {
                    child.Registered.gameObject.GetComponent<Button>().interactable = true;
                }
                else 
                {
                    child.Registered.gameObject.GetComponent<Button>().interactable = false;
                }

                if (professionModel[i].reg == "0")
                {
                    child.Register.SetActive(true);
                    child.Seller.SetActive(true);
                }
                else if (professionModel[i].reg == "1")
                {
                    registeredCount += 1;
                    if (child.type == "Farmer" || child.type == "Miner" || child.type =="Lumberjack") {
                        child.ItemInfo.text = "Item : " + professionModel[i].items.Length.ToString();
                        child.action_text.text = "Work";
                    } else if (child.type == "Carpenter"|| child.type == "Tailor" || child.type == "Blacksmith"){
                        child.ItemInfo.text = "Craft";
                        child.action_text.text = "Refine";
                        child.Seller.gameObject.GetComponent<Button>().interactable = false;
                    } else {
                        child.action_text.text = "Craft";
                        child.Seller.gameObject.GetComponent<Button>().interactable = false;
                    }

                    // Debug.Log(child.assetId + ":" + child.type + ":" + professionModel[i].status + ":" + professionModel[i].last_material_search);

                    if (professionModel[i].status == "Idle " || professionModel[i].last_material_search == "1970-01-01T00:00:00")
                    {
                        child.Check.SetActive(false);
                        child.Registered.SetActive(true);
                        if (child.type == "Farmer" || child.type == "Miner" || child.type =="Lumberjack"){
                            child.ItemSeller.SetActive(true);
                            string[] items = professionModel[i].items;
                            child.ActionBtn.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                            child.ActionBtn.gameObject.GetComponent<Button>().onClick.AddListener(delegate { WorkButtonClick(child.assetId, child.type);});
                            child.ItemBtn.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                            child.ItemBtn.gameObject.GetComponent<Button>().onClick.AddListener(delegate { ItemButtonClick(items, child.type, child.assetId); }); 
                        } else if (child.type == "Blacksmith"){
                            child.ItemSeller.SetActive(true);
                            child.ActionBtn.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                            child.ActionBtn.gameObject.GetComponent<Button>().onClick.AddListener(delegate { RefineButtonClick(child.assetId, child.type);});
                            child.ItemBtn.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                            child.ItemBtn.gameObject.GetComponent<Button>().onClick.AddListener(delegate { CraftButtonClick(child.assetId, child.type); });
                        }
                        else if (child.type == "Carpenter"|| child.type == "Tailor")
                        {
                            child.Seller.SetActive(true);
                            child.ActionBtn.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                            child.ActionBtn.gameObject.GetComponent<Button>().onClick.AddListener(delegate { RefineButtonClick(child.assetId , child.type); });
                        }
                        else
                        {
                            child.Seller.SetActive(true);
                            child.ActionBtn.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                            child.ActionBtn.gameObject.GetComponent<Button>().onClick.AddListener(delegate { CraftButtonClick(child.assetId, child.type); });
                        }

                        if(professionModel[i].uses_left == "0") 
                        {
                            child.Registered.SetActive(false);
                            child.BurnBtn.SetActive(true);
                            child.BurnBtn.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                            child.BurnBtn.gameObject.GetComponent<Button>().onClick.AddListener(delegate { BurnButtonClick(child.assetId, child.type, child.item_count); });
                            // child.BurnBtn.gameObject.GetComponent<Button>().onClick.AddListener(delegate { Show_BurnPanel(child.assetId, child.type); });
                            child.ActionBtn.gameObject.GetComponent<Button>().interactable = false;
                            if(child.type == "Blacksmith")
                            {
                                child.ItemBtn.gameObject.GetComponent<Button>().interactable = false;
                            }
                        }
                    }
                    else
                    {
                        child.Timer.SetActive(true);
                        if (child.type == "Engineer")
                        {
                            child.Seller.SetActive(true);
                            child.Seller.gameObject.GetComponent<Button>().interactable = false;
                            string[] arr = professionModel[i].status.Split('%');
                            foreach (Config_CraftComboModel data_model in MessageHandler.userModel.craft_combos)
                            {
                                if (data_model.template_id == arr[1])
                                {
                                    child.craft_time = int.Parse(data_model.delay);
                                    break;
                                }
                            }
                            child.StartTimer(professionModel[i].last_material_search, child.craft_time);
                        }
                        else if (child.type == "Carpenter"|| child.type == "Tailor")
                        {
                            child.Seller.SetActive(true);
                            child.ItemBtn.gameObject.GetComponent<Button>().interactable = false;
                            child.StartTimer(professionModel[i].last_material_search, MessageHandler.userModel.refining_delay);
                        }
                        else
                        {
                            // Debug.Log(professionModel[i].status + "+++++++++++++++++++++++++");
                            if(child.type == "Blacksmith" && professionModel[i].status.Contains("Crafting"))
                            {
                                child.craft = true;
                                string[] arr = professionModel[i].status.Split('%');
                                foreach (Config_CraftComboModel data_model in MessageHandler.userModel.craft_combos)
                                {
                                    if (data_model.template_id == arr[1])
                                    {
                                        child.craft_time = int.Parse(data_model.delay);
                                        break;
                                    }
                                }
                                // Debug.Log(professionModel[i].status + "====================");
                            }

                            if(child.type == "Blacksmith")
                            {
                                if(professionModel[i].status.Contains("Crafting"))
                                {
                                    child.StartTimer(professionModel[i].last_material_search, child.craft_time);
                                }
                                else // Refining
                                {
                                    child.StartTimer(professionModel[i].last_material_search, MessageHandler.userModel.refining_delay);
                                }
                            }
                            else // Gatherer mining...
                            {
                                child.StartTimer(professionModel[i].last_material_search, MessageHandler.userModel.finding_delay);
                            }

                            child.ItemSeller.SetActive(true);
                            child.Timer.SetActive(true);
                            child.ItemBtn.gameObject.GetComponent<Button>().interactable = false;
                        }
                    }
                }
            }
        }
        // setting the header of panel
        if (type == "Farmer" || type == "Miner" || type =="Lumberjack"){
            CountInfoText.text = type + " - " + registeredCount.ToString() + "/" + maxCount;
        } else {
            CountInfoText.text = type + " - " + registeredCount.ToString() + "/" + professionModel.Count;
        }

        CheckAllButton.GetComponent<Toggle>().isOn = false;
    }

    public void Show_BurnPanel(string asset_id, string type)
    {
        PermissionPanel.SetActive(true);
        text_permission.text = "Do you really want to burn the selected profession(s)?";
        burn_asset_id = asset_id;
        burn_type = type;
    }

    public void BurnYesClick() {
        LoadingPanel.SetActive(true);
        BulkButton.gameObject.GetComponent<Button>().interactable = false;
        coroutine = WaitAndClose(Config.configData.TIME_LOADING_PANEL);
        StartCoroutine(coroutine);
        MessageHandler.Server_BurnProfession(burn_type, burn_asset_id);
    }

    public void ItemButtonClick(string[] item_ids,string profession_name,string profession_id)
    {
        ItemsPopup.SetActive(true);
        if (ItemParent.childCount >= 1)
        {
            foreach (Transform p in ItemParent)
            {
                GameObject.Destroy(p.gameObject);
            }
        }
        var ins = Instantiate(ProfessionItemPrefab);
        ins.transform.SetParent(ItemParent);
        ins.transform.localScale = new Vector3(1, 1, 1);
        // make the prefab's ui correct
        var child = ins.gameObject.GetComponent<ProfessionItemStatus>();
        child.professionName = profession_name;
        child.prefessionId.text = " #" + profession_id;
        foreach(ProfessionItemSelect t in child.EachItems)
        {
            foreach(ImgObjectView b in images)
            {
                if(b.name == profession_name + "Default" + t.type)
                {
                    t.tool_img.texture = b.img;
                }
            }
            t.EquipButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
            t.EquipButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { EquipButtonClick(t.type, profession_id, child.professionName); });
        }

        foreach (ImgObjectView data in images)
        {
            if (profession_name == data.name)
            {
                child.image.texture = data.img;
                break;
            }
        }

        // item_ids: ids of item in the contract
        for (int i = 0; i < item_ids.Length; i++)
        {

            foreach (ItemDataModel m in MessageHandler.userModel.items)
            {
                if(m.asset_id == item_ids[i])
                {
                    foreach(ImgObjectView data in images)
                    {
                        if(m.name == data.name)
                        {
                            foreach(ProfessionItemSelect r in child.EachItems)
                            {
                                if(r.type == m.function_name)
                                {
                                    r.tool_img.texture = data.img;
                                    r.EquipButton.SetActive(false);
                                    r.UnequipButton.SetActive(true);
                                    r.UnequipButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                                    r.UnequipButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { UnequipButtonClick(m.asset_id, m.name, profession_id); });
                                    r.UseLeftText.text = m.uses_left;
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    break;
                }
            }
        }

        if(item_ids.Length > 0) {
            child.UnequipAllButton.gameObject.GetComponent<Button>().interactable = true;
            child.UnequipAllButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
            // child.UnequipAllButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { UnequipButtonClick(string.Join(",", item_ids), "", profession_id); });
            child.UnequipAllButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { UnequipAllButtonClick(string.Join(",", item_ids), "", profession_id); });
        }
        else {
            child.UnequipAllButton.gameObject.GetComponent<Button>().interactable = false;
            child.UnequipAllButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }

    public void EquipButtonClick(string selectedItemType, string profession_id, string professionName)
    {
        PopupEquipButtonInactive.SetActive(true);
        PopupEquipButtonActive.SetActive(false);
        int itemCount = 0;
        // selectedItemType: Luck = More rare materials || Double = Luck / 2nd material || Extra = More materials
        if (EquipableItemParent.childCount >= 1)
        {
            foreach (Transform c in EquipableItemParent)
            {
                GameObject.Destroy(c.gameObject);
            }
        }

        string[] item_names = helper.profession_equip_items[professionName];// get available items for profession of kind

        foreach (string n in item_names)
        {
            foreach (ItemDataModel t in MessageHandler.userModel.items)
            {
                if (t.equipped == "0" && n == t.name && t.function_name == selectedItemType)
                {
                    if(t.uses_left != "0")
                    {
                        itemCount++;
                        var ins = Instantiate(EquipItemPrefab);
                        ins.transform.SetParent(EquipableItemParent);
                        ins.transform.localScale = new Vector3(1, 1, 1);
                        var child = ins.gameObject.GetComponent<EquipItemStatus>();
                        child.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                        child.gameObject.GetComponent<Button>().onClick.AddListener(delegate { ItemImageClick(t.asset_id, profession_id); });
                        child.UseLeftCount.text = "x" + t.uses_left;
                        foreach (ImgObjectView m in images)
                        {
                            if (t.name == m.name)
                            {
                                child.image.texture = m.img;
                                break;
                            }
                        }
                    }
                }
            }
        }
        if (itemCount < 1)
        {
            string itemCanBeCraftedByProfession = "";
            if (selectedItemType == ("Extra")) {
			    itemCanBeCraftedByProfession = "Engineer";
            } else {
                itemCanBeCraftedByProfession = "Blacksmith";
            }
            EquipItemPopup.SetActive(false);
            EmptyEquipItemPopup.SetActive(true);
            string itemType = "";
            if(selectedItemType == "Luck") {
                itemType = "Rarity";
            } else if (selectedItemType == "Double") {
                itemType = "Luck";
            } else {
                itemType = "Extra";
            }
            EmptyEquipItemPopupInfo1.text = "No item of type \"" + itemType + "\" for \""+ professionName +"\" profession in your wallet.";
            EmptyEquipItemPopupCraftButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
            EmptyEquipItemPopupCraftButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { ProfessionButtonClick(itemCanBeCraftedByProfession);} );
        }
        else
        {
            EquipItemPopup.SetActive(true);
            EmptyEquipItemPopup.SetActive(false);
        }
    }

    public void ItemImageClick(string assetId, string professionId)
    {
        // Debug.Log("ItemImageClick");
        // PopupEquipButton.gameObject.GetComponent<Button>().interactable = true;
        PopupEquipButtonInactive.SetActive(false);
        PopupEquipButtonActive.SetActive(true);
        PopupEquipButtonActive.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        PopupEquipButtonActive.gameObject.GetComponent<Button>().onClick.AddListener(delegate { PopupEquipButtonClick(assetId, professionId); });
        // equipableItemsPopup.equip_id = id;
    }

    public void PopupEquipButtonClick(string assetId, string professionId)
    {
        // Debug.Log(assetId);
        // Debug.Log(professionId);
        // Debug.Log("PopupEquipButtonClick");
        // Debug.Log(p_id);
        if (!string.IsNullOrEmpty(assetId))
        {
            LoadingPanel.SetActive(true);
            coroutine = WaitAndClose(Config.configData.TIME_LOADING_PANEL);
            StartCoroutine(coroutine);
            MessageHandler.Server_EquipItems(professionId, assetId);
        }
        else {
            // SSTools.ShowMessage("No item was selected to equip.", SSTools.Position.bottom, SSTools.Time.twoSecond);
            // InfoPopup.SetActive(true);
            // InfoText.text = "No item was selected to equip";
            DisplayInfoPopup(Constants.INFO_TYPE_DEFAULT, Constants.INFO_TITLE_DEFAULT, "No item was selected to equip.");
        }
    }

    public void UnequipAllButtonClick(string item_id, string item_name, string profession_id)
    {
        UnequipAllPopup.SetActive(true);
        UnequipAllYesButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        UnequipAllYesButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { UnequipButtonClick(item_id, item_name, profession_id); });
    }

    public void UnequipButtonClick(string item_id, string item_name, string profession_id)
    {
        if (!string.IsNullOrEmpty(item_id))
        {
            LoadingPanel.SetActive(true);
            coroutine = WaitAndClose(Config.configData.TIME_LOADING_PANEL);
            StartCoroutine(coroutine);
            MessageHandler.Server_UnequipItems(item_id, item_name, profession_id);
        }
        else {
            DisplayInfoPopup(Constants.INFO_TYPE_DEFAULT, Constants.INFO_TITLE_DEFAULT, "No item was selected to unequip.");
        }
    }

    public void OnProfessionData(ProfessionDataModel[] profession)
    {
        SetModels(profession);
    }

    public void OnCallBackData(CallBackDataModel[] callback)
    {
        LoadingPanel.SetActive(false);
        StopCoroutine(coroutine);
        CallBackDataModel callBack = callback[0];
        // if(callBack.status == "Blended Successfully")
        // {
        //     return;
        // }
        List<ProfessionDataModel> profess = new List<ProfessionDataModel>();
        switch (callBack.name)
        {
            case "Miner":
                profess = Miners;
                SetProfessions(Miners, callBack.name);
                break;
            case "Farmer":
                profess = Farmers;
                SetProfessions(Farmers, callBack.name);
                break;
            case "Engineer":
                SetProfessions(Engineer, callBack.name);
                break;
            case "Carpenter":
                SetProfessions(Carpenter, callBack.name);
                break;
            case "Tailor":
                SetProfessions(Tailor, callBack.name);
                break;
            case "Blacksmith":
                SetProfessions(Blacksmith, callBack.name);
                break;
            case "Lumberjack":
                profess = LumberJacks;
                SetProfessions(LumberJacks, callBack.name);
                break;
            case ("Mine"):
                ShowSettlements(Mine, callBack.name);
                break;
            case ("Forest"):
                ShowSettlements(Forest, callBack.name);
                break;
            case ("Field"):
                ShowSettlements(Field, callBack.name);
                break;
            default:
                break;
        }

        switch (callBack.status)
        {
            case ("Registered Successfully"):
                DisplayInfoPopup(Constants.INFO_TYPE_SUCCESS, Constants.INFO_TITLE_SUCCESS, "NFT was successfully registered.");
                break;
            case ("De-Registered Successfully"):
                DisplayInfoPopup(Constants.INFO_TYPE_SUCCESS, Constants.INFO_TITLE_SUCCESS, "NFT was successfully unregistered.");
                break;
            case ("Item Equipped"):
                DisplayInfoPopup(Constants.INFO_TYPE_SUCCESS, Constants.INFO_TITLE_SUCCESS, "Item was successfully equipped.");
                break;
            case ("De-Equiped"):
                DisplayInfoPopup(Constants.INFO_TYPE_SUCCESS, Constants.INFO_TITLE_SUCCESS, "Item(s) successfully unequipped.");
                break;
            case ("Work Started"):
                DisplayInfoPopup(Constants.INFO_TYPE_SUCCESS, Constants.INFO_TITLE_SUCCESS, "Working process was successfully started.");
                break;
            case ("Refining Started"):
                DisplayInfoPopup(Constants.INFO_TYPE_SUCCESS, Constants.INFO_TITLE_SUCCESS, "Refining process was successfully started.");
                break;
            case ("Crafting Started"):
                DisplayInfoPopup(Constants.INFO_TYPE_SUCCESS, Constants.INFO_TITLE_SUCCESS, "Crafting process was successfully started.");
                break;
            case ("RNG Failed !"):
                DisplayInfoPopup(Constants.INFO_TYPE_DEFAULT, Constants.INFO_TITLE_DEFAULT, "RNG failed. Most probably the global WAX blockchain RNG services is struggling at the moment.");
                break;
            case ("Burnt Successfully"):
                DisplayInfoPopup(Constants.INFO_TYPE_SUCCESS, Constants.INFO_TITLE_SUCCESS, "Item was successfully burned.");
                break;
            case ("Blended Successfully"):
                PopupBlendedInfo.text = "You successfully blended a \"" + callBack.name + "\"!";
                MessageHandler.userModel.citizens = callBack.totalCitizensCount;
                for(int j = 0; j < images.Length; j++)
                {
                    if(images[j].name == callBack.name)
                    {
                        PopupBlendedImage.texture = images[j].img;
                        break;
                    }
                }
                PopupBlendedPanel.SetActive(true);
                break;
            default:
                break;
        }

        if (!string.IsNullOrEmpty(callBack.matFound) && callBack.matFound == "true" && !string.IsNullOrEmpty(callBack.matRefined) && callBack.matRefined == "false")
        {
            MessageHandler.userModel.total_matCount = callBack.totalMatCount;
            float common_val = 0, uncommon_val = 0, rare_val = 0;

            if((callBack.name == "Miner" || callBack.name == "Lumberjack" || callBack.name == "Farmer")) {

                // foreach(ImgObjectView i in images)
                // {
                //     if(i.name == callBack.name)
                //     {
                //         PopupBulkWorkResultNFTImage.texture = i.img;
                //         break;
                //     }
                // }

                string checked_str = String.Join(",", checked_items);
                bool check_flag = true;
                // Debug.Log("check list ===> " + checked_str);
                for(int i = 0; i < profess.Count; i++)
                {
                    if(checked_str.IndexOf(profess[i].asset_id) >= 0)
                    {
                        // Debug.Log(profess[i].asset_id);
                        // Debug.Log(profess[i].status);
                        if(profess[i].status == "holdup" || profess[i].status == "holdup1") {
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
                    new_nft_name = "";
                    int new_nft_cnt = 0;

                    List<string> new_nft_names = new List<string>();
                    List<int> new_nft_cnts = new List<int>();


                    foreach(ExtraDataModel extra in MessageHandler.userModel.extraData) {
                        AssetModel[] old_data = oldExtra.Where(
                            data => (data.schema == extra.schema_name && data.template == extra.template_id))
                            .ToArray();
                        AssetModel[] new_data = MessageHandler.userModel.assets.Where(
                            data => (data.schema == extra.schema_name && data.template == extra.template_id))
                            .ToArray();
                        if(new_data.Length > old_data.Length)
                        {
                            new_nft_names.Add(new_data[0].name);
                            new_nft_cnts.Add(new_data.Length - old_data.Length);
                        }
                    }

                    if(new_nft_names.Count != 0)
                    {
                        PopupBulkWorkResultNFTName.transform.position = new Vector3(704, 218, 0);
                        PopupBulkWorkResultNFTName.text = "Found extra NFTs:";
                        string out_str = "";
                        for(int i = 0; i < new_nft_names.Count; i++){
                            if(i != 0) out_str += "\n";
                            out_str += "\"" + new_nft_names[i] + "\" " + new_nft_cnts[i].ToString() + "x";
                        }
                        PopupBulkWorkResultNFTAmount.text = out_str;
                    }
                    else
                    {
                        PopupBulkWorkResultNFTName.transform.position = new Vector3(704, 173, 0);
                        // Not active
                        PopupBulkWorkResultNFTName.text = "Currently no extra NFT(s) can be found.\nStay tuned for the next update.";
                        // Not found
                        // PopupBulkWorkResultNFTName.text = "Not found any\nextra NFTs";
                        PopupBulkWorkResultNFTAmount.text = "";
                    }

                    switch (callBack.name)
                    {
                        case "Miner":
                            common_val = GetCommonAmount(callBack.name);
                            uncommon_val = GetUnCommonAmount(callBack.name);
                            rare_val = GetRareAmount(callBack.name);
                            foreach(ImgObjectView data in images)
                            {
                                if(data.name == "CO") PopupBulkWorkResultCommonImage.texture = data.img;
                                if(data.name == "TO") PopupBulkWorkResultUncommonImage.texture = data.img;
                                if(data.name == "IO") PopupBulkWorkResultRareImage.texture = data.img;
                            }
                            PopupBulkWorkResultCommonInfo.text = "Found \"Copper Ore\"";
                            PopupBulkWorkResultUncommonInfo.text = "Found \"Tin Ore\"";
                            PopupBulkWorkResultRareInfo.text = "Found \"Iron Ore\"";
                            PopupBulkWorkResultCommonAmount.text = (common_val - common_cnt).ToString("0.00") + "x";
                            PopupBulkWorkResultUncommonAmount.text = (uncommon_val - uncommon_cnt).ToString("0.00") + "x";
                            PopupBulkWorkResultRareAmount.text = (rare_val - rare_cnt).ToString("0.00") + "x";
                            PopupBulkWorkResult.SetActive(true);
                            break;
                        case "Lumberjack":
                            common_val = GetCommonAmount(callBack.name);
                            uncommon_val = GetUnCommonAmount(callBack.name);
                            rare_val = GetRareAmount(callBack.name);
                            foreach(ImgObjectView data in images)
                            {
                                if(data.name == "BIRCH") PopupBulkWorkResultCommonImage.texture = data.img;
                                if(data.name == "OAK") PopupBulkWorkResultUncommonImage.texture = data.img;
                                if(data.name == "TEAK") PopupBulkWorkResultRareImage.texture = data.img;
                            }
                            PopupBulkWorkResultCommonInfo.text = "Found \"Birch\"";
                            PopupBulkWorkResultUncommonInfo.text = "Found \"Oak\"";
                            PopupBulkWorkResultRareInfo.text = "Found \"Teak\"";
                            PopupBulkWorkResultCommonAmount.text = (common_val - common_cnt).ToString("0.00") + "x";
                            PopupBulkWorkResultUncommonAmount.text = (uncommon_val - uncommon_cnt).ToString("0.00") + "x";
                            PopupBulkWorkResultRareAmount.text = (rare_val - rare_cnt).ToString("0.00") + "x";
                            PopupBulkWorkResult.SetActive(true);
                            break;
                        case "Farmer":
                            common_val = GetCommonAmount(callBack.name);
                            uncommon_val = GetUnCommonAmount(callBack.name);
                            rare_val = GetRareAmount(callBack.name);
                            foreach(ImgObjectView data in images)
                            {
                                if(data.name == "COTTON") PopupBulkWorkResultCommonImage.texture = data.img;
                                if(data.name == "FLAX") PopupBulkWorkResultUncommonImage.texture = data.img;
                                if(data.name == "SWORMS") PopupBulkWorkResultRareImage.texture = data.img;
                            }
                            PopupBulkWorkResultCommonInfo.text = "Found \"Cotton\"";
                            PopupBulkWorkResultUncommonInfo.text = "Found \"Flax\"";
                            PopupBulkWorkResultRareInfo.text = "Found \"Silkworms\"";
                            PopupBulkWorkResultCommonAmount.text = (common_val - common_cnt).ToString("0.00") + "x";
                            PopupBulkWorkResultUncommonAmount.text = (uncommon_val - uncommon_cnt).ToString("0.00") + "x";
                            PopupBulkWorkResultRareAmount.text = (rare_val - rare_cnt).ToString("0.00") + "x";
                            PopupBulkWorkResult.SetActive(true);
                            break;
                        default:
                            break;
                    }
                }
            }

            // if (!string.IsNullOrEmpty(callBack.matName) || !string.IsNullOrEmpty(callBack.matCount))
            // {
            //     MessageHandler.userModel.total_matCount = callBack.totalMatCount;
            //     string title = "Work Result";
            //     Texture b = null;
            //     string result = "";
            //     foreach(ImgObjectView data in images)
            //     {
            //         if(data.name == callBack.matName)
            //         {
            //             b = data.img;
            //             decimal matCount =  Math.Round(System.Convert.ToDecimal(callBack.matCount), 2);
            //             result = "You found " + matCount.ToString() + "x \"" + helper.mat_abv[callBack.matName] + "\"!";
            //             break;
            //         }
            //     }
            //     SetWorkResult(title, result, b);
            // }
        }
        else if (!string.IsNullOrEmpty(callBack.matFound) && !string.IsNullOrEmpty(callBack.matRefined) && callBack.matFound == "false" && callBack.matRefined == "true")
        {
            if (!string.IsNullOrEmpty(callBack.matName))
            {
                MessageHandler.userModel.total_matCount = callBack.totalMatCount;
                string title = "Refine Result";
                Texture b = null;
                string result = "";
                string refined_mat = helper.mat_abv[callBack.matName];
                foreach (RefineDataModel refine_mat in refineData)
                {
                    if (refine_mat.name == refined_mat)
                    {
                        b = refine_mat.refined_product_img;
                        result = "You successfully refined 3x \""+ refine_mat.name + "\" " + "into 1x \"" + refine_mat.refined_product +"\".";
                        break;
                    }
                }
                SetWorkResult(title, result, b);
            }
        }
        else if (!string.IsNullOrEmpty(callBack.matFound) && !string.IsNullOrEmpty(callBack.matRefined) && !string.IsNullOrEmpty(callBack.matCrafted) && callBack.matFound == "false" && callBack.matRefined == "false" && callBack.matCrafted == "true")
        {

            if (!string.IsNullOrEmpty(callBack.matName))
            {
                MessageHandler.userModel.total_matCount = callBack.totalMatCount;
                string title = "Craft Result";
                Texture b = null;
                string result = "";
                foreach(ImgObjectView img in images)
                {
                    if(img.name == callBack.matName)
                    {
                        b = img.img;
                        result = "You successfully crafted \"" + callBack.matName + "\".";
                        break;
                    }
                }
                SetWorkResult(title, result, b);
            }
        }
        else if(!string.IsNullOrEmpty(callBack.matFound) && !string.IsNullOrEmpty(callBack.matRefined) && callBack.matFound == "false" && callBack.matRefined == "false" && callBack.equipped == "1")
        {
            ItemButtonClick(callBack.items_ids,callBack.name,callBack.asset_id);
        }

        else if (!string.IsNullOrEmpty(callBack.matFound) && !string.IsNullOrEmpty(callBack.matRefined) && callBack.matFound == "false" && callBack.matRefined == "false" && callBack.equipped == "0")
        {

            ItemButtonClick(callBack.items_ids, callBack.name, callBack.asset_id);
        }
        SetUIElements();
        onSetHeaderElements();

        if(callBack.name == "Miner" || callBack.name == "Lumberjack" || callBack.name == "Farmer")
        {
            common_cnt = GetCommonAmount(callBack.name);
            uncommon_cnt = GetUnCommonAmount(callBack.name);
            rare_cnt = GetRareAmount(callBack.name);
        }
    }

    public void RefineButtonClick(string profession_id, string profession)
    {
        RefinePanel.SetActive(true);
        RefineActionInfo.SetActive(false);
        RefineHelloInfo.SetActive(true);
        PopupRefineButton.gameObject.GetComponent<Button>().interactable = false;
        if (RefineParent.childCount >= 1)
        {
            foreach (Transform child in RefineParent)
            {
                Destroy(child.gameObject);
            }
        }
        foreach (RefineDataModel refine_mat in refineData)
        {
            if(refine_mat.profession == profession)
            {
                var ins = Instantiate(RefineMaterialPrefab);
                ins.transform.SetParent(RefineParent);
                ins.transform.localScale = new Vector3(1, 1, 1);
                var child = ins.gameObject.GetComponent<RefineMaterialStatus>();
                child.image.texture = refine_mat.img;
                child.quantity.text = "x0";
                child.count = 0;
                string material_short_name = helper.mat_abv_rev[refine_mat.name];
                if (MessageHandler.userModel.inventory.Length > 0)
                {
                    foreach (InventoryModel data in MessageHandler.userModel.inventory)
                    {
                        if (data.name == material_short_name)
                        {
                            child.quantity.text = "x" + data.count;
                            child.count = (int)(float.Parse(data.count));
                            break;
                        }
                    }
                }
                child.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                child.gameObject.GetComponent<Button>().onClick.AddListener(delegate { RefineMaterialImageClick(refine_mat.name, profession_id, child.count); });
            }
        }
    }

    public void RefineMaterialImageClick(string mat_name, string profession_id, int count)
    {
        RefineHelloInfo.SetActive(false);
        RefineActionInfo.SetActive(true);
        PopupRefineButton.gameObject.GetComponent<Button>().interactable = false;
        foreach (RefineDataModel refine_mat in refineData)
        {
            if(refine_mat.name == mat_name)
            {
                RefineMaterialImage.texture = refine_mat.img;
                RefinedMaterialImage.texture = refine_mat.refined_product_img;
                RefineMaterialText.text = mat_name + "\n3x";
                RefinedMaterialText.text = refine_mat.refined_product  + "\n1x";
                if (count > 2)
                {
                    string profession_name = refine_mat.profession;
                    foreach(DelayDataModel data in MessageHandler.userModel.config.rawmat_refined)
                    {
                        if(data.key == helper.mat_abv_rev[mat_name])
                        {
                            PopupRefineButton.gameObject.GetComponent<Button>().interactable = true;
                            PopupRefineButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                            PopupRefineButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { PopupRefineButtonClick(data.value, profession_id, profession_name); });
                            break;
                        }
                    }
                }
                break;
            }
        }
    }

    public void PopupRefineButtonClick(string mat_name,string profession_id,string profession_name)
    {
        LoadingPanel.SetActive(true);
        coroutine = WaitAndClose(Config.configData.TIME_LOADING_PANEL);
        StartCoroutine(coroutine);
        MessageHandler.Server_RefineMat(profession_id, mat_name, profession_name);
    }

    public void CraftButtonClick(string profession_id, string type)
    {
        CraftPanel.SetActive(true);
        CraftHelloInfo.SetActive(true);
        CraftActionInfo.SetActive(false);
        CraftActionSeries.SetActive(false);
        PopupCraftButton.gameObject.GetComponent<Button>().interactable = false;
        if (type == "Blacksmith")
        {
            CraftEngineerActionTop.SetActive(false);
            CraftBlackActionTop.SetActive(true);
        }
        else
        {
            CraftBlackActionTop.SetActive(false);
            CraftEngineerActionTop.SetActive(true);
        }
        CraftPanel.GetComponent<CraftPanelIndices>().profession_id = profession_id;
        CraftPanel.GetComponent<CraftPanelIndices>().profession_name = type;
    }

    public void ShowCraft_Rarity(string param)
    {
        CraftHelloInfo.SetActive(false);
        CraftActionInfo.SetActive(false);
        CraftActionSeries.SetActive(true);
        PopupCraftButton.gameObject.GetComponent<Button>().interactable = false;
        // selected_item_group has two properties: one: crafte_name: Hoe || Sickle || hammer || ... two: profession_name: who is gonna craft Blacksmith || Engineer
        string[] paramArr = param.Split('+');

        string mat_name = paramArr[0];
        string type = paramArr[1];
        // Debug.Log(paramArr[0]);
        // Debug.Log(paramArr[1]);
        CraftPanelIndices craftScript = CraftPanel.GetComponent<CraftPanelIndices>(); 
        foreach(RefineRarityIndex s in craftScript.Series)
        {
            foreach(Config_CraftComboModel data_model in MessageHandler.userModel.craft_combos)
            {
                if(data_model.item_name == mat_name && s.type == data_model.rarity)
                {
                    string craft_name = "";
                    if (type == "Engineer")
                    {
                        switch (data_model.rarity)
                        {
                            case ("Common"):
                                craft_name = "Birch " + mat_name;
                                break;
                            case ("Uncommon"):
                                craft_name = "Oak " + mat_name;
                                break;
                            case ("Rare"):
                                craft_name = "Teak " + mat_name;
                                break;
                        }
                    }
                    else
                    {
                        switch (data_model.rarity)
                        {
                            case ("Common"):
                                craft_name = "Copper " + mat_name;
                                break;
                            case ("Uncommon"):
                                craft_name = "Tin " + mat_name;
                                break;
                            case ("Rare"):
                                craft_name = "Iron " + mat_name;
                                break;
                        }
                    }
                    foreach(ImgObjectView img in images)
                    {
                        if(img.name == craft_name)
                        {
                            s.image.texture = img.img;
                            break;
                        }
                    }
                    s.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                    s.gameObject.GetComponent<Button>().onClick.AddListener(delegate { ShowFinalCraftIngredients(mat_name, craft_name, data_model); });
                    break;
                }
            }
        }
    }

    public void ShowFinalCraftIngredients(string mat_name, string craft_name, Config_CraftComboModel c_data)
    {
        CraftPanelIndices craftScript = CraftPanel.GetComponent<CraftPanelIndices>();
        CraftActionInfo.SetActive(true);
        // if (!craftScript.ing_obj_parent.gameObject.activeInHierarchy) craftScript.ing_obj_parent.gameObject.SetActive(true);
        DelayDataModel[] ingredients_craft = new DelayDataModel[3];
        foreach (Config_CraftComboModel data_model in MessageHandler.userModel.craft_combos)
        {
            if (data_model.item_name == mat_name && data_model.rarity == c_data.rarity)
            {
                ingredients_craft = data_model.ingredients;
                break;
            }
        }
        int canCraft = 0;
        for (int i = 0; i < 3; i++)
        {
            foreach (ImgObjectView img in images)
            {
                if (img.name == ingredients_craft[i].key)
                {
                    TimeSpan t = TimeSpan.FromSeconds(Double.Parse(c_data.delay));
                    craftScript.product_durability.text = "The crafting process will take " + t.Hours + " hours";

                    craftScript.craft_img[i].texture = img.img;
                    craftScript.ing_name[i].text = helper.mat_abv[ingredients_craft[i].key];
                    craftScript.req_qty[i].text = ingredients_craft[i].value + "x";
                    craftScript.ing_qty[i].text = "x0";
                    if (MessageHandler.userModel.inventory.Length > 0)
                    {
                        foreach (InventoryModel inv_data in MessageHandler.userModel.inventory)
                        {
                            if (inv_data.name == ingredients_craft[i].key)
                            {
                                craftScript.ing_qty[i].text = "x" + inv_data.count;

                                if (Int64.Parse(inv_data.count) >= Int64.Parse(ingredients_craft[i].value))
                                {
                                    canCraft += 1;
                                }
                                break;
                            }
                        }
                        break;
                    }
                }
            }
        }
        // setting the result image
        // ss
        foreach (ImgObjectView img in images)
        {
            if(craft_name == img.name)
            {
                craftScript.end_product.texture = img.img;
                if (craft_name == "Copper Hammer and Chisel")
                {
                    craftScript.product_name.text = "Copper\nH. & C.";
                }
                else if(craft_name == "Tin Hammer and Chisel")
                {
                    craftScript.product_name.text = "Tin\nH. & C.";
                }
                else if(craft_name == "Iron Hammer and Chisel")
                {
                    craftScript.product_name.text = "Iron\nH. & C.";
                }
                else if(craft_name == "Birch Wheelbarrow")
                {
                    craftScript.product_name.text = "Birch\nWheelb.";
                }
                else if(craft_name == "Oak Wheelbarrow")
                {
                    craftScript.product_name.text = "Oak\nWheelb.";
                }
                else if(craft_name == "Teak Wheelbarrow")
                {
                    craftScript.product_name.text = "Teak\nWheelb.";
                }
                else if(craft_name == "Birch Mining Cart")
                {
                    craftScript.product_name.text = "Birch\nMining Cart";
                }
                else if(craft_name == "Oak Mining Cart")
                {
                    craftScript.product_name.text = "Oak\nMining Cart";
                }
                else if(craft_name == "Teak Mining Cart")
                {
                    craftScript.product_name.text = "Teak\nMining Cart";
                }
                else
                {
                    craftScript.product_name.text = craft_name.Replace(' ', '\n');
                }
                break;
            }
        }
        if (canCraft == 3)
        {
            PopupCraftButton.gameObject.GetComponent<Button>().interactable = true;
            PopupCraftButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
            PopupCraftButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { PopupCraftButtonClick(craftScript.profession_id, c_data.template_id, craftScript.profession_name); });
        }
        else
        {
            PopupCraftButton.gameObject.GetComponent<Button>().interactable = false;
        }

    }

    public void PopupCraftButtonClick(string profession_id,string template,string profession_name)
    {
        // Debug.Log("PopupCraftButtonClick");
        // Debug.Log(profession_id);
        // Debug.Log(template);
        // Debug.Log(profession_name);
        CraftPanel.SetActive(false);
        // LoadingPanel.SetActive(true);
        // foreach (Transform child in ActionsPanel)
        // {
        //     if(child.assetId == profession_id)
        //     {
        //         foreach (Config_CraftComboModel data_model in MessageHandler.userModel.craft_combos)
        //         {
        //             if (data_mode.template_id == template)
        //             {
        //                 child.craft_time = data_model.delay;
        //                 break;
        //             }
        //         }
        //         break;
        //     }
        // }
        LoadingPanel.SetActive(true);
        coroutine = WaitAndClose(Config.configData.TIME_LOADING_PANEL);
        StartCoroutine(coroutine);
        MessageHandler.Server_CraftMat(profession_id, template, profession_name);
    }

    public void ShowSettlements(List<SettlementsModel> settlementsModels, string type)
    {
        // NoForest_Text.SetActive(false);
        ProfessionEmptyPanel.SetActive(false);
        if (settlementsModels.Count < 1)
        {
            ProfessionEmptyPanel.SetActive(true);
            TopBlendButton.SetActive(true);
            SettlementEmptyPopup.SetActive(true);
            ProfessionShowPanel.SetActive(false);
            SettlementEmptyTitleText.text = "No \"" + type + "\" NFT in wallet.";
            SettlementEmptyInfoText.text = "Unfortunately you don't have any \"" + type + "\" NFT in your wallet and will need to obtain one first.";
        }
        else
        {
            ProfessionShowPanel.SetActive(true);
            string maxCount = settlementsModels.Count.ToString();
            int registeredCount = 0;
            foreach (SettlementsModel c in settlementsModels)
            {
                if (c.reg == "1")
                {
                    registeredCount += 1;
                }
            }
            CountInfoText.text = type + " - " + registeredCount.ToString() + "/" + maxCount;
            if (ActionsPanel.childCount >= 1)
            {
                foreach (Transform child in ActionsPanel)
                {
                    GameObject.Destroy(child.gameObject);
                }
            }
            for (int i = 0; i < settlementsModels.Count; i++)
            {
                var ins = Instantiate(OneSettlementPrefab);
                ins.transform.SetParent(ActionsPanel);
                ins.transform.localScale = new Vector3(1, 1, 1);
                var child = ins.gameObject.GetComponent<OneSettlementStatus>();
                child.IdText.text = "#" + settlementsModels[i].asset_id.ToString();
                child.assetId = settlementsModels[i].asset_id;
                child.type = settlementsModels[i].name;
                for (int j = 0; j < images.Length; j++)
                {
                    if (images[j].name == child.type)
                    {
                        child.image.texture = images[j].img;
                        break;
                    }
                }
                if (settlementsModels[i].reg == "0")
                {
                    child.Register.SetActive(true);
                    child.Register.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                    child.Register.gameObject.GetComponent<Button>().onClick.AddListener(delegate { Register_Settlement(child.assetId, child.type);});
                }
                else if (settlementsModels[i].reg == "1")
                {
                    child.Unregister.SetActive(true);
                    child.Unregister.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                    child.Unregister.gameObject.GetComponent<Button>().onClick.AddListener(delegate { DeRegister_Settlement(child.assetId, child.type);});
                    child.SellBtn.gameObject.GetComponent<Button>().interactable = false;
                }
            }
        }
    }

    public void UpgradeButtonClick()
    {
        ProfessionBlendingPanel.SetActive(false);
        TopBlendButton.SetActive(false);
        BulkButton.SetActive(false);
        CheckAllButton.SetActive(false);
        string type = ProfessionPanelParent.GetComponent<ProfessionUpgradeIndex>().upgrade_indexer;
        switch (type)
        {
            case ("Miner"):
                ShowSettlements(Mine, "Mine");
                break;
            case ("Lumberjack"):
                ShowSettlements(Forest, "Forest");
                break;
            case ("Farmer"):
                ShowSettlements(Field, "Field");
                break;
            default:
                break;
        }
    }

    public void Register_Settlement(string asset_id, string type)
    {
        LoadingPanel.SetActive(true);
        coroutine = WaitAndClose(Config.configData.TIME_LOADING_PANEL);
        StartCoroutine(coroutine);
        MessageHandler.Server_TransferAsset(asset_id, "regupgrade", type);
    }

    public void Blend_Profession(string asset_id, string type)
    {
        LoadingPanel.SetActive(true);
        coroutine = WaitAndClose(Config.configData.TIME_LOADING_PANEL);
        StartCoroutine(coroutine);
        MessageHandler.Server_BlendProfession(asset_id, "blendprofession", type);
    }

    public void DeRegister_Settlement(string asset_id, string type)
    {
        LoadingPanel.SetActive(true);
        coroutine = WaitAndClose(Config.configData.TIME_LOADING_PANEL);
        StartCoroutine(coroutine);
        MessageHandler.Server_WithdrawAsset(asset_id, type);
    }

    public void OnSettlementData(SettlementsModel[] settlements)
    {
        SetSettlementsModel(settlements);
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

    public void ChangeProfessionStatus()
    {
        if(checkAllClicked) return;
        itemCheckClicked = true;
        bool sel_flag = false;
        bool check_flag = true;
        foreach (Transform child in ActionsPanel)
        {
            if(child.gameObject.GetComponent<Toggle>().isOn)
            {
                sel_flag = true;
                break;
            }
        }

        foreach (Transform child in ActionsPanel)
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

    public void RegisterProfessions()
    {
        bulkActionVisible = false;
        ActionGroup.SetActive(false);
        List<string> id_arr = new List<string>();
        string race = "";
        bool self_flag = true;
        foreach (Transform child in ActionsPanel)
        {
            var obj = child.gameObject.GetComponent<OneProfessionStatus>();
            if(child.gameObject.GetComponent<Toggle>().isOn)
            {
                if(obj.Register.activeSelf == false)
                {
                    self_flag = false;
                    break;
                }
                id_arr.Add(obj.assetId);
                race = obj.type;
            }
        }
        if(self_flag == false)
        {
            DisplayInfoPopup(Constants.INFO_TYPE_DEFAULT, Constants.INFO_TITLE_DEFAULT, "Unable to \"Register\" as not all selected professions have the same status.");
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

    public void UnregisterProfessions()
    {
        bulkActionVisible = false;
        ActionGroup.SetActive(false);
        List<string> id_arr = new List<string>();
        string race = "";
        bool self_flag = true;
        foreach (Transform child in ActionsPanel)
        {
            var obj = child.gameObject.GetComponent<OneProfessionStatus>();
            if(child.gameObject.GetComponent<Toggle>().isOn)
            {
                if(obj.Timer.activeSelf == true || obj.Check.activeSelf == true || obj.Register.activeSelf == true)
                {
                    self_flag = false;
                    break;
                }
                id_arr.Add(obj.assetId);
                race = obj.type;
            }
        }
        if(self_flag == false)
        {
            DisplayInfoPopup(Constants.INFO_TYPE_DEFAULT, Constants.INFO_TITLE_DEFAULT, "Unable to \"Unregister\" as not all selected professions have the same status.");
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

    public void WorkProfessions()
    {
        bulkActionVisible = false;
        ActionGroup.SetActive(false);
        List<string> id_arr = new List<string>();
        string race = "";
        bool state_flag = true;
        foreach (Transform child in ActionsPanel)
        {
            var obj = child.gameObject.GetComponent<OneProfessionStatus>();
            if(child.gameObject.GetComponent<Toggle>().isOn)
            {
                if(obj.Timer.activeSelf == true || obj.Check.activeSelf == true || obj.Register.activeSelf == true || obj.UseLeftCount.text == "0")
                {
                    state_flag = false;
                    break;
                }
                id_arr.Add(obj.assetId);
                race = obj.type;
            }
        }
        if(state_flag == false)
        {
            DisplayInfoPopup(Constants.INFO_TYPE_DEFAULT, Constants.INFO_TITLE_DEFAULT, "Unable to \"Work\" as not all selected professions have the same status.");
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

    public void CheckProfessions()
    {
        bulkActionVisible = false;
        ActionGroup.SetActive(false);
        List<string> id_arr = new List<string>();
        string race = "";
        bool state_flag = true;

        foreach (Transform child in ActionsPanel)
        {
            var obj = child.gameObject.GetComponent<OneProfessionStatus>();
            if(child.gameObject.GetComponent<Toggle>().isOn)
            {
                if(obj.Check.activeSelf == false)
                {
                    state_flag = false;
                    break;
                }
                id_arr.Add(obj.assetId);
                race = obj.type;
            }
        }

        checked_items.Clear();
        foreach (Transform child in ActionsPanel) 
        {
            var obj = child.gameObject.GetComponent<OneProfessionStatus>();
            if(child.gameObject.GetComponent<Toggle>().isOn)
            {
                checked_items.Add(obj.assetId);
            }
        }

        if(state_flag == false)
        {
            DisplayInfoPopup(Constants.INFO_TYPE_DEFAULT, Constants.INFO_TITLE_DEFAULT, "Unable to \"Check\" as not all selected professions have the same status.");
        }
        else
        {
            LoadingPanel.SetActive(true);
            coroutine = WaitAndClose(Config.configData.TIME_LOADING_PANEL);
            StartCoroutine(coroutine);
            BulkButton.gameObject.GetComponent<Button>().interactable = false;

            oldExtra.Clear();
            AddExtraData();

            MessageHandler.Server_FindMat(String.Join(",", id_arr));
        }
    }

    public void BurnProfessions()
    {
        bulkActionVisible = false;
        ActionGroup.SetActive(false);
        List<string> id_arr = new List<string>();
        string race = "";
        bool state_flag = true;
        foreach (Transform child in ActionsPanel)
        {
            var obj = child.gameObject.GetComponent<OneProfessionStatus>();
            if(child.gameObject.GetComponent<Toggle>().isOn)
            {
                if(obj.BurnBtn.activeSelf == false || obj.item_count != 0)
                {
                    state_flag = false;
                    break;
                }
                id_arr.Add(obj.assetId);
                race = obj.type;
            }
        }
        if(state_flag == false)
        {
            DisplayInfoPopup(Constants.INFO_TYPE_DEFAULT, Constants.INFO_TITLE_DEFAULT, "Unable to \"Burn\" as not all selected professions have the same status.");
        }
        else
        {
            // LoadingPanel.SetActive(true);
            // coroutine = WaitAndClose(Config.configData.TIME_LOADING_PANEL);
            // StartCoroutine(coroutine);
            // BulkButton.gameObject.GetComponent<Button>().interactable = false;
            // MessageHandler.Server_BurnProfession(race, String.Join(",", id_arr));
            Show_BurnPanel(String.Join(",", id_arr), race);
        }
    }

    public void CheckAllButtonClick() 
    {
        if(itemCheckClicked) return;
        checkAllClicked = true;
        bool checkState = CheckAllButton.GetComponent<Toggle>().isOn;
        foreach (Transform child in ActionsPanel)
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

    public void CheckButtonClick(OneProfessionStatus profess)
    {
        LoadingPanel.SetActive(true);
        coroutine = WaitAndClose(Config.configData.TIME_LOADING_PANEL);
        StartCoroutine(coroutine);
        switch (profess.type)
        {
            case "Miner":
            case "Lumberjack":
            case "Farmer":
                checked_items.Clear();
                checked_items.Add(profess.assetId);
                oldExtra.Clear();
                AddExtraData();
                MessageHandler.Server_FindMat(profess.assetId);
                break;
            case "Engineer":
                MessageHandler.Server_CraftComp(profess.assetId);
                break;
            case "Carpenter":
            case "Tailor":
                MessageHandler.Server_RefineComp(profess.assetId);
                break;
            case "Blacksmith":
                if (!profess.craft) MessageHandler.Server_RefineComp(profess.assetId);
                else
                MessageHandler.Server_CraftComp(profess.assetId);
                break;
            default:
                break;
        }
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

    public void ActionButtonClick()
    {
        LoadingPanel.SetActive(true);
        coroutine = WaitAndClose(Config.configData.TIME_LOADING_PANEL);
        StartCoroutine(coroutine);
    }

    public float GetCommonAmount(string profession_name) {
        float find_val = 0;
        switch (profession_name) {
            case ("Miner"):
                for (int i = 0; i < Miners.Count; i++)
                {
                    if (Miners[i].status.IndexOf("Mine success- Mined CO") >= 0)
                    {
                        string temp = Miners[i].status;
                        temp = temp.Replace("Mine success- Mined CO" , "");
                        find_val += float.Parse(temp);
                    }
                }
                break;
            case ("Lumberjack"):
                for (int i = 0; i < LumberJacks.Count; i++)
                {
                    if (LumberJacks[i].status.IndexOf("Mine success- Mined BIRCH") >= 0)
                    {
                        string temp = LumberJacks[i].status;
                        temp = temp.Replace("Mine success- Mined BIRCH" , "");
                        find_val += float.Parse(temp);
                    }
                }
                break;
            case ("Farmer"):
                for (int i = 0; i < Farmers.Count; i++)
                {
                    if (Farmers[i].status.IndexOf("Mine success- Mined COTTON") >= 0)
                    {
                        string temp = Farmers[i].status;
                        temp = temp.Replace("Mine success- Mined COTTON" , "");
                        find_val += float.Parse(temp);
                    }
                }
                break;
            default:
                return 0;
                break;
        }
        return find_val / 100.0f;
    }

    public float GetUnCommonAmount(string profession_name) {
        float find_val = 0;
        switch (profession_name) {
            case ("Miner"):
                for (int i = 0; i < Miners.Count; i++)
                {
                    if (Miners[i].status.IndexOf("Mine success- Mined TO") >= 0)
                    {
                        string temp = Miners[i].status;
                        temp = temp.Replace("Mine success- Mined TO" , "");
                        find_val += float.Parse(temp);
                    }
                }
                break;
            case ("Lumberjack"):
                for (int i = 0; i < LumberJacks.Count; i++)
                {
                    if (LumberJacks[i].status.IndexOf("Mine success- Mined OAK") >= 0)
                    {
                        string temp = LumberJacks[i].status;
                        temp = temp.Replace("Mine success- Mined OAK" , "");
                        find_val += float.Parse(temp);
                    }
                }
                break;
            case ("Farmer"):
                for (int i = 0; i < Farmers.Count; i++)
                {
                    if (Farmers[i].status.IndexOf("Mine success- Mined FLAX") >= 0)
                    {
                        string temp = Farmers[i].status;
                        temp = temp.Replace("Mine success- Mined FLAX" , "");
                        find_val += float.Parse(temp);
                    }
                }
                break;
            default:
                return 0;
                break;
        }
        return find_val / 100.0f;
    }

    public float GetRareAmount(string profession_name) {
        float find_val = 0;
        switch (profession_name) {
            case ("Miner"):
                for (int i = 0; i < Miners.Count; i++)
                {
                    if (Miners[i].status.IndexOf("Mine success- Mined IO") >= 0)
                    {
                        string temp = Miners[i].status;
                        temp = temp.Replace("Mine success- Mined IO" , "");
                        find_val += float.Parse(temp);
                    }
                }
                break;
            case ("Lumberjack"):
                for (int i = 0; i < LumberJacks.Count; i++)
                {
                    if (LumberJacks[i].status.IndexOf("Mine success- Mined TEAK") >= 0)
                    {
                        string temp = LumberJacks[i].status;
                        temp = temp.Replace("Mine success- Mined TEAK" , "");
                        find_val += float.Parse(temp);
                    }
                }
                break;
            case ("Farmer"):
                for (int i = 0; i < Farmers.Count; i++)
                {
                    if (Farmers[i].status.IndexOf("Mine success- Mined SWORMS") >= 0)
                    {
                        string temp = Farmers[i].status;
                        temp = temp.Replace("Mine success- Mined SWORMS" , "");
                        find_val += float.Parse(temp);
                    }
                }
                break;
            default:
                return 0;
                break;
        }
        return find_val / 100.0f;
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

    public void OnTransactionData()
    {
        if (MessageHandler.transactionModel.transactionid != "")
        {
            string[] new_string = MessageHandler.transactionModel.transactionid.Split('%');
            if(new_string.Length > 1)
            {
                PermissionPanel.SetActive(false);
                MessageHandler.userModel.citizens = MessageHandler.transactionModel.citizens;
                // citizens_text.text = MessageHandler.userModel.citizens;
                string citizens_found = new_string[1];
                if (Int64.Parse(citizens_found) == 0)
                {
                    done_panel_text.text = "No \"Citizen\" survived!";
                }
                else
                {
                    DonePanel.SetActive(true);
                    done_panel_img.gameObject.SetActive(true);
                    done_panel_text.text = citizens_found.ToString() + " \"Citizen\"(s) survived while burning the profession NFT and was added to your account!";
                }
            }
            onSetHeaderElements();
        }
        LoadingPanel.SetActive(false);
        StopCoroutine(coroutine);
    }

    public void AddExtraData()
    {
        ExtraDataModel[] extraData = MessageHandler.userModel.extraData;
        string extra_str = ":";
        foreach(ExtraDataModel extra in extraData)
        {
            AssetModel[] asset = MessageHandler.userModel.assets.Where(data => (data.schema == extra.schema_name && data.template == extra.template_id)).ToArray();
            oldExtra.AddRange(asset);
        }
    }

    public void OnItemData() { }

    private void OnErrorData(string errorData)
    {
        // Debug.Log(errorData + "==============Error Handler==============");
        DisplayInfoPopup(Constants.INFO_TYPE_DEFAULT, Constants.INFO_TITLE_DEFAULT, errorData);
    }
}
