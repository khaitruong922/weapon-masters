using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinAbility : MonoBehaviour
{
    [Header("Object references")]
    public Rigidbody2D rb;
    public Transform firePoint;
    public GameObject projectile;
    public GameObject shootEffect;
    private Player player;
    
    [Header("Attack")]
    public float damage = 100f;
    public float attackDelay = 1f;
    public float attackRange = 10f;
    public Transform attackPosition;
    public LayerMask enemyOnly;
    private float attackDelayLeft;

    [Header("Poison Blade")]
    public float poisonDamage = 500f;
    public float poisonDuration = 10f;
    private float poisonDurationLeft;

    [Header("Narrow Escape")]
    public float qDamage = 100f;
    public float qCooldown = 8f;
    public int numberOfShots = 3;
    public float dashForce=50f;
    public float dashTime=0.2f;
    public float bulletSpreadValue = 0.2f;
    public float bulletForce = 35f;
    private float qCooldownLeft;
    private float dashTimeLeft;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(attackDelayLeft <= 0)
        {
            if(Input.GetMouseButton(0))
            {
                Collider2D[] hitZone = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, enemyOnly);
                for (int i = 0; i < hitZone.Length; i++)
                {
                    hitZone[i].GetComponent<Enemy>().TakeDamage(damage);
                    attackDelayLeft = attackDelay;
                }
            }
        }
        else
        {
            attackDelayLeft -= Time.deltaTime;
        }     
        if(qCooldownLeft<=0)
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                Shoot(0f,1f);
                Shoot(bulletSpreadValue,1f);
                Shoot(-bulletSpreadValue,1f);
                dashTimeLeft = dashTime;
                if(dashTimeLeft>=0)
                {
                    dashTimeLeft -= Time.deltaTime;
                }
                qCooldownLeft = qCooldown; 
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

    void Shoot(float x,float y)
    {
        GameObject bullet = Instantiate(projectile, firePoint.position,firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.TransformVector(x,y,0) * bulletForce, ForceMode2D.Impulse);
        GameObject effect = Instantiate(shootEffect,firePoint.position,firePoint.rotation);
        Destroy(effect,0.05f);
    }
}
