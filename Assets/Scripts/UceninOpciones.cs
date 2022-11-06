using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UceninOpciones : MonoBehaviour
{

    private bool Exit;
    private int contRotaciones;

    [SerializeField] private GameObject Ucenin;

    private void Start() 
    {
        this.Exit = false;
        contRotaciones = 0;
    }

    public void OnEnter()
    {
        MusicController.Instancia.BotonEncima();
        this.Exit = false;
        Debug.Log("On enter");
        StartCoroutine(Rotar());
    }

    public void OnClick()
    {
        MusicController.Instancia.BotonClick();
    }

    IEnumerator Rotar()
    {
        contRotaciones++;
        Ucenin.transform.Rotate(new Vector3(0, 0, 10));
        yield return new WaitForSeconds(0.025f);
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
    }

}
