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
        RB.AddForce(transform.TransformDirection(new Vector3(0, 0, 1)) * intensity, ForceMode.Impulse);
    }

    /*void onCollisionEnter(Collider other)
    {
        Debug.Log("COLISION");
        Debug.Log(other);
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Proyectil");
            PlayerEvents.OnDamageCall(transform.GetComponent<PlayerDamageSource>().RangedDamage);
        }
    }*/
}