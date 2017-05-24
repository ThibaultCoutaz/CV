using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDElementShop : MonoBehaviour {

    public Image logo;
    public Text description;
    public Text priceText;
    private float price;
    public Game.Tower_Types towerType;
    public Button buy;

    TowerBehaviour towerScript;

    public void Init(Sprite _logo, string _description, float _price,Game.Tower_Types type)
    {
        logo.sprite = _logo;
        description.text = _description;
        priceText.text = _price.ToString();
        price = _price;
        towerType = type;
    }

    public void InitScript(TowerBehaviour script)
    {
        towerScript = script;
        if (price > towerScript.scriptManager.currentMoney)
            buy.interactable = false;
        else
            buy.interactable = true;
    }

    public void Buy()
    {
        towerScript.SetTower(towerType,price);
        HUDManager.Instance.DisplayTowerShop(false);
    }
}
