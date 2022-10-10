using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryItem : MonoBehaviour
{
    private Item item;

    void Start()
    {
        item = GetComponent<Item>();
    }


    void Update()
    {
        if (item.equipped)
        {
            if (GameManager.BatteriesQty > 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (GameManager.FLLevel < 100)
                    {
                        GameManager.BatteriesQty--;
                        
                        GameManager.FLLevel = 100;
                    }
                    else Debug.Log("Bateria Llena");
                }
            }
            else Debug.Log("Sin baterias restantes");
        }
    }
}
