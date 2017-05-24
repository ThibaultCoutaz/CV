using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageTurnFight : MonoBehaviour {

    #region character
    public float maxLife;
    #region TimeToAttack
    [Tooltip("In seconds")]
    public float timeToAttackBase;
    public float MinimumSpeed;
    private float timeToAttackCurrent;
    public float GetSpeed() { return timeToAttackCurrent; }
    public void SetSpeed(float tmp)
    {
        timeToAttackCurrent = tmp;
        PlayerPrefs.SetFloat("speedAttack", timeToAttackCurrent);
    }
    #endregion
    #region dmgPlayer
    public Vector2 dmgPlayerBase;
    public float MaxAttack;
    private Vector2 dmgPlayerCurrent;
    public Vector2 Getdmg() { return dmgPlayerCurrent; }
    public void Setdmg(Vector2 tmp)
    {
        dmgPlayerCurrent = tmp;
        PlayerPrefs.SetFloat("DmgMini", dmgPlayerCurrent.x);
        PlayerPrefs.SetFloat("DmgMax", dmgPlayerCurrent.y);
    }
    #endregion

    private float currentTimeToAttack;
    [HideInInspector]
    public float currentlife;
    public bool ShieldUP ;
    #endregion

    [System.Serializable]
    public struct ennemy
    {
        public float maxLifeEnnemy;
        [Tooltip("In seconds")]
        public Vector2 timeToAttackEnnemy;
        public Vector2 dmgEnnemy;
    }

    #region Ennemy
    public ennemy[] listEnnemies;
    private int currentEnnemyIndex;
    private float currentTimerAttackEnnemy;
    private float currentlifeEnnemy, currentTimeToAttackEnnemy;
    #endregion

    private bool needAction, waitAction;

    private bool pause = false;
    public int BeatPrevious;//0 = false , 1 = true;

	// Use this for initialization
	void Start () {
        pause = false;

        currentlife = maxLife;
        currentTimeToAttack = 0;

        currentEnnemyIndex = PlayerPrefs.GetInt("EnnemyIndexTurnFight");

        if(currentEnnemyIndex == 0)
        {
            PlayerPrefs.SetFloat("BaseDmgMini", dmgPlayerBase.x);
            PlayerPrefs.SetFloat("BaseDmgMax", dmgPlayerBase.y);
            PlayerPrefs.SetFloat("BasespeedAttack", timeToAttackBase);
            dmgPlayerCurrent = dmgPlayerBase;
            timeToAttackCurrent = timeToAttackBase;
        }
        else if (PlayerPrefs.GetInt("BeatPrevious") == 0)
        {
            PlayerPrefs.SetFloat("DmgMini", PlayerPrefs.GetFloat("BaseDmgMini"));
            PlayerPrefs.SetFloat("DmgMax", PlayerPrefs.GetFloat("BaseDmgMax"));
            dmgPlayerCurrent = new Vector2(PlayerPrefs.GetFloat("BaseDmgMini"), PlayerPrefs.GetFloat("BaseDmgMax"));

            PlayerPrefs.SetFloat("speedAttack", PlayerPrefs.GetFloat("BasespeedAttack"));
            timeToAttackCurrent = PlayerPrefs.GetFloat("BasespeedAttack");
        }
        else
        {
            dmgPlayerCurrent.x = PlayerPrefs.GetFloat("DmgMini");
            dmgPlayerCurrent.y = PlayerPrefs.GetFloat("DmgMax");
            timeToAttackCurrent = PlayerPrefs.GetFloat("speedAttack");
        }
        
        HUDManager.Instance.SetPicBoss(currentEnnemyIndex);
        currentlifeEnnemy = listEnnemies[currentEnnemyIndex].maxLifeEnnemy;
        currentTimeToAttackEnnemy = 0;

        needAction = false;
        waitAction = false;

        currentTimerAttackEnnemy = Random.Range(listEnnemies[currentEnnemyIndex].timeToAttackEnnemy.x, listEnnemies[currentEnnemyIndex].timeToAttackEnnemy.y);
        HUDManager.Instance.InitTurnFight(this,maxLife, timeToAttackCurrent, currentlifeEnnemy, currentTimerAttackEnnemy);
        HUDManager.Instance.DisplayTurnFight(true);

        HUDManager.Instance.InitMenuPause("Dialogue", "TurnFight");
        ShieldUP = false;

        TimerManager.Instance.canStartGame = false;
        HUDManager.Instance.DisplayTuto(true, true,HUDTuto.tutoStyle.TurnFight);
        PlayerPrefs.SetInt("BeatPrevious", 0);
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Pause
        if (InputManager.Instance.Pause && TimerManager.Instance.canStartGame)
        {
            pause = !pause;
            HUDManager.Instance.DisplayMenuPause(pause);
        }

        if (!pause && TimerManager.Instance.canStartGame)
        {
            if (!needAction)
            {
                if (currentTimeToAttack < timeToAttackCurrent)
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
        int dmg = (int)Random.Range(dmgPlayerCurrent.x, dmgPlayerCurrent.y);
        if(currentlifeEnnemy - dmg < 0)
        {
            currentlifeEnnemy = 0;
            pause = true;
            HUDManager.Instance.SetEnnemyLifeTurnFight(currentlifeEnnemy);
            HUDManager.Instance.SetDmgIndicatorEnemy(dmg);
            HUDManager.Instance.DisplayEndTurnFight(true, false);
            PlayerPrefs.SetFloat("BaseDmgMini", dmgPlayerCurrent.x);
            PlayerPrefs.SetFloat("BaseDmgMax", dmgPlayerCurrent.y);
            PlayerPrefs.SetFloat("BasespeedAttack", timeToAttackCurrent);
            PlayerPrefs.SetFloat("DmgMini", dmgPlayerCurrent.x);
            PlayerPrefs.SetFloat("DmgMax", dmgPlayerCurrent.y);
            PlayerPrefs.SetFloat("speedAttack", timeToAttackCurrent);
            PlayerPrefs.SetInt("BeatPrevious", 1);
            return;
        }
        else
        {
            currentlifeEnnemy -= dmg;
            HUDManager.Instance.SetEnnemyLifeTurnFight(currentlifeEnnemy);
            HUDManager.Instance.SetDmgIndicatorEnemy(dmg);
        }
    }

    private void AttackPlayer()
    {
        if (!ShieldUP)
        {
            int dmg = (int)Random.Range(listEnnemies[currentEnnemyIndex].dmgEnnemy.x, listEnnemies[currentEnnemyIndex].dmgEnnemy.y);
            if (currentlife - dmg < 0)
            {
                currentlife = 0;
                pause = true;
                HUDManager.Instance.DisplayEndTurnFight(true, true);
                HUDManager.Instance.GetHitOrHeal(true);
                HUDManager.Instance.SetYourLifeTurnFight(currentlife);
                HUDManager.Instance.SetDmgIndicatorYou(dmg);
                return;
            }
            else
            {
                currentlife -= dmg;
                HUDManager.Instance.GetHitOrHeal(true);
                HUDManager.Instance.SetYourLifeTurnFight(currentlife);
                HUDManager.Instance.SetDmgIndicatorYou(dmg);
            }
        }
        else
        {
            ShieldUP = false;
            HUDManager.Instance.DisplayShield(false);
            HUDManager.Instance.SetDmgIndicatorYou(0);
        }
    }

    public void HealPlayer(int amount)
    {
        if(currentlife >= maxLife)
        {
            return;
        }
        else
        {
            if(currentlife+amount >= maxLife)
            {
                currentlife = maxLife;
            }
            else
            {
                currentlife += amount;
            }
        }

        HUDManager.Instance.SetYourLifeTurnFight(currentlife);
    }

    public void StartTimer()
    {
        waitAction = false;
        needAction = false;
    } 
}
