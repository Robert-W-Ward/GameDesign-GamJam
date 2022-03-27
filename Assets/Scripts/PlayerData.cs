using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData 
{
    public string lastCompletedLevel;
    public PlayerData(Player player)
    {
        lastCompletedLevel = player.lastCompletedlevel;
    }
}
