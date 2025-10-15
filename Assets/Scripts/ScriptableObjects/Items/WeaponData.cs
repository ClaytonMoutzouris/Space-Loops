using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/Items/WeaponData")]
public class WeaponData : EquipmentData
{
    public AttackData attackData;
    //AttackData previousAttackData;
    public float minDamage = 0;
    public float maxDamage = 0;
    //attacks per second
    public float attackSpeed = 1;
    public float projectileSpeed = 1;
    public float projectileLifetime = 5;
    public int numberOfProjectiles = 1;
    public float spreadAngle = 45;
    public float projectileSize = 1;


    public override bool Equip(ShipController ship)
    {

        if (!base.Equip(ship))
        {
            return false;
        }
        attackData = Instantiate(attackData);
        //ship.shipData.attack = Instantiate(attackData);

        return true;
    }

    public override bool Unequip()
    {

        owner.shipData.attack = owner.defaultAttack;

        if (!base.Unequip())
        {
            return false;
        }

        return true;
    }

    public override string GetTooltip()
    {
        string ttString = base.GetTooltip();

        ttString += "\n" + attackData.name;

        return ttString;
    }
}
