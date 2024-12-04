using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatosInterfazCuerpo
{
    public float[] posX;
    public float[] posY;

    public DatosInterfazCuerpo()
    {
        posX = null;
        posY = null;
    }

    public DatosInterfazCuerpo(float[] posX, float[] posY)
    {
        this.posX = posX;
        this.posY = posY;
    }
}
