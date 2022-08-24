using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private float runEnergy;
    public float runAccess { get { return runEnergy; } set { runEnergy = value; } }

    private float flashLightPower;
    public float FlashLightLevel { get { return flashLightPower; } set { flashLightPower = value; } }

    private int throwRocks;
    public int throwRocksAmmount { get { return throwRocks; } set { throwRocks = value; } }

    private int health;
    public int HP { get { return health; } set { health = value; } }

    private void Start()
    {
        HP = 100;
    }

    private void Update()
    {
        if (HP == 0) Debug.Log("Estoy muerto");
    }
}


