using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EquipmentTooltipUI : MonoBehaviour
{
    public static EquipmentTooltipUI instance;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemSlot;
    public TextMeshProUGUI itemDesc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }


    public void ShowTooltip(ShipEquipSlotUI equipSlot)
    {
        if(!equipSlot.equipment)
        {
            return;
        }
        gameObject.SetActive(true);
        transform.position = equipSlot.transform.position;
        itemName.text = "<color=#" + equipSlot.equipment.GetColorForRarity().ToHexString() + ">" + equipSlot.equipment.itemName + "</color>";
        itemSlot.text = equipSlot.equipment.slotType.ToString();

        itemDesc.text = "";

        foreach (StatBonus bonus in equipSlot.equipment.statBonuses)
        {
            if(itemDesc.text != "")
            {
                itemDesc.text += "\n";
            }
            itemDesc.text += bonus.GetTooltip();
        }
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
        itemName.text = "";
        itemSlot.text = "";

        itemDesc.text = "";
    }
}
