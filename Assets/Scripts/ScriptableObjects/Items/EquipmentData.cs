using System.Collections.Generic;
using UnityEngine;
public enum EquipmentSlot { Weapon, Hull, Shields, Propulsion, Sensors }

[CreateAssetMenu(fileName = "EquipmentData", menuName = "ScriptableObjects/Item/EquipmentData")]
public class EquipmentData : ItemData
{
    public EquipmentSlot slotType;
    public List<StatBonus> statBonuses;

    public List<StatBonus> possibleBonuses;

    public ShipEquipmentSlot slot = null;

    [HideInInspector]
    public List<ShipAbility> abilities = new List<ShipAbility>();

    public List<ShipAbility> possibleAbilities = new List<ShipAbility>();
    public virtual bool Equip(ShipController ship)
    {
        foreach (StatBonus bonus in statBonuses)
        {
            ship.stats.AddBonus(bonus);
        }

        foreach (ShipAbility ability in abilities)
        {
            //ship.stats.AddBonus(bonus);
            ability.GainAbility(ship);
        }

        this.owner = ship;
        return true;
    }

    public virtual bool Unequip()
    {
        foreach (StatBonus bonus in statBonuses)
        {
            owner.stats.RemoveBonus(bonus);
        }

        foreach (ShipAbility ability in abilities)
        {
            //ship.stats.AddBonus(bonus);
            ability.LoseAbility();
        }

        owner = null;

        return true;
    }

    public override void Randomize()
    {
        base.Randomize();

        //statBonuses.Clear();
        //List<StatBonus> unpickedBonuses = new List<StatBonus>();
        //unpickedBonuses.AddRange(possibleBonuses);

        for (int i = 0; i < (int)itemRarity; i++)
        {

            StatBonus bonus = possibleBonuses[Random.Range(0, possibleBonuses.Count)];

            //scale bonus
            bonus.bonusValue *= Random.Range(1, GameManager.instance.roundNumber);

            statBonuses.Add(bonus.CopyBonus());
            //unpickedBonuses.Remove(bonusType);
            //statBonuses.Add
        }

        if(possibleAbilities.Count > 0)
        {
            abilities.Add(Instantiate(possibleAbilities[Random.Range(0, possibleAbilities.Count)]));
        }
    }

    public override string GetTooltip()
    {
        string ttString = base.GetTooltip();

        foreach (StatBonus bonus in statBonuses)
        {
            ttString += "\n";
            ttString += bonus.GetTooltip();
        }

        return ttString;
    }
}
