using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinAbility : MonoBehaviour
{
    [Header("Object references")]
    private Rigidbody2D rb;
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
    public GameObject shuriken;    
    public float damagePerTick = 200f;
    public float frequency = 0.25f;
    public int numberOfTicks = 8;

    [Header("Arc Stars")]
    public float qCooldown = 8f;
    public float projectileSpreadValue = 0.2f;
    public float projectileForce = 20f;
    [HideInInspector]public float qCooldownLeft;
    private float dashTimeLeft;
    [Header("Lightning Dash")]
    public float eCooldown = 2f;
    [HideInInspector] public float eCooldownLeft;
    public float dashTime = 0.2f;
    public float dashForce = 40f;
    [Header("Lethal Attack")]
    public float rCooldown = 18f;
    public float reactivateTime = 5f;
    [HideInInspector] public float reactivateTimeLeft;
    public GameObject kunai;
    public GameObject dashHitbox;
    public float kunaiForce = 40f;
    public float ultimateDashSpeed = 40f;
    [HideInInspector] public float rCooldownLeft;


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
        if (attack == true)
        {
            if (timeBtwAttack <= 0)
            {
                if (Input.GetMouseButton(0))
                {
                    anim.SetTrigger("Attack");
                   

                    Collider2D[] hitZone = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, enemyOnly);
                    if (hitZone.Length == 0){
                        AudioManager.Instance.Play("AssassinAttack");
                    }
                    else{
                        AudioManager.Instance.Play("AssassinAttackHit");
                    }
                    for (int i = 0; i < hitZone.Length; i++)
                    {
                        hitZone[i].GetComponent<Enemy>().TakeDamage(damage);
                        hitZone[i].GetComponent<Enemy>().TakeDamageOverTime(damagePerTick, frequency, numberOfTicks);
                    }
                    timeBtwAttack = 1 / attackSpeed;
                }
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;
            }
        }

        if (qCooldownLeft <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                AudioManager.Instance.Play("Shuriken");
                qCooldownLeft = qCooldown;
                for (float i = -2; i < 3; i++)
                {
                    Shoot(projectileSpreadValue * i, 1f);
                }
            }
        }
        else
        {
            qCooldownLeft -= Time.deltaTime;
        }
        if (eCooldownLeft <= 0)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                AudioManager.Instance.Play("Dash");
                dashTimeLeft = dashTime;
                eCooldownLeft = eCooldown;
                if (dashTimeLeft >= 0)
                {
                    dashTimeLeft -= Time.deltaTime;
                }
            }
        }
        else
        {
            eCooldownLeft -= Time.deltaTime;
        }
        if (rCooldownLeft <= 0)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Kunai(0, 1);
                rCooldownLeft = rCooldown;
                reactivateTimeLeft = reactivateTime;
            }
        }
        else
        {
            rCooldownLeft -= Time.deltaTime;
        }
        if (reactivateTimeLeft >= 0)
        {
            reactivateTimeLeft -= Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.R) && reactivateTimeLeft < reactivateTime - 0.1f)
            {
                StartCoroutine(LethalDash());
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
        if (dashTimeLeft >= 0)
        {
            rb.AddForce(player.lookDir.normalized * dashForce, ForceMode2D.Impulse);
            dashTimeLeft -= Time.fixedDeltaTime;
        }
    }
    void Shoot(float x, float y)
    {
        GameObject go = Instantiate(shuriken, attackPosition.position, attackPosition.rotation);
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        rb.AddForce(attackPosition.TransformVector(x, y, 0) * projectileForce, ForceMode2D.Impulse);
    }
    void Kunai(float x, float y)
    {
        AudioManager.Instance.Play("KunaiThrow");
        GameObject go = Instantiate(kunai, transform.position, transform.rotation);
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        rb.AddForce(attackPosition.TransformVector(x, y, 0) * kunaiForce, ForceMode2D.Impulse);
    }
    private IEnumerator LethalDash()
    {

        Transform kunai = FindObjectOfType<Kunai>().GetComponent<Transform>();
        if (kunai != null)
        {
            AudioManager.Instance.Play("Dash");
            dashHitbox.SetActive(true);
            while (Vector2.Distance(transform.position,kunai.position)>1.5)
            {
                transform.position = Vector2.MoveTowards(transform.position, kunai.position, ultimateDashSpeed * Time.deltaTime);
                yield return null;
            }
            reactivateTimeLeft = -1f;
            dashHitbox.SetActive(false);
        }
    }
}
