using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharClass {
    Other,

    Zombie, Ork, Monster, Robot, Knight, Ninja, Human,
}

public class CharacterAsset : ScriptableObject
{
    public CharClass Class;
    public string ClassName;

    public int MaxHealth = 999999;
    public string HeroPowerName;

    public Sprite AvatarImage;
    public Sprite AvatarBGImage;
    public Sprite HeroTypeIconImage;
    public Sprite HeroTypeBGImage;

    public Color32 AvatarBGTint;
    public Color32 HeroTypeBGTint;
    public Color32 ClassCardTint;
}
