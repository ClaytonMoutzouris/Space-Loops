using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

//These should all be floats, to make sure it covers any kind of number we need
public enum StatType { Attack, Defense, Speed, Health, MoveSpeed, MaxHealth, CurrentHealth, DamageBonus, Regen, AttackSpeed, DamageReduction, Size, Shields, ShieldsMax, ShieldRegen, ShieldRegenTime, CrewCapacity, Currency, Charisma, Accuracy, Evasion, Count }
public enum PrimaryAttribute { Attack, Defense, Speed, Regen, Health, Shield };

[System.Serializable]
public class ShipStat
{
    public List<StatDependancy> statDependancies;
    public ShipStatType type;
    public float baseValue;
    [HideInInspector]
    public List<StatBonus> bonuses;
    //Refence to the parent stats, mostly for secondary stats
    public ShipStats stats;

    public ShipStat(ShipStats pStats, ShipStatType t, List<StatDependancy> dependencies, float startingValue = 5)
    {
        stats = pStats;
        statDependancies = dependencies;
        //type = t;
        baseValue = startingValue;
        bonuses = new List<StatBonus>();
    }

    public void AddBonus(StatBonus bonus)
    {
        bonuses.Add(bonus);
    }

    public void RemoveBonus(StatBonus bonus)
    {
        bonuses.Remove(bonus);
    }

    public float GetBaseValue()
    {
        return baseValue;
    }

    public virtual float GetValue()
    {
        float fullValue = baseValue;
        float percentBonusMultiplier = 0;
        float multiplier = 0;

        foreach (StatDependancy dependency in statDependancies)
        {
            fullValue += stats.GetStat(dependency.type).GetValue() * dependency.valuePerStat;
        }

        foreach (StatBonus bonus in bonuses)
        {
            switch (bonus.modType)
            {
                case StatModType.FlatBonus:
                    fullValue += bonus.bonusValue;
                    break;
                case StatModType.Multiplier:
                    multiplier += bonus.bonusValue;
                    break;
                case StatModType.PercentBonus:
                    percentBonusMultiplier += bonus.bonusValue;
                    break;
                default:

                    break;
            }
        }

        //
        fullValue += fullValue * (percentBonusMultiplier / 100);

        return fullValue + fullValue * multiplier;
    }

    public static string AddSpacesBeforeCapitals(string input)
    {
        // Use a negative lookbehind to ensure the capital letter is not at the start of the string
        // and a positive lookbehind to ensure it's preceded by a lowercase letter.
        return Regex.Replace(input, "(?<!^)(?=[A-Z])", " ");
    }

    public static string StringForType(ShipStatType type)
    {
        return AddSpacesBeforeCapitals(type.ToString());
    }

}

