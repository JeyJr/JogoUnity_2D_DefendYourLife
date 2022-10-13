using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    private CharacterAttributes cAttributes;
    private PlayerMove playerMove;


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
            if (Input.GetKeyDown(KeyCode.Alpha1) && playerSKills.FloorOfHellLevel > 0 && cAttributes.Mana > playerSKills.FloorOfHellManaCost) {
                SetAttackingMagical();

                Vector3 pos = new Vector3(transform.position.x, transform.position.y - .99f, -4);
                playerSKills.SpawnSkill(.33f,0, playerSKills.FloorOfHellManaCost, .5f, playerSKills.FloorOfHellLevel, pos,Quaternion.identity);
            }

            //BladesOfWind
            if (Input.GetKeyDown(KeyCode.Alpha2) && playerSKills.BladesOfWindLevel > 0 && cAttributes.Mana > playerSKills.BladesOfWindManaCost) {
                SetAttackingMelee();
                RandomAtkMelee = 4;

                Quaternion q = Quaternion.Euler(transform.localEulerAngles);
                Vector3 pos = new Vector3(transform.position.x + .3f, transform.position.y - .5f, -4);
                playerSKills.SpawnSkill(.25f, 1, playerSKills.BladesOfWindManaCost, 0, playerSKills.BladesOfWindLevel, pos, q);
            }

            //WaterSpikes   
            if (Input.GetKeyDown(KeyCode.Alpha3) && playerSKills.WaterSpikesLevel > 0 && cAttributes.Mana > playerSKills.WaterSpikesManaCost) {
                SetAttackingMagical();
                for(float i = 0; i < 4; i+= .5f){
                    Vector3 pos = new Vector3(transform.position.x + transform.right.x * (i * 2), -1.5f, -4f);
                    playerSKills.SpawnSkill(i, 2, playerSKills.WaterSpikesManaCost, 0, playerSKills.WaterSpikesLevel, pos, Quaternion.Euler(transform.localEulerAngles));
                } 
            }

            //LifeSteal
            if(Input.GetKeyDown(KeyCode.Alpha4) && playerSKills.LifeStealLevel > 0 && cAttributes.Mana > playerSKills.LifeStealManaCost){
                SetAttackingMagical();
                playerSKills.LifeSteal();
                cAttributes.LifeSteal = true;
            }
            
            //Lucky
            if(Input.GetKeyDown(KeyCode.Alpha5) && playerSKills.LuckyLevel > 0 && cAttributes.Mana > playerSKills.LuckyManaCost && cAttributes.BonusLuck <= 0)
            {
                SetAttackingMagical();
                playerSKills.Lucky();
            }

            //Invencible
            if(Input.GetKeyDown(KeyCode.Alpha6) && playerSKills.InvencibleLevel > 0 && cAttributes.Mana > playerSKills.InvencibleManaCost)
            {
                SetAttackingMagical();
                playerSKills.Invencible();
                cAttributes.Invencible = true;
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


