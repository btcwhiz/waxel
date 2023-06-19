using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartView : BaseView
{
    public GameObject Login_btn;
    public GameObject Start_btn;
    public GameObject NodeSel;
    public GameObject ApiSel;

    protected override void Start()
    {
        base.Start();
        MessageHandler.onLoadingData += onLoadingData;
        MessageHandler.OnLoginData += doLoginSuccessAction;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        MessageHandler.onLoadingData -= onLoadingData;
        MessageHandler.OnLoginData -= doLoginSuccessAction;
    }

    public void OnAnchorButtonClick()
    {
        MessageHandler.LoginRequest("anchor", NodeSel.gameObject.GetComponent<TMPro.TMP_Dropdown>().value.ToString(), ApiSel.gameObject.GetComponent<TMPro.TMP_Dropdown>().value.ToString());
    }

    public void OnWaxButtonClick()
    {
        MessageHandler.LoginRequest("cloud", NodeSel.gameObject.GetComponent<TMPro.TMP_Dropdown>().value.ToString(), ApiSel.gameObject.GetComponent<TMPro.TMP_Dropdown>().value.ToString());
    }

    public void onCreateWalletButtonClick()
    {
        Application.OpenURL("https://waxel.net/set-up-wax-wallet/");
    }

    private void onLoadingData(string status)
    {
        if(status == "true")
        {
            Login_btn.SetActive(false);
            Start_btn.SetActive(false);
        }
    }

    private void doLoginSuccessAction()
    {
        // GameObjectHandler.OpenScene("MinigameScene");
        GameObjectHandler.OpenScene("MapScene");
    }
}
