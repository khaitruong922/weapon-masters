using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart: MonoBehaviour
{
    public float cartSpeed=1f;
    private Vector2 cartPosition;

    void Start() 
    {
        cartPosition = transform.position;
    }

    void OnTriggerStay2D (Collider2D other)
    {
        if(other.gameObject.tag == "Player"){
        cartPosition.x += cartSpeed*Time.deltaTime;
        transform.position = cartPosition;
    }
    }
}
