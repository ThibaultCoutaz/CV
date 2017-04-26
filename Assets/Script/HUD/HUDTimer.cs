using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDTimer : HUDElement {

    public Sprite[] timerNumber;

    private bool start = false;
    private int currentCount = 0;

    private TimerManager scriptManager;

    public void InitTimer(TimerManager script)
    {
        scriptManager = script;
    }

    public void StartCount()
    {
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
            scriptManager.canStartGame = true;
            HUDManager.Instance.DisplayTimer(false);
        }
    }
}
