using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class InputManager : MonoBehaviour
{
    private static InputManager sharedInstance;
    private InputModel inputSensors;
    private RemoteInputListener remoteInputListener;

    public static InputManager GetSharedInstance()
    {
        return sharedInstance;
    }
    public InputModel GetInput()
    {
        return inputSensors;
    }

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
            DontDestroyOnLoad(gameObject);
        }else if (sharedInstance != this)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        inputSensors = new InputModel();
        remoteInputListener = RemoteInputListener.GetSharedInstance();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        inputSensors = remoteInputListener.GetInputModel();
    }

}
