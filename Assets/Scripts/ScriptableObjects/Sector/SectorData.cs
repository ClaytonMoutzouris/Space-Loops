using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public enum SectorType { Aliens, Robots, Creepies }
[CreateAssetMenu(fileName = "SectorData", menuName = "ScriptableObjects/SectorData")]
public class SectorData : CardData
{
    public string sectorName = "";
    //Sector Info
    public SectorType sectorType;
    public int sectorLevel = 1;
    public int numberOfWaves = 3;

    public List<WaveData> waveDatas = new List<WaveData>();

    public WaveData bossWave;
    //Not in use

    //Generation Stuff
    public List<Color> shipColors;
    public List<WaveData> possibleWaveDatas = new List<WaveData>();
    public List<EventData> possibleEvents = new List<EventData>();

    //Bonuses for enemies in this sector

    public override void GenerateCard()
    {
        sectorLevel = GameManager.instance.roundNumber+1;

        List<WaveData> possibleWaves = new List<WaveData>();

        possibleWaves.AddRange(possibleWaveDatas);

        possibleWaves.RemoveAll(obj => obj.numEnemies > GameManager.instance.roundNumber+1);

        for (int i = 0; i < numberOfWaves-1; i++)
        {
            WaveData data = Instantiate(possibleWaves[Random.Range(0, possibleWaves.Count)]);
            data.sectorData = this;
            waveDatas.Add(data);
        }
        bossWave = Instantiate(bossWave);
        bossWave.sectorData = this;
        waveDatas.Add(bossWave);

    }

    public override string GetTooltip()
    {

        string tooltip = base.GetTooltip();

        tooltip += sectorType.ToString() + " : " + numberOfWaves + " Waves";

        foreach (WaveData waveData in waveDatas)
        {
            tooltip += "\n" + waveData.GetTooltip();
        }
        return tooltip;
    }
}
