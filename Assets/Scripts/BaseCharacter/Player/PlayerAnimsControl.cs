using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimsControl : MonoBehaviour
{
    public PlayerInputs playerInputs;
    public Animator anim;
    public PlayerMeleeAtk playerMeleeAtk;
    public List<string> animsName = new(); 


    private void Update()
    {
        if(!playerInputs.Attacking)
        {
            AnimIdleAndWalk();
        }
        else if(playerInputs.Attacking)
        {
            AnimAtkMelee();
        }
    }

    private void AnimIdleAndWalk()
    {
        if (playerInputs.InputDir != 0)
            anim.Play($"Base Layer.{animsName[1]}", 0); //Run
        else
            anim.Play($"Base Layer.{animsName[0]}", 0); //Idle
    }
    private void AnimAtkMelee()
    {
        switch(playerInputs.RandomAtkMelee){
            case 1:
                anim.Play($"Base Layer.{animsName[2]}", 0); //Atk1
                break;
            case 2:
                anim.Play($"Base Layer.{animsName[3]}", 0); //Atk2
                break;
            case 3:
                anim.Play($"Base Layer.{animsName[4]}", 0); //Atk3
                break;
            case 4:
                anim.Play($"Base Layer.{animsName[5]}", 0); //Atk4 - Used to Magic Atk
                break;
            default:
                anim.Play($"Base Layer.{animsName[2]}", 0); 
                break;
        }
    }

    public void SetAttacking() => playerInputs.SetAttacking();
    public void SetAtk() => playerMeleeAtk.Atk();
}

