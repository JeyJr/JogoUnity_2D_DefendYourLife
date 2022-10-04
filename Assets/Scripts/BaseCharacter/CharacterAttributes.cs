using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterAttributes : MonoBehaviour
{
    [SerializeField] private int strength, vitality, intelligence, luck;

    [SerializeField] private int life, maxLife, atkPower, criticalRate;
    public bool criticalDmg;


    [SerializeField] private GameObject textDmg;
    [SerializeField] private Transform spawnPosition;


    private void Awake()
    {
        VitalityBase();
        StrengthBase();
        LuckBase();
    }
    void StrengthBase()
    {
        atkPower = strength * 3;
    }

    void VitalityBase()
    {
        maxLife = 100 + (vitality * 25);
        life = maxLife;
    }

    void LuckBase()
    {
        //Every 3pts in luck: atkPower+1, criticalRate+1
        if(luck % 3 == 0)
        {
            criticalRate = luck / 3;
        }
    }

    public void TakeDMG(int dmg, bool critical)
    {
        SpawnText(dmg.ToString(), critical);
        life -= dmg;
    }

    public int DealDmg()
    {
        int dmg = (int)Random.Range(atkPower / 1.2f, atkPower / 0.9f);

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
        if (critical) textDmg.GetComponent<TextMeshPro>().color = Color.red;
        else textDmg.GetComponent<TextMeshPro>().color = Color.white;

        textDmg.GetComponent<TextMeshPro>().text = text;
        Instantiate(textDmg, spawnPosition.position, Quaternion.Euler(0, 0, 0));
    }
}
