using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    public GameObject audioManager;

    private void Awake()
    {
        if (PersistentData.Instance == null)
        {
            PersistentData Instance = this.gameObject.AddComponent<PersistentData>();
        }
        if (AudioManager.Instance == null)
        {
            Instantiate(audioManager, transform.position, Quaternion.identity);
        }
    }
}
