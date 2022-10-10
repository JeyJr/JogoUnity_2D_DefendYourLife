using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    private CharacterAttributes cAttributes;

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
        cAttributes = GetComponent<CharacterAttributes>();
    }

    private void Update()
    {
        //PlayerMove
        inputDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);

        //PhysicalAtk------------------------
        if (!attackingMelee && Input.GetMouseButton(0)){
            RandomAtkMelee = Random.Range(1, 4);
            SetAttackingMelee();
        } 

        if (!attackingMagic)
        {
            //FloorOfHell
            if (Input.GetKeyDown(KeyCode.Alpha1) && playerSKills.fohLevel > 0 && cAttributes.Mana > playerSKills.fohManaCost) {
                SetAttackingMagical();

                Vector3 pos = new Vector3(transform.position.x, transform.position.y - 1, -4);
                playerSKills.SpawnSkill(.33f,0, playerSKills.fohManaCost * playerSKills.fohLevel, playerSKills.fohDelayHit, playerSKills.fohLevel, pos,Quaternion.identity);
            }

            //BladesOfWind
            if (Input.GetKeyDown(KeyCode.Alpha2) && playerSKills.bowLevel > 0 && cAttributes.Mana > playerSKills.bowManaCost) {
                SetAttackingMelee();
                RandomAtkMelee = 4;

                Quaternion q = Quaternion.Euler(transform.localEulerAngles);
                Vector3 pos = transform.position + (transform.right * 2);
                playerSKills.SpawnSkill(.25f, 1, playerSKills.bowManaCost * playerSKills.bowLevel, 0, playerSKills.bowLevel, pos, q);
            }

            //WaterSpikes   
            if (Input.GetKeyDown(KeyCode.Alpha3) && playerSKills.wsLevel > 0 && cAttributes.Mana > playerSKills.wsManaCost) {
                SetAttackingMagical();

                //int mult = 2;
                Quaternion q = Quaternion.Euler(transform.localEulerAngles);
                for(int i = 0; i < 3; i++){
                    Vector3 pos = new Vector3(transform.position.x + (transform.right.x * ( 1 + i)), transform.position.y - 1.5f, -4);
                    playerSKills.SpawnSkill(i - .2f, 2, playerSKills.wsManaCost * playerSKills.wsLevel, 0, playerSKills.wsLevel, pos, q);
                }
            }
        }
    }


    //this methods is call in animPlayer_MeleeAtk[1][2][3]
    public void SetAttackingMelee()
    {
        attackingMelee = !attackingMelee;
    }

    //this methods is call in animPlayer_MagicAtk1
    public void SetAttackingMagical()
    {
        attackingMagic = !attackingMagic;
    }
}
