using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAtk : MonoBehaviour
{
    [SerializeField] private Transform startPosition;
    [SerializeField] public RaycastHit2D[] hit2DAtk;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private float atkRange;

    //this methods is call in animPlayer_MeleeAtk[1][2][3]
    public void AtkArea() 
    {
        CharacterAttributes cAttributes = GetComponent<CharacterAttributes>();
        hit2DAtk = Physics2D.RaycastAll(startPosition.position, startPosition.right, atkRange, enemyMask);
        
        if (hit2DAtk != null)
            for (int i = 0; i < hit2DAtk.Length; i++)
            {
                //fazer critico
                hit2DAtk[i].collider.GetComponent<CharacterAttributes>().TakeDMG(cAttributes.DealDmg(true), cAttributes.criticalDmg, GetComponent<CharacterExpControl>());
            }
    }
}
