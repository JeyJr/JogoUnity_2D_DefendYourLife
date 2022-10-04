using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    [Header("Movement Controll")]
    [SerializeField] private Transform startPosition;
    [SerializeField] private float jumpForce, distance;
    [SerializeField] private float moveSpeed;
    [SerializeField] private LayerMask groundMask; 
    public Rigidbody2D rb2D;
    private Vector3 inputDir;
    private bool onGround;
    private bool jumpActive;

    public float InputDir{ get => inputDir.x;}
    public bool OnGround{ get => onGround;}
    private void Update() {
        inputDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);

        if (Input.GetButtonDown("Jump")) jumpActive = true;
        else jumpActive = false;
    }
    private void FixedUpdate() {
        PlayerRotate();
        PlayerMovement();
        GroundCheck();
        Jump();
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
    }
    private void Jump()
    {
        if(onGround && jumpActive)
            rb2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }
    private void GroundCheck(){
        RaycastHit2D hit2D = Physics2D.Raycast(startPosition.position, Vector2.down, distance, groundMask);

        if (hit2D.collider != null)
            onGround = true;
        else
            onGround = false;
    }

    private void OnDrawGizmos() {
        //Ground
        Debug.DrawRay(startPosition.position, Vector2.down * distance, Color.green);
    }
}
