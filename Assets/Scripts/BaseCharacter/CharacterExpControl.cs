using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterExpControl : MonoBehaviour
{
    //--------------------------------------ExpControl
    [SerializeField] private int currentExp, nextLevelExp, level, attributePoints, usedAttributePoints;
    private int expScaleValue = 125, attributesPointsScaleValue = 3;

    //---------------------------------------Propriedades
    public int AttributePoints { get => attributePoints; set => attributePoints = value; }
    public int UsedAttributePoints { get => usedAttributePoints; set => usedAttributePoints = value; }


    private void Awake()
    {
        InitialValueExp();
    }
    private void InitialValueExp()
    {
        //Exp
        if (level < 1)
        {
            level = 1;
            nextLevelExp = level * expScaleValue;
            attributePoints = 0;
        }
        else
        {
            nextLevelExp = level * expScaleValue;
            attributePoints = level * attributesPointsScaleValue - usedAttributePoints;
        }
    }


    private void Update()
    {
        CheckLevelUp();
    }
    //Chamar metodo sempre que derrotar um inimigo? 
    private void CheckLevelUp()
    {
        if (currentExp >= nextLevelExp && level  < 99)
        {
            level++;
            currentExp -= nextLevelExp;
            nextLevelExp = level * expScaleValue;
            
        }

        if(usedAttributePoints > 0)
        {
            attributePoints = level * attributesPointsScaleValue - usedAttributePoints;
        }
        else
        {
            attributePoints = level * attributesPointsScaleValue;
        }
    }

    public void UsingAttributePoints()
    {
        if (attributePoints > 0)
        {
            attributePoints--;
            usedAttributePoints++;
        }
    }

    public void ReturningAttributePoints()
    {
        if (usedAttributePoints > 0)
        {
            attributePoints++;
            usedAttributePoints--;
        }
    }


    //Possivel implementacao: Add exp pt a pt ao inves do valor completo
    public void GetExp(int value)
    {
        currentExp += value;
        //CheckLevelUp();
    }
}
