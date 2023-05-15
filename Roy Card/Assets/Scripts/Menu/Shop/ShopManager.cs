using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShopManager : MonoBehaviour
{

    public GameObject ScreenContent;
    public GameObject PackPrefab;

    public int PackPrice;
    public Transform PacksParent;
    public Transform InitialPackSpot;
    public float PosXRange = 4f;
    public float PosYRange = 8f;
    public float RotationRange = 10f;

    public Text MoneyText;
    public Text DustText;
    public GameObject MoneyHUD;
    public GameObject DustHUD;
    public PackOpeningArea OpeningArea;

    public int StartingAmountOfDust = 1000;
    public int StartingAmountOfMoney = 1000;

    public static ShopManager Instance;

    public int PacksCreated { get; set; }

    private float packPlacementOffset = -0.01f;

    void Awake()
    {
        Instance = this;
        HideScreen();

        if (PlayerPrefs.HasKey("UnopenedPacks"))
        {
            int unOpenedPacks = PlayerPrefs.GetInt("UnopenedPacks");

            // todo: b/c of the hide screen, StartCoroutine failed
            Debug.Log("+++++ ShopManager");
            Debug.Log("+++++ ShopManager unOpenedPacks: " + unOpenedPacks);

            StartCoroutine(
                GivePacks(unOpenedPacks, true)
                );
        }

        // load dust & moeny from player pref
        LoadDustAndMoneyToPlayerPrefs();
    }

    private int money;
    public int Money
    {
        get { return money; }
        set
        {
            money = value;
            MoneyText.text = money.ToString();
        }
    }

    private int dust;
    public int Dust
    {
        get { return dust; }
        set
        {
            dust = value;
            DustText.text = dust.ToString();
        }
    }

    public void BuyPack()
    {
        if (money >= PackPrice)
        {
            //Money -= PackPrice;
            // not instant, animation
            StartCoroutine(GivePacks(1));
        }
    }

    public IEnumerator GivePacks(int NumberOfPacks, bool instant = false)
    {
        for (int i = 0; i < NumberOfPacks; i++)
        {
            GameObject newPack = Instantiate(PackPrefab, PacksParent);

            // random position for new pack
            Vector3 localPositionForNewPack = new Vector3(
                Random.Range(-PosXRange, PosXRange),
                Random.Range(-PosYRange, PosYRange) + 2,
                PacksCreated * packPlacementOffset // offset
                );

            Debug.Log("+++++ ShopManager");
            Debug.Log("+++++ ShopManager localPositionForNewPack: " + localPositionForNewPack);
           
            newPack.transform.localEulerAngles = new Vector3(
                0f,
                0f,
                Random.Range(-RotationRange, RotationRange)
                );

            //
            PacksCreated++;

            // make this pack appear on top of all the previous packs using PacksCreated;
            newPack.GetComponentInChildren<Canvas>().sortingOrder = PacksCreated;

            //
            if (instant)
                newPack.transform.localPosition = localPositionForNewPack;
            else
            {
                newPack.transform.position = InitialPackSpot.position;
                newPack.transform.DOLocalMove(localPositionForNewPack, 0.5f);
                //
                yield return new WaitForSeconds(0.5f);
            }
        }
        yield break;
    }

    void OnApplicationQuit()
    {
        SaveDustAndMoneyToPlayerPrefs();
        PlayerPrefs.SetInt("UnopenedPacks", PacksCreated);
    }

    public void LoadDustAndMoneyToPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("Dust"))
            Dust = PlayerPrefs.GetInt("Dust");
        else
            Dust = StartingAmountOfDust;  // default value of dust to give to player

        if (PlayerPrefs.HasKey("Money"))
            Money = PlayerPrefs.GetInt("Money");
        else
            Money = StartingAmountOfMoney;  // default value of dust to give to player
    }

    public void SaveDustAndMoneyToPlayerPrefs()
    {
        PlayerPrefs.SetInt("Dust", dust);
        PlayerPrefs.SetInt("Money", money);
    }

    public void ShowScreen()
    {
        ScreenContent.SetActive(true);
        MoneyHUD.SetActive(true);
    }

    public void HideScreen()
    {
        ScreenContent.SetActive(false);
        MoneyHUD.SetActive(false);
    }
}
