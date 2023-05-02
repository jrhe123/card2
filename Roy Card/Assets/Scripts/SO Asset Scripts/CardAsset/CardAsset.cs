using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Mathematics;
using Unity.Burst.Intrinsics;

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
    PhysicalAttack_0,
    PhysicalAttack_1,
    PhysicalAttack_2,
    PhysicalAttack_3,
    SpellAttack_0,
    SpellAttack_1,
    SpellAttack_2,
    SpellAttack_3,
    PhysicalDefense_0,
    PhysicalDefense_1,
    PhysicalDefense_2,
    PhysicalDefense_3,
    SpellDefense_0,
    SpellDefense_1,
    SpellDefense_2,
    SpellDefense_3,
    Heal_0,
    Heal_1,
    Heal_2,
    Heal_3,
    Dodge_0,
    Dodge_1,
    Dodge_2,
    Dodge_3,
}

public class CardAsset : ScriptableObject, IComparable<CardAsset>
{
    public string GUID;

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

    [Range(1, 999999999999)]
    public int AccumulativeExperience = 500;

    [Range(1, 999999999)]
    public int ExperienceRequiredNextLevel = 1000;

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


    private int FindCurrentLevel(
        int StartExp, int EndExp, int BaseLevel, int NumberOfLevel
        )
    {
        int ExpPerLevel = (EndExp - StartExp) / NumberOfLevel;
        int TopLevels = (AccumulativeExperience - StartExp) / ExpPerLevel;
        return BaseLevel + TopLevels;
    }

    private int FindNextLevelRequiredExperience(
        int BaseExp, int MaxExp,
        int BaseLevel, int CurrentLevel,
        int ExpPerLevel, int ExpPerLevelNextPhrase
        )
    {
        if (AccumulativeExperience == MaxExp)
        {
            return ExpPerLevelNextPhrase;
        }
        return (CurrentLevel + 1 - BaseLevel) * ExpPerLevel + BaseExp - AccumulativeExperience;
    }

    private CaptainSkills FindCaptainSkillByLevel(int level)
    {
        int captainSkillLevel;
        // evolution at level: 20, 50, 80
        if (level < 20)
        {
            captainSkillLevel = 0;
        }
        else if (level < 50)
        {
            captainSkillLevel = 1;
        }
        else if (level < 80)
        {
            captainSkillLevel = 2;
        }
        else
        {
            captainSkillLevel = 3;
        }
        // find captian skill
        if (captainSkillLevel == 1)
        {
            if (CaptainSkill == CaptainSkills.PhysicalAttack_0)
            {
                return CaptainSkills.PhysicalAttack_1;
            }
            else if (CaptainSkill == CaptainSkills.SpellAttack_0)
            {
                return CaptainSkills.SpellAttack_1;
            }
            else if (CaptainSkill == CaptainSkills.PhysicalDefense_0)
            {
                return CaptainSkills.PhysicalDefense_1;
            }
            else if (CaptainSkill == CaptainSkills.SpellDefense_0)
            {
                return CaptainSkills.SpellDefense_1;
            }
            else if (CaptainSkill == CaptainSkills.Heal_0)
            {
                return CaptainSkills.Heal_1;
            }
            else if (CaptainSkill == CaptainSkills.Dodge_0)
            {
                return CaptainSkills.Dodge_1;
            }
        }
        else if (captainSkillLevel == 2)
        {
            if (CaptainSkill == CaptainSkills.PhysicalAttack_0)
            {
                return CaptainSkills.PhysicalAttack_2;
            }
            else if (CaptainSkill == CaptainSkills.SpellAttack_0)
            {
                return CaptainSkills.SpellAttack_2;
            }
            else if (CaptainSkill == CaptainSkills.PhysicalDefense_0)
            {
                return CaptainSkills.PhysicalDefense_2;
            }
            else if (CaptainSkill == CaptainSkills.SpellDefense_0)
            {
                return CaptainSkills.SpellDefense_2;
            }
            else if (CaptainSkill == CaptainSkills.Heal_0)
            {
                return CaptainSkills.Heal_2;
            }
            else if (CaptainSkill == CaptainSkills.Dodge_0)
            {
                return CaptainSkills.Dodge_2;
            }
        }
        else if (captainSkillLevel == 3)
        {
            if (CaptainSkill == CaptainSkills.PhysicalAttack_0)
            {
                return CaptainSkills.PhysicalAttack_3;
            }
            else if (CaptainSkill == CaptainSkills.SpellAttack_0)
            {
                return CaptainSkills.SpellAttack_3;
            }
            else if (CaptainSkill == CaptainSkills.PhysicalDefense_0)
            {
                return CaptainSkills.PhysicalDefense_3;
            }
            else if (CaptainSkill == CaptainSkills.SpellDefense_0)
            {
                return CaptainSkills.SpellDefense_3;
            }
            else if (CaptainSkill == CaptainSkills.Heal_0)
            {
                return CaptainSkills.Heal_3;
            }
            else if (CaptainSkill == CaptainSkills.Dodge_0)
            {
                return CaptainSkills.Dodge_3;
            }
        }
        return CaptainSkill;
    }

    private void UpdateAbilityByLevel(int Level, CaptainSkills captainSkills)
    {
        float extraFactor;
        RarityOptions rarity = Rarity;
        switch (rarity)
        {
            case RarityOptions.Basic:
                extraFactor = 1.0f;
                break;
            case RarityOptions.Common:
                extraFactor = 1.05f;
                break;
            case RarityOptions.Rare:
                extraFactor = 1.1f;
                break;
            case RarityOptions.Epic:
                extraFactor = 1.2f;
                break;
            case RarityOptions.Legendary:
                extraFactor = 1.3f;
                break;
            default:
                extraFactor = 1.0f;
                break;
        }

        int maxHealth = MaxHealth;
        float hitPercentage = HitPercentage;
        float dodgePercentage = DodgePercentage;
        int attack = Attack;
        int defense = Defense;
        int attacksForOneTurn = AttacksForOneTurn;
        int numberOfTargeting = NumberOfTargeting;

        // health
        maxHealth += (int)Math.Ceiling(Level * 100 * extraFactor);

        // hit percentage
        hitPercentage += (Level - 1) * 0.01f;
        if (attacksForOneTurn > 1)
        {
            hitPercentage -= 0.05f * attacksForOneTurn;
        }
        if (numberOfTargeting > 1)
        {
            hitPercentage -= 0.05f * numberOfTargeting;
        }
        hitPercentage *= extraFactor;
        if (hitPercentage > 1.0f) hitPercentage = 1.0f;

        // dodge percentage
        dodgePercentage += (Level - 1) * 0.005f;
        if (captainSkills == CaptainSkills.Dodge_1)
        {
            dodgePercentage += 0.05f;
        }
        else if (captainSkills == CaptainSkills.Dodge_2)
        {
            dodgePercentage += 0.75f;
        }
        else if (captainSkills == CaptainSkills.Dodge_3)
        {
            dodgePercentage += 0.1f;
        }
        dodgePercentage *= extraFactor;
        if (dodgePercentage > 0.5f) dodgePercentage = 0.5f;

        // attack
        attack += (Level - 1) * 10;
        if (captainSkills == CaptainSkills.PhysicalAttack_1)
        {
            attack += 100;
        }
        else if (captainSkills == CaptainSkills.PhysicalAttack_2)
        {
            attack += 150;
        }
        else if (captainSkills == CaptainSkills.PhysicalAttack_3)
        {
            attack += 250;
        }
        else if (captainSkills == CaptainSkills.SpellAttack_1)
        {
            attack += 120;
        }
        else if (captainSkills == CaptainSkills.SpellAttack_2)
        {
            attack += 180;
        }
        else if (captainSkills == CaptainSkills.SpellAttack_3)
        {
            attack += 300;
        }
        else if (captainSkills == CaptainSkills.Heal_1)
        {
            attack += 100;
        }
        else if (captainSkills == CaptainSkills.Heal_2)
        {
            attack += 130;
        }
        else if (captainSkills == CaptainSkills.Heal_3)
        {
            attack += 180;
        }
        attack = (int)Math.Floor(attack * extraFactor);

        // defense
        defense += (Level - 1) * 10;
        if (captainSkills == CaptainSkills.PhysicalDefense_1)
        {
            defense += 50;
        }
        else if (captainSkills == CaptainSkills.PhysicalDefense_2)
        {
            defense += 75;
        }
        else if (captainSkills == CaptainSkills.PhysicalDefense_3)
        {
            defense += 125;
        }
        else if (captainSkills == CaptainSkills.SpellDefense_1)
        {
            defense += 60;
        }
        else if (captainSkills == CaptainSkills.SpellDefense_2)
        {
            defense += 90;
        }
        else if (captainSkills == CaptainSkills.SpellDefense_3)
        {
            defense += 150;
        }
        defense = (int)Math.Floor(defense * extraFactor);

        this.MaxHealth = maxHealth;
        this.HitPercentage = hitPercentage;
        this.DodgePercentage = dodgePercentage;
        this.Attack = attack;
        this.Defense = defense;
    }

    // We only store accumulative experience in the user prefs
    // So we can update other attributes based on exp
    public void UpdateCardAssetByAccumulativeExperience(
        int AccumulativeExperience
    )
    {
        Guid uuid = new Guid();
        this.GUID = uuid.ToString();

        int level;
        int experienceRequiredNextLevel;
        // level 1-10 (each level cost 1000)
        // 1000 * 9 = 9000
        if (AccumulativeExperience <= 9000)
        {
            level = FindCurrentLevel(0, 9000, 1, 9);
            experienceRequiredNextLevel = FindNextLevelRequiredExperience(
                0, 9000, 1, level, 1000, 2000
                );
        }
        // level 11-20 (each level cost 2000)
        // 9000 + 2000 * 10 = 29000
        else if (AccumulativeExperience <= 29000)
        {
            level = FindCurrentLevel(9000, 29000, 10, 10);
            experienceRequiredNextLevel = FindNextLevelRequiredExperience(
                9000, 29000, 10, level, 2000, 3000
                );
        }
        // (evolution at level 20)
        // level 21-30 (each level cost 3000)
        // 29000 + 3000 * 10 = 59000
        else if (AccumulativeExperience <= 59000)
        {
            level = FindCurrentLevel(29000, 59000, 20, 10);
            experienceRequiredNextLevel = FindNextLevelRequiredExperience(
                29000, 59000, 20, level, 3000, 4000
                );
        }
        // level 31-40 (each level cost 4000)
        // 59000 + 4000 * 10 = 99000
        else if (AccumulativeExperience <= 99000)
        {
            level = FindCurrentLevel(59000, 99000, 30, 10);
            experienceRequiredNextLevel = FindNextLevelRequiredExperience(
                59000, 99000, 30, level, 4000, 5000
                );
        }
        // level 41-50 (each level cost 5000)
        // 99000 + 5000 * 10 = 149000
        else if (AccumulativeExperience <= 149000)
        {
            level = FindCurrentLevel(99000, 149000, 40, 10);
            experienceRequiredNextLevel = FindNextLevelRequiredExperience(
                99000, 149000, 40, level, 5000, 6000
                );
        }
        // (evolution - merge x 3, at level 50)
        // level 51-60 (each level cost 6000)
        // 149000 * 3 + 6000 * 10 = 507000
        else if (AccumulativeExperience <= 507000)
        {
            level = FindCurrentLevel(149000 * 3, 507000, 50, 10);
            experienceRequiredNextLevel = FindNextLevelRequiredExperience(
                149000 * 3, 507000, 50, level, 6000, 7000
                );
        }
        // level 61-70 (each level cost 7000)
        // 507000 + 7000 * 10 = 577000
        else if (AccumulativeExperience <= 577000)
        {
            level = FindCurrentLevel(507000, 577000, 60, 10);
            experienceRequiredNextLevel = FindNextLevelRequiredExperience(
                507000, 577000, 60, level, 7000, 8000
                );
        }
        // level 71-80 (each level cost 8000)
        // 577000 + 8000 * 10 = 657000
        else if (AccumulativeExperience <= 657000)
        {
            level = FindCurrentLevel(577000, 657000, 70, 10);
            experienceRequiredNextLevel = FindNextLevelRequiredExperience(
                577000, 657000, 70, level, 8000, 9000
                );
        }
        // (evolution - merge x 3, at level 80)
        // level 81-90 (each level cost 9000)
        // 657000 * 3 + 9000 * 10 = 2061000
        else if (AccumulativeExperience <= 2061000)
        {
            level = FindCurrentLevel(657000 * 3, 2061000, 80, 10);
            experienceRequiredNextLevel = FindNextLevelRequiredExperience(
                657000 * 3, 2061000, 80, level, 9000, 10000
                );
        }
        // level 91-100 (each level cost 10000)
        // 2061000 + 10000 * 10 = 2161000
        else if (AccumulativeExperience <= 2161000)
        {
            level = FindCurrentLevel(2061000, 2161000, 90, 10);
            experienceRequiredNextLevel = FindNextLevelRequiredExperience(
                2061000, 2161000, 90, level, 10000, 999999999
                );
        }
        else
        {
            throw new Exception(
                "Invalid accumulative experience: " + AccumulativeExperience
                );
        }

        CaptainSkills captainSkills = FindCaptainSkillByLevel(level);
        UpdateAbilityByLevel(level, captainSkills);

        this.Level = level;
        this.AccumulativeExperience = AccumulativeExperience;
        this.ExperienceRequiredNextLevel = experienceRequiredNextLevel;
        this.CaptainSkill = captainSkills;
    }

    // Exp will receive if it's defeated
    public int DefeatExperience()
    {
        return this.AccumulativeExperience / 5;
    }

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
