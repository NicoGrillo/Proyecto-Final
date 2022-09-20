using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerEvents
{
    public static event Action OnWin;
    public static event Action OnLose;
    public static event Action<bool> OnCantMove;
    public static event Action OnStateHypno;
    public static event Action<int> OnHeal;
    public static event Action<int> OnDamage;
    public static event Action<int> OnShot;
    public static event Action<int> OnAmmoLoad;

    public static void OnWinCall() { OnWin?.Invoke(); }
    public static void OnLoseCall() { OnLose?.Invoke(); }
    public static void OnCantMoveCall(bool value) { OnCantMove?.Invoke(value); }
    public static void OnStateHypnoCall() { OnStateHypno?.Invoke(); }
    public static void OnHealCall(int hp) { OnHeal?.Invoke(hp); }
    public static void OnDamageCall(int hp) { OnDamage?.Invoke(hp); }
    public static void OnShotCall(int hp) { OnShot?.Invoke(hp); }
    public static void OnAmmoLoadCall(int hp) { OnAmmoLoad?.Invoke(hp); }
}
