using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//sprite: free-game-assets.itch.io/fire-pixel-art-animation-sprites
public class FloorOfHell : MonoBehaviour
{
    private float delayHit = .05f;

    [SerializeField] private int dmg;
    [SerializeField] private float delayNextHit;
    [SerializeField] private int maxHit;
    [SerializeField] private LayerMask target;

    public Transform ground;

    /// <summary>
    /// All targets within the spell's area take damage per second.
    /// </summary>
    /// <param name="dmg"> Set dmg per hit</param>
    /// <param name="delayNexHitSequence">Set delay per hit while skill active</param>
    /// <param name="maxHit"> Define a quantidade de hits in every target  </param>
    /// <param name="target"> Set target take dmg  </param>
    public void SetSkillValues(int dmg, float delayNexHitSequence, int maxHit, LayerMask target)
    {
        this.dmg = dmg;
        this.delayNextHit = delayNexHitSequence;
        this.maxHit = maxHit;
        this.target = target;
    }


    private void Awake()
    {
        transform.position = new Vector3(transform.position.x, ground.position.y + 1, 4);
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
                    //Debug.Log($"Index: {i} / Name: {hit[i].collider.name}");
                    hit[i].collider.GetComponent<CharacterAttributes>().TakeDMG(dmg, false);
                    yield return new WaitForSeconds(delayHit);
                }
            }
            yield return new WaitForSeconds(delayNextHit);
            maxHit--;

            if (maxHit <= 0) Destroy(this.gameObject);
        }
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
