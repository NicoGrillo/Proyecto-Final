using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwing : MonoBehaviour
{
    //---------------------- PROPIEDADES SERIALIZADAS ----------------------
    [SerializeField][Range(1f, 2000f)] int intensity = 40;
    //---------------------- PROPIEDADES PRIVADAS ----------------------
    private Rigidbody RB;

    void Start()
    {
        RB = GetComponent<Rigidbody>();
        RB.AddForce(transform.TransformDirection(new Vector3(0, 0.17f, 1)) * intensity, ForceMode.Impulse);
    }

    void Update()
    {
        //transform.Translate(Vector3.forward * intensity * Time.deltaTime);
    }
}
