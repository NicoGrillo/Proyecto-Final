using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    //---------------------- PROPIEDADES SERIALIZADAS ----------------------
    [SerializeField] int playerTakeDamage;
    //---------------------- PROPIEDADES PUBLICAS ----------------------
    //---------------------- PROPIEDADES PRIVADAS ----------------------
    private PlayerData playerData;
    private PlayerMove playerMove;
    private Flashlight flashLight;
    private bool firstRock, beingHit;

    void Start()
    {
        playerData = GetComponent<PlayerData>();
        playerMove = GetComponent<PlayerMove>();
        firstRock = true;
        beingHit = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Battery"))
        {
            Destroy(other.gameObject);
            playerData.FlashLightLevel = 100f;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Rocks"))
        {
            if (firstRock)
            {
                firstRock = false;
                Debug.Log("Algunas rocas, creo que puedo usarlas");
                playerData.throwRocksAmmount += 5;
            }
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            if (!beingHit)
            {
                beingHit = true;
                Debug.Log("Fui golpeado por enemigo, me queda " + playerData.HP + " de vida");
                playerData.HP -= playerTakeDamage;
                Invoke("canHitAgain", 1);
            }
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (!beingHit)
            {
                beingHit = true;
                Debug.Log("Fui golpeado por enemigo, me queda " + playerData.HP + " de vida");
                playerData.HP -= playerTakeDamage;
                Invoke("canHitAgain", 1);
            }
        }
    }

    private void canHitAgain()
    {
        beingHit = false;
    }
}