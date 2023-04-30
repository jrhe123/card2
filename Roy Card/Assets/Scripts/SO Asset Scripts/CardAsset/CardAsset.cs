using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum TargetingOptions
{
    NoTarget,

    AllCreatures,

    EnemyCreatures,

    YourCreatures,
}

public enum RarityOptions
{
    Basic, Common, Rare, Epic, Legendary,
}

public enum TypesOfCards
{
    Creature, Experience, Weapon
}

public enum EvoluationConditions
{
    None, Merge
}

public enum TypeOfKinds
{
    Physical, Spell
}

public enum CaptainSkills
{
    PhysicalAttack_1,
    PhysicalAttack_2,
    PhysicalAttack_3,
    SpellAttack_1,
    SpellAttack_2,
    SpellAttack_3,
    PhysicalDefense_1,
    PhysicalDefense_2,
    PhysicalDefense_3,
    SpellDefense_1,
    SpellDefense_2,
    SpellDefense_3,
    Heal_1,
    Heal_2,
    Heal_3,
    Dodge_1,
    Dodge_2,
    Dodge_3,
}

public class CardAsset : ScriptableObject, IComparable<CardAsset>
{
    [Header("General info")]
    public CharacterAsset CharacterAsset;

    [TextArea(2, 3)]
    public string Description;

    [TextArea(2, 3)]
    public string Tags;

    public Sprite CardImage;

    public CaptainSkills CaptainSkill;

    public bool IsCaptain = false;



    [Header("Creature Info")]
    [Range(1, 100)]
    public int Level = 1;

    [Range(1, 999999999)]
    public int ExperienceRequiredNextLevel = 100;

    [Range(1, 100)]
    public int EvolutionLevel = 30;

    public EvoluationConditions EvoluationCondition = EvoluationConditions.None;

    public CardAsset EvolutedCardAsset;

    [Range(0, 1.0f)]
    public float HitPercentage = 1.0f;

    [Range(0, 1.0f)]
    public float DodgePercentage = 0f;

    [Range(1, 999999)]
    public int MaxHealth = 1;

    [Range(1, 999999)]
    public int Attack;

    [Range(1, 999999)]
    public int Defense;

    public TypeOfKinds AttackType;
    public TypeOfKinds DefenseType;

    [Range(1, 4)]
    public int AttacksForOneTurn = 1;

    [Range(1, 6)]
    public int NumberOfTargeting = 1;

    public string CreatureScriptName;
    public string ExperienceScriptName;
    public string WeaponScriptName;

    public TypesOfCards TypeOfCard;
    public RarityOptions Rarity;
    public TargetingOptions Targets;

    public int CompareTo(CardAsset other)
    {
        if (this.Rarity < other.Rarity)
        {
            return 1;
        } else if (this.Rarity > other.Rarity)
        {
            return -1;
        } else
        {
            return this.name.CompareTo(other.name);
        }
    }

    // Define the is greater than operator.
    public static bool operator >(
        CardAsset operand1, CardAsset operand2
        )
    {
        return operand1.CompareTo(operand2) == 1;
    }

    // Define the is less than operator.
    public static bool operator <(
        CardAsset operand1, CardAsset operand2
        )
    {
        return operand1.CompareTo(operand2) == -1;
    }

    // Define the is greater than or equal to operator.
    public static bool operator >=(
        CardAsset operand1, CardAsset operand2
        )
    {
        return operand1.CompareTo(operand2) >= 0;
    }

    // Define the is less than or equal to operator.
    public static bool operator <=(
        CardAsset operand1, CardAsset operand2
        )
    {
        return operand1.CompareTo(operand2) <= 0;
    }
}
