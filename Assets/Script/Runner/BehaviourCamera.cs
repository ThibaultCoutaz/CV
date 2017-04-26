using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourCamera : MonoBehaviour {

    public bool debug;

    public float speed = 1;
    public bool gameStart = false;

    public Transform boxLeft;
    public Transform boxRight;

    public Transform startSlow;
    public Transform endSlow;

    [HideInInspector]
    public bool pause = false;

	// Update is called once per frame
	void Update () {
        if (gameStart)
        {
            if(!pause)
                if (transform.position.x > startSlow.position.x && transform.position.x < endSlow.position.x)
                    transform.position += new Vector3(speed * Time.deltaTime*0.5f, 0, 0);
                else
                    transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }
	}
}
