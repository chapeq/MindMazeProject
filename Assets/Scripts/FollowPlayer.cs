using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player; 
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }


    void LateUpdate()
    {
        this.transform.position = Vector3.SmoothDamp(this.transform.position, player.transform.position + offset, ref velocity, smoothTime);
        
    }
}
    

