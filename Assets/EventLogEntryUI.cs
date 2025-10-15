using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventLogEntryUI : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI choiceText;
    public Image icon;
    public GameObject optionsContainer;
    public EventOptionUI optionPrefab;
    public List<EventOptionUI> eventOptions = new List<EventOptionUI>();
    public EventData eventData;
    public EventLogUI eventLog;

    public bool redeemed = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetData(EventData data)
    {
        eventData = Instantiate(data);
        titleText.text = eventData.titleText;
        choiceText.text = "";
        icon.sprite = eventData.image;

        foreach(EventOptionData option in eventData.options)
        {
            EventOptionUI newOption = Instantiate(optionPrefab, optionsContainer.transform);
            newOption.SetOption(option);
            eventOptions.Add(newOption);
            //newOption.parent = this;
        }
    }

    public void SelectOption(EventOptionData option)
    {
        if(!redeemed)
        {
            option.ApplyOption(GameManager.instance.playerShip);
            redeemed = true;

            //eventLog.RemoveEntry(this);
        }

    }

}
