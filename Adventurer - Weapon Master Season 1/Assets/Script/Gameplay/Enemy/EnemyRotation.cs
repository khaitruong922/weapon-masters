using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotation: MonoBehaviour
{
    private Transform target;
    public float stopDistance = 6f;
    public float retreatDistance= 4f;
    public float speed = 4f;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 lookDir = target.position - this.transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg-90f;
        this.transform.rotation = Quaternion.Euler(0f,0f,angle);
        if(target != null){
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
