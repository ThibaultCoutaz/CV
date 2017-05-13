using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageDialogue : MonoBehaviour {

    public string[] Dialogue;
    public string[] Replys;

    private int indexDialg;
    private int indexReplys;

	// Use this for initialization
	void Start () {
        indexDialg = PlayerPrefs.GetInt("story"); // A changer pour sauver quand on change de scene
        HUDManager.Instance.InitDialogue(this);
        HUDManager.Instance.DisplayDialogue(true);
        HUDManager.Instance.SetTextDialogue(Dialogue[indexDialg]);
        indexReplys = 0;
    }

    public void InitText() //In the cas we didn't left the DialogueScene
    {
        indexDialg++;
        HUDManager.Instance.SetTextDialogue(Dialogue[indexDialg]);
        indexReplys = 0;
    }
	
    public void NextSpeach()
    {
        indexDialg++;
        Debug.LogError(indexDialg);
        if (indexDialg == 5)
        {
            PlayerPrefs.SetInt("story", indexDialg);
            HUDManager.Instance.DisplayDialogue(false);
            HUDManager.Instance.StartScene("Runner");
        }
        else if (indexDialg == 7)
        {
            PlayerPrefs.SetInt("story", indexDialg);
            HUDManager.Instance.DisplayDialogue(false);
            HUDManager.Instance.StartScene("Shooter");
        }
        else if(indexDialg == 13)
        {
            PlayerPrefs.SetInt("story", indexDialg);
            HUDManager.Instance.DisplayDialogue(false);
            HUDManager.Instance.StartScene("TurnFight");
            PlayerPrefs.SetInt("EnnemyIndexTurnFight", 0);
        }
        else if (indexDialg == 16)
        {
            PlayerPrefs.SetInt("story", indexDialg);
            HUDManager.Instance.DisplayDialogue(false);
            HUDManager.Instance.StartScene("TurnFight");
            PlayerPrefs.SetInt("EnnemyIndexTurnFight", 1);
        }
        else if (indexDialg == 18)
        {
            PlayerPrefs.SetInt("story", indexDialg);
            HUDManager.Instance.DisplayDialogue(false);
            HUDManager.Instance.StartScene("TurnFight");
            PlayerPrefs.SetInt("EnnemyIndexTurnFight", 2);
        }
        else if (indexDialg == 22)
        {
            PlayerPrefs.SetInt("story", indexDialg);
            HUDManager.Instance.DisplayDialogue(false);
            HUDManager.Instance.StartScene("TowerDefence");
            PlayerPrefs.SetInt("EnnemyIndexTurnFight", 0);
        }
        else
        {
            PlayerPrefs.SetInt("story", indexDialg);
            HUDManager.Instance.SetTextDialogue(Dialogue[indexDialg]);
            if(indexDialg == 20)
            {
                HUDManager.Instance.DisplayButton(true);
            }
        }
    }

    public void NextReply()
    {
        if (Replys.Length == 0)
        {
            Debug.LogError("No Replys");
            return;
        }

        if (indexReplys >= Replys.Length)
        {
            InitText();
            HUDManager.Instance.DisplayButton(false);
            return;
        }

        HUDManager.Instance.SetTextDialogue(Replys[indexReplys]);
        indexReplys++;
    }
}
