using UnityEngine;

public class MapPanelUI : UIPanel
{
    public static MapPanelUI instance;

    public void Awake()
    {
        instance = this;
        gameObject.SetActive(false);

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickUpCard(CardData card)
    {
        if (card is SectorData sectorCard)
        {
            SectorCardInventoryUI.instance.AddSector(sectorCard);
        }
        else if(card is WaveData waveCard)
        {
            WaveCardInventoryUI.instance.AddWave(waveCard);
        }
    }

    public void GetWave()
    {

    }
}
