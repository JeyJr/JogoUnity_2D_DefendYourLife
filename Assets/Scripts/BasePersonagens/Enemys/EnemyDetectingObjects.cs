using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectingObjects : MonoBehaviour
{
    [Header("Enemy Detecting Objects")]

    [SerializeField] private LayerMask playerMask;
    [SerializeField] private Transform startPosition;
    [SerializeField] private float frontRange, backRange, atkRange;
    [SerializeField] protected bool playerAhead,playerBehind, atkArea, lookToRight;


    private void Update() {
        DetectingPlayerAhead();
        DetectingPlayerBehind();
        AtkArea();
    }
    //A frente
    private void DetectingPlayerAhead(){
        Debug.DrawRay(startPosition.position, startPosition.right * frontRange, Color.green);
        RaycastHit2D h = Physics2D.Raycast(startPosition.position, startPosition.right, frontRange, playerMask);

        if (h.collider != null && !atkArea) playerAhead = true;
        else playerAhead = false;
    }
    //Atras
    private void DetectingPlayerBehind()
    {
        RaycastHit2D h = Physics2D.Raycast(transform.position, -transform.right, backRange, playerMask);
        Debug.DrawRay(transform.position, -transform.right * backRange, Color.blue);

        if (h.collider != null) {
            playerBehind = true;

            if (h.collider.GetComponent<Transform>().position.x > transform.position.x)
                lookToRight = true;
            else 
                lookToRight = false;
        }
        else playerBehind = false;
    }
    //AtkArea
    private void AtkArea()
    {
        RaycastHit2D h = Physics2D.Raycast(transform.position, transform.right, atkRange, playerMask);
        Debug.DrawRay(transform.position, transform.right * atkRange, Color.red);

        if (h.collider != null) atkArea = true;
        else atkArea = false;
    }
}
