using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVision : MonoBehaviour
{
    [SerializeField] private Transform visionPoint;
    [SerializeField] private float rayDistance = 10f;

    private PlayerMove playerMove;
    private PlayerData playerData;
    private bool canHypno;
    //private float count=0;
    private float xValue = -1f;

    void Start()
    {
        canHypno = true;
        playerMove = GetComponent<PlayerMove>();
        playerData = GetComponent<PlayerData>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        VisionRaycast();
    }

    private void VisionRaycast()
    {
        RaycastHit hit;
        //if (Physics.Raycast(visionPoint.position, visionPoint.TransformDirection(Vector3.forward), out hit, rayDistance))
        if (Physics.Raycast(visionPoint.position, transform.TransformDirection(viewRange()), out hit, rayDistance))
        {
            if (hit.transform.CompareTag("HypnoEnemy") && canHypno)
            {
                playerData.FearLVL++;
                HUDManager.SetFearBar((playerData.FearLVL / 5) * 20);
                if (playerData.FearLVL >= 5)
                {
                    PlayerEvents.OnStateHypnoCall();
                    canHypno = false;
                    Invoke("delayRecover", 6);
                }
            }
        }
    }

    private Vector3 viewRange()
    {
        Vector3 value;
        xValue += .1f;
        value = new Vector3(xValue, 0, 1);
        if (xValue > .5) xValue = -.5f;
        return value;
    }

    void delayRecover()
    {
        canHypno = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 direction = visionPoint.TransformDirection(viewRange()) * rayDistance;
        Gizmos.DrawRay(visionPoint.position, direction);
    }
}
