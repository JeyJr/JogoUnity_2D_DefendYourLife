using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    PlayerAtk playerMeleeAtk, playerMagicAtk;
    public Rigidbody2D rb2D;
    //-----------------------------------------
    private Vector3 inputDir;
    public float InputDir{ get => inputDir.x;}
    //------------------------------------------

    [SerializeField] private float moveSpeed, dashForce, dashDelayValue = 5;
    private bool dashInDelayTime, dashActive;

    //----------------------------------------    
    public float DashDelayValue { get => dashDelayValue; }
    public bool DashInDelayTime { get => dashInDelayTime; set => dashInDelayTime =value;} //PlayerCanvas
    public bool DashActive { get => dashActive;} //PlayerAnims
    private float timeD = 0;



    private void Start() 
    {
        playerMagicAtk = GetComponent<PlayerAtk>();
        playerMeleeAtk = GetComponent<PlayerAtk>();
    }

    private void Update() 
    {
        inputDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);

        if (Input.GetKey(KeyCode.Space) && !dashInDelayTime){
            dashInDelayTime = true;
            SetDashActive();
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

        if(dashActive) PlayerDash();
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
        timeD += Time.deltaTime;
        transform.position += new Vector3(inputDir.x * moveSpeed, 0, 0) * Time.deltaTime;
    }
    private void PlayerDash(){
        //rb2D.velocity = transform.right * dashForce;
        //rb2D.AddForce(transform.right * dashForce, ForceMode2D.Force);
        transform.Translate(Vector3.right *  dashForce * Time.deltaTime, Space.Self);
    }

    public void SetDashActive(){
        dashActive = !dashActive;
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