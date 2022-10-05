using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicOne : MonoBehaviour
{
    public int DMG { get; set; }
    public float delayNexHitSequence = 1f;
    public float delayHit = .05f;
    public int maxHit = 10;
    public LayerMask target;

    private void Awake()
    {
        StartCoroutine(nameof(HitArea));
    }
  
    IEnumerator HitArea()
    {
        while (maxHit > 0)
        {
            RaycastHit2D[] hit = Physics2D.BoxCastAll(transform.position, transform.localScale, 0f, Vector2.zero, 0, target);
            
            if(hit != null)
            {
                for (int i = 0; i < hit.Length; i++)
                {
                    Debug.Log($"Index: {i} / Name: {hit[i].collider.name}");
                    hit[i].collider.GetComponent<CharacterAttributes>().TakeDMG(10, false);
                    yield return new WaitForSeconds(delayHit);
                }
            }
            yield return new WaitForSeconds(delayNexHitSequence);
            maxHit--;
        }

        if (maxHit <= 0) Destroy(this.gameObject);
    }

    //private void HitSingleTargetInArea()
    //{
    //    RaycastHit2D hit = Physics2D.BoxCast(transform.position, transform.localScale, 0f, Vector2.zero);
    //    if(hit.collider != null && hit.collider.gameObject.layer == targetMask)
    //    {
    //        hit.collider.GetComponent<CharacterAttributes>().TakeDMG(10, false);
    //    }
    //}
}
