using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToLobby : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        //ExibirMensagemParaVoltar

        if (Input.GetKeyDown(KeyCode.E) && collision.gameObject.layer == 6)
        {
            SceneManager.LoadScene(0);
        }
    }
}
