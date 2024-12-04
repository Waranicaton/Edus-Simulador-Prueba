using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using TMPro;
using System.Threading;
using System.Net.Sockets;
using UnityEngine.SceneManagement;

public class ImprimirIps : MonoBehaviour
{
    public int listenPort = 6000;
    public volatile bool runTask = true;
    public bool estado = false;
    // Start is called before the first frame update
    void Start()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                //Debug.Log(ip.ToString());
                gameObject.transform.GetComponent<TextMeshProUGUI>().text += ip.ToString() +"\n";
            }
        }
        //throw new System.Exception("NO network adapters with an IPV4 address in the system!");
    }

    // Update is called once per frame

    
    void Update()
    {
    }

    public void CambiarEstado()
    {
        estado = !estado;
        gameObject.SetActive(estado);
    }
    private void OnDestroy()
    {
        runTask = false;
    }

}
