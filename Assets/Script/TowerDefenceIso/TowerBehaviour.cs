using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{

    public GameManagerTower scriptManager;

    [System.Serializable]
    public struct Level
    {
        public int level;
        public GameObject sprite;
        public GameObject rangeCollider;
        public int dmg;
        public float speedAttack;
    }

    [System.Serializable]
    public struct towerType
    {
        public Game.Tower_Types type;
        public Level[] levels;
    }

    public towerType[] listTower;

    private int currentLevel = 0; public int GetCurrentLevel() { return currentLevel; }
    private int maxlevel;
    private Game.Tower_Types currentType = Game.Tower_Types.NONE;
    private int currentDMG = 0;
    private float currentSpeedAttack = 0;

    private bool canFire = false;

    //[HideInInspector]
    public List<BehaviourTowerEnnemy> EnnemiesInRange;

    // Update is called once per frame
    void Update()
    {
        if(EnnemiesInRange.Count > 0 && canFire)
        {
            canFire = false;
            StartCoroutine("Fire");
        }
    }

    IEnumerator Fire()
    {
        if (EnnemiesInRange[0].hitEnnemy(currentDMG))
        {
            EnnemiesInRange.Remove(EnnemiesInRange[0]);
        }
        yield return new WaitForSeconds(currentSpeedAttack);
        canFire = true;
    }

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (currentType == Game.Tower_Types.NONE)
            {
                HUDManager.Instance.InitScriptTower(this);
                HUDManager.Instance.DisplayTowerShop(true);
            }
            else
            {
                HUDManager.Instance.DisplayLevelUpTower(true, this);
            }
        }
    }

    public void LevelUpTower()
    {
        towerType typetmp = GetTowerType(currentType);

        if (typetmp.type != Game.Tower_Types.NONE)
        {
            if (currentLevel + 1 < typetmp.levels.Length)
            {
                //First remove previous
                typetmp.levels[currentLevel].sprite.SetActive(false);
                //typetmp.levels[currentLevel].rangeCollider.SetActive(false); //If one day the range change with level

                currentLevel++;
                typetmp.levels[currentLevel].sprite.SetActive(true);
                //typetmp.levels[currentLevel].rangeCollider.SetActive(true);
                currentDMG = typetmp.levels[currentLevel].dmg;
                currentSpeedAttack = typetmp.levels[currentLevel].speedAttack;


                scriptManager.EditMoney(0); //A modifier pour le levelUp en echange de thunes
            }
            else
            {
                Debug.LogError("LevelMax");
            }
        }
    }

    public void SellTower()
    {
        towerType typetmp = GetTowerType(currentType);

        if (typetmp.type != Game.Tower_Types.NONE)
        {
            typetmp.levels[currentLevel].sprite.SetActive(false);
            typetmp.levels[currentLevel].rangeCollider.SetActive(false);
            currentType = Game.Tower_Types.NONE;
            currentDMG = 0;
            currentSpeedAttack = -1;
            canFire = false;

            currentLevel = 0;
            scriptManager.EditMoney(0); //A modifier pour le vendre en echange de thunes
        }
    }

    public void SetTower(Game.Tower_Types _type,float price)
    {
        towerType typetmp = GetTowerType(_type);

        if(typetmp.type != Game.Tower_Types.NONE)
        {
            typetmp.levels[currentLevel].sprite.SetActive(true);
            typetmp.levels[currentLevel].rangeCollider.SetActive(true);
            currentType = _type;
            currentDMG = typetmp.levels[currentLevel].dmg;
            currentSpeedAttack = typetmp.levels[currentLevel].speedAttack;
            canFire = true;

            maxlevel = typetmp.levels.Length-1;

            scriptManager.EditMoney(-price);
        }
    }

    private towerType GetTowerType(Game.Tower_Types type)
    {
        for (int i = 0; i < listTower.Length; i++)
        {
            if (listTower[i].type == type)
            {
                return listTower[i];
            }
        }

        towerType typeNULL = new towerType();
        typeNULL.type = Game.Tower_Types.NONE;

        return typeNULL;
    }

    //************Inutile car level rangé dans lordre******************//
    //private Level GetGoodLevel(towerType tower,int level)
    //{
    //    for (int i = 0; i < tower.levels.Length; i++)
    //    {
    //        if (tower.levels[i].level == level)
    //        {
    //            return tower.levels[i];
    //        }
    //    }

    //    Level levelNull = new towerType();
    //    typeNULL.type = Game.Tower_Types.NONE;

    //    return typeNULL;
    //}

    public Level GetInfosLevels(int level)
    {
        towerType typetmp = GetTowerType(currentType);
        Level leveltmp = new Level();

        if (typetmp.type != Game.Tower_Types.NONE)
        {
            leveltmp.dmg = typetmp.levels[level].dmg;
            leveltmp.speedAttack = typetmp.levels[level].speedAttack;
        }
        else
        {
            leveltmp.dmg = -1;
            leveltmp.speedAttack = -1;
        }

        return leveltmp;
    }

    public bool IsMaxLevel()
    {
        if (currentLevel < maxlevel)
            return false;
        else
            return true;
    }
}
