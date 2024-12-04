using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManejadorPuntoInteres : MonoBehaviour
{
    public static Dictionary<string, Dictionary<string, Dictionary<string, string>>> resultados;
    public GameObject barraAutoevaluacion;
    public string[,,] grillaPuntosClave;
    public static InformacionClave infoClave;
    public CargarOrganos cargarOrganos;
    public GameObject circuloGrande;
    public GameObject circuloChiquito;
    public GameObject puntoDeInteresPrefab;
    private int cantGrados;
    private double gradosEntreImagen;
    private float limSuperior;
    private float limInferior;
    float factor;
    private int cantImagenes;

    public ImageManager3 imageManager;
    private void Start()
    {
        resultados = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
    }

    private void Update()
    {
    }

    public void Recargar(int cantGrados, double gradosEntreImagen, int cantImagenes, float factor, float limSuperior, float limInferior)
    {
        this.cantGrados = cantGrados;
        this.gradosEntreImagen = gradosEntreImagen;
        this.cantImagenes = cantImagenes;
        this.factor = factor;
        this.limSuperior = limSuperior;
        this.limInferior = limInferior;
        infoClave = cargarOrganos.InfoOrgano();
        grillaPuntosClave = new string[cantImagenes, cantImagenes, 4];
        grillaPuntosClave.Initialize();
        SetearZonaDeCalor(2);
    }

    public void ReiniciarTextoClave()
    {
        //Funciona pero no limpia nada 
        //le saca a resultados, los puntos clave que se guardaron durante la autoevaluacion
        if (resultados.ContainsKey(ImageManager3.datosPuntoActual.nombre))
        {
            if (resultados[ImageManager3.datosPuntoActual.nombre].ContainsKey("Imagen" + ImageManager3.imagen))
            {
                barraAutoevaluacion.GetComponent<BarrasProgreso>().Restar(resultados[ImageManager3.datosPuntoActual.nombre]["Imagen" + ImageManager3.imagen].Count);
                resultados[ImageManager3.datosPuntoActual.nombre].Remove("Imagen" + ImageManager3.imagen);
                if (resultados[ImageManager3.datosPuntoActual.nombre].Count == 0) 
                    resultados.Remove(ImageManager3.datosPuntoActual.nombre);
                imageManager.Pausa();
                imageManager.Pausa();
            }
        }
        
    }

    public void ComprobarYCargarPunto(int posX, int posY, int posZ)
    {

        //Deberia tener un control local de los puntos claves, recibir la pos en la que esta parado
        // e indicarle la imagenmanager que cargue esa imagen clave
        if (grillaPuntosClave[posX, posY, posZ] != null)
        {
            for (int i = 0; i < infoClave.cantPuntos; i++)
            {
                if ((infoClave.posX[i] == posX) && (infoClave.posY[i] == posY) && (infoClave.posZ[i] == posZ))
                {
                    ImageManager3.imagen = i;
                    break;
                }
            }
            imageManager.CargarImagenClave(grillaPuntosClave[posX, posY, posZ]);
        }
    }

    /// informacion clave (cuadraditos amarillos)
    public void CargarGrillaInformacion()
    {
        //intancia en una matriz el nombre de las imagenes clave
        for (int i = 0; i < infoClave.cantPuntos; i++)
        {
            grillaPuntosClave[infoClave.posX[i], infoClave.posY[i], infoClave.posZ[i]] = infoClave.imagen[i];
        }
    }

    //Esta funcion podria delegarse a un script que controle los puntos de interes
    //setea la zona de calor con lo que puedo comprender, los puntos clave 
    public void SetearZonaDeCalor(int ejeZ)
    {
        for (int i = circuloGrande.transform.childCount - 1; i >= 0; i--)
        {
            Transform hijo = circuloGrande.transform.GetChild(i);

            if (hijo.name == circuloChiquito.name)
            {
                continue; // Si coincide, salta este hijo y pasa al siguiente
            }
            Destroy(hijo.gameObject);
        }
        GameObject aux;
        for (int i = 0; i < infoClave.cantPuntos; i++)
        {
            if (infoClave.posZ[i] == ejeZ)
            {
                aux = Instantiate(puntoDeInteresPrefab);
                aux.transform.SetParent(circuloGrande.transform, false);
                aux.transform.localScale = new Vector3(1f, 1f, 1f);
                aux.transform.GetComponent<Image>().SetNativeSize();

                double auxX = 360 - (infoClave.posX[i] * gradosEntreImagen + limInferior + gradosEntreImagen / 2) % 360;
                double auxY = 360 - (infoClave.posY[i] * gradosEntreImagen + limInferior + gradosEntreImagen / 2) % 360;
                float offsetX = (float)((auxX > 180) ? -(auxX - 360) : -auxX);
                float offsetY = (float)((auxY > 180) ? -(auxY - 360) : -auxY);

                aux.transform.localPosition = new Vector3(offsetX * factor, offsetY * factor, 0);
                if (aux.transform.localPosition.magnitude > 250)
                {
                    aux.transform.localPosition = aux.transform.localPosition.normalized * 250;
                }
            }
        }
    }

    public Dictionary<string, Dictionary<string, Dictionary<string, string>>> GetResultados()
    {
        return resultados;
    }
}
