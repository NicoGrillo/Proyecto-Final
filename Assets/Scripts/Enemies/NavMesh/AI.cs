using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] protected GameObject player;
    [SerializeField] protected Animation enemyAnimation;
    [Header("Player")]
    [SerializeField] private float distanceToHearPlayer = 5;
    [SerializeField] private float distanceToFollowPlayer = 10;
    [SerializeField] private float speedToFollowPlayer = 3;
    [SerializeField] private float distanceToAttackPlayer = 1;
    [Header("Patrol")]
    [SerializeField] protected float distanceToPatrol = 5;
    [SerializeField] private float speedToPatrol = 1;

    protected Vector3 PointToPatrol;

    private float xValue = -1f;
    private float distanceToPlayer = 0;
    private float distanceToPointToPatrol = 0;
    private float resetPatrol = 0;

    private bool inSight = false;
    private bool idleState = false;
    protected bool canAttack = true;
    protected bool canMove = true;

    protected virtual void Start()
    {
        enemyAnimation = transform.GetChild(0).GetComponent<Animation>();
        player = GameObject.Find("Player CC");
        StartCoroutine(NewPointToPatrol());
    }

    void Update()
    {
        EnemyAction();
    }

    void EnemyAction()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        PlayerOnSight();

        if (inSight) FollowPlayer();
        else if (distanceToPlayer < distanceToHearPlayer) FollowPlayer();
        else EnemyPatrol();
    }

    void FollowPlayer()
    {
        navMeshAgent.speed = speedToFollowPlayer;
        if (distanceToPlayer > distanceToAttackPlayer && canMove)
        {
            enemyAnimation.Play("Run");
            navMeshAgent.destination = player.transform.position;
        }
        else
        {
            navMeshAgent.destination = transform.position;
            if (canAttack)
            {
                Attack();
            }
        }
    }

    void EnemyPatrol()
    {
        if (!idleState)
        {
            navMeshAgent.speed = speedToPatrol;
            enemyAnimation.Play("Walk");
            resetPatrol += Time.deltaTime;
            distanceToPointToPatrol = Vector3.Distance(transform.position, PointToPatrol);
            if (distanceToPointToPatrol < 0.3f || resetPatrol >= 5) StartCoroutine(NewPointToPatrol());
            else navMeshAgent.destination = PointToPatrol;
        }
    }

    IEnumerator NewPointToPatrol()
    {
        PatrolType();
        navMeshAgent.destination = transform.position;
        idleState = true;
        enemyAnimation.Play("Idle");
        resetPatrol = 0;
        yield return new WaitForSeconds(2);
        idleState = false;
    }

    protected virtual void PatrolType()
    {
        PointToPatrol = transform.position + new Vector3(Random.Range(-distanceToPatrol, distanceToPatrol), 0, (Random.Range(-distanceToPatrol, distanceToPatrol)));
    }

    private void PlayerOnSight()
    {
        RaycastHit seePlayer;
        if (Physics.Raycast(new Vector3(transform.position.x, 1, transform.position.z),
                transform.TransformDirection(viewRange()),
                    out seePlayer,
                        distanceToFollowPlayer,
                            ~(1 << 6)))
        {
            if (seePlayer.transform.CompareTag("Player")) inSight = true;
        }
        if (distanceToPlayer > distanceToFollowPlayer) inSight = false;
    }

    private Vector3 viewRange()
    {
        Vector3 value;
        xValue += .1f;
        value = new Vector3(xValue, 0, 1);
        if (xValue > 1) xValue = -1f;

        return value;
    }

    protected virtual void Attack()
    {
        enemyAnimation.Play("Attack1");
        canMove = false;
        canAttack = false;
        Invoke("resetAttack", 1.5f);
    }

    protected void resetAttack()
    {
        canMove = true;
        canAttack = true;
    }
}
