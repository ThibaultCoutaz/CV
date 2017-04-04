using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDTowerShop : HUDElement {

    [System.Serializable]
    public struct elementStruct
    {
        public Sprite image;
        public string descritpion;
        public float price;
        public Game.Tower_Types typeTower;
    }

    public GameObject element;
    public Transform parentElement;
    private GameObject[] listElements;

    public void Init(elementStruct[] elements)
    {
        listElements = new GameObject[elements.Length];
        for(int i = 0; i < elements.Length; i++)
        {
            listElements[i] = Instantiate(element, parentElement);
            listElements[i].GetComponent<HUDElementShop>().Init(elements[i].image, elements[i].descritpion, elements[i].price, elements[i].typeTower);
        }
    }

    public void InitScriptTower(TowerBehaviour script)
    {
        HUDManager.Instance.DisplayLevelUpTower(false, script);

        for (int i = 0; i < listElements.Length; i++)
        {
            listElements[i].GetComponent<HUDElementShop>().InitScript(script);
        }
    }

    public void ExitShop()
    {
        HUDManager.Instance.DisplayTowerShop(false);
    }
}
