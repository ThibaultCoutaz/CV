using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BehaviourEnnemy : MonoBehaviour {

    public enum direction
    {
        left,
        right
    }

    public float speed = 2;
    public Transform positionEnd;
    public direction directionStart;
    public Sprite leftDirection;
    public Sprite rightDirection;

    private direction currentDirection;
    private Vector3 posStart;
    private Vector3 posEnd;

	// Use this for initialization
	void Start () {
        posStart = transform.position;
        posEnd = positionEnd.position;
        if (directionStart == direction.left)
            GetComponent<SpriteRenderer>().sprite = leftDirection;
        if (directionStart == direction.right)
            GetComponent<SpriteRenderer>().sprite = rightDirection;

        currentDirection = directionStart;
	}
	
	// Update is called once per frame
	void Update () {
		if(currentDirection == direction.right)
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            if (transform.position.x >= posStart.x)
            {
                currentDirection = direction.left;
                GetComponent<SpriteRenderer>().sprite = leftDirection;
            }
        }
        else if (currentDirection == direction.left)
        {
            transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            if (transform.position.x <= posEnd.x)
            {
                currentDirection = direction.right;
                GetComponent<SpriteRenderer>().sprite = rightDirection;
            }
        }
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
            coll.gameObject.GetComponent<CharacterBehaviour>().LostLife(1);
    }
}
