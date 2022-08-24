using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour
{
    [SerializeField] Light sun;
    [SerializeField][Range(1, 60)] int daySpeed = 1;
    [SerializeField][Range(1, 10)] int dayUpSpeed = 2;

    private Transform sunTransform;
    private float newXValue;
    //private float newXValue;

    void Start()
    {
        newXValue = 315f;
        sunTransform = sun.GetComponent<Transform>();
        sunTransform.localRotation = Quaternion.Euler(newXValue, 0, 0);
    }

    void Update()
    {
        SunTranslation();
    }

    private void SunTranslation()
    {
        NewXRotation();
        sunTransform.localRotation = Quaternion.Euler(newXValue, 0, 0);
    }

    private void NewXRotation()
    {
        if (newXValue < 180) newXValue += Time.deltaTime * daySpeed * dayUpSpeed;
        else newXValue += Time.deltaTime * daySpeed;
        if (newXValue >= 360) newXValue = 0f;
    }
}
