using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventScript : MonoBehaviour
{
    public UnityEvent OnTriggerZone;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnTriggerZone?.Invoke();
            Debug.Log(gameObject.name + " llamó al evento OnTriggerZone");
        }
    }
}
