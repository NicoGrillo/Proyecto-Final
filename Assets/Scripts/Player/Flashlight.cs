using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    //---------------------- PROPIEDADES SERIALIZADAS ----------------------
    [SerializeField] GameObject flashlightGO;
    [SerializeField][Range(1f, 30f)] float decayTime = 1f;
    //---------------------- PROPIEDADES PUBLICAS ----------------------
    //---------------------- PROPIEDADES PRIVADAS ----------------------
    private Flashlight_PRO flashlight;
    private PlayerData playerData;
    private float count = 0f;
    private bool torchOn = false;

    void Start()
    {
        flashlight = flashlightGO.GetComponent<Flashlight_PRO>();
        playerData = GetComponent<PlayerData>();

        playerData.FlashLightLevel = 100f;
        flashlight.Change_Intensivity(playerData.FlashLightLevel);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && playerData.FlashLightLevel > 0)
        {
            torchOn = !torchOn;
            flashlight.Switch();

        }
        if (torchOn)
        {
            flashlight.Change_Intensivity(playerData.FlashLightLevel);
            count += Time.deltaTime;

            if (count >= decayTime)
            {
                playerData.FlashLightLevel -= 10;
                count = 0;

                if (playerData.FlashLightLevel == 0)
                {
                    flashlight.Switch();
                    torchOn = !torchOn;
                }
            }
        }
    }
}
