using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMap : Item
{
    public GameObject MapButton; 
    public override void ItemAction()
    {
        MapButton.SetActive(true);
    }
}
