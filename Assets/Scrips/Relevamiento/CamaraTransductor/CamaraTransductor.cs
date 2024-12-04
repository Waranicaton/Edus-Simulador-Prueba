using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamaraTransductor : MonoBehaviour
{
    public Camera cameraToDisplay;
    private RenderTexture renderTexture;

    void Start()
    {
        renderTexture = new RenderTexture(1920, 1080, 24);
        cameraToDisplay.targetTexture = renderTexture;
        RawImage rawImage = GetComponent<RawImage>();
        rawImage.texture = renderTexture;
        rawImage.SetNativeSize();
    }
}




