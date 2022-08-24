using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveForce2 : MonoBehaviour
{
    //---------------------- PROPIEDADES SERIALIZADAS ----------------------
    [SerializeField][Range(1f, 2000f)] private int movementForce = 1;
    [SerializeField][Range(1f, 10f)] private int runBoost = 1;
    [SerializeField][Range(1f, 2000f)] private int jumpForce = 40;
    [SerializeField][Range(1f, 200f)] private int MaxSpeed = 5;
    [SerializeField][Range(0.1f, 10f)] private float rotateSpeed = 0.1f;
    [SerializeField][Range(1f, 200f)] private int delayNextJump = 1;
    //---------------------- PROPIEDADES PUBLICAS ----------------------
    //---------------------- PROPIEDADES PRIVADAS ----------------------
    private float cameraAxisX;
    private bool isRunning, isJumping, inDelayJump, isStop;
    private Rigidbody RB;
    private Animator anim; //animRun;
    private Vector3 playerDirection;
    private string[,] moveAnim = new string[,]
    {{"Walk Forward","Walk Back" ,"Walk Right","Walk Left"},
     {"Run Forward","Run Back" ,"Run Right","Run Left"}};

    void Start()
    {
        RB = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        isRunning = false;
        isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();
        //Reseteo la direccion del movimiento
        playerDirection = Vector3.zero;
        //Variables para las Animaciones
        bool Forward = Input.GetKeyDown(KeyCode.W);
        bool Back = Input.GetKeyDown(KeyCode.S);
        bool Right = Input.GetKeyDown(KeyCode.D);
        bool Left = Input.GetKeyDown(KeyCode.A);
        //Reviso si corre o no
        if (Input.GetKeyDown(KeyCode.LeftShift)) RunMode();
        //Animo en funcion del movimiento y si corre o no
        if (Forward) anim.SetTrigger("Forward");
        if (Back) anim.SetTrigger("Back");
        if (Right) anim.SetTrigger("Right");
        if (Left) anim.SetTrigger("Left");
        // Estoy en reposo si se deja de presionar alguna de las teclas de movimiento.

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) isStop = true;
        if (isStop)
        {
            if (RB.velocity.magnitude <= 1 && !IsAnimation("Idle"))
            {
                anim.SetTrigger("Idle");
                isStop = false;
            }
        }

        if (Input.GetKey(KeyCode.W)) playerDirection += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) playerDirection += Vector3.back;
        if (Input.GetKey(KeyCode.A)) playerDirection += Vector3.left;
        if (Input.GetKey(KeyCode.D)) playerDirection += Vector3.right;

        if (Input.GetKey(KeyCode.Space) && !isJumping) isJumping = true;
    }

    void FixedUpdate()
    {
        if (playerDirection != Vector3.zero && RB.velocity.magnitude < MaxSpeed)
        {
            if (isRunning) RB.AddForce(transform.TransformDirection(playerDirection) * movementForce * runBoost, ForceMode.Force);
            else RB.AddForce(transform.TransformDirection(playerDirection) * movementForce, ForceMode.Force);
        }

        if (isJumping && !inDelayJump)
        {
            /*La fuerza a aplicar es instantánea (ForceMode.Impulse)
            Fuerzas instantáneas: Actúan por un breve instante y, 
            por lo tanto, originan movimientos uniformes 
            acelerando la rigibody con un impulso.
            */
            RB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            inDelayJump = true;
            Invoke("DelayNextJump", delayNextJump);
        }

    }

    private void DelayNextJump()
    {
        inDelayJump = false;
        isJumping = false;
    }

    private void RotatePlayer()
    {
        cameraAxisX += Input.GetAxis("Mouse X");
        Quaternion newRotationX = Quaternion.Euler(0, cameraAxisX * rotateSpeed, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotationX, 2.5f * Time.deltaTime);
    }

    private void RunMode()
    {
        isRunning = !isRunning;
    }

    private bool IsAnimation(string animName)
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName(animName);
    }
}