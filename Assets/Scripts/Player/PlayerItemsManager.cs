using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemsManager : MonoBehaviour
{
    [SerializeField] Transform playerHand;

    //List
    [SerializeField] List<GameObject> rocksList;
    public List<GameObject> RocksList { get => rocksList; set => rocksList = value; }

    //Dictionary
    private Dictionary<string, GameObject> itemDirectory;
    public Dictionary<string, GameObject> ItemDirectory { get => itemDirectory; set => itemDirectory = value; }
    //----------------------------------------------------------------------------------//
    [SerializeField] GameObject inventory;
    [SerializeField] GameObject slotHolder;
    private bool inventoryEnabled;

    private int allSlots;
    private bool enableSlots;

    private Dictionary<string, GameObject> inventorySlot;
    public Dictionary<string, GameObject> Inventory { get => inventorySlot; set => inventorySlot = value; }



    //----------------------------------------------------------------------------------//
    bool FlashlightON;

    void Start()
    {
        rocksList = new List<GameObject>();
        itemDirectory = new Dictionary<string, GameObject>();
        FlashlightON = false;

        allSlots = slotHolder.transform.childCount;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) CheckInventory();

        if (Input.GetKeyDown(KeyCode.F)) FlashlightItem();

        if (Input.GetKeyDown(KeyCode.I)) inventoryEnabled = !inventoryEnabled;
        if (inventoryEnabled) inventory.SetActive(true);
        else inventory.SetActive(false);
    }

    void CheckInventory()
    {
        int count = 0;

        for (int i = 0; i < itemDirectory.Count; i++)
        {
            count++;
        }
        HUDManager.Instance.SetSelectedText("Tengo " + count + " items guardados");
    }

    //Método que permite equipar la linterna al Player
    private void FlashlightItem()
    {
        if (!FlashlightON)
        {
            EquipItem(itemDirectory["Flashlight"]);
            HUDManager.EnableItem(0, 1);
            FlashlightON = true;
        }
        else
        {
            DetachItems();
            HUDManager.EnableItem(0, 0);
            FlashlightON = false;
        }
    }

    private void EquipItem(GameObject item)
    {
        DetachItems();
        item.SetActive(true);
        item.transform.parent = playerHand;
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    private void DetachItems()
    {
        foreach (Transform child in playerHand)
        {
            child.parent = null;
            child.gameObject.SetActive(false);
        }
    }
}
