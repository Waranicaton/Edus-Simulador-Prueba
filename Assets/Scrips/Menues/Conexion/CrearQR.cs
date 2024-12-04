using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;


public class CrearQR : MonoBehaviour
{
    public int qrSize = 256; // Tamaño del código QR
    public static CrearQR instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
    }

    public void HacerQR(string text)
    {
        string textoQR = "ip;";

        IPAddress[] addresses = Dns.GetHostAddresses(Dns.GetHostName());

        // Crea una cadena para almacenar las direcciones IP separadas por punto y coma

        // Recorre todas las direcciones IP y las agrega a la cadena
        foreach (IPAddress address in addresses)
        {
            // Ignora las direcciones IPv6 (si lo deseas)
            if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                // Agrega la dirección IP a la cadena
                textoQR += address.ToString() + ";";
            }
        }
        textoQR += "token;" + text;
        Texture2D qrTexture = GenerateQRCode(textoQR, qrSize);
        Sprite newSprite = Sprite.Create(qrTexture, new Rect(0, 0, qrTexture.width, qrTexture.height), Vector2.one * 0.5f);
        // Asigna el nuevo sprite al componente SpriteRenderer
        GetComponent<Image>().sprite = newSprite;

        //GetComponent<Renderer>().material.mainTexture = qrTexture; // Aplicar el código QR como textura
    }

    Texture2D GenerateQRCode(string text, int size)
    {
        BarcodeWriter writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = size,
                Width = size
            }
        };

        Color32[] pixels = writer.Write(text);
        Texture2D qrTexture = new Texture2D(size, size);
        qrTexture.SetPixels32(pixels);
        qrTexture.Apply();

        return qrTexture;
    }
}
