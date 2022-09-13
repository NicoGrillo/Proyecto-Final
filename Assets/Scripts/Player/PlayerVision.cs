using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVision : MonoBehaviour
{
    [SerializeField] private Transform visionPoint;
    [SerializeField] private float rayDistance = 10f;
    [SerializeField][Range(1, 10)] private int timeToHypno = 2;

    private bool canHypno;
    private PlayerMove playerMove;
    private float count;

    void Start()
    {
        canHypno = true;
        count = 0;
        playerMove = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        VisionRaycast();
    }

    private void VisionRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(visionPoint.position, visionPoint.TransformDirection(Vector3.forward), out hit, rayDistance))
        {
            if (hit.transform.CompareTag("HypnoEnemy") && canHypno)
            {
                count += Time.deltaTime;
                if (count >= timeToHypno) //canHypno = true;
                {
                    //HUDManager.Instance.SetSelectedText("Hipnotizado");
                    PlayerEvents.OnStateHypnoCall();
                    Debug.Log(gameObject.name + " llamó al evento OnStateHypno");
                    //playerMove.CantMove = true;
                    canHypno = false;
                    Invoke("delayRecover", 3f);
                }
            }
            else count = 0;
            HUDManager.SetFearBar(count * 10);
        }
    }

    void delayRecover()
    {
        //playerMove.CantMove = false;
        canHypno = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 direction = visionPoint.TransformDirection(Vector3.forward) * rayDistance;
        Gizmos.DrawRay(visionPoint.position, direction);
        //Gizmos.DrawLine(shootPoint.position, direction); ESTE GIZMO NO AFECTA LA ROTACION
    }
}
