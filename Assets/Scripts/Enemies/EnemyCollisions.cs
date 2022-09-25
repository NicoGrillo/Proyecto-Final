using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisions : MonoBehaviour
{
    //---------------------- PROPIEDADES SERIALIZADAS ----------------------
    //---------------------- PROPIEDADES PUBLICAS ----------------------
    //---------------------- PROPIEDADES PRIVADAS ----------------------
    private EnemyData enemyData;
    private bool beingHit;

    void Start()
    {
        enemyData = GetComponent<EnemyData>();
        beingHit = false;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("ThrowRock"))
        {
            if (!beingHit)
            {
                enemyData.KnockedLevel -= 5;
                Destroy(other.gameObject);
                beingHit = true;
                Invoke("canHitAgain", 1);
            }
        }
    }

    private void canHitAgain()
    {
        beingHit = false;
    }
}
