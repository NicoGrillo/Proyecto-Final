using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    //
    //MOVIMIENTO DE PATRULLA BASICO DEL ENEMIGO
    //
    [SerializeField] protected BasicEnemyData basicEnemyData;
    [SerializeField] protected GameObject player;
    [SerializeField] protected Animation enemyAnimation;

    private Vector3 originPoint;
    private Vector3 patrolPoint;
    private Vector3 enemyPosition;

    private float resetPatrolPointCount = 0;
    private float patrolPointDistance = 0;

    private int knockedLevel;

    protected bool canMove = true;
    private bool isKnocked = false;
    private bool beingHit = false;

    void Start()
    {
        enemyAnimation = transform.GetChild(0).GetComponent<Animation>();
        originPoint = transform.position;
        knockedLevel = basicEnemyData.KnockedPreset;
        NewPatrolPoint();
    }

    protected virtual void Update()
    {
        CheckIsKnocked();
        PositionsUpdate();
        if (canMove) Patrol();
    }

    protected virtual void Patrol()
    {
        transform.LookAt(patrolPoint);
        transform.Translate(Vector3.forward * basicEnemyData.PatrolSpeed * Time.deltaTime);
        enemyAnimation.Play("Walk");

        resetPatrolPointCount += Time.deltaTime;            //Si se quedo trabado en algun Point se resetea el Point
        if (resetPatrolPointCount >= 4) NewPatrolPoint();

        patrolPointDistance = patrolPoint.magnitude - enemyPosition.magnitude;         //Seteo Distancia como numero positivo
        if (patrolPointDistance < 0) patrolPointDistance = -patrolPointDistance;
        if (patrolPointDistance <= 0.01f) NewPatrolPoint();
    }

    private void NewPatrolPoint()
    {
        enemyAnimation.Play("Idle");
        canMove = false;
        Invoke("CanActionAgain", basicEnemyData.IdleTime);
        resetPatrolPointCount = 0;

        patrolPoint = originPoint + new Vector3(
            Random.Range(-basicEnemyData.PatrolMaxRange, basicEnemyData.PatrolMaxRange),
                0,
                    Random.Range(-basicEnemyData.PatrolMaxRange, basicEnemyData.PatrolMaxRange));
    }

    protected virtual void CanActionAgain()
    {
        canMove = true;
    }

    protected virtual void PositionsUpdate()
    {
        enemyPosition = transform.position;
    }

    private void CheckIsKnocked()
    {

        if (knockedLevel <= 0) IsKnocked();
    }

    private void IsKnocked()
    {
        if (!isKnocked)
        {
            enemyAnimation.Play("Death");
            canMove = false;
            CancelInvoke("CanActionAgain");
            Invoke("WakeUp", basicEnemyData.KnockedTime);
            resetPatrolPointCount = 0;
            isKnocked = true;
            SetColliders(false);
        }
    }

    private void WakeUp()
    {
        CanActionAgain();
        knockedLevel = basicEnemyData.KnockedMaxLevel;
        isKnocked = false;
        SetColliders(true);
    }

    protected virtual void SetColliders(bool value)
    {
        transform.GetComponent<CapsuleCollider>().enabled = value;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("ThrowRock"))
        {
            if (!beingHit && !isKnocked)
            {
                knockedLevel -= 5;
                Destroy(other.gameObject);
                beingHit = true;
                Invoke("canBeingHitAgain", 1f);
            }
        }
    }

    private void canBeingHitAgain()
    {
        beingHit = false;
    }
}
