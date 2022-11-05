using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonVolverMenu : Boton
{
    override
    public void OnUp(){

        SceneManager.LoadScene("MainMenu");

    }

}
