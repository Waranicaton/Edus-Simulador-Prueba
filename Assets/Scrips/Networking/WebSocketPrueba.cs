using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Server;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using System;
using System.Linq;
using Newtonsoft.Json;
using System.Text;


public class WebSocketPrueba : MonoBehaviour
{
    [SerializeField] GameObject prefabCanvas;
    private WebSocket webSocket;
    [SerializeField] string url;
    public GameObject mensaje_SessionInciada;
    
    public static WebSocketPrueba sharedInstance;
    private string token;

    private bool movilEnviandoDatos = false;
    private float tiempoDeEspera = 20f;
    private bool primerDatoRecibido;
    private bool cartelPerdida = true;

    public bool conectado = false;
    bool cerrarConexion = false;
    public bool conexionOnClose = true;


    //mensaje counter
    private int cantMensajes = 0;
    private float tiempoTranscurrido = 0f;
    

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (sharedInstance != this)
        {
            Destroy(gameObject);
        }
    }

    public static WebSocketPrueba GetSharedInstance()
    {
        return sharedInstance;
    }

    string DecodeToken(string token)
    {
        string id = null;
        //token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjQyMCIsImV4cCI6MTcxNjUwMjM5M30.uuzNRjz6uqLEGJitqRWPMrjepUY6GIOWNs93ia0WCSw";
        try
        {
            string[] tokenParts = token.Split('.');

            string base64EncodedBody = tokenParts[1];
            // Calcula cu�ntos caracteres de relleno se necesitan
            int remainder = base64EncodedBody.Length % 4;
            int paddingNeeded = (remainder == 0) ? 0 : (4 - remainder);

            // Agrega los caracteres de relleno necesarios
            base64EncodedBody += new string('=', paddingNeeded);
            Debug.Log(base64EncodedBody);
            string bodyJson = Encoding.UTF8.GetString(Convert.FromBase64String(base64EncodedBody));

            var bodyData = JsonConvert.DeserializeObject<Dictionary<string, object>>(bodyJson);

            /*foreach (var kpv in bodyData)
            {
                Debug.Log("Campo: " + kpv.Key + ", Valor: " + kpv.Value);
            }*/

            if (bodyData.ContainsKey("id"))
            {
                id = bodyData["id"].ToString();
                Debug.Log(id);
            }
            else
            {
                Debug.Log("El token no contiene un campo id");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error al decodificar el token " + e.Message);
            return "111";
        }
        return id;
    }

    void Start()
    {
        DontDestroyOnLoad(this);
        webSocket = new WebSocket("wss://" + url);

        webSocket.OnMessage += (sender, e) =>
        {
            if (e.Data == '"' + "Session iniciada" + '"')
            {
            }
            else if (e.Data == "Transductor desconectado")
            {
                tiempoDeEspera = 6f;
                movilEnviandoDatos = false;
            }
            else if (e.Data == "Cierre forzado")
            {
                cerrarConexion = true;
                webSocket.Close();
            }
            else
            {
                string[] valores = e.Data.Split('&');
                if (valores.Length == 3)
                {
                    RemoteInputListener.GetSharedInstance().Datos(e.Data);
                    cantMensajes++;

                    movilEnviandoDatos = true;
                }
                if (tiempoDeEspera > 5f)
                {
                    primerDatoRecibido = true;
                }
            }
        };

        webSocket.OnOpen += (sender, e) =>
        {
                //este decode devuelve el id del usuario, mandarselo al servidor y que el mismo se fije que hacer
                //DecodeToken(token);
                webSocket.Send(Formato(token));
            conectado = true;
        };

        webSocket.OnClose += (sender, e) =>
        {
            Debug.Log("Conexion cerrada");
            conectado = false;
            conexionOnClose = false;
        };

        webSocket.OnError += (sender, e) =>
        {
            Debug.Log("error: " + e.Message);
        };
    }

    void Update()
    {
        RemoteInputListener.GetSharedInstance().comprobarCanvas2Estado();
        if (primerDatoRecibido)
        {
            RemoteInputListener.GetSharedInstance().cambiarEstado("conectado");
            primerDatoRecibido=false;
            cartelPerdida = false;
        }
        if (movilEnviandoDatos)
        {
            tiempoDeEspera = 0;
            movilEnviandoDatos = false;
        }
        else
        {
            tiempoDeEspera += Time.deltaTime;
        }
        if ((tiempoDeEspera > 5f) && !cartelPerdida)
        {
            RemoteInputListener.GetSharedInstance().cambiarEstado("perdida");
            cartelPerdida = true;
        }
        if (cerrarConexion)
        {
            //activo un canvas de error
            Destroy(InputFieldSimulacion.instance);
            Instantiate(prefabCanvas);
            cerrarConexion = false;
        }
        if (tiempoTranscurrido < 1f)
        {
            tiempoTranscurrido += Time.deltaTime;
        }
        else
        {
            tiempoTranscurrido = 0f;
            cantMensajes = 0;
        }
        if (!conexionOnClose)
        {
            mensaje_SessionInciada.SetActive(true);
        }

    }

    void OnDestroy()
    {
        try
        {
            webSocket.Close();
        }
        catch
        {

        }
        
    }

    
    private string Formato(string token)
    {
        string id = DecodeToken(token);
        Debug.LogError(id);
        FormatoObject formatoObject = new FormatoObject(token, "simulador", false, id);
        Formato2 formato2 = new Formato2(formatoObject);
        return JsonConvert.SerializeObject(formato2);
        
    }

    public void Conectar(string token2)
    {
        token = token2;
        
        try {
            webSocket.Connect();
        }
        catch
        {

        }
        
    }
    

}
