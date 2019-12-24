using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : MonoBehaviour
{
    [SerializeField]
    private float damage = 50f;
    private GameObject impactEffect;
    private Enemy enemy;
    private Player target;
    void Start(){
        enemy = GetComponent<Enemy>();
        target = FindObjectOfType<Player>();
    }

    void OnCollisionEnter2D(Collision2D other) {
    if(other.collider.CompareTag("Player")){ 
        target.TakeDamage(damage);
        enemy.currentHP = -1;
    }
}
}
