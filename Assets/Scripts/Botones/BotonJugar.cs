using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BotonJugar : Boton
{

    [SerializeField] private GameObject PanelEscogerSkin;
    [SerializeField] GameObject Botones;

    override
    public void OnUp(){

        if(PlayerPrefs.GetString("Skin", "").Equals(""))
        {
            Botones.SetActive(false);
            MusicController.Instancia.setLowPassMusic(true);
            PanelEscogerSkin.SetActive(true);
        }
        else
        {
            MusicController.Instancia.BotonClick();
            SceneManager.LoadScene("Juego");
        }
    }
}
