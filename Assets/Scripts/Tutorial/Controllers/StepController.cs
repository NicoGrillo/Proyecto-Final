using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StepController : MonoBehaviour
{
    [SerializeField] GameObject[] objectsToSpawn;
    [SerializeField] GameObject textPanel;
    [SerializeField] TMP_Text noteText;
    [SerializeField] string stepTXT;

    bool firstCollision = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerEvents.OnCantMoveCall(true);              //PlayerCC: Player no se mueve
            textPanel.SetActive(true);
            noteText.text = stepTXT;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!firstCollision)
            {
                firstCollision = true;
                for (int i = 0; i < objectsToSpawn.Length; i++)
                {
                    objectsToSpawn[i].SetActive(true);
                }
                gameObject.SetActive(false);
            }
        }
    }
}
