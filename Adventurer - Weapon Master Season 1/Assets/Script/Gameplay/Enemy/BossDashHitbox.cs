using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDashHitbox : MonoBehaviour
{
    public float damage = 100f;
    private void OnTriggerEnter2D(Collider2D other) {
    if (other.CompareTag("Player")){
        other.GetComponent<Player>().TakeDamage(damage);
    }
}
}
