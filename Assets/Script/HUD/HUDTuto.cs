using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDTuto : HUDElement {

    public enum tutoStyle
    {
        Runner,
        Bucket,
        Shooter,
        TurnFight,
        TowerDefence,
        None
    }
    
    private GameObject currentTuto;
    [HideInInspector]
    public tutoStyle currentTutoStyle;

    public GameObject buttonQuit;
    public GameObject buttonStartPlaying;
    public GameObject buttonPassLevel;

    public GameObject tutoRunner;
    public GameObject tutoShooter;
    public GameObject tutoTurnFight;
    public GameObject tutoTowerDefence;

    public void Init(tutoStyle tutoStylish,bool firstTime)
    {
        buttonStartPlaying.SetActive(firstTime);
        buttonPassLevel.SetActive(firstTime);
        buttonQuit.SetActive(!firstTime);

        if (tutoStylish == tutoStyle.Runner)
            currentTuto = tutoRunner;
        //else if (tutoStylish == tutoStyle.Bucket)
        //    currentTuto = tutoBucket;
        if (tutoStylish == tutoStyle.Shooter)
            currentTuto = tutoShooter;
        if (tutoStylish == tutoStyle.TurnFight)
            currentTuto = tutoTurnFight;
        if (tutoStylish == tutoStyle.TowerDefence)
            currentTuto = tutoTowerDefence;

        currentTutoStyle = tutoStylish;
        currentTuto.SetActive(true);
    }

    public void StartPlaying()
    {
        if (currentTuto == tutoShooter)
            Cursor.visible = false;
        currentTuto.SetActive(false);
        HUDManager.Instance.DisplayTuto(false);
        if(currentTuto != tutoTowerDefence)
            HUDManager.Instance.DisplayTimer(true);
    }

    public void QuitTutoMenu()
    {
        currentTuto.SetActive(false);
        HUDManager.Instance.DisplayTuto(false);
        HUDManager.Instance.DisplayMenuPause(true);
    }

    public void PassLevel()
    {
        currentTuto.SetActive(false);
    }
}
