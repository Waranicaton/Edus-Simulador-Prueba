using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridInst : MonoBehaviour
{
    [SerializeField] Image gridPrefab;
    [SerializeField] Canvas canvas;
    [SerializeField] Image image;
    private Image[,] casillas;
    private int posActualX;
    private int posActualY;
    private int cantGradosTotal;
    private double cantGradosEntreImagen;
    private Color colorAux = Color.cyan;
    private int ejeZ;

    private void IniciarStart()
    {
        int cantImagenesaux = (int)(cantGradosTotal / cantGradosEntreImagen);
        float tamImage = image.GetComponent<RectTransform>().rect.height;
        float tamCasilla = tamImage / cantImagenesaux;
        Vector3 posInicialZ = -new Vector3((tamImage / 2) - (tamCasilla / 2), 1, 0);
        Vector3 posInicial = -new Vector3(1, 1, 0) * (tamImage / 2) + new Vector3(1, 1, 0) * (tamCasilla / 2);
        float tamCasillaReducido = tamCasilla * 0.85f;
        //Debug.Log(posInicial);
        posActualY = -1;
        if (ejeZ != 1) ejeZ = cantImagenesaux; 
        casillas = new Image[cantImagenesaux, ejeZ];
        posActualX = cantImagenesaux / 2;

        for (int i = 0; i < cantImagenesaux; i++)
        {
            
            for (int j = 0; j < ejeZ; j++)
            {
                Image grid = Instantiate(gridPrefab) as Image;
                grid.GetComponent<Image>().color = Color.red;
                if (ejeZ == 1)
                {
                    grid.transform.position = new Vector3(i * tamCasilla, j * tamCasilla, 0f) + posInicialZ;
                    grid.transform.localScale = new Vector3(1*tamCasillaReducido, 0.85f*tamImage, 0);
                }
                else
                {
                    grid.transform.position = new Vector3(i * tamCasilla, j * tamCasilla, 0f) + posInicial;
                    grid.transform.localScale = new Vector3(1, 1, 0) * tamCasillaReducido;
                }
                grid.transform.SetParent(image.transform, false);
                casillas[i, j] = grid;
            }
        }

    }

    public void cambiarEstado(int posX, int posY)
    {
        casillas[posX, posY].GetComponent<Image>().color = Color.blue;
        if ((posX != posActualX) || (posY != posActualY))
        {
            if (posActualY != -1)
            {
                casillas[posActualX, posActualY].GetComponent<Image>().color = Color.green;
            }
            if (posX != posActualX)
            {
                if (posY != posActualY)
                {
                    posActualX = posX;
                    posActualY = posY;
                }
                else
                {
                    posActualX = posX;
                }

            }
            else
            {
                posActualY = posY;
            }
        }
    }

    public void cambiarEstado2(int posX, int posY)
    {
        if (colorAux == Color.cyan)
        {
            colorAux = casillas[posX, posY].GetComponent<Image>().color;
        }
        
        if ((posX != posActualX) || (posY != posActualY))
        {
            if (posActualY != -1)
            {
                casillas[posActualX, posActualY].GetComponent<Image>().color = colorAux;
                colorAux = casillas[posX, posY].GetComponent<Image>().color;
            }
            if (posX != posActualX)
            {
                if (posY != posActualY)
                {
                    posActualX = posX;
                    posActualY = posY;
                }
                else
                {
                    posActualX = posX;
                }

            }
            else
            {
                posActualY = posY;
            }
        }
        casillas[posX, posY].GetComponent<Image>().color = Color.blue;

    }
    
    public void setPosColor(int posX, int posY, Color color)
    {
        casillas[posX, posY].GetComponent<Image>().color = color;
        if (color == Color.blue)
        {
            posActualX = posX;
            posActualY = posY;
        }
    }

    public void fueraLimite()
    {
        casillas[posActualX, posActualY].GetComponent<Image>().color = Color.yellow;
    }

    public void cargarDatos(int cantGradosTotal, double cantGradosEntreImagen, int ejeZ) //si ejeZ es 1 va a hacer la grilla como un vector horizontal
    {
        if (ejeZ == 1)
        {
            this.cantGradosTotal = 5;
            this.cantGradosEntreImagen = 1;
        }
        else
        {
            this.cantGradosTotal = cantGradosTotal;
            this.cantGradosEntreImagen = cantGradosEntreImagen;
        }
        this.ejeZ = ejeZ;
        this.IniciarStart();
    }

    public void reiniciar()
    {
        int cantImagenesaux = (int)(cantGradosTotal / cantGradosEntreImagen);
        for (int i = 0; i < cantImagenesaux; i++)
        {
            for (int j = 0; j < ejeZ; j++)
            {
                casillas[i, j].GetComponent<Image>().color = Color.red;
            }
        }
    }
}
