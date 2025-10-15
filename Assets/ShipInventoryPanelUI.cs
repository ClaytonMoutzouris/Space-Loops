using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShipInventoryPanelUI : MonoBehaviour
{
    public static ShipInventoryPanelUI instance;
    public GameObject container;
    public ShipInventoryNode prefab;
    public List<ShipInventoryNode> itemNodes = new List<ShipInventoryNode>();

    public void Awake()
    {
        instance = this;
    }
    public void AddItem(ItemData item)
    {
        ShipInventoryNode newNode = Instantiate(prefab, container.transform);

        //newNode.SetEntry(text);
        newNode.SetItem(item);
        itemNodes.Add(newNode);
    }

    public void RemoveItem(ItemData item)
    {
        foreach(ShipInventoryNode node in itemNodes)
        {
            if(node.itemData == item)
            {
                Destroy(node.gameObject);
                itemNodes.Remove(node);
                return;
            }
        }
    }

    public void ClearInventory()
    {
        foreach (ShipInventoryNode node in itemNodes)
        {
                Destroy(node.gameObject);
        }

        itemNodes.Clear();
    }
}
