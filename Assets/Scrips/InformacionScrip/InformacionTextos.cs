using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformacionTextos
{
    public float[] posX;

    public float[] posY;

    public string[] nombre;
    // Start is called before the first frame update
    public InformacionTextos()
    {
        nombre = null;
        posX = null;
        posY = null;
    }

    public InformacionTextos(string[] nombre, float[] posX, float[] posY)
    {
        this.nombre = nombre;
        this.posX = posX;
        this.posY = posY;
    }
}
