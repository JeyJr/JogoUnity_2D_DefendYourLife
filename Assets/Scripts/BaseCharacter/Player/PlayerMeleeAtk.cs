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
    public int RandomAtkMelee { get; set;}

    private void Update()
    {
        if (!attackingMelee && Input.GetMouseButton(0)) SetAttackingMelee();
    }


    //this methods is call in animPlayer_MeleeAtk1
    public void AtkArea() {
        hit2DAtk = Physics2D.RaycastAll(startPosition.position, startPosition.right, atkRange, enemyMask);
        
        if (hit2DAtk != null)
            for (int i = 0; i < hit2DAtk.Length; i++)
            {
                //fazer critico
                hit2DAtk[i].collider.GetComponent<CharacterAttributes>().TakeDMG(characterAttributes.DealDmg(), characterAttributes.criticalDmg, GetComponent<CharacterExpControl>());
            }
    }

    //this methods is call in animPlayer_MeleeAtk1
    public void SetAttackingMelee()
    {
        RandomAtkMelee = Random.Range(1,4);
        attackingMelee = !attackingMelee;
    }
}
