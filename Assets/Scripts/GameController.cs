using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //0. LeftHand
    //1. RightHand
    //2. LeftFoot
    //3. RightFoot
    //4. LeftLeg
    //5. RightLeg
    //6. LeftArm
    //7. RightArm
    //8. 
    //9. 
    //10. 

    public static GameController Instancia;

    //Referencias a los objetos de Ucenin.
    [SerializeField] GameObject[] PartesUcenin;
    [SerializeField] GameObject[] PartesUceninEspecial;
    [SerializeField] Renderer[] PartesChest;
    [SerializeField] Renderer[] PartesChestEspecial;
    [SerializeField] GameObject NBackground;
    [SerializeField] GameObject BackgroundCross;
    [SerializeField] GameObject MiddleArrow;
    [SerializeField] GameObject NBackgroundEspecial;
    [SerializeField] GameObject BackgroundCrossEspecial;
    [SerializeField] GameObject MiddleArrowEspecial;
    [SerializeField] GameObject Ucenin;
    [SerializeField] GameObject UceninEspecial;

    [SerializeField] GameObject ARCamera;

    //Referencias a distintas interacciones que pueden aparecer.
    [SerializeField] TextMeshPro TextUcenin;
    [SerializeField] GameObject MapaUCN;
    [SerializeField] GameObject TrucoDesbloqueado;

    [SerializeField] private  GameObject StopVideo;
    private Color BackgroundColor;

    //Para cambiar el color de Ucenin.
    private int interaccion;
    private int auxInteraccion;
    private Color MaterialAux;

    //Para generar texto
    private int AccionAux;
    private StreamReader reader;
    private string path;
    private string Textpath;

    //Referencia a los colores de Ucenin. No necesariamente son los colores Azul y naranja.
    private Color materialAzul;
    private Color materialNaranjo;

    //Para Desbloquear el aspecto desconocido de Ucenin.
    private string ConcatenacionAcciones;
    private string ConcatenacionAccionesCompletas;

    private bool GameOnPause;

    private void Awake() 
    {
        if(Instancia != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instancia = this;
        }
    }

    private void Start() 
    {    
        Application.targetFrameRate = 90;
        MusicController.Instancia.PauseAudioSource(MusicController.Instancia.MusicAudioSource);
        MusicController.Instancia.CambiarVolumen(0, false);
        
        if(PlayerPrefs.GetString("Skin", "").Equals("UceninEspecial"))
        {
            UceninEspecial.SetActive(true);
            Ucenin.SetActive(false);
            PartesUcenin = PartesUceninEspecial;
            PartesChest = PartesChestEspecial;
            NBackground = NBackgroundEspecial;
            BackgroundCross = BackgroundCrossEspecial;
            MiddleArrow = MiddleArrowEspecial;
        }
        else
        {
            Ucenin.SetActive(true);
            UceninEspecial.SetActive(false);
        }

        materialAzul = PartesUcenin[8].GetComponent<Renderer>().material.color;
        materialNaranjo = PartesUcenin[10].GetComponentInChildren<Renderer>().material.color;

        AccionAux = -1;         //Si AccionAux = -1, significa que es la primera accion realizada.      
        auxInteraccion = -2;    //Si auxInteraccion = -2, Significa que es la primera accion realiazada sobre una parte especifica de ucenin.

        path = "Assets/Recursos Taller 2/Extras/";

        ConcatenacionAccionesCompletas = "0123456";

        /* PartesUcenin[0] = Ucenin.transform.GetChild(5).GetChild(0).gameObject;
        PartesUcenin[1] = Ucenin.transform.GetChild(6).GetChild(0).gameObject;
        PartesUcenin[2] = Ucenin.transform.GetChild(4).gameObject;
        PartesUcenin[3] = Ucenin.transform.GetChild(3).gameObject;
        PartesUcenin[4] = Ucenin.transform.GetChild(1).gameObject;
        PartesUcenin[5] = Ucenin.transform.GetChild(2).gameObject;
        PartesUcenin[6] = Ucenin.transform.GetChild(5).gameObject;
        PartesUcenin[7] = Ucenin.transform.GetChild(6).gameObject;
        PartesUcenin[8] = Ucenin.transform.GetChild(0).gameObject;
        PartesUcenin[9] = Ucenin.transform.GetChild(0).GetChild(0).gameObject;
        PartesUcenin[10] = Ucenin.transform.GetChild(0).GetChild(3).gameObject; */
        
    }

    public void StopGame()
    {
        MusicController.Instancia.setLowPassMusic(true);
        MusicController.Instancia.PauseAudioSource(MusicController.Instancia.FxAudioSourceUcenin);
        MusicController.Instancia.UnpauseAudioSource(MusicController.Instancia.MusicAudioSource);
        MusicController.Instancia.FadeInMusicVoid();
        GameOnPause = true;
        /* Debug.Log("Inicio");
        Texture2D sprites = ScreenCapture.CaptureScreenshotAsTexture();

        Rect rec = new Rect(0, 0, sprites. width, sprites. height);
        StopVideo.GetComponent<Image>().sprite = Sprite.Create(sprites,rec,new Vector2(0,0),1);
        ARCamera.transform.GetChild(0).GetComponent<MonoBehaviour>().enabled = false;
        Debug.Log("Inicio"); */
    }
    
    public void ResumeGame()
    {
        MusicController.Instancia.setLowPassMusic(false);
        MusicController.Instancia.PauseAudioSource(MusicController.Instancia.MusicAudioSource);
        GameOnPause = false;
        /* ARCamera.transform.GetChild(0).GetComponent<MonoBehaviour>().enabled = true; */
    }


    private void Update() 
    {
       if(Input.GetMouseButtonDown(0))
        {
            if(GameOnPause)
            {
                return;
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            float Distancia = 100f;
            Debug.DrawRay(ray.origin, ray.direction*Distancia, Color.black);

            if(Physics.Raycast(ray, out hit, Distancia))
            {
                if (hit.collider != null)
                {   
                    Debug.Log(hit.collider.name);
                    switch(hit.collider.name)
                    {    
                        case "LeftHand":
                            interaccion = 0;
                            GoAccion(0);
                            break;
                        case "RightHand":
                            interaccion = 1;        
                            GoAccion(0);
                            break;
                        case "LeftFoot":
                            interaccion = 2;
                            GoAccion(1);
                            break;
                        case "RightFoot":
                            interaccion = 3;
                            GoAccion(1);
                            break;
                        case "LeftLeg":
                            interaccion = 4;
                            GoAccion(2);
                            break;
                        case "RightLeg":
                            interaccion = 5;
                            GoAccion(2);
                            break;
                        case "LeftArm":
                            interaccion = 6;
                            GoAccion(3);
                            break;
                        case "RightArm":
                            interaccion = 7;
                            GoAccion(3);
                            break;
                        case "Body":
                            interaccion = 8;
                            GoAccion(4);
                            break;
                        case "Chest":
                            interaccion = 9;
                            GoAccion(5);
                            break;
                        case "UIcon":
                            interaccion = 10;
                            GoAccion(6);
                            break;
                    }
                    CambiarColor();
                } 
            }
        } 
    }

    //Verificamos si hemos realizado todas las interaccion posibles.
    private void VerificarAcciones()
    {
        if(PlayerPrefs.GetInt("SkinDesbloqueada", 0) == 1)
        {
            return;
        }

        foreach(char numero in ConcatenacionAccionesCompletas.ToCharArray())
        {

            foreach(char numeroAux in ConcatenacionAcciones.ToCharArray())
            {
                Debug.Log("[" + numeroAux + "][" + numero + "]");
                if(numero.Equals(numeroAux))
                {
                    Debug.Log("asdasdad");
                    goto next;
                }
            }
            return;
            next:
            continue;
        }

        MusicController.Instancia.BotonEncima();
        Debug.Log(PlayerPrefs.GetFloat("Slider"));
        PlayerPrefs.SetInt("SkinDesbloqueada", 1);
        ConcatenacionAccionesCompletas = "";
        StartCoroutine(TrucoActivadoE());
    }

    IEnumerator TrucoActivadoE()
    {
        TrucoDesbloqueado.SetActive(true);
        yield return new WaitForSeconds(4);
        TrucoDesbloqueado.SetActive(false);
    }

    //Inicia una Accion dependiento de con que hayamos interactuado
    private void GoAccion(int Accion)
    {
        ConcatenacionAcciones = ConcatenacionAcciones + Accion;
        VerificarAcciones();
        if(AccionAux == Accion)
        {
            return;
        }
        if(AccionAux != -1)
        {
            StopAccion(AccionAux);
        }
        MusicController.Instancia.ReproducirUceninSonido(Accion);
        switch(Accion)
        {
            case 0:
                reader = new StreamReader(path + "Quien es Ucenin.txt");
                TextUcenin.text = reader.ReadLine();
                break;
            case 1:
                MapaUCN.SetActive(true);
                break;
            case 2:
                reader = new StreamReader(path + "Velocidad teórica.txt");
                TextUcenin.text = reader.ReadLine();
                break;
            case 3:
                reader = new StreamReader(path + "Musculatura Ucenin.txt");
                TextUcenin.text = reader.ReadLine();
                break;
            case 4:
                reader = new StreamReader(path + "Coraza Ucenin.txt");
                TextUcenin.text = reader.ReadLine();
                break;
            case 5:
                reader = new StreamReader(path + "Esqueleto Reforzado.txt");
                TextUcenin.text = reader.ReadLine();
                break;
            case 6:
                break;

        }
        AccionAux = Accion;
    }

    //Para (O cancela) ciera accion.
    private void StopAccion(int Accion)
    {
        switch(Accion)
        {
            case 0:
                TextUcenin.text = "";
                break;
            case 1:
                MapaUCN.SetActive(false);
                break;
            case 2:
                TextUcenin.text = "";
                break;
            case 3:
                TextUcenin.text = "";
                break;
            case 4:
                TextUcenin.text = "";
                break;
            case 5:
                TextUcenin.text = "";
                break;
            case 6:
                break;  

        }
    }

    private void CambiarColor()
    {
        if(interaccion == auxInteraccion)
        {
            return;
        }
        if(interaccion > 8)
        {
            CambiarColorCompleto();
            return;
        }
        if(auxInteraccion == -2)
        {
            this.auxInteraccion = interaccion;
        }
        else if(interaccion != auxInteraccion)
        {
            if(auxInteraccion > 8)
            {
                if(auxInteraccion == 10)
                {
                    foreach(Renderer render in PartesUcenin[auxInteraccion].GetComponentsInChildren<Renderer>())
                    {
                        render.material.color = materialNaranjo;
                    }
                    NBackground.GetComponent<Renderer>().material.color = materialNaranjo;
                    BackgroundCross.GetComponent<Renderer>().material.color = materialAzul;
                    MiddleArrow.GetComponent<Renderer>().material.color = materialAzul;
                    PartesUcenin[0].GetComponent<Renderer>().material.color = materialAzul;
                    PartesUcenin[1].GetComponent<Renderer>().material.color = materialAzul;
                    PartesUcenin[2].GetComponent<Renderer>().material.color = materialAzul;
                    PartesUcenin[3].GetComponent<Renderer>().material.color = materialAzul;
                    PartesUcenin[8].GetComponent<Renderer>().material.color = materialAzul;
                }
                else
                {
                    foreach(Renderer render in PartesChest)
                    {
                        render.material.color = MaterialAux;
                    }
                }
            }
            else
            {
                this.PartesUcenin[auxInteraccion].GetComponent<Renderer>().material.color = MaterialAux;
            }
        }
        this.MaterialAux = PartesUcenin[interaccion].GetComponent<Renderer>().material.color;
        this.PartesUcenin[interaccion].GetComponent<Renderer>().material.color = Color.green;
        this.auxInteraccion = interaccion;
    }

    private void CambiarColorCompleto()
    {
        if(interaccion == auxInteraccion)
        {
            return;
        }

        if(this.auxInteraccion == -2)
        {
            this.auxInteraccion = interaccion;
        }

        if(interaccion != auxInteraccion)
        {
            if(this.auxInteraccion < 9)
            {
                this.PartesUcenin[this.auxInteraccion].GetComponent<Renderer>().material.color = MaterialAux;
            }
            else
            {
                if(auxInteraccion == 10)
                {
                    foreach(Renderer render in PartesUcenin[auxInteraccion].GetComponentsInChildren<Renderer>())
                    {
                        render.material.color = materialNaranjo;
                    }
                    NBackground.GetComponent<Renderer>().material.color = materialNaranjo;
                    BackgroundCross.GetComponent<Renderer>().material.color = materialAzul;
                    MiddleArrow.GetComponent<Renderer>().material.color = materialAzul;
                    PartesUcenin[0].GetComponent<Renderer>().material.color = materialAzul;
                    PartesUcenin[1].GetComponent<Renderer>().material.color = materialAzul;
                    PartesUcenin[2].GetComponent<Renderer>().material.color = materialAzul;
                    PartesUcenin[3].GetComponent<Renderer>().material.color = materialAzul;
                    PartesUcenin[8].GetComponent<Renderer>().material.color = materialAzul;
                }
                else
                {
                    Debug.Log("Cambiando color Chest");
                    foreach(Renderer render in PartesChest)
                    {
                        render.material.color = MaterialAux;
                    }
                }
            }
        }
        
        if(interaccion == 10)
        {
            this.MaterialAux = PartesUcenin[interaccion].GetComponentInChildren<Renderer>().material.color;    
            foreach(Renderer render in PartesUcenin[interaccion].GetComponentsInChildren<Renderer>())
            {
                render.material.color = Color.magenta;
            }
            NBackground.GetComponent<Renderer>().material.color = Color.magenta;
            BackgroundCross.GetComponent<Renderer>().material.color = Color.magenta;
            MiddleArrow.GetComponent<Renderer>().material.color = Color.magenta;
            PartesUcenin[0].GetComponent<Renderer>().material.color = Color.magenta;
            PartesUcenin[1].GetComponent<Renderer>().material.color = Color.magenta;
            PartesUcenin[2].GetComponent<Renderer>().material.color = Color.magenta;
            PartesUcenin[3].GetComponent<Renderer>().material.color = Color.magenta;    
            PartesUcenin[8].GetComponent<Renderer>().material.color = Color.magenta;    
        }
        else
        {
            Debug.Log("Interaccion : " + interaccion);
            Debug.Log("Nombre: " + PartesUcenin[interaccion].name);
            this.MaterialAux = PartesUcenin[interaccion].GetComponent<Renderer>().material.color;
            foreach(Renderer render in PartesChest)
            {
                render.material.color = Color.green;
            }

        }
        this.auxInteraccion = interaccion;
    }
}
