 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingBullets : MonoBehaviour
{
public GameObject impactEffect;
public float damage= 20f;
public float destroyTime = 0.6f;
private Rigidbody2D rb;

void Start(){
    StartCoroutine("ProjectileTime",destroyTime);
    rb = GetComponent<Rigidbody2D>();
}
void Update(){
}

void OnCollisionEnter2D(Collision2D other) {
    CreateEffect();
    if(other.gameObject.tag == "Enemy"){ //if it hits enemy, deal damage
        other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        DestroyProjectile();
    }

    }
void DestroyProjectile(){ //what happens when projectile collides
    CreateEffect();
    Destroy(gameObject); //destroy projectile
}
void CreateEffect(){
    GameObject effect = Instantiate(impactEffect, transform.position, Quaternion.identity); //create effect
    Destroy(effect,0.25f); //destroy effect
}
IEnumerator ProjectileTime(float seconds){ //destroy projectile after a moment
    yield return new WaitForSeconds(seconds);
    DestroyProjectile();
}
}
