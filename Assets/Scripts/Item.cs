using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public GameObject UIPanel;
    protected void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Destroy(gameObject);
            ShowInfoUI();
            ItemAction();
        }
    }

    protected void ShowInfoUI()
    {
        UIPanel.SetActive(true);
    }

    public abstract void ItemAction();

}
