using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
     [SerializeField] private Rigidbody2D rb2D;
     [SerializeField] private float jumpForce, distance;
     [SerializeField] private float moveSpeed;
     private Vector3 inputDir;
    [SerializeField] private LayerMask groundMask; 


    private void Update() {
        inputDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
        Jump();
    }

    private void FixedUpdate() {
        PlayerRotate();
        PlayerMovement();
    }

    private void PlayerRotate()
    {
        if(inputDir.x > 0)
        {
            transform.localEulerAngles = new Vector3(0,0,0);
        }
        else if(inputDir.x < 0){
            transform.localEulerAngles = new Vector3(0,180,0);
        }
    }
    private void PlayerMovement() => transform.position += new Vector3(inputDir.x * moveSpeed, 0,0) * Time.deltaTime;
    private void Jump(){
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Vector2.down, distance, groundMask);
        if(hit2D && Input.GetButtonDown("Jump")){
            rb2D.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    private void OnDrawGizmos() {

        float x = transform.localRotation.y == 0 ? 1 : -1;
        //Face
        Debug.DrawRay(transform.position, new Vector3(x * 1.5f,  0, 0), Color.yellow);

        //Ground
        Debug.DrawRay(transform.position, Vector2.down * distance, Color.green);
    }
}
