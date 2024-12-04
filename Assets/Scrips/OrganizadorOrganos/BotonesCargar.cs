using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using TMPro;
using System.IO;
using UnityEngine.UI;

public class BotonesCargar : MonoBehaviour
{
    public CargarOrganos cargarOrganos;
    public GameObject imageManager3;
    private int puntoActual = 0;
    public GameObject Parametros;
    public GameObject interfazCuerpo;

    /// <summary>
    /// CambiarPunto
    /// </summary>

    private GameObject puntoAnterior = null;
    public Sprite texturaActivo;
    public Sprite texturaDesactivado;
    public GameObject tituloPunto;
    public GameObject contenido;
    private GameObject cantidadDePuntosClave;
    public GameObject cantidadDePuntosClave1;
    public GameObject cantidadDePuntosClave2;
    public GameObject cantidadDePuntosClave3;


    /// <summary>
    /// 
    /// </summary>

    private bool hayParametros = false;


    ///PRIMERA VEZ DE EJECUCION
    private const string isFirstTimeKey = "IsFirstTime";
    public GameObject menuInformativo;
    private static bool primera = true;
    public BotonZoomYBrillo botonZoomYBrillo;

    public void CargaParametrizada(int punto)
    {
        hayParametros = Parametros.GetComponent<ComprobarParametros>().ParametrosCorrectos();
        if (IsFirstTime())
        {
        }
        else
        {
            RealizarAlgunaFuncion();
        }
        if (hayParametros)
        {
            cargarOrganos.CargarOrgano(Parametros.GetComponent<ComprobarParametros>().SetImagenes());
            puntoActual = Parametros.GetComponent<ComprobarParametros>().Punto();
            string nombrePuntoClave = "PuntoClave-" + puntoActual;
            Transform hijo = interfazCuerpo.transform.Find(nombrePuntoClave);
            hijo.gameObject.GetComponent<Image>().sprite = texturaActivo;
            hijo.gameObject.GetComponent<Image>().SetNativeSize();
            puntoAnterior = hijo.gameObject;
            cargarOrganos.AsignarPuntoActual(puntoActual);
            cargarOrganos.cargarGrilla();
            cargarOrganos.dificultad = Parametros.GetComponent<ComprobarParametros>().Dificultad();
            primera = false;
            imageManager3.SetActive(true);
            if (Parametros.GetComponent<ComprobarParametros>().Dificultad() == 2)
            {
                cantidadDePuntosClave = cantidadDePuntosClave2;
                cantidadDePuntosClave1.SetActive(false);
            }
            else
            {
                cantidadDePuntosClave = cantidadDePuntosClave1;
            }
            actualizarPuntoYTexto(cargarOrganos.datosPunto().descripcion);
        }
        else
        {
            cargarOrganos.CargarOrgano(Parametros.GetComponent<ComprobarParametros>().SetImagenes());
            CargarOrgano(1);
            actualizarPuntoYTexto(cargarOrganos.datosPunto().descripcion);
        }
    }

    public void CargarOrgano(int punto)
    {
        if (punto != puntoActual)
        {
            puntoActual = punto;
            string nombrePuntoClave = "PuntoClave-" + puntoActual;
            Transform hijo = interfazCuerpo.transform.Find(nombrePuntoClave);
            if (puntoAnterior != null)
            {
                puntoAnterior.GetComponent<Image>().sprite = texturaDesactivado;
                puntoAnterior.GetComponent<Image>().SetNativeSize();
            }
            hijo.gameObject.GetComponent<Image>().sprite = texturaActivo;
            hijo.gameObject.GetComponent<Image>().SetNativeSize();
            puntoAnterior = hijo.gameObject;
            //cargarOrganos.CargarOrgano(Parametros.GetComponent<ComprobarParametros>().SetImagenes());
            cargarOrganos.AsignarPuntoActual(punto);
            cargarOrganos.cargarGrilla();
            if (primera)
            {
                primera = false;
                imageManager3.SetActive(true);
            }
            else
            {
                imageManager3.GetComponent<ImageManager3>().recargarImageManager();
            }
            actualizarPuntoYTexto(cargarOrganos.datosPunto().descripcion);
            botonZoomYBrillo.ReiniciarBotonesBrillo();
        }
    }

    private void actualizarPuntoYTexto(string descripcion)
    {
        string[] datos = descripcion.Split(";");
        tituloPunto.GetComponent<TextMeshProUGUI>().text = datos[0];
        contenido.GetComponent<TextMeshProUGUI>().text = datos[1];
        cantidadDePuntosClave.GetComponent<TextMeshProUGUI>().text = datos[2];
        cantidadDePuntosClave3.GetComponent<TextMeshProUGUI>().text = datos[3];
        /*switch (numero)
        {
            case 1:
                tituloPunto.GetComponent<TextMeshProUGUI>().text = "ETAPA 1";
                contenido.GetComponent<TextMeshProUGUI>().text = "Corte axial y sagital de epigastrio. \nVena porta y sus ramas izquierdas. \nVisualización de los segmentos hepáticos izquierdos (II, III y IV) y el segmento I.";
                cantidadDePuntosClave.GetComponent<TextMeshProUGUI>().text = "IDENTIFIQUE 3 PUNTOS";
                cantidadDePuntosClave3.GetComponent<TextMeshProUGUI>().text = "IDENTIFIQUE 3 PUNTOS";
                break;
            case 2:
                tituloPunto.GetComponent<TextMeshProUGUI>().text = "ETAPA 2";
                contenido.GetComponent<TextMeshProUGUI>().text = "Corte axial.\nVena porta. Bifurcación y ramas derechas.\nArteria hepática. Colédoco. Segmento IV.\nVenas suprahepáticas y relación con la vena cava.   Segmentos II, IV, VIII y VII.";
                cantidadDePuntosClave.GetComponent<TextMeshProUGUI>().text = "IDENTIFIQUE 2 PUNTOS";
                cantidadDePuntosClave3.GetComponent<TextMeshProUGUI>().text = "IDENTIFIQUE 2 PUNTOS";
                break;
            case 3:
                tituloPunto.GetComponent<TextMeshProUGUI>().text = "ETAPA 3";
                contenido.GetComponent<TextMeshProUGUI>().text = "SUBCOSTAL MEDIO LATERAL.\nCorte axial.\nVenas suprahepáticas y relación con la vena cava.\nDiafragma e imagen en espejo.";
                cantidadDePuntosClave.GetComponent<TextMeshProUGUI>().text = "IDENTIFIQUE 1 PUNTO";
                cantidadDePuntosClave3.GetComponent<TextMeshProUGUI>().text = "IDENTIFIQUE 1 PUNTO";
                break;
            case 4:
                tituloPunto.GetComponent<TextMeshProUGUI>().text = "ETAPA 4";
                contenido.GetComponent<TextMeshProUGUI>().text = "INTERCOSTAL BAJO VERTICAL.\nCorte sagital.\nEspacio de Morrison entre hígado y riñón derecho.\nSegmentos derechos V, VI, VII y VIII.";
                cantidadDePuntosClave.GetComponent<TextMeshProUGUI>().text = "IDENTIFIQUE 1 PUNTO";
                cantidadDePuntosClave3.GetComponent<TextMeshProUGUI>().text = "IDENTIFIQUE 1 PUNTO";
                break;
            case 5:
                tituloPunto.GetComponent<TextMeshProUGUI>().text = "ETAPA 5";
                contenido.GetComponent<TextMeshProUGUI>().text = "INTERCOSTAL AXIAL.\nCorte axial.\nVena suprahepática, diafragma y Segmento VIII.";
                cantidadDePuntosClave.GetComponent<TextMeshProUGUI>().text = "NO HAY PUNTOS";
                cantidadDePuntosClave3.GetComponent<TextMeshProUGUI>().text = "NO HAY PUNTOS";
                break;
            case 6:
                tituloPunto.GetComponent<TextMeshProUGUI>().text = "ETAPA 6";
                contenido.GetComponent<TextMeshProUGUI>().text = "VESICULA SUBCOSTAL.\nCorte sagital.\nVesícula biliar, Vena Cava y Vena Porta.\nSegmentos derechos V y VI.\nColédoco.";
                cantidadDePuntosClave.GetComponent<TextMeshProUGUI>().text = "IDENTIFIQUE 2 PUNTOS";
                cantidadDePuntosClave3.GetComponent<TextMeshProUGUI>().text = "IDENTIFIQUE 2 PUNTOS";
                break;
            case 7:
                tituloPunto.GetComponent<TextMeshProUGUI>().text = "ETAPA 7";
                contenido.GetComponent<TextMeshProUGUI>().text = "VESICULA LATERAL.\nVesícula biliar (fondo, cuerpo y cuello). Conducto cístico.\nSegmentos derechos V y VI.";
                cantidadDePuntosClave.GetComponent<TextMeshProUGUI>().text = "IDENTIFIQUE 1 PUNTO";
                cantidadDePuntosClave3.GetComponent<TextMeshProUGUI>().text = "IDENTIFIQUE 1 PUNTO";
                break;
        }*/
    }
    bool IsFirstTime()
    {
#if UNITY_STANDALONE_OSX
        return File.Exists(Path.Combine(Application.streamingAssetsPath, "datos.json"));
#elif UNITY_STANDALONE_WIN
        return File.Exists(Path.Combine(Application.persistentDataPath, "datos.json"));
#endif
    }
    void RealizarAlgunaFuncion()
    {
        menuInformativo.SetActive(true);
    }
    void CopyDirectory(string source, string destination)
    {
        DirectoryInfo sourceDir = new DirectoryInfo(source);
        DirectoryInfo destinationDir = new DirectoryInfo(destination);
        if (!sourceDir.Exists)
        {
            Debug.LogError("ERROR EN EL DIRECTORIO ORIGEN");
            Debug.LogError(source);
            return;
        }
        if (!destinationDir.Exists)
        {
            destinationDir.Create();
        }
        foreach (FileInfo file in sourceDir.GetFiles())
        {
            string destinationFilePath = Path.Combine(destinationDir.FullName, file.Name);
            file.CopyTo(destinationFilePath, true);
        }
        foreach (DirectoryInfo subdir in sourceDir.GetDirectories())
        {
            string destinationSubDirPath = Path.Combine(destinationDir.FullName, subdir.Name);
            CopyDirectory(subdir.FullName, destinationSubDirPath);
        }
    }
}
 