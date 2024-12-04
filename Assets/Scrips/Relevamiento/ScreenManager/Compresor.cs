using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class Compresor : MonoBehaviour
{
    private string origen;
    private string destino;
    private DatosPunto datosPunto;
    public GameObject menuFinal;
    private void comprimir()
    {

        string json = JsonConvert.SerializeObject(datosPunto);
        Debug.Log(json);
        File.WriteAllText(origen  + @"/Datos.txt", json);
        if (datosPunto.info)
        {
            this.guardaInfoPuntosClave();
        }
        //ZipFile.CreateFromDirectory(origen, destino);
        menuFinal.SetActive(true);
    }

    
    private void guardaInfoPuntosClave()
    {
       
        string[] archivos = Directory.GetFiles(origen + "/Imagenes_Clave");

        // Arrays para almacenar los números y los nombres de archivo
        int[] numero1 = new int[archivos.Length];
        int[] numero2 = new int[archivos.Length];
        int[] numero3 = new int[archivos.Length];
        string[] nombresArchivos = new string[archivos.Length];

        for (int i = 0; i < archivos.Length; i++)
        {
            // Obtener el nombre del archivo sin la extensión
            string nombreArchivo = Path.GetFileNameWithoutExtension(archivos[i]);

            // Almacenar el nombre del archivo completo
            nombresArchivos[i] = nombreArchivo + ".png";

            // Dividir el nombre del archivo en partes usando el carácter '_'
            string[] partes = nombreArchivo.Split('_');

            // Asegurarse de que haya exactamente tres partes
            if (partes.Length == 3)
            {
                // Convertir las partes en números enteros
                if (int.TryParse(partes[0], out int num1) &&
                    int.TryParse(partes[1], out int num2) &&
                    int.TryParse(partes[2], out int num3))
                {
                    numero1[i] = num1;
                    numero2[i] = num2;
                    numero3[i] = num3;
                }
            }
        }

        InformacionClave informacion = new InformacionClave(archivos.Length,numero1,numero2,numero3,nombresArchivos);
        string json = JsonConvert.SerializeObject(informacion);
        File.WriteAllText(origen + "/Imagenes_Clave" + @"/Informacion.txt", json);

    }
    public void cargarDirOrigen(string origen1, int alto, int ancho)
    {
        this.origen = origen1;
        this.datosPunto.height = alto;
        this.datosPunto.width = ancho;
        this.comprimir();
    }

    public void cargarDatos(DatosPunto datosPunto)
    {
        
        this.datosPunto = datosPunto;
        //this.destino = destino2 + @"\" + this.datosOrganos.nombre + ".zip";
        //Debug.Log("destino ya sumado"+this.destino);
        
    }
}
