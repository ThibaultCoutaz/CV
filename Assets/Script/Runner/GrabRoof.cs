using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabRoof : MonoBehaviour {

    private bool characterTry = false;

    private Rigidbody2D rigdCharacter;

	// Use this for initialization
	void Start () {
		
	}

    void Update()
    {
        if (characterTry)
        {
            if (InputManager.Instance.IsPressingHaut)
            {
                rigdCharacter.gravityScale = 0;
            }
            else
            {
                rigdCharacter.gravityScale = 1;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            characterTry = true;
            rigdCharacter = other.gameObject.GetComponent<Rigidbody2D>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            rigdCharacter.gravityScale = 3;
            characterTry = false;
        }
    }

}
