using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Volumen : MonoBehaviour
{

    [SerializeField] private GameObject MuteGameObject;
    [SerializeField] private Sprite spriteMute;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider slider;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("volumenMixer");
        mixer.SetFloat("MixerMaster", (slider.value*83 - 80));
        SliderOnUp();
    }

    public void ChangeSlider() 
    {
        mixer.SetFloat("MixerMaster", (slider.value*83 - 80));
    }

    public void SliderOnUp() 
    {
        PlayerPrefs.SetFloat("volumenMixer", slider.value);

        if (slider.value == 0.3f)
        {
            MuteGameObject.GetComponent<Image>().sprite = spriteMute;
            MuteGameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }
        else 
        { 
            MuteGameObject.GetComponent<Image>().sprite = null;
            MuteGameObject.GetComponent<Image>().color = new Color(255, 255, 255, 0);
        }
    }
}
