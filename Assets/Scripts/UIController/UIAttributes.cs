using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAttributes : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private LobbyUIController lobbyUI;

    [Header("TextMeshPro")]
    [SerializeField] private TextMeshProUGUI txtTitle;
    [SerializeField] private TextMeshProUGUI txtAttributePoints;
    [SerializeField] private TextMeshProUGUI txtStrValue;
    [SerializeField] private TextMeshProUGUI txtIntValue;
    [SerializeField] private TextMeshProUGUI txtVitValue;
    [SerializeField] private TextMeshProUGUI txtLukValue;

    [Header("Sliders")]
    [SerializeField] private Slider sStr;
    [SerializeField] private Slider sInt;
    [SerializeField] private Slider sVit;
    [SerializeField] private Slider sLuk;


    [Header("PlayerInfo")]
    [SerializeField] private TextMeshProUGUI txtLevel;
    [SerializeField] private TextMeshProUGUI txtPhysicalAtkValue;
    [SerializeField] private TextMeshProUGUI txtMagicAtkValue;
    [SerializeField] private TextMeshProUGUI txtMaxLifeValue;
    [SerializeField] private TextMeshProUGUI txtMaxManaValue;
    [SerializeField] private TextMeshProUGUI txtCriticalRateValue;

    private void Start()
    {
        PanelAttributes();
        sStr.maxValue = 100;
        sInt.maxValue = 100;
        sVit.maxValue = 100;
        sLuk.maxValue = 100;
    }


    public void AddAttributePoints(string attributeName)
    {
        if (lobbyUI.GetAttributePointsRealValue() > 0)
        {
            switch (attributeName)
            {
                case "Str":
                    if (lobbyUI.Str < 100)
                    {
                        lobbyUI.Str++;
                    }
                    break;
                case "Int":
                    if (lobbyUI.Inte < 100)
                    {
                        lobbyUI.Inte++;
                    }
                    break;
                case "Vit":
                    if (lobbyUI.Vit < 100)
                    {
                        lobbyUI.Vit++;
                    }
                    break;
                case "Luk":
                    if (lobbyUI.Luk < 100)
                    {
                        lobbyUI.Luk++;
                    }
                    break;
            }
        }
        Debug.Log(lobbyUI.GetAttributePointsRealValue());
        Debug.Log(lobbyUI.GetUsedPointsRealValue());
        lobbyUI.SaveAttributes();
    }
    public void SubtractAttributePoints(string attributeName)
    {
        if (lobbyUI.GetUsedPointsRealValue() > 0)
        {
            switch (attributeName)
            {
                case "Str":
                    if (lobbyUI.Str > 1)
                    {
                        lobbyUI.Str--;
                    }
                    break;
                case "Int":
                    if (lobbyUI.Inte > 1)
                    {
                        lobbyUI.Inte--;
                    }
                    break;
                case "Vit":
                    if (lobbyUI.Vit > 1)
                    {
                        lobbyUI.Vit--;
                    }
                    break;
                case "Luk":
                    if (lobbyUI.Luk > 1)
                    {
                        lobbyUI.Luk--;
                    }
                    break;
            }
        }
        Debug.Log(lobbyUI.GetAttributePointsRealValue());
        Debug.Log(lobbyUI.GetUsedPointsRealValue());
        lobbyUI.SaveAttributes();
    }


    private void Update()
    {
        if (panel.activeSelf)
        {
            PanelAttributes();
        }

        //Status
        txtLevel.text = $"Level: {lobbyUI.Level}";
    }

    void PanelAttributes()
    {
        txtTitle.text = "Attributes";
        txtStrValue.text = lobbyUI.Str.ToString();
        txtIntValue.text = lobbyUI.Inte.ToString();
        txtVitValue.text = lobbyUI.Vit.ToString();
        txtLukValue.text = lobbyUI.Luk.ToString();

        sStr.value = lobbyUI.Str;
        sInt.value = lobbyUI.Inte;
        sVit.value = lobbyUI.Vit;
        sLuk.value = lobbyUI.Luk;

        txtAttributePoints.text = lobbyUI.GetAttributePointsRealValue().ToString();
        
    }


    void PlayerInfo()
    {

    }

    //void UpdatePlayerInfo()
    //{
    //    PlayerData playerData = GameData.LoadData();

    //    AttributePointsControl();
    //    
    //    
    //    
    //    txtVitValue.text = playerData.vit.ToString();
    //    
    //    ;



    //    
    //    txtPhysicalAtkValue.text = playerData.physical.ToString();
    //    txtMagicAtkValue.text = playerData.magical.ToString();
    //    txtMaxLifeValue.text = playerData.maxLife.ToString();
    //    txtMaxManaValue.text = playerData.maxMana.ToString();
    //    txtCriticalRateValue.text = playerData.criticalRate.ToString();
    //}



}
