using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class ProfileManager : MonoBehaviour
{
    [SerializeField] private GameObject globalProfile;
    [SerializeField] private GameObject hypnoProfile;
    [SerializeField] private GameObject damageProfile;

    private void Awake()
    {
        PlayerEvents.OnStateHypno += SetHypnoProfile;
        PlayerEvents.OnDamage += SetDamageProfile;
    }

    private void SetHypnoProfile()
    {
        StartCoroutine(SetHypnocoroutine());
    }

    private void SetDamageProfile(int value)
    {
        StartCoroutine(SetDamagecoroutine());
    }

    IEnumerator SetHypnocoroutine()
    {
        globalProfile.SetActive(false);
        hypnoProfile.SetActive(true);
        yield return new WaitForSeconds(2);
        hypnoProfile.SetActive(false);
        globalProfile.SetActive(true);
    }

    IEnumerator SetDamagecoroutine()
    {
        damageProfile.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        damageProfile.SetActive(false);
    }

    private void OnDisable()
    {
        PlayerEvents.OnStateHypno -= SetHypnoProfile;
        PlayerEvents.OnDamage -= SetDamageProfile;
    }
}
