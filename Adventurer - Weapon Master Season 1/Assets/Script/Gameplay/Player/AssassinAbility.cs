using System.Collections;
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
    private bool attack = true;
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

    [Header("Plan B")]
    public float eDamage = 100f;
    public float eReloadTime = 3f;
    public int eAmmo = 10;
    private float eReloadTimeLeft;
    private float eAmmoLeft;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        eAmmoLeft = eAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (attack == true)
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

        if(eAmmoLeft > 0)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(timeBtwAttack <= 0)
                {
                    attack = false;
                    Shoot(0f,1f);
                    eAmmoLeft -= 1;
                }
            }
        }
        else
        {
            attack = true;
            if(eReloadTimeLeft <= 0)
            {
                eAmmoLeft = eAmmo;
                eReloadTimeLeft = eReloadTime;
            }
            else
            {
                eReloadTimeLeft -= Time.deltaTime;
            }
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
