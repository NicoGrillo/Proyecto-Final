using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowRock : MonoBehaviour
{
    //---------------------- PROPIEDADES SERIALIZADAS ----------------------
    [SerializeField][Range(1f, 5f)] int reloadTime = 1;
    [SerializeField] GameObject rock;
    //---------------------- PROPIEDADES PRIVADAS ----------------------
    private bool canThrow = true;

    public bool activatedPoint;
    private bool canAction;

    void Awake()
    {
        PlayerEvents.OnCantMove += CanThrow;
    }


    void Update()
    {
        if (canAction)
        {
            if (activatedPoint)
            {
                if (Input.GetMouseButton(0))
                {
                    Throw();
                }
            }
        }
    }

    void Throw()
    {
        if (canThrow && GameManager.RocksAmmo > 0)
        {
            Instantiate(rock, transform.position, transform.rotation);
            GameManager.RocksAmmo--;
            PlayerEvents.OnInventoryRefreshCall();
            canThrow = false;
            Invoke("Reload", reloadTime);
        }
    }

    private void Reload()
    {
        canThrow = true;
    }

    void CanThrow(bool value)
    {
        canAction = !value;
    }

    void OnDisable()
    {
        PlayerEvents.OnCantMove -= CanThrow;
    }
}
