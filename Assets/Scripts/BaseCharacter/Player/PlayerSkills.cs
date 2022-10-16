using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    public SpriteRenderer playerSkin;
    public PlayerInputs playerInputs;
    public CharacterAttributes cA;
    public CharacterExpControl cE;
    public LayerMask target;

    //SkillList-------------------------
    public List<GameObject> skillList = new();

    private int skillsMaxLevel = 10;
    public int SkillsMaxLevel { get => skillsMaxLevel;}

    //FloorOfHell Attributes-----------------------
    [Header("FloorOfHell")]
    private int fohLevel;
    public int FloorOfHellLevel { get => fohLevel; set => fohLevel = value; }
    public int FloorOfHellManaCost  => 10 * fohLevel;
    public bool FloorOfHellCountdown { get; set; }
    private float fohDelayToUseAgain = 5;

    //WaterSpikes----------------------------------
    [Header("WaterSpikes")]
    private int wsLevel;
    public int WaterSpikesLevel { get => wsLevel; set => wsLevel = value; }
    public int WaterSpikesManaCost => 5; //por spike
    public bool WaterSpikesCountdown { get; set; }
    private float wsDelayToUseAgain = 5;

    //BladesOfWind----------------------------------
    [Header("BladesOfWind")]
    private int bowLevel;
    public int BladesOfWindLevel { get => bowLevel; set => bowLevel = value; }
    public int BladesOfWindManaCost => 6 * bowLevel;
    public bool BladesOfWindCountdown { get; set; }
    private float bowDelayToUseAgain = 5;

    //LifeSteal----------------------------------
    [Header("LifeSteal")]
    private int lsLevel;
    public float LifeStealDuration => lsLevel * 2;
    public int LifeStealLevel { get => lsLevel; set => lsLevel = value; }
    public int LifeStealManaCost => 3 * lsLevel;
    public bool LifeStealCountdown { get; set; }
    private float lsDelayToUseAgain = 30;
    //Lucky----------------------------------
    [Header("Lucky")]
    private int lkLevel;
    public float LuckyDuration => lkLevel * 2;
    public int LuckyLevel { get => lkLevel; set=> lkLevel = value; }
    public int LuckyManaCost => 3 * lkLevel;
    public bool LuckyCountdown { get; set; }
    private float lkDelayToUseAgain = 30;

    //Invencible----------------------------------
    [Header("Invencible")]
    private int iLevel;
    public int InvencibleDuration => iLevel * 2;
    public int InvencibleLevel { get => iLevel; set => iLevel = value; }
    public int InvencibleManaCost => iLevel * 30;
    public bool InvencibleCountdown { get; set; }
    private float iDelayToUseAgain = 60;

    #region Active Skills 
    public void SpawnSkill(float delay, int skillNum, int mana, float delayHit, int level, Vector3 pos,Quaternion quaternion)
    {
        StartCoroutine(Spawn(delay, skillNum, mana, delayHit, level, pos, quaternion));
    }
    IEnumerator Spawn(float delay, int skillNum, int mana, float delayHit, int level, Vector3 pos,Quaternion quaternion){
        yield return new WaitForSeconds(delay);
        var skill = skillList[skillNum].GetComponent<SkillBehavior>();
        cA.Mana -= mana;
        skill.SetSkillValues(delayHit, cA.DealDmg(false), level, target, cE);
        Instantiate(skillList[skillNum], pos, quaternion);
    }
    #endregion

    #region Buffs and passive skills

    //LifeSteal -------------------------------------------------
    public void LifeSteal() => StartCoroutine(LifeStealDelay());
    IEnumerator LifeStealDelay(){
        yield return new WaitForSeconds(.6f);
        cA.Mana -= LifeStealManaCost;

        playerSkin.color = new Color(1f, 1f, .3f, 1);
        yield return new WaitForSeconds(LifeStealDuration);

        playerSkin.color = new Color(1, 1, 1, 1);
        cA.LifeSteal = false;
    }
   
    //BonusLUK----------------------------------------------------
    public void Lucky(){
        cA.Mana -= LuckyManaCost;

        cA.BonusLuck = lkLevel * 2;
        cA.Luck += cA.BonusLuck;
        StartCoroutine(LuckyDelay());
   }
    IEnumerator LuckyDelay(){
        yield return new WaitForSeconds(.6f);
        playerSkin.color = new Color(1f, .3f, 1f, 1);
        yield return new WaitForSeconds(LuckyDuration);
        playerSkin.color = new Color(1, 1, 1, 1);
        cA.Luck -= cA.BonusLuck;
        cA.BonusLuck = 0;
   }
    //Invencible -------------------------------------------------
    public void Invencible() => StartCoroutine(InvencibleDelay());
    IEnumerator InvencibleDelay(){
        yield return new WaitForSeconds(.6f);
        cA.Mana -= InvencibleManaCost;

        playerSkin.color = new Color(.3f, 1f, 1f, 1);
        yield return new WaitForSeconds(InvencibleDuration);

        playerSkin.color = new Color(1, 1, 1, 1);
        cA.Invencible = false;
    }
    #endregion

    //CD Skills---------------------------------

    public void StartCountdown(string skill) => StartCoroutine(skill);
    IEnumerator FloorOfHellCD() {

        FloorOfHellCountdown = true;
        yield return new WaitForSeconds(fohDelayToUseAgain);
        FloorOfHellCountdown = false;
    }    
    IEnumerator WaterSpikesCD() {
        WaterSpikesCountdown = true;
        yield return new WaitForSeconds(wsDelayToUseAgain);
        WaterSpikesCountdown = false;
    }
    IEnumerator BladesOfWindCD() {
        BladesOfWindCountdown = true;
        yield return new WaitForSeconds(bowDelayToUseAgain);
        BladesOfWindCountdown = false;
    }
    IEnumerator LifeStealCD()
    {
        LifeStealCountdown = true;
        yield return new WaitForSeconds(lsDelayToUseAgain);
        LifeStealCountdown = false;
    }    
    IEnumerator LuckyCD()
    {
        LuckyCountdown = true;
        yield return new WaitForSeconds(lkDelayToUseAgain);
        LuckyCountdown = false;
    }    
    IEnumerator InvencibleCD()
    {
        InvencibleCountdown = true;
        yield return new WaitForSeconds(iDelayToUseAgain);
        InvencibleCountdown = false;
    }
}
