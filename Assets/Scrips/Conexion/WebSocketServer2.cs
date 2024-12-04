using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Server;
using UnityEngine.UI;
using TMPro;


public class WebSocketServer2 : MonoBehaviour
{
    private static WebSocketServer server;
    [SerializeField] Text messageText;
    private static WebSocketServer2 sharedInstance;
    public GameObject MENSAJE;


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

    void Start()
    {
        server = new WebSocketServer(6000);
        server.AddWebSocketService<Echo>("/");
        server.Start();
    }

    void Update()
    {
        //messageText.text = Echo.message;
        //MENSAJE.GetComponent<TMP_Text>().text = "WEBSOKET";
        RemoteInputListener.GetSharedInstance().comprobarCanvas2Estado();
    }

    void OnDestroy()
    {
        server.Stop();
        server = null;
    }
}

public class Echo : WebSocketBehavior
{

    public static string message;

    protected override void OnOpen()
    {
        
        RemoteInputListener.GetSharedInstance().cambiarEstado("conectado");
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        

        RemoteInputListener.GetSharedInstance().Datos(e.Data);

    }

    protected override void OnClose(CloseEventArgs e)
    {
        message = null;
       
        RemoteInputListener.GetSharedInstance().cambiarEstado("perdida");
        RemoteInputListener.GetSharedInstance().comprobarCanvas2Estado();
    }
}