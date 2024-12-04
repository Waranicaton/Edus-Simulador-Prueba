using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing.Aztec.Internal;

public class volverPlataforma : MonoBehaviour
{
    float tiempo = 0f;

    private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    private const int stringLength = 20;
    
    public void CerrarIrPagina()
    {
        Application.OpenURL("https://app.edus.ar/home");
        Application.Quit();
    }

    private void Update()
    {
        if (tiempo < 10f)
        {
            tiempo += Time.deltaTime;
        }
        else
        {
#if UNITY_EDITOR
#else
            Application.OpenURL("https://app.edus.ar/home");
            Application.Quit();
#endif
        }
        if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) 
            && Input.GetKey(KeyCode.E))
        {
            tiempo = 0f;
            string token = GenerateRandomString(20);
            CrearQR.instance.HacerQR(token);
            WebSocketPrueba.GetSharedInstance().Conectar(token);
            gameObject.SetActive(false);
        }


    }

    string GenerateRandomString(int length)
    {
        char[] stringChars = new char[length];
        System.Random random = new System.Random();

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        return new string(stringChars);
    }
}
