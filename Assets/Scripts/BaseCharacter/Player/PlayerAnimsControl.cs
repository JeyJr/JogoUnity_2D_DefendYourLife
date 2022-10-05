using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimsControl : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private PlayerMeleeAtk playerMeleeAtk;
    [SerializeField] private PlayerMagicAtk playerMagicAtk;

    private void Update()
    {
        if (playerMove.OnGround)
        {
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
        else
        {
            AnimJump();
        }
    }
    private void AnimIdleAndWalk()
    {
        if (playerMove.InputDir != 0)
            anim.Play("Base Layer.animPlayer_Run", 0);
        else
            anim.Play("Base Layer.animPlayer_Idle", 0);
    }
    private void AnimJump()
    {
        if (playerMove.rb2D.velocity.y > 0)
            anim.Play("Base Layer.animPlayer_Jump", 0);
        else
            anim.Play("Base Layer.animPlayer_Fall", 0);
    }
    private void AnimAtkMelee()
    {
        anim.Play("Base Layer.animPlayer_MeleeAtk1", 0);
    }
    private void AnimAtkMagic()
    {
        anim.Play("Base Layer.animPlayer_MagicAtk1", 0);
    }
}
