using System.Collections.Generic;
using UnityEngine;

public class ShipInventory
{
    public List<ItemData> items = new List<ItemData>();
    public ShipController ship;

    public ShipInventory(ShipController controller)
    {
        ship = controller;
    }

    public void AddItem(ItemData newItem)
    {
        if (newItem.itemName.Equals("Junk"))
        {
            ship.shipData.currency += (int)newItem.itemValue + (int)(newItem.itemValue * ship.stats.GetStat(ShipStatType.CurrencyBonus).GetValue() * 0.01f);
            //itemData.owner.inventory.RemoveItem(itemData, true);
            return;
        }


        newItem.owner = ship;
        ShipInventoryPanelUI.instance.AddItem(newItem);
        items.Add(newItem);


    }


    public void RemoveItem(ItemData newItem, bool removeOwner = true)
    {
        ShipInventoryPanelUI.instance.RemoveItem(newItem);
        if(removeOwner)
            newItem.owner = null;

        items.Remove(newItem);
    }
}
