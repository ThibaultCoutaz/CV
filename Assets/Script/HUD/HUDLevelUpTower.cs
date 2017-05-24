using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDLevelUpTower : HUDElement {

    private TowerBehaviour scriptTower;

    [System.Serializable]
    public struct stats
    {
        public Text level;
        public Text textDmg;
        public Text textSpeed;
    }

    public Button levelUp;
    public stats[] statList;
    public Text priceLevelUp;

    public void Init(TowerBehaviour script)
    {
        scriptTower = script;
        levelUp.interactable = true;
        InitText();
    }

    private void InitText()
    {
        int currentlevel = scriptTower.GetCurrentLevel();
        statList[0].level.text = "lvl." + (currentlevel+1);
        statList[0].textDmg.text = "Damages : "+scriptTower.GetInfosLevels(currentlevel).dmg.ToString();
        statList[0].textSpeed.text = "Speed Attaques : " + scriptTower.GetInfosLevels(currentlevel).speedAttack.ToString();

        if (!scriptTower.IsMaxLevel())
        {
            statList[1].level.text = "lvl." + (currentlevel+2);
            statList[1].textDmg.text = "Damages : " + scriptTower.GetInfosLevels(currentlevel + 1).dmg.ToString();
            statList[1].textSpeed.text = "Speed Attaques : " + scriptTower.GetInfosLevels(currentlevel + 1).speedAttack.ToString();
            priceLevelUp.text = scriptTower.GetInfosLevels(currentlevel).priceLevelUp.ToString() + " Golds";
        }
        else
        {
            statList[1].level.text = "MaxLvl";
            statList[1].textDmg.text = "MaxLevel";
            statList[1].textSpeed.text = "MaxLevel";
            priceLevelUp.text = "0 Gold";
            levelUp.interactable = false;
        }

        if (scriptTower.GetInfosLevels(currentlevel).priceLevelUp > scriptTower.scriptManager.currentMoney)
            levelUp.interactable = false;
        else
            levelUp.interactable = true;
    }

    public void LevelUp()
    {
        scriptTower.LevelUpTower();
        InitText();
    }

    public void Sell()
    {
        scriptTower.SellTower();
        HUDManager.Instance.DisplayLevelUpTower(false, scriptTower);
        InitText();
    }

    public void Exit()
    {
        HUDManager.Instance.DisplayLevelUpTower(false, scriptTower);
    }
}
