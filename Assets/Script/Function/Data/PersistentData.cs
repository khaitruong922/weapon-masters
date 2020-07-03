using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PersistentData : MonoBehaviour
{
    public static PersistentData Instance { get; set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Load();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public int level = 1;
    public string playerName = "Teacher Three";
    public void Save()
    {
        SaveSystem.SavePlayer(this);
    }
    public void Load()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        level = data.level;
        playerName = data.playerName;
    }
    private void OnApplicationQuit()
    {
        Save();
    }

}

