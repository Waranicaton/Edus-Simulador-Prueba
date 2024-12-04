using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapaDeColor : MonoBehaviour
{
    private Grid grid;
    private Mesh mesh;
    private int aux;

    private void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }
    public void SetGrid(Grid grid)
    {
        this.grid = grid;
        UpdateMapaDeColor();
        
    }

    public void si()
    {
        aux++;
        UpdateMapaDeColor();
    }

    public void OnServerInitialized()
    {
        UpdateMapaDeColor();
    }

    private void UpdateMapaDeColor()
    {
        MeshUtils.CreateEmptyMeshArrays(grid.GetWidth() * grid.GetHeight(), out Vector3[] vertices, out Vector2[] uv, out int[] triangles);

        for ( int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                Debug.Log("si");
                int index = x * grid.GetHeight() + y;
                Vector3 quadSize = new Vector3(1, 1) * grid.GetCellSize();
                Vector2 gridValueUV = new Vector2(aux, 0f);
                MeshUtils.AddToMeshArrays(vertices, uv, triangles, index, grid.GetWorldPosition(x, y) + quadSize * 0.5f, 0f, quadSize, gridValueUV, gridValueUV);
            } 
        }
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }
}
