using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelSkills : MonoBehaviour
{
    MainUI mainUI;
    [SerializeField] private GameObject player;
    private CharacterExpControl cExpControl;

    //Skills--------------------------------------------------------------
    private SkillFloorOfHell floorOfHell;

    //PanelComponentes------------------------------------------------------
    public List<TextMeshProUGUI> textSkillLevel = new List<TextMeshProUGUI> ();
    public List<Slider> sliders = new List<Slider> ();

    //Componentes-------------------------------------------------------------
    public TextMeshProUGUI textSkillsPointsValue, textPointsUsedValue;



    void Awake()
    {
        mainUI = GetComponent<MainUI>();
        cExpControl = player.GetComponent<CharacterExpControl>();
        floorOfHell = player.GetComponentInChildren<SkillFloorOfHell>();
        
        mainUI.SetMaxValueSliders("sliderFloorOfHellValue", floorOfHell.SkillMaxLevel, sliders);
    }

    void Update()
    {
        if (this.gameObject.activeSelf)
        {
            TextSkillsUpdate();
            cExpControl.UsedSkillsPoints = floorOfHell.SkillLevel; //More skills, update here
        }
    }

    private void TextSkillsUpdate(){
        mainUI.SetTextMeshProUGUIValues("txtFloorOfHellValue", $"{floorOfHell.SkillLevel}/{floorOfHell.SkillMaxLevel}", textSkillLevel);
        mainUI.SetValuesSliders("sliderFloorOfHellValue", floorOfHell.SkillLevel, sliders);

        textSkillsPointsValue.text = cExpControl.SkillPoints.ToString();
        textPointsUsedValue.text = cExpControl.UsedSkillsPoints.ToString();
    }

    public void AddSkillPoint(string skillName){
        if(cExpControl.SkillPoints > 0)
        switch(skillName){
            case "FloorOfHell":
                if( floorOfHell.SkillLevel < floorOfHell.SkillMaxLevel){
                    floorOfHell.SkillLevel++;
                    cExpControl.SkillPoints--;
                }
                break;
        }
    }

    public void SubtractSkillPoint(string skillName){
        if(cExpControl.UsedSkillsPoints > 0){
            switch(skillName){
                case "FloorOfHell":
                    if( floorOfHell.SkillLevel > 0){
                        floorOfHell.SkillLevel--;
                        cExpControl.SkillPoints++;
                    }
                    break;
            }
        }
    }
}
