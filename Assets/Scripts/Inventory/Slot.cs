using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour, IPointerClickHandler

{
    [SerializeField] public GameObject item;
    [SerializeField] public int ID;
    [SerializeField] public string type;
    [SerializeField] public string description;
    [SerializeField] public Sprite icon;

    [HideInInspector] public int quantity = 0;
    [HideInInspector] public bool empty;

    public Transform slotIconGameObject;        //Icon del Slot
    public TMP_Text textQuantityGameObject;     //Quantity del Slot

    void Awake()
    {
        //PlayerEvents.OnInventoryRefresh += UpdateStats;
    }

    void Start()
    {
        PlayerEvents.OnInventoryRefreshCall();

        slotIconGameObject = transform.GetChild(0);
        textQuantityGameObject = transform.GetChild(1).GetComponent<TMP_Text>();
        textQuantityGameObject.text = "";
    }

    public void UpdateSlot()
    {
        slotIconGameObject.GetComponent<Image>().sprite = icon;
        if (type == "SingleUse")
        {
            switch (ID)
            {
                case 10:
                    quantity = GameManager.BatteriesQty;
                    textQuantityGameObject.text = ("x" + quantity);
                    break;
                case 11:
                    TutorialEvents.OnRocksFirstPickCall();
                    quantity = GameManager.RocksAmmo;
                    textQuantityGameObject.text = ("x" + quantity);
                    break;
            }
        }
        if (type == "Utility")
        {
            quantity = 1;
            if (ID == 1) HUDManager.Instance.SetSelectedText("You found a Flashlight. Equip it and press F button to activate it.");
        }
        if (type == "Special")
        {
            quantity = 1;
            if (ID == 20) HUDManager.Instance.SetSelectedText("You found a Rune. Equip it to knock down some enemies.");
        }
    }

    public void UseItem()
    {
        item.GetComponent<Item>().ItemUsage(quantity);
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (item != null) UseItem();
    }

    void UpdateStats()
    {
        switch (ID)
        {
            case 10:
                quantity = GameManager.BatteriesQty;
                textQuantityGameObject.text = ("x" + quantity);
                break;
            case 11:
                quantity = GameManager.RocksAmmo;
                textQuantityGameObject.text = ("x" + quantity);
                break;
        }
    }

    void OnDisable()
    {
        PlayerEvents.OnInventoryRefresh -= UpdateStats;
    }

    void OnEnable()
    {
        PlayerEvents.OnInventoryRefresh += UpdateStats;
    }
}
