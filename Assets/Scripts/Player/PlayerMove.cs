using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //---------------------- PROPIEDADES SERIALIZADAS ----------------------
    [SerializeField][Range(1f, 2000f)] private int moveForce = 1;
    [SerializeField][Range(1f, 10f)] private int runForce = 1;
    [SerializeField][Range(1f, 2000f)] private int jumpForce = 40;
    [SerializeField][Range(1f, 200f)] private int MaxSpeed = 5;
    [SerializeField][Range(0.1f, 10f)] private float rotateSpeed = 1f;
    [SerializeField][Range(1f, 200f)] private int delayNextJump = 1;
    //---------------------- PROPIEDADES PUBLICAS ----------------------
    //---------------------- PROPIEDADES PRIVADAS ----------------------
    private Rigidbody RB;
    private Animator anim; //animRun;
    private PlayerData playerData;
    private Vector3 playerDirection;
    private string[,] moveAnim = new string[,]
    {{"Walk Forward","Walk Back" ,"Walk Right","Walk Left"},
     {"Run Forward","Run Back" ,"Run Right","Run Left"}};

    private float cameraAxisX;
    private bool isRunning, isJumping, inDelayJump, isStop;
    private float count;

    private bool isHypno;
    public bool IsHypno { get => isHypno; set => isHypno = value; }

    void Start()
    {
        RB = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        playerData = GetComponent<PlayerData>();
        isRunning = false;
        isJumping = false;
        isHypno = false;
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHypno)
        {
            RotatePlayer();
            WalkOrRun();
            AnimPlayer();
            InputsPlayer();
        }
    }

    void FixedUpdate()
    {
        if (!isHypno)
        {
            Move();
            Jump();
        }
        else
        {
            Hypnotized();
        }
    }

    private void Move()
    {
        if (playerDirection != Vector3.zero && RB.velocity.magnitude < MaxSpeed)
        {
            if (isRunning) RB.AddForce(transform.TransformDirection(playerDirection) * moveForce * runForce, ForceMode.Force);
            else RB.AddForce(transform.TransformDirection(playerDirection) * moveForce, ForceMode.Force);
        }
    }

    private void Jump()
    {
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
        if (!isHypno)
        {
            cameraAxisX += Input.GetAxis("Mouse X");
            Quaternion newRotationX = Quaternion.Euler(0, cameraAxisX * rotateSpeed, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotationX, 2.5f * Time.deltaTime);
        }
    }

    private void WalkOrRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) isRunning = !isRunning;
    }

    private void InputsPlayer()
    {
        playerDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) playerDirection += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) playerDirection += Vector3.back;
        if (Input.GetKey(KeyCode.A)) playerDirection += Vector3.left;
        if (Input.GetKey(KeyCode.D)) playerDirection += Vector3.right;

        if (Input.GetKey(KeyCode.Space) && !isJumping) isJumping = true;

    }

    private void AnimPlayer()
    {
        //Variables para las Animaciones
        bool Forward = Input.GetKey(KeyCode.W);
        bool Back = Input.GetKey(KeyCode.S);
        bool Right = Input.GetKey(KeyCode.D);
        bool Left = Input.GetKey(KeyCode.A);

        if (Forward) anim.SetBool("Forward",true);
        if (Back) anim.SetBool("Back",true);
        if (Right) anim.SetBool("Right",true);
        if (Left) anim.SetBool("Left",true);

        if (!Forward) anim.SetBool("Forward",false);
        if (!Back) anim.SetBool("Back",false);
        if (!Right) anim.SetBool("Right",false);
        if (!Left) anim.SetBool("Left",false);

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
    }

    private bool IsAnimation(string animName)
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName(animName);
    }

    private void Hypnotized()
    {
        anim.SetBool("Forward",true);
        RB.AddForce(transform.TransformDirection(Vector3.forward) * moveForce, ForceMode.Force);

        if (count == 0)
        {
            GameManager.HP -= playerData.DamageTake("Hypno");
            HUDManager.SetHPBar(GameManager.HP);
            HUDManager.Instance.SetSelectedText("HIPNOTIZADO");
        }
        count += Time.deltaTime;
        if (count >= 1) count = 0;
    }
}
