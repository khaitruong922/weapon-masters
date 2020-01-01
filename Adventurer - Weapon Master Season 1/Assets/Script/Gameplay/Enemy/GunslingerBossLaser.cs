using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GunslingerBossLaser: MonoBehaviour{
    public float damagePerSec = 100f;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().TakeDamage(damagePerSec * Time.deltaTime);
        }
    }
}