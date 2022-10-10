using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMelee : AI
{
    private bool runeActivated = false;
    private bool isDying = false;


    protected override void Update()
    {
        if (GameManager.RuneEquipped && distanceToPlayer <= 2) runeActivated = true;
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
        Destroy(Instantiate(enemyKnockedSound, transform.position, Quaternion.identity), 1f);
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
