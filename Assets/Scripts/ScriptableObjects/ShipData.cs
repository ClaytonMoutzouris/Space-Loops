using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipData", menuName = "ScriptableObjects/ShipData")]
public class ShipData : ScriptableObject
{
    public int maxHealth;

    public Vector2 shipSize;
    public string shipName;
    public WeaponData defaultWeapon;

    public AttackData attack;
    public AttackData attack2;
    public float currentHeath;
    public float currentShields;
    public ShipHostility hosility;
    public ShipController shipPrefab;
    public List<Sprite> possibleSprites;
    public List<Color> possibleColors;
    public float amplitude = 2f;

    public List<CrewMember> startingCrew;

    public int currency = 0;
    public List<ShipAbility> baseAbilities = new List<ShipAbility>();

    //[Header("Stats")]
    public ShipStat[] startingStats = new ShipStat[(int)StatType.Count];

    [Header("Base Stats")]
    public ShipBaseStats baseStats;

    public ShipStats GetStats()
    {
        ShipStats newStats = new ShipStats();
        newStats.SetStats(baseStats);

        return newStats;

    }

    public LootTable lootTable;

}
