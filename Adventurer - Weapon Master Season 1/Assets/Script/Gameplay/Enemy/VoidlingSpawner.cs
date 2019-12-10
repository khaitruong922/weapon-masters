using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidlingSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject voidlings;
    public float spawnDuration=15f;
    private float spawnDurationLeft;
    public float spawnRate = 1f;
    void Start(){
        InvokeRepeating("SpawnVoidlings",0f, spawnRate);
        spawnDurationLeft=spawnDuration;
    }
    void SpawnVoidlings(){
        Instantiate(voidlings,transform.position,Quaternion.identity);
}
   void Update(){
    spawnDurationLeft-= Time.deltaTime;
    if(spawnDurationLeft<0){
        CancelInvoke("SpawnVoidlings");
        Destroy(gameObject);
    }
}
}
