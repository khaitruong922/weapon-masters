using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidlingProjectile : MonoBehaviour
{
public float force=1f;
public float destroyTime=1f;
public Rigidbody2D rb;
public GameObject impactEffect;
private GameObject target;
private Transform targetPos;
private Player targetHealth;
private Vector2 direction;
public float damage = 20f;

void Start() {
    
    target = GameObject.FindGameObjectWithTag("Player");
    StartCoroutine("ProjectileTime",destroyTime);
    if(target!= null){
    
    targetPos = target.GetComponent<Transform>();
    targetHealth = target.GetComponent<Player>(); //get references
    direction = (targetPos.position - transform.position); //register the shoot direction 
    rb.AddForce(direction*force, ForceMode2D.Impulse);
    }
}
void OnCollisionEnter2D(Collision2D other) {
    if (other.gameObject.tag != "Enemy" && other.gameObject.tag != "EnemyProjectile"){ //avoid player collision
            DestroyProjectile();
            }
    if (target != null){
    if(other.gameObject.tag == "Player"){ //if it hits enemy, deal damage
        targetHealth.TakeDamage(damage);
    }
    }
}

void DestroyProjectile(){
    GameObject effect = Instantiate(impactEffect, transform.position, Quaternion.identity); //same
    Destroy(effect,0.25f);
    Destroy(gameObject);
}
IEnumerator ProjectileTime(float seconds){ //destroy projectile after a moment
    yield return new WaitForSeconds(seconds);
    DestroyProjectile();
}
}
