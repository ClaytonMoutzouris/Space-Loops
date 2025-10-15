using UnityEngine;

//The multiplier mod type is really an additive multiplier. Adding a .5 mod adds 50%, it doesn't halve our value. That would be -.5
public enum StatModType { FlatBonus, Multiplier, PercentBonus }

[System.Serializable]
public class StatBonus
{
    public StatModType modType = StatModType.FlatBonus;
    public ShipStatType type;
    public float bonusValue;

    public StatBonus(ShipStatType t, float min, StatModType modType = StatModType.FlatBonus)
    {
        type = t;
        bonusValue = min;
        this.modType = modType;
    }

    public string GetTooltip()
    {
        string tooltip = "";
        if (bonusValue >= 0)
        {
            tooltip += "+";
        }

        switch (modType)
        {
            case StatModType.FlatBonus:
                tooltip += bonusValue + " " + ShipStat.StringForType(type);
                break;
            case StatModType.Multiplier:
                tooltip += ShipStat.StringForType(type) + " x" + bonusValue;
                break;
            case StatModType.PercentBonus:
                tooltip += bonusValue * 100 + "% " + ShipStat.StringForType(type);
                break;
        }

        return tooltip;
    }

    public StatBonus CopyBonus() {
        return new StatBonus(type, bonusValue, modType);
    }
}