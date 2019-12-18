using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunslingerAbility : MonoBehaviour
{
    [Header("Object references")]
    private Rigidbody2D rb;
    public Transform firePoint;
    public GameObject projectile;
    public GameObject shootEffect;
    public GameObject qBullet;
    private Player player;
    
    [Header("Attack")]
    public float bulletForce = 20f;
    public float attackSpeed = 3f; //how many attacks player can make in a second
    private float timeBtwAttack;
    public int attackNeeded = 3;
    private int attackCounter;
    public float bulletSpreadValue = 0.2f;

    [Header("Ricochet Bullet")]
    public float qCooldown = 8f;
    [HideInInspector] public float qCooldownLeft;
    public int numberOfShots = 4;
    public float delay = 0.1f;
    
    [Header("High Noon")]
    public float eCooldown = 12f;
    [HideInInspector] public float eCooldownLeft;
    public float eDuration = 5f;
    public float multiplier=2f;
    public float dashForce=50f;
    public float dashTime=0.2f;
    private float dashTimeLeft;
    [Header("Laser")]
    public float rCooldown = 30f;
    [HideInInspector] public float rCooldownLeft;
    public float rDuration = 3f;
    public GameObject laser;
    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }
    void Update()
    {
        if(timeBtwAttack<=0)
        {
            if(Input.GetMouseButton(0))
            {
                attackCounter+=1;
                timeBtwAttack = 1/attackSpeed ; 
                if (attackCounter < attackNeeded)
                {
                    Shoot(0f,1f);
                }
                else //passive: every 3 attacks, gunslinger will shoot 3 bullets in a cone.
                {
                    attackCounter = 0;
                    Shoot(0f,1f);
                    Shoot(bulletSpreadValue,1f);
                    Shoot(-bulletSpreadValue,1f);
                }
            }
        }
        else timeBtwAttack -= Time.deltaTime;  
            

        if(eCooldownLeft<=0)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                eCooldownLeft = eCooldown; 
                StartCoroutine("HighNoon",eDuration);
                dashTimeLeft = dashTime;
                if(dashTimeLeft>=0){
                    dashTimeLeft -= Time.deltaTime;
                }
            }
        }
        else eCooldownLeft -= Time.deltaTime;

        if(qCooldownLeft<=0)
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                StartCoroutine(MultiShoot(numberOfShots,delay));
                qCooldownLeft = qCooldown; 
            }
        }
        else qCooldownLeft -= Time.deltaTime; 
        if(rCooldownLeft<=0)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(Laser(rDuration));
                rCooldownLeft = rCooldown; 
            }
        }
        else rCooldownLeft -= Time.deltaTime; 
    }
    
    private void FixedUpdate() {
        if(dashTimeLeft>=0){
        rb.AddForce(-player.lookDir.normalized * dashForce,ForceMode2D.Impulse);
        dashTimeLeft -= Time.fixedDeltaTime;
        }
    }
      
    void Shoot(float x,float y){
        GameObject bullet = Instantiate(projectile, firePoint.position,firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.TransformVector(x,y,0) * bulletForce, ForceMode2D.Impulse);
        GameObject effect = Instantiate(shootEffect,firePoint.position,firePoint.rotation);
        Destroy(effect,0.05f);
    }
    void Ricochet(float x,float y){
        GameObject bullet = Instantiate(qBullet, firePoint.position,firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.TransformVector(x,y,0) * bulletForce, ForceMode2D.Impulse);
        GameObject effect = Instantiate(shootEffect,firePoint.position,firePoint.rotation);
        Destroy(effect,0.05f);
    }
    IEnumerator HighNoon(float duration)
    {   
        attackSpeed *= multiplier;
        player.speed *= multiplier;
        yield return new WaitForSeconds(duration); 
        attackSpeed /= multiplier;
        player.speed /= multiplier;
    }
    IEnumerator MultiShoot(int shots,float delay){
        
        for (int i=0;i<shots;i++)
        {
            Ricochet(0f,1f);
            yield return new WaitForSeconds(delay);
        }
    }
    IEnumerator Laser(float duration){
        laser.SetActive(true);
        yield return new WaitForSeconds(duration);
        laser.SetActive(false);
    }
    }