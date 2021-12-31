using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenButtonHandler : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject start;
    public Text Textfield;

    private int hours;

    public void StartSimulation()
    {
        if (int.TryParse(Textfield.text, out hours))
        {
            start.GetComponent<Simulation>().StartSimulation(hours);
            ClosePanel(startScreen);
        }
    }
    
    public void ClosePanel(GameObject panel)
    {
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }
}
