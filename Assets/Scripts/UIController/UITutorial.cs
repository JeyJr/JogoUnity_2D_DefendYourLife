using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITutorial : MonoBehaviour
{
    [SerializeField] private GameObject panelTutorial;

    public List<GameObject> panels;

    private void Start()
    {
        panelTutorial.SetActive(false);
    }

    public void EnableTutorial() => panelTutorial.SetActive(!panelTutorial.activeSelf);

    public void EnablePanels(int panelNum)
    {
        for(int i = 0; i < panels.Count; i++)
        {
            if (i == panelNum)
                panels[i].SetActive(true);
            else
                panels[i].SetActive(false);
        }
    }
}
