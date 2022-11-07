using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    
    public static GameController Instancia;
    [SerializeField] GameObject[] PartesUcenin;
    [SerializeField] Renderer[] PartesChest;
    private int interaccion;
    private int auxInteraccion;
    private Color MaterialAux;

    private int contador;
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
        
        interaccion = -2;
        auxInteraccion = -2;
        contador = 0;

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
                //Debug.Log("cast " + contador);
                contador++;
                if (hit.collider != null)
                {   
                    Debug.Log(hit.collider.name);
                    switch(hit.collider.name)
                    {    
                        case "LeftHand":
                            interaccion = 0;
                            MusicController.Instancia.ReproducirUcenin(0);
                            break;
                        case "RightHand":
                            interaccion = 1;        
                            MusicController.Instancia.ReproducirUcenin(0);
                            break;
                        case "LeftFoot":
                            interaccion = 2;
                            MusicController.Instancia.ReproducirUcenin(1);
                            break;
                        case "RightFoot":
                            interaccion = 3;
                            MusicController.Instancia.ReproducirUcenin(1);
                            break;
                        case "LeftLeg":
                            interaccion = 4;
                            MusicController.Instancia.ReproducirUcenin(2);
                            break;
                        case "RightLeg":
                            interaccion = 5;
                            MusicController.Instancia.ReproducirUcenin(2);
                            break;
                        case "LeftArm":
                            interaccion = 6;
                            MusicController.Instancia.ReproducirUcenin(3);
                            break;
                        case "RightArm":
                            interaccion = 7;
                            MusicController.Instancia.ReproducirUcenin(3);
                            break;
                        case "Body":
                            interaccion = 8;
                            MusicController.Instancia.ReproducirUcenin(4);
                            break;
                        case "Chest":
                            interaccion = 9;
                            //CambiarColorCompleto(9);
                            MusicController.Instancia.ReproducirUcenin(5);
                            break;
                        case "UIcon":
                            interaccion = 10;
                            //CambiarColorCompleto(10);
                            MusicController.Instancia.ReproducirUcenin(6);
                            break;
                    }
                    CambiarColor(interaccion);
                } 
            }
        } 
    }
    private void CambiarColor(int interaccion)
    {
        if(interaccion == auxInteraccion)
        {
            return;
        }
        if(interaccion > 8)
        {
            CambiarColorCompleto(interaccion);
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
                        render.material.color = MaterialAux;
                    }
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

    private void CambiarColorCompleto(int interaccion)
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
                        render.material.color = MaterialAux;
                    }
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
            Debug.Log("Interaccion 10");
            this.MaterialAux = PartesUcenin[interaccion].GetComponentInChildren<Renderer>().material.color;    
            foreach(Renderer render in PartesUcenin[interaccion].GetComponentsInChildren<Renderer>())
            {
                render.material.color = Color.green;
            }
        }
        else
        {
            Debug.Log("Interaccion 9");
            this.MaterialAux = PartesUcenin[interaccion].GetComponent<Renderer>().material.color;
            foreach(Renderer render in PartesChest)
            {
                render.material.color = Color.green;
            }

        }
        this.auxInteraccion = interaccion;
    }
}
