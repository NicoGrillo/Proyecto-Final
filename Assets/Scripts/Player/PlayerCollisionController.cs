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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Battery"))
        {
            Destroy(other.gameObject);
            GameManager.FLLevel = 100;
            HUDManager.SetFLBar(GameManager.FLLevel);
            HUDManager.Instance.SetSelectedText("Encontré unas baterias, tengo la linterna completa");
        }

        if (other.CompareTag("Items"))
        {
            if (!playerItemManager.ItemDirectory.ContainsKey(other.gameObject.name))
            {
                playerItemManager.ItemDirectory.Add(other.gameObject.name, other.gameObject);
                HUDManager.Instance.SetSelectedText("Encontré una linterna. Con F la uso");
                other.gameObject.SetActive(false);

                if (other.gameObject.name == "Flashlight")
                {
                    GameManager.FLLevel = 100;
                    HUDManager.SetFLBar(GameManager.FLLevel);
                }
            }
        }

        if (other.CompareTag("WinWall"))
        {
            PlayerEvents.OnWinCall();
            Debug.Log(gameObject.name + " llamó al evento OnWin");
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
                HUDManager.EnableItem(1, 0);
                HUDManager.Instance.ThrowRocksText("x" + GameManager.RocksAmmo);
                HUDManager.Instance.SetSelectedText("Algunas rocas, creo que puedo usarlas");
            }
        }

        if (other.gameObject.CompareTag("ThrowRock"))
        {
            if (!beingHit)
            {
                beingHit = true;
                PlayerEvents.OnDamageCall(transform.GetComponent<PlayerDamageSource>().RangedDamage);
                Debug.Log(gameObject.name + " llamó al evento OnDamage");
                HUDManager.Instance.SetSelectedText("Fui golpeado por un proyectil, me queda " + GameManager.HP + " de vida");
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
                PlayerEvents.OnDamageCall(transform.GetComponent<PlayerDamageSource>().MeleeDamage);
                Debug.Log(gameObject.name + " llamó al evento OnDamage");
                //HUDManager.Instance.SetSelectedText("Fui golpeado por un enemigo, me queda " + GameManager.HP + " de vida");
                Invoke("canHitAgain", 1);
            }
        }
    }

    private void canHitAgain()
    {
        beingHit = false;
    }
}
