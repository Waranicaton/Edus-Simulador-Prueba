using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.IO.Compression;
using UnityEngine.UI;
using TMPro;
using System;
using System.Reflection;

public class CargarOrganos : MonoBehaviour
{
    private OrganosCargados organosCargados;
    private BuscadorDirectorio buscadorDirectorio;
    private string organosCargadosPath;

    private static DatosOrganos datosOrganosImportado;
    private static DatosPunto datosPuntoImportado;
    private DatosInterfazCuerpo datosInterfaz;

    [SerializeField] GameObject prefabPuntos;

    public GameObject dropdownObject;
    public TMP_Text cantGradosTotal;
    public GameObject cantGradosImagen;
    public GameObject descripcion;
    public static GameObject datosOrganoCargado;
    public GameObject gridInst;
    public GameObject gridInstZ;
    public GameObject interfazCuerpo;

    private int puntoActual = 1;
    public int dificultad = 1;

    private Texture2D texture;

    public GameObject menuManager;

    void Start()
    {

        buscadorDirectorio = new BuscadorDirectorio();
        organosCargados = new OrganosCargados();
        cargarCarpetas();
        //cargarOpciones();
    }

    /*
    public void importarOrgano()
    {
        var directorio = buscadorDirectorio.ReadZipPath();
        if (directorio != "")
        {
            var dirTemporal = Path.GetFullPath("./") + "Organos/temporal";
            Directory.CreateDirectory(dirTemporal);
            ZipFile.ExtractToDirectory(directorio, dirTemporal);
            string json = File.ReadAllText(dirTemporal + "/Datos.txt");
            datosOrganosImportado = JsonConvert.DeserializeObject<DatosOrganos>(json);
            Directory.Move(dirTemporal, Path.GetFullPath("./") + "Organos/" + datosOrganosImportado.nombre);
            cargarCarpetas();
            cargarOpciones();
        }
    }
    */
    /*
    private void ajustarJson()
    {
        File.Delete(organosCargadosPath);
        string json = JsonConvert.SerializeObject(organosCargados);
        File.WriteAllText(organosCargadosPath, json);
    }
    */

    /*private void cargarOpciones()
    {
        TMP_Dropdown dropdown = dropdownObject.GetComponent<TMP_Dropdown>();

        dropdown.ClearOptions();

        List<string> listaOrganos = new List<string>();
        listaOrganos.AddRange(organosCargados.organos);

        dropdown.AddOptions(listaOrganos);
        if (listaOrganos.Count != 0)
        {
            DropdownIteemSelected(dropdown);
        }
    }

    public void DropdownIteemSelected(TMP_Dropdown dropdown)
    {
        //Debug.Log(dropdown.options[dropdown.value].text);
        string nombre = dropdown.options[dropdown.value].text;
#if UNITY_STANDALONE_OSX
        string json = File.ReadAllText(Application.streamingAssetsPath + "/" + "Organos/" + nombre + "/Informacion.txt");
#elif UNITY_STANDALONE_WIN
        string json = File.ReadAllText( Application.persistentDataPath + "/" + "Organos/" + nombre + "/Informacion.txt");
#endif

        datosOrganosImportado = JsonConvert.DeserializeObject<DatosOrganos>(json);
        //cantGradosTotal.text = datosOrganosImportado.cantGradosTotal.ToString();
        //cantGradosImagen.GetComponent<TMP_Text>().text = datosOrganosImportado.cantGradosEntreImagen.ToString();
        descripcion.GetComponent<TMP_Text>().text = datosOrganosImportado.descripcion;
    }*/

    private void cargarCarpetas()
    {
#if UNITY_STANDALONE_OSX
        string[] directorios = Directory.GetDirectories(Application.streamingAssetsPath + "/" + "Organos/");
#elif UNITY_STANDALONE_WIN
        string[] directorios = Directory.GetDirectories(Application.persistentDataPath + "/" + "Organos/");
#endif

        //string[] directorios = Directory.GetDirectories(Path.GetFullPath("./") + "Organos");
        for (int i = 0; i < directorios.Length; i++)
        {
            directorios[i] = Path.GetFileName(directorios[i]);
        }
        organosCargados.organos = directorios;
    }

    public DatosOrganos datosOrgano()
    {

        //datosOrganoCargado.GetComponent<TMP_Text>().text = "Organo:"+datosOrganosImportado.nombre+"\nDescripcion:"+datosOrganosImportado.descripcion;
        //Debug.Log(datosOrganosImportado.nombre);
        Debug.Log(datosOrganosImportado.nombre);
        return datosOrganosImportado;
    }

    public DatosPunto datosPunto()
    {
#if UNITY_STANDALONE_OSX
        string json = File.ReadAllText(Application.streamingAssetsPath + "/Organos/" + datosOrganosImportado.nombre + "/Punto" + puntoActual + "/Datos.txt");
#elif UNITY_STANDALONE_WIN
        string json = File.ReadAllText(Application.persistentDataPath + "/Organos/" + datosOrganosImportado.nombre + "/Punto" + puntoActual + "/Datos.txt");
#endif

        //string json = File.ReadAllText(Path.GetFullPath("./Organos/" + datosOrganosImportado.nombre + "/Punto" + puntoActual + "/Datos.txt"));
        datosPuntoImportado = JsonConvert.DeserializeObject<DatosPunto>(json);
        return datosPuntoImportado;
    }

    public void AsignarPuntoActual(int punto)
    {
        puntoActual = punto;
#if UNITY_STANDALONE_OSX
        string json = File.ReadAllText(Application.streamingAssetsPath + "/Organos/" + datosOrganosImportado.nombre + "/Punto" + puntoActual + "/Datos.txt");
#elif UNITY_STANDALONE_WIN
        string json = File.ReadAllText(Application.persistentDataPath + "/Organos/" + datosOrganosImportado.nombre + "/Punto" + puntoActual + "/Datos.txt");
#endif
        //string json = File.ReadAllText(Path.GetFullPath("./Organos/" + datosOrganosImportado.nombre + "/Punto" + puntoActual + "/Datos.txt"));
        datosPuntoImportado = JsonConvert.DeserializeObject<DatosPunto>(json);
    }

    public void cargarGrilla()
    {
        //gridInst.GetComponent<GridInst>().cargarDatos(datosOrganosImportado.cantGradosTotal, datosOrganosImportado.cantGradosEntreImagen, 0);
        gridInstZ.GetComponent<GridInst>().cargarDatos(0, 0, 1);
    }

    public void CargarOrgano(string organo)
    {
#if UNITY_STANDALONE_OSX
        string json = File.ReadAllText(Application.streamingAssetsPath + "/" + "Organos/" + organo + "/Informacion.txt");
        string json2 = File.ReadAllText(Application.streamingAssetsPath + "/" + "Organos/" + organo + "/InterfazCuerpo/Informacion.txt");
#elif UNITY_STANDALONE_WIN
        string json = File.ReadAllText(Application.persistentDataPath + "/" + "Organos/" + organo + "/Informacion.txt");
        string json2 = File.ReadAllText(Application.persistentDataPath + "/" + "Organos/" + organo + "/InterfazCuerpo/Informacion.txt");
#endif
        datosOrganosImportado = JsonConvert.DeserializeObject<DatosOrganos>(json);
        datosInterfaz = JsonConvert.DeserializeObject<DatosInterfazCuerpo>(json2);
        //creo prefab higado

        Texture2D.Destroy(texture);
        texture = new Texture2D(1, 1);
        texture.LoadImage(File.ReadAllBytes(Application.persistentDataPath + "/" + "Organos/" + organo + "/InterfazCuerpo/xRay.png"));
        interfazCuerpo.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

        for (int i = 0; i < datosInterfaz.posX.Length; i++)
        {
            GameObject instantiatedButton = Instantiate(prefabPuntos, interfazCuerpo.transform);
            instantiatedButton.name = "PuntoClave-" + (i+1);
            //instantiatedButton.transform.localPosition = new Vector3(0,-1190,0);
            instantiatedButton.transform.localPosition = new Vector3(datosInterfaz.posX[i], datosInterfaz.posY[i] - 1190, 0f);
            //instantiatedButton.transform.position = new Vector3(datosInterfaz.posX[i], datosInterfaz.posY[i], 0f);
            Debug.Log(datosInterfaz.posY[i]);
            Button button = instantiatedButton.GetComponent<Button>();

            button.onClick.RemoveAllListeners();
            int valorActual = i+1;
            button.onClick.AddListener(() => menuManager.GetComponent<BotonesCargar>().CargarOrgano(valorActual));
        }
        //cantGradosTotal.text = datosOrganosImportado.cantGradosTotal.ToString();
        //cantGradosImagen.GetComponent<TMP_Text>().text = datosOrganosImportado.cantGradosEntreImagen.ToString();
        descripcion.GetComponent<TMP_Text>().text = datosOrganosImportado.descripcion;
    }

    /*public void CargarHigado()
    {
#if UNITY_STANDALONE_OSX
        string json = File.ReadAllText(Application.streamingAssetsPath + "/" + "Organos/Higado/Informacion.txt");
#elif UNITY_STANDALONE_WIN
        string json = File.ReadAllText(Application.persistentDataPath + "/" + "Organos/Higado/Informacion.txt");
#endif

        //string json = File.ReadAllText(Path.GetFullPath("./" + "Organos/Higado/Informacion.txt"));
        datosOrganosImportado = JsonConvert.DeserializeObject<DatosOrganos>(json);
        //cantGradosTotal.text = datosOrganosImportado.cantGradosTotal.ToString();
        //cantGradosImagen.GetComponent<TMP_Text>().text = datosOrganosImportado.cantGradosEntreImagen.ToString();
        descripcion.GetComponent<TMP_Text>().text = datosOrganosImportado.descripcion;
    }*/

    /*public void CargarOrganoDeParametro(string organo)
    {
#if UNITY_STANDALONE_OSX
        string json = File.ReadAllText(Application.streamingAssetsPath + "/" + "Organos/" + organo + "/Informacion.txt");
#elif UNITY_STANDALONE_WIN
        string json = File.ReadAllText(Application.persistentDataPath + "/" + "Organos/" + organo + "/Informacion.txt");
#endif

        //string json = File.ReadAllText(Path.GetFullPath("./" + "Organos/"+organo+"/Informacion.txt"));
        datosOrganosImportado = JsonConvert.DeserializeObject<DatosOrganos>(json);
        descripcion.GetComponent<TMP_Text>().text = datosOrganosImportado.descripcion;
    }*/

    /*
    public void cargarOrganoElegido(string nombre)
    {
        string json = File.ReadAllText(Path.GetFullPath("./" + "Organos/" + nombre + "/Datos.txt"));
        datosOrganosImportado = JsonConvert.DeserializeObject<DatosOrganos>(json);
    }
    */
    public InformacionClave InfoOrgano()
    {
#if UNITY_STANDALONE_OSX
        string json = File.ReadAllText(Application.streamingAssetsPath + "/" + "Organos/" + datosOrganosImportado.nombre + "/" + datosPuntoImportado.nombre + "/Imagenes_Clave/Informacion.txt");
#elif UNITY_STANDALONE_WIN
        string json = File.ReadAllText(Application.persistentDataPath + "/" + "Organos/" + datosOrganosImportado.nombre + "/" + datosPuntoImportado.nombre + "/Imagenes_Clave/Informacion.txt");
#endif

        //string json = File.ReadAllText(Path.GetFullPath("./" + "Organos/" + datosOrganosImportado.nombre + "/"+datosPuntoImportado.nombre + "/Imagenes_Clave/Informacion.txt"));
        return JsonConvert.DeserializeObject<InformacionClave>(json);
    }

}
