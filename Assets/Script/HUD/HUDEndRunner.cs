using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDEndRunner : HUDElement {

    public void CloseGame()
    {
        HUDManager.Instance.DisplayEndRunner(false);
        HUDManager.Instance.DisplayLifeRunner(false);
        HUDManager.Instance.StartScene("Dialogue");
    }

}
