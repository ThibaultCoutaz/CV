using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourCheckPoint : MonoBehaviour {

    public Sprite CheckPointDone;
    private bool saveCheckPoint = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(!saveCheckPoint)
            if(other.gameObject.tag == "Player")
            {
                GetComponent<SpriteRenderer>().sprite = CheckPointDone;
                saveCheckPoint = true;
                other.gameObject.GetComponent<CharacterBehaviour>().SetSpawn(transform.position);
            }
    }
}
