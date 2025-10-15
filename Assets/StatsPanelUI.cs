using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class StatsPanelUI : MonoBehaviour
{
    public static StatsPanelUI instance;
    public TextMeshProUGUI statsText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(ShipController ship)
    {
        string text = "";

        foreach (ShipStat stat in ship.stats.stats.Values)
        {
            if (stat.type == ShipStatType.MaxHealth || stat.type == ShipStatType.MaxShields)
            {
                continue;
            }
            text += stat.type + " - " + stat.GetValue() + "\n";
        }

        statsText.text = text;
    }
}
