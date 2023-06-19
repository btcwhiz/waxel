using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OneCampStatus : MonoBehaviour
{
    public ImageLoader img;
    public TMP_Text name;
    public GameObject Register;
    public GameObject Unregister;

    public GameObject SellBtn;

    public string assetId;

    public void RegisterButtonClick()
    {
        MessageHandler.Server_TransferAsset(assetId, "regupgrade","Camp");
    }
        public void UnregisterButtonClick()
    {
        MessageHandler.Server_WithdrawAsset(assetId, "Camp");
    }
}
