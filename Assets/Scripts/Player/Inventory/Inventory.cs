using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] GameObject slotHolder;

    Transform singleUseItemIcon;       //Icon del Slot SingleUse
    Transform utilityItemIcon;         //Icon del Slot Utility

    GameObject[] slot;

    int allSlots;
    int enableSlots;

    void Start()
    {
        singleUseItemIcon = GameObject.FindWithTag("HandManager").transform.GetChild(0);
        utilityItemIcon = GameObject.FindWithTag("AccessorySlotManager").transform.GetChild(0);

        allSlots = slotHolder.transform.childCount;
        slot = new GameObject[allSlots];

        for (int i = 0; i < allSlots; i++)
        {
            slot[i] = slotHolder.transform.GetChild(i).gameObject;
            if (slot[i].GetComponent<Slot>().item == null)
            {
                slot[i].GetComponent<Slot>().empty = true;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) Unequip();
    }

    public void AddItem(GameObject itemObject, int itemID, string itemType, string itemDescription, Sprite itemIcon)
    {
        AddToSlot(itemObject, itemID, itemType, itemDescription, itemIcon);
    }

    void AddToSlot(GameObject itemObject, int itemID, string itemType, string itemDescription, Sprite itemIcon)
    {
        if (itemID == 10) GameManager.BatteriesQty++;
        if (itemID == 11) GameManager.RocksAmmo++;

        for (int i = 0; i < allSlots; i++)
        {
            Slot actualSlot = slot[i].GetComponent<Slot>();
            if (actualSlot.empty)
            {
                itemObject.GetComponent<Item>().pickedUp = true;

                actualSlot.item = itemObject;
                actualSlot.ID = itemID;
                actualSlot.type = itemType;
                actualSlot.description = itemDescription;
                actualSlot.icon = itemIcon;
                actualSlot.quantity = 1;

                itemObject.transform.parent = slot[i].transform;
                itemObject.SetActive(false);
                actualSlot.UpdateSlot();

                actualSlot.empty = false;

                return;
            }
            else if (itemID == actualSlot.ID)
            {
                actualSlot.quantity++;

                Destroy(itemObject);

                actualSlot.UpdateSlot();
                return;
            }
        }
    }

    void Unequip()
    {
        utilityItemIcon.GetComponent<Image>().sprite = null;
        singleUseItemIcon.GetComponent<Image>().sprite = null;

        GameManager.FLEquipped = false;
        GameManager.RuneEquipped = false;
    }
}
