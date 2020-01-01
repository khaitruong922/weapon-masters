using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageExplosion : MonoBehaviour
{
    public float damage = 600f;
    public float delay = 0.4f;
    private CircleCollider2D circleCollider2D;
    private void Start() {
        circleCollider2D = GetComponent<CircleCollider2D>();
        circleCollider2D.enabled = true;
        StartCoroutine(Explode());
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemy")){
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
    private IEnumerator Explode(){
        yield return new WaitForSeconds(0.03f);
        circleCollider2D.enabled=false;
        yield return new WaitForSeconds(delay-0.03f);
        circleCollider2D.enabled=true;
        yield return new WaitForSeconds(0.03f);
        circleCollider2D.enabled=false;
    }
}
