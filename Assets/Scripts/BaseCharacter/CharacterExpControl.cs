using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterExpControl : MonoBehaviour
{
    PlayerSkills playerSkills;

    //--------------------------------------ExpControl
    [SerializeField] private int currentExp, nextLevelExp, level, attributePoints;
    private int expScaleValue = 125, attributesPointsScaleValue = 3;
    public int usedAttributePoints;

    //--------------------------------------Skill
    [SerializeField] private int skillLevel, skillPoints, usedSkillPoints;
    public int SkillPoints { get => skillPoints; set => skillPoints = value; }
    public int UsedSkillsPoints{ get => usedSkillPoints;} 

    //---------------------------------------Propriedades
    public int AttributePoints { get => attributePoints; set => attributePoints = value; }
    public int UsedAttributePoints { get => usedAttributePoints; set => usedAttributePoints = value; }


    private void Awake()
    {
        playerSkills = GetComponentInChildren<PlayerSkills>();

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
