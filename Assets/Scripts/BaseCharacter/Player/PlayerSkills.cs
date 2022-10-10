using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
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



    private void Start()
    {
        playerInputs = GetComponentInParent<PlayerInputs>();
        cExpControl = GetComponentInParent<CharacterExpControl>();
        cAttributes = GetComponentInParent<CharacterAttributes>();
    }

    //public void SpawnFloorOfHell()
    //{
    //    var skill = skillList[0].GetComponent<SkillBehavior>();
    //    fohManaCost = 12 * fohLevel;

    //    if (cAttributes.Mana > fohManaCost)
    //    {
    //        cAttributes.Mana -= fohManaCost;

    //        skill.SetSkillValues(fohDelayHit, cAttributes.DealDmg(false), fohLevel, target, cExpControl);
    //        Instantiate(skillList[0], transform.position, Quaternion.identity);
    //    }
    //    else
    //    {
    //        Debug.Log("Sem mana!");
    //    }
    //}   
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="skillNum"></param>
    /// <param name="mana"></param>
    /// <param name="delayHit">Delay to cast the next hit</param>
    /// <param name="level"></param>
    /// <param name="quaternion"></param>
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

}
