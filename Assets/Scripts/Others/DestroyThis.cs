using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThis : MonoBehaviour
{
    [SerializeField][Range(0f, 10f)] float timeAlive = 1f;

    void Update()
    {
        Invoke("Destroy", timeAlive);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
