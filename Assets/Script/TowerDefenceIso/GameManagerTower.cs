using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerTower : MonoBehaviour {

    public float currentMoney = 300;
    public int totalLife;

    public Transform parentlife;
    public GameObject life;
    private GameObject[] currentLife;
    private int currentlifeInt;

    public HUDTowerShop.elementStruct[] listTower;

    public Transform[] WayEnnemy;
    public float speedEnnemies;

    public GameObject ennemy;
    public int[] waveMonster;
    private GameObject[] listEnnemies;

    private bool waveStart = false;
    [HideInInspector]
    public int currentWave = 0;
    private int indexEnnemySpawn = 0;

    [HideInInspector]
    public bool pause = false;

    [HideInInspector]
    public bool end;

	// Use this for initialization
	void Start () {
        HUDManager.Instance.InitTowerShop(listTower);
        currentWave = 0;
        int maxEnnemies = CheckMaxEnnemies();

        listEnnemies = new GameObject[maxEnnemies];
        for (int i = 0; i < maxEnnemies; i++)
        {
            listEnnemies[i] = Instantiate(ennemy, WayEnnemy[0].position, Quaternion.identity, this.transform);
            listEnnemies[i].GetComponent<BehaviourTowerEnnemy>().InitEnnemy(WayEnnemy, speedEnnemies,this);
            listEnnemies[i].SetActive(false);
        }

        HUDManager.Instance.InitStartWave(this);
        HUDManager.Instance.DisplayStartWave(true);
        HUDManager.Instance.DisplayMoneyTowerDefence(true);
        HUDManager.Instance.EditMoneyTowerDefence(currentMoney);

        currentLife = new GameObject[totalLife];
        currentLife[0] = life;
        currentlifeInt = totalLife;

        for (int i = 1; i < totalLife; i++)
        {
            currentLife[i] = Instantiate(life, parentlife);
        }

        HUDManager.Instance.InitMenuPause("Dialogue", "TowerDefence");

        HUDManager.Instance.DisplayTuto(true, true,HUDTuto.tutoStyle.TowerDefence);

    }

    // Update is called once per frame
    void Update()
    {
        if (!end)
        {
            //Pause
            if (InputManager.Instance.Pause)
            {
                pause = !pause;
                HUDManager.Instance.DisplayMenuPause(pause);
                if (IsInvoking())
                    waveStart = true;
                CancelInvoke();
            }

            if (waveStart && !pause)
            {
                if (indexEnnemySpawn < waveMonster[currentWave - 1])
                {
                    Invoke("spawnEnemy", Random.Range(0.5f, 1.5f));
                    waveStart = false;
                }
                else
                {
                    indexEnnemySpawn = 0;
                    waveStart = false;
                }
            }
        }
    }

    private void spawnEnemy()
    {
        listEnnemies[indexEnnemySpawn].SetActive(true);
        indexEnnemySpawn++;
        waveStart = true;
    }

    public void StartWave()
    {
        currentWave++;
        waveStart = true;
    }

    private int CheckMaxEnnemies()
    {
        int tmp = 0;
        for(int i=0; i < waveMonster.Length; i++)
        {
            if (waveMonster[i] > tmp)
                tmp = waveMonster[i];
        }
        return tmp;
    }

    public void EditMoney(float amount)
    {
        currentMoney += amount;
        HUDManager.Instance.EditMoneyTowerDefence(currentMoney);
    }

    public void CheckEndWave()
    {
        int nbActif = 0;
        for(int i=0; i < waveMonster[currentWave - 1]; i++)
        {
            if (listEnnemies[i].activeSelf)
                nbActif++;
        }

        if(nbActif == 0)
        {
            if (currentWave >= waveMonster.Length - 1)
            {
                end = true;
                HUDManager.Instance.DisplayEndScreenTowerDefence(true, "Well Done! You beat Gamagora.");
            }
            else
                HUDManager.Instance.EnableButtonWaveStart();
        }
    }

    public void RemoveLife()
    {
        if (currentlifeInt > 0)
        {
            currentLife[currentlifeInt - 1].SetActive(false);
            currentlifeInt--;
        }

        if (currentlifeInt <= 0)
        {
            HUDManager.Instance.DisplayEndScreenTowerDefence(true, "Sorry! Gamagora beat you.");
            end = true;
        }
        
    }
}
