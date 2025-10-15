using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShipEquipSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image slotIcon;
    public List<Sprite> emptyIcons;

    public EquipmentSlot slotType;
    public EquipmentData equipment;
    


    public void SetSlot(EquipmentData equipment)
    {
        this.equipment = equipment;

        slotIcon.sprite = equipment.itemIcon;
    }


    public void ClearSlot()
    {
        equipment = null;

        slotIcon.sprite = emptyIcons[(int)slotType];
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShipEquipmentPanel.instance.tooltipObject.ShowTooltip(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ShipEquipmentPanel.instance.tooltipObject.HideTooltip();
    }
}
