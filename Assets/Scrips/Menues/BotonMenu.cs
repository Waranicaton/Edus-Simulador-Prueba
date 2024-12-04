using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonMenu : MonoBehaviour
{

    public Button botonPausa;
    public GameObject menuSoloHigado;
    public GameObject recuadroExterno;
    private Sprite recuadroNormal;
    public Sprite recuadroPausa;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void cambiarEstadoMenuInterno()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void cambiarEstadoMenuDatos()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void cambiarColor()
    {
        Debug.Log(botonPausa.colors.disabledColor == Color.green);
        ColorBlock color = ColorBlock.defaultColorBlock;

        if (botonPausa.colors.normalColor == Color.green)
        {
            //recuadroNormal = recuadroExterno.GetComponent<Image>().sprite;
            //recuadroExterno.GetComponent<Image>().sprite = recuadroPausa;
            //recuadroExterno.GetComponent<Image>().SetNativeSize();
            color.normalColor = Color.red;
            color.selectedColor = Color.red;
            color.disabledColor = Color.red;
            color.pressedColor = Color.red;
            color.highlightedColor = Color.red;
            botonPausa.colors = color;
        }
        else
        {
            //recuadroExterno.GetComponent<Image>().sprite = recuadroNormal;
            color.normalColor = Color.green;
            color.selectedColor = Color.green;
            color.disabledColor = Color.green;
            color.pressedColor = Color.green;
            color.highlightedColor = Color.green;
            botonPausa.colors = color;
        }
    }


    public void colorDefault()
    {
        ColorBlock color = ColorBlock.defaultColorBlock;
        color.normalColor = Color.green;
        color.selectedColor = Color.green;
        color.disabledColor = Color.green;
        color.pressedColor = Color.green;
        color.highlightedColor = Color.green;
        botonPausa.colors = color;
    }

    public void desactivarMenuSoloHigado()
    {
        menuSoloHigado.SetActive(false);
    }
}
