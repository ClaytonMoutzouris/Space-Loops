using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OnShipShootTrigger", menuName = "ScriptableObjects/Effects/EffectTriggers/OnShipShootTrigger")]
[System.Serializable]
public class OnShipShootTrigger : EffectTrigger
{
    public List<Effect> shooterEffects;
    public List<Effect> puckEffects;

    public override void TriggerEffect(EffectContext context)
    {
        base.TriggerEffect(context);

        if (context.effectOwner)
        {

        }

        if (context.shooter)
        {
            foreach (Effect shooterEffectProto in shooterEffects)
            {
                //TODO: NULL CHECK
                Effect shooterEffect = ScriptableObject.Instantiate(shooterEffectProto);
                shooterEffect.SetContext(context.effectOwner, context.shooter, context);
                shooterEffect.TryDoEffect();
            }
        }

        /*
        if (context.shi)
        {
            foreach (NewEffect puckEffectProto in puckEffects)
            {
                NewEffect puckEffect = ScriptableObject.Instantiate(puckEffectProto);
                puckEffect.SetContext(context.effectOwner, context.puckTarget.gameObject, context);
                puckEffect.TryDoEffect();
            }
        }
        */

    }
}
