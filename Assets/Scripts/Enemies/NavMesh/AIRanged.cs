using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRanged : AI
{
    [SerializeField] GameObject enemyAmmoType;
    [SerializeField] GameObject enemyThrowPoint;

    private bool inPoint1 = true;

    protected override void Attack()
    {
        enemyAnimation.Play("Attack2");
        canMove = false;
        canAttack = false;
        transform.LookAt(player.transform.position);
        Invoke("resetAttack", 2f);
        Invoke("ammoThrow", 1);
    }

    private void ammoThrow()
    {
        Instantiate(enemyAmmoType, enemyThrowPoint.transform.position, transform.rotation);
    }

    protected override void PatrolType()
    {
        if (inPoint1)
        {
            PointToPatrol = transform.localPosition + new Vector3(distanceToPatrol, 0, 0);

            inPoint1 = false;
        }
        else
        {
            PointToPatrol = transform.localPosition + new Vector3(-distanceToPatrol, 0, 0);
            inPoint1 = true;
        }
    }
}
