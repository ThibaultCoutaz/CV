using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnnemy : MonoBehaviour
{

    public TowerBehaviour towerParent;

    // Use this for initialization
    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<BehaviourTowerEnnemy>())
        {
            towerParent.EnnemiesInRange.Add(other.gameObject.GetComponent<BehaviourTowerEnnemy>());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<BehaviourTowerEnnemy>())
        {
            towerParent.EnnemiesInRange.Remove(other.gameObject.GetComponent<BehaviourTowerEnnemy>());
        }
    }
}
