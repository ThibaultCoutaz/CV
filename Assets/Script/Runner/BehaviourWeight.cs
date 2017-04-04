using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourWeight : MonoBehaviour {

    private Vector3 posStart;

	// Use this for initialization
	void Start () {
        posStart = transform.position;
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Water")
        {
            transform.parent.GetComponent<BehaviourWeightSpawn>().SetWeight();
            gameObject.SetActive(false);
            transform.position = posStart;
        }
        else if (coll.gameObject.tag == "Player")
        {
            transform.position = posStart;
            gameObject.SetActive(false);
            coll.gameObject.GetComponent<CharacterBehaviour>().LostLife(1);
        }
    }
}
