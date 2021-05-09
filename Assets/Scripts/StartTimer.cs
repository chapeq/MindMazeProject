using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTimer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Timer.instance.isTimerOn = true;
            Timer.instance.StartTimer();
            Destroy(gameObject);
        }
    }
}
