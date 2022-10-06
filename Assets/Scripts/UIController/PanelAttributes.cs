using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelAttributes : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private CharacterExpControl cExpControl;
    private CharacterAttributes cAttributes;

    //PanelComponentes

    public List<TextMeshProUGUI> textAttributes = new List<TextMeshProUGUI> ();
    public List<TextMeshProUGUI> textStatus = new List<TextMeshProUGUI> ();
    public List<Slider> sliders = new List<Slider> ();

    //Decorative informative
    public TextMeshProUGUI textAttributePointsValue, textPointsUsedValue;

    private void Start()
    {
        cExpControl = player.GetComponent<CharacterExpControl>();
        cAttributes = player.GetComponent<CharacterAttributes>();

        foreach (var item in sliders)
        {
            item.maxValue = 100;
        }

        //Update values 
        PanelComponentsUpdate();
    }

    private void Update()
    {
        if (this.gameObject.activeSelf)
        {
            PanelComponentsUpdate();
            cExpControl.usedAttributePoints = (cAttributes.Strength - 1) + (cAttributes.Intelligence - 1) + (cAttributes.Vitality - 1) + (cAttributes.Luck - 1);
        }

    }

    #region buttons
    public void ChangeStrValues(bool value)
    {
        if(cExpControl.AttributePoints > 0 && value && cAttributes.Strength < 100)
        {
            cAttributes.Strength++;
            AddStatusPoint(true);
        }
        
        if(cExpControl.UsedAttributePoints > 0 && !value && cAttributes.Strength > 1)
        {
            cAttributes.Strength--;
            AddStatusPoint(false);
        }
    }
    public void ChangeIntValues(bool value)
    {
        if(cExpControl.AttributePoints > 0 && value && cAttributes.Intelligence < 100)
        {
            cAttributes.Intelligence++;
            AddStatusPoint(true);
        }
        
        if(cExpControl.UsedAttributePoints > 0 && !value && cAttributes.Intelligence > 1)
        {
            cAttributes.Intelligence--;
            AddStatusPoint(false);
        }
    }    
    public void ChangeVitValues(bool value)
    {
        if(cExpControl.AttributePoints > 0 && value && cAttributes.Vitality < 100)
        {
            cAttributes.Vitality++;
            AddStatusPoint(true);
        }
        
        if(cExpControl.UsedAttributePoints > 0 && !value && cAttributes.Vitality > 1)
        {
            cAttributes.Vitality--;
            AddStatusPoint(false);
        }
    }    
    public void ChangeLukValues(bool value)
    {
        if(cExpControl.AttributePoints > 0 && value && cAttributes.Luck < 100)
        {
            cAttributes.Luck++;
            AddStatusPoint(true);
        }
        
        if(cExpControl.UsedAttributePoints > 0 && !value && cAttributes.Luck > 1)
        {
            cAttributes.Luck--;
            AddStatusPoint(false);
        }
    }
    private void AddStatusPoint(bool value)
    {
        cExpControl.usedAttributePoints = (cAttributes.Strength - 1) + (cAttributes.Intelligence - 1) + (cAttributes.Vitality - 1) + (cAttributes.Luck - 1);
        if (value)
            cExpControl.AttributePoints--;
        else
            cExpControl.AttributePoints++;
    }
    #endregion

    # region PanelUIUpdate
    private void PanelComponentsUpdate()
    {
        //Attributes
        SetValuesTextAttributes("txtStrValue", cAttributes.Strength);
        SetValuesTextAttributes("txtIntValue", cAttributes.Intelligence);
        SetValuesTextAttributes("txtVitValue", cAttributes.Vitality);
        SetValuesTextAttributes("txtLukValue", cAttributes.Luck);


        //Status
        SetValuesTextStatus("txtAtkPowerValue", cAttributes.PhysicalAtkPower);
        SetValuesTextStatus("txtMagicPowerValue", cAttributes.MagicAtkPower);
        SetValuesTextStatus("txtMaxLifeValue", cAttributes.MaxLife);
        SetValuesTextStatus("txtCriticalRateValue", cAttributes.CriticalRate);


        //Sliders
        SetValuesSliders("sliderStrValue", cAttributes.Strength);
        SetValuesSliders("sliderIntValue", cAttributes.Intelligence);
        SetValuesSliders("sliderVitValue", cAttributes.Vitality);
        SetValuesSliders("sliderLukValue", cAttributes.Luck);

        //Decorative attributePoints
        textAttributePointsValue.text = cExpControl.AttributePoints.ToString();
        textPointsUsedValue.text = cExpControl.usedAttributePoints.ToString();
    }

    /// <summary>
    /// [0]txtStrValue, [1]txtIntValue, [2]txtVitValue, [3]txtLukValue
    /// </summary>
    private void SetValuesTextAttributes(string name, int value)
    {
        //txtStrValue, txtIntValue, txtVitValue, txtLukValue

        foreach (var item in textAttributes)
        {
            if(item.name == name)
                item.text = value.ToString();
        }
    }

    /// <summary>
    /// [0]txtAtkPowerValue, [1]txtMagicPowerValue, [2]txtMaxLifeValue, [3]txtCriticalRateValue
    /// </summary>
    private void SetValuesTextStatus(string name, int value)
    {
        foreach (var item in textStatus)
        {
            if(item.name == name)
                item.text = value.ToString();
        }
    }

    /// <summary>
    /// [0]sliderStrValue, [1]sliderIntValue, [2]sliderVitValue, [3]sliderLukValue
    /// </summary>
    private void SetValuesSliders(string name, int value)
    {
        foreach (var item in sliders)
        {
            if(item.name == name)
                item.value = value;
        }
    }
    #endregion
}
