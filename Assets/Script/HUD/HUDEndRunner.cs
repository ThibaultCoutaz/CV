using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDEndRunner : HUDElement {

    public void Init(string t)
    {
        GetComponent<Text>().text = t;
    }

    public void CloseGame()
    {
        HUDManager.Instance.DisplayEndRunner(false);
        HUDManager.Instance.DisplayLifeRunner(false);
        HUDManager.Instance.StartScene("Dialogue");
    }

    public void Restart()
    {
        HUDManager.Instance.DisplayEndRunner(false);
        HUDManager.Instance.DisplayLifeRunner(false);
        HUDManager.Instance.StartScene("Runner");
    }

}
