using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCC : MonoBehaviour
{
    //---------------------- PROPIEDADES SERIALIZADAS ----------------------
    [SerializeField][Range(1f, 500f)] private int moveSpeed = 1;
    [SerializeField][Range(2f, 750f)] private int runSpeed = 2;
    //[SerializeField][Range(1f, 200f)] private int MaxSpeed = 5;
    [SerializeField][Range(0.1f, 10f)] private float rotateSpeed = 1f;
    //---------------------- PROPIEDADES PUBLICAS ----------------------
    //---------------------- PROPIEDADES PRIVADAS ----------------------
    //private Rigidbody RB;
    private CharacterController CC;
    private Animator anim;
    private PlayerData playerData;
    private Vector3 playerDirection;

    private float cameraAxisX;
    private float count = 0;
    private float xCountMove = 0;
    private float yCountMove = 0;
    private float xMove = 0;
    private float yMove = 0;
    private bool isRunning = false;

    private bool isHypno;
    public bool IsHypno { get => isHypno; set => isHypno = value; }

    void Start()
    {
        CC = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        playerData = GetComponent<PlayerData>();
        isHypno = false;
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
        }
        else
        {
            Hypnotized();
        }
    }

    private void Move()
    {
        if (isRunning) CC.SimpleMove(playerDirection.normalized * runSpeed);
        else CC.SimpleMove(playerDirection.normalized * moveSpeed);

        /*if (playerDirection != Vector3.zero && RB.velocity.magnitude < MaxSpeed)
        {
            if (isRunning) RB.AddForce(transform.TransformDirection(playerDirection) * moveForce * runForce, ForceMode.Force);
            else RB.AddForce(transform.TransformDirection(playerDirection) * moveForce, ForceMode.Force);
        }*/
    }

    private void RotatePlayer()
    {
        cameraAxisX += Input.GetAxis("Mouse X");
        Quaternion newRotationX = Quaternion.Euler(0, cameraAxisX * rotateSpeed, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotationX, 2.5f * Time.deltaTime);
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

    }

    private void AnimPlayer()
    {
        //Variables para las Animaciones
        yAnimMovement();
        xAnimMovement();

        anim.SetFloat("YSpeed", yMove);
        anim.SetFloat("XSpeed", xMove);
    }

    private void yAnimMovement()
    {
        bool Forward = Input.GetKey(KeyCode.W);
        bool Back = Input.GetKey(KeyCode.S);

        if (Forward)
        {
            yCountMove += Time.deltaTime;
            if (yCountMove >= 1) yCountMove = 1;
            if (isRunning) yCountMove += 1;
        }

        if (Back)
        {
            yCountMove -= Time.deltaTime;
            if (yCountMove <= -1) yCountMove = -1;
            if (isRunning) yCountMove -= 1;
        }

        if (!Forward && !Back)
        {
            if (yCountMove > 0)
            {
                yCountMove -= Time.deltaTime * 3;
                if (yCountMove <= 0) yCountMove = 0;
            }
            else
            {
                yCountMove += Time.deltaTime * 3;
                if (yCountMove >= 0) yCountMove = 0;
            }
        }
        yMove = yCountMove;
    }

    private void xAnimMovement()
    {
        bool Right = Input.GetKey(KeyCode.D);
        bool Left = Input.GetKey(KeyCode.A);

        if (Right)
        {
            xCountMove += Time.deltaTime;
            if (xCountMove >= 1) xCountMove = 1;
            if (isRunning) xCountMove += 1;
        }

        if (Left)
        {
            xCountMove -= Time.deltaTime;
            if (xCountMove <= -1) xCountMove = -1;
            if (isRunning) xCountMove -= 1;
        }

        if (!Right && !Left)
        {
            if (xCountMove > 0)
            {
                xCountMove -= Time.deltaTime * 3;
                if (xCountMove <= 0) xCountMove = 0;
            }
            else
            {
                xCountMove += Time.deltaTime * 3;
                if (xCountMove >= 0) xCountMove = 0;
            }
        }
        xMove = xCountMove;
    }

    private bool IsAnimation(string animName)
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName(animName);
    }

    private void Hypnotized()
    {
        anim.SetFloat("YSpeed", 0.5f);
        CC.SimpleMove(playerDirection.normalized * runSpeed);

        if (count == 0)
        {
            PlayerEvents.OnDamageCall(transform.GetComponent<PlayerDamageSource>().HypnoDamage);
            HUDManager.Instance.SetSelectedText("HIPNOTIZADO");
        }
        count += Time.deltaTime;
        if (count >= 1) count = 0;
    }
}
