using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNum : MonoBehaviour
{
    public int bossNum;
    public void UnlockNextLevel() => GameData.SaveLevelData(bossNum);
}
