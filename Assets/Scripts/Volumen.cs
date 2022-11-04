using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volumen : MonoBehaviour
{
    public Slider slider;
    public float sliderValue;
    public Image imageMute;
    // Start is called before the first frame update
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        AudioListener.volume = slider.value;
        MuteCheck();
        
    }

    public void ChangeSlider(float valor) 
    {
        sliderValue = valor;
        PlayerPrefs.SetFloat("volumenAudio", sliderValue);
        AudioListener.volume = slider.value;
        MuteCheck();
    }

    public void MuteCheck() 
    {
        if (sliderValue == 0)
        {
            imageMute.enabled = true;
        }
        else 
        { 
            imageMute.enabled = false;
        }
    }
}
