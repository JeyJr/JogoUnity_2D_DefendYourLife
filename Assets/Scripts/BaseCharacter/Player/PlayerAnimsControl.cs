using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimsControl : MonoBehaviour
{
    PlayerInputs playerInputs;
    Animator anim;
    //-----------------------------------------


    private void Start() 
    {
        playerInputs = GetComponent<PlayerInputs>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(!playerInputs.AttackingMelee && !playerInputs.AttackingMagic)
        {
            AnimIdleAndWalk();
        }
        else if(playerInputs.AttackingMelee && !playerInputs.AttackingMagic)
        {
            AnimAtkMelee();
        }
        else if(!playerInputs.AttackingMelee && playerInputs.AttackingMagic)
        {
            AnimAtkMagic();
        }        
    }
    private void AnimIdleAndWalk()
    {
        if (playerInputs.InputDir != 0)
            anim.Play("Base Layer.animPlayer_Run", 0);
        else
            anim.Play("Base Layer.animPlayer_Idle", 0);
    }
    private void AnimAtkMelee()
    {
        switch(playerInputs.RandomAtkMelee){
            case 1:
                anim.Play("Base Layer.animPlayer_MeleeAtk1", 0);
                break;
            case 2:
                anim.Play("Base Layer.animPlayer_MeleeAtk2", 0);
                break;
            case 3:
                anim.Play("Base Layer.animPlayer_MeleeAtk3", 0);
                break;
            case 4:
                anim.Play("Base Layer.animPlayer_MeleeAtk4", 0);
                break;
            default:
                anim.Play("Base Layer.animPlayer_MeleeAtk1", 0);
                break;
        }
    }
    private void AnimAtkMagic()
    {
        anim.Play("Base Layer.animPlayer_MagicAtk1", 0);
    }

}

