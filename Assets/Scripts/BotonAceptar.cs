using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonAceptar : Boton
{

    [SerializeField] private GameObject PanelToClose;

    override
    public void OnUp()
    {
        MusicController.Instancia.BotonClick();
        PanelToClose.SetActive(false);
    }

}
