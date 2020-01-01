using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunslingerBossRicochet : MonoBehaviour
{
    public float damage = 50f;
    public float destroyTime=5f;
    public GameObject effect;
    private void Start() {
        Destroy(gameObject,destroyTime);
    }
    private void OnCollisionEnter2D(Collision2D other) {
        GameObject go = Instantiate(effect,transform.position, Quaternion.identity);
        Destroy(go,0.1f);
        if(other.gameObject.tag=="Player"){
            other.gameObject.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
