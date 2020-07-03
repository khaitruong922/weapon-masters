using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public float delay = 1f;
    public float damagePerSecond = 100f;
    public float lifetime = 5f;
    public float sizeIncrease = 5f;
    public float pullForce = 3.5f;
    private Rigidbody2D rb;
    private CircleCollider2D col;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
        Destroy(gameObject, lifetime);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Activate();
        col.isTrigger = true;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.transform.position = Vector2.MoveTowards(other.transform.position, transform.position, pullForce * Time.deltaTime);
            other.GetComponent<Enemy>().TakeDamage(damagePerSecond * Time.deltaTime);
        }
    }
    private void Activate()
    {
        AudioManager.Instance.Play("BlackHole");
        StartCoroutine(Activate(delay));
        rb.velocity = new Vector2(0, 0);
    }
    private IEnumerator Activate(float delay)
    {
        float t = 0;
        while (t <= 1f)
        {
            t += Time.deltaTime / delay;
            transform.localScale = new Vector3(1 + t * sizeIncrease, 1 + t * sizeIncrease, 1);
            yield return null;
        }
    }
}
