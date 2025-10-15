using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventData", menuName = "ScriptableObjects/EventData")]
public class EventData : ScriptableObject
{
    public string titleText;
    public string descriptionText;
    public Sprite image;

    public List<EventOptionData> options;

    public string GetTooltip()
    {

        string tooltip = "";

        return tooltip;
    }
}
