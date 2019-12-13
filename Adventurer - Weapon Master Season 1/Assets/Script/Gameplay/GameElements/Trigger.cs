using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    private GameObject objects;
    void Awake(){
        foreach(Transform child in transform) {
        child.gameObject.SetActive(false);
    }
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            Collider2D collider = GetComponent<Collider2D>();
            collider.enabled = false;
            foreach(Transform child in transform) {
            child.gameObject.SetActive(true);
        }  
    }
}
}
