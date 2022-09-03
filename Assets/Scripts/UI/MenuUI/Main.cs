using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Main : MonoBehaviour
{
    [SerializeField] private TMP_Text playTMP;

    // UN METODO PARA CAMBIAR LA ESCENA -> CUANDO SE PRESIONAL EL BOTON PLAY
    public void OnClickPlay()
    {
        SceneManager.LoadScene(1);
        GameManager.SceneLevel0 = true;
        //playTMP.text = "CONTINUE";
    }

    public void OnExitGame()
    {
        Application.Quit();
        Debug.Log("QUIT");
    }

    public void OnClickRestart()
    {
        GameManager.instance.InitialSettings();
        Debug.Log("Juego reseteado");
        //SceneManager.LoadScene(1);
        //playTMP.text = "PLAY";
    }
}
