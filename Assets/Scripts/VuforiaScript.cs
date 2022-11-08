using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VuforiaScript : MonoBehaviour
{
    public void OnTargetFound()
    {
        MusicController.Instancia.ReproducirUceninSonido(7);
        Destroy(this.gameObject.GetComponent<VuforiaScript>());
    }
}
