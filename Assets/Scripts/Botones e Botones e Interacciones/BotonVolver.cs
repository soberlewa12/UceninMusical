using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BotonVolver : Boton
{

    
    [SerializeField] private GameObject PanelToOpen;
    [SerializeField] private GameObject PanelToClose;

    override
    public void OnUp(){

        MusicController.Instancia.BotonAtras();
        if(PanelToClose.name.Equals("CreditosPanel"))
        {
                MusicController.Instancia.CambiarMusica(0);
                PanelToOpen.SetActive(true);
                PanelToClose.SetActive(false);
        }
        else if(NameController.Instancia.getNombreIncorrecto())
        {
            NameController.Instancia.startParpadeo();
        }else
        {
            PanelToOpen.SetActive(true);
            PanelToClose.SetActive(false);
        }
    }
}
