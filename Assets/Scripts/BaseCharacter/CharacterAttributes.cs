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
    [SerializeField] private int str;
    [SerializeField] private int inte;
    [SerializeField] private int vit;
    [SerializeField] private int luk;
 
    //----------------------------------------------
    [Space(10)]
    [Header("Status")]

    [SerializeField] private int physical;
    [SerializeField] private int magical;
    [SerializeField] private int life, maxLife;
    [SerializeField] private int mana, maxMana;
    [SerializeField] private int criticalRate;
    public bool criticalDmg;
    int dmg;

    //Properties: Attribute Base-------------------------------------

    public int Str { get => str; set => str = value; }
    public int Inte { get => inte; set => inte = value; }
    public int Vit { get => vit; set => vit = value; }
    public int Luk { get => luk; set => luk = value; }
 
     //Properties: Stats----------------------------------------------

    public int Physical { get => physical; set => physical = value; }
    public int Magical { get => magical; set => magical = value; }
    public int Life { get => life; set => life = value; }
    public int MaxLife { get => maxLife; set => maxLife = value; }
    public int Mana { get => mana; set => mana = value; }
    public int MaxMana {get => maxMana; set => maxMana = value; }
    public int CriticalRate { get => criticalRate; set => criticalRate = value; }
    
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
    [SerializeField] private SpriteRenderer spriteRenderer;
    Color originalColor;
    #endregion

    private void Start()
    {
        originalColor = spriteRenderer.color;

        if (player) StartPlayerAttributes();
        if (enemy) StartAttributesEnemys(5, 10, 20, 5);
        if (boss) StartAttributesEnemys(15, 100, 50, 10);
    }

    void StartPlayerAttributes() {
        physical = Mathf.RoundToInt(str * 2.5f);
        magical = Mathf.RoundToInt(inte * 1.5f);
        maxLife = Mathf.RoundToInt(vit * 50);
        maxMana = Mathf.RoundToInt(inte * 25);
        criticalRate = Mathf.RoundToInt(luk / 2);

        life = maxLife;
        mana = maxMana;
    }
    void StartAttributesEnemys(int attributesMultiplicador, int dropExp, int lifeMult, int manaMult) {
        str = Random.Range(enemyLevel, enemyLevel + attributesMultiplicador);
        vit = Random.Range(enemyLevel, enemyLevel + attributesMultiplicador);
        inte = Random.Range(enemyLevel, enemyLevel + attributesMultiplicador);
        luk = Random.Range(enemyLevel, enemyLevel + attributesMultiplicador);

        expToDrop = Random.Range(enemyLevel + (dropExp / 2), enemyLevel + dropExp);

        life = vit * lifeMult;
        maxLife = life;
        mana = inte * manaMult;
        maxMana = mana;

        physical = Mathf.RoundToInt(str * enemyLevel); //atk = str * 1.5f 
        magical = Mathf.RoundToInt(inte * enemyLevel); //magic = int * 3.5f
        criticalRate = Mathf.RoundToInt(luk / 2);
    }

    private void Update(){
        if (player) {
            if (life > maxLife) life = maxLife;
            if (mana > maxMana) mana = maxMana;
        }
    }

    #region DMGControl
    public void TakeDMG(int dmg, bool critical, CharacterExpControl gainExp)
    {
        spriteRenderer.color = Color.red;
        StartCoroutine(SetColorDefault(originalColor));

        if (!Invencible){
            life -= dmg;

            if(life > 0){
                SpawnText(dmg.ToString(), critical, false, false);
                Dead = false;
            }
            else if(life < 0 && !Dead){
                gainExp.GetExp(expToDrop);
                Dead = true;

                if (boss) {
                    GetComponent<BossNum>().UnlockNextLevel();
                    GameObject.FindGameObjectWithTag("SceneControl").GetComponent<SceneControl>().SpawnBoss();
                }

                if (enemy) GameObject.FindGameObjectWithTag("SceneControl").GetComponent<SceneControl>().EnemySpawned(1);
            }
        }
        else{
            SpawnText("Invencible", false, false, false);
        }

     }
    public void TakeDMG(int dmg, bool critical)
    {
        spriteRenderer.color = Color.red;
        StartCoroutine(SetColorDefault(originalColor));

        if(!Invencible){
            if(life > 0){
                SpawnText(dmg.ToString(), critical, false, false);
                life -= dmg;
                Dead = false;
            }
            else{
                Dead = true;
                if (boss) GetComponent<BossNum>().UnlockNextLevel();
                if (enemy) GameObject.FindGameObjectWithTag("SceneControl").GetComponent<SceneControl>().EnemySpawned(1);
            }
        }
        else{
            SpawnText("Invencible", false, false, false);
        }
     }

    IEnumerator SetColorDefault(Color color)
    {
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = color;
    }

    public int DealDmg(bool isPhysical)
    {
        float critChance = Random.Range(1, 100);

        if(isPhysical){
            dmg = Mathf.RoundToInt(Random.Range(physical / 1.2f, physical / 0.9f));
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
            dmg = Mathf.RoundToInt(Random.Range(magical / 1.2f, magical / 0.9f));
            return dmg;
        }
    }
    #endregion

    #region LifeSteal and RecoveryLife and Mana
    public void LifeStealControl(int value){
        RecoveryLife(Mathf.RoundToInt(value / 10)); 
        RecoveryMana(Mathf.RoundToInt(value / 15)); 
    }

    public void RecoveryLife(int value){
        if(life < maxLife)
        {
            if(value <= 0)
            {
                SpawnText($"1", false, true, true);
                life ++;
            }
            else
            {
                SpawnText($"{value}", false, true, true);
                life += value;
            }
        }
    }
    public void RecoveryMana(int value){
        if(mana < maxMana){

            if(value <= 0)
            {
                SpawnText($"1", false, true, false);
                mana ++;
            }
            else
            {
                SpawnText($"{value}", false, true, false);
                mana += value;
            }
        }
    }
    #endregion

    #region SpawnText
    private void SpawnText(string text, bool critical, bool suport, bool healt)
    {
        if(!suport){
            if(this.gameObject.layer == 6) //Player
                TextColor(1, 0, 0, 1);
            else //Other 
                TextColor(1, 1, 1, 1);

            textDmg.GetComponent<TextMeshPro>().text = text;
        }
        else{
            if (healt)
            {
                TextColor(0, 1, 0, 1); //life
                textDmg.GetComponent<TextMeshPro>().text = $"+{text}";
            }
            else {
                TextColor(0, 0, 1, 1); //mana
                textDmg.GetComponent<TextMeshPro>().text = $"+{text}";
            }
        }            
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
