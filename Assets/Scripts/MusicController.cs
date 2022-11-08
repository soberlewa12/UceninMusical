using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour
{
    
    public static MusicController Instancia;

    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioSource MusicAudioSource;
    [SerializeField] private AudioSource FxAudioSource;
    [SerializeField] private AudioSource FxAudioSourceUcenin;
    [SerializeField] private List<AudioClip> Fx;
    [SerializeField] private List<AudioClip> FxUcenin;

    public void Awake() 
    {
        Debug.Log("Awake Music Controller");
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
        if(SceneManager.GetActiveScene().name.Equals("MainMenu"))
        {
            Debug.Log("Entrando a escena Menu");
            MusicAudioSource.Play();
            FxAudioSourceUcenin.Stop();
        }
        else
        {   
            Debug.Log("Entrando a escena Juego");
            MusicAudioSource.Stop();
            StartCoroutine(FadeInMusic(PlayerPrefs.GetFloat("Slider", 0.0f)/10));
        }
        CambiarVolumen(PlayerPrefs.GetFloat("Slider", 0.0f));
    }

    public void CambiarVolumen(float volumen)
    {
        PlayerPrefs.SetFloat("Slider", volumen);
        mixer.SetFloat("MixerMaster", volumen*83- 80);
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

    public void ReproducirUceninSonido(int index)
    {
        this.FxAudioSourceUcenin.clip = FxUcenin[index];
        this.FxAudioSourceUcenin.Play();
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

    public void stopMusic()
    {
        MusicAudioSource.Stop();
    }

    public void PlayMusic()
    {
        FxAudioSourceUcenin.Stop();
        MusicAudioSource.Play();
    }

    IEnumerator FadeInMusic(float divisiones)
    {
        CambiarVolumen(divisiones);
        yield return new WaitForSeconds(0.1f);
        if(PlayerPrefs.GetFloat("Slider", 0.0f) < divisiones*10)
        {
            FadeInMusic(divisiones + divisiones);
        }
        else
        {
            CambiarVolumen(divisiones*10);
        }
    }
}
