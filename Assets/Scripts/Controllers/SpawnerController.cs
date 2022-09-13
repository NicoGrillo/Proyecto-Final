using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] GameObject spawnGO;
    [SerializeField][Range(1, 600)] int spawnTime;
    [SerializeField][Range(0, 30)] int timePreset;
    [SerializeField] public bool spawnOn;

    private float count;

    void Start()
    {
        count = timePreset;
    }

    void Update()
    {
        itsActivated();
    }

    private void itsActivated()
    {
        if (spawnOn)
        {
            count += Time.deltaTime;
            if (count >= spawnTime)
            {
                Spawn();
                count = 0;
                spawnOn = false;
            }
        }
    }

    private void Spawn()
    {
        Instantiate(spawnGO, transform.position, transform.rotation);
    }
}
