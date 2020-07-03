using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashHitbox : MonoBehaviour
{
public float damage = 500f;
private void OnTriggerEnter2D(Collider2D other) {
    if (other.CompareTag("Enemy")){
        other.GetComponent<Enemy>().TakeDamage(damage);
    }
}
}