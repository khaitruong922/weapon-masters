using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    private Rigidbody2D rb;
    public float stopTime = 0.25f;
    public float lifetime = 5f;
    public float damage = 250f;
    private CircleCollider2D circleCollider2D;
    private void Start(){
        rb = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        StartCoroutine(Stop());
        StartCoroutine(SetInactive());
        Destroy(gameObject,lifetime+10);
    }   
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Wall")){
            rb.velocity = Vector3.zero;
            circleCollider2D.enabled = false;
        }
        if(other.CompareTag("Enemy")){
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
    private IEnumerator Stop(){
        yield return new WaitForSeconds(stopTime);
        rb.velocity = Vector3.zero;
        circleCollider2D.enabled = false;
    }
    private IEnumerator SetInactive(){
        yield return new WaitForSeconds(lifetime);
        gameObject.SetActive(false);
    }
}
