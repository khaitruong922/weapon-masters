using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageVoidling : MonoBehaviour
{
    public float range = 40f;
    public float timeBtwCast = 7f;
    private float timeforNextCast;

    private Transform target;
    public GameObject spell;
    public float accuracy = 2f;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<Player>().GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && Vector2.Distance(transform.position, target.position) <= range)
        {
            if (timeforNextCast <= 0)
            {
                Instantiate(spell, target.position + new Vector3(Random.Range(-accuracy, accuracy), Random.Range(-accuracy, accuracy), 0), Quaternion.identity);
                timeforNextCast = timeBtwCast;
            }
            else
            {
                timeforNextCast -= Time.deltaTime;
            }
        }
    }
}
