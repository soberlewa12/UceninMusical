using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonSalir : Boton
{
    override
    public void OnUp(){

        MusicController.Instancia.BotonAtras();
        Application.Quit();

    }

}
