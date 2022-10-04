using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAtk : MonoBehaviour
{
    [SerializeField] private CharacterAttributes characterAttributes;

    [SerializeField] private Transform startPosition;
    [SerializeField] private RaycastHit2D[] hit2DAtk;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private float atkRange;

    private bool isAtk;
    public bool IsAtk{ get => isAtk;}

    private void Update()
    {
        if (!isAtk && Input.GetMouseButtonDown(0)) SetIsAtk();
        //Anims();
    }

    public void AtkArea() {
        hit2DAtk = Physics2D.RaycastAll(startPosition.position, startPosition.right, atkRange, enemyMask);
        
        if (hit2DAtk != null)
            for (int i = 0; i < hit2DAtk.Length; i++)
            {
                //fazer critico
                hit2DAtk[i].collider.GetComponent<CharacterAttributes>().TakeDMG(characterAttributes.DealDmg(), characterAttributes.criticalDmg);
            }
    }

    //end anim atk, set isAtk value
    public void SetIsAtk()
    {
        isAtk = !isAtk;
    }

    private void OnDrawGizmos()
    {
        //Debug.DrawRay(startPosition.position, startPosition.right * atkRange, Color.red);
    }
}
