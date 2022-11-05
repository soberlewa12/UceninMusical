using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameController : MonoBehaviour
{

    public static NameController Instancia;
    [SerializeField] private TextMeshProUGUI TextAdvertencia;
    [SerializeField] private TextMeshProUGUI TextCuadro;
    private bool NombreIncorrecto;


    private void Start() 
    {
        Instancia = this;
    }

    public void OnValueChanged()
    {
        if((this.TextCuadro.text.Length - 1) >= 15)
        {
            TextAdvertencia.text = "El nombre no puede tener más de 15 caracteres.";
            GameController.Instancia.setNombreIncorrecto(true);
            this.NombreIncorrecto = true;
        }
        else if ((this.TextCuadro.text.Length - 1) <= 3)
        {
            TextAdvertencia.text = "El nombre no puede tener menos de 3 caracteres.";
            GameController.Instancia.setNombreIncorrecto(true);
            this.NombreIncorrecto = true;
        }
        else
        {
            TextAdvertencia.text = "";
            GameController.Instancia.setNombreIncorrecto(false);
            this.NombreIncorrecto = false;
        }
        Debug.Log(this.TextCuadro.text.Length - 1);
    }

    IEnumerator Parpadeo(){

        TextAdvertencia.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        TextAdvertencia.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        TextAdvertencia.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        TextAdvertencia.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        TextAdvertencia.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        TextAdvertencia.gameObject.SetActive(true);

    }

    public void startParpadeo()
    {
        StartCoroutine(Parpadeo());
    }

    public bool getNombreIncorrecto()
    {
        return this.NombreIncorrecto;
    }

}
