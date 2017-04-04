using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class HUDManager : Singleton<HUDManager>
{

    #region BaseClass
    [SerializeField]
    private GameObject LocalWorldCanvas;

    protected HUDManager() { }

    Dictionary<Game.UI_Types, HUDElement> elements;

    void Start()
    {
        DontDestroyOnLoad(LocalWorldCanvas);

    }

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
            key == Game.UI_Types.Dialogue)
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

    public void DisplayScoreBucketGame(bool display)
    {
        HUDElement score;
        if (elements.TryGetValue(Game.UI_Types.ScoreBucket, out score))
        {
            score.displayGroup(true);
        }
    }

    public void InitLifeRunner(int nbLife)
    {
        HUDElement life;
        if (elements.TryGetValue(Game.UI_Types.LifeRunner, out life))
        {
            life.displayGroup(true);
            ((HUDLifeRunner)life).InitLife(nbLife);
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

    public void DisplayTuto(bool display, CharacterBehaviour player, HUDTuto.tutoStyle stylish = HUDTuto.tutoStyle.Bucket)
    {
        HUDElement tuto;
        if (elements.TryGetValue(Game.UI_Types.Tutorial, out tuto))
        {
            if (display)
            {
                tuto.displayGroup(true);
                ((HUDTuto)tuto).Init(stylish, player);
            }
            else
            {
                tuto.displayGroup(false);
            }
        }
    }

    public void DisplayTimer(bool display,CharacterBehaviour player)
    {
        HUDElement timer;
        if (elements.TryGetValue(Game.UI_Types.Timer, out timer))
        { 
            if (display)
            {
                timer.displayGroup(true);
                ((HUDTimer)timer).StartCount(player);
            }
            else
            {
                timer.displayGroup(false);
            }
        }
    }

    public void DisplayEndRunner(bool display)
    {
        HUDElement end;
        if (elements.TryGetValue(Game.UI_Types.EndRunner, out end))
        {
            end.displayGroup(display);
        }
    }


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

    public void DisplaySatrtWave(bool display)
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
            money.displayGroup(true);
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
            if(display)
                ((HUDLevelUpTower)levelUp).Init(script);
        }
    }

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
            dialg.displayGroup(true);
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
}


