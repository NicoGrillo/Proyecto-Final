using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHypno : AI
{
    private Vector3 initialPosition;

    protected override void Start()
    {
        base.Start();
        initialPosition = transform.position;
    }

    protected override void Attack()
    {
        enemyAnimation.Play("Idle");
        canMove = false;
        canAttack = false;
        transform.LookAt(player.transform.position);
        Invoke("resetAttack", 2f);
    }

    protected override void PatrolType()
    {
        PointToPatrol = initialPosition;
    }
}
