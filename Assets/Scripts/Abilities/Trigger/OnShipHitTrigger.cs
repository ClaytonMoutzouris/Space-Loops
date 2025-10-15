using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OnShipHitTrigger", menuName = "ScriptableObjects/Effects/EffectTriggers/OnShipHitTrigger")]
[System.Serializable]
public class OnShipHitTrigger : EffectTrigger
{
    public List<Effect> hitterEffects;
    public List<Effect> hitEffects;

    public override void TriggerEffect(EffectContext context)
    {
        base.TriggerEffect(context);

        if (context.shipHit)
        {
            foreach (Effect hitEffectProto in hitEffects)
            {
                //TODO: NULL CHECK
                Effect hitEffect = ScriptableObject.Instantiate(hitEffectProto);
                hitEffect.SetContext(context.effectOwner, context.shipHit, context);
                hitEffect.TryDoEffect();
            }
        }

        if (context.effectOwner)
        {
            foreach (Effect hitterEffectProto in hitterEffects)
            {
                //TODO: NULL CHECK
                Effect hitterEffect = ScriptableObject.Instantiate(hitterEffectProto);
                hitterEffect.SetContext(context.effectOwner, context.effectOwner, context);
                hitterEffect.TryDoEffect();
            }
            

        }


    }
}
