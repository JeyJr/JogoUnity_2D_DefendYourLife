using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterExpControl : MonoBehaviour
{
    CharacterAttributes cA;

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


    private void Awake()
    {
        cA = GetComponent<CharacterAttributes>();

        level = PlayerPrefs.GetInt("level");
        currentExp = PlayerPrefs.GetInt("currentExp");
        nextLevelExp = PlayerPrefs.GetInt("nextLevelExp");
    }

    private void Update()
    {
        CheckLevelUp();
    }


    public void CheckLevelUp()
    {
        if (currentExp >= nextLevelExp && level  < 100)
            CharLevelUp(1);

        if(level == 100) 
            currentExp = nextLevelExp;
    }

    private void CharLevelUp(int value)
    {
        level += value;
        currentExp -= nextLevelExp;
        nextLevelExp = level * expScaleValue;

        PlayerPrefs.SetInt("level", level);

        //Recovery life and mana
        cA.RecoveryLife(cA.MaxLife - cA.Life);
        cA.RecoveryMana(cA.MaxMana - cA.Mana);
    }
    //private void SkillLevelUp(int value)
    //{
    //    if(level % 2  == 0){
    //        skillLevel += value;
    //        skillPoints = level / 2 - usedSkillPoints;

    //        PlayerPrefs.SetInt("skillLevel", skillLevel);
    //        PlayerPrefs.SetInt("skillPoints", skillPoints);
    //    }
    //}

    public void GetExp(int value)
    {
        currentExp += value;
        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.SetInt("currentExp", currentExp);
        PlayerPrefs.SetInt("nextLevelExp", nextLevelExp);
    }
}
