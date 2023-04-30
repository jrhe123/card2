using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DamageOpponentBattlecry : CreatureEffect
{
    public DamageOpponentBattlecry(
        CreatureLogic creature, int specialAmount
        ) : base(creature, specialAmount)
    { }

    // BATTLECRY
    public override void WhenACreatureIsPlayed()
    {
        //
    }
}
