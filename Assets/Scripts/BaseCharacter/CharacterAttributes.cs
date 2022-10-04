using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterAttributes : MonoBehaviour
{
    //---------------------------------------AttributesBase
    [SerializeField] private int strength, intelligence, vitality, luck;

    //---------------------------------------Status
    [SerializeField] private int physicalAtkPower, magicAtkPower, life, maxLife, criticalRate;
    public bool criticalDmg;


    //---------------------------------------Propriedades AttributeBase
    public int Strength { get => strength; set => strength = value; }
    public int Intelligence { get => intelligence; set => intelligence = value; }
    public int Vitality { get => vitality; set => vitality = value; }
    public int Luck { get => luck; set => luck = value; }

    //---------------------------------------Propriedades Status
    public int PhysicalAtkPower { get => physicalAtkPower; }
    public int MagicAtkPower { get => magicAtkPower;}
    public int MaxLife { get => maxLife;}
    public int CriticalRate { get => criticalRate;}

    //---------------------------------------Objs
    [SerializeField] private GameObject textDmg;
    [SerializeField] private Transform spawnPosition;

    private void Awake()
    {
        AttributeValues();

        if (strength < 1) strength = 1;
        if (vitality < 1) vitality = 1;
        if (intelligence < 1) intelligence = 1;
        if (luck < 1) luck = 1;

        life = vitality * 50;
    }
    private void Update()
    {
        AttributeValues();
    }
    private void AttributeValues()
    {
        physicalAtkPower = strength * 3; //atk = str * 3 
        magicAtkPower = Mathf.RoundToInt(Intelligence * 1.5f); //magic = int * 1.5f
        maxLife = vitality * 50; //maxlife = vit * 50 
        criticalRate = Mathf.RoundToInt(luck / 2); 
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
        
        if(this.gameObject.layer == 6) //Player
        {
            if (critical)
                TextColor(0, 0, 1, 1, true);
            else
                TextColor(0, 0, 0.5f, 1, false);
        }
        else //Other 
        {
            if (critical)
                TextColor(1, 0, 0, 1, true);
            else
                TextColor(1, 1, 1, 1, false);
        }
            

        textDmg.GetComponent<TextMeshPro>().text = text;
        Instantiate(textDmg, spawnPosition.position, Quaternion.Euler(0, 0, 0));
    }

    private void TextColor(float r, float g, float b, float a, bool critical)
    {
        textDmg.GetComponent<TextMeshPro>().color = new Color(r, g, b, a);
        textDmg.GetComponent<TextDmgBehavior>().bgCritical.SetActive(critical);
    }
}
