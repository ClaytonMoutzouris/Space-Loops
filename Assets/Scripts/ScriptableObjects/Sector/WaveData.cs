using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public enum WaveType { Swarm, Fleet, TagTeam, MiniBoss, Boss, Scout }
[CreateAssetMenu(fileName = "WaveData", menuName = "ScriptableObjects/WaveData")]
public class WaveData : CardData
{
    public int numEnemies = 1;

    public List<ShipData> waveShips;

    public List<ShipData> possibleShips;
    public WaveType waveType;
    //Bonuses for enemies in this sector
    public List<ItemData> waveRewards;

    [HideInInspector]
    public SectorData sectorData;
    //Environment hazards in this wave

    public override void GenerateCard()
    {
        waveShips = new List<ShipData>();

        for (int i = 0; i < numEnemies; i++)
        {
            //float trueRadius = (i / 6) * followRadius + followRadius;
            ShipData newData = Instantiate(possibleShips[Random.Range(0, possibleShips.Count)]);
            waveShips.Add(newData);
        }

        Debug.Log("Generate Wave");
    }

    public override string GetTooltip()
    {

        string tooltip = base.GetTooltip();

        tooltip += waveType.ToString() + " : " + numEnemies + " Enemies";

        foreach(ShipData ship in waveShips)
        {
            tooltip += "\n" + ship.shipName;
        }

        return tooltip;
    }
}

public class CardData : ScriptableObject
{
    public Sprite cardIcon;

    public virtual string GetTooltip()
    {
        string tooltip = "";

        return tooltip;
    }

    public virtual void GenerateCard()
    {

    }
}