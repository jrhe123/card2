using UnityEngine;
using System.Collections;

public abstract class CreatureEffect
{
    protected CreatureLogic creature;
    protected int specialAmount;

    public CreatureEffect(CreatureLogic creature, int specialAmount)
    {
        this.creature = creature;
        this.specialAmount = specialAmount;
    }

    // METHODS FOR SPECIAL FX THAT LISTEN TO EVENTS
    public virtual void RegisterEventEffect() { }

    public virtual void UnRegisterEventEffect() { }

    public virtual void CauseEventEffect() { }

    // BATTLECRY
    public virtual void WhenACreatureIsPlayed() { }

    // DEATHRATTLE
    public virtual void WhenACreatureDies() { }


}
