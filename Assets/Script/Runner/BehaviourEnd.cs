using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourEnd : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
            coll.gameObject.GetComponent<CharacterBehaviour>().EndGame();
    }
}
