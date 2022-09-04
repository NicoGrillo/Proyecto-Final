using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Basic Enemy Data", menuName = "Enemies/Basic Data")]
public class BasicEnemyData : ScriptableObject
{
    [Header("Movement Setup")]
    [Tooltip("Patrol Speed (1 to 10)")]
    [SerializeField][Range(1, 10)] private int patrolSpeed = 1;
    public int PatrolSpeed { get => patrolSpeed; set => patrolSpeed = value; }

    [Tooltip("Chase Speed (1 to 10)")]
    [SerializeField][Range(1, 10)] private int chaseSpeed = 1;
    public int ChaseSpeed { get => chaseSpeed; set => chaseSpeed = value; }

    [Tooltip("Max Patrol Range (5 to 100)")]
    [SerializeField][Range(5, 100)] private int patrolMaxRange = 5;
    public int PatrolMaxRange { get => patrolMaxRange; set => patrolMaxRange = value; }

    [Tooltip("Max Chase Range (5 to 200)")]
    [SerializeField][Range(5, 200)] private int chaseMaxRange = 5;
    public int ChaseMaxRange { get => chaseMaxRange; set => chaseMaxRange = value; }

    [Tooltip("Idle Time in Seconds (1 to 15)")]
    [SerializeField][Range(1, 15)] private int idleTime = 1;
    public int IdleTime { get => idleTime; set => idleTime = value; }

    [Header("Mechanical Configuration")]
    [Tooltip("Knocked Time in Seconds (1 to 30)")]
    [SerializeField][Range(1, 30)] private int knockedTime = 1;
    public int KnockedTime { get => knockedTime; set => knockedTime = value; }

    [Tooltip("Knocked Preset Level  (0 to 100)")]
    [SerializeField][Range(0, 100)] private int knockedPreset = 5;
    public int KnockedPreset { get { return knockedPreset; } set { knockedPreset = value; } }

    [Tooltip("Max Knocked Level (5 to 100)")]
    [SerializeField][Range(5, 100)] private int knockedMaxLevel = 5;
    public int KnockedMaxLevel { get => knockedMaxLevel; set => knockedMaxLevel = value; }

    [Header("Attack Configuration")]
    [Tooltip("Minimum Distance to Attack (1 to 20)")]
    [SerializeField][Range(1, 20)] private int minToPlayer = 1;
    public int MinToPlayer { get => minToPlayer; set => minToPlayer = value; }

    [Tooltip("Time Between Attacks in Seconds (1 to 10)")]
    [SerializeField][Range(1, 10)] private int attackTime = 1;
    public int AttackTime { get => attackTime; set => attackTime = value; }

}
