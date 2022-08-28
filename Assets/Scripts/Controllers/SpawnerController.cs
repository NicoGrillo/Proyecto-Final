using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] GameObject spawnGO;
    [SerializeField][Range(1, 10)] int spawnTime;
    [SerializeField][Range(1, 10)] int spawnDelay;
    [SerializeField] bool spawnOn;

    private float count;
    
    void Start()
    {
        count = spawnDelay;
    }
    
    void Update()
    {
        count += Time.deltaTime;
        if (count >= spawnTime && spawnOn)
        {
            if (spawnOn) Spawn();
            count = 0;
        }
    }

    private void Spawn()
    {
        Instantiate(spawnGO, transform.position, transform.rotation);
    }
}
