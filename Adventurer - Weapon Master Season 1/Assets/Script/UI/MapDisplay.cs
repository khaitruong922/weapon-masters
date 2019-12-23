using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MapDisplay : MonoBehaviour
{
    public Map[] map;
    public TextMeshProUGUI mapNameText;
    public TextMeshProUGUI modeText;
    public TextMeshProUGUI difficultyText;
    public TextMeshProUGUI descriptionText;
    int mapNumber;
    void OnEnable() {
        mapNumber = PlayerPrefs.GetInt("MapNumber");
        mapNameText.text = map[mapNumber].mapName;
        modeText.text = map[mapNumber].mode;
        difficultyText.text = map[mapNumber].difficulty;
        descriptionText.text = map[mapNumber].description;
    }


}

