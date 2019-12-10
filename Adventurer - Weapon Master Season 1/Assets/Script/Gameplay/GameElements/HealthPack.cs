using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    private Player playerHealth;
    public float healAmount =1f;

    void Start(){
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Destroy(gameObject,30f);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player"){
            playerHealth.Heal(healAmount);
        Destroy(gameObject);
        }
    }
}
