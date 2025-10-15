using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipEffect", menuName = "ScriptableObjects/Effects/ShipEffect")]
public class ShipEffect : Effect
{
    public ShipController shipOwner;
    public ShipController shipEffected;

    public override void SetContext(ShipController owner, ShipController effected, EffectContext context)
    {
        //base
        base.SetContext(owner, effected, context);

        //
        ShipController pOwner = effectOwner.GetComponent<ShipController>();

        if (!pOwner)
        {
            //This is an effect to be owned by a player?
            //Or applied to a player?
        }
        shipOwner = pOwner;

        ShipController pEffected = effectedEntity.GetComponent<ShipController>();

        if (!pEffected)
        {
            //This is an effect to be owned by a player?
            //Or applied to a player?
        }
        shipEffected = pEffected;


    }

    public override void ApplyEffect()
    {

        if (shipEffected)
        {
            shipEffected.shipEffects.Add(this);

        }

        if (effectedEntity)
        {
            if (visualEffectPrefab)
            {
                activeEffect = Instantiate(visualEffectPrefab, effectedEntity.transform);

            }
            coroutine = HandleEffect();

            if (shipEffected)
            {
                shipEffected.StartCoroutine(HandleEffect());
                return;
            }
        }

    }

    public override IEnumerator HandleEffect()
    {
        OnEffectTrigger();

        while (durationType == DurationType.Unlimited || Time.time < timeStamp + duration)
        {

            //Do stuff

            yield return null;
        }

        OnEffectEnd();

    }


    public override void OnEffectTrigger()
    {
        base.OnEffectTrigger();

    }

    public override void OnEffectEnd()
    {
        base.OnEffectEnd();

        RemoveEffect();
        //remove this from the list of effects, if we added it?
    }

    public override void RemoveEffect()
    {
        if (shipEffected)
        {
            shipEffected.shipEffects.Remove(this);
            shipEffected = null;

        }
        //remove this from the list of effects, if we added it?

        if (effectedEntity)
        {
            if (activeEffect)
            {
                Destroy(activeEffect.gameObject);
                activeEffect = null;
                effectedEntity = null;
            }
        }

    }

}
