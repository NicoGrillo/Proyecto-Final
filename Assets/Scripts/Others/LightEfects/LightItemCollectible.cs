using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightItemCollectible : MonoBehaviour
{
    [SerializeField] Light spotlight;
    [Tooltip("Tiempo que tarda en llegar al maximo brillo")]
    [SerializeField][Range(1, 5)] float blinkTime = 1;
    [Tooltip("Multiplicador de intensidad en base al tiempo")]
    [SerializeField][Range(0.1f, 5)] float intensityMultiplier = 2;
    [Tooltip("Intensidad minima")]
    [SerializeField][Range(0, 20)] float minimunIntensity = 0;

    private float count = 0;
    private bool intensity;

    void Update()
    {
        ChangeIntensity();
    }

    private void ChangeIntensity()
    {
        if (intensity)
        {
            count += Time.deltaTime;
            if (count >= blinkTime) intensity = false;
        }
        else
        {
            count -= Time.deltaTime;
            if (count <= 0) intensity = true;
        }
        spotlight.intensity = minimunIntensity + (count * intensityMultiplier);
    }
}
