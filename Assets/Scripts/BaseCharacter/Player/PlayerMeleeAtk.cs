using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAtk : MonoBehaviour
{
    [SerializeField] private CharacterAttributes characterAttributes;

    [SerializeField] private Transform startPosition;
    [SerializeField] public RaycastHit2D[] hit2DAtk;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private float atkRange;

    private bool attackingMelee;
    public bool AttackingMelee { get => attackingMelee;}

    private void Update()
    {
        if (!attackingMelee && Input.GetMouseButton(0)) SetIsAtk();
    }


    //this methods is call in animPlayer_MeleeAtk1
    public void AtkArea() {
        hit2DAtk = Physics2D.RaycastAll(startPosition.position, startPosition.right, atkRange, enemyMask);
        
        if (hit2DAtk != null)
            for (int i = 0; i < hit2DAtk.Length; i++)
            {
                //fazer critico
                hit2DAtk[i].collider.GetComponent<CharacterAttributes>().TakeDMG(characterAttributes.DealDmg(), characterAttributes.criticalDmg);
            }
    }

    //this methods is call in animPlayer_MeleeAtk1
    public void SetIsAtk()
    {
        attackingMelee = !attackingMelee;
    }
}
