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
    private GameObject lightGO;
    private float count = 0f;
    private float lowLevelCount = 10f;
    private bool withBatteryLeft;


    void Start()
    {
        flashlight = GetComponent<Flashlight_PRO>();
        lightGO = transform.GetChild(1).gameObject;
        withBatteryLeft = true;
        flashlight.Change_Intensivity(GameManager.FLLevel);
    }

    // Update is called once per frame
    void Update()
    {
        if (withBatteryLeft)
        {
            Flashlight();
            Flashlightfailure();
            LostBatteryLevel();
        }
        else BatteryRecharge();
    }

    private void Flashlight()
    {
        //lightGO.SetActive(true);
        if (GameManager.FLLevel <= 0)
        {
            withBatteryLeft = false;
        }
    }

    private void Flashlightfailure()
    {
        if (GameManager.FLLevel <= 40)
        {
            if (lowLevelCount >= 5)
            {
                StartCoroutine(LowBatteryFailure());
                lowLevelCount = 0;
            }
            lowLevelCount += Time.deltaTime;
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
        lightGO.SetActive(false);
        if (GameManager.FLLevel > 0)
        {
            withBatteryLeft = true;
        }
    }

    IEnumerator LowBatteryFailure()
    {
        lightGO.SetActive(false);
        yield return new WaitForSeconds(0.05f);
        lightGO.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        lightGO.SetActive(false);
        yield return new WaitForSeconds(0.05f);
        lightGO.SetActive(true);
        yield return new WaitForSeconds(0.45f);
        lightGO.SetActive(false);
        yield return new WaitForSeconds(0.9f);
        lightGO.SetActive(true);
    }


    private void ActivatedFL(bool value)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(value);
        }
    }

    private void OnEnable()
    {
        lightGO = transform.GetChild(1).gameObject;
        lightGO.SetActive(true);
        //withBatteryLeft = true;
    }
}

