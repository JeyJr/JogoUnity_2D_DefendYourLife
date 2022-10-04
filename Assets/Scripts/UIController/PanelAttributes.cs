using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelAttributes : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private CharacterExpControl characterExpControl;
    private CharacterAttributes characterAttributes;

    //PanelComponentes

    public TextMeshProUGUI textStrValue, textIntValue, textVitValue, textLukValue;
    public TextMeshProUGUI textAtkPowerValue, textMagicPowerValue, textMaxLifeValue, textCriticalRateValue;
    public Slider sliderStrValue, sliderIntValue, sliderVitValue, sliderLukValue;


    private void Start()
    {
        characterExpControl = player.GetComponent<CharacterExpControl>();
        characterAttributes = player.GetComponent<CharacterAttributes>();

        sliderStrValue.maxValue = 100;
        sliderIntValue.maxValue = 100;
        sliderVitValue.maxValue = 100;
        sliderLukValue.maxValue = 100;

        //Update values 
        PanelComponentsUpdate();
    }
    private void Update()
    {
        if (this.gameObject.activeSelf) PanelComponentsUpdate();
    }

    //--------------------------------------Refatorar(se possível) sequencia abaixo
    public void ChangeStrValues(bool value)
    {
        if(characterExpControl.AttributePoints > 0 && value && characterAttributes.Strength < 100)
        {
            characterAttributes.Strength++;
            AddStatusPoint(true);
        }
        
        if(characterExpControl.UsedAttributePoints > 0 && !value && characterAttributes.Strength > 1)
        {
            characterAttributes.Strength--;
            AddStatusPoint(false);
        }
    }
    public void ChangeIntValues(bool value)
    {
        if(characterExpControl.AttributePoints > 0 && value && characterAttributes.Intelligence < 100)
        {
            characterAttributes.Intelligence++;
            AddStatusPoint(true);
        }
        
        if(characterExpControl.UsedAttributePoints > 0 && !value && characterAttributes.Intelligence > 1)
        {
            characterAttributes.Intelligence--;
            AddStatusPoint(false);
        }
    }    
    public void ChangeVitValues(bool value)
    {
        if(characterExpControl.AttributePoints > 0 && value && characterAttributes.Vitality < 100)
        {
            characterAttributes.Vitality++;
            AddStatusPoint(true);
        }
        
        if(characterExpControl.UsedAttributePoints > 0 && !value && characterAttributes.Vitality > 1)
        {
            characterAttributes.Vitality--;
            AddStatusPoint(false);
        }
    }    
    public void ChangeLukValues(bool value)
    {
        if(characterExpControl.AttributePoints > 0 && value && characterAttributes.Luck < 100)
        {
            characterAttributes.Luck++;
            AddStatusPoint(true);
        }
        
        if(characterExpControl.UsedAttributePoints > 0 && !value && characterAttributes.Luck > 1)
        {
            characterAttributes.Luck--;
            AddStatusPoint(false);
        }
    }
    private void AddStatusPoint(bool value)
    {
        if (value)
        {
            characterExpControl.AttributePoints--;
            characterExpControl.UsedAttributePoints++;
        }
        else
        {
            characterExpControl.AttributePoints++;
            characterExpControl.UsedAttributePoints--;
        }
    }
    private void PanelComponentsUpdate()
    {
        //STR
        textAtkPowerValue.text = characterAttributes.PhysicalAtkPower.ToString();
        textStrValue.text = characterAttributes.Strength.ToString();
        sliderStrValue.value = characterAttributes.Strength;

        //INT
        textMagicPowerValue.text = characterAttributes.MagicAtkPower.ToString();
        textIntValue.text = characterAttributes.Intelligence.ToString();
        sliderIntValue.value = characterAttributes.Intelligence;

        //VIT
        textMaxLifeValue.text = characterAttributes.MaxLife.ToString();
        textVitValue.text = characterAttributes.Vitality.ToString();
        sliderVitValue.value = characterAttributes.Vitality;

        //LUK
        textCriticalRateValue.text = characterAttributes.CriticalRate.ToString();
        textLukValue.text = characterAttributes.Luck.ToString();
        sliderLukValue.value = characterAttributes.Luck;
    }

}
