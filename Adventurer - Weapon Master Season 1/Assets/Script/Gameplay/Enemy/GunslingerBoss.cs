using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunslingerBoss : MonoBehaviour
{
    [Header("Object references")]
    private Rigidbody2D rb;
    public Transform firePoint;
    public GameObject bullet;
    private EnemyRotation enemy;

    [Header("Attack")]
    public float bulletForce = 20f;
    public float attackSpeed = 2.5f;
    private float timeBtwAttack;
    public int attackNeeded = 3;
    private int attackCounter;
    public float bulletSpreadValue = 0.1f;

    [Header("Ricochet Bullet")]
    public GameObject qBullet;
    public float qCooldown = 8f;
    public int numberOfShots = 10;
    public float delay = 0.1f;
    public int bonus = 4;

    [Header("High Noon")]
    public float eCooldown = 12f;
    public float eDuration = 5f;
    [Header("Laser")]
    public float rCooldown = 30f;
    public float rDuration = 3f;
    public GameObject laser;
    private void Start()
    {
        enemy = GetComponent<EnemyRotation>();
        InvokeRepeating("ShootWithPassive", 1f, 1 / attackSpeed);
        InvokeRepeating("Ricochet", qCooldown, qCooldown);
        InvokeRepeating("HighNoon", eCooldown, eCooldown);
        InvokeRepeating("Laser", rCooldown, rCooldown);
    }
    private void Shoot(float x, float y, GameObject projectile)
    {
        GameObject go = Instantiate(projectile, firePoint.position, transform.rotation);
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.TransformVector(x, y, 0) * bulletForce, ForceMode2D.Impulse);
    }
    private void ShootWithPassive()
    {
        attackCounter += 1;
        if (attackCounter < attackNeeded)
        {
            Shoot(0, 1, bullet);
            AudioManager.Instance.Play("Shoot");
        }
        else
        {
            attackCounter -= attackNeeded;
            for (int i = -2; i < 3; i++)
            {
                Shoot(i * bulletSpreadValue, 1, bullet);
                AudioManager.Instance.Play("TripleShoot");
            }
        }
    }
    private IEnumerator MultiShoot()
    {
        for (int i = 0; i < numberOfShots; i++)
        {
            Shoot(0, 1, qBullet);
            AudioManager.Instance.Play("Shoot");
            yield return new WaitForSeconds(delay);
        }
        numberOfShots += bonus;
    }
    private void Ricochet()
    {
        StartCoroutine(MultiShoot());
    }
    private void HighNoon()
    {
        StartCoroutine(HighNoonCoroutine());
    }
    private void Laser()
    {
        StartCoroutine(LaserCoroutine());

    }
    private IEnumerator HighNoonCoroutine()
    {
        attackSpeed *= 2;
        enemy.speed *= 2;
        yield return new WaitForSeconds(eDuration);
        attackSpeed /= 2;
        enemy.speed /= 2;
    }
    private IEnumerator LaserCoroutine()
    {
        laser.SetActive(true);
        AudioManager.Instance.Play("Laser");
        yield return new WaitForSeconds(rDuration);
        laser.SetActive(false);
    }
}
