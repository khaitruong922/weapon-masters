
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public float maxHP = 100f;
    public float currentHP;
    public float dropChance = 0.5f;
    public GameObject deathEffect;
    public GameObject healthBar; //access to the health bar
    public GameObject healthPack;
    public GameObject textPrefab;
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
        ShowFloatingText(damage);
        }
    public void TakeDamageOverTime(float damagePerTick,float frequency,int numberOfTicks){
        StartCoroutine(DamageOverTime(damagePerTick,frequency,numberOfTicks));
    }
    private IEnumerator DamageOverTime(float damagePerTick, float frequency, int numberOfTicks)
    {
        for (int i=0;i<numberOfTicks;i++){
            TakeDamage(damagePerTick);
            yield return new WaitForSeconds(frequency);
        }
    }
    private void ShowFloatingText(float damage){
        GameObject go = Instantiate(textPrefab,transform.position,Quaternion.identity,transform);
        go.GetComponent<TextMeshPro>().text=damage.ToString();
        go.GetComponent<TextMeshPro>().fontSize=Mathf.Clamp(damage/maxHP,0.5f,1f)*12;
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
        if (Random.Range(0.0f,1.0f) <= dropChance){
        Instantiate(healthPack,transform.position,Quaternion.identity);
        }
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect,0.1f);
        }
    public float PercentHP(){
        return(float) currentHP/maxHP;
    }

    }

  
