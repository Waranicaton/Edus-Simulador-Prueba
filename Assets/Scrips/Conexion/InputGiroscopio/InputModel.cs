using UnityEngine;

public class InputModel
{
    public float GyroscopeX {set; get;}
    public float GyroscopeY {set; get;}
    public float GyroscopeZ {set; get;}
   
    
    public InputModel()
    {
        GyroscopeX = 0;
        GyroscopeY = 0;
        GyroscopeZ = 0;
    }

    public InputModel(float Gyroscopex , float Gyroscopey, float Gyroscopez)
    {
        GyroscopeX = Gyroscopex;
        GyroscopeY = Gyroscopey;
        GyroscopeZ = Gyroscopez;
    }
}
