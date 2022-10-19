using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class LobbyUIController : MonoBehaviour
{
    private int str, inte, vit, luk, level, currentExp, nextLevelExp;
    private int physical, magical, maxLife, maxMana, criticalRate;

    public int Str { get => str; set => str = value; }
    public int Inte { get => inte; set => inte = value; }
    public int Vit { get => vit; set => vit = value; }
    public int Luk { get => luk; set => luk = value; }
    public int Level { get => level ; set => level = value; }

    public int Physical { get => physical; }
    public int Magical { get => magical; }
    public int MaxLife { get => maxLife; }
    public int MaxMana { get => maxMana; }
    public int CriticalRate { get => criticalRate; }

    //---------------------------
    public List<GameObject> panels;

    private void Start()
    {
        if (!File.Exists(Application.persistentDataPath + "/sData.data"))
            FirstTimeSave();
        else 
            Load();


        for (int i = 0; i < panels.Count; i++)
        {
            if (i > 0) panels[i].SetActive(false);
            else panels[i].SetActive(true);
        }

    }
    public void SetActivePanel(int panel)
    {
        for (int i = 0; i < panels.Count; i++)
        {
            if (i == panel) panels[i].SetActive(true);
            else panels[i].SetActive(false);
        }
    }

    void Load()
    {
        PlayerData playerData = GameData.LoadData();

        level = playerData.level;
        currentExp = playerData.currentExp;
        nextLevelExp = playerData.nextLevelExp;
    }

    void FirstTimeSave()
    {
        level = 1;
        currentExp = 1;
        nextLevelExp = 125;
        GameData.CreateFile();
        GameData.SaveLevel(level, currentExp, nextLevelExp);
    }

    public void DeleteSave() {
        File.Delete(Application.persistentDataPath + "/sData.data");
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
