using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatosOrganos
{
    public string nombre { set; get; }
    public string descripcion { set; get; }
    public int puntos { set; get; }


    public DatosOrganos()
    {
        nombre = "";
        descripcion = "";
        puntos = 0;
    }

    public DatosOrganos(string nombre, string descripcion, int puntos)
    {

        this.nombre = nombre;
        this.descripcion = descripcion;
        this.puntos = puntos;
    }
}
