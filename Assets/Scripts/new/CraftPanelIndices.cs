using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftPanelIndices : MonoBehaviour
{
    public RefineRarityIndex[] Series;
    public RawImage[] craft_img;
    public RawImage end_product;
    public TMP_Text[] ing_name;
    public TextMeshProUGUI[] ing_qty;
    public TextMeshProUGUI[] req_qty;

    public TMP_Text product_name;
    public TMP_Text product_durability;
    public string profession_id;
    public string profession_name;
    // junks
    public GameObject grade_obj_parent;
    public GameObject ing_obj_parent;
    public GameObject EngineerType;
    public GameObject BlacksmithType;
    public TMP_Text product_rarity;
    public TMP_Text product_function;
    public TMP_Text product_equipped_By;
}
