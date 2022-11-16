using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class CharacterExpControl : MonoBehaviour
{
    CharacterAttributes cA;
    [SerializeField] private PlayerSkills playerSkill;

    //ExpControl--------------------------------------

    [Space(10)]
    [Header("ExpControl")]
    [SerializeField] private int currentExp;
    [SerializeField] private int nextLevelExp;
    [SerializeField] private int level;


    //Properties----------------------------------
    private int expScaleValue = 125;
    public int CurrentExp => currentExp;
    public int NextLevelExp => nextLevelExp;
    public int Level => level;

    //SkillExpControl------------------------------------
    [Space(10)]
    [Header("SkillExpControl")]
    [SerializeField] private int currentSkillExp;
    [SerializeField] private int nextSkillLevelExp;
    [SerializeField] private int skillLevel;

    //Properties----------------------------------
    public int CurrentSkillExp => currentSkillExp;
    public int NextSkillLevelExp => nextSkillLevelExp;
    public int SkillLevel => skillLevel;

    //Obj----------------------------------
    [SerializeField] private GameObject textLevelUp;
    [SerializeField] private Transform spawnPosition;

    private void Start()
    {
        cA = GetComponent<CharacterAttributes>();
        Load();
    }

    private void Update()
    {
        CheckLevelUp();
        CheckSkillLevelUp();
    }

    #region BaseExp
    public void CheckLevelUp()
    {
        if (currentExp >= nextLevelExp && level  < 100)
            CharLevelUp(1);

        if(level == 100) 
            currentExp = nextLevelExp;
    }    


    private void CharLevelUp(int value)
    {
        textLevelUp.GetComponent<TextMeshPro>().text = "Level Up";
        textLevelUp.GetComponent<TextMeshPro>().color = Color.yellow;
        Instantiate(textLevelUp, spawnPosition.position, Quaternion.Euler(0, 0, 0));

        level += value;
        currentExp -= nextLevelExp;
        nextLevelExp = level * expScaleValue;
        Save();

        cA.RecoveryLife(cA.MaxLife - cA.Life);
        cA.RecoveryMana(cA.MaxMana - cA.Mana);
    }
    #endregion


    #region SkillExp
    public void CheckSkillLevelUp()
    {
        if (currentSkillExp >= nextSkillLevelExp && skillLevel < 50)
            SkillLevelUp(1);

        if (skillLevel == 50)
            currentSkillExp = nextSkillLevelExp;
    }

    private void SkillLevelUp(int value)
    {
        skillLevel += value;
        currentSkillExp -= nextSkillLevelExp;
        nextSkillLevelExp = skillLevel * expScaleValue;
    }
    #endregion

    public void GetExp(int value)
    {
        currentExp += value;
        currentSkillExp += Mathf.RoundToInt(value / 2);
        Save();
    }


    void Load()
    {
        PlayerData playerData = GameData.LoadData();
        PlayerSkillsData playerSkillsData = GameData.LoadSkillsData();

        //-----------------------------
        level = playerData.level;
        currentExp = playerData.currentExp;
        nextLevelExp = playerData.nextLevelExp;

        cA.Str = playerData.str;
        cA.Inte = playerData.inte;
        cA.Vit = playerData.vit;
        cA.Luk = playerData.luk;

        //-----------------------------
        skillLevel = playerSkillsData.skillLevel;
        currentSkillExp = playerSkillsData.currentSkillExp;
        nextSkillLevelExp = playerSkillsData.nextSkillLevelExp;

        playerSkill.FloorOfHellLevel = playerSkillsData.fohLevel;
        playerSkill.WaterSpikesLevel = playerSkillsData.wsLevel;
        playerSkill.BladesOfWindLevel = playerSkillsData.bowLevel;
        playerSkill.LifeStealLevel = playerSkillsData.lsLevel;
        playerSkill.LuckyLevel = playerSkillsData.lkLevel;
        playerSkill.InvencibleLevel = playerSkillsData.iLevel;
    }

    void Save()
    {
        GameData.SaveData(level, currentExp, nextLevelExp, cA.Str, cA.Inte, cA.Vit, cA.Luk);
        GameData.SaveSkillsData(skillLevel,
            currentSkillExp,
            nextSkillLevelExp,
            playerSkill.FloorOfHellLevel,
            playerSkill.WaterSpikesLevel,
            playerSkill.BladesOfWindLevel,
            playerSkill.LifeStealLevel,
            playerSkill.LuckyLevel,
            playerSkill.InvencibleLevel);
    }
}
