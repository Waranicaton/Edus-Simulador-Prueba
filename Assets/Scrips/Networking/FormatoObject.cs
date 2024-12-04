using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormatoObject 
{

    [JsonProperty("token")]
    public string token { set; get; }
    [JsonProperty("id")]
    public string id { set; get; }
    [JsonProperty("app")]
    public string app { set; get; }
    [JsonProperty("sendData")]
    public Boolean sendData { set; get; }


    public FormatoObject(string token2, string app2, Boolean senddata2, string id2)
    {

        token = token2;
        app = app2;
        sendData = senddata2;
        id = id2;   
    }
}