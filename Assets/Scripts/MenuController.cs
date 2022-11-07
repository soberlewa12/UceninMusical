using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    
    [SerializeField] GameObject OpcionUceninEspecial; 
    [SerializeField] GameObject OpcionUceninIncognito; 
    public static MenuController Instancia;
    private bool NombreIncorrecto;
    private bool SkinEspecial;

    private void Awake() 
    {
        
        if(Instancia != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instancia = this;
        }
    }
    private void Start() 
    {
        if(PlayerPrefs.GetInt("SkinDesbloqueada", 0) == 1)
        {  
            OpcionUceninEspecial.SetActive(true);
            OpcionUceninIncognito.SetActive(false);
        }
        else if(PlayerPrefs.GetInt("SkinDesbloqueada", 0) == 0)
        {
            OpcionUceninEspecial.SetActive(false);
            OpcionUceninIncognito.SetActive(true);
        }
              
    }

    public void setNombreIncorrecto(bool NombreIncorrecto)
    {
        this.NombreIncorrecto = NombreIncorrecto;
    }

    public bool getNombreIncorrecto()
    {
        return this.NombreIncorrecto;
    }

    public void setSkinEspecial(bool SkinEspecial)
    {
        this.SkinEspecial = SkinEspecial;
    }

    public bool getSkinEspecial()
    {
        return this.SkinEspecial;
    }
}
