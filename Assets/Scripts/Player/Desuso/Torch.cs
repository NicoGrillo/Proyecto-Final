using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField] GameObject torch;
    [SerializeField] GameObject torchLight;

    [SerializeField][Range(1f, 5f)] float decayTime;

    Light intensity;
    bool torchOn = false;
    float count;

    void Start()
    {
        intensity = torchLight.GetComponent<Light>();

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.T))
        {
            torch.SetActive(torchOn);
            torch.GetComponent<Flashlight_PRO>().Switch();
            torchOn = !torchOn;
            Debug.Log(torchOn);
        }

        if (!torchOn)
        {
            count += Time.deltaTime;
            if (count >= decayTime)
            {
                count = 0;
                intensity.intensity -= 1;
                Debug.Log("se baja intensidad");
            }
        }
    }

}
