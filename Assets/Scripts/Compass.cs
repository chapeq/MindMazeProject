using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    public Vector3 NorthDirection;
    public Transform Player;

    public RectTransform compass;

    
    // Update is called once per frame
    void Update()
    {
        NorthDirection.z =  Player.eulerAngles.y;
   
        compass.eulerAngles = NorthDirection;
        
    }
}
