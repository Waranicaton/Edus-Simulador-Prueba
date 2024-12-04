using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class InputFieldSimulacion : MonoBehaviour
{
    public static InputFieldSimulacion instance;
    public GameObject menuInterno;
    public GameObject menuCarga;
    public GameObject camaraJugador;
    public GameObject canvas;
    public GameObject imageManager;
    public TMP_Dropdown organos;
    public CargarOrganos cargarOrganos;
    public GameObject botonMenu;
    public DificultadDropdown dificultadDropdown;
    public BotonZoomYBrillo botonZoomYBrillo;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (menuCarga.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Menu");
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                menuCarga.SetActive(false);
                imageManager.SetActive(true);
                camaraJugador.GetComponent<CamaraJugador>().cambiarEstadoMenuConexion(true);
                cargarOrganos.cargarGrilla();
            }
        }
        else {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                menuInterno.SetActive(!menuInterno.activeSelf);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetAll();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                dificultadDropdown.SetPauseState();
                
                menuInterno.GetComponent<BotonMenu>().cambiarColor();
                imageManager.GetComponent<ImageManager3>().Pausa();
            }
        }
    }


    public void ResetAll()
    {
        //Reinicia el simulador con la r y con el boton de reset
        // lo pongo aca pero deberia ir en otro lado unificado
        //
        if (imageManager.GetComponent<ImageManager3>().getPausa() == true)
        {
            botonMenu.GetComponent<BotonMenu>().cambiarColor();
        }
        camaraJugador.transform.GetComponent<CamaraJugador>().resetRot();
        //ApuntPartes es para mover el transductorChiquito, ni lo estamos usando ahora
        //canvas.transform.GetComponent<ApuntPartes>().reset();
        //
        if (imageManager.transform.GetComponent<ImageManager3>().getPausa())
        {
            imageManager.transform.GetComponent<ImageManager3>().Pausa();
        }
        menuInterno.SetActive(false);
        botonZoomYBrillo.ReiniciarBotonesBrillo();
    }
}
