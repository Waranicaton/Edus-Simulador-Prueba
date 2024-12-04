using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;
using Microsoft.Win32;

public class Descargador : MonoBehaviour
{
    private string urlInstalador = "https://storage.googleapis.com/gpe_edus/EDUS%20Setup.exe";
    private string urlArchivo = "https://storage.googleapis.com/gpe_edus/Actualizacion.txt";
    private string installerPath;
    public GameObject mensaje_actualizacionEncontrada;
    public GameObject mensaje_Actualizando;
    public GameObject mensaje_Error;
    public GameObject mensaje_ErrorComprobar;
    public GameObject autoCLick;
    public GameObject botonSalir;
    public GameObject mensaje_Aviso;
    private bool run = false;
    private string randomStr;
    private string parametrosPagina;

    private string rutaDelProgramaInstalado;

    void Start()
    {
        string[] args = System.Environment.GetCommandLineArgs();
        
        if (args.Length >= 2) parametrosPagina = args[1];
        else parametrosPagina = "";
            
        
        // Define the file path where the installer will be saved
        installerPath = Path.Combine(Application.persistentDataPath, "MiAplicacionSetup.exe");

        rutaDelProgramaInstalado = Directory.GetParent(Application.dataPath).FullName;

        // Start the download and install process
        randomStr = ("?" +Random.Range(0, 9).ToString() + Random.Range(0, 9).ToString() + Random.Range(0, 9).ToString());
        StartCoroutine(ComprobarActualizacion(Application.persistentDataPath + "/Actualizacion.txt"));

    }


    IEnumerator ComprobarActualizacion(string path)
    {
        run = true;
        using (UnityWebRequest uwr = UnityWebRequest.Get(urlArchivo + randomStr))
        {
            uwr.downloadHandler = new DownloadHandlerFile(path);
            yield return uwr.SendWebRequest();

            if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
            {
                UnityEngine.Debug.LogError("Download error: " + uwr.error);
                mensaje_ErrorComprobar.SetActive(true);

            }
            else
            {
                UnityEngine.Debug.Log("Download complete. File saved to: " + path);
                
                string versionNueva =File.ReadAllText(Application.persistentDataPath + "/" + "Actualizacion.txt");

                if (versionNueva != Application.version)
                {
                    botonSalir.SetActive(true);
                    mensaje_actualizacionEncontrada.SetActive(true);
                    UnityEngine.Debug.Log(versionNueva);
                    UnityEngine.Debug.Log(Application.version);
                    UnityEngine.Debug.Log("ActualizacionEncontrada");
                    StartCoroutine(DownloadAndInstall());
                }else autoCLick.GetComponent<AutoClick>().Click();
                yield return null;
            
            }
            
        }
        run = false;
    }
    IEnumerator DownloadAndInstall()
    {
        // Start the download
        run = true;
        UnityEngine.Debug.Log("Descargando nueva actualizacion");
        mensaje_Actualizando.SetActive(true);
        mensaje_Aviso.SetActive(true);
        using (UnityWebRequest uwr = UnityWebRequest.Get(urlInstalador))
        {
            uwr.downloadHandler = new DownloadHandlerFile(installerPath);
            yield return uwr.SendWebRequest();

            if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
            {
                UnityEngine.Debug.LogError("Download error: " + uwr.error);
                mensaje_Actualizando.SetActive(false);
                mensaje_Error.SetActive(true);
                mensaje_Aviso.SetActive(false);

            }
            else
            {
                UnityEngine.Debug.Log("Download complete. File saved to: " + installerPath);
                mensaje_Actualizando.SetActive(false);
                
                RunInstaller();
            }
        }
        run = false;
    }

    void RunInstaller()
    {
        if (File.Exists(installerPath))
        {
            string installDir = rutaDelProgramaInstalado;
            // Start the process with parameters for silent install and custom directory
            Process process = new Process();
            process.StartInfo.FileName = "C:\\Users\\franr\\Documents\\EdusEcografo\\Instalador\\InstaladorCompilado\\EDUS 1.7.1.exe";
            //process.StartInfo.Arguments = $"/VERYSILENT /DIR=\"{installDir}\"";
            process.StartInfo.Arguments = $"/SILENT /DIR=\"{installDir}\" /param={parametrosPagina} /runapp=true";
            process.StartInfo.UseShellExecute = false;
            process.Start();
            Application.Quit();
            UnityEngine.Debug.Log("Installer started: " + installerPath);
        }
        else
        {
            UnityEngine.Debug.LogError("Installer not found: " + installerPath);
        }
    }

    public void ReintentarComprobar()
    {
        if (!run)
        {
            StartCoroutine(ComprobarActualizacion(Application.persistentDataPath + "/Actualizacion.txt"));
            
        }
    }

    public void ReintentarDescarga()
    {
        if (!run)
        {
            StartCoroutine(DownloadAndInstall());

        }
    }

    
}