using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemyRanged : MonoBehaviour
{
    private Player targetHealth;
    private float timeBtwAttack;
    public Transform[] attackPoints;
    public float attackSpeed = 0.5f;
    public GameObject projectile;
    private int maxShot = 1;
    void Update()
    {
        if (timeBtwAttack <= 0)
        {

            StartCoroutine(Shoot());
            maxShot++;
            timeBtwAttack = 1 / attackSpeed;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }

    }
    private IEnumerator Shoot()
    {

        for (int i = 1; i < maxShot; i++)
        {
            foreach (Transform a in attackPoints)
            {
                Instantiate(projectile, a.position, Quaternion.identity);
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(0.2f / maxShot);
        }
    }
}
