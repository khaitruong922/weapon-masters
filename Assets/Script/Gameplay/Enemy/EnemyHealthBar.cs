using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    private Transform bar;
    public Enemy enemy;
    void Start()
    {
        bar = transform.Find("Bar");
    }

    public void SetSize(float size)
    {
        bar.localScale = new Vector3(size, 1f);
    }
}

