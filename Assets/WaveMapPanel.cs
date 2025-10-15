using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveMapPanel : MonoBehaviour
{
    public static WaveMapPanel instance;
    public GameObject container;
    public WaveCardUI prefab;
    public List<WaveCardUI> waveNodes = new List<WaveCardUI>();
    public ScrollRect scrollRect;

    public void Awake()
    {
        instance = this;
    }

    public void AddWave(WaveData wData)
    {
        WaveCardUI newCard = Instantiate(prefab, container.transform);
        newCard.SetData(wData);
        newCard.wavePanel = this;

        waveNodes.Add(newCard);
        UpdateWaveCards();

    }

    public void AddWave(WaveCardUI card)
    {

        card.wavePanel = this;
        card.transform.SetParent(container.transform);



        if (waveNodes.Count > 0 && waveNodes[waveNodes.Count-1].waveData.waveType == WaveType.Boss)
        {
            waveNodes.Insert(waveNodes.Count - 1, card);
            card.transform.SetSiblingIndex(waveNodes.Count - 1);
        }
        else
        {
            waveNodes.Add(card);
        }

        UpdateWaveCards();
    }

    public static void Swap<T>(List<T> list, int indexA, int indexB)
    {
        T tmp = list[indexA];
        list[indexA] = list[indexB];
        list[indexB] = tmp;
    }

    public WaveData GetWave(int sectorIndex)
    {
        if (waveNodes.Count >= sectorIndex)
        {
            return waveNodes[sectorIndex].waveData;
        }

        return null;
    }
    public WaveData GetCurrentWave()
    {
        if (waveNodes.Count > GameManager.instance.waveNumber)
        {
            return waveNodes[GameManager.instance.waveNumber].waveData;
        }

        return null;
    }

    public WaveData GetNextWave()
    {
        if (waveNodes.Count > GameManager.instance.waveNumber+1)
        {
            return waveNodes[GameManager.instance.waveNumber+1].waveData;
        }

        return null;
    }

    public void UpdateWaveCards()
    {
        for (int i = 0; i < waveNodes.Count; i++)
        {
            if (i+1 < GameManager.instance.waveNumber)
            {
                waveNodes[i].SetStatus(CardStatus.Complete);
            }
            else if (i+1 == GameManager.instance.waveNumber)
            {
                waveNodes[i].SetStatus(CardStatus.Current);
                scrollRect.content.localPosition = scrollRect.GetSnapToPositionToBringChildIntoViewHorizontal(waveNodes[i].GetComponent<RectTransform>());
            }
            else
            {
                waveNodes[i].SetStatus(CardStatus.Planned);
            }
        }
    }

    public void ClearWaves()
    {
        foreach(WaveCardUI wave in waveNodes)
        {
            Destroy(wave.gameObject);
        }

        waveNodes.Clear();
    }

    public void AddWaveAtIndex(WaveData sData, int index)
    {

    }
}
