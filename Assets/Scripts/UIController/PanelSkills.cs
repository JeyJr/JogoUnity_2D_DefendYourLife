using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelSkills : MonoBehaviour
{

    public List<Button> btnSkills;
    public List<GameObject> btnUpgradeSkills;

    public PlayerSkills playerSkills;
    public CharacterExpControl cExp;
    

    bool panelEnabled;

    void Update()
    {
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
        if(cExp.SkillPoints > 0){
            switch(skillNum){
                case 1:
                    if(playerSkills.FloorOfHellLevel < playerSkills.SkillsMaxLevel){
                        playerSkills.FloorOfHellLevel++;
                        cExp.SkillPoints--;
                    }
                    break;
                case 2:
                    if(playerSkills.BladesOfWindLevel < playerSkills.SkillsMaxLevel){
                        playerSkills.BladesOfWindLevel++;
                        cExp.SkillPoints--;
                    }
                    break;
                case 3:
                    if(playerSkills.WaterSpikesLevel < playerSkills.SkillsMaxLevel){
                        playerSkills.WaterSpikesLevel++;
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
}
