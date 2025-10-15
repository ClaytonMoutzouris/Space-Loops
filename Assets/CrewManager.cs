using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class CrewManager : MonoBehaviour
{
    public ShipController ship;
    public ShipCrewPanel crewPanel;
    public List<CrewMember> crewActive = new List<CrewMember>();
    public List<CrewMember> passengers = new List<CrewMember>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInitialCrew(ShipData ship)
    {
        crewPanel.ClearIcons();
        foreach (CrewMember member in ship.startingCrew)
        {
            PickUpCrew(member);

        }
    }

    public void CrewToPassenger(CrewMember member)
    {
        if (crewActive.Contains(member))
        {
            crewActive.Remove(member);
            passengers.Add(member);
            crewPanel.RemoveCrewMember(member);
            //CrewPanelUI.instance.RemoveCrew(member);
            CrewPanelUI.instance.crewLimitText.text = crewActive.Count + "/" + (int)ship.stats.GetStat(ShipStatType.CrewCapacity).GetValue();

            foreach (StatBonus bonus in member.statsBonuses)
            {
                ship.stats.RemoveBonus(bonus);
            }

            ship.healthbar.SetHealth(ship.shipData.currentHeath, ship.stats.GetStat(ShipStatType.MaxHealth).GetValue());
            ship.shieldsbar.SetHealth(ship.shipData.currentShields, ship.stats.GetStat(ShipStatType.MaxShields).GetValue());
        }
    }

    public bool PassengerToCrew(CrewMember member)
    {
        if(passengers.Contains(member) && crewActive.Count < (int)ship.stats.GetStat(ShipStatType.CrewCapacity).GetValue())
        {
            passengers.Remove(member);
            crewActive.Add(member);
            crewPanel.AddCrewMember(member);
            //CrewPanelUI.instance.AddCrew(member);
            CrewPanelUI.instance.crewLimitText.text = crewActive.Count + "/" + (int)ship.stats.GetStat(ShipStatType.CrewCapacity).GetValue();

            foreach (StatBonus bonus in member.statsBonuses)
            {
                ship.stats.AddBonus(bonus.CopyBonus());
            }

            ship.healthbar.SetHealth(ship.shipData.currentHeath, ship.stats.GetStat(ShipStatType.MaxHealth).GetValue());
            ship.shieldsbar.SetHealth(ship.shipData.currentShields, ship.stats.GetStat(ShipStatType.MaxShields).GetValue());

            return true;

        }

        return false;
    }

    public void PickUpCrew(CrewMember member, bool isPassenger = false)
    {

        if (!isPassenger && crewActive.Count >= (int)ship.stats.GetStat(ShipStatType.CrewCapacity).GetValue())
        {
            isPassenger = true;
        }

        CrewMember newMember = Instantiate(member);
        newMember.crew = this;
        //newMember.crewName = ship.name;
        if(isPassenger)
        {
            passengers.Add(newMember);
            CrewPanelUI.instance.AddCrew(newMember);

        }
        else
        {
            crewActive.Add(newMember);
            crewPanel.AddCrewMember(newMember);
            CrewPanelUI.instance.AddCrew(newMember, true);
            CrewPanelUI.instance.crewLimitText.text = crewActive.Count + "/" + (int)ship.stats.GetStat(ShipStatType.CrewCapacity).GetValue();

            foreach (StatBonus bonus in member.statsBonuses)
            {
                ship.stats.AddBonus(bonus.CopyBonus());
            }

            ship.healthbar.SetHealth(ship.shipData.currentHeath, ship.stats.GetStat(ShipStatType.MaxHealth).GetValue());
            ship.shieldsbar.SetHealth(ship.shipData.currentShields, ship.stats.GetStat(ShipStatType.MaxShields).GetValue());
        }


    }

    public void RemoveCrewMember(CrewMember member)
    {
        crewActive.Remove(member);
        //crewPanel.r(newMember);
        CrewPanelUI.instance.RemoveCrew(member);
        foreach (StatBonus bonus in member.statsBonuses)
        {
            ship.stats.RemoveBonus(bonus);
        }

        ship.healthbar.SetHealth(ship.shipData.currentHeath, ship.stats.GetStat(ShipStatType.MaxHealth).GetValue());
        ship.shieldsbar.SetHealth(ship.shipData.currentShields, ship.stats.GetStat(ShipStatType.MaxShields).GetValue());
    }
}
