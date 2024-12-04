using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UbicarTexto : MonoBehaviour, IPointerClickHandler
{
    private int id;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetId(int id)
    {
        this.id = id;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(id);
        if (InteraccionTexto.seleccionado != null)
        {
            this.transform.parent.parent.parent.Find("Barra_Punto").GetComponent<BarrasProgreso>().Agregar();
            this.transform.parent.parent.parent.Find("Barra_Autoevaluacion").GetComponent<BarrasProgreso>().Agregar();
            InteraccionTexto.seleccionado.transform.GetComponent<InteraccionTexto>().Ubicar();
            InteraccionTexto.seleccionado.transform.position = this.transform.position;
            Debug.Log(ImageManager3.datosPuntoActual.nombre);
            if (ManejadorPuntoInteres.resultados.ContainsKey(ImageManager3.datosPuntoActual.nombre))
            {
                if (ManejadorPuntoInteres.resultados[ImageManager3.datosPuntoActual.nombre].ContainsKey("Imagen" + ImageManager3.imagen))
                {
                    if (ManejadorPuntoInteres.resultados[ImageManager3.datosPuntoActual.nombre]["Imagen" + ImageManager3.imagen].ContainsKey("Texto" + id))
                    {
                        ManejadorPuntoInteres.resultados[ImageManager3.datosPuntoActual.nombre]["Imagen" + ImageManager3.imagen]["Texto" + id] = QuitarTildes(InteraccionTexto.seleccionado.transform.Find("TextoParaCompletarAutoevaluacion").GetComponent<TextMeshProUGUI>().text);

                    }
                    else
                    {
                        ManejadorPuntoInteres.resultados[ImageManager3.datosPuntoActual.nombre]["Imagen" + ImageManager3.imagen].Add("Texto" + id, QuitarTildes(InteraccionTexto.seleccionado.transform.Find("TextoParaCompletarAutoevaluacion").GetComponent<TextMeshProUGUI>().text));
                    }
                }
                else
                {
                    Dictionary<string, string> miDiccionario = new Dictionary<string, string>();
                    miDiccionario.Add("Texto" + id, QuitarTildes(InteraccionTexto.seleccionado.transform.Find("TextoParaCompletarAutoevaluacion").GetComponent<TextMeshProUGUI>().text));
                    ManejadorPuntoInteres.resultados[ImageManager3.datosPuntoActual.nombre].Add("Imagen" + ImageManager3.imagen, miDiccionario);
                }
            }
            else
            {
                Dictionary<string, string> miDiccionario = new Dictionary<string, string>();
                miDiccionario.Add("Texto" + id, QuitarTildes(InteraccionTexto.seleccionado.transform.Find("TextoParaCompletarAutoevaluacion").GetComponent<TextMeshProUGUI>().text));
                Dictionary<string, Dictionary<string, string>> miDiccionario2 = new Dictionary<string, Dictionary<string, string>>();
                miDiccionario2.Add("Imagen" + ImageManager3.imagen, miDiccionario);
                ManejadorPuntoInteres.resultados.Add(ImageManager3.datosPuntoActual.nombre, miDiccionario2);
            }
            InteraccionTexto.seleccionado = null;
            Destroy(this.gameObject);
            
        }
    }

    string QuitarTildes(string input)
    {
        string normalizedString = input.Normalize(NormalizationForm.FormD);
        StringBuilder stringBuilder = new StringBuilder();

        foreach (char c in normalizedString)
        {
            UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
