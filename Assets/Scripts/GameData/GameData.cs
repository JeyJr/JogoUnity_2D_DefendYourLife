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
        FileStream file = File.Create(Application.persistentDataPath + "/sData.data");
        bf.Serialize(file, playerData);
        file.Close();

        Debug.Log($"File saved: Level: {playerData.level} | CurrentExp: {playerData.currentExp} | NextLevel: {playerData.nextLevelExp}");
    }    


    public static PlayerData LoadData()
    {
        BinaryFormatter bf = new();
        FileStream file = File.Open(Application.persistentDataPath + "/sData.data", FileMode.Open);
        PlayerData playerData = (PlayerData)bf.Deserialize(file);
        file.Close();

        Debug.Log("FileLoaded!");
        return playerData;
    }


    public static void CreateFile()
    {
        PlayerData playerData = new PlayerData();
        BinaryFormatter bf = new();
        FileStream file = File.Create(Application.persistentDataPath + "/sData.data");
        bf.Serialize(file, playerData);
        file.Close();
    }
}
