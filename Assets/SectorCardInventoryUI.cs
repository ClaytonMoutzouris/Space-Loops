using System.Collections.Generic;
using UnityEngine;

public class SectorCardInventoryUI : MonoBehaviour
{
    public static SectorCardInventoryUI instance;
    public GameObject container;
    public SectorCardUI prefab;
    public List<SectorCardUI> sectorNodes = new List<SectorCardUI>();

    public void Awake()
    {
        instance = this;
    }

    public void AddSector(SectorData sData)
    {
        SectorCardUI newCard = Instantiate(prefab, container.transform);
        newCard.SetData(sData);
        newCard.sectorInventory = this;
        newCard.SetStatus(CardStatus.Idle);
        sectorNodes.Add(newCard);

    }

    public void AddSector(SectorCardUI card)
    {
        card.transform.SetParent(container.transform);
        card.SetStatus(CardStatus.Idle);
        card.sectorInventory = this;

        sectorNodes.Add(card);

    }

    public void ClearSectors()
    {
        foreach (SectorCardUI sector in sectorNodes)
        {
            Destroy(sector.gameObject);
        }

        sectorNodes.Clear();
    }

}
