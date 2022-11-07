using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour
{
    
    public static MusicController Instancia;

    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioSource FxAudioSource;
    [SerializeField] private List<AudioClip> Fx;
    [SerializeField] private List<AudioClip> FxUcenin;

    public void Awake() 
    {
        if(Instancia != null)
        {  
            Destroy(this.gameObject);
        }
        else
        {
            Instancia = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start() 
    {    
        mixer.SetFloat("MixerMaster", (PlayerPrefs.GetFloat("volumenMixer")*83 - 80));
    }

    //Sonido cuando pasamos el cursor por encima
    public void BotonEncima()
    {
        this.FxAudioSource.clip = Fx[0];
        this.FxAudioSource.Play();
    }

    //Sonido cuando hacemos click en un boton
    public void BotonClick()
    {
        this.FxAudioSource.clip = Fx[1];
        this.FxAudioSource.Play();
    }

    //Sonido cuando hacemos click en un boton que nos hace retroceder de un panel a otro.
    public void BotonAtras()
    {
        this.FxAudioSource.clip = Fx[2];
        this.FxAudioSource.Play();
    }

    public void ReproducirUcenin(int index)
    {
        this.FxAudioSource.clip = FxUcenin[index];
        this.FxAudioSource.Play();
    }

    public void setLowPassMusic(bool Activo)
    {
        if(Activo)
        {  
            mixer.SetFloat("LowPassMusic", 1000.00f);
        }
        else
        {
            mixer.SetFloat("LowPassMusic", 22000.00f);
        }
    }

}
