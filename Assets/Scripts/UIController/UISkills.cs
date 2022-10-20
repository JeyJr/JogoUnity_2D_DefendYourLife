using System.Collections;
using System.Collections.Generic;
using System.IO;
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
            SetBtnColorEffect();

            txtTitle.text = "Skills";
            txtSkillPoints.text = $"{lobbyUI.GetSkillPoints()}";
            Debug.Log(lobbyUI.GetUsedSkillsPoints());
        }
    }


    public void AddSkillPoint(int skillNum)
    {
        if (lobbyUI.GetSkillPoints() > 0)
        {
            switch (skillNum)
            {
                case 1:
                    if (lobbyUI.FohLevel < lobbyUI.SkillsMaxLevel)
                        lobbyUI.FohLevel++;
                    break;
                case 2:
                    if (lobbyUI.WsLevel < lobbyUI.SkillsMaxLevel)
                        lobbyUI.WsLevel++;
                    break;
                case 3:
                    if (lobbyUI.BowLevel < lobbyUI.SkillsMaxLevel)
                        lobbyUI.BowLevel++;
                    break;
                case 4:
                    if (lobbyUI.LsLevel < lobbyUI.SkillsMaxLevel)
                        lobbyUI.LsLevel++;
                    break;
                case 5:
                    if (lobbyUI.LkLevel < lobbyUI.SkillsMaxLevel)
                        lobbyUI.LkLevel++;
                    break;
                case 6:
                    if (lobbyUI.ILevel < lobbyUI.SkillsMaxLevel)
                        lobbyUI.ILevel++;
                    break;
            }
            lobbyUI.SaveSkills();
        }
    }

    public void Confirm()
    {
        lobbyUI.SaveSkills();
    }

    public void ResetSkills()
    {
        lobbyUI.FohLevel = 0;
        lobbyUI.WsLevel = 0;
        lobbyUI.BowLevel = 0;
        lobbyUI.LsLevel = 0;
        lobbyUI.LkLevel = 0;
        lobbyUI.ILevel = 0;
        SetSkillsDescription(1);
        lobbyUI.SaveSkills();
    }
    void SetBtnColorEffect()
    {
        if (lobbyUI.FohLevel <= 0)
            btnSkills[0].GetComponent<Image>().color = Color.gray;
        else
            btnSkills[0].GetComponent<Image>().color = Color.white;

        if (lobbyUI.WsLevel <= 0)
            btnSkills[1].GetComponent<Image>().color = Color.gray;
        else
            btnSkills[1].GetComponent<Image>().color = Color.white;

        if (lobbyUI.BowLevel <= 0)
            btnSkills[2].GetComponent<Image>().color = Color.gray;
        else
            btnSkills[2].GetComponent<Image>().color = Color.white;

        if (lobbyUI.LsLevel <= 0)
            btnSkills[3].GetComponent<Image>().color = Color.gray;
        else
            btnSkills[3].GetComponent<Image>().color = Color.white;

        if (lobbyUI.LkLevel <= 0)
            btnSkills[4].GetComponent<Image>().color = Color.gray;
        else
            btnSkills[4].GetComponent<Image>().color = Color.white;

        if (lobbyUI.ILevel <= 0)
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
                Description("Floor of Hell", $"{lobbyUI.FohLevel}/{lobbyUI.SkillsMaxLevel}", $"Every <color=red><b>0.5s burn</b></color> the enemys!");
                break;
            case 2:
                Description("Water spikes", $"{lobbyUI.WsLevel}/{lobbyUI.SkillsMaxLevel}", $"Spawn of the ground, <color=blue>water spikes</color> that pierce enemies!");
                break;
            case 3:
                Description("Blades Of Wind", $"{lobbyUI.BowLevel}/{lobbyUI.SkillsMaxLevel}", $"Generate a blade of <color=green>wind to slash</color> enemies!");
                break;
            case 4:
                Description("Life Steal", $"{lobbyUI.LsLevel}/{lobbyUI.SkillsMaxLevel}", $"Steals an amount of health and mana from enemies!");
                break;
            case 5:
                Description("Lucky", $"{lobbyUI.LkLevel}/{lobbyUI.SkillsMaxLevel}", $"Get an amount of luck!");
                break;
            case 6:
                Description("Invencible", $"{lobbyUI.ILevel}/{lobbyUI.SkillsMaxLevel}", $"Become a god...for a few seconds!");
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
