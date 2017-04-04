using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScieBheaviour : MonoBehaviour {

    public enum direction
    {
        top,
        bottom,
        right,
        left
    }

    public float speedScie = 2;
    public direction directionScie;
    public Transform limitDistance;
    

    private Vector3 posEnd;
    private Vector3 posStart;

	// Use this for initialization
	void Start () {
        posEnd = limitDistance.position;
        posStart = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(directionScie == direction.top)
        {
            transform.position += new Vector3(0, speedScie*Time.deltaTime, 0);
            if(transform.position.y >= posEnd.y)
            {
                directionScie = direction.bottom;
                //rotation change;
            }
        }
        else if(directionScie == direction.bottom)
        {
            transform.position -= new Vector3(0, speedScie * Time.deltaTime, 0);
            if(transform.position.y <= posStart.y)
            {
                directionScie = direction.top;
            }
        }
        else if(directionScie == direction.right)
        {
            transform.position += new Vector3(speedScie * Time.deltaTime, 0, 0);
            if(transform.position.x >= posEnd.x)
            {
                directionScie = direction.left;
            }
        }
        else if(directionScie == direction.left)
        {
            transform.position -= new Vector3(speedScie * Time.deltaTime, 0, 0);
            if (transform.position.x <= posStart.x)
            {
                directionScie = direction.right;
            }
        }
	}


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
            other.gameObject.GetComponent<CharacterBehaviour>().LostLife(1);
    }
}
