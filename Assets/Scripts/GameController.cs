using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    
    public static GameController Instancia;
    private bool NombreIncorrecto;
    private bool SkinEspecial;

    private void Awake() 
    {
        
        if(Instancia != null){

            Destroy(this.gameObject);

        }
        else
        {
            Instancia = this;
            DontDestroyOnLoad(this);

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
