using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinBoss : MonoBehaviour
{
    [Header("Object references")]
    private Rigidbody2D rb;
    private Animator anim;
    private EnemyRotation enemy;
    private SpriteRenderer spriteRenderer;
    public LayerMask playerOnly;

    [Header("Attack")]
    public Transform attackPosition;
    public float damage = 60f;
    public float attackRadius = 1.2f;
    public float attackSpeed = 2.5f;

    [Header("Bleeding Edge")]
    public float damagePerTick = 10f;
    public int numberOfTicks = 4;
    public float frequency = 0.5f;


    [Header("Arc Stars")]
    public GameObject shuriken;
    public float qCooldown = 5f;
    public float projectileSpreadValue = 0.2f;
    public float projectileForce = 30f;

    [Header("Dash")]
    public float eCooldown = 5f;
    public float dashSpeed = 30f;
    public float dashTime = 0.2f;
    private float dashTimeLeft;
    [Header("Lethal Attack")]
    public GameObject kunai;
    public GameObject dashHitbox;
    public float ultDashSpeed = 50f;
    public float rCooldown = 22f;
    private Transform player;

    [Header("Clone")]
    public GameObject clone;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>().GetComponent<Transform>();
        enemy = GetComponent<EnemyRotation>();
        InvokeRepeating("Attack", 1f, 1 / attackSpeed);
        InvokeRepeating("SpreadProjectile", qCooldown, qCooldown);
        InvokeRepeating("Dash", eCooldown, eCooldown);
        InvokeRepeating("LethalAttack", rCooldown, rCooldown);
    }
    private void Shoot(float x, float y, GameObject projectile)
    {
        GameObject go = Instantiate(projectile, attackPosition.position, transform.rotation);
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        rb.AddForce(attackPosition.TransformVector(x, y, 0) * projectileForce, ForceMode2D.Impulse);
    }
    private void Dash(){
        dashTimeLeft=dashTime;
    }
    private void FixedUpdate()
    {
        
        if (dashTimeLeft > 0)
        {
            dashTimeLeft-=Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, player.position, dashSpeed * Time.deltaTime);
        }
    }
    private void SpreadProjectile()
    {
        for (int i = -5; i < 6; i++)
        {
            Shoot(i*projectileSpreadValue, 1, shuriken);
        }
    }
    private IEnumerator LethalAttack(){
        Shoot(0,1,kunai);
        Transform destination = kunai.GetComponent<Transform>();
        yield return new WaitForSeconds(0.75f);
        while (Vector2.Distance(transform.position,destination.position)<1.5){
            dashHitbox.SetActive(true);
            DashToKunai(destination);
            yield return null;
        }
        dashHitbox.SetActive(false);
        
    }
    private void DashToKunai(Transform destination){
        transform.position = Vector2.MoveTowards(transform.position,destination.position,ultDashSpeed*Time.deltaTime);
    }
    private void Attack()
    {
        anim.SetTrigger("Attack");
        Collider2D[] hitZone = Physics2D.OverlapCircleAll(attackPosition.position, attackRadius, playerOnly);
        for (int i = 0; i < hitZone.Length; i++)
        {
            hitZone[i].GetComponent<Player>().TakeDamage(damage);
            hitZone[i].GetComponent<Player>().TakeDamageOverTime(damagePerTick, frequency, numberOfTicks);
        }
    }
}
