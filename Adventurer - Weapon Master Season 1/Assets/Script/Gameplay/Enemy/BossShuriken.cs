using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShuriken : MonoBehaviour
{
    public GameObject impactEffect;
    public float damage = 20f;
    public float returnTime = 1f;
    public float returnSpeed = 50f;
    public float anglePerSecond = 120f;
    private Transform boss;

    void Start()
    {
        returnTime = Time.time + returnTime;
        boss = FindObjectOfType<AssassinBoss>().GetComponent<Transform>();
    }
    void Update()
    {
        if (Time.time > returnTime && boss!=null)
        {
            transform.position = Vector2.MoveTowards(transform.position, boss.position, returnSpeed * Time.deltaTime);
            if (transform.position == boss.position)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        { //if it hits enemy, deal damage
            other.gameObject.GetComponent<Player>().TakeDamage(damage);
            GameObject effect = Instantiate(impactEffect, transform.position, Quaternion.identity); //create effect
            Destroy(effect, 0.25f);
        }
    }
}
