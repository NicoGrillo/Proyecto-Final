using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    //---------------------- PROPIEDADES SERIALIZADAS ----------------------    
    //---------------------- PROPIEDADES PUBLICAS ----------------------
    //---------------------- PROPIEDADES PRIVADAS ----------------------
    private bool beingHit;
    private PlayerData playerData;
    private PlayerItemsManager playerItemManager;

    void Start()
    {
        playerData = GetComponent<PlayerData>();
        playerItemManager = GetComponent<PlayerItemsManager>();
        beingHit = false;
        ResetSelectedText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Battery"))
        {
            Destroy(other.gameObject);
            GameManager.FLLevel = 100;
            HUDManager.Instance.SetSelectedText("Encontré unas baterias, tengo la linterna completa");
            Invoke("ResetSelectedText", 3);
        }

        if (other.CompareTag("Items"))
        {
            if (!playerItemManager.ItemDirectory.ContainsKey(other.gameObject.name))
            {
                playerItemManager.ItemDirectory.Add(other.gameObject.name, other.gameObject);
                HUDManager.Instance.SetSelectedText("Encontré una linterna. Con F la uso");
                Invoke("ResetSelectedText", 3);
                other.gameObject.SetActive(false);

                if (other.gameObject.name == "Flashlight") GameManager.FLLevel = 100;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Rocks"))
        {
            bool repeated = false;
            foreach (GameObject rocks in playerItemManager.RocksList)
            {
                if (other.gameObject.transform.parent.name == rocks.name) repeated = true;
            }
            /*
            for (int i = 0; i < playerItemManager.RocksList.Count; i++)
            {
                if (other.gameObject.transform.parent.name == playerItemManager.RocksList[i].name) repeated = true;
            }
            */
            if (!repeated)
            {
                playerItemManager.RocksList.Add(other.gameObject.transform.parent.gameObject);
                GameManager.RocksAmmo += 2;
                HUDManager.Instance.SetSelectedText("Algunas rocas, creo que puedo usarlas");
                Invoke("ResetSelectedText", 3);
            }
        }

        if (other.gameObject.CompareTag("ThrowRock"))
        {
            if (!beingHit)
            {
                beingHit = true;
                GameManager.HP -= playerData.DamageTake("Ranged");
                HUDManager.Instance.SetSelectedText("Fui golpeado por un proyectil, me queda " + GameManager.HP + " de vida");
                Invoke("ResetSelectedText", 3);
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
                GameManager.HP -= playerData.DamageTake("Melee");
                HUDManager.Instance.SetSelectedText("Fui golpeado por enemigo, me queda " + GameManager.HP + " de vida");
                Invoke("ResetSelectedText", 3);
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
