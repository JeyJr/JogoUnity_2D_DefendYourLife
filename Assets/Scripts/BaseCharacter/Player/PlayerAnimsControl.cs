using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimsControl : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private PlayerMeleeAtk playerMeleeAtk;

    private void Update()
    {
        StateIdleAndWalk();
        StateJump();
        StateAtkMelee();
    }
    private void StateIdleAndWalk()
    {
        if (playerMove.OnGround && !playerMeleeAtk.IsAtk)
        {
            if(playerMove.InputDir != 0 )
                anim.Play("Base Layer.animPlayer_Run", 0);
            else
                anim.Play("Base Layer.animPlayer_Idle", 0);
        }
    }
    private void StateJump()
    {
        if (!playerMove.OnGround)
        {
            if(playerMove.rb2D.velocity.y > 0)
                anim.Play("Base Layer.animPlayer_Jump", 0);
            else
                anim.Play("Base Layer.animPlayer_Fall", 0);
        }
    }
    private void StateAtkMelee()
    {
        if (playerMove.OnGround)
        {
            if(playerMeleeAtk.IsAtk)
                anim.Play("Base Layer.animPlayer_MeleeAtk1", 0);
        }
    }
}
