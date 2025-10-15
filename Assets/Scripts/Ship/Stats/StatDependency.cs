using UnityEngine;

public enum StatDependencyType { Flat, Multiplier }

[System.Serializable]
public class StatDependancy
{
    public ShipStatType type;
    public StatDependencyType dependencyType;
    public float valuePerStat;

    public StatDependancy(ShipStatType t, float min, StatDependencyType dType = StatDependencyType.Flat)
    {
        type = t;
        valuePerStat = min;
    }

}
