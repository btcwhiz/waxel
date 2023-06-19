using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CampShow : BaseView
{
    public GameObject MintConfirmPopup;
    public GameObject MintSuccessPopup;
    public GameObject NoCitizenToMintPopup;
    public GameObject AddConfirmPopup;
    public GameObject AddSucessPopup;
    public GameObject NoCitizenToAddPopup;

    public TMP_Text MintedInfoText;
    public TMP_Text CitizenInfoText;

    private IEnumerator coroutine;

    // setting the header
    public delegate void SetHeader();
    public static SetHeader onSetHeaderElements;

    protected override void Start()
    {
        base.Start();
        SetUIElements();
        MessageHandler.OnTransactionData += OnTransactionData;
        ErrorHandler.OnErrorData += OnErrorData;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        MessageHandler.OnTransactionData -= OnTransactionData;
        ErrorHandler.OnErrorData -= OnErrorData;
    }

    private void SetUIElements()
    {
        MintedInfoText.text = MessageHandler.userModel.citizens_pack_count.ToString();
        CitizenInfoText.text = MessageHandler.userModel.citizens;
    }

    public void MintButtonClick()
    {
        if (Int64.Parse(MessageHandler.userModel.citizens) >= 10)
        {
            MintConfirmPopup.SetActive(true);
        }
        else if(Int64.Parse(MessageHandler.userModel.citizens) < 10)
        {
            NoCitizenToMintPopup.SetActive(true);
        }
    }
    public void AddButtonClick()
    {
        if (MessageHandler.userModel.citizens_pack_count > 0)
        {
            AddConfirmPopup.SetActive(true);
        }
        else
        {
            NoCitizenToAddPopup.SetActive(true);
        }
    }
    public void MintYesButtonClick()
    {
        LoadingPanel.SetActive(true);
        coroutine = WaitAndClose(Config.configData.TIME_LOADING_PANEL);
        StartCoroutine(coroutine);
        MintConfirmPopup.SetActive(false);
        MessageHandler.Server_MintCitizenPack();
    }
    public void AddYesButtonClick()
    {
        LoadingPanel.SetActive(true);
        coroutine = WaitAndClose(Config.configData.TIME_LOADING_PANEL);
        StartCoroutine(coroutine);
        AddConfirmPopup.SetActive(false);
        MessageHandler.Server_BurnCitizenPack();
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
        LoadingPanel.SetActive(false);
        StopCoroutine(coroutine);
        if (MessageHandler.transactionModel.transactionid != "")
        {
            if (MessageHandler.transactionModel.transactionid == "Mint")
            {
                MintSuccessPopup.SetActive(true);
                MessageHandler.userModel.citizens = MessageHandler.transactionModel.citizens;
                MessageHandler.userModel.citizens_pack_count = MessageHandler.transactionModel.citizens_pack_count;
                // MessageHandler.userModel.citizens = callBack.totalCitizensCount;
            }
            if (MessageHandler.transactionModel.transactionid == "Burn")
            {
                AddSucessPopup.SetActive(true);
                MessageHandler.userModel.citizens = MessageHandler.transactionModel.citizens;
                MessageHandler.userModel.citizens_pack_count = MessageHandler.transactionModel.citizens_pack_count;
            }
            MintedInfoText.text = MessageHandler.transactionModel.citizens_pack_count.ToString();
            CitizenInfoText.text = MessageHandler.transactionModel.citizens;
            onSetHeaderElements();
        }
    }

    private void OnErrorData(string errorData)
    {
        // Debug.Log(errorData + "==============Error Handler==============");
        DisplayInfoPopup(Constants.INFO_TYPE_DEFAULT, Constants.INFO_TITLE_DEFAULT, errorData);
    }
}
