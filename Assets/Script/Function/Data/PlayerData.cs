using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string playerName;
    public int level;
    public PlayerData(PersistentData persistentData)
    {
        playerName = persistentData.playerName;
        level = persistentData.level;
    }
}
