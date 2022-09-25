using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TutorialEvents : MonoBehaviour
{
    public static event Action<bool> OnTextPanelActivate;
    public static event Action OnFLPick;
    public static event Action OnRocksFirstPick;

    private static bool firstRock = true;

    public static void OnTextPanelActivateCall(bool value) { OnTextPanelActivate?.Invoke(value); }
    public static void OnFLPickCall() { OnFLPick?.Invoke(); }

    public static void OnRocksFirstPickCall()
    {
        if (firstRock)
        {
            OnRocksFirstPick?.Invoke();
            firstRock = false;
        }
    }
}
