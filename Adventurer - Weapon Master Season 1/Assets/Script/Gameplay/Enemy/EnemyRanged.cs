using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged: MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    private Player targetHealth;
    private float timeBtwAttack;
    public float attackSpeed=1f;
    public GameObject projectile;
    void Update(){
        if (timeBtwAttack <= 0)
                {
                    Instantiate(projectile,transform.position,Quaternion.identity);
                    timeBtwAttack = 1 / attackSpeed;
                }
                else
                {
                    timeBtwAttack -= Time.deltaTime;
                }
            }   
}


