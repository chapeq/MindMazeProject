using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionOrb : Item
{
    public PlayerStats player;
    public override void ItemAction()
    {
        player.AddPtsVie(10);
    }
}
