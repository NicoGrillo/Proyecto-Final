using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    //---------------------- PROPIEDADES SERIALIZADAS ----------------------
    [SerializeField] int takeMeleeDamage;
    [SerializeField] int takeRangedDamage;
    //---------------------- PROPIEDADES PUBLICAS ----------------------
    //---------------------- PROPIEDADES PRIVADAS ----------------------
    private bool firstRock, beingHit;

    void Start()
    {
        firstRock = true;
        beingHit = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Battery"))
        {
            Destroy(other.gameObject);
            GameManager.FLLevel = 100;
            Debug.Log("Encontré unas baterias, tengo la linterna completa");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Rocks"))
        {
            if (firstRock)
            {
                firstRock = false;
                GameManager.RocksAmmo += 5;
                HUDManager.Instance.SetSelectedText("Algunas rocas, creo que puedo usarlas");
                Invoke("ResetSelectedText", 5);
            }
        }

        if (other.gameObject.CompareTag("ThrowRock"))
        {
            if (!beingHit)
            {
                beingHit = true;
                GameManager.HP -= takeRangedDamage;
                Debug.Log("Fui golpeado por un proyectil, me queda " + GameManager.HP + " de vida");
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
                GameManager.HP -= takeMeleeDamage;
                Debug.Log("Fui golpeado por enemigo, me queda " + GameManager.HP + " de vida");
                Invoke("canHitAgain", 1);
            }
        }
    }

    private void canHitAgain()
    {
        beingHit = false;
    }

    private void ResetSelectedText()
    {
        HUDManager.Instance.SetSelectedText("");
    }
}