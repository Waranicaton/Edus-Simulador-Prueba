using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantallaConectando : MonoBehaviour
{
    // Start is called before the first frame update
   
    private void Update()
    {
        if(WebSocketPrueba.GetSharedInstance().conectado)
        {
            gameObject.SetActive(false);
        }
    }
}
