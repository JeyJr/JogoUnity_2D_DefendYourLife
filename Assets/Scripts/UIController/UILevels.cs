using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILevels : MonoBehaviour
{
    public void LoadScene(int num){
        SceneManager.LoadScene($"Level{num}");
    }
}
