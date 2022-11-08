using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonReanudar : Boton
{

    [SerializeField] private GameObject PanelToOpen;
    [SerializeField] private GameObject PanelToClose;

    override
    public void OnUp()
    {
        Time.timeScale = 1f;
        PanelToOpen.SetActive(true);
        PanelToClose.SetActive(false);
    }
}
