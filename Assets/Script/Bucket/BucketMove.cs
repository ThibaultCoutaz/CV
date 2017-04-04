using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketMove : MonoBehaviour {

    public float speed = 10;
    private Rigidbody2D rig2D;

	// Use this for initialization
	void Start () {
        rig2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        rig2D.velocity = new Vector2(speed * InputManager.Instance.GetHorizontalAxis(), 0);
    }


}
