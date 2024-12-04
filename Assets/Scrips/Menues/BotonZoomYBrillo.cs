using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonZoomYBrillo : MonoBehaviour
{
    public GameObject padreZoom;
    public GameObject padreBrillo;
    public Sprite texturaApagado;
    public Sprite texturaPrendido;
    public ImageManager3 imageManager;
    private int indiceBrillo;
    private int indiceZoom;
    public Material brilloZoom;
    public Material brilloZoomAnimacion;

    private void Start()
    {
        foreach (Transform hijo in padreZoom.transform)
        {
            hijo.GetComponent<Image>().sprite = texturaApagado;
        }
        foreach (Transform hijo in padreBrillo.transform)
        {
            hijo.GetComponent<Image>().sprite = texturaApagado;
        }
        indiceBrillo = 0;
        indiceZoom = 0;
    }

    public void ZoomIn()
    {
        if (indiceZoom < 24)
        { 
            if (indiceZoom == -1)
            {
                indiceZoom++;
            }
            Transform hijo = padreZoom.transform.GetChild(indiceZoom);
            hijo.GetComponent<Image>().sprite = texturaPrendido;
            ZoomIn2();
            indiceZoom++;
        }
    }

    public void ZoomDown()
    {
        if (indiceZoom > 0)
        {
            indiceZoom--;
            Transform hijo = padreZoom.transform.GetChild(indiceZoom);
            hijo.GetComponent<Image>().sprite = texturaApagado;
            ZoomDown2();
        }
    }

    public void BrilloIn()
    {
        if (indiceBrillo < 24)
        {
            if (indiceBrillo < 0)
            {
                indiceBrillo++;
            }
            Transform hijo = padreBrillo.transform.GetChild(indiceBrillo);
            hijo.GetComponent<Image>().sprite = texturaPrendido;
            BrilloIn2();
            indiceBrillo++;
        }
    }

    public void BrilloDown()
    {
        if (indiceBrillo > 0)
        {
            indiceBrillo--;
            Transform hijo = padreBrillo.transform.GetChild(indiceBrillo);
            hijo.GetComponent<Image>().sprite = texturaApagado;
            BrilloOut2();
        }
    }


    public void ReiniciarBotonesBrillo()
    {
        //Reinicia de manera grafica los botones osea las barritas
        while (indiceBrillo > 0)
        {
            indiceBrillo--;
            Transform hijo = padreBrillo.transform.GetChild(indiceBrillo);
            hijo.GetComponent<Image>().sprite = texturaApagado;
        }

        while (indiceZoom > 0)
        {
            indiceZoom--;
            Transform hijo = padreZoom.transform.GetChild(indiceZoom);
            hijo.GetComponent<Image>().sprite = texturaApagado;
            
        }
        brilloZoom.SetFloat("_Zoom", 1f);
        brilloZoom.SetFloat("_Brightness", 0f);
        brilloZoomAnimacion.SetFloat("_Zoom", 1f);
        brilloZoomAnimacion.SetFloat("_Brightness", 0f);

    }

    //Las 4 que siguen estan para que lo de arriba se lea mas facil, acomodan el material zoomybrillo
    //segun suba o baje 
    //
    private void ZoomIn2()
    {
        float aux = brilloZoom.GetFloat("_Zoom");
        if (aux > 0.1f)
        {
            aux = aux - 0.0375f;
            brilloZoom.SetFloat("_Zoom", aux);
            brilloZoomAnimacion.SetFloat("_Zoom", aux);
        }
    }
    private void ZoomDown2()
    {
        float aux = brilloZoom.GetFloat("_Zoom");

        if (aux < 1.0f)
        {
            aux = aux + 0.0375f;
            brilloZoom.SetFloat("_Zoom", aux);
            brilloZoomAnimacion.SetFloat("_Zoom", aux);
        }
    }
    private void BrilloIn2()
    {
        float aux = brilloZoom.GetFloat("_Brightness");
        if (aux < 1.56f)
        {
            aux = aux + 0.065f;
            brilloZoom.SetFloat("_Brightness", aux);
            brilloZoomAnimacion.SetFloat("_Brightness", aux);
            //imageObject_Superior.GetComponent<Image>().color = aux;
        }
    }
    private void BrilloOut2()
    {
        float aux = brilloZoom.GetFloat("_Brightness");
        if (aux > 0f)
        {
            aux = aux - 0.065f;
            brilloZoom.SetFloat("_Brightness", aux);
            brilloZoomAnimacion.SetFloat("_Brightness", aux);
            //imageObject_Superior.GetComponent<Image>().color = aux;
        }
    }
}
