using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CreatureLogic : ICharacter
{
    // PUBLIC FIELDS
    // A CardAsset that holds the info about this creature
    public CardAsset ca;

    // Reference to CreatureEffect script that causes unique effect for this creature
    public CreatureEffect effect;

    // an ID of this creature in Logic (is similar to an ID of Game Object that represents this creature in Visual)
    public int UniqueCreatureID;

    // A flag that marks this creature as frosen
    public bool Frozen = false;

    // PROPERTIES
    // property from ICharacter interface
    public int ID
    {
        get { return UniqueCreatureID; }
    }

    // the basic health that we have in CardAsset
    private int baseHealth;
    // health with all the current buffs taken into account
    public int MaxHealth
    {
        get { return baseHealth; }
    }

    // current health of this creature
    private int health;
    public int Health
    {
        get { return health; }

        set
        {
            if (value > MaxHealth)
                health = MaxHealth;
            else if (value <= 0)
                Die();
            else
                health = value;
        }
    }

    public bool Taunt
    {
        get;
        set;
    }


    // returns true if we can attack with this creature now
    public bool CanAttack
    {
        get
        {
            return true;
        }
    }

    // property for Attack
    private int baseAttack;
    public int Attack
    {
        get { return baseAttack; }
    }

    // number of attacks for one turn if (attacksForOneTurn==2) => Windfury
    private int attacksForOneTurn = 1;
    public int AttacksLeftThisTurn
    {
        get;
        set;
    }

    // CONSTRUCTOR
    public CreatureLogic(CardAsset ca)
    {
        this.ca = ca;
        baseHealth = ca.MaxHealth;
        Health = ca.MaxHealth;
        baseAttack = ca.Attack;
        attacksForOneTurn = ca.AttacksForOneTurn;
        UniqueCreatureID = IDFactory.GetUniqueID();
    }

    // METHODS
    public void OnTurnStart()
    {
        AttacksLeftThisTurn = attacksForOneTurn;
    }

    public void Die()
    {
        
    }

    public void AttackCreature(CreatureLogic target)
    {
        
    }

    public void AttackCreatureWithID(int uniqueCreatureID)
    {
        CreatureLogic target = CreatureLogic.CreaturesCreatedThisGame[uniqueCreatureID];
        AttackCreature(target);
    }

    // STATIC For managing IDs
    public static Dictionary<int, CreatureLogic> CreaturesCreatedThisGame = new Dictionary<int, CreatureLogic>();

}
