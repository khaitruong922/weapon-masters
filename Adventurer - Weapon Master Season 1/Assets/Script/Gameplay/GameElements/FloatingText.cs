using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float destroyTime = 1f;
    public int sortingLayer = 60;
    private MeshRenderer meshRenderer;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.sortingOrder = sortingLayer;
        Destroy(gameObject,destroyTime);
        transform.localPosition+= new Vector3(Random.Range(-2f,2f),Random.Range(-2f,2f),0);
    }
}
