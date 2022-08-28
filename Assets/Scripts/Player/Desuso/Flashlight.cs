using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    //---------------------- PROPIEDADES SERIALIZADAS ----------------------
    [SerializeField] GameObject flashlightGO;
    [SerializeField][Range(1, 30)] int decayTime = 1;
    //---------------------- PROPIEDADES PUBLICAS ----------------------
    //---------------------- PROPIEDADES PRIVADAS ----------------------
    private Flashlight_PRO flashlight;
    private float count = 0f;
    private bool torchOn = false;

    void Start()
    {
        flashlight = flashlightGO.GetComponent<Flashlight_PRO>();
        flashlight.Change_Intensivity(GameManager.FLLevel);
        Debug.Log("FLlevel es" + GameManager.FLLevel);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && GameManager.FLLevel > 0)
        {
            torchOn = !torchOn;
            flashlight.Switch();
        }

        if (torchOn)
        {
            flashlight.Change_Intensivity(GameManager.FLLevel);
            count += Time.deltaTime;

            if (count >= decayTime)
            {
                GameManager.FLLevel -= 10;
                count = 0;

                if (GameManager.FLLevel == 0)
                {
                    flashlight.Switch();
                    torchOn = !torchOn;
                }
            }
        }
    }
}
