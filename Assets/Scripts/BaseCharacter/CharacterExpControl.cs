using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterExpControl : MonoBehaviour
{
    PlayerSkills playerSkills;
    CharacterAttributes cA;

    //ExpControl--------------------------------------

    [Space(10)]
    [Header("ExpControl")]
    [SerializeField] private int currentExp;
    [SerializeField] private int nextLevelExp;
    [SerializeField] private int level;

    //Points: Attributes Base------------------------

    [Space(10)]
    [Header("Points: Attributes Base")]
    [SerializeField] private int attributePoints;
    public int usedAttributePoints;

    //Skill----------------------------------------

    [Space(10)]
    [Header("Points: Skills")]
    [SerializeField] private int skillLevel;
    [SerializeField] private int skillPoints;
    [SerializeField] private int usedSkillPoints;


    //Properties----------------------------------
    private int expScaleValue = 125, attributesPointsScaleValue = 3;
    public int CurrentExp => currentExp;
    public int NextLevelExp => nextLevelExp;
    public int Level => level;
    public int AttributePoints { get => attributePoints; set => attributePoints = value; }
    public int UsedAttributePoints { get => usedAttributePoints; set => usedAttributePoints = value; }
    public int SkillPoints { get => skillPoints; set => skillPoints = value; }
    public int UsedSkillsPoints => usedSkillPoints; 


    private void Awake()
    {
        playerSkills = GetComponentInChildren<PlayerSkills>();
        cA = GetComponent<CharacterAttributes>();

        if (level < 1)
        {
            level = 1;
            nextLevelExp = level * expScaleValue;
            attributePoints = level * attributesPointsScaleValue - usedAttributePoints;
        }
        else
        {
            //LoadData
        }
    }

    private void Update()
    {
        CheckLevelUp();
        UsedSkills();
    }


    public void CheckLevelUp()
    {
        if (currentExp >= nextLevelExp && level  < 100){
            CharLevelUp(1);
            SkillLevelUp(1);
        }

        if(level == 100) currentExp = nextLevelExp;
    }

    private void CharLevelUp(int value)
    {
        level += value;
        currentExp -= nextLevelExp;
        nextLevelExp = level * expScaleValue;
        attributePoints = level * attributesPointsScaleValue - usedAttributePoints;

        //Recovery life and mana
        cA.RecoveryLife(cA.MaxLife - cA.Life);
        cA.RecoveryMana(cA.MaxMana - cA.Mana);
    }
    private void SkillLevelUp(int value)
    {
        if(level % 2  == 0){
            skillLevel += value;
            skillPoints = level / 2 - usedSkillPoints;
        }
    }


    //Possivel implementacao: Add exp pt a pt ao inves do valor completo
    public void GetExp(int value)
    {
        currentExp += value;
    }



    public void UsedSkills(){
        //Esse sistema sera utilizado no "reset"
        //Ira verificar a quantidade de pts de skills utilizados e devolvera para o usuario, zerando todas as skills
        //Utilizado no update apenas para testes
        usedSkillPoints = playerSkills.FloorOfHellLevel;
    }
}
