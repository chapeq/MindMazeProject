using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionFlashlight : Item
{
    public GameObject LightButton;
    public override void ItemAction()
    {
        LightButton.SetActive(true);
    }
}
