using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageVoidling : MonoBehaviour
{
    public float range = 10f;
    public float timeBtwCast=6f;
    private float timeforNextCast;

    private Transform target;
    public GameObject spell; 

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<Player>().GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null && Vector2.Distance(transform.position, target.position) <= range)
        {
            if(timeforNextCast<=0)
            {
                Instantiate(spell, target.position,Quaternion.identity);
                timeforNextCast=timeBtwCast;
            }
            else 
            {
                timeforNextCast -= Time.deltaTime;
            }
        }
    } 
} 
