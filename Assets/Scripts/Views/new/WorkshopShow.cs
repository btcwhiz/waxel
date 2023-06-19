using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorkshopShow : BaseView
{
    [Space]
    [Header("MainPanel")]
    public Transform ItemGroup;
    public GameObject ItemStatsPrefab;

    [Space]
    [Header("ItemDetailPopup")]
    public GameObject ItemDetailPopup;
    public RawImage ItemDetailImage;
    public TMP_Text ItemDetailPanelTitleText;
    public TMP_Text ItemDetailToptext;
    public TMP_Text ItemDetailRarityText;
    public TMP_Text ItemDetailFunctionText;
    public TMP_Text ItemDetailDurablilityText;
    public TMP_Text ItemDetailUsedByText;
    public TMP_Text ItemDetailCraftedByText;
    public TMP_Text ItemDetailMaterialsNeededText;
    public TMP_Text ItemDetailBottomTopText;
    public GameObject ItemDetailEmptyInfo;
    public TMP_Text ItemDetailEmptyInfoText;
    public Transform ItemDetailBottomParent;
    public GameObject OneItemPrefab;
    public Button CraftButton;

    [Space]
    [Header("Burning Action")]
    public GameObject BurnPopupAlarm;
    public TMP_Text BurnPopupAlarmInfo;
    public GameObject BurnResultPopup;
    public GameObject BurnPopupYesButton;
    public RawImage BurnResultPopupImage;
    public TMP_Text BurnResultPopupInfo;

    [Space]
    [Header("Variables")]
    public static string SelectedProfession;
    public WorkshopDataModel[] ItemSchema;
    public MaterialDataModel[] MaterialSchema;
    private List<ItemDataModel> CHammer = new List<ItemDataModel>();
    private List<ItemDataModel> IHammer = new List<ItemDataModel>();
    private List<ItemDataModel> THammer = new List<ItemDataModel>();
    private List<ItemDataModel> CPickAxe = new List<ItemDataModel>();
    private List<ItemDataModel> IPickAxe = new List<ItemDataModel>();
    private List<ItemDataModel> TPickAxe = new List<ItemDataModel>();
    private List<ItemDataModel> BCart = new List<ItemDataModel>();
    private List<ItemDataModel> OCart = new List<ItemDataModel>();
    private List<ItemDataModel> TCart = new List<ItemDataModel>();
    private List<ItemDataModel> CSaw = new List<ItemDataModel>();
    private List<ItemDataModel> TSaw = new List<ItemDataModel>();
    private List<ItemDataModel> ISaw = new List<ItemDataModel>();
    private List<ItemDataModel> CAxe = new List<ItemDataModel>();
    private List<ItemDataModel> TAxe = new List<ItemDataModel>();
    private List<ItemDataModel> IAxe = new List<ItemDataModel>();
    private List<ItemDataModel> BWheelbarrow = new List<ItemDataModel>();
    private List<ItemDataModel> OWheelBarrow = new List<ItemDataModel>();
    private List<ItemDataModel> TWheelBarrow = new List<ItemDataModel>();
    private List<ItemDataModel> CSickle = new List<ItemDataModel>();
    private List<ItemDataModel> TSickle = new List<ItemDataModel>();
    private List<ItemDataModel> ISickle = new List<ItemDataModel>();
    private List<ItemDataModel> CHoe = new List<ItemDataModel>();
    private List<ItemDataModel> THoe = new List<ItemDataModel>();
    private List<ItemDataModel> IHoe = new List<ItemDataModel>();
    private List<ItemDataModel> BWagon = new List<ItemDataModel>();
    private List<ItemDataModel> OWagon = new List<ItemDataModel>();
    private List<ItemDataModel> TWagon = new List<ItemDataModel>();
    public GameObject workshop_prefab;
    public GameObject items_panel;
    public GameObject items_panel2;
    public GameObject no_asset_panel;
    public GameObject title_panel;
    public GameObject asset_display_panel;
    public GameObject item_select_prefab;
    public GameObject permission_panel;
    public GameObject donePanel;
    public TMP_Text rarity_text;
    public TMP_Text function_text;
    public TMP_Text durability_text;
    public TMP_Text used_by_text;
    public TMP_Text material_text;
    public TMP_Text crafted_by_text;
    public TMP_Text item_name_text;
    public TMP_Text craft_text;
    public TMP_Text infotext1_text;
    public TMP_Text permission_panel_text;
    public TMP_Text done_panel_text;
    public Image item_img;
    public Transform parent_object;
    public Button YesBtn;


    public AbbreviationsHelper helper;

    private IEnumerator coroutine;

    // setting the header
    public delegate void SetHeader();
    public static SetHeader onSetHeaderElements;


    protected override void Start()
    {
        base.Start();
        SetItemData(MessageHandler.userModel.items);
        SetItems();
        MessageHandler.OnCallBackData += OnCallBackData;
        MessageHandler.OnTransactionData += OnTransactionData;
        MessageHandler.OnItemData += OnItemData;
        MessageHandler.OnInventoryData += OnInventoryData;
        MessageHandler.OnProfessionData += OnProfessionData;
        ErrorHandler.OnErrorData += OnErrorData;
    }
    public void SetItems()
    {
        if (ItemGroup.childCount >= 1)
        {
            foreach (Transform child in ItemGroup)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        foreach (WorkshopDataModel i in ItemSchema)
        {
            var ins = Instantiate(ItemStatsPrefab);
            ins.transform.SetParent(ItemGroup);
            ins.transform.localScale = new Vector3(1, 1, 1);
            var child = ins.gameObject.GetComponent<OneItemStatus>();
            child.image.texture = i.image;
            // child.type = i.name;
            // child.end_product = m.end_product;
            switch (i.name)
            {
                case "Copper Hammer and Chisel":
                    child.CountText.text = "x" + CHammer.Count.ToString();
                    child.DetailButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { DetailButtonClick(CHammer, i); });
                    break;
                case "Tin Hammer and Chisel":
                    child.CountText.text = "x" + THammer.Count.ToString();
                    child.DetailButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { DetailButtonClick(THammer, i); });
                    break;
                case "Iron Hammer and Chisel":
                    child.CountText.text = "x" + IHammer.Count.ToString();
                    child.DetailButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { DetailButtonClick(IHammer, i); });
                    break;
                case "Copper Pickaxe":
                    child.CountText.text = "x" + CPickAxe.Count.ToString();
                    child.DetailButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { DetailButtonClick(CPickAxe, i); });
                    break;
                case "Tin Pickaxe":
                    child.CountText.text = "x" + TPickAxe.Count.ToString();
                    child.DetailButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { DetailButtonClick(TPickAxe, i); });
                    break;
                case "Iron Pickaxe":
                    child.CountText.text = "x" + IPickAxe.Count.ToString();
                    child.DetailButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { DetailButtonClick(IPickAxe, i); });
                    break;
                case "Oak Mining Cart":
                    child.CountText.text = "x" + OCart.Count.ToString();
                    child.DetailButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { DetailButtonClick(OCart, i); });
                    break;
                case "Birch Mining Cart":
                    child.CountText.text = "x" + BCart.Count.ToString();
                    child.DetailButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { DetailButtonClick(BCart, i); });
                    break;
                case "Teak Mining Cart":
                    child.CountText.text = "x" + TCart.Count.ToString();
                    child.DetailButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { DetailButtonClick(TCart, i); });
                    break;
                case "Copper Saw":
                    child.CountText.text = "x" + CSaw.Count.ToString();
                    child.DetailButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { DetailButtonClick(CSaw, i); });
                    break;
                case "Tin Saw":
                    child.CountText.text = "x" + TSaw.Count.ToString();
                    child.DetailButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { DetailButtonClick(TSaw, i); });
                    break;
                case "Iron Saw":
                    child.CountText.text = "x" + ISaw.Count.ToString();
                    child.DetailButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { DetailButtonClick(ISaw, i); });
                    break;
                case "Copper Axe":
                    child.CountText.text = "x" + CAxe.Count.ToString();
                    child.DetailButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { DetailButtonClick(CAxe, i); });
                    break;
                case "Tin Axe":
                    child.CountText.text = "x" + TAxe.Count.ToString();
                    child.DetailButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { DetailButtonClick(TAxe, i); });
                    break;
                case "Iron Axe":
                    child.CountText.text = "x" + IAxe.Count.ToString();
                    child.DetailButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { DetailButtonClick(IAxe, i); });
                    break;
                case "Birch Wheelbarrow":
                    child.CountText.text = "x" + BWheelbarrow.Count.ToString();
                    child.DetailButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { DetailButtonClick(BWheelbarrow, i); });
                    break;
                case "Oak Wheelbarrow":
                    child.CountText.text = "x" + OWheelBarrow.Count.ToString();
                    child.DetailButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { DetailButtonClick(OWheelBarrow, i); });
                    break;
                case "Teak Wheelbarrow":
                    child.CountText.text = "x" + TWheelBarrow.Count.ToString();
                    child.DetailButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { DetailButtonClick(TWheelBarrow, i); });
                    break;
                case "Copper Sickle":
                    child.CountText.text = "x" + CSickle.Count.ToString();
                    child.DetailButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { DetailButtonClick(CSickle, i); });
                    break;
                case "Tin Sickle":
                    child.CountText.text = "x" + TSickle.Count.ToString();
                    child.DetailButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { DetailButtonClick(TSickle, i); });
                    break;
                case "Iron Sickle":
                    child.CountText.text = "x" + ISickle.Count.ToString();
                    child.DetailButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { DetailButtonClick(ISickle, i); });
                    break;
                case "Copper Hoe":
                    child.CountText.text = "x" + CHoe.Count.ToString();
                    child.DetailButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { DetailButtonClick(CHoe, i); });
                    break;
                case "Tin Hoe":
                    child.CountText.text = "x" + THoe.Count.ToString();
                    child.DetailButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { DetailButtonClick(THoe, i); });
                    break;
                case "Iron Hoe":
                    child.CountText.text = "x" + IHoe.Count.ToString();
                    child.DetailButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { DetailButtonClick(IHoe, i); });
                    break;
                case "Oak Wagon":
                    child.CountText.text = "x" + OWagon.Count.ToString();
                    child.DetailButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { DetailButtonClick(OWagon, i); });
                    break;
                case "Birch Wagon":
                    child.CountText.text = "x" + BWagon.Count.ToString();
                    child.DetailButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { DetailButtonClick(BWagon, i); });
                    break;
                case "Teak Wagon":
                    child.CountText.text = "x" + TWagon.Count.ToString();
                    child.DetailButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { DetailButtonClick(TWagon, i); });
                    break;
                default:
                    break;
            }
        }
    }

    public void DetailButtonClick(List<ItemDataModel> my_items, WorkshopDataModel item_data)
    {
        ItemDetailPopup.SetActive(true);
        ItemDetailPanelTitleText.text = item_data.name;
        ItemDetailImage.texture = item_data.image;
        if (item_data.function.Contains("Rarity") == true) {
            ItemDetailToptext.text = "The \"" + item_data.name + "\" is 1 of 3 different items that can be equipped by the \""
                                        + item_data.profession_type + "\" profession to boost the gathering efficiency. Depending on the rarity value of the item, the profession will have a higher chance of gathering more rare materials.";
        } else if (item_data.function.Contains("Luck") == true) {
            ItemDetailToptext.text = "The \"" + item_data.name + "\" is 1 of 3 different items that can be equipped by the \""
                                        + item_data.profession_type + "\" profession to boost the gathering efficiency. Depending on the rarity value of the item, the profession will have a higher chance of gathering a second material.";
        } else if (item_data.function.Contains("Extra") == true) {
            ItemDetailToptext.text = "The \"" + item_data.name + "\" is 1 of 3 different items that can be equipped by the \""
                                        + item_data.profession_type + "\" profession to boost the gathering efficiency. Depending on the rarity value of the item, the profession will gather extra materials.";
        }
        ItemDetailRarityText.text = item_data.rarity;
        ItemDetailFunctionText.text = item_data.function;
        ItemDetailDurablilityText.text = item_data.durability;
        ItemDetailUsedByText.text = item_data.profession_type;
        ItemDetailCraftedByText.text = item_data.crafter;
        ItemDetailMaterialsNeededText.text = item_data.mat_need;
        ItemDetailBottomTopText.text = item_data.name + " - "+ my_items.Count.ToString();
        CraftButton.onClick.AddListener(delegate {OpenSchoolWithSelectedProfession(item_data.crafter);});

        if (ItemDetailBottomParent.childCount >= 1)
        {
            foreach (Transform child in ItemDetailBottomParent)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        if (my_items.Count > 0)
        {
            ItemDetailEmptyInfo.SetActive(false);
            for (int i = 0; i < my_items.Count; i++)
            {
                var ins = Instantiate(OneItemPrefab);
                ins.transform.SetParent(ItemDetailBottomParent);
                ins.transform.localScale = new Vector3(1, 1, 1);
                var child = ins.gameObject.GetComponent<ItemStatus>();
                child.image.texture = item_data.image;
                child.AssetIdText.text = "#" + my_items[i].asset_id.ToString();
                child.UseLeftText.text = my_items[i].uses_left;

                child.asset_id = my_items[i].asset_id;
                child.mat_name = my_items[i].name;
                if (my_items[i].equipped == "1")
                {
                    child.EquipButton.SetActive(false);
                    child.UnEquipButton.SetActive(true);
                    child.UnEquipButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate {OpenSchoolWithSelectedProfession(item_data.profession_type);});
                    child.SellButton.gameObject.GetComponent<Button>().interactable = false;
                    child.BurnButton.gameObject.GetComponent<Button>().interactable = false;
                    child.p_id = my_items[i].profession;
                }
                else if (my_items[i].equipped == "0")
                {
                    child.EquipButton.SetActive(true);
                    child.EquipButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate {OpenSchoolWithSelectedProfession(item_data.profession_type);});
                    child.BurnButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                    child.BurnButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate {BurnButtonClick(child.asset_id, child.mat_name);});
                    // string item_name = idata[i].name;
                    if(my_items[i].uses_left == "0")
                    {
                        child.EquipButton.gameObject.GetComponent<Button>().interactable = false;
                        child.SellButton.gameObject.GetComponent<Button>().interactable = false;
                    }
                }
            }
        } else {
            ItemDetailEmptyInfo.SetActive(true);
            ItemDetailEmptyInfoText.text = "Unfortunately you don't have any \"" + item_data.name + "s\".\n\nYou could craft one by using the \"Craft\" button or obtain one by using the \"Buy\" button at the top right of this menu.";
        }
    }

    public void OpenSchoolWithSelectedProfession(string professionName) {
        SelectedProfession = professionName;
        SceneManager.LoadScene("SchoolScene");
    }

    public void BurnButtonClick(string assetId, string matName)
    {
        BurnPopupAlarm.SetActive(true);
        BurnPopupAlarm.GetComponent<BurnItemPopupProperty>().itemId = assetId;
        BurnPopupAlarm.GetComponent<BurnItemPopupProperty>().matName = matName;
        BurnPopupAlarmInfo.text = "Do you really want to burn your item with id \"#" + BurnPopupAlarm.GetComponent<BurnItemPopupProperty>().itemId + "\"?";
        BurnPopupYesButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        BurnPopupYesButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate {BurnPopupYesButtonClick();});
    }

    public void BurnPopupYesButtonClick()
    {
        BurnItemPopupProperty burnPopupPropery = BurnPopupAlarm.GetComponent<BurnItemPopupProperty>();
        if (!string.IsNullOrEmpty(burnPopupPropery.itemId))
        {
            LoadingPanel.SetActive(true);
            coroutine = WaitAndClose(Config.configData.TIME_LOADING_PANEL);
        StartCoroutine(coroutine);
            MessageHandler.Server_BurnItem(burnPopupPropery.matName, burnPopupPropery.itemId);
        }
        else
        {
            DisplayInfoPopup(Constants.INFO_TYPE_DEFAULT, Constants.INFO_TITLE_DEFAULT, "ID of NFT is null.");
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        MessageHandler.OnCallBackData -= OnCallBackData;
        MessageHandler.OnTransactionData -= OnTransactionData;
        MessageHandler.OnItemData -= OnItemData;
        MessageHandler.OnInventoryData -= OnInventoryData;
        MessageHandler.OnProfessionData -= OnProfessionData;
        ErrorHandler.OnErrorData -= OnErrorData;
    }

    public void SetItemData(ItemDataModel[] items)
    {
        foreach(ItemDataModel idata in items)
        {
            switch (idata.name)
            {
                case "Copper Hammer and Chisel":
                    CHammer.Add(idata);
                    break;
                case "Tin Hammer and Chisel":
                    THammer.Add(idata);
                    break;
                case "Iron Hammer and Chisel":
                    IHammer.Add(idata);
                    break;
                case "Copper Pickaxe":
                    CPickAxe.Add(idata);
                    break;
                case "Tin Pickaxe":
                    TPickAxe.Add(idata);
                    break;
                case "Iron Pickaxe":
                    IPickAxe.Add(idata);
                    break;
                case "Birch Mining Cart":
                    BCart.Add(idata);
                    break;
                case "Oak Mining Cart":
                    OCart.Add(idata);
                    break;
                case "Teak Mining Cart":
                    TCart.Add(idata);
                    break;
                case "Copper Saw":
                    CSaw.Add(idata);
                    break;
                case "Tin Saw":
                    TSaw.Add(idata);
                    break;
                case "Iron Saw":
                    ISaw.Add(idata);
                    break;
                case "Copper Axe":
                    CAxe.Add(idata);
                    break;
                case "Tin Axe":
                    TAxe.Add(idata);
                    break;
                case "Iron Axe":
                    IAxe.Add(idata);
                    break;
                case "Birch Wheelbarrow":
                    BWheelbarrow.Add(idata);
                    break;
                case "Oak Wheelbarrow":
                    OWheelBarrow.Add(idata);
                    break;
                case "Teak Wheelbarrow":
                    TWheelBarrow.Add(idata);
                    break;
                case "Copper Sickle":
                    CSickle.Add(idata);
                    break;
                case "Tin Sickle":
                    TSickle.Add(idata);
                    break;
                case "Iron Sickle":
                    ISickle.Add(idata);
                    break;
                case "Copper Hoe":
                    CHoe.Add(idata);
                    break;
                case "Tin Hoe":
                    THoe.Add(idata);
                    break;
                case "Iron Hoe":
                    IHoe.Add(idata);
                    break;
                case "Birch Wagon":
                    BWagon.Add(idata);
                    break;
                case "Oak Wagon":
                    OWagon.Add(idata);
                    break;
                case "Teak Wagon":
                    TWagon.Add(idata);
                    break;
                default:
                    break;
            }
        }
    }

    public void Display_No_Asset(WorkshopDataModel item_data, string name)
    {
        foreach (Transform child in parent_object.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        items_panel.SetActive(false);
        items_panel2.SetActive(true);
        title_panel.SetActive(true);
        no_asset_panel.SetActive(true);
        item_img.sprite = item_data.img.sprite;
        infotext1_text.text = "The \"" + name + "\" is 1 of 3 different items that can be equipped by the \"" + item_data.profession_type + "\" profession to boost the gathering process.";
        item_name_text.text = name;
        function_text.text = item_data.function;
        material_text.text = item_data.mat_need;
        rarity_text.text = item_data.rarity;
        durability_text.text = item_data.durability;
        used_by_text.text = item_data.profession_type;
        crafted_by_text.text = item_data.crafter;
        craft_text.text = "Unfortunately you don't have any \"" + name + "\".";
    }

    public void Display_Asset(List<ItemDataModel> idata,WorkshopDataModel item_data)
    {
        foreach (Transform child in parent_object.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        items_panel.SetActive(false);
        items_panel2.SetActive(true);
        title_panel.SetActive(true);
        asset_display_panel.SetActive(true);
        item_img.sprite = item_data.img.sprite;
        infotext1_text.text = "The \"" + idata[0].name + "\" is 1 of 3 different items that can be equipped by the \"" + item_data.profession_type + "\" profession to boost the gathering process.";
        item_name_text.text = idata[0].name;
        function_text.text = item_data.function;
        material_text.text = item_data.mat_need;
        rarity_text.text = item_data.rarity;
        durability_text.text = item_data.durability;
        used_by_text.text = item_data.profession_type;
        crafted_by_text.text = item_data.crafter;

        for (int i = 0; i < idata.Count; i++)
        {
            var ins = Instantiate(workshop_prefab);
            ins.transform.SetParent(parent_object);
            ins.transform.localScale = new Vector3(1, 1, 1);
            var child = ins.gameObject.GetComponent<ItemCall>();
            child.asset_ids.text = idata[i].asset_id.ToString();
            child.asset_id = idata[i].asset_id;
            child.LoadingPanel = LoadingPanel;
            child.durability.text = "Durability: " + idata[i].uses_left + "/20";
            child.img.sprite = item_data.img.sprite;
            child.mat_name = idata[i].name;
            if (idata[i].equipped == "1")
            {
                child.unequip.SetActive(true);
                child.burn.gameObject.GetComponent<Button>().interactable = false;
                child.sell.gameObject.GetComponent<Button>().interactable = false;
                child.p_id = idata[i].profession;
                YesBtn.onClick.RemoveAllListeners();
                YesBtn.onClick.AddListener(delegate { child.BurnBtn(); });
            }
            else if (idata[i].equipped == "0")
            {
                child.equip.SetActive(true);
                string item_name = idata[i].name;
                child.equip.gameObject.GetComponent<Button>().onClick.AddListener(delegate { EquipItem(item_name); });
                if(idata[i].uses_left == "0")
                {
                    child.equip.gameObject.GetComponent<Button>().interactable = false;
                    child.sell.gameObject.GetComponent<Button>().interactable = false;
                }
            }
        }
    }

    public void returnToItems()
    {
        items_panel.SetActive(true);
        items_panel2.SetActive(false);
        title_panel.SetActive(false);
        asset_display_panel.SetActive(false);
        no_asset_panel.SetActive(false);
        SetItems();
    }

    public void OnItemData()
    {
        CHammer.Clear();
        THammer.Clear();
        IHammer.Clear();
        CPickAxe.Clear();
        TPickAxe.Clear();
        IPickAxe.Clear();
        BCart.Clear();
        OCart.Clear();
        TCart.Clear();
        CSaw.Clear();
        TSaw.Clear();
        ISaw.Clear();
        CAxe.Clear();
        TAxe.Clear();
        IAxe.Clear();
        BWheelbarrow.Clear();
        OWheelBarrow.Clear();
        TWheelBarrow.Clear();
        CSickle.Clear();
        TSickle.Clear();
        ISickle.Clear();
        CHoe.Clear();
        THoe.Clear();
        IHoe.Clear();
        BWagon.Clear();
        OWagon.Clear();
        TWagon.Clear();
        SetItemData(MessageHandler.userModel.items);
    }

    public void OnTransactionData()
    {
        if (MessageHandler.transactionModel.transactionid != "")
        {
            // LoadingPanel.SetActive(false);
            WorkshopDataModel item_data = new WorkshopDataModel();
            string item_name = MessageHandler.transactionModel.transactionid;
            MessageHandler.userModel.total_matCount = MessageHandler.transactionModel.citizens;
            SetItems();

            foreach (WorkshopDataModel i in ItemSchema)
            {
                if(i.name == item_name) 
                {
                    switch (i.name)
                    {
                        case "Copper Hammer and Chisel":
                            DetailButtonClick(CHammer, i); break;
                        case "Tin Hammer and Chisel":
                            DetailButtonClick(THammer, i); break;
                        case "Iron Hammer and Chisel":
                            DetailButtonClick(IHammer, i); break;
                        case "Copper Pickaxe":
                            DetailButtonClick(CPickAxe, i); break;
                        case "Tin Pickaxe":
                            DetailButtonClick(TPickAxe, i); break;
                        case "Iron Pickaxe":
                            DetailButtonClick(IPickAxe, i); break;
                        case "Oak Mining Cart":
                            DetailButtonClick(OCart, i);  break;
                        case "Birch Mining Cart":
                            DetailButtonClick(BCart, i); break;
                        case "Teak Mining Cart":
                            DetailButtonClick(TCart, i); break;
                        case "Copper Saw":
                            DetailButtonClick(CSaw, i); break;
                        case "Tin Saw":
                            DetailButtonClick(TSaw, i); break;
                        case "Iron Saw":
                            DetailButtonClick(ISaw, i); break;
                        case "Copper Axe":
                            DetailButtonClick(CAxe, i); break;
                        case "Tin Axe":
                            DetailButtonClick(TAxe, i); break;
                        case "Iron Axe":
                            DetailButtonClick(IAxe, i); break;
                        case "Birch Wheelbarrow":
                            DetailButtonClick(BWheelbarrow, i); break;
                        case "Oak Wheelbarrow":
                            DetailButtonClick(OWheelBarrow, i); break;
                        case "Teak Wheelbarrow":
                            DetailButtonClick(TWheelBarrow, i); break;
                        case "Copper Sickle":
                            DetailButtonClick(CSickle, i); break;
                        case "Tin Sickle":
                            DetailButtonClick(TSickle, i); break;
                        case "Iron Sickle":
                            DetailButtonClick(ISickle, i); break;
                        case "Copper Hoe":
                            DetailButtonClick(CHoe, i); break;
                        case "Tin Hoe":
                            DetailButtonClick(THoe, i); break;
                        case "Iron Hoe":
                            DetailButtonClick(IHoe, i); break;
                        case "Oak Wagon":
                            DetailButtonClick(OWagon, i); break;
                        case "Birch Wagon":
                            DetailButtonClick(BWagon, i); break;
                        case "Teak Wagon":
                            DetailButtonClick(TWagon, i); break;
                        default:
                            break;
                    }
                    break;
                }
            }
            onSetHeaderElements();
        }
        LoadingPanel.SetActive(false);
        StopCoroutine(coroutine);
    }

    public void OnCallBackData(CallBackDataModel[] callback)
    {
        LoadingPanel.SetActive(false);
        StopCoroutine(coroutine);
        CallBackDataModel callBack = callback[0];
        switch (callBack.status)
        {
            case ("De-Equiped Successfully"):
                DisplayInfoPopup(Constants.INFO_TYPE_SUCCESS, Constants.INFO_TITLE_SUCCESS, "Item(s) successfully unequipped.");
                break;
            default:
                break;
        }
    }

    public void EquipItem(string item_name)
    {
        var ins = Instantiate(item_select_prefab);
        var child = ins.gameObject.GetComponent<ItemSelectedModel>();
        child.item_selected = item_name;
        LoadingPanel.SetActive(true);
        coroutine = WaitAndClose(Config.configData.TIME_LOADING_PANEL);
        StartCoroutine(coroutine);
        SceneManager.LoadScene("ProfessionScene");
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

    public void Show_BurnPanel(string item_name)
    {
        permission_panel.SetActive(true);
        permission_panel_text.text = "Do you really want to burn your \"" + item_name + "\"? If yes, you will have a 10% chance to get back 1 random material that was used for crafting it.";
    }

    private void OnInventoryData(InventoryModel[] inventoryData)
    {
        InventoryModel inventory = inventoryData[0];
        if (inventory.name == "null")
        {
            BurnResultPopupInfo.text = "No material could be extracted";
            BurnResultPopupImage.gameObject.SetActive(false);
        }
        else
        {
            BurnResultPopup.SetActive(true);
            foreach(MaterialDataModel w in MaterialSchema)
            {
                if(w.name == inventory.name)
                {
                    BurnResultPopupImage.texture = w.image;
                    BurnResultPopupImage.gameObject.SetActive(true);
                    BurnResultPopupInfo.text = inventory.count + " \"" +  helper.mat_abv[inventory.name] + "\" survived while burning the item and was added to your account!";
                    break;
                }
            }
        }
        onSetHeaderElements();
    }

    public void NoBtn()
    {
        permission_panel.SetActive(false);
        donePanel.SetActive(false);
    }

    public void OnProfessionData(ProfessionDataModel[] pr) { }

    private void OnErrorData(string errorData)
    {
        // Debug.Log(errorData + "==============Error Handler==============");
        DisplayInfoPopup(Constants.INFO_TYPE_DEFAULT, Constants.INFO_TITLE_DEFAULT, errorData);
    }
}
