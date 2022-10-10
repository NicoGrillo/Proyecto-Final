using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Special Items Equipped//
    private static bool flEquipped;
    public static bool FLEquipped { get => flEquipped; set => flEquipped = value; }

    private static bool runeEquipped;
    public static bool RuneEquipped { get => runeEquipped; set => runeEquipped = value; }

    //Parameters Levels//

    private static int hp;
    public static int HP { get => hp; set => hp = value; }

    private static int flLevel;
    public static int FLLevel { get => flLevel; set => flLevel = value; }

    //SingleUse Quantity Items//

    private static int rocksAmmo;
    public static int RocksAmmo { get => rocksAmmo; set => rocksAmmo = value; }

    private static int batteriesQty;
    public static int BatteriesQty { get => batteriesQty; set => batteriesQty = value; }

    //--//

    public static GameManager instance;
    public static GameManager Instance { get => instance; }

    [SerializeField] public static bool testing;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
            PlayerEvents.OnLose += EndGame;
            PlayerEvents.OnWin += EndGame;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        if (testing)
        {
            InitialSettings();
            testing = false;
        }
    }

    IEnumerator HUDActivate()
    {
        yield return new WaitForSeconds(.1f);
        HUDManager.SetHPBar(hp);
        HUDManager.SetFLBar(flLevel);
        HUDManager.SetFearBar(0);
    }

    public void InitialSettings()
    {
        Debug.Log(testing);
        hp = 100;
        RocksAmmo = 1;
        flLevel = 100;
        StartCoroutine(HUDActivate());
    }

    private void EndGame()
    {
        StartCoroutine(BackToMenu());
    }

    IEnumerator BackToMenu()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }

    private void OnDisable()
    {
        PlayerEvents.OnLose -= EndGame;
    }
}
