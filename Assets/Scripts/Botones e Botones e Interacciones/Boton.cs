using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Boton : MonoBehaviour
{
    private Color32 colorAux;

    public abstract void OnUp();

    private void Start() 
    {
        colorAux = this.GetComponentInChildren<TextMeshProUGUI>().color; 
    }

    private void OnDisable() 
    {
        this.GetComponentInChildren<TextMeshProUGUI>().color = colorAux;
    }

    public void OnEnter()
    {
        this.GetComponentInChildren<TextMeshProUGUI>().color = new Color32(148, 211, 243, 255);
        MusicController.Instancia.BotonEncima();
    }

    public void OnExit()
    {
        this.GetComponentInChildren<TextMeshProUGUI>().color = colorAux;
    }

}
