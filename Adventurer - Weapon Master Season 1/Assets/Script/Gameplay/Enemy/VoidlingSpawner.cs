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
    void Awake(){
        Enemy.count ++;
    }
    void Start(){
        InvokeRepeating("SpawnVoidlings",0f, spawnRate);
        spawnDurationLeft=spawnDuration;
        Invoke("SelfDestroy",spawnDuration);
    }
    void SpawnVoidlings(){
        Instantiate(voidlings,transform.position,Quaternion.identity);
    }
    void SelfDestroy(){
        Destroy(gameObject);
        Enemy.count--;
    }
}
