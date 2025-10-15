using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class WaveCardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;
    public Image border;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI numEnemies;

    public WaveData waveData;

    public CardStatus status = CardStatus.Idle;
    public WaveMapPanel wavePanel;
    public WaveCardInventoryUI waveInventory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void SetData(WaveData wData)
    {
        waveData = wData;

        nameText.text = waveData.waveType.ToString();
        numEnemies.text = waveData.numEnemies.ToString();
        icon.sprite = waveData.cardIcon;
        SetStatus(status);

    }

    public void SelectCard()
    {
        if (wavePanel && status == CardStatus.Planned && waveData.waveType != WaveType.Boss)
        {
            wavePanel.waveNodes.Remove(this);
            wavePanel = null;
            WaveCardInventoryUI.instance.AddWave(this);
        }
        else if (waveInventory)
        {
            waveInventory.waveNodes.Remove(this);
            waveInventory = null;
            WaveMapPanel.instance.AddWave(this);
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
        CardTooltipUI.instance.ShowTooltip(this);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        CardTooltipUI.instance.HideTooltip();
    }
}
