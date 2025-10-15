using System.Collections.Generic;
using UnityEngine;

public enum DamageType { Physical, Energy, Fire, Ice, Electric }
[CreateAssetMenu(fileName = "AttackData", menuName = "ScriptableObjects/Attack")]
public class AttackData : ScriptableObject
{
    public float minDamage;
    public float maxDamage;
    public DamageType damageType;
    public ProjectileData projectile;
    public float attackSpeed;
    public float lastAttackTime = 0;
    [HideInInspector]
    public List<Projectile> activeProjectiles;

    public int numberOfProjectiles = 1;
    public float spreadAngle = 45;
    public float projectileSize = 1;

    public float GetDamage()
    {
        return Random.Range(minDamage, maxDamage);
    }
}
