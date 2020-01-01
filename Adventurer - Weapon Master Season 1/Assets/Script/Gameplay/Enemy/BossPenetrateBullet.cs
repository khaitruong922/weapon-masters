using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPenetrateBullet : MonoBehaviour
{
    public float damage = 50f;
    public float destroyTime=0.75f;
    public GameObject effect;
    private void Start() {
        Destroy(gameObject,destroyTime);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            GameObject go = Instantiate(effect,transform.position, Quaternion.identity);
            Destroy(go,0.1f);
            other.GetComponent<Player>().TakeDamage(damage);
        }
        if(other.CompareTag("Wall")){
            Destroy(gameObject);
        }
    }
}
