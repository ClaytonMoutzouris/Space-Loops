using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLogUI : MonoBehaviour
{
    public static EventLogUI instance;
    public GameObject container;
    public EventLogEntryUI entryPrefab;
    
    public List<EventData> eventLog = new List<EventData>();
    public bool picking = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.autoRun)
        {
            AutoPick();
        }
    }

    public void AddEntry(EventData newEvent) 
    {
        //EventLogEntryUI newEntry = Instantiate(entryPrefab, container.transform);
        //newEntry.SetData(newEvent);
        
        eventLog.Add(Instantiate(newEvent));
        //newEntry.eventLog = this;
    }

    public void RemoveEntry(EventData eventEntry)
    {
        eventLog.Remove(eventEntry);

        //Destroy(eventEntry.gameObject);
    }

    public void ClearLog()
    {
        /*
        foreach(EventLogEntryUI entry in eventLog)
        {
            Destroy(entry.gameObject);
        }
        */
        eventLog.Clear();
    }

    public void AutoPick()
    {
        if(picking)
        {
            return;
        }

        if(eventLog.Count > 0 && !picking)
        {
            StartCoroutine(AutoPickDelay());
        }

    }

    public IEnumerator AutoPickDelay()
    {
        /*
        picking = true;
        int randomIndex = Random.Range(0, eventLog[0].eventOptions.Count);
        EventOptionData eventOption = eventLog[0].eventOptions[randomIndex].optionData;
        eventLog[0].eventOptions[randomIndex].text.color = Color.red;
        eventLog[0].choiceText.text = eventOption.description;

        yield return new WaitForSeconds(2.5f);

        if (!eventLog[0])
        {
            yield break;
        }
        eventLog[0].SelectOption(eventOption);
        picking = false;
        */
        picking = true;

        yield return new WaitForSeconds(2.5f);
        picking = false;

    }
}
