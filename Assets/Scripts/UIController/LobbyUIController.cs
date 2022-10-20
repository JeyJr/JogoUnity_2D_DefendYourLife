using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class LobbyUIController : MonoBehaviour
{
    private int str, inte, vit, luk, level, currentExp, nextLevelExp;
    private int physical, magical, maxLife, maxMana, criticalRate;

    
    private int skillPoints, usedSkillPoints, skillLevel, currentSkillExp, nextSkillLevelExp;
    private int skillsMaxLevel = 10;
    private int fohLevel, wsLevel, bowLevel, lsLevel, lkLevel, iLevel;


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

    public int SkillsMaxLevel { get => skillsMaxLevel; }
    public int FohLevel { get => fohLevel; set => fohLevel = value; }
    public int WsLevel { get => wsLevel; set => wsLevel = value; }
    public int BowLevel { get => bowLevel; set => bowLevel = value; }
    public int LsLevel { get => lsLevel; set => lsLevel = value; }
    public int LkLevel { get => lkLevel; set => lkLevel = value; }
    public int ILevel { get => iLevel; set => iLevel = value; }


    //---------------------------
    public List<GameObject> panels;

    private void Start()
    {
        if (!File.Exists(Application.persistentDataPath + "/aData.data"))
            FirstTimeSave();
        else 
            Load();


        if (!File.Exists(Application.persistentDataPath + "/sData.data"))
            FirstTimeSkillSave();
        else
            LoadSkills();


        for (int i = 0; i < panels.Count; i++)
        {
            if (i > 0) panels[i].SetActive(false);
            else panels[i].SetActive(true);
        }
        SetPlayerInfo();
    }

    public void SetPlayerInfo()
    {
        physical = Mathf.RoundToInt(str * 2.5f);
        magical = Mathf.RoundToInt(inte * 1.5f);
        maxLife = Mathf.RoundToInt(vit * 50);
        maxMana = Mathf.RoundToInt(inte * 25);
        criticalRate = Mathf.RoundToInt(luk / 2);
    }

    public void SetActivePanel(int panel)
    {
        for (int i = 0; i < panels.Count; i++)
        {
            if (i == panel) panels[i].SetActive(true);
            else panels[i].SetActive(false);
        }
    }

    public int GetAttributePoints()
    {
        int usedPoints = (str - 1) + (inte - 1) + (vit - 1) + (luk - 1);
        int points = (level * 3) - usedPoints;
        return points;
    }    
    
    public int GetUsedAttributesPoints()
    {
        int usedPoints = (str - 1) + (inte - 1) + (vit - 1) + (luk - 1);
        return usedPoints;
    }

    public int GetSkillPoints()
    {
        int usedSkillsPoints = fohLevel + wsLevel + bowLevel + lsLevel + lkLevel + iLevel;
        int points = skillLevel - usedSkillsPoints;
        return points;
    }

    public int GetUsedSkillsPoints()
    {
        int usedSkillsPoints = fohLevel + wsLevel + bowLevel + lsLevel + lkLevel + iLevel;
        return usedSkillsPoints;
    }

    void Load()
    {
        PlayerData playerData = GameData.LoadData();

        level = playerData.level;
        currentExp = playerData.currentExp;
        nextLevelExp = playerData.nextLevelExp;

        str = playerData.str;
        inte = playerData.inte;
        vit = playerData.vit;
        luk = playerData.luk;
    }

    void LoadSkills()
    {
        PlayerSkillsData playerSkillsData = GameData.LoadSkillsData();

        skillLevel = playerSkillsData.skillLevel;
        currentSkillExp = playerSkillsData.currentSkillExp;
        nextSkillLevelExp = playerSkillsData.nextSkillLevelExp;

        fohLevel = playerSkillsData.fohLevel;
        wsLevel = playerSkillsData.wsLevel;
        bowLevel = playerSkillsData.bowLevel;
        lsLevel = playerSkillsData.lsLevel;
        lkLevel = playerSkillsData.lkLevel;
        iLevel = playerSkillsData.iLevel;
    }

    void FirstTimeSave()
    {
        level = 1;
        currentExp = 0;
        nextLevelExp = 125;

        str = 1;
        inte = 1;
        vit = 1;
        luk = 1;

        GameData.CreateDataFile();
        GameData.SaveData(level, currentExp, nextLevelExp, str, inte, vit, luk);
    }

    void FirstTimeSkillSave()
    {
        skillLevel = 0;
        currentSkillExp = 0;
        nextSkillLevelExp = 125;

        fohLevel = 0;
        wsLevel = 0;
        bowLevel = 0;
        lsLevel = 0;
        lkLevel = 0;
        iLevel = 0;
        GameData.CreateSkillDataFile();
        GameData.SaveSkillsData(skillLevel, currentSkillExp, nextSkillLevelExp, fohLevel, wsLevel, bowLevel, lsLevel, lkLevel, iLevel);
    }

    public void DeleteSave() {
        File.Delete(Application.persistentDataPath + "/aData.data");
        File.Delete(Application.persistentDataPath + "/sData.data");
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void SaveAttributes() => GameData.SaveData(level, currentExp, nextLevelExp, str, inte, vit, luk);
    public void SaveSkills() => GameData.SaveSkillsData(skillLevel, currentSkillExp, nextSkillLevelExp, fohLevel, wsLevel, bowLevel, lsLevel, lkLevel, iLevel);


}
