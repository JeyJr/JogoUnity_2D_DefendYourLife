using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{

    public void BtnClose(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
    public void SetTextMeshProUGUIValues(string name, int value, List<TextMeshProUGUI> textList)
    {
        foreach (var item in textList)
        {
            if(item.name == name)
                item.text = value.ToString();
        }
    }
    public void SetTextMeshProUGUIValues(string name, string text, List<TextMeshProUGUI> textList)
    {
        foreach (var item in textList)
        {
            if(item.name == name)
                item.text = text;
        }
    }
    public void SetValuesSliders(string name, int value, List<Slider> sliders)
    {
        foreach (var item in sliders)
        {
            if(item.name == name)
                item.value = value;
        }
    }
    public void SetMaxValueSliders(string name, int value, List<Slider> sliders)
    {
        foreach (var item in sliders)
        {
            if(item.name == name)
                item.maxValue = value;
        }
    }
    public void SetMaxValueSliders(int value, List<Slider> sliders)
    {
        foreach (var item in sliders)
        {
            item.maxValue = value;
        }
    }
}
