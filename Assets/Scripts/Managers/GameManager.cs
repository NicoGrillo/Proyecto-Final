using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static int hp;
    public static int HP { get => hp; set => hp = value; }

    private static int rocksAmmo;
    public static int RocksAmmo { get => rocksAmmo; set => rocksAmmo = value; }

    private static int flLevel;
    public static int FLLevel { get => flLevel; set => flLevel = value; }

    public static GameManager instance;
    public static GameManager Instance { get => instance; }


    private static bool sceneLevel0 = true;
    public static bool SceneLevel0 { get => sceneLevel0; set => sceneLevel0 = value; }

    private bool sceneActivated = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            InitialSettings();
            DontDestroyOnLoad(gameObject);
            RenderSettings.fog = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        if (SceneManager.GetActiveScene().name == "Level0")
        {
            if (SceneLevel0) HUDActive();
            SceneLevel0 = false;
        }
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            if (sceneActivated) HUDActive();
            sceneActivated = false;
        }
    }

    private void HUDActive()
    {
        HUDManager.SetHPBar(hp);
        HUDManager.Instance.ThrowRocksText("x" + GameManager.RocksAmmo);
        HUDManager.SetFLBar(flLevel);
        HUDManager.SetFearBar(0);
    }

    public void InitialSettings()
    {
        hp = 100;
        RocksAmmo = 1;
        flLevel = 100;
    }
}
