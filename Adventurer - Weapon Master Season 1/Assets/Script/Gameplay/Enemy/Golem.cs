using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour
{
    public float attackRange = 1f;
    public float damage = 20f;
    public float attackSpeed = 1f;
    public float radius = 2f;
    public float attackDelay = 0.4f;
    private float timeBtwAttack;
    public Transform attackPoint;
    public LayerMask playerLayer;
    private GameObject target;
    private Transform targetPos;
    private Animator anim;
    void Start()
    {
        target = GameObject.FindWithTag("Player");
        targetPos = target.GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        /* Check Distance between Enemy and Player
        If in range -> Attack
        If not -> Stand By */
        if (target != null)
        { /*make sure the attack command is only called when the player is still alive
       or it will print errors in the console */
            float distanceToPlayer = Vector3.Distance(this.transform.position, targetPos.position);
            if (distanceToPlayer <= attackRange)
            {
                // Check if enough time have passed since the last attack so that it can attack again
                if (timeBtwAttack <= 0)
                {
                    StartCoroutine(Attack(attackDelay, damage));
                    timeBtwAttack = 1 / attackSpeed;
                }
                else
                {
                    timeBtwAttack -= Time.deltaTime;
                }
            }
        }
    }
    private IEnumerator Attack(float delay, float damage)
    {
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(delay);
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, radius, playerLayer);
        foreach (Collider2D player in hitPlayers)
        {
            player.GetComponent<Player>().TakeDamage(damage);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }
}
