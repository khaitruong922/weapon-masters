using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
public GameObject impactEffect;
public float damage= 50f;
public float returnTime = 1f;
public float returnSpeed = 50f;
public float anglePerSecond = 120f;
private Transform player;

void Start(){
    returnTime = Time.time + returnTime;
    player = FindObjectOfType<Player>().GetComponent<Transform>();
}
void Update(){
    if(Time.time > returnTime){
        transform.position = Vector2.MoveTowards(transform.position,player.position,returnSpeed*Time.deltaTime);
        if (transform.position == player.position){
            Destroy(gameObject);
        }
    }
}

void OnTriggerEnter2D(Collider2D other) {
    if(other.gameObject.tag == "Enemy"){ //if it hits enemy, deal damage
        other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        GameObject effect = Instantiate(impactEffect, transform.position, Quaternion.identity); //create effect
        Destroy(effect,0.25f);
    }
} 
}

