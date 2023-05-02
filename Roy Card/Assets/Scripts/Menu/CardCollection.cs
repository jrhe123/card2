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

    // get owned card asset by guid
    public CardAsset GetCardAssetByName(string guid)
    {
        return OwnedCardList.Find(card => card.GUID == guid);
    }

    // search in owned card assets
    public List<CardAsset> GetOwnedCards(
        string keyword = ""
        )
    {
        List<CardAsset> result = OwnedCardList;
        if (keyword != null && keyword != "")
        {
            result = result.FindAll(card => (
                    card.name.ToLower().Contains(keyword.ToLower()) ||
                    (
                        card.Tags.ToLower().Contains(keyword.ToLower()) &&
                        !keyword.ToLower().Contains(" ")
                    )
                ));
        }
        return result;
    }

    // search in all card assets
    public List<CardAsset> GetCards(
        string keyword = ""
        )
    {
        // initially select all cards
        var cards = from card in AllCardArray select card;

        if (keyword != null && keyword != "")
        {
            cards = cards.Where(
                card =>
                (
                    card.name.ToLower().Contains(keyword.ToLower()) ||
                    (
                        card.Tags.ToLower().Contains(keyword.ToLower()) &&
                        !keyword.ToLower().Contains(" ")
                    )
                )
            );
        }

        var returnList = cards.ToList<CardAsset>();
        returnList.Sort();

        return returnList;
    }
}
