using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAbility : MonoBehaviour
{
    [Header("Object references")]
    public Rigidbody2D rb;
    public Transform firePoint;
    public GameObject projectile;
    public GameObject shootEffect;
    private Player player;
    [Header("Attack")]
    public float magicForce = 20f;
    public float attackSpeed = 1.5f; //how many attacks player can make in a second
    private float timeBtwAttack;
    public float delay = 0.1f;
    public int maxAttack=4;
    [Header("Black Hole")]
    public GameObject blackHole;
    public float qCooldown = 8f;
    [HideInInspector] public float qCooldownLeft;
    [Header("Shield")]
    public GameObject shield;
    public float eCooldown = 8f;
    [HideInInspector] public float eCooldownLeft;
    public float eDuration = 4f;
    public GameObject lightOrb;

    void Start() 
    {
        player = gameObject.GetComponent<Player>();
    }
    void Update()
    {
        if(timeBtwAttack<=0)
        { 
            if(Input.GetMouseButton(0))
            {
                timeBtwAttack = 1/attackSpeed ; 
                StartCoroutine(MultiShoot(Random.Range(1,maxAttack+1),delay));
            }
        }
        else timeBtwAttack -= Time.deltaTime; 
        if(qCooldownLeft<=0)
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                BlackHole();
                qCooldownLeft = qCooldown; 
            }
        }
        else qCooldownLeft -= Time.deltaTime;
        if(eCooldownLeft<=0)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(Shield(eDuration));
                StartCoroutine(LightOrb(eDuration));
                eCooldownLeft = eCooldown; 
            }
        }
        else eCooldownLeft -= Time.deltaTime;
    }
    void BlackHole(){
        GameObject p = Instantiate(blackHole, firePoint.position,Quaternion.identity);
        Rigidbody2D rb = p.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up* magicForce, ForceMode2D.Impulse);
   
    }
    void Shoot(float x, float y){
        GameObject p = Instantiate(projectile, firePoint.position,firePoint.rotation);
        Rigidbody2D rb = p.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.TransformVector(x,y,0)* magicForce, ForceMode2D.Impulse);
        GameObject effect = Instantiate(shootEffect,firePoint.position,firePoint.rotation);
        Destroy(effect,0.05f);
    
    }
    void LightOrb(float x, float y){
        GameObject p = Instantiate(lightOrb, transform.position+new Vector3(x,y,0).normalized*2.2f,transform.rotation);
        Rigidbody2D rb = p.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.TransformVector(x,y,0)* magicForce, ForceMode2D.Impulse);
    }
    IEnumerator MultiShoot(int shots,float delay){
        
        for (int i=0;i<shots;i++)
        {
            Shoot(0f,1f);
            yield return new WaitForSeconds(delay);
        }
    }
    IEnumerator LightOrb(float duration){
        for (float t=0;t<duration;t+=Time.deltaTime){
            Vector2 dir = new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f)).normalized;
            LightOrb(dir.x,dir.y);
            yield return null;
        }
        
    }
    IEnumerator Shield(float duration){
        shield.SetActive(true);
        yield return new WaitForSeconds(duration);
        shield.SetActive(false);
    }        
}





