using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwing : MonoBehaviour
{
    [SerializeField][Range(1f, 50f)] float intensity = 1f;

    void Update()
    {
        transform.Translate(Vector3.forward * intensity * Time.deltaTime);
    }
}
