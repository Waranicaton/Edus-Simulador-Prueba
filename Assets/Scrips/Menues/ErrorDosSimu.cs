using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ErrorDosSimu : MonoBehaviour
{
    public void CerrarApp()
    {
        Debug.Log("Programa Cerrado");
        Application.Quit();
    }
}
