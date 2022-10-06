using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//sprite: free-game-assets.itch.io/fire-pixel-art-animation-sprites
public class FloorOfHellBehavior : MonoBehaviour
{
    public CharacterExpControl gainExp;
    [SerializeField] private LayerMask target;
    [SerializeField] private int dmg, maxHit;

    //----------------------------------------------
    private float delayHit = .05f, delayNextHit = .5f;


    [SerializeField] private SpriteRenderer[] sprites;
    public Transform ground;

    /// <summary>
    /// All targets within the spell's area take damage per second.
    /// </summary>
    /// <param name="dmg"> Set dmg per hit</param>
    /// <param name="maxHit"> Set amount hits dmg in every target  </param>
    /// <param name="target"> Set target take dmg  </param>
    public void SetSkillValues(int dmg, int skillLevel, LayerMask target)
    {
        this.dmg = dmg;
        this.maxHit = skillLevel;
        this.target = target;
    }
    public void SetSkillValues(int dmg, int skillLevel, LayerMask target, CharacterExpControl gainExp)
    {
        this.dmg = dmg;
        this.maxHit = skillLevel;
        this.target = target;
        this.gainExp = gainExp;
    }

    private void Awake()
    {
        transform.position = new Vector3(transform.position.x, ground.position.y + 1, 4);
        StartCoroutine(nameof(HitArea));
        StartCoroutine(FadeIn());
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
                    if(gainExp != null)
                        hit[i].collider.GetComponent<CharacterAttributes>().TakeDMG(dmg, false, gainExp);
                    else
                        hit[i].collider.GetComponent<CharacterAttributes>().TakeDMG(dmg, false);

                    yield return new WaitForSeconds(delayHit);
                }
            }
            yield return new WaitForSeconds(delayNextHit);
            maxHit--;

            if (maxHit <= 0) StartCoroutine(FadeOut());
        }
    }
    IEnumerator FadeOut(){
        for(float i = 1; i > -0.2f; i -= .1f){
            sprites[0].color = new Color(1,1,1,i);
            sprites[1].color = new Color(1,1,1,i);
            yield return new WaitForSeconds(.01f);
        }

        Destroy(this.gameObject);
    }
    IEnumerator FadeIn(){
        for(float i = 0; i < 1.2f; i += .1f){
            sprites[0].color = new Color(1,1,1,i);
            sprites[1].color = new Color(1,1,1,i);
            yield return new WaitForSeconds(.01f);
        }
    }
}
