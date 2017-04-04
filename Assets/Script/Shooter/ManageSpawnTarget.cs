using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageSpawnTarget : MonoBehaviour {

    public GameObject target;

    private GameObject[] poolTarget;
    private Vector2 sizeX;
    private float hauteur;
    private bool canSpawn = false;

	// Use this for initialization
	void Start () {

        poolTarget = new GameObject[10];
        for(int i=0;i< poolTarget.Length; i++)
        {
            poolTarget[i] = Instantiate(target, transform);
            poolTarget[i].SetActive(false);
        }

        float widthDemi = GetComponent<Renderer>().bounds.size.x / 2;
        sizeX = new Vector2(transform.position.x - widthDemi, transform.position.x + widthDemi);
        hauteur = transform.position.y + GetComponent<Renderer>().bounds.size.y / 2 ;
        canSpawn = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (canSpawn)
        {
            Invoke("Spawn", 1);
            canSpawn = false;
        }
	}

    private void Spawn()
    {
        int tmpI = FindAvailable();
        if (tmpI == -1)
            return;

        //Check if not overlaping
        Vector3 pos = new Vector3(Random.Range(sizeX.x, sizeX.y), hauteur, 0);
        float raduis = target.transform.GetChild(0).GetComponent<CircleCollider2D>().radius;
        int nbEssaie = 10;
        int currentEssaie = 0;

        while (Physics2D.OverlapCircle(pos, raduis) != null )
        {
            currentEssaie++;
            if (currentEssaie >= nbEssaie)
                return;
            pos = new Vector3(Random.Range(sizeX.x, sizeX.y), hauteur, 0);
        }

        poolTarget[tmpI].transform.position = pos;
        poolTarget[tmpI].SetActive(true);

        canSpawn = true;
    }

    private int FindAvailable()
    {
        for(int i = 0; i < poolTarget.Length; i++)
        {
            if (!poolTarget[i].activeSelf)
                return i;
        }

        return -1;
    }
}
