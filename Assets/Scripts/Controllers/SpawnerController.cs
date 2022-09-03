using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] GameObject spawnGO;
    [SerializeField][Range(1, 30)] int spawnTime;
    [SerializeField][Range(1, 30)] int timePreset;
    [SerializeField] public bool spawnOn;

    private float count;

    void Start()
    {
        count = timePreset;
    }

    void Update()
    {
        count += Time.deltaTime;
        if (count >= spawnTime && spawnOn)
        {
            if (spawnOn) Spawn();
            count = 0;
            spawnOn = false;
        }
    }

    private void Spawn()
    {
        Instantiate(spawnGO, transform.position, transform.rotation);
    }
}
