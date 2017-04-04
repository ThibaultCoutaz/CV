using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourWeightSpawn : MonoBehaviour {

    public float timeSpawn = 2f;
    public GameObject weigh;

    public Sprite Attached;
    public Sprite attachedWeight;

    private SpriteRenderer currentSprite;
    private GameObject weightspawn;
	// Use this for initialization
	void Start () {
        currentSprite = GetComponent<SpriteRenderer>();
        currentSprite.sprite = attachedWeight;

        weightspawn = Instantiate(weigh, transform.position,Quaternion.identity,transform);
        weightspawn.SetActive(false);
        InvokeRepeating("SpawnWeight", 0, timeSpawn);
	}

    private void SpawnWeight()
    {
        currentSprite.sprite = Attached;
        weightspawn.SetActive(true);
    }

    public void SetWeight()
    {
        currentSprite.sprite = attachedWeight;
    }
}
