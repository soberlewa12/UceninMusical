using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonOpciones : Boton
{

    
    [SerializeField] private GameObject PanelToOpen;
    [SerializeField] private GameObject PanelToClose;

    override
    public void OnUp(){

        MusicController.Instancia.BotonSeleccion();
        PanelToOpen.SetActive(true);
        PanelToClose.SetActive(false);

    }

}
