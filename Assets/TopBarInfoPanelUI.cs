using TMPro;
using UnityEngine;

public class TopBarInfoPanelUI : MonoBehaviour
{
    public static TopBarInfoPanelUI instance;

    public HealthBar healthbar;
    public HealthBar shieldsbar;
    public HealthBar expbar;

    public TextMeshProUGUI levelLabel;
    public TextMeshProUGUI roundLabel;
    public TextMeshProUGUI waveLabel;
    public TextMeshProUGUI currencyLabel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInfo(ShipController ship)
    {
        healthbar.SetHealth(ship.shipData.currentHeath, ship.stats.GetStat(ShipStatType.MaxHealth).GetValue());
        shieldsbar.SetHealth(ship.shipData.currentShields, ship.stats.GetStat(ShipStatType.MaxShields).GetValue());
        expbar.SetHealth(ship.exp - ship.GetEXPToLevel(ship.shipLevel - 1), ship.GetEXPToLevel() - ship.GetEXPToLevel(ship.shipLevel-1));
        levelLabel.text = "Level - " + ship.shipLevel;
        roundLabel.text = GameManager.instance.roundNumber.ToString();
        currencyLabel.text = ship.shipData.currency.ToString();
        if(GameManager.instance.currentWave && GameManager.instance.currentWave.waveType == WaveType.Boss)
        {
            waveLabel.text = "Boss";
        }
        else
        {
            waveLabel.text = GameManager.instance.waveNumber.ToString();
        }

        //statsText.text = text;
    }
}
