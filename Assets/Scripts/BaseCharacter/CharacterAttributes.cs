using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterAttributes : MonoBehaviour
{
    //---------------------------------------AttributesBase
    [SerializeField] private int strength, vitality, intelligence, luck;
    [SerializeField] private int life, maxLife, physicalAtkPower, magicAtkPower, criticalRate;
    public bool criticalDmg;
    //---------------------------------------Exp Control
    [SerializeField] private int currentExp, nextLevelExp, level, attributePoints, usedAttributePoints;
    private int expScaleValue = 125, attributesPointsScaleValue = 3;

    [SerializeField] private int dropExp;
    //---------------------------------------Objs
    [SerializeField] private GameObject textDmg;
    [SerializeField] private Transform spawnPosition;

   
    private void Awake()
    {
        InitialAttributesValue();
        CheckLevelUp();
    }

    private void InitialAttributesValue()
    {
        //Exp
        if (level < 1)
        {
            level = 1;
            nextLevelExp = level * expScaleValue;
            attributePoints = level * attributesPointsScaleValue;
        }
        else
        {
            nextLevelExp = level * expScaleValue;
            attributePoints = level * attributesPointsScaleValue - usedAttributePoints;
        }


        physicalAtkPower = strength * 3; //atk = str * 3 
        maxLife = vitality * 50; //maxlife = vit * 50 
        life = maxLife;
        magicAtkPower = Mathf.RoundToInt(intelligence * 1.5f); //magic = int * 1.5f
        criticalRate = Mathf.RoundToInt(luck / 2); 
    }

    private void Update()
    {
        CheckLevelUp();
    }

    private void CheckLevelUp()
    {
        nextLevelExp = level * expScaleValue;
        attributePoints = attributesPointsScaleValue * level;
        

        if (currentExp >= nextLevelExp)
        {
            level++;
            currentExp -= nextLevelExp;
            nextLevelExp = level * expScaleValue;
            attributePoints = attributesPointsScaleValue * level;
        }

        if (usedAttributePoints > 0) attributePoints -= usedAttributePoints;
        
        if(attributePoints < 0)
        {
            attributePoints++;
            usedAttributePoints--;
        }
        
    }

    public void UsingAttributePoints()
    {
        if(attributePoints > 0)
        {
            attributePoints--;
            usedAttributePoints++;
        }
    }

    public void ReturningAttributePoints()
    {
        if(usedAttributePoints > 0)
        {
            attributePoints++;
            usedAttributePoints--;
        }
    }


    public void GetExp(int value)
    {
        currentExp += value;
        //CheckLevelUp();
    }
    public int DropExp()
    {
        return dropExp;
    }

    public void TakeDMG(int dmg, bool critical)
    {
        SpawnText(dmg.ToString(), critical);
        life -= dmg;
    }

    public int DealDmg()
    {
        int dmg = Mathf.RoundToInt(Random.Range(physicalAtkPower / 1.2f, physicalAtkPower / 0.9f));
        float critChance = Random.Range(1, 100);

        if (critChance <= criticalRate)
        {
            criticalDmg = true;
            dmg *= 2;
        }
        else criticalDmg = false;
            
        return  dmg;
    }

    private void SpawnText(string text, bool critical)
    {
        if (critical) 
            textDmg.GetComponent<TextMeshPro>().color = Color.red;
        else 
            textDmg.GetComponent<TextMeshPro>().color = Color.white;

        textDmg.GetComponent<TextMeshPro>().text = text;
        Instantiate(textDmg, spawnPosition.position, Quaternion.Euler(0, 0, 0));
    }
}
