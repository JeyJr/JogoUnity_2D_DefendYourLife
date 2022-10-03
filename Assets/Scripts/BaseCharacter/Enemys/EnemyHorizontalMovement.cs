using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorizontalMovement : MonoBehaviour
{
    [SerializeField] private EnemyDetectingObjects enemyDetectingObjects;

    [SerializeField] private Animator anim;
    [SerializeField] private float moveSpeed;

    private void Awake()
    {
        Rotate();
    }


    private void Update()
    {
        Anims();
    }

    private void FixedUpdate()
    {
        HorizontalMovement();
        SwitchDirection();
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
        anim.SetBool("isWalk", enemyDetectingObjects.playerAhead);
        anim.SetBool("isAtk", enemyDetectingObjects.atkArea);
    }
}
