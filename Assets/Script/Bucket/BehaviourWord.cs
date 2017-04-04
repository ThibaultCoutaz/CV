using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourWord : MonoBehaviour {

    [HideInInspector]
    public SpawnWords sky;

    [HideInInspector]
    public Vector2 index;

	// Use this for initialization
	void Start () {

	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            sky.DisableWord(index);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bucket")
        {
            sky.DisableWord(index);
        }
    }
}
