using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageSpawnTarget : MonoBehaviour {

    [Tooltip("In minutes")]
    public int timerGame;
    public float startingTimeSpawn;
    private float currentTimeLeft;

    public int minScoreToWin;

    public GameObject target;

    public GameObject[] spawnPlace;

    public Sprite goodWord;
    public Sprite badWord;

    public string[] listGoodWords;
    public string[] listBadWords;

    public int scoreGoodword;
    public int scoreBadWord;
    public int scoreMissBadWord;
    public int palierManche2;

    private int score;
    private float[] hauteur;
    private Vector2[] largeur;
    private GameObject[] poolTarget;
    private bool canSpawn = false;

    private bool manche2 = false;

    private bool end = false;
    private bool endDisplay = false;
    private bool pause = false;

	// Use this for initialization
	void Start () {

        HUDManager.Instance.InitMenuPause("Dialogue", "Shooter");

        end = false;
        endDisplay = false;
        currentTimeLeft = timerGame * 60;
        score = 0;

        poolTarget = new GameObject[10];
        int i;
        for(i=0;i< poolTarget.Length; i++)
        {
            poolTarget[i] = Instantiate(target, transform);
            poolTarget[i].GetComponentInChildren<BehaviourTarget>().manageScrore = this;
            poolTarget[i].SetActive(false);
        }
        
        hauteur = new float[spawnPlace.Length];
        largeur = new Vector2[spawnPlace.Length];

        for (i = 0; i < spawnPlace.Length; i++)
        {

            float widthDemi = spawnPlace[i].GetComponent<Renderer>().bounds.size.x / 2;
            largeur[i] = new Vector2(spawnPlace[i].transform.position.x - widthDemi, spawnPlace[i].transform.position.x + widthDemi);

            hauteur[i] = spawnPlace[i].transform.position.y + spawnPlace[i].GetComponent<Renderer>().bounds.size.y / 2;
        }
        canSpawn = true;

        HUDManager.Instance.InitShooter(timerGame);
        HUDManager.Instance.DisplayShooter(true);
        HUDManager.Instance.SetScoreShooter(score);
        HUDManager.Instance.SetManche(false);

        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (!end)
        {
            //Pause
            if (InputManager.Instance.Pause)
            {
                pause = !pause;
                HUDManager.Instance.DisplayMenuPause(pause);
                HUDManager.Instance.DisplayViewFinder(!pause);
                Cursor.visible = pause;
            }

            if (!pause)
            {
                currentTimeLeft -= Time.deltaTime;
                HUDManager.Instance.setTimerShooter(currentTimeLeft);
                if (currentTimeLeft <= 0)
                {
                    end = true;
                    CancelInvoke();
                    HUDManager.Instance.DisplayShooter(false);
                    Cursor.visible = true;
                }
                if (canSpawn)
                {
                    Invoke("Spawn", startingTimeSpawn);
                    canSpawn = false;
                }
            }
        }else if(!endDisplay)
        {
            if(score >= minScoreToWin)
            {
                HUDManager.Instance.DisplayEndScreenShooter(true, "YOU WIN");
            }
            else
            {
                HUDManager.Instance.DisplayEndScreenShooter(true, "YOU LOOSE");
            }
            endDisplay = true;
        }
	}

    private void Spawn()
    {
        int tmpI = FindAvailable();
        if (tmpI == -1)
            return;

        int randEtage = Random.Range(0, spawnPlace.Length);
        //Check if not overlaping
        Vector3 pos = new Vector3(Random.Range(largeur[randEtage].x, largeur[randEtage].y), hauteur[randEtage], 0);
        float raduis = target.transform.GetChild(0).GetComponent<CircleCollider2D>().radius;
        int nbEssaie = 10;
        int currentEssaie = 0;

        while (Physics2D.OverlapCircle(pos, raduis) != null )
        {
            currentEssaie++;
            if (currentEssaie >= nbEssaie)
                return;
            pos = new Vector3(Random.Range(largeur[randEtage].x, largeur[randEtage].y), hauteur[randEtage], 0);
        }

        poolTarget[tmpI].transform.position = pos;
        int rand = Random.Range(0, 2);
        BehaviourTarget tmpScript = poolTarget[tmpI].GetComponentInChildren<BehaviourTarget>();

        if (rand == 0)
        {
            tmpScript.goodWord = false;
            if(!manche2)
                tmpScript.SetSprite(badWord);
            else
            {
                rand = Random.Range(0, 2);
                if (rand == 0)
                    tmpScript.SetSprite(badWord);
                else
                    tmpScript.SetSprite(goodWord);
            }
            
            tmpScript.SetText(listBadWords[Random.Range(0, listBadWords.Length)]);
        }
        else
        {
            tmpScript.goodWord = true;
            if(!manche2)
                tmpScript.SetSprite(goodWord);
            else
            {
                rand = Random.Range(0, 2);
                if (rand == 0)
                    tmpScript.SetSprite(badWord);
                else
                    tmpScript.SetSprite(goodWord);
            }
            tmpScript.SetText(listGoodWords[Random.Range(0, listGoodWords.Length)]);
        }

        poolTarget[tmpI].SetActive(true);
        tmpScript.enabled = true;

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

    /// <summary>
    /// To manage the score
    /// </summary>
    /// <param name="value"> 
    /// can be 3 value :
    /// - 1 => Good word
    /// - 2 => Bad word
    /// - 3 => Miss good word
    /// </param>
    public void ManageScrore(int value)
    {
        if (value > 3 || value < 1)
        {
            Debug.LogError("Not the good value check the code of the function");
            return;
        }

        if (value == 1)
            if (score - scoreGoodword < 0)
                score = 0;
            else
                score -= scoreGoodword;
        else if(value == 2)
        {
            score += scoreBadWord;
        }else if(value == 3)
        {
            if (score - scoreMissBadWord < 0)
                score = 0;
            else
                score -= scoreMissBadWord;
        }

        if (score >= palierManche2 && !manche2)
        {
            manche2 = true;
            HUDManager.Instance.SetManche(manche2);
        }


        if (score < palierManche2 && manche2)
        {
            manche2 = false;
            HUDManager.Instance.SetManche(manche2);
        }

        HUDManager.Instance.SetScoreShooter(score);
    }
}
