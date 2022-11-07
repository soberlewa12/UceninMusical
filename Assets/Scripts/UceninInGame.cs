using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UceninInGame : MonoBehaviour
{
    public void setAspecto(string aspecto)
    {
        switch(aspecto)
        {
            case "Estandar":
                Debug.Log("Set Estandar en Ucenin");
                
                break;
                
            case "Especial":
                Debug.Log("Set Especial en Ucenin");
                break;
        }
    }
}
