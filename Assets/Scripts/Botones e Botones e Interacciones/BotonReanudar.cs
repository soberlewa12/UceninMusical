using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonReanudar : Boton
{

    [SerializeField] private GameObject PanelToOpen;
    [SerializeField] private GameObject PanelToClose;

    //Referencia al Back ground de la ARCamera.
    [SerializeField] GameObject BackgroundCamera;
    override
    public void OnUp()
    {
        GameController.Instancia.ResumeGame();
        Time.timeScale = 1f;
        PanelToOpen.SetActive(true);
        PanelToClose.SetActive(false);
    }
}
