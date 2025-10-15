using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum ItemType { Equipment, Weapon, Consumable, KeyItem }
public enum ItemRarity { Dirt, Terra, Solar, Galactic, Cosmic, Universal }
[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/Items/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public float itemValue;
    public ItemRarity itemRarity;
    public Sprite itemIcon;
    public ShipController owner;


    public virtual string GetTooltip()
    {
        string ttString = "<color=#" + GetColorForRarity().ToHexString() + ">" + itemName + "</color>";
        return ttString;
    }

    public virtual void Randomize()
    {
        int rarityRoll = Random.Range(0, 100);
        //int rarityRoll = Random.Range(0, 100);
        if(rarityRoll < 50)
        {
            itemRarity = ItemRarity.Dirt;
        } else if(rarityRoll >= 50 && rarityRoll < 75)
        {
            itemRarity = ItemRarity.Terra;
        }
        else if (rarityRoll >= 75 && rarityRoll < 90)
        {
            itemRarity = ItemRarity.Solar;
        }
        else if (rarityRoll >= 90 && rarityRoll < 95)
        {
            itemRarity = ItemRarity.Galactic;
        }
        else if (rarityRoll >= 95 && rarityRoll < 98)
        {
            itemRarity = ItemRarity.Cosmic;
        }
        else if (rarityRoll >= 99)
        {
            itemRarity = ItemRarity.Universal;
        }

        itemValue = itemValue * ((int)itemRarity+1);

        //foreach()
    }

    public Color GetColorForRarity()
    {
        Color color = Color.black;

        switch (itemRarity)
        {
            case ItemRarity.Dirt:
                color = Color.grey;
                break;
            case ItemRarity.Terra:
                color = Color.black;
                break;
            case ItemRarity.Solar:
                color = Color.green;
                break;
            case ItemRarity.Galactic:
                color = Color.blue;
                break;
            case ItemRarity.Cosmic:
                color = Color.magenta;
                break;
            case ItemRarity.Universal:
                color = Color.red;
                break;
        }

        return color;
    }
}
