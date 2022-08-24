using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    //---------------------- PROPIEDADES SERIALIZADAS ----------------------
    [SerializeField] GameObject player;
    [SerializeField] GameObject avatar;
    [SerializeField][Range(1f, 10f)] int patrolSpeed = 1;
    [SerializeField][Range(1f, 10f)] int chaseSpeed = 3;
    [SerializeField][Range(5f, 100f)] int patrolLimitZone = 10;
    [SerializeField][Range(5f, 100f)] int chaseZone = 10;
    //---------------------- PROPIEDADES PUBLICAS ----------------------
    public float knockedTime = 1f;
    //---------------------- PROPIEDADES PRIVADAS ----------------------
    private Vector3 patrolPoint, idlePoint, playerPosition, enemyPosition;
    private float wayPointDistance, distanceToPlayer, resetPointCount;
    private Transform playerTransform;
    private Animation ghoulAnimation;
    private bool canMove, canAttack, alreadyKnocked;
    private EnemyData enemyData;

    void Start()
    {
        playerTransform = player.GetComponent<Transform>();
        ghoulAnimation = avatar.GetComponent<Animation>();
        enemyData = GetComponent<EnemyData>();

        enemyPosition = transform.position;
        idlePoint = enemyPosition;
        canMove = true;
        canAttack = true;
        alreadyKnocked = true;
        enemyData.KnockedLevel = 5;

        NewPatrolPoint();
    }

    void Update()
    {
        if (enemyData.KnockedLevel == 0) IsKnocked();

        //Actualizo las posiciones del Enemy y del Player
        enemyPosition = transform.position;
        playerPosition = playerTransform.position - transform.position;

        //Chequeo si la distancia al player es menor a ChaseZone
        distanceToPlayer = playerPosition.magnitude;
        if (distanceToPlayer < 0) distanceToPlayer = distanceToPlayer * -1;             //Seteo Distancia como numero positivo

        if (distanceToPlayer < chaseZone)   //Si está cerca, Enemy caza
        {
            if (distanceToPlayer < 1f && canAttack)     //Si está muy cerca, Enemy se detiene a golpear
            {
                ghoulAnimation.Play("Attack2");
                canAttack = false;
                canMove = false;
                Invoke("AttackAgain", 2f);
            }
            if (canMove)
            {
                transform.LookAt(playerTransform.position);
                Chase();
            }
        }
        else    //Sino no esta cerca, Enemy patrulla
        {
            if (canMove) Patrol();
        }
    }

    private void Chase()
    {
        ghoulAnimation.Play("Run");
        transform.Translate(Vector3.forward * chaseSpeed * Time.deltaTime);
    }

    private void Patrol()
    {
        transform.LookAt(patrolPoint);
        transform.Translate(Vector3.forward * patrolSpeed * Time.deltaTime);
        ghoulAnimation.Play("Walk");

        resetPointCount += Time.deltaTime;      //Si se quedo trabado en algun Point se resetea el Point
        if (resetPointCount >= 4)
        {
            NewPatrolPoint();
            canMove = false;
            Invoke("Idle", 3);
            resetPointCount = 0;
            ghoulAnimation.Play("Idle");
        }

        wayPointDistance = patrolPoint.magnitude - enemyPosition.magnitude;         //Seteo Distancia como numero positivo
        if (wayPointDistance < 0) wayPointDistance = -wayPointDistance;
        if (wayPointDistance <= 0.01f)
        {
            NewPatrolPoint();
            canMove = false;
            Invoke("Idle", 3);
            resetPointCount = 0;
            ghoulAnimation.Play("Idle");
        }
    }

    private void NewPatrolPoint()
    {
        patrolPoint = idlePoint + new Vector3(Random.Range(-patrolLimitZone, patrolLimitZone), 0, Random.Range(-patrolLimitZone, patrolLimitZone));
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
            Invoke("WakeUp", 10);
            resetPointCount = 0;
            ghoulAnimation.Play("Death");
            alreadyKnocked = false;
            //Invoke("StopAnim", knockedTime);
        }
    }

    private void WakeUp()
    {
        canMove = true;
        enemyData.KnockedLevel = 25;
        alreadyKnocked = true;
    }

    private void StopAnim()
    {
        ghoulAnimation.Stop();
    }
}

