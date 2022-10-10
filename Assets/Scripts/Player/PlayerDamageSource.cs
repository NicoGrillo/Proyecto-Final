using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageSource : MonoBehaviour
{
    [SerializeField][Range(1, 50)] private int rangedDamage = 15;
    public int RangedDamage { get { return rangedDamage; } }

    [SerializeField][Range(1, 50)] private int meleeDamage = 25;
    public int MeleeDamage { get { return meleeDamage; } }

    [SerializeField][Range(1, 50)] private int hypnoDamage = 5;
    public int HypnoDamage { get { return hypnoDamage; } }

    [SerializeField][Range(1, 50)] private int meleeBossDamage = 40;
    public int MeleeBossDamage { get { return meleeBossDamage; } }
    
}
