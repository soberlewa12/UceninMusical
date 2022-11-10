using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonPausa : Boton
{

    [SerializeField] private GameObject PanelToOpen;
    [SerializeField] private GameObject PanelToClose;

    override
    public void OnUp()
    {
        //Time.timeScale = 0f;
        GameController.Instancia.StopGame();
        PanelToOpen.SetActive(true);
        PanelToClose.SetActive(false);
    }
}
