using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener 
{

#if UNITY_IOS
    string GAME_ID = "4851104"; // 4851104

#elif UNITY_ANDROID
    string GAME_ID = "4851105"; // 4851105

#endif

    private const string BANNER_PLACEMENT = "Banner_Android";

    private const string VIDEO_PLACEMENT = "Interstitial_Android";

    private const string REWARDED_VIDEO_PLACEMENT = "Rewarded_Android";

    [SerializeField] private BannerPosition bannerPosition = BannerPosition.BOTTOM_CENTER;

    private bool testMode = true;
    private bool showBanner = false;

    //utility wrappers for debuglog
    public delegate void DebugEvent(string msg);
    public static event DebugEvent OnDebugLog;

    // Rewarded Ads Script
    // public RewardedAds rewardedAds;  // ! TODO: Uncomment

    //private void Awake()
    //{
        //if you didn't assign in the inspector
    //     if (!rewardedAds == null) // ! TODO: Uncomment
    //     {
    //         rewardedAds = FindObjectOfType<RewardedAds>();
    //     }
    // }

    public void Initialize()
    {
        if (Advertisement.isSupported)
        {
            DebugLog(Application.platform + " supported by Advertisement");
        }
        Advertisement.Initialize(GAME_ID, testMode, this);
    }

    // public void ToggleBanner() 
    // {
    //     showBanner = !showBanner;

    //     if (showBanner)
    //     {
    //         Advertisement.Banner.SetPosition(bannerPosition);
    //         Advertisement.Banner.Show(BANNER_PLACEMENT);
    //     }
    //     else
    //     {
    //         Advertisement.Banner.Hide(false);
    //     }
    // }

    // show banner
    public void ShowBanner()
    {
        Advertisement.Banner.SetPosition(bannerPosition);
        Advertisement.Banner.Show(BANNER_PLACEMENT);
    }

    // hide banner
    public void HideBanner()
    {
        Advertisement.Banner.Hide(false);
    }

    // Loading Rewareded Video Ad
    public void LoadRewardedAd()
    {
        Advertisement.Load(REWARDED_VIDEO_PLACEMENT, this);
    }

    // Showing Rewareded Video Ad
    public void ShowRewardedAd()
    {
        Advertisement.Show(REWARDED_VIDEO_PLACEMENT, this);
    }

    // Loading Interstitial Ad
    public void LoadNonRewardedAd()
    {
        Advertisement.Load(VIDEO_PLACEMENT, this);
    }

    // Showing Interstitial Ad
    public void ShowNonRewardedAd()
    {
        Advertisement.Show(VIDEO_PLACEMENT, this);
    }

    #region Interface Implementations
    public void OnInitializationComplete()
    {
        DebugLog("Init Success");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        DebugLog($"Init Failed: [{error}]: {message}");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        DebugLog($"Load Success: {placementId}");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        DebugLog($"Load Failed: [{error}:{placementId}] {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        DebugLog($"OnUnityAdsShowFailure: [{error}]: {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        DebugLog($"OnUnityAdsShowStart: {placementId}");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        DebugLog($"OnUnityAdsShowClick: {placementId}");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        DebugLog($"OnUnityAdsShowComplete: [{showCompletionState}]: {placementId}");

        // Reward the player for watching the ad
        //rewardedAds.AfterWatchingRewardedAd(); // ! TODO: uncomment
    }
    #endregion

    public void OnGameIDFieldChanged(string newInput)
    {
        GAME_ID = newInput;
    }

    public void ToggleTestMode(bool isOn)
    {
        testMode = isOn;
    }

    //wrapper around debug.log to allow broadcasting log strings to the UI
    void DebugLog(string msg)
    {
        OnDebugLog?.Invoke(msg);
        Debug.Log(msg);
    }
}
