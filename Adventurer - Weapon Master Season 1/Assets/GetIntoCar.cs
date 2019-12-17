using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetIntoCar : MonoBehaviour
{
private Transform cart;
private Transform player;
void Start(){
    player = FindObjectOfType<Player>().GetComponent<Transform>();
    cart = GetComponent<Transform>();
}
void Update(){
    cart.position = player.position + new Vector3(0,-1,0);
}
}
