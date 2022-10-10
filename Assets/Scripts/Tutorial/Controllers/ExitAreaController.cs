using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExitAreaController : MonoBehaviour
{
    [SerializeField] GameObject textPanel;
    [SerializeField] TMP_Text noteText;
    [SerializeField] string stepTXT;
    [SerializeField] bool fogActivated = true;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerEvents.OnCantMoveCall(true);              //PlayerCC: Player no se mueve
            gameObject.SetActive(false);
            textPanel.SetActive(true);
            noteText.text = stepTXT;
            RenderSettings.fog = fogActivated;
        }
    }
}
