using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputs : MonoBehaviour
{
    public CharacterAttributes cA;
    public PlayerMove playerMove;
    public PlayerSkills playerSkills;

    //Move-------------------------------------
    private Vector3 inputDir;
    public float InputDir { get => inputDir.x; }

    //PhysicalAtk------------------------
    private bool attacking;
    public bool Attacking { get => attacking; }
    public int RandomAtkMelee { get; set; }
    public bool MeleeAtkEnabled {get; set;}

    public float y;
    private float x = 2;
    void Start()
    {
        MeleeAtkEnabled = true;
    }

    private void Update()
    {
        if (!attacking)
        {
            //PlayerMove
            inputDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);

            //PhysicalAtk------------------------
            if(Input.GetMouseButton(0) && MeleeAtkEnabled){
                RandomAtkMelee = Random.Range(1, 4);
                SetAttacking();
            }
        }
            if (Input.GetKeyDown(KeyCode.Alpha1)) FloorOfHellSkill();
            if (Input.GetKeyDown(KeyCode.Alpha2)) WaterSpikesSkill();
            if (Input.GetKeyDown(KeyCode.Alpha3)) BladesOfWindSkill();
            if (Input.GetKeyDown(KeyCode.Alpha4)) LifeStealSkill();
            if (Input.GetKeyDown(KeyCode.Alpha5)) LuckySkill();
            if (Input.GetKeyDown(KeyCode.Alpha6)) InvencibleSkill();
    }

    public void FloorOfHellSkill(){
        if(!attacking && playerSkills.FloorOfHellLevel > 0 && cA.Mana > playerSkills.FloorOfHellManaCost && !playerSkills.FloorOfHellCountdown){
            RandomAtkMelee = 4;
            SetAttacking();
            Vector3 pos = new Vector3(transform.position.x, transform.position.y + .5f, -2);
            playerSkills.SpawnSkill(.33f,0, playerSkills.FloorOfHellManaCost, .5f, playerSkills.FloorOfHellLevel, pos,Quaternion.identity);

            playerSkills.StartCountdown("FloorOfHellCD");
        }
    }
    public void WaterSpikesSkill(){
        if (!attacking && playerSkills.WaterSpikesLevel > 0 && cA.Mana > playerSkills.WaterSpikesManaCost * 4 && !playerSkills.WaterSpikesCountdown) {
            RandomAtkMelee = 4;
            SetAttacking();
            //playerSkills.SpawnSkill(1, 1, 0, 1, 1, transform.right * x + transform.position, Quaternion.Euler(playerMove.PlayerRotate()));
            playerSkills.StartCountdown("WaterSpikesCD");
            for (float i = 0; i < 2.5; i += .5f)
            {
                Vector3 p = (transform.right * (x * (i + 1))) + transform.position;
                p.y =  -2.22f;
                p.z = -2f;
                playerSkills.SpawnSkill(i + .3f, 1, playerSkills.WaterSpikesManaCost, 0, playerSkills.WaterSpikesLevel, p, Quaternion.Euler(playerMove.PlayerRotate()));
            }
        }
    }
    public void BladesOfWindSkill(){
        if (!attacking && playerSkills.BladesOfWindLevel > 0 && cA.Mana > playerSkills.BladesOfWindManaCost && !playerSkills.BladesOfWindCountdown) {
            RandomAtkMelee = 4;
            SetAttacking();

            playerSkills.StartCountdown("BladesOfWindCD");

            Vector3 p = transform.right * 1.2f + transform.position;
            p.y = -1.5f;
            p.z = -2f;
            playerSkills.SpawnSkill(.5f, 2, playerSkills.BladesOfWindManaCost, 0, playerSkills.BladesOfWindLevel, p, Quaternion.Euler(playerMove.PlayerRotate()));
        }
    }
    public void LifeStealSkill(){
        if(!attacking && playerSkills.LifeStealLevel > 0 && cA.Mana > playerSkills.LifeStealManaCost && !playerSkills.LifeStealCountdown){
            RandomAtkMelee = 4;
            SetAttacking();
            playerSkills.LifeSteal();
            cA.LifeSteal = true;
            playerSkills.StartCountdown("LifeStealCD");
        }
    }
    public void LuckySkill(){
        if(!attacking && playerSkills.LuckyLevel > 0 && cA.Mana > playerSkills.LuckyManaCost && cA.BonusLuck <= 0 && !playerSkills.LuckyCountdown){
            RandomAtkMelee = 4;
            SetAttacking();
            playerSkills.Lucky();
            playerSkills.StartCountdown("LuckyCD");
        }
    }
    public void InvencibleSkill(){
        if(!attacking && playerSkills.InvencibleLevel > 0 && cA.Mana > playerSkills.InvencibleManaCost && !playerSkills.InvencibleCountdown){
            RandomAtkMelee = 4;
            SetAttacking();
            playerSkills.Invencible();
            cA.Invencible = true;
            playerSkills.StartCountdown("InvencibleCD");
        }
    }


    //this methods is called in Atk anims
    public void SetAttacking() => attacking = !attacking;
}


