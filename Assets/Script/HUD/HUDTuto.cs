using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDTuto : HUDElement {

    public enum tutoStyle
    {
        Runner,
        Bucket,
        None
    }

    private GameObject currentTuto;

    public GameObject tutoRunner;
    public GameObject tutoBucket;

    public void Init(tutoStyle tutoStylish)
    {
        if (tutoStylish == tutoStyle.Runner)
            currentTuto = tutoRunner;
        else if (tutoStylish == tutoStyle.Bucket)
            currentTuto = tutoBucket;

        currentTuto.SetActive(true);
    }

    public void StartPlaying()
    {
        HUDManager.Instance.DisplayTuto(false);
        HUDManager.Instance.DisplayTimer(true);
    }
}
