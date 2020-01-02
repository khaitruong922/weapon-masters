using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinBoss : MonoBehaviour
{
    [Header("Object references")]
    private Rigidbody2D rb;
    private EnemyRotation enemy;
    private SpriteRenderer spriteRenderer;
    public LayerMask playerOnly;

    [Header("Attack")]
    public Transform attackPosition;
    public float damage = 60f;
    public float attackRadius = 1.2f;
    public float attackSpeed = 2.5f;
    [Header("Bleeding Edge")]
    public float damagePerTick=10f;
    public int numberOfTicks=4;
    public float frequency=0.5f;


    [Header("Arc Stars")]
    public GameObject shuriken;
    public float qCooldown = 5f;
    public float projectileSpreadVaule = 0.1f;
    public float projectileForce = 30f;

    [Header("Dash")]
    public float eCooldown = 5f;
    public float dashSpeed = 30f;
    public float dashTime = 0.2f;
    private float dashTimeLeft;
    [Header("Lethal Attack")]
    public GameObject kunai;
    public GameObject dashHitbox;
    public float rCooldown = 22f;
    private Transform player;

    [Header("Clone")]
    public GameObject clone;

    private void Start()
    {
        player = FindObjectOfType<Player>().GetComponent<Transform>();
        enemy = GetComponent<EnemyRotation>();
        InvokeRepeating("Attack", 1f, 1 / attackSpeed);
        InvokeRepeating("BlackHole", qCooldown, qCooldown);
        InvokeRepeating("Shield", eCooldown, eCooldown);
        InvokeRepeating("Explosion", rCooldown, rCooldown);
    }
    private void Shoot(float x, float y, GameObject projectile)
    {
        GameObject go = Instantiate(projectile, attackPosition.position, transform.rotation);
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        rb.AddForce(attackPosition.TransformVector(x, y, 0) * projectileForce, ForceMode2D.Impulse);
    }
    private IEnumerator MultiShoot()
    {
        for (int i = 0; i < Random.Range(1, 4); i++)
        {
            Shoot(0, 1, shuriken);
            AudioManager.Instance.Play("MageAttack");
            yield return new WaitForSeconds(1);
        }
    }
    private void Attack()
    {
        Collider2D[] hitZone = Physics2D.OverlapCircleAll(attackPosition.position, attackRadius, playerOnly);
        for (int i = 0; i < hitZone.Length; i++)
        {
            hitZone[i].GetComponent<Player>().TakeDamage(damage);
            hitZone[i].GetComponent<Player>().TakeDamageOverTime(damagePerTick, frequency, numberOfTicks);
        }
    }
    private IEnumerator ShieldCoroutine()
    {
        dashHitbox.SetActive(true);
        AudioManager.Instance.Play("Laser");
        yield return new WaitForSeconds(1);
        dashHitbox.SetActive(false);
    }
}
