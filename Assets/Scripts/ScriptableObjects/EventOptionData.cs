using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public enum EventOptionType { }
[CreateAssetMenu(fileName = "EventOptionData", menuName = "ScriptableObjects/EventOptionData")]
public class EventOptionData : ScriptableObject
{
    public string title;
    public string description;
    public int baseChanceToSucceed = 100;
    //Costs to attempt
    public int moneyCost = 0;

    //Fail costs
    public List<StatBonus> statDebuffs;
    public int crewDie = 0;
    public int failDamage;
    public int damage;
    public List<WaveData> encounters;

    //Rewards
    //what goes here? rewards?
    public List<CrewMember> possibleCrewMembers;
    public List<StatBonus> possibleStatBonuses;
    public List<ItemData> possibleRewards;
    public List<CardData> cardRewards;


    public int healing;

    public List<AttackData> possibleNewAttacks;

    public void ApplyOption(ShipController ship)
    {

        if(moneyCost > 0)
        {
            ship.shipData.currency -= moneyCost;
        }

        if(CheckSuccess(ship))
        {
            if (possibleCrewMembers.Count > 0)
            {
                CrewMember newCrew = Instantiate(possibleCrewMembers[Random.Range(0, possibleCrewMembers.Count)]);
                ship.crewManager.PickUpCrew(newCrew);
            }

            if (possibleRewards.Count > 0)
            {
                ItemData newItem = Instantiate(possibleRewards[Random.Range(0, possibleRewards.Count)]);
                newItem.Randomize();
                ship.inventory.AddItem(newItem);
                
            }
            if (cardRewards.Count > 0)
            {
                CardData newCard = Instantiate(cardRewards[Random.Range(0, cardRewards.Count)]);
                newCard.GenerateCard();
                MapPanelUI.instance.PickUpCard(newCard);
            }

            if (possibleStatBonuses.Count > 0)
            {
                List<StatBonus> newBonuses = new List<StatBonus>();
                newBonuses.Add(possibleStatBonuses[Random.Range(0, possibleStatBonuses.Count)].CopyBonus());
                ship.stats.AddBonuses(newBonuses);
            }

            if (damage > 0)
            {
                ship.shipData.currentHeath -= damage;
            }

            if (healing > 0)
            {
                ship.shipData.currentHeath += healing + healing*ship.stats.GetStat(ShipStatType.HealingBonus).GetValue() * 0.01f;
            }

            if (possibleNewAttacks.Count > 0)
            {
                ship.shipData.attack = Instantiate(possibleNewAttacks[Random.Range(0, possibleNewAttacks.Count)]);
                ship.CleanUpProjectiles();
            }
        }
        else
        {
            if (crewDie > 0)
            {
                for(int i = 0; i < crewDie; i++)
                {
                    ship.crewManager.RemoveCrewMember(ship.crewManager.crewActive[Random.Range(0, ship.crewManager.crewActive.Count)]);
                }
            }

            if (statDebuffs.Count > 0)
            {
                List<StatBonus> newBonuses = new List<StatBonus>();
                newBonuses.Add(possibleStatBonuses[Random.Range(0, possibleStatBonuses.Count)].CopyBonus());
                ship.stats.AddBonuses(newBonuses);
            }

            if (failDamage > 0)
            {
                ship.shipData.currentHeath -= damage;
            }
        }




    }

    public bool CheckSuccess(ShipController ship)
    {
        return (Random.Range(0, 100) - ship.stats.GetStat(ShipStatType.Charisma).GetValue() < baseChanceToSucceed);

    }

    public string GetTooltip()
    {
        string tooltip = "";

        if(baseChanceToSucceed != 100)
        {
            tooltip += baseChanceToSucceed + "% Succes chance";
        }

        if (moneyCost > 0)
        {
            if (tooltip != "")
            {
                tooltip += "\n";
            }
            tooltip += "-" + moneyCost + " Galc";
        }

        if (possibleCrewMembers.Count > 0)
        {
            if (tooltip != "")
            {
                tooltip += "\n";
            }
            tooltip += "+1 Crew";
        }

        if (possibleRewards.Count > 0)
        {
            if (tooltip != "")
            {
                tooltip += "\n";
            }
            tooltip += "+1 Item";
        }

        if (possibleStatBonuses.Count > 0)
        {
            if (tooltip != "")
            {
                tooltip += "\n";
            }
            tooltip += "+1 Stat";
        }

        if (damage > 0)
        {
            if (tooltip != "")
            {
                tooltip += "\n";
            }
            tooltip += "Take " + damage + " damage";
        }

        if (healing > 0)
        {
            if (tooltip != "")
            {
                tooltip += "\n";
            }
            tooltip += "Heal " + healing + " life";
        }

        if(cardRewards.Count > 0)
        {
            if (tooltip != "")
            {
                tooltip += "\n";
            }
            tooltip += cardRewards[0].GetTooltip();
        }

        //tooltip += 
        return tooltip;
    }

}
