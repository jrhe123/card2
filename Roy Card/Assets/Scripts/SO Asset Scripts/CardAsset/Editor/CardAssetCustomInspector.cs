using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CardAsset)), CanEditMultipleObjects]
public class CardAssetCustomInspector : Editor
{
    public SerializedProperty
    CharacterAsset_prop,
    Description_prop,
    Tags_prop,
    CardImage_prop,

    CaptainSkill_prop,
    IsCaptain_prop,

    Level_prop,
    AccumulativeExperience_prop,
    ExperienceRequiredNextLevel_prop,
    EvolutionLevel_prop,
    EvoluationCondition_prop,
    EvolutedCardAsset_prop,
    HitPercentage_prop,
    DodgePercentage_prop,

    MaxHealth_prop,
    Attack_prop,
    Defense_prop,
    AttackType_prop,
    DefenseType_prop,
    AttacksForOneTurn_prop,
    NumberOfTargeting_prop,

    CreatureScriptName_prop,
    ExperienceScriptName_prop,
    WeaponScriptName_prop,

    TypeOfCard_prop,
    Rarity_prop,
    Targets_prop
    ;

    void OnEnable()
    {
        // Setup the SerializedProperties

        // all the common general fields
        CharacterAsset_prop = serializedObject.FindProperty("CharacterAsset");
        Description_prop = serializedObject.FindProperty("Description");
        Tags_prop = serializedObject.FindProperty("Tags");
        CardImage_prop = serializedObject.FindProperty("CardImage");

        CaptainSkill_prop = serializedObject.FindProperty("CaptainSkill");
        IsCaptain_prop = serializedObject.FindProperty("IsCaptain");

        // all the creature fields:
        Level_prop = serializedObject.FindProperty("Level");
        AccumulativeExperience_prop = serializedObject.FindProperty("AccumulativeExperience");
        ExperienceRequiredNextLevel_prop = serializedObject.FindProperty("ExperienceRequiredNextLevel");
        EvolutionLevel_prop = serializedObject.FindProperty("EvolutionLevel");
        EvoluationCondition_prop = serializedObject.FindProperty("EvoluationCondition");
        EvolutedCardAsset_prop = serializedObject.FindProperty("EvolutedCardAsset");
        HitPercentage_prop = serializedObject.FindProperty("HitPercentage");
        DodgePercentage_prop = serializedObject.FindProperty("DodgePercentage");
        MaxHealth_prop = serializedObject.FindProperty("MaxHealth");
        Attack_prop = serializedObject.FindProperty("Attack");
        Defense_prop = serializedObject.FindProperty("Defense");
        AttackType_prop = serializedObject.FindProperty("AttackType");
        DefenseType_prop = serializedObject.FindProperty("DefenseType");
        AttacksForOneTurn_prop = serializedObject.FindProperty("AttacksForOneTurn");
        NumberOfTargeting_prop = serializedObject.FindProperty("NumberOfTargeting");

        // custom scripts
        CreatureScriptName_prop = serializedObject.FindProperty("CreatureScriptName");
        ExperienceScriptName_prop = serializedObject.FindProperty("ExperienceScriptName");
        WeaponScriptName_prop = serializedObject.FindProperty("WeaponScriptName");

        // types
        TypeOfCard_prop = serializedObject.FindProperty("TypeOfCard");
        Rarity_prop = serializedObject.FindProperty("Rarity");
        Targets_prop = serializedObject.FindProperty("Targets");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(CharacterAsset_prop);
        EditorGUILayout.PropertyField(Description_prop);
        EditorGUILayout.PropertyField(Tags_prop);
        EditorGUILayout.PropertyField(CardImage_prop);

        EditorGUILayout.PropertyField(TypeOfCard_prop);
        EditorGUILayout.PropertyField(Rarity_prop);
        EditorGUILayout.PropertyField(Targets_prop);

        TypesOfCards st = (TypesOfCards)TypeOfCard_prop.enumValueIndex;

        switch (st)
        {
            case TypesOfCards.Creature:
                EditorGUILayout.PropertyField(CaptainSkill_prop);
                EditorGUILayout.PropertyField(IsCaptain_prop);

                EditorGUILayout.PropertyField(Level_prop);
                EditorGUILayout.PropertyField(AccumulativeExperience_prop);
                EditorGUILayout.PropertyField(ExperienceRequiredNextLevel_prop);
                EditorGUILayout.PropertyField(EvolutionLevel_prop);
                EditorGUILayout.PropertyField(EvoluationCondition_prop);
                EditorGUILayout.PropertyField(EvolutedCardAsset_prop);
                EditorGUILayout.PropertyField(HitPercentage_prop);
                EditorGUILayout.PropertyField(DodgePercentage_prop);
                EditorGUILayout.PropertyField(MaxHealth_prop);
                EditorGUILayout.PropertyField(Attack_prop);
                EditorGUILayout.PropertyField(Defense_prop);
                EditorGUILayout.PropertyField(AttackType_prop);
                EditorGUILayout.PropertyField(DefenseType_prop);
                EditorGUILayout.PropertyField(AttacksForOneTurn_prop);

                EditorGUILayout.PropertyField(NumberOfTargeting_prop);

                EditorGUILayout.PropertyField(CreatureScriptName_prop);
                break;

            case TypesOfCards.Experience:
                EditorGUILayout.PropertyField(NumberOfTargeting_prop);

                EditorGUILayout.PropertyField(ExperienceScriptName_prop);
                break;

            case TypesOfCards.Weapon:
                EditorGUILayout.PropertyField(WeaponScriptName_prop);
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }

}
