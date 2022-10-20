using System;

[Serializable]
public class PlayerData
{
    public int level;
    public int currentExp;
    public int nextLevelExp;

    public int str;
    public int inte;
    public int vit;
    public int luk;
}

[Serializable]
public class PlayerSkillsData
{
    public int skillLevel;
    public int currentSkillExp;
    public int nextSkillLevelExp;

    public int fohLevel;
    public int wsLevel;
    public int bowLevel;
    public int lsLevel;
    public int lkLevel;
    public int iLevel;
}
