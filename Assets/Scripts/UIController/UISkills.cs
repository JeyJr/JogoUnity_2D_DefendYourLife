using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISkills : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private LobbyUIController lobbyUI;


    [Header("TextMeshPro")]
    [SerializeField] private TextMeshProUGUI txtTitle;
    [SerializeField] private TextMeshProUGUI txtSkillPoints;

    [Header("Skill Points Control")]
    [SerializeField] private int skillPoints;
    [SerializeField] private int usedSkillPoints;


    [Header("Skills level")]
    [SerializeField] private int skillsMaxLevel = 10;
    [SerializeField] private int fohLevel, wsLevel, bowLevel, lsLevel, lkLevel, iLevel;

    [Header("Skills Btn")]
    public List<GameObject> btnSkills;

    [Header("Skill description")]
    [SerializeField] private TextMeshProUGUI txtSkillName;
    [SerializeField] private TextMeshProUGUI txtSkillLevel;
    [SerializeField] private TextMeshProUGUI txtDescription;

    private void Start()
    {
        SetSkillsDescription(1);
    }

    private void Update()
    {
        if(panel.activeSelf == true)
        {
            SkillPoints();
            SetBtnColorEffect();

            txtTitle.text = "Skills";
            txtSkillPoints.text = $"{skillPoints}";
        }
    }
    void SkillPoints()
    {
        usedSkillPoints = fohLevel + wsLevel + bowLevel + lsLevel + lkLevel + iLevel;

        if (lobbyUI.Level % 2 == 0)
        {
            skillPoints = (lobbyUI.Level / 2) - usedSkillPoints;
        }
    }
    public void AddSkillPoint(int skillNum)
    {
        if(skillPoints > 0)
        {
            switch (skillNum)
            {
                case 1:
                    if(fohLevel < skillsMaxLevel)
                        fohLevel++;
                    break;
                case 2:
                    if(wsLevel < skillsMaxLevel)
                        wsLevel++;
                    break;
                case 3:
                    if(bowLevel < skillsMaxLevel)
                        bowLevel++;
                    break; 
                case 4:
                    if(lsLevel < skillsMaxLevel)
                        lsLevel++;
                    break;
                case 5:
                    if(lkLevel < skillsMaxLevel)
                        lkLevel++;
                    break; 
                case 6:
                    if(iLevel < skillsMaxLevel)
                        iLevel++;
                    break;
            }
        }
    }
    public void ResetSkills()
    {
        fohLevel = 0;
        wsLevel = 0;
        bowLevel = 0;
        lsLevel = 0;
        lkLevel = 0;
        iLevel = 0;
        SetSkillsDescription(1);
    }
    public void SaveSkills()
    {
        PlayerPrefs.SetInt("fohLevel", fohLevel);
        PlayerPrefs.SetInt("wsLevel", wsLevel);
        PlayerPrefs.SetInt("bowLevel", bowLevel);
        PlayerPrefs.SetInt("lsLevel", lsLevel);
        PlayerPrefs.SetInt("lkLevel", lkLevel);
        PlayerPrefs.SetInt("iLevel", iLevel);
    }
    void SetBtnColorEffect()
    {

        if (fohLevel <= 0)
            btnSkills[0].GetComponent<Image>().color = Color.gray;
        else
            btnSkills[0].GetComponent<Image>().color = Color.white;
        
        if (wsLevel <= 0)
            btnSkills[1].GetComponent<Image>().color = Color.gray;
        else
            btnSkills[1].GetComponent<Image>().color = Color.white;
        
        if (bowLevel <= 0)
            btnSkills[2].GetComponent<Image>().color = Color.gray;
        else
            btnSkills[2].GetComponent<Image>().color = Color.white;
        
        if (lsLevel <= 0)
            btnSkills[3].GetComponent<Image>().color = Color.gray;
        else
            btnSkills[3].GetComponent<Image>().color = Color.white;
        
        if (lkLevel <= 0)
            btnSkills[4].GetComponent<Image>().color = Color.gray;
        else
            btnSkills[4].GetComponent<Image>().color = Color.white;
        
        if (iLevel <= 0)
            btnSkills[5].GetComponent<Image>().color = Color.gray;
        else
            btnSkills[5].GetComponent<Image>().color = Color.white;
    }

    //Description--------------------------
    public void SetSkillsDescription(int skillNum)
    {
        switch (skillNum)
        {
            case 1:
                Description("Floor of Hell", $"{fohLevel}/{skillsMaxLevel}", $"Every 0.5s burn the enemys!");
                break;
            case 2:
                Description("Water spikes", $"{wsLevel}/{skillsMaxLevel}", $"Spawn of the ground, water spikes that pierce enemies!");
                break;
            case 3:
                Description("Blades Of Wind", $"{bowLevel}/{skillsMaxLevel}", $"Generate a blade of wind to slash enemies!");
                break;
            case 4:
                Description("Life Steal", $"{lsLevel}/{skillsMaxLevel}", $"Steals an amount of health and mana from enemies!");
                break;
            case 5:
                Description("Lucky", $"{lkLevel}/{skillsMaxLevel}", $"Get an amount of luck!");
                break;
            case 6:
                Description("Invencible", $"{iLevel}/{skillsMaxLevel}", $"Become a god...for a few seconds!");
                break;
        }
    }
    void Description(string sName, string sLevel, string sDescription)
    {
        txtSkillName.text = sName;
        txtSkillLevel.text = sLevel;
        txtDescription.text = sDescription;
    }
}
