using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesAreaController : MonoBehaviour
{
    void Awake()
    {
        TutorialEvents.OnTextPanelActivate += SetActiveState;
    }

    void SetActiveState(bool value)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
        TutorialEvents.OnTextPanelActivate -= SetActiveState;
    }
}
