using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageBoss : MonoBehaviour
{
    [Header("Object references")]
    private Rigidbody2D rb;
    public Transform firePoint;
    public GameObject bullet;
    private EnemyRotation enemy;

    [Header("Attack")]
    public float bulletForce = 20f;
    public float attackSpeed = 2.5f;

    [Header("Black Hole")]
    public GameObject blackHole;
    public float qCooldown = 8f;
    public float delay = 0.1f;

    [Header("Shield")]
    public GameObject shield;
    public GameObject[] eProjectile;
    public float blackHoleProbability = 0.3f;
    public float eCooldown = 14f;
    public float eDuration = 4f;
    [Header("Explosion")]
    public GameObject explosion;
    public float rCooldown = 22f;
    private Transform player;

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
        GameObject go = Instantiate(projectile, firePoint.position, transform.rotation);
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.TransformVector(x, y, 0) * bulletForce, ForceMode2D.Impulse);
    }
    private IEnumerator MultiShoot()
    {
        for (int i = 0; i < Random.Range(1,4); i++)
        {
            Shoot(0, 1, bullet);
            AudioManager.Instance.Play("MageAttack");
            yield return new WaitForSeconds(delay);
        }
    }
    private void Attack()
    {
        StartCoroutine(MultiShoot());
    }
    private void BlackHole()
    {
        Shoot(0,1,blackHole);
    }
    private void Shield()
    {
        StartCoroutine(ShieldCoroutine());
        StartCoroutine(LightOrbCoroutine());
    }
    private IEnumerator LightOrbCoroutine()
    {
        for (float t = 0; t < eDuration; t += Time.deltaTime)
        {
            Vector2 dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            LightOrb(dir.x, dir.y);
            yield return null;
        }
    }
    private GameObject RandomProjectile(){
        if(Random.Range(0f,1f)<=blackHoleProbability){
            return eProjectile[0];
        }
        else return eProjectile[1];
    }
    void Explosion(){
        AudioManager.Instance.Play("Explosion");
        GameObject go = Instantiate(explosion,player.position,Quaternion.identity);
        Destroy(go,1.1f);
    }
    void LightOrb(float x, float y)
    {
        GameObject p = Instantiate(RandomProjectile(), transform.position + new Vector3(x, y, 0).normalized * 2.2f, transform.rotation);
        Rigidbody2D rb = p.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.TransformVector(x, y, 0) * bulletForce, ForceMode2D.Impulse);
    }
    private IEnumerator ShieldCoroutine()
    {
        shield.SetActive(true);
        AudioManager.Instance.Play("Laser");
        yield return new WaitForSeconds(eDuration);
        shield.SetActive(false);
    }
}
