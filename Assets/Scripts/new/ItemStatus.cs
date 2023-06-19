using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ItemStatus : MonoBehaviour
{
    public RawImage image;
    public TMP_Text AssetIdText;
    public TMP_Text UseLeftText;
    public GameObject EquipButton;
    public GameObject UnEquipButton;
    public GameObject BurnButton;
    public GameObject SellButton;
    public string asset_id;
    public string mat_name;
    public string p_id;
    public void BurnButtonClick()
    {
        if (!string.IsNullOrEmpty(asset_id))
        {
            // LoadingPanel.SetActive(true);
            MessageHandler.Server_BurnItem(mat_name, asset_id);
        }
        else
        {
            // SSTools.ShowMessage("Asset ID is null.", SSTools.Position.bottom, SSTools.Time.twoSecond);
        }
    }



}
