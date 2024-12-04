using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CamaraDevice : MonoBehaviour
{
    int indiceCamaraActual = 0;
    WebCamTexture tex;
    public RawImage display;
    public Text nombreCamara;
    public Image image;
    public CamaraDevice SharedInstance;

   

    void Start()
    {
        if (WebCamTexture.devices.Length > 0)
        {
            nombreCamara.text = WebCamTexture.devices[indiceCamaraActual].name;
        }
        else
        {
            nombreCamara.text = "";
        }
    }

    public void SwapCam_Clicked()
    {
        if (WebCamTexture.devices.Length > 0)
        {
            indiceCamaraActual += 1;
            indiceCamaraActual %= WebCamTexture.devices.Length;
            nombreCamara.text = WebCamTexture.devices[indiceCamaraActual].name;
        }
    }

    public void CambiarEstado()
    {

        if (!Destruir())
        {
            WebCamDevice device = WebCamTexture.devices[indiceCamaraActual];
            tex = new WebCamTexture(device.name);
            display.texture = tex;
            tex.Play();
            Debug.Log(device.name);
        }
       
    }

    public bool Destruir()
    {
        if (tex != null)
        {
            display.texture = null;
            tex.Stop();
            WebCamTexture.Destroy(tex);
            //Destroy(tex);
            tex = null;
            return true;
        }
        return false;
    }

    public void sacarScreen()
    {
        Texture2D aux = TextureToTexture2D(display.texture);
        Sprite itemBGSprite = Sprite.Create(aux, new Rect(0, 0, aux.width, aux.height), new Vector2(0, 0), 1);
        Texture2D itemBGTex = itemBGSprite.texture;
        byte[] itemBGBytes = itemBGTex.EncodeToPNG();

#if UNITY_STANDALONE_OSX
        File.WriteAllBytes(Application.streamingAssetsPath + "/Item Images/Background.png", itemBGBytes);
#elif UNITY_STANDALONE_WIN
        File.WriteAllBytes(Application.persistentDataPath + "/Item Images/Background.png", itemBGBytes);
#endif

        image.sprite = Sprite.Create(aux, new Rect(0, 0, aux.width, aux.height), new Vector2(0, 0), 1);
    }
    private Texture2D TextureToTexture2D(Texture texture)
    {
        Texture2D texture2D = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false);
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture renderTexture = RenderTexture.GetTemporary(texture.width, texture.height, 32);
        Graphics.Blit(texture, renderTexture);

        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();

        RenderTexture.active = currentRT;
        RenderTexture.ReleaseTemporary(renderTexture);
        return texture2D;
    }

    void OnDestroy()
    {
        Destruir();
        //CambiarEstado();
        //Destruir();
        //Destroy(this);
        Debug.Log("se ejecuto el destroy");
    }

}
