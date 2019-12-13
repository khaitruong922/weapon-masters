﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHP = 100f;
    public float currentHP;
    public GameObject deathEffect;
    public GameObject healthBar; //access to the health bar
    public GameObject healthPack;
    EnemyHealthBar enemyHealthBar;// access to health bar script


    public static int count;
    void Awake() {
        count++;
    }
    void Start(){
        currentHP=maxHP;
        
        }

    public void TakeDamage (float damage){ 
        currentHP -= damage; //health is decreased by damage dealt.
        EnemyHealthBar enemyHealthBar = healthBar.GetComponent<EnemyHealthBar>();
        enemyHealthBar.SetSize(PercentHP());
        }
    void Update() {
        if (currentHP <= 0){
            Die();
        }
    }
    public void Die(){
        count--;
        Debug.Log("Enemy left: " + count);
        Destroy(gameObject);
        //death effect
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect,0.1f);
        Instantiate(healthPack,transform.position,Quaternion.identity);
        }
    public float PercentHP(){
        return(float) currentHP/maxHP;
    }

    }

  