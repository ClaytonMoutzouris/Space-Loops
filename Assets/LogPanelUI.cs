using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public enum LogEntryType { Combat, Event, Other }

public class LogPanelUI : UIPanel
{
    public static LogPanelUI instance;
    public GameObject logContainer;
    public LogNodeUI prefab;
    public List<LogNodeUI> logNodes = new List<LogNodeUI>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddEntry(string text, LogEntryType logEntryType = LogEntryType.Other)
    {
        switch (logEntryType)
        {
            case LogEntryType.Combat:
                text = "<color=red>" + text + "</color>";
                break;
            case LogEntryType.Event:
                text = "<color=blue>" + text + "</color>";
                break;
            case LogEntryType.Other:
                text = "<color=purple>" + text + "</color>";
                break;
        }

        LogNodeUI newNode = Instantiate(prefab, logContainer.transform);

        newNode.SetEntry(text);
    }

}
