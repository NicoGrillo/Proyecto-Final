using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent navMeshAgent;
    [SerializeField] protected GameObject player;
    [SerializeField] protected Animation enemyAnimation;
    [Header("Player")]
    [SerializeField] private float distanceToHearPlayer = 5;
    [SerializeField] protected float distanceToFollowPlayer = 10;
    [SerializeField] private float speedToFollowPlayer = 3;
    [SerializeField] private float distanceToAttackPlayer = 1;
    [Header("Patrol")]
    [SerializeField] protected float distanceToPatrol = 5;
    [SerializeField] private float speedToPatrol = 1;
    [Header("Sounds")]
    [SerializeField] private AudioClip[] enemyAudios;
    [SerializeField] protected GameObject enemyAttack;
    [SerializeField] private GameObject enemyPlayerSight;
    [SerializeField] private GameObject enemyRockHit;
    [SerializeField] protected GameObject enemyKnockedSound;

    private AudioSource controlEnemyAudio;
    protected Vector3 PointToPatrol;

    private float xValue = -1f;
    protected float distanceToPlayer = 100;
    private float distanceToPointToPatrol = 0;
    private float resetPatrol = 0;
    protected float soundAttackTime = 1f;

    private bool inSight = false;
    private bool idleState = false;
    protected bool canAttack = true;
    protected bool canMove = true;
    private bool isAudioActive = false;

    protected virtual void Start()
    {
        controlEnemyAudio = GetComponent<AudioSource>();
        enemyAnimation = transform.GetChild(0).GetComponent<Animation>();
        player = GameObject.Find("Player CC");
        StartCoroutine(NewPointToPatrol());
    }

    protected virtual void Update()
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
            if (!isAudioActive) EnemyAudioSelection(-1, 1f);
            navMeshAgent.destination = player.transform.position;
        }
        else
        {
            navMeshAgent.destination = transform.position;
            StopSound();
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
            if (!isAudioActive) EnemyAudioSelection(0, 1f);
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
        StopSound();
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
            if (seePlayer.transform.CompareTag("Player")) InVision(true);
        }
        if (distanceToPlayer > distanceToFollowPlayer) InVision(false);
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
        Destroy(Instantiate(enemyAttack, transform.position, Quaternion.identity), soundAttackTime);
        canMove = false;
        canAttack = false;
        Invoke("resetAttack", 1.5f);
    }

    protected void resetAttack()
    {
        canMove = true;
        canAttack = true;
    }

    protected virtual void InVision(bool value)
    {
        //Destroy(Instantiate(enemyPlayerSight, transform.position, Quaternion.identity), 1f);
        inSight = value;
    }

    protected void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("ThrowRock"))
        {
            RockHit();
            Destroy(other.gameObject);
        }
    }

    protected virtual void RockHit()
    {
        Destroy(Instantiate(enemyRockHit, transform.position, Quaternion.identity), 1f);
    }

    //-----------------------------Sounds--------------------------------------------------------------------------------------------------------------------
    private void EnemyAudioSelection(int index, float volumen)
    {
        if (index != -1)
        {
            controlEnemyAudio.pitch = 1;
            controlEnemyAudio.PlayOneShot(enemyAudios[index], volumen);
        }
        else
        {
            index = 0;
            controlEnemyAudio.pitch = 1.5f;
            controlEnemyAudio.PlayOneShot(enemyAudios[index], volumen);
        }
        isAudioActive = true;
    }

    private void StopSound()
    {
        if (controlEnemyAudio.isPlaying) controlEnemyAudio.Stop();
        isAudioActive = false;
    }

    private void OnEnabled()
    {
        StartCoroutine(NewPointToPatrol());
    }

}
