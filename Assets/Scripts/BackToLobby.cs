using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToLobby : MonoBehaviour
{
    public GameObject canvas;

    private void Awake()
    {
        canvas.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //ExibirMensagemParaVoltar
        if (collision.gameObject.layer == 6) {
            canvas.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
                SceneManager.LoadScene(0);
        } 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            canvas.SetActive(false);
        }
    }
}
