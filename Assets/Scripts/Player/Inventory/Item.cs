using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item : MonoBehaviour
{
    [SerializeField] public int ID;
    [SerializeField] public string type;
    [SerializeField] public string description;
    [SerializeField] public Sprite icon;

    [HideInInspector] public bool pickedUp;
    [HideInInspector] public bool equipped;
    [HideInInspector] public GameObject singleUseSlotManager;   //Slot de SingleUse en uso
    [HideInInspector] public GameObject utilitySlotManager;     //Slot de Utility en uso
    [HideInInspector] public Transform singleUseItemIcon;       //Icon del Slot SingleUse
    [HideInInspector] public Transform utilityItemIcon;         //Icon del Slot Utility

    public bool playerItem;
    private GameObject throwPoint;

    void Start()
    {
        throwPoint = GameObject.FindWithTag("ThrowPoint");
        singleUseSlotManager = GameObject.FindWithTag("HandManager");
        utilitySlotManager = GameObject.FindWithTag("AccessorySlotManager");

        if (!playerItem)
        {
            singleUseItemIcon = singleUseSlotManager.transform.GetChild(0);
            utilityItemIcon = utilitySlotManager.transform.GetChild(0);
        }
    }

    public void ItemUsage(int qty)
    {
        if (qty > 0)
        {
            if (type == "Utility")  //ID: 1=FL 2=
            {
                UnequipUtility();
                utilityItemIcon.GetComponent<Image>().sprite = icon;
                if (ID == 1)
                {
                    GameManager.FLEquipped = true;
                    HUDManager.Instance.SetSelectedText("Flashlight equipped");
                }
            }

            if (type == "Special")  //ID: 20=Rune 21=
            {
                UnequipSingleUse();
                singleUseItemIcon.GetComponent<Image>().sprite = icon;
                if (ID == 20)
                {
                    GameManager.RuneEquipped = true;
                    equipped = true;
                    HUDManager.Instance.SetSelectedText("Rune equipped");
                }
            }

            else if (type == "SingleUse") //ID: 10=Batteries 11=Rocks 12=
            {

                if (ID == 10)
                {
                    if (GameManager.FLLevel < 100)
                    {
                        GameManager.FLLevel = 100;
                        GameManager.BatteriesQty--;
                        HUDManager.Instance.SetSelectedText("Flashlight recharged");
                    }
                    else HUDManager.Instance.SetSelectedText("Flashlight already full");
                }
                if (ID == 11)
                {
                    UnequipSingleUse();
                    if (GameManager.RocksAmmo > 0)
                    {
                        singleUseItemIcon.GetComponent<Image>().sprite = icon;
                        equipped = true;

                        throwPoint.GetComponent<ThrowRock>().activatedPoint = true;
                        HUDManager.Instance.SetSelectedText("Rocks ready to throw");
                    }
                    else HUDManager.Instance.SetSelectedText("Out of ammo");
                }
                PlayerEvents.OnInventoryRefreshCall();
            }
        }
        else HUDManager.Instance.SetSelectedText("Empty");
    }

    void UnequipUtility()
    {
        GameManager.FLEquipped = false;
    }

    void UnequipSingleUse()
    {
        throwPoint.GetComponent<ThrowRock>().activatedPoint = false;
        GameManager.RuneEquipped = false;
    }
}
