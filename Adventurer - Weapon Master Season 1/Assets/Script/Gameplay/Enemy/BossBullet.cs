using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float damage = 50f;
    public float destroyTime=0.75f;
    public GameObject effect;
    private void Start() {
        Destroy(gameObject,destroyTime);
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag=="Player"){
            GameObject go = Instantiate(effect,transform.position, Quaternion.identity);
            Destroy(go,0.1f);
            other.gameObject.GetComponent<Player>().TakeDamage(damage);
            Destroy(gameObject);
        }
        if(other.gameObject.tag=="Wall"){
            Destroy(gameObject);
        }
    }
}
