using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatosGuardados 
{
    public bool reglaFirewall { set; get; }  

    public DatosGuardados()
    {
        reglaFirewall = false;
    }

    public DatosGuardados(bool ReglaFirewall)
    {
        reglaFirewall = ReglaFirewall;
    }
   
}
