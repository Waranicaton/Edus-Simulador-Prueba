using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.UI;
using System.Net;

public class EnviarInfo : MonoBehaviour
{
    public string apiUrl = "Default"; // Reemplaza con tu propia URL
    public string authToken = "Default";
    public GameObject imageManager;
    public GameObject Parametros;
    public GameObject textoError;
    public GameObject botonSalir;
    public GameObject botonEnviarInfo;

    public void EnviarDatos()
    {
        
        //if (Parametros.GetComponent<ComprobarParametros>().Dificultad() == 2)
        if (Parametros.GetComponent<ComprobarParametros>().Dificultad() == 2)
        {
            textoError.SetActive(true);
            textoError.GetComponent<TMP_Text>().text = "Enviando datos a la plataforma de evaluación";
            textoError.GetComponent<TMP_Text>().color = Color.green;
            botonEnviarInfo.GetComponent<Button>().interactable = false;
            apiUrl = Parametros.GetComponent<ComprobarParametros>().URL();
            authToken = Parametros.GetComponent<ComprobarParametros>().Token();
            StartCoroutine(SendPostRequest());
        }
        else
        {
            gameObject.GetComponent<MenuManager>().buttonQuit();
        }

    }

    IEnumerator SendPostRequest()
    {
        // Crear el JSON que deseas enviar a la API
        //Dictionary<string, string> jsonData = new Dictionary<string, string>();
        //jsonData.Add("clave", "UNITY");
        //string json = JsonUtility.ToJson(jsonData);
        
        string json = JsonConvert.SerializeObject(imageManager.GetComponent<ImageManager3>().manejadorPuntoInteres.GetResultados());

       

        // Crear la solicitud POST
        UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
        //request.certificateHandler = null;
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Authorization", "Bearer "+authToken);
        request.SetRequestHeader("Content-Type", "application/json");

        // Enviar la solicitud y esperar la respuesta
        yield return request.SendWebRequest();

        // Verificar si hay algún error
        if ((request.result != UnityWebRequest.Result.Success)||(request.responseCode != 200))
        {

            Debug.LogError("Error al enviar la solicitud: " + request.error);
            textoError.GetComponent<TMP_Text>().text = "Error al comunicarse con la plataforma de evaluación";
            textoError.GetComponent<TMP_Text>().color = Color.red;
            //botonSalir.SetActive(true);
        }
        else
        {
            // Acceder a la respuesta de la API
            string response = request.downloadHandler.text;
            Debug.Log("Respuesta de la API: " + request.responseCode);
            Debug.Log("Info del json: " + json);
            gameObject.GetComponent<MenuManager>().buttonQuit();

        }
        botonEnviarInfo.GetComponent<Button>().interactable = true;
    }
}



