using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPlayerInfo : MonoBehaviour
{
    [SerializeField] private LobbyUIController lobbyUIController;

    [SerializeField] private TextMeshProUGUI txtLevel;
    [SerializeField] private TextMeshProUGUI txtPhysicalAtkValue;
    [SerializeField] private TextMeshProUGUI txtMagicAtkValue;
    [SerializeField] private TextMeshProUGUI txtMaxLifeValue;
    [SerializeField] private TextMeshProUGUI txtMaxManaValue;
    [SerializeField] private TextMeshProUGUI txtCriticalRateValue;

    private void Update()
    {
        TextPlayerInfo();
    }
    void TextPlayerInfo()
    {
        txtLevel.text = $"Level {lobbyUIController.Level}";
        txtPhysicalAtkValue.text = Mathf.RoundToInt(lobbyUIController.Str * 1.5f).ToString();
        txtMagicAtkValue.text = Mathf.RoundToInt(lobbyUIController.Inte * 3.5f).ToString();
        txtMaxLifeValue.text = Mathf.RoundToInt(lobbyUIController.Vit * 50).ToString();
        txtMaxManaValue.text = Mathf.RoundToInt((lobbyUIController.Inte * 4.5f) + 50).ToString();
        txtCriticalRateValue.text = Mathf.RoundToInt(lobbyUIController.Luk / 2).ToString();
    }
}
