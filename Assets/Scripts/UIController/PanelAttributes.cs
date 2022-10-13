using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelAttributes : MonoBehaviour
{   
    MainUI mainUI;
    [SerializeField] private GameObject player;

    private CharacterExpControl cExpControl;
    private CharacterAttributes cAttributes;

    //PanelComponentes

    public List<TextMeshProUGUI> textAttributes = new List<TextMeshProUGUI> ();
    public List<TextMeshProUGUI> textStatus = new List<TextMeshProUGUI> ();
    public List<Slider> sliders = new List<Slider> ();

    //Decorative informative
    public TextMeshProUGUI textAttributePointsValue, textPointsUsedValue;

    private void Awake()
    {
        mainUI = GetComponent<MainUI>();
        cExpControl = player.GetComponent<CharacterExpControl>();
        cAttributes = player.GetComponent<CharacterAttributes>();

        mainUI.SetMaxValueSliders(100, sliders);

        //Update values 
        UpdateComponentesPanelAttributess();
    }

    private void Update()
    {
        if (this.gameObject.activeSelf)
        {
            UpdateComponentesPanelAttributess();
            cExpControl.usedAttributePoints = (cAttributes.Strength - 1) + (cAttributes.Intelligence - 1) + (cAttributes.Vitality - 1) + ((int)cAttributes.Luck - 1);
        }
    }

    public void AddAttributePoints(string attributeName){
        if(cExpControl.AttributePoints > 0){
            switch(attributeName){
                case "Strength":
                    if(cAttributes.Strength < 100){
                        cAttributes.Strength++;
                        cExpControl.AttributePoints--;
                    }
                break;
                case "Intelligence":
                    if(cAttributes.Intelligence < 100){
                        cAttributes.Intelligence++;
                        cExpControl.AttributePoints--;
                    }
                break;
                case "Vitality":
                    if(cAttributes.Vitality < 100){
                        cAttributes.Vitality++;
                        cExpControl.AttributePoints--;
                    }
                break;
                case "Luck":
                    if(cAttributes.Luck < 100){
                        cAttributes.Luck++;
                        cExpControl.AttributePoints--;
                    }
                break;
            }
        }
    }
    public void SubtractAttributePoints(string attributeName){
        if(cExpControl.UsedAttributePoints > 0){
            switch(attributeName){
                case "Strength":
                    if(cAttributes.Strength > 1){
                        cAttributes.Strength--;
                        cExpControl.AttributePoints++;
                    }
                break;
                case "Intelligence":
                    if(cAttributes.Intelligence > 1){
                        cAttributes.Intelligence--;
                        cExpControl.AttributePoints++;
                    }
                break;
                case "Vitality":
                    if(cAttributes.Vitality > 1){
                        cAttributes.Vitality--;
                        cExpControl.AttributePoints++;
                    }
                break;
                case "Luck":
                    if(cAttributes.Luck > 1){
                        cAttributes.Luck--;
                        cExpControl.AttributePoints++;
                    }
                break;
            }
        }
    }
    private void UpdateComponentesPanelAttributess()
    {
        mainUI.SetTextMeshProUGUIValues(textAttributes[0].name, cAttributes.Strength,  textAttributes);
        mainUI.SetTextMeshProUGUIValues(textAttributes[1].name, cAttributes.Intelligence,  textAttributes);
        mainUI.SetTextMeshProUGUIValues(textAttributes[2].name, cAttributes.Vitality,  textAttributes);
        mainUI.SetTextMeshProUGUIValues(textAttributes[3].name, cAttributes.Luck,  textAttributes);


        mainUI.SetTextMeshProUGUIValues(textStatus[0].name, cAttributes.PhysicalAtkPower,  textStatus);
        mainUI.SetTextMeshProUGUIValues(textStatus[1].name, cAttributes.MagicAtkPower,  textStatus);
        mainUI.SetTextMeshProUGUIValues(textStatus[2].name, cAttributes.MaxMana,  textStatus);
        mainUI.SetTextMeshProUGUIValues(textStatus[3].name, cAttributes.MaxLife,  textStatus);
        mainUI.SetTextMeshProUGUIValues(textStatus[4].name, cAttributes.CriticalRate,  textStatus);

        mainUI.SetValuesSliders(sliders[0].name, cAttributes.Strength, sliders);
        mainUI.SetValuesSliders(sliders[1].name, cAttributes.Intelligence, sliders);
        mainUI.SetValuesSliders(sliders[2].name, cAttributes.Vitality, sliders);
        mainUI.SetValuesSliders(sliders[3].name, cAttributes.Luck, sliders);


        //Decorative attributePoints
        textAttributePointsValue.text = cExpControl.AttributePoints.ToString();
        textPointsUsedValue.text = cExpControl.usedAttributePoints.ToString();
    }
}
