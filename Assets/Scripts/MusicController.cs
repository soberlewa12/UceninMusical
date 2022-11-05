using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour
{
    
    public static MusicController Instancia;


    [SerializeField] private AudioSource FxAudioSource;
    [SerializeField] private List<AudioClip> Fx;

    public void Awake() {
        if(this != null){

            Destroy(this);

        }

        Instancia = this;
        DontDestroyOnLoad(this);

    }

    public void BotonEncima(){

        FxAudioSource.clip = Fx[0];
        FxAudioSource.Play();

    }

    public void BotonSeleccion(){

        FxAudioSource.clip = Fx[1];
        FxAudioSource.Play();

    }

    public void BotonAtras(){

        FxAudioSource.clip = Fx[2];
        FxAudioSource.Play();

    }

}
