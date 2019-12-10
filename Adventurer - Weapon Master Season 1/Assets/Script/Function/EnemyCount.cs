using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCount : MonoBehaviour
{
    public GameObject block;
    void Awake() {
        Enemy.count = 0;
        block.SetActive(false);
    }
    void Update(){
        if(Enemy.count > 0){
            block.SetActive(true);
        }
        else{
            block.SetActive(false);
        }
    }
}
