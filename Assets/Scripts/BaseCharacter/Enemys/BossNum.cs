using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNum : MonoBehaviour
{
    
    public int bossNum;
    public void UnlockNextLevel() {
        LevelUnlockData levelUnlock = GameData.LoadLevelData();

        //12 endLevel
        if(levelUnlock.levelUnlock <= bossNum && bossNum != 5)
        {
            GameData.SaveLevelData(bossNum + 1);
            levelUnlock = GameData.LoadLevelData();
        }
    }
}
