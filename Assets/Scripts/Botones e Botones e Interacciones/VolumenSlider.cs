using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumenSlider : MonoBehaviour
{

    [SerializeField] private GameObject MuteGameObject;
    [SerializeField] private Sprite spriteMute;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider slider;

    void Start()
    {
    }
    private void OnEnable() 
    {
        slider.value = PlayerPrefs.GetFloat("Slider", 0.0f);
    }

    public void ChangeSlider() 
    {
        MusicController.Instancia.CambiarVolumen(slider.value, true);
    }


    public void SliderOnUp() 
    {
        MusicController.Instancia.CambiarVolumen(slider.value, true);

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
