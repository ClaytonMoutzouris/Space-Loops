using NUnit.Framework.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ShipInventoryOptionType { Equip, Equip2, Sell, Use }
public class ShipInventoryNode : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Image icon;
    public ItemData itemData;
    public Button[] buttons;

    public void SetItem(ItemData item)
    {
        itemData = item;
        text.text = itemData.GetTooltip();
        icon.sprite = itemData.itemIcon;

        buttons[(int)ShipInventoryOptionType.Sell].gameObject.SetActive(true);

        if (itemData is EquipmentData equipmentData)
        {
            buttons[(int)ShipInventoryOptionType.Equip].gameObject.SetActive(true);
        }
        else
        {
            buttons[(int)ShipInventoryOptionType.Equip].gameObject.SetActive(false);

        }

        if (itemData is WeaponData weaponData)
        {
            buttons[(int)ShipInventoryOptionType.Equip2].gameObject.SetActive(true);

        } else
        {
            buttons[(int)ShipInventoryOptionType.Equip2].gameObject.SetActive(false);
        }

        buttons[(int)ShipInventoryOptionType.Use].gameObject.SetActive(false);

    }

    public void EquipItem()
    {
        if(itemData.owner && itemData is EquipmentData equipmentData)
        {
            itemData.owner.inventory.RemoveItem(itemData, false);
            itemData.owner.equipmentManager.Equip(equipmentData);
        }
    }
    
    public void EquipItem2()
    {
        if(itemData.owner && itemData is EquipmentData equipmentData)
        {
            itemData.owner.inventory.RemoveItem(itemData, false);
            itemData.owner.equipmentManager.Equip(equipmentData, 1);
        }
    }

    public void SellItem()
    {
        itemData.owner.shipData.currency += (int)itemData.itemValue + (int)(itemData.itemValue* itemData.owner.stats.GetStat(ShipStatType.CurrencyBonus).GetValue() * 0.01f);
        itemData.owner.inventory.RemoveItem(itemData, true);

    }

    public void UseItem()
    {

    }
}
