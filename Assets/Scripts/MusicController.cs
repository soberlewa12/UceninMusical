using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour
{
    
    public static MusicController Instancia;

    [SerializeField] private AudioMixer mixer;
    [SerializeField] public AudioSource MusicAudioSource;
    [SerializeField] public AudioSource FxAudioSource;
    [SerializeField] public AudioSource FxAudioSourceUcenin;
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
        }
        CambiarVolumen(PlayerPrefs.GetFloat("Slider", 0.0f), true);
    }

    public void CambiarVolumen(float volumen, bool CambiarSlider)
    {
        Debug.Log("CambiarSlider: " + CambiarSlider + " Valor: " + volumen);
        if(CambiarSlider)
        {   
            Debug.Log("Entrando a cambiar slider");
            PlayerPrefs.SetFloat("Slider", volumen);
        } 
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

    public void PauseAudioSource(AudioSource audioSoruce)
    {
        audioSoruce.Pause();
    }

    public void UnpauseAudioSource(AudioSource audioSource)
    {
        audioSource.UnPause();
        //FadeInMusic(PlayerPrefs.GetFloat("Slider", 0.0f), PlayerPrefs.GetFloat("Slider", 0.0f)/10);
    }

    public void FadeInMusicVoid()
    {

        StartCoroutine(FadeInMusic(PlayerPrefs.GetFloat("Slider", 0)/10, PlayerPrefs.GetFloat("Slider", 0), PlayerPrefs.GetFloat("Slider", 0)/10, 1));
    }

    public IEnumerator FadeInMusic(float divisiones, float originalSlider,float originalDivision, int contDivision)
    {
        Debug.Log("Division: " + divisiones + " player Slider: " + PlayerPrefs.GetFloat("Slider") + " slider: " + originalSlider);
        contDivision++;
        CambiarVolumen(divisiones, false);
        Debug.Log("Antes ");
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Despues ");
        if(originalSlider > divisiones)
        {
            Debug.Log("Iniciando");
            StartCoroutine(FadeInMusic((originalDivision*contDivision), originalSlider, originalDivision, contDivision));
        }
        else
        {
            Debug.Log("Entrando");
            CambiarVolumen(originalSlider, false);
        }
    }
}
