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

    [SerializeField] private int meleeDamageTake;
    [SerializeField] private int rangedDamageTake;
    [SerializeField] private int hypnoDamageTake;

    enum DamageTypes { Melee, Range, Hypno };

    private void Start()
    {
        HP = 100;
    }

    private void Update()
    {
        if (HP <= 0) Debug.Log("Estoy muerto");
    }

    public int DamageTake(string value)
    {
        int damage = 0;
        switch (value)
        {
            case "Melee":
                damage = meleeDamageTake;
                break;
            case "Range":
                damage = rangedDamageTake;
                break;
            case "Hypno":
                damage = hypnoDamageTake;
                break;
        }
        return damage;
    }
}


