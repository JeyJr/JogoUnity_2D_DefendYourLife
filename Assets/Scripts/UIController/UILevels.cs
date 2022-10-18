using UnityEngine;
using UnityEngine.SceneManagement;

public class UILevels : MonoBehaviour
{
    public void LoadScene(int num){
        SceneManager.LoadScene(num);
    }
}
