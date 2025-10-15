using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShipBaseStats
{
    [Header("Primary Attributes")]
    public ShipStat basePower = new ShipStat(null, ShipStatType.Power, new List<StatDependancy> { }, 5);
    public ShipStat baseArmor = new ShipStat(null, ShipStatType.Armor, new List<StatDependancy> { }, 5);
    public ShipStat baseHull = new ShipStat(null, ShipStatType.Hull, new List<StatDependancy> { }, 5);
    public ShipStat baseSpeed = new ShipStat(null, ShipStatType.Speed, new List<StatDependancy> { }, 5);
    public ShipStat baseShields = new ShipStat(null, ShipStatType.Shields, new List<StatDependancy> { }, 5);
    public ShipStat baseSensors = new ShipStat(null, ShipStatType.Sensors, new List<StatDependancy> { }, 5);
    public ShipStat baseLuck = new ShipStat(null, ShipStatType.Luck, new List<StatDependancy> { }, 5);
    public ShipStat baseCharisma = new ShipStat(null, ShipStatType.Charisma, new List<StatDependancy> { }, 5);
    [Header("Attack Attributes")]

    public ShipStat baseDamageBonus = new ShipStat(null, ShipStatType.DamageBonus, new List<StatDependancy> {
        new StatDependancy(ShipStatType.Power, 5f)}, 0);
    public ShipStat baseCritChanceBonus = new ShipStat(null, ShipStatType.CritChanceBonus, new List<StatDependancy> {
        new StatDependancy(ShipStatType.Luck, 1),
        new StatDependancy(ShipStatType.Sensors, 1)}, 5);
    public ShipStat baseCritDamageBonus = new ShipStat(null, ShipStatType.CritDamageBonus, new List<StatDependancy> {
        new StatDependancy(ShipStatType.Power, 5f)}, 50);
    public ShipStat baseAttackSpeedBonus = new ShipStat(null, ShipStatType.AttackSpeedBonus, new List<StatDependancy> {
        new StatDependancy(ShipStatType.Speed, 1f)}, 0);
    public ShipStat baseAccuracy = new ShipStat(null, ShipStatType.Accuracy, new List<StatDependancy> {
        new StatDependancy(ShipStatType.Sensors, 1f)}, 50);
    public ShipStat baseProjectileSpeedBonus = new ShipStat(null, ShipStatType.ProjectileSpeedBonus, new List<StatDependancy> { }, 0);
    public ShipStat baseProjectilePierceNumberBonus = new ShipStat(null, ShipStatType.ProjectilePierceNumberBonus, new List<StatDependancy> { }, 0);
    public ShipStat baseProjectileChainNumberBonus = new ShipStat(null, ShipStatType.ProjectileChainNumberBonus, new List<StatDependancy> { }, 0);
    [Header("Defense Attributes")]
    public ShipStat baseMoveSpeed = new ShipStat(null, ShipStatType.MoveSpeed, new List<StatDependancy> {
        new StatDependancy(ShipStatType.Speed, .05f)}, 1);
    public ShipStat baseSize = new ShipStat(null, ShipStatType.Size, new List<StatDependancy> { }, 1);

    public ShipStat baseDamageReduction = new ShipStat(null, ShipStatType.DamageReduction, new List<StatDependancy> {
        new StatDependancy(ShipStatType.Armor, 1f)}, 0);
    public ShipStat baseEvasion = new ShipStat(null, ShipStatType.Evasion, new List<StatDependancy> {
        new StatDependancy(ShipStatType.Luck, 1f),
        new StatDependancy(ShipStatType.Sensors, 1f)}, 5);
    public ShipStat baseStatusDurationReduction = new ShipStat(null, ShipStatType.StatusDurationReduction, new List<StatDependancy> { }, 0);
    [Header("Health Attributes")]

    public ShipStat baseMaxHealth = new ShipStat(null, ShipStatType.MaxHealth, new List<StatDependancy> {
        new StatDependancy(ShipStatType.Hull, 5f)}, 10);
    public ShipStat baseHealthRegen = new ShipStat(null, ShipStatType.HealthRegen, new List<StatDependancy> {
        new StatDependancy(ShipStatType.Hull, 0.05f)}, 0);
    public ShipStat baseHealingBonus = new ShipStat(null, ShipStatType.HealingBonus, new List<StatDependancy> { }, 0);
    [Header("Shield Attributes")]

    public ShipStat baseMaxShields = new ShipStat(null, ShipStatType.MaxShields, new List<StatDependancy> {
        new StatDependancy(ShipStatType.Shields, 1f)}, 5);
    public ShipStat baseShieldRegen = new ShipStat(null, ShipStatType.ShieldRegen, new List<StatDependancy> {
        new StatDependancy(ShipStatType.Shields, 0.05f)}, 0);
    public ShipStat baseShieldRegenTime = new ShipStat(null, ShipStatType.ShieldRegenTime, new List<StatDependancy> {
        new StatDependancy(ShipStatType.Shields, -0.01f) }, 5);

    [Header("Other Attributes")]

    public ShipStat baseStatusDurationBonus = new ShipStat(null, ShipStatType.StatusDurationBonus, new List<StatDependancy> { }, 0);
    public ShipStat baseCurrencyBonus = new ShipStat(null, ShipStatType.CurrencyBonus, new List<StatDependancy> {
        new StatDependancy(ShipStatType.Charisma, 1f)}, 0);
    public ShipStat baseXPBonus = new ShipStat(null, ShipStatType.XpBonus, new List<StatDependancy> { }, 0);
    public ShipStat baseCrewCapacity = new ShipStat(null, ShipStatType.CrewCapacity, new List<StatDependancy> {
        new StatDependancy(ShipStatType.Charisma, 0.2f)}, 5);

    [Header("New and Unsorted Attributes")]
    public ShipStat baseProjectileSplit = new ShipStat(null, ShipStatType.ProjectileSplitNumberBonus, new List<StatDependancy> { }, 0);
    public ShipStat baseLifeSteal = new ShipStat(null, ShipStatType.LifeSteal, new List<StatDependancy> { }, 0);
    public ShipStat baseIgnoreArmor = new ShipStat(null, ShipStatType.IgnoreArmor, new List<StatDependancy> { }, 0);
    public ShipStat baseIgnoreShields = new ShipStat(null, ShipStatType.IgnoreShields, new List<StatDependancy> { }, 0);
    public ShipStat baseShieldDrain = new ShipStat(null, ShipStatType.ShieldDrain, new List<StatDependancy> { }, 0);
    public ShipStat baseShieldBurn = new ShipStat(null, ShipStatType.ShieldBurn, new List<StatDependancy> { }, 0);
    public ShipStat baseProjectileAcceleration = new ShipStat(null, ShipStatType.ProjectileAccelerationBonus, new List<StatDependancy> { }, 0);
    public ShipStat baseAdditionalProjectiles = new ShipStat(null, ShipStatType.AdditionalProjectilesBonus, new List<StatDependancy> { }, 0);



}
