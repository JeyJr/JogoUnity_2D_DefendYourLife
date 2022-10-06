using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterExpControl : MonoBehaviour
{
    CharacterAttributes c;
    //--------------------------------------ExpControl
    [SerializeField] private int currentExp, nextLevelExp, level, attributePoints;
    private int expScaleValue = 125, attributesPointsScaleValue = 3;
    public int usedAttributePoints;

    //--------------------------------------Skill
    [SerializeField] private int skillLevel, skillPoints, usedSkillPoints;

    //---------------------------------------Propriedades
    public int AttributePoints { get => attributePoints; set => attributePoints = value; }
    public int UsedAttributePoints { get => usedAttributePoints; set => usedAttributePoints = value; }


    private void Awake()
    {
        c = GetComponent<CharacterAttributes>();

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
    }


    private void CheckLevelUp()
    {
        if (currentExp >= nextLevelExp && level  < 99)
            CharLevelUp(1);
    }

    private void CharLevelUp(int value)
    {
        level += value;
        currentExp -= nextLevelExp;
        nextLevelExp = level * expScaleValue;
        attributePoints = level * attributesPointsScaleValue - usedAttributePoints;
    }


    private void SkillLevelUp(int value)
    {
        skillLevel += value;

    }


    //Possivel implementacao: Add exp pt a pt ao inves do valor completo
    public void GetExp(int value)
    {
        currentExp += value;
        //CheckLevelUp();
    }
}
