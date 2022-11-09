using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameController : MonoBehaviour
{

    public static NameController Instancia;
    [SerializeField] private TextMeshProUGUI TextAdvertencia;
    [SerializeField] private TMP_InputField TextCuadro;
    [SerializeField] private TextMeshProUGUI PlaceHolder;
    private bool NombreIncorrecto;


    private void Start() 
    {

        TextCuadro.text = PlayerPrefs.GetString("Nombre");
        Instancia = this;

    }
    private void OnEnable() 
    {
        TextCuadro.text = PlayerPrefs.GetString("Nombre");
    }

    public void OnValueChanged()
    {
        if((this.TextCuadro.text.Length) >= 15)
        {
            Debug.Log(" + 15");
            TextAdvertencia.text = "El nombre no puede tener más de 15 caracteres.";
            //MenuController.Instancia.setNombreIncorrecto(true);
            this.NombreIncorrecto = true;
        }
        else if ((this.TextCuadro.text.Length) <= 3)
        {
            Debug.Log(" -3");
            TextAdvertencia.text = "El nombre no puede tener menos de 3 caracteres.";
            //MenuController.Instancia.setNombreIncorrecto(true);
            this.NombreIncorrecto = true;
        }
        else
        {
            Debug.Log(" Bien");
            TextAdvertencia.text = "";
            //MenuController.Instancia.setNombreIncorrecto(false);
            PlayerPrefs.SetString("Nombre", this.TextCuadro.text);
            this.NombreIncorrecto = false;
        }
    }

    IEnumerator Parpadeo()
    {
        for(int c = 0; c < 6; c++)
        {
            TextAdvertencia.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            TextAdvertencia.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
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
