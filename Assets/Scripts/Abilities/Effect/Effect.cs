using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

//public enum TargettingMode { Self, Target, CenterIce, RandomTeammate, RandomOpponent };
//public enum NetTargettingMode { Self, Target, CenterIce, RandomTeammate, RandomOpponent };
public enum DurationType { Instant, Limited, Unlimited };
//public enum TargettingMode2 { Self, Target, Shooter, Puck, Net };
//[CreateAssetMenu(fileName = "NewEffect", menuName = "ScriptableObjects/NewEffects/NewEffect")]
public class Effect : ScriptableObject
{
    [HideInInspector]
    public ShipController effectOwner;
    [HideInInspector]
    public ShipController effectedEntity;
    [Header("Info")]
    public string description = "";

    [Header("Sounds")]
    public List<AudioClip> soundEffects;

    [Header("Timing")]
    public DurationType durationType = DurationType.Instant;
    public float duration = 0;
    protected float timeStamp;
    protected IEnumerator coroutine;

    [Header("Rendering")]
    public Color effectColor;
    public float intensity = 0;
    public ParticleSystem visualEffectPrefab;
    protected ParticleSystem activeEffect;

    public EffectContext effectContext;


    public virtual void TryDoEffect()
    {
        if (effectedEntity)
        {
            ApplyEffect();
        }
    }

    public virtual void ApplyEffect()
    {


        //player.persistentEffects.Add(this);


    }

    public virtual IEnumerator HandleEffect()
    {
        OnEffectTrigger();

        while (durationType == DurationType.Unlimited || Time.time < timeStamp + duration)
        {

            //Do stuff

            yield return null;
        }

        OnEffectEnd();

    }

    /*
     * The trigger called on every effect, this is where most of the magic happens?
     * Maybe we do another method for handling the middle part.
     */
    public virtual void OnEffectTrigger()
    {

        timeStamp = Time.time;

    }

    public virtual void OnEffectEnd()
    {

        //remove this from the list of effects, if we added it?
        //RemoveEffect();
    }

    public virtual void RemoveEffect()
    {

        //remove this from the list of effects, if we added it?

    }


    public virtual void SetContext(ShipController owner, ShipController effected, EffectContext context)
    {
        effectOwner = owner;
        effectedEntity = effected;
        effectContext = context;
    }

    public virtual string GetDescription()
    {
        return description;
    }
}

public class EffectContext
{
    public ShipController effectOwner = null;
    //public Puck puckTarget = null;
    //public GoalNet netTarget = null;
    //We may be able to always get this from the puck itself
    public ShipController shooter = null;
    public ShipController shipHit = null;
    public ShipController shipTarget = null;
    public Projectile projectile = null;
    public AttackData attack = null;
    //public Hitbox hitObject = null;
    //public bool scored = false;
    //public Vector3 faceoffLocation;
}

public interface IEffector
{

}

public interface IEffectable
{
    //List<NewEffect> GeEffects();
}