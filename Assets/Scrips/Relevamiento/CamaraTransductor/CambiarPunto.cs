using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CambiarPunto : MonoBehaviour
{
    public GameObject puntoAnterior = null;
    public Sprite texturaActivo;
    public Sprite texturaDesactivado;
    public void CambiarEstadoActivo(GameObject actual)
    {
        if (puntoAnterior != null)
        {
            puntoAnterior.GetComponent<Image>().sprite = texturaDesactivado;
            puntoAnterior.GetComponent<Image>().SetNativeSize();
        }
        actual.GetComponent<Image>().sprite = texturaActivo;
        actual.GetComponent<Image>().SetNativeSize();
        puntoAnterior = actual;
        
    }
}
