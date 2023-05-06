using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

public class Deck : MonoBehaviour
{

    public List<CardAsset> cards = new List<CardAsset>();

    private static System.Random rng = new System.Random();

    void Awake()
    {
        //cards.Shuffle();
        int n = cards.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            CardAsset value = cards[k];
            cards[k] = cards[n];
            cards[n] = value;
        }
    }

}
