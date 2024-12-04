using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using TMPro;
using UnityEngine.SceneManagement;



public class ComprobarParametros : MonoBehaviour
{
    private bool parametros = false;
    private string setDeImagenes = "";
    private int puntoAIniciar = 0;
    private int dificultad = -1;
    private string token = "Default";
    private string url = "Default";
    private string texto;
    public GameObject RUTA;


    //COSAS OCULTABLES
    public GameObject selectorDeDificultad;
    public GameObject barraAutoevaluacion;
    public GameObject barraPunto;
    public GameObject botonEnviarDatos;

    public GameObject menuNoToken;

 
    /// <summary>
    /// EJEMPLO: MiAppUnity.exe -SetDeImagenes "Set1" -PuntoAIniciar 5 -Dificultad "Dif�cil" -Token "miToken123" -URL "http://miurl.com"
    /// </summary>
    /// 

    private void ComprobarParametro()
    {
        //Debug.LogError(url);

#if UNITY_STANDALONE_OSX
        
        string url1 = Application.absoluteURL;
        if (!string.IsNullOrEmpty(url1))
        {
            Debug.LogError("TIENE URL:" + url1);
        }
        texto = url1;
        
#endif
#if UNITY_STANDALONE_WIN


        string[] args = System.Environment.GetCommandLineArgs();
        if (args.Length > 1)
        {
            texto = args[1];
        }
        else
        {
            texto = "";
        }
        
        //Debug.LogError("EL PEPE");

#endif

        // Separar el string utilizando '/' como separador
        string[] argumentos = texto.Split(';');

        // Ignorar el primer resultado
        string[] argumentosSinPrimero = new string[argumentos.Length - 1];
        Array.Copy(argumentos, 1, argumentosSinPrimero, 0, argumentos.Length - 1);


        // Procesar los argumentos
        for (int i = 0; i < argumentosSinPrimero.Length; i++)
        {
            switch (argumentosSinPrimero[i])
            {
                case "SetDeImagenes":
                    if (i + 1 <= argumentosSinPrimero.Length)
                    {
                        setDeImagenes = argumentosSinPrimero[i + 1];
                    }
                    break;
                case "PuntoAIniciar":
                    if (i + 1 <= argumentosSinPrimero.Length)
                    {
                        int.TryParse(argumentosSinPrimero[i + 1], out puntoAIniciar);

                    }
                    break;
                case "Dificultad":
                    if (i + 1 <= argumentosSinPrimero.Length)
                    {
                        int.TryParse(argumentosSinPrimero[i + 1], out dificultad);

                    }
                    break;
                case "Token":
                    if (i + 1 <= argumentosSinPrimero.Length)
                    {
                        token = argumentosSinPrimero[i + 1];
                    }
                    break;
                case "URL":
                    if (i + 1 <= argumentosSinPrimero.Length)
                    {
                        url = argumentosSinPrimero[i + 1];
                    }
                    break;
            }
        }


#if UNITY_EDITOR
        dificultad = 2;
        //token = "ABC";
        url = "https://en8uiaazorc87.x.pipedream.net/";
        parametros = true;
        setDeImagenes = "HigadoTest";
#endif


        if (setDeImagenes != "")
        {
            bool existeAux = false;
#if UNITY_STANDALONE_OSX
            existeAux = Directory.Exists(Application.streamingAssetsPath + "/" + "Organos/" + setDeImagenes);
#elif UNITY_STANDALONE_WIN
            existeAux = Directory.Exists(Application.persistentDataPath + "/" + "Organos/" + setDeImagenes);
#endif
            if (existeAux)
            {
                //Debug.LogError("comprobar parametros");
                parametros = true;
#if UNITY_STANDALONE_OSX
                existeAux = Directory.Exists(Application.streamingAssetsPath + "/" + "Organos/" + setDeImagenes + "/Punto" + puntoAIniciar);
#elif UNITY_STANDALONE_WIN
                existeAux = Directory.Exists(Application.persistentDataPath + "/" + "Organos/" + setDeImagenes + "/Punto" + puntoAIniciar);
#endif
                if (!existeAux)
                    puntoAIniciar = 1;
                if ((dificultad != 0) && (dificultad != 1) && (dificultad != 2))
                    dificultad = 1;
            }
        }


        if (token == "Default")
        {
            menuNoToken.SetActive(true);
            dificultad = 2;
            token = "ABCDE";
            url = "https://en8uiaazorc87.x.pipedream.net/";
            parametros = true;
        }
        else
        {
            CrearQR.instance.HacerQR(token);
            WebSocketPrueba.GetSharedInstance().Conectar(token);
        }
        if (dificultad == 2)
        {
            selectorDeDificultad.SetActive(false);
        }
        else
        {
            barraAutoevaluacion.SetActive(false);
            barraPunto.SetActive(false);
            botonEnviarDatos.SetActive(false);
        }
    }
        

    public bool ParametrosCorrectos()
    {
        ComprobarParametro();
        return this.parametros;
    }

    public string SetImagenes()
    {
        return this.setDeImagenes;
    }

    public int Punto()
    {
        return this.puntoAIniciar;
    }

    public int Dificultad()
    {
        return this.dificultad;
    }

    public string Token()
    {
        return token;
    }

    public string URL()
    {
        return url;
    }

   
}
