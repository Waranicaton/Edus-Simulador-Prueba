using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ApuntPartes : MonoBehaviour
{
    private Vector3 posInicial;
    LayerMask mask;
    public Text texto;
    public string textoAux;
    public Vector3 auxPos;
    public bool apuntando;
    public GameObject transductor;

    // Start is called before the first frame update
    void Start()
    {
        mask = LayerMask.GetMask("RayCast");
        apuntando = false;
        posInicial = transductor.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Raycast(origen, direccion, out hit, distancia, mascara);

               
        RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.zero, mask);
        if (hit.collider != null)
        {
            if (!apuntando) {
                textoAux = texto.text;
                if (hit.collider.tag == "MouseUp")
                {
                    texto.text = hit.collider.transform.GetComponent<Text>().text;
                    auxPos = hit.collider.transform.position;
                }
                apuntando = true;
            }
        }
        else if (apuntando)
        {
            texto.text = textoAux;
            apuntando = false;
        }
    }

    public void ActuAux(string dato)
    {
        textoAux = dato;
        auxPos.y = auxPos.y + 6;
        transductor.transform.position = auxPos;
        apuntando = false;
    }

    public void reset()
    {
        apuntando = false;
        texto.text = "";
        transductor.transform.position = posInicial;
    }
}
