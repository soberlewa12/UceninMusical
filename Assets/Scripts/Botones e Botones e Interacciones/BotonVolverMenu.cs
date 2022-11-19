using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonVolverMenu : Boton
{
    override
    public void OnUp(){

        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        MusicController.Instancia.setLowPassMusic(false);
        MusicController.Instancia.stopUceninFx();
    }
}
