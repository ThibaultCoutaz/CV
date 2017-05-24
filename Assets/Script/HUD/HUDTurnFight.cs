using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HUDTurnFight : HUDElement {

    private ManageTurnFight manager;

    public Button attack;
    public Button defence;
    public Button lvlUP;
    public Button pass;
    public GameObject LvlUpScreen;

    #region LvlUP
    public Text CAttack, CAttackSpeed, Clife;
    public Button heal,attackB,speedAttackB;
    #endregion

    public GameObject endScreen;
    public GameObject endButton, restartButton;

    public GameObject shield;

    public Text dmgIndicatorYou;
    public Text dmgIndicatorEnemy;

    #region YourLifeVariable
    public RectTransform barTransformLife;
    public Text textLife;
    private float cachedYLife, minXValueLife, maxXValueLife, maxLife;
    #endregion

    #region YourTimerVariable
    public RectTransform barTransformTimer;
    private float cachedYTimer, minXValueTimer, maxXValueTimer, maxTimer;
    #endregion

    #region EnmnemyLifeVariable
    public RectTransform barTransformLifeEnnemy;
    public Text textLifeEnnemy;
    private float cachedYLifeEnnemy, minXValueLifeEnnemy, maxXValueLifeEnnemy, maxLifeEnnemy;
    #endregion

    #region EnnemyTimerVariable
    public RectTransform barTransformTimerEnnemy;
    private float cachedYTimerEnnemy, minXValueTimerEnnemy, maxXValueTimerEnnemy, maxTimerEnnemy;
    #endregion

    #region BossPic
    public Image picBoss;
    public Sprite[] bossSprites;
    #endregion

    public GameObject dmgScreen;

    // Use this for initialization
    public void Init(ManageTurnFight _manager, float _maxlife, float _maxTimer, float _maxlifeEnnemy, float _maxTimerEnnemy) {

        manager = _manager;

        maxLife = _maxlife;
        cachedYLife = barTransformLife.localPosition.y;
        maxXValueLife = barTransformLife.localPosition.x;
        minXValueLife = barTransformLife.localPosition.x - barTransformLife.rect.width;
        textLife.text = _maxlife.ToString() + " / " + _maxlife.ToString();


        maxTimer = _maxTimer;
        cachedYTimer = barTransformTimer.localPosition.y;
        maxXValueTimer = barTransformTimer.localPosition.x;
        minXValueTimer = barTransformTimer.localPosition.x - barTransformTimer.rect.width;
        SetYourTimer(0);

        maxLifeEnnemy = _maxlifeEnnemy;
        cachedYLifeEnnemy = barTransformLifeEnnemy.localPosition.y;
        maxXValueLifeEnnemy = barTransformLifeEnnemy.localPosition.x;
        minXValueLifeEnnemy = barTransformLifeEnnemy.localPosition.x - barTransformLifeEnnemy.rect.width;
        textLifeEnnemy.text = _maxlifeEnnemy.ToString() + " / " + _maxlifeEnnemy.ToString();


        maxTimerEnnemy = _maxTimerEnnemy;
        cachedYTimerEnnemy = barTransformTimerEnnemy.localPosition.y;
        maxXValueTimerEnnemy = barTransformTimerEnnemy.localPosition.x;
        minXValueTimerEnnemy = barTransformTimerEnnemy.localPosition.x - barTransformTimerEnnemy.rect.width;
        SetEnnemyTimer(0);
    }

    public void setBackEnnemyTimerAttack(float EnnemyAttackTimer)
    {
        maxTimerEnnemy = EnnemyAttackTimer;
    }


    //**********To calculate the value of the Bar*******//
    private float MapValues(float x, float inMin, float inMax, float outMin, float outMax)
    {
        return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;

    }

    //***To update the Bar********//
    public void SetYourLife(float life)
    {
        textLife.text = life.ToString() + " / " + maxLife.ToString();

        float currentXValue = MapValues(life, 0f, maxLife, minXValueLife, maxXValueLife);
        currentXValue = Mathf.Clamp(currentXValue, minXValueLife, maxXValueLife);
        barTransformLife.localPosition = new Vector3(currentXValue, cachedYLife);
    }

    public void HitOrHeal(bool hit)
    {
        if (hit)
        {
            dmgScreen.SetActive(true);
            Invoke("DisableDmg", 0.5f);
        }

    }

    private void DisableDmg()
    {
        dmgScreen.SetActive(false);
    }

    public void SetYourTimer(float timer)
    {
        float currentXValue = MapValues(timer, 0f, maxTimer, minXValueTimer, maxXValueTimer);
        currentXValue = Mathf.Clamp(currentXValue, minXValueTimer, maxXValueTimer);
        barTransformTimer.localPosition = new Vector3(currentXValue, cachedYTimer);
    }

    public void SetEnnemyLife(float life)
    {
        textLifeEnnemy.text = life.ToString() + " / " + maxLifeEnnemy.ToString();

        float currentXValue = MapValues(life, 0f, maxLifeEnnemy, minXValueLifeEnnemy, maxXValueLifeEnnemy);
        currentXValue = Mathf.Clamp(currentXValue, minXValueLifeEnnemy, maxXValueLifeEnnemy);
        barTransformLifeEnnemy.localPosition = new Vector3(currentXValue, cachedYLifeEnnemy);
    }

    public void SetEnnemyTimer(float timer)
    {
        float currentXValue = MapValues(timer, 0f, maxTimerEnnemy, minXValueTimerEnnemy, maxXValueTimerEnnemy);
        currentXValue = Mathf.Clamp(currentXValue, minXValueTimerEnnemy, maxXValueTimerEnnemy);
        barTransformTimerEnnemy.localPosition = new Vector3(currentXValue, cachedYTimerEnnemy);
    }

    public void ActivateAction(bool activate)
    {
        lvlUP.interactable = activate;
        attack.interactable = activate;
        defence.interactable = activate;
        pass.interactable = activate;
    }

    public void Pass()
    {
        manager.StartTimer();
        ActivateAction(false);
    }

    public void Attack()
    {
        manager.StartTimer();
        manager.AttackEnnemy();
        ActivateAction(false);
    }

    public void Defence()
    {
        manager.StartTimer();
        ActivateAction(false);
        manager.ShieldUP = true;
        HUDManager.Instance.DisplayShield(true);
    }

    public void LvlUp()
    {
        CAttack.text = manager.Getdmg().x + "-" + manager.Getdmg().y;
        CAttackSpeed.text = manager.GetSpeed().ToString();
        Clife.text = manager.currentlife.ToString()+"/"+manager.maxLife.ToString();
        LvlUpScreen.SetActive(true);
        ActivateAction(false);

        if (manager.currentlife >= manager.maxLife)
            heal.interactable = false;
        else
            heal.interactable = true;

        if (manager.GetSpeed() <= manager.MinimumSpeed)
        {
            speedAttackB.interactable = false;
            speedAttackB.GetComponentInChildren<Text>().text = "max Level";
        }
        else
        {
            speedAttackB.interactable = true;
            speedAttackB.GetComponentInChildren<Text>().text = "- 0.25 secondes";
        }

        if (manager.Getdmg().y >= manager.MaxAttack)
        {
            attackB.interactable = false;
            attackB.GetComponentInChildren<Text>().text = " Max Level";
        }
        else
        {
            attackB.interactable = true;
            attackB.GetComponentInChildren<Text>().text = "+10";
        }

    }

    public void LvlUPLeave()
    {
        LvlUpScreen.SetActive(false);
        ActivateAction(true);
    }

    public void DisplayLvlUp(bool display)
    {
        LvlUpScreen.SetActive(display);
    }

    public void LvlUpAttack(int amount)
    {
        manager.StartTimer();
        LvlUpScreen.SetActive(false);
        manager.Setdmg(new Vector2(manager.Getdmg().x + amount, manager.Getdmg().y + amount));
    }

    public void LvlUpAttackSpeed(float amount)
    {
        manager.StartTimer();
        LvlUpScreen.SetActive(false);
        manager.SetSpeed(manager.GetSpeed() - amount);
        ResizeSpeedAttack(manager.GetSpeed());
    }

    private void ResizeSpeedAttack(float newMax)
    {
        SetYourTimer(maxTimer);
        maxTimer = newMax;
        cachedYTimer = barTransformTimer.localPosition.y;
        maxXValueTimer = barTransformTimer.localPosition.x;
        minXValueTimer = barTransformTimer.localPosition.x - barTransformTimer.rect.width;
        SetYourTimer(0);
    }

    public void LvlUpLife(int amount)
    {
        manager.StartTimer();
        LvlUpScreen.SetActive(false);
        manager.HealPlayer(amount);
    }

    public void RestartValue()
    {
        SetYourLife(maxLife);
        SetEnnemyLife(maxLifeEnnemy);
        SetYourTimer(maxTimer);
        SetEnnemyTimer(maxTimerEnnemy);
    }

    public void SetPicBoss(int index)
    {
        picBoss.sprite = bossSprites[index];
    }

    public void SetEdnScreen(bool display,bool restart)
    {
        if (restart)
            restartButton.SetActive(true);
        else
            endButton.SetActive(true);

        endScreen.SetActive(display);
        ActivateAction(false);
    }

    public void ButtonRestart()
    {
        RestartValue();
        HUDManager.Instance.DisplayEndTurnFight(false, false);
        endButton.SetActive(false);
        restartButton.SetActive(false);
        HUDManager.Instance.DisplayTimer(false);
        HUDManager.Instance.DisplayTuto(false);
        HUDManager.Instance.DisplayMenuPause(false);
        HUDManager.Instance.DisplayTurnFight(false);
        HUDManager.Instance.StartScene("TurnFight");
    }

    public void ButtonContinue()
    {
        RestartValue();
        HUDManager.Instance.DisplayEndTurnFight(false, false);
        endButton.SetActive(false);
        restartButton.SetActive(false);
        HUDManager.Instance.DisplayTimer(false);
        HUDManager.Instance.DisplayTuto(false);
        HUDManager.Instance.DisplayMenuPause(false);
        HUDManager.Instance.DisplayTurnFight(false);
        HUDManager.Instance.StartScene("Dialogue");
        
    }

    public void displayShield(bool display)
    {
        shield.SetActive(display);
    }

    public void EditDmgIndicatorYou(int value)
    {
        dmgIndicatorYou.gameObject.SetActive(true);
        dmgIndicatorYou.text = "-"+value.ToString();
        Invoke("displayOffYou", 0.5f);
    }

    private void displayOffYou()
    {
        dmgIndicatorYou.gameObject.SetActive(false);
    }

    public void EditDmgIndicatorEnemy(int value)
    {
        dmgIndicatorEnemy.gameObject.SetActive(true);
        dmgIndicatorEnemy.text = "-" + value.ToString();
        Invoke("displayOffEnemy", 0.5f);
    }

    private void displayOffEnemy()
    {
        dmgIndicatorEnemy.gameObject.SetActive(false);
    }
}
