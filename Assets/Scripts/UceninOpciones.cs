using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UceninOpciones : MonoBehaviour
{

    [SerializeField] private GameObject otherSeleccion;
    [SerializeField] private GameObject Seleccion;
    [SerializeField] private GameObject Ucenin;
    [SerializeField] private GameObject OpcionUceninIncognito;
    [SerializeField] private GameObject OpcionUcenin;

    private bool Exit;
    private bool seleccionado;
    private int contRotaciones;

    private void Start() 
    {
        this.Exit = false;
        contRotaciones = 0;
        seleccionado = false;
        Debug.Log(Ucenin.name);
        Debug.Log(PlayerPrefs.GetString("Skin", "")  + "Skin");

        if(PlayerPrefs.GetString("Skin", "").Equals(Ucenin.name))
        {
            OnClick();
        }

        if(!MenuController.Instancia.getSkinEspecial() && Ucenin.name.Equals("UceninEspecial"))
        {  
            this.gameObject.transform.parent.gameObject.SetActive(false);
            OpcionUceninIncognito.SetActive(true);
        }
        else if(MenuController.Instancia.getSkinEspecial() && Ucenin.name.Equals("UceninEspecial"))
        {
            this.gameObject.transform.parent.gameObject.SetActive(true);
            OpcionUceninIncognito.SetActive(false);
        }
    }

    public void OnEnter()
    {
        this.Exit = false;
        MusicController.Instancia.BotonEncima();
        if(Ucenin.name.Equals("UceninIncognito"))
        {
            StartCoroutine(Rotar());
            return;
        }

        if(!this.Seleccion.activeSelf)
        {
            seleccionado = false;
        }

        StartCoroutine(Rotar());
        Seleccion.SetActive(true);
    }

    public void OnClick()
    {
        if(Ucenin.name.Equals("UceninIncognito"))
        {
            return;
        }

        if(seleccionado)
        {
            PlayerPrefs.SetString("Skin", "");
    
            seleccionado = false;
            this.Seleccion.SetActive(false);
        }
        else
        {
            PlayerPrefs.SetString("Skin", Ucenin.name);
            
            seleccionado = true;
            this.Seleccion.SetActive(true);
            otherSeleccion.SetActive(false);
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
            StartCoroutine(Rotar());            
        }
    }

    public void OnExit()
    {
        Ucenin.transform.Rotate(new Vector3(0, 0, -(contRotaciones*10)));
        this.Exit = true;
        contRotaciones = 0;

        if(Ucenin.name.Equals("UceninIncognito"))
        {
            return;
        }

        if(!seleccionado)
        {
            Seleccion.SetActive(false);
        }
    }
}
