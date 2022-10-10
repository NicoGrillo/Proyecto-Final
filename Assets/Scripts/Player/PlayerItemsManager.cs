using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemsManager : MonoBehaviour
{
    [SerializeField] Transform playerHand;

    //List
    [SerializeField] List<GameObject> rocksList;
    public List<GameObject> RocksList { get => rocksList; set => rocksList = value; }

    //----------------------------------------------------------------------------------//
    [SerializeField] GameObject inventory;
    private bool inventoryEnabled = false;

    //----------------------------------------------------------------------------------//
    bool FlashlightON;


    void Awake()
    {
        //PlayerEvents.OnInventoryRefresh += UpdatePlayer;
    }

    void Start()
    {
        rocksList = new List<GameObject>();
        FlashlightON = false;
        GameManager.FLLevel = 100;
        //inventory.SetActive(false);
        inventoryEnabled = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) ActivateEquippedUtilityItem();

        if (Input.GetKeyDown(KeyCode.Q)) DetachEquippedItems();

        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryEnabled = !inventoryEnabled;
            inventory.SetActive(inventoryEnabled);
            PlayerEvents.OnCantMoveCall(inventoryEnabled);
            PlayerEvents.OnInventoryRefreshCall();
        }
    }

    //Método que permite usar algun Utility al Player
    private void ActivateEquippedUtilityItem()
    {
        if (GameManager.FLEquipped)
        {
            playerHand.GetChild(0).gameObject.SetActive(!FlashlightON);
            FlashlightON = !FlashlightON;
        }
    }

    private void EquipItem(GameObject item)
    {
        DetachEquippedItems();
        item.SetActive(true);
        item.transform.parent = playerHand;
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    private void DetachEquippedItems()
    {
        foreach (Transform child in playerHand)
        {
            child.gameObject.SetActive(false);
        }
    }
}
