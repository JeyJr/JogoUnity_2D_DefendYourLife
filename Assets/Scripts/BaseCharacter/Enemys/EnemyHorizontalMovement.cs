using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorizontalMovement : MonoBehaviour
{
    [SerializeField] private EnemyDetectingObjects enemyDetectingObjects;
    [SerializeField] private CharacterAttributes cAttributes;
    [SerializeField] private Animator anim;
    private float moveSpeed;

    [SerializeField] private string enemyName;
     private string animIdle, animRun, animAtk, animDying;

    private void Awake()
    {
        moveSpeed = Random.Range(0.5f, 1.2f);
        Rotate();

        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        

        foreach(var i in clips)
        {
            if(i.name == $"{enemyName}Idle")
                animIdle = i.name;
            else if(i.name == $"{enemyName}Run")
                animRun = i.name;            
            else if(i.name == $"{enemyName}Atk")
                animAtk = i.name;            
            else if(i.name == $"{enemyName}Dying")
                animDying = i.name;

        }
         
    }


    private void Update()
    {
        Anims();
    }

    private void FixedUpdate()
    {
        if(!cAttributes.Dead){
            HorizontalMovement();
            SwitchDirection();
        }
    }
    private void HorizontalMovement()
    {
        if (enemyDetectingObjects.playerAhead)
        {
            if(enemyDetectingObjects.lookToRight) transform.Translate(moveSpeed * Time.deltaTime * transform.right);
            else transform.Translate(moveSpeed * Time.deltaTime * -transform.right);
        }
    }
    private void SwitchDirection()
    {
        if(enemyDetectingObjects.playerBehind)
        {
            Rotate();
        }
    }
    private void Rotate()
    {
        transform.localRotation = Quaternion.Euler(0, enemyDetectingObjects.lookToRight ? 0 : 180, 0);
    }

    private void Anims()
    {
        if (!cAttributes.Dead)
        {
            if (enemyDetectingObjects.atkArea)
            {
                anim.Play($"Base Layer.{animAtk}");
            }
            else
            {
                if (enemyDetectingObjects.playerAhead)
                {
                    anim.Play($"Base Layer.{animRun}");
                }
                else
                {
                    anim.Play($"Base Layer.{animIdle}");
                }
            }
        }
        else
        {
            anim.Play($"Base Layer.{animDying}");
        }
        //anim.SetBool("isWalk", enemyDetectingObjects.playerAhead);
        //anim.SetBool("isAtk", enemyDetectingObjects.atkArea);
        //anim.SetBool("isDead", cAttributes.Dead);
    }
}
