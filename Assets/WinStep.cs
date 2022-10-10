using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinStep : MonoBehaviour
{
    bool firstCollision = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!firstCollision)
            {
                TutorialEvents.OnTextPanelActivateCall(false);   //EnemiesAreaController: Desactiva a todos los enemies del area
                StartCoroutine(WinGame());
                PlayerEvents.OnCantMoveCall(true);
                HUDManager.Instance.SetSelectedText("You completed the tutorial");
                firstCollision = true;
            }
        }
    }

    IEnumerator WinGame()
    {
        yield return new WaitForSeconds(2);
        PlayerEvents.OnWinCall();
    }
}
