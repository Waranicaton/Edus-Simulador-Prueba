using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using System.Numerics;

public struct imagenAGuardar
{
    public byte[] imagen;
    public int x;
    public int y;
    public int z;

    public imagenAGuardar(byte[] imagen, int x, int y, int z)
    {
        this.imagen = imagen;
        this.x = x;
        this.y = y;
        this.z = z;
    }
}

public class ScreenManager : MonoBehaviour
{

    public GameObject camaraDevice;
    public RawImage display;
    //public ScreenManager SharedInstance;

    private Boolean[,,] matrizBoolScreenShots;
    private Stack<imagenAGuardar> imagenesAGuardar;

    private int cantGradosTotal;
    private double limSuperior;
    private double limInferior;
    private double cantGradosEntreImagen;
    private Vector3 vectorsin;
    public GameObject cubo;

    private double auxX;
    private double auxY;
    private double auxZ;
    private int posImagenMatrizX = 0;
    private int posImagenMatrizY = 0;
    private int posImagenMatrizZ = 0;
    private int posImagenMatrizZAnterior;

    private double anguloTruncadoX;
    private double anguloTruncadoY;
    private bool camaraActiva;
    public GridInst grid;
    public GridInst gridZ;
    private String path;
    //public String nombreCarpeta;
    public TMP_Text campoDireccionTexto;
    // Start is called before the first frame update
    private string nombreOrgano;
    private Boolean cargado;
    private int cantImagenes;
    private int cantMaxImagenes;
    private int cantImagenesEje;
    public GameObject compresor;
    //InformacionClave

    private bool info;
    //
    public GameObject ubicacion;
    float factor;
    private void IniciarStart()
    {
        posImagenMatrizZAnterior = -1;
        cantGradosTotal = (int)(cantGradosTotal - (cantGradosTotal % cantGradosEntreImagen));
        factor = 450 / cantGradosTotal;

        cantImagenesEje = (int)(cantGradosTotal / cantGradosEntreImagen);
        cantMaxImagenes = cantImagenesEje * cantImagenesEje * 5;
        cantImagenes = 0;
        //path = Application.persistentDataPath;
        //path = Path.GetFullPath("./") + @"Organos\" +nombreOrgano;
        //Directory.CreateDirectory(path);
        
        
        camaraActiva = false;
        limSuperior = ((double)cantGradosTotal / 2);
        limInferior = (360 -((double)cantGradosTotal / 2));
        Debug.Log("lim superior :" + limSuperior);
        Debug.Log("lim inferior :" + limInferior);
        // la cantidad de grados entre imagen tiene que ser 1 o mayor
        matrizBoolScreenShots = new Boolean[cantImagenesEje, cantImagenesEje, 5];
        /*for (int i = 0; i < cantImagenesEje; i++)
        {
            for (int j = 0; j < cantImagenesEje; j++)
            {
                for (int k = 0; k < cantImagenesEje; k++)
                {
                    matrizBoolScreenShots[i, j, k] = false;
                }
            }
        }*/
        matrizBoolScreenShots.Initialize();
        imagenesAGuardar = new Stack<imagenAGuardar>();
    }
    /*
    private void Awake()
    {
        if (SharedInstance == null)
        {
            SharedInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (SharedInstance != this)
        {
            Destroy(gameObject);
        }
        //InputModel = new InputModel();
    }
    */
    // Update is called once per frame
    void Update()
    {
        if (cargado)
        {
            if (camaraActiva)
            {
                vectorsin = cubo.transform.localRotation.eulerAngles;
                auxY = vectorsin.x;
                auxX = 360-vectorsin.z;
                auxZ = vectorsin.y;
                double offsetX = (auxX > 180) ? -(auxX - 360) : -auxX;
                double offsetY = (auxY > 180) ? -(auxY - 360) : -auxY;
                float clampedX = Mathf.Clamp((float)offsetX * factor, -225f, 225f);
                float clampedY = Mathf.Clamp((float)offsetY * factor, -225f, 225f);
                ubicacion.transform.localPosition = new Vector3(clampedX, clampedY, 0);

                if (((auxX <= limSuperior) || (auxX >= limInferior)) && ((auxY <= limSuperior) || (auxY >= limInferior)))
                {
;                   if (matrizBoolScreenShots[posImagenMatrizX, posImagenMatrizY, posImagenMatrizZ])
                    {
                        grid.setPosColor(posImagenMatrizX, posImagenMatrizY, Color.green);
                    }
                    else grid.setPosColor(posImagenMatrizX, posImagenMatrizY, Color.red);
                    posImagenMatrizX = cantImagenesEje - 1 - Numero_Imagen(auxX);
                    posImagenMatrizY = cantImagenesEje - 1 - Numero_Imagen(auxY);
                    //grid.cambiarEstado(posImagenMatrizX, posImagenMatrizY);
                    grid.setPosColor(posImagenMatrizX, posImagenMatrizY, Color.blue);

                    for (int i = 0; i < 5; i++)
                    {
                        if (matrizBoolScreenShots[posImagenMatrizX, posImagenMatrizY, i])
                        {
                            gridZ.GetComponent<GridInst>().setPosColor(i, 0, Color.green);
                        }
                        else
                        {
                            gridZ.GetComponent<GridInst>().setPosColor(i, 0, Color.red);
                        }
                    }

                    if ((auxZ < 112.5) || (auxZ > 247.5))
                    {
                        posImagenMatrizZ = 4 - Numero_ImagenZ(auxZ);
                        if (posImagenMatrizZ != posImagenMatrizZAnterior)
                        {
                            for (int i = 0; i < cantImagenesEje; i++)
                            {
                                for (int j = 0; j < cantImagenesEje; j++)
                                {
                                    if (matrizBoolScreenShots[i, j, posImagenMatrizZ])
                                    {
                                        grid.GetComponent<GridInst>().setPosColor(i, j, Color.green);
                                    }
                                    else
                                    {
                                        grid.GetComponent<GridInst>().setPosColor(i, j, Color.red);
                                    }
                                }
                            }
                            posImagenMatrizZAnterior = posImagenMatrizZ;
                        }
                        //gridZ.cambiarEstado(posImagenMatrizZ, 0);
                        gridZ.setPosColor(posImagenMatrizZ, 0, Color.blue);
                        if (!matrizBoolScreenShots[posImagenMatrizX, posImagenMatrizY, posImagenMatrizZ])
                        {
                            sacarScreen(posImagenMatrizX, posImagenMatrizY, posImagenMatrizZ);
                            matrizBoolScreenShots[posImagenMatrizX, posImagenMatrizY, posImagenMatrizZ] = true;
                            cantImagenes++;
                        }
                    }
                    else
                    {
                        gridZ.fueraLimite();
                    }
                }
                else
                {
                    grid.fueraLimite();
                    if ((auxZ < 112.5) || (auxZ > 247.5))
                    {
                        posImagenMatrizZ = 4 - Numero_ImagenZ(auxZ);
                        //gridZ.cambiarEstado(posImagenMatrizZ, 0);
                        for (int i = 0; i < 5; i++)
                        {
                            gridZ.GetComponent<GridInst>().setPosColor(i, 0, Color.red);
                        }
                        gridZ.setPosColor(posImagenMatrizZ, 0, Color.blue);
                    }
                    else
                    {
                        gridZ.fueraLimite();
                    } 
                }
                if (cantImagenes == cantMaxImagenes)
                {
                    finalizar();
                }

            }
        }
    }

    /*public int Numero_ImagenZ(double X)
    {
        double aux = 0;

        if (X < 0)
        {
            X = 360 + X;
        }
        if (X > 360)
        {
            X = X - 360;
        }
        if (X >= 292.5)
        {
            aux = X - 292.5;
            aux = aux / 45;
        }
        else if (X >= 0)
        {
            aux = X + 67.5;
            aux = aux / 45;
        }
        return (int)aux;
    }*/

    public int Numero_ImagenZ(double X)
    {
        double aux = 0;

        X = (X + 360) % 360;
        if (X >= 247.5)
        {
            aux = X - 247.5;
        }
        else if (X >= 0)
        {
            aux = X + 112.5;
        }
        aux = aux / 45;
        return (int)aux;
    }

    public int Numero_Imagen(double X)
    {
        X = (X + 360) % 360;

        int numeroImagen;
        if (X >= limInferior)
        {
            numeroImagen = (int)((X - limInferior + 360) % 360 / cantGradosEntreImagen);
        }
        else
        {
            numeroImagen = (int)((X + limSuperior + 360) % 360 / cantGradosEntreImagen);
            if (numeroImagen == cantImagenesEje)
            {
                numeroImagen--;
            }
        }

        return numeroImagen;
    }
    /*public int Numero_Imagen(double X)
    {
        double aux = 0;

        if (X < 0)
        {
            X = 360 + X;
        }
        if (X > 360)
        {
            X = X - 360;
        }
        if (X >= limInferior)
        {
            aux = X - limInferior;
            aux = aux / cantGradosEntreImagen;
        }
        else if (X >= 0)
        {
            aux = X + limSuperior;
            aux = aux / cantGradosEntreImagen;
            if ((int)aux == cantImagenes)
            {
                aux--;
            }
        }
        return (int)aux;
    }*/

    public void finalizar()
    {
        while (imagenesAGuardar.Count > 0)
        {
            //pantalla de carga si es que es muy lento?
        }
        if (cantImagenes > 0)
        {
            camaraDevice.GetComponent<CamaraJugador>().cambiarEstadoMenuConexion(false);
            cargado = false;
            this.cargarAltoyAncho();
            //compresor.GetComponent<Compresor>().cargarDirOrigen(path, altoYAncho());
            this.gameObject.SetActive(false);

        }
    }

    private void sacarScreen(int posX, int posY, int posZ)
    {
        Texture2D aux = TextureToTexture2D(display.texture);
        Sprite itemBGSprite = Sprite.Create(aux, new Rect(0, 0, aux.width, aux.height), new Vector2(0, 0), 1);
        Texture2D itemBGTex = itemBGSprite.texture;
        byte[] itemBGBytes = itemBGTex.EncodeToPNG();
        imagenesAGuardar.Push(new imagenAGuardar(itemBGBytes, posX, posY, posZ));
        System.Threading.Thread thread = new System.Threading.Thread(guardarImagen);
        thread.Start();
    }

    private void sacarScreenClave(int posX, int posY, int posZ)
    {
        Texture2D aux = TextureToTexture2D(display.texture);
        Sprite itemBGSprite = Sprite.Create(aux, new Rect(0, 0, aux.width, aux.height), new Vector2(0, 0), 1);
        Texture2D itemBGTex = itemBGSprite.texture;
        byte[] itemBGBytes = itemBGTex.EncodeToPNG();
        imagenesAGuardar.Push(new imagenAGuardar(itemBGBytes, posX, posY, posZ));
        System.Threading.Thread thread = new System.Threading.Thread(guardarImagen2);
        thread.Start();
    }

    private void guardarImagen2()
    {
        if (imagenesAGuardar.Count > 0)
        {
            imagenAGuardar imagen = imagenesAGuardar.Pop();
            File.WriteAllBytes(path + "/Imagenes_Clave" + "/" + imagen.x.ToString() + "_" + imagen.y.ToString() + "_" + imagen.z.ToString() + ".png", imagen.imagen);
        }
    }
    private void guardarImagen()
    {
        if (imagenesAGuardar.Count > 0)
        {
            imagenAGuardar imagen = imagenesAGuardar.Pop();
            File.WriteAllBytes(path + "/Imagenes_Base" + "/" + imagen.x.ToString() + "_" + imagen.y.ToString() + "_" + imagen.z.ToString() + ".png", imagen.imagen);
        }
    }

    private Texture2D TextureToTexture2D(Texture texture)
    {
        Texture2D texture2D = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false);
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture renderTexture = RenderTexture.GetTemporary(texture.width, texture.height, 32);
        Graphics.Blit(texture, renderTexture);

        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();

        RenderTexture.active = currentRT;
        RenderTexture.ReleaseTemporary(renderTexture);
        return texture2D;
    }

    public void activarCamara()
    {
        camaraActiva = !camaraActiva;
    }


    public void cargarDatos(string nombre, int cantGradosTotal, double cantGradosEntreImagen, string path, bool info)
    {
        this.cantGradosTotal = cantGradosTotal;
        this.cantGradosEntreImagen = cantGradosEntreImagen;
        this.nombreOrgano = nombre;
        this.cargado = true;
        this.path = path;
        this.info = info;
 
        this.IniciarStart();
    }
    public void reiniciar()
    {
        imagenesAGuardar.Clear();
        cantImagenes = 0;
        int aux = (int)(cantGradosTotal / cantGradosEntreImagen);
        for (int i = 0; i < aux; i++)
        {
            for (int j = 0; j < aux; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    matrizBoolScreenShots[i, j, k] = false;
                }
            }
        }

    }

    public void salirRelev()
    {
        if (cargado)
        {
            cargado = false;
            if (Directory.Exists(path))
            {
                //Directory.Delete(path);
                DeleteDirectory(path);
            }
        }
    }

    private static void DeleteDirectory(string target_dir)
    {
        string[] files = Directory.GetFiles(target_dir);
        string[] dirs = Directory.GetDirectories(target_dir);

        foreach (string file in files)
        {
            File.SetAttributes(file, FileAttributes.Normal);
            File.Delete(file);
        }

        foreach (string dir in dirs)
        {
            DeleteDirectory(dir);
        }

        Directory.Delete(target_dir, false);
    }

    private void cargarAltoyAncho()
    {
        Texture2D aux = TextureToTexture2D(display.texture);
        compresor.GetComponent<Compresor>().cargarDirOrigen(path, aux.height, aux.width);

    }
  
    public void capturarNuevamente()
    {
        if (camaraActiva)
        {
            if (((auxX <= limSuperior) || (auxX >= limInferior)) && ((auxY <= limSuperior) || (auxY >= limInferior)))
            {
                sacarScreen(posImagenMatrizX, posImagenMatrizY, posImagenMatrizZ);
                if (info) 
                {
                    sacarScreenClave(posImagenMatrizX, posImagenMatrizY, posImagenMatrizZ);
                }

            }

        }
    }

    private void OnDestroy()
    {
        
    }
}
