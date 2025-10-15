using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public enum SideBarTabIndexEnum { Ship, Crew, Map, Log }
public class SideBarPanelUI : MonoBehaviour
{
    public static SideBarPanelUI instance;
    public List<UIPanel> panels = new List<UIPanel>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        ChangePanel(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangePanel(int index)
    {
        foreach(UIPanel panel in panels)
        {
            panel.ClosePanel();
        }

        panels[index].OpenPanel();
    }
}
