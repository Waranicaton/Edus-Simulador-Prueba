using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Net;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public GameObject camara;
    public GameObject screenManager;
    private int[] ultimoTamanio = new int[2];
    public void buttonQuit()
    {
        Debug.LogError("Programa Cerrado");
        Application.Quit();
    }
  
    public void buttonInit()
    {
        SceneManager.LoadScene("Simulacion");
    }

    public void buttonInitRelev()
    {
        SceneManager.LoadScene("Relevamiento");
    }

    public void buttonVolverMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void buttonVolverMenuInRelev()
    {
        screenManager.transform.GetComponent<ScreenManager>().salirRelev();
        camara.transform.GetComponent<CamaraDevice>().Destruir();
        SceneManager.LoadScene("Menu");
    }

    public void buttonQuitInRelev()
    {
        screenManager.transform.GetComponent<ScreenManager>().salirRelev();
        Debug.Log("Programa Cerrado");
        Application.Quit();
    }

    public void buttonFullScreen()
    {
        if (!Screen.fullScreen)
        {
            ultimoTamanio[0] = Screen.width;
            ultimoTamanio[1] = Screen.height;
        }
        Screen.SetResolution(ultimoTamanio[0], ultimoTamanio[1], !Screen.fullScreen);
        
    }
}
