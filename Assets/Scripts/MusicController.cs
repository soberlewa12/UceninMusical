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

    private float VolumenMusic;
    private bool FadeInMusicIsOn;
    private bool FadeOutMusicIsOn;

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
        VolumenMusic = PlayerPrefs.GetFloat("Slider", 0.0f);
        CambiarVolumenMixer(PlayerPrefs.GetFloat("Slider", 0.0f), true);
    }

    public void CambiarVolumenMixer(float volumen, bool CambiarSlider)
    {
        Debug.Log("CambiarSlider: " + CambiarSlider + " Valor: " + volumen);
        if(CambiarSlider)
        {   
            Debug.Log("Entrando a cambiar slider");
            PlayerPrefs.SetFloat("Slider", volumen);
        } 
        mixer.SetFloat("MixerMaster", volumen*83- 80);
    }

    public void CambiarVolumenMusic(float volumen)
    {
        Debug.Log("volumeen de audioSource musica" + MusicAudioSource.volume);
        VolumenMusic = volumen;
        mixer.SetFloat("MixerMusic", VolumenMusic*83 - 80);
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

    public void UnpauseAudioSource(AudioSource audioSource, bool mode)
    {
        if(mode)
        {
            FadeInMusicIsOn = true;
        }
        else
        {
            FadeOutMusicIsOn = true;
        }
        FadeInMusicVoid(mode);
        StartCoroutine(espera(audioSource));
        //FadeInMusic(PlayerPrefs.GetFloat("Slider", 0.0f), PlayerPrefs.GetFloat("Slider", 0.0f)/10);
    }
    public void stopUceninFx()
    {
        FxAudioSourceUcenin.Stop();
    }

    public void FadeInMusicVoid(bool mode)
    {
        StartCoroutine(Fade(VolumenMusic/10, VolumenMusic, VolumenMusic/10, 0, mode));
    }

    public IEnumerator espera(AudioSource audioSource)
    {
        CambiarVolumenMixer(0, false);
        yield return new WaitForSeconds(0.1f);
        audioSource.UnPause();
        CambiarVolumenMixer(PlayerPrefs.GetFloat("Slider", 0f), false);
    }

    public IEnumerator Fade(float divisiones, float originalSlider,float originalDivision, int contDivision, bool mode)
    {
        float volumen = mode ? divisiones : originalSlider - divisiones;

        Debug.Log("Division: " + divisiones + " player Slider: " + PlayerPrefs.GetFloat("Slider") + " slider: " + originalSlider);
        contDivision++;
        CambiarVolumenMusic(volumen);
        Debug.Log("Antes ");
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Despues ");
        if(originalSlider > divisiones)
        {
            Debug.Log("Iniciando");
            StartCoroutine(Fade((originalDivision*contDivision), originalSlider, originalDivision, contDivision, true));
        }
        else
        {
            if(mode)
            {
                FadeOutMusicIsOn = false;
            }
            else
            {
                FadeInMusicIsOn = true;
            }
            Debug.Log("Entrando");
            CambiarVolumenMusic(originalSlider);
        }
    }
}
