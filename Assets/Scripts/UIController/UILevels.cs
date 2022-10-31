using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILevels : MonoBehaviour
{

    //Show title 
    public TextMeshProUGUI levelPanelTitle;

    public void SetTitleLevelPanel()
    {
        levelPanelTitle.text = "Level";
    }

    public void LoadScene(int num){
        SceneManager.LoadScene($"Level{num}");
    }
}
