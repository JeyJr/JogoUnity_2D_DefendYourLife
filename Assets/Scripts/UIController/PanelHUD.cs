using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelHUD : MonoBehaviour
{
    public PlayerSkills playerSkills;
    public PlayerInputs playerInputs;
    public CharacterAttributes cAttributes;
    public CharacterExpControl cExp;
    
    //Skills----------------------------------
    public List<Button> btnSkills;
    public List<GameObject> btnUpgradeSkills;
    //Sliders----------------------------------
    public List<Slider> sliders;
    public TextMeshProUGUI textLevel, textExp;
    bool panelEnabled;

    //DescriptionSkills-----------------------
    public GameObject skillsDescription;
    public Image iconSkill;
    public TextMeshProUGUI txtSkillName, txtSkillLevel, txtSkillMana, txtSkillDmg;



    void Update()
    {
        SkillsChecks();
        SlidersBar();
        Texts();

        SetBtnEnabled();
    }

    #region BTNs
    public void BtnClose(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }


    public void SetBtnEnabled(){

        if(playerSkills.FloorOfHellLevel <= 0 || playerSkills.FloorOfHellCountdown)
            btnSkills[0].interactable = false;
        else if(playerSkills.FloorOfHellLevel > 0 && !playerSkills.FloorOfHellCountdown)
            btnSkills[0].interactable = true;


        if (playerSkills.WaterSpikesLevel <= 0 || playerSkills.WaterSpikesCountdown) 
            btnSkills[1].interactable = false;
        else 
            btnSkills[1].interactable = true;
        
        if (playerSkills.BladesOfWindLevel <= 0 || playerSkills.BladesOfWindCountdown) 
            btnSkills[2].interactable = false;
        else 
            btnSkills[2].interactable = true;
        
        if (playerSkills.LifeStealLevel <= 0 || playerSkills.LifeStealCountdown) 
            btnSkills[3].interactable = false;
        else 
            btnSkills[3].interactable = true;

        if (playerSkills.LuckyLevel <= 0 || playerSkills.LuckyCountdown) 
            btnSkills[4].interactable = false;
        else 
            btnSkills[4].interactable = true;
        
        if (playerSkills.InvencibleLevel <= 0 || playerSkills.InvencibleCountdown) 
            btnSkills[5].interactable = false;
        else 
            btnSkills[5].interactable = true;
    }
    #endregion

    #region EventTrigger
    public void SetPointerEnter(bool value){
        playerInputs.MeleeAtkEnabled = false;
        skillsDescription.SetActive(value);
    }
    public void SetPointerExit(bool value)
    {
        playerInputs.MeleeAtkEnabled = true;
        skillsDescription.SetActive(value);
    }

    public void SetSkillsDescription(int skillNum){
        switch(skillNum){
            case 1:
                SkillDescription(btnSkills[0].GetComponent<Image>(), 1, "Floor of Hell", playerSkills.FloorOfHellLevel, playerSkills.FloorOfHellManaCost, true,"every 0.5s");
                break;
            case 2:
                SkillDescription(btnSkills[1].GetComponent<Image>(), 2, "Water Spikes", playerSkills.WaterSpikesLevel, playerSkills.WaterSpikesManaCost, true,"per spike");
                break;
            case 3:
                SkillDescription(btnSkills[2].GetComponent<Image>(), 3, "Blades Of Wind", playerSkills.BladesOfWindLevel, playerSkills.BladesOfWindManaCost, true,"");
                break;
            case 4:
                SkillDescription(btnSkills[3].GetComponent<Image>(), 4, "Life Steal", playerSkills.LifeStealLevel, playerSkills.LifeStealManaCost, false, $" Recovers some HP and mana for {playerSkills.LifeStealDuration}s");
                break;
            case 5:
                SkillDescription(btnSkills[4].GetComponent<Image>(), 5, "Lucky", playerSkills.LuckyLevel, playerSkills.LuckyManaCost, false,$"Increase {playerSkills.LuckyLevel * 2} LUK per {playerSkills.LuckyDuration}s");
                break;
            case 6:
                SkillDescription(btnSkills[5].GetComponent<Image>(), 6, "Invincible", playerSkills.InvencibleLevel, playerSkills.InvencibleManaCost, false, $"invincible for {playerSkills.InvencibleDuration}s");
                break;
        }
    }


    void SkillDescription(Image img, int skillNum,string name, int level, int mana, bool dmg, string desc){
        iconSkill.sprite = img.sprite;
        txtSkillName.text = $"[{skillNum}] {name}";
        txtSkillLevel.text = $"{level}/{playerSkills.SkillsMaxLevel}";
        txtSkillMana.text = $"{mana} Mana";

        if(dmg)
            txtSkillDmg.text = $"Skill Dmg: {cAttributes.MagicAtkPower} {desc}";
        else
            txtSkillDmg.text = $"{desc}";
    }
    #endregion

    #region Player status
    void SlidersBar(){
        sliders[0].value = cAttributes.Life;
        sliders[0].maxValue = cAttributes.MaxLife;

        sliders[1].value = cAttributes.Mana;
        sliders[1].maxValue = cAttributes.MaxMana;

        sliders[2].value = cExp.CurrentExp;
        sliders[2].maxValue = cExp.NextLevelExp;
    }
    void Texts(){
        if(cExp.Level < 10) textLevel.text = $"Lv: 0{cExp.Level}";
        else if(cExp.Level == 100) textLevel.text = $"Lv max: {cExp.Level}";
        else textLevel.text = $"Lv: {cExp.Level}";
        textExp.text = $"Exp: {cExp.CurrentExp} / {cExp.NextLevelExp}";
    }
    #endregion

    #region Skills
    void SkillsChecks(){
        if(cExp.SkillPoints > 0){
            btnUpgradeSkills[0].SetActive(SkillsLevelCheck(playerSkills.FloorOfHellLevel, playerSkills.SkillsMaxLevel));
            btnUpgradeSkills[1].SetActive(SkillsLevelCheck(playerSkills.BladesOfWindLevel, playerSkills.SkillsMaxLevel));
            btnUpgradeSkills[2].SetActive(SkillsLevelCheck(playerSkills.WaterSpikesLevel, playerSkills.SkillsMaxLevel));
            btnUpgradeSkills[3].SetActive(SkillsLevelCheck(playerSkills.LifeStealLevel, playerSkills.SkillsMaxLevel));
            btnUpgradeSkills[4].SetActive(SkillsLevelCheck(playerSkills.LuckyLevel, playerSkills.SkillsMaxLevel));
            btnUpgradeSkills[5].SetActive(SkillsLevelCheck(playerSkills.InvencibleLevel, playerSkills.SkillsMaxLevel));
            panelEnabled = false;
        }
        else{
            if(!panelEnabled) {
                panelEnabled = true;
                foreach(var i in btnUpgradeSkills){
                    i.SetActive(false);
                }
            }
        }
    }
    bool SkillsLevelCheck(int level, int maxLevel){
        if(level < maxLevel)
            return true;
        else 
            return false;
    }
    public void UpgradeSkill(int skillNum){
        //Btn
        if(cExp.SkillPoints > 0){
            switch(skillNum){
                case 1:
                    if(playerSkills.FloorOfHellLevel < playerSkills.SkillsMaxLevel){
                        playerSkills.FloorOfHellLevel++;
                        cExp.SkillPoints--;
                    }
                    break;
                case 2:
                    if(playerSkills.WaterSpikesLevel < playerSkills.SkillsMaxLevel){
                        playerSkills.WaterSpikesLevel++;
                        cExp.SkillPoints--;
                    }
                    break;
                case 3:
                    if(playerSkills.BladesOfWindLevel < playerSkills.SkillsMaxLevel){
                        playerSkills.BladesOfWindLevel++;
                        cExp.SkillPoints--;
                    }
                    break;
                case 4:
                    if(playerSkills.LifeStealLevel < playerSkills.SkillsMaxLevel){
                        playerSkills.LifeStealLevel++;
                        cExp.SkillPoints--;
                    }
                    break;
                case 5:
                    if(playerSkills.LuckyLevel < playerSkills.SkillsMaxLevel){
                        playerSkills.LuckyLevel++;
                        cExp.SkillPoints--;
                    }
                    break;
                case 6:
                    if(playerSkills.InvencibleLevel < playerSkills.SkillsMaxLevel){
                        playerSkills.InvencibleLevel++;
                        cExp.SkillPoints--;
                    }
                    break;
            }
        }
    }
    #endregion
}
