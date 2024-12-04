using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ValorEjeZ : MonoBehaviour
{

    public TMP_Text gradoZ;
    public int auxZ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gradoZ.text = ((int)transform.eulerAngles.y).ToString();
    }
}
