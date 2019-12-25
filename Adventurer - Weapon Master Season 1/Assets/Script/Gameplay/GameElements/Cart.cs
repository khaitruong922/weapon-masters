using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cart: MonoBehaviour
{
    public float cartSpeed=1f;
    public Transform winPoint;
    private float maxDistance;
    [HideInInspector] public float currentProgress;
    void Start(){
        maxDistance = Vector2.Distance(transform.position,winPoint.position);
    }

    void OnTriggerStay2D (Collider2D other)
    {
        if(other.gameObject.tag == "Player"){
        transform.position = Vector2.MoveTowards(transform.position, winPoint.position, cartSpeed * Time.deltaTime);
        currentProgress=(maxDistance - Vector2.Distance(transform.position,winPoint.position))/maxDistance;
        if(Vector2.Distance(transform.position,winPoint.position) == 0)
        GameHandler.Instance.Win();
    }
    }
}

