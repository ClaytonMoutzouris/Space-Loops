using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ShipEquipmentPanel : MonoBehaviour
{
    public static ShipEquipmentPanel instance;
    public ShipEquipSlotUI slotPrefab;
    public Dictionary<EquipmentSlot, List<ShipEquipSlotUI>> slotDictionary;
    public EquipmentTooltipUI tooltipObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
        //InitSlots();
        InitDictionary();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitDictionary()
    {
        slotDictionary = new Dictionary<EquipmentSlot, List<ShipEquipSlotUI>>();

        foreach (EquipmentSlot slot in System.Enum.GetValues(typeof(EquipmentSlot)))
        {
            slotDictionary.Add(slot, new List<ShipEquipSlotUI>());
        }
    }

    public ShipEquipSlotUI AddSlot(EquipmentSlot slotType)
    {
        ShipEquipSlotUI newSlot = Instantiate(slotPrefab, transform);
        newSlot.slotType = slotType;
        newSlot.ClearSlot();
        slotDictionary[slotType].Add(newSlot);
        return newSlot;
    }

    public void EquipItem(EquipmentData newEquip)
    {


    }

    public void ClearSlots()
    {
        foreach (EquipmentSlot slotType in slotDictionary.Keys)
        {
            foreach(ShipEquipSlotUI slot in slotDictionary[slotType])
            {
                Destroy(slot.gameObject);
            }
        }

        slotDictionary.Clear();

        InitDictionary();
    }


}
