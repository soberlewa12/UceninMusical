using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boton : MonoBehaviour
{
    
    public abstract void OnUp();

    public void OnEnter()
    {
        MusicController.Instancia.BotonEncima();
    }

}
