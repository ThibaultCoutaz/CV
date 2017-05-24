using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDStartWave : HUDElement {

    private GameManagerTower scriptmanager;
    public Text textButton;
    private int indexWave;
    private int maxWave;

	public void InitButtonStartWave(GameManagerTower script)
    {
        scriptmanager = script;
        indexWave = script.currentWave + 1;
        maxWave = script.waveMonster.Length;
        textButton.text = "Start Wave "+ indexWave +"/"+ maxWave;
        GetComponent<Button>().interactable = true;
    }

    public void StartWave()
    {
        textButton.text = "Wave " + indexWave + " en cours";
        scriptmanager.StartWave();
        GetComponent<Button>().interactable = false;
        indexWave++;
    }

    public void EnableButton()
    {
        textButton.text = "Start Wave " + indexWave+"/"+maxWave;
        GetComponent<Button>().interactable = true;
    }
}
