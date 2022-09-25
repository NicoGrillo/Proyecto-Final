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
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (!beingHit)
            {
                beingHit = true;
                PlayerEvents.OnDamageCall(transform.GetComponent<PlayerDamageSource>().MeleeDamage);
                Invoke("canHitAgain", 1);
            }
        }

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

                if (other.gameObject.name == "Flashlight")
                {
                    TutorialEvents.OnFLPickCall();
                    other.gameObject.GetComponentInChildren<Flashlight_PRO>().Switch();
                    HUDManager.Instance.SetSelectedText("Encontré una linterna. Con F la uso");
                    GameManager.FLLevel = 100;
                    HUDManager.SetFLBar(GameManager.FLLevel);
                }
                other.gameObject.SetActive(false);
            }

            if (other.gameObject.name == "StartNote")
            {
                Destroy(other.gameObject);
                HUDManager.Instance.enableTextPanel("StartNote");
            }
        }

        if (other.CompareTag("WinWall"))
        {
            PlayerEvents.OnWinCall();
        }

        if (other.CompareTag("EnemyProyectile"))
        {
            if (!beingHit)
            {
                Destroy(other.gameObject);
                beingHit = true;
                PlayerEvents.OnDamageCall(transform.GetComponent<PlayerDamageSource>().RangedDamage);
                Invoke("canHitAgain", 1);
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit other)
    {
        if (other.gameObject.CompareTag("Enemy")) enemyCollision();
        if (other.gameObject.CompareTag("Rocks"))
        {
            TutorialEvents.OnRocksFirstPickCall();
            bool repeated = false;
            foreach (GameObject rocks in playerItemManager.RocksList)
            {
                if (other.gameObject.transform.parent.name == rocks.name) repeated = true;
            }

            if (!repeated)
            {
                playerItemManager.RocksList.Add(other.gameObject.transform.parent.gameObject);
                GameManager.RocksAmmo += 2;
                HUDManager.EnableItem(1, 0);
                HUDManager.Instance.ThrowRocksText("x" + GameManager.RocksAmmo);
                HUDManager.Instance.SetSelectedText("Algunas rocas, creo que puedo usarlas");
            }
        }

        /**/
    }

    private void canHitAgain()
    {
        beingHit = false;
    }

    private void enemyCollision()
    {
        if (!beingHit)
        {
            beingHit = true;
            PlayerEvents.OnDamageCall(transform.GetComponent<PlayerDamageSource>().MeleeDamage);
            Invoke("canHitAgain", 1);
        }
    }
}
