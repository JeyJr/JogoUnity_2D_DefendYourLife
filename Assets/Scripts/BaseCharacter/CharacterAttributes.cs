using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterAttributes : MonoBehaviour
{
    #region Attributes
    [Header("EnemyLevel")]
    [SerializeField] private int enemyLevel;
    [SerializeField] private bool player, enemy, boss;
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
    [SerializeField] private CharacterExpControl playerExp;

    #endregion

    private void Start()
    {
        if (player) StartAttributesPlayer(); //Carregar
        if (enemy) StartAttributesEnemys(5, 1000, 20, 5);
        if (boss) StartAttributesEnemys(15, 100, 50, 10);
    }

    void StartAttributesPlayer()
    {
        physicalAtkPower = PlayerPrefs.GetInt("physicalAtkPower");
        magicAtkPower = PlayerPrefs.GetInt("magicAtkPower");
        maxLife = PlayerPrefs.GetInt("maxLife");
        life = maxLife;
        maxMana = PlayerPrefs.GetInt("maxMana");
        mana = maxMana;
        criticalRate = PlayerPrefs.GetInt("criticalRate");
    }

    void StartAttributesEnemys(int attributesMultiplicador, int dropExp, int lifeMult, int manaMult) {
        strength = Random.Range(enemyLevel, enemyLevel + attributesMultiplicador);
        vitality = Random.Range(enemyLevel, enemyLevel + attributesMultiplicador);
        intelligence = Random.Range(enemyLevel, enemyLevel + attributesMultiplicador);
        luck = Random.Range(enemyLevel, enemyLevel + attributesMultiplicador);

        expToDrop = Random.Range(enemyLevel + (dropExp / 2), enemyLevel + dropExp);

        life = vitality * lifeMult;
        maxLife = life;
        mana = intelligence * manaMult;
        maxMana = mana;

        physicalAtkPower = Mathf.RoundToInt(strength * enemyLevel); //atk = str * 1.5f 
        magicAtkPower = Mathf.RoundToInt(Intelligence * enemyLevel); //magic = int * 3.5f
        criticalRate = Mathf.RoundToInt(luck / 2);
    }

    private void Update(){
        if (player) {
            if (life > maxLife) life = maxLife;
            if (mana > maxMana) mana = maxMana;
        }
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
