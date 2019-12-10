 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
public GameObject impactEffect;
public float damage= 20f;
public float destroyTime = 0.6f;
private Rigidbody2D rb;

void Start(){
    StartCoroutine("ProjectileTime",destroyTime);
}

void OnCollisionEnter2D(Collision2D other) {
    if (other.gameObject.tag != "Player" && other.gameObject.tag != "AllyProjectile"){ //avoid player collision
        DestroyProjectile();
    }
    if(other.gameObject.tag == "Enemy"){ //if it hits enemy, deal damage
        other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
    }
    }
void DestroyProjectile(){ //what happens when projectile collides
    GameObject effect = Instantiate(impactEffect, transform.position, Quaternion.identity); //create effect
            Destroy(effect,0.25f); //destroy effect
            Destroy(gameObject); //destroy projectile
}
IEnumerator ProjectileTime(float seconds){ //destroy projectile after a moment
    yield return new WaitForSeconds(seconds);
    DestroyProjectile();
}
}

