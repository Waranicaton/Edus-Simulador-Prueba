using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuConexionManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject menuConexion;
    public GameObject fondoMenuConexion;
    private static MenuConexionManager SharedInstance;
    public GameObject textoConectado;
    public GameObject textoDesconectado;
    public GameObject textoInformativo;

    private void Awake()
    {
        if (SharedInstance == null)
        {
            SharedInstance = this;
            DontDestroyOnLoad(gameObject);
            gameObject.SetActive(false);
        }
        else if (SharedInstance != this)
        {
            Destroy(gameObject);
        }
    }

    public static MenuConexionManager GetSharedInstance()
    {
        return SharedInstance;
    }

    public void conexionAceptada()
    {
        textoInformativo.SetActive(false);
        textoDesconectado.SetActive(false);
        textoConectado.SetActive(true);
    }

    public void conexionPerdida()
    {
        fondoMenuConexion.SetActive(true);
        menuConexion.SetActive(true);
        textoConectado.SetActive(false);
        textoDesconectado.SetActive(true);
        
    }
    // gameObject.transform.GetComponent<TextMeshProUGUI>().text

    public void desactivarMenuConexion()
    {
        fondoMenuConexion.SetActive(false);
        menuConexion.SetActive(false);
    }

    public GameObject GetGO()
    {
        return gameObject;
    }

}
