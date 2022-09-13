using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrollRanged : MeleeAttackEnemy
{
    [SerializeField] GameObject enemyAmmoType;
    [SerializeField] GameObject enemyThrowPoint;

    private void Awake()
    {
        transform.localScale = new Vector3(1f, 1f, 1f) * 0.75f;
        player = GameObject.Find("Player");
    }

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

    protected override void SetColliders(bool value)
    {
        transform.GetComponent<CapsuleCollider>().enabled = value;
    }
}