using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum PlayerTargettingMode { Target, RandomTeammate, RandomEnemy, Referee, EntireTeam, EntireEnemyTeam };
[CreateAssetMenu(fileName = "EffectShipEffect", menuName = "ScriptableObjects/Effects/ShipEffects/EffectShipEffect")]
public class EffectShipEffect : ShipEffect
{
    [Header("Ship Effects")]
    public List<Effect> shipEffects;
    //public PlayerTargettingMode playerTargetting;
    List<Effect> activeEffects = new List<Effect>();

    List<ShipController> effectedShips = new List<ShipController>();

    public override void OnEffectTrigger()
    {
        base.OnEffectTrigger();

        if (shipEffected)
        {

            effectedShips.Add(shipEffected);

            foreach (ShipController ship in effectedShips)
            {
                foreach (Effect playerEffect in shipEffects)
                {
                    Effect newEffect = Instantiate(playerEffect);
                    newEffect.duration = duration;
                    newEffect.durationType = durationType;
                    newEffect.SetContext(effectOwner, ship, effectContext);
                    newEffect.TryDoEffect();
                    activeEffects.Add(newEffect);
                }

            }
        }

    }

    public override void RemoveEffect()
    {

        foreach (ShipController player in effectedShips)
        {
            /*
            foreach (NewPlayerEffect playerEffect in activeEffects)
            {
                if (player.playerEffects.Contains(playerEffect)) {
                    player.playerEffects.Remove(playerEffect);
                }
            }
            */
        }


        effectedShips.Clear();

        activeEffects.Clear();
        base.RemoveEffect();
    }
}
