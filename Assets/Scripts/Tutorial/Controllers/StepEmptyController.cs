using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepEmptyController : MonoBehaviour
{
    [SerializeField] GameObject[] objectsToSpawn;
    [SerializeField] GameObject[] objectsToDesactivate;
    [SerializeField] bool desactivateEnemies = false;
    [SerializeField] bool activateEnemies = false;
    [SerializeField] bool fogActivated = true;

    bool firstCollision = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!firstCollision)
            {
                if (desactivateEnemies) TutorialEvents.OnTextPanelActivateCall(false);   //EnemiesAreaController: Desactiva a todos los enemies del area
                if (activateEnemies) TutorialEvents.OnTextPanelActivateCall(true);   //EnemiesAreaController: Desactiva a todos los enemies del area
                RenderSettings.fog = fogActivated;
                firstCollision = true;
                for (int i = 0; i < objectsToSpawn.Length; i++)
                {
                    objectsToSpawn[i].SetActive(true);
                }

                for (int i = 0; i < objectsToDesactivate.Length; i++)
                {
                    objectsToDesactivate[i].SetActive(false);
                }
                gameObject.SetActive(false);
            }
        }
    }
}
