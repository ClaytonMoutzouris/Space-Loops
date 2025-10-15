using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipCrewPanel : MonoBehaviour
{
    public Dictionary<CrewMember, GameObject> icons = new Dictionary<CrewMember, GameObject>();
    public Image iconPrefab;
    public ShipController controller;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void AddCrewMember(CrewMember crew)
    {
        Image icon = Instantiate(iconPrefab, transform);
        icon.color = crew.color;
        icons.Add(crew, icon.gameObject);


    }

    public void RemoveCrewMember(CrewMember crew)
    {
        Destroy(icons[crew].gameObject);
        icons.Remove(crew);
    }

    public void ClearIcons()
    {
        foreach (KeyValuePair<CrewMember, GameObject> icon in icons)
        {
            Destroy(icon.Value.gameObject);
        }

        icons.Clear();
    }

}
