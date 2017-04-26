using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDShooter : HUDElement {

    public Text Timer;
    public Text score;
    public Image manche;

    public Sprite manche1Sprite;
    public Sprite manche2Sprite;

    public GameObject viewFinder;

    public void Init(int timeTotal)
    {
        if (timeTotal < 10)
            Timer.text = "0" + timeTotal + ":00";
        else
            Timer.text = timeTotal + ":00";
        manche.sprite = manche1Sprite;
    }

    public void SetTimer(double time)
    {
        float minutes = Mathf.Floor((float)time / 60);
        float seconds = Mathf.Floor((float)time % 60);

        Timer.text = string.Format("{00:00}:{01:00}", minutes, seconds);
    }

    public void SetScore(int scoret)
    {
        score.text = "Score = "+scoret.ToString();
    }

    public void SetManche(bool isManche2)
    {
        if (!isManche2)
            manche.sprite = manche1Sprite;
        else
            manche.sprite = manche2Sprite;
    }

    public void DisplayViewFinder(bool display)
    {
        viewFinder.SetActive(display);
    }
}
