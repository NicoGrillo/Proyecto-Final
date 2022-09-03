using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerDetecter : MonoBehaviour
{
    [SerializeField] GameObject spawnPoint;
    [SerializeField] private float rayDistance = 10f;

    private bool canSpawn = true;

    void Start()
    {

    }

    private void FixedUpdate()
    {
        SpawnRaycast();
    }

    private void SpawnRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayDistance))
        {
            if (hit.transform.CompareTag("Player") && canSpawn)
            {
                //HUDManager.Instance.SetSelectedText("Bonus activado: Baterias extras");
                spawnPoint.GetComponent<SpawnerController>().spawnOn = true;
                //spawnPoint.SetActive(true);
                //canSpawn = false;
                //Invoke("DesactivatedSpawn", 1);
            }
        }
    }

    private void DesactivatedSpawn()
    {
        spawnPoint.SetActive(false);
        canSpawn = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * rayDistance;
        Gizmos.DrawRay(transform.position, direction);
        //Gizmos.DrawLine(shootPoint.position, direction); ESTE GIZMO NO AFECTA LA ROTACION
    }
}
