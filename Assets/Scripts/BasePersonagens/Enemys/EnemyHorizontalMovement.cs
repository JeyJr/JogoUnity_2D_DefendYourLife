using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorizontalMovement : EnemyDetectingObjects
{
    [Header("Enemy Horizontal Movement")] 

    [SerializeField] private float moveSpeed;

    private void Awake()
    {
        Rotate();
    }
    private void FixedUpdate()
    {
        HorizontalMovement();
        SwitchDirection();
    }
    private void HorizontalMovement()
    {
        if (playerAhead)
        {
            if(lookToRight) transform.Translate(moveSpeed * Time.deltaTime * transform.right);
            else transform.Translate(moveSpeed * Time.deltaTime * -transform.right);
        }
    }
    private void SwitchDirection()
    {
        if(playerBehind)
        {
            Rotate();
        }
    }
    private void Rotate()
    {
        transform.localRotation = Quaternion.Euler(0, lookToRight ? 0 : 180, 0);
    }
}
