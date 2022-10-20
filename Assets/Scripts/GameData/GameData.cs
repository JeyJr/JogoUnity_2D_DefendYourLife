using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public class GameData : MonoBehaviour
{
    //public static PlayerData playerData = new PlayerData();

    //public static void SaveData(int level, int currentExp, int nextLevelExp, int str, int inte, int vit, int luk)
    //{
    //    PlayerData playerData = new PlayerData();

    //    //LEVEL
    //    playerData.level = level;
    //    playerData.currentExp = currentExp;
    //    playerData.nextLevelExp = nextLevelExp;

    //    //ATTRIBUTES
    //    playerData.str = str;
    //    playerData.inte = inte;
    //    playerData.vit = vit;
    //    playerData.luk = luk;

    //    //STATUS
    //    playerData.physical = Mathf.RoundToInt(playerData.str * 1.5f);
    //    playerData.magical = Mathf.RoundToInt(playerData.inte * 3.5f);
    //    playerData.maxLife = Mathf.RoundToInt(playerData.vit * 50);
    //    playerData.maxMana = Mathf.RoundToInt((playerData.inte * 4.5f) + 50);
    //    playerData.criticalRate = Mathf.RoundToInt(playerData.luk / 2);

    //    BinaryFormatter bf = new();
    //    FileStream file = File.Create(Application.persistentDataPath + "/sData.data");
    //    bf.Serialize(file, playerData);
    //    file.Close();
    //}

    public static void SaveData(int level, int currentExp, int nextLevelExp, int str, int inte, int vit, int luk)
    {
        PlayerData playerData = new PlayerData();

        playerData.level = level;
        playerData.currentExp = currentExp;
        playerData.nextLevelExp = nextLevelExp;

        playerData.str = str;
        playerData.inte = inte;
        playerData.vit = vit;
        playerData.luk = luk;

        BinaryFormatter bf = new();
        FileStream file = File.Create(Application.persistentDataPath + "/aData.data");
        bf.Serialize(file, playerData);
        file.Close();

        //Debug.Log($"File saved: Level: {playerData.level} | CurrentExp: {playerData.currentExp} | NextLevel: {playerData.nextLevelExp}");
    }    

    public static PlayerData LoadData()
    {
        BinaryFormatter bf = new();
        FileStream file = File.Open(Application.persistentDataPath + "/aData.data", FileMode.Open);
        PlayerData playerData = (PlayerData)bf.Deserialize(file);
        file.Close();

        Debug.Log("FileLoaded!");
        return playerData;
    }


    public static void SaveSkillsData(int skillLevel, int currentSkillExp, int nextSkillLevelExp, int fohLevel, int wsLevel, int bowLevel, int lsLevel, int lkLevel, int iLevel)
    {
        PlayerSkillsData playerSkillsData = new();

        playerSkillsData.skillLevel = skillLevel;
        playerSkillsData.currentSkillExp = currentSkillExp;
        playerSkillsData.nextSkillLevelExp = nextSkillLevelExp;

        playerSkillsData.fohLevel = fohLevel;
        playerSkillsData.wsLevel = wsLevel;
        playerSkillsData.bowLevel = bowLevel;
        playerSkillsData.lsLevel = lsLevel;
        playerSkillsData.lkLevel = lkLevel;
        playerSkillsData.iLevel = iLevel;

        BinaryFormatter bf = new();
        FileStream file = File.Create(Application.persistentDataPath + "/sData.data");
        bf.Serialize(file, playerSkillsData);
        file.Close();
    }
    public static PlayerSkillsData LoadSkillsData()
    {
        BinaryFormatter bf = new();
        FileStream file = File.Open(Application.persistentDataPath + "/sData.data", FileMode.Open);
        PlayerSkillsData playerData = (PlayerSkillsData)bf.Deserialize(file);
        file.Close();

        Debug.Log("FileLoaded!");
        return playerData;
    }
    public static void CreateDataFile()
    {
        PlayerData playerData = new PlayerData();
        BinaryFormatter bf = new();
        FileStream file = File.Create(Application.persistentDataPath + "/aData.data");
        bf.Serialize(file, playerData);
        file.Close();
    }
    public static void CreateSkillDataFile()
    {
        PlayerSkillsData playerSkillsData = new PlayerSkillsData();
        BinaryFormatter bf = new();
        FileStream file = File.Create(Application.persistentDataPath + "/sData.data");
        bf.Serialize(file, playerSkillsData);
        file.Close();
    }
}
