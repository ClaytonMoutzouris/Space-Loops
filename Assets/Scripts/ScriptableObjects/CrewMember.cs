using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public enum CrewRole { Engineer, Security, Captain, Pilot, Medic, Scientist }

[CreateAssetMenu(fileName = "CrewMember", menuName = "ScriptableObjects/CrewMember")]
public class CrewMember : ScriptableObject
{
    public string crewName;
    public CrewRole role;
    public Color color;

    public List<StatBonus> statsBonuses;
    public CrewManager crew;
}
