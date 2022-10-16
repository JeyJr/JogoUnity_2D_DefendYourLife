using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAtk : MonoBehaviour
{
    [SerializeField] private Transform startPosition;
    [SerializeField] public RaycastHit2D[] hit2DAtk;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private float atkRange;
    public CharacterAttributes cA;
    public CharacterExpControl cE;

    //this method is called in Atk anims
    public void Atk() 
    {
        hit2DAtk = Physics2D.RaycastAll(startPosition.position, startPosition.right, atkRange, enemyMask);
        
        if (hit2DAtk != null)
            for (int i = 0; i < hit2DAtk.Length; i++)
            {
                //fazer critico
                hit2DAtk[i].collider.GetComponent<CharacterAttributes>().TakeDMG(cA.DealDmg(true), cA.criticalDmg, cE);
            }
    }

    void OnDrawGizmos()
    {
        Debug.DrawRay(startPosition.position, startPosition.right * atkRange, Color.green);
    }
}
