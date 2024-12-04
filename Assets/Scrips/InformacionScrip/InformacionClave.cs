using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformacionClave
{
    public int cantPuntos { set; get; }

    //public string header { set; get; }

    //public string content { set; get; }
    public int[] posX;

    public int[] posY;

    public int[] posZ;

    public string[] imagen;
   
    
    public InformacionClave()
    {
        cantPuntos = 0;
        posX = null;
        posY = null;
        posZ = null; 
        imagen = null;
    }

    public InformacionClave(int cantPuntos, int[] posX, int[] posY, int[] posZ,string[] imagen)
    {
        this.cantPuntos = cantPuntos;
        this.posX = posX;
        this.posY = posY;
        this.posZ = posZ;
        this.imagen = imagen;
    }
}
