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

    public Sprite tutoRunner;
    public Sprite tutoBucket;

    public void Init(tutoStyle tutoStylish)
    {
        if (tutoStylish == tutoStyle.Runner)
            GetComponent<Image>().sprite = tutoRunner;

        else if (tutoStylish == tutoStyle.Bucket)
            GetComponent<Image>().sprite = tutoBucket;
    }

    public void StartPlaying()
    {
        HUDManager.Instance.DisplayTuto(false);
        HUDManager.Instance.DisplayTimer(true);
    }
}
