using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Animation Controll")]
    [SerializeField] private Animator anim;
    public bool isWalk, isJump; //Controlling anims state in animator

    [Header("Movement Controll")]
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private Transform startPosition;
    [SerializeField] private float jumpForce, distance;
    [SerializeField] private float moveSpeed;
    [SerializeField] private LayerMask groundMask; 
    private Vector3 inputDir;


    private void Update() {
        inputDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
        Jump();
        Anims();
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
    private void PlayerMovement()
    {
        transform.position += new Vector3(inputDir.x * moveSpeed, 0, 0) * Time.deltaTime;

        if (inputDir.x != 0) isWalk = true;
        else isWalk = false;
    }
    private void Jump(){
        RaycastHit2D hit2D = Physics2D.Raycast(startPosition.position, Vector2.down, distance, groundMask);

        if (hit2D && Input.GetButtonDown("Jump"))
        {
            rb2D.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            isJump = true;
        }
        else isJump = false;
    }
    private void Anims()
    {
        anim.SetBool("isWalk", isWalk);
        anim.SetBool("isJump", isJump);
    }
    private void OnDrawGizmos() {
        //Ground
        Debug.DrawRay(startPosition.position, Vector2.down * distance, Color.green);
    }
}
