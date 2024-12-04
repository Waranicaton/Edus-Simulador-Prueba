using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InteraccionTexto : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    static bool interaccionHabilitada;
    public static GameObject seleccionado;
    public bool ubicado = false;
    private Color colorBase;
    public static GameObject ultimo;
    public Sprite spriteBase;
    public Sprite spriteHover;
    public Sprite spriteSelected;
    // Start is called before the first frame update
    void Start()
    {
        //colorBase = this.GetComponent<TextMeshProUGUI>().color;
        //ubicado = false;//if tercera dificultad
        ultimo = null;
        colorBase = new Color(130f / 255f, 234f / 255f, 223f / 255f, 1f);
        //if (interaccionHabilitada)
        //    this.GetComponent<TextMeshProUGUI>().color = new Color(0, 1, 0, 0);

        // else
        //   this.GetComponent<TextMeshProUGUI>().color = new Color(0, 1, 0, 1);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        if ((InteraccionTexto.seleccionado != this.gameObject)&&(!ubicado))
        {
            this.transform.GetComponent<Image>().sprite = spriteHover;
        }
        //if (interaccionHabilitada)
        //this.GetComponent<TextMeshProUGUI>().color = new Color(0, 1, 0, 1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if ((InteraccionTexto.seleccionado != this.gameObject) && (!ubicado))
        {
            this.transform.GetComponent<Image>().sprite = spriteBase;
        }
        
        //if (interaccionHabilitada)
        // this.GetComponent<TextMeshProUGUI>().color = new Color(0, 1, 0, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
        if (!ubicado)
        {
            if (seleccionado == null)
            {

                this.transform.GetComponent<Image>().sprite = spriteSelected;
                
            }
            else
            {
                InteraccionTexto.seleccionado.transform.GetComponent<Image>().sprite = spriteBase;
                Debug.Log("deberia cambiar el ultiomo color ");
                this.transform.GetComponent<Image>().sprite = spriteSelected;

            }

            seleccionado = this.gameObject;
            /*
            Transform padre = this.transform.parent;

            seleccionado = padre.gameObject;
            / 
            if (ultimo == null)
            {
                ultimo = this.gameObject;
                this.GetComponent<TextMeshProUGUI>().color = Color.white;
                ultimo = this.gameObject;
            }
            else
            {
                ultimo.GetComponent<TextMeshProUGUI>().color = colorBase;
                this.GetComponent<TextMeshProUGUI>().color = Color.white;
                ultimo = this.gameObject;
            }
                */
        }


    }


    public void Ubicar()
    {
        ubicado = true;

        try
        {
            this.transform.GetComponent<Image>().sprite = spriteBase;
        }
        catch
        {

        }

    }

    // Update is called once per frame
    public static void ActivarScript(bool activar)
    {
        interaccionHabilitada = activar;
    }
}
