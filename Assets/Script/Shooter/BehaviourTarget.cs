﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTarget : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
            gameObject.SetActive(false);
    }
}
