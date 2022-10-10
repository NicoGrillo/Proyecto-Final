using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBoss : AI
{
    [Header("Invoking")]
    [SerializeField][Range(15, 120)] float invokeTime = 60;
    [SerializeField] GameObject enemyToInvoke;
    [SerializeField] GameObject enemyInvokePoint;

    bool invoking = true;

    protected override void Update()
    {
        if (!invoking && canMove) base.Update();
        else if (invoking) StartCoroutine(CastEnemies());
    }

    IEnumerator CastEnemies()
    {
        invoking = false;
        yield return new WaitForSeconds(invokeTime);
        canMove = false;
        enemyAnimation.Play("Attack2");
        Instantiate(enemyToInvoke, enemyInvokePoint.transform.position, enemyInvokePoint.transform.rotation);
        yield return new WaitForSeconds(2);
        enemyAnimation.Play("Attack2");
        Instantiate(enemyToInvoke, enemyInvokePoint.transform.position, enemyInvokePoint.transform.rotation);
        yield return new WaitForSeconds(2);
        invoking = true;
        canMove = true;
    }
}
