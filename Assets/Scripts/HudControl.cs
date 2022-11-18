using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HudControl : MonoBehaviour
{
    public Button btnFoh, btnWs, btnBoW, btnLS, btnLuk, btnInv;
    public TextMeshProUGUI txtLife, txtMana, txtLevel, txtExp;
    public Slider liferBar, manaBar, expBar;

    [Space(5)]
    [Header("Get Player")]
    public GameObject player;
    CharacterExpControl cExp;
    CharacterAttributes cAtr;

    [Space(5)]
    [Header("Dead")]
    public GameObject deadPanel;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cAtr = player.GetComponent<CharacterAttributes>();
        cExp = player.GetComponent<CharacterExpControl>();
        BtnSkills();
    }

    private void Update()
    {
        if(cExp.Level < 10)
            txtLevel.text = $"Level: 0{cExp.Level}";
        else
            txtLevel.text = $"Level: {cExp.Level}";

        Life();
        Mana();
        Exp();
        BtnSkills();
    }

    void Life()
    {
        float life = cAtr.Life;
        float maxLife = cAtr.MaxLife;


        liferBar.value = life;
        liferBar.maxValue = maxLife;

        float p = (life / maxLife) * 100;
        txtLife.text = p.ToString("F0") + "%";

        if (cAtr.Dead) deadPanel.SetActive(true);
    }

    void Mana()
    {
        float mana = cAtr.Mana;
        float maxMana = cAtr.MaxMana;

        manaBar.value = mana;
        manaBar.maxValue = maxMana;

        float p = (mana / maxMana) * 100;
        txtMana.text = p.ToString("F0") + "%";
    }

    void Exp()
    {
        float exp = cExp.CurrentExp;
        float maxExp = cExp.NextLevelExp;

        expBar.value = exp;
        expBar.maxValue = maxExp;

        txtExp.text = $"{cExp.CurrentExp} | <b>{cExp.NextLevelExp}</b>";
    }


    # region SKILLS BTN
    void BtnSkills()
    {
        PlayerSkills playerSkills = player.GetComponentInChildren<PlayerSkills>();

        btnFoh.interactable = playerSkills.FloorOfHellLevel > 0 &&
            !playerSkills.FloorOfHellCountdown &&
            cAtr.Mana > playerSkills.FloorOfHellManaCost ? true : false;

        btnWs.interactable = playerSkills.WaterSpikesLevel > 0 && 
            !playerSkills.WaterSpikesCountdown &&
            cAtr.Mana > playerSkills.WaterSpikesManaCost ? true : false;

        btnBoW.interactable = playerSkills.BladesOfWindLevel > 0 && 
            !playerSkills.BladesOfWindCountdown &&
            cAtr.Mana > playerSkills.BladesOfWindManaCost ? true : false;

        btnLS.interactable = playerSkills.LifeStealLevel > 0 && 
            !playerSkills.LifeStealCountdown &&
            cAtr.Mana > playerSkills.LifeStealManaCost ? true : false;

        btnLuk.interactable = playerSkills.LuckyLevel > 0 && 
            !playerSkills.LuckyCountdown &&
            cAtr.Mana > playerSkills.LuckyManaCost ? true : false;

        btnInv.interactable = playerSkills.InvencibleLevel > 0 && 
            !playerSkills.InvencibleCountdown &&
            cAtr.Mana > playerSkills.InvencibleManaCost ? true : false;
    }

    public void UseSkill(bool value) => player.GetComponentInChildren<PlayerInputs>().UseSkill = value;
    #endregion

    #region DeathPanel
    public void Revive() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    public void BackToMainMenu() => SceneManager.LoadScene(0);

    #endregion
}
