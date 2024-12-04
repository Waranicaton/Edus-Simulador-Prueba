using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formato2
{

    [JsonProperty("event")]
    public string _event { set; get; }
    [JsonProperty("data")]
    public FormatoObject data { set; get; }
    







    public Formato2(FormatoObject formatoObject)
    {
        _event = "identidad";
        data = formatoObject;
    }
}
