using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDEndTowerDefence : HUDElement {

    public Text textTitle;

    public void SetTitle(string t)
    {
        textTitle.text = t;
    }

    public void Restart()
    {
        HUDManager.Instance.DisplayTowerShop(false);
        HUDManager.Instance.DisplayMoneyTowerDefence(false);
        HUDManager.Instance.DisplayStartWave(false);
        HUDManager.Instance.DisplayLevelUpTower(false, null);
        HUDManager.Instance.DisplayTimer(false);
        HUDManager.Instance.DisplayTuto(false);
        HUDManager.Instance.DisplayEndScreenTowerDefence(false);
        HUDManager.Instance.StartScene("TowerDefence");
    }
}
