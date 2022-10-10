using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    //---------------------- PROPIEDADES SERIALIZADAS ----------------------
    [SerializeField] GameObject meleeHitSound;
    [SerializeField] GameObject pickItemSound;
    //---------------------- PROPIEDADES PUBLICAS ----------------------
    //---------------------- PROPIEDADES PRIVADAS ----------------------
    private bool beingHit;
    private PlayerData playerData;
    private PlayerItemsManager playerItemManager;
    private Inventory inventory;
    private PlayerSoundManager playerSoundManager;

    void Start()
    {
        playerData = GetComponent<PlayerData>();
        playerItemManager = GetComponent<PlayerItemsManager>();
        inventory = GetComponent<Inventory>();
        playerSoundManager = GetComponent<PlayerSoundManager>();
        beingHit = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy")) enemyCollision();

        if (other.CompareTag("Items"))
        {
            GameObject itemPickedUp = other.gameObject;
            Item item = itemPickedUp.GetComponent<Item>();
            inventory.AddItem(itemPickedUp, item.ID, item.type, item.description, item.icon);
            Destroy(Instantiate(pickItemSound, transform.position, Quaternion.identity), .5f);
            PlayerEvents.OnInventoryRefreshCall();

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
                StartCoroutine(DamageSound(2));
            }
        }
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
            StartCoroutine(DamageSound(2));
        }
    }

    IEnumerator DamageSound(int index)
    {
        /*
        yield return new WaitForSeconds(0.2f);
        playerSoundManager.PlayerAudioSelection(index, 0.5f);
        yield return new WaitForSeconds(1f);
        playerSoundManager.StopSound();
        */

        Destroy(Instantiate(meleeHitSound, transform.position, Quaternion.identity), 1f);
        yield return new WaitForSeconds(1f);
        beingHit = false;
    }
}
