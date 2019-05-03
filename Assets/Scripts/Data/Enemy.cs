using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType { Simple,Small,Large }

[CreateAssetMenu(fileName ="Enemy",menuName ="Western/Enemy",order =1)]
public class Enemy : ScriptableObject
{
    public float range;
    public float moveSpeed;
    public float bulletSpeed;
    public float chargeDuration;
    public float shootDuration;
    public float bulletDamage;
}
