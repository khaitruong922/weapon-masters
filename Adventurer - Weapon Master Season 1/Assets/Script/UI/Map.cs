using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Map", menuName = "Map", order = 0)]
public class Map: ScriptableObject {
    [TextArea(2,2)]
    public string mapName;

    public string mode;
    public string difficulty;
    [TextArea(15,20)]
    public string description;
}
