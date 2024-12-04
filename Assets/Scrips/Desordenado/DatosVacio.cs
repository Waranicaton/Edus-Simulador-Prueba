using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatosVacio : MonoBehaviour
{
    public int cantImagenes { set; get; }

    public int width { set; get; }

    public int height { set; get; }

    public DatosVacio()
    {
        cantImagenes = 0;
        width = 1;
        height = 1;
    }

    public DatosVacio(int cantImagenes, int width, int height)
    {
        this.cantImagenes = cantImagenes;
        this.width = width;
        this.height = height;
    }
}
