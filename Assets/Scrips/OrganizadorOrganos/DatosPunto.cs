using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatosPunto
{
    public string nombre { set; get; }

    public int cantGradosTotal { set; get; }

    public double cantGradosEntreImagen { set; get; }

    public string descripcion { set; get; }

    public int width { set; get; }

    public int height { set; get; }

    public bool info { set; get; }

    public DatosPunto()
    {
        this.nombre = "";
        this.cantGradosTotal = 0;
        this.cantGradosEntreImagen = 0;
        this.descripcion = "";
        this.width = 0;
        this.height = 0;
        this.info = false;

    }

    public DatosPunto(string nombre, int cantGradosTotal, double cantGradosEntreImagen, string descripcion, int width, int height, bool info)
    {
        this.nombre = nombre;
        this.cantGradosTotal = cantGradosTotal;
        this.cantGradosEntreImagen = cantGradosEntreImagen;
        this.descripcion = descripcion;
        this.width = width;
        this.height = height;
        this.info = info;
    }
}

