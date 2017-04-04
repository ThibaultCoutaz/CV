using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWords : MonoBehaviour {

    public GameObject[] words;

    [SerializeField]
    private Transform parentWords;
    private Vector2 sizeX;
    private GameObject[,] wordsPool;
    private int nbExemplaire = 2;

	// Use this for initializationS
	void Start () {
        InvokeRepeating("Spawn", 2, 3);

        wordsPool = new GameObject[words.Length,nbExemplaire];

        for (int i = 0; i < words.Length; i++)
        {
            for (int j = 0; j < nbExemplaire; j++) {
                wordsPool[i,j] = (GameObject)Instantiate(words[i]);
                wordsPool[i,j].SetActive(false);
                wordsPool[i, j].GetComponent<BehaviourWord>().sky = this;
                wordsPool[i, j].GetComponent<BehaviourWord>().index = new Vector2(i, j);
            }
        }   

        /*Define Size Spawn*/
        float widthDemi = GetComponent<Renderer>().bounds.size.x/2;
        sizeX = new Vector2(transform.position.x - widthDemi,transform.position.x+widthDemi);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Spawn()
    {
        int random = Random.Range(0, words.Length - 1);
        int index = ChooseActive(random);
        if (index != -1)
        {
            wordsPool[random, ChooseActive(random)].transform.position = new Vector3(Random.Range(sizeX.x, sizeX.y), transform.position.y, 0);
            wordsPool[random, ChooseActive(random)].SetActive(true);
        }
        else
        {
            Debug.LogError("Aucun objt dispo dans le POOL");
        }
    }

    private int ChooseActive(int index)
    {
        for(int i = 0; i < nbExemplaire; i++)
        {
            if (!wordsPool[index,i].activeSelf)
                return i;
        }
        return -1;
    }

    public void DisableWord(Vector2 index)
    {
        wordsPool[(int)index.x, (int)index.y].SetActive(false);
    }
}
