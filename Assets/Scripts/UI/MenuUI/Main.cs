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
    public void OnClickPlayTutorial()
    {
        SceneManager.LoadScene(1);
        GameManager.testing = true;
        //GameManager.Instance.InitialSettings();
    }

    public void OnExitGame()
    {
        Application.Quit();
        Debug.Log("QUIT");
    }

    public void OnClickSkipTutorial()
    {
        SceneManager.LoadScene(2);
        GameManager.testing = true;
        //GameManager.Instance.InitialSettings();
    }
}
