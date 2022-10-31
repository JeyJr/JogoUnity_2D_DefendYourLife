using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAttributes : MonoBehaviour
{
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
        PlayerInfo();
        sStr.maxValue = 100;
        sInt.maxValue = 100;
        sVit.maxValue = 100;
        sLuk.maxValue = 100;
    }

    public void AddAttributePoints(string attributeName)
    {
        if (lobbyUI.GetAttributePoints() > 0)
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
        //lobbyUI.SaveAttributes();
        PanelAttributes();
        PlayerInfo();
    }
    public void SubtractAttributePoints(string attributeName)
    {
        if (lobbyUI.GetUsedAttributesPoints() > 0)
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
        PlayerInfo();
        PanelAttributes();
        //lobbyUI.SaveAttributes();
    }

    public void SaveAttributes() => lobbyUI.SaveAttributes();
    public void ResetAttributes(){
        lobbyUI.Str = 1;
        lobbyUI.Inte = 1;
        lobbyUI.Vit = 1;
        lobbyUI.Luk = 1;
        lobbyUI.SaveAttributes();
        PanelAttributes();
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

        txtAttributePoints.text = lobbyUI.GetAttributePoints().ToString();
        
    }
    void PlayerInfo()
    {
        txtLevel.text = $"Level: {lobbyUI.Level}";
        lobbyUI.SetPlayerInfo();

        txtPhysicalAtkValue.text = lobbyUI.Physical.ToString();
        txtMagicAtkValue.text = lobbyUI.Magical.ToString();
        txtMaxLifeValue.text = lobbyUI.MaxLife.ToString();
        txtMaxManaValue.text = lobbyUI.MaxMana.ToString();
        txtCriticalRateValue.text = lobbyUI.CriticalRate.ToString();
    }
}
