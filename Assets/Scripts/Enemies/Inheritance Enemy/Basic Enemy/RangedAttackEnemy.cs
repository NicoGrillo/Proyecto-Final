using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackEnemy : MeleeAttackEnemy
{
    [SerializeField] GameObject enemyAmmoType;
    [SerializeField] GameObject enemyThrowPoint;

    protected override void Attack()
    {
        //base.Attack();
        enemyAnimation.Play("Attack2");
        canAttack = false;
        canMove = false;
        Invoke("CanActionAgain", basicEnemyData.AttackTime);
        Invoke("ammoThrow", 1);
        transform.LookAt(player.transform.position);
    }

    private void ammoThrow()
    {
        Instantiate(enemyAmmoType, enemyThrowPoint.transform.position, transform.rotation);
    }
}
