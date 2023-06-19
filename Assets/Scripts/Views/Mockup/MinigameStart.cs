using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MinigameStart : BaseView
{
    public GameObject PlayButton;
    public GameObject StartPopup;
    public GameObject NocitizenPopup;
    public GameObject PopupYesButton;

    protected override void Start()
    {
        base.Start();
        MessageHandler.OnCallBackData += onCallBackData;
        MessageHandler.OnTransactionData += OnTransactionData;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        MessageHandler.OnCallBackData -= onCallBackData;
        MessageHandler.OnTransactionData -= OnTransactionData;
    }

    // private void doLoginSuccessAction()
    // {
    //     // GameObjectHandler.OpenScene("MapScene");
    //     int citizens = int.Parse(MessageHandler.userModel.citizens);
    //     if(citizens >= 1) {
    //         StartPopup.SetActive(true);
    //     }
    //     else {
    //         NocitizenPopup.SetActive(true);
    //     }
    // }

    public void PlayButtonClick() {
        if(Int64.Parse(MessageHandler.userModel.citizens) > 0) {
            StartPopup.SetActive(true);
        }
        else {
            NocitizenPopup.SetActive(true);
        }
    }

    public void PopupYesButtonClick() {
        MessageHandler.Server_MinigameStart();
    }

    private void onCallBackData(CallBackDataModel[] callback) {
    }

    private void OnTransactionData() {
        MessageHandler.userModel.citizens = MessageHandler.transactionModel.citizens;
        GameObjectHandler.OpenScene("MapScene");
        Debug.Log("OK");
    }
}
