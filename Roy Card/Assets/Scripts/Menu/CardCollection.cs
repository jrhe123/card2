using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class CardCollection : MonoBehaviour
{
    public static CardCollection Instance;

    // store all the avaliable card assets
    private CardAsset[] AllCardArray;

    private Dictionary<string, CardAsset> AllCardsDictionary =
        new Dictionary<string, CardAsset>();

    public List<CardAsset> OwnedCardList = new List<CardAsset>();

    void Awake()
    {
        Instance = this;
        AllCardArray = Resources.LoadAll<CardAsset>("");
        foreach (CardAsset ca in AllCardArray)
        {
            if (!AllCardsDictionary.ContainsKey(ca.name))
            {
                AllCardsDictionary.Add(ca.name, ca);
            }
        }
        LoadOwnedCardsFromPlayerPrefs();
    }

    private void ClearOwnedCardsFromPlayerPrefs()
    {
        foreach (CardAsset ca in AllCardArray)
        {
            PlayerPrefs.DeleteKey(ca.name);
        }
    }

    private void LoadOwnedCardsFromPlayerPrefs()
    {
        foreach (CardAsset ca in AllCardArray)
        {
            if (PlayerPrefs.GetString(ca.name, "") != "")
            {
                string data = PlayerPrefs.GetString(ca.name);
                List<string> experiences = data.Split('_').ToList();

                foreach (string expStr in experiences)
                {
                    ca.UpdateCardAssetByAccumulativeExperience(
                        int.Parse(expStr)
                        );
                    OwnedCardList.Add(ca);
                }
            }
        }
    }

    private void SaveOwnedCardsIntoPlayerPrefs()
    {
        ClearOwnedCardsFromPlayerPrefs();

        /**
         * NinjaGirl: 100_200_3999
         * ZombieMale: 99_50
         * ...
         */
        foreach (CardAsset ca in OwnedCardList)
        {
            string data;
            if (PlayerPrefs.GetString(ca.name, "") != "")
            {
                data = PlayerPrefs.GetString(ca.name);
                data += "_" + ca.AccumulativeExperience;
            } else
            {
                data = ca.AccumulativeExperience.ToString();
            }
            PlayerPrefs.SetString(ca.name, data);
        }
    }

    void OnApplicationQuit()
    {
        SaveOwnedCardsIntoPlayerPrefs();
    }

    public CardAsset GetCardAssetByName(string name)
    {
        // if there is a card with this name, return its CardAsset
        if (AllCardsDictionary.ContainsKey(name))
            return AllCardsDictionary[name];
        else        // if there is no card with name
            return null;
    }

    public List<CardAsset> GetCardsOfCharacter(CharacterAsset asset)
    {
        /*
        // get cards that blong to a particular character or neutral if asset == null
        var cards = from card in AllCardArray
                                    where card.CharacterAsset == asset
                                    select card; 
        
        var returnList = cards.ToList<CardAsset>();
        returnList.Sort();
        */
        return GetCards(true, true, false, RarityOptions.Basic, asset);
    }

    public List<CardAsset> GetCardsWithRarity(RarityOptions rarity)
    {
        /*
        // get neutral cards
        var cards = from card in AllCardArray
                where card.Rarity == rarity
            select card; 

        var returnList = cards.ToList<CardAsset>();
        returnList.Sort();

        return returnList;
        */
        return GetCards(true, false, true, rarity);

    }

    /// the most general method that will use multiple filters
    public List<CardAsset> GetCards(bool showingCardsPlayerDoesNotOwn = false, bool includeAllRarities = true, bool includeAllCharacters = true, RarityOptions rarity = RarityOptions.Basic,
                CharacterAsset asset = null, string keyword = "", int manaCost = -1, bool includeTokenCards = false)
    {
        // initially select all cards
        var cards = from card in AllCardArray select card;

        //if (!showingCardsPlayerDoesNotOwn)
        //    cards = cards.Where(card => QuantityOfEachCard[card] > 0);

        //if (!includeTokenCards)
        //    cards = cards.Where(card => card.TokenCard == false);

        //if (!includeAllRarities)
        //    cards = cards.Where(card => card.Rarity == rarity);

        //if (!includeAllCharacters)
        //    cards = cards.Where(card => card.CharacterAsset == asset);

        //if (keyword != null && keyword != "")
        //    cards = cards.Where(card => (card.name.ToLower().Contains(keyword.ToLower()) ||
        //        (card.Tags.ToLower().Contains(keyword.ToLower()) && !keyword.ToLower().Contains(" "))));

        //if (manaCost == 7)
        //    cards = cards.Where(card => card.ManaCost >= 7);
        //else if (manaCost != -1)
        //    cards = cards.Where(card => card.ManaCost == manaCost);

        var returnList = cards.ToList<CardAsset>();
        returnList.Sort();

        return returnList;
    }
}
