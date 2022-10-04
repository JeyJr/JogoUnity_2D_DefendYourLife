using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAtk : MonoBehaviour
{
    [SerializeField] private CharacterAttributes characterAttributes;
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private Animator anim;

    [SerializeField] private Transform startPosition;
    [SerializeField] private RaycastHit2D[] hit2DAtk;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private float atkRange;

    private bool isAtk;

    private void Update()
    {
        if (!playerMove.isJump && !isAtk && Input.GetMouseButtonDown(0)) SetIsAtk();

        Anims();
    }

    public void AtkArea() {
        hit2DAtk = Physics2D.RaycastAll(startPosition.position, startPosition.right, atkRange, enemyMask);
        Debug.DrawRay(startPosition.position, startPosition.right * atkRange, Color.red);
        if (hit2DAtk != null)
            for (int i = 0; i < hit2DAtk.Length; i++)
            {
                //fazer critico
                hit2DAtk[i].collider.GetComponent<CharacterAttributes>().TakeDMG(characterAttributes.DealDmg());
            }
    }

    //end anim atk, set isAtk value
    public void SetIsAtk()
    {
        isAtk = !isAtk;
    }
    public void Anims()
    {
        anim.SetBool("isAtk", isAtk);
    }
    private void OnDrawGizmos()
    {
        //Debug.DrawRay(startPosition.position, startPosition.right * atkRange, Color.red);
    }
}
