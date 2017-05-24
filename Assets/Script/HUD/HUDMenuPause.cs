using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDMenuPause : HUDElement {

    private string nextScene = "";
    private string previousScene = "";

    public HUDTuto tutoScript;

    public void Init(string _nextScene, string _previousScene)
    {
        nextScene = _nextScene;
        previousScene = _previousScene;
    }

    public void PassLevel()
    {
        DiseableHUD();
        HUDManager.Instance.StartScene(nextScene);
    }

    public void BackToMenu()
    {
        DiseableHUD();
        HUDManager.Instance.StartScene("Menu");
    }

    private void DiseableHUD()
    {
        if (previousScene == "Runner")
        {
            HUDManager.Instance.DisplayEndRunner(false);
            HUDManager.Instance.DisplayLifeRunner(false);
            HUDManager.Instance.DisplayTimer(false);
            HUDManager.Instance.DisplayTuto(false);
        }
        else if (previousScene == "Shooter")
        {
            HUDManager.Instance.DisplayShooter(false);
            HUDManager.Instance.DisplayTimer(false);
            HUDManager.Instance.DisplayTuto(false);
            HUDManager.Instance.DisplayEndScreenShooter(false);
        }
        else if (previousScene == "TurnFight")
        {
            HUDManager.Instance.RestartTurnFight();
            HUDManager.Instance.DisplayTurnFight(false);
            HUDManager.Instance.DisplayTimer(false);
            HUDManager.Instance.DisplayTuto(false);
            HUDManager.Instance.ActivateButton(false);
        }
        else if (previousScene == "TowerDefence")
        {
            HUDManager.Instance.DisplayTowerShop(false);
            HUDManager.Instance.DisplayMoneyTowerDefence(false);
            HUDManager.Instance.DisplayStartWave(false);
            HUDManager.Instance.DisplayLevelUpTower(false, null);
            HUDManager.Instance.DisplayEndScreenTowerDefence(false);
            HUDManager.Instance.DisplayTimer(false);
            HUDManager.Instance.DisplayTuto(false);
        }
        HUDManager.Instance.DisplayMenuPause(false);

    }

    public void OpenTutorial()
    {
        HUDManager.Instance.DisplayTuto(true ,false,tutoScript.currentTutoStyle);
        HUDManager.Instance.DisplayMenuPause(false);
    }

}












