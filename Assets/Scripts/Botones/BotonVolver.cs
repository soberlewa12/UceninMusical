using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonVolver : Boton
{

    
    [SerializeField] private GameObject PanelToOpen;
    [SerializeField] private GameObject PanelToClose;

    override
    public void OnUp(){

        MusicController.Instancia.BotonAtras();
        PanelToOpen.SetActive(true);
        PanelToClose.SetActive(false);

    }

}
