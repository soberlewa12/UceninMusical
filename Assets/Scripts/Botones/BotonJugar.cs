using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonJugar : Boton
{
    override
    public void OnUp(){

        MusicController.Instancia.BotonClick();
        SceneManager.LoadScene("Juego");

    }
}
