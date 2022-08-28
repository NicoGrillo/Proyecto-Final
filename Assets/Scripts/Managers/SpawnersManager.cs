using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnersManager : MonoBehaviour
{
    [SerializeField] GameObject[] spawners;
    [SerializeField] bool activateSpawners;

    private bool activated;
    private bool desactivated;

    void Start()
    {
        activateSpawners = true;
        activated = true;
        desactivated = false;
    }

    void Update()
    {
        CheckStatus();
    }

    private void CheckStatus()
    {
        if (!activateSpawners && desactivated)
        {
            activated = true;
            desactivated = false;
            for (int i = 0; i < spawners.Length; i++)
            {
                spawners[i].SetActive(false);
            }
        }

        if (activateSpawners && activated)
        {
            activated = false;
            desactivated = true;
            for (int i = 0; i < spawners.Length; i++)
            {
                spawners[i].SetActive(true);
            }
        }
    }
}
