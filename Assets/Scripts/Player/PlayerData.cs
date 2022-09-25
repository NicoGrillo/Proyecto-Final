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

    [SerializeField] private bool isHealActive = true;
    [SerializeField] private bool isFearActive = true;
    [SerializeField][Range(1, 60)] int healingTime = 10;
    [SerializeField][Range(1, 60)] int fearRecoverTime = 2;

    private void Awake()
    {
        PlayerEvents.OnDamage += TakeDamage;
    }

    private void Start()
    {
        HP = 100;
    }

    private void Update()
    {
        if (HP <= 0)
        {
            PlayerEvents.OnLoseCall();
        }
        StartCoroutine(PassiveHealing());
        StartCoroutine(PassiveFearRecover());
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
        if (isHealActive)
        {
            if (HP < 100)
            {
                isHealActive = false;
                yield return new WaitForSeconds(healingTime);
                isHealActive = true;
                HP++;
                HUDManager.SetHPBar(HP);
            }
        }
    }
    IEnumerator PassiveFearRecover()
    {
        if (isFearActive)
        {
            if (FearLVL > 0)
            {
                isFearActive = false;
                yield return new WaitForSeconds(fearRecoverTime);
                isFearActive = true;
                FearLVL--;
                HUDManager.SetFearBar(FearLVL);
            }
        }
    }
}