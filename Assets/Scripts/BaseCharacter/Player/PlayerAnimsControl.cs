using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimsControl : MonoBehaviour
{
    PlayerMove playerMove;
    PlayerAtk playerMeleeAtk, playerMagicAtk ;
    Animator anim;
    //-----------------------------------------


    private void Start() 
    {
        playerMove = GetComponent<PlayerMove>();
        playerMeleeAtk = GetComponent<PlayerAtk>();
        playerMagicAtk = GetComponent<PlayerAtk>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(!playerMove.DashActive){
            if(!playerMeleeAtk.AttackingMelee && !playerMagicAtk.AttackingMagic)
            {
                AnimIdleAndWalk();
            }
            else if(playerMeleeAtk.AttackingMelee && !playerMagicAtk.AttackingMagic)
            {
                AnimAtkMelee();
            }
            else if(!playerMeleeAtk.AttackingMelee && playerMagicAtk.AttackingMagic)
            {
                AnimAtkMagic();
            }        
        }
        else{
            AnimDash();
        }
    }
    private void AnimIdleAndWalk()
    {
        if (playerMove.InputDir != 0)
            anim.Play("Base Layer.animPlayer_Run", 0);
        else
            anim.Play("Base Layer.animPlayer_Idle", 0);
    }
    private void AnimAtkMelee()
    {
        switch(playerMeleeAtk.RandomAtkMelee){
            case 1:
                anim.Play("Base Layer.animPlayer_MeleeAtk1", 0);
                break;
            case 2:
                anim.Play("Base Layer.animPlayer_MeleeAtk2", 0);
                break;
            case 3:
                anim.Play("Base Layer.animPlayer_MeleeAtk3", 0);
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
    private void AnimDash()
    {
        anim.Play("Base Layer.animPlayer_Dash", 0);
    }
}

    // private void AnimJump()
    // {
    //     if (playerMove.rb2D.velocity.y > 0)
    //         anim.Play("Base Layer.animPlayer_Jump", 0);
    //     else
    //         anim.Play("Base Layer.animPlayer_Fall", 0);
    // }