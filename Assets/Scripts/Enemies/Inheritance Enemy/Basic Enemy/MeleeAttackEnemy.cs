using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackEnemy : BasicEnemy
{
    private Vector3 playerPosition;

    private float distanceToPlayer;
    private float xValue = -1;

    private bool inSight = false;
    protected bool canAttack = true;

    protected override void Update()
    {
        base.Update();
        DistanceToPlayer();

        if (distanceToPlayer < basicEnemyData.ChaseMaxRange && inSight) Chase();
    }

    private void Chase()
    {
        if (distanceToPlayer < basicEnemyData.MinToPlayer && canAttack) Attack();   //Si está muy cerca, Enemy se detiene a golpear
        if (canMove) LookAndChasePlayer();
    }

    protected virtual void Attack()
    {
        enemyAnimation.Play("Attack1");
        canAttack = false;
        canMove = false;
        Invoke("CanActionAgain", basicEnemyData.AttackTime);
    }

    private void LookAndChasePlayer()
    {
        transform.LookAt(player.transform.position);
        enemyAnimation.Play("Run");
        transform.Translate(Vector3.forward * basicEnemyData.ChaseSpeed * Time.deltaTime);
    }

    private void DistanceToPlayer()
    {
        distanceToPlayer = playerPosition.magnitude;
        if (distanceToPlayer < 0) distanceToPlayer = distanceToPlayer * -1;             //Seteo Distancia como numero positivo

        RaycastHit seePlayer;
        if (Physics.Raycast(new Vector3(transform.position.x, 1, transform.position.z),
                transform.TransformDirection(viewRange()),
                    out seePlayer,
                        basicEnemyData.ChaseMaxRange,
                            ~(1 << 6)))
        {
            if (seePlayer.transform.CompareTag("Player")) inSight = true;
        }
        if (distanceToPlayer > basicEnemyData.ChaseMaxRange) inSight = false;
    }

    private Vector3 viewRange()
    {
        Vector3 value;
        xValue += .1f;
        value = new Vector3(xValue, 0, 1);
        if (xValue > 1) xValue = -1f;

        return value;
    }

    protected override void PositionsUpdate()
    {
        base.PositionsUpdate();
        playerPosition = player.transform.position - transform.position;
    }

    protected override void CanActionAgain()
    {
        base.CanActionAgain();
        canAttack = true;
    }

    protected override void Patrol()
    {
        if (!inSight) base.Patrol();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 direction = transform.TransformDirection(viewRange()) * basicEnemyData.ChaseMaxRange;
        Gizmos.DrawRay(transform.position, direction);

    }
}
