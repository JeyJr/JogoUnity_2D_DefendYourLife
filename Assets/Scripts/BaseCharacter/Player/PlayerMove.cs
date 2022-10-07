using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    PlayerAtk playerMeleeAtk, playerMagicAtk;
    public Rigidbody2D rb2D;

    //-----------------------------------------
    [SerializeField] private float moveSpeed, dashForce, dashDelay = 5;
    private bool useDash, dashActive;
    public bool DashActive { get => dashActive;}
    private Vector3 inputDir;
    public float InputDir{ get => inputDir.x;}




    private void Start() 
    {
        playerMagicAtk = GetComponent<PlayerAtk>();
        playerMeleeAtk = GetComponent<PlayerAtk>();
    }

    private void Update() 
    {
        inputDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);

        if (Input.GetKey(KeyCode.Space) && !useDash){
            useDash = true;
            StartCoroutine(DashDelay());
            SetDashActive();
            PlayerDash();
        }
    }

    private void FixedUpdate() {
        PlayerRotate();

        if(!playerMagicAtk.AttackingMagic && !playerMeleeAtk.AttackingMelee){
            if(!dashActive)
                PlayerMovement();
            //else
            //    PlayerDash();
        }
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
    private void PlayerDash(){
        rb2D.velocity = transform.right * dashForce;
    }

    public void SetDashActive(){
        dashActive = !dashActive;
    }

    IEnumerator DashDelay(){
        yield return new WaitForSeconds(dashDelay);
        useDash = false;
    }
}

    // private void Jump()
    // {
    //     if (onGround && jumpActive)
    //         rb2D.velocity = Vector2.up * jumpForce;    
    //         //rb2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    // }

    // private void GroundCheck(){
    //     RaycastHit2D hit2D = Physics2D.Raycast(startPosition.position, Vector2.down, distance, groundMask);

    //     if (hit2D.collider != null)
    //         onGround = true;
    //     else
    //         onGround = false;
    // }