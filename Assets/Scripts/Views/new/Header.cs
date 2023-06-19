using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class Header : BaseView
{
    public TMP_Text username;
    public TMP_Text citizens;
    public TMP_Text professions;
    public TMP_Text materials;
    public TMP_Text ninjas;
    public TMP_Text items;
    //public GameObject LoadingPanel;
    public GameObject SyncPopup;
    public GameObject EventsPopup;
    protected override void Start()
    {
        base.Start();
        NinjaShow.onSetHeaderElements += onSetHeaderElements;
        CampShow.onSetHeaderElements += onSetHeaderElements;
        SchoolShow.onSetHeaderElements += onSetHeaderElements;
        WarehouseShow.onSetHeaderElements += onSetHeaderElements;
        WorkshopShow.onSetHeaderElements += onSetHeaderElements;
        MessageHandler.OnReloadSync += OnReloadSync;
        onSetHeaderElements();
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        NinjaShow.onSetHeaderElements -= onSetHeaderElements;
        CampShow.onSetHeaderElements -= onSetHeaderElements;
        SchoolShow.onSetHeaderElements -= onSetHeaderElements;
        WarehouseShow.onSetHeaderElements -= onSetHeaderElements;
        WorkshopShow.onSetHeaderElements -= onSetHeaderElements;
        MessageHandler.OnReloadSync -= OnReloadSync;
    }

    private void onSetHeaderElements()
    {
        // Debug.Log(MessageHandler.userModel.account + "account");
        if (MessageHandler.userModel.account != null)
        {
            // Debug.Log(MessageHandler.userModel.ninjas.Length.ToString());
            ninjas.text = MessageHandler.userModel.ninjas.Length.ToString();
            // Debug.Log(MessageHandler.userModel.citizens);
            citizens.text = MessageHandler.userModel.citizens;
            // Debug.Log(MessageHandler.userModel.professions.Length.ToString());
            professions.text = MessageHandler.userModel.professions.Length.ToString();
            // Debug.Log((float.Parse(MessageHandler.userModel.total_matCount)).ToString());
            materials.text = (float.Parse(MessageHandler.userModel.total_matCount)).ToString();
            // Debug.Log(MessageHandler.userModel.items.Length.ToString());
            items.text = MessageHandler.userModel.items.Length.ToString();
            // Debug.Log(MessageHandler.userModel.account);
            username.text = MessageHandler.userModel.account;
        }
    }

    public void profession_btn()
    {
        SceneManager.LoadScene("ProfessionScene");
    }

    public void citizen_btn()
    {
        SceneManager.LoadScene("CitizensScene");
    }

    public void material_btn()
    {
        SceneManager.LoadScene("MaterialsScene");
    }

    public void ninjas_btn()
    {
        SceneManager.LoadScene("NinjaScene");
    }

    public void market_btn()
    {
        Application.OpenURL(Config.configData.MARKET_SECONDARY_ALL);
    }

    public void shop_btn()
    {
        SceneManager.LoadScene("ShopScene");
    }

    public void workshop_btn()
    {
        SceneManager.LoadScene("WorkshopScene");
    }

    public void reloadSyncClick()
    {
        SceneManager.LoadScene("MapScene");
        EventsPopup.SetActive(false);
        SyncPopup.SetActive(true);
        MessageHandler.Server_Reload();
    }

    public void OnReloadSync()
    {
        onSetHeaderElements();
        Invoke("SetFalse",5.0f); // disable after 5 seconds
    }

    void SetFalse()
    {
        SyncPopup.SetActive(false);
    }
}
