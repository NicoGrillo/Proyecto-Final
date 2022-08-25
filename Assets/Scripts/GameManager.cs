using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static int hp;
    public static int HP { get => hp; set => hp = value; }

    private static int rocksAmmo;
    public static int RocksAmmo { get => rocksAmmo; set => rocksAmmo = value; }

    private static int flLevel;
    public static int FLLevel { get => flLevel; set => flLevel = value; }

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            hp = 100;
            RocksAmmo = 1;
            flLevel = 100;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
