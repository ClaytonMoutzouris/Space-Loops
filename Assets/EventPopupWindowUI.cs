using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventPopupWindowUI : MonoBehaviour
{
    public static EventPopupWindowUI instance;

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI choiceText;
    public Image eventIcon;
    public EventData currentEvent;
    public GameObject choicesContainer;
    //public EventData eventData;

    public EventOptionUI optionPrefab;
    public List<EventOptionUI> eventOptions;
    public bool redeemed = false;
    public bool picking = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.autoRun)
        {
            AutoPick();
        }
    }

    public void SetData(EventData data)
    {
        if(data)
        {
            redeemed = false;

            currentEvent = Instantiate(data);
            titleText.text = currentEvent.titleText;
            choiceText.text = "";
            eventIcon.sprite = currentEvent.image;

            foreach (EventOptionData option in currentEvent.options)
            {
                EventOptionUI newOption = Instantiate(optionPrefab, choicesContainer.transform);
                newOption.SetOption(option);
                eventOptions.Add(newOption);
                newOption.parent = this;
            }
        }
        else
        {
            currentEvent = null;
            titleText.text = "";
            choiceText.text = "";
            eventIcon.sprite = null;

            foreach(EventOptionUI eventOption in eventOptions)
            {
                Destroy(eventOption.gameObject);
            }

            eventOptions.Clear();
        }


    }

    internal void SelectOption(EventOptionData option)
    {
        if (!redeemed)
        {
            if(GameManager.instance.playerShip.shipData.currency < option.moneyCost)
            {
                return;
            }
            option.ApplyOption(GameManager.instance.playerShip);
            redeemed = true;
            picking = false;
            //eventLog.RemoveEntry(this);
            CloseWindow();
        }
    }

    public void OpenWindow(EventData newData = null)
    {
        if(newData)
        {
            SetData(newData);
        }
        else if (currentEvent)
        {
            SetData(currentEvent);
        }

        gameObject.SetActive(true);


    }

    public void CloseWindow()
    {
        gameObject.SetActive(false);
        SetData(null);
    }

    public void AutoPick()
    {
        if (picking)
        {
            return;
        }

        if (!picking)
        {
            StartCoroutine(AutoPickDelay());
        }

    }

    public IEnumerator AutoPickDelay()
    {
        
        picking = true;
        int randomIndex = Random.Range(0, eventOptions.Count);
        EventOptionData eventOption = eventOptions[randomIndex].optionData;
        eventOptions[randomIndex].text.color = Color.red;
        choiceText.text = eventOption.description;

        yield return new WaitForSecondsRealtime(2.5f);

        SelectOption(eventOption);
        //picking = false;

    }

}
