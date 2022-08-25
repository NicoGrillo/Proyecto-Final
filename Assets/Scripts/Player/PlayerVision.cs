using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVision : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;

    [SerializeField]
    private float rayDistance = 10f;

    [SerializeField] private GameObject bullet;

    private bool canShoot = true;

    void Start()
    {

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        CannonRaycast();
    }

    private void CannonRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(shootPoint.position, shootPoint.TransformDirection(Vector3.forward), out hit, rayDistance))
        {
            if (hit.transform.CompareTag("Player") && canShoot)
            {
                Debug.Log("COLLISION CON PLAYER");
                Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation);
                canShoot = false;
                Invoke("delayShoot", 1f);
            }
        }
    }

    void delayShoot()
    {
        canShoot = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 direction = shootPoint.TransformDirection(Vector3.forward) * rayDistance;
        Gizmos.DrawRay(shootPoint.position, direction);
        //Gizmos.DrawLine(shootPoint.position, direction); ESTE GIZMO NO AFECTA LA ROTACION
    }
}
