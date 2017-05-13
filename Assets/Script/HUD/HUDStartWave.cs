using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDStartWave : HUDElement {

    private GameManagerTower scriptmanager;
    public Text textButton;
    private int indexWave = 1;

	public void InitButtonStartWave(GameManagerTower script)
    {
        scriptmanager = script;
        textButton.text = "Start Wave "+indexWave;
    }

    public void StartWave()
    {
        textButton.text = "Wave " + indexWave + " en cours";
        scriptmanager.StartWave();
        GetComponent<Button>().interactable = false;
    }

    public void EnableButton()
    {
        indexWave++;
        textButton.text = "Start Wave " + indexWave;
        GetComponent<Button>().interactable = true;
    }
}
