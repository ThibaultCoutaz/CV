using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDStartWave : HUDElement {

    private GameManagerTower scriptmanager;

	public void InitButtonStartWave(GameManagerTower script)
    {
        scriptmanager = script;
    }

    public void StartWave()
    {
        scriptmanager.StartWave();
        GetComponent<Button>().interactable = false;
    }

    public void EnableButton()
    {
        GetComponent<Button>().interactable = true;
    }
}
