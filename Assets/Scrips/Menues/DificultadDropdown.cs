 using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

public class DificultadDropdown : MonoBehaviour
{
    public  TMP_Dropdown dropdown;
    public GameObject imageManager;

    private bool isPaused = false;

    public void Iniciar(int difInicial)
    {
        dropdown = this.GetComponent<TMP_Dropdown>();
        dropdown.value = difInicial;
        dropdown.onValueChanged.AddListener(HandleDropdownValueChanged);
        HandleDropdownValueChanged(difInicial);
        
    }

    void HandleDropdownValueChanged(int value)
    {
        int selectedIndex = dropdown.value;
        string selectedOption = dropdown.options[selectedIndex].text;
        Debug.Log(selectedOption);
        // Realiza acciones según la opción seleccionada
        switch (value)
        {
            case 0:
                // Acciones para la opción 1
                imageManager.GetComponent<ImageManager3>().CambiarDificultad(0);
                Debug.Log("Seleccionaste la opción 1");
                break;

            case 1:
                // Acciones para la opción 2
                imageManager.GetComponent<ImageManager3>().CambiarDificultad(1);
                Debug.Log("Seleccionaste la opción 2");
                break;

            // Agrega más casos según sea necesario

            default:
                break;
        }
    }

    void Start()
    {


        

    }

    public void ToggleDropdown(bool enable)
    {
        dropdown.interactable = enable;
        dropdown.Hide(); // Asegúrate de ocultar el Dropdown al deshabilitarlo
    }

    // Método para detectar cuando el juego está pausado o reanudado
    public void SetPauseState()
    {
        isPaused = !isPaused;
        ToggleDropdown(!isPaused); // Invierte el estado del Dropdown según la pausa
    }


}
