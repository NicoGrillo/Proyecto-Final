using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRanged : AI
{
    [Header("Ammo")]
    [SerializeField] GameObject enemyAmmoType;
    [SerializeField] GameObject enemyThrowPoint;


    private bool inPoint1 = true;
    private bool alreadyKnocked = false;

    protected override void Update()
    {
        base.Update();
        if (alreadyKnocked && distanceToPlayer <= 2)
        {
            alreadyKnocked = false;
            Invoke("WakeUp", 2f);
        }
    }

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
            PointToPatrol = transform.position + new Vector3(distanceToPatrol, 0, 0);
            inPoint1 = false;
        }
        else
        {
            PointToPatrol = transform.position + new Vector3(-distanceToPatrol, 0, 0);
            inPoint1 = true;
        }
    }

    protected override void RockHit()
    {
        base.RockHit();
        if (!alreadyKnocked)
        {
            canMove = false;
            canAttack = false;
            CancelInvoke();
            enemyAnimation.Play("Death");
            alreadyKnocked = true;
            Destroy(Instantiate(enemyKnockedSound, transform.position, Quaternion.identity), 1f);
        }
    }

    private void WakeUp()
    {
        canMove = true;
        canAttack = true;
    }
}
