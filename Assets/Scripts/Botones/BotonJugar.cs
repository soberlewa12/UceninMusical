using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BotonJugar : Boton
{

    [SerializeField] private GameObject PanelEscogerSkin;

    override
    public void OnUp(){

        if(PlayerPrefs.GetString("Skin", "").Equals(""))
        {
            PanelEscogerSkin.SetActive(true);
        }
        else
        {
            MusicController.Instancia.BotonClick();
            SceneManager.LoadScene("Juego");
        }
    }
}
