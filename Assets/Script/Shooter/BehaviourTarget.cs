using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTarget : MonoBehaviour {

    public bool goodWord;
    public float timeStay;
    public TextMesh word;

    [HideInInspector]
    public ManageSpawnTarget manageScrore;

    void OnEnable()
    {
        Invoke("DisplayOff", timeStay);
    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.transform.parent.gameObject.SetActive(false);
            CancelInvoke();
            if (goodWord)
                manageScrore.ManageScrore(1);
            else
                manageScrore.ManageScrore(2);
            CancelInvoke();
        }
    }

    private void DisplayOff()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
        if(!goodWord)
            manageScrore.ManageScrore(3);
    }

    public void SetSprite(Sprite s)
    {
        GetComponent<SpriteRenderer>().sprite = s;
    }

    public void SetText(string t)
    {
        word.text = t;
    }
}
