using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourSpike : MonoBehaviour
{

    public bool IsMoving = false;

    public enum position
    {
        top,
        bottom,
        left,
        right
    }

    public float speedSipke = 2;
    public float delaySpike = 1.8f;
    public position positionWall;


    private float height = 0;
    private Vector3 positionClose;
    private Vector3 positionOpen;

    enum state
    {
        open,
        close,
        stop
    }


    private state currentState;

    // Use this for initialization
    void Start()
    {
        if (IsMoving)
        {
            height = GetComponent<SpriteRenderer>().sprite.bounds.size.y;
            positionOpen = transform.position;

            if (positionWall == position.top)
                positionClose = transform.position + new Vector3(0, +height / 2, 0);
            else if (positionWall == position.bottom)
                positionClose = transform.position + new Vector3(0, -height / 2, 0);
            else
                Debug.LogError("left and right not implement");

            transform.position = positionClose;
            currentState = state.open;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsMoving)
        {
            if (currentState == state.open)
            {
                if (positionWall == position.top)
                {
                    transform.position -= new Vector3(0, speedSipke * Time.deltaTime, 0);
                    if (transform.position.y <= positionOpen.y)
                        currentState = state.close;
                }
                else if (positionWall == position.bottom)
                {
                    transform.position += new Vector3(0, speedSipke * Time.deltaTime, 0);
                    if (transform.position.y >= positionOpen.y)
                        currentState = state.close;
                }
                else
                {
                    Debug.LogError("Right and Left nor implemented");
                }
            }
            else if (currentState == state.close)
            {
                if (positionWall == position.top)
                {
                    transform.position += new Vector3(0, speedSipke * Time.deltaTime, 0);
                    if (transform.position.y >= positionClose.y)
                    {
                        currentState = state.stop;
                        Invoke("StartSpike", delaySpike);
                    }
                }
                else if (positionWall == position.bottom)
                {
                    transform.position -= new Vector3(0, speedSipke * Time.deltaTime, 0);
                    if (transform.position.y <= positionClose.y)
                    {
                        currentState = state.stop;
                        Invoke("StartSpike", delaySpike);
                    }
                }
                else
                {
                    Debug.LogError("Right and Left nor implemented");
                }
            }
        }
    }

    private void StartSpike()
    {
        currentState = state.open;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
            coll.gameObject.GetComponent<CharacterBehaviour>().LostLife(1);
    }
}
