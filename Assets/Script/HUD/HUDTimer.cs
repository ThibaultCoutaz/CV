using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDTimer : HUDElement {

    public Sprite[] timerNumber;

    private bool start = false;
    private int currentCount = 0;
    private CharacterBehaviour player;

    public void StartCount(CharacterBehaviour _player)
    {
        player = _player;
        start = true;
        currentCount = timerNumber.Length;
        ProgressCount();
    }
	
	// Update is called once per frame
	void Update () {
        if (start)
        {
            start = false;
            Invoke("ProgressCount", 1);
        }
	}

    private void ProgressCount()
    {
        if (currentCount > 0)
        {
            GetComponent<Image>().sprite = timerNumber[currentCount - 1];
            currentCount--;
            start = true;
        }
        else
        {
            player.StartGame();
            HUDManager.Instance.DisplayTimer(false,player);
        }
    }
}
