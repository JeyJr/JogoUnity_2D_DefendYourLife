using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAtk : MonoBehaviour
{
    [SerializeField] private CharacterAttributes characterAttributes;
    [SerializeField] private EnemyDetectingObjects enemyDetectingObjects;

    //method in animation atk
    public void Dmg()
    {
        if (enemyDetectingObjects.hit2DAtk.collider != null)
            enemyDetectingObjects.hit2DAtk.collider.GetComponent<CharacterAttributes>().TakeDMG(characterAttributes.DealDmg(), characterAttributes.criticalDmg);
    }
}
