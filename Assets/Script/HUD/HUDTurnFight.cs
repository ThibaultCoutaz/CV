using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HUDTurnFight : HUDElement {

    private ManageTurnFight manager;

    public Button attack;
    public Button defence;

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
	
	// Update is called once per frame
	void Update () {
		
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
        attack.interactable = activate;
        defence.interactable = activate;
    }

    public void Attack()
    {
        manager.StartTimer();
        manager.AttackEnnemy();
        attack.interactable = false;
        defence.interactable = false;
    }

    public void Defence()
    {
        manager.StartTimer();
        attack.interactable = false;
        defence.interactable = false;
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
}
