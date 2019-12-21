﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinAbility : MonoBehaviour
{
    [Header("Object references")]
    private Rigidbody2D rb;
    public GameObject projectile;
    public GameObject shootEffect;
    private Player player;
    private Animator anim;
    
    [Header("Attack")]
    public float damage = 100f;
    private float timeBtwAttack;
    public float attackSpeed = 2.5f;
    public float attackRange = 10f;
    public Transform attackPosition;
    public LayerMask enemyOnly;

    [Header("Poison Blade")]
    public float poisonDamage = 500f;
    public float poisonDuration = 10f;
    private float poisonDurationLeft;

    [Header("Narrow Escape")]
    public float qDamage = 100f;
    public float qCooldown = 8f;
    public float dashForce=20f;
    public float dashTime=0.2f;
    public float projectileSpreadValue = 0.2f;
    public float projectileForce = 20f;
    private float qCooldownLeft;
    private float dashTimeLeft;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(timeBtwAttack <= 0)
        {
            if(Input.GetMouseButton(0))
            {
                anim.SetTrigger("Attack");

                Collider2D[] hitZone = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, enemyOnly);
                for (int i = 0; i < hitZone.Length; i++)
                {
                    hitZone[i].GetComponent<Enemy>().TakeDamage(damage);
                    timeBtwAttack = 1/attackSpeed;
                }
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }     
        if(qCooldownLeft<=0)
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                qCooldownLeft = qCooldown;
                for(float i =-2;i<3;i++){
                   Shoot(projectileSpreadValue*i,1f);
                }
                dashTimeLeft = dashTime;
                if(dashTimeLeft>=0)
                {
                    dashTimeLeft -= Time.deltaTime;
                }
            }
        }
        else
        {
            qCooldownLeft -= Time.deltaTime;
        }
    }
    
    void OnDrawGizmosSelected() //hitZone radius checker (Scene only)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }
    
    private void FixedUpdate() 
    {
        if(dashTimeLeft>=0)
        {
            rb.AddForce(-player.lookDir.normalized * dashForce,ForceMode2D.Impulse);
            dashTimeLeft -= Time.fixedDeltaTime;
        }
    }

    void Shoot(float x,float y){
        GameObject bullet = Instantiate(projectile, attackPosition.position,attackPosition.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(attackPosition.TransformVector(x,y,0) * projectileForce, ForceMode2D.Impulse);
    }
}