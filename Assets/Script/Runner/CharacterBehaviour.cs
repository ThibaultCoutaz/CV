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

	// Use this for initialization
	void Start () {
        spawn = transform.position;
        currentNbTry = nbTry;
        cam.GetComponent<BehaviourCamera>().gameStart = false;
        GetComponent<Platformer2DUserControl>().enabled = false;
        GetComponent<PlatformerCharacter2D>().enabled = false;
        HUDManager.Instance.InitLifeRunner(nbTry);
        HUDManager.Instance.DisplayTuto(true,this, HUDTuto.tutoStyle.Runner);
	}
	
	// Update is called once per frame
	void Update () {
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
    }

    private void Restart()
    {
        transform.position = spawn;
        cam.transform.position = new Vector3(spawn.x, cam.transform.position.y, cam.transform.position.z);
        cam.GetComponent<BehaviourCamera>().gameStart = false;
        GetComponent<Platformer2DUserControl>().enabled = false;
        GetComponent<PlatformerCharacter2D>().enabled = false;
        HUDManager.Instance.DisplayTimer(true, this);
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
