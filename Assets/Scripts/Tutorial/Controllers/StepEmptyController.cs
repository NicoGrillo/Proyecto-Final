using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepEmptyController : MonoBehaviour
{
    [SerializeField] GameObject[] objectsToSpawn;
    [SerializeField] bool desactiveEnemies = true;

    bool firstCollision = false;

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!firstCollision)
            {
                if (desactiveEnemies) TutorialEvents.OnTextPanelActivateCall(true);   //EnemiesAreaController: Desactiva a todos los enemies del area
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
