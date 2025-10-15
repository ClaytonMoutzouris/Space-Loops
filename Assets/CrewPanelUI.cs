using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CrewPanelUI : UIPanel
{
    public static CrewPanelUI instance;
    public GameObject logContainer;
    public CrewInventoryNode prefab;
    public List<CrewInventoryNode> crewNodes = new List<CrewInventoryNode>();
    public TextMeshProUGUI crewLimitText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }
    void Start()
    {

    }

    public void AddCrew(CrewMember newCrew, bool setToggle = false)
    {

        CrewInventoryNode newNode = Instantiate(prefab, logContainer.transform);
        if(setToggle)
        {
            newNode.crewToggle.SetIsOnWithoutNotify(true);
        }

        newNode.SetCrewText(newCrew);
        crewNodes.Add(newNode);
    }

    public void RemoveCrew(CrewMember newCrew)
    {
        CrewInventoryNode toRemove = null;
        foreach (CrewInventoryNode node in crewNodes)
        {
            if(node.crewMember == newCrew)
            {
                toRemove = node;
            }
        }

        if(toRemove)
        {
            crewNodes.Remove(toRemove);
            Destroy(toRemove.gameObject);
        }
    }

    public void ClearCrew()
    {
        foreach(CrewInventoryNode node in crewNodes)
        {
            Destroy(node.gameObject);
        }

        crewNodes.Clear();
    }
}
