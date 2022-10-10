using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCC : MonoBehaviour
{
    //---------------------- PROPIEDADES SERIALIZADAS ----------------------
    [SerializeField][Range(1f, 500f)] private int moveSpeed = 1;
    [SerializeField][Range(2f, 750f)] private int runSpeed = 2;
    [SerializeField][Range(0.1f, 10f)] private float rotateSpeed = 1f;
    [SerializeField][Range(0.5f, 5)] private float hypnoDelay = 1;
    [SerializeField] private new Transform camera;
    [SerializeField] private GameObject sunController;
    //---------------------- PROPIEDADES PUBLICAS ----------------------
    //---------------------- PROPIEDADES PRIVADAS ----------------------
    private CharacterController CC;
    private Animator anim;
    private PlayerData playerData;
    private RocksThrow rocksThrow;
    private PlayerSoundManager playerSoundManager;
    private Vector3 playerDirection;

    private float cameraAxisX;
    private float cameraAxisY;
    private float xCountMove = 0;
    private float yCountMove = 0;
    private float xMove = 0;
    private float yMove = 0;

    private bool isRunning = false;
    private bool isAudioActive = false;
    private bool isIdle = true;

    private bool cantMove;
    public bool IsHypno { get => cantMove; set => cantMove = value; }

    private void Awake()
    {
        PlayerEvents.OnStateHypno += Hypnotized;
        PlayerEvents.OnCantMove += PlayerMove;
    }

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        CC = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        playerData = GetComponent<PlayerData>();
        rocksThrow = GetComponent<RocksThrow>();
        playerSoundManager = GetComponent<PlayerSoundManager>();
        cantMove = false;
    }

    void Update()
    {
        if (!cantMove)
        {
            CameraPlayer();
            WalkOrRun();
            AnimPlayer();
            InputsPlayer();

        }
        else AnimPlayer();
    }

    void FixedUpdate()
    {
        if (!cantMove)
        {
            Move();
        }
        else CC.SimpleMove(Vector3.forward * 0f);
    }

    private void Move()
    {
        if (isRunning) CC.SimpleMove(transform.TransformDirection(playerDirection) * runSpeed);
        else CC.SimpleMove(transform.TransformDirection(playerDirection) * moveSpeed);
    }

    private void CameraPlayer()
    {
        cameraAxisX = Input.GetAxis("Mouse X");
        cameraAxisY = Input.GetAxis("Mouse Y");

        if (cameraAxisX != 0)
        {
            transform.Rotate(Vector3.up * cameraAxisX * rotateSpeed);
        }

        if (cameraAxisY != 0)
        {
            float angle = (camera.localEulerAngles.x - cameraAxisY * rotateSpeed + 360) % 360;
            if (angle > 180) angle -= 360;
            angle = Mathf.Clamp(angle, -25f, 45f);
            camera.localEulerAngles = Vector3.right * angle;
        }
    }

    private void WalkOrRun()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            AudioPlayer(-1);
        }
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = false;
            AudioPlayer(0);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift)) StopAudio();
        if (Input.GetKeyUp(KeyCode.LeftShift)) StopAudio();
    }

    private void InputsPlayer()
    {
        playerDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) playerDirection += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) playerDirection += Vector3.back;
        if (Input.GetKey(KeyCode.A)) playerDirection += Vector3.left;
        if (Input.GetKey(KeyCode.D)) playerDirection += Vector3.right;

        if (Input.GetKeyDown(KeyCode.F1)) sunController.GetComponent<SunController>().selectCase(1);
        if (Input.GetKeyDown(KeyCode.F2)) sunController.GetComponent<SunController>().selectCase(2);
        if (Input.GetKeyDown(KeyCode.F3)) sunController.GetComponent<SunController>().selectCase(3);
    }

    private void AnimPlayer()
    {
        //Variables para las Animaciones
        yAnimMovement();
        xAnimMovement();

        anim.SetFloat("YSpeed", yMove);
        anim.SetFloat("XSpeed", xMove);
    }

    private void AudioPlayer(int index)
    {
        if (playerDirection != Vector3.zero)
        {
            if (!isAudioActive)
            {
                playerSoundManager.PlayerAudioSelection(index, 0.3f);
                isAudioActive = true;
                isIdle = false;
            }
        }
        else
        {
            if (!isIdle)
            {
                isIdle = true;
                StopAudio();
            }
        }
    }

    private void StopAudio()
    {
        playerSoundManager.StopSound();
        isAudioActive = false;
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

    private void Hypnotized()
    {
        cantMove = true;

        StartCoroutine(HypnoState());

        Invoke("HypnoDelay", hypnoDelay);
    }

    private void HypnoDelay()
    {
        playerData.FearLVL = 0;
        HUDManager.SetFearBar(0);
        cantMove = false;
    }

    private void PlayerMove(bool value)
    {
        cantMove = value;
        StopAudio();
    }

    private void OnDisable()
    {
        PlayerEvents.OnCantMove -= PlayerMove;
        PlayerEvents.OnStateHypno -= Hypnotized;
    }

    IEnumerator HypnoState()
    {
        PlayerEvents.OnDamageCall(transform.GetComponent<PlayerDamageSource>().HypnoDamage);
        yield return new WaitForSeconds(1);
        PlayerEvents.OnDamageCall(transform.GetComponent<PlayerDamageSource>().HypnoDamage);
        yield return new WaitForSeconds(1);
        PlayerEvents.OnDamageCall(transform.GetComponent<PlayerDamageSource>().HypnoDamage);
    }
}
