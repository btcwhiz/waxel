using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WarehouseShow : BaseView
{
    [Space]
    [Header("Main Panel")]
    public GameObject MainMaterialPanel;
    public Transform MaterailGroup;
    public GameObject OneMaterialPrefab;

    [Space]
    [Header("Details Panel")]
    public GameObject OneMaterialPopup;
    public TMP_Text PopupTitle;
    public TMP_Text TopSectionTitle;
    public TMP_Text LeftSectionTitle;
    public RawImage LeftSectionImage;
    public TMP_Text LeftSectionInfo;
    public TMP_Text RightSectionTitle;
    public RawImage RightSctionImage;
    public TMP_Text RightSectionInfo;
    public GameObject MintButton;
    public GameObject AddButton;
    public TMP_Text MintedInfoText;
    public TMP_Text MatInforText;

    [Space]
    [Header("Mint Popup")]
    public GameObject MintPopup;
    public TMP_Text MintPopupTitle;
    public TMP_Text MintPopupTopInfo;
    public TMP_Text MintPopupBottomInfo;
    public GameObject MintPopupYesButton;

    [Space]
    [Header("Mint Popup Not Enough")]
    public GameObject NotMintPopup;
    public TMP_Text NotMintPopupTitle;
    public TMP_Text NotMintPopupTopInfo;
    public TMP_Text NotMintPopupBottomInfo;

    [Space]
    [Header("Add Popup")]
    public GameObject AddPopup;
    public TMP_Text AddPopupTitle;
    public TMP_Text AddPopupTopInfo;
    public TMP_Text AddPopupBottomInfo;
    public GameObject AddPopupYesButton;

    [Space]
    [Header("Add Popup Not Enough")]
    public GameObject NotAddPopup;
    public TMP_Text NotAddPopupTitle;
    public TMP_Text NotAddPopupTopInfo;
    public TMP_Text NotAddPopupBottomInfo;

    [Space]
    [Header("Result Popup")]
    public GameObject ResultPopup;
    public TMP_Text ResultPopupTitle;
    public Image ResultPopupMaterialImage;
    public TMP_Text ResultPopupMaterialInfoText;

    [Space]
    [Header("Variables")]
    public MaterialDataModel[] MaterialSchema;
    public AbbreviationsHelper helper;

    public RawImage done_panel_img;
    public GameObject DonePanel;
    public TMP_Text done_panel_text;
    public TMP_Text done_panel_header;


    private IEnumerator coroutine;

    // setting the header
    public delegate void SetHeader();
    public static SetHeader onSetHeaderElements;

    protected override void Start()
    {
        base.Start();
        SetMaterials();
        MessageHandler.OnTransactionData += OnTransactionData;
        ErrorHandler.OnErrorData += OnErrorData;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        MessageHandler.OnTransactionData -= OnTransactionData;
        ErrorHandler.OnErrorData -= OnErrorData;
    }

    public void SetMaterials()
    {
        if (MaterailGroup.childCount >= 1)
        {
            foreach (Transform child in MaterailGroup)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        foreach (MaterialDataModel m in MaterialSchema)
        {
            var ins = Instantiate(OneMaterialPrefab);
            ins.transform.SetParent(MaterailGroup);
            ins.transform.localScale = new Vector3(1, 1, 1);
            var child = ins.gameObject.GetComponent<OneMaterialStatus>();
            child.CountText.text = "x0";
            child.image.texture = m.image;
            // child.type = m.type;
            // child.end_product = m.end_product;
            foreach (InventoryModel r in MessageHandler.userModel.inventory)
            {
                if (m.name == r.name){
                    child.CountText.text = "x" + r.count;
                }
            }
            child.DetailButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
            child.DetailButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { DetailButtonClick(m); });
        }
    }

    public void DetailButtonClick(MaterialDataModel m)
    {
        OneMaterialPopup.SetActive(true);
        string matName = helper.mat_abv[m.name];
        PopupTitle.text = matName;
        if (m.type == "raw" || m.type == "ore")
        {
            TopSectionTitle.text = "Refine raw materials into refined materials";
        }
        else
        {
            TopSectionTitle.text= "Use refined materials to craft items";
        }
        LeftSectionTitle.text = "Mint \"" + matName + "\" as NFT Pack of 10";
        LeftSectionInfo.text = "By using the \"Mint\" button, you will be able to mint 10 \"" + matName +"\" into a \"" + matName +" - 10x'\" NFT pack and 10 \"" + matName  + "\" will be deducted from your account. Afterwards you will be able to sell that NFT on the market or trade.";
        LeftSectionImage.texture = m.image_multi;
        RightSectionTitle.text = "Add \"" + matName + "\"";
        RightSectionInfo.text = "By using the \"Add\" button, you will burn a \"" + matName + " - 10x\" NFT pack and 10 \"" + matName + "\" will be added to your account.";
        RightSctionImage.texture = m.image_multi;
        MintButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        AddButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        MintButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { MintButtonClick(m.name, matName); });
        AddButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { AddButtonClick(m.name, matName); });

        // Update current material and minted nft amount

        int mintCnt = 0;
        string template_id = "", matCnt = "";
        foreach(DelayDataModel data in MessageHandler.userModel.config.template_ids)
        {
            if(data.key == m.name)
            {
                template_id = data.value;
                break;
            }
        }

        foreach(AssetModel asset in MessageHandler.assetModel)
        {
            if(asset.template == template_id) mintCnt++;
        }

        foreach(InventoryModel inv in MessageHandler.userModel.inventory)
        {
            if(inv.name == m.name) {
                matCnt = inv.count;
                break;
            }
        }

        MintedInfoText.text = mintCnt.ToString();
        MatInforText.text = matCnt;
        
        //===================================================
    }

    public void MintButtonClick(string keyName, string matName)
    {

        if(MessageHandler.userModel.inventory.Length == 0)
        {
            NotMintPopup.SetActive(true);
            NotMintPopupTitle.text = "Mint " + matName;
            NotMintPopupTopInfo.text = "Not enough \"" + matName + "\".";
            NotMintPopupBottomInfo.text = "Unfortunately you don't have enough \"" + matName + "\" (less than 10) in your account and will need to obtain more first.";
        }
        else
        {
            bool flag = false;
            foreach(InventoryModel inv in MessageHandler.userModel.inventory)
            {
                if(inv.name == keyName)
                {
                    if ((int)float.Parse(inv.count) >= 10)
                    {
                        NotMintPopup.SetActive(false);
                        MintPopup.SetActive(true);
                        MintPopupTitle.text = "Mint " + matName + " as NFT";
                        MintPopupTopInfo.text ="Do you really want to mint 10 \"" + matName + "\" into a \"" + matName + " - 10x\" NFT pack?";
                        MintPopupBottomInfo.text = "10 \"" + matName + "\" will be deducted from your account and moved into the NFT.";
                        MintPopupYesButton.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                        MintPopupYesButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { MintPopupYesButtonClick(keyName); });
                    }
                    else if ((int)float.Parse(inv.count) < 10)
                    {
                        MintPopup.SetActive(false);
                        NotMintPopup.SetActive(true);
                        NotMintPopupTitle.text = "Mint " + matName;
                        NotMintPopupTopInfo.text = "Not enough \"" + matName + "\".";
                        NotMintPopupBottomInfo.text = "Unfortunately you don't have enough \"" + matName + "\" (less than 10) in your account and will need to obtain more first.";
                    }
                    flag = true;
                    break;
                }
            }
            if(flag == false)
            {
                MintPopup.SetActive(false);
                NotMintPopup.SetActive(true);
                NotMintPopupTitle.text = "Mint " + matName;
                NotMintPopupTopInfo.text = "Not enough \"" + matName + "\".";
                NotMintPopupBottomInfo.text = "Unfortunately you don't have enough \"" + matName + "\" (less than 10) in your account and will need to obtain more first.";
            }
        }
    }

    public void MintPopupYesButtonClick(string keyName)
    {
        LoadingPanel.SetActive(true);
        coroutine = WaitAndClose(Config.configData.TIME_LOADING_PANEL);
        StartCoroutine(coroutine);
        MintPopup.SetActive(false);
        MessageHandler.Server_MintMat(keyName, "10");
    }

    public void AddButtonClick(string keyName, string matName)
    {
        string template_id = "";
        int tempCnt = 0;
        // Debug.Log(MessageHandler.userModel.config.template_ids.Length);
        foreach(DelayDataModel data in MessageHandler.userModel.config.template_ids)
        {
            if(data.key == keyName)
            {
                template_id = data.value;
                break;
            }
        }

        // Debug.Log(template_id);
        // Debug.Log(MessageHandler.assetModel.Length);

        foreach(AssetModel asset in MessageHandler.assetModel)
        {
            if(asset.template == template_id) tempCnt++;
        }

        NotAddPopup.SetActive(false);
        AddPopup.SetActive(false);

        if(tempCnt == 0)
        {
            NotAddPopup.SetActive(true);
            NotAddPopupTitle.text = "Add " + matName + " to Account";
            NotAddPopupTopInfo.text = "No \"" + matName + " - 10x\" NFT pack in wallet.";
            NotAddPopupBottomInfo.text = "Unfortunately you don't have any \"" + matName + " - 10x\" NFT pack in your wallet and will need to obtain one first.";
        }
        else
        {
            AddPopup.SetActive(true);
            AddPopupTitle.text = "Add " + matName + " to Account";
            AddPopupTopInfo.text ="Do you really want to add 10 \"" + matName + "\" to your account?";
            AddPopupBottomInfo.text = "A \"" + matName + " - 10x\" NFT pack from your wallet will be burned and 10 \"" + matName + "\" added to your account.";
            AddPopupYesButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate { AddPopupYesButtonClick(keyName); });
        }
    }

    public void AddPopupYesButtonClick(string keyName)
    {
        LoadingPanel.SetActive(true);
        coroutine = WaitAndClose(Config.configData.TIME_LOADING_PANEL);
        StartCoroutine(coroutine);
        AddPopup.SetActive(false);
        MessageHandler.Server_BurnMat(keyName);
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

    private void OnTransactionData()
    {
        if (MessageHandler.transactionModel.transactionid != "")
        {
            DonePanel.SetActive(true);
            string[] transaction = MessageHandler.transactionModel.transactionid.Split(' ');
            string action_type = transaction[0];
            string mat_name = transaction[1];

            SetMaterials();
            foreach (MaterialDataModel data in MaterialSchema)
            {
                if (data.name == mat_name)
                {
                    done_panel_img.texture = data.image;
                    DetailButtonClick(data);
                    break;
                }
            }

            if (action_type == "Mint")
            {
                done_panel_header.text = "Minted " + helper.mat_abv[mat_name] + " NFT pack";
                done_panel_text.text = "Added \"" + helper.mat_abv[mat_name] + " - 10x\" NFT pack to your wallet.";
                MessageHandler.userModel.total_matCount = MessageHandler.transactionModel.citizens;
            }

            if (action_type == "Burn")
            {
                done_panel_header.text = "Added " + helper.mat_abv[mat_name] + " to your account";
                done_panel_text.text = "Added 10x \"" + helper.mat_abv[mat_name] + "\" to your account.";
                MessageHandler.userModel.total_matCount = MessageHandler.transactionModel.citizens;
            }

            if(action_type == "None")
            {
                DonePanel.SetActive(false);
                DisplayInfoPopup(Constants.INFO_TYPE_DEFAULT, Constants.INFO_TITLE_DEFAULT, "Unfortuantely there is nothing to burn.");
            }

            onSetHeaderElements();
        }
        LoadingPanel.SetActive(false);
        StopCoroutine(coroutine);
    }

    private void OnErrorData(string errorData)
    {
        // Debug.Log(errorData + "==============Error Handler==============");
        DisplayInfoPopup(Constants.INFO_TYPE_DEFAULT, Constants.INFO_TITLE_DEFAULT, errorData);
    }
}
