using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterAttributes : MonoBehaviour
{
    #region Attributes

    //----------------------------------------------
    [Space(10)]
    [Header("Attributes Base")]
    [SerializeField] private int strength;
    [SerializeField] private int intelligence;
    [SerializeField] private int vitality;
    [SerializeField] private int luck;
 
    //----------------------------------------------
    [Space(10)]
    [Header("Status")]

    [SerializeField] private int physicalAtkPower;
    [SerializeField] private int magicAtkPower;
    [SerializeField] private int life, maxLife;
    [SerializeField] private int mana, maxMana;
    [SerializeField] private int criticalRate;
    public bool criticalDmg;
    int dmg;

    //Properties: Attribute Base-------------------------------------

    public int Strength { get => strength; set => strength = value; }
    public int Intelligence { get => intelligence; set => intelligence = value; }
    public int Vitality { get => vitality; set => vitality = value; }
    public int Luck { get => luck; set => luck = value; }
 
     //Properties: Stats----------------------------------------------

    public int PhysicalAtkPower { get => physicalAtkPower; }
    public int MagicAtkPower { get => magicAtkPower;}
    public int Life { get => life;}
    public int MaxLife { get => maxLife;}
    public int Mana {get => mana; set => mana = value; }
    public int MaxMana {get => maxMana;}
    public int CriticalRate { get => criticalRate;}
    
    //----------------------------------------------
    [Space(10)]
    [Header("Behaviors, Bonus and Drops")]
    [SerializeField] private int expToDrop;
    public int BonusLuck { get; set; }
    public bool Dead { get; private set;}
    public bool LifeSteal {get; set;}
    public bool Invencible {get; set;}

    //----------------------------------------------
    [Header("Objs")]
    [SerializeField] private GameObject textDmg;
    [SerializeField] private Transform spawnPosition;

    #endregion

    private void Start()
    {
        if (strength < 1) strength = 1;
        if (vitality < 1) vitality = 1;
        if (intelligence < 1) intelligence = 1;
        if (luck < 1) luck = 1;

        life = vitality * 50;
        mana = Mathf.RoundToInt(intelligence * 4.5f) + 50;
    }

    private void Update(){
        physicalAtkPower = Mathf.RoundToInt(strength * 1.5f); //atk = str * 1.5f 
        magicAtkPower = Mathf.RoundToInt(Intelligence * 3.5f); //magic = int * 3.5f
        maxLife = vitality * 50; //maxlife = vit * 50 
        maxMana = Mathf.RoundToInt(intelligence * 4.5f) + 50;
        criticalRate = Mathf.RoundToInt(luck / 2);

        if(life > maxLife) life = maxLife;
        if(mana > maxMana) mana = maxMana;
    }

    public void TakeDMG(int dmg, bool critical, CharacterExpControl gainExp)
    {
        if(!Invencible){
            life -= dmg;

            if(life > 0){
                SpawnText(dmg.ToString(), critical, false, false);
                Dead = false;
            }
            else if(life < 0 && !Dead){
                gainExp.GetExp(expToDrop);
                Dead = true;
            }
        }
        else{
            SpawnText("Invencible", false, false, false);
        }

     }

    public void TakeDMG(int dmg, bool critical)
    {
        if(!Invencible){
            if(life > 0){
                SpawnText(dmg.ToString(), critical, false, false);
                life -= dmg;
                Dead = false;
            }
            else{
                Dead = true;
            }
        }
        else{
            SpawnText("Invencible", false, false, false);
        }
     }


    public int DealDmg(bool isPhysical)
    {
        float critChance = Random.Range(1, 100);

        if(isPhysical){
            dmg = Mathf.RoundToInt(Random.Range(physicalAtkPower / 1.2f, physicalAtkPower / 0.9f));
            if (critChance <= criticalRate)
            {
                criticalDmg = true;
                if(LifeSteal) LifeStealControl(dmg * 2);

                return dmg *= 2;
            }
            else{
                criticalDmg = false;
                if(LifeSteal) LifeStealControl(dmg);


                return dmg;
            }
        }
        else{
            dmg = Mathf.RoundToInt(Random.Range(magicAtkPower / 1.2f, magicAtkPower / 0.9f));
            return dmg;
        }
    }


    #region LifeSteal and RecoveryLife and Mana
    public void LifeStealControl(int value){
        RecoveryLife(Mathf.RoundToInt(value / 10)); 
        RecoveryMana(Mathf.RoundToInt(value / 15)); 
    }

    public void RecoveryLife(int value){
        if(life < maxLife){
            SpawnText($"+{value}", false, true, true);
            life += value;
        }
        else{
            life = maxLife;
        }
    }
    public void RecoveryMana(int value){
        if(mana < maxMana){
            SpawnText($"+{value}", false, true, false);
            mana += value;
        }
        else{
            mana = maxMana;
        }
    }
    #endregion

    #region SpawnText
    private void SpawnText(string text, bool critical, bool suport, bool healt)
    {
        if(!suport){
            if(this.gameObject.layer == 6) //Player
            {
                if (critical)
                    TextColor(1, 0, 0, 1);
                else
                    TextColor(0.3f, 0, 0, 1);
            }
            else //Other 
            {
                if (critical)
                    TextColor(0.1f, 0, 1, 1);
                else
                    TextColor(1, 1, 1, 1);
            }
        }
        else{
            if(healt) TextColor(0, 1, 0, 1); //life
            else TextColor(0, 0, 1, 1); //mana
        }            

        textDmg.GetComponent<TextMeshPro>().text = text;
        Instantiate(textDmg, spawnPosition.position, Quaternion.Euler(0, 0, 0));
    }
    private void TextColor(float r, float g, float b, float a)
    {
        textDmg.GetComponent<TextMeshPro>().color = new Color(r, g, b, a);
    }
    #endregion

    #region Death
    //end anims death
    public void Destroy() => StartCoroutine(FadeOutAndDestroy());
    IEnumerator FadeOutAndDestroy(){
        var sprite = GetComponent<SpriteRenderer>();
        for(float i = 1; i > -0.2f; i -= .1f){
        sprite.color = new Color(1,1,1,i);
            yield return new WaitForSeconds(.01f);
        }

        Destroy(this.gameObject);
    }
    #endregion
}
