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
                contenido.GetComponent<TextMeshProUGUI>().text = "Corte axial y sagital de epigastrio. \nVena porta y sus ramas izquierdas. \nVisualizaci�n de los segmentos hep�ticos izquierdos (II, III y IV) y el segmento I.";
                break;
            case 2:
                tituloPunto.GetComponent<TextMeshProUGUI>().text = "PUNTO 2";
                contenido.GetComponent<TextMeshProUGUI>().text = "Corte axial.\nVena porta. Bifurcaci�n y ramas derechas.\nArteria hep�tica. Col�doco. Segmento IV.\nVenas suprahep�ticas y relaci�n con la vena cava.   Segmentos II, IV, VIII y VII.";
                break;
            case 3:
                tituloPunto.GetComponent<TextMeshProUGUI>().text = "PUNTO 3";
                contenido.GetComponent<TextMeshProUGUI>().text = "SUBCOSTAL MEDIO LATERAL.\nCorte axial.\nVenas suprahep�ticas y relaci�n con la vena cava.\nDiafragma e imagen en espejo.";
                break;
            case 4:
                tituloPunto.GetComponent<TextMeshProUGUI>().text = "PUNTO 4";
                contenido.GetComponent<TextMeshProUGUI>().text = "INTERCOSTAL BAJO VERTICAL.\nCorte sagital.\nEspacio de Morrison entre h�gado y ri��n derecho.\nSegmentos derechos V, VI, VII y VIII.";
                break;
            case 5:
                tituloPunto.GetComponent<TextMeshProUGUI>().text = "PUNTO 5";
                contenido.GetComponent<TextMeshProUGUI>().text = "INTERCOSTAL AXIAL.\nCorte axial.\nVena suprahep�tica, diafragma y Segmento VIII.";
                break;
            case 6:
                tituloPunto.GetComponent<TextMeshProUGUI>().text = "PUNTO 6";
                contenido.GetComponent<TextMeshProUGUI>().text = "VESICULA SUBCOSTAL.\nCorte sagital.\nVes�cula biliar, Vena Cava y Vena Porta.\nSegmentos derechos V y VI.\nCol�doco.";
                break;
            case 7:
                tituloPunto.GetComponent<TextMeshProUGUI>().text = "PUNTO 7";
                contenido.GetComponent<TextMeshProUGUI>().text = "VESICULA LATERAL.\nVes�cula biliar (fondo, cuerpo y cuello). Conducto c�stico.\nSegmentos derechos V y VI.";
                break;

        }
    }


}
