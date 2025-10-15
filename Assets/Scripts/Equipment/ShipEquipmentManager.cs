using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
//public enum EquipmentSlot
public class ShipEquipmentManager
{
    public Dictionary<EquipmentSlot, List<ShipEquipmentSlot>> slotDictionary;

    public ShipController ship;

    public ShipEquipmentManager (ShipController controller)
    {
        ship = controller;
        InitDictionary();
        InitSlots();
    }

    public void InitDictionary()
    {
        slotDictionary = new Dictionary<EquipmentSlot, List<ShipEquipmentSlot>>();

        foreach (EquipmentSlot slot in System.Enum.GetValues(typeof(EquipmentSlot)))
        {
            slotDictionary.Add(slot, new List<ShipEquipmentSlot>());
        }
    }

    public void InitSlots()
    {
        ShipEquipmentSlot hullSlot = new ShipEquipmentSlot(EquipmentSlot.Hull);
        hullSlot.uiSlot = ShipEquipmentPanel.instance.AddSlot(EquipmentSlot.Hull);
        slotDictionary[EquipmentSlot.Hull].Add(hullSlot);
        ShipEquipmentSlot sensorSlot = new ShipEquipmentSlot(EquipmentSlot.Sensors);
        sensorSlot.uiSlot = ShipEquipmentPanel.instance.AddSlot(EquipmentSlot.Sensors);
        slotDictionary[EquipmentSlot.Sensors].Add(sensorSlot);
        ShipEquipmentSlot shieldsSlot = new ShipEquipmentSlot(EquipmentSlot.Shields);
        shieldsSlot.uiSlot = ShipEquipmentPanel.instance.AddSlot(EquipmentSlot.Shields);
        slotDictionary[EquipmentSlot.Shields].Add(shieldsSlot);
        ShipEquipmentSlot propSlot = new ShipEquipmentSlot(EquipmentSlot.Propulsion);
        propSlot.uiSlot = ShipEquipmentPanel.instance.AddSlot(EquipmentSlot.Propulsion);
        slotDictionary[EquipmentSlot.Propulsion].Add(propSlot);
        ShipEquipmentSlot weapon1Slot = new ShipEquipmentSlot(EquipmentSlot.Weapon);
        weapon1Slot.uiSlot = ShipEquipmentPanel.instance.AddSlot(EquipmentSlot.Weapon);
        slotDictionary[EquipmentSlot.Weapon].Add(weapon1Slot);
        ShipEquipmentSlot weapon2Slot = new ShipEquipmentSlot(EquipmentSlot.Weapon);
        weapon2Slot.uiSlot = ShipEquipmentPanel.instance.AddSlot(EquipmentSlot.Weapon);
        slotDictionary[EquipmentSlot.Weapon].Add(weapon2Slot);

    }


    public void Equip(EquipmentData equipment)
    {
        ShipEquipmentSlot slot = GetAvailableSlot(equipment.slotType);

        if(slot != null)
        {
            if(!slot.IsEmpty())
            {
                Unequip(slot);
            }
            equipment.Equip(ship);

            slot.SetSlot(equipment);

        }
        LogPanelUI.instance.AddEntry(ship.name + " equipped " + equipment.itemName, LogEntryType.Other);

    }

    public void Equip(EquipmentData equipment, int index)
    {
        ShipEquipmentSlot slot = GetSlot(equipment.slotType, index);

        if (slot != null)
        {
            if (!slot.IsEmpty())
            {
                Unequip(slot);
            }
            equipment.Equip(ship);

            slot.SetSlot(equipment);
            LogPanelUI.instance.AddEntry(ship.name + " equipped " + equipment.itemName, LogEntryType.Other);

        }

    }

    public ShipEquipmentSlot GetAvailableSlot(EquipmentSlot type)
    {
        ShipEquipmentSlot possibleSlot = null;
        foreach (ShipEquipmentSlot slot in slotDictionary[type])
        {
            if (slot.slotType == type)
            {
                if(slot.IsEmpty())
                {
                    return slot;
                }

                if (possibleSlot == null)
                {
                    possibleSlot = slot;
                }
            }
        }

        return possibleSlot;
    }

    public ShipEquipmentSlot GetSlot(EquipmentSlot type, int index = 0)
    {
        return slotDictionary[type][index];
    }

    public void Unequip(EquipmentData equipment)
    {
        equipment.slot.SetSlot(null);
        equipment.Unequip();
    }

    public void Unequip(ShipEquipmentSlot slot)
    {
        if (slot.equipment)
        {
            slot.equipment.Unequip();
            slot.SetSlot(null);
        }        
    }
}

public class ShipEquipmentSlot
{
    public EquipmentSlot slotType;
    public EquipmentData equipment;
    public ShipEquipSlotUI uiSlot;
    public ShipEquipmentSlot(EquipmentSlot type)
    {
        slotType = type;
    }
    public void SetSlot(EquipmentData equip)
    {

        if(equip)
        {
            if(equipment)
            {
                equipment.slot = null;

            }

            equipment = equip;
            equipment.slot = this;

            uiSlot.SetSlot(equipment);

        }
        else
        {
            if (equipment)
            {
                equipment.slot = null;

            }
            uiSlot.ClearSlot();
        }
    }

    public bool IsEmpty()
    {
        return equipment == null;
    }

}
