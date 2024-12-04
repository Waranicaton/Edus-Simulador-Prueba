using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click()
    {
        this.GetComponent<Button>().onClick.Invoke();
    }
}
