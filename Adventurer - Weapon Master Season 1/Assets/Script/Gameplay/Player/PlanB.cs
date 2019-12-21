using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanB : MonoBehaviour
{
    private float timeBtwAttack;
    public float attackSpeed = 0.5f;
    public float projectileForce = 20f;

    private Rigidbody2D rb;
    public GameObject projectile;
    public Transform attackPosition;

    private GameObject target;
    private Transform player;
    private float eAmmoLeft;
    // Start is called before the first frame update
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        target = GameObject.FindWithTag("Player");
        eAmmoLeft = target.GetComponent<AssassinAbility>().eAmmoLeft;

        player = FindObjectOfType<Player>().GetComponent<Transform>();    
    }

    // Update is called once per frame
    void Update()
    {
        if(timeBtwAttack <= 0)
        {
            if(Input.GetMouseButton(0))
            {
                Kunai(0f,1f);
                timeBtwAttack = 1/attackSpeed;
                eAmmoLeft -= 1;
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    void Kunai(float x,float y)
    {
        GameObject bullet = Instantiate(projectile, attackPosition.position,attackPosition.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(attackPosition.TransformVector(x,y,0) * projectileForce, ForceMode2D.Impulse);  
    }
}
