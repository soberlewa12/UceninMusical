using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UceninOpciones : MonoBehaviour
{

    [SerializeField] private GameObject otherSeleccion;
    [SerializeField] private GameObject SeleccionPanel;
    [SerializeField] private GameObject ClickPanel;
    [SerializeField] private GameObject Ucenin;
    [SerializeField] private GameObject PanelDesconocida;
    [SerializeField] private GameObject UceninEspecial;

    private bool Exit;
    private bool seleccionado;
    private int contRotaciones;

    private bool InteraccionActiva;

    private void Start() 
    {
        Debug.Log(PlayerPrefs.GetInt("SkinDesbloqueada", 0));
        if(PlayerPrefs.GetInt("SkinDesbloqueada", 0) == 1)
        {
            if(Ucenin.Equals("UceninIncognito"))
            {
                UceninEspecial.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }

        this.Exit = false;
        contRotaciones = 0;
        seleccionado = false;

        if(PlayerPrefs.GetString("Skin", "").Equals(Ucenin.name))
        {
            OnClick();
        }
        else
        {
            OnExit();
        }
    }

    public void OnEnter()
    {
        Debug.Log("On Enter");
        this.Exit = false;
        MusicController.Instancia.BotonEncima();
        if(Ucenin.name.Equals("UceninIncognito"))
        {
            PanelDesconocida.SetActive(true);
            StartCoroutine(Rotar());
            return;
        }

        if(!this.ClickPanel.activeSelf)
        {
            seleccionado = false;
        }
        else
        {
            SeleccionPanel.SetActive(true);
        }

        StartCoroutine(Rotar());
    }

    public void OnClick()
    {
        if(Ucenin.name.Equals("UceninIncognito"))
        {
            MusicController.Instancia.BotonAtras();
            return;
        }

        if(seleccionado)
        {
            PlayerPrefs.SetString("Skin", "");
    
            seleccionado = false;
            
            this.ClickPanel.SetActive(false);
            this.SeleccionPanel.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetString("Skin", Ucenin.name);
            
            seleccionado = true;
            this.ClickPanel.SetActive(true);
            otherSeleccion.SetActive(false);
            this.SeleccionPanel.SetActive(false);
        }
        MusicController.Instancia.BotonClick();
    }

    IEnumerator Rotar()
    {
        contRotaciones++;
        Ucenin.transform.Rotate(new Vector3(0, 0, 10));
        yield return new WaitForSeconds(0.04f);     
        if(!this.Exit)
        {
            Debug.Log("Rotando Nuevamente");
            StartCoroutine(Rotar());            
        }
    }

    public void OnExit()
    {
        Debug.Log("On Exit");
        Ucenin.transform.Rotate(new Vector3(0, 0, -(contRotaciones*10)));
        this.Exit = true;
        contRotaciones = 0;

        if(Ucenin.name.Equals("UceninIncognito"))
        {
            PanelDesconocida.SetActive(false);
            return;
        }

        if(!seleccionado)
        {
            SeleccionPanel.SetActive(false);
        }
    }
}
