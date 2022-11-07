using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

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
    [SerializeField] GameObject[] PartesUcenin;
    [SerializeField] Renderer[] PartesChest;
    [SerializeField] GameObject NBackground;
    [SerializeField] GameObject BackgroundCross;
    [SerializeField] GameObject MiddleArrow;

    [SerializeField] TextMeshPro TextUcenin;
    [SerializeField] GameObject MapaUCN;
    private int interaccion;
    private int auxInteraccion;
    private Color MaterialAux;

    private int AccionAux;
    private StreamReader reader;
    private string path;
    private string Textpath;

    private Color materialAzul;
    private Color materialNaranjo;

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

    private void Start() {
    
        materialAzul = PartesUcenin[8].GetComponent<Renderer>().material.color;
        materialNaranjo = PartesUcenin[10].GetComponentInChildren<Renderer>().material.color;
        Debug.Log(materialAzul + " | " + materialNaranjo);
        AccionAux = -1;
        interaccion = -2;
        auxInteraccion = -2;
        MusicController.Instancia.StopMusic();
        path = "Assets/Recursos Taller 2/Extras/";

    }

    private void FixedUpdate() 
    {
       if(Input.GetMouseButton(0))
        {
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
                            break;
                    }
                    CambiarColor();
                } 
            }
        } 
    }

    //Inicia una Accion dependiento de con que hayamos interactuado
    private void GoAccion(int Accion)
    {

        if(AccionAux == Accion)
        {
            return;
        }
        if(AccionAux != -1)
        {
            StopAccion(AccionAux);
        }
        MusicController.Instancia.ReproducirUcenin(Accion);
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
            this.MaterialAux = PartesUcenin[interaccion].GetComponent<Renderer>().material.color;
            foreach(Renderer render in PartesChest)
            {
                render.material.color = Color.green;
            }

        }
        this.auxInteraccion = interaccion;
    }
}
