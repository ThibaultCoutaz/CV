using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDMenuPause : HUDElement {

    private string nextScene = "";
    private string previousScene = "";

    public void Init(string _nextScene,string _previousScene)
    {
        nextScene = _nextScene;
        previousScene = _previousScene;
    }

    public void PassLevel()
    {
        if(previousScene == "Runner")
        {
            HUDManager.Instance.DisplayEndRunner(false);
            HUDManager.Instance.DisplayLifeRunner(false);
            HUDManager.Instance.DisplayTimer(false);
            HUDManager.Instance.DisplayTuto(false);
        }
        else if(previousScene == "Shooter")
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
        }
        HUDManager.Instance.DisplayMenuPause(false);
        HUDManager.Instance.StartScene(nextScene);
    }

}












