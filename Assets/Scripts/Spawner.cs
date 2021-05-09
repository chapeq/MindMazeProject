using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Enemy;
    public Transform[] positions;
  
    void Start()
    {
       Transform temp = positions[Random.Range(0, positions.Length)];

        Instantiate<GameObject>(Enemy,temp.position ,Quaternion.identity);
    }
}


