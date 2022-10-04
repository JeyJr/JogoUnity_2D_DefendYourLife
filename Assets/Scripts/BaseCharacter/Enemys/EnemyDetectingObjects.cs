using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectingObjects : MonoBehaviour
{
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private Transform frontStartPosition, backStartPosition, atkStartPosition;
    [SerializeField] private float frontRange, backRange, atkRange;

    public bool playerAhead,playerBehind, atkArea, lookToRight;
    public RaycastHit2D hit2DAtk;


    private void Update() {
        DetectingPlayerAhead();
        DetectingPlayerBehind();
        AtkArea();
    }
    //A frente
    private void DetectingPlayerAhead(){
        RaycastHit2D h = Physics2D.Raycast(frontStartPosition.position, frontStartPosition.right, frontRange, playerMask);

        if (h.collider != null && !atkArea) playerAhead = true;
        else playerAhead = false;
    }
    //Atras
    private void DetectingPlayerBehind()
    {
        RaycastHit2D h = Physics2D.Raycast(backStartPosition.position, -backStartPosition.right, backRange, playerMask);

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
        hit2DAtk = Physics2D.Raycast(atkStartPosition.position, atkStartPosition.right, atkRange, playerMask);

        if (hit2DAtk.collider != null) atkArea = true;
        else atkArea = false;
    }

    private void OnDrawGizmos()
    {   
        //A frente
        Debug.DrawRay(frontStartPosition.position, frontStartPosition.right * frontRange, Color.green);

        //Atras
        Debug.DrawRay(backStartPosition.position, -transform.right * backRange, Color.blue);

        //AtkArea
        Debug.DrawRay(atkStartPosition.position, transform.right * atkRange, Color.red);
    }
}
