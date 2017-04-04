using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTowerEnnemy : MonoBehaviour {

    public float Maxhealth = 10f;
    [SerializeField] private float currentHealth;
    public float moneyEarn = 100;

    public TextMesh lifeIndicator;

    private Vector3[] CheckPoint;
    private int currentCheckPoint = 0;
    private float speed;
    private Vector3 direction;
    private GameManagerTower scriptManager;

    public void InitEnnemy(Transform[] way,float _speed, GameManagerTower script)
    {
        currentHealth = Maxhealth;
        lifeIndicator.text = currentHealth.ToString();
        scriptManager = script;
        CheckPoint = new Vector3[way.Length];

        for (int i = 0; i < way.Length; i++)
            CheckPoint[i] = way[i].position;

        speed = _speed;
        direction = Vector3.Normalize(new Vector3(CheckPoint[currentCheckPoint+1].x - CheckPoint[currentCheckPoint].x, CheckPoint[currentCheckPoint + 1].y - CheckPoint[currentCheckPoint].y, CheckPoint[currentCheckPoint + 1].z - CheckPoint[currentCheckPoint].z));
    }
	
	void Update () {
        if (Vector3.Distance(transform.position, CheckPoint[currentCheckPoint + 1]) > speed)
        {
            transform.position += direction * speed;
        }
        else
        {
            currentCheckPoint++;
            direction = Vector3.Normalize(new Vector3(CheckPoint[currentCheckPoint + 1].x - CheckPoint[currentCheckPoint].x, CheckPoint[currentCheckPoint + 1].y - CheckPoint[currentCheckPoint].y, CheckPoint[currentCheckPoint + 1].z - CheckPoint[currentCheckPoint].z));
        }
    }
    
    private void DeadEnnemy()
    {
        gameObject.SetActive(false);
        currentCheckPoint = 0;
        transform.position = CheckPoint[currentCheckPoint];
        direction = Vector3.Normalize(new Vector3(CheckPoint[currentCheckPoint + 1].x - CheckPoint[currentCheckPoint].x, CheckPoint[currentCheckPoint + 1].y - CheckPoint[currentCheckPoint].y, CheckPoint[currentCheckPoint + 1].z - CheckPoint[currentCheckPoint].z));
        currentHealth = Maxhealth;
        lifeIndicator.text = currentHealth.ToString();
    }

    public bool hitEnnemy(float hitDmg)
    {
        if (hitDmg < currentHealth)
        {
            currentHealth -= hitDmg;
            lifeIndicator.text = currentHealth.ToString();
            return false;
        }
        else
        {
            DeadEnnemy();
            scriptManager.EditMoney(moneyEarn);
            scriptManager.CheckEndWave();
            return true;
        }
    }
}
