using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerDetecter : MonoBehaviour
{
    [SerializeField] GameObject spawnPoint;
    [SerializeField] private float rayDistance = 10f;

    private bool canSpawn = true;

    private void FixedUpdate()
    {
        SpawnRaycast();
    }

    private void SpawnRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayDistance))
        {
            if (hit.transform.CompareTag("Player") && canSpawn) spawnPoint.GetComponent<SpawnerController>().spawnOn = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * rayDistance;
        Gizmos.DrawRay(transform.position, direction);
    }
}
