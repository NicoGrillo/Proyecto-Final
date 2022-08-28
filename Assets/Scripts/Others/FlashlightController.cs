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
}

