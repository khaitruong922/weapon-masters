using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public float healAmount = 1f;
    void Start()
    {
        Destroy(gameObject, 20f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().Heal(healAmount);
            Destroy(gameObject);
        }
    }

}
