using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public float damagePerSec = 20f;
    public float delay = 1f;
    public float duration = 4f;
    private SpriteRenderer sprite;
    private bool activated = false;
    private void Start(){
        Destroy(gameObject,duration+delay);
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(Activate(delay));
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Player")&& activated == true){
            other.GetComponent<Player>().TakeDamage(damagePerSec*Time.deltaTime);
        }
    } 
    private IEnumerator Activate(float delay){
        float t = 0;
        while (t<=1f)
        {
            t += Time.deltaTime / delay;
            sprite.color = new Color(1f,1f,1f,t);
            transform.localScale = new Vector3(t,t,t);
            yield return null;          
        }
        //yield return new WaitForSeconds(delay);
        activated = true;
    }
}
