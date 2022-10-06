using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtk : MonoBehaviour
{
 
    //PhysicalAtk------------------------
    private bool attackingMelee;
    public bool AttackingMelee { get => attackingMelee;}
    public int RandomAtkMelee { get; set;}

    //MagicalAtk-----------------------------------
    private bool attackingMagic;
    public bool AttackingMagic { get => attackingMagic;}


  

    private void Update()
    {
        //PhysicalAtk------------------------
        if (!attackingMelee && Input.GetMouseButton(0)) SetAttackingMelee();

        //FloorOfHell
        if (!AttackingMagic && Input.GetKeyDown(KeyCode.Alpha1)) {
            SetAttackingMagical();
            GetComponentInChildren<SkillFloorOfHell>().SetSkillActivation(true);
        }
    }


    //this methods is call in animPlayer_MeleeAtk[1][2][3]
    public void SetAttackingMelee()
    {
        RandomAtkMelee = Random.Range(1,4);
        attackingMelee = !attackingMelee;
    }

    //this methods is call in animPlayer_MagicAtk1
    public void SetAttackingMagical(){
        attackingMagic = !attackingMagic;
    }
}
