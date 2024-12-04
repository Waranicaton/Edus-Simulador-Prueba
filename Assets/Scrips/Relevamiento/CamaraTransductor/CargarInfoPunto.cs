using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CargarInfoPunto : MonoBehaviour
{
    public GameObject tituloPunto;
    public GameObject contenido;
   

    public void actualizarPuntoYTexto(int numero)
    {
        switch (numero)
        {
            case 1:
                tituloPunto.GetComponent<TextMeshProUGUI>().text = "PUNTO 1";
                contenido.GetComponent<TextMeshProUGUI>().text = "Corte axial y sagital de epigastrio. \nVena porta y sus ramas izquierdas. \nVisualización de los segmentos hepáticos izquierdos (II, III y IV) y el segmento I.";
                break;
            case 2:
                tituloPunto.GetComponent<TextMeshProUGUI>().text = "PUNTO 2";
                contenido.GetComponent<TextMeshProUGUI>().text = "Corte axial.\nVena porta. Bifurcación y ramas derechas.\nArteria hepática. Colédoco. Segmento IV.\nVenas suprahepáticas y relación con la vena cava.   Segmentos II, IV, VIII y VII.";
                break;
            case 3:
                tituloPunto.GetComponent<TextMeshProUGUI>().text = "PUNTO 3";
                contenido.GetComponent<TextMeshProUGUI>().text = "SUBCOSTAL MEDIO LATERAL.\nCorte axial.\nVenas suprahepáticas y relación con la vena cava.\nDiafragma e imagen en espejo.";
                break;
            case 4:
                tituloPunto.GetComponent<TextMeshProUGUI>().text = "PUNTO 4";
                contenido.GetComponent<TextMeshProUGUI>().text = "INTERCOSTAL BAJO VERTICAL.\nCorte sagital.\nEspacio de Morrison entre hígado y riñón derecho.\nSegmentos derechos V, VI, VII y VIII.";
                break;
            case 5:
                tituloPunto.GetComponent<TextMeshProUGUI>().text = "PUNTO 5";
                contenido.GetComponent<TextMeshProUGUI>().text = "INTERCOSTAL AXIAL.\nCorte axial.\nVena suprahepática, diafragma y Segmento VIII.";
                break;
            case 6:
                tituloPunto.GetComponent<TextMeshProUGUI>().text = "PUNTO 6";
                contenido.GetComponent<TextMeshProUGUI>().text = "VESICULA SUBCOSTAL.\nCorte sagital.\nVesícula biliar, Vena Cava y Vena Porta.\nSegmentos derechos V y VI.\nColédoco.";
                break;
            case 7:
                tituloPunto.GetComponent<TextMeshProUGUI>().text = "PUNTO 7";
                contenido.GetComponent<TextMeshProUGUI>().text = "VESICULA LATERAL.\nVesícula biliar (fondo, cuerpo y cuello). Conducto cístico.\nSegmentos derechos V y VI.";
                break;

        }
    }


}
