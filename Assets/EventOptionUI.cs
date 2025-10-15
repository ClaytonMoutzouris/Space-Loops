using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventOptionUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI text;
    public EventOptionData optionData;
    public EventPopupWindowUI parent;
    public void OptionSelect()
    {
        parent.SelectOption(optionData);
    }

    public void SetOption(EventOptionData data)
    {
        optionData = Instantiate(data);
        text.text = optionData.title;
    }

    public void OnMouseOver()
    {
        //parent
        parent.choiceText.text = optionData.description;
    }

    public void OnPointerEnter(PointerEventData data)
    {
        parent.choiceText.text = optionData.GetTooltip();
        parent.descriptionText.text = optionData.description;
    }

    public void OnPointerExit(PointerEventData data)
    {
        parent.choiceText.text = "";
    }
}
