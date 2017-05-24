using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class HUDManager : Singleton<HUDManager>
{

    #region BaseClass

    protected HUDManager() { }

    Dictionary<Game.UI_Types, HUDElement> elements;

    public void registerElement(Game.UI_Types key, HUDElement element)
    {
        if (key == Game.UI_Types.NULL)
            return;

        if (elements == null)
            elements = new Dictionary<Game.UI_Types, HUDElement>();

        if (!elements.ContainsKey(key))
            elements.Add(key, element);
        else
            Debug.LogError("HUDManager already contains key " + key);

        disableElement(key, element);

    }

    void disableElement(Game.UI_Types key, HUDElement element)
    {
        if (key == Game.UI_Types.ScoreBucket ||
            key == Game.UI_Types.LifeRunner ||
            key == Game.UI_Types.Tutorial ||
            key == Game.UI_Types.Timer ||
            key == Game.UI_Types.EndRunner ||
            key == Game.UI_Types.TowerShop ||
            key == Game.UI_Types.StartWave ||
            key == Game.UI_Types.MoneyTowerDefence ||
            key == Game.UI_Types.LevelUpTower ||
            key == Game.UI_Types.Dialogue ||
            key == Game.UI_Types.Shooter ||
            key == Game.UI_Types.TurnFight ||
            key == Game.UI_Types.MenuPause ||
            key == Game.UI_Types.EndScreenShooter ||
            key == Game.UI_Types.Credit ||
            key == Game.UI_Types.CV ||
            key == Game.UI_Types.Menu ||
            key == Game.UI_Types.EndTowerDefence)
            element.displayGroup(false, .0f, false, false);
    }

    //to get the gameObject we want from the dictionnary
    public GameObject getElement(Game.UI_Types key)
    {
        HUDElement obj;
        if (elements.TryGetValue(key, out obj))
        {
            return obj.gameObject;
        }
        Debug.LogError("No Element with the Type :" + key);
        return null;
    }

    public void DisableAll()
    {
        foreach (KeyValuePair<Game.UI_Types, HUDElement> element in elements)
        {

            disableElement(element.Key, element.Value);
        }
    }

    #endregion

    public void DisplayTuto(bool display, bool firstTime = false, HUDTuto.tutoStyle stylish = HUDTuto.tutoStyle.Bucket)
    {
        HUDElement tuto;
        if (elements.TryGetValue(Game.UI_Types.Tutorial, out tuto))
        {
            tuto.displayGroup(display);
            if (display)
                ((HUDTuto)tuto).Init(stylish,firstTime);
        }
    }

    public void InitTimer(TimerManager script)
    {
        HUDElement timer;
        if (elements.TryGetValue(Game.UI_Types.Timer, out timer))
        {
            ((HUDTimer)timer).InitTimer(script);
        }
    }

    public void DisplayTimer(bool display)
    {
        HUDElement timer;
        if (elements.TryGetValue(Game.UI_Types.Timer, out timer))
        {
            if (display)
            {
                timer.displayGroup(true);
                ((HUDTimer)timer).StartCount();
            }
            else
            {
                timer.displayGroup(false);
            }
        }
    }

    public void PauseTimer(bool pause)
    {
        HUDElement timer;
        if (elements.TryGetValue(Game.UI_Types.Timer, out timer))
        {
            ((HUDTimer)timer).PauseCount(pause);
        }
    }

    public void DisplayScoreBucketGame(bool display)
    {
        HUDElement score;
        if (elements.TryGetValue(Game.UI_Types.ScoreBucket, out score))
        {
            score.displayGroup(true);
        }
    }

    public void InitMenuPause(string nextScene = "", string previousScene = "")
    {
        HUDElement passLevel;
        if (elements.TryGetValue(Game.UI_Types.MenuPause, out passLevel))
        {
            ((HUDMenuPause)passLevel).Init(nextScene, previousScene);
        }
    }

    public void DisplayMenuPause(bool display)
    {
        HUDElement passLevel;
        if (elements.TryGetValue(Game.UI_Types.MenuPause, out passLevel))
        {
            passLevel.displayGroup(display);
        }
    }

    #region Runner
    public void DisplayLifeRunner(bool display, int nbLife = 0)
    {
        HUDElement life;
        if (elements.TryGetValue(Game.UI_Types.LifeRunner, out life))
        {
            if (display)
            {
                life.displayGroup(display);
                ((HUDLifeRunner)life).InitLife(nbLife);
            }
            else
            {
                life.displayGroup(false);
            }
        }
    }

    public void RemoveLife(int nb)
    {
        HUDElement life;
        if (elements.TryGetValue(Game.UI_Types.LifeRunner, out life))
        {
            ((HUDLifeRunner)life).RemoveLife(nb);
        }
    }

    public void DisplayEndRunner(bool display,string t = "")
    {
        HUDElement end;
        if (elements.TryGetValue(Game.UI_Types.EndRunner, out end))
        {
            if (display)
                ((HUDEndRunner)end).Init(t);
            end.displayGroup(display);
        }
    }
    #endregion

    #region TowerDefence
    public void InitTowerShop(HUDTowerShop.elementStruct[] elementsList)
    {
        HUDElement shopT;
        if (elements.TryGetValue(Game.UI_Types.TowerShop, out shopT))
        {
            ((HUDTowerShop)shopT).Init(elementsList);
        }
    }

    public void InitScriptTower(TowerBehaviour script)
    {
        HUDElement shopT;
        if (elements.TryGetValue(Game.UI_Types.TowerShop, out shopT))
        {
            ((HUDTowerShop)shopT).InitScriptTower(script);
        }
    }

    public void DisplayTowerShop(bool display)
    {
        HUDElement shopT;
        if (elements.TryGetValue(Game.UI_Types.TowerShop, out shopT))
        {
            shopT.displayGroup(display);
        }
    }

    public void InitStartWave(GameManagerTower script)
    {
        HUDElement startWave;
        if (elements.TryGetValue(Game.UI_Types.StartWave, out startWave))
        {
            ((HUDStartWave)startWave).InitButtonStartWave(script);
        }
    }

    public void DisplayStartWave(bool display)
    {
        HUDElement startWave;
        if (elements.TryGetValue(Game.UI_Types.StartWave, out startWave))
        {
            startWave.displayGroup(display);
        }
    }

    public void EnableButtonWaveStart()
    {
        HUDElement startWave;
        if (elements.TryGetValue(Game.UI_Types.StartWave, out startWave))
        {
            ((HUDStartWave)startWave).EnableButton();
        }
    }

    public void DisplayMoneyTowerDefence(bool display)
    {
        HUDElement money;
        if (elements.TryGetValue(Game.UI_Types.MoneyTowerDefence, out money))
        {
            money.displayGroup(display);
        }
    }

    public void EditMoneyTowerDefence(float amount)
    {
        HUDElement money;
        if (elements.TryGetValue(Game.UI_Types.MoneyTowerDefence, out money))
        {
            money.setText(amount.ToString());
        }
    }
    
    public void DisplayLevelUpTower(bool display, TowerBehaviour script)
    {
        HUDElement levelUp;
        if (elements.TryGetValue(Game.UI_Types.LevelUpTower, out levelUp))
        {
            levelUp.displayGroup(display);
            if (display)
                ((HUDLevelUpTower)levelUp).Init(script);
        }
    }

    public void DisplayEndScreenTowerDefence(bool display, string title = "")
    {
        HUDElement endScreen;
        if (elements.TryGetValue(Game.UI_Types.EndTowerDefence, out endScreen))
        {
            endScreen.displayGroup(display);
            if (display)
                ((HUDEndTowerDefence)endScreen).SetTitle(title);
        }
    }
    #endregion

    #region Dialogue
    public void InitDialogue(ManageDialogue script)
    {
        HUDElement dialg;
        if (elements.TryGetValue(Game.UI_Types.Dialogue, out dialg))
        {
            ((HUDDialogues)dialg).Init(script);
        }
    }

    public void DisplayDialogue(bool display)
    {
        HUDElement dialg;
        if (elements.TryGetValue(Game.UI_Types.Dialogue, out dialg))
        {
            dialg.displayGroup(display);
        }
    }

    public void SetTextDialogue(string text)
    {
        HUDElement dialg;
        if (elements.TryGetValue(Game.UI_Types.Dialogue, out dialg))
        {
            ((HUDDialogues)dialg).SetText(text);
        }
    }

    public void DisplayButton(bool display)
    {
        HUDElement dialg;
        if (elements.TryGetValue(Game.UI_Types.Dialogue, out dialg))
        {
            ((HUDDialogues)dialg).DisplayButton(display);
        }
    }

    public void DisplaySuccess(bool display)
    {
        HUDElement dialg;
        if (elements.TryGetValue(Game.UI_Types.Dialogue, out dialg))
        {
            ((HUDDialogues)dialg).DisplaySuccess(display);
        }
    }
    #endregion

    #region Shooter
    public void DisplayShooter(bool display)
    {
        HUDElement shooter;
        if (elements.TryGetValue(Game.UI_Types.Shooter, out shooter))
        {
            shooter.displayGroup(display);
        }
    }

    public void InitShooter(int timeTotal)
    {
        HUDElement shooter;
        if (elements.TryGetValue(Game.UI_Types.Shooter, out shooter))
        {
            ((HUDShooter)shooter).Init(timeTotal);
        }
    }

    public void setTimerShooter(double timer)
    {
        HUDElement shooter;
        if (elements.TryGetValue(Game.UI_Types.Shooter, out shooter))
        {
            ((HUDShooter)shooter).SetTimer(timer);
        }
    }

    public void SetScoreShooter(int score)
    {
        HUDElement shooter;
        if (elements.TryGetValue(Game.UI_Types.Shooter, out shooter))
        {
            ((HUDShooter)shooter).SetScore(score);
        }
    }

    public void SetManche(bool manche2)
    {
        HUDElement shooter;
        if (elements.TryGetValue(Game.UI_Types.Shooter, out shooter))
        {
            ((HUDShooter)shooter).SetManche(manche2);
        }
    }

    public void DisplayViewFinder(bool display)
    {
        HUDElement shooter;
        if (elements.TryGetValue(Game.UI_Types.Shooter, out shooter))
        {
            ((HUDShooter)shooter).DisplayViewFinder(display);
        }
    }

    public void DisplayEndScreenShooter(bool display, string text = "")
    {
        HUDElement endShooter;
        if (elements.TryGetValue(Game.UI_Types.EndScreenShooter, out endShooter))
        {
            if (display)
                ((HUDFinalScreenShooter)endShooter).Init(text);

            endShooter.displayGroup(display);
        }
    }
    #endregion

    #region TurnFight

    public void DisplayTurnFight(bool display)
    {
        HUDElement turnFight;
        if (elements.TryGetValue(Game.UI_Types.TurnFight, out turnFight))
        {
            turnFight.displayGroup(display);
            if (!display)
                ((HUDTurnFight)turnFight).DisplayLvlUp(false);
        }
    }

    public void InitTurnFight(ManageTurnFight manager, float maxlife, float maxTimer, float maxlifeEnnemy, float maxTimerEnnemy)
    {
        HUDElement turnFight;
        if (elements.TryGetValue(Game.UI_Types.TurnFight, out turnFight))
        {
            ((HUDTurnFight)turnFight).Init(manager, maxlife, maxTimer, maxlifeEnnemy, maxTimerEnnemy);
        }
    }

    public void SetYourLifeTurnFight(float life)
    {
        HUDElement turnFight;
        if (elements.TryGetValue(Game.UI_Types.TurnFight, out turnFight))
        {
            ((HUDTurnFight)turnFight).SetYourLife(life);
        }
    }

    public void GetHitOrHeal(bool hit)
    {
        HUDElement turnFight;
        if (elements.TryGetValue(Game.UI_Types.TurnFight, out turnFight))
        {
            ((HUDTurnFight)turnFight).HitOrHeal(hit);
        }
    }

    public void SetYourTimerTurnFight(float timer)
    {
        HUDElement turnFight;
        if (elements.TryGetValue(Game.UI_Types.TurnFight, out turnFight))
        {
            ((HUDTurnFight)turnFight).SetYourTimer(timer);
        }
    }

    public void SetEnnemyLifeTurnFight(float life)
    {
        HUDElement turnFight;
        if (elements.TryGetValue(Game.UI_Types.TurnFight, out turnFight))
        {
            ((HUDTurnFight)turnFight).SetEnnemyLife(life);
        }
    }

    public void SetBackMaxTimerAttackEnnemy(float value)
    {
        HUDElement turnFight;
        if (elements.TryGetValue(Game.UI_Types.TurnFight, out turnFight))
        {
            ((HUDTurnFight)turnFight).setBackEnnemyTimerAttack(value);
        }
    }

    public void SetEnnemyTimerTurnFight(float timer)
    {
        HUDElement turnFight;
        if (elements.TryGetValue(Game.UI_Types.TurnFight, out turnFight))
        {
            ((HUDTurnFight)turnFight).SetEnnemyTimer(timer);
        }
    }

    public void ActivateButton(bool activate)
    {
        HUDElement turnFight;
        if (elements.TryGetValue(Game.UI_Types.TurnFight, out turnFight))
        {
            ((HUDTurnFight)turnFight).ActivateAction(activate);
        }
    }

    public void RestartTurnFight()
    {
        HUDElement turnFight;
        if (elements.TryGetValue(Game.UI_Types.TurnFight, out turnFight))
        {
            ((HUDTurnFight)turnFight).RestartValue();
        }
    }

    public void SetPicBoss(int index)
    {
        HUDElement turnFight;
        if (elements.TryGetValue(Game.UI_Types.TurnFight, out turnFight))
        {
            ((HUDTurnFight)turnFight).SetPicBoss(index);
        }
    }

    public void DisplayEndTurnFight(bool display,bool restart)
    {
        HUDElement turnFight;
        if (elements.TryGetValue(Game.UI_Types.TurnFight, out turnFight))
        {
            ((HUDTurnFight)turnFight).SetEdnScreen(display, restart);
        }
    }

    public void DisplayShield(bool display)
    {
        HUDElement turnFight;
        if (elements.TryGetValue(Game.UI_Types.TurnFight, out turnFight))
        {
            ((HUDTurnFight)turnFight).displayShield(display);
        }
    }

    public void SetDmgIndicatorYou(int value)
    {
        HUDElement turnFight;
        if (elements.TryGetValue(Game.UI_Types.TurnFight, out turnFight))
        {
            ((HUDTurnFight)turnFight).EditDmgIndicatorYou(value);
        }
    }

    public void SetDmgIndicatorEnemy(int value)
    {
        HUDElement turnFight;
        if (elements.TryGetValue(Game.UI_Types.TurnFight, out turnFight))
        {
            ((HUDTurnFight)turnFight).EditDmgIndicatorEnemy(value);
        }
    }
    #endregion

    #region menu
    public void displayMenu(bool display)
    {
        HUDElement menu;
        if (elements.TryGetValue(Game.UI_Types.Menu, out menu))
        {
            menu.displayGroup(display);
        }
    }

    public void StartScene(string nameScene)
    {
        HUDElement menu;
        if (elements.TryGetValue(Game.UI_Types.Menu, out menu))
        {
            ((HUDMenu)menu).LaunchGame(nameScene);
        }
    }
    
    public void DisplayCredit(bool display)
    {
        HUDElement credit;
        if (elements.TryGetValue(Game.UI_Types.Credit, out credit))
        {
            credit.displayGroup(display);
        }
    }

    public void DisplayCV(bool display)
    {
        HUDElement cv;
        if (elements.TryGetValue(Game.UI_Types.CV, out cv))
        {
            cv.displayGroup(display);
        }
    }
    #endregion

}


