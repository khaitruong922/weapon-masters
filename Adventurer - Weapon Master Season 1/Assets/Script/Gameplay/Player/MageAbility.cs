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
    [Header("Shield")]
    public GameObject shield;
    public float eCooldown = 8f;
    [HideInInspector] public float eCooldownLeft;
    public float eDuration = 3f;

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
        if(eCooldownLeft<=0)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(Shield(eDuration));
                eCooldownLeft = eCooldown; 
            }
        }
        else eCooldownLeft -= Time.deltaTime;  
    }
    void Shoot(){
        GameObject p = Instantiate(projectile, firePoint.position,firePoint.rotation);
        Rigidbody2D rb = p.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * magicForce, ForceMode2D.Impulse);
        GameObject effect = Instantiate(shootEffect,firePoint.position,firePoint.rotation);
        Destroy(effect,0.05f);
    
    }
    IEnumerator MultiShoot(int shots,float delay){
        
        for (int i=0;i<shots;i++)
        {
            Shoot();
            yield return new WaitForSeconds(delay);
        }
    }
    IEnumerator Shield(float duration){
        shield.SetActive(true);
        yield return new WaitForSeconds(duration);
        shield.SetActive(false);
    }        
}





