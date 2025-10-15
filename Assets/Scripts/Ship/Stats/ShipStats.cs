using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStats
{

    public Dictionary<ShipStatType, ShipStat> stats;
    //public BaseStats baseStats;

    //Need a way to set some base stats
    public ShipStats()
    {

        //weaponBonuses = new List<WeaponAttributeBonus>();

    }

    /*
    public ShipStats(BasePlayerStats basePlayerStats)
    {


        //baseStats = Instantiate(baseStats);
        stats = new Dictionary<StatType, ShipStat>();

        foreach (ShipStat stat in basePlayerStats.startingStats)
        {
            stats.Add(stat.type, new ShipStat(this, stat.type, stat.statDependancies, stat.baseValue));
        }

        stats[StatType.CurrentStamina].baseValue = stats[StatType.MaxStamina].GetValue();

        //weaponBonuses = new List<WeaponAttributeBonus>();

    }
    */

    public void SetStats(List<ShipStat> stats)
    {
        //this.primaryStats.Clear();
        this.stats = new Dictionary<ShipStatType, ShipStat>();

        foreach (ShipStat stat in stats)
        {
            stat.stats = this;
            //this.stats[stat.type] = stat;
            this.stats.Add(stat.type, stat);

        }

    }

    public void SetStats(ShipBaseStats baseStats)
    {
        //this.primaryStats.Clear();
        this.stats = new Dictionary<ShipStatType, ShipStat>();
        stats.Add(ShipStatType.Power, baseStats.basePower);
        baseStats.basePower.stats = this;
        stats.Add(ShipStatType.Armor, baseStats.baseArmor);
        baseStats.baseArmor.stats = this;

        stats.Add(ShipStatType.Hull, baseStats.baseHull);
        baseStats.baseHull.stats = this;

        stats.Add(ShipStatType.Speed, baseStats.baseSpeed);
        baseStats.baseSpeed.stats = this;

        stats.Add(ShipStatType.Shields, baseStats.baseShields);
        baseStats.baseShields.stats = this;

        stats.Add(ShipStatType.Sensors, baseStats.baseSensors);
        baseStats.baseSensors.stats = this;

        stats.Add(ShipStatType.Luck, baseStats.baseLuck);
        baseStats.baseLuck.stats = this;

        stats.Add(ShipStatType.Charisma, baseStats.baseCharisma);
        baseStats.baseCharisma.stats = this;

        stats.Add(ShipStatType.DamageBonus, baseStats.baseDamageBonus);
        baseStats.baseDamageBonus.stats = this;

        stats.Add(ShipStatType.CritChanceBonus, baseStats.baseCritChanceBonus);
        baseStats.baseCritChanceBonus.stats = this;

        stats.Add(ShipStatType.CritDamageBonus, baseStats.baseCritDamageBonus);
        baseStats.baseCritDamageBonus.stats = this;

        stats.Add(ShipStatType.AttackSpeedBonus, baseStats.baseAttackSpeedBonus);
        baseStats.baseAttackSpeedBonus.stats = this;

        stats.Add(ShipStatType.Accuracy, baseStats.baseAccuracy);
        baseStats.baseAccuracy.stats = this;

        stats.Add(ShipStatType.ProjectileSpeedBonus, baseStats.baseProjectileSpeedBonus);
        baseStats.baseProjectileSpeedBonus.stats = this;

        stats.Add(ShipStatType.ProjectilePierceNumberBonus, baseStats.baseProjectilePierceNumberBonus);
        baseStats.baseProjectilePierceNumberBonus.stats = this;

        stats.Add(ShipStatType.ProjectileChainNumberBonus, baseStats.baseProjectileChainNumberBonus);
        baseStats.baseProjectileChainNumberBonus.stats = this;

        stats.Add(ShipStatType.MoveSpeed, baseStats.baseMoveSpeed);
        baseStats.baseMoveSpeed.stats = this;

        stats.Add(ShipStatType.Size, baseStats.baseSize);
        baseStats.baseSize.stats = this;

        stats.Add(ShipStatType.DamageReduction, baseStats.baseDamageReduction);
        baseStats.baseDamageReduction.stats = this;

        stats.Add(ShipStatType.Evasion, baseStats.baseEvasion);
        baseStats.baseEvasion.stats = this;

        stats.Add(ShipStatType.StatusDurationReduction, baseStats.baseStatusDurationReduction);
        baseStats.baseStatusDurationReduction.stats = this;

        stats.Add(ShipStatType.MaxHealth, baseStats.baseMaxHealth);
        baseStats.baseMaxHealth.stats = this;

        stats.Add(ShipStatType.HealthRegen, baseStats.baseHealthRegen);
        baseStats.baseHealthRegen.stats = this;

        stats.Add(ShipStatType.HealingBonus, baseStats.baseHealingBonus);
        baseStats.baseHealingBonus.stats = this;

        stats.Add(ShipStatType.MaxShields, baseStats.baseMaxShields);
        baseStats.baseMaxShields.stats = this;

        stats.Add(ShipStatType.ShieldRegen, baseStats.baseShieldRegen);
        baseStats.baseShieldRegen.stats = this;

        stats.Add(ShipStatType.ShieldRegenTime, baseStats.baseShieldRegenTime);
        baseStats.baseShieldRegenTime.stats = this;

        stats.Add(ShipStatType.StatusDurationBonus, baseStats.baseStatusDurationBonus);
        baseStats.baseStatusDurationBonus.stats = this;

        stats.Add(ShipStatType.CurrencyBonus, baseStats.baseCurrencyBonus);
        baseStats.baseCurrencyBonus.stats = this;

        stats.Add(ShipStatType.XpBonus, baseStats.baseXPBonus);
        baseStats.baseXPBonus.stats = this;

        stats.Add(ShipStatType.CrewCapacity, baseStats.baseCrewCapacity);
        baseStats.baseCrewCapacity.stats = this;

        stats.Add(ShipStatType.ProjectileSplitNumberBonus, baseStats.baseProjectileSplit);
        baseStats.baseProjectileSplit.stats = this;

        stats.Add(ShipStatType.LifeSteal, baseStats.baseLifeSteal);
        baseStats.baseLifeSteal.stats = this;

        stats.Add(ShipStatType.IgnoreArmor, baseStats.baseIgnoreArmor);
        baseStats.baseIgnoreArmor.stats = this;

        stats.Add(ShipStatType.IgnoreShields, baseStats.baseIgnoreShields);
        baseStats.baseIgnoreShields.stats = this;

        stats.Add(ShipStatType.ShieldDrain, baseStats.baseShieldDrain);
        baseStats.baseShieldDrain.stats = this;

        stats.Add(ShipStatType.ShieldBurn, baseStats.baseShieldBurn);
        baseStats.baseShieldBurn.stats = this;

        stats.Add(ShipStatType.ProjectileAccelerationBonus, baseStats.baseProjectileAcceleration);
        baseStats.baseProjectileAcceleration.stats = this;

        stats.Add(ShipStatType.AdditionalProjectilesBonus, baseStats.baseAdditionalProjectiles);
        baseStats.baseAdditionalProjectiles.stats = this;

        /*
        foreach (ShipBaseStats baseStats in stats)
        {
            stat.stats = this;
            //this.stats[stat.type] = stat;
            this.stats.Add(stat.type, stat);

        }
        */
    }

    public ShipStat GetStat(ShipStatType type)
    {
        return stats[type];
    }

    public void AddBonus(StatBonus bonus)
    {
        stats[bonus.type].AddBonus(bonus);
    }

    public void RemoveBonus(StatBonus bonus)
    {
        stats[bonus.type].RemoveBonus(bonus);
    }

    public void AddBonuses(List<StatBonus> bonuses)
    {
        foreach (StatBonus bonus in bonuses)
        {
            stats[bonus.type].AddBonus(bonus);
        }

    }

    public void RemoveBonuses(List<StatBonus> bonuses)
    {
        foreach (StatBonus bonus in bonuses)
        {
            stats[bonus.type].RemoveBonus(bonus);
        }

    }

}





