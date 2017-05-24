using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDLifeRunner : HUDElement {

    public Sprite iconLifeFull;
    public Sprite iconLifeEmpty;
    public GameObject goIcon;
    private GameObject[] listLife;
    private int lifeToRemove;

    public void InitLife(int nbLife)
    {
        ResetLife();
        lifeToRemove = nbLife - 1;
        listLife = new GameObject[nbLife];
        for(int i = 0; i < nbLife; i++)
        {
            listLife[i] = Instantiate(goIcon, transform);
            listLife[i].GetComponent<Image>().sprite = iconLifeFull;
        }
    }

    public void RemoveLife(int nb)
    {
        for(int i=0; i< nb; i++)
        {
            listLife[lifeToRemove].GetComponent<Image>().sprite = iconLifeEmpty;
            lifeToRemove--;
        }
    }

    private void ResetLife()
    {
        if(listLife != null)
        {
            for(int i = 0; i < listLife.Length; i++)
            {
                Destroy(listLife[i]);
            }
        }
    }

}
