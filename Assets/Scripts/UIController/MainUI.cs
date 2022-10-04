using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{

    public void BtnClose(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
}
