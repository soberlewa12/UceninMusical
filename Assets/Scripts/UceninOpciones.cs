using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UceninOpciones : MonoBehaviour
{

    public void OnEnter(){
        Debug.Log("On enter");
        this.transform.Rotate(new Vector3(10,0,0));

    }
}
