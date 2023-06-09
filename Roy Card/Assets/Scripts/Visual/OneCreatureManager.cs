﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class OneCreatureManager : MonoBehaviour
{
    public CardAsset cardAsset;
    public OneCardManager PreviewManager;
    [Header("Text Component References")]
    public Text HealthText;
    public Text AttackText;
    [Header("Image References")]
    public Image CreatureGraphicImage;
    public Image CreatureGlowImage;
    public bool Taunt = false;

    private float glowPulse = 1f;

    void Awake()
    {
        if (cardAsset != null)
            ReadCreatureFromAsset();

        CreatureGlowImage.DOFade(0.4f, glowPulse).SetLoops(-1, LoopType.Yoyo);
    }

    private bool canAttackNow = false;
    public bool CanAttackNow
    {
        get
        {
            return canAttackNow;
        }

        set
        {
            canAttackNow = value;

            CreatureGlowImage.enabled = value;
        }
    }


    public void ReadCreatureFromAsset()
    {
        // Change the card graphic sprite
        CreatureGraphicImage.sprite = cardAsset.CardImage;

        AttackText.text = cardAsset.Attack.ToString();
        HealthText.text = cardAsset.MaxHealth.ToString();

        if (PreviewManager != null)
        {
            PreviewManager.cardAsset = cardAsset;
            PreviewManager.ReadCardFromAsset();
        }

        // TODO: this info should be taken not from the asset, but from the CardLogic object.
        //Taunt = cardAsset.Taunt;
    }

    public void TakeDamage(int amount, int healthAfter)
    {
        if (amount > 0)
        {
            DamageEffect.CreateDamageEffect(transform.position, amount);
            HealthText.text = healthAfter.ToString();
        }
    }
}
