using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    //---------------------- PROPIEDADES SERIALIZADAS ----------------------
    [SerializeField] GameObject player;
    [SerializeField] GameObject avatar;
    [SerializeField] GameObject ammo;
    [SerializeField] GameObject throwPoint;
    [SerializeField] EnemyTypes enemyTypes;
    [SerializeField][Range(1, 10)] int patrolSpeed = 1;
    [SerializeField][Range(1, 10)] int chaseSpeed = 3;
    [SerializeField][Range(5, 100)] int patrolLimitZone = 10;
    [SerializeField][Range(5, 100)] int chaseZone = 10;
    [SerializeField][Range(1, 10)] int knockedTime = 10;
    //---------------------- PROPIEDADES PUBLICAS ----------------------
    //---------------------- PROPIEDADES PRIVADAS ----------------------
    private Transform playerTransform;
    private Animation avatarAnimation;
    private EnemyData enemyData;
    enum EnemyTypes { Melee, Range, Elusive, Hypno };
    private Vector3 patrolPoint, originPoint, playerPosition, enemyPosition;
    //private Vector3 setThrowPoint;
    private float wayPointDistance, distanceToPlayer, resetPointCount;
    private float minimunDistanceToPlayer;
    private float xValue = -1f;
    private bool canMove, canAttack, alreadyKnocked;
    private bool toPlayer, ammoThrow, inSight;

    //private bool boolean = true;

    void Start()
    {
        playerTransform = player.GetComponent<Transform>();
        avatarAnimation = avatar.GetComponent<Animation>();
        enemyData = GetComponent<EnemyData>();

        originPoint = transform.position;
        canMove = true;
        canAttack = true;
        alreadyKnocked = true;
        inSight = false;
        enemyData.KnockedLevel = 5;
        //setThrowPoint = throwPoint.transform.position;

        NewPatrolPoint();
    }

    void Update()
    {
        CheckIsKnocked();   //Verifico si está noqueado
        PositionReset();    //Actualizo las posiciones del Enemy y del Player
        DistanceToPlayer(); //Seteo la distancia al player
        EnemyType();        //Que tipo de Enemy es

        if (distanceToPlayer < chaseZone && inSight) Chase(toPlayer, ammoThrow, minimunDistanceToPlayer);   //Chequeo si la distancia al player es menor a ChaseZone, si esta cerca Chase
        else if (canMove) Patrol();   //Sino no esta cerca, Patrol
    }

    private void Chase(bool value1, bool value2, float value3)
    //Value1: True corre tras Player, False huye de él
    //Value2: Lanza o no ammo
    //Value3: Distancia minima al player

    {
        if (distanceToPlayer < value3 && canAttack)     //Si está muy cerca, Enemy se detiene a golpear
        {
            avatarAnimation.Play("Attack2");
            canAttack = false;
            canMove = false;
            Invoke("AttackAgain", 2f);
            if (value2) Instantiate(ammo, throwPoint.transform.position, transform.rotation);
        }
        if (canMove)
        {
            transform.LookAt(playerTransform.position);
            if (!value1) transform.Rotate(0, 180, 0);
            avatarAnimation.Play("Run");
            transform.Translate(Vector3.forward * chaseSpeed * Time.deltaTime);
        }
    }

    private void Patrol()
    {
        inSight = false;
        transform.LookAt(patrolPoint);
        transform.Translate(Vector3.forward * patrolSpeed * Time.deltaTime);
        avatarAnimation.Play("Walk");

        resetPointCount += Time.deltaTime;      //Si se quedo trabado en algun Point se resetea el Point
        if (resetPointCount >= 4)
        {
            NewPatrolPoint();
            canMove = false;
            Invoke("Idle", 3);
            resetPointCount = 0;
            avatarAnimation.Play("Idle");
        }

        wayPointDistance = patrolPoint.magnitude - enemyPosition.magnitude;         //Seteo Distancia como numero positivo
        if (wayPointDistance < 0) wayPointDistance = -wayPointDistance;
        if (wayPointDistance <= 0.01f)
        {
            NewPatrolPoint();
            canMove = false;
            Invoke("Idle", 3);
            resetPointCount = 0;
            avatarAnimation.Play("Idle");
        }

    }

    private void EnemyType()
    {
        switch (enemyTypes)
        {
            case EnemyTypes.Melee:
                ammoThrow = false;
                toPlayer = true;
                minimunDistanceToPlayer = 1f;
                break;
            case EnemyTypes.Range:
                ammoThrow = true;
                toPlayer = true;
                minimunDistanceToPlayer = 5f;
                break;
            case EnemyTypes.Elusive:
                ammoThrow = false;
                toPlayer = false;
                minimunDistanceToPlayer = 1f;
                break;
            case EnemyTypes.Hypno:
                ammoThrow = false;
                toPlayer = true;
                minimunDistanceToPlayer = 10f;
                //canAttack = false;
                break;

        }
    }

    private void NewPatrolPoint()
    {
        patrolPoint = originPoint + new Vector3(Random.Range(-patrolLimitZone, patrolLimitZone), 0, Random.Range(-patrolLimitZone, patrolLimitZone));
    }

    private void Idle()
    {
        canMove = true;
    }

    private void AttackAgain()
    {
        canAttack = true;
        canMove = true;
    }

    private void IsKnocked()
    {
        if (alreadyKnocked)
        {
            Debug.Log("enemigo knoqueado");
            canMove = false;
            canAttack = false;
            CancelInvoke();
            Invoke("WakeUp", knockedTime);
            resetPointCount = 0;
            avatarAnimation.Play("Death");
            alreadyKnocked = false;
        }
    }

    private void WakeUp()
    {
        canMove = true;
        canAttack = true;
        enemyData.KnockedLevel = 25;
        alreadyKnocked = true;
    }

    private void CheckIsKnocked()
    {
        if (enemyData.KnockedLevel == 0) IsKnocked();
    }

    private void PositionReset()
    {
        enemyPosition = transform.position;
        playerPosition = playerTransform.position - transform.position;
    }

    private void DistanceToPlayer()
    {
        distanceToPlayer = playerPosition.magnitude;
        if (distanceToPlayer < 0) distanceToPlayer = distanceToPlayer * -1;             //Seteo Distancia como numero positivo

        RaycastHit seePlayer;
        if (Physics.Raycast(new Vector3(transform.position.x, 1, transform.position.z), transform.TransformDirection(viewRange()), out seePlayer, chaseZone, ~(1 << 6))/* ||
        Physics.Raycast(new Vector3(transform.position.x, 1, transform.position.z), transform.TransformDirection(Vector3.forward), out seePlayer, chaseZone, ~(1 << 6))*/)
        {
            if (seePlayer.transform.CompareTag("Player"))
            {
                inSight = true;
            }
        }
    }

    private Vector3 viewRange()
    {
        Vector3 value;
        xValue += .1f;
        value = new Vector3(xValue, 0, 1);
        if (xValue > 1) xValue = -1f;

        /*if (boolean) value = new Vector3(-1, 0, 1);
        else value = new Vector3(1, 0, 1);
        boolean = !boolean;*/

        return value;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 direction = transform.TransformDirection(viewRange()) * chaseZone;
        Gizmos.DrawRay(transform.position, direction);
        /*direction = transform.TransformDirection(Vector3.forward) * chaseZone;
        Gizmos.DrawRay(transform.position, direction);*/
        //Gizmos.DrawLine(shootPoint.position, direction); ESTE GIZMO NO AFECTA LA ROTACION
    }

}

