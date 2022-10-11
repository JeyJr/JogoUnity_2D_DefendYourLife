using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    public SpriteRenderer playerSkin;
    public PlayerInputs playerInputs;
    CharacterAttributes cAttributes;
    CharacterExpControl cExpControl;
    public LayerMask target;

    //SkillList-------------------------
    public List<GameObject> skillList = new();

    //FloorOfHell Attributes-----------------------
    [Header("FloorOfHell")]
    public float fohDelayHit;
    public int fohLevel, fohMaxLevel, fohManaCost = 12;

    //BladesOfWind----------------------------------
    [Header("BladesOfWind")]
    public float bowDelayHit;
    public int bowLevel, bowMaxLevel, bowManaCost;

    //BladesOfWind----------------------------------
    [Header("WaterSpikes")]
    public float wsDelayHit;
    public int wsLevel, wsMaxLevel, wsManaCost;
    
    //LifeSteal----------------------------------
    [Header("LifeSteal")]
    public float lsDuration;
    public int lsLevel, lsMaxLevel, lsManaCost;
    
    //LifeSteal----------------------------------
    [Header("Lucky")]
    public float lkDuration;
    public int lkLevel, lkMaxLevel, lkManaCost;




    private void Start()
    {
        playerInputs = GetComponentInParent<PlayerInputs>();
        cExpControl = GetComponentInParent<CharacterExpControl>();
        cAttributes = GetComponentInParent<CharacterAttributes>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="skillNum"></param>
    /// <param name="mana"></param>
    /// <param name="delayHit">Delay to cast the next hit</param>
    /// <param name="level"></param>
    /// <param name="quaternion"></param>

    #region Active Skills 
    public void SpawnSkill(float delay, int skillNum, int mana, float delayHit, int level, Vector3 pos,Quaternion quaternion)
    {
        StartCoroutine(Spawn(delay, skillNum, mana, delayHit, level, pos, quaternion));
    }
     IEnumerator Spawn(float delay, int skillNum, int mana, float delayHit, int level, Vector3 pos,Quaternion quaternion){
        yield return new WaitForSeconds(delay);
        var skill = skillList[skillNum].GetComponent<SkillBehavior>();
        cAttributes.Mana -= mana;
        skill.SetSkillValues(delayHit, cAttributes.DealDmg(false), level, target, cExpControl);
        Instantiate(skillList[skillNum], pos, quaternion);
    }
    #endregion

    #region Buffs and passive skills

    //LifeSteal -------------------------------------------------
    public void LifeSteal() => StartCoroutine(LifeStealDelay());

    IEnumerator LifeStealDelay(){
        cAttributes.Mana -= lsManaCost;
        playerSkin.color = new Color(.25f, 0, 0, 1);
        yield return new WaitForSeconds(lsDuration);

        playerSkin.color = new Color(1, 1, 1, 1);
        cAttributes.LifeSteal = false;
    }
   
   //BonusLUK----------------------------------------------------
   public void Lucky(){
        cAttributes.Mana -= lkManaCost;
        cAttributes.BonusLuck = lkLevel * 2;
        cAttributes.Luck += cAttributes.BonusLuck;
        StartCoroutine(LuckyDelay());
   }

   
   IEnumerator LuckyDelay(){
    yield return new WaitForSeconds(lkDuration);
    cAttributes.Luck -= cAttributes.BonusLuck;
    cAttributes.BonusLuck = 0;
   }
   #endregion
}
