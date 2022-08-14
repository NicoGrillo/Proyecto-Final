using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocksThrow : MonoBehaviour
{
    [SerializeField] GameObject rock;
    [SerializeField] GameObject rockPoint;

    [SerializeField][Range(1f, 5f)] float reloadTime = 1f;
    Vector3 setRockPoint;
    bool canThrow = true;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) Throw();
    }

    private void Throw()
    {
        if (canThrow)
        {
            setRockPoint = rockPoint.transform.position;
            Instantiate(rock, setRockPoint, transform.rotation);
            canThrow = false;
            Invoke("Reload", reloadTime);
        }
    }

    private void Reload()
    {
        canThrow = true;
    }
}
