using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour
{
    [SerializeField] bool initialSettings = false;
    [SerializeField] bool isTutorial = false;

    void Start()
    {
        RenderSettings.fog = true;
        if (initialSettings) GameManager.testing = true;
        if (isTutorial) PlayerEvents.OnTutorialCall();
    }

}
