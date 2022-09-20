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

    private float fear;
    public float FearLVL { get { return fear; } set { fear = value; } }

    //[SerializeField] private int meleeDamageTake;
    //[SerializeField] private int rangedDamageTake;
    //[SerializeField] private int hypnoDamageTake;

    [SerializeField] private bool isActive = true;
    [SerializeField][Range(1, 60)] int healingTime = 10;
    [SerializeField][Range(1, 60)] int fearRecoverTime = 2;

    //enum DamageTypes { Melee, Range, Hypno };

    private void Awake()
    {
        PlayerEvents.OnDamage += TakeDamage;
    }

    private void Start()
    {
        HP = 100;
        StartCoroutine(PassiveHealing());
    }

    private void Update()
    {
        if (HP <= 0)
        {
            PlayerEvents.OnLoseCall();
        }
    }

    public void TakeDamage(int value)
    {
        HP -= value;
        HUDManager.SetHPBar(HP);
    }

    private void OnDisable()
    {
        PlayerEvents.OnDamage -= TakeDamage;
    }

    IEnumerator PassiveHealing()
    {
        if (isActive)
        {
            if (health < 100)
            {
                //ESPERAR x SEGUNDOS PARA CURAR
                yield return new WaitForSeconds(healingTime);
                //CURA 1 HP
                HP++;
            }
        }
    }
    IEnumerator PassiveFearRecover()
    {
        if (isActive)
        {
            if (fear > 0)
            {
                //ESPERAR x SEGUNDOS PARA CURAR
                yield return new WaitForSeconds(fearRecoverTime);
                //CURA 1 HP
                fear--;
            }
        }
    }
}