using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHypno : AI
{
    private Vector3 initialPosition;
    private bool isScreaming = false;

    protected override void Start()
    {
        base.Start();
        initialPosition = transform.position;
        soundAttackTime = 2.5f;
    }

    protected override void Attack()
    {
        enemyAnimation.Play("Idle");
        transform.LookAt(player.transform.position);
        if (!isScreaming) StartCoroutine(Screaming());
    }

    protected override void PatrolType()
    {
        PointToPatrol = initialPosition;
    }

    IEnumerator Screaming()
    {
        isScreaming = true;
        Destroy(Instantiate(enemyAttack, transform.position, Quaternion.identity), soundAttackTime);
        yield return new WaitForSeconds(5);
        isScreaming = false;
    }
}
