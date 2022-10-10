using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMelee : AI
{
    private bool runeActivated = false;
    private bool isDying = false;


    protected override void Update()
    {
        if (GameManager.RuneEquipped && distanceToPlayer <= 5) runeActivated = true;
        if (!runeActivated) base.Update();
        else if (runeActivated) EnemyDeath();
    }

    private void EnemyDeath()
    {
        if (!isDying)
        {
            isDying = true;
            StartCoroutine(DeathSecuence());
        }
    }

    IEnumerator DeathSecuence()
    {
        enemyAnimation.Play("Death");
        navMeshAgent.destination = transform.position;
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
