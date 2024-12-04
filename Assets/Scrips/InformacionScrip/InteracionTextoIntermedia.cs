using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteracionTextoIntermedia : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    static bool interaccionHabilitada;
    public static GameObject seleccionado;
    public bool ubicado = false;
    private Color colorBase;
    public static GameObject ultimo;
    
    // Start is called before the first frame update
    void Start()
    {
        
        //ubicado = false;//if tercera dificultad
        ultimo = null;
        colorBase = new Color(130f / 255f, 234f / 255f, 223f / 255f, 1f);
        if (interaccionHabilitada)
            this.GetComponent<TextMeshProUGUI>().color = new Color(0, 1, 0, 0);
         else
           this.GetComponent<TextMeshProUGUI>().color = new Color(0, 1, 0, 1);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        
        if (interaccionHabilitada)
            this.GetComponent<TextMeshProUGUI>().color = new Color(0, 1, 0, 1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        

        if (interaccionHabilitada)
         this.GetComponent<TextMeshProUGUI>().color = new Color(0, 1, 0, 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        if (!ubicado)
        {
            

            
                
        }


    }


    public void Ubicar()
    {
        ubicado = true;

       

    }

    // Update is called once per frame
    public static void ActivarScript(bool activar)
    {
        interaccionHabilitada = activar;
    }
}
