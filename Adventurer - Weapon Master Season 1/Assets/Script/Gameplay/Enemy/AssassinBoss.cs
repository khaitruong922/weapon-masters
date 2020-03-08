using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinBoss : MonoBehaviour
{
    [Header("Object references")]
    private Rigidbody2D rb;
    private Animator anim;
    private EnemyRotation enemy;
    
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
    [Header("Invisible")]
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer[] healthBarSprites;
    private Transform healthBar;
    public float invisbleDuration = 5f;
    public float invisibleStartTime = 8f;
    public float invisbleCooldown = 10f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        healthBarSprites = GetComponentsInChildren<SpriteRenderer>();
        healthBar = transform.Find("EnemyHealthBar");
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>().GetComponent<Transform>();
        enemy = GetComponent<EnemyRotation>();
        InvokeRepeating("Attack", 1f, 1 / attackSpeed);
        InvokeRepeating("SpreadProjectile", qCooldown, qCooldown);
        InvokeRepeating("Dash", eCooldown, eCooldown);
        InvokeRepeating("LethalAttack", rCooldown, rCooldown);
        InvokeRepeating("InvisibleCoroutineCaller",invisibleStartTime,invisbleCooldown);
    }
    private void Shoot(float x, float y, GameObject projectile)
    {
        GameObject go = Instantiate(projectile, attackPosition.position, transform.rotation);
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        rb.AddForce(attackPosition.TransformVector(x, y, 0) * projectileForce, ForceMode2D.Impulse);
    }
    private void Dash(){
        AudioManager.Instance.Play("Dash");
        dashTimeLeft=dashTime;
    }
    private void InvisibleCoroutineCaller(){
        StartCoroutine(InvisibleCoroutine());
    }
    private IEnumerator InvisibleCoroutine(){
        Invisible();
        yield return new WaitForSeconds(invisbleDuration);
        Visible();
    }
    private void Invisible(){
        spriteRenderer.enabled = false;
        foreach(SpriteRenderer sr in healthBarSprites){
            sr.enabled = false;
        }
    }
    private void Visible(){
        spriteRenderer.enabled = true;
        foreach(SpriteRenderer sr in healthBarSprites){
            sr.enabled = true;
        }
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
        AudioManager.Instance.Play("Shuriken");
        for (int i = -4; i < 5; i++)
        {
            Shoot(i*projectileSpreadValue, 1, shuriken);
            Shoot(i*projectileSpreadValue, -1, shuriken);
        }
    }
    private void LethalAttack(){
        StartCoroutine(LethalAttackCoroutine());
    }
    private IEnumerator LethalAttackCoroutine(){
        Shoot(0,2,kunai);
        AudioManager.Instance.Play("KunaiThrow");
        Transform destination = FindObjectOfType<BossKunai>().GetComponent<Transform>();
        yield return new WaitForSeconds(0.75f);
        while (Vector2.Distance(transform.position,destination.position)>1.5){
            dashHitbox.SetActive(true);
            DashToKunai(destination);
            yield return null;
        }
        dashHitbox.SetActive(false);
        
    }
    private void DashToKunai(Transform destination){
        AudioManager.Instance.Play("Dash");
        transform.position = Vector2.MoveTowards(transform.position,destination.position,ultDashSpeed*Time.deltaTime);
    }
    private void Attack()
    {
        Collider2D[] hitZone = Physics2D.OverlapCircleAll(attackPosition.position, attackRadius, playerOnly);
        for (int i = 0; i < hitZone.Length; i++)
        {
            anim.SetTrigger("Attack");
            AudioManager.Instance.Play("AssassinAttackHit");
            hitZone[i].GetComponent<Player>().TakeDamage(damage);
            hitZone[i].GetComponent<Player>().TakeDamageOverTime(damagePerTick, frequency, numberOfTicks);
        }
    }
}
