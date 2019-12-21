using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    public GameObject impactEffect;
    public float damage= 50f;
    public float returnSpeed = 50f;
    public float projectileForce = 20f;
    
    private GameObject target;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player");
        player = FindObjectOfType<Player>().GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {  
        if(AssassinAbility.eAmmoLeft <= 0)
        {
            transform.position = Vector2.MoveTowards(transform.position,player.position,returnSpeed*Time.deltaTime);
            if (transform.position == player.position)
            {
                Destroy(gameObject);
            }
        }
    }
}
