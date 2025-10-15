using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[CreateAssetMenu(fileName = "ShipAbility", menuName = "ScriptableObjects/Abilities/ShipAbility")]
public class ShipAbility : Ability
{
    public string abilityDescription = "";
    public ShipController abilityOwner;

    public List<EffectTrigger> onMoveEffects;
    public List<EffectTrigger> OnWaveStartEffects;
    public List<EffectTrigger> OnRoundStartEffects;
    public List<OnShipHitTrigger> onGetHitEffects;
    public List<OnShipHitTrigger> onHitShipEffects;
    public List<OnShipHitTrigger> OnDestroyEnemyEffects;
    public List<OnShipHitTrigger> OnShipDestroyedEffects;
    public List<OnShipShootTrigger> OnShootEffects;
    public List<OnShipShootTrigger> OnEnemyShootEffects;


    public virtual void GainAbility(ShipController owner)
    {
        abilityOwner = owner;
        abilityOwner.shipAbilities.Add(this);
        
    }

    public virtual void LoseAbility()
    {
        abilityOwner.shipAbilities.Remove(this);
        abilityOwner = null;

        //probably remove from list of their abilities
    }

    public virtual void OnMove()
    {
        foreach (EffectTrigger effectTrigger in onMoveEffects)
        {
            //Instantiate
            effectTrigger.TriggerEffect(new EffectContext()
            {
                effectOwner = abilityOwner
            });
        }
    }

    public virtual void OnShoot(ShipController target, AttackData attack)
    {
        foreach (OnShipShootTrigger shotTrigger in OnShootEffects)
        {
            //Instantiate
            shotTrigger.TriggerEffect(new EffectContext()
            {
                effectOwner = abilityOwner,
                shooter = abilityOwner,
                shipTarget = target,
                attack = attack
                //puckTarget = puck
            });
        }

        //When this player shoots, trigger all the teammate abilities
        //This might move but its clean here for now.

    }

    public virtual void OnHitAPlayer(ShipController shipHit, Projectile projectile)
    {
        foreach (OnShipHitTrigger hitTrigger in onHitShipEffects)
        {
            //Instantiate
            hitTrigger.TriggerEffect(new EffectContext()
            {
                effectOwner = abilityOwner,
                shipHit = shipHit,
                projectile = projectile
            });
        }
    }

    public virtual void OnDestroyEnemy(ShipController shipHit, Projectile projectile)
    {
        foreach (OnShipHitTrigger hitTrigger in onHitShipEffects)
        {
            //Instantiate
            hitTrigger.TriggerEffect(new EffectContext()
            {
                effectOwner = abilityOwner,
                shipHit = shipHit,
                projectile = projectile
            });
        }
    }

    public virtual void OnDestroyed(Projectile projectile)
    {
        foreach (OnShipHitTrigger hitTrigger in onHitShipEffects)
        {
            //Instantiate
            hitTrigger.TriggerEffect(new EffectContext()
            {
                effectOwner = abilityOwner,
                shipHit = abilityOwner,
                projectile = projectile
            });
        }
    }

    public virtual void OnGetHit(Projectile projectile)
    {
        foreach (OnShipHitTrigger hitTrigger in onGetHitEffects)
        {
            //Instantiate
            hitTrigger.TriggerEffect(new EffectContext()
            {
                effectOwner = abilityOwner,
                shipHit = abilityOwner,
                projectile = projectile
            });
        }

    }

    public virtual void OnWaveStart()
    {
        foreach (EffectTrigger periodStartTrigger in OnWaveStartEffects)
        {
            //Instantiate
            periodStartTrigger.TriggerEffect(new EffectContext()
            {
                effectOwner = abilityOwner,
            });
        }
    }

    public virtual void OnRoundStart()
    {
        foreach (EffectTrigger gameStartTrigger in OnRoundStartEffects)
        {
            //Instantiate
            gameStartTrigger.TriggerEffect(new EffectContext()
            {
                effectOwner = abilityOwner,
            });
        }
    }

    public virtual void OnEnemyShoot(ShipController enemy, Projectile proj)
    {
        foreach (OnShipShootTrigger shotTrigger in OnEnemyShootEffects)
        {
            //Instantiate
            shotTrigger.TriggerEffect(new EffectContext()
            {
                effectOwner = abilityOwner,
                shooter = enemy,
                projectile = proj
            });
        }
    }
}
