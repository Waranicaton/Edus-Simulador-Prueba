using System.Threading;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using System.Threading.Tasks;
using System;
using System.Text.RegularExpressions;
using System.IO;
using System.Globalization;
using TMPro;

public class RemoteInputListener : MonoBehaviour
{
    private static RemoteInputListener SharedInstance;
    public int listenPort = 6000;
    private InputModel InputModel;
    public GameObject canvas2;
    public  volatile bool runTask = true;
    private int i = 0;
    private Socket connectedTcpClient = null;
    private TcpListener server;
    IPEndPoint ipEndPoint = new(0, 6000);
    private string canvas2Estado = "";
    private DatosGuardados datosGuardados;
    //private WebSocketServer.WebSocketServer webSocket;

    public GameObject MENSAJE;

    public static RemoteInputListener GetSharedInstance()
    {
        return SharedInstance;
    }

    public InputModel GetInputModel ()
    {
        return InputModel;
    }

    private void Awake()
    {
        //Regla Firewall
        /*
        string json;
        if (!File.Exists(Application.persistentDataPath + "/DatosGuardados.txt"))
        {
            datosGuardados = new DatosGuardados(false);
            json = JsonConvert.SerializeObject(datosGuardados);
            File.WriteAllText(Application.persistentDataPath + "/DatosGuardados.txt", json);
        }
        json = File.ReadAllText(Application.persistentDataPath + "/DatosGuardados.txt");
        datosGuardados = JsonConvert.DeserializeObject<DatosGuardados>(json);
        if (!datosGuardados.reglaFirewall)
        {
            System.Diagnostics.ProcessStartInfo myProcessInfo = new System.Diagnostics.ProcessStartInfo(); //Initializes a new ProcessStartInfo of name myProcessInfo
            myProcessInfo.FileName = Environment.ExpandEnvironmentVariables("%SystemRoot%") + @"\System32\cmd.exe"; //Sets the FileName property of myProcessInfo to %SystemRoot%\System32\cmd.exe where %SystemRoot% is a system variable which is expanded using Environment.ExpandEnvironmentVariables
            myProcessInfo.Arguments = @" /C netsh advfirewall firewall add rule name=""EDUSTCP6000"" protocol=TCP dir=in localport=6000 action=allow"; //Sets the arguments to cd..
            myProcessInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden; //Sets the WindowStyle of myProcessInfo which indicates the window state to use when the process is started to Hidden
            myProcessInfo.Verb = "runas"; //The process should start with elevated permissions
            System.Diagnostics.Process.Start(myProcessInfo); //Starts the process based on myProcessInfo

            datosGuardados.reglaFirewall = true;
            json = JsonConvert.SerializeObject(datosGuardados);
            File.WriteAllText(Application.persistentDataPath + "/DatosGuardados.txt", json);
        }
        */

        if (SharedInstance == null)
        {
            SharedInstance = this;
            DontDestroyOnLoad(gameObject);
        }else if (SharedInstance != this)
        {
            Destroy(gameObject);
        }
        InputModel = new InputModel();

        //this.crearInformacionClave2();
    }
    
    public void comprobarCanvas2Estado()
    {
        if (canvas2Estado == "perdida")
        {
            canvas2.GetComponent<MenuConexionManager>().conexionPerdida();
            canvas2Estado = "esperando";
        }
        if (canvas2Estado == "conectado")
        {
            canvas2.GetComponent<MenuConexionManager>().conexionAceptada();
            
            StartCoroutine(TemporarilyDeactivate(3));
        }
    }

    private IEnumerator TemporarilyDeactivate(int duration)
    {
        for (i = 0; i < duration; i++)
        {
            yield return new WaitForSeconds(1);
            if (canvas2Estado == "perdida")
            {
                break;
            }
        }
        if (canvas2Estado == "conectado")
        {
            //Debug.Log(canvas2Estado);
            canvas2.GetComponent<MenuConexionManager>().desactivarMenuConexion();
        }
    }

    private void OnDestroy()
    {
        runTask = false;
    }

    public void cambiarEstado(String x)
    {
        canvas2Estado = x;
    }

    public void Datos(String datos)
    {
        string[] result = datos.Split('&');
        InputModel = new InputModel(float.Parse(result[0], new CultureInfo("en-US")), float.Parse(result[1], new CultureInfo("en-US")), float.Parse(result[2], new CultureInfo("en-US")));
        
        //Debug.Log("Informacion Recibida: "+ datos);
    }
}
