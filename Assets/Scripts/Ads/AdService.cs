using System;
using Unity.Services.Core;
using Unity.Services.Mediation;
using UnityEngine;


public class AdService
{
    private IRewardedAd _adRewarded;
    private IInterstitialAd _adInterstitial;
    private GameManager _gameManager;
    private string _adRewardedUnitId = "AdMob";
    private string _adInterstitialUnitId = "AdMob_Interstitial";
    private string _gameId = "4716511";

    public async void InitServices()
    {
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        try
        {
            InitializationOptions initializationOptions = new InitializationOptions();
            initializationOptions.SetGameId(_gameId);
            await UnityServices.InitializeAsync(initializationOptions);

            InitializationComplete();
        }
        catch (Exception e)
        {
            InitializationFailed(e);
        }
    }

    public void SetupAd()
    {
        //Create
        _adRewarded = MediationService.Instance.CreateRewardedAd(_adRewardedUnitId);
        _adInterstitial = MediationService.Instance.CreateInterstitialAd(_adInterstitialUnitId);


        //Subscribe to events
        _adRewarded.OnLoaded += AdLoaded;
        _adRewarded.OnFailedLoad += AdFailedLoad;

        _adRewarded.OnShowed += AdShown;
        _adRewarded.OnFailedShow += AdFailedShow;
        _adRewarded.OnClosed += AdClosed;
        _adRewarded.OnClicked += AdClicked;
        _adRewarded.OnUserRewarded += UserRewarded;

        //Subscribe to events
        _adInterstitial.OnLoaded += AdLoaded;
        _adInterstitial.OnFailedLoad += AdFailedLoad;

        _adInterstitial.OnShowed += AdShown;
        _adInterstitial.OnFailedShow += AdFailedShow;
        _adInterstitial.OnClosed += AdClosed;
        _adInterstitial.OnClicked += AdClicked;

        // Impression Event
        MediationService.Instance.ImpressionEventPublisher.OnImpression += ImpressionEvent;
    }

    public void ShowRewardedAd()
    {
        if (_adRewarded.AdState == AdState.Loaded)
        {
            _adRewarded.Show();
        }
    }
    public void ShowInterstitialAd()
    {
        if (_adInterstitial.AdState == AdState.Loaded)
        {
            _adInterstitial.Show();
        }
    }

    void InitializationComplete()
    {
        SetupAd();
        _adRewarded.Load();
        _adInterstitial.Load();
    }

    void InitializationFailed(Exception e)
    {
        Debug.Log("Initialization Failed: " + e.Message);
    }

    void AdLoaded(object sender, EventArgs args)
    {
        Debug.Log("Ad loaded");
    }

    void AdFailedLoad(object sender, LoadErrorEventArgs args)
    {
        Debug.Log("Failed to load ad");
        Debug.Log(args.Message);
    }

    void AdShown(object sender, EventArgs args)
    {
        Debug.Log("Ad shown!");
    }

    void AdClosed(object sender, EventArgs e)
    {
        // Pre-load the next ad
        _adRewarded.Load();
        Debug.Log("Ad has closed");
        // Execute logic after an ad has been closed.
    }

    void AdClicked(object sender, EventArgs e)
    {
        Debug.Log("Ad has been clicked");
        // Execute logic after an ad has been clicked.
    }

    void AdFailedShow(object sender, ShowErrorEventArgs args)
    {
        Debug.Log(args.Message);
    }

    void ImpressionEvent(object sender, ImpressionEventArgs args)
    {
        var impressionData = args.ImpressionData != null ? JsonUtility.ToJson(args.ImpressionData, true) : "null";
        Debug.Log("Impression event from ad unit id " + args.AdUnitId + " " + impressionData);
    }

    void UserRewarded(object sender, RewardEventArgs e)
    {
        _gameManager.DoublePoints();
    }

}