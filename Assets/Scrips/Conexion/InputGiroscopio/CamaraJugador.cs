using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraJugador : MonoBehaviour
{
    // Start is called before the first frame update
    private Quaternion rotacion;
    
    private bool primerRotacion;
    private Quaternion rotacionInversa;
    void Start()
    {
        primerRotacion = true;
    }

    // Update is called once per frame
    void Update()
    {
        if ((primerRotacion) & (InputManager.GetSharedInstance().GetInput().GyroscopeX != 0))
        {
            primerRotacion = false;
            rotacionInversa = Quaternion.Inverse(Quaternion.Euler(InputManager.GetSharedInstance().GetInput().GyroscopeX, InputManager.GetSharedInstance().GetInput().GyroscopeY, InputManager.GetSharedInstance().GetInput().GyroscopeZ));
        }
        rotacion = Quaternion.Euler(InputManager.GetSharedInstance().GetInput().GyroscopeX, InputManager.GetSharedInstance().GetInput().GyroscopeY, InputManager.GetSharedInstance().GetInput().GyroscopeZ);
        gameObject.transform.rotation = rotacionInversa * rotacion;
    }

    public void resetRot()
    {
        primerRotacion = true;
    }

    private void OnDestroy()
    {
        cambiarMenuConexion(false);
    }

    private void cambiarMenuConexion(bool estado)
    {
        if (MenuConexionManager.GetSharedInstance() != null)
        {
            MenuConexionManager.GetSharedInstance().gameObject.SetActive(estado);
        }
    }

    public  void cambiarEstadoMenuConexion(bool estado)
    {
        cambiarMenuConexion(estado);
    }
}
