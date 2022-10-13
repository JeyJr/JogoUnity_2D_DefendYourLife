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

    private int skillsMaxLevel = 10;
    public int SkillsMaxLevel { get => skillsMaxLevel;}

    //FloorOfHell Attributes-----------------------
    [Header("FloorOfHell")]
    private int fohLevel, fohManaCost = 12;
    public int FloorOfHellLevel { get => fohLevel; set => fohLevel = value; }
    public int FloorOfHellManaCost  => (fohManaCost * fohLevel)/2;

    //BladesOfWind----------------------------------
    [Header("BladesOfWind")]
    private int bowLevel, bowManaCost = 5;
    public int BladesOfWindLevel { get => bowLevel; set => bowLevel = value; }
    public int BladesOfWindManaCost => bowManaCost * bowLevel;

    //BladesOfWind----------------------------------
    [Header("WaterSpikes")]
    private int wsLevel, wsManaCost = 15;
    public int WaterSpikesLevel { get => wsLevel; set => wsLevel = value; }
    public int WaterSpikesManaCost => wsManaCost; //por spike
    
    //LifeSteal----------------------------------
    [Header("LifeSteal")]
    private int lsLevel, lsManaCost = 3;
    public float LifeStealDuration => lsLevel * 2;
    public int LifeStealLevel { get => lsLevel; set => lsLevel = value; }
    public int LifeStealManaCost => lsManaCost * lsLevel;
    
    //Lucky----------------------------------
    [Header("Lucky")]
    private int lkLevel, lkManaCost = 3;
    public float LuckyDuration => lkLevel * 2;
    public int LuckyLevel { get => lkLevel; set=> lkLevel = value; }
    public int LuckyManaCost => lkManaCost * lkLevel;

    
    //Invencible----------------------------------
    [Header("Invencible")]
    private int iLevel, iManaCost;
    public int InvencibleDuration => iLevel * 2;
    public int InvencibleLevel { get => iLevel; set => iLevel = value; }
    public int InvencibleManaCost => iManaCost = iLevel * 30;


    private void Start()
    {
        playerInputs = GetComponentInParent<PlayerInputs>();
        cExpControl = GetComponentInParent<CharacterExpControl>();
        cAttributes = GetComponentInParent<CharacterAttributes>();
    }

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
        cAttributes.Mana -= LifeStealManaCost;

        playerSkin.color = new Color(1f, 1f, .3f, 1);
        yield return new WaitForSeconds(LifeStealDuration);

        playerSkin.color = new Color(1, 1, 1, 1);
        cAttributes.LifeSteal = false;
    }
   
   //BonusLUK----------------------------------------------------
   public void Lucky(){
        cAttributes.Mana -= LuckyManaCost;

        cAttributes.BonusLuck = lkLevel * 2;
        cAttributes.Luck += cAttributes.BonusLuck;
        StartCoroutine(LuckyDelay());
   }
   IEnumerator LuckyDelay(){
    playerSkin.color = new Color(1f, .3f, 1f, 1);
    yield return new WaitForSeconds(LuckyDuration);
    playerSkin.color = new Color(1, 1, 1, 1);
    cAttributes.Luck -= cAttributes.BonusLuck;
    cAttributes.BonusLuck = 0;
   }


    //Invencible -------------------------------------------------
    public void Invencible() => StartCoroutine(InvencibleDelay());
    IEnumerator InvencibleDelay(){
        cAttributes.Mana -= InvencibleManaCost;

        playerSkin.color = new Color(.3f, 1f, 1f, 1);
        yield return new WaitForSeconds(InvencibleDuration);

        playerSkin.color = new Color(1, 1, 1, 1);
        cAttributes.Invencible = false;
    }
   #endregion
}
