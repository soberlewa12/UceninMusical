using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonAceptar : Boton
{

    [SerializeField] private GameObject PanelToClose;
    [SerializeField] GameObject Botones;

    override
    public void OnUp()
    {
        MusicController.Instancia.setLowPassMusic(false);
        MusicController.Instancia.BotonClick();
        Botones.SetActive(true);
        PanelToClose.SetActive(false);
    }

}
