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
    public float attackSpeed = 2f; //how many attacks player can make in a second
    private float timeBtwAttack;
    public float delay = 0.1f;
    public int maxAttack=4;

    void Start() 
    {
        player = gameObject.GetComponent<Player>();
    }
    void Update()
    {
        if(timeBtwAttack<=0)
        { //attack delay
            if(Input.GetMouseButton(0))
            {
                timeBtwAttack = 1/attackSpeed ; //when you shoot, the timer resets
                StartCoroutine(MultiShoot(Random.Range(1,maxAttack+1),delay));
            }
        }
        else timeBtwAttack -= Time.deltaTime;  //attack delay countdown
    }
    void Shoot(){
        GameObject magic = Instantiate(projectile, firePoint.position,firePoint.rotation);
        Rigidbody2D rb = magic.GetComponent<Rigidbody2D>();
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
    }        





