using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform target;
    public float speed = 1f;
    public float stopDistance = 3f;
    public float retreatDistance = 2f;
    void Start()
    {
        target= GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {   if(target != null){
        if (Vector2.Distance(transform.position,target.position)>stopDistance){
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }   else if(Vector2.Distance(transform.position,target.position)<stopDistance && Vector2.Distance(transform.position,target.position)>retreatDistance){
        transform.position=this.transform.position;
    }   else if (Vector2.Distance(transform.position,target.position)<retreatDistance){
        transform.position = Vector2.MoveTowards(transform.position, target.position, -speed * Time.deltaTime);
    }
    }
}
}
