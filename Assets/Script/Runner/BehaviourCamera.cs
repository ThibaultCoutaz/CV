using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourCamera : MonoBehaviour {

    public float speed = 1;
    public bool gameStart = false;

    private bool pause = false;
	
	// Update is called once per frame
	void Update () {
        if (gameStart)
        {
            if (InputManager.Instance.PauseCamera)
                pause = !pause;

            if (!pause)
                transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }
	}
}
