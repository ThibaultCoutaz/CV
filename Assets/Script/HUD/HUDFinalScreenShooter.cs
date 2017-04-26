using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDFinalScreenShooter : HUDElement {

    public Text textInfo;

    public void Init(string text)
    {
        textInfo.text = text;
    }
	
    public void Restart()
    {
        HUDManager.Instance.DisplayShooter(false);
        HUDManager.Instance.DisplayTimer(false);
        HUDManager.Instance.DisplayTuto(false);
        HUDManager.Instance.DisplayEndScreenShooter(false);
        HUDManager.Instance.DisplayMenuPause(false);
        HUDManager.Instance.StartScene("Shooter");
    }

    public void NextLevel()
    {
        HUDManager.Instance.DisplayShooter(false);
        HUDManager.Instance.DisplayTimer(false);
        HUDManager.Instance.DisplayTuto(false);
        HUDManager.Instance.DisplayEndScreenShooter(false);
        HUDManager.Instance.DisplayMenuPause(false);
        HUDManager.Instance.StartScene("Dialogue");
    }
}
