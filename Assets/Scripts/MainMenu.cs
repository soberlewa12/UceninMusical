using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clip;

    // Update is called once per frame
    void Update()
    {
        TouchPhases();
    }

    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Salir()
    {
        Debug.Log("Salir...");
        Application.Quit();
    }
    public void TouchPhases() 
    {
        if (Input.touchCount > 0) 
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    audioSource.clip = clip;
                    audioSource.Play();
                    break;
                case TouchPhase.Moved:
                    break;
                case TouchPhase.Ended:
                    break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Canceled:
                    break;     
            }
        }
    }
}
