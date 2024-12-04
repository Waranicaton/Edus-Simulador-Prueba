using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using Newtonsoft.Json;
using UnityEngine.UI;

public class CargarDatos : MonoBehaviour
{
    public GameObject nombre;
    public GameObject cantGradosT;
    public GameObject cantGradosEntreI;
    public GameObject organoD;
    public TMP_Text campoDireccionTexto;
    public TMP_Text campoError;
    private string nombreOrgano;
    private int cantGradosTotal;
    private double cantGradosEntreImagen;
    private string organoDescripcion;
    private int cantImagenes;
    private string directorio;
    private BuscadorDirectorio buscadorDirectorio;

    public GameObject menuDatos;

    public GameObject screenManager;

    public GameObject ubicacion;
    public GameObject gridInst;
    public GameObject gridInstZ;

    public GameObject compresor;

    public GameObject camarJugador;

    public GameObject cantPuntos;

    public GameObject menuSelecPunto;
    //Datos De Organo Y punto
    //Datos organo
    private DatosOrganos datosOrganos;
    private string dirOrgano;
    public GameObject descripcionOrganoElegido;
    public GameObject dropdownOrganos;
    public GameObject cantPuntosOrgano;

    //
    //Datos Punto
    private DatosPunto datosPunto;
    public GameObject dropdownPuntos;
    private string puntoAcutal = "";
    public GameObject descripcionPunto;
    public Toggle infoClaveBool;
    public TMP_Text errorPunto;
    public GameObject menuDatosOrgano;
    //
    //
    public void Start()
    {
        directorio = "";
        buscadorDirectorio = new BuscadorDirectorio();
    }
    public void asignarOrgano()
    {
        nombreOrgano = nombre.GetComponent<TMP_InputField>().text;
        
        organoDescripcion = organoD.GetComponent<TMP_InputField>().text;
        int cantPuntoselegidos = 0;
        int.TryParse(cantPuntos.GetComponent<TMP_InputField>().text, out cantPuntoselegidos);
        if (nombreOrgano == "")
        {
            campoError.text = "El campo 'nombre' no debe estar vacio";
        }
        else if (cantPuntoselegidos < 1) 
        {
            campoError.text = "La cantidad de puntos elegidos es incorrecta";
        }  
        else if (Directory.Exists(Path.GetFullPath("./") + "/Organos/" + nombreOrgano))
        {
            campoError.text = "Ya existe un Organo Con ese Nombre";
        }else
        {
            
            campoError.text = "";
            
            

           
            Debug.Log(directorio);
            datosOrganos = new DatosOrganos(nombreOrgano, organoDescripcion, cantPuntoselegidos);
            //compresor.GetComponent<Compresor>().cargarDatos(directorio, datosOrganos);

            //Crea la carpeta Del Nuevo Organo y sus carpetas de puntos
            //
            string dir = Path.GetFullPath("./") + "/Organos/" + nombreOrgano;
            Directory.CreateDirectory(dir);
            for (int i = 1; i <= cantPuntoselegidos; i++)
            {
                Directory.CreateDirectory(dir + "/Punto" +i);
            }
            string json = JsonConvert.SerializeObject(datosOrganos);
            
            File.WriteAllText(dir + @"/Informacion.txt", json);
            dirOrgano = dir;

            menuSelecPunto.SetActive(true);
            menuDatosOrgano.SetActive(false);
            this.cargarOpcionesPunto();
            
            
        }
        Debug.Log(directorio);
       
    }


    public void AsignarPunto()
    {
        var aux = cantGradosT.GetComponent<TMP_InputField>().text;
        int.TryParse(aux, out cantGradosTotal);
        //cantGradosTotal = aux;
        aux = cantGradosEntreI.GetComponent<TMP_InputField>().text;
        double.TryParse(aux, out cantGradosEntreImagen);

        if (cantGradosTotal > 180 || cantGradosTotal < 5)
        {
            errorPunto.text = "El campo 'Cantidad Grados' debe contenener un número entre 5 y 180";
        }
        else if (cantGradosEntreImagen < 0.5)
        {
            errorPunto.text = "El campo 'Grados Entre Imagen' debe contenener un número mayor o igual a 0.5";
        }
        else
        {
            
            string[] files = Directory.GetFiles(dirOrgano + "/" + puntoAcutal);
            string[] directories = Directory.GetDirectories(dirOrgano + "/" + puntoAcutal);

            // Borra todos los archivos dentro de la carpeta
            foreach (string file in files)
            {
                File.Delete(file);
            }

            // Borra todos los subdirectorios y su contenido
            foreach (string directory in directories)
            {
                Directory.Delete(directory, true);
            }
            Directory.CreateDirectory(dirOrgano + "/" + puntoAcutal + "/Imagenes_Base");
            string dir1 = Path.GetFullPath(dirOrgano + "/" + puntoAcutal);
            Directory.CreateDirectory(dirOrgano + "/" + puntoAcutal + "/Imagenes_Clave");
            datosPunto = new DatosPunto(puntoAcutal, cantGradosTotal, cantGradosEntreImagen, descripcionPunto.GetComponent<TMP_InputField>().text, 0, 0, infoClaveBool.isOn);

            camarJugador.GetComponent<CamaraJugador>().cambiarEstadoMenuConexion(true);
            errorPunto.text = "";
            gridInst.GetComponent<GridInst>().cargarDatos(cantGradosTotal, cantGradosEntreImagen, 0);
            screenManager.SetActive(true);
            screenManager.GetComponent<ScreenManager>().cargarDatos(nombreOrgano, cantGradosTotal, cantGradosEntreImagen, dir1, datosPunto.info);
            menuSelecPunto.SetActive(false);
            ubicacion.transform.SetSiblingIndex(gridInst.transform.childCount - 1);
            gridInstZ.GetComponent<GridInst>().cargarDatos(cantGradosTotal, cantGradosEntreImagen, 1);
            compresor.GetComponent<Compresor>().cargarDatos(datosPunto);


        }
    }


    public void cargarOpcionesPunto()
    {
        string[] directoriosPuntos = Directory.GetDirectories(Path.GetFullPath("./") + "Organos/" + datosOrganos.nombre);
        Debug.Log(datosOrganos.nombre);
        for (int i = 0; i < directoriosPuntos.Length; i++)
        {
            directoriosPuntos[i] = Path.GetFileName(directoriosPuntos[i]);
        }

        TMP_Dropdown dropdown = dropdownPuntos.GetComponent<TMP_Dropdown>();

        dropdown.ClearOptions();

        List<string> listaOrganos = new List<string>();
        listaOrganos.AddRange(directoriosPuntos);

        dropdown.AddOptions(listaOrganos);
        if (listaOrganos.Count != 0)
        {
            DropdownIteemSelectedPunto(dropdown);
        }


    }

    public void cargarOpcionesOrgano()
    {
        //string[] directoriosOrganos = Directory.GetDirectories(Path.GetFullPath("./") + "Organos");
#if UNITY_STANDALONE_OSX
        string[] directoriosOrganos  = Directory.GetDirectories(Application.streamingAssetsPath + "/" + "Organos");
#elif UNITY_STANDALONE_WIN
        string[] directoriosOrganos = Directory.GetDirectories(Application.persistentDataPath + "/" + "Organos");
#endif
        for (int i = 0; i < directoriosOrganos.Length; i++)
        {
            directoriosOrganos[i] = Path.GetFileName(directoriosOrganos[i]);
        }

        TMP_Dropdown dropdown = dropdownOrganos.GetComponent<TMP_Dropdown>();

        dropdown.ClearOptions();

        List<string> listaOrganos = new List<string>();
        listaOrganos.AddRange(directoriosOrganos);

        dropdown.AddOptions(listaOrganos);
        if (listaOrganos.Count != 0)
        {
            DropdownIteemSelectedOrgano(dropdown);
        }


    }


    public void DropdownIteemSelectedPunto(TMP_Dropdown dropdown)
    {
        Debug.Log(dropdown.options[dropdown.value].text);
        string nombre = dropdown.options[dropdown.value].text;
        cantGradosT.GetComponent<TMP_InputField>().text = "";
        cantGradosEntreI.GetComponent<TMP_InputField>().text = "";
        descripcionPunto.GetComponent<TMP_InputField>().text = "";
        //string json = File.ReadAllText(Path.GetFullPath("./" + "Organos/" + nombre + "/Informacion.txt"));
        //datosOrganos = JsonConvert.DeserializeObject<DatosOrganos>(json);
        //cantGradosTotal.text = datosOrganosImportado.cantGradosTotal.ToString();
        //cantGradosImagen.GetComponent<TMP_Text>().text = datosOrganosImportado.cantGradosEntreImagen.ToString();
        //descripcionOrganoElegido.GetComponent<TMP_Text>().text = datosOrganos.descripcion;
        puntoAcutal = nombre;
    }

    public void DropdownIteemSelectedOrgano(TMP_Dropdown dropdown)
    {
        Debug.Log(dropdown.options[dropdown.value].text);
        string nombre = dropdown.options[dropdown.value].text;
        //string json = File.ReadAllText(Application.persistentDataPath + "/" + "Organos/" + nombre + "/Informacion.txt");
#if UNITY_STANDALONE_OSX
        string json = File.ReadAllText(Application.streamingAssetsPath + "/" + "Organos/" + nombre + "/Informacion.txt");
#elif UNITY_STANDALONE_WIN
        string json = File.ReadAllText(Application.persistentDataPath + "/" + "Organos/" + nombre + "/Informacion.txt");
#endif
        datosOrganos = JsonConvert.DeserializeObject<DatosOrganos>(json);
        cantPuntosOrgano.GetComponent<TMP_InputField>().text = datosOrganos.puntos.ToString();
        descripcionOrganoElegido.GetComponent<TMP_InputField>().text = datosOrganos.descripcion;
        dirOrgano = Path.GetFullPath("/" + "Organos/" + nombre );
        //datosOrganosImportado = JsonConvert.DeserializeObject<DatosOrganos>(json);
        //cantGradosTotal.text = datosOrganosImportado.cantGradosTotal.ToString();
        //cantGradosImagen.GetComponent<TMP_Text>().text = datosOrganosImportado.cantGradosEntreImagen.ToString();
        //descripcion.GetComponent<TMP_Text>().text = datosOrganosImportado.descripcion;
    }

    /*
    public void setPath()
    {

        directorio = buscadorDirectorio.ReadPath();
        //campoDireccionTexto.text = BuscadorDirectorio.readExePath();
        campoDireccionTexto.text = directorio;
    }
    */
    public void guardarInfoPunto(int width, int height)
    {
        string dir = Path.GetFullPath("/") + "/Organos" + datosOrganos.nombre + "/" + datosPunto.nombre ;
        datosPunto.width = width;
        datosPunto.height = height;
        string json = JsonConvert.SerializeObject(datosPunto);

        File.WriteAllText(dir + @"/Datos.txt", json);

    }
    /*
    public string[,,,] inicializarMatriz()
    {
        int cantImagenesAux = (int)(cantGradosTotal / cantGradosEntreImagen);
        string[,,,] datos = new string[cantImagenesAux, cantImagenesAux, cantImagenesAux, 2];
        for (int i = 0; i < cantImagenesAux; i++)
        {
            for (int j = 0; j < cantImagenesAux; j++)
            {
                for (int k = 0; k < cantImagenesAux; k++)
                {

                    datos[i, j, k, 0] = "";
                    datos[i, j, k, 1] = "";

                }
            }
        }
        return datos;

    }
    */
}