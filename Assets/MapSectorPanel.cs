using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSectorPanel : MonoBehaviour
{
    public static MapSectorPanel instance;
    public GameObject container;
    public SectorCardUI prefab;
    public List<SectorCardUI> sectorNodes = new List<SectorCardUI>();
    public ScrollRect scrollRect;

    public void Awake()
    {
        instance = this;
    }

    public void AutoPick()
    {
        if(SectorCardInventoryUI.instance.sectorNodes.Count > 0)
        {
            SectorCardInventoryUI.instance.sectorNodes[0].SelectCard();
        }
    }

    public void AddSector(SectorData sData)
    {
        SectorCardUI newCard = Instantiate(prefab, container.transform);
        newCard.SetData(sData);
        newCard.sectorPanel = this;
        sectorNodes.Add(newCard);
        UpdateSectorCards();

    }

    public void AddSector(SectorCardUI card)
    {
        card.transform.SetParent(container.transform);
        
        sectorNodes.Add(card);

        card.sectorPanel = this;

        UpdateSectorCards();

    }


    public SectorData GetSector(int sectorIndex)
    {
        if (sectorNodes.Count >= sectorIndex)
        {
            return sectorNodes[sectorIndex].sectorData;
        }

        return null;
    }
    public SectorData GetCurrentSector()
    {
        if (sectorNodes.Count > GameManager.instance.roundNumber)
        {
            return sectorNodes[GameManager.instance.roundNumber].sectorData;
        }

        return null;
    }

    public SectorData GetNextSector()
    {
        if (sectorNodes.Count > GameManager.instance.roundNumber+1)
        {
            return sectorNodes[GameManager.instance.roundNumber+1].sectorData;
        }

        return null;
    }

    public void ClearSectors()
    {
        foreach (SectorCardUI sector in sectorNodes)
        {
            Destroy(sector.gameObject);
        }

        sectorNodes.Clear();
    }

    public void UpdateSectorCards()
    {
        for(int i = 0; i < sectorNodes.Count; i++)
        {
            if(i+1 < GameManager.instance.roundNumber)
            {
                sectorNodes[i].SetStatus(CardStatus.Complete);
            } else if (i+1 == GameManager.instance.roundNumber)
            {
                sectorNodes[i].SetStatus(CardStatus.Current);
                scrollRect.content.localPosition = scrollRect.GetSnapToPositionToBringChildIntoViewHorizontal(sectorNodes[i].GetComponent<RectTransform>());
            }
            else
            {
                sectorNodes[i].SetStatus(CardStatus.Planned);
            }
        }
    }

    public void AddSectorAtIndex(SectorData sData, int index)
    {

    }
}
