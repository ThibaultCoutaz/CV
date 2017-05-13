using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageTurnFight : MonoBehaviour {

    #region character
    public float maxLife;
    [Tooltip("In seconds")]
    public float timeToAttack;
    public float dmg;
    private float currentlife, currentTimeToAttack;
    #endregion

    [System.Serializable]
    public struct ennemy
    {
        public float maxLifeEnnemy;
        [Tooltip("In seconds")]
        public Vector2 timeToAttackEnnemy;
        public float dmgEnnemy;
    }

    #region Ennemy
    public ennemy[] listEnnemies;
    private int currentEnnemyIndex;
    private float currentTimerAttackEnnemy;
    private float currentlifeEnnemy, currentTimeToAttackEnnemy;
    #endregion

    private bool needAction, waitAction;

    private bool pause = false;

	// Use this for initialization
	void Start () {
        pause = false;
        
        currentlife = maxLife;
        currentTimeToAttack = 0;

        currentEnnemyIndex = PlayerPrefs.GetInt("EnnemyIndexTurnFight");

        HUDManager.Instance.SetPicBoss(currentEnnemyIndex);
        currentlifeEnnemy = listEnnemies[currentEnnemyIndex].maxLifeEnnemy;
        currentTimeToAttackEnnemy = 0;

        needAction = false;
        waitAction = false;

        currentTimerAttackEnnemy = Random.Range(listEnnemies[currentEnnemyIndex].timeToAttackEnnemy.x, listEnnemies[currentEnnemyIndex].timeToAttackEnnemy.y);
        HUDManager.Instance.InitTurnFight(this,maxLife, timeToAttack, currentlifeEnnemy, currentTimerAttackEnnemy);
        HUDManager.Instance.DisplayTurnFight(true);

        HUDManager.Instance.InitMenuPause("Dialogue", "TurnFight");
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Pause
        if (InputManager.Instance.Pause)
        {
            pause = !pause;
            HUDManager.Instance.DisplayMenuPause(pause);
        }

        if (!pause)
        {
            if (!needAction)
            {
                if (currentTimeToAttack < timeToAttack)
                {
                    currentTimeToAttack += Time.deltaTime;
                }
                else
                {
                    needAction = true;
                    currentTimeToAttack = 0;
                }

                if (currentTimeToAttackEnnemy < currentTimerAttackEnnemy)
                {
                    currentTimeToAttackEnnemy += Time.deltaTime;
                }
                else
                {
                    currentTimeToAttackEnnemy = 0;
                    currentTimerAttackEnnemy = Random.Range(listEnnemies[currentEnnemyIndex].timeToAttackEnnemy.x, listEnnemies[currentEnnemyIndex].timeToAttackEnnemy.y);
                    HUDManager.Instance.SetBackMaxTimerAttackEnnemy(currentTimerAttackEnnemy);
                    AttackPlayer();
                }
                HUDManager.Instance.SetEnnemyTimerTurnFight(currentTimeToAttackEnnemy);
                HUDManager.Instance.SetYourTimerTurnFight(currentTimeToAttack);
            }
            else if (!waitAction)
            {
                waitAction = true;
                HUDManager.Instance.ActivateButton(true);
            }
        }
    }
    
    public void AttackEnnemy()
    {
        if(currentlifeEnnemy - dmg < 0)
        {
            currentlifeEnnemy = 0;
        }
        else
        {
            currentlifeEnnemy -= dmg;
        }
        HUDManager.Instance.SetEnnemyLifeTurnFight(currentlifeEnnemy);
    }

    private void AttackPlayer()
    {
        if(currentlife- listEnnemies[currentEnnemyIndex].dmgEnnemy < 0)
        {
            currentlife = 0;
        }
        else
        {
            currentlife-= listEnnemies[currentEnnemyIndex].dmgEnnemy;
        }
        HUDManager.Instance.GetHitOrHeal(true);
        HUDManager.Instance.SetYourLifeTurnFight(currentlife);
    }

    public void StartTimer()
    {
        waitAction = false;
        needAction = false;
    } 
}
