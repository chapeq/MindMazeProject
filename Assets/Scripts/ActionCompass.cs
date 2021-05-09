using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCompass : Item
{
    public GameObject compass;
    public override void ItemAction()
    {
        compass.SetActive(true);
    }
}
