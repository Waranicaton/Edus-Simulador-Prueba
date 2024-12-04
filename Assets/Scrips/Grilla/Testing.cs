using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private MapaDeColor mapaDeColor;
    private Grid grid;
    // Start is called before the first frame update
    private void Start()
    {
        grid = new Grid(4, 2, 5f, new Vector3(4,0,12));

        mapaDeColor.SetGrid(grid);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            grid.SetValue(GetMouseWorldPosition(), 56);
            Debug.Log(GetMouseWorldPosition());
            mapaDeColor.si();
        }
    }


    // Get Mouse Position in World with Z = 0f
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}
