using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class CharacterBehaviour : MonoBehaviour {

    public Camera cam;
    public float speedClimb = 2;
    public int nbTry = 3;
    private int currentNbTry;

    private bool canClimb = false;
    private bool justClimb = false;

    private Vector3 spawn;
    [HideInInspector]
    public bool pause = false;
    private bool displayPause = false;

	// Use this for initialization
	void Start () {

        HUDManager.Instance.InitMenuPause("Dialogue","Runner");

        spawn = transform.position;
        currentNbTry = nbTry;
        cam.GetComponent<BehaviourCamera>().gameStart = false;
        GetComponent<Platformer2DUserControl>().enabled = false;
        GetComponent<PlatformerCharacter2D>().enabled = false;
        HUDManager.Instance.DisplayLifeRunner(true,nbTry);
        HUDManager.Instance.DisplayTuto(true, HUDTuto.tutoStyle.Runner);
	}
	
	// Update is called once per frame
	void Update () {

        //Pause
        if (InputManager.Instance.Pause)
        {
            pause = !pause;
            HUDManager.Instance.DisplayMenuPause(pause);
            cam.GetComponent<BehaviourCamera>().pause = pause;
            GetComponent<Platformer2DUserControl>().justPause = true;
            HUDManager.Instance.PauseTimer(pause);
        }

        if (!pause)
        {
            if (TimerManager.Instance.canStartGame)
                StartGame();
            if (canClimb)
            {
                if (InputManager.Instance.IsPressingHaut)
                {
                    if (!justClimb)
                        justClimb = true;
                    GetComponent<Rigidbody2D>().gravityScale = 0;
                    transform.position += new Vector3(0, speedClimb, 0);
                }
                else
                {
                    GetComponent<Rigidbody2D>().gravityScale = 1.5f;
                }
            }
            else if (justClimb)
            {
                GetComponent<Rigidbody2D>().gravityScale = 3;
            }
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ladder")
            canClimb = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ladder")
            canClimb = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Water")
            LostLife(1);
    }


    public void LostLife(int nb)
    {
        if (nb <= currentNbTry)
        {
            HUDManager.Instance.RemoveLife(nb);
            currentNbTry -= nb;
        }
        else
        {
            HUDManager.Instance.RemoveLife(currentNbTry);
            currentNbTry -= currentNbTry;
        }

        if (currentNbTry <= 0)
            EndGame();
        else
            Restart();
    }

    public void StartGame()
    {
        cam.GetComponent<BehaviourCamera>().gameStart = true;
        GetComponent<Platformer2DUserControl>().enabled = true;
        GetComponent<PlatformerCharacter2D>().enabled = true;
        TimerManager.Instance.canStartGame = false;
    }

    private void Restart()
    {
        cam.transform.position = new Vector3(spawn.x, cam.transform.position.y, cam.transform.position.z);
        transform.position = spawn;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        cam.GetComponent<BehaviourCamera>().gameStart = false;
        GetComponent<Platformer2DUserControl>().enabled = false;
        GetComponent<PlatformerCharacter2D>().enabled = false;
        HUDManager.Instance.DisplayTimer(true);
    }

    public void SetSpawn(Vector3 newSpawn)
    {
        spawn = newSpawn;
    }

    public void EndGame()
    {
        HUDManager.Instance.DisplayEndRunner(true);
        cam.GetComponent<BehaviourCamera>().gameStart = false;
        GetComponent<Platformer2DUserControl>().enabled = false;
        GetComponent<PlatformerCharacter2D>().enabled = false;
    }
}
