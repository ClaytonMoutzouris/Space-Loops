using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatSummaryPanel : MonoBehaviour
{
    public static StatSummaryPanel instance;
    public GameObject leftStats;
    public GameObject rightStats;

    public UIStatObject prefab;
    public List<UIStatObject> statObjects = new List<UIStatObject>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetStats(ShipController ship)
    {
        ClearStats();

        foreach (ShipStat stat in ship.stats.stats.Values)
        {
            if (stat.type == ShipStatType.MaxHealth || stat.type == ShipStatType.MaxShields)
            {
                continue;
            }

            UIStatObject statObject = null;

            if ((int)stat.type < (int)ShipStatType.Count/2)
            {
                statObject = Instantiate(prefab, leftStats.transform);
            }
            else
            {
                statObject = Instantiate(prefab, rightStats.transform);

            }

            statObject.SetStat(stat.type, stat.GetValue());
            if(statObject)
                statObjects.Add(statObject);
        }

        //statsText.text = text;
    }

    public void ClearStats()
    {
        foreach(UIStatObject obj in statObjects)
        {
            Destroy(obj.gameObject);
        }

        statObjects.Clear();
    }
}
