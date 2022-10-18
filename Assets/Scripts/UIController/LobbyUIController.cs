using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyUIController : MonoBehaviour
{
    private int str, inte, vit, luk, level;
    public int Str { get => str; set => str = value; }
    public int Inte { get => inte; set => inte = value; }
    public int Vit { get => vit; set => vit = value; }
    public int Luk { get => luk; set => luk = value; }
    public int Level { get => level ; set => level = value; }

    //---------------------------
    public List<GameObject> panels;



    private void Start()
    {
        if (str < 1) PlayerPrefs.SetInt("str", 1);
        if (inte < 1) PlayerPrefs.SetInt("inte", 1);
        if (vit < 1) PlayerPrefs.SetInt("vit", 1);
        if (luk < 1) PlayerPrefs.SetInt("luk", 1);
        if (level < 1) PlayerPrefs.SetInt("level", 1);

        LoadAttributes();

        for(int i = 0; i < panels.Count; i++)
        {
            if (i > 0) panels[i].SetActive(false);
            else panels[i].SetActive(true);
        }
    }
    void LoadAttributes()
    {
        str = PlayerPrefs.GetInt("str");
        inte = PlayerPrefs.GetInt("inte");
        vit = PlayerPrefs.GetInt("vit");
        luk = PlayerPrefs.GetInt("luk");
        level = PlayerPrefs.GetInt("level");
    }

    //BTN
    public void SavePlayerStatus()
    {
        PlayerPrefs.SetInt("physicalAtkPower", Mathf.RoundToInt(str * 1.5f));
        PlayerPrefs.SetInt("magicAtkPower", Mathf.RoundToInt(inte * 3.5f));
        PlayerPrefs.SetInt("maxLife", Mathf.RoundToInt(vit * 50));
        PlayerPrefs.SetInt("maxMana", Mathf.RoundToInt((inte * 4.5f) + 50));
        PlayerPrefs.SetInt("criticalRate", Mathf.RoundToInt(luk / 2));
    }
    public void SetActivePanel(int panel)
    {
        for (int i = 0; i < panels.Count; i++)
        {
            if (i == panel) panels[i].SetActive(true);
            else panels[i].SetActive(false);
        }
    }
}
