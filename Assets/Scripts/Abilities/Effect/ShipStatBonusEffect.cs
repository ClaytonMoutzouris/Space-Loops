using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipStatBonusEffect", menuName = "ScriptableObjects/Effects/ShipStatBonusEffect")]
public class ShipStatBonusEffect : ShipEffect
{
    [Header("Stat Bonuses")]
    public List<StatBonus> playerStatBonuses;

    public override void OnEffectTrigger()
    {
        base.OnEffectTrigger();

        if (shipEffected)
        {
            shipEffected.stats.AddBonuses(playerStatBonuses);
            Debug.Log("Triggered Effect");
            //shipEffected.ScalePlayer();
        }
    }

    public override void RemoveEffect()
    {
        //Do stuff here
        if (shipEffected)
        {
            shipEffected.stats.RemoveBonuses(playerStatBonuses);
            //shipEffected.ScalePlayer();
        }

        base.RemoveEffect();
    }



}
