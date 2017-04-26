using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageDialogue : MonoBehaviour {

    public string[] Dialogue;

    private int indexDialg;

	// Use this for initialization
	void Start () {
        indexDialg = PlayerPrefs.GetInt("story"); // A changer pour sauver quand on change de scene
        Debug.LogError(PlayerPrefs.GetInt("story"));
        HUDManager.Instance.InitDialogue(this);
        HUDManager.Instance.DisplayDialogue(true);
        HUDManager.Instance.SetTextDialogue(Dialogue[indexDialg]);
    }
	
    public void NextSpeach()
    {
        if (indexDialg == 4)
        {
            indexDialg++;
            PlayerPrefs.SetInt("story", indexDialg);
            HUDManager.Instance.DisplayDialogue(false);
            HUDManager.Instance.StartScene("Runner");
        }
        else if (indexDialg == 6)
        {
            indexDialg++;
            PlayerPrefs.SetInt("story", indexDialg);
            HUDManager.Instance.DisplayDialogue(false);
            HUDManager.Instance.StartScene("Shooter");
        }
        else if(indexDialg == 12)
        {
            indexDialg++;
            PlayerPrefs.SetInt("story", indexDialg);
            HUDManager.Instance.DisplayDialogue(false);
            HUDManager.Instance.StartScene("TurnFight");
            PlayerPrefs.SetInt("EnnemyIndexTurnFight", 0);
        }
        else if (indexDialg == 15)
        {
            indexDialg++;
            PlayerPrefs.SetInt("story", indexDialg);
            HUDManager.Instance.DisplayDialogue(false);
            HUDManager.Instance.StartScene("TurnFight");
            PlayerPrefs.SetInt("EnnemyIndexTurnFight", 1);
        }
        else if (indexDialg == 17)
        {
            indexDialg++;
            PlayerPrefs.SetInt("story", indexDialg);
            HUDManager.Instance.DisplayDialogue(false);
            HUDManager.Instance.StartScene("TurnFight");
            PlayerPrefs.SetInt("EnnemyIndexTurnFight", 2);
        }
        else
        {
            indexDialg++;
            PlayerPrefs.SetInt("story", indexDialg);
            HUDManager.Instance.SetTextDialogue(Dialogue[indexDialg]);
        }

    }
}
