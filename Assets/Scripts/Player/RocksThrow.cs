using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocksThrow : MonoBehaviour
{
    //---------------------- PROPIEDADES SERIALIZADAS ----------------------
    [SerializeField] GameObject rock;
    [SerializeField] GameObject rockPoint;
    [SerializeField][Range(1f, 5f)] int reloadTime = 1;
    //---------------------- PROPIEDADES PUBLICAS ----------------------
    //---------------------- PROPIEDADES PRIVADAS ----------------------
    private Vector3 setRockPoint;
    private bool canThrow;

    void Start()
    {
        canThrow = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) Throw();
    }

    private void Throw()
    {
        if (canThrow && GameManager.RocksAmmo > 0)
        {
            GameManager.RocksAmmo--;
            HUDManager.Instance.ThrowRocksText("x" + GameManager.RocksAmmo);
            HUDManager.EnableItem(1, 2);
            setRockPoint = rockPoint.transform.position;
            Instantiate(rock, setRockPoint, transform.rotation);
            canThrow = false;
            Invoke("Reload", reloadTime);
            if (GameManager.RocksAmmo == 0) HUDManager.EnableItem(1, 2);
        }
        if (GameManager.RocksAmmo == 0)
        {
            HUDManager.Instance.SetSelectedText("No tengo mas piedras");
        }
    }

    private void Reload()
    {
        HUDManager.EnableItem(1, 0);
        canThrow = true;
    }
}
