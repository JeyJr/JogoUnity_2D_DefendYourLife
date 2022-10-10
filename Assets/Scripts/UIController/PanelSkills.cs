using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelSkills : MonoBehaviour
{
    public enum Skills{floorOfHell}
    public List<string> skill = new List<string>{"floorOfHell"};
    MainUI mainUI;
    [SerializeField] private GameObject player;
    private CharacterExpControl cExpControl;

    //Skills--------------------------------------------------------------
    private PlayerSkills playerSkills;

    //PanelComponentes------------------------------------------------------
    public List<TextMeshProUGUI> textSkillLevel = new();
    public List<Slider> sliders = new();

    //Componentes-------------------------------------------------------------
    public TextMeshProUGUI textSkillsPointsValue, textPointsUsedValue;



    void Awake()
    {
        mainUI = GetComponent<MainUI>();
        cExpControl = player.GetComponent<CharacterExpControl>();
        playerSkills = player.GetComponentInChildren<PlayerSkills>();
        
        mainUI.SetMaxValueSliders("sliderFloorOfHellValue", playerSkills.fohLevel, sliders);
    }

    void Update()
    {
        if (this.gameObject.activeSelf)
        {
            TextSkillsUpdate();
            cExpControl.UsedSkillsPoints = playerSkills.fohLevel; //More skills, update here
        }
    }

    private void TextSkillsUpdate(){
        mainUI.SetTextMeshProUGUIValues("txtFloorOfHellValue", $"{playerSkills.fohLevel}/{playerSkills.fohMaxLevel}", textSkillLevel);
        mainUI.SetValuesSliders("sliderFloorOfHellValue", playerSkills.fohLevel, sliders);

        textSkillsPointsValue.text = cExpControl.SkillPoints.ToString();
        textPointsUsedValue.text = cExpControl.UsedSkillsPoints.ToString();
    }

    public void AddSkillPoint(string skillName){
        if(cExpControl.SkillPoints > 0)
        switch(skillName){
            case "FloorOfHell":
                if(playerSkills.fohLevel < playerSkills.fohMaxLevel){
                    playerSkills.fohLevel++;
                    cExpControl.SkillPoints--;
                }
                break;
        }
    }

    public void SubtractSkillPoint(string skillName){
        if(cExpControl.UsedSkillsPoints > 0){
            switch(skillName){
                case "FloorOfHell":
                    if( playerSkills.fohLevel > 0){
                        playerSkills.fohLevel--;
                        cExpControl.SkillPoints++;
                    }
                    break;
            }
        }
    }
}
