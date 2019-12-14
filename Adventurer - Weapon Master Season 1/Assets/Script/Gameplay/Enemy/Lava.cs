using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public float damagePerSec = 20f;
    public float delay = 1f;
    public float duration = 4f;
    private void Start(){
        Destroy(gameObject,duration);
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Player")){
            other.GetComponent<Player>().TakeDamage(damagePerSec*Time.deltaTime);
        }
    } 
}
