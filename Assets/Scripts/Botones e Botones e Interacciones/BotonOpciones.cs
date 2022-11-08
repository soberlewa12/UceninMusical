using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonOpciones : Boton
{

    
    [SerializeField] private GameObject PanelToOpen;
    [SerializeField] private GameObject PanelToClose;

    override
    public void OnUp()
    {
        MusicController.Instancia.BotonClick();
        PanelToOpen.SetActive(true);
        PanelToClose.SetActive(false);
    }

}
