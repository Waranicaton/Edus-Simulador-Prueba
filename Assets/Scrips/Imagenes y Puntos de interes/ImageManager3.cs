using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

public struct formatoImagenes
{
    public byte[] imagen;
}

public class ImageManager3 : MonoBehaviour
{

    public ManejadorPuntoInteres manejadorPuntoInteres;
    private bool updateActivo = true;
    private static bool fuera; //variable que indica si se está fuera de la matriz ya sea en los ejes x,y como z
    private static bool vacio; //variable que indica si se están pasando las imagenes cuando no hay ninguna imagen
    public Image imageObject;
    public Sprite among;
    private float auxX;
    private float auxY;
    private float auxZ;
    public GameObject cubo;
    private Vector3 vectorsin;
    private float limSuperior;
    private float limInferior;
    public GameObject cargar;
    public GameObject gridInstZ;
    public static DatosOrganos datosOrgano;
    public static DatosPunto datosPuntoActual;
    private string pathImagenes;
    private string pathImagenesClave;
    private int cantGrados;
    private double gradosEntreImagen;
    private int cantImagenes;
    // sensor de calor
    float factor;
    public GameObject circuloGrande;
    public GameObject circuloChiquito;
    public GameObject puntoDeInteresPrefab;

    public Material brilloZoom;
    public Material brilloZoomAnimacion;
    public Image imagenClaveSuperior;
    //private string[,,] grillaPuntosClave;
    public static InformacionClave infoClave;
    private Texture2D texturePuntoClave;

    private bool pausa;
    public GameObject RecuadroPantalla;

    private Texture2D texture;
    private Texture2D texture2;
    private Sprite[] imagenesVacio;
    private formatoImagenes[,,] imagenes;
    private Vector3[,,] posImagenes;

    //variables de la animacion
    private float porcentaje;
    public Image imageObject_Superior;
    private bool cambioImagen;

    //variables comprobacion de movimiento
    private float oldX;
    private float oldY;
    private float oldZ;

    public Image barraDeCarga;
    public GameObject pantallaCargando;

    //texto imagenes, el prefab y el objeto donde se crearan los prefab
    public GameObject prefabTexto;
    public GameObject prefabTextoIntermedia;
    public GameObject prefabUbicacion;
    public GameObject contenedorOpcionesAutoevaluacion;
    private bool textoCargado;

    public int dificultad;

    public GameObject selectorDificultad;
    //public static Dictionary<string, Dictionary<string, Dictionary<string, string>>> resultados;
    public static int imagen;

    public GameObject botonReinicioTexto;
    private string ultimaDir;
    private string[] opciones;

    public GameObject barraPunto;
    public GameObject barraAutoevaluacion;

    void Start()
    {
        //resultados = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
        textoCargado = false;
        pantallaCargando.SetActive(true);
        updateActivo = false;
        texture = new Texture2D(1, 1);
        texture2 = new Texture2D(1, 1);
        texturePuntoClave = new Texture2D(1, 1);
        fuera = false;
        vacio = true;
        pausa = false;
        datosOrgano = cargar.transform.GetComponent<CargarOrganos>().datosOrgano();
        datosPuntoActual = cargar.transform.GetComponent<CargarOrganos>().datosPunto();
        dificultad = cargar.transform.GetComponent<CargarOrganos>().dificultad;
        cantGrados = datosPuntoActual.cantGradosTotal - (int)(datosPuntoActual.cantGradosTotal % datosPuntoActual.cantGradosEntreImagen);
        if(dificultad != 2)
        {
            selectorDificultad.GetComponent<DificultadDropdown>().Iniciar(dificultad);
        }
        factor = 500 / cantGrados;
        gradosEntreImagen = datosPuntoActual.cantGradosEntreImagen;
#if UNITY_STANDALONE_OSX
        pathImagenes = Application.streamingAssetsPath + "/" + @"Organos/" + datosOrgano.nombre + "/" + datosPuntoActual.nombre + "/Imagenes_Base" + @"/";
        if (datosPuntoActual.info)
        {
            pathImagenesClave = Application.streamingAssetsPath + "/" + @"Organos/" + datosOrgano.nombre + "/" + datosPuntoActual.nombre + "/Imagenes_Clave" + @"/";
        }
#elif UNITY_STANDALONE_WIN
        pathImagenes = Application.persistentDataPath + "/" + @"Organos/" + datosOrgano.nombre + "/" + datosPuntoActual.nombre + "/Imagenes_Base" + @"/";
        if (datosPuntoActual.info)
        {
            pathImagenesClave = Application.persistentDataPath + "/" + @"Organos/" + datosOrgano.nombre + "/" + datosPuntoActual.nombre + "/Imagenes_Clave" + @"/";
        }
#endif
        imageObject.GetComponent<Image>().sprite = among;
        limSuperior = ((float)cantGrados / 2);
        limInferior = (360 - ((float)cantGrados / 2));
        cantImagenes = (int)(cantGrados / gradosEntreImagen);
        manejadorPuntoInteres.Recargar(cantGrados, gradosEntreImagen, cantImagenes, factor, limSuperior, limInferior);
        if (datosPuntoActual.info)
        {
            
            infoClave = cargar.transform.GetComponent<CargarOrganos>().InfoOrgano();
            manejadorPuntoInteres.CargarGrillaInformacion();
        }
        setearGrillaZ(0, 0);
        Cargar_Imagenes_Vacio();
        Thread hilo = new Thread(() => Cargar_Imagenes_hilo_2());
        hilo.Start();
        //Animacion
        cambioImagen = false;
        brilloZoom.SetFloat("_Brightness", 0);
        GameObject objeto = barraPunto.transform.Find("Titulo_Punto").gameObject;
        //sacarle la primera parte y que quede solo el numero de punto
        char punto = datosPuntoActual.nombre[datosPuntoActual.nombre.Length - 1];
        objeto.GetComponent<TextMeshProUGUI>().text = "ETAPA "+punto;
        SetBarraPunto();
        SetBarraAutoevaluacion();
    }
    public void recargarImageManager()
    {
        oldX = -1;
        updateActivo = false;
        pantallaCargando.SetActive(true);
        fuera = false;
        vacio = true;
        if (pausa)
            Pausa();
        datosOrgano = cargar.transform.GetComponent<CargarOrganos>().datosOrgano();
        datosPuntoActual = cargar.transform.GetComponent<CargarOrganos>().datosPunto();
        cantGrados = datosPuntoActual.cantGradosTotal - (int)(datosPuntoActual.cantGradosTotal % datosPuntoActual.cantGradosEntreImagen);
        factor = 500 / cantGrados;
        gradosEntreImagen = datosPuntoActual.cantGradosEntreImagen;
#if UNITY_STANDALONE_OSX
        pathImagenes = Application.streamingAssetsPath + "/" + @"Organos/" + datosOrgano.nombre + "/" + datosPuntoActual.nombre + "/Imagenes_Base" + @"/";
        if (datosPuntoActual.info)
        {
            pathImagenesClave = Application.streamingAssetsPath + "/" + @"Organos/" + datosOrgano.nombre + "/" + datosPuntoActual.nombre + "/Imagenes_Clave" + @"/";
        }
#elif UNITY_STANDALONE_WIN
        pathImagenes = Application.persistentDataPath + "/" + @"Organos/" + datosOrgano.nombre + "/" + datosPuntoActual.nombre + "/Imagenes_Base" + @"/";
        if (datosPuntoActual.info)
        {
            pathImagenesClave = Application.persistentDataPath + "/" + @"Organos/" + datosOrgano.nombre + "/" + datosPuntoActual.nombre + "/Imagenes_Clave" + @"/";
        }
#endif
        imageObject.GetComponent<Image>().sprite = among;
        limSuperior = ((float)cantGrados / 2);
        limInferior = (360 - ((float)cantGrados / 2));
        cantImagenes = (int)(cantGrados / gradosEntreImagen);
        manejadorPuntoInteres.Recargar(cantGrados, gradosEntreImagen, cantImagenes, factor, limSuperior, limInferior);
        if (datosPuntoActual.info)
        {
            infoClave = cargar.transform.GetComponent<CargarOrganos>().InfoOrgano();
            manejadorPuntoInteres.CargarGrillaInformacion();
        }
        manejadorPuntoInteres.SetearZonaDeCalor((int)oldZ);
        setearGrillaZ(0, 0);
        Cargar_Imagenes_Vacio();
        Thread hilo = new Thread(() => Cargar_Imagenes_hilo_2());
        hilo.Start();
        //Animacion
        cambioImagen = false;
        brilloZoom.SetFloat("_Brightness", 0);
        Cargar_Imagen_Vacio();
        GameObject objeto = barraPunto.transform.Find("Titulo_Punto").gameObject;
        //sacarle la primera parte y que quede solo el numero de punto
        char punto = datosPuntoActual.nombre[datosPuntoActual.nombre.Length - 1];
        objeto.GetComponent<TextMeshProUGUI>().text = "ETAPA " + punto;
        SetBarraPunto();
    }

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
            numeroImagen = (int)((X - limInferior + 360) % 360 / gradosEntreImagen);
        }
        else
        {
            numeroImagen = (int)((X + limSuperior + 360) % 360 / gradosEntreImagen);
            if (numeroImagen == cantImagenes)
            {
                numeroImagen--;
            }
        }

        return numeroImagen;
    }
    public void setUpdateActivo(bool updateActivo)
    {
        this.updateActivo = updateActivo;
    }
    void Update()
    {
        if (updateActivo)
        {
            pantallaCargando.SetActive(false);
            for (int i = 0; i < infoClave.cantPuntos; i++)
            {
                if (infoClave.posX[i] == (int)auxX && infoClave.posY[i] == (int)auxY && infoClave.posZ[i] == (int)auxZ)
                {
                    circuloChiquito.GetComponent<Image>().color = Color.green;
                    break;
                }
                else
                {
                    circuloChiquito.GetComponent<Image>().color = Color.white;
                }
            }
            if (!pausa)
            {
                if (RecuadroPantalla.GetComponent<Image>().color != Color.white) RecuadroPantalla.GetComponent<Image>().color = Color.white;
                vectorsin = cubo.transform.localRotation.eulerAngles;
                auxX = 360 - vectorsin.z;
                auxY = vectorsin.x;
                auxZ = vectorsin.y;
                float offsetX = (auxX > 180) ? -(auxX - 360) : -auxX;
                float offsetY = (auxY > 180) ? -(auxY - 360) : -auxY;
                circuloChiquito.transform.localPosition = new Vector3(offsetX * factor, offsetY * factor, 0);
                if (circuloChiquito.transform.localPosition.magnitude > 250)
                {
                    circuloChiquito.transform.localPosition = circuloChiquito.transform.localPosition.normalized * 250;
                }
                fuera = true;
                if (((auxX <= limSuperior) || (auxX >= limInferior)) && ((auxY <= limSuperior) || (auxY >= limInferior)))
                {
                    auxX = cantImagenes - 1 - Numero_Imagen(auxX);
                    auxY = cantImagenes - 1 - Numero_Imagen(auxY);
                    setearGrillaZ((int)auxX, (int)auxY);
                    if ((auxZ < 112.5) || (auxZ > 247.5))
                    {
                        fuera = false;
                        auxZ = 4 - Numero_ImagenZ(auxZ);
                        gridInstZ.GetComponent<GridInst>().cambiarEstado2((int)auxZ, 0);
                        if ((oldX != auxX) || (oldY != auxY) || (oldZ != auxZ))
                        {
                            cambioImagen = true;
                            if (oldZ != auxZ)
                            {
                                manejadorPuntoInteres.SetearZonaDeCalor((int)auxZ);
                            }
                            if (oldX != -1)
                            {
                                animacionTransicion2(0.5f);
                            }
                            oldX = auxX;
                            oldY = auxY;
                            oldZ = auxZ;
                            Cargar_Imagen2((int)auxX, (int)auxY, (int)auxZ);
                        }
                    }
                }
                else
                {
                    if ((auxZ < 112.5) || (auxZ > 247.5))
                    {
                        auxZ = 4 - Numero_ImagenZ(auxZ);
                        gridInstZ.GetComponent<GridInst>().cambiarEstado2((int)auxZ, 0);
                    }
                }
                if (fuera && !vacio)
                {
                    vacio = true;
                    Cargar_Imagen_Vacio();
                }
            }
            else
            {
                if (RecuadroPantalla.GetComponent<Image>().color != Color.red)
                    RecuadroPantalla.GetComponent<Image>().color = Color.red;
                if (auxX < cantImagenes && auxY < cantImagenes && auxZ < cantImagenes && !textoCargado)
                {
                    manejadorPuntoInteres.ComprobarYCargarPunto((int)auxX, (int)auxY, (int)auxZ);
                }
            }
        }
    }
    private void Cargar_Imagenes_Vacio()
    {
#if UNITY_STANDALONE_OSX
        string json = File.ReadAllText(Application.streamingAssetsPath + "/" + "Vacio/Datos.txt");
#elif UNITY_STANDALONE_WIN
        string json = File.ReadAllText(Application.persistentDataPath + "/" + "Vacio/Datos.txt");
#endif
        DatosVacio datosVacio = JsonConvert.DeserializeObject<DatosVacio>(json);
        imagenesVacio = new Sprite[datosVacio.cantImagenes];
        for (int i = 0; i < datosVacio.cantImagenes; i++)
        {
#if UNITY_STANDALONE_OSX
            byte[] pcxFile = File.ReadAllBytes(Application.streamingAssetsPath + "/" + @"Vacio/eco-" + i + ".png");
#elif UNITY_STANDALONE_WIN
            byte[] pcxFile = File.ReadAllBytes(Application.persistentDataPath + "/" + @"Vacio/eco-" + i + ".png");
#endif
            Texture2D loadTexture = new Texture2D(1, 1);
            loadTexture.LoadImage(pcxFile);
            imagenesVacio[i] = Sprite.Create(loadTexture, new Rect(0, 0, datosVacio.width, datosVacio.height), new Vector2(0, 0));
        }
    }
    private void Cargar_Imagenes_hilo_2()
    {
        imagenes = new formatoImagenes[cantImagenes, cantImagenes, 5];
        posImagenes = new Vector3[cantImagenes, cantImagenes, 5];
        for (int i = 0; i < cantImagenes; i++)
        {
            for (int j = 0; j < cantImagenes; j++)
            {
                for (int k = 0; k < 5; k++)
                {
                    if (File.Exists(pathImagenes + i + "_" + j + "_" + k + ".png"))
                    {
                        byte[] pcxFile = File.ReadAllBytes(pathImagenes + i + "_" + j + "_" + k + ".png");
                        try
                        {
                            imagenes[i, j, k].imagen = pcxFile;
                            posImagenes[i, j, k] = new Vector3(i, j, k);
                        }
                        catch (Exception ex)
                        {
                            Debug.LogError("error" + ex.Message);
                        }
                    }
                }
            }
        }
        for (int i = 0; i < cantImagenes; i++)
        {
            for (int j = 0; j < cantImagenes; j++)
            {
                for (int k = 0; k < 5; k++)
                {
                    if (imagenes[i, j, k].imagen == null)
                        ImagenCercana(new Vector3(i, j, k));
                }
            }
        }
        updateActivo = true;
    }

    private void ImagenCercana(Vector3 targetPoint)
    {
        float closestDistance = float.MaxValue;
        Vector3 infoSpacePosition;
        for (int i = 0; i < cantImagenes; i++)
        {
            for (int j = 0; j < cantImagenes; j++)
            {
                infoSpacePosition = new Vector3(i, j, targetPoint.z);
                if (imagenes[i, j, (int)targetPoint.z].imagen != null)
                {
                    float distance = Vector3.Distance(targetPoint, infoSpacePosition);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        posImagenes[(int)targetPoint.x, (int)targetPoint.y, (int)targetPoint.z] = new Vector3(i, j, targetPoint.z);
                    }
                }
            }
        }
    }

    private void Cargar_Imagen2(int X, int Y, int Z)
    {
        Vector3 vector = posImagenes[X, Y, Z];
        X = (int)vector.x;
        Y = (int)vector.y;
        Z = (int)vector.z;
        if (X < cantImagenes && Y < cantImagenes && Z < 5)
        {
            if (imagenes[X, Y, Z].imagen != null)
            {
                if (vacio)
                {
                    CancelInvoke();
                    vacio = false;
                }
                Texture2D.Destroy(texture);
                texture = new Texture2D(1, 1);
                texture.LoadImage(imagenes[X, Y, Z].imagen);
                imageObject.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, datosPuntoActual.width, datosPuntoActual.height), new Vector2(0.5f, 0.5f));
            }
        }
    }

    private void Cargar_Imagen_Vacio()
    {
        int X = UnityEngine.Random.Range(0, imagenesVacio.Length);
        imageObject.GetComponent<Image>().sprite = imagenesVacio[X];
        Invoke("Cargar_Imagen_Vacio", 0.3f);

    }

    IEnumerator Corrutina_Imagen_Clave(string dir)
    {
        yield return new WaitForSeconds(3f);
        if (File.Exists(pathImagenesClave + dir + "b.png"))
        {
            byte[] pcxFile2 = File.ReadAllBytes(pathImagenesClave + dir + "b.png");
            texturePuntoClave.LoadImage(pcxFile2);
            Sprite sp2 = Sprite.Create(texturePuntoClave, new Rect(0, 0, datosPuntoActual.width, datosPuntoActual.height), new Vector2(0, 0));
            Debug.Log(imagenClaveSuperior);
            imagenClaveSuperior.sprite = sp2;
        }
    }

    public void CargarImagenClave(string dir)
    {
        Cargar_Imagen_Clave(dir);
        AcomodarColor();
    }

    public void AcomodarColor() {
        Color color = imagenClaveSuperior.color;
        color.a = 1;
        imagenClaveSuperior.color = color;
    }

    private void setearGrillaZ(int ejeX, int ejeY)
    {
        for (int i = 0; i < 5; i++)
        {
            if (File.Exists(pathImagenes + ejeX + "_" + ejeY + "_" + i + ".png"))
            {

                gridInstZ.GetComponent<GridInst>().setPosColor(i, 0, Color.green);
            }
            else
            {
                gridInstZ.GetComponent<GridInst>().setPosColor(i, 0, Color.red);
            }
        }
    }

    private void animacionTransicion2(float duration)
    {
        Vector3 vector = posImagenes[(int)oldX, (int)oldY, (int)oldZ];
        oldX = (int)vector.x;
        oldY = (int)vector.y;
        oldZ = (int)vector.z;
        if (oldX < cantImagenes && oldY < cantImagenes && oldZ < 3)
        {
            if (imagenes[(int)oldX, (int)oldY, (int)oldZ].imagen != null)
            {
                if (vacio)
                {
                    CancelInvoke();
                    vacio = false;
                }
                Texture2D.Destroy(texture2);
                texture2 = new Texture2D(1, 1);
                texture2.LoadImage(imagenes[(int)oldX, (int)oldY, (int)oldZ].imagen);
                imageObject_Superior.GetComponent<Image>().sprite = Sprite.Create(texture2, new Rect(0, 0, datosPuntoActual.width, datosPuntoActual.height), new Vector2(0.5f, 0.5f));
                cambioImagen = false;
                StartCoroutine(Transicion(duration));
            }
        }
    }
    private IEnumerator Transicion(float duration)
    {
        float time = 0f;
        float porcentaje;
        Color color2 = imageObject.GetComponent<Image>().color;
        while (time < duration)
        {
            time += Time.deltaTime;
            porcentaje = time / duration;
            color2.a = 1f - porcentaje;
            imageObject_Superior.GetComponent<Image>().color = color2;
            brilloZoomAnimacion.SetFloat("_Alpha", color2.a);

            if (cambioImagen)
            {
                color2.a = 0f;
                imageObject_Superior.GetComponent<Image>().color = color2;
                brilloZoomAnimacion.SetFloat("_Alpha", color2.a);

                break;
            }
            yield return null;
        }
    }

    //No se
    public void Reset_R()
    {
        //Reinicia el image managera con la tecla R, corrige el zoom, el color
        //Deberia ir en otro lado, junto con el reset del inputfieldsimulacion
        Color aux = new Color(1f, 1f, 1f, 1f);
        imageObject.GetComponent<Image>().color = aux;
        imageObject_Superior.GetComponent<Image>().color = aux;
        imageObject.transform.localScale = new Vector3(1f, 1f, 0);
        imageObject_Superior.transform.localScale = new Vector3(1f, 1f, 0);
    }

    public void Pausa()
    {
        pausa = !pausa;
        botonReinicioTexto.SetActive(false);
        textoCargado = false;
        //limpiar prefabs de cuando es un punto clave 
        for (int i = 0; i < contenedorOpcionesAutoevaluacion.transform.childCount; i++)
        {
            if (contenedorOpcionesAutoevaluacion.transform.GetChild(i).name != "Pantalla" &&
                contenedorOpcionesAutoevaluacion.transform.GetChild(i).name != "Pantalla_Superior_ImagenClave" &&
                contenedorOpcionesAutoevaluacion.transform.GetChild(i).name != "Pantalla_Superior")
                Destroy(contenedorOpcionesAutoevaluacion.transform.GetChild(i).gameObject);
        }
        Color color = imagenClaveSuperior.color;
        color.a = 0;
        imagenClaveSuperior.color = color;
    }

    //tengo que revisar esta funcion, para mi que se puede optimizar o almenos modularizar
    private void Cargar_Imagen_Clave(string direccion)
    {
        ultimaDir = direccion;
        if (File.Exists(pathImagenesClave + direccion))
        {
            textoCargado = true;
            byte[] pcxFile2 = File.ReadAllBytes(pathImagenesClave + direccion);
            string dir = direccion.Substring(0, direccion.Length - 4); //le quito la extension (.png)
            Debug.Log(dir);
            if (dificultad == 0)
            {
                InteraccionTexto.ActivarScript(false);
                string json = File.ReadAllText(Path.GetFullPath(pathImagenesClave + dir + ".txt"));
                pcxFile2 = File.ReadAllBytes(pathImagenesClave + dir + "a.png");
                StartCoroutine(Corrutina_Imagen_Clave(dir));
            }
            else if (dificultad == 1)
            {
                InteracionTextoIntermedia.ActivarScript(true);
                string json = File.ReadAllText(Path.GetFullPath(pathImagenesClave + dir + ".txt"));
                InformacionTextos textos = JsonConvert.DeserializeObject<InformacionTextos>(json);
                for (int i = 0; i < textos.nombre.Length; i++)
                {
                    GameObject newObject = Instantiate(prefabTextoIntermedia, contenedorOpcionesAutoevaluacion.transform);
                    newObject.transform.GetComponent<TextMeshProUGUI>().text = textos.nombre[i];
                    newObject.transform.localPosition = new Vector2(textos.posX[i], textos.posY[i]);
                }
            }
            else if (dificultad == 2)
            {
                botonReinicioTexto.SetActive(true);
                GameObject objeto = barraPunto.transform.Find("Titulo_Punto").gameObject;
                objeto.GetComponent<TextMeshProUGUI>().text = datosPuntoActual.nombre;
                string[] partes = pathImagenesClave.Split('/');
                string dirOpciones = "";
                int j;
                for (j = 0; j < partes.Length - 3; j++)
                {
                    dirOpciones = dirOpciones + partes[j] + "/";
                }
                dirOpciones = dirOpciones + "opciones.txt";
                string json2 = File.ReadAllText(dirOpciones);
                partes = JsonConvert.DeserializeObject<string[]>(json2);
                InteraccionTexto.ActivarScript(false);//activar el drag
                string json = File.ReadAllText(Path.GetFullPath(pathImagenesClave + dir + ".txt"));
                InformacionTextos textos = JsonConvert.DeserializeObject<InformacionTextos>(json);
                int x = -805;
                int y = -750;
                int cantValores = textos.nombre.Length;
                SetBarraPunto();
                int[] indices = new int[12];
                for (int i = 0; i < 12; i++)
                {
                    indices[i] = i;
                }
                System.Random rand = new System.Random();
                for (int i = 12 - 1; i > 0; i--)
                {
                    j = rand.Next(0, i + 1);
                    int temp = indices[i];
                    indices[i] = indices[j];
                    indices[j] = temp;
                }
                bool textoPuesto = false;
                int aux = 0;
                GameObject[] newObject2 = new GameObject[cantValores];
                for (int i = 0; i < cantValores; i++)
                {
                    newObject2[i] = Instantiate(prefabUbicacion, contenedorOpcionesAutoevaluacion.transform);
                    newObject2[i].transform.localPosition = new Vector2(textos.posX[i], textos.posY[i]);
                    newObject2[i].GetComponent<UbicarTexto>().SetId(i);
                    j = 0;
                    foreach (string texto in partes)
                    {
                        if (QuitarTildes(textos.nombre[i].Replace(" ", string.Empty)) == QuitarTildes(texto.Replace(" ", string.Empty)))
                        {
                            Debug.Log("esigual" + cantValores);
                            partes[j] = "";
                            aux++;
                        }
                        j++;
                    }
                }
                foreach (string texto in textos.nombre)
                {
                    Debug.Log(QuitarTildes(System.Text.RegularExpressions.Regex.Replace(texto, @"\s+", " ")));
                }
                j = partes.Length - 1;
                for (int i = 0; i < partes.Length-aux; i++)
                {
                    if (partes[i] == "")
                    {
                        while (partes[j] == "")
                        {
                            j--;
                        }
                        partes[i] = partes[j];
                        j--;
                    }
                }
                for (int i = 0; i < 12; i++) //12 son la cantidad de opciones
                {
                    int currentIndex = indices[i];
                    GameObject newObject;
                    if (currentIndex >= cantValores)
                    {
                        newObject = Instantiate(prefabTexto, contenedorOpcionesAutoevaluacion.transform);
                        if ((aux % 2) == 1)
                            newObject.transform.Find("TextoParaCompletarAutoevaluacion").GetComponent<TextMeshProUGUI>().text = partes[currentIndex];
                        else
                            newObject.transform.Find("TextoParaCompletarAutoevaluacion").GetComponent<TextMeshProUGUI>().text = partes[partes.Length - aux - currentIndex];
                    }
                    else
                    {
                        newObject = Instantiate(prefabTexto, contenedorOpcionesAutoevaluacion.transform);
                        newObject.transform.Find("TextoParaCompletarAutoevaluacion").GetComponent<TextMeshProUGUI>().text = textos.nombre[currentIndex];
                    }
                    newObject.transform.localPosition = new Vector2(x, y);
                    newObject.transform.Find("TextoParaCompletarAutoevaluacion").GetComponent<TextMeshProUGUI>().text = newObject.transform.Find("TextoParaCompletarAutoevaluacion").GetComponent<TextMeshProUGUI>().text.Trim();
                    newObject.transform.Find("TextoParaCompletarAutoevaluacion").GetComponent<TextMeshProUGUI>().text = System.Text.RegularExpressions.Regex.Replace(newObject.transform.Find("TextoParaCompletarAutoevaluacion").GetComponent<TextMeshProUGUI>().text, @"\s+", " ");
                    newObject.transform.Find("TextoParaCompletarAutoevaluacion").GetComponent<TextMeshProUGUI>().text = newObject.transform.Find("TextoParaCompletarAutoevaluacion").GetComponent<TextMeshProUGUI>().text.Substring(0, newObject.transform.Find("TextoParaCompletarAutoevaluacion").GetComponent<TextMeshProUGUI>().text.Length - 1);
                    for (j = 0; j < cantValores; j++)
                    {
                        if (ManejadorPuntoInteres.resultados.ContainsKey(datosPuntoActual.nombre) && ManejadorPuntoInteres.resultados[datosPuntoActual.nombre].ContainsKey("Imagen" + imagen))
                        {
                            if (ManejadorPuntoInteres.resultados[datosPuntoActual.nombre]["Imagen" + imagen].ContainsKey("Texto" + j) &&
                                ManejadorPuntoInteres.resultados[datosPuntoActual.nombre]["Imagen" + imagen]["Texto" + j] == QuitarTildes(newObject.transform.Find("TextoParaCompletarAutoevaluacion").GetComponent<TextMeshProUGUI>().text))
                            {
                                    newObject.transform.GetComponent<InteraccionTexto>().ubicado = true;
                                    newObject.transform.localPosition = new Vector2(textos.posX[j], textos.posY[j]);
                                    //newObject.transform.localPosition = new Vector2(x, y);
                                    textoPuesto = true;
                                    Destroy(newObject2[j]);
                                    break;
                            }
                        }
                        else break;
                    }
                    if (textoPuesto)
                    {
                        textoPuesto = false;
                        continue;
                    }
                    x += 544;
                    if (x > 1100)
                    {
                        x = -805;
                        y -= 113;
                        
                    }
                }
            }
            //////////////////////////////////////////////////////////////////////
            texturePuntoClave.LoadImage(pcxFile2);
            Sprite sp2 = Sprite.Create(texturePuntoClave, new Rect(0, 0, datosPuntoActual.width, datosPuntoActual.height), new Vector2(0, 0));
            imagenClaveSuperior.sprite = sp2;
        }
    }

    // Creo que las siguientes funciones podrian estar en uno o varios scripts distintos de este
    public void SetBarraPunto()
    {
        int cantDatosPunto = 0;
        int cantDatosImagen = 0;
        int auxPos = 0;
        int cantTextoUbicado = 0;
        
        foreach (string contenido in manejadorPuntoInteres.grillaPuntosClave)
        {
            if (contenido != null)
            {
                cantDatosImagen = JsonConvert.DeserializeObject<InformacionTextos>(File.ReadAllText(Path.GetFullPath(pathImagenesClave + contenido.Substring(0, contenido.Length - 4) + ".txt"))).nombre.Length;
                cantDatosPunto += cantDatosImagen;
                if (ManejadorPuntoInteres.resultados.ContainsKey(datosPuntoActual.nombre) && ManejadorPuntoInteres.resultados[datosPuntoActual.nombre].ContainsKey("Imagen" + auxPos))
                {
                    for (int i = 0; i < cantDatosImagen; i++)
                    {
                        if (ManejadorPuntoInteres.resultados[datosPuntoActual.nombre]["Imagen" + auxPos].ContainsKey("Texto" + i))
                        {
                            cantTextoUbicado++;
                        }
                    }
                }
                auxPos++;
            }
        }
        if (cantDatosPunto == 0)
            barraPunto.GetComponent<BarrasProgreso>().ValorMaximo(1, 1);
        else
            barraPunto.GetComponent<BarrasProgreso>().ValorMaximo(cantDatosPunto, cantTextoUbicado);
    }

    public void SetBarraAutoevaluacion()
    {
        InformacionClave infoAux;
        string[,,] grillaPuntosClaveAux;
        int cantDatosPunto = 0;
        int cantDatosImagen = 0;
        string dir;
        for (int i = 0; i < infoClave.cantPuntos; i++)
        {
            manejadorPuntoInteres.grillaPuntosClave[infoClave.posX[i], infoClave.posY[i], infoClave.posZ[i]] = infoClave.imagen[i];
        }
        for (int i = 1; i <= datosOrgano.puntos; i++)
        {
    #if UNITY_STANDALONE_OSX
                dir = Application.streamingAssetsPath + "/Organos/" + datosOrgano.nombre + "/Punto" + i + "/Imagenes_Clave/";
    #elif UNITY_STANDALONE_WIN
                dir = Application.persistentDataPath + "/Organos/" + datosOrgano.nombre + "/Punto" + i + "/Imagenes_Clave/";
    #endif
            if (File.Exists(dir + "/Informacion.txt"))
            {
                infoAux = JsonConvert.DeserializeObject<InformacionClave>(File.ReadAllText(dir + "/Informacion.txt"));
                grillaPuntosClaveAux = new string[cantImagenes, cantImagenes, 4];
                for (int j = 0; j < infoAux.cantPuntos; j++)
                {
                    grillaPuntosClaveAux[infoAux.posX[j], infoAux.posY[j], infoAux.posZ[j]] = infoAux.imagen[j];
                }
                foreach (string contenido in grillaPuntosClaveAux)
                {
                    if (contenido != null)
                    {
                        cantDatosImagen = JsonConvert.DeserializeObject<InformacionTextos>(File.ReadAllText(Path.GetFullPath(dir + contenido.Substring(0, contenido.Length - 4) + ".txt"))).nombre.Length;
                        cantDatosPunto += cantDatosImagen;
                    }
                }
            }
        }
        if (cantDatosPunto == 0)
        {
            barraAutoevaluacion.GetComponent<BarrasProgreso>().ValorMaximo(1, 1);
        }
        else
        {
            barraAutoevaluacion.GetComponent<BarrasProgreso>().ValorMaximo(cantDatosPunto, 0);
        }

    }

    string QuitarTildes(string input)
    {
        string normalizedString = input.Normalize(NormalizationForm.FormD);
        StringBuilder stringBuilder = new StringBuilder();

        foreach (char c in normalizedString)
        {
            UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }

    public bool getPausa()
    {
        return pausa;
    }

    public void CambiarDificultad(int dificultad)
    {
        this.dificultad = dificultad;
    }

}