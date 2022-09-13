using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFailure : MonoBehaviour
{
    private float lowLevel;
    private float initialIntensity;
    private bool onOff = true;
    private Light lightIntensity;

    // Start is called before the first frame update
    void Start()
    {
        lightIntensity = GetComponentInChildren<Light>();
        initialIntensity = lightIntensity.intensity;
        lowLevel = Random.Range(0, 6);
    }

    // Update is called once per frame
    void Update()
    {
        LowBatteryFailure();
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
        if (onOff) lightIntensity.intensity = 0;
        else lightIntensity.intensity = initialIntensity;
        onOff = !onOff;
    }
}
