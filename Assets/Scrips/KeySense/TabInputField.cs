using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Windows.Forms;

public class TabInputField : MonoBehaviour
{
    public TMP_InputField nombre;
    public TMP_InputField gradosTot;
    public TMP_InputField gradosEntreI;
    public TMP_InputField descripcion;
    public GameObject comenzar;
    public GameObject menuInterno;
    public GameObject screenManager;
    public GameObject cube;
    public GameObject grid;
    public GameObject gridZ;
    public GameObject menuDatos;

    public int inputSelected;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
        {
            inputSelected--;
            if (inputSelected < 0)
            {
                inputSelected = 3;
            }
            SelectInputField();
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            inputSelected++;
            inputSelected %= 4;
            SelectInputField();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //comenzar.transform.GetComponent<CargarDatos>().asignar();
            Debug.Log("asignar");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuInterno.SetActive(!menuInterno.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.R) && !menuDatos.activeSelf)
        {
            cube.transform.GetComponent<CamaraJugador>().resetRot();
            screenManager.transform.GetComponent<ScreenManager>().reiniciar();
            grid.transform.GetComponent<GridInst>().reiniciar();
            gridZ.transform.GetComponent<GridInst>().reiniciar();
            
        }
    }

    void SelectInputField()
    {
        switch (inputSelected)
        {
            case 0: nombre.Select();
                break;
            case 1: gradosTot.Select();
                break;
            case 2: gradosEntreI.Select();
                break;
            case 3: descripcion.Select();
                break;
        }
    }

    public void nameSelected() => inputSelected = 0;
    public void gradosTotSelected() => inputSelected = 1;
    public void gradosEntreISelected() => inputSelected = 2;
    public void descripcionSelected() => inputSelected = 3;


}
