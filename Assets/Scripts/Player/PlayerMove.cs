using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField][Range(1f, 10f)] float speed = 1;
    [SerializeField][Range(1f, 10f)] float runBoost = 1;
    [SerializeField][Range(1f, 10f)] float jumpForce = 1;
    [SerializeField][Range(1f, 10f)] float gravityForce = 9.81f;
    [SerializeField][Range(0.1f, 1f)] float rotateSpeed = 0.1f;

    private CharacterController CC;
    private float cameraAxisX;
    //private float cameraAxisY;
    private float gravityEffect;
    private bool isRunnnin = false;

    void Start()
    {
        CC = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();
        if (Input.GetKey(KeyCode.W)) Movement(Vector3.forward);
        if (Input.GetKey(KeyCode.S)) Movement(Vector3.back);
        if (Input.GetKey(KeyCode.A)) Movement(Vector3.left);
        if (Input.GetKey(KeyCode.D)) Movement(Vector3.right);

        if (Input.GetKey(KeyCode.Space)) Movement(Vector3.up * jumpForce);
        if (Input.GetKeyDown(KeyCode.LeftShift)) RunMode();

        if (CC.isGrounded)
        {
            gravityEffect = 0;
        }
        else
        {
            gravityEffect += gravityForce * Time.deltaTime;
            Movement(Vector3.down * gravityEffect);
        }

    }

    private void Movement(Vector3 value)
    {
        if (isRunnnin) CC.Move(transform.TransformDirection(value) * (speed + runBoost) * Time.deltaTime);
        else CC.Move(transform.TransformDirection(value) * speed * Time.deltaTime);
    }
    private void RotatePlayer()
    {
        cameraAxisX += Input.GetAxis("Mouse X");
        //transform.rotation = Quaternion.Euler(0, cameraAxisX * rotateSpeed, 0);
        Quaternion newRotationX = Quaternion.Euler(0, cameraAxisX * rotateSpeed, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotationX, 2f * Time.deltaTime);

        /*
        cameraAxisY += Input.GetAxis("Mouse Y");
        Quaternion newRotationY = Quaternion.Euler(0, cameraAxisY * rotateSpeed, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotationY, 2f * Time.deltaTime);
        */
    }

    private void RunMode()
    {
        if (!isRunnnin) isRunnnin = true;
        else isRunnnin = false;
    }
}
