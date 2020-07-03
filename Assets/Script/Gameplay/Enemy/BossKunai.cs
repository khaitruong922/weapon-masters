using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKunai : MonoBehaviour
{
    private Rigidbody2D rb;
    public float stopTime = 0.4f;
    public float lifetime = 2f;
    public float damage = 250f;
    private CircleCollider2D circleCollider2D;
    private void Start(){
        rb = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        StartCoroutine(Stop());
        Destroy(gameObject,lifetime);
    }   
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Wall")){
            rb.velocity = Vector3.zero;
            circleCollider2D.enabled = false;
            AudioManager.Instance.Play("KunaiHit");
        }
        if(other.CompareTag("Player")){
            other.GetComponent<Player>().TakeDamage(damage);
        }
    }
    private IEnumerator Stop(){
        yield return new WaitForSeconds(stopTime);
        rb.velocity = Vector3.zero;
        circleCollider2D.enabled = false;
        AudioManager.Instance.Play("KunaiHit");
    }
}
