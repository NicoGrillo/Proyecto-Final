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
        lowLevel = Random.Range(0, 8);
    }

    // Update is called once per frame
    void Update()
    {
        if (lowLevel >= 7)
        {
            StartCoroutine(LowBatteryFailure());
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

    IEnumerator LowBatteryFailure()
    {
        TurnOnOff();
        yield return new WaitForSeconds(0.05f);
        TurnOnOff();
        yield return new WaitForSeconds(0.05f);
        TurnOnOff();
        yield return new WaitForSeconds(0.05f);
        TurnOnOff();
        yield return new WaitForSeconds(0.45f);
        TurnOnOff();
        yield return new WaitForSeconds(0.9f);
        TurnOnOff();
    }
}
