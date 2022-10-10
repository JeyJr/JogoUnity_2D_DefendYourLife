using System.Collections;
using System;
using UnityEngine;

public class SkillBehavior : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] sprites;
    [SerializeField] private LayerMask target;
    public Transform ground;
    public CharacterExpControl gainExp;

    public int dmg, maxHit;
    public float delayHit, delayNextHit = .5f;

    //------------------------------------------
    public bool bladeOfWindActive;

    /// <summary>
    /// All targets within the spell's area take damage per second.
    /// </summary>
    /// <param name="dmg"> Set dmg per hit</param>
    /// <param name="maxHit"> Set amount hits dmg in every target  </param>
    /// <param name="target"> Set target take dmg  </param>
    public void SetSkillValues(float delayHit, int dmg, int skillLevel, LayerMask target)
    {
        this.delayHit = delayHit;
        this.dmg = dmg;
        this.maxHit = skillLevel;
        this.target = target;
    }

    /// <param name="gainExp"> Player receive exp after kill an enemy,  </param>
    public void SetSkillValues(float delayHit, int dmg, int skillLevel, LayerMask target, CharacterExpControl gainExp)
    {
        this.delayHit = delayHit;
        this.dmg = dmg;
        this.maxHit = skillLevel;
        this.target = target;
        this.gainExp = gainExp;
    }

    private void Awake()
    {
        transform.position = new Vector3(transform.position.x, ground.position.y + 1, 4);
        
        StartCoroutine(FadeIn());

        if(bladeOfWindActive){
            StartCoroutine(FadeOut(5));
        }
        else{
            StartCoroutine(HitAreaWithDelay());
        }

    }


    IEnumerator HitAreaWithDelay()
    {
        while (maxHit > 0)
        {

            RaycastHit2D[] hit = Physics2D.BoxCastAll(transform.position, transform.localScale, 0f, Vector2.zero, 0, target);

            if (hit != null)
            {
                for (int i = 0; i < hit.Length; i++)
                {
                    //Debug.Log($"Index: {i} / Name: {hit[i].collider.name}");
                    if (gainExp != null)
                        hit[i].collider.GetComponent<CharacterAttributes>().TakeDMG(dmg, false, gainExp);
                    else
                        hit[i].collider.GetComponent<CharacterAttributes>().TakeDMG(dmg, false);

                    yield return new WaitForSeconds(delayHit);
                }
            }
            yield return new WaitForSeconds(delayNextHit);
            maxHit--;

            if (maxHit <= 0){
                StopCoroutine(HitAreaWithDelay());
                StartCoroutine(FadeOut(0));
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(bladeOfWindActive){
            if(other.gameObject.layer == 7){
                if (gainExp != null)
                    other.GetComponent<CharacterAttributes>().TakeDMG(dmg, false, gainExp);
                else
                    other.GetComponent<CharacterAttributes>().TakeDMG(dmg, false);
            }
        }
    }


    IEnumerator FadeOut(float initialDelay)
    {
        yield return new WaitForSeconds(initialDelay);

        for (float i = 1; i > -0.2f; i -= .1f)
        {
            sprites[0].color = new Color(1, 1, 1, i);
            sprites[1].color = new Color(1, 1, 1, i);
            yield return new WaitForSeconds(.01f);
        }

        Destroy(this.gameObject);
    }
    IEnumerator FadeIn()
    {
        for (float i = 0; i < 1.2f; i += .1f)
        {
            sprites[0].color = new Color(1, 1, 1, i);
            sprites[1].color = new Color(1, 1, 1, i);
            yield return new WaitForSeconds(.01f);
        }
    }
}
