using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using UnityEngine.UI;
//using AnotherFileBrowser.Windows;

public class BuscadorDirectorio
{
   private string[] extensiones = { "Carpeta", "" };
   public string nombreCarpeta ;

    
    /*
   public string ReadPath()
    {
        string path = EditorUtility.OpenFolderPanel("Selecciona una Carpeta", "", "");
        path = System.IO.Path.Combine(path, nombreCarpeta);
        System.IO.Directory.CreateDirectory(path);
        return path;
    }
    */
    public string readExePath()
    {
        return Path.GetFullPath("./");
    }
    
    /*
    public string ReadZipPath()
    {
        var bp = new BrowserProperties();
        bp.filter = "Zip files (*.zip) | *.zip";
        bp.filterIndex = 0;
        var dir = "";
        new FileBrowser().OpenFileBrowser(bp, path =>
        {
            dir = path;
        });
        return dir;
    }

    public string ReadPath()
    {
        var bp = new BrowserProperties();
        //bp.filter = "t (*.zip) | *.zip";
        bp.filterIndex = 2;
        var dir = "";
        new FileBrowser().OpenFolderBrowser(bp, path =>
        {
            dir = path;
        });
        return dir;
    }

    */

}
