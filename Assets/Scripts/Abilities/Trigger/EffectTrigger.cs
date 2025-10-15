using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EffectTrigger", menuName = "ScriptableObjects/Effects/EffectTriggers/EffectTrigger")]
[System.Serializable]
public class EffectTrigger
{
    public List<Effect> ownerEffects;

    public virtual void TriggerEffect(EffectContext context)
    {
        if (context.effectOwner)
        {
            foreach (Effect ownerEffectProto in ownerEffects)
            {
                Effect ownerEffect = ScriptableObject.Instantiate(ownerEffectProto);
                ownerEffect.SetContext(context.effectOwner, context.effectOwner, context);
                ownerEffect.TryDoEffect();
            }
        }

    }
}
