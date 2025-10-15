using TMPro;
using UnityEngine;

public class UIStatObject : MonoBehaviour
{
    public TextMeshProUGUI statName;
    public TextMeshProUGUI statValue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetStat(ShipStatType statType, float value, bool round = false)
    {
        statName.text = ShipStat.StringForType(statType);
        if(round)
        {
            statValue.text = ((int)value).ToString();
        }
        else
        {
            statValue.text = value.ToString();
        }

    }

    public void SetStat(string statType, float value, bool round = false)
    {
        statName.text = statType.ToString();
        if (round)
        {
            statValue.text = ((int)value).ToString();
        }
        else
        {
            statValue.text = value.ToString();
        }

    }
}
