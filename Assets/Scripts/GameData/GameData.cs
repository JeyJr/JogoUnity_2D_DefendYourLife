using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public class GameData : MonoBehaviour
{
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

        //Debug.Log("FileLoaded!");
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

        //Debug.Log("FileLoaded!");
        return playerData;
    }
    
    public static void SaveLevelData(int levelNum)
    {
        LevelUnlockData levelData = new();
        LevelUnlockData checkLevelData = LoadLevelData();

        Debug.Log($"CheckLevelData: {checkLevelData.levelUnlock}");
        Debug.Log($"LevelUnlok: {levelData.levelUnlock}");
        Debug.Log($"LevelNum: {levelNum}");
        Debug.Log("------------------------------------------");

        if (checkLevelData.levelUnlock < levelNum)
        {
            //--------------------------------
            levelData.levelUnlock = levelNum;

            Debug.Log("Depois de salvar!");
            Debug.Log($"CheckLevelData: {checkLevelData.levelUnlock}");
            Debug.Log($"LevelUnlok: {levelData.levelUnlock}");
            Debug.Log($"LevelNum: {levelNum}");
            Debug.Log("Fase salva com sucesso!");
        }
        else
        {
            Debug.Log($"LevelUnlok: {levelData.levelUnlock}");
            Debug.Log("Fase ja foi desbloqueado!");
        }
            

        BinaryFormatter bf = new();
        FileStream file = File.Create(Application.persistentDataPath + "/lData.data");
        bf.Serialize(file, levelData);
        file.Close();
    }
    public static LevelUnlockData LoadLevelData()
    {
        BinaryFormatter bf = new();
        FileStream file = File.Open(Application.persistentDataPath + "/lData.data", FileMode.Open);
        LevelUnlockData levelData = (LevelUnlockData)bf.Deserialize(file);
        file.Close();
        return levelData;
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
    public static void CreateLevelDataFile()
    {
        LevelUnlockData levelUnlockData = new LevelUnlockData();
        BinaryFormatter bf = new();
        FileStream file = File.Create(Application.persistentDataPath + "/lData.data");
        bf.Serialize(file, levelUnlockData);
        file.Close();
    }



}
