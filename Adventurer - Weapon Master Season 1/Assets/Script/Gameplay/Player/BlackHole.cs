using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public float delay =1f;
    public float sizeIncrease = 5f;
    private void Start() {
        StartCoroutine(Activate(delay));
    }
    
    private IEnumerator Activate(float delay){
        float t = 0;
        while (t<=1f)
        {
            t += Time.deltaTime / delay;
            transform.localScale = new Vector3(1+t*sizeIncrease,1+t*sizeIncrease,1);
            yield return null;          
        }
    }
}
