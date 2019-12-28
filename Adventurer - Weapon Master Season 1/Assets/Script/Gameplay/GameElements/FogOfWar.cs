using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{
    private SpriteRenderer fogOfWar;
    void Start()
    {
        fogOfWar = GetComponent<SpriteRenderer>();
    }
    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")
        {
            fogOfWar.enabled = false;
        }
    }
    void OnTriggerExit2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")
        {
            fogOfWar.enabled = true;
        }
    }
}


