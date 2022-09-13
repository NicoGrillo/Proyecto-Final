using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    //---------------------- PROPIEDADES SERIALIZADAS ----------------------
    [SerializeField][Range(1, 30)] int decayTime = 1;
    //---------------------- PROPIEDADES PUBLICAS ----------------------
    //---------------------- PROPIEDADES PRIVADAS ----------------------
    private Flashlight_PRO flashlight;
    private float count = 0f;
    private float lowLevel = 10f;
    private bool withBatteryLeft;

    void Start()
    {
        flashlight = GetComponent<Flashlight_PRO>();
        flashlight.Switch();
        withBatteryLeft = true;
        flashlight.Change_Intensivity(GameManager.FLLevel);
    }

    // Update is called once per frame
    void Update()
    {
        if (withBatteryLeft) Flashlight();
        else BatteryRecharge();
        LostBatteryLevel();
        if (GameManager.FLLevel <= 40) LowBatteryFailure();
    }

    private void Flashlight()
    {
        flashlight.Change_Intensivity(GameManager.FLLevel);
        if (GameManager.FLLevel <= 0)
        {
            flashlight.Switch();
            withBatteryLeft = false;
        }
    }

    private void LostBatteryLevel()
    {
        count += Time.deltaTime;

        if (count >= decayTime)
        {
            GameManager.FLLevel -= 10;
            count = 0;
            HUDManager.SetFLBar(GameManager.FLLevel);
        }
    }

    private void BatteryRecharge()
    {
        if (GameManager.FLLevel > 0)
        {
            flashlight.Switch();
            withBatteryLeft = true;
        }
    }

    private void LowBatteryFailure()
    {
        if (lowLevel >= 5)
        {
            TurnOnOff();
            Invoke("TurnOnOff", 0.05f);
            Invoke("TurnOnOff", 0.10f);
            Invoke("TurnOnOff", 0.15f);
            Invoke("TurnOnOff", 0.6f);
            Invoke("TurnOnOff", 1.5f);
            lowLevel = 0;
        }
        lowLevel += Time.deltaTime;
    }

    private void TurnOnOff()
    {
        flashlight.SwitchFailure();
    }

}

