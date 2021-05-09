using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
    public GameObject WinUI;
    private GameObject bgMusic;

    private void Start()
    {
        bgMusic = GameObject.Find("BackgroundMusic");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bgMusic.SetActive(false);
            Timer.instance.isTimerOn = false;
            WinUI.SetActive(true);
            Destroy(gameObject);
        }
    }
}
