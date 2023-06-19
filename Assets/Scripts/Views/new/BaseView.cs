using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using System.Collections;

public class BaseView : MonoBehaviour
{
    [Space]
    [Header("General")]
    public GameObject LoadingPanel;

    [Space]
    [Header("InfoPanel")]
    public GameObject InfoPopup;
    public TMP_Text InfoTitle;
    public TMP_Text InfoText;

    private void Awake() {}

    protected virtual void Start()
    {
        Config.LoadConfigData();
        ErrorHandler.OnErrorData += OnErrorData;
    }

    protected virtual void OnDestroy()
    {
        ErrorHandler.OnErrorData -= OnErrorData;
    }

    private void OnErrorData(string errorData)
    {
        // Debug.Log(errorData + "==============Error Handler==============");
        // LoadingPanel.SetActive(false);
        // InfoText.text = errorData;
        // InfoPopup.SetActive(true);
    }

    private void Update() {}

    public void DisplayInfoPopup(string infoType, string infoTitle, string infoText)
    {
        LoadingPanel.SetActive(false);
        string title = "";
        if(infoType == Constants.INFO_TYPE_SUCCESS) {
            title = Constants.INFO_TITLE_SUCCESS;
        } else if (infoType == Constants.INFO_TYPE_ERROR) {
            title = Constants.INFO_TITLE_ERROR;
        } else {
            title = Constants.INFO_TITLE_DEFAULT;
        }
        InfoTitle.text = title;
        InfoText.text = infoText;
        InfoPopup.SetActive(true);
    }

    public void HideInfoPopup() {
        InfoPopup.SetActive(false);
    }

}