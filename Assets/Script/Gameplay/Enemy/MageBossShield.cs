using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageBossShield : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("AllyProjectile")) Destroy(other.gameObject);
    }
}
