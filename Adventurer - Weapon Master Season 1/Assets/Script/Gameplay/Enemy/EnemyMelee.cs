using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    public float enemyAttackRange=1f;
    public float enemyDamage=20f;
    // These are needed for the Attack System 
    public float attackSpeed = 3f; //how many attacks player can make in a second
    private float timeBtwAttack;
    private GameObject target;
    private Transform targetPos;
    private Player targetHealth;
    void Start(){
        target = GameObject.FindWithTag("Player");
        targetPos = target.GetComponent<Transform>();
        targetHealth = target.GetComponent<Player>();
    }
    void Update()
    {
       /* Check Distance between Enemy and Player
       If in range -> Attack
       If not -> Stand By */
       if(target!=null){ /*make sure the attack command is only called when the player is still alive
       or it will print errors in the console */
            float distanceToPlayer = Vector3.Distance(this.transform.position, targetPos.position);
            if (distanceToPlayer <= enemyAttackRange)
            {
           // Check if enough time have passed since the last attack so that it can attack again
                if (timeBtwAttack <= 0)
                {
                    targetHealth.TakeDamage(enemyDamage);
                    // Record the attack's time
                    timeBtwAttack = 1 / attackSpeed;
                }
                else
                {
                    timeBtwAttack -= Time.deltaTime;
                }
            }
       }
    }
}