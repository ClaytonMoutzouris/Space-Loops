using System.Collections.Generic;
using UnityEngine;

public class WaveCardInventoryUI : MonoBehaviour
{
    public static WaveCardInventoryUI instance;
    public GameObject container;
    public WaveCardUI prefab;
    public List<WaveCardUI> waveNodes = new List<WaveCardUI>();

    public void Awake()
    {
        instance = this;
    }

    public void AddWave(WaveData wData)
    {
        WaveCardUI newCard = Instantiate(prefab, container.transform);
        newCard.SetData(wData);
        newCard.waveInventory = this;

        newCard.SetStatus(CardStatus.Idle);
        waveNodes.Add(newCard);
    }

    public void AddWave(WaveCardUI card)
    {
        card.transform.SetParent(container.transform);
        card.SetStatus(CardStatus.Idle);
        card.waveInventory = this;
        waveNodes.Add(card);

    }

    public void ClearWaves()
    {
        foreach (WaveCardUI wave in waveNodes)
        {
            Destroy(wave.gameObject);
        }

        waveNodes.Clear();
    }
}
