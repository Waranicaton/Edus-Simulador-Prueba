using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarrasProgreso : MonoBehaviour
{
    private int valorMaximo = 0;
    private int valorActual = 0;

    public void ValorMaximo(int valorMaximo, int valorActualInicial)
    {
        this.valorMaximo = valorMaximo;
        valorActual = valorActualInicial;
        this.transform.Find("Texto_Porcentaje").gameObject.GetComponent<TextMeshProUGUI>().text = (int)(((float)valorActual / valorMaximo) * 100) + "% completo";
        this.transform.Find("Slider_Porcentaje").gameObject.GetComponent<Slider>().value = (float)valorActual / valorMaximo;
    }

    public void Agregar()
    {
        valorActual++;
        this.transform.Find("Texto_Porcentaje").gameObject.GetComponent<TextMeshProUGUI>().text = (int)(((float)valorActual / valorMaximo) * 100) + "% completo";
        this.transform.Find("Slider_Porcentaje").gameObject.GetComponent<Slider>().value = (float)valorActual / valorMaximo;
    }

    public void Restar(int valor)
    {
        valorActual -= valor;
        this.transform.Find("Texto_Porcentaje").gameObject.GetComponent<TextMeshProUGUI>().text = (int)(((float)valorActual / valorMaximo) * 100) + "% completo";
        this.transform.Find("Slider_Porcentaje").gameObject.GetComponent<Slider>().value = (float)valorActual / valorMaximo;
    }

    public void ReiniciarBarra()
    {
        valorActual = 0;
    }
}
