using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour
{
    [SerializeField] Light sun;
    //[SerializeField][Range(1, 60)] int daySpeed = 1;
    //[SerializeField][Range(1, 10)] int dayUpSpeed = 2;
    [SerializeField] IluminationCases iluminationCase;

    private Transform sunTransform;
    private float newXValue;
    enum IluminationCases { Day, Night, InGame };


    void Start()
    {
        newXValue = 315f;
        sunTransform = sun.GetComponent<Transform>();
        sunTransform.localRotation = Quaternion.Euler(newXValue, 0, 0);
    }

    void Update()
    {
        SunPosition();
    }

    private void SunTranslation()
    {
        /*
        NewXRotation();
        sunTransform.localRotation = Quaternion.Euler(newXValue, 0, 0);
        */
    }

    private void NewXRotation()
    {
        /*
        if (newXValue < 180) newXValue += Time.deltaTime * daySpeed * dayUpSpeed;
        else newXValue += Time.deltaTime * daySpeed;
        if (newXValue >= 360) newXValue = 0f;
        */
    }

    private void SunPosition()
    {
        switch (iluminationCase)
        {
            case IluminationCases.Day:
                sunTransform.localRotation = Quaternion.Euler(90, 0, 0);
                sun.intensity = 10f;
                break;
            case IluminationCases.Night:
                sunTransform.localRotation = Quaternion.Euler(90, 0, 0);
                sun.intensity = .5f;
                break;
            case IluminationCases.InGame:
                sunTransform.localRotation = Quaternion.Euler(50, 0, 0);
                sun.intensity = 0.03f;
                break;
        }
    }

    public void selectCase(int value)
    {
        switch (value)
        {
            case 1:
                iluminationCase = IluminationCases.Day;
                break;
            case 2:
                iluminationCase = IluminationCases.Night;
                break;
            case 3:
                iluminationCase = IluminationCases.InGame;
                break;
        }
    }
}
