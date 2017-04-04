using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageDialogue : MonoBehaviour {

    public string[] Dialogue;

    private int indexDialg;

	// Use this for initialization
	void Start () {
        indexDialg = 0; // A changer pour sauver quand on change de scene
        HUDManager.Instance.InitDialogue(this);
        HUDManager.Instance.DisplayDialogue(true);
        HUDManager.Instance.SetTextDialogue(Dialogue[indexDialg]);
    }
	
    public void NextSpeach()
    {
        indexDialg++;
        HUDManager.Instance.SetTextDialogue(Dialogue[indexDialg]);
    }
}
