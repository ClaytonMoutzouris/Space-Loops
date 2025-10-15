using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CardTooltipUI : MonoBehaviour
{
    public static CardTooltipUI instance;

    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemSlot;
    public TextMeshProUGUI itemDesc;

    void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }


    public void ShowTooltip(SectorCardUI card)
    {

        itemName.text = card.sectorData.sectorName;
        itemSlot.text = card.sectorData.sectorType.ToString() + " Lvl: " + card.sectorData.sectorLevel;
        itemDesc.text = card.sectorData.numberOfWaves + " Waves";
        string waveTypes = "";
        /*
        foreach (WaveData wave in card.sectorData.waveDatas)
        {
            if (waveTypes != "")
            {
                waveTypes += ", ";
            }
            waveTypes += wave.waveType.ToString();
        }
        itemDesc.text += "\n" + waveTypes;
        */

        transform.position = card.transform.position;
        gameObject.SetActive(true);

    }

    public void ShowTooltip(WaveCardUI card)
    {
        itemName.text = card.waveData.name;
        itemSlot.text = card.waveData.waveType.ToString();
        itemDesc.text = card.waveData.numEnemies + " Enemies";
        string shipNames = "";
        foreach (ShipData ship in card.waveData.waveShips)
        {
            if (shipNames != "")
            {
                shipNames += ", ";
            }
            shipNames += ship.shipName;
        }

        itemDesc.text += "\n" + shipNames;

        transform.position = card.transform.position;
        gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
        itemName.text = "";
        itemSlot.text = "";

        itemDesc.text = "";
    }
}
