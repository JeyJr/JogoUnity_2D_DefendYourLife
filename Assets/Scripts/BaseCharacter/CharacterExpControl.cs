using System.Collections;
using System.Collections.Generic;
using System.IO;
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


    private void Start()
    {
        cA = GetComponent<CharacterAttributes>();
        Load();
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
        Save();

        cA.RecoveryLife(cA.MaxLife - cA.Life);
        cA.RecoveryMana(cA.MaxMana - cA.Mana);
    }

    public void GetExp(int value)
    {
        currentExp += value;
        Save();
    }


    void Load()
    {
        PlayerData playerData = GameData.LoadData();

        level = playerData.level;
        currentExp = playerData.currentExp;
        nextLevelExp = playerData.nextLevelExp;
    }

    void Save()
    {
        GameData.SaveLevel(level, currentExp, nextLevelExp);
    }
}
