using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    //Move-------------------------------------
    private Vector3 inputDir;
    public float InputDir { get => inputDir.x; }

    //PhysicalAtk------------------------
    private bool attackingMelee;
    public bool AttackingMelee { get => attackingMelee; }
    public int RandomAtkMelee { get; set; }

    //MagicalAtk-----------------------------------
    private bool attackingMagic;
    public bool AttackingMagic { get => attackingMagic;}
    PlayerSkills playerSKills;

    private void Start()
    {
        playerSKills = GetComponentInChildren<PlayerSkills>();
    }

    private void Update()
    {
        //PlayerMove
        inputDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);

        //PhysicalAtk------------------------
        if (!attackingMelee && Input.GetMouseButton(0)) SetAttackingMelee();

        if (!attackingMagic)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && playerSKills.fohLevel > 0) {
                playerSKills.SpawnSkill(0, playerSKills.fohManaCost * playerSKills.fohLevel, playerSKills.fohDelayHit, playerSKills.fohLevel, Quaternion.identity);
                SetAttackingMagical();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) && playerSKills.bowLevel > 0) {
                Quaternion q = Quaternion.Euler(transform.localEulerAngles);
                playerSKills.SpawnSkill(1, playerSKills.bowManaCost * playerSKills.bowLevel, 0, playerSKills.bowLevel, q);
                SetAttackingMelee();
            }
        }
    }


    //this methods is call in animPlayer_MeleeAtk[1][2][3]
    public void SetAttackingMelee()
    {
        RandomAtkMelee = Random.Range(1, 4);
        attackingMelee = !attackingMelee;
    }

    //this methods is call in animPlayer_MagicAtk1
    public void SetAttackingMagical()
    {
        attackingMagic = !attackingMagic;
    }
}
