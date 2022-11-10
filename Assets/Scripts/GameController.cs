using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;
using System.Windows.Input;

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
    //private bool anotherInteractionSelected;
    private bool InteractionIsOn;
    
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
        //MusicController.Instancia.CambiarVolumen(0, false);
        
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

        path = "Assets/Recursos Taller 2/Extras/";

        ConcatenacionAccionesCompletas = "0123456";

        InteractionIsOn = false;
        GameOnPause = false;

        interaccion = -2;
        auxInteraccion = -3;

        AccionAux = -1;
    }

    //Para la partida.
    public void StopGame()
    {
        MusicController.Instancia.setLowPassMusic(true);
        MusicController.Instancia.PauseAudioSource(MusicController.Instancia.FxAudioSourceUcenin);
        MusicController.Instancia.UnpauseAudioSource(MusicController.Instancia.MusicAudioSource, true);
        GameOnPause = true;
    }
    
    //Reanuda la partida.
    public void ResumeGame()
    {
        MusicController.Instancia.setLowPassMusic(false);
        MusicController.Instancia.PauseAudioSource(MusicController.Instancia.MusicAudioSource);
        MusicController.Instancia.UnpauseAudioSource(MusicController.Instancia.FxAudioSourceUcenin, false);
        GameOnPause = false;
        /* ARCamera.transform.GetChild(0).GetComponent<MonoBehaviour>().enabled = true; */
    }

    //Espera hasta que el audio haya terminado de reproducirse, para luego cambiar el color de Ucenin.
    private IEnumerator EsperaColorOriginal(GameObject parte, Color color, float duracion, int interaccion)
    {
        yield return new WaitForSeconds(duracion == -1 ? MusicController.Instancia.FxAudioSourceUcenin.clip.length : duracion);
        if(GameOnPause)
        {
            yield return new WaitUntil(() => !GameOnPause);
        }
        InteractionIsOn = false;
        if(interaccion != this.auxInteraccion)
        {
            Debug.Log("Etrando a interacciones");
            yield break;
        }
        Debug.Log("Decidiendo");
        switch(parte.name)
        {
            case "Chest":
                Debug.Log("Entrando Chest");
                CambiarColorChest(color);
                break;
            case "UIcon":
                Debug.Log("Entrando UIcon");
                CambiarColorUIcon();
                break;
            default:
                parte.GetComponent<Renderer>().material.color = color;
                break;
        }
    }
    
    //Despliega un mensaje en la esquina superior izqueirda, indicando que se ha desbloqueado el aspecto alternativo de Ucenin.
    IEnumerator TrucoActivadoE()
    {
        TrucoDesbloqueado.SetActive(true);
        yield return new WaitForSeconds(4);
        TrucoDesbloqueado.SetActive(false);
    }

    //Crea Un RayCast si se ha presinado click izquierdo
    private void Update() 
    {
       if(Input.GetMouseButtonDown(0))
        {
            if(GameOnPause)
            {
                return;
            }
            Debug.Log("Entrado");
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
                        default:
                            interaccion = -1;
                            break;
                    }
                        if(interaccion == auxInteraccion)
                        {
                            return;
                        }
                        InteractionIsOn = true;
                        CambiarColor();
                } 
            }
        } 
    }

    //Verificamos si hemos realizado todas las interaccion posibles para desbloquear el Aspeceto alternativo de Ucenin.
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
   //Inicia una Accion dependiento de con que hayamos interactuado.
    private void GoAccion(int Accion)
    {
        if(interaccion == auxInteraccion)
        {
            return;
        }

        ConcatenacionAcciones = ConcatenacionAcciones + Accion;
        VerificarAcciones();
        MusicController.Instancia.ReproducirUceninSonido(Accion);

        if(AccionAux == Accion)
        {
            return;
        }

        if(AccionAux != -1)
        {
            StopAccion(AccionAux);
        }

        if(Accion == 0|| Accion >1)
        {
            TextUcenin.gameObject.SetActive(true);
        }

        switch(Accion)
        {
            case 0:
                reader = new StreamReader(path + "Quien es Ucenin.txt");
                TextUcenin.text = reader.ReadLine();
                break;
            case 1:
                TextUcenin.gameObject.SetActive(false);
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
                TextUcenin.gameObject.SetActive(false);
                break;

        }
        AccionAux = Accion;
    }

    //Cancela ciera accion.
    private void StopAccion(int Accion)
    {
        switch(Accion)
        {
            case 0:
                TextUcenin.gameObject.SetActive(false);
                TextUcenin.text = "";
                break;
            case 1:
                MapaUCN.SetActive(false);
                break;
            case 2:
                TextUcenin.gameObject.SetActive(false);
                TextUcenin.text = "";
                break;
            case 3:
                TextUcenin.gameObject.SetActive(false);
                TextUcenin.text = "";
                break;
            case 4:
                TextUcenin.gameObject.SetActive(false);
                TextUcenin.text = "";
                break;
            case 5:
                TextUcenin.gameObject.SetActive(false);
                TextUcenin.text = "";
                break;
            case 6:
                TextUcenin.gameObject.SetActive(false);
                break;  

        }
    }

    //Cambia el color de las partes más complejas de Ucenin.
    private void CambiarColorCompleto()
    {
        if(this.auxInteraccion == -3)
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
                    CambiarColorUIcon();
                }
                else
                {
                    CambiarColorChest(MaterialAux);
                }
            }
        }

        if(interaccion == 10)
        {

            this.MaterialAux = PartesUcenin[interaccion].GetComponentInChildren<Renderer>().material.color == Color.magenta ? this.MaterialAux : PartesUcenin[interaccion].GetComponentInChildren<Renderer>().material.color;    
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
            StartCoroutine(EsperaColorOriginal(PartesUcenin[10], MaterialAux, -1f, interaccion));
        }
        else
        {
            Debug.Log("Interaccion : " + interaccion);
            Debug.Log("Nombre: " + PartesUcenin[interaccion].name);

            this.MaterialAux = PartesUcenin[interaccion].GetComponent<Renderer>().material.color == Color.green ? this.MaterialAux : PartesUcenin[interaccion].GetComponent<Renderer>().material.color;
            Debug.Log("Material Aux " + MaterialAux + " Material Ucenin " + PartesUcenin[interaccion].GetComponent<Renderer>().material.color);
            //this.MaterialAux = PartesUcenin[interaccion].GetComponent<Renderer>().material.color;
            foreach(Renderer render in PartesChest)
            {
                render.material.color = Color.green;
            }
            StartCoroutine(EsperaColorOriginal(PartesUcenin[9], MaterialAux, -1f, interaccion));

        }
        this.auxInteraccion = interaccion;
    }

    //Cambia el color de las partes simples de Ucenin.
    private void CambiarColor()
    {
        if(interaccion > 8)
        {
            CambiarColorCompleto();
            return;
        }
        if(auxInteraccion == -3)
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
        
        this.MaterialAux = PartesUcenin[interaccion].GetComponent<Renderer>().material.color == Color.green ? this.MaterialAux : PartesUcenin[interaccion].GetComponent<Renderer>().material.color;
        this.PartesUcenin[interaccion].GetComponent<Renderer>().material.color = Color.green;
        this.auxInteraccion = interaccion;
        Debug.Log("Material: " + this.MaterialAux + " Color original: " + PartesUcenin[interaccion].GetComponent<Renderer>().material.color);
        StartCoroutine(EsperaColorOriginal(PartesUcenin[interaccion], MaterialAux, -1f, interaccion));
    }

    //Devuelve el color Original al UIcon de Ucenin
    private void CambiarColorUIcon()
    {
        foreach(Renderer render in PartesUcenin[10].GetComponentsInChildren<Renderer>())
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

    //Devuelve el color Original al Chest de Ucenin
    private void CambiarColorChest(Color color)
    {
        Debug.Log("Cambiando color Chest");
        foreach(Renderer render in PartesChest)
        {
            render.material.color = color;
        }
    }

}
