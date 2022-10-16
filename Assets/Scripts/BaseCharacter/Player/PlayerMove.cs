using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public PlayerInputs playerInputs;
    public Rigidbody2D rb2D;
    [SerializeField] private float moveSpeed;


    private void FixedUpdate() {
        PlayerRotate();

        if(!playerInputs.Attacking) PlayerMovement();
    }

    public Vector3 PlayerRotate()
    {
        if(playerInputs.InputDir > 0)
            transform.localEulerAngles = new Vector3(0,0,0);
        else if(playerInputs.InputDir < 0)
            transform.localEulerAngles = new Vector3(0,180,0);
        
        return transform.localEulerAngles;
    }

    private void PlayerMovement()
    {
        transform.position += new Vector3(playerInputs.InputDir * moveSpeed, 0, 0) * Time.deltaTime;
    }
    //private void PlayerDash(){
    //    transform.Translate(Vector3.right *  dashForce * Time.deltaTime, Space.Self);
    //}

}
