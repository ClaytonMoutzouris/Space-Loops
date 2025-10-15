using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum CardStatus { Idle, Complete, Current, Planned };
public class SectorCardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;
    public Image border;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI waveNumText;

    public SectorData sectorData;
    public CardStatus status = CardStatus.Idle;

    public MapSectorPanel sectorPanel;
    public SectorCardInventoryUI sectorInventory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void SetData(SectorData sData)
    {
        sectorData = sData;

        nameText.text = sectorData.sectorType.ToString();
        levelText.text = sectorData.sectorLevel.ToString();
        waveNumText.text = sectorData.numberOfWaves.ToString();
        icon.sprite = sectorData.cardIcon;
        SetStatus(status);
    }

    public void SelectCard()
    {
        if(sectorPanel && status == CardStatus.Planned)
        {
            sectorPanel.sectorNodes.Remove(this);
            sectorPanel = null;
            SectorCardInventoryUI.instance.AddSector(this);
        }
        else if(sectorInventory)
        {
            sectorInventory.sectorNodes.Remove(this);
            sectorInventory = null;
            MapSectorPanel.instance.AddSector(this);
        }
        
    }

    public void SetStatus(CardStatus newStatus)
    {
        status = newStatus;
        switch (status)
        {
            case CardStatus.Idle:
                border.color = Color.black;
                break;
            case CardStatus.Complete:
                border.color = Color.grey;

                break;
            case CardStatus.Current:
                border.color = Color.green;

                break;
            case CardStatus.Planned:
                border.color = Color.blue;

                break;
        }
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        //OnPointerEnter(eventData);
        CardTooltipUI.instance.ShowTooltip(this);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        //OnPointerExit(eventData);
        CardTooltipUI.instance.HideTooltip();

    }
}
