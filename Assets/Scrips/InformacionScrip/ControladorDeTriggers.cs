using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorDeTriggers : MonoBehaviour
{
    public GameObject pantalla;
    private GameObject puntoClave;
    public GameObject puntoClaveprefav;
    private string header;
    private string content;
   

    public void cargarInfo(string header,string content)
    {
        this.header = header;
        this.content = content;
    }
    public void crearPuntoClave(float Xpos,float Ypos)
    {
        if (puntoClave != null)
        {
            Destroy(puntoClave);
            puntoClave = null;
        }
        puntoClave = Instantiate(puntoClaveprefav, new Vector3(0, 0, 0), Quaternion.identity);
        puntoClave.GetComponent<ToolTipTrigger>().content = content;
        puntoClave.GetComponent<ToolTipTrigger>().header = header;
        puntoClave.transform.SetParent(pantalla.transform);
        puntoClave.transform.localPosition = new Vector3(Xpos,Ypos,0);
    }

    public void retirarPuntoClave() {

        Destroy(puntoClave);
        puntoClave = null;
    
    }


}
