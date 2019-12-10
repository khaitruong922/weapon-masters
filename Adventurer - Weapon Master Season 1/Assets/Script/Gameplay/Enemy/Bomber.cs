using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : MonoBehaviour
{
    [SerializeField]
    private float damage = 50f;
    private GameObject impactEffect;
    private Enemy enemy;
    private Player playerHealth;
    void Start(){
        enemy = gameObject.GetComponent<Enemy>();
        playerHealth = FindObjectOfType<Player>();
    }

    void OnCollisionEnter2D(Collision2D other) {
    if(other.gameObject.tag == "Player"){ 
        playerHealth.TakeDamage(damage);
        enemy.currentHP = -1;
    }
}
}
