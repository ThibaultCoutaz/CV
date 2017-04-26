using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDEndRunner : HUDElement {

    public void CloseGame()
    {
        HUDManager.Instance.StartScene("Dialogue");
    }

}
