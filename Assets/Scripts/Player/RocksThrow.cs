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
    private PlayerData playerData;

    void Start()
    {
        playerData = GetComponent<PlayerData>();
        playerData.throwRocksAmmount = 1;
        canThrow = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) Throw();
    }

    private void Throw()
    {
        if (canThrow && playerData.throwRocksAmmount > 0)
        {
            playerData.throwRocksAmmount -= 1;
            setRockPoint = rockPoint.transform.position;
            Instantiate(rock, setRockPoint, transform.rotation);
            canThrow = false;
            Invoke("Reload", reloadTime);
        }
        if (playerData.throwRocksAmmount == 0) Debug.Log("No tengo mas piedras");
    }

    private void Reload()
    {
        canThrow = true;
    }
}
