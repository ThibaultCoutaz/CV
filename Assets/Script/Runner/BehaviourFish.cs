using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourFish : MonoBehaviour {

    public Transform maxHauteur;
    public float speedFish = 2;
    public float timeBetweenFish = 2;

    public Sprite spriteUpFish;
    public Sprite spriteDownFish;

    private SpriteRenderer currentSprite;
    private Vector3 positionStart;
    private Vector3 positionEnd;

    enum state
    {
        up,
        down,
        stop
    }

    private state stateFish;

	// Use this for initialization
	void Start () {
        currentSprite = GetComponent<SpriteRenderer>();
        positionStart = transform.position;
        positionEnd = maxHauteur.position;
        stateFish = state.up;

	}
	
	// Update is called once per frame
	void Update () {
		if(stateFish == state.up)
        {
            if (transform.position.y < positionEnd.y)
            {
                transform.position += new Vector3(0, speedFish * Time.deltaTime, 0);
            }
            else
            {
                stateFish = state.down;
                currentSprite.sprite = spriteDownFish;
            }
        }
        else if(stateFish == state.down)
        {
            if (transform.position.y > positionStart.y)
            {
                transform.position -= new Vector3(0,speedFish * Time.deltaTime, 0);
            }
            else
            {
                stateFish = state.stop;
                currentSprite.sprite = spriteUpFish;
                Invoke("StartFish", timeBetweenFish);
            }
        }
	}

    private void StartFish()
    {
        stateFish = state.up;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
            other.gameObject.GetComponent<CharacterBehaviour>().LostLife(1);
    }
}
